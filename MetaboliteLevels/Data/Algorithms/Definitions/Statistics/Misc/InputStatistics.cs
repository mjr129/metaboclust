﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Algorithms.Definitions.Base.Misc;
using MetaboliteLevels.Data.Algorithms.General;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Session.Main;
using MGui.Datatypes;
using MGui.Helpers;

namespace MetaboliteLevels.Data.Algorithms.Definitions.Statistics.Misc
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
                peak = this.PeakA;
                con = this.Args.VectorAConstraint;
            }
            else
            {
                switch (this.Args.VectorBSource)
                {
                    case EAlgoInputBSource.AltPeak:
                        peak = this.Args.VectorBPeak;
                        con = this.Args.VectorAConstraint; // to avoid error - we never expect conditions for A and B to be different
                        break;

                    case EAlgoInputBSource.SamePeak:
                        peak = this.PeakA;
                        con = this.Args.VectorBConstraint;
                        break;

                    case EAlgoInputBSource.Time:
                        peak = this.PeakA;
                        con = this.Args.VectorAConstraint; // to avoid error
                        break;

                    case EAlgoInputBSource.DmPeak:
                        peak = this.PeakB;
                        con = this.Args.VectorAConstraint; // to avoid error
                        break;

                    case EAlgoInputBSource.None:
                    default:
                        throw new SwitchException(this.Args.VectorBSource);
                }
            }

            GetDataInfo r = new GetDataInfo();

            bool primaryIsTime = (v == EAlgoInput.B && this.Args.VectorBSource == EAlgoInputBSource.Time);

            if (getPrimary)
            {
                getIntensity |= !primaryIsTime;
                getTime |= primaryIsTime;
            }

            // TODO: This whole section is awful legacy stuff, why do we keep looking this stuff up!
            Vector srcV = this.Args.SourceMatrix.Find( peak );
            // null condition just means "everything"
            IEnumerable<int> indices = con == null ? srcV.Observations.Indices() : srcV.Observations.Which( λ => con.Test( (ObservationInfo) λ ) );
            IEnumerable<ObservationInfo> srcC = srcV.Observations.At( indices);

            if (getIntensity)
            {
                r.Intensity = srcV.Values.At( indices ).ToArray();
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

            if (getPrimary)
            {
                r.Primary = primaryIsTime ? r.Time.Select(z => (double)z).ToArray() : r.Intensity;
            }

            return r;
        }
    }
}
