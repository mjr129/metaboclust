using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Visualisables;

namespace MetaboliteLevels.Algorithms.Statistics.Results
{
    [Serializable]
    class ResultStatistic : ResultBase
    {
        public readonly double Min;
        public readonly double Max;
        public readonly Dictionary<Peak, double> Results;

        public ResultStatistic( Dictionary<Peak, double> results, double min, double max)
        {
            this.Results = results;
            this.Min = min;
            this.Max = max;
        }
    }
}
