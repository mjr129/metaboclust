using MetaboliteLevels.Algorithms.Statistics.Statistics;

namespace MetaboliteLevels.Algorithms.Statistics.Metrics
{
    /// <summary>
    /// Base class for metrics.
    /// 
    /// Metrics are a special case of StatisticBase that takes two input vectors.
    /// This allows them to be used in e.g. clustering as the distance measure.
    /// </summary>
    abstract class MetricBase : StatisticBase
    {
        public MetricBase(string id, string name)
            : base(id, name)
        {
            // NA
        }

        /// <summary>
        /// Calculates the metric of the two input vectors [a] and [b].
        /// This is only available if the statistic has declared [ESpecial.SupportsQuickCalculate] on its parameters.
        /// </summary>
        public abstract double QuickCalculate(double[] a, double[] b, object[] parameters);
    }
}
