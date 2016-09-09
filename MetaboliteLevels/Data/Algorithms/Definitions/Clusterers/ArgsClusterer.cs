using System;
using System.Text;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Settings;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Algorithms.Statistics.Results;
using MetaboliteLevels.Data.Algorithms.Definitions.Configurations;
using MGui.Helpers;
using MetaboliteLevels.Data.Session.Associational;

namespace MetaboliteLevels.Algorithms.Statistics.Arguments
{
    /// <summary>
    /// ArgsBase for clusterers.
    /// </summary>
    [Serializable]
    class ArgsClusterer : ArgsBase
    {
        /// <summary>
        /// Defines what peaks go into the "insignificants" cluster.
        /// </summary>
        public readonly PeakFilter PeakFilter;

        /// <summary>
        /// Defines what creates the cluster vectors.
        /// </summary>
        public readonly ObsFilter ObsFilter;

        /// <summary>
        /// Defines the distance metric used.
        /// (Not used by all algorithms!)
        /// </summary>
        public readonly ConfigurationMetric Distance;            

        /// <summary>
        /// When set peaks are split into one vecror per group.
        /// </summary>
        public readonly bool SplitGroups;

        /// <summary>
        /// What statistics to calculate when the algorithm is complete.
        /// </summary>
        public readonly EClustererStatistics Statistics;

        /// <summary>
        /// Constructor.
        /// </summary>  
        public ArgsClusterer( IProvider<IntensityMatrix> source, PeakFilter sigFilter, ConfigurationMetric distance, ObsFilter atypes, bool splitGroups, EClustererStatistics suppressMetric, object[] parameters)
            : base( source, parameters )
        {
            this.PeakFilter = sigFilter;
            this.Distance = distance;                               
            this.ObsFilter = atypes;
            this.SplitGroups = splitGroups;
            this.Statistics = suppressMetric;
        }

        public override string ToString(AlgoBase algorithm)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append( base.ToString() );

            if (ObsFilter != null)
            {
                sb.Append("; o = " + ObsFilter.ToString());
            }

            if (SplitGroups)
            {
                sb.Append("; [G]");
            }

            if (Distance != null)
            {
                sb.Append("; d = " + Distance.ToString());
            }

            if (PeakFilter != null)
            {
                sb.Append("; p = " + PeakFilter.ToString());
            }

            return sb.ToString();
        }
    }
}
