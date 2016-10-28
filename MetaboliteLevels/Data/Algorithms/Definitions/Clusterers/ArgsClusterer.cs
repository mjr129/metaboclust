using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Algorithms.Definitions.Base;
using MetaboliteLevels.Data.Algorithms.Definitions.Base.Misc;
using MetaboliteLevels.Data.Algorithms.Definitions.Metrics;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Session.Main;
using MetaboliteLevels.Gui.Controls.Lists;

namespace MetaboliteLevels.Data.Algorithms.Definitions.Clusterers
{
    /// <summary>
    /// ArgsBase for clusterers.
    /// </summary>
    [Serializable]
    class ArgsClusterer : ArgsBase<ClustererBase>
    {
        /// <summary>
        /// Defines what peaks go into the "insignificants" cluster.
        /// </summary>
        [XColumn]
        public readonly PeakFilter PeakFilter;

        /// <summary>
        /// Defines what creates the cluster vectors.
        /// </summary>
        [XColumn]
        public readonly ObsFilter ObsFilter;

        /// <summary>
        /// Defines the distance metric used.
        /// (Not used by all algorithms!)
        /// </summary>
        [XColumn]
        public readonly ConfigurationMetric Distance;

        /// <summary>
        /// When set peaks are split into one vecror per group.
        /// </summary>
        [XColumn]
        public readonly bool SplitGroups;

        /// <summary>
        /// What statistics to calculate when the algorithm is complete.
        /// </summary>
        [XColumn]
        public readonly EClustererStatistics Statistics;

        /// <summary>
        /// Prefix of cluster names user override.
        /// </summary>
        [XColumn]
        public readonly string OverrideShortName;

        /// <summary>
        /// Constructor.
        /// </summary>  
        public ArgsClusterer( string id, IMatrixProvider source, PeakFilter sigFilter, ConfigurationMetric distance, ObsFilter atypes, bool splitGroups, EClustererStatistics suppressMetric, object[] parameters, string clusterNamePrefix)
            : base( id, source, parameters )
        {
            this.PeakFilter = sigFilter;
            this.Distance = distance;                               
            this.ObsFilter = atypes;
            this.SplitGroups = splitGroups;
            this.Statistics = suppressMetric;
            this.OverrideShortName = clusterNamePrefix;
        }

        /// <summary>
        /// Prefix of cluster names.
        /// </summary>
        [XColumn]
        public string ShortName
        {
            get
            {
                if (string.IsNullOrEmpty( OverrideShortName ))
                {
                    return DefaultShortName;
                }
                else
                {
                    return OverrideShortName;
                }
            }
        }

        /// <summary>
        /// Default prefix of cluster names.
        /// </summary>
        public string DefaultShortName
        {
            get
            {
                string x = DisplayName;
                return x.Substring( 0, Math.Min( x.Length, 2 ) );
            }
        }

        public override string DefaultDisplayName
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                sb.Append( base.DefaultDisplayName );

                if (ObsFilter != null)
                {
                    sb.Append( "; o = " + ObsFilter.ToString() );
                }

                if (SplitGroups)
                {
                    sb.Append( "; [G]" );
                }

                if (Distance != null)
                {
                    sb.Append( "; d = " + Distance.ToString() );
                }

                if (PeakFilter != null)
                {
                    sb.Append( "; p = " + PeakFilter.ToString() );
                }

                return sb.ToString();
            }
        } 
    }
}
