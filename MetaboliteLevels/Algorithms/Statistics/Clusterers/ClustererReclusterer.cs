using MetaboliteLevels.Data.Visualisables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Algorithms.Statistics.Arguments;
using MetaboliteLevels.Algorithms.Statistics.Clusterers.Legacy;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Data.DataInfo;
using MetaboliteLevels.Settings;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Algorithms.Statistics.Clusterers
{
    /// <summary>
    /// A clusterer that assigns observations to existing centres.
    /// 
    /// The observations can be from a different experimental group but must cover the same time range and use the trendline.
    /// </summary>
    class ClustererReclusterer : ClustererBase
    {
        protected override IEnumerable<Cluster> Cluster(ValueMatrix vmatrix, DistanceMatrix UNUSED, ArgsClusterer args, ConfigurationClusterer tag, IProgressReporter prog)
        {
            ConfigurationClusterer config = ((WeakReference<ConfigurationClusterer>)args.Parameters[0]).GetTargetOrThrow();
            List<Cluster> myClusters = new List<Cluster>();

            // Iterate existing clusters
            prog.ReportProgress("Iterating existing");

            for (int index = 0; index < config.Results.Clusters.Length; index++)
            {
                Cluster cluster = config.Results.Clusters[index];
                prog.ReportProgress(index, config.Results.Clusters.Length);

                if (!cluster.States.HasFlag(Data.Visualisables.Cluster.EStates.Insignificants))
                {
                    // Get the centre
                    double[] centre = cluster.GetCentre(ECentreMode.Average, ECandidateMode.Assignments)[0];

                    // Reorder the centre to match our vmatrix
                    // centre = Reorder(centre, config.Results.VMatrix.Conditions, vmatrix.Conditions);

                    Cluster myCluster = new Cluster(cluster.DisplayName, tag);
                    myCluster.Exemplars.Add(centre);
                    myClusters.Add(myCluster);
                }
            }

            prog.ReportProgress("Assigning peaks");
            LegacyClustererHelper.Assign(vmatrix, myClusters, ECandidateMode.Exemplars, args.Distance, prog);

            Cluster matchCluster = new Cluster("Matches", tag);
            matchCluster.States |= Data.Visualisables.Cluster.EStates.Insignificants;

            return myClusters;
        }

        private double[] Reorder(double[] values, ConditionInfo[] origOrder, ConditionInfo[] newOrder)
        {
            if (origOrder == null || newOrder == null)
            {
                throw new InvalidOperationException("Both clusterers must use the trend.");
            }

            int n = newOrder.Length;

            if (values.Length != n || origOrder.Length != n)
            {
                throw new InvalidOperationException("Input vector size must match existing cluster vector size.");
            }

            double[] result = new double[n];
            bool[] check = new bool[n];

            for (int repIndex = 0; repIndex < n; repIndex++)
            {
                ConditionInfo rep = newOrder[repIndex];

                int origIndex = origOrder.FirstIndexWhere(z => z.Time == rep.Time);

                UiControls.Assert(!check[origIndex], "Can't use same index twice - make sure constraints on input vectors for both clusters do not overlap.");

                result[repIndex] = values[origIndex];

                check[origIndex] = true;
            }

            return result;
        }

        public override AlgoParameters GetParams()
        {
            AlgoParameters.Param param = new AlgoParameters.Param("Existing clusters (input vector size must match)", AlgoParameters.EType.WeakRefConfigurationClusterer);
            AlgoParameters.Param[] @params = { param };

            return new AlgoParameters(AlgoParameters.ESpecial.ClustererIgnoresDistanceMatrix, @params);
        }

        public ClustererReclusterer(string id, string name)
            : base(id, name)
        {
        }
    }
}
