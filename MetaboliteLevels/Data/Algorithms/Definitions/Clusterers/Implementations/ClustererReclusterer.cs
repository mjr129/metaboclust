using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Algorithms.Definitions.Clusterers.Implementations.Legacy;
using MetaboliteLevels.Data.Algorithms.General;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Types.General;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Data.Algorithms.Definitions.Clusterers.Implementations
{
    /// <summary>
    /// A clusterer that assigns observations to existing centres.
    /// 
    /// The observations can be from a different experimental group but must cover the same time range and use the trendline.
    /// </summary>
    class ClustererReclusterer : ClustererBase
    {
        public ClustererReclusterer(string id, string name)
            : base(id, name)
        {
            Comment = "Performs k-means clustering on vectors starting with the cluster centres at existing cluster assignments.";
        }

        protected override IEnumerable<Cluster> Cluster(IntensityMatrix vmatrix, DistanceMatrix UNUSED, ArgsClusterer args, ConfigurationClusterer tag, ProgressReporter prog)
        {
            ConfigurationClusterer config = ((WeakReference<ConfigurationClusterer>)args.Parameters[0]).GetTargetOrThrow();
            List<Cluster> myClusters = new List<Cluster>();

            // Iterate existing clusters
            prog.Enter("Iterating existing");

            for (int index = 0; index < config.Results.Clusters.Length; index++)
            {
                Cluster cluster = config.Results.Clusters[index];
                prog.SetProgress(index, config.Results.Clusters.Length);

                if (!cluster.States.HasFlag(Session.Associational.Cluster.EStates.Insignificants))
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

            prog.Leave();

            prog.Enter("Assigning peaks");
            LegacyClustererHelper.Assign(vmatrix, myClusters, ECandidateMode.Exemplars, args.Distance, prog);
            prog.Leave();

            Cluster matchCluster = new Cluster("Matches", tag);
            matchCluster.States |= Session.Associational.Cluster.EStates.Insignificants;

            return myClusters;
        }      

        protected override AlgoParameterCollection CreateParamaterDesription()
        {
            return new AlgoParameterCollection( new[] 
            {
                new AlgoParameter(  "Clusters",
                                    "The algorithm that produced set of existing clusters to use as exemplars. The observations can be from a different experimental group but must cover the same time range and use the same trendline.",
                                    AlgoParameterTypes.WeakRefConfigurationClusterer)
            } );
        }

        public override bool RequiresDistanceMatrix { get { return false; } }
        public override bool SupportsDistanceMetrics { get { return true; } }
        public override bool SupportsObservationFilters { get { return true; } }
    }
}
