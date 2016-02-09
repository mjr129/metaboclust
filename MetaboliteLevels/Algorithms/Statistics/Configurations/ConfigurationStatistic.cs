using System;
using MetaboliteLevels.Algorithms.Statistics.Arguments;
using MetaboliteLevels.Algorithms.Statistics.Inputs;
using MetaboliteLevels.Algorithms.Statistics.Results;
using MetaboliteLevels.Algorithms.Statistics.Statistics;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Visualisables;
using System.Drawing;

namespace MetaboliteLevels.Algorithms.Statistics.Configurations
{
    /// <summary>
    /// Configured statistic algorithm (see ConfigurationBase).
    /// </summary>
    [Serializable]
    class ConfigurationStatistic : ConfigurationBase<StatisticBase, ArgsStatistic, ResultStatistic>
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
    }
}
