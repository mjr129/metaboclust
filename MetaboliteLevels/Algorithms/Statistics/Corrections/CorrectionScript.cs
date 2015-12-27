using MetaboliteLevels.Algorithms.Statistics.Arguments;
using MetaboliteLevels.Algorithms.Statistics.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Algorithms.Statistics.Corrections
{
    class CorrectionScript : CorrectionBase
    {
        public readonly RScript _script;
        public const string INPUTS = "intensity=y";

        public CorrectionScript(string script, string id, string name)
            : base(id, name)
        {
            this._script = new RScript(script, INPUTS);
        }

        public override double[] Calculate(double[] raw, ArgsCorrection args)
        {
            object[] inputs = { raw };
            object[] parameters = args.Parameters;

            return Arr.Instance.RunScriptDoubleV(_script, inputs, parameters).ToArray();
        }

        protected override AlgoParameterCollection CreateParamaterDesription()
        {
            return _script.RequiredParameters;
        }
    }
}
