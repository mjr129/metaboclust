using System;

namespace MetaboliteLevels.Algorithms.Statistics.Arguments
{
    /// <summary>
    /// Arguments for trends (see TrendBase).
    /// </summary>
    [Serializable]
    class ArgsTrend : ArgsBase
    {
        public ArgsTrend(object[] parameters)
            : base(parameters)
        {
        }

        public override string ToString(AlgoBase algorithm)
        {
            if (Parameters != null)
            {
                return ("(" + AlgoParameters.ParamsToHumanReadableString(Parameters, algorithm) + ") ");
            }

            return string.Empty;
        }
    }
}
