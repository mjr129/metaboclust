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
using MetaboliteLevels.Algorithms;
using MetaboliteLevels.Algorithms.Statistics.Configurations;

namespace MetaboliteLevels.Viewers.Charts
{
    class ChartHelperForPeaks : ChartHelper
    {
        public static ConfigurationTrend MinSmoother = new ConfigurationTrend( "Minimum", null, Algo.ID_TREND_MOVING_MINIMUM, new Algorithms.Statistics.Arguments.ArgsTrend( null, new object[] { 1 } ) );
        public static ConfigurationTrend MaxSmoother = new ConfigurationTrend( "Maximum", null, Algo.ID_TREND_MOVING_MAXIMUM, new Algorithms.Statistics.Arguments.ArgsTrend( null, new object[] { 1 } ) );
        public static ConfigurationTrend FallbackSmoother = new ConfigurationTrend( "Median", null, Algo.ID_TREND_MOVING_MEDIAN, new Algorithms.Statistics.Arguments.ArgsTrend( null, new object[] { 1 } ) );

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

        public ChartHelperForPeaks( ISelectionHolder selector, Core core, Control targetSite )
            : base( selector, core, targetSite, false )
        {
        }

        public Bitmap CreateBitmap( int width, int height, StylisedPeak peak )
        {
            Plot( peak );
            return CreateBitmap( width, height );
        }

        public void Plot( StylisedPeak stylisedPeak )
        {
            Debug.WriteLine( "PeakPlot: " + stylisedPeak );
            Dictionary<string, MCharting.Series> seriesNames = new Dictionary<string, MCharting.Series>();
            Peak peak = stylisedPeak?.Peak;

            // Clear plot
            MCharting.Plot plot = PrepareNewPlot( stylisedPeak != null && !stylisedPeak.IsPreview, peak );

            // Get selection   
            SelectedPeak = peak;
            SetCaption( "Plot of {0}.", peak );

            if (peak == null)
            {
                CompleteNewPlot( plot );
                return;
            }

            // Get options
            StylisedPeakOptions opts = stylisedPeak.OverrideDefaultOptions ?? new StylisedPeakOptions( _core );

            // Get order data                                      
            ObservationInfo[] obsOrder = _core.Observations.ToArray();

            // Group legends
            IEnumerable<GroupInfoBase> order = opts.ShowAcqisition ? (IEnumerable<GroupInfoBase>)opts.ViewBatches : (IEnumerable<GroupInfoBase>)opts.ViewGroups;
            Dictionary<GroupInfoBase, MCharting.Series> groupLegends = DrawLegend( plot, order );

            // Get observations
            Vector vector;

            if (stylisedPeak.ForceObservations != null)
            {
                vector = stylisedPeak.ForceObservations;
            }
            else
            {
                vector = _core.Options.SelectedMatrix.Find( stylisedPeak.Peak );
            }

            if (vector == null)
            {
                CompleteNewPlot( plot );
                return;
            }

            // Show acquisition and batches?
            if (opts.ShowAcqisition)
            {
                // --- RAW DATA (points) ---
                MCharting.Series legendEntry = new MCharting.Series();
                legendEntry.Name = "Observations";
                legendEntry.Style.DrawPoints = new SolidBrush( Color.Black );
                plot.LegendEntries.Add( legendEntry );

                AddToPlot( plot, peak, seriesNames, vector, "Raw data", opts, EPlot.ByBatch, groupLegends, legendEntry );

                // --- TREND (thick line) ---
                if (stylisedPeak.ForceTrend != null)
                {
                    MCharting.Series legendEntry2 = new MCharting.Series();
                    legendEntry2.Name = "Trend";
                    legendEntry2.Style.DrawLines = new Pen( Color.Black, _core.Options.LineWidth );
                    legendEntry2.Style.DrawLines.Width = 4;
                    plot.LegendEntries.Add( legendEntry2 );

                    AddToPlot( plot, peak, seriesNames, vector, "Trend data", opts, EPlot.ByBatch | EPlot.DrawLine | EPlot.DrawBold, groupLegends, legendEntry2 );
                }

                DrawLabels( plot, opts.ConditionsSideBySide, order, opts.DrawExperimentalGroupAxisLabels );
                CompleteNewPlot( plot );
                return;
            }

            // Sort data                                                
            ConfigurationTrend trend = _core.Options.SelectedTrend;
            Vector avg = stylisedPeak.ForceTrend ?? trend.CreateTrend( _core, vector );
            Vector min = MinSmoother.CreateTrend( _core, vector );
            Vector max = MaxSmoother.CreateTrend( _core, vector );

            // --- PLOT MEAN & SD (lines across)
            if (opts.ShowVariableMean)
            {
                AddMeanAndSdLines( plot, opts, vector.Values, peak, groupLegends );
            }

            // --- RANGE (shaded area) ---
            if (opts.ShowRanges)
            {
                AddUpperAndLowerShade( plot, opts, seriesNames, peak, min, max, groupLegends );
            }

            // --- RAW DATA (points) ---
            if (opts.ShowPoints && !stylisedPeak.IsPreview)
            {
                MCharting.Series legendEntry = new MCharting.Series();
                legendEntry.Name = "Observations";
                legendEntry.Style.DrawPoints = new SolidBrush( Color.Black );
                plot.LegendEntries.Add( legendEntry );

                AddToPlot( plot, peak, seriesNames, vector, "Raw data", opts, EPlot.None, groupLegends, legendEntry );
            }

            // --- RANGE (lines) ---
            if (opts.ShowMinMax)
            {
                MCharting.Series legendEntry = new MCharting.Series();
                legendEntry.Name = "Range min/max";
                legendEntry.Style.DrawLines = new Pen( Color.Gray, _core.Options.LineWidth );
                plot.LegendEntries.Add( legendEntry );

                AddToPlot( plot, peak, seriesNames, min, "Min value", opts, EPlot.DrawLine, groupLegends, legendEntry );
                AddToPlot( plot, peak, seriesNames, max, "Max value", opts, EPlot.DrawLine, groupLegends, legendEntry );
            }

            // --- TREND (thick line) ---
            if (opts.ShowTrend)
            {
                MCharting.Series legendEntry = new MCharting.Series();
                legendEntry.Name = "Trend";
                legendEntry.Style.DrawLines = new Pen( Color.Black, _core.Options.LineWidth );
                legendEntry.Style.DrawLines.Width = _core.Options.LineWidth * 4;
                plot.LegendEntries.Add( legendEntry );

                AddToPlot( plot, peak, seriesNames, avg, "Trend data", opts, EPlot.DrawLine | EPlot.DrawBold, groupLegends, legendEntry );
            }

            // --- LABELS ---
            DrawLabels( plot, opts.ConditionsSideBySide, order, opts.DrawExperimentalGroupAxisLabels );

            CompleteNewPlot( plot );
        }

        private void AddUpperAndLowerShade(
            MCharting.Plot plot,
            StylisedPeakOptions o,
            Dictionary<string, MCharting.Series> seriesNames,
            Peak peak,
            Vector min,
            Vector max,
            Dictionary<GroupInfoBase, MCharting.Series> groupLegends )
        {
            MCharting.Series legendEntry = new MCharting.Series();
            legendEntry.Name = "Range";
            legendEntry.Style.DrawVBands = new SolidBrush( Color.Gray );
            plot.LegendEntries.Add( legendEntry );

            // Iterate the conditions
            for (int i = 0; i < min.Observations.Length; i++)
            {
                ObservationInfo obs = min.Observations[i];

                Debug.Assert( max.Observations[i] == min.Observations[i], "Expected max and min trends to match sequences." );

                if (o.ViewGroups.Contains( obs.Group ))
                {
                    // Name the series
                    string name = "Range for " + obs.Group.DisplayName;
                    MCharting.Series series;

                    // Create the series (if required)
                    if (!seriesNames.ContainsKey( name ))
                    {
                        series = plot.Series.Add( name );
                        series.Style.StrictOrder = SeriesStyle.EOrder.X;
                        Color c = obs.Group.ColourLight;
                        c = Color.FromArgb( 0x80, c.R, c.G, c.B );
                        series.Tag = peak;
                        series.Style.DrawVBands = obs.Group.CreateBrush( c );
                        series.ApplicableLegends.Add( groupLegends[obs.Group] );
                        series.ApplicableLegends.Add( legendEntry );
                        seriesNames.Add( name, series );
                    }
                    else
                    {
                        series = seriesNames[name];
                    }

                    // Get the X coordinate
                    int typeOffset = GetTypeOffset( o.ViewGroups, obs.Group );

                    double xVal = obs.Time;

                    if (o.ConditionsSideBySide)
                    {
                        xVal += typeOffset;
                    }

                    // Get the Y coordinates
                    double yMin = min.Values[i];
                    double yMax = max.Values[i];

                    if (double.IsNaN( yMin ) || double.IsInfinity( yMin ))
                    {
                        yMin = 0;
                    }

                    if (double.IsNaN( yMax ) || double.IsInfinity( yMax ))
                    {
                        yMax = 0;
                    }

                    // Create the point
                    IntensityInfo info1 = new IntensityInfo( obs.Time, null, obs.Group, yMin );
                    IntensityInfo info2 = new IntensityInfo( obs.Time, null, obs.Group, yMax );
                    MCharting.DataPoint cdp = new MCharting.DataPoint( xVal, new[] { yMin, yMax } );
                    cdp.Tag = new[] { info1, info2 };
                    series.Points.Add( cdp );
                }
            }
        }

        private void AddMeanAndSdLines(
            MCharting.Plot plot,
            StylisedPeakOptions o,
            double[] observations,
            Peak peak,
            Dictionary<GroupInfoBase, MCharting.Series> groupLegends )
        {
            MCharting.Series legendEntry = new MCharting.Series();
            legendEntry.Name = "Std. Dev. Min/Max";
            legendEntry.Style.DrawLines = new Pen( Color.Gray, _core.Options.LineWidth );
            legendEntry.Style.DrawLines.DashStyle = DashStyle.Dot;
            plot.LegendEntries.Add( legendEntry );

            MCharting.Series legendEntry2 = new MCharting.Series();
            legendEntry2.Name = "Mean";
            legendEntry2.Style.DrawLines = new Pen( Color.Black, _core.Options.LineWidth );
            plot.LegendEntries.Add( legendEntry2 );

            // Iterate the types
            foreach (GroupInfo group in o.ViewGroups)
            {
                // Get the Y values
                double yMean = Maths.Mean( observations );
                double sd = Maths.StdDev( observations, yMean );
                double yMin = yMean - sd / 2;
                double yMax = yMean + sd / 2;

                // Get the X values
                int xLeft = @group.Range.Min;
                int xRight = @group.Range.Max;

                if (o.ConditionsSideBySide)
                {
                    int typeOffset = GetTypeOffset( o.ViewGroups, @group );
                    xLeft += typeOffset;
                    xRight += typeOffset;
                }

                // Create the series
                MCharting.Series sMean = plot.Series.Add( @group.DisplayName + ": Mean" );
                MCharting.Series sMin = plot.Series.Add( @group.DisplayName + ": StdDevMin" );
                MCharting.Series sMax = plot.Series.Add( @group.DisplayName + ": StdDevMax" );

                sMean.Style.StrictOrder = SeriesStyle.EOrder.X;
                sMin.Style.StrictOrder = SeriesStyle.EOrder.X;
                sMax.Style.StrictOrder = SeriesStyle.EOrder.X;

                sMean.ApplicableLegends.Add( groupLegends[group] );
                sMin.ApplicableLegends.Add( groupLegends[group] );
                sMax.ApplicableLegends.Add( groupLegends[group] );
                sMean.ApplicableLegends.Add( legendEntry2 );
                sMin.ApplicableLegends.Add( legendEntry );
                sMax.ApplicableLegends.Add( legendEntry );

                sMean.Tag = peak;
                sMin.Tag = peak;
                sMax.Tag = peak;

                Color c = @group.ColourLight;
                sMean.Style.DrawLines = new Pen( c, _core.Options.LineWidth );
                sMin.Style.DrawLines = new Pen( c, _core.Options.LineWidth );
                sMax.Style.DrawLines = new Pen( c, _core.Options.LineWidth );

                sMin.Style.DrawLines.DashStyle = DashStyle.Dot;
                sMax.Style.DrawLines.DashStyle = DashStyle.Dot;

                // Add the points
                AddDataPoint( sMean, xLeft, yMean, @group );
                AddDataPoint( sMean, xRight, yMean, @group );

                AddDataPoint( sMin, xLeft, yMin, @group );
                AddDataPoint( sMin, xRight, yMin, @group );

                AddDataPoint( sMax, xLeft, yMax, @group );
                AddDataPoint( sMax, xRight, yMax, @group );
            }
        }

        /// <summary>
        /// Adds a tagged datapoint to a series.
        /// </summary>
        private static void AddDataPoint( MCharting.Series series, int x, double y, GroupInfo type )
        {
            MCharting.DataPoint dp = new MCharting.DataPoint( x, y );
            dp.Tag = new IntensityInfo( null, null, type, y );

            if (double.IsNaN( y ) || double.IsInfinity( y ))
            {
                y = 0;
            }

            series.Points.Add( dp );
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
            Vector intensities,
            string seriesName,
            StylisedPeakOptions o,
            EPlot draw,
            Dictionary<GroupInfoBase, MCharting.Series> groupLegends,
            MCharting.Series legend )
        {

            // Iterate whatever it is we're iterating
            for (int i = 0; i < intensities.Observations.Length; ++i)
            {
                // Get the values                                 
                ObservationInfo obs = intensities.Observations[i];

                // Name the series
                if (draw.HasFlag( EPlot.ByBatch ))
                {
                    if (!o.ViewBatches.Contains( obs.Batch ))
                    {
                        continue;
                    }
                }
                else
                {
                    if (!o.ViewGroups.Contains( obs.Group ))
                    {
                        continue;
                    }
                }

                bool colorByBatch = draw.HasFlag( EPlot.ByBatch );
                GroupInfoBase seriesUsing = colorByBatch ? (GroupInfoBase)obs.Batch : (GroupInfoBase)obs.Group;

                string name = seriesName + " for " + seriesUsing.DisplayName;
                MCharting.Series series;

                // Create the series (if required)
                if (!seriesNames.ContainsKey( name ))
                {
                    series = plot.Series.Add( name );
                    series.Style.StrictOrder = SeriesStyle.EOrder.X;
                    series.ApplicableLegends.Add( groupLegends[seriesUsing] );
                    series.ApplicableLegends.Add( legend );
                    seriesNames.Add( name, series );
                    series.Tag = peak;

                    Color colour = (draw.HasFlag( EPlot.DrawBold ) || draw.HasFlag( EPlot.ByBatch ) || !o.ShowTrend) ? seriesUsing.Colour : seriesUsing.ColourLight;

                    if (draw.HasFlag( EPlot.DrawLine ))
                    {
                        series.Style.DrawLines = new Pen( colour );
                        series.Style.DrawLines.Width = draw.HasFlag( EPlot.DrawBold ) ? _core.Options.LineWidth * 4 : _core.Options.LineWidth;
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

                if (draw.HasFlag( EPlot.ByBatch ))
                {
                    xPos = obs.Order;

                    if (o.ConditionsSideBySide)
                    {
                        xPos += GetBatchOffset( o.ViewBatches, obs.Batch );
                    }
                }
                else
                {
                    xPos = obs.Time;

                    if (o.ConditionsSideBySide)
                    {
                        xPos += GetTypeOffset( o.ViewGroups, obs.Group );
                    }
                }

                // Get the Y position
                double yPos = intensities.Values[i];

                if (double.IsNaN( yPos ) || double.IsInfinity( yPos ))
                {
                    yPos = 0;
                }

                // Create the point
                MCharting.DataPoint cdp = new MCharting.DataPoint( xPos, yPos );
                IntensityInfo tag = new IntensityInfo( obs.Time, obs.Rep, obs.Group, yPos );
                cdp.Tag = tag;
                series.Points.Add( cdp );
            }
        }
    } // class
} // namespace
