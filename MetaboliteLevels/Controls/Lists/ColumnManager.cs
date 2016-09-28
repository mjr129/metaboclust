﻿using System;
using System.Collections;
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
    class ColumnCollection : IEnumerable<Column>
    {
        private List<Column> _list;

        public ColumnCollection()
        {
            _list = new List<Column>();
        }

        protected ColumnCollection( ColumnCollection list )
        {
            _list = list._list;
        }

        public void AddRange( IEnumerable<Column> range )
        {                   
            foreach (Column x in range)
            {
                Add( x );
            }
        }

        public Column Find( string id )
        {
            string propertyIdU = id.ToUpper();
            return _list.FirstOrDefault( z => z.Id.ToUpper() == propertyIdU );
        }

        public IEnumerator<Column> GetEnumerator()
        {
            return ((IEnumerable<Column>)this._list).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Column>)this._list).GetEnumerator();
        }

        internal void Add( Column column )
        {
            Debug.Assert( !_list.Any( z => z.Id == column.Id ), "Duplicate column ID." );
            _list.Add( column );
        }

        public ColumnCollection<T> Cast<T>()
        {
            return new ColumnCollection<T>( this );
        }
    }

    class ColumnCollection<T> : ColumnCollection
    {
        public ColumnCollection( ColumnCollection columnList )
            : base( columnList )
        {
        }

        public void Add( string name, EColumn special, Column<T>.ColumnProvider provider, Column<T>.ColourProvider colour = null )
        {
            base.Add( new Column<T>( name, special, null, provider, colour ) );
        }

        public  void Add(  string name, Column<T>.ColumnProvider provider, Column<T>.ColourProvider colour = null )
        {
            base.Add( new Column<T>( name, EColumn.None, null, provider, colour ) );
        }               
    }

    internal static class ColumnManager
    {
        public static ColumnCollection GetColumns( Core core, object @object )
        {                                                       
            return ColumnManager.GetColumns( @object.GetType(), @object, core );
        }

        public static ColumnCollection GetColumns<T>( Core core )
        {
            return ColumnManager.GetColumns( typeof( T ), null, core );
        }

        public static ColumnCollection GetColumns( Type type, Core core )
        {
            return ColumnManager.GetColumns( type, null, core );
        }

        public static object QueryProperty( Core core, object target, string propertyId )
        {   
            var col = GetColumns(core, target).Find(propertyId);

            if (col == null)
            {
                return "{MISSING: \"" + propertyId + "\"}";
            }

            return col.GetRowAsString( target );
        }

        private static ColumnCollection GetColumns( Type type, object @object, Core core )
        {
            ColumnCollection results = new ColumnCollection();
            GetCustomColumns( results, type, @object, core );
            GetReflectedColumns( results, type, core );

            bool anyVisible = results.Any( z => z.Special.Has( EColumn.Visible ) );

            results.Add( new Column<object>( "Data", anyVisible ? EColumn.Advanced : EColumn.Visible, null, z => z.ToStringSafe(), null ) );
            results.Add( new Column<object>( "Type", EColumn.Advanced, null, z => z.GetType().ToUiString(), null ) );

            return results;
        }

        private static void GetCustomColumns( ColumnCollection results, Type type, object @object , Core core)
        {
            if (!typeof( IColumnProvider ).IsAssignableFrom( type ))
            {
                // Not supported
                return;
            }

            if (@object == null)
            {
                // Unfortunately this won't work if the type is abstract
                if (type.IsAbstract)
                {
                    return;
                }

                @object = FormatterServices.GetUninitializedObject( type );
            }

            ((IColumnProvider)@object).GetXColumns( results, core );
        }

        private static void GetReflectedColumns( ColumnCollection results, Type type, Core core )
        {                                                                          
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