using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.Singular;
using MGui.Helpers;

namespace MetaboliteLevels.Controls.Lists
{
    internal static class ColumnManager
    {
        public static IEnumerable<Column> GetColumns( Core core, object @object )
        {                                                       
            return ColumnManager.GetColumns( @object.GetType(), @object, core );
        }

        public static IEnumerable<Column> GetColumns<T>( Core core )
        {
            return ColumnManager.GetColumns( typeof( T ), null, core );
        }

        public static IEnumerable<Column> GetColumns( Type type, Core core )
        {
            return ColumnManager.GetColumns( type, null, core );
        }

        public static object QueryProperty( Core core, object target, string propertyId )
        {
            string propertyIdU = propertyId.ToUpper();
            var col = GetColumns(core, target).FirstOrDefault( z => z.Id.ToUpper() == propertyIdU );

            if (col == null)
            {
                return "{MISSING: \"" + propertyId + "\"}";
            }

            return col.GetRowAsString( target );
        }

        private static IEnumerable<Column> GetColumns( Type type, object @object, Core core )
        {
            List<Column> results = new List<Column>();
            results.AddRange( GetCustomColumns( type, @object, core ) ?? new Column[0] );
            results.AddRange( GetReflectedColumns(type, core) );

            bool anyVisible = results.Any( z => z.Special.Has( EColumn.Visible ) );

            results.Add( new Column<object>( "Data", anyVisible ? EColumn.Advanced : EColumn.Visible, null, z => z.ToStringSafe(), null ) );
            results.Add( new Column<object>( "Type", EColumn.Advanced, null, z => z.GetType().ToUiString(), null ) );

            Debug.Assert( results.Any( z => results.Count( y => y.Id == z.Id ) != 1 ), "Duplicate column IDs." );

            return results;
        }

        private static IEnumerable<Column>  GetCustomColumns( Type type, object @object , Core core)
        {
            if (!typeof( IColumnProvider ).IsAssignableFrom( type ))
            {
                // Not supported
                return null;
            }

            if (@object == null)
            {
                // Unfortunately this won't work if the type is abstract
                if (type.IsAbstract)
                {
                    return null;
                }

                @object = FormatterServices.GetUninitializedObject( type );
            }

            return ((IColumnProvider)@object).GetXColumns(core);
        }

        private static IEnumerable<Column> GetReflectedColumns( Type type, Core core )
        {
            List<Column> results = new List<Column>(); 

            foreach (MemberInfo x in type.GetMembers( BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance ))
            {
                XColumnAttribute attr = x.GetCustomAttribute<XColumnAttribute>();

                string name;

                if (attr != null)
                {
                    name = attr.Name ?? StringHelper.UndoCamelCase( x.Name );
                }
                else
                {
                    name = null;
                }

                string pname = "<DATA>\\" + x.Name;
                string pdesc = "Internal field: " + x.DeclaringType.Name + "." + x.Name;

                Column<object>.ColumnProvider provider;
                Type provType;

                if (x is MethodInfo)
                {
                    if (attr != null)
                    {
                        MethodInfo method = (MethodInfo)x;
                        provider = z => method.Invoke( z, null );
                        provType = method.ReturnType;
                    }
                    else
                    {
                        provider = null;
                        provType = null;
                    }
                }
                else if (x is PropertyInfo)
                {
                    PropertyInfo property = (PropertyInfo)x;
                    provider = z => property.GetValue( z );
                    provType = property.PropertyType;

                }
                else if (x is FieldInfo)
                {
                    FieldInfo field = (FieldInfo)x;
                    provider = z => field.GetValue( z );
                    provType = field.FieldType;
                }
                else
                {
                    provider = null;
                    provType = null;
                }

                if (provider != null)
                {
                    if (attr != null)
                    {
                        if (attr.Special.Has( EColumn.Decompose ))
                        {
                            var xx = attr.Special;
                            xx = xx & ~EColumn.Decompose;

                            results.AddRange( ColumnManager.AddSubObject<object>( xx, xx, core, attr.Name, provider, provType ) );
                        }
                        else
                        {
                            results.Add( new Column<object>( name, attr.Special, attr.Description, provider, null ) );
                        }
                    }

                    results.Add( new Column<object>( pname, EColumn.Advanced | EColumn.IsProperty, pdesc, provider, null ) );
                }
            }

            return results;
        }

        public static Color GetColumnColour( Column col )
        {
            if (col.Special.Has( EColumn.Visible ))
            {
                return Color.Blue;
            }
            else if (col.Special.Has( EColumn.IsStatistic ))
            {
                return Color.DarkCyan;
            }
            else if (col.Special.Has( EColumn.IsMeta ))
            {
                return Color.Green;
            }
            else if (col.Special.Has( EColumn.IsProperty ))
            {
                return Color.DimGray;
            }
            else if (col.Special.Has( EColumn.Advanced ))
            {
                return Color.Olive;
            }

            return Color.Black;
        }

        public static void Add<T>( this List<Column<T>> self, string name, EColumn special, Column<T>.ColumnProvider provider, Column<T>.ColourProvider colour = null )
        {
            self.Add( new Column<T>( name, special, null, provider, colour ) );
        }

        public static void Add<T>( this List<Column<T>> self, string name, Column<T>.ColumnProvider provider, Column<T>.ColourProvider colour = null )
        {
            self.Add( new Column<T>( name, EColumn.None, null, provider, colour ) );
        }

        public static IEnumerable<Column<T>> AddSubObject<T>( EColumn valueVisibility, EColumn descendentVisibility, Core core, string prefix, Column<T>.ColumnProvider convertor, Type targetType )
        {   
            yield return new Column<T>( prefix + "(value)", valueVisibility, null, z => convertor( z ), null );

            foreach (Column column in ColumnManager.GetColumns( targetType, core ))
            {
                yield return new Column<T>(
                            prefix + column.Id,
                            InheritVisibility( descendentVisibility, column.Special ),
                            column.Comment,
                            z =>
                            {
                                var x = convertor( z );
                                return x != null ? column.GetRow( x ) : null;
                            },
                            z =>
                            {
                                var x = convertor( z );
                                return x != null ? column.GetColour( x ) : Color.Black;
                            }
                    );
            }
        }

        public static IEnumerable<Column<TList>> AddSubObject<TList, TTarget>( EColumn valueVisibility, EColumn descendentVisibility, Core core, string prefix, Converter<TList, object> convertor )
        {
            return AddSubObject<TList>( valueVisibility, descendentVisibility, core, prefix, new Column<TList>.ColumnProvider( convertor ), typeof( TTarget ) );
        }

        private static EColumn InheritVisibility( EColumn parent, EColumn child )
        {
            if (!parent.Has( EColumn.Visible ))
            {
                child &= ~EColumn.Visible;
            }

            if (parent.Has( EColumn.Advanced ))
            {
                child |= EColumn.Advanced;
            }

            return child;
        }
    }
}
