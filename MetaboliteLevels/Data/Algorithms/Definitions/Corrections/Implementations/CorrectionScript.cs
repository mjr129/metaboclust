using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Algorithms.General;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Data.Algorithms.Definitions.Corrections.Implementations
{
    class CorrectionScript : CorrectionBase
    {
        public readonly RScript _script;
        public const string INPUT_TABLE =
@"y,        y,  Numeric vector of length n. The source values (intensities).
  RETURNS,  ,   Numeric vector of length n. The corrected values (replacement intensities).
  SUMMARY,  ,   Performs data correction based on intensity data alone. This is performed on a per-peak basis.";

        public CorrectionScript(string script, string id, string name, string fileName)
            : base(id, name  )
        {
            this._script = new RScript(script, INPUT_TABLE, fileName);
        }

        public override double[] Calculate(IReadOnlyList< double> raw, ArgsCorrection args)
        {
            object[] inputs = { raw };
            object[] parameters = args.Parameters;

            return Arr.Instance.RunScriptDoubleV(_script, inputs, parameters).ToArray();
        }

        protected override AlgoParameterCollection CreateParamaterDesription()
        {
            return _script.RequiredParameters;
        }

        public override RScript Script => _script;
    }
}
