using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Session.Associational;

namespace MetaboliteLevels.Algorithms.Statistics.Results
{
    [Serializable]
    class ResultTrend : ResultBase
    {
        public IntensityMatrix Matrix;

        public ResultTrend( IntensityMatrix result )
        {
            this.Matrix = result;
        }
    }
}
