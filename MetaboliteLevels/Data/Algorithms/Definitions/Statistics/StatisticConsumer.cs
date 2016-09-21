using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Algorithms.Definitions.Base;
using MetaboliteLevels.Data.Algorithms.Definitions.Configurations;
using MetaboliteLevels.Data.Algorithms.General;
using MGui.Helpers;

namespace MetaboliteLevels.Data.Algorithms.Definitions.Statistics
{
    /// <summary>
    /// Special case of Statistic that take other statistics as inputs.
    /// </summary>
    sealed class StatisticConsumer : StatisticBase
    {
        private readonly AlgoDelegate_Input1 _delegate;

        public StatisticConsumer(AlgoDelegate_Input1 method, string id, string name)
            : base(id, name)
        {
            this._delegate = method;
        }

        public override double Calculate(InputStatistic input)
        {
            var src = (IEnumerable<WeakReference<ConfigurationStatistic>>)input.Args.Parameters[0];



            double[] vals = src.Select(delegate (WeakReference<ConfigurationStatistic> z)
                                       {
                                           ConfigurationStatistic a = z.GetTarget();

                                           if (a != null)
                                           {
                                               return a.Results.Results[ input.PeakA];
                                           }
                                           else
                                           {
                                               throw new InvalidOperationException("Statistic in " + this.ToString() + " no longer exists.");
                                           }
                                       }).ToArray();

            return _delegate(vals);
        }

        protected override AlgoParameterCollection CreateParamaterDesription()
        {
            return new AlgoParameterCollection(new AlgoParameter("statistics", AlgoParameterTypes.WeakRefStatisticArray));
        }

        public override bool SupportsInputFilters { get { return false; } }
    }
}
