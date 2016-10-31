using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Algorithms.Definitions.Statistics;

namespace MetaboliteLevels.Data.Algorithms.Definitions.Metrics
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
        public abstract double QuickCalculate(IReadOnlyList< double> a, IReadOnlyList< double> b, object[] parameters);

        /// <summary>
        /// OVERRIDE
        /// </summary>
        public sealed override bool IsMetric
        {
            get
            {
                return true;
            }
        }

        /// <summary>                                    
        /// Designates support for the QuickCalculate method that takes two input vectors and no filters.
        /// Most metrics will support this unless they use some obscure script that requires additional information, such as time or rep№s for each peak.
        /// </summary>

        public abstract bool SupportsQuickCalculate { get; }

    }
}
