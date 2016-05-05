﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Algorithms.Statistics.Arguments;

namespace MetaboliteLevels.Algorithms.Statistics.Corrections
{
    class CorrectionDirtyRectify : CorrectionBase
    {
        public CorrectionDirtyRectify(string id, string name) : base(id, name)
        {
        }

        public override double[] Calculate(double[] raw, ArgsCorrection args)
        {
            double c = (double)args.Parameters[0];

            double[] result = new double[raw.Length];

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
            return new AlgoParameterCollection(new AlgoParameter("zero", EAlgoParameterType.Double));
        }
    }
}