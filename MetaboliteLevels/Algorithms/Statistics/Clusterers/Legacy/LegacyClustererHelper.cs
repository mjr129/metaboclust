using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Data.General;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Settings;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Algorithms.Statistics.Clusterers.Legacy
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
        public static void PerformKMeansCentering(ValueMatrix vmatrix, IReadOnlyList<Cluster> toChoose, ConfigurationMetric metric, IProgressReporter prog)
        {
            int n = 0;

            do
            {
                prog.ReportProgress("k-means (iteration " + (++n) + ")");
            } while (Assign(vmatrix, toChoose, ECandidateMode.Assignments, metric, prog));
        }

        /// <summary>
        /// Assigns peaks to clusters
        /// A single k-means iteration.
        /// </summary>
        public static bool Assign(ValueMatrix vmatrix, IReadOnlyList<Cluster> toChoose, ECandidateMode source, ConfigurationMetric distanceMetric, IProgressReporter prog)
        {
            // Get the current cluster centres
            prog.ReportProgress(0, vmatrix.NumVectors);

            for (int index = 0; index < toChoose.Count; index++)
            {
                prog.ReportProgress(index, toChoose.Count);
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
            for (int index = 0; index < vmatrix.NumVectors; index++)
            {
                Vector vec = vmatrix.Vectors[index];
                prog.ReportProgress(index, vmatrix.NumVectors);

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
