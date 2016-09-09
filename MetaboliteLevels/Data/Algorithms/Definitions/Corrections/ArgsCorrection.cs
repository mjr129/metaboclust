using System;
using System.Text;
using MetaboliteLevels.Data.Algorithms.Definitions.Configurations;

namespace MetaboliteLevels.Algorithms.Statistics.Arguments
{
    /// <summary>
    /// Arguments for CorrectionBase derivatives.
    /// 
    /// Nothing really special here!
    /// </summary>
    [Serializable]
    class ArgsCorrection : ArgsBase
    {
        public ArgsCorrection( MatrixProducer source, object[] parameters)
            : base( source, parameters )
        {
        }

        public override string ToString(AlgoBase algorithm)
        {
            StringBuilder sb = new StringBuilder();

            if (Parameters != null)
            {
                sb.Append(AlgoParameterCollection.ParamsToHumanReadableString(Parameters,algorithm ));
            }

            return sb.ToString();
        }
    }
}
