using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaboliteLevels.Algorithms.Statistics.Results
{
    [Serializable]
    class ResultStatistic : ResultBase
    {
        public readonly double Min;
        public readonly double Max;

        public ResultStatistic(double min, double max)
        {
            this.Min = min;
            this.Max = max;
        }
    }
}
