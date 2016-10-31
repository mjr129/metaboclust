using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Algorithms.Definitions.Base;
using MetaboliteLevels.Data.Algorithms.Definitions.Clusterers.Misc;
using MetaboliteLevels.Data.Algorithms.Definitions.Metrics;
using MetaboliteLevels.Data.Algorithms.General;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Session.Main;
using MetaboliteLevels.Gui.Controls.Lists;
using MetaboliteLevels.Utilities;
using MGui.Datatypes;
using MGui.Helpers;

namespace MetaboliteLevels.Data.Algorithms.Definitions.Clusterers
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
        private const string STAT_ASSIGNMENT_EUCLIDEAN_FROM_AVG_SQUARED = "Euclidean distance squared";
        private const string STAT_ASSIGNMENT_DISTANCE_FROM_AVG = " distance";
        private const string STAT_ASSIGNMENT_DISTANCE_FROM_AVG_SQUARED = " distance squared";

        // Clusters
        private const string STAT_CLUSTER_AVERAGE_HIGHEST_NUM_COMPOUNDS = "Max compounds in pathway";
        private const string STAT_CLUSTER_AVERAGE_NUM_COMPOUNDS = "Number of compounds";
        private const string STAT_CLUSTER_AVERAGE_HIGHEST_NUM_PEAKS = "Max peaks in pathway";
        private const string STAT_CLUSTER_AVERAGE_NUM_PATHWAYS = "Number of pathways";

        // Clusterers
        private const string STAT_CLUSTERER_BIC = "BIC";
        private const string STAT_NUM_VECTORS = "Number of vectors";
        private const string STAT_LENGTH_OF_VECTORS = "Length of vectors";

        /// <summary>
        /// Clusters (inc. insignificants)
        /// </summary>
        [XColumn( "Clusters (inc. filtered)")]
        public readonly Cluster[] Clusters;

        /// <summary>
        /// Assignments (exc. insignificants)
        /// </summary>     
        public readonly List<Assignment> Assignments = new List<Assignment>();

        /// <summary>
        /// Statistics
        /// </summary>
        [XColumn]
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
        [XColumn("Clusters (exc. filtered)")]
        public IEnumerable<Cluster> RealClusters
        {
            get { return Clusters.Where(z => z.States == Cluster.EStates.None); }
        }

        public class ForStat
        {
            internal Assignment Assignment;
            internal double[] ClusterVector;
            internal ObsFilter ObsFilter;
            public Vector AssignmentVector;
            internal DistanceMatrix DistanceMatrix;
        }

        /// <summary>
        /// Action completed - calculate statisstics
        /// </summary>
        internal void FinalizeResults(Core core, ConfigurationMetric metric, IntensityMatrix vmatrix, DistanceMatrix dmatrix, EClustererStatistics statistics, ProgressReporter prog)
        {
            UiControls.Assert(Assignments.IsEmpty(), "FinalizeResults on ClusterResults already called.");

            // Get ALL the assignments
            foreach (Cluster cluster in RealClusters)
            {
                Assignments.AddRange(cluster.Assignments.List);
            }

            RecalculateStatistics(core, metric, vmatrix, dmatrix, statistics, prog);
        }

        public override string ToString()
        {
            return Assignments.Count + " : " + RealClusters.Count();
        }

        /// <summary>
        /// Recalculates the statistics.
        /// </summary>
        /// <param name="core">Core</param>
        /// <param name="metric">Metric for statistics</param>
        /// <param name="statistics">What to calculate</param>
        /// <param name="prog">Report progress to</param>
        /// <param name="vmatrix">Value matrix</param>
        /// <param name="dmatrix">Distance matrix (optional - if not present will be calculated if necessary)</param>
        internal void RecalculateStatistics(Core core, ConfigurationMetric metric, IntensityMatrix vmatrix, DistanceMatrix dmatrix, EClustererStatistics statistics, ProgressReporter prog)
        {
            // Add basics
            ClustererStatistics[STAT_NUM_VECTORS] = vmatrix.NumRows;
            ClustererStatistics[STAT_LENGTH_OF_VECTORS] = vmatrix.NumCols;

            // Don't calculate metrics?
            if (statistics == EClustererStatistics.None)
            {
                return;
            }

            // Get the non-insig clusters
            Cluster[] realClusters = RealClusters.ToArray();

            // If we don't have a DMatrix we should calculate the sil. width manually
            // The DMatrix might be too big to pass to R so its better just to avoid it.
            prog.Enter("Calculating statistics");
            List<ObsFilter> groupFilters = new List<ObsFilter>();

            // No filter
            groupFilters.Add(null);

            if (!vmatrix.HasSplitGroups)
            {
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
            }

            List<ForStat> needsCalculating = new List<ForStat>();

            prog.Enter("Input vectors");
            ProgressParallelHandler progP = prog.CreateParallelHandler(groupFilters.Count);
            ProgressParallelHandler closure1 = progP;
            Parallel.ForEach(groupFilters, obsFilter => Thread_AddFilterToCalculationList(core, metric, vmatrix, dmatrix, statistics, realClusters, obsFilter, needsCalculating, closure1));
            prog.Leave();

            // ASSIGNMENT STATS
            prog.Enter("Assignments");
            progP = prog.CreateParallelHandler(needsCalculating.Count);
            ProgressParallelHandler closure2 = progP;
            Parallel.ForEach(needsCalculating, z => Thread_CalculateAssignmentStatistics(statistics, z, realClusters, metric, closure2));
            prog.Leave();

            // CLUSTER STATS
            prog.Enter("Clusters");
            progP = prog.CreateParallelHandler(this.Clusters.Length);
            Parallel.ForEach(this.Clusters, z => Thread_CalculateClusterStatistics(core, statistics, z, progP));
            prog.Leave();

            // SUMMARY STATS
            prog.Enter("Summary");
            CalculateSummaryStatistics(core, statistics, realClusters);
            prog.Leave();

            prog.Leave();
        }

        /// <summary>
        /// Determines what needs calculating.
        /// </summary>                        
        private void Thread_AddFilterToCalculationList([Const]Core core, [Const]ConfigurationMetric metric, [Const]IntensityMatrix vmatrix, [Const]DistanceMatrix dmatrix, [Const]EClustererStatistics statistics, [Const]Cluster[] realClusters, [Const]ObsFilter obsFilter, [MutableUnsafe] List<ForStat> needsCalculating, [MutableSafe]ProgressParallelHandler progP)
        {
            progP.SafeIncrement();

            IntensityMatrix vmatFiltered;
            DistanceMatrix dmatFiltered;
            int[] filteredIndices;

            if (obsFilter == null)
            {
                vmatFiltered = vmatrix;
                dmatFiltered = dmatrix;
                filteredIndices = null;
            }
            else
            {
                filteredIndices = vmatrix.Columns.Which(z=>  obsFilter.Test(z.Observation) ).ToArray(); // TODO: Multuple iteration
                vmatFiltered = vmatrix.Subset(null, obsFilter, ESubsetFlags.None);
                dmatFiltered = null;
            }

            Dictionary<Cluster, double[]> centreVectors = new Dictionary<Cluster, double[]>();

            foreach (Cluster cluster in realClusters)
            {
                /////////////////////
                // ASSIGNMENT STATS
                var centre = cluster.GetCentre(ECentreMode.Average, ECandidateMode.Assignments);
                double[] centreVector = centre.Count != 0 ? centre[0] : null;

                if (filteredIndices != null)
                {
                    centreVector = centreVector.Extract(filteredIndices);
                }

                centreVectors.Add(cluster, centreVector);
            }

            foreach (Assignment ass in Assignments)
            {
                ForStat f = new ForStat();
                f.Assignment = ass;
                f.ObsFilter = obsFilter;

                if (filteredIndices != null)
                {
                    f.AssignmentVector = vmatFiltered.Vectors[ass.Vector.Index];
                }
                else
                {
                    f.AssignmentVector = ass.Vector;
                }

                f.ClusterVector = centreVectors[ass.Cluster];

                if (statistics.HasFlag(EClustererStatistics.SilhouetteWidth))
                {
                    if (dmatFiltered == null)
                    {
                        dmatFiltered = DistanceMatrix.Create(core, vmatrix, metric, ProgressReporter.GetEmpty());
                    }
                }

                f.DistanceMatrix = dmatFiltered;

                lock (needsCalculating)
                {
                    needsCalculating.Add(f);
                }
            }
        }

        /// <summary>
        /// Calculates statistics for the algorithm as a whole.
        /// </summary>                                         
        private void CalculateSummaryStatistics(Core core, EClustererStatistics statistics, Cluster[] realClusters)
        {
            if (statistics.HasFlag(EClustererStatistics.AlgorithmAverages))
            {
                AddAveragedStatistics(core, this.ClustererStatistics, Assignments);
            }

            if (statistics.HasFlag(EClustererStatistics.BayesianInformationCriterion))
            {
                this.ClustererStatistics[STAT_CLUSTERER_BIC] = ClustererStatisticsHelper.CalculateBic(realClusters, Assignments);
            }
        }

        /// <summary>
        /// Thread operation to calculate statistics for [cluster].
        /// 
        /// [cluster] is guarenteed to be unique, so needn't be locked.
        /// </summary>                                                
        private static void Thread_CalculateClusterStatistics([Const]Core core, [Const]EClustererStatistics statistics, [MutableSafe] Cluster cluster, [MutableSafe]ProgressParallelHandler prog)
        {
            prog.SafeIncrement();
            cluster.CalculateAveragedStatistics();
            cluster.CalculateCommentFlags();

            Dictionary<string, double> clusterStatistics = cluster.ClusterStatistics;
            List<Assignment> assignments = cluster.Assignments.List;

            int hcomp, numcomp, hpeak, numpath;
            ClustererStatisticsHelper.CalculateHighestCompounds(cluster, out hcomp, out numcomp);
            ClustererStatisticsHelper.CalculateHighestPeaks(cluster, out hpeak, out numpath);
            clusterStatistics[STAT_CLUSTER_AVERAGE_HIGHEST_NUM_COMPOUNDS] = hcomp;
            clusterStatistics[STAT_CLUSTER_AVERAGE_NUM_COMPOUNDS] = numcomp;
            clusterStatistics[STAT_CLUSTER_AVERAGE_HIGHEST_NUM_PEAKS] = hpeak;
            clusterStatistics[STAT_CLUSTER_AVERAGE_NUM_PATHWAYS] = numpath;

            //////////////////////////
            // GROUP STATS (cluster)
            if (statistics.HasFlag(EClustererStatistics.ClusterAverages))
            {
                AddAveragedStatistics(core, clusterStatistics, assignments);
            }
        }

        /// <summary>
        /// Thread operation fo calculate statistics for [stat].
        /// 
        /// [stat] is guarenteed to be unique, however stat.Assignment is not, hence stat.Assignment must be locked.
        /// 
        /// Currently only stat.Assignment.AssignmentStatistics is the only member to be R/W locked, since that is all
        /// that is modified.
        /// </summary>                                                                                                                   
        private static void Thread_CalculateAssignmentStatistics([Const]EClustererStatistics statistics, [MutableUnsafe]ForStat stat, [Const] Cluster[] realClusters, [Const] ConfigurationMetric metric, [MutableSafe]ProgressParallelHandler prog)
        {
            prog.SafeIncrement();

            // STATS: Distance from avg
            if (stat.ClusterVector != null)
            {
                // Euclidean
                if (statistics.HasFlag(EClustererStatistics.EuclideanFromAverage))
                {
                    double ed = Maths.Euclidean(stat.AssignmentVector.Values, stat.ClusterVector);
                    stat.Assignment.AssignmentStatistics.ThreadSafeIndex(CreatePartialKey(stat.ObsFilter, STAT_ASSIGNMENT_EUCLIDEAN_FROM_AVG), ed);
                    stat.Assignment.AssignmentStatistics.ThreadSafeIndex(CreatePartialKey(stat.ObsFilter, STAT_ASSIGNMENT_EUCLIDEAN_FROM_AVG_SQUARED), ed * ed);
                }

                // Custom (if applicable)
                if (metric != null
                    && statistics.HasFlag(EClustererStatistics.DistanceFromAverage)
                    && !(metric.Args.Id == Algo.ID_METRIC_EUCLIDEAN && statistics.HasFlag(EClustererStatistics.EuclideanFromAverage)))
                {
                    string key1 = metric.ToString() + STAT_ASSIGNMENT_DISTANCE_FROM_AVG;
                    string key2 = metric.ToString() + STAT_ASSIGNMENT_DISTANCE_FROM_AVG_SQUARED;
                    double dd = metric.Calculate(stat.AssignmentVector.Values, stat.ClusterVector);

                    stat.Assignment.AssignmentStatistics.ThreadSafeIndex(CreatePartialKey(stat.ObsFilter, key1), dd);
                    stat.Assignment.AssignmentStatistics.ThreadSafeIndex(CreatePartialKey(stat.ObsFilter, key2), dd * dd);
                }
            }

            // STATS: Silhouette
            Cluster nextNearestCluster = null;

            if (statistics.HasFlag(EClustererStatistics.SilhouetteWidth))
            {
                double silhouetteWidth;
                double nextNearestClusterId;

                ClustererStatisticsHelper.CalculateSilhouette(stat, realClusters, out silhouetteWidth, out nextNearestCluster);

                if (!double.TryParse(nextNearestCluster.ShortName, out nextNearestClusterId))
                {
                    nextNearestClusterId = double.NaN;
                }

                // Silhouette
                stat.Assignment.AssignmentStatistics.ThreadSafeIndex(CreatePartialKey(stat.ObsFilter, STAT_ASSIGNMENT_SILHOUETTE_WIDTH), silhouetteWidth);
                stat.Assignment.AssignmentStatistics.ThreadSafeIndex(CreatePartialKey(stat.ObsFilter, STAT_ASSIGNMENT_NEXT_NEAREST_CLUSTER), nextNearestClusterId);
            }

            // STATS: Score
            if (stat.ObsFilter == null)
            {
                // Score
                stat.Assignment.AssignmentStatistics.ThreadSafeIndex(STAT_ASSIGNMENT_SCORE, stat.Assignment.Score);

                // Next nearest cluster
                stat.Assignment.NextNearestCluster = nextNearestCluster; // Only one ForStat per Assignment has ObsFilter == null so thread safe not required
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



        private static string CreatePartialKey(ObsFilter obsFilter, string key)
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
                return "Average of " + g.DisplayName + " assignments\\" + key;
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
                        // TODO: kvp.Key should never be null (but it sometimes is, this is only here to avoid an error!)
                        if (kvp.Key != null)
                        {
                            totals.Increment(kvp.Key);
                            sums[kvp.Key] = sums.GetOrDefault(kvp.Key, 0.0d) + kvp.Value;
                        }
                    }
                }

                foreach (var kvp in sums)
                {
                    string statKey = CreateAveragedKey(kvp.Key, g);

                    stats[statKey] = kvp.Value / totals[kvp.Key];
                }
            }
        }
    }
}
