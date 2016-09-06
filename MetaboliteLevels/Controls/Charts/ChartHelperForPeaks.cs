using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using MCharting;
using MetaboliteLevels.Data;
using MetaboliteLevels.Data.DataInfo;
using MetaboliteLevels.Data.General;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Forms;
using MetaboliteLevels.Types.UI;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Viewers.Charts
{
    class ChartHelperForPeaks : ChartHelper
    {
        public Peak SelectedPeak
        {
            get;
            private set;
        }

        public override IVisualisable CurrentPlot
        {
            get
            {
                return SelectedPeak;
            }
        }

        public ChartHelperForPeaks(ISelectionHolder selector, Core core, Control targetSite)
            : base(selector, core, targetSite, false)
        {
        }

        public Bitmap CreateBitmap(int width, int height, StylisedPeak peak)
        {
            Plot(peak);
            return CreateBitmap(width, height);
        }

        public void Plot(StylisedPeak stylisedPeak)
        {
            Debug.WriteLine("PeakPlot: " + stylisedPeak);
            Dictionary<string, MCharting.Series> seriesNames = new Dictionary<string, MCharting.Series>();
            Peak peak = stylisedPeak?.Peak;

            // Clear plot
            MCharting.Plot plot = PrepareNewPlot(stylisedPeak != null && !stylisedPeak.IsPreview, peak);

            // Get selection   
            SelectedPeak = peak;
            SetCaption("Plot of {0}.", peak);

            if (peak == null)
            {
                CompleteNewPlot(plot);
                return;
            }

            // Get options
            StylisedPeakOptions opts = stylisedPeak.OverrideDefaultOptions ?? new StylisedPeakOptions(_core);

            // Get order data
            ConditionInfo[] condOrder = _core.Conditions.ToArray();
            ObservationInfo[] obsOrder = _core.Observations.ToArray();

            // Group legends
            IEnumerable<GroupInfoBase> order = opts.ShowAcqisition ? (IEnumerable<GroupInfoBase>)opts.ViewBatches : (IEnumerable<GroupInfoBase>)opts.ViewTypes;
            Dictionary<GroupInfoBase, MCharting.Series> groupLegends = DrawLegend(plot, order);

            // Get observations
            PeakValueSet observations;

            if (stylisedPeak.ForceObservations != null)
            {
                observations = stylisedPeak.ForceObservations;
            }
            else if (opts.ViewAlternativeObservations)
            {
                observations = LegacyHelper.Get_AltObservations( peak, _core );
            }
            else
            {
                observations = LegacyHelper.Get_Observations( peak, _core );
            }

            if (observations == null)
            {
                CompleteNewPlot(plot);
                return;
            }

            // COPY the observations (since we will reorder them)
            double[] raw = observations.Raw.ToArray();

            // Show acquisition and batches?
            if (opts.ShowAcqisition)
            {
                // --- RAW DATA (points) ---
                MCharting.Series legendEntry = new MCharting.Series();
                legendEntry.Name = "Observations";
                legendEntry.Style.DrawPoints = new SolidBrush(Color.Black);
                plot.LegendEntries.Add(legendEntry);

                ArrayHelper.Sort(obsOrder, raw, ObservationInfo.BatchAcquisitionDisplayOrder);
                AddToPlot(plot, peak, seriesNames, raw, "Raw data", obsOrder, opts, EPlot.ByBatch, groupLegends, legendEntry);

                // --- TREND (thick line) ---
                if (stylisedPeak.ForceTrend != null)
                {
                    MCharting.Series legendEntry2 = new MCharting.Series();
                    legendEntry2.Name = "Trend";
                    legendEntry2.Style.DrawLines = new Pen(Color.Black, _core.Options.LineWidth);
                    legendEntry2.Style.DrawLines.Width = 4;
                    plot.LegendEntries.Add(legendEntry2);

                    raw = stylisedPeak.ForceTrend.ToArray();
                    obsOrder = ((IEnumerable<ObservationInfo>)stylisedPeak.ForceTrendOrder).ToArray();
                    ArrayHelper.Sort(obsOrder, raw, ObservationInfo.BatchAcquisitionDisplayOrder);

                    AddToPlot(plot, peak, seriesNames, raw, "Trend data", obsOrder, opts, EPlot.ByBatch | EPlot.DrawLine | EPlot.DrawBold, groupLegends, legendEntry2);
                }

                DrawLabels(plot, opts.ConditionsSideBySide, order, opts.DrawExperimentalGroupAxisLabels);
                CompleteNewPlot(plot);
                return;
            }

            // Sort data
            Tuple<double, double, double>[] amm = ArrayHelper.Zip(observations.Trend, observations.Min, observations.Max).ToArray(); // TODO: this is awful sorting

            ArrayHelper.Sort(condOrder, amm, ConditionInfo.GroupTimeDisplayOrder);
            ArrayHelper.Sort(obsOrder, raw, ObservationInfo.GroupTimeDisplayOrder);

            double[] avg = amm.Select(z => z.Item1).ToArray();
            double[] min = amm.Select(z => z.Item2).ToArray();
            double[] max = amm.Select(z => z.Item3).ToArray();

            // --- PLOT MEAN & SD (lines across)
            if (opts.ShowVariableMean)
            {
                AddMeanAndSdLines(plot, opts, observations, peak, groupLegends);
            }

            // --- RANGE (shaded area) ---
            if (opts.ShowRanges)
            {
                AddUpperAndLowerShade(plot, condOrder, opts, seriesNames, peak, min, max, groupLegends);
            }

            // --- RAW DATA (points) ---
            if (opts.ShowPoints && !stylisedPeak.IsPreview)
            {
                MCharting.Series legendEntry = new MCharting.Series();
                legendEntry.Name = "Observations";
                legendEntry.Style.DrawPoints = new SolidBrush(Color.Black);
                plot.LegendEntries.Add(legendEntry);

                AddToPlot(plot, peak, seriesNames, raw, "Raw data", obsOrder, opts, EPlot.None, groupLegends, legendEntry);
            }

            // --- RANGE (lines) ---
            if (opts.ShowMinMax)
            {
                MCharting.Series legendEntry = new MCharting.Series();
                legendEntry.Name = "Range min/max";
                legendEntry.Style.DrawLines = new Pen(Color.Gray, _core.Options.LineWidth);
                plot.LegendEntries.Add(legendEntry);

                AddToPlot(plot, peak, seriesNames, min, "Min value", condOrder, opts, EPlot.DrawLine, groupLegends, legendEntry);
                AddToPlot(plot, peak, seriesNames, max, "Max value", condOrder, opts, EPlot.DrawLine, groupLegends, legendEntry);
            }

            // --- TREND (thick line) ---
            if (opts.ShowTrend)
            {
                MCharting.Series legendEntry = new MCharting.Series();
                legendEntry.Name = "Trend";
                legendEntry.Style.DrawLines = new Pen(Color.Black, _core.Options.LineWidth);
                legendEntry.Style.DrawLines.Width = _core.Options.LineWidth * 4;
                plot.LegendEntries.Add(legendEntry);

                if (stylisedPeak.ForceTrend != null)
                {
                    ConditionInfo[] forder = stylisedPeak.ForceTrendOrder.Cast<ConditionInfo>().ToArray();
                    double[] fdata = stylisedPeak.ForceTrend.ToArray();

                    ArrayHelper.Sort(forder, fdata, ConditionInfo.GroupTimeDisplayOrder);

                    AddToPlot(plot, peak, seriesNames, fdata, "Trend data", forder, opts, EPlot.DrawLine | EPlot.DrawBold, groupLegends, legendEntry);
                }
                else
                {
                    AddToPlot(plot, peak, seriesNames, avg, "Trend data", condOrder, opts, EPlot.DrawLine | EPlot.DrawBold, groupLegends, legendEntry);
                }
            }

            // --- LABELS ---
            DrawLabels(plot, opts.ConditionsSideBySide, order, opts.DrawExperimentalGroupAxisLabels);

            CompleteNewPlot(plot);
        }

        private void AddUpperAndLowerShade(
            MCharting.Plot plot,
            ConditionInfo[] condOrder,
            StylisedPeakOptions o, 
            Dictionary<string, MCharting.Series> seriesNames, 
            Peak peak,
            double[] min,
            double[] max,
            Dictionary<GroupInfoBase, MCharting.Series> groupLegends)
        {
            MCharting.Series legendEntry = new MCharting.Series();
            legendEntry.Name = "Range";
            legendEntry.Style.DrawVBands = new SolidBrush(Color.Gray);
            plot.LegendEntries.Add(legendEntry);

            // Iterate the conditions
            for (int i = 0; i < condOrder.Length; i++)
            {
                ConditionInfo cond = condOrder[i];

                if (o.ViewTypes.Contains(cond.Group))
                {
                    // Name the series
                    string name = "Range for " + cond.Group.DisplayName;
                    MCharting.Series series;

                    // Create the series (if required)
                    if (!seriesNames.ContainsKey(name))
                    {
                        series = plot.Series.Add(name);
                        Color c = cond.Group.ColourLight;
                        c = Color.FromArgb(0x80, c.R, c.G, c.B);
                        series.Tag = peak;
                        series.Style.DrawVBands =  cond.Group.CreateBrush( c );
                        series.ApplicableLegends.Add(groupLegends[cond.Group]);
                        series.ApplicableLegends.Add(legendEntry);
                        seriesNames.Add(name, series);
                    }
                    else
                    {
                        series = seriesNames[name];
                    }

                    // Get the X coordinate
                    int typeOffset = GetTypeOffset(o.ViewTypes, cond.Group);

                    double xVal = cond.Time;

                    if (o.ConditionsSideBySide)
                    {
                        xVal += typeOffset;
                    }

                    // Get the Y coordinates
                    double yMin = min[i];
                    double yMax = max[i];

                    if (double.IsNaN(yMin) || double.IsInfinity(yMin))
                    {
                        yMin = 0;
                    }

                    if (double.IsNaN(yMax) || double.IsInfinity(yMax))
                    {
                        yMax = 0;
                    }

                    // Create the point
                    IntensityInfo info1 = new IntensityInfo(cond.Time, null, cond.Group, yMin);
                    IntensityInfo info2 = new IntensityInfo(cond.Time, null, cond.Group, yMax);
                    MCharting.DataPoint cdp = new MCharting.DataPoint(xVal, new[] { yMin, yMax });
                    cdp.Tag = new[] { info1, info2 };
                    series.Points.Add(cdp);
                }
            }
        }

        private void AddMeanAndSdLines(
            MCharting.Plot plot,
            StylisedPeakOptions o,
            PeakValueSet observations, 
            Peak peak, 
            Dictionary<GroupInfoBase, MCharting.Series> groupLegends)
        {
            MCharting.Series legendEntry = new MCharting.Series();
            legendEntry.Name = "Std. Dev. Min/Max";
            legendEntry.Style.DrawLines = new Pen(Color.Gray, _core.Options.LineWidth);
            legendEntry.Style.DrawLines.DashStyle = DashStyle.Dot;
            plot.LegendEntries.Add(legendEntry);

            MCharting.Series legendEntry2 = new MCharting.Series();
            legendEntry2.Name = "Mean";
            legendEntry2.Style.DrawLines = new Pen(Color.Black, _core.Options.LineWidth);
            plot.LegendEntries.Add(legendEntry2);

            // Iterate the types
            foreach (GroupInfo group in o.ViewTypes)
            {
                // Get the Y values
                double sd = observations.StdDev[@group.Order];
                double yMean = observations.Mean[@group.Order];
                double yMin = yMean - sd / 2;
                double yMax = yMean + sd / 2;

                // Get the X values
                int xLeft = @group.Range.Min;
                int xRight = @group.Range.Max;

                if (o.ConditionsSideBySide)
                {
                    int typeOffset = GetTypeOffset(o.ViewTypes, @group);
                    xLeft += typeOffset;
                    xRight += typeOffset;
                }

                // Create the series
                MCharting.Series sMean = plot.Series.Add(@group.DisplayName + ": Mean");
                MCharting.Series sMin = plot.Series.Add(@group.DisplayName + ": StdDevMin");
                MCharting.Series sMax = plot.Series.Add(@group.DisplayName + ": StdDevMax");

                sMean.ApplicableLegends.Add(groupLegends[group]);
                sMin.ApplicableLegends.Add(groupLegends[group]);
                sMax.ApplicableLegends.Add(groupLegends[group]);
                sMean.ApplicableLegends.Add(legendEntry2);
                sMin.ApplicableLegends.Add(legendEntry);
                sMax.ApplicableLegends.Add(legendEntry);

                sMean.Tag = peak;
                sMin.Tag = peak;
                sMax.Tag = peak;

                Color c = @group.ColourLight;
                sMean.Style.DrawLines = new Pen(c, _core.Options.LineWidth);
                sMin.Style.DrawLines = new Pen(c, _core.Options.LineWidth);
                sMax.Style.DrawLines = new Pen(c, _core.Options.LineWidth);

                sMin.Style.DrawLines.DashStyle = DashStyle.Dot;
                sMax.Style.DrawLines.DashStyle = DashStyle.Dot;

                // Add the points
                AddDataPoint(sMean, xLeft, yMean, @group);
                AddDataPoint(sMean, xRight, yMean, @group);

                AddDataPoint(sMin, xLeft, yMin, @group);
                AddDataPoint(sMin, xRight, yMin, @group);

                AddDataPoint(sMax, xLeft, yMax, @group);
                AddDataPoint(sMax, xRight, yMax, @group);
            }
        }

        /// <summary>
        /// Adds a tagged datapoint to a series.
        /// </summary>
        private static void AddDataPoint( MCharting.Series series, int x, double y, GroupInfo type)
        {
            MCharting.DataPoint dp = new MCharting.DataPoint(x, y);
            dp.Tag = new IntensityInfo(null, null, type, y);

            if (double.IsNaN(y) || double.IsInfinity(y))
            {
                y = 0;
            }

            series.Points.Add(dp);
        }

        [Flags]
        private enum EPlot
        {
            None = 0,
            DrawLine = 1,
            DrawBold = 2,
            ByBatch = 8,
        }

        /// <summary>
        /// Plots a line of conditions on the graph
        /// </summary>
        /// <param name="peak">Peak to plot</param>
        /// <param name="seriesNames">Names of existing series</param>
        /// <param name="intensities">Intensity values (y)</param>
        /// <param name="seriesName">Name of this series</param>
        /// <param name="xInfo">Info for x position of intentises (in same order as intensities)</param>
        /// <param name="line">Line or dots/</param>
        /// <param name="bold">Bold line?</param>
        /// <param name="isConditions">Order by conditions or obervations</param>
        private void AddToPlot( 
            MCharting.Plot plot, 
            Peak peak, 
            Dictionary<string, MCharting.Series> seriesNames,
            double[] intensities,
            string seriesName, 
            IEnumerable xInfo,
            StylisedPeakOptions o,
            EPlot draw,
            Dictionary<GroupInfoBase, MCharting.Series> groupLegends,
            MCharting.Series legend)
        {
            bool byCondition = xInfo.FirstOrDefault2() is ConditionInfo;
            int i = -1;

            // Iterate whatever it is we're iterating
            foreach (object ord in xInfo)
            {
                i++;

                // Get the values
                GroupInfo condType;
                int condDay;
                int? condRep;
                BatchInfo condBatch;
                int condAcquisition;

                if (byCondition)
                {
                    ConditionInfo cond = (ConditionInfo)ord;
                    condType = cond.Group;
                    condDay = cond.Time;
                    condRep = null;
                    condBatch = null;
                    condAcquisition = -1;
                }
                else
                {
                    ObservationInfo obs = (ObservationInfo)ord;
                    condType = obs.Group;
                    condDay = obs.Time;
                    condRep = obs.Rep;
                    condBatch = obs.Batch;
                    condAcquisition = obs.Acquisition;
                }

                // Name the series
                if (draw.HasFlag(EPlot.ByBatch))
                {
                    if (!o.ViewBatches.Contains(condBatch))
                    {
                        continue;
                    }
                }
                else
                {
                    if (!o.ViewTypes.Contains(condType))
                    {
                        continue;
                    }
                }

                bool colorByBatch = draw.HasFlag(EPlot.ByBatch);
                GroupInfoBase seriesUsing = colorByBatch ? (GroupInfoBase)condBatch : (GroupInfoBase)condType;

                string name = seriesName + " for " + seriesUsing.DisplayName;
                MCharting.Series series;

                // Create the series (if required)
                if (!seriesNames.ContainsKey(name))
                {
                    series = plot.Series.Add(name);
                    series.ApplicableLegends.Add(groupLegends[seriesUsing]);
                    series.ApplicableLegends.Add(legend);
                    seriesNames.Add(name, series);
                    series.Tag = peak;

                    Color colour = (draw.HasFlag(EPlot.DrawBold) || draw.HasFlag(EPlot.ByBatch) || !o.ShowTrend ) ? seriesUsing.Colour : seriesUsing.ColourLight;

                    if (draw.HasFlag(EPlot.DrawLine))
                    {
                        series.Style.DrawLines = new Pen(colour);
                        series.Style.DrawLines.Width = draw.HasFlag(EPlot.DrawBold) ? _core.Options.LineWidth * 4 : _core.Options.LineWidth;
                    }
                    else
                    {                                   
                        UiControls.CreateIcon( series, seriesUsing );
                    }
                }
                else
                {
                    series = seriesNames[name];
                }

                // Get the X position
                double xPos;

                if (draw.HasFlag(EPlot.ByBatch))
                {
                    xPos = condAcquisition;

                    if (o.ConditionsSideBySide)
                    {
                        xPos += GetBatchOffset(o.ViewBatches, condBatch);
                    }
                }
                else
                {
                    xPos = condDay;

                    if (o.ConditionsSideBySide)
                    {
                        xPos += GetTypeOffset(o.ViewTypes, condType);
                    }
                }

                // Get the Y position
                double yPos = intensities[i];

                if (double.IsNaN(yPos) || double.IsInfinity(yPos))
                {
                    yPos = 0;
                }

                // Create the point
                MCharting.DataPoint cdp = new MCharting.DataPoint(xPos, yPos);
                IntensityInfo tag = new IntensityInfo(condDay, condRep, condType, yPos);
                cdp.Tag = tag;
                series.Points.Add(cdp);
            }
        }
    } // class
} // namespace
