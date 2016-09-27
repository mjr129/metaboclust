using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Algorithms.Definitions.Statistics;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.Singular;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Data.Algorithms.Definitions.Configurations
{
    /// <summary>
    /// Configured statistic algorithm (see ConfigurationBase).
    /// </summary>
    [Serializable]
    sealed class ConfigurationStatistic : ConfigurationBase<StatisticBase, ArgsStatistic, ResultStatistic, SourceTracker>
    {                      
        internal double Calculate(Core core, Peak a)
        {
            return Args.GetAlgorithmOrThrow().Calculate(new InputStatistic(core, a, a, Args));
        }

        internal double Calculate(Core core, Peak a, Peak b) // TODO: What is this for?!
        {
            return Args.GetAlgorithmOrThrow().Calculate(new InputStatistic(core, a, b, Args));
        }

        protected override SourceTracker GetTracker()
        {
            return new SourceTracker( Args );
        }

        protected override void OnRun( Core core, ProgressReporter prog )
        {
            IntensityMatrix source = Args.SourceMatrix;

            double max = double.MinValue;
            double min = double.MaxValue;

            Dictionary<Peak, double> results = new Dictionary<Peak, double>();

            for (int peakIndex = 0; peakIndex < source.Rows.Length; peakIndex++)
            {
                prog.SetProgress( peakIndex, source.Rows.Length );

                Peak peak = source.Rows[peakIndex].Peak;
                double value = this.Calculate( core, peak );
                max = Math.Max( max, value );
                min = Math.Min( min, value );

                results.Add( peak, value );
            }

            SetResults( new ResultStatistic( results, min, max ) );
        }

        public double Get( Peak peak )
        {
            if (Results == null)
            {
                return double.NaN;
            }

            double result;

            if (!Results.Results.TryGetValue( peak, out result ))
            {
                return double.NaN;
            }

            return result;
        }
    }
}
