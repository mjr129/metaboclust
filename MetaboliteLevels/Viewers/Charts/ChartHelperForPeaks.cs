using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MetaboliteLevels.Data;
using MetaboliteLevels.Data.DataInfo;
using MetaboliteLevels.Data.General;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Visualisables;
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

        public ChartHelperForPeaks(Chart chart, Core core, Button menuButton)
            : base(chart, core, menuButton)
        {
        }

        public Bitmap CreateBitmap(int width, int height, StylisedPeak variable)
        {
            Plot(variable);
            return CreateBitmap(width, height);
        }

        public void Plot(StylisedPeak sPeak)
        {
            var seriesNames = new HashSet<string>();

            // Clear plot
            ClearPlot(sPeak != null && !sPeak.IsPreview, sPeak != null ? sPeak.Peak : null);

            // Get selection
            Peak peak = sPeak != null ? sPeak.Peak : null;
            SelectedPeak = peak;
            SetCaption("Plot of {0}.", peak);

            if (peak == null)
            {
                return;
            }

            // Get options
            var opts = sPeak.OverrideDefaultOptions ?? new StylisedPeakOptions(_core);

            // Get order data
            var condOrder = _core.Conditions.ToArray();
            var obsOrder = _core.Observations.ToArray();

            // Get observations
            PeakValueSet observations;

            if (sPeak.ForceObservations != null)
            {
                observations = sPeak.ForceObservations;
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
                return;
            }

            // COPY the observations (since we will reorder them)
            var raw = observations.Raw.ToArray();

            // Show acquisition abd batches?
            if (opts.ShowAcqisition)
            {
                // --- RAW DATA (points) ---
                ArrayHelper.Sort(obsOrder, raw, ObservationInfo.BatchAcquisitionOrder);
                AddToPlot(peak, seriesNames, raw, "Raw data", obsOrder, opts, EPlot.ByBatch);

                // --- TREND (thick line) ---
                if (sPeak.ForceTrend != null)
                {
                    raw = sPeak.ForceTrend.ToArray();
                    obsOrder = ((IEnumerable<ObservationInfo>)sPeak.ForceTrendOrder).ToArray();
                    ArrayHelper.Sort(obsOrder, raw, ObservationInfo.BatchAcquisitionOrder);

                    AddToPlot(peak, seriesNames, raw, "Trend data", obsOrder, opts, EPlot.ByBatch | EPlot.DrawLine | EPlot.DrawBold);
                }

                DrawLabels(true, opts.ViewBatches);
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
            if (opts.ShowVariableMean && !sPeak.IsPreview)
            {
                AddMeanAndSdLines(opts, observations, peak);
            }

            // --- RANGE (shaded area) ---
            if (opts.ShowRanges)
            {
                AddUpperAndLowerShade(condOrder, opts, seriesNames, peak, min, max);
            }

            // --- RAW DATA (points) ---
            if (opts.ShowPoints && !sPeak.IsPreview)
            {
                AddToPlot(peak, seriesNames, raw, "Raw data", obsOrder, opts, EPlot.None);
            }

            // --- RANGE (lines) ---
            if (opts.ShowRanges && !sPeak.IsPreview)
            {
                AddToPlot(peak, seriesNames, min, "Min value", condOrder, opts, EPlot.DrawLine);
                AddToPlot(peak, seriesNames, max, "Max value", condOrder, opts, EPlot.DrawLine);
            }

            // --- TREND (thick line) ---
            if (opts.ShowTrend && !sPeak.IsPreview)
            {
                if (sPeak.ForceTrend != null)
                {
                    ConditionInfo[] forder = sPeak.ForceTrendOrder.Cast<ConditionInfo>().ToArray();
                    double[] fdata = sPeak.ForceTrend.ToArray();

                    ArrayHelper.Sort(forder, fdata, ConditionInfo.GroupTimeOrder);

                    AddToPlot(peak, seriesNames, fdata, "Trend data", forder, opts, EPlot.DrawLine | EPlot.DrawBold);
                }
                else
                {
                    AddToPlot(peak, seriesNames, avg, "Trend data", condOrder, opts, EPlot.DrawLine | EPlot.DrawBold);
                }
            }

            // --- LABELS ---
            DrawLabels(opts.ConditionsSideBySide, opts.ViewTypes);
        }

        private void AddUpperAndLowerShade(ConditionInfo[] condOrder, StylisedPeakOptions o, HashSet<string> seriesNames, Peak peak, double[] min, double[] max)
        {
            // Iterate the conditions
            for (int i = 0; i < condOrder.Length; i++)
            {
                ConditionInfo cond = condOrder[i];

                if (o.ViewTypes.Contains(cond.Group))
                {
                    // Name the series
                    string name = "Range for " + cond.Group.Name;

                    // Create the series (if required)
                    if (!seriesNames.Contains(name))
                    {
                        var series = _chart.Series.Add(name);
                        Color c = cond.Group.ColourLight;
                        c = Color.FromArgb(0x80, c.R, c.G, c.B);
                        series.Tag = peak;
                        series.Color = c;
                        series.ChartType = SeriesChartType.Range;
                        series.IsVisibleInLegend = false;
                        seriesNames.Add(name);
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
                    var info1 = new IntensityInfo(cond.Time, null, cond.Group, yMin);
                    var info2 = new IntensityInfo(cond.Time, null, cond.Group, yMax);
                    var cdp = new DataPoint(xVal, new[] { yMin, yMax });
                    cdp.Tag = new[] { info1, info2 };
                    _chart.Series[name].Points.Add(cdp);
                }
            }
        }

        private void AddMeanAndSdLines(StylisedPeakOptions o, PeakValueSet observations, Peak peak)
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
                var sMean = _chart.Series.Add(@group.Name + ": Mean");
                var sMin = _chart.Series.Add(@group.Name + ": StdDevMin");
                var sMax = _chart.Series.Add(@group.Name + ": StdDevMax");

                sMean.Tag = peak;
                sMin.Tag = peak;
                sMax.Tag = peak;

                sMean.IsVisibleInLegend = false;
                sMin.IsVisibleInLegend = false;
                sMax.IsVisibleInLegend = false;

                Color c = @group.ColourLight;
                sMean.Color = c;
                sMin.Color = c;
                sMax.Color = c;

                sMean.ChartType = SeriesChartType.Line;
                sMin.ChartType = SeriesChartType.Line;
                sMax.ChartType = SeriesChartType.Line;

                sMin.BorderDashStyle = ChartDashStyle.Dot;
                sMax.BorderDashStyle = ChartDashStyle.Dot;

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
        private static void AddDataPoint(Series series, int x, double y, GroupInfo type)
        {
            DataPoint dp = new DataPoint(x, y);
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
        private void AddToPlot(Peak peak, HashSet<string> seriesNames, double[] intensities, string seriesName, IEnumerable xInfo, StylisedPeakOptions o, EPlot draw)
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

                // Create the series (if required)
                if (!seriesNames.Contains(name))
                {
                    seriesNames.Add(name);
                    var series = _chart.Series.Add(name);
                    series.IsVisibleInLegend = false;
                    series.Tag = peak;

                    series.Color = (draw.HasFlag(EPlot.DrawBold) | draw.HasFlag(EPlot.ByBatch)) ? seriesUsing.Colour : seriesUsing.ColourLight;

                    if (draw.HasFlag(EPlot.DrawLine))
                    {
                        series.ChartType = SeriesChartType.Line;
                        series.BorderWidth = draw.HasFlag(EPlot.DrawBold) ? 4 : 1;
                    }
                    else
                    {
                        series.ChartType = SeriesChartType.Point;
                    }
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
                var cdp = new DataPoint(xPos, yPos);
                var tag = new IntensityInfo(condDay, condRep, condType, yPos);
                cdp.Tag = tag;
                _chart.Series[name].Points.Add(cdp);
            }
        }
    } // class
} // namespace
