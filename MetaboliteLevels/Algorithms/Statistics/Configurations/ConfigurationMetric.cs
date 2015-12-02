using System;
using MetaboliteLevels.Algorithms.Statistics.Arguments;
using MetaboliteLevels.Algorithms.Statistics.Metrics;
using MetaboliteLevels.Algorithms.Statistics.Results;

namespace MetaboliteLevels.Algorithms.Statistics.Configurations
{
    /// <summary>
    /// Configured metric algorithm (see ConfigurationBase).
    /// </summary>
    [Serializable]
    class ConfigurationMetric : ConfigurationBase<MetricBase, ArgsMetric, ResultStatistic>
    {
        public ConfigurationMetric( string name, string comments, string id, ArgsMetric args)
            : base( name, comments, id, args)
        {
        }

        internal double Calculate(double[] a, double[] b)
        {
            return Cached.QuickCalculate(a, b, this.Args.Parameters);
        }
    }
}
