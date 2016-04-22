using System;
using System.Collections.Generic;
using MetaboliteLevels.Algorithms.Statistics.Arguments;
using MetaboliteLevels.Algorithms.Statistics.Metrics;
using MetaboliteLevels.Algorithms.Statistics.Results;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Viewers.Lists;

namespace MetaboliteLevels.Algorithms.Statistics.Configurations
{
    /// <summary>
    /// Configured metric algorithm (see ConfigurationBase).
    /// </summary>
    [Serializable]
    sealed class ConfigurationMetric : ConfigurationBase<MetricBase, ArgsMetric, ResultStatistic>
    {
        public ConfigurationMetric( string name, string comments, string id, ArgsMetric args)
            : base( name, comments, id, args)
        {
            // NA
        }

        internal double Calculate(double[] a, double[] b)
        {
            return Cached.QuickCalculate(a, b, this.Args.Parameters);
        }

        protected sealed override IEnumerable<Column> GetExtraColumns(Core core)
        {
            List<Column<ConfigurationMetric>> columns = new List<Column<ConfigurationMetric>>();

            columns.Add("Arguments\\Parameters", z => z.Args.Parameters);

            return columns;
        }
    }
}
