using System;
using MetaboliteLevels.Algorithms.Statistics.Arguments;
using MetaboliteLevels.Algorithms.Statistics.Trends;
using MetaboliteLevels.Data.DataInfo;
using System.Collections.Generic;
using System.Linq;
using MetaboliteLevels.Algorithms.Statistics.Results;
using MetaboliteLevels.Data.Algorithms.Definitions.Configurations;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Viewers.Lists;

namespace MetaboliteLevels.Algorithms.Statistics.Configurations
{
    /// <summary>
    /// Configured trend algorithm (see ConfigurationBase).
    /// </summary>
    [Serializable]
    sealed class ConfigurationTrend : ConfigurationBase<TrendBase, ArgsTrend, ResultTrend>, IProvider<IntensityMatrix>
    {
        public ConfigurationTrend(string name, string comments, string trendId, ArgsTrend args)
            : base(name, comments, trendId, args)
        {
            // NA
        }

        public Vector CreateTrend( Core core, Vector vector )
        {
            double[] newValues = CreateTrend( vector.Observations, core.Conditions, core.Groups, vector.Values );
            IntensityMatrix temporary = new IntensityMatrix( new[] { vector.Peak }, core.Conditions.ToArray(), new[] { newValues } );
            return temporary.Vectors[0];
        }

        internal double[] CreateTrend(IReadOnlyList<ObservationInfo> inOrder, IReadOnlyList<ObservationInfo> outOrder, IReadOnlyList<GroupInfo> typeInfo, double[] raw)
        {
            return Cached.SmoothByType(inOrder, outOrder, typeInfo, raw, this.Args);
        }

        protected override IEnumerable<Column> GetExtraColumns(Core core)
        {
            List<Column<ConfigurationTrend>> columns = new List<Column<ConfigurationTrend>>();

            columns.Add("Arguments\\Parameters", z => z.Args.Parameters);

            return columns;
        }

        public IntensityMatrix Provide => Results.Matrix;
    }
}
