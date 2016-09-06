using System.Collections.Generic;
using System.Linq;
using MetaboliteLevels.Algorithms.Statistics.Arguments;
using MetaboliteLevels.Data.DataInfo;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Settings;
using MGui.Helpers;
using MGui.Datatypes;
using MetaboliteLevels.Data.Session.Associational;

namespace MetaboliteLevels.Algorithms.Statistics.Inputs
{
    /// <summary>
    /// Inputs required by the StatisticBase calculation methods.
    /// 
    /// Some of the other algorithms just take their parameters directly so they don't have this class.
    /// </summary>
    class InputStatistic
    {
        public readonly Core Core; // core
        public readonly Peak PeakA; // first peak
        public readonly Peak PeakB; // second peak (null if not used, e.g. for things like the mean)
        public readonly ArgsStatistic Args; // configuration

        public InputStatistic(Core core, Peak a, Peak b, ArgsStatistic args)
        {
            this.Core = core;
            this.PeakA = a;
            this.PeakB = b;
            this.Args = args;
        }

        public class GetDataInfo
        {
            public double[] Primary;        // y (i.e. intensity or time)
            public double[] Intensity;      // intensity
            public GroupInfo[] Group;       // group
            public int[] Time;              // time
            public int[] Rep;               // rep
        }

        public GetDataInfo GetData(EAlgoInput v, bool getPrimary, bool getIntensity, bool getGroup, bool getTime, bool getRep)
        {
            // Get the peak
            // Get the constraint
            // We always use "constraint A" for anything other than "SamePeak" since different constraints on the second peak/time can only be out of error presently
            Peak peak;
            ObsFilter con;

            if (v == EAlgoInput.A)
            {
                peak = PeakA;
                con = Args.VectorAConstraint;
            }
            else
            {
                switch (Args.VectorBSource)
                {
                    case EAlgoInputBSource.AltPeak:
                        peak = Args.VectorBPeak;
                        con = Args.VectorAConstraint; // to avoid error - we never expect conditions for A and B to be different
                        break;

                    case EAlgoInputBSource.SamePeak:
                        peak = PeakA;
                        con = Args.VectorBConstraint;
                        break;

                    case EAlgoInputBSource.Time:
                        peak = PeakA;
                        con = Args.VectorAConstraint; // to avoid error
                        break;

                    case EAlgoInputBSource.DmPeak:
                        peak = PeakB;
                        con = Args.VectorAConstraint; // to avoid error
                        break;

                    case EAlgoInputBSource.None:
                    default:
                        throw new SwitchException(Args.VectorBSource);
                }
            }

            GetDataInfo r = new GetDataInfo();

            bool primaryIsTime = (v == EAlgoInput.B && Args.VectorBSource == EAlgoInputBSource.Time);

            if (getPrimary)
            {
                getIntensity |= !primaryIsTime;
                getTime |= primaryIsTime;
            }

            switch (Args.SourceMode)
            {
                case EAlgoSourceMode.Trend:
                    {
                        IEnumerable<int> indices = con != null ? Core.Conditions.Which(λ => con.Test(λ)) : Core.Conditions.Indices();
                        IEnumerable<double> srcV = peak.Get_Observations_Trend( Core ).At(indices);
                        IEnumerable<ConditionInfo> srcC = Core.Conditions.At(indices);

                        if (getIntensity)
                        {
                            r.Intensity = srcV.ToArray();
                        }

                        if (getTime)
                        {
                            r.Time = srcC.Select(z => z.Time).ToArray();
                        }

                        if (getGroup)
                        {
                            r.Group = srcC.Select(z => z.Group).ToArray();
                        }

                        if (getRep)
                        {
                            r.Rep = srcC.Select(z => -1).ToArray();
                        }
                    }
                    break;

                case EAlgoSourceMode.Full:
                default:
                    {
                        IEnumerable<int> indices = con != null ? Core.Observations.Which(λ => con.Test(λ)) : Core.Conditions.Indices();
                        IEnumerable<double> srcV = peak.Get_Observations_Raw(Core).At(indices);
                        IEnumerable<ObservationInfo> srcC = Core.Observations.At( indices);

                        if (getIntensity)
                        {
                            r.Intensity = srcV.ToArray();
                        }

                        if (getTime)
                        {
                            r.Time = srcC.Select(z => z.Time).ToArray();
                        }

                        if (getGroup)
                        {
                            r.Group = srcC.Select(z => z.Group).ToArray();
                        }

                        if (getTime)
                        {
                            r.Rep = srcC.Select(z => z.Rep).ToArray();
                        }
                    }
                    break;
            }

            if (getPrimary)
            {
                r.Primary = primaryIsTime ? r.Time.Select(z => (double)z).ToArray() : r.Intensity;
            }

            return r;
        }
    }
}
