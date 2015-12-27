using System.Collections.Generic;
using System.Linq;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Algorithms.Statistics.Trends
{
    /// <summary>
    /// Script-based trends.
    /// </summary>
    sealed class TrendScript : TrendBase
    {
        public const string INPUTS = "time=t1,value=x,result.time=t2";
        public readonly RScript _script;

        public TrendScript(string script, string id, string name)
            : base(id, name)
        {
            this._script = new RScript(script, INPUTS);
        }

        protected override double[] Smooth(IEnumerable<double> vIn, IEnumerable<int> o, IEnumerable<int> c, object[] args)
        {
            object[] inputs = { vIn, o, c };
            double[] r = Arr.Instance.RunScriptDoubleV(_script, inputs, args).ToArray();
            return r;
        }

        protected override AlgoParameterCollection CreateParamaterDesription()
        {
            return _script.RequiredParameters;
        }
    }
}
