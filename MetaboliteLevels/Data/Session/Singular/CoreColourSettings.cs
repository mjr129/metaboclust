using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Data.Session.Singular
{
    [Serializable]
    public class CoreColourSettings
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public CoreColourSettings()
        {
            UiControls.ApplyDefaultsFromAttributes(this);
        }

        [OnDeserializing]
        private void OnDeserializing(StreamingContext context)
        {
            UiControls.InvokeConstructor(this);
        }

        public override string ToString()
        {
            return "Colour settings";
        }

        [Description("Cluster centres")]
        [DefaultValue(typeof(Color), "0xFF000000")]
        public Color ClusterCentre { get; set; }

        [Description("Selected plot items")]
        [DefaultValue(typeof(Color), "0xFF0000FF")]
        public Color SelectedSeries { get; set; } // TODO: This should be used but isn't

        [Description("Things of interest in the plot")]
        [DefaultValue(typeof(Color), "0xFFFF0000")]
        public Color NotableHighlight { get; set; }

        [Description("Minor grid lines")]
        [DefaultValue(typeof(Color), "0xFFC0C0C0")]
        public Color MinorGrid { get; set; }

        [Description("Major grid lines and the axes")]
        [DefaultValue(typeof(Color), "0xFF000000")]
        public Color MajorGrid { get; set; }

        [Description("Plot labels")]
        [DefaultValue(typeof(Color), "0xFF000000")]
        public Color AxisTitle { get; set; }

        [Description("Background of plots in thumnail view")]
        [DefaultValue(typeof(Color), "0xFFF5F5F5")]
        public Color PreviewBackground { get; set; }

        [Description("Background of plots")]
        [DefaultValue(typeof(Color), "0xFFFFFFFF")]
        public Color PlotBackground { get; set; }

        [Description("The parts of the input vectors which can't be coloured by experimental group")]
        [DefaultValue(typeof(Color), "0xFFC0C0C0")]
        public Color InputVectorJoiners { get; set; }
    }
}
