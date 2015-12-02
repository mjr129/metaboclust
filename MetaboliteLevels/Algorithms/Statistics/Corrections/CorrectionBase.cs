using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Algorithms.Statistics.Inputs;
using MetaboliteLevels.Algorithms.Statistics.Arguments;

namespace MetaboliteLevels.Algorithms.Statistics.Corrections
{
    /// <summary>
    /// Base class for corrections - algorithms that transform the input vector.
    /// </summary>
    abstract class CorrectionBase : AlgoBase
    {
        public CorrectionBase(string id, string name)
            : base(id, name)
        {
            // NA
        }

        public abstract double[] Calculate(double[] raw, ArgsCorrection args);

        public abstract override AlgoParameters GetParams();
    }
}
