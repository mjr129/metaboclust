using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Gui.Controls.Lists;
using MetaboliteLevels.Properties;
using MGui.Helpers;

namespace MetaboliteLevels.Data.Session.Main
{
    /// <summary>
    /// Stuff that can have associations.
    /// Peaks...clusters...adducts...metabolites...pathways.
    /// </summary>
    [Serializable]
    internal abstract class Associational : Visualisable
    {
        public ContentsRequest FindAssociations( Core core, EVisualClass type )
        {
            return FindAssociations( this, new ContentsRequest( core, this, type ) );
        }

        public ContentsRequest FindAssociations( ContentsRequest request )
        {
            return FindAssociations( this, request );
        }

        public static ContentsRequest FindAssociations( object target, Core core, EVisualClass type )
        {
            return FindAssociations( target, new ContentsRequest( core, target, type ) );
        }

        public static ContentsRequest FindAssociations( object target, ContentsRequest request )
        {
            switch (request.Type)
            {
                case EVisualClass.SpecialAdvanced:
                case EVisualClass.SpecialMeta:
                case EVisualClass.SpecialAll:
                case EVisualClass.SpecialStatistic:
                    request.AddExtraColumn( "Value", "Value of the field" );

                    IEnumerable<Column> cols = ColumnManager.GetColumns( request.Core, target ).OrderBy( z => z.Special.Has( EColumn.Visible ) );
                    bool shortName;

                    switch (request.Type)
                    {
                        case EVisualClass.SpecialAll:
                            request.Text = "All data fields for {0}";
                            cols = cols.Where( z => !z.Special.Has( EColumn.Advanced ) );
                            shortName = false;
                            break;

                        case EVisualClass.SpecialStatistic:
                            request.Text = "Statistics fields for {0}";
                            cols = cols.Where( z => !z.Special.Has( EColumn.Advanced ) && z.Special.Has( EColumn.IsStatistic ) );
                            shortName = true;
                            break;

                        case EVisualClass.SpecialMeta:
                            request.Text = "Meta-data fields for {0}";
                            cols = cols.Where( z => !z.Special.Has( EColumn.Advanced ) && z.Special.Has( EColumn.IsMeta ) );
                            shortName = true;
                            break;

                        default:
                        case EVisualClass.SpecialAdvanced:
                            request.Text = "Internal data for {0}";
                            shortName = false;
                            break;
                    }

                    foreach (var c in cols)
                    {
                        request.Add( new ColumnValuePair( c, target, ColumnManager.GetColumnColour( c ), Resources.ListIconInformation, shortName ) );
                    }
                    break;

                default:
                    if (target is Associational)
                    {
                        ((Associational)target).OnFindAssociations( request );
                    }
                    break;
            }

            return request;
        }         

        public class ColumnValuePair : Gui.Controls.Lists.IColumnProvider, IIconProvider
        {    
            [XColumn("Field\\", EColumn.Decompose)]
            private Column Column;

            private object Target;
            private readonly Color ColumnColour;
            private readonly bool _shortName;

            public Image Icon { get; }

            public string Field => this._shortName ? this.Column.DisplayName : this.Column.Id;
            public Color Colour => this.Column.GetColour( this.Target );
            public object Value => this.Column.GetRow( this.Target );

            public ColumnValuePair( Column column, object target, Color colour, Image icon, bool shortName )
            {
                this.Column = column;
                this.Target = target;      
                this.Icon = icon;
                this.ColumnColour = colour;
                this._shortName = shortName;
            }

            public void GetXColumns( ColumnCollection uresults, Core core )
            {
                ColumnCollection<ColumnValuePair> result = uresults.Cast<ColumnValuePair>();

                result.Add( "Field", EColumn.Visible, z => z.Field, z => z.ColumnColour );
                result.Add( "Value", EColumn.Visible, z => z.Value, z => z.Colour );
            }

            public override string ToString()
            {
                return this.Field + " = " + Column.AsString( this.Value );
            }
        }    

        /// <summary>
        /// Gets related items.
        /// </summary>
        protected abstract void OnFindAssociations( ContentsRequest request );

        /// <summary>
        /// VisualClass of IVisualisable
        /// </summary>
        public abstract EVisualClass AssociationalClass { get; }
    }
}
