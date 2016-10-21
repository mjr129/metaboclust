using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using MetaboliteLevels.Data.Session;

namespace MetaboliteLevels.Utilities
{
    internal static class Maths
    {
        /// <summary>
        /// A set of basic colours.
        /// </summary>
        public static readonly Color[] BasicColours = { Color.FromArgb(128, 0, 0), Color.FromArgb(0, 128, 0), Color.FromArgb(0, 0, 128), Color.FromArgb(128, 128, 0), Color.FromArgb(128, 0, 128), Color.FromArgb(0, 128, 128), Color.FromArgb(128, 128, 128), Color.FromArgb(255, 0, 0), Color.FromArgb(0, 255, 0), Color.FromArgb(0, 0, 255), Color.FromArgb(255, 255, 0), Color.FromArgb(255, 0, 255), Color.FromArgb(0, 255, 255) };

        /// <summary>
        /// The names of the [BasicColours] array.
        /// </summary>
        public static readonly string[] BasicColourNames = { "dark red", "dark green", "dark blue", "dark yellow", "dark magenta", "dark cyan", "gray", "bright red", "bright green", "bright blue", "bright yellow", "bright magenta", "bright cyan" };

        /// <summary>
        /// Calculates the Euclidean distance between two vectors.
        /// </summary>
        public static double Euclidean(double[] a, double[] b)
        {
            double t = 0;

            for (int n = 0; n < a.Length; n++)
            {
                double d = a[n] - b[n];
                t += d * d;
            }

            t = Math.Sqrt(t);

            return t;
        }

        /// <summary>
        /// Calculates the RMS distance between two vectors.
        /// </summary>
        public static double RootMeanSquare(double[] a, double[] b)
        {
            double t = 0;

            for (int n = 0; n < a.Length; n++)
            {
                double d = a[n] - b[n];
                t += d * d;
            }

            t = Math.Sqrt(t);

            return t;
        }             

        /// <summary>
        /// Returns the unique combinations of [count] numbers, with each combination containing
        /// [len] elements.
        /// </summary>
        public static IEnumerable<int[]> UniqueCombinations(int len, int count)
        {
            // A special case if len is greater than the count then just return the maximum number we can
            // i.e. 1 result, e.g. "1, 2, 3" even if count = 4 was requested.
            if (len > count)
            {
                len = count;
            }

            int[] a = new int[len];
            int n;

            for (n = 0; n < a.Length; n++)
            {
                a[n] = n;
            }

            while (true)
            {
                yield return a;

                n = len - 1;
                a[n]++;

                while (a[n] == count - (a.Length - n - 1))
                {
                    if (n == 0)
                    {
                        break;
                    }

                    a[n - 1]++;

                    for (int m = n; m < a.Length; m++)
                    {
                        a[m] = a[m - 1] + 1;
                    }
                    n -= 1;
                }

                if (a[0] == count - (a.Length - 1))
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Returns the minimum of vars.
        /// </summary>                  
        public static T Min<T>(IEnumerable<T> vars) where T : IComparable
        {
            return GenericMax<T>(vars, -1);
        }

        /// <summary>
        /// Returns the maximum of vars.
        /// </summary>                  
        public static T Max<T>(IEnumerable<T> vars) where T : IComparable
        {
            return GenericMax<T>(vars, 1);
        }

        /// <summary>
        /// Returns the absolute minimum of vars.
        /// </summary>                  
        public static double AbsMin(IEnumerable<double> vars)
        {
            return AbsMax(vars, -1);
        }

        /// <summary>
        /// Returns the absolute maximum of vars.
        /// </summary>                  
        public static double AbsMax(IEnumerable<double> vars)
        {
            return AbsMax(vars, 1);
        }

        /// <summary>
        /// Returns the absolute minimum ([v] = -1) or maximum ([v] = 1) of vars.
        /// </summary>                  
        private static double AbsMax(IEnumerable<double> vars, int v)
        {
            bool assigned = false;
            double absResult = 0;
            double result = 0;

            foreach (double val in vars)
            {
                double absVal = Math.Abs(val);

                if (!assigned || absVal.CompareTo(absResult) == v)
                {
                    assigned = true;
                    absResult = absVal;
                    result = val;
                }
            }

            UiControls.Assert(assigned, "Maths.GenericMax: Expects at least one value.");

            return result;
        }

        /// <summary>
        /// Returns the minimum ([v] = -1) or maximum ([v] = 1) of vars.
        /// </summary>               
        private static T GenericMax<T>(IEnumerable<T> vars, int v) where T : IComparable
        {
            bool assigned = false;
            T result = default(T);

            foreach (T var in vars)
            {
                if (!assigned || var.CompareTo(result) == v)
                {
                    assigned = true;
                    result = var;
                }
            }

            UiControls.Assert(assigned, "Maths.GenericMax: Expects at least one value.");

            return result;
        }

        /// <summary>
        /// Returns the median of vars.
        /// </summary>
        public static double Median(IEnumerable<double> vars)
        {
            return Median(vars.ToArray());
        }

        /// <summary>
        /// Calculates the median of vars.
        /// </summary>
        public static double Median(double[] vars)
        {
            Array.Sort<double>(vars);

            if (vars.Length % 2 == 0)
            {
                return (vars[(vars.Length / 2) - 1] + vars[(vars.Length / 2)]) / 2;
            }
            else
            {
                return vars[vars.Length / 2];
            }
        }

        /// <summary>
        /// Returns the highest of [vars] as defined by the [comparison].
        /// </summary>                                                   
        public static T FindHighest<T>(this IEnumerable<T> vars, Comparison<T> comparison) where T : class
        {
            T max = null;

            foreach (T t in vars)
            {
                if (max == null || comparison(t, max) > 0)
                {
                    max = t;
                }
            }

            return max;
        }

        /// <summary>
        /// Returns the highest of [vars] as defined by the [convertor].
        /// </summary>                                                   
        public static T FindHighest<T>(this IEnumerable<T> list, Converter<T, IComparable> convertor) where T : class
        {
            T max = null;
            IComparable maxc = null;

            foreach (T t in list)
            {                  
                if (max == null || convertor(t).CompareTo(maxc) > 0)
                {
                    max = t;
                    maxc = convertor(max);
                }
            }

            return max;
        }

        /// <summary>
        /// Returns the lowest of [vars] as defined by the [convertor].
        /// </summary>              
        public static T FindLowest<T>(this IEnumerable<T> list, Converter<T, IComparable> convertor) where T : class
        {
            T max = null;
            IComparable maxc = null;

            foreach (T t in list)
            {
                if (max == null || convertor(t).CompareTo(maxc) < 0)
                {
                    max = t;
                    maxc = convertor(max);
                }
            }

            return max;
        }

        /// <summary>
        /// Returns the lowest of [vars] as defined by the [comparison].
        /// </summary>              
        public static T FindLowest<T>(this IEnumerable<T> list, Comparison<T> comparison) where T : class
        {
            T max = null;

            foreach (T t in list)
            {
                if (max == null || comparison(t, max) < 0)
                {
                    max = t;
                }
            }

            return max;
        }                
    

        /// <summary>
        /// Shuffles a list.
        /// Modifies the original list.
        /// </summary>
        public static void Shuffle<T>(this IList<T> list, int seed = 0)
        {
            Random rng = seed == 0 ? new Random() : new Random(seed);
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        /// <summary>
        /// Returns the basic colour indexed by [i] (looping - any value is valid).
        /// </summary>             
        internal static Color GetBasicColour(int i)
        {
            return BasicColours[i % BasicColours.Length];
        }

        /// <summary>
        /// Returns the name of the basic colour indexed by [i] (looping - any value is valid).
        /// </summary>             
        internal static string GetBasicColourName(int index)
        {
            return BasicColourNames[index % BasicColours.Length];
        }

        /// <summary>
        /// (MJR) Displays a double to a specified number of significant digits
        /// </summary>
        public static string SignificantDigits(this double d, int digits = 3)
        {
            int magnitude = (d == 0.0) ? 0 : (int)Math.Floor(Math.Log10(Math.Abs(d))) + 1;

            if (magnitude < -3 || magnitude > 3)
            {
                return d.ToString("E" + (digits - 1)).Replace("-0", "-").Replace("-0", "-");
            }

            digits -= magnitude;
            if (digits < 0)
                digits = 0;
            string fmt = "f" + digits.ToString();
            return d.ToString(fmt);
        }

        /// <summary>
        /// Searches a list of lists to find another list.
        /// Elements may be in any order (doesn't handle repeats).
        /// </summary>
        internal static int FindMatch<T>(List<List<T>> toSearch, List<T> toFind)
        {        
            for (int i = 0; i < toSearch.Count; i++)
            {
                List<T> toCompare = toSearch[i];

                if (toCompare.Count == toFind.Count)
                {
                    bool match = true;

                    foreach (T element in toFind)
                    {
                        if (!toCompare.Contains(element))
                        {
                            match = false;
                            break;
                        }
                    }

                    if (match)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        /// <summary>
        /// Calculates the mean of a set of values.
        /// </summary>
        internal static double Mean(IEnumerable<double> values)
        {
            double total = 0;
            int count = 0;

            foreach (double d in values)
            {
                total += d;
                ++count;
            }

            return total / count;
        }

        /// <summary>
        /// Calculates the sum of a set of values.
        /// </summary>
        internal static double Sum(IEnumerable<double> values)
        {
            double total = 0;   

            foreach (double d in values)
            {
                total += d;  
            }

            return total;
        }

        /// <summary>
        /// Calculates the negative sum of a set of values.
        /// </summary>
        internal static double NegSum(IEnumerable<double> values)
        {
            double total = 0;

            foreach (double d in values)
            {
                total -= d;
            }

            return total;
        }

        /// <summary>
        /// Calculates the standard deviation of a set of values.
        /// For a faster version if the mean is already known use the other overload.
        /// </summary>
        internal static double StdDev(IEnumerable<double> values)
        {
            return StdDev(values, Mean(values));
        }

        /// <summary>
        /// Calculates the standard deviation of a set of values.
        /// Quicker if the mean is already known.
        /// </summary>
        internal static double StdDev(IEnumerable<double> values, double mean)
        {
            double total = 0;
            int count = 0;

            foreach (double d in values)
            {
                total += Math.Pow(d - mean, 2);
                ++count;
            }

            return Math.Sqrt(total / count);
        }      

        /// <summary>
        /// Retuns the Qian distance between two vectors.
        /// </summary>                                   
        public static double QianDistance(double[] X, double[] y)
        {
            return -Qian(X, y);
        }

        /// <summary>
        /// My interpretation of the local clustering distance metric as detailed by Qian et al.
        /// 
        /// Jiang Qian, Marisa Dolled-Filhart, Jimmy Lin, Haiyuan Yu, and Mark Gerstein.
        /// Beyond synexpression relationships: local clustering of time-shifted and inverted
        /// gene expression profiles identifies new, biologically relevant interactions. Journal of
        /// molecular biology, 314(5):1053–1066, 2001.
        /// </summary>
        public static double Qian(double[] X, double[] y)
        {
            UiControls.Assert(X.Length == y.Length, "Qian metric, X and Y lengths differ");

            X = Normalise(X);
            y = Normalise(y);

            int c = X.Length;

            // Score matrix M
            // Mij = Xi * Yj
            double[,] M = new double[c, c];

            for (int i = 0; i < c; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    M[i, j] = X[i] * y[j];
                }
            }

            // Sum matrices D/E
            // Eij = max( Mij * Ei-1j-1 , 0)
            double[,] D = new double[c, c];
            double[,] E = new double[c, c];

            for (int i = 0; i < c; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    double d;
                    double e;

                    if (i != 0 && j != 0)
                    {
                        d = D[i - 1, j - 1];
                        e = E[i - 1, j - 1];
                    }
                    else
                    {
                        d = 0d;
                        e = 0d;
                    }

                    D[i, j] = Math.Max(d - M[i, j], 0d);
                    E[i, j] = Math.Max(e + M[i, j], 0d);
                }
            }

            // Match score s
            double s = E[0, 0];

            for (int i = 0; i < c; i++)
            {
                s = Maths.AbsMax(s, E[i, c - 1]);
                s = Maths.AbsMax(s, E[c - 1, i]);
                s = Maths.AbsMax(s, D[c - 1, i]);
                s = Maths.AbsMax(s, D[c - 1, i]);
            }

            return s;
        }

        /// <summary>
        /// Normalises the vector.
        /// </summary>            
        private static double[] Normalise(double[] X)
        {
            double mean = Mean(X);
            double sd = StdDev(X, mean);
            double[] Xprime = X.Select(λ => (λ - mean) / sd).ToArray();

            return Xprime;
        }

        /// <summary>
        /// Returns the absolute maximum of two numbers.
        /// </summary>
        private static double AbsMax(double a, double b)
        {
            if (Math.Abs(a) >= Math.Abs(b))
            {
                return a;
            }
            else
            {
                return b;
            }
        }
    }    
}
