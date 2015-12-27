using System;

namespace MetaboliteLevels.Algorithms.Statistics.Arguments
{
    /// <summary>
    /// Arguments for metrics (See MetricBase).
    /// 
    /// Nothing really special here!
    /// </summary>
    [Serializable]
    class ArgsMetric : ArgsBase
    {
        public ArgsMetric(object[] parameters)
            : base(parameters)
        {
        }

        public override string ToString(AlgoBase algorithm)
        {
            if (Parameters != null)
            {
                return  AlgoParameterCollection.ParamsToHumanReadableString(Parameters, algorithm);
            }

            return "";
        }
    }
}
