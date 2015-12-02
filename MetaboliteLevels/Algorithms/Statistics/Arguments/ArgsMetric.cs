using System;

namespace MetaboliteLevels.Algorithms.Statistics.Arguments
{
    /// <summary>
    /// Arguments for metrics (See MetricBase).
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
                return  AlgoParameters.ParamsToHumanReadableString(Parameters, algorithm);
            }

            return "";
        }
    }
}
