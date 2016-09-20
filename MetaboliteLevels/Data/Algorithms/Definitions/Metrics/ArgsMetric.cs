using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Algorithms.Definitions.Base;
using MetaboliteLevels.Data.Algorithms.Definitions.Configurations;

namespace MetaboliteLevels.Data.Algorithms.Definitions.Metrics
{
    /// <summary>
    /// Arguments for metrics (See MetricBase).
    /// 
    /// Nothing really special here!
    /// </summary>
    [Serializable]
    class ArgsMetric : ArgsBase<MetricBase>
    {
        public ArgsMetric( string id, IMatrixProvider source, object[] parameters)
            : base( id, source, parameters )
        {
        }   
    }
}
