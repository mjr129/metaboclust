using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Algorithms.Definitions.Trends;
using MetaboliteLevels.Data.Algorithms.General;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Session.Main;

namespace MetaboliteLevels.Gui.Datatypes
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
        public Vector ForceTrend;        

        /// <summary>
        /// Override the user's choices on visualisations (if not null).
        /// </summary>
        public StylisedPeakOptions OverrideDefaultOptions;

        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public StylisedPeak(Peak peak)
        {
            this.Peak = peak;
        }

        /// <summary>
        /// OVERRIDES Object
        /// </summary>
        public override string ToString()
        {
            return this.Peak?.ToString();
        }
    }

    /// <summary>
    /// Options for plotting a peak.
    /// </summary>
    internal sealed class StylisedPeakOptions
    {
        public IEnumerable<GroupInfo> ViewGroups;            // See CoreVisualOptions
        public IEnumerable<BatchInfo> ViewBatches;          // Like ViewTypes for batches (only if ShowAcqisition == true)
        public bool ConditionsSideBySide;                   // See CoreVisualOptions
        public bool ShowRanges;                             // See CoreVisualOptions
        public bool ShowMinMax;                             // See CoreVisualOptions
        public bool ShowPoints;                             // See CoreVisualOptions
        public bool ShowTrend;                              // See CoreVisualOptions
        public bool ShowAcqisition;                         // See CoreVisualOptions
        public bool ShowVariableMean;                       // See CoreVisualOptions
        public bool DrawExperimentalGroupAxisLabels;
        public ConfigurationTrend SelectedTrend;

        public StylisedPeakOptions(Core core)
        {
            CoreOptions visualOptions = core.Options;
                                                                                    
            this.ShowRanges = visualOptions.ShowVariableRanges;
            this.ShowMinMax = visualOptions.ShowVariableMinMax;
            this.ViewGroups = visualOptions.ViewTypes;
            this.ViewBatches = core.Batches;
            this.ConditionsSideBySide = visualOptions.ConditionsSideBySide;
            this.ShowPoints = visualOptions.ShowPoints;
            this.ShowTrend = visualOptions.ShowTrend;
            this.ShowAcqisition = visualOptions.ViewAcquisition;
            this.ShowVariableMean = visualOptions.ShowVariableMean;
            this.DrawExperimentalGroupAxisLabels = visualOptions.DrawExperimentalGroupAxisLabels;
            this.SelectedTrend = visualOptions.SelectedTrend;
        }

        public StylisedPeakOptions Clone()
        {
            return (StylisedPeakOptions)this.MemberwiseClone();
        }
    }
}
