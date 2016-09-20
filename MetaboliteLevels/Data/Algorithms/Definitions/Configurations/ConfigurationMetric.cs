using System;
using System.Collections.Generic;
using MetaboliteLevels.Algorithms.Statistics.Arguments;
using MetaboliteLevels.Algorithms.Statistics.Metrics;
using MetaboliteLevels.Algorithms.Statistics.Results;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Viewers.Lists;

namespace MetaboliteLevels.Algorithms.Statistics.Configurations
{
    /// <summary>
    /// Configured metric algorithm (see ConfigurationBase).
    /// </summary>
    [Serializable]
    sealed class ConfigurationMetric : ConfigurationBase<MetricBase, ArgsMetric, ResultStatistic, SourceTracker>
    {        
        internal double Calculate(double[] a, double[] b)
        {
            return Args.GetAlgorithmOrThrow().QuickCalculate(a, b, this.Args.Parameters);
        }

        protected override SourceTracker GetTracker()
        {
            return new SourceTracker( Args );
        }

        protected override void OnRun( Core core, ProgressReporter prog )
        {
            throw new InvalidOperationException("Metrics serve as part of another algorithm and cannot be run alone.");
        }
    }
}
