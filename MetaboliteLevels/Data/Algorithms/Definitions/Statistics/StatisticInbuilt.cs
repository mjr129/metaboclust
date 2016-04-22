using System;
using MetaboliteLevels.Algorithms.Statistics.Arguments;
using MetaboliteLevels.Algorithms.Statistics.Inputs;

namespace MetaboliteLevels.Algorithms.Statistics.Statistics
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
            IsMathDotNet = isMathDotNet;
        }

        public override double Calculate(InputStatistic input)
        {
            double[] a = input.GetData(EAlgoInput.A, true, false, false, false, false).Primary;

            return _delegate(a);
        }

        protected override AlgoParameterCollection CreateParamaterDesription()
        {
            return new AlgoParameterCollection();
        }

        public override bool SupportsInputFilters { get { return true; } }
    }
}
