using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Algorithms.Statistics.Arguments;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Settings;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Data.DataInfo;

namespace MetaboliteLevels.Algorithms.Statistics.Clusterers.Legacy
{
    class LegacyDKMeansPPClusterer : ClustererBase
    {
        public LegacyDKMeansPPClusterer(string id, string name)
            : base(id, name)
        {
        }

        protected override IEnumerable<Cluster> Cluster(ValueMatrix vmatrix, DistanceMatrix UNUSED, ArgsClusterer args, ConfigurationClusterer tag, IProgressReporter prog)
        {
            // Get parameters
            // COUNT LIMIT
            int countLimit = (int)tag.Args.Parameters[0];
            // DISTANCE LIMIT
            double distanceLimit = (double)tag.Args.Parameters[1];
            // SEED PEAK
            WeakReference<Peak> seedPeakRef = (WeakReference<Peak>)tag.Args.Parameters[2];
            Peak seedPeak = seedPeakRef.GetTargetOrThrow();
            // SEED GROUP
            GroupInfo groupInfo = (GroupInfo)tag.Args.Parameters[3];
            // DO-K-MEANS?
            bool doKMeans = (int)tag.Args.Parameters[4] != 0;

            // Create the seed cluster
            Cluster seedCluster = new Cluster("1", tag);
            List<Cluster> seedList = new List<Cluster> { seedCluster };
            seedCluster.Exemplars.Add(vmatrix.Extract(seedPeak, args.SplitGroups ? groupInfo : null));

            // Autogenerate the clusters
            int? nCountLimit = (countLimit != Int32.MinValue) ? countLimit : (int?)null;
            double? nDistanceLimit = (distanceLimit != Double.MinValue) ? countLimit : (double?)null;

            List<Cluster> autoGenClusters = AutogenerateClusters(vmatrix, seedList, nDistanceLimit, nCountLimit, args.Distance, tag, prog);

            // Do k-means (if requested)
            if (doKMeans)
            {
                prog.ReportProgress("k-means");
                LegacyClustererHelper.PerformKMeansCentering(vmatrix, autoGenClusters, args.Distance, prog);
            }

            // Return full list
            return autoGenClusters;
        }

        /// <summary>
        /// d-k-means++
        /// Ignores insignificant variables.
        /// Returns new clusters (these won't be added to the core so make sure to do so)
        /// </summary>
        private static List<Cluster> AutogenerateClusters(ValueMatrix vmatrix, List<Cluster> seed, double? stoppingDistance, int? stoppingCount, ConfigurationMetric metric, ConfigurationClusterer tag, IProgressReporter prog)
        {
            // Make a log of whatever limits have been set
            if (!stoppingCount.HasValue && !stoppingDistance.HasValue)
            {
                throw new InvalidOperationException("No stopping condition set.");
            }

            // Assign all variables to nearest
            List<Cluster> result = new List<Cluster>(seed);

            // Get the actual limits
            int iterations = 0;
            int count = (stoppingCount - seed.Count) ?? Int32.MaxValue;
            double distance = stoppingDistance ?? Double.MinValue;

            // Get the most distant variable
            prog.ReportProgress("Initialising assignments");
            LegacyClustererHelper.Assign(vmatrix, result, ECandidateMode.Exemplars, metric, prog);
            Assignment mostDistant = GetMostDistantAssignment(result);

            // Continue until our limits are breached
            while ((count > 0) && (mostDistant.Score > distance))
            {
                // Check we haven't got unreasonable limits
                iterations++;

                prog.ReportProgress("Centre generation (iteration " + iterations + ")");

                if (iterations > 1000)
                {
                    throw new InvalidOperationException("Too many iterations - exiting.");
                }

                // Create a new cluster with the most distant variable as its exemplar
                var newCluster = new Cluster((result.Count + 1).ToString(), tag);
                result.Add(newCluster);
                newCluster.Exemplars.Add(mostDistant.Vector.Values);  // todo: check to prevent multiple assignment?

                // Make the assignments based on the closest exemplars
                LegacyClustererHelper.Assign(vmatrix, result, ECandidateMode.Exemplars, metric, prog);

                // Basic check
                if (!newCluster.Assignments.Vectors.Contains(mostDistant.Vector))
                {
                    throw new InvalidOperationException("Problem creating new cluster from peak - " + mostDistant.Peak.DisplayName + " doesn't like being in its own cluster. Check this peak for discrepancies.");
                }

                // Get the next most distant variable
                count = count - 1;
                mostDistant = GetMostDistantAssignment(result);
            }

            // Return the number of iterations
            return result;
        }

        public override AlgoParameters GetParams()
        {
            AlgoParameters.Param[] p = {
                                           new AlgoParameters.Param("k", AlgoParameters.EType.Integer),
                                           new AlgoParameters.Param("d", AlgoParameters.EType.Double),
                                           new AlgoParameters.Param("seed.peak", AlgoParameters.EType.WeakRefPeak),
                                           new AlgoParameters.Param("seed.group", AlgoParameters.EType.Group),
                                           new AlgoParameters.Param("use.kmeans", AlgoParameters.EType.Integer)
                                       };

            return new AlgoParameters(AlgoParameters.ESpecial.ClustererIgnoresDistanceMatrix, p);
        }

        private static Assignment GetMostDistantAssignment(IEnumerable<Cluster> clusters)
        {
            Assignment furthest = null;

            foreach (Cluster cluster in clusters)
            {
                foreach (var assignment in cluster.Assignments.List)
                {
                    if (furthest == null || assignment.Score > furthest.Score)
                    {
                        furthest = assignment;
                    }
                }
            }

            UiControls.Assert(furthest != null, "GetMostDistant: Failed to find furthest assignment.");

            return furthest;
        }
    }
}
