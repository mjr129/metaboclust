using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGui.Helpers;

namespace MetaboliteLevels.Gui.Datatypes
{
    /// <summary>
    /// An item of type "T" with a name.
    /// Useful for listboxes, or to name things.
    /// </summary>
    class NamedItem<T> : IComparable<NamedItem<T>>, IComparable
    {
        public string DisplayName;
        public T Value;

        public NamedItem(T value, string displayName)
        {
            this.Value = value;
            this.DisplayName = displayName;
        }

        public NamedItem(T value, Converter<T, string> displayName)
        {
            this.Value = value;
            this.DisplayName = displayName(value);
        }

        public NamedItem(T value)
        {
            this.Value = value;
            this.DisplayName = value != null ? value.ToString() : string.Empty;
        }

        public override string ToString()
        {
            return this.DisplayName;
        }

        public override bool Equals(object obj)
        {
            // Named item - compare values
            NamedItem<T> asNamedItem = obj as NamedItem<T>;

            if (asNamedItem != null)
            {
                if (this.Value == null)
                {
                    return asNamedItem.Value == null;
                }

                return this.Value.Equals(asNamedItem.Value);
            }

            // Direct value - compare to my value
            if (this.Value == null)
            {
                return obj == null;
            }
            else
            {
                return this.Value.Equals(obj);
            }
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
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

        public int CompareTo( NamedItem<T> other )
        {
            return StringHelper.StrCmpLogicalW( this.DisplayName, other?.DisplayName );
        }

        public int CompareTo( object other )
        {
            return StringHelper.StrCmpLogicalW( this.ToString(), other?.ToString() );
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
