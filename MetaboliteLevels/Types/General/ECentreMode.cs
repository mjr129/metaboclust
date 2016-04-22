using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaboliteLevels.Settings
{
    /// <summary>
    /// How the candidates define the centre (see CandidateMode)
    /// </summary>
    public enum ECentreMode
    {
        /// <summary>
        /// All candidates
        /// </summary>
        All,

        /// <summary>
        /// Average of all candidates
        /// </summary>
        Average,
    }
}
