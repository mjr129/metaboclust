using MetaboliteLevels.Algorithms.Statistics.Arguments;
using MetaboliteLevels.Algorithms.Statistics.Inputs;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Algorithms.Statistics.Statistics
{
    /// <summary>
    /// Script-based statistics.
    /// </summary>
    sealed class StatisticScript : StatisticBase
    {
        public const string INPUT_TABLE =
@"input,    x,  Numeric vector of length n. The input vector to calculate the statistic for (the source depends on the user's choice)
  intensity,-,  Numeric vector of length n. The intensities corresponding to the input vector
  group,    -,  Numeric vector of length n. The experimental groups corresponding to the input vector
  time,     -,  Numeric vector of length n. The times corresponding the the input vector
  rep,      -,  Numeric vector of length n. The replicates corresponding the input vector
  SUMMARY,   ,  Calculates a statistic for the user's chosen input
  RETURNS,   ,  Numeric. The statistic calculated for the specified vectors.";

        public readonly RScript _script;                

        public StatisticScript(string script, string id, string name, string fileName)
            : base(id, name )
        {
            this._script = new RScript(script, INPUT_TABLE, fileName);
            // this._script.CheckInputMask("10000");
        }

        public override double Calculate(InputStatistic input)
        {
            RScript rp = _script;

            var a = input.GetData(EAlgoInput.A, rp.IsInputPresent(0), rp.IsInputPresent(1), rp.IsInputPresent(2), rp.IsInputPresent(3), rp.IsInputPresent(4));

            object[] inputs = { a.Primary, a.Intensity, a.Group, a.Time, a.Rep };

            return Arr.Instance.RunScriptDouble(_script, inputs, input.Args.Parameters);
        }

        protected override AlgoParameterCollection CreateParamaterDesription()
        {
            return _script.RequiredParameters;
        }

        public override bool SupportsInputFilters { get { return true; } }

        public override RScript Script => _script;
    }
}
