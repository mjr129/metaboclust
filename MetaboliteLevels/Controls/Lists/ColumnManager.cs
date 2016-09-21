using System;
using System.Collections.Generic;
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
            var col = GetColumns(core, target).FirstOrDefault( z => z.DefaultDisplayName == propertyId );

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

            results.Add( new Column<object>( "Value", anyVisible ? EColumn.Advanced : EColumn.Visible, null, z => z.ToStringSafe(), null ) );
            results.Add( new Column<object>( "DataType", EColumn.Advanced, null, z => z.GetType().ToUiString(), null ) );

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
                            results.AddRange( ColumnExtensions.AddSubObject<object>( attr.Special, core, attr.Name, provider, provType ) );
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
    }
}
