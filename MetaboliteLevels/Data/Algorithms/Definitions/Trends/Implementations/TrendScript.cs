using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Algorithms.General;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Data.Algorithms.Definitions.Trends.Implementations
{
    /// <summary>
    /// Script-based trends.
    /// </summary>
    sealed class TrendScript : TrendBase
    {
        public const string INPUT_TABLE =
@"y,        y,      Numeric vector of length n. The Y values (intensity) of the input
  x,        x,      Numeric vector of length n. The X values (time or acquisition index) of the input  
  x.out,    x.out,  Numeric vector of length m. The X values of the output
  RETURNS,  ,       Numeric vector of length m. The Y values of the output
  SUMMARY,  ,       Smooths a set of values across time or acquisition order";

        public readonly RScript _script;

        public TrendScript(string script, string id, string name, string fileName)
            : base(id, name )
        {
            this._script = new RScript(script, INPUT_TABLE, fileName);
        }

        protected override double[] Smooth(IEnumerable<double> yIn, IEnumerable<int> xIn, IEnumerable<int> xOut, object[] args)
        {
            object[] inputs = { yIn, xIn, xOut };
            double[] r = Arr.Instance.RunScriptDoubleV(_script, inputs, args).ToArray();
            return r;
        }

        protected override AlgoParameterCollection CreateParamaterDesription()
        {
            return _script.RequiredParameters;
        }

        public override RScript Script => _script;
    }
}
