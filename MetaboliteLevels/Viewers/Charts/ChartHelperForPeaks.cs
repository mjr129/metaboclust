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
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Forms;
using MetaboliteLevels.Utilities;

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
            : base(selector, core, targetSite)
        {
        }

        public Bitmap CreateBitmap(int width, int height, StylisedPeak variable)
        {
            Plot(variable);
            return CreateBitmap(width, height);
        }

        public void Plot(StylisedPeak stylisedPeak)
        {
            Debug.WriteLine("PeakPlot: " + stylisedPeak);
            Dictionary<string, MChart.Series> seriesNames = new Dictionary<string, MChart.Series>();

            // Clear plot
            MChart.Plot plot = PrepareNewPlot(stylisedPeak != null && !stylisedPeak.IsPreview, stylisedPeak != null ? stylisedPeak.Peak : null);

            // Get selection
            Peak peak = stylisedPeak != null ? stylisedPeak.Peak : null;
            SelectedPeak = peak;
            SetCaption("Plot of {0}.", peak);

            if (peak == null)
            {
                CompleteNewPlot(plot);
                return;
            }

            // Get options
            var opts = stylisedPeak.OverrideDefaultOptions ?? new StylisedPeakOptions(_core);

            // Get order data
            var condOrder = _core.Conditions.ToArray();
            var obsOrder = _core.Observations.ToArray();

            // Get observations
            PeakValueSet observations;

            if (stylisedPeak.ForceObservations != null)
            {
                observations = stylisedPeak.ForceObservations;
            }
            else if (opts.ViewAlternativeObservations)
            {
                observations = peak.AltObservations;
            }
            else
            {
                observations = peak.Observations;
            }

            if (observations == null)
            {
                CompleteNewPlot(plot);
                return;
            }

            // COPY the observations (since we will reorder them)
            var raw = observations.Raw.ToArray();

            // Show acquisition abd batches?
            if (opts.ShowAcqisition)
            {
                // --- RAW DATA (points) ---
                ArrayHelper.Sort(obsOrder, raw, ObservationInfo.BatchAcquisitionOrder);
                AddToPlot(plot, peak, seriesNames, raw, "Raw data", obsOrder, opts, EPlot.ByBatch);

                // --- TREND (thick line) ---
                if (stylisedPeak.ForceTrend != null)
                {
                    raw = stylisedPeak.ForceTrend.ToArray();
                    obsOrder = ((IEnumerable<ObservationInfo>)stylisedPeak.ForceTrendOrder).ToArray();
                    ArrayHelper.Sort(obsOrder, raw, ObservationInfo.BatchAcquisitionOrder);

                    AddToPlot(plot, peak, seriesNames, raw, "Trend data", obsOrder, opts, EPlot.ByBatch | EPlot.DrawLine | EPlot.DrawBold);
                }

                DrawLabels(plot, true, opts.ViewBatches);
                CompleteNewPlot(plot);
                return;
            }

            // Sort data
            var amm = ArrayHelper.Zip(observations.Trend, observations.Min, observations.Max).ToArray(); // TODO: this is awful sorting

            ArrayHelper.Sort(condOrder, amm, ConditionInfo.GroupTimeOrder);
            ArrayHelper.Sort(obsOrder, raw, ObservationInfo.GroupTimeOrder);

            double[] avg = amm.Select(z => z.Item1).ToArray();
            double[] min = amm.Select(z => z.Item2).ToArray();
            double[] max = amm.Select(z => z.Item3).ToArray();

            // --- PLOT MEAN & SD (lines across)
            if (opts.ShowVariableMean && !stylisedPeak.IsPreview)
            {
                AddMeanAndSdLines(plot, opts, observations, peak);
            }

            // --- RANGE (shaded area) ---
            if (opts.ShowRanges)
            {
                AddUpperAndLowerShade(plot, condOrder, opts, seriesNames, peak, min, max);
            }

            // --- RAW DATA (points) ---
            if (opts.ShowPoints && !stylisedPeak.IsPreview)
            {
                AddToPlot(plot, peak, seriesNames, raw, "Raw data", obsOrder, opts, EPlot.None);
            }

            // --- RANGE (lines) ---
            if (opts.ShowRanges && !stylisedPeak.IsPreview)
            {
                AddToPlot(plot, peak, seriesNames, min, "Min value", condOrder, opts, EPlot.DrawLine);
                AddToPlot(plot, peak, seriesNames, max, "Max value", condOrder, opts, EPlot.DrawLine);
            }

            // --- TREND (thick line) ---
            if (opts.ShowTrend && !stylisedPeak.IsPreview)
            {
                if (stylisedPeak.ForceTrend != null)
                {
                    ConditionInfo[] forder = stylisedPeak.ForceTrendOrder.Cast<ConditionInfo>().ToArray();
                    double[] fdata = stylisedPeak.ForceTrend.ToArray();

                    ArrayHelper.Sort(forder, fdata, ConditionInfo.GroupTimeOrder);

                    AddToPlot(plot, peak, seriesNames, fdata, "Trend data", forder, opts, EPlot.DrawLine | EPlot.DrawBold);
                }
                else
                {
                    AddToPlot(plot, peak, seriesNames, avg, "Trend data", condOrder, opts, EPlot.DrawLine | EPlot.DrawBold);
                }
            }

            // --- LABELS ---
            DrawLabels(plot, opts.ConditionsSideBySide, opts.ViewTypes);

            CompleteNewPlot(plot);
        }

        private void AddUpperAndLowerShade(MChart.Plot plot, ConditionInfo[] condOrder, StylisedPeakOptions o, Dictionary<string, MChart.Series> seriesNames, Peak peak, double[] min, double[] max)
        {
            // Iterate the conditions
            for (int i = 0; i < condOrder.Length; i++)
            {
                ConditionInfo cond = condOrder[i];

                if (o.ViewTypes.Contains(cond.Group))
                {
                    // Name the series
                    string name = "Range for " + cond.Group.Name;
                    MChart.Series series;

                    // Create the series (if required)
                    if (!seriesNames.ContainsKey(name))
                    {
                        series = plot.Series.Add(name);
                        Color c = cond.Group.ColourLight;
                        c = Color.FromArgb(0x80, c.R, c.G, c.B);
                        series.Tag = peak;
                        series.Style.DrawBands = new SolidBrush(c);
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
                    MChart.DataPoint cdp = new MChart.DataPoint(xVal, new[] { yMin, yMax });
                    cdp.Tag = new[] { info1, info2 };
                    series.Points.Add(cdp);
                }
            }
        }

        private void AddMeanAndSdLines(MChart.Plot plot, StylisedPeakOptions o, PeakValueSet observations, Peak peak)
        {
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
                var sMean = plot.Series.Add(@group.Name + ": Mean");
                var sMin = plot.Series.Add(@group.Name + ": StdDevMin");
                var sMax = plot.Series.Add(@group.Name + ": StdDevMax");

                sMean.Tag = peak;
                sMin.Tag = peak;
                sMax.Tag = peak;

                Color c = @group.ColourLight;
                sMean.Style.DrawLines = new Pen(c);
                sMin.Style.DrawLines = new Pen(c);
                sMax.Style.DrawLines = new Pen(c);

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
        private static void AddDataPoint(MChart.Series series, int x, double y, GroupInfo type)
        {
            MChart.DataPoint dp = new MChart.DataPoint(x, y);
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
        private void AddToPlot(MChart.Plot plot, Peak peak, Dictionary<string, MChart.Series> seriesNames, double[] intensities, string seriesName, IEnumerable xInfo, StylisedPeakOptions o, EPlot draw)
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
                    var cond = (ConditionInfo)ord;
                    condType = cond.Group;
                    condDay = cond.Time;
                    condRep = null;
                    condBatch = null;
                    condAcquisition = -1;
                }
                else
                {
                    var obs = (ObservationInfo)ord;
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

                bool colorByBatch = draw.HasFlag(EPlot.ByBatch) && draw.HasFlag(EPlot.DrawLine);
                GroupInfoBase seriesUsing = colorByBatch ? (GroupInfoBase)condBatch : (GroupInfoBase)condType;

                string name = seriesName + " for " + seriesUsing.Name;
                MChart.Series series;

                // Create the series (if required)
                if (!seriesNames.ContainsKey(name))
                {
                    series = plot.Series.Add(name);
                    seriesNames.Add(name, series);
                    series.Tag = peak;

                    Color colour = (draw.HasFlag(EPlot.DrawBold) | draw.HasFlag(EPlot.ByBatch)) ? seriesUsing.Colour : seriesUsing.ColourLight;

                    if (draw.HasFlag(EPlot.DrawLine))
                    {
                        series.Style.DrawLines = new Pen(colour);
                        series.Style.DrawLines.Width = draw.HasFlag(EPlot.DrawBold) ? 4 : 1;
                    }
                    else
                    {
                        series.Style.DrawPoints = new SolidBrush(colour);
                        series.Style.DrawPointsSize = 8;
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
                var cdp = new MChart.DataPoint(xPos, yPos);
                var tag = new IntensityInfo(condDay, condRep, condType, yPos);
                cdp.Tag = tag;
                series.Points.Add(cdp);
            }
        }
    } // class
} // namespace
