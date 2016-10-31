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
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Data.Algorithms.Definitions.Metrics.Implementations
{
    /// <summary>
    /// Script-based metrics.
    /// </summary>
    class MetricScript : MetricBase
    {
        public readonly RScript _script;
        public const string INPUT_TABLE =
@"value.a,      a,  ""Numeric vector of length n. The first input vector, corresponding to the user's chosen criteria.""
  value.b,      b,  ""Numeric vector of length m. The second input vector, corresponding to the user's chosen criteria.""
  intensity.a,  -,  Numeric vector of length n. The intensities corresponding to the first input vector.
  intensity.b,  -,  Numeric vector of length m. The intensities corresponding to the second input vector.
  group.a,      -,  Numeric vector of length n. The experimental groups corresponding to the first input vector.
  group.b,      -,  Numeric vector of length m. The experimental groups corresponding to the second input vector.
  time.a,       -,  Numeric vector of length n. The times corresponding to the first input vector.
  time.b,       -,  Numeric vector of length m. The times corresponding to the second input vector.
  rep.a,        -,  Numeric vector of length n. The replicate indices corresponding to the first input vector.
  rep.b,        -,  Numeric vector of length m. The replicate indices corresponding to the second input vector.
  SUMMARY,      ,   Calculates a statistic comparing two vectors.
  RETURNS,      ,   Numeric. The result of the calculation.";

        private readonly bool _supportsQuickCalculate;

        public MetricScript(string script, string id, string name, string fileName)
            : base(id, name  )
        {
            this._script = new RScript(script, INPUT_TABLE, fileName);

            _supportsQuickCalculate = _script.CheckInputMask("1100000000");   
        }

        public override double QuickCalculate(IReadOnlyList< double> a, IReadOnlyList< double> b, object[] args)
        {
            UiControls.Assert(SupportsQuickCalculate, "Quick calculate called on a non quick-calculate script.");

            object[] inputs = { a, b, null, null, null, null, null, null, null, null };
            return Arr.Instance.RunScriptDouble(_script, inputs, args);
        }

        public override RScript Script => _script;

        public override double Calculate(InputStatistic input)
        {
            RScript rp = _script;

            InputStatistic.GetDataInfo a = input.GetData(EAlgoInput.A, rp.IsInputPresent(0), rp.IsInputPresent(2), rp.IsInputPresent(4), rp.IsInputPresent(6), rp.IsInputPresent(8));
            InputStatistic.GetDataInfo b = input.GetData(EAlgoInput.B, rp.IsInputPresent(1), rp.IsInputPresent(3), rp.IsInputPresent(5), rp.IsInputPresent(7), rp.IsInputPresent(9));

            object[] inputs = { a.Primary, b.Primary, a.Intensity, b.Intensity, a.Group, b.Group, a.Time, b.Time, a.Rep, b.Rep };

            return Arr.Instance.RunScriptDouble(_script, inputs, input.Args.Parameters);
        }

        protected override AlgoParameterCollection CreateParamaterDesription()
        {
            return _script.RequiredParameters;
        }

        public override bool SupportsQuickCalculate
        {
            get
            {
                return _supportsQuickCalculate;
            }
        }

        public override bool SupportsInputFilters { get { return true; } }
    }
}
