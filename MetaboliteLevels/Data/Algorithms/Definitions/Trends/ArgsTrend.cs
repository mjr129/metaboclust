using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Algorithms.Definitions.Base;
using MetaboliteLevels.Data.Algorithms.Definitions.Configurations;

namespace MetaboliteLevels.Data.Algorithms.Definitions.Trends
{
    /// <summary>
    /// Arguments for trends (see TrendBase).
    /// 
    /// Nothing special here!
    /// </summary>
    [Serializable]
    class ArgsTrend : ArgsBase<TrendBase>
    {
        public ArgsTrend( string id, IMatrixProvider source, object[] parameters)
            : base(id, source, parameters)
        {
        }                                                                          
    }
}
