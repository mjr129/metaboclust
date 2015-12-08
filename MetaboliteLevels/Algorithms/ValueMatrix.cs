using MetaboliteLevels.Data.Visualisables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Data.DataInfo;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Settings;
using MetaboliteLevels.Utilities;
using System.IO;

namespace MetaboliteLevels.Algorithms
{
    [Serializable]
    internal class Vector
    {
        public readonly Peak Peak;
        public readonly GroupInfo Group;
        public readonly ConditionInfo[] Conditions;
        public readonly ObservationInfo[] Observations;
        public readonly double[] Values;

        public Vector(Peak peak, GroupInfo group, ConditionInfo[] conditions, ObservationInfo[] observations, double[] values)
        {
            Peak = peak;
            Group = group;
            Conditions = conditions;
            Observations = observations;
            Values = values;
        }

        internal bool DiffGroups(Vector vector)
        {
            return Conditions != vector.Conditions || Observations != vector.Observations;
        }

        public override string ToString()
        {
            if (Group == null)
            {
                return Peak.DisplayName;
            }
            else
            {
                return Peak.DisplayName + "∩" + Group.Name;
            }
        }
    }

    [Serializable]
    class ValueMatrix
    {
        public readonly bool UsesTrend;
        public readonly Vector[] Vectors;

        private ValueMatrix(bool useTrend, Vector[] vectors)
        {
            this.UsesTrend = useTrend;
            this.Vectors = vectors;
        }

        public IEnumerable<Peak> Peaks
        {
            get { return Vectors.Select(z => z.Peak); }
        }

        public int NumVectors
        {
            get { return Vectors.Length; }
        }

        internal static double[,] Flatten(double[][] jagged)
        {
            int numi = jagged.Length;
            int numj = jagged[0].Length;
            double[,] result = new double[numi, numj];

            for (int i = 0; i < numi; i++)
            {
                for (int j = 0; j < numj; j++)
                {
                    result[i, j] = jagged[i][j];
                }
            }

            return result;
        }

        internal double[,] Flatten()
        {
            return Flatten(Vectors.Select(z => z.Values).ToArray());
        }

        /// <summary>
        /// Creates a values matrix from a list of peaks.
        /// </summary>
        /// <param name="peaks">Peaks</param>
        /// <param name="useTrend">true to use trend, false to use all observations</param>
        /// <param name="core">Core</param>
        /// <param name="obsFilterOrNull">Filter or NULL (== ObsFilter.Empty)</param>
        /// <param name="splitGroups">Create a unique vector for each experimental group (as opposed to concatenating)</param>
        /// <returns>ValueMatrix for peaks</returns>
        public static ValueMatrix Create(IReadOnlyList<Peak> peaks, bool useTrend, Core core, ObsFilter obsFilterOrNull, bool splitGroups, ProgressReporter prog)
        {
            int x = peaks.Count;
            ConditionInfo[] obs1;
            ObservationInfo[] obs2;
            GroupInfo[] groups;
            var obsFilter = obsFilterOrNull ?? ObsFilter.Empty;

            if (splitGroups)
            {
                groups = new HashSet<GroupInfo>(core.Conditions.Where(obsFilter.Test).Select(z => z.Group)).ToArray();
            }
            else
            {
                groups = new GroupInfo[1] { null };
            }

            Vector[] vectors = new Vector[peaks.Count * groups.Length];

            for (int groupIndex = 0; groupIndex < groups.Length; groupIndex++)
            {
                GroupInfo group = groups[groupIndex];
                int groupOffset = groupIndex * peaks.Count;

                prog.SetProgress(groupIndex, groups.Length);

                if (useTrend)
                {
                    // TREND VECTORS
                    int[] which;

                    if (group == null)
                    {
                        which = core.Conditions.WhichInOrder(obsFilter.Test, ConditionInfo.GroupTimeOrder).ToArray();
                    }
                    else
                    {
                        which = core.Conditions.WhichInOrder(z => z.Group == group && obsFilter.Test(z), ConditionInfo.GroupTimeOrder).ToArray();
                    }

                    obs1 = core.Conditions.In(which).ToArray();
                    int y = which.Length;

                    if (groupIndex != 0)
                    {
                        ValidateOrder(vectors[groupOffset - 1].Conditions, obs1);
                    }

                    ValidateSize(core, vectors.Length, which.Length);

                    for (int i = 0; i < x; i++)
                    {
                        double[] s = new double[y];

                        for (int j = 0; j < y; j++)
                        {
                            s[j] = peaks[i].Observations.Trend[which[j]];
                        }

                        vectors[groupOffset + i] = new Vector(peaks[i], group, obs1, null, s);
                    }
                }
                else
                {
                    // OBSERVATION VECTORS
                    int[] which;

                    if (group == null)
                    {
                        which = core.Observations.WhichInOrder(obsFilter.Test, ObservationInfo.GroupTimeOrder).ToArray();
                    }
                    else
                    {
                        which = core.Observations.WhichInOrder(z => z.Group == group && obsFilter.Test(z), ObservationInfo.GroupTimeOrder).ToArray();
                    }

                    obs2 = core.Observations.In(which).ToArray();
                    int y = which.Length;

                    if (groupIndex != 0)
                    {
                        ValidateOrder(vectors[groupOffset - 1].Observations, obs2);
                    }

                    ValidateSize(core, vectors.Length, which.Length);

                    for (int i = 0; i < x; i++)
                    {
                        double[] s = new double[y];

                        for (int j = 0; j < y; j++)
                        {
                            s[j] = peaks[i].Observations.Raw[which[j]];
                        }

                        vectors[groupOffset + i] = new Vector(peaks[i], group, null, obs2, s);
                    }
                }
            }

            return new ValueMatrix(useTrend, vectors);
        }

        private static void ValidateSize(Core core, int numIVs, int lenIVs)
        {
            int bytesRequired = (numIVs * lenIVs) * 8;
            int mbRequired = bytesRequired / (1024 * 1024);
            int limit = core.Options.ObjectSizeLimit * 1024 * 1024;

            if (bytesRequired > limit)
            {
                throw new InvalidOperationException("n = " + numIVs + " input vectors, each of length l = " + lenIVs + ". n * l = " + (numIVs * lenIVs) + " elements in the value matrix. 8 bytes per element gives " + (numIVs * lenIVs * 8) + " bytes, or " + mbRequired + " megabytes. ObjectSizeLimit is " + core.Options.ObjectSizeLimit + " Mb. Reduce the number of input vectors by filtering the data, or change the limit from the preferences menu.");
            }
        }

        private static void ValidateOrder(ObservationInfo[] a, ObservationInfo[] b)
        {
            if (a.Length != b.Length)
            {
                throw new InvalidOperationException("The size of the vector for " + a.First().Group + " isn't the same as for " + b.First().Group + ". Try putting constraints on the replicates/timepoints.");
            }

            for (int index = 0; index < a.Length; index++)
            {
                var x = a[index];
                var y = b[index];

                if (x.Time != y.Time)
                {
                    throw new InvalidOperationException("The order of the time points isn't the same for " + x.Group + " as for " + y.Group + ". Try putting constraints on the replicates/timepoints.");
                }

                if (x.Rep != y.Rep)
                {
                    throw new InvalidOperationException("The order of the replicates isn't the same for " + x.Group + " as for " + y.Group + ". Try putting constraints on the replicates/timepoints.");
                }
            }
        }

        private static void ValidateOrder(ConditionInfo[] a, ConditionInfo[] b)
        {
            if (a.Length != b.Length)
            {
                throw new InvalidOperationException("The size of the vector for " + a.First().Group + " isn't the same as for " + b.First().Group + ". Try putting constraints on the timepoints.");
            }

            for (int index = 0; index < a.Length; index++)
            {
                var x = a[index];
                var y = b[index];

                if (x.Time != y.Time)
                {
                    throw new InvalidOperationException("The order of the time points isn't the same for " + x.Group + " as for " + y.Group + ". Try putting constraints on the timepoints.");
                }
            }
        }

        /// <summary>
        /// Gets the vector for the specified peak
        /// TODO: This is quite slow as we need to find the peak index, the peaks will save their index later anyway so if needed
        /// this could be sped up by saving the indices in advance.
        /// </summary>
        internal double[] Extract(Peak peak, GroupInfo group)
        {
            int index = FindIndex(peak, group);
            return Vectors[index].Values;
        }

        /// <summary>
        /// Gets the vector at the specified index.
        /// </summary>
        public double[] this[int index]
        {
            get
            {
                return Vectors[index].Values;
            }
        }

        public int FindIndex(Peak peak, GroupInfo group)
        {
            int index = Vectors.FirstIndexWhere(z => z.Peak == peak && z.Group == group);

            if (index == -1)
            {
                throw new KeyNotFoundException("Cannot find peak \"" + peak + "\" (group \"" + group + "\") in vmatrix. Using a peak as an algorithm parameter (such as the seed peak parameter) when that peak has been excluded by the peak filter is a potential cause of this error.");
            }

            return index;
        }
    }

    [Serializable]
    internal class DistanceMatrix
    {
        public readonly double[,] Values; // Peak, Peak
        public readonly ValueMatrix ValueMatrix;

        public int NumPeaks
        {
            get { return Values.GetLength(0); }
        }

        public IEnumerable<Peak> Peaks
        {
            get { return ValueMatrix.Peaks; }
        }

        public DistanceMatrix(double[,] values, ValueMatrix peaks)
        {
            this.Values = values;
            this.ValueMatrix = peaks;
        }


        /// <summary>
        /// Returns the distance matrix for a set of peaks.
        /// </summary>
        public static DistanceMatrix Create(Core core, ValueMatrix valueMatrix, ConfigurationMetric metric, ProgressReporter prog)
        {
            int n = valueMatrix.NumVectors;

            int bytesRequired = (n * n) * 8;
            int mbRequired = bytesRequired / (1024 * 1024);
            int limit = core.Options.ObjectSizeLimit * 1024 * 1024;

            if (bytesRequired > limit)
            {
                // It ain't gonna happen. I'm not creating a distance matrix over 500Mb
                throw new InvalidOperationException("n = " + n + " input vectors. n * n = " + (n * n) + " elements in the distance matrix. 8 bytes per element gives " + (n * n * 8) + " bytes, or " + mbRequired + " megabytes. ObjectSizeLimit is " + core.Options.ObjectSizeLimit + " Mb. Reduce the number of input vectors by filtering the data, or change the limit from the preferences menu. Some algorithms also have the option to disable the distance matrix.");
            }

            double[,] s = new double[n, n];

            for (int i = 0; i < n; i++)
            {
                prog.SetProgress(i, n);

                for (int j = 0; j < n; j++)
                {
                    s[i, j] = metric.Calculate(valueMatrix[i], valueMatrix[j]);
                }
            }

            return new DistanceMatrix(s, valueMatrix);
        }

        internal double Extract(Peak v1, GroupInfo g1, Peak v2, GroupInfo g2)
        {
            int i = ValueMatrix.FindIndex(v1, g1);
            int j = ValueMatrix.FindIndex(v2, g2);
            return Values[i, j];
        }
    }
}
