using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Algorithms.Definitions.Base;
using MetaboliteLevels.Data.Session.Associational;

namespace MetaboliteLevels.Data.Algorithms.Definitions.Trends
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
