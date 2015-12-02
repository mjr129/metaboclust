using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Visualisables;

namespace MetaboliteLevels.Utilities
{
    internal static class Maths
    {
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

        public static T[] StringToArray<T>(string str, Converter<string, T> conversion, string sep = ",")
        {
            string[] elements = str.Split(new[] { sep }, StringSplitOptions.RemoveEmptyEntries);

            T[] result = new T[elements.Length];

            for (int i = 0; i < elements.Length; i++)
            {
                string element = elements[i].Trim();
                result[i] = conversion(element);
            }

            return result;
        }

        public static string ArrayToStringInt(IEnumerable<int> list, string prefix = "")
        {
            if (list == null)
            {
                return null;
            }

            List<int> hs = new List<int>(list);

            hs.Sort();

            if (hs.Count == 0)
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();

            int? onHold = null;

            sb.Append(hs[0]);

            for (int i = 1; i < hs.Count; i++)
            {
                int c = hs[i];

                if (hs[i] == (hs[i - 1] + 1))
                {
                    if (!onHold.HasValue)
                    {
                        // One more!
                        onHold = hs[i - 1];
                    }
                }
                else
                {
                    if (onHold.HasValue)
                    {
                        // Sequence broken
                        int s = onHold.Value;
                        int e = hs[i - 1];
                        onHold = null;

                        WriteFromTo(sb, s, e, prefix);
                    }

                    sb.Append(", ");
                    sb.Append(prefix);
                    sb.Append(c);
                }
            }

            if (onHold.HasValue)
            {
                // Sequence broken
                int s = onHold.Value;
                int e = hs[hs.Count - 1];
                WriteFromTo(sb, s, e, prefix);
            }

            return sb.ToString();
        }

        private static void WriteFromTo(StringBuilder sb, int s, int e, string prefix)
        {
            if (e == s + 1)
            {
                sb.Append(", ");
                sb.Append(prefix);
                sb.Append(e);
            }
            else
            {
                sb.Append("-");
                sb.Append(prefix);
                sb.Append(e);
            }
        }

        public static List<int> StringToArrayInt(string str, EErrorHandler error)
        {
            List<int> result = new List<int>();

            foreach (string elem in str.Split(new[] { ",", ";" }, StringSplitOptions.RemoveEmptyEntries))
            {
                string elem2 = elem.Trim();

                int i = elem2.IndexOf('-', 1);

                if (i != -1)
                {
                    string[] elem3 = elem2.Split("-".ToCharArray(), 2, StringSplitOptions.RemoveEmptyEntries);

                    if (elem3.Length == 2)
                    {
                        int iS, iE;

                        if (int.TryParse(elem3[0].Trim(), out iS)
                            && int.TryParse(elem3[1].Trim(), out iE))
                        {
                            for (int iC = iS; iC <= iE; iC++)
                            {
                                result.Add(iC);
                            }
                        }
                        else
                        {
                            switch (error)
                            {
                                case EErrorHandler.Ignore:
                                    break;

                                case EErrorHandler.ReturnNull:
                                    return null;

                                case EErrorHandler.ThrowError:
                                default:
                                    throw new FormatException("Cannot parse: " + elem2);
                            }
                        }
                    }
                }
                else
                {
                    int iC;

                    if (int.TryParse(elem2, out iC))
                    {
                        result.Add(iC);
                    }
                    else
                    {
                        switch (error)
                        {
                            case EErrorHandler.Ignore:
                                break;

                            case EErrorHandler.ReturnNull:
                                return null;

                            case EErrorHandler.ThrowError:
                            default:
                                throw new FormatException("Cannot parse: " + elem2);
                        }
                    }
                }
            }

            return result;
        }


        /// <summary>
        /// Unique combinations of [count] numbers, with each combination containing [len] elements.
        /// </summary>
        /// <param name="len"></param>
        /// <param name="count"></param>
        /// <returns></returns>
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

        public static T Min<T>(IEnumerable<T> vars) where T : IComparable
        {
            return GenericMax<T>(vars, -1);
        }

        public static T Max<T>(IEnumerable<T> vars) where T : IComparable
        {
            return GenericMax<T>(vars, 1);
        }

        public static double AbsMin(IEnumerable<double> vars)
        {
            return AbsMax(vars, -1);
        }

        public static double AbsMax(IEnumerable<double> vars)
        {
            return AbsMax(vars, 1);
        }

        public static double AbsMax(IEnumerable<double> vars, int v)
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
        /// Calculates the median.
        /// </summary>
        public static double Median(IEnumerable<double> vars)
        {
            return Median(vars.ToArray());
        }

        /// <summary>
        /// Calculates the median.
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

        public static IEnumerable<T> Corresponding<T>(this IEnumerable<T> array1, IEnumerable<bool> array2)
        {
            return Corresponding<T, bool>(array1, array2, λ => λ);
        }

        /// <summary>
        /// (MJR) Yields elements of array where corrsponding elements in array2 are matched by the predicate
        /// </summary>
        public static IEnumerable<T> Corresponding<T, U>(this IEnumerable<T> array1, IEnumerable<U> array2, Predicate<U> predicate)
        {
            var e1 = array1.GetEnumerator();
            var e2 = array2.GetEnumerator();

            while (e1.MoveNext() && e2.MoveNext())
            {
                if (predicate(e2.Current))
                {
                    yield return e1.Current;
                }
            }
        }

        /// <summary>
        /// (MJR) Yields indices of array where the predicate is true (or -1 if not).
        /// </summary>
        public static int FirstIndexWhere<T>(this IEnumerable<T> array, Predicate<T> predicate)
        {
            int n = 0;

            foreach (T t in array)
            {
                if (predicate(t))
                {
                    return n;
                }

                ++n;
            }

            return -1;
        }

        /// <summary>
        /// (MJR) Yields first and only element of array where the predicate is true (or throws an exception).
        /// </summary>
        public static T Only<T>(this IEnumerable<T> array, Predicate<T> predicate)
        {
            T result = default(T);
            bool foundResult = false;

            foreach (T t in array)
            {
                if (predicate(t))
                {
                    if (foundResult)
                    {
                        throw new InvalidOperationException("Multiple elements satisfy condition.");
                    }
                    else
                    {
                        result = t;
                        foundResult = true;
                    }
                }
            }

            if (!foundResult)
            {
                throw new InvalidOperationException("No elements satisfy condition.");
            }

            return result;
        }

        /// <summary>
        /// (MJR) Yields indices of array where the predicate is true (or -1 if not).
        /// </summary>
        public static int IndexOf<T>(this IEnumerable<T> array, T item)
        {
            int n = 0;

            foreach (T t in array)
            {
                if (t.Equals(item))
                {
                    return n;
                }

                ++n;
            }

            return -1;
        }

        /// <summary>
        /// (MJR) Yields indices of array where the predicate is true and returns the results in the specified order.
        /// </summary>
        public static IEnumerable<int> WhichInOrder<T>(this IReadOnlyList<T> array, Predicate<T> predicate, Comparison<T> order)
        {
            int[] which = Which(array, predicate).ToArray();
            T[] vals = In(array, which).ToArray();

            UiControls.Sort(vals, which, order);

            return which;
        }

        /// <summary>
        /// (MJR) Yields indices of array where the predicate is true.
        /// </summary>
        public static IEnumerable<int> Which<T>(this IEnumerable<T> array, Predicate<T> predicate)
        {
            int n = 0;

            foreach (T t in array)
            {
                if (predicate(t))
                {
                    yield return n;
                }

                ++n;
            }
        }

        public static T[] In2<T>(this T[] array, int[] which)
        {
            T[] result = new T[which.Length];

            for (int i = 0; i < which.Length; i++)
            {
                result[i] = array[which[i]];
            }

            return result;
        }

        /// <summary>
        /// (MJR) Yields the elements of array which correspond to the indices of which.
        /// Indices may be specified in any order and duplicates are permitted.
        /// </summary>
        public static IEnumerable<T> In<T>(this T[] array, IEnumerable<int> which)
        {
            foreach (int i in which)
            {
                yield return array[i];
            }
        }

        /// <summary>
        /// (MJR) Yields the elements of array which correspond to the indices of which.
        /// Indices may be specified in any order and duplicates are permitted.
        /// </summary>
        public static IEnumerable<T> In<T>(this IReadOnlyList<T> array, IEnumerable<int> which)
        {
            foreach (int i in which)
            {
                yield return array[i];
            }
        }

        /// <summary>
        /// (MJR) Replaces the items in array with indices "indices" with the sequential values in "values".
        /// </summary>
        public static void ReplaceIn<T>(this T[] array, IEnumerable<int> indices, IEnumerable<T> values)
        {
            var e1 = indices.GetEnumerator();
            var e2 = values.GetEnumerator();

            while (e1.MoveNext() && e2.MoveNext())
            {
                array[e1.Current] = e2.Current;
            }
        }

        public static string ArrayToString(IEnumerable array, string delimiter = ", ")
        {
            if (array == null)
            {
                return null;
            }

            StringBuilder sb = new StringBuilder();

            foreach (object t in array)
            {
                if (sb.Length != 0)
                {
                    sb.Append(delimiter);
                }

                if (t != null)
                {
                    sb.Append(t.ToString());
                }
            }

            return sb.ToString();
        }

        public static string ArrayToString(IEnumerable array, Converter<object, string> conversion, string delimiter = ", ")
        {
            if (array == null)
            {
                return null;
            }

            StringBuilder sb = new StringBuilder();

            foreach (object t in array)
            {
                if (sb.Length != 0)
                {
                    sb.Append(delimiter);
                }

                sb.Append(conversion(t));
            }

            return sb.ToString();
        }

        public static string ArrayToString<T>(IEnumerable<WeakReference<T>> array, Converter<T, string> conversion, string delimiter = ", ")
            where T : class
        {
            return ArrayToString(array, z => SafeToString(z, conversion), delimiter);
        }

        public static string SafeToString<T>(this WeakReference<T> z, Converter<T, string> conversion)
            where T : class
        {
            T a = z.GetTarget();
            return (a == null) ? "〿" : conversion(a);
        }

        public static string SafeToString<T>(this WeakReference<T> z)
            where T : class
        {
            T a = z.GetTarget();
            return (a == null) ? "〿" : a.ToString();
        }

        public static string ArrayToString<T>(IEnumerable<T> array, Converter<T, string> conversion, string delimiter = ", ")
        {
            if (array == null)
            {
                return null;
            }

            StringBuilder sb = new StringBuilder();

            foreach (T t in array)
            {
                if (sb.Length != 0)
                {
                    sb.Append(delimiter);
                }

                sb.Append(conversion(t));
            }

            return sb.ToString();
        }

        internal static string ArrayCount(IList list)
        {
            return list.Count != 0 ? list.Count.ToString() : string.Empty;
        }

        public static T FindHighest<T>(this IEnumerable<T> list, Comparison<T> comparison) where T : class
        {
            T max = null;

            foreach (T t in list)
            {
                if (max == null || comparison(t, max) > 0)
                {
                    max = t;
                }
            }

            return max;
        }

        public static T FindHighest<T>(this IEnumerable<T> list, Converter<T, IComparable> convertor) where T : class
        {
            T max = null;

            foreach (T t in list)
            {
                if (max == null || convertor(t).CompareTo(convertor(max)) > 0)
                {
                    max = t;
                }
            }

            return max;
        }

        public static T FindLowest<T>(this IEnumerable<T> list, Converter<T, IComparable> convertor) where T : class
        {
            T max = null;

            foreach (T t in list)
            {
                if (max == null || convertor(t).CompareTo(convertor(max)) < 0)
                {
                    max = t;
                }
            }

            return max;
        }

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
        /// Splits a string about a symbol
        /// </summary>
        /// <param name="elem">The original string --> the first part</param>
        /// <param name="splitSign">What to split about</param>
        /// <returns>The last part</returns>
        internal static string SplitEquals(ref string elem, string splitSign = "=")
        {
            int i = elem.IndexOf(splitSign);

            if (i != -1)
            {
                string result = elem.Substring(i + 1);
                elem = elem.Substring(0, i);
                return result;
            }
            else
            {
                return null;
            }
        }

        internal static bool SplitEquals(string elem, out string left, out string right, string splitSign = "=")
        {
            int i = elem.IndexOf(splitSign);

            if (i != -1)
            {
                right = elem.Substring(i + 1);
                left = elem.Substring(0, i);
                return true;
            }
            else
            {
                left = elem;
                right = null;
                return false;
            }
        }

        /// <summary>
        /// Describes a as a fraction of b (as a string).
        /// </summary>
        internal static string AsFraction(int a, int b)
        {
            return a.ToString() + " / " + b + " (" + (((double)a / b) * 100).ToString("F00") + "%)";
        }

        /// <summary>
        /// Shuffles a list.
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

        public static readonly Color[] BasicColours = { Color.FromArgb(128, 0, 0), Color.FromArgb(0, 128, 0), Color.FromArgb(0, 0, 128), Color.FromArgb(128, 128, 0), Color.FromArgb(128, 0, 128), Color.FromArgb(0, 128, 128), Color.FromArgb(128, 128, 128), Color.FromArgb(255, 0, 0), Color.FromArgb(0, 255, 0), Color.FromArgb(0, 0, 255), Color.FromArgb(255, 255, 0), Color.FromArgb(255, 0, 255), Color.FromArgb(0, 255, 255) };
        public static readonly string[] BasicColourNames = { "dark red", "dark green", "dark blue", "dark yellow", "dark magenta", "dark cyan", "gray", "bright red", "bright green", "bright blue", "bright yellow", "bright magenta", "bright cyan" };

        internal static Color GetBasicColour(int ci)
        {
            return BasicColours[ci % BasicColours.Length];
        }

        internal static string GetBasicColourName(int index)
        {
            return BasicColourNames[index % BasicColours.Length];
        }

        /// <summary>
        /// (MJR) Displays a double to a specified number of significant digits
        /// </summary>
        static public string SignificantDigits(this double d, int digits = 3)
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

        /*   /// <summary>
           /// Measures the distance between two vectors using the specified metric.
           /// </summary>
           internal static double MeasureDistance(double[] a, double[] b, EDistanceMetric distanceMetric)
           {
               switch (distanceMetric)
               {
                   case EDistanceMetric.Euclidean:
                       return Maths.Euclidean(a, b);

                   case EDistanceMetric.Pearson:
                       return Arr.Instance.Pearson(a, b);

                   case EDistanceMetric.Qian:
                       return Maths.Qian(a, b);

                   default:
                       throw new SwitchException(distanceMetric);
               }
           }   */

        public static double QianDistance(double[] X, double[] Y)
        {
            return -Qian(X, Y);
        }

        /// <summary>
        /// My interpretation of the local clustering distance metric as detailed by Qian et al.
        /// 
        /// Jiang Qian, Marisa Dolled-Filhart, Jimmy Lin, Haiyuan Yu, and Mark Gerstein.
        /// Beyond synexpression relationships: local clustering of time-shifted and inverted
        /// gene expression profiles identifies new, biologically relevant interactions. Journal of
        /// molecular biology, 314(5):1053–1066, 2001.
        /// </summary>
        public static double Qian(double[] X, double[] Y)
        {
            UiControls.Assert(X.Length == Y.Length);

            X = Normalise(X);
            Y = Normalise(Y);

            int c = X.Length;

            // Score matrix M
            // Mij = Xi * Yj
            double[,] M = new double[c, c];

            for (int i = 0; i < c; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    M[i, j] = X[i] * Y[j];
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

        private static double[] Normalise(double[] X)
        {
            double mean = Mean(X);
            double sd = StdDev(X, mean);
            double[] Xprime = X.Select(λ => (λ - mean) / sd).ToArray();

            return Xprime;
        }

        /// <summary>
        /// Returns the value of the largest absolute value
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

    class ParseElementCollectionConverter : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                return new ParseElementCollection((string)value);
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return ParseElementCollection.GetOriginal((ParseElementCollection)value);
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    /// <summary>
    /// Collection of 'ParseElement's. 
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(ParseElementCollectionConverter))]
    class ParseElementCollection
    {
        /// <summary>
        /// A string and whether it is in brackets.
        /// </summary>
        [Serializable]
        private class ParseElement
        {
            public readonly string Value;
            public readonly bool IsInBrackets;

            public ParseElement(string value, bool isInBrackets)
            {
                this.Value = value;
                this.IsInBrackets = isInBrackets;
            }

            public override string ToString()
            {
                if (IsInBrackets)
                {
                    return "{" + Value + "}";
                }
                else
                {
                    return Value;
                }
            }
        }

        private readonly List<ParseElement> _contents;

        /// <summary>
        /// Parses a string like
        /// abc{xyz}abc{xyz}
        /// </summary>
        public ParseElementCollection(string x)
        {
            _contents = new List<ParseElement>();
            StringBuilder sb = new StringBuilder();
            bool isOpen = false;

            // Could be more efficient but it suitable for purpose
            foreach (char c in x)
            {
                if (!isOpen && c == '{')
                {
                    if (sb.Length != 0)
                    {
                        _contents.Add(new ParseElement(sb.ToString(), false));
                    }
                    sb.Clear();
                    isOpen = true;
                }
                else if (isOpen && c == '}')
                {
                    _contents.Add(new ParseElement(sb.ToString(), true));
                    sb.Clear();
                    isOpen = false;
                }
                else
                {
                    sb.Append(c);
                }
            }

            if (sb.Length != 0)
            {
                _contents.Add(new ParseElement(sb.ToString(), isOpen));
            }
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <param name="propertyTarget">Elements {!xxx} will get the xxx property from this object, elements {xxx} will get the xxx value from QueryValue(xxx)</param>
        /// <returns>String</returns>
        public string ConvertToString(IVisualisable propertyTarget, Core core)
        {
            var r = new StringBuilder();

            foreach (var x in _contents)
            {
                if (x.IsInBrackets)
                {
                    r.Append(propertyTarget.QueryProperty(x.Value, core) ?? "");
                }
                else
                {
                    r.Append(x.Value);
                }
            }

            return r.ToString();
        }

        private string GetOriginal()
        {
            return string.Join(string.Empty, _contents);
        }

        public static string GetOriginal(ParseElementCollection collection)
        {
            if (collection == null)
            {
                return string.Empty;
            }

            return collection.GetOriginal();
        }

        public static bool IsNullOrEmpty(ParseElementCollection collection)
        {
            return (collection == null || collection._contents.Count == 0
                    || (collection._contents.Count == 1 && string.IsNullOrWhiteSpace(collection._contents[0].Value)));
        }
    }
}
