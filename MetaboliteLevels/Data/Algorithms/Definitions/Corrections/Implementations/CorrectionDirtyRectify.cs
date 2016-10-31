using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Algorithms.General;

namespace MetaboliteLevels.Data.Algorithms.Definitions.Corrections.Implementations
{
    class CorrectionDirtyRectify : CorrectionBase
    {
        public CorrectionDirtyRectify(string id, string name) : base(id, name)
        {
        }

        public override double[] Calculate(IReadOnlyList< double> raw, ArgsCorrection args)
        {
            double c = (double)args.Parameters[0];

            double[] result = new double[raw.Count];

            for (int index = 0; index < result.Length; index++)
            {
                double v = raw[index];

                if (double.IsNaN(v) || double.IsInfinity(v))
                {
                    result[index] = c;
                }
                else
                {
                    result[index] = v;
                }
            }

            return result;
        }

        protected override AlgoParameterCollection CreateParamaterDesription()
        {                                                                                          
            return new AlgoParameterCollection( new[]
                {
                new AlgoParameter(
                                "c",
                                "The value to set NaN and infinite value to.",
                                AlgoParameterTypes.Double)
                } );
        }
    }
}
