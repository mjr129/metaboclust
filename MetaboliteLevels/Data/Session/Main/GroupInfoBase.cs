using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Gui.Controls.Lists;
using MetaboliteLevels.Gui.Datatypes;
using MetaboliteLevels.Utilities;
using MGui.Datatypes;
using MGui.Helpers;

namespace MetaboliteLevels.Data.Session.Main
{
    /// <summary>
    /// Experimental group / batch information.
    /// </summary>
    [Serializable]
    internal abstract class GroupInfoBase : Visualisable
    {       
        private string _id;
        public readonly int Order;          // This program's internal index (Core.Groups[this.Order] / Core.Batches[this.Order]). This is arbitrary but MUST NOT BE CHANGED.
        [XColumn]
        public Range Range;        // Range covered (days / acquisition-order)
        public Color ColourLight;           // Display colour (light)
        public Color Colour;                // Display colour       

        [XColumn]
        public string DisplayShortName => string.IsNullOrEmpty(this.OverrideShortName) ? this.DefaultShortName : this.OverrideShortName;

        public string DefaultShortName => this.Id;

        public abstract override string DefaultDisplayName { get; }

        public string OverrideShortName { get; set; }

        public override EPrevent SupportsHide => EPrevent.Hide;

        [XColumn]
        public EHatchStyle HatchStyle { get; set; } = EHatchStyle.Solid;

        public EGraphIcon GraphIcon { get; set; }

        [XColumn]
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

            uint colour = unchecked(((uint)this.Order)) % 7;

            switch (colour)
            {
                case 0:
                    this.Colour = Color.FromArgb( 0, 0, 255 );
                    this.ColourLight = Color.FromArgb( 128, 128, 255 ); // blue
                    this.GraphIcon = EGraphIcon.Circle;
                    break;

                case 1:
                    this.Colour =  Color.FromArgb( 255, 0, 0 ) ;
                    this.ColourLight = Color.FromArgb( 255, 128, 128 ); // red
                    this.GraphIcon = EGraphIcon.Square;
                    break;

                case 2:
                    this.Colour =  Color.FromArgb( 0, 128, 0 ) ;
                    this.ColourLight = Color.FromArgb( 64, 128, 64 ); // green
                    this.GraphIcon = EGraphIcon.Triangle;
                    break;

                case 3:
                    this.Colour =  Color.FromArgb( 128, 128, 0 ) ;
                    this.ColourLight = Color.FromArgb( 128, 128, 6 ); // yellow
                    this.GraphIcon = EGraphIcon.Diamond;
                    break;

                case 4:
                    this.Colour =  Color.FromArgb( 255, 0, 255 ) ;
                    this.ColourLight = Color.FromArgb( 255, 128, 255 ); // magenta
                    this.GraphIcon = EGraphIcon.InvertedTriangle;
                    break;

                case 5:
                    this.Colour =  Color.FromArgb( 0, 128, 128 );
                    this.ColourLight = Color.FromArgb( 64, 128, 128 ); // cyan
                    this.GraphIcon = EGraphIcon.Cross;
                    break;

                case 6:
                    this.Colour =  Color.FromArgb( 128, 128, 128 ) ;
                    this.ColourLight = Color.FromArgb( 192, 192, 192 ); // gray
                    this.GraphIcon = EGraphIcon.Plus;
                    break;

                default:
                    throw new SwitchException( this._id.ToString() );
            }
        }               

        internal void SetColour(Color color)
        {
            this.Colour = color;
            this.ColourLight = GetLightVersionOfColour(color);
        }

        public static Color GetLightVersionOfColour(Color color)
        {
            return ColourHelper.Blend(Color.White, color, 0.5);
        }

        public static int GroupOrderBy(GroupInfoBase a)
        {
            return a.DisplayPriority;
        }

        public override void GetXColumns(ColumnCollection list, Core core)
        {
            var columns = list .Cast< GroupInfoBase>();
                                                                               
            columns.Add("Colour", z => ColourHelper.ColourToName(z.Colour), z => z.Colour);
            columns.Add("Light colour", z => ColourHelper.ColourToName(z.ColourLight), z => z.ColourLight);
            columns.Add( "Graph icon", z => z.GraphIcon, z=> z.Colour );  
        }

        public override Image Icon
        {
            get { return UiControls.CreateExperimentalGroupImage( true, this, false ); }
        }

        [XColumn]
        public string Id
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
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
