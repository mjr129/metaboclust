using System;
using MetaboliteLevels.Data.Algorithms.Definitions.Configurations;
using MetaboliteLevels.Data.Session.Associational;

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
        public ArgsMetric( IProvider<IntensityMatrix> source, object[] parameters)
            : base( source, parameters )
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
