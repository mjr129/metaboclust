using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Algorithms.Definitions.Base;
using MetaboliteLevels.Data.Session.Associational;

namespace MetaboliteLevels.Data.Algorithms.Definitions.Statistics
{
    [Serializable]
    class ResultStatistic : ResultBase
    {
        public readonly double Min;
        public readonly double Max;                

        public IReadOnlyDictionary<Peak, double> Results => _results;

        public readonly Dictionary<Peak, double> _results;

        public ResultStatistic( Dictionary<Peak, double> results, double min, double max)
        {
            Debug.Assert( results.Count != 0 );

            this._results = results;
            this.Min = min;
            this.Max = max;
        }

        public override string ToString()
        {
            return Results.Count.ToString();
        }
    }
}
