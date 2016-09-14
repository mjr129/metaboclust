using System;
using System.Collections.Generic;
using System.Drawing;
using MetaboliteLevels.Data.General;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Viewers.Lists;
using MSerialisers;
using System.Runtime.Serialization;
using System.Diagnostics;
using MGui;
using MetaboliteLevels.Types.UI;
using System.Drawing.Drawing2D;
using MCharting;
using MGui.Datatypes;

namespace MetaboliteLevels.Data.DataInfo
{
    /// <summary>
    /// Experimental group / batch information.
    /// </summary>
    [Serializable]
    internal abstract class GroupInfoBase : Visualisable
    {       
        private string _id;
        public readonly int Order;          // This program's internal index (Core.Groups[this.Order] / Core.Batches[this.Order]). This is arbitrary but MUST NOT BE CHANGED.
        public readonly Range Range;        // Range covered (days / acquisition-order)
        public Color ColourLight;           // Display colour (light)
        public Color Colour;                // Display colour       

        public string DisplayShortName => string.IsNullOrEmpty(OverrideShortName) ? DefaultShortName : OverrideShortName;

        public string DefaultShortName => StringId;

        public abstract override string DefaultDisplayName { get; }

        public string OverrideShortName { get; set; }

        public override EPrevent SupportsHide => EPrevent.Hide;          

        public EHatchStyle HatchStyle { get; set; } = EHatchStyle.Solid;

        public EGraphIcon GraphIcon { get; set; }

        public int DisplayPriority;

        protected GroupInfoBase( string groupId, int order, Range xRange, string name, string shortName, int displayPriority )
        {
            Debug.Assert( !string.IsNullOrEmpty( groupId ) );
                              
            this._id = groupId;
            this.Order = order;
            this.Range = xRange;
            this.OverrideDisplayName = name;
            this.OverrideShortName = shortName;
            this.DisplayPriority = displayPriority;

            switch (Order % 7)
            {
                case 0:
                    Colour = Color.FromArgb( 0, 0, 255 );
                    ColourLight = Color.FromArgb( 128, 128, 255 ); // blue
                    GraphIcon = EGraphIcon.Circle;
                    break;

                case 1:
                    Colour =  Color.FromArgb( 255, 0, 0 ) ;
                    ColourLight = Color.FromArgb( 255, 128, 128 ); // red
                    GraphIcon = EGraphIcon.Square;
                    break;

                case 2:
                    Colour =  Color.FromArgb( 0, 128, 0 ) ;
                    ColourLight = Color.FromArgb( 64, 128, 64 ); // green
                    GraphIcon = EGraphIcon.Triangle;
                    break;

                case 3:
                    Colour =  Color.FromArgb( 128, 128, 0 ) ;
                    ColourLight = Color.FromArgb( 128, 128, 6 ); // yellow
                    GraphIcon = EGraphIcon.Diamond;
                    break;

                case 4:
                    Colour =  Color.FromArgb( 255, 0, 255 ) ;
                    ColourLight = Color.FromArgb( 255, 128, 255 ); // magenta
                    GraphIcon = EGraphIcon.InvertedTriangle;
                    break;

                case 5:
                    Colour =  Color.FromArgb( 0, 128, 128 );
                    ColourLight = Color.FromArgb( 64, 128, 128 ); // cyan
                    GraphIcon = EGraphIcon.Cross;
                    break;

                case 6:
                    Colour =  Color.FromArgb( 128, 128, 128 ) ;
                    ColourLight = Color.FromArgb( 192, 192, 192 ); // gray
                    GraphIcon = EGraphIcon.Plus;
                    break;

                default:
                    throw new SwitchException( _id.ToString() );
            }
        }               

        internal void SetColour(Color color)
        {
            Colour = color;
            ColourLight = GetLightVersionOfColour(color);
        }

        public static Color GetLightVersionOfColour(Color color)
        {
            return ColourHelper.Blend(Color.White, color, 0.5);
        }

        public static int GroupOrderBy(GroupInfoBase a)
        {
            return a.DisplayPriority;
        }

        public override IEnumerable<Column> GetColumns(Core core)
        {
            List<Column<GroupInfoBase>> columns = new List<Column<GroupInfoBase>>();

            columns.Add("ID", z => z.StringId);
            columns.Add("Range", EColumn.Visible, z => z.Range.ToString());
            columns.Add("Name", EColumn.Visible, z => z.DisplayName);
            columns.Add("Short name", z => z.DisplayShortName);
            columns.Add("Default name", z => z.DefaultDisplayName);
            columns.Add("Default short name", z => z.DefaultShortName);
            columns.Add("User provided name", z => z.OverrideDisplayName);
            columns.Add("User provided short name", z => z.OverrideShortName);
            columns.Add("Colour", z => ColourHelper.ColourToName(z.Colour), z => z.Colour);
            columns.Add("Light colour", z => ColourHelper.ColourToName(z.ColourLight), z => z.ColourLight);
            columns.Add("Comment", z => z.Comment);
            columns.Add("Display priority", z => z.DisplayPriority);
            columns.Add( "Graph icon", z => z.GraphIcon, z=> z.Colour );
            columns.Add( "Graph brush", z => z.HatchStyle );

            return columns;
        }

        public override  UiControls.ImageListOrder Icon => UiControls.ImageListOrder.Group;   

        public string StringId
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }    

        internal Brush CreateBrush( Color colour )
        {
            switch (this.HatchStyle)
            {
                case EHatchStyle.Solid:
                    return new SolidBrush( colour );

                case EHatchStyle.None:
                    return null;

                default:
                    return new HatchBrush( (HatchStyle)this.HatchStyle, colour, Color.Transparent );
            }
        }
    }    
}
