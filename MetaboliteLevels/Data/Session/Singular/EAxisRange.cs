using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGui.Helpers;

namespace MetaboliteLevels.Data.Session.Singular
{
    /// <summary>
    /// Modes for <see cref="AxisRange"/>.
    /// </summary>
    public enum EAxisRange
    {
        /// <summary>
        /// Autoscale the axis
        /// </summary>
        [Name("(Automatic - this vector)")]
        Automatic,

        /// <summary>
        /// Autoscale the axis to all possibilities
        /// </summary>
        [Name( "(Automatic - all vectors)" )]
        General,

        /// <summary>
        /// Use a fixed scale
        /// </summary>
        Fixed,
    }
}
