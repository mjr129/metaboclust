using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaboliteLevels.Controls
{
    /// <summary>
    /// An item of type "T" with a name.
    /// Useful for listboxes, or to name things.
    /// </summary>
    class NamedItem<T>
    {
        public string DisplayName;
        public T Value;

        public NamedItem(T value, string displayName)
        {
            Value = value;
            DisplayName = displayName;
        }

        public NamedItem(T value, Converter<T, string> displayName)
        {
            Value = value;
            DisplayName = displayName(value);
        }

        public NamedItem(T value)
        {
            Value = value;
            DisplayName = value != null ? value.ToString() : string.Empty;
        }

        public override string ToString()
        {
            return DisplayName;
        }

        public override bool Equals(object obj)
        {
            // Named item - compare values
            NamedItem<T> asNamedItem = obj as NamedItem<T>;

            if (asNamedItem != null)
            {
                if (Value == null)
                {
                    return asNamedItem.Value == null;
                }

                return Value.Equals(asNamedItem.Value);
            }

            // Direct value - compare to my value
            if (Value == null)
            {
                return obj == null;
            }
            else
            {
                return Value.Equals(obj);
            }
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        internal static T Extract(object p)
        {
            NamedItem<T> v = p as NamedItem<T>;

            if (v == null)
            {
                return default(T);
            }

            return v.Value;
        }
    }

    /// <summary>
    /// Methods for NamedItem(of T)
    /// </summary>
    static class NamedItem
    {
        public static NamedItem<T> Create<T>(T value, string name)
        {
            return new NamedItem<T>(value, name);
        }

        public static NamedItem<T> Create<T>(T value)
        {
            return new NamedItem<T>(value);
        }

        public static NamedItem<T> Create<T>(T value, Converter<T, string> nameGenerator)
        {
            return new NamedItem<T>(value, nameGenerator);
        }

        public static IEnumerable<NamedItem<T>> GetRange<T>(IEnumerable<T> values, Converter<T, string> displayNames)
        {
            return values.Select(z => new NamedItem<T>(z, displayNames));
        }

        public static IEnumerable<NamedItem<T>> GetRange<T>(IEnumerable<T> values)
        {
            return values.Select(z => new NamedItem<T>(z));
        }

        public static IEnumerable<NamedItem<T>> GetRange<T>(IEnumerable<T> values, IEnumerable displayNames)
        {
            var v = values.GetEnumerator();
            var n = displayNames.GetEnumerator();

            while (v.MoveNext() && n.MoveNext())
            {
                yield return new NamedItem<T>(v.Current, n.Current.ToString());
            }
        }
    }
}
