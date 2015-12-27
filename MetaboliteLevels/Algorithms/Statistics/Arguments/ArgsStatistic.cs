using MetaboliteLevels.Data.Visualisables;
using System;
using System.Text;
using MetaboliteLevels.Settings;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Algorithms.Statistics.Arguments
{
    /// <summary>
    /// Arguments for statistics (see StatisticBase).
    /// </summary>
    [Serializable]
    class ArgsStatistic : ArgsBase
    {
        public readonly EAlgoSourceMode SourceMode;                 // Where the input vectors come from
        public readonly EAlgoInputBSource VectorBSource;            // Where the second input vector comes from
        public readonly ObsFilter VectorAConstraint;                // Filter on the first input vector
        public readonly ObsFilter VectorBConstraint;                // Filter on the second input vector (only used if [VectorBSource] is Peak)
        public readonly Peak VectorBPeak;                           // Which peak the second input vector comes from (only used if [VectorBSource] is AltPeak)

        public ArgsStatistic(EAlgoSourceMode src, ObsFilter atypes, EAlgoInputBSource bsrc, ObsFilter btypes, Peak compareTo, object[] parameters)
            : base(parameters)
        {
            this.SourceMode = src;
            this.VectorAConstraint = atypes;
            this.VectorBConstraint = btypes;
            this.VectorBSource = bsrc;
            this.VectorBPeak = compareTo;
        }

        public override string ToString(AlgoBase algorithm)
        {
            StringBuilder r = new StringBuilder();

            if (Parameters != null)
            {
                r.Append(AlgoParameterCollection.ParamsToHumanReadableString(Parameters, algorithm));
            }

            r.Append("; ");

            if (SourceMode != EAlgoSourceMode.Full)
            {
                r.Append(" of ");
                r.Append(SourceMode.ToUiString());
            }

            if (VectorAConstraint != null)
            {
                r.Append(" for ");
                r.Append(VectorAConstraint.ToString());
            }

            if (VectorBSource != EAlgoInputBSource.None)
            {
                r.Append(" against ");

                switch (VectorBSource)
                {
                    case EAlgoInputBSource.SamePeak:
                        if (VectorBConstraint != null)
                        {
                            r.Append(VectorBConstraint.ToString());
                        }
                        break;

                    case EAlgoInputBSource.AltPeak:
                        r.Append("peak " + VectorBPeak.REMOVE_THIS_FUNCTION);
                        break;

                    case EAlgoInputBSource.Time:
                        r.Append(VectorBSource.ToUiString());
                        break;

                    default:
                        throw new SwitchException(VectorBSource);
                }
            }

            return r.ToString();
        }
    }
}
