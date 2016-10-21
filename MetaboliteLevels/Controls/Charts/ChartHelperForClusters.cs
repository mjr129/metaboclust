using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MCharting;
using MetaboliteLevels.Data.Algorithms.General;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Session.Singular;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Types.UI;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Controls.Charts
{
    class LineInfo
    {
        public string SeriesName;
        public Color Colour;
        public DashStyle DashStyle;

        public LineInfo(string seriesName, Color colour, DashStyle dashStyle)
        {
            this.SeriesName = seriesName;
            this.Colour = colour;
            this.DashStyle = dashStyle;
        }
    }

    class ChartHelperForClusters : ChartHelper
    {
        public StylisedCluster SelectedCluster { get; private set; }

        public override Visualisable CurrentPlot
        {
            get
            {
                return SelectedCluster?.ActualElement;
            }
        }

        /// <summary>
        /// Ctor
        /// </summary>
        public ChartHelperForClusters( ISelectionHolder selector, Core core, Control targetSite)
            : base(selector, core, targetSite, true)
        {
            _chart.SelectionChanging += _chart_SelectionChanging;
        }

        /// <summary>
        /// Selects the specified series on the chart
        /// </summary>
        public void SelectSeries(Peak peak)
        {
            if (_chart.SelectedItem.SelectedSeries != null && _chart.SelectedItem.SelectedSeries.Tag == peak)
            {
                // Already selected
                return;
            }

            _chart.SelectedItem = new MCharting.Selection(GetSeries(peak).ToArray());
        }

        private List<MCharting.Series> GetSeries(Peak peak)
        {
            List<MCharting.Series> theSeries = new List<MCharting.Series>();

            if (peak == null)
            {
                return theSeries;
            }

            foreach (MCharting.Series series in _chart.CurrentPlot.Series)
            {
                if (series.Tag == peak)
                {
                    theSeries.Add(series);
                }
            }

            return theSeries;
        }

        /// <summary>
        /// Creates a bitmap of the specified cluster
        /// </summary>
        public Bitmap CreateBitmap(int width, int height, StylisedCluster cluster)
        {
            if (cluster.Cluster.Assignments.Count == 0)
            {
                return null;
            }

            Plot(cluster);
            return CreateBitmap(width, height);
        }

        private void _chart_SelectionChanging(object sender, SelectingChangingEventArgs e)
        {
            if (e.Selection.Series.Length == 1)
            {
                Peak peak = (Peak)e.Selection.Series[0].Tag;
                e.Selection = new MCharting.Selection(GetSeries(peak).ToArray(), e.Selection.DataPoint, e.Selection.YIndex, e.Selection.XIndex);
            }
        }

        /// <summary>
        /// Actual plotting.
        /// </summary>                            
        public void Plot(StylisedCluster stylisedCluster)
        {
            Debug.WriteLine("ClusterPlot: " + stylisedCluster);

            // Reset the chart
            bool isPreview = stylisedCluster != null && stylisedCluster.IsPreview;
            Plot plot = PrepareNewPlot(!isPreview, (stylisedCluster != null) ? stylisedCluster.Cluster : null);

            // Set the selected cluster
            Cluster p = stylisedCluster != null ? stylisedCluster.Cluster : null;
            Dictionary<Peak, LineInfo> colours = stylisedCluster != null ? stylisedCluster.Colours : null;
            SelectedCluster = stylisedCluster;

            if (stylisedCluster != null)
            {
                SetCaption(stylisedCluster.CaptionFormat, stylisedCluster.ActualElement, stylisedCluster.WhatIsHighlighted);
            }
            else
            {
                SetCaption("No plot displayed.");
            }

            Core core = _core;
            CoreColourSettings colourSettings = core.Options.Colours;

            // If there is nothing to plot then exit
            if (p == null)
            {
                CompleteNewPlot(plot);
                return;
            }

            // Sort the variables exemplars top-most
            List<Assignment> toPlot = new List<Assignment>(p.Assignments.List);

            // Too much to plot
            while (toPlot.Count > core.Options.MaxPlotVariables)
            {
                //toPlot = toPlot.OrderBy(z => z.Peak.Index).ToList();
                toPlot.RemoveRange(core.Options.MaxPlotVariables, toPlot.Count - core.Options.MaxPlotVariables);
            }

            int numClusterCentres = 0;
            HashSet<GroupInfo> groupsInPlot = p.Assignments.Vectors.First().ColHeaders.Unique( z => z.Observation.Group );

            if (!_core.Options.DisplayAllGroupsInClusterPlot)
            {
                groupsInPlot.RemoveWhere( z => !_core.Options.ViewTypes.Contains( z ) );
            }

            GroupInfo[] groupOrder = groupsInPlot.OrderBy( GroupInfoBase.GroupOrderBy).ToArray();

            MCharting.Series legendEntry = new MCharting.Series();
            legendEntry.Name = "Input vector";
            legendEntry.Style.DrawLines = new Pen(Color.Black, _core.Options.LineWidth * 2);
            plot.LegendEntries.Add(legendEntry);

            // --- LEGEND ---
            var groupLegends = DrawLegend(plot, groupOrder);

            HashSet<MCharting.Series> toBringToFront = new HashSet<MCharting.Series>();

            // --- PLOT CLUSTER ASSIGNMENTS ---
            // Iterate variables in cluster
            for (int assignmentIndex = 0; assignmentIndex < toPlot.Count; assignmentIndex++)
            {
                Assignment assignment = toPlot[assignmentIndex];
                Vector vec = assignment.Vector;
                Peak peak = vec.Peak;
                Dictionary<GroupInfo, MCharting.Series> vecSeries = new Dictionary<GroupInfo, MCharting.Series>();
                Dictionary<GroupInfo, MCharting.Series> vec2Series = new Dictionary<GroupInfo, MCharting.Series>();
                MCharting.DataPoint lastPoint = null;
                MCharting.Series lastSeries = null;

                ///////////////////
                // PLOT THE POINT
                // Okay! Now plot the values pertinent to this peak & type                                           
                for(int i = 0; i < vec.Length; ++i)
                {
                    // Peak, by observation
                    ObservationInfo obs = vec.Observations[i];
                    GroupInfo group = obs.Group;

                    if (!groupOrder.Contains(group))
                    {
                        continue;
                    }

                    MCharting.Series series = GetOrCreateSeries(plot, vecSeries, group, vec, stylisedCluster, groupLegends, legendEntry, toBringToFront, false);

                    // When the series changes we create an intermediate series to join the two together
                    //MCharting.Series seriesb = null;

                    //if (series != lastSeries && lastSeries != null)
                    //{
                    //    seriesb = GetOrCreateSeries(plot, vec2Series, group, vec, stylisedCluster, groupLegends, legendEntry, toBringToFront, true);
                    //}

                    lastSeries = series;

                    int typeOffset = GetTypeOffset(groupOrder, group);

                    int x = typeOffset + obs.Time;
                    double v = vec.Values[i];
                    MCharting.DataPoint dataPoint = new MCharting.DataPoint(x, v);
                    dataPoint.Tag = new IntensityInfo(obs.Time, obs.Rep, obs.Group, v);
                    series.Points.Add(dataPoint);

                    //if (seriesb != null)
                    //{
                    //    seriesb.Points.Add(lastPoint);
                    //    seriesb.Points.Add(dataPoint);
                    //}

                    lastPoint = dataPoint;
                }     
            }

            // --- PLOT CLUSTER CENTRES ---
            if (!isPreview && core.Options.ShowCentres && p.Centres.Count != 0 && p.Centres.Count < 100)
            {
                MCharting.Series legendEntry2 = new MCharting.Series();
                legendEntry2.Name = (p.Centres.Count == 1) ? "Cluster centre" : "Cluster centres";
                legendEntry2.Style.DrawLines = new Pen(colourSettings.ClusterCentre, _core.Options.LineWidth * 2);
                legendEntry2.Style.DrawLines.DashStyle = DashStyle.Dash;
                legendEntry2.Style.DrawPoints = new SolidBrush(colourSettings.ClusterCentre);
                legendEntry2.Style.DrawPointsSize = 8; // MarkerStyle.Diamond;
                plot.LegendEntries.Add(legendEntry2);

                var templateAssignment = p.Assignments.List.First();

                foreach (double[] centre in p.Centres)
                {
                    MCharting.Series series = new MCharting.Series();
                    series.Style.StrictOrder = SeriesStyle.EOrder.X;
                    series.ApplicableLegends.Add(legendEntry2);
                    plot.Series.Add(series);
                    series.Name = "Cluster centre #" + (++numClusterCentres);
                    series.Style.DrawLines = new Pen(colourSettings.ClusterCentre, _core.Options.LineWidth * 2);
                    series.Style.DrawLines.DashStyle = DashStyle.Dash;
                    series.Style.DrawPoints = new SolidBrush(colourSettings.ClusterCentre);
                    series.Style.DrawPointsSize = 8; // MarkerStyle.Diamond;
                         
                    IEnumerable<int> order = templateAssignment.Vector.Observations.WhichOrder(ObservationInfo.GroupTimeReplicateDisplayOrder);

                    foreach (int index in order)
                    {
                        // Centre, by observation
                        ObservationInfo cond = templateAssignment.Vector.Observations[index];

                        if (!groupOrder.Contains(cond.Group))
                        {
                            continue;
                        }

                        double dp = centre[index];
                        int x = GetTypeOffset(groupOrder, cond.Group) + cond.Time;
                        MCharting.DataPoint cdp = new MCharting.DataPoint(x, dp);
                        cdp.Tag = new IntensityInfo(cond.Time, cond.Rep, cond.Group, dp);
                        series.Points.Add(cdp);
                    }          
                }
            }

            // Bring highlighted series to the front
            foreach (MCharting.Series series in toBringToFront)
            {
                plot.Series.Remove(series);
                plot.Series.Add(series);
            }

            // --- LABELS ---
            DrawLabels(plot, true, groupOrder, true);

            // --- COMPLETE ---
            CompleteNewPlot(plot);
        }

        /// <summary>
        /// Returns the series for a group and vector, creating one if it doesn't already exist
        /// </summary>
        /// <param name="plot">Plot to create series in</param>
        /// <param name="existingSeries">Existing series</param>
        /// <param name="group">Group to create series for</param>
        /// <param name="vector">Vector to create series for</param>
        /// <param name="style">Styles (colours and highlights)</param>
        /// <param name="groupLegends">Where to add legends to</param>
        /// <param name="lineLegend">Where to add legends to</param>
        /// <returns>Series (new or existing)</returns>
        private MCharting.Series GetOrCreateSeries(
            MCharting.Plot plot, 
            Dictionary<GroupInfo, MCharting.Series> existingSeries,
            GroupInfo group,
            Vector vector,
            StylisedCluster style,
            Dictionary<GroupInfoBase, MCharting.Series> groupLegends,
            MCharting.Series lineLegend, 
            HashSet<MCharting.Series> toBringToFront,
            bool bandw)
        {
            MCharting.Series series;

            if (existingSeries.TryGetValue(group, out series))
            {
                return series;
            }

            Peak peak = vector.Peak;
            Dictionary<Peak, LineInfo> colours = style?.Colours;

            // Each peak + condition gets its own series (yes we end up with lots of series)
            series = new MCharting.Series();
            series.Style.StrictOrder = SeriesStyle.EOrder.X;
            series.Name = vector.ToString() + " : " + group.DisplayName;
            series.ApplicableLegends.Add(groupLegends[group]);
            series.ApplicableLegends.Add(lineLegend);
            plot.Series.Add(series);

            // If the parameters specify a colour for this peak use that, else use the default
            if (colours != null && colours.ContainsKey(peak))
            {
                series.Style.DrawLines = new Pen(colours[peak].Colour, _core.Options.LineWidth);
                series.Style.DrawLines.DashStyle = colours[peak].DashStyle;
                series.Name = colours[peak].SeriesName + " : " + group.DisplayName;
            }
            else
            {
                series.Style.DrawLines = new Pen(group.Colour, _core.Options.LineWidth);
            }

            if (bandw)
            {
                series.Style.DrawLines = new Pen(_core.Options.Colours.InputVectorJoiners, _core.Options.LineWidth);
            }

            series.Style.DrawLines.Width = _core.Options.LineWidth;
            series.Tag = peak;

            if (style.Highlight != null && !bandw)
            {
                foreach (StylisedCluster.HighlightElement highlight in style.Highlight)
                {
                    if (highlight.Peak == vector.Peak
                        && (highlight.Group == null || highlight.Group == group))
                    {
                        series.Style.DrawLines.Color = _core.Options.Colours.NotableHighlight;
                        series.Style.DrawLines.Width = _core.Options.LineWidth * 2;
                        toBringToFront.Add(series);
                        break;
                    }
                }
            }

            existingSeries.Add(group, series);

            return series;
        }

        private int GetTypeOffset(IEnumerable<GroupInfo> order, GroupInfo type, bool groupMode)
        {
            if (groupMode)
            {
                return 0;
            }
            else
            {
                return base.GetTypeOffset(order, type);
            }
        }

        public static Bitmap CreatePlaceholderBitmap(object x, Size sz)
        {
            Visualisable xx = x as Visualisable;
            int width = sz.Width;
            int height = sz.Height;
            Bitmap result = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(result))
            {
                g.Clear(Color.LightGray);
                g.DrawLine(Pens.Silver, 0, 0, width, height);
                g.DrawLine(Pens.Silver, width, 0, 0, height);
                Image icon = UiControls.EmboldenImage(xx?.Icon ?? Resources.IconUnknown);
                Rectangle rect = new Rectangle(sz.Width / 2 - icon.Width / 2, sz.Height / 2 - icon.Height / 2, icon.Width, icon.Height);
                g.FillRectangle(Brushes.White, rect);
                g.DrawImage(icon, rect);
                g.DrawRectangle(Pens.Silver, rect);
            }

            return result;
        }
    }
}
