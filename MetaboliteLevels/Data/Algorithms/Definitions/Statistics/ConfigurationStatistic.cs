using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Algorithms.Definitions.Base;
using MetaboliteLevels.Data.Algorithms.Definitions.Base.Misc;
using MetaboliteLevels.Data.Algorithms.Definitions.Statistics.Misc;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Session.Main;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Data.Algorithms.Definitions.Statistics
{
    /// <summary>
    /// Configured statistic algorithm (see ConfigurationBase).
    /// </summary>
    [Serializable]
    sealed class ConfigurationStatistic : ConfigurationBase<StatisticBase, ArgsStatistic, ResultStatistic, SourceTracker>
    {                      
        internal double Calculate(Core core, Peak a)
        {
            return this.Args.GetAlgorithmOrThrow().Calculate(new InputStatistic(core, a, a, this.Args));
        }

        internal double Calculate(Core core, Peak a, Peak b) // TODO: What is this for?!
        {
            return this.Args.GetAlgorithmOrThrow().Calculate(new InputStatistic(core, a, b, this.Args));
        }

        protected override SourceTracker GetTracker()
        {
            return new SourceTracker( this.Args );
        }

        protected override void OnRun( Core core, ProgressReporter prog )
        {
            IntensityMatrix source = this.Args.SourceMatrix;

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

            this.SetResults( new ResultStatistic( results, min, max ) );
        }

        protected override Image ResultIcon => Resources.ListIconResultVector;

        public double Get( Peak peak )
        {
            if (this.Results == null)
            {
                return double.NaN;
            }

            double result;

            if (!this.Results.Results.TryGetValue( peak, out result ))
            {
                return double.NaN;
            }

            return result;
        }
    }
}
