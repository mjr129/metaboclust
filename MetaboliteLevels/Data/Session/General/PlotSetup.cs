using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGui.Datatypes;
using MGui.Helpers;

namespace MetaboliteLevels.Data.Session.General
{
    [Serializable]
    internal class PlotSetup
    {
        const string BASIC_HELP = "\r\n\r\nEnclose the IDs of fields in {braces} to show those fields. For a list of possible field IDs click the button to the right of the input.";
        const string AXIS_HELP = "\r\n\r\nSpecify a number to use that value. Use asterisk (*) to autoscale the axis to its current contents. Use a double asterisk (**) to autoscale the axis to the matrix from which the contents originate (this is useful, for instance, to identify \"flat\" profiles from noise).";

        [DisplayName( "Information bar" )]
        [Description( "The text to display in the toolbar above the plot." + BASIC_HELP )]
        [DefaultValue( "" )]
        public ParseElementCollection Information { get; set; }

        [DisplayName( "Plot title" )]
        [Description( "Plot title. You can leave this empty to hide the title and conserve screen space."+BASIC_HELP )]
        [DefaultValue( "" )]
        public ParseElementCollection Title { get; set; }

        [DisplayName( "Plot sub-title" )]
        [Description( "Plot sub-title. You can leave this empty to hide the sub-title and conserve screen space."+BASIC_HELP )]
        [DefaultValue( "" )]
        public ParseElementCollection SubTitle { get; set; }

        [DisplayName( "X Axis Label." )]
        [Description( "Label for the X axis, e.g. \"day\" or \"hour\". You can leave this empty to hide the axis title and conserve screen space." +BASIC_HELP)]
        [DefaultValue( "" )]
        public ParseElementCollection AxisX { get; set; }

        [DisplayName( "Y Axis Label" )]
        [Description( "Label for the Y axis, e.g. \"intensity\". You can leave this empty to hide the axis title and conserve screen space."+BASIC_HELP )]
        [DefaultValue( "" )]
        public ParseElementCollection AxisY { get; set; }

        [Name( "X Axis Minumum" )]
        [Description( "Minimum value for the Y axis."+AXIS_HELP )]
        public AxisRange RangeXMin;

        [Name( "X Axis Maximum" )]
        [Description( "Maximum value for the X axis." + AXIS_HELP )]
        public AxisRange RangeXMax;

        [Name( "Y Axis Minimum" )]
        [Description( "Minimum value for the Y axis." + AXIS_HELP )]
        public AxisRange RangeYMin;

        [Name( "Y Axis Maximum" )]
        [Description( "Maximum value for the Y axis."+AXIS_HELP )]
        public AxisRange RangeYMax;  
    }
}
