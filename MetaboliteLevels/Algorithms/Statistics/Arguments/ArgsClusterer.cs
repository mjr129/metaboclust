using System;
using System.Text;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Settings;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Algorithms.Statistics.Results;

namespace MetaboliteLevels.Algorithms.Statistics.Arguments
{
    /// <summary>
    /// Arguments for clusterers (see ClustererBase).
    /// </summary>
    [Serializable]
    class ArgsClusterer : ArgsBase
    {
        public readonly PeakFilter PeakFilter;
        public readonly ObsFilter ObsFilter;
        public readonly ConfigurationMetric Distance;
        public readonly EAlgoSourceMode SourceMode;
        public readonly bool SplitGroups;
        public readonly EClustererStatistics Statistics;

        public ArgsClusterer(PeakFilter sigFilter, ConfigurationMetric distance, EAlgoSourceMode src, ObsFilter atypes, bool splitGroups, EClustererStatistics suppressMetric, object[] parameters)
            : base(parameters)
        {
            this.PeakFilter = sigFilter;
            this.Distance = distance;
            this.SourceMode = src;
            this.ObsFilter = atypes;
            this.SplitGroups = splitGroups;
            this.Statistics = suppressMetric;
        }

        public override string ToString(AlgoBase algorithm)
        {
            StringBuilder sb = new StringBuilder();

            if (Parameters != null)
            {
                sb.Append(AlgoParameters.ParamsToHumanReadableString(Parameters, algorithm));
            }

            sb.Append("; x = " + SourceMode.ToUiString());

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
