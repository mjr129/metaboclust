﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.Singular;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Controls.Lists
{
    [Flags]
    public enum EColumn
    {
        None,
        Visible = 1,
        Advanced = 2,
        Decompose = 4,
        IsMeta = 8,
        IsStatistic = 16,
        IsProperty = 32,
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

    [DebuggerDisplay( "{Id}" )]
    abstract class Column : Visualisable
    {
        [XColumn(EColumn.Visible)]
        public readonly string Id;        
                     
        public readonly EColumn Special;

        public ColumnHeader Header;                    
        public override EPrevent SupportsHide =>  EPrevent.Hide | EPrevent.Comment;

        [XColumn( EColumn.Visible )]
        public bool Visible;

        [XColumn( EColumn.Visible )]
        public int Width = 128;
        public bool DisableMenu;

        [XColumn( EColumn.None )]
        public int DisplayIndex;

        [XColumn( EColumn.None )]
        public EListDisplayMode DisplayMode = EListDisplayMode.Smart;                 

        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public Column(string name, EColumn special, string description)
        {
            this.Id = name;                 
            this.Special = special;
            this.Visible = special == EColumn.Visible;
            this.Comment = description;
        }                                                                              

        /// <summary>
        /// Gets ID (or display name if set).
        /// </summary>
        public override string DefaultDisplayName
        {
            get
            {
                int lbs = Id.LastIndexOf( '\\' );

                if (lbs != -1)
                {
                    return Id.Substring( lbs + 1 );
                }
                else
                {
                    return Id;
                }
            }
        }

        public string GetRowAsString(object line)
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

        public static string AsString( object result, EListDisplayMode listDisplayMode )
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
                            return AsString( enu.FirstOrDefault2() );
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
                        return "(" + count.ToString() + ") " + StringHelper.ArrayToString( enu, AsString );

                    case EListDisplayMode.Content:
                        return StringHelper.ArrayToString( enu, AsString );
                }
            }

            if (result is Visualisable)
            {
                Visualisable v = (Visualisable)result;

                return v.DisplayName;
            }

            if (result is double)
            {
                double d = (double)result;

                if (double.IsNaN( d ))
                {
                    return "";
                }

                return Maths.SignificantDigits( d );
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

            if (result is bool)
            {
                if ((bool)result)
                {
                    return "☑";
                }
                else
                {
                    return "☐";
                }
            }

            if (result is Type)
            {
                return ((Type)result).ToUiString();
            }

            if (result is WeakReference)
            {
                object t = ((WeakReference)result).Target;

                if (t == null)
                {
                    return "(Expired reference)";
                }

                return "&" + AsString( t, listDisplayMode );
            }

            if (ReflectionHelper.IsOfGenericType( result, typeof(WeakReference<>) ))
            {
                object t = WeakReferenceHelper.GetUntypedTarget( result );

                if (t == null)
                {
                    return "(Expired reference)";
                }

                return "&" + AsString( t, listDisplayMode );
            }

            return result.ToString();
        }

        public abstract object GetRow(object line);

        public abstract Type GetTemplateType();

        public abstract bool IsAlwaysEmpty { get; }

        public abstract Color GetColour( object line );

        public override Image Icon => Resources.IconPoint;        

        [XColumn( EColumn.None )]
        public abstract bool HasColourSupport { get; }

        public abstract string FunctionDescription { get; }
    }

    sealed class Column<T> : Column
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

        public override string FunctionDescription
        {
            get
            {
                if (Provider == null)
                {
                    return "null";
                }

                return Provider.Method.ReturnType.ToUiString();
            }
        }

        public override Type GetTemplateType()
        {
            return typeof( T );
        }

        public override object GetRow( object x )
        {                                      
            if (x == null || Provider == null || !(x is T))
            {
                return null;
            }    

            try
            {   
                return Provider( (T)x );
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

        public override Color GetColour( object x )
        {
            if (x == null || Colour == null || !(x is T))
            {
                return Color.Black;
            }

            try
            {
                return Colour( (T)x );
            }
            catch (Exception)
            {
                return Color.Magenta;
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
}
