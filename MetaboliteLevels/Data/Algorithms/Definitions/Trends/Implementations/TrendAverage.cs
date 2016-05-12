using System.Collections.Generic;
using MetaboliteLevels.Algorithms.Statistics.Arguments;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Algorithms.Statistics.Trends
{
    sealed class TrendAverage : TrendInbuilt
    {
        private readonly AlgoDelegate_EInput1 _avg;

        public TrendAverage(AlgoDelegate_EInput1 del, string id, string name)
            : base(id, name)
        {
            Description = "Calculates an average or moving average. The window width parameter 'w' must be specified.";
            _avg = del;
        }

        protected override AlgoParameterCollection CreateParamaterDesription()
        {                                          
            return new AlgoParameterCollection(new AlgoParameter("w", EAlgoParameterType.Integer));
        }

        protected override double SmoothPoint(IEnumerable<int> x, double[] y, int xTarget, object arg)
        {
            int medianRadius = (int)arg;

            IEnumerable<int> indicesForWindow = x.Which(λ => (λ >= (xTarget - medianRadius)) && (λ <= (xTarget + medianRadius)));
            IEnumerable<double> valuesForWindow = y.At( indicesForWindow);

            return _avg(valuesForWindow);
        }

        protected override object InterpretArgs(object[] args)
        {
            int medianWindow = (int)args[0];

            int medianRadius = (medianWindow - 1) / 2;

            if (medianRadius != 0)
            {
                UiControls.Assert((medianRadius * 2) == (medianWindow - 1), "Moving average window must be an odd number.");
            }

            return medianRadius;
        }
    }
}
