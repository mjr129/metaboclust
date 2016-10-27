using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Algorithms.Definitions.Base.Misc;
using MetaboliteLevels.Data.Algorithms.Definitions.Statistics.Misc;
using MetaboliteLevels.Data.Algorithms.General;

namespace MetaboliteLevels.Data.Algorithms.Definitions.Statistics.Implementations
{
    /// <summary>
    /// Inbuilt statistics.
    /// </summary>
    sealed class StatisticInbuilt : StatisticBase
    {
        private readonly AlgoDelegate_Input1 _delegate;

        public StatisticInbuilt(AlgoDelegate_Input1 method, bool isMathDotNet)
            : base(method.Method.Name.ToUpper(), method.Method.Name)
        {
            this._delegate = method;      
        }

        public override double Calculate(InputStatistic input)
        {
            double[] a = input.GetData(EAlgoInput.A, true, false, false, false, false).Primary;

            return this._delegate(a);
        }

        protected override AlgoParameterCollection CreateParamaterDesription()
        {
            return new AlgoParameterCollection();
        }

        public override bool SupportsInputFilters { get { return true; } }
    }
}
