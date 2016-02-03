using MetaboliteLevels.Data.Visualisables;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Session;
using System.Runtime.Serialization;
using MetaboliteLevels.Utilities;
using System.Collections;
using MetaboliteLevels.Settings;

namespace MetaboliteLevels.Viewers.Lists
{
    public enum EColumn
    {
        None,
        Visible,
        Meta,
        Statistic
    }

    enum EListDisplayMode
    {
        Smart = 0,
        Count = 1,
        Content = 2,
        [Name("Count and content")] CountAndContent = 3,
    }

    abstract class Column
    {
        public readonly string Id;
        public readonly string Description;
        public readonly EColumn Special;

        public ColumnHeader Header;
        public string OverrideDisplayName;
        public bool Visible;
        public int Width = 128;
        public bool DisableMenu;
        public int DisplayIndex;
        public EListDisplayMode DisplayMode = EListDisplayMode.Smart;

        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public Column(string name, EColumn special, string description)
        {
            this.Id = name;
            this.OverrideDisplayName = null;
            this.Special = special;
            this.Visible = special == EColumn.Visible;
            this.Description = description;
        }

        public string Name
        {
            get
            {
                return OverrideDisplayName ?? Id;
            }
        }

        /// <summary>
        /// Gets ID (or display name if set).
        /// </summary>
        public override string ToString()
        {
            if (string.IsNullOrEmpty(OverrideDisplayName))
            {
                int lbs = Id.LastIndexOf('\\');

                if (lbs != -1)
                {
                    return Id.Substring(lbs + 1);
                }
                else
                {
                    return Id;
                }
            }
            else
            {
                return OverrideDisplayName;
            }
        }

        public string GetRowAsString(IVisualisable line)
        {
            return AsString(GetRow(line), DisplayMode);
        }

        public static string AsString(object result)
        {
            return AsString(result, EListDisplayMode.Smart);
        }

        public static string AsString(object result, EListDisplayMode listDisplayMode)
        {
            if (result == null)
            {
                return "";
            }

            if (result is string)
            {
                return (string)result;
            }

            if (result is IEnumerable)
            {
                ICollection col = result as ICollection;
                IEnumerable enu = (IEnumerable)result;
                int count;

                if (col != null)
                {
                    count = col.Count;
                }
                else
                {
                    count = enu.CountAll();
                }

                switch (listDisplayMode)
                {
                    case EListDisplayMode.Smart:
                        if (count == 1)
                        {
                            return AsString(enu.FirstOrDefault2());
                        }
                        else if (count == 0)
                        {
                            return "";
                        }
                        else
                        {
                            return "(" + count.ToString() + ")";
                        }

                    case EListDisplayMode.Count:
                        return count.ToString();

                    case EListDisplayMode.CountAndContent:
                        return "(" + count.ToString() + ") " + StringHelper.ArrayToString(enu, AsString);

                    case EListDisplayMode.Content:
                        return StringHelper.ArrayToString(enu, AsString);
                }
            }

            if (result is IVisualisable)
            {
                IVisualisable v = (IVisualisable)result;

                return v.DisplayName;
            }

            if (result is double)
            {
                double d = (double)result;

                if (double.IsNaN(d))
                {
                    return "";
                }

                return Maths.SignificantDigits(d);
            }

            if (result is int)
            {
                if ((int)result == 0)
                {
                    return "";
                }
                else
                {
                    return result.ToString();
                }
            }

            return result.ToString();
        }

        public abstract object GetRow(IVisualisable line);

        public abstract bool IsAlwaysEmpty { get; }

        internal abstract Color GetColour(IVisualisable line);
        public abstract bool HasColourSupport { get; }
    }

    sealed class Column<T> : Column
        where T : class, IVisualisable
    {
        public delegate object ColumnProvider(T item);
        public delegate Color ColourProvider(T item);
        public readonly ColumnProvider Provider;
        public ColourProvider Colour;

        public Column(string name, EColumn special, string description, ColumnProvider provider, ColourProvider colour = null)
            : base(name, special, description)
        {
            Provider = provider;
        }

        public override object GetRow(IVisualisable line)
        {
            return Provider((T)line);
        }

        public override bool IsAlwaysEmpty
        {
            get { return Provider == null; }
        }

        internal override Color GetColour(IVisualisable line)
        {
            if (Colour != null)
            {
                return Colour((T)line);
            }
            else
            {
                return Color.Black;
            }
        }

        public override bool HasColourSupport
        {
            get
            {
                return Colour != null;
            }
        }
    }

    static class ColumnManager
    {
        internal static IEnumerable<Column> GetColumns(Core core, IVisualisable visualisable)
        {
            return visualisable.GetColumns(core);
        }

        internal static IEnumerable<Column> GetColumns<U>(Core core)
            where U : class, IVisualisable
        {
            return ((U)(FormatterServices.GetUninitializedObject(typeof(U)))).GetColumns(core);
        }

        internal static IEnumerable<Column> GetColumns(Core core, Type type)
        {
            return ((IVisualisable)(FormatterServices.GetUninitializedObject(type))).GetColumns(core);
        }
    }

    static class ColumnExtensions
    {
        public static void Add<T>(this List<Column<T>> self, string name, EColumn special, Column<T>.ColumnProvider provider, Column<T>.ColourProvider colour = null)
             where T : class, IVisualisable
        {
            self.Add(new Column<T>(name, special, null, provider, colour));
        }

        public static void AddSubObject<T, U>(this List<Column<T>> self, Core core, string prefix, Converter<T, U> convertor)
            where T : class, IVisualisable
            where U : class, IVisualisable
        {
            Add(self, prefix, EColumn.None, new Column<T>.ColumnProvider(convertor));

            foreach (Column column in ColumnManager.GetColumns<U>(core))
            {
                self.Add(new Column<T>(prefix + "\\" + column.Id, EColumn.None, column.Description, z => column.GetRow(convertor(z)), z => column.GetColour(convertor(z))));
            }
        }
    }
}
