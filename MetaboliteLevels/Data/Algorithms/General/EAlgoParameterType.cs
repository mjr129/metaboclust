using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Algorithms.Statistics
{
    /// <summary>
    /// Types of parameters passed to algorithms
    /// </summary> 
    public enum EAlgoParameterType
    {
        /// <summary>
        /// Integers (e.g. the k in k-means)
        /// </summary>
        [Name("Int")]
        Integer,

        /// <summary>
        /// Reals
        /// </summary>
        [Name("Double")]
        Double,

        /// <summary>
        /// Zero or more statistics
        /// (as WeakReference&lt;ConfigurationStatistic&gt;[])
        /// </summary>
        [Name("StatisticArray")]
        WeakRefStatisticArray,


        /// <summary>
        /// Peaks
        /// (as WeakReference&lt;Peak&gt;)
        /// </summary>
        [Name("Peak")]
        WeakRefPeak,

        /// <summary>
        /// Experimental groups
        /// (as GroupInfo)
        /// </summary>
        [Name("Group")]
        Group,

        /// <summary>
        /// Clusterers
        /// (as WeakReference&lt;ConfigurationClusterer&gt;)
        /// </summary>
        [Name("ConfigurationClusterer")]
        WeakRefConfigurationClusterer,
    }
}
