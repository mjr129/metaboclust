using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Algorithms.Definitions.Base;
using MetaboliteLevels.Data.Algorithms.Definitions.Base.Misc;
using MetaboliteLevels.Data.Algorithms.Definitions.Statistics;
using MetaboliteLevels.Data.Algorithms.Definitions.Statistics.Misc;
using MetaboliteLevels.Data.Algorithms.General;

namespace MetaboliteLevels.Data.Algorithms.Definitions.Metrics.Implementations
{
    /// <summary>
    /// Inbuilt metrics.
    /// </summary>
    class MetricInbuilt : MetricBase
    {
        public readonly AlgoDelegate_Input2 _delegate;

        public MetricInbuilt(AlgoDelegate_Input2 method, bool isMathDotNet)
            : base(method.Method.Name.ToUpper(), method.Method.Name)
        {
            this._delegate = method;             
        }

        public MetricInbuilt(AlgoDelegate_Input2 method, string id, string name, bool isMathDotNet )
            : base(id, name)
        {
            this._delegate = method;            
        }

        public override double Calculate(InputStatistic input)
        {
            double[] a = input.GetData(EAlgoInput.A, true, false, false, false, false).Primary;
            double[] b = input.GetData(EAlgoInput.B, true, false, false, false, false).Primary;

            return _delegate(a, b);
        }

        public override double QuickCalculate(IReadOnlyList< double> a, IReadOnlyList< double> b, object[] parameters)
        {   
            return _delegate(a.ToArray(), b.ToArray()); // TODO: Inefficient
        }

        protected override AlgoParameterCollection CreateParamaterDesription()
        {
            return new AlgoParameterCollection();
        }

        public override bool SupportsQuickCalculate
        {
            get
            {
                return true;
            }
        }

        public override bool SupportsInputFilters { get { return true; } }
    }
}
