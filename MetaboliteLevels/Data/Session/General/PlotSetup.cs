using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGui.Datatypes;

namespace MetaboliteLevels.Data.Session.General
{
    [Serializable]
    internal class PlotSetup
    {
        [DisplayName( "Information bar" )]
        [Description( "The text to display in the toolbar above the plot. You can use {braces} to specify special values by ID - the \"info\" panel on the main screen shows the available IDs." )]
        [DefaultValue( "" )]
        public ParseElementCollection Information { get; set; }

        [DisplayName( "Plot title" )]
        [Description( "Plot title. You can leave this empty to hide the title and conserve screen space. You can use {braces} to specify special values by ID - the \"info\" panel on the main screen shows the available IDs." )]
        [DefaultValue( "" )]
        public ParseElementCollection Title { get; set; }

        [DisplayName( "Plot sub-title" )]
        [Description( "Plot sub-title. You can leave this empty to hide the sub-title and conserve screen space. You can use {braces} to specify special values by ID - the \"info\" panel on the main screen shows the available IDs." )]
        [DefaultValue( "" )]
        public ParseElementCollection SubTitle { get; set; }

        [DisplayName( "X Axis Label." )]
        [Description( "Label for the X axis, e.g. \"day\" or \"hour\". You can leave this empty to hide the axis title and conserve screen space. You can use {braces} to specify special values by ID - the \"info\" panel on the main screen shows the available IDs." )]
        [DefaultValue( "" )]
        public ParseElementCollection AxisX { get; set; }

        [DisplayName( "Y Axis Label" )]
        [Description( "Label for the Y axis, e.g. \"intensity\". You can leave this empty to hide the axis title and conserve screen space. You can use {braces} to specify special values by ID - the \"info\" panel on the main screen shows the available IDs." )]
        [Category( "All plots" )]
        [DefaultValue( "" )]
        public ParseElementCollection AxisY { get; set; }

        public AxisRange RangeXMin;
        public AxisRange RangeXMax;
        public AxisRange RangeYMin;
        public AxisRange RangeYMax;

        public override string ToString()
        {
            return (ParseElementCollection.IsNullOrEmpty( this.Information )
                && ParseElementCollection.IsNullOrEmpty( this.Title )
                && ParseElementCollection.IsNullOrEmpty( this.SubTitle )
                && ParseElementCollection.IsNullOrEmpty( this.AxisX )
                && ParseElementCollection.IsNullOrEmpty( this.AxisY ))
                ? "(None)"
                : "(Custom text)";
        }
    }
}
