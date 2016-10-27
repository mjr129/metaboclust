using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Algorithms.Definitions.Base;
using MetaboliteLevels.Data.Algorithms.Definitions.Base.Misc;
using MetaboliteLevels.Data.Algorithms.General;

namespace MetaboliteLevels.Data.Algorithms.Definitions.Trends.Implementations
{
    /// <summary>
    /// Trend which is constant across X.
    /// </summary>
    sealed class TrendFlatLine : TrendBase
    {
        private readonly AlgoDelegate_EInput1 _avg;

        public TrendFlatLine(AlgoDelegate_EInput1 del, string id, string name)
            : base(id, name)
        {
            _avg = del;
        }

        protected override AlgoParameterCollection CreateParamaterDesription()
        {
            return new AlgoParameterCollection();
        }

        protected override double[] Smooth(IEnumerable<double> y, IEnumerable<int> xIn, IEnumerable<int> xOut, object[] args)
        {
            double avg = _avg(y);
            double[] r = new double[xOut.Count()];

            for (int i = 0; i < r.Length; i++)
            {
                r[i] = avg;
            }

            return r;
        }
    }
}
