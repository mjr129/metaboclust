using System;
using MetaboliteLevels.Data.Algorithms.Definitions.Configurations;
using MetaboliteLevels.Data.Session.Associational;

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
        public ArgsTrend( string id, IProvider<IntensityMatrix> source, object[] parameters)
            : base(id, source, parameters)
        {
        }            
    }
}
