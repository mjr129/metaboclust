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
using System.Diagnostics;

namespace MetaboliteLevels.Algorithms
{
  

    /// <summary>
    /// A matrix of Vector rows, where each vector represents an input
    /// </summary>
    [Serializable]
    class ValueMatrix
    {
        public readonly bool UsesTrend;     // Do the vectors use [Vector.Observations] or [Vector.Trend]
        public readonly Vector[] Vectors;   // The vectors

        /// <summary>
        /// (Private) Constructor.
        /// </summary>            
        private ValueMatrix(bool useTrend, Vector[] vectors)
        {
            this.UsesTrend = useTrend;
            this.Vectors = vectors;
        }

        /// <summary>
        /// Peaks in the matrix.
        /// </summary>
        public IEnumerable<Peak> Peaks
        {
            get { return Vectors.Select(z => z.Peak); }
        }

        /// <summary>
        /// Number of vectors.
        /// </summary>
        public int NumVectors
        {
            get { return Vectors.Length; }
        }

        /// <summary>
        /// Gets a flat array representing the vectors.
        /// </summary>           
        public double[,] Flatten()
        {
            return ArrayHelper.Flatten(Vectors.Select(z => z.Values).ToArray());
        }

        /// <summary>
        /// Returns if the peaks in the matrix have been split to one vector per experimental groups.
        /// </summary>
        public bool HasSplitGroups
        {
            get
            {
                return Vectors[0].Group != null;
            }
        }

        private int[] GetFilterIndices(ObsFilter ofilter)
        {
            if (this.HasSplitGroups)
            {
                throw new InvalidOperationException("GetFilterIndices invalid where SplitGroups is true.");
            }

            if (ofilter == null)
            {
                return Enumerable.Range(0, this.Vectors[0].Values.Length).ToArray();
            }

            if (this.UsesTrend)
            {
                return this.Vectors[0].Conditions.Which(z => ofilter.Test(z)).ToArray();
            }
            else
            {
                return this.Vectors[0].Observations.Which(z => ofilter.Test(z)).ToArray();
            }
        }

        /// <summary>
        /// Creates a ValueMatrix from an existing one by applying [obsFilter].
        /// </summary>              
        public static ValueMatrix Create(ValueMatrix original, ObsFilter obsFilter, out int[] filteredIndices)
        {
            filteredIndices = original.GetFilterIndices(obsFilter);
            Vector[] newVectors = new Vector[original.Vectors.Length];

            if (original.UsesTrend)
            {
                Debug.Assert(original.Vectors.CompareAdjacent((x, y) => object.ReferenceEquals(x.Conditions, y.Conditions)));

                var conditions = original.Vectors[0].Conditions.In(filteredIndices).ToArray(); // We assume all condition arrays are equal

                for (int r = 0; r < original.NumVectors; r++)
                {
                    Vector o = original.Vectors[r];
                    newVectors[r] = new Vector(o.Peak, o.Group, conditions, null, o.Values.In(filteredIndices).ToArray(), r);
                }
            }
            else
            {
                Debug.Assert(original.Vectors.CompareAdjacent((x, y) => object.ReferenceEquals(x.Observations, y.Observations)));

                var observations = original.Vectors[0].Observations.In(filteredIndices).ToArray(); // We assume all observation arrays are equal

                for (int r = 0; r < original.NumVectors; r++)
                {
                    Vector o = original.Vectors[r];
                    newVectors[r] = new Vector(o.Peak, o.Group, null, observations, o.Values.In(filteredIndices).ToArray(), r);
                }
            }

            return new ValueMatrix(original.UsesTrend, newVectors);
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
            ConditionInfo[] conditions;
            ObservationInfo[] observations;
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

                    conditions = core.Conditions.In(which).ToArray();
                    int y = which.Length;

                    if (groupIndex != 0)
                    {
                        ValidateOrder(vectors[groupOffset - 1].Conditions, conditions);
                    }

                    ValidateSize(core, vectors.Length, which.Length);

                    for (int i = 0; i < x; i++)
                    {
                        double[] s = new double[y];

                        for (int j = 0; j < y; j++)
                        {
                            s[j] = peaks[i].Observations.Trend[which[j]];
                        }

                        vectors[groupOffset + i] = new Vector(peaks[i], group, conditions, null, s, groupOffset + i);
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

                    observations = core.Observations.In(which).ToArray();
                    int y = which.Length;

                    if (groupIndex != 0)
                    {
                        ValidateOrder(vectors[groupOffset - 1].Observations, observations);
                    }

                    ValidateSize(core, vectors.Length, which.Length);

                    for (int i = 0; i < x; i++)
                    {
                        double[] s = new double[y];

                        for (int j = 0; j < y; j++)
                        {
                            s[j] = peaks[i].Observations.Raw[which[j]];
                        }

                        vectors[groupOffset + i] = new Vector(peaks[i], group, null, observations, s, groupOffset + i);
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

        /// <summary>
        /// Finds the index of a vector.
        /// </summary>                  
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
}
