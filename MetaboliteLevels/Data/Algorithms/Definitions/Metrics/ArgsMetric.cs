﻿using System;
using MetaboliteLevels.Algorithms.Statistics.Metrics;
using MetaboliteLevels.Data.Algorithms.Definitions.Configurations;
using MetaboliteLevels.Data.Session.Associational;

namespace MetaboliteLevels.Algorithms.Statistics.Arguments
{
    /// <summary>
    /// Arguments for metrics (See MetricBase).
    /// 
    /// Nothing really special here!
    /// </summary>
    [Serializable]
    class ArgsMetric : ArgsBase
    {
        public ArgsMetric( string id, IProvider<IntensityMatrix> source, object[] parameters)
            : base( id, source, parameters )
        {
        }   
    }
}
