using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Data.DataInfo;
using MetaboliteLevels.Data.General;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Settings;
using MetaboliteLevels.Algorithms;

namespace MetaboliteLevels.Data
{
    /// <summary>
    /// A peak with extra information for plotting graphs.
    /// </summary>
    internal sealed class StylisedPeak
    {
        /// <summary>
        /// Peak to plot.
        /// </summary>
        public readonly Peak Peak;

        /// <summary>
        /// Plot in preview mode (suitable for small display)
        /// </summary>
        public bool IsPreview;

        /// <summary>
        /// The observations to plot (otherwise the default will be plotted - Peak.Observations or Peak.AltObservations)
        /// </summary>
        public Vector ForceObservations;

        /// <summary>
        /// Batch trend line (only if OverrideDefaultOptions.ShowAcqisition == true)
        /// </summary>
        public double[] ForceTrend;

        /// <summary>
        /// Any array of ConditionInfo[] or ObservationInfo[] determining the order of the trend
        /// </summary>
        public IEnumerable ForceTrendOrder;

        /// <summary>
        /// Override the user's choices on visualisations (if not null).
        /// </summary>
        public StylisedPeakOptions OverrideDefaultOptions;

        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public StylisedPeak(Peak peak)
        {
            Peak = peak;
        }

        /// <summary>
        /// OVERRIDES Object
        /// </summary>
        public override string ToString()
        {
            return Peak?.ToString();
        }
    }

    /// <summary>
    /// Options for plotting a peak.
    /// </summary>
    internal sealed class StylisedPeakOptions
    {
        public IEnumerable<GroupInfo> ViewTypes;            // See CoreVisualOptions
        public IEnumerable<BatchInfo> ViewBatches;          // Like ViewTypes for batches (only if ShowAcqisition == true)
        public bool ConditionsSideBySide;                   // See CoreVisualOptions
        public bool ShowRanges;                             // See CoreVisualOptions
        public bool ShowMinMax;                             // See CoreVisualOptions
        public bool ShowPoints;                             // See CoreVisualOptions
        public bool ShowTrend;                              // See CoreVisualOptions
        public bool ShowAcqisition;                         // See CoreVisualOptions
        public bool ShowVariableMean;                       // See CoreVisualOptions
        public bool DrawExperimentalGroupAxisLabels;

        public StylisedPeakOptions(Core core)
        {
            CoreOptions visualOptions = core.Options;

            ViewAlternativeObservations = visualOptions.ViewAlternativeObservations;
            ShowRanges = visualOptions.ShowVariableRanges;
            ShowMinMax = visualOptions.ShowVariableMinMax;
            ViewTypes = visualOptions.ViewTypes;
            ViewBatches = core.Batches;
            ConditionsSideBySide = visualOptions.ConditionsSideBySide;
            ShowPoints = visualOptions.ShowPoints;
            ShowTrend = visualOptions.ShowTrend;
            ShowAcqisition = visualOptions.ViewAcquisition;
            ShowVariableMean = visualOptions.ShowVariableMean;
            DrawExperimentalGroupAxisLabels = visualOptions.DrawExperimentalGroupAxisLabels;
        }

        public StylisedPeakOptions(StylisedPeakOptions copyFrom)
        {
            ViewAlternativeObservations = copyFrom.ViewAlternativeObservations;
            ShowRanges = copyFrom.ShowRanges;
            ShowRanges = copyFrom.ShowRanges;
            ViewTypes = copyFrom.ViewTypes;
            ViewBatches = copyFrom.ViewBatches;
            ConditionsSideBySide = copyFrom.ConditionsSideBySide;
            ShowPoints = copyFrom.ShowPoints;
            ShowTrend = copyFrom.ShowTrend;
            ShowAcqisition = copyFrom.ShowAcqisition;
            DrawExperimentalGroupAxisLabels = copyFrom.DrawExperimentalGroupAxisLabels;
        }
    }
}
