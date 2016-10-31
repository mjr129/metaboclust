using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Algorithms.Definitions.Base;
using MetaboliteLevels.Data.Algorithms.General;

namespace MetaboliteLevels.Data.Algorithms.Definitions.Corrections
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

        public abstract double[] Calculate(IReadOnlyList< double> raw, ArgsCorrection args);

        protected abstract override AlgoParameterCollection CreateParamaterDesription();
    }
}
