using MetaboliteLevels.Data.Visualisables;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetaboliteLevels.Viewers.Lists
{
    abstract class Column
    {
        public readonly string Id;
        public string DisplayName;
        public bool Visible;
        public ColumnHeader Header;
        public int Width = 128;
        public bool DisableMenu;
        public int DisplayIndex;
        public string Description;

        /// <summary>
        /// Ctor
        /// </summary>
        public Column(string name, bool visible)
        {
            this.Id = name;
            this.DisplayName = null;
            this.Visible = visible;
        }

        public static Column<T> New<T>(string name, bool visible, Column<T>.ColumnProvider provider)
            where T : class, IVisualisable
        {
            return new Column<T>(name, visible, provider);
        }

        /// <summary>
        /// Gets ID (or display name if set).
        /// </summary>
        public override string ToString()
        {
            if (string.IsNullOrEmpty(DisplayName))
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
                return DisplayName;
            }
        }

        public abstract object GetRow(IVisualisable line);

        public abstract bool IsAlwaysEmpty { get; }

        internal abstract Color GetColour(IVisualisable line);
    }

    sealed class Column<T> : Column
        where T : class, IVisualisable
    {
        public delegate object ColumnProvider(T item);
        public delegate Color ColourProvider(T item);
        public readonly ColumnProvider Provider;
        public ColourProvider Colour;

        public Column(string name, bool visible, ColumnProvider provider)
            : base(name, visible)
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
                return Colour((T) line);
            }
            else
            {
                return Color.Black;
            }
        }
    }

    static class ColumnExtensions
    {
        public static void Add<T>(this List<Column<T>> self, string name, bool visible, Column<T>.ColumnProvider provider)
             where T : class, IVisualisable
        {
            self.Add(new Column<T>(name, visible, provider));
        }
    }
}
