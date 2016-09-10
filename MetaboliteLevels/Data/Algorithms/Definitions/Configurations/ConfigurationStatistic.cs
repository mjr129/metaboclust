using System;
using MetaboliteLevels.Algorithms.Statistics.Arguments;
using MetaboliteLevels.Algorithms.Statistics.Inputs;
using MetaboliteLevels.Algorithms.Statistics.Results;
using MetaboliteLevels.Algorithms.Statistics.Statistics;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Visualisables;
using System.Drawing;
using MetaboliteLevels.Viewers.Lists;
using System.Collections.Generic;
using System.Diagnostics;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Algorithms.Statistics.Configurations
{
    /// <summary>
    /// Configured statistic algorithm (see ConfigurationBase).
    /// </summary>
    [Serializable]
    sealed class ConfigurationStatistic : ConfigurationBase<StatisticBase, ArgsStatistic, ResultStatistic>
    {
        public ConfigurationStatistic(string name, string comments, string id, ArgsStatistic args)
            : base(name, comments, id, args)
        {
        }

        internal double Calculate(Core core, Peak a)
        {
            return Cached.Calculate(new InputStatistic(core, a, a, Args));
        }

        internal double Calculate(Core core, Peak a, Peak b) // TODO: What is this for?!
        {
            return Cached.Calculate(new InputStatistic(core, a, b, Args));
        }

        protected sealed override IEnumerable<Column> GetExtraColumns(Core core)
        {
            List<Column<ConfigurationStatistic>> columns = new List<Column<ConfigurationStatistic>>();

            columns.Add("Arguments\\Parameters", z => z.Args.Parameters);
            columns.Add("Arguments\\Source", z => z.Args.SourceProvider);
            columns.AddSubObject(core, "Arguments\\First vector constraint", z => z.Args.VectorAConstraint);
            columns.AddSubObject(core, "Arguments\\Second vector constraint", z => z.Args.VectorBConstraint);
            columns.AddSubObject(core, "Arguments\\Second vector peak", z => z.Args.VectorBPeak);
            columns.Add("Arguments\\Second vector source", z => z.Args.VectorBSource);

            return columns;
        }

        internal override bool Run( Core core, ProgressReporter prog )
        {
            IntensityMatrix source = Args.SourceMatrix;
                                                                      
            double max = double.MinValue;
            double min = double.MaxValue;

            try
            {
                Dictionary<Peak, double> results = new Dictionary<Peak, double>();

                for (int peakIndex = 0; peakIndex < source.Rows.Length; peakIndex++)
                {
                    prog.SetProgress( peakIndex, source.Rows.Length );

                    Peak peak = source.Rows[peakIndex].Peak;
                    double value = this.Calculate( core, peak );
                    max = Math.Max( max, value );
                    min = Math.Min( min, value ); 
                }

                SetResults( new ResultStatistic( results, min, max ) );
                return true;
            }
            catch (Exception ex)
            {                                   
                this.SetError( ex );
                return false;
            }
        }
    }
}
