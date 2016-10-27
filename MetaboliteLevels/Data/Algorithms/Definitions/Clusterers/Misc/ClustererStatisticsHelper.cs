using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Session.Main;
using MGui.Datatypes;

namespace MetaboliteLevels.Data.Algorithms.Definitions.Clusterers.Misc
{
    static class ClustererStatisticsHelper
    {
        /// <summary>
        /// Silhouette width:
        /// s(i) = b(i) - a(i) / max(a(i), b(i))
        /// d(i, c) = avg(all j in cluster c)( | x(i) - x(j) |)
        /// a(i) = d(i, c) where c is i's cluster
        /// b(i) = lowest d(i, c) for all c which aren't i's cluster
        /// fuck it, just use R.
        /// actually ballocks, can't, R can't handle big matrices
        /// </summary>
        public static void CalculateSilhouette(ResultClusterer.ForStat ass, IReadOnlyList<Cluster> clusters, out double silhouette, out Cluster nearestCluster)
        {
            // a = d(i, c) where c is i's cluster
            double a = CalculateSilhouette_CalculateD(ass, ass.Assignment.Cluster.Assignments.List);

            Cluster nearest = null;
            double b = double.MaxValue;

            foreach (Cluster cluster in clusters)
            {
                if (cluster != ass.Assignment.Cluster)
                {
                    double d = CalculateSilhouette_CalculateD(ass, cluster.Assignments.List);

                    if (d < b)
                    {
                        b = d;
                        nearest = cluster;
                    }
                }
            }

            double s = (b - a) / Math.Max(a, b);

            silhouette = s;
            nearestCluster = nearest;
        }

        /// <summary>
        /// Used by CalculateSilhouette.
        /// Calculates d(i).
        /// </summary>
        private static double CalculateSilhouette_CalculateD(ResultClusterer.ForStat stat, IReadOnlyList<Assignment> ass2s)
        {
            double[] result = new double[ass2s.Count];

            for (int index = 0; index < ass2s.Count; index++)
            {
                result[index] = stat.DistanceMatrix.Values[stat.Assignment.Vector.Index, ass2s[index].Vector.Index];
            }

            if (result.Length == 0)
            {
                return double.NaN;
            }

            return result.Average();
        }        

        /// <summary>
        /// Calculates the highest number of peaks in this cluster seen in a single pathway.
        /// </summary>
        public static void CalculateHighestPeaks(Cluster cluster, out int highestPeaks, out int numPathways)
        {
            Counter<Pathway> paths = new Counter<Pathway>();

            foreach (Peak p in cluster.Assignments.Peaks)
            {
                HashSet<Pathway> paths2 = new HashSet<Pathway>();

                // Pathways for this peak
                foreach (Annotation c in p.Annotations)
                {
                    foreach (Pathway pa in c.Compound.Pathways)
                    {
                        paths2.Add(pa);
                    }
                }

                // +1 to each pathway
                foreach (Pathway pa in paths2)
                {
                    paths.Increment(pa);
                }
            }

            // hpeak, num comps
            numPathways = paths.Count;
            highestPeaks = paths.FindMax().Value;
        }

        /// <summary>
        /// Calculates the highest number of compounds in this cluster seen in a single pathway.
        /// </summary>
        public static void CalculateHighestCompounds(Cluster cluster, out int highestCompounds, out int numCompounds)
        {
            HashSet<Compound> compounds = new HashSet<Compound>();
            Counter<Pathway> paths = new Counter<Pathway>();

            // Compounds for this cluster
            foreach (Peak p in cluster.Assignments.Peaks)
            {
                foreach (Annotation c in p.Annotations)
                {
                    compounds.Add(c.Compound);
                }
            }

            // Increment pathways
            foreach (Compound c in compounds)
            {
                foreach (Pathway pa in c.Pathways)
                {
                    paths.Increment(pa);
                }
            }

            // hcomp, num comps
            numCompounds = compounds.Count;
            highestCompounds = paths.FindMax().Value;
        }

        /// <summary>
        /// Calculates BIC
        /// 
        /// https://github.com/mynameisfiber/pyxmeans/blob/master/pyxmeans/xmeans.py
        /// </summary>
        public static double CalculateBic(IReadOnlyList<Cluster> clusters, IReadOnlyList<Assignment> assignments)
        {
            double numClusters = clusters.Count();
            double numPoints = assignments.Count;
            double numDims = assignments[0].Vector.Values.Length;

            double logLikelihood = CalculateBic_CalculateLogLikelihood(clusters, numPoints, numDims);
            double numParams = CalculateBic_CalculateFreeParams(numClusters, numDims);

            return logLikelihood - numParams / 2.0 * Math.Log(numPoints);
        }


        private static double CalculateBic_CalculateFreeParams(double numClusters, double numDims)
        {
            return numClusters * (numDims + 1);
        }

        private static double CalculateBic_CalculateLogLikelihood(IReadOnlyList<Cluster> clusters, double numPoints, double numDims)
        {
            double ll = 0;

            foreach (Cluster cluster in clusters)
            {
                double fRn = cluster.Assignments.Count;
                double t1 = fRn * Math.Log(fRn);
                double t2 = fRn * Math.Log(numPoints);
                double variance = CalculateBic_CalculateClusterVariance(clusters, numPoints); //or np.nextafter(0, 1)
                double t3 = ((fRn * numDims) / 2.0) * Math.Log((2.0 * Math.PI) * variance);
                double t4 = (fRn - 1.0) / 2.0;
                ll += t1 - t2 - t3 - t4;
            }

            return ll;
        }

        private static double CalculateBic_CalculateClusterVariance(IReadOnlyList<Cluster> clusters, double numPoints)
        {
            double s = 0;
            double denom = numPoints - clusters.Count();

            foreach (Cluster cluster in clusters)
            {
                // TODO: 1 Check this is equal to "euclidean_distances(cluster, centroid)" in the original code
                // 2: Replace Score with Euclidean, since Score can be NULL.
                IEnumerable<double> distancesSquared = cluster.Assignments.List.Select(z => z.Score * z.Score);
                s += distancesSquared.Sum();
            }

            return s / denom;
        }
    }
}
