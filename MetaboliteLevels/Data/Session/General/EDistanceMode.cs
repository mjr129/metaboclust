using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaboliteLevels.Data.Session.General
{
    /// <summary>
    /// How the cluster centres are used to define the distance.
    /// </summary>
    public enum EDistanceMode
    {
        /// <summary>
        /// Use closest centre
        /// </summary>
        ClosestCentre,

        /// <summary>
        /// Use average of all centres
        /// </summary>
        AverageToAllCentres,
    }
}
