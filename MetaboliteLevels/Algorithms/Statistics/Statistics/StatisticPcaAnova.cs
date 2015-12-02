using System.Collections.Generic;
using System.Linq;
using MetaboliteLevels.Algorithms.Statistics.Inputs;
using MetaboliteLevels.Data.DataInfo;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Algorithms.Statistics.Statistics
{
    /// <summary>
    /// Statistic specialisation just for PCA-ANOVA.
    /// </summary>
    sealed class StatisticPcaAnova : StatisticBase
    {
        public StatisticPcaAnova(string id, string name)
            : base(id, name)
        {
        }

        public override double Calculate(InputStatistic input)
        {
            var vo = input.Args.VectorAConstraint.Test(input.Core.Observations).Passed;

            HashSet<GroupInfo> groups = new HashSet<GroupInfo>(vo.Select(z => z.Group));
            HashSet<int> reps = new HashSet<int>(vo.Select(z => z.Rep));

            return Arr.Instance.PcaAnova(input.PeakA, input.Core, groups.ToList(), reps.ToList());
        }

        public override AlgoParameters GetParams()
        {
            return new AlgoParameters(AlgoParameters.ESpecial.None, null);
        }
    }
}
