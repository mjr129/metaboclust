﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Algorithms.Definitions.Base;
using MetaboliteLevels.Data.Algorithms.Definitions.Statistics.Misc;

namespace MetaboliteLevels.Data.Algorithms.Definitions.Statistics
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

        ///<summary>
        /// Class takes two input vectors as the input (e.g. "control-intensity" vs "drought-intensity" or even "intensity" vs. "time").
        /// </summary>
        public virtual bool IsMetric
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Designates support for observation input filters (e.g. "control, days 5-12" only).
        /// Most algorithms will support filters on the input vectors unless they themselves need to perform internal
        /// filtering of the input.
        /// </summary>
        public abstract bool SupportsInputFilters { get; }
    }
}
