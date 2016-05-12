using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Data.DataInfo;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Data.General
{
    /// <summary>
    /// Set of observations for a specific peak.
    /// </summary>
    [Serializable]
    class PeakValueSet
    {
        /// <summary>
        /// Raw data [index ≘ observation].
        /// </summary>
        public readonly double[] Raw;

        /// <summary>
        /// Averaged data [index ≘ condition].
        /// </summary>
        public double[] Trend;

        /// <summary>
        /// Minimum data [index ≘ condition].
        /// </summary>
        public readonly double[] Min;

        /// <summary>
        /// Maximum data [index ≘ condition].
        /// </summary>
        public readonly double[] Max;

        /// <summary>
        /// Mean [index ≘ group]
        /// </summary>
        public readonly double[] Mean;

        /// <summary>
        /// Standard deviation [index ≘ group]
        /// </summary>
        public readonly double[] StdDev;

        /// <summary>
        /// Debugging
        /// </summary>
        public override string ToString()
        {
            return "PeakValueSet: " + Raw.Length + " observations, " + Trend.Length + " conditions, " + Mean.Length + " types.";
        }

        /// <summary>
        /// Recalculates values and returns new result
        /// </summary>
        public PeakValueSet(Core core, double[] raw, ConfigurationTrend avgSmoother)
            : this(core.Observations, core.Conditions, core.Groups, raw, avgSmoother, core.MinSmoother, core.MaxSmoother)
        {
            // NA
        }   

        /// <summary>
        /// Recalculates values and returns new result
        /// </summary>
        public PeakValueSet(Core core, double[] raw)
            : this(core.Observations, core.Conditions, core.Groups, raw, core.AvgSmoother, core.MinSmoother, core.MaxSmoother)
        {
            // NA
        }

        /// <summary>
        /// Calculates values
        /// </summary>
        public PeakValueSet(IReadOnlyList<ObservationInfo> obsInfo, IReadOnlyList<ConditionInfo> condInfo, IReadOnlyList<GroupInfo> typeInfo, double[] raw, ConfigurationTrend avgSmoother, ConfigurationTrend minSmoother, ConfigurationTrend maxSmoother)
        {
            // Calculate { raw } for each OBSERVATION
            this.Raw = raw;

            this.Trend = avgSmoother.CreateTrend(obsInfo, condInfo, typeInfo, raw);
            this.Min = minSmoother.CreateTrend(obsInfo, condInfo, typeInfo, raw);
            this.Max = maxSmoother.CreateTrend(obsInfo, condInfo, typeInfo, raw);

            // Calculate { mean, sd } for each TYPE
            this.Mean = new double[typeInfo.Count];
            this.StdDev = new double[typeInfo.Count];

            foreach (GroupInfo type in typeInfo)
            {
                IEnumerable<int> indicesForType = obsInfo.Which(λ => λ.Group == type);
                IEnumerable<double> valuesForType = raw.At( indicesForType);

                this.Mean[type.Order] = Maths.Mean(valuesForType);
                this.StdDev[type.Order] = Maths.StdDev(valuesForType, this.Mean[type.Order]);
            }
        }

        /// <summary>
        /// Extracts the values in this.Avg commensurate with the [conditionsOfInterest]. 
        /// The order of values [conditions] in Avg is required. 
        /// </summary>
        public IEnumerable<double> ExtractValues(IEnumerable<ConditionInfo> conditions, IEnumerable<GroupInfo> conditionsOfInterest)
        {
            IEnumerable<int> cindexes = conditions.Which(λ => conditionsOfInterest.Contains(λ.Group));
            IEnumerable<double> cvalues = this.Trend.At( cindexes);
            return cvalues;
        }
    }
}
