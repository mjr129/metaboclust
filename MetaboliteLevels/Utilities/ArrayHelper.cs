using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Algorithms;

namespace MetaboliteLevels.Utilities
{
    static class ArrayHelper
    {
        /// <summary>
        /// (MJR) Is the enum empty?
        /// </summary>
        public static bool IsEmpty(this IEnumerable enumerable)
        {
            return !enumerable.GetEnumerator().MoveNext();
        }

        /// <summary>
        /// (MJR) Returns the unique elements.
        /// </summary>
        public static HashSet<T> Unique<T>(this IEnumerable<T> self)
        {
            return new HashSet<T>(self);
        }

        /// <summary>
        /// (MJR) Like Linq.Concat for single elements
        /// </summary>
        public static IEnumerable<T> ConcatSingle<T>(this IEnumerable<T> self, T toAdd)
        {
            return self.Concat(new T[] { toAdd });
        }

        /// <summary>
        /// (MJR) Appends item(s) to an array, returns the result.
        /// </summary>
        public static T[] Append<T>(this T[] self, params T[] toAdd)
        {
            int p = self.Length;
            Array.Resize(ref self, self.Length + toAdd.Length);

            for (int i = 0; i < toAdd.Length; i++)
            {
                self[p + i] = toAdd[i];
            }

            return self;
        }

        /// <summary>
        /// (MJR) Converts or returns a reference to the list.
        /// </summary>
        public static List<T> AsList<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
            {
                return null;
            }

            var list = enumerable as List<T>;

            if (list != null)
            {
                return list;
            }

            return enumerable.ToList();
        }

        /// <summary>
        /// (MJR) Replaces the contents of this list with another.
        /// </summary>
        public static void ReplaceAll<T>(this List<T> self, IEnumerable<T> newContent)
        {
            if (newContent == self)
            {
                return;
            }

            self.Clear();
            self.AddRange(newContent);
        }

        public static void AddRange<T>(this HashSet<T> self, IEnumerable<T> toAdd)
        {
            foreach (T o in toAdd)
            {
                self.Add(o);
            }
        }

        public static void AddRange<TKey, TValue>(this Dictionary<TKey, TValue> self, Dictionary<TKey, TValue> toAdd)
        {
            foreach (KeyValuePair<TKey, TValue> kvp in toAdd)
            {
                self.Add(kvp.Key, kvp.Value);
            }
        }

        public static void AddRange<TKey, TValue>(this Dictionary<TKey, TValue> self, IEnumerable<TValue> toAdd, Converter<TValue, TKey> keySelector)
        {
            foreach (TValue kvp in toAdd)
            {
                self.Add(keySelector(kvp), kvp);
            }
        }

        public static void AddRange<TKey, TValue, TEnum>(this Dictionary<TKey, TValue> self, IEnumerable<TEnum> toAdd, Converter<TEnum, TKey> keySelector, Converter<TEnum, TValue> valueSelector)
            where TEnum : struct, IComparable, IFormattable, IConvertible // aka. Enum
        {
            foreach (TEnum kvp in toAdd)
            {
                self.Add(keySelector(kvp), valueSelector(kvp));
            }
        }

        /// <summary>
        /// (MJR) Returns the indices of the enumerable
        /// </summary>
        public static IEnumerable<int> Indices(this IList self)
        {
            return Enumerable.Range(0, self.Count);
        }

        /// <summary>
        /// (MJR) Compares all adjacent elements
        /// </summary>
        internal static bool CompareAdjacent<T>(this IEnumerable<T> self, Func<T, T, bool> comparer)
        {
            T prevT = default(T);
            bool hasT = false;

            foreach (T t in self)
            {
                if (hasT)
                {
                    if (!comparer(prevT, t))
                    {
                        return false;
                    }
                }
                else
                {
                    hasT = true;
                }

                prevT = t;
            }

            return true;
        }

        /// <summary>
        /// (MJR) Returns the indices of the enumerable
        /// </summary>
        public static IEnumerable<int> Indices(this IEnumerable self)
        {
            var e = self.GetEnumerator();
            int i = 0;

            while (e.MoveNext())
            {
                yield return ++i;
            }
        }

        /// <summary>
        /// (MJR) Yields a set of KVPs containing the objects and their indices
        /// </summary>
        public static IEnumerable<KeyValuePair<int, T>> IndicesAndObject<T>(this IEnumerable<T> self)
        {
            int i = 0;

            foreach (T o in self)
            {
                yield return new KeyValuePair<int, T>(i++, o);
            }
        }

        /// <summary>
        /// (MJR) Returns the value of the [key], if not present a new one is automatically added using new [TValue]().
        /// </summary>
        internal static TValue GetOrNew<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key)
            where TValue : new()
        {
            TValue value;

            if (!dict.TryGetValue(key, out value))
            {
                value = new TValue();
                dict.Add(key, value);
            }

            return value;
        }

        /// <summary>
        /// (MJR) Returns the value of the [key], if not present a new one is automatically added using [creator].
        /// </summary>
        internal static TValue GetOrCreate<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, Converter<TKey, TValue> creator)
        {
            TValue value;

            if (!dict.TryGetValue(key, out value))
            {
                value = creator(key);
                dict.Add(key, value);
            }

            return value;
        }

        /// <summary>
        /// (MJR) Returns the value of the [key], if not present a new one is automatically added using new [TValue]()
        /// and initialised using [action].
        /// </summary>
        internal static TValue GetOrNew<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, Action<TKey, TValue> action)
            where TValue : new()
        {
            TValue value;

            if (!dict.TryGetValue(key, out value))
            {
                value = new TValue();
                dict.Add(key, value);
                action(key, value);
            }

            return value;
        }

        /// <summary>
        /// (MJR) Returns the value of the [key], if not present returns double.NaN.
        /// </summary>
        internal static double GetOrNan<TKey>(this Dictionary<TKey, double> dict, TKey key)
        {
            double value;

            if (!dict.TryGetValue(key, out value))
            {
                return double.NaN;
            }

            return value;
        }

        /// <summary>
        /// (MJR) Returns the value of the [key], if not present returns [defaultValue].
        /// </summary>
        internal static TValue GetOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue defaultValue)
        {
            TValue value;

            if (!dict.TryGetValue(key, out value))
            {
                return defaultValue;
            }

            return value;
        }

        /// <summary>
        /// (MJR) Counts items in enumerable. Iteratively.
        /// </summary>
        public static int CountAll(this IEnumerable self)
        {
            int i = 0;
            var e = self.GetEnumerator();

            while (e.MoveNext())
            {
                i++;
            }

            return i;
        }

        /// <summary>
        /// Truncates the list to the specified size.
        /// </summary>
        internal static void TrimList<T>(List<T> list, int max)
        {
            if (list.Count > max)
            {
                list.RemoveRange(max, list.Count - max);
            }
        }

        /// <summary>
        /// (MJR) Converts an enumerable to a dictionary.
        /// </summary>
        internal static Dictionary<TKey, TValue> ToDictionary<TEnumerable, TKey, TValue>(this IEnumerable<TEnumerable> enumerable, Converter<TEnumerable, TKey> keySelector, Converter<TEnumerable, TValue> valueSelector)
        {
            Dictionary<TKey, TValue> r = new Dictionary<TKey, TValue>(enumerable.Count());

            foreach (TEnumerable e in enumerable)
            {
                r.Add(keySelector(e), valueSelector(e));
            }

            return r;
        }

        /// <summary>
        /// Returns the index of the first difference between two lists.
        /// </summary>
        internal static int GetIndexOfFirstDifference<T>(IEnumerable<T> dest, IEnumerable<T> source)
        {
            // if its new
            // if anything before has been added
            // if anything before has been removed
            // if anything before has been changed
            var d = dest.GetEnumerator();
            var s = source.GetEnumerator();
            int i = 0;

            while (s.MoveNext())
            {
                if (!d.MoveNext() || !d.Current.Equals(s.Current))
                {
                    return i;
                }

                i++;
            }

            return i;
        }

        /// <summary>
        /// Sorts the [values] based on [order].
        /// </summary>
        internal static void Sort<TKey, TValue>(TKey[] keys, TValue[] values, Comparison<TKey> order)
        {
            Array.Sort(keys, values, new ComparisonComparer<TKey>(order));
        }

        /// <summary>
        /// (MJR) Merges two sequences into a tuple
        /// </summary>
        internal static IEnumerable<Tuple<T1, T2>> Zip<T1, T2>(this IEnumerable<T1> a, IEnumerable<T2> b)
        {
            var x = a.GetEnumerator();
            var y = b.GetEnumerator();

            while (x.MoveNext() && y.MoveNext())
            {
                yield return new Tuple<T1, T2>(x.Current, y.Current);
            }
        }

        /// <summary>
        /// (MJR) Merges three sequences into a tuple
        /// </summary>
        internal static IEnumerable<Tuple<T1, T2, T3>> Zip<T1, T2, T3>(this IEnumerable<T1> a, IEnumerable<T2> b, IEnumerable<T3> c)
        {
            var x = a.GetEnumerator();
            var y = b.GetEnumerator();
            var z = c.GetEnumerator();

            while (x.MoveNext() && y.MoveNext() && z.MoveNext())
            {
                yield return new Tuple<T1, T2, T3>(x.Current, y.Current, z.Current);
            }
        }

        /// <summary>
        /// (MJR) Returns the first item in the IEnumerable or null if the IEnumerable is empty.
        /// </summary>
        internal static object FirstOrDefault2(this IEnumerable self)
        {
            var e = self.GetEnumerator();

            if (e.MoveNext())
            {
                return e.Current;
            }

            return null;
        }

        /// <summary>
        /// (MJR) Returns the first item in the IEnumerable or [default] if the IEnumerable is empty.
        /// </summary>
        internal static T FirstOrDefault<T>(this IEnumerable<T> self, T @default)
        {
            if (self == null)
            {
                return @default;
            }

            IEnumerator<T> e = self.GetEnumerator();

            if (e.MoveNext())
            {
                return e.Current;
            }

            return @default;
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

            ArrayHelper.Sort(vals, which, order);

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
    }
}
