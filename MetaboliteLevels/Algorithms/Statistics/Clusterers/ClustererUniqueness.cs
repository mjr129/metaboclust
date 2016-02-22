using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Algorithms.Statistics.Arguments;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Data.DataInfo;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Utilities;
using RefAss = System.Tuple<MetaboliteLevels.Data.DataInfo.GroupInfo, MetaboliteLevels.Data.Visualisables.Cluster>;

namespace MetaboliteLevels.Algorithms.Statistics.Clusterers
{
    /// <summary>
    /// A clusterer that finds the unique combinations of existing clusters.
    /// </summary>
    class ClustererUniqueness : ClustererBase
    {
        /// <summary>
        /// 
        /// </summary>
        public ClustererUniqueness(string id, string name)
            : base(id, name)
        {
            Description = "Sorts peaks into clusters based on the clusters to which they have been assigned by a previous clustering algorithm. (The previous algorithm must have been capable of assigning peaks to multiple clusters - such as by creating a vector per experimental group.";
        }

        /// <summary>
        /// 
        /// </summary>
        protected override IEnumerable<Cluster> Cluster(ValueMatrix vmatrix, DistanceMatrix dmatrix, ArgsClusterer args, ConfigurationClusterer tag, ProgressReporter prog)
        {
            ConfigurationClusterer config = ((WeakReference<ConfigurationClusterer>)args.Parameters[0]).GetTarget();
            List<List<Assignment>> uniqueCombinations = new List<List<Assignment>>();
            List<Cluster> newClusters = new List<Cluster>();
            List<ConditionInfo[]> conditions = new List<ConditionInfo[]>();
            List<ObservationInfo[]> observations = new List<ObservationInfo[]>();

            prog.Enter("Finding unique matches");

            for (int vmatIndex = 0; vmatIndex < vmatrix.NumVectors; vmatIndex++)
            {
                Vector vp = vmatrix.Vectors[vmatIndex];
                Peak peak = vp.Peak;
                prog.SetProgress(vmatIndex, vmatrix.NumVectors);

                List<Assignment> assignments = new List<Assignment>(peak.Assignments.List
                                                         .Where(z => config.Results.Clusters.Contains(z.Cluster))
                                                         .OrderBy(z => z.Vector.Group.Id));
                //.Select(z => new RefAss(z.Vector.Group, z.Cluster)));

                int index = FindMatch(uniqueCombinations, assignments);
                Cluster pat;

                if (index == -1)
                {
                    uniqueCombinations.Add(assignments);

                    string name = StringHelper.ArrayToString<Assignment>(assignments, z => z.Vector.Group.DisplayShortName + "." + z.Cluster.ShortName, " / ");

                    pat = new Cluster(name, tag);

                    // Centre (merge centres)
                    IEnumerable<double[]> centres = assignments.Select(z => z.Cluster.Centres.First());
                    pat.Centres.Add(centres.SelectMany(z => z).ToArray());

                    // Vector (merge vectors)
                    if (assignments[0].Vector.Conditions != null)
                    {
                        conditions.Add(assignments.Select(z => z.Vector.Conditions).SelectMany(z => z).ToArray());
                    }
                    else
                    {
                        conditions.Add(null);
                    }

                    if (assignments[0].Vector.Observations != null)
                    {
                        observations.Add(assignments.Select(z => z.Vector.Observations).SelectMany(z => z).ToArray());
                    }
                    else
                    {
                        observations.Add(null);
                    }

                    // Relations (all clusters)
                    pat.Related.AddRange(assignments.Select(z => z.Cluster).Unique());

                    foreach (Cluster pat2 in pat.Related)
                    {
                        if (!pat2.Related.Contains(pat))
                        {
                            pat2.Related.Add(pat);
                        }
                    }

                    index = newClusters.Count;
                    newClusters.Add(pat);
                }


                pat = newClusters[index];

                double[] values = assignments.Select(z => z.Vector.Values).SelectMany(z => z).ToArray();
                Vector v = new Vector(peak, null, conditions[index], observations[index], values, vmatIndex);
                pat.Assignments.Add(new Assignment(v, pat, assignments.Count));
            }

            prog.Leave();

            return newClusters;
        }

        /// <summary>
        /// 
        /// </summary>
        private int FindMatch(List<List<Assignment>> uniqueCombinations, List<Assignment> pats)
        {
            for (int index = 0; index < uniqueCombinations.Count; index++)
            {
                var list = uniqueCombinations[index];
                UiControls.Assert(list.Count == pats.Count);

                if (IsEqual(pats, list))
                {
                    return index;
                }
            }

            return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        private static bool IsEqual(List<Assignment> pats, List<Assignment> list)
        {
            for (int index = 0; index < pats.Count; index++)
            {
                Assignment v = pats[index];
                Assignment t = list[index];

                UiControls.Assert(v.Vector.Group == t.Vector.Group);

                if (t.Cluster != v.Cluster)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        protected override AlgoParameterCollection CreateParamaterDesription()
        {
            return new AlgoParameterCollection(new AlgoParameter("source", EAlgoParameterType.WeakRefConfigurationClusterer));
        }

        public override bool SupportsObservationFilters { get { return false; } }

        public override bool RequiresDistanceMatrix { get { return false; } }

        public override bool SupportsDistanceMetrics { get { return false; } }
    }
}
