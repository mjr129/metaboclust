using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Algorithms.Definitions.Statistics.Misc;
using MetaboliteLevels.Data.Algorithms.General;
using MetaboliteLevels.Data.Session.Main;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Data.Algorithms.Definitions.Statistics.Implementations
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

            return Arr.Instance.PcaAnova(input.Args.SourceMatrix, input.PeakA, input.Core, groups.ToList(), reps.ToList());
        }

        protected override AlgoParameterCollection CreateParamaterDesription()
        {
            return new AlgoParameterCollection();
        }

        public override bool SupportsInputFilters { get { return true; } }
    }
}
