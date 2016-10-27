using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Algorithms.Definitions.Metrics;
using MetaboliteLevels.Data.Algorithms.General;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Session.Main;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Data.Algorithms.Definitions.Clusterers.Implementations.Legacy
{
    class LegacyDkMeansPpClusterer : ClustererBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>  
        public LegacyDkMeansPpClusterer(string id, string name)
            : base(id, name)
        {
            Comment = "k-means clustering using the k-means++ initial centre assignment, always choosing the most likley (most distant) centre.";
        }

        public override bool RequiresDistanceMatrix { get { return false; } }
        public override bool SupportsDistanceMetrics { get { return true; } }
        public override bool SupportsObservationFilters { get { return true; } }

        /// <summary>
        /// 
        /// </summary>
        protected override IEnumerable<Cluster> Cluster( IntensityMatrix vmatrix, DistanceMatrix UNUSED, ArgsClusterer args, ConfigurationClusterer tag, ProgressReporter prog)
        {
            // Get parameters
            // COUNT LIMIT
            int countLimit = (int)tag.UntypedArgs.Parameters[0];
            // DISTANCE LIMIT
            double distanceLimit = (double)tag.UntypedArgs.Parameters[1];
            // SEED PEAK
            WeakReference<Peak> seedPeakRef = (WeakReference<Peak>)tag.UntypedArgs.Parameters[2];
            Peak seedPeak = seedPeakRef.GetTargetOrThrow();
            // SEED GROUP
            GroupInfo groupInfo = (GroupInfo)tag.UntypedArgs.Parameters[3];
            // DO-K-MEANS?
            bool doKMeans = (int)tag.UntypedArgs.Parameters[4] != 0;

            // Create the seed cluster
            Cluster seedCluster = new Cluster("1", tag);
            List<Cluster> seedList = new List<Cluster> { seedCluster };
            int seedIndex = vmatrix.FindIndex( new IntensityMatrix.RowHeader( seedPeak, args.SplitGroups ? groupInfo : null ) );

            if (seedIndex == -1)
            {
                throw new InvalidOperationException( $"The chosen peak {{{seedPeak}}} cannot be used a seed because it is not present in the value matrix. Please check that this peak has not been excluded by the filter condition {{{args.PeakFilter}}}.");
            }

            seedCluster.Exemplars.Add( vmatrix.Values[seedIndex]);

            // Autogenerate the clusters
            int? nCountLimit = (countLimit != Int32.MinValue) ? countLimit : (int?)null;
            double? nDistanceLimit = (distanceLimit != Double.MinValue) ? countLimit : (double?)null;

            List<Cluster> autoGenClusters = AutogenerateClusters(vmatrix, seedList, nDistanceLimit, nCountLimit, args.Distance, tag, prog);

            // Do k-means (if requested)
            if (doKMeans)
            {
                prog.Enter("k-means");
                LegacyClustererHelper.PerformKMeansCentering(vmatrix, autoGenClusters, args.Distance, prog);
                prog.Leave();
            }

            // Return full list
            return autoGenClusters;
        }

        /// <summary>
        /// d-k-means++
        /// Ignores insignificant variables.
        /// Returns new clusters (these won't be added to the core so make sure to do so)
        /// </summary>
        private static List<Cluster> AutogenerateClusters( IntensityMatrix vmatrix, List<Cluster> seed, double? stoppingDistance, int? stoppingCount, ConfigurationMetric metric, ConfigurationClusterer tag, ProgressReporter prog)
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
            prog.Enter("Initialising assignments");
            LegacyClustererHelper.Assign(vmatrix, result, ECandidateMode.Exemplars, metric, prog);
            Assignment mostDistant = GetMostDistantAssignment(result);
            prog.Leave();

            // Continue until our limits are breached
            while ((count > 0) && (mostDistant.Score > distance))
            {
                // Check we haven't got unreasonable limits
                iterations++;

                prog.Enter("Centre generation (iteration " + iterations + ")");

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
                    throw new InvalidOperationException("Problem creating new cluster from vector - " + mostDistant.Vector.ToString() + " doesn't like being in its own cluster. Check this vector for discrepancies.");
                }

                // Get the next most distant variable
                count = count - 1;
                mostDistant = GetMostDistantAssignment(result);

                prog.Leave();
            }

            // Return the number of iterations
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        protected override AlgoParameterCollection CreateParamaterDesription()
        {
            return new AlgoParameterCollection( new AlgoParameter("k", "Maximum number of clusters to return (use MAX for no limit)", AlgoParameterTypes.Integer),
                                                new AlgoParameter("d", "Minimum distance between exemplars before no more are selected (use MIN for no limit)", AlgoParameterTypes.Double),
                                                new AlgoParameter("seed.peak", "The peak to act as the first exemplar", AlgoParameterTypes.WeakRefPeak),
                                                new AlgoParameter("seed.group", "The group to act as the first exemplar (only if one vector is generated per group, otherwise this is ignored)", AlgoParameterTypes.Group),
                                                new AlgoParameter("use.kmeans", "Whether to perform k-means after the exemplars have been selected (0 = no, 1 = yes).", AlgoParameterTypes.Integer)
                                              );
        }    

        /// <summary>
        /// 
        /// </summary>
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
