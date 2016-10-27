using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGui.Helpers;

namespace MetaboliteLevels.Data.Session.General
{
    /// <summary>
    /// Modes for <see cref="AxisRange"/>.
    /// </summary>
    public enum EAxisRange
    {
        /// <summary>
        /// Autoscale the axis
        /// </summary>
        [Name("*")]
        [Description("Scale to min/max")]
        Automatic,

        /// <summary>
        /// Autoscale the axis to all possibilities
        /// </summary>
        [Name( "{I}" )]
        [Description( "Scale to min/max of the intensity matrix" )]
        General,

        /// <summary>
        /// Use a fixed scale
        /// </summary>
        [Description( "Custom scale" )]
        Fixed,
    }
}
