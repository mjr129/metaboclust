using MetaboliteLevels.Data.Visualisables;
using System;
using System.Text;
using MetaboliteLevels.Data.Algorithms.Definitions.Configurations;
using MetaboliteLevels.Settings;
using MetaboliteLevels.Utilities;
using MGui.Helpers;
using MGui.Datatypes;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Viewers.Lists;
using MetaboliteLevels.Data.Session;
using System.Collections.Generic;
using MetaboliteLevels.Algorithms.Statistics.Statistics;

namespace MetaboliteLevels.Algorithms.Statistics.Arguments
{
    /// <summary>
    /// Arguments for statistics (see StatisticBase).
    /// </summary>
    [Serializable]
    internal class ArgsStatistic : ArgsBase
    {
        [XColumn]
        public readonly EAlgoInputBSource VectorBSource;            // Where the second input vector comes from
        [XColumn]
        public readonly ObsFilter VectorAConstraint;                // Filter on the first input vector
        [XColumn]
        public readonly ObsFilter VectorBConstraint;                // Filter on the second input vector (only used if [VectorBSource] is Peak)
        [XColumn]
        public readonly Peak VectorBPeak;                           // Which peak the second input vector comes from (only used if [VectorBSource] is AltPeak)

        public ArgsStatistic( string id, IMatrixProvider source, ObsFilter atypes, EAlgoInputBSource bsrc, ObsFilter btypes, Peak compareTo, object[] parameters)
            : base(id, source, parameters)
        {                                                           
            this.VectorAConstraint = atypes;
            this.VectorBConstraint = btypes;
            this.VectorBSource = bsrc;
            this.VectorBPeak = compareTo;
        }

        public override string DefaultDisplayName
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                sb.Append( base.DefaultDisplayName );

                if (VectorAConstraint != null)
                {
                    sb.Append( " for " );
                    sb.Append( VectorAConstraint.ToString() );
                }

                if (VectorBSource != EAlgoInputBSource.None)
                {
                    sb.Append( " against " );

                    switch (VectorBSource)
                    {
                        case EAlgoInputBSource.SamePeak:
                            if (VectorBConstraint != null)
                            {
                                sb.Append( VectorBConstraint.ToString() );
                            }
                            break;

                        case EAlgoInputBSource.AltPeak:
                            sb.Append( "peak " + VectorBPeak.DisplayName );
                            break;

                        case EAlgoInputBSource.Time:
                            sb.Append( VectorBSource.ToUiString() );
                            break;

                        default:
                            throw new SwitchException( VectorBSource );
                    }
                }

                return sb.ToString();
            }
        }              
    }
}
