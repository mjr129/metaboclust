using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Algorithms.Statistics.Arguments;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Algorithms.Statistics.Clusterers
{
    class ClustererAffinityPropagation : ClustererBase  // TODO: Include in algo
    {
        public ClustererAffinityPropagation(string id, string name)
            : base(id, name)
        {
        }

        public override AlgoParameters GetParams()
        {
            return new AlgoParameters(AlgoParameters.ESpecial.None, null);
        }

        /// <summary>
        /// ACTION!
        /// </summary>
        protected override IEnumerable<Cluster> Cluster(ValueMatrix vm, DistanceMatrix dm, ArgsClusterer args, ConfigurationClusterer tag, IProgressReporter prog)
        {
            // Construct similarity matrix
            int N = vm.NumVectors;
            double[,] s; // = new double[N, N];
            double[,] r = new double[N, N];
            double[,] a = new double[N, N];
            double[,] rn = new double[N, N];
            double[,] an = new double[N, N];

            // CALCULATE SIMILARITIES "S"
            s = dm.Values;

            // CALCULATE PREFERENCES "diag(S)"
            double median = ApMedian(s);

            for (int i = 0; i < N; i++)
            {
                s[i, i] = median;
            }

            // SET LAMBDA (change rate)
            const double lambda = 0.5;

            prog.ReportProgress(-1);

            // CALCULATE NEXT R AND NEXT A
            for (int iter = 0; iter < 100; iter++)
            {
                prog.ReportProgress("Affinity Propagation Iteration " + iter);

                // CALCULATE R
                // r[i,k] =   s[i,k]
                //          - max( a[i,kp] + s[i,kp] )
                for (int i = 0; i < N; i++)
                {
                    for (int k = 0; k < N; k++)
                    {
                        double v = double.MinValue;

                        for (int kp = 0; kp < N; kp++)
                        {
                            if (kp != k)
                            {
                                v = Math.Max(v, a[i, kp] + s[i, kp]);
                            }
                        }

                        rn[i, k] = s[i, k] - v;
                    }
                }

                for (int i = 0; i < N; i++)
                {
                    for (int k = 0; k < N; k++)
                    {
                        r[i, k] = r[i, k] * lambda + rn[i, k] * (1 - lambda);
                    }
                }

                // CALCULATE A
                // a[i, k] = min(0, r(k,k)
                //                 + sum( max ( 0, 
                //                        r(ip, k)
                // a[k, k] = sum( max( 0, r(ip, k ) )
                for (int i = 0; i < N; i++)
                {
                    for (int k = 0; k < N; k++)
                    {
                        if (i != k)
                        {
                            double v = 0;

                            for (int ip = 0; ip < N; ip++)
                            {
                                if (ip != i && ip != k)
                                {
                                    v += Math.Max(0, r[ip, k]);
                                }
                            }

                            an[i, k] = Math.Min(0, r[k, k] + v);
                        }
                        else
                        {
                            double v = 0;

                            for (int ip = 0; ip < N; ip++)
                            {
                                if (ip != i && ip != k)
                                {
                                    v += Math.Max(0, r[ip, k]);
                                }
                            }

                            an[k, k] = v;
                        }
                    }
                }

                for (int i = 0; i < N; i++)
                {
                    for (int k = 0; k < N; k++)
                    {
                        a[i, k] = a[i, k] * lambda + an[i, k] * (1 - lambda);
                    }
                }
            }

            // CALCULATE EXEMPLARS "E"
            // the value of k that maximizes a(i,k) + r(i,k)
            int[] exemplars = new int[N];
            double[] scores = new double[N];

            for (int i = 0; i < N; i++)
            {
                double maxVal = double.MinValue;
                int maxK = -1;

                for (int k = 0; k < N; k++)
                {
                    double val = a[i, k] + r[i, k];

                    if (val > maxVal)
                    {
                        maxVal = val;
                        maxK = k;
                    }
                }

                exemplars[i] = maxK;
                scores[i] = maxVal; // HIGHER is better
            }

            // CONVERT TO CLUSTERS
            Dictionary<int, Cluster> dict = new Dictionary<int, Cluster>();

            for (int pInd = 0; pInd < vm.NumVectors; pInd++)
            {
                var vec = vm.Vectors[pInd];
                int exe = exemplars[pInd];
                var vecx = vm.Vectors[exe];
                double score = scores[pInd]; // HIGHER is better

                var clu = dict.GetOrCreate(exe, x => new Cluster(vecx.Peak.DisplayName, tag));

                clu.Assignments.Add(new Assignment(vec, clu, score));
            }

            return dict.Values.OrderBy(z => z.DisplayName);
        }

        /// <summary>
        /// Calculate the median of matrix "S".
        /// Exclude the diagonal from the calculation.
        /// </summary>
        private static double ApMedian(double[,] s)
        {
            // Flatten the matrix and remove the diagnonal
            double[] flat = new double[s.Length - s.GetLength(0)];
            int k = 0;

            for (int i = 0; i < s.GetLength(0); i++)
            {
                for (int j = 0; j < s.GetLength(1); j++)
                {
                    if (i != j)
                    {
                        flat[k] = s[i, j];
                        k++;
                    }
                }
            }

            UiControls.Assert(k == flat.Length);

            return Maths.Median(flat);
        }
    }
}
