using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.DataInfo;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Settings
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

        public override string ToString()
        {
            return "Colour settings";
        }

        [DefaultValue(typeof(Color), "0xFF000000")]
        public Color ClusterCentre { get; set; }

        [DefaultValue(typeof(Color), "0xFF0000FF")]
        public Color SelectedSeries { get; set; } // TODO: This should be used but isn't

        [DefaultValue(typeof(Color), "0xFFC0C0C0")]
        public Color MinorGrid { get; set; }

        [DefaultValue(typeof(Color), "0xFF000000")]
        public Color MajorGrid { get; set; }

         [DefaultValue(typeof(Color), "0xFF000000")]
        public Color AxisTitle { get; set; } // TODO: This should be used but isn't
    }
}
