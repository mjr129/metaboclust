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
        public const string INPUTS = "input=x,intensity=-,group=-,time=-,rep=-";
        public readonly RScript _script;                

        public StatisticScript(string script, string id, string name)
            : base(id, name)
        {
            this._script = new RScript(script, INPUTS);
            // this._script.CheckInputMask("10000");
        }

        public override double Calculate(InputStatistic input)
        {
            var rp = _script;

            var a = input.GetData(EAlgoInput.A, rp.IsInputPresent(0), rp.IsInputPresent(1), rp.IsInputPresent(2), rp.IsInputPresent(3), rp.IsInputPresent(4));

            object[] inputs = { a.Primary, a.Intensity, a.Group, a.Time, a.Rep };

            return Arr.Instance.RunScriptDouble(_script, inputs, input.Args.Parameters);
        }

        protected override AlgoParameterCollection CreateParamaterDesription()
        {
            return _script.RequiredParameters;
        }

        public override bool SupportsInputFilters { get { return true; } }
    }
}
