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
        public readonly EAlgoSourceMode SourceMode;                 // Sort of information to get
        public readonly EAlgoInputBSource VectorBSource;            // Where to get vector B
        public readonly ObsFilter VectorAConstraint;                // Contents (types) of VectorA
        public readonly ObsFilter VectorBConstraint;                // Contents (types) of VectorB (only used if VectorBSource is Peak, otherwise this is the same as ConstraintA)
        public readonly Peak VectorBPeak;                           // Peak to compare to

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
                r.Append(AlgoParameters.ParamsToHumanReadableString(Parameters, algorithm));
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
                        r.Append("peak " + VectorBPeak.DisplayIcon);
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
