using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Session.Associational;

namespace MetaboliteLevels.Algorithms.Statistics.Results
{
    [Serializable]
    class ResultCorrection : ResultBase
    {
        /// <summary>
        /// Resultant intensity matrix
        /// </summary>
        public readonly IntensityMatrix Matrix;

        public ResultCorrection( IntensityMatrix result )
        {
            Matrix = result;
        }
    }
}
