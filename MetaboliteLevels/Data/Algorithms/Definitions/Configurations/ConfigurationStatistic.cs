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
            columns.Add("Arguments\\Source mode", z => z.Args.SourceMode);
            columns.AddSubObject(core, "Arguments\\First vector constraint", z => z.Args.VectorAConstraint);
            columns.AddSubObject(core, "Arguments\\Second vector constraint", z => z.Args.VectorBConstraint);
            columns.AddSubObject(core, "Arguments\\Second vector peak", z => z.Args.VectorBPeak);
            columns.Add("Arguments\\Second vector source", z => z.Args.VectorBSource);

            return columns;
        }
    }
}
