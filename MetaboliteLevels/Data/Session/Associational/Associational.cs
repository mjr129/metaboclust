using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Controls.Lists;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Session.Singular;
using MetaboliteLevels.Utilities;
using MGui.Helpers;
using IColumnProvider = MetaboliteLevels.Controls.Lists.IColumnProvider;

namespace MetaboliteLevels.Data.Session.Associational
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
            ContentsRequest request = new ContentsRequest( core, this, type );
            FindAssociations( request );
            return request;
        }

        private class FieldValuePair : IColumnProvider, IIconProvider
        {
            private string Field;
            private string Value;
            private Color Colour;
            public UiControls.ImageListOrder Icon { get; }

            public FieldValuePair( string field, string value, Color colour, UiControls.ImageListOrder icon )
            {
                this.Field = field;
                this.Value = value;
                this.Colour = colour;
                this.Icon = icon;
            }

            public IEnumerable<Column> GetXColumns( Core core )
            {
                List<Column<FieldValuePair>> result = new List<Column<FieldValuePair>>();

                result.Add( "Field", EColumn.Visible, z => z.Field, z => z.Colour );
                result.Add( "Value", EColumn.Visible, z => z.Value, z => z.Colour );

                return result;
            }
        }

        public void FindAssociations( ContentsRequest request )
        {
            switch (request.Type)
            {
                case EVisualClass.Info:
                    request.AddExtraColumn( "Value", "Value of the field" );

                    var cols = ColumnManager.GetColumns( request.Core, this ).OrderBy( z => z.Special.Has( EColumn.Visible ) );

                    foreach (var c in cols)
                    {
                        request.Add( new FieldValuePair( c.DisplayName, c.GetRowAsString( this ), ColumnManager.GetColumnColour( c ), UiControls.ImageListOrder.Info ) );
                    }
                    break;

                default:
                    OnFindAssociations( request );
                    break;
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
