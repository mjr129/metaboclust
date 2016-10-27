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

namespace MetaboliteLevels.Data.Algorithms.Definitions.Clusterers.Implementations.Legacy
{
    static class LegacyClustererHelper
    {
        class ClusterScore
        {
            public Cluster Cluster;
            public double Score;

            public ClusterScore(Cluster bestCluster, double bestScore)
            {
                this.Cluster = bestCluster;
                this.Score = bestScore;
            }
        }

        /// <summary>
        /// K-means centering.
        /// </summary>
        public static void PerformKMeansCentering( IntensityMatrix vmatrix, IReadOnlyList<Cluster> toChoose, ConfigurationMetric metric, ProgressReporter prog)
        {
            int n = 0;

            do
            {
                if (n != 0)
                {
                    prog.Leave();
                }

                prog.Enter("k-means (iteration " + (++n) + ")");
            } while (Assign(vmatrix, toChoose, ECandidateMode.Assignments, metric, prog));

            prog.Leave();
        }

        /// <summary>
        /// Assigns peaks to clusters
        /// A single k-means iteration.
        /// </summary>
        public static bool Assign( IntensityMatrix vmatrix, IReadOnlyList<Cluster> toChoose, ECandidateMode source, ConfigurationMetric distanceMetric, ProgressReporter prog)
        {
            // Get the current cluster centres
            prog.SetProgress(0, vmatrix.NumRows);

            for (int index = 0; index < toChoose.Count; index++)
            {
                prog.SetProgress(index, toChoose.Count);
                toChoose[index].SetCentre(ECentreMode.Average, source);
            }

            // Clear the previous assignments
            Dictionary<Cluster, List<Vector>> previousAssignments = new Dictionary<Cluster, List<Vector>>();

            for (int index = 0; index < toChoose.Count; index++)
            {
                previousAssignments.Add(toChoose[index], toChoose[index].Assignments.Vectors.ToList());
                toChoose[index].Assignments.ClearAll();
            }

            // Detect changes so we know when the algorithm has converged
            bool somethingChanged = false;

            // Assign peaks to centres
            for (int index = 0; index < vmatrix.NumRows; index++)
            {
                Vector vec = vmatrix.Vectors[index];
                prog.SetProgress(index, vmatrix.NumRows);

                ClusterScore best = FindClosestCluster(vec.Values, toChoose, distanceMetric);

                // Something changed?
                if (!previousAssignments[best.Cluster].Contains(vec))
                {
                    somethingChanged = true;
                }

                // Create new assignment
                best.Cluster.Assignments.Add(new Assignment(vec, best.Cluster, best.Score));
            }

            return somethingChanged;
        }

        /// <summary>
        /// Calculates the cluster with the best score for v.
        /// Will not assign to insignificant, distant or disabled clusters.
        /// </summary>
        private static ClusterScore FindClosestCluster(double[] vector, IEnumerable<Cluster> toChoose, ConfigurationMetric distanceMetric)
        {
            Cluster bestCluster = null;
            double bestScore = double.NaN;

            foreach (Cluster p in toChoose)
            {
                double s = p.CalculateScore(vector, EDistanceMode.ClosestCentre, distanceMetric);

                if (double.IsNaN(bestScore) || s < bestScore)
                {
                    bestScore = s;
                    bestCluster = p;
                }
            }

            return new ClusterScore(bestCluster, bestScore);
        }
    }
}
