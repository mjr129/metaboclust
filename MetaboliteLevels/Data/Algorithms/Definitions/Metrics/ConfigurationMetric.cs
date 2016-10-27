using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Algorithms.Definitions.Base;
using MetaboliteLevels.Data.Algorithms.Definitions.Base.Misc;
using MetaboliteLevels.Data.Algorithms.Definitions.Statistics;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Data.Algorithms.Definitions.Metrics
{
    /// <summary>
    /// Configured metric algorithm (see ConfigurationBase).
    /// </summary>
    [Serializable]
    sealed class ConfigurationMetric : ConfigurationBase<MetricBase, ArgsMetric, ResultStatistic, SourceTracker>
    {        
        internal double Calculate(double[] a, double[] b)
        {
            return this.Args.GetAlgorithmOrThrow().QuickCalculate(a, b, this.Args.Parameters);
        }

        protected override SourceTracker GetTracker()
        {
            return new SourceTracker( this.Args );
        }

        protected override void OnRun( Core core, ProgressReporter prog )
        {
            throw new InvalidOperationException("Metrics serve as part of another algorithm and cannot be run alone.");
        }

        protected override Image ResultIcon => Resources.ListIconResultVector;
    }
}
