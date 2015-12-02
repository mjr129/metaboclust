using MetaboliteLevels.Algorithms.Statistics.Inputs;

namespace MetaboliteLevels.Algorithms.Statistics.Statistics
{
    /// <summary>
    /// Base class for statistics.
    /// 
    /// Statistics calculate a single value from the input.
    /// </summary>
    abstract class StatisticBase : AlgoBase
    {
        public StatisticBase(string id, string name)
            : base(id, name)
        {
            // NA
        }

        public abstract double Calculate(InputStatistic input);
    }
}
