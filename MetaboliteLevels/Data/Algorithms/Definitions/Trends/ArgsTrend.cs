using System;
using MetaboliteLevels.Data.Algorithms.Definitions.Configurations;

namespace MetaboliteLevels.Algorithms.Statistics.Arguments
{
    /// <summary>
    /// Arguments for trends (see TrendBase).
    /// 
    /// Nothing special here!
    /// </summary>
    [Serializable]
    class ArgsTrend : ArgsBase
    {
        public ArgsTrend( MatrixProducer source, object[] parameters)
            : base(source, parameters)
        {
        }

        public override string ToString(AlgoBase algorithm)
        {
            return base.ToString();
        }
    }
}
