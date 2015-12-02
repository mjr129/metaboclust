using System;
using MetaboliteLevels.Algorithms.Statistics.Arguments;
using MetaboliteLevels.Algorithms.Statistics.Trends;
using MetaboliteLevels.Data.DataInfo;
using System.Collections.Generic;
using MetaboliteLevels.Algorithms.Statistics.Results;

namespace MetaboliteLevels.Algorithms.Statistics.Configurations
{
    /// <summary>
    /// Configured trend algorithm (see ConfigurationBase).
    /// </summary>
    [Serializable]
    class ConfigurationTrend : ConfigurationBase<TrendBase, ArgsTrend, ResultTrend>
    {
        public ConfigurationTrend( string name, string comments, string trendId, ArgsTrend args)
            : base( name, comments, trendId, args)
        {
            // NA
        }

        internal double[] CreateTrend(IReadOnlyList<ObservationInfo> obsInfo, IReadOnlyList<ConditionInfo> condInfo, IReadOnlyList<GroupInfo> typeInfo, double[] raw)
        {
            return Cached.SmoothByType(obsInfo, condInfo, typeInfo, raw, this.Args);
        }
    }
}
