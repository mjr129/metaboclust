using MetaboliteLevels.Data.Visualisables;
using System;
using System.Text;
using MetaboliteLevels.Data.Algorithms.Definitions.Configurations;
using MetaboliteLevels.Settings;
using MetaboliteLevels.Utilities;
using MGui.Helpers;
using MGui.Datatypes;
using MetaboliteLevels.Data.Session.Associational;

namespace MetaboliteLevels.Algorithms.Statistics.Arguments
{
    /// <summary>
    /// Arguments for statistics (see StatisticBase).
    /// </summary>
    [Serializable]
    class ArgsStatistic : ArgsBase
    {                                                                                                              
        public readonly EAlgoInputBSource VectorBSource;            // Where the second input vector comes from
        public readonly ObsFilter VectorAConstraint;                // Filter on the first input vector
        public readonly ObsFilter VectorBConstraint;                // Filter on the second input vector (only used if [VectorBSource] is Peak)
        public readonly Peak VectorBPeak;                           // Which peak the second input vector comes from (only used if [VectorBSource] is AltPeak)

        public ArgsStatistic( string id, IProvider<IntensityMatrix> source, ObsFilter atypes, EAlgoInputBSource bsrc, ObsFilter btypes, Peak compareTo, object[] parameters)
            : base(id, source, parameters)
        {                                                           
            this.VectorAConstraint = atypes;
            this.VectorBConstraint = btypes;
            this.VectorBSource = bsrc;
            this.VectorBPeak = compareTo;
        }

        public override string ToString(AlgoBase algorithm)
        {
            StringBuilder sb = new StringBuilder();
                                              
            sb.Append(base.ToString());

            if (VectorAConstraint != null)
            {
                sb.Append(" for ");
                sb.Append(VectorAConstraint.ToString());
            }

            if (VectorBSource != EAlgoInputBSource.None)
            {
                sb.Append(" against ");

                switch (VectorBSource)
                {
                    case EAlgoInputBSource.SamePeak:
                        if (VectorBConstraint != null)
                        {
                            sb.Append(VectorBConstraint.ToString());
                        }
                        break;

                    case EAlgoInputBSource.AltPeak:
                        sb.Append("peak " + VectorBPeak.DisplayName);
                        break;

                    case EAlgoInputBSource.Time:
                        sb.Append(VectorBSource.ToUiString());
                        break;

                    default:
                        throw new SwitchException(VectorBSource);
                }
            }

            return sb.ToString();
        }
    }
}
