using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.DataInfo;
using MetaboliteLevels.Data.General;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Settings;
using MetaboliteLevels.Data.Session;

namespace MetaboliteLevels.Algorithms.Statistics.Results
{
    /// <summary>
    /// Cache of clustering results.
    /// </summary>
    [Serializable]
    class ResultClusterer : ResultBase
    {
        // Stats keys
        // Assignments
        private const string STAT_ASSIGNMENT_SILHOUETTE_WIDTH = "Silhouette width";
        private const string STAT_ASSIGNMENT_NEXT_NEAREST_CLUSTER = "Next nearest cluster";
        private const string STAT_ASSIGNMENT_SCORE = "Score";
        private const string STAT_ASSIGNMENT_EUCLIDEAN_FROM_AVG = "Euclidean distance";
        private const string STAT_ASSIGNMENT_DISTANCE_FROM_AVG = " distance";

        // Clusters
        private const string STAT_CLUSTER_AVERAGE_HIGHEST_NUM_COMPOUNDS = "Max compounds in pathway";
        private const string STAT_CLUSTER_AVERAGE_NUM_COMPOUNDS = "Number of compounds";
        private const string STAT_CLUSTER_AVERAGE_HIGHEST_NUM_PEAKS = "Max peaks in pathway";
        private const string STAT_CLUSTER_AVERAGE_NUM_PATHWAYS = "Number of pathways";

        // Clusterers
        private const string STAT_CLUSTERER_BIC = "BIC";
        private const string STAT_NUM_VECTORS = "Number of vectors";
        private const string STAT_LENGTH_OF_VECTORS = "Length of vectors";

        // Clusters AND Clusterers
        private const string STAT_ANYGROUP_AVERAGE_SCORE = "Average score";
        private const string STAT_ANYGROUP_AVERAGE_SW = "Average silhouette width";
        private const string STAT_ANYGROUP_AVERAGE_EUCLIDEAN = "Euclidean";
        private const string STAT_ANYGROUP_AVERAGE_DISTANCE = "Distance";
        private const string STAT_ANYGROUP_FILTER_AVERAGE_SCORE = "Average score";
        private const string STAT_ANYGROUP_FILTER_AVERAGE_SW = "Average silhouette width";
        private const string STAT_ANYGROUP_FILTER_AVERAGE_EUCLIDEAN = "Euclidean";
        private const string STAT_ANYGROUP_FILTER_AVERAGE_DISTANCE = "Distance";

        /// <summary>
        /// Clusters (inc. insignificants)
        /// </summary>
        public readonly Cluster[] Clusters;

        /// <summary>
        /// Assignments (exc. insignificants)
        /// </summary>
        public readonly List<Assignment> Assignments = new List<Assignment>();

        /// <summary>
        /// Statistics
        /// </summary>
        public Dictionary<string, double> ClustererStatistics = new Dictionary<string, double>();

        /// <summary>
        /// Constructor
        /// </summary>
        public ResultClusterer(IEnumerable<Cluster> clusters)
        {
            this.Clusters = clusters.ToArray();
        }

        /// <summary>
        /// Clusters (exc. insignificants)
        /// </summary>
        public IEnumerable<Cluster> RealClusters
        {
            get { return Clusters.Where(z => z.States == Cluster.EStates.None); }
        }

        /// <summary>
        /// Action completed - calcula
        /// </summary>
        internal void FinalizeResults(Core core, ConfigurationMetric metric, ValueMatrix vmatrix, DistanceMatrix dmatrix, EClustererStatistics statistics, IProgressReporter prog)
        {
            UiControls.Assert(Assignments.IsEmpty(), "FinalizeResults on ClusterResults already called.");

            ClustererStatistics.Add(STAT_NUM_VECTORS, vmatrix.NumVectors);
            ClustererStatistics.Add(STAT_LENGTH_OF_VECTORS, vmatrix[0].Length);

            // Get the non-insig clusters
            var realClusters = RealClusters.ToArray();

            // Get ALL the assignments
            foreach (Cluster cluster in realClusters)
            {
                Assignments.AddRange(cluster.Assignments.List);
            }

            // Don't calculate metrics?
            if (statistics == EClustererStatistics.None)
            {
                return;
            }

            // If we don't have a DMatrix we should calculate the sil. width manually
            // The DMatrix might be too big to pass to R so its better just to avoid it.
            prog.ReportProgress("Calculating assignment statistics");
            int progressIndicator = 0;
            List<ObsFilter> groupFilters = new List<ObsFilter>();

            // No filter
            groupFilters.Add(null);

            // Defined filters
            if (statistics.HasFlag(EClustererStatistics.IncludePartialVectorsForFilters))
            {
                groupFilters.AddRange(core.ObsFilters);
            }

            // Group filters (if not already)
            if (statistics.HasFlag(EClustererStatistics.IncludePartialVectorsForGroups))
            {
                AllGroupsFilters(core, groupFilters);
            }

            foreach (ObsFilter obsFilter in groupFilters)
            {
                int[] filteredIndices = GetFilterIndices(obsFilter);

                foreach (Assignment ass in Assignments)
                {
                    if (filteredIndices != null)
                    {
                        ass.CurrentStatisticVector = ass.Vector.Values.In2(filteredIndices); // TODO: Not thread safe but it is too slow to use a lookup
                    }
                    else
                    {
                        ass.CurrentStatisticVector = ass.Vector.Values;
                    }
                }

                foreach (Cluster cluster in realClusters)
                {
                    /////////////////////
                    // ASSIGNMENT STATS
                    var centre = cluster.GetCentre(ECentreMode.Average, ECandidateMode.Assignments);
                    double[] centreVector = centre.Count != 0 ? centre[0] : null;

                    if (filteredIndices != null)
                    {
                        centreVector = centreVector.In2(filteredIndices);
                    }

                    foreach (Assignment assignment in cluster.Assignments.List)
                    {
                        prog.ReportProgress(progressIndicator++, Assignments.Count * groupFilters.Count);

                        if (centreVector != null)
                        {
                            if (statistics.HasFlag(EClustererStatistics.EuclideanFromAverage))
                            {
                                // Euclidean
                                assignment.AssignmentStatistics.Add(CreatePartialKey(obsFilter, STAT_ASSIGNMENT_EUCLIDEAN_FROM_AVG), Maths.Euclidean(assignment.CurrentStatisticVector, centreVector));
                            }

                            if (metric != null // Metric available
                                && statistics.HasFlag(EClustererStatistics.DistanceFromAverage) // Set to do
                                && !(metric.Id == Algo.ID_METRIC_EUCLIDEAN && statistics.HasFlag(EClustererStatistics.EuclideanFromAverage))) // Not done already!
                            {
                                // Distance
                                assignment.AssignmentStatistics.Add(CreatePartialKey(obsFilter, metric.ToString() + STAT_ASSIGNMENT_DISTANCE_FROM_AVG), metric.Calculate(assignment.CurrentStatisticVector, centreVector));
                            }
                        }

                        double silhouetteWidth;
                        Cluster nextNearestCluster = null;
                        double nextNearestClusterId;

                        if (statistics.HasFlag(EClustererStatistics.SilhouetteWidth))
                        {
                            ClustererStatisticsHelper.CalculateSilhouette(assignment, realClusters, metric, out silhouetteWidth, out nextNearestCluster);

                            if (!double.TryParse(nextNearestCluster.ShortName, out nextNearestClusterId))
                            {
                                nextNearestClusterId = double.NaN;
                            }

                            // Silhouette
                            assignment.AssignmentStatistics.Add(CreatePartialKey(obsFilter, STAT_ASSIGNMENT_SILHOUETTE_WIDTH), silhouetteWidth);
                            assignment.AssignmentStatistics.Add(CreatePartialKey(obsFilter, STAT_ASSIGNMENT_NEXT_NEAREST_CLUSTER), nextNearestClusterId);
                        }

                        // Score is constant so only create one
                        if (obsFilter == null)
                        {
                            // Score
                            assignment.AssignmentStatistics.Add(STAT_ASSIGNMENT_SCORE, assignment.Score);

                            // Next nearest cluster
                            assignment.NextNearestCluster = nextNearestCluster;
                        }
                    }
                }
            }

            // Clear the CurrentStatisticVector, it's a massive memory waste
            foreach (Assignment ass in Assignments)
            {
                ass.CurrentStatisticVector = null;
            }

            //////////////////
            // CLUSTER STATS
            foreach (Cluster cluster in this.Clusters)
            {
                cluster.CalculateAveragedStatistics();
                cluster.CalculateCommentFlags();

                var clusterStatistics = cluster.ClusterStatistics;
                var assignments = cluster.Assignments.List;

                int hcomp, numcomp, hpeak, numpath;
                ClustererStatisticsHelper.CalculateHighestCompounds(cluster, out hcomp, out numcomp);
                ClustererStatisticsHelper.CalculateHighestPeaks(cluster, out hpeak, out numpath);
                clusterStatistics.Add(STAT_CLUSTER_AVERAGE_HIGHEST_NUM_COMPOUNDS, hcomp);
                clusterStatistics.Add(STAT_CLUSTER_AVERAGE_NUM_COMPOUNDS, numcomp);
                clusterStatistics.Add(STAT_CLUSTER_AVERAGE_HIGHEST_NUM_PEAKS, hpeak);
                clusterStatistics.Add(STAT_CLUSTER_AVERAGE_NUM_PATHWAYS, numpath);

                //////////////////////////
                // GROUP STATS (cluster)
                if (statistics.HasFlag(EClustererStatistics.ClusterAverages))
                {
                    AddAveragedStatistics(core, clusterStatistics, assignments);
                }
            }

            //////////////////////
            // GROUP STATS (all)
            if (statistics.HasFlag(EClustererStatistics.AlgorithmAverages))
            {
                AddAveragedStatistics(core, this.ClustererStatistics, Assignments);
            }

            if (statistics.HasFlag(EClustererStatistics.BayesianInformationCriterion))
            {
                this.ClustererStatistics.Add(STAT_CLUSTERER_BIC, ClustererStatisticsHelper.CalculateBic(realClusters, Assignments));
            }
        }

        private void AllGroupsFilters(Core core, List<ObsFilter> results)
        {
            foreach (GroupInfo group in core.Groups)
            {
                if (!results.Any(z => Represents(z, group)))
                {
                    ObsFilter.Condition cond = new ObsFilter.ConditionGroup(Filter.ELogicOperator.And, false, Filter.EElementOperator.Is, new GroupInfo[] { group });
                    ObsFilter toAdd = new ObsFilter(null, null, new[] { cond });

                    results.Add(toAdd);
                }
            }
        }

        internal static bool Represents(ObsFilter a, GroupInfo b)
        {
            if (a == null || a.Conditions.Count != 1)
            {
                return false;
            }

            var x = a.Conditions[0] as ObsFilter.ConditionGroup;

            if (x == null
                || x.Negate
                || x.Operator != Filter.EElementOperator.Is
                || x.Possibilities.Count != 1
                || x.CombiningOperator != Filter.ELogicOperator.And)
            {
                return false;
            }

            if (x.Possibilities[0] != b)
            {
                return false;
            }

            return true;
        }

        private int[] GetFilterIndices(ObsFilter ofilter)
        {
            if (ofilter != null)
            {
                if (Assignments.First().Vector.Observations != null)
                {
                    return Assignments.First().Vector.Observations.Which(z => ofilter.Test(z)).ToArray();
                }
                else
                {
                    return Assignments.First().Vector.Conditions.Which(z => ofilter.Test(z)).ToArray();
                }
            }
            else
            {
                return null;
            }
        }

        private string CreatePartialKey(ObsFilter obsFilter, string key)
        {
            if (obsFilter != null)
            {
                return "Partial vector (" + obsFilter.ToString() + ")\\" + key;
            }
            else
            {
                return "Full vector\\" + key;
            }
        }

        /// <summary>
        /// Key for average across all assignments in set.
        /// </summary>
        private static string CreateAveragedKey(string key, GroupInfo g)
        {
            if (g == null)
            {
                return "Average of all assignments\\" + key;
            }
            else
            {
                return "Average of " + g.Name + " assignments\\" + key;
            }
        }

        /// <summary>
        /// All groups and null
        /// </summary>
        /// <param name="core"></param>
        /// <returns></returns>
        private static IEnumerable<GroupInfo> AllGroupsAndNull(Core core)
        {
            return core.Groups.ConcatSingle(null);
        }

        /// <summary>
        /// Adds averages statististics of [allAssignments] to the [stats] dictionary
        /// </summary>
        private static void AddAveragedStatistics(Core core, Dictionary<string, double> stats, List<Assignment> allAssignments)
        {
            // For each group and all (null)
            foreach (GroupInfo g in AllGroupsAndNull(core))
            {
                // Get the pertinent assignments
                Assignment[] assignments;

                if (g == null)
                {
                    assignments = allAssignments.ToArray();
                }
                else
                {
                    assignments = allAssignments.Where(z => z.Vector.Group == g).ToArray();
                }

                // Ignore empty groups
                if (assignments.Length == 0)
                {
                    continue;
                }

                //stats.Add(GenerateAvgKey(GRP_STAT_AVERAGE_SCORE, g), assignments.Average(z => z.Score));

                // Calculate average of each AssignmentStatistic (ASS_STAT_*)
                Counter<string> totals = new Counter<string>();
                Dictionary<string, double> sums = new Dictionary<string, double>();

                foreach (var ass in assignments)
                {
                    foreach (var kvp in ass.AssignmentStatistics)
                    {
                        totals.Increment(kvp.Key);
                        sums[kvp.Key] = sums.GetOrDefault(kvp.Key, 0.0d) + kvp.Value;
                    }
                }

                foreach (var kvp in sums)
                {
                    stats.Add(CreateAveragedKey(kvp.Key, g), kvp.Value / totals.Counts[kvp.Key]);
                }
            }
        }
    }
}
