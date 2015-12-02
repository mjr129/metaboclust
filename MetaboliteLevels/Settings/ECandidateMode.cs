using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaboliteLevels.Settings
{
    /// <summary>
    /// The candidates for the cluster centre - see CentreMode for which one(s) get chosen.
    /// </summary>
    public enum ECandidateMode
    {
        /// <summary>
        /// Use exemplars as candidates for cluster centre
        /// </summary>
        Exemplars,

        /// <summary>
        /// Use assignments as candidates for cluster centre
        /// </summary>
        Assignments,
    }
}
