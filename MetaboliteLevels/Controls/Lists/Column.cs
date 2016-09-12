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
using System.Reflection;
using MGui.Helpers;
using MetaboliteLevels.Types.UI;

namespace MetaboliteLevels.Viewers.Lists
{
    public enum EColumn
    {
        None,
        Visible,
        Meta,
        Statistic,
        Advanced,
    }

    enum EListDisplayMode
    {
        Smart = 0,
        Count = 1,
        Content = 2,
        [Name("Count and content")]
        CountAndContent = 3,
    }

    abstract class ColumnAttribute
    {
        public string Name { get; set; }
        public EColumn Visibility { get; set; }

        public ColumnAttribute()
        {
        }

        public ColumnAttribute( string name )
        {
            Name = name;
        }

        public ColumnAttribute( EColumn visibility )
        {
            Visibility = visibility;
        }

        public ColumnAttribute( string name, EColumn visibility )
        {
            Name = name;
            Visibility = visibility;
        }
    }

    abstract class Column : IVisualisable
    {
        public readonly string Id;
        public readonly string Description;
        public readonly EColumn Special;

        public ColumnHeader Header;
        public string OverrideDisplayName { get; set; }
        bool INameable.Hidden { get { return false; } set { /* NA */ } }
        public bool Visible;
        public int Width = 128;
        public bool DisableMenu;
        public int DisplayIndex;
        public EListDisplayMode DisplayMode = EListDisplayMode.Smart;   
        string INameable.DefaultDisplayName => Id;
                       
        /// <summary>
        /// Hide implementation because comment is read-only.
        /// </summary>
        string INameable.Comment
        {
            get { return Description; }
            set { /* READONLY */ }
        }                        

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

        public string DisplayName => IVisualisableExtensions.FormatDisplayName( this );

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

        public static double AsDouble( object result )
        {
            if (result == null)
            {
                return double.NaN;
            }

            if (result is string)
            {
                double r;

                if (!double.TryParse( (string)result, out r ))
                {
                    r = double.NaN;
                }

                return r;
            }

            if (result is IEnumerable)
            {
                int count = ((IEnumerable)result).Cast<object>().Count();

                if (count == 1)
                {
                    return AsDouble( ((IEnumerable)result).FirstOrDefault2() );
                }

                return count;
            }   

            if (result is double)
            {
                return (double)result;
            }

            if (result is int)
            {
                return (int)result;
            }

            if (result is IConvertible)
            {
                try
                {
                    return ((IConvertible)result).ToDouble(null);
                }
                catch
                {
                    return double.NaN;
                }
            }

            return double.NaN;
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

            if (result is INameable)
            {
                INameable v = (INameable)result;

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

        public abstract Type GetTemplateType();

        public abstract bool IsAlwaysEmpty { get; }

        public abstract Color GetColour(IVisualisable line);          

        UiControls.ImageListOrder IVisualisable.GetIcon()
        {
            throw new NotImplementedException();
        }   

        IEnumerable<Column> IVisualisable.GetColumns( Core core )
        {
            List<Column<Column>> result = new List<Column<Column>>();

            result.Add( "ID", EColumn.Visible, z => z.Id );
            result.Add( "Preferred display order", z => z.DisplayIndex );
            result.Add( "Preferred name", z => z.OverrideDisplayName );
            result.Add( "Preferred width", z => z.Width );
            result.Add( "Description", z => z.Description );
            result.Add( "Visible", EColumn.Visible, z => z.Visible );

            result.Add( "Disable menu", EColumn.Advanced, z => z.DisableMenu );     
            result.Add( "Display mode", EColumn.Advanced, z => z.DisplayMode );
            result.Add( "Associated header", EColumn.Advanced, z => z.Header?.Name );
            result.Add( "Has colour support", EColumn.Advanced, z => z.HasColourSupport );
            result.Add( "Is always empty", EColumn.Advanced, z => z.IsAlwaysEmpty );
            result.Add( "Special", EColumn.Advanced, z => z.Special );
            result.Add( "Displayed name", EColumn.Advanced, z => z.DisplayName );
            result.Add( "Hidden", EColumn.Advanced, z => ((INameable)z).Hidden );

            return result;
        }

        public abstract bool HasColourSupport { get; }
    }

    sealed class Column<T> : Column
        where T : class, IVisualisable
    {
        public delegate object ColumnProvider(T item);
        public delegate Color ColourProvider(T item);                

        public readonly ColumnProvider Provider;
        public ColourProvider Colour;        

        public Column(string name, EColumn special, string description, ColumnProvider provider, ColourProvider colour)
            : base(name, special, description)
        {
            Provider = provider;
            Colour = colour;             
        }

        public override Type GetTemplateType()
        {
            return typeof( T );
        }

        public override object GetRow(IVisualisable line)
        {
            T x = line as T;

            if (x == null || Provider == null)
            {
                return null;
            }    

            try
            {   
                return Provider( x );
            }
            catch (Exception)
            {
                return "?MISSING?";
            }
        }

        public override bool IsAlwaysEmpty
        {
            get { return Provider == null; }
        }

        public override Color GetColour(IVisualisable line)
        {
            T x = line as T;

            if (x != null && Colour != null)
            {
                return Colour(x);
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

    internal static class ColumnManager
    {
        public static IEnumerable<Column> GetColumns(Core core, IVisualisable visualisable)
        {
            return AddProperties( visualisable.GetColumns( core ), visualisable.GetType() );
        }

        public static IEnumerable<Column> GetColumns<T>(Core core)
            where T : class, IVisualisable
        {
            return GetColumns( core, (IVisualisable)(T)(FormatterServices.GetUninitializedObject( typeof( T ) )));
        }

        private static IEnumerable<Column> AddProperties( IEnumerable<Column> def, Type t )
        {
            var bf = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            var fields = t.GetProperties( bf );

            List<Column<IVisualisable>> columns = new List<Column<IVisualisable>>();

            foreach (PropertyInfo prop in t.GetProperties( bf ))
            {
                columns.Add( "IData\\" + prop.Name, EColumn.Advanced, prop.GetValue);
            }

            foreach (FieldInfo field in t.GetFields( bf ))
            {
                columns.Add( "IData\\" + field.Name, EColumn.Advanced, field.GetValue );
            }

            return def.Concat( columns );
        }
    }

    static class ColumnExtensions
    {
        public static void Add<T>(this List<Column<T>> self, string name, EColumn special, Column<T>.ColumnProvider provider, Column<T>.ColourProvider colour = null)
             where T : class, IVisualisable
        {
            self.Add(new Column<T>(name, special, null, provider, colour));
        }

        public static void Add<T>(this List<Column<T>> self, string name, Column<T>.ColumnProvider provider, Column<T>.ColourProvider colour = null )
          where T : class, IVisualisable
        {
            self.Add(new Column<T>(name, EColumn.None, null, provider, colour));
        }

        public static void AddSubObject<T, U>(this List<Column<T>> self, Core core, string prefix, Converter<T, U> convertor)
            where T : class, IVisualisable
            where U : class, IVisualisable
        {
            Add(self, prefix, EColumn.None, new Column<T>.ColumnProvider(convertor));

            foreach (Column column in ColumnManager.GetColumns<U>(core))
            {
                self.Add(new Column<T>(
                            prefix + "\\" + column.Id,
                            EColumn.None,
                            column.Description,
                            z =>
                            {
                                var x = convertor(z);
                                return x != null ? column.GetRow(x) : null;
                            },
                            z =>
                            {
                                var x = convertor(z);
                                return x != null ? column.GetColour(x) : Color.Black;
                            }  
                    ) );
            }
        }
    }
}
