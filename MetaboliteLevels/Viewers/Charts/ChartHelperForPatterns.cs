using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MetaboliteLevels.Data;
using MetaboliteLevels.Data.DataInfo;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Utilities;
using System;
using MetaboliteLevels.Algorithms;

namespace MetaboliteLevels.Viewers.Charts
{
    class LineInfo
    {
        public string SeriesName;
        public Color Colour;
        public ChartDashStyle DashStyle;

        public LineInfo(string seriesName, Color colour, ChartDashStyle dashStyle)
        {
            this.SeriesName = seriesName;
            this.Colour = colour;
            this.DashStyle = dashStyle;
        }
    }

    class ChartHelperForClusters : ChartHelper
    {
        public Cluster SelectedCluster { get; private set; }

        /// <summary>
        /// Ctor
        /// </summary>
        public ChartHelperForClusters(Chart chart, Core core, Button menuButton)
            : base(chart, core, menuButton)
        {
            _enableHighlightSeries = true;
        }

        /// <summary>
        /// Selects the specified series on the chart
        /// </summary>
        public void SelectSeries(Peak v)
        {
            List<Series> theSeries = new List<Series>();

            foreach (Series series in _chart.Series)
            {
                if (series.Tag == v)
                {
                    theSeries.Add(series);
                    break;
                }
            }

            if (theSeries.Count != 0)
            {
                SetSelection(theSeries, null, -1);
            }
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

        /// <summary>
        /// Override to select all series with the same variable at once.
        /// </summary>
        protected override void BeforeSelectSeries(List<Series> series)
        {
            if (series.Count == 1)
            {
                Peak v = (Peak)series[0].Tag;

                foreach (Series s in _chart.Series)
                {
                    if (s.Tag == v)
                    {
                        if (!series.Contains(s))
                        {
                            series.Add(s);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Actual plotting.
        /// </summary>
        /// <param name="sp"></param>
        public void Plot(StylisedCluster sp)
        {
            // Reset the chart
            ClearPlot(sp != null && !sp.IsPreview, (sp != null) ? sp.Cluster : null);

            // Set the selected cluster
            Cluster p = sp != null ? sp.Cluster : null;
            Dictionary<Peak, LineInfo> colours = sp != null ? sp.Colours : null;
            SelectedCluster = p;

            if (sp != null)
            {
                SetCaption(sp.CaptionFormat, sp.ActualElement, sp.Source);
            }
            else
            {
                SetCaption("Plot of {0}.", null);
            }

            var core = _core;
            var colourSettings = core.Options.Colours;

            // If there is nothing to plot then exit
            if (p == null)
            {
                return;
            }

            // Sort the variables exemplars top-most
            List<Assignment> toPlot = new List<Assignment>(p.Assignments.List);

            // Too much to plot
            while (toPlot.Count > core.Options.MaxPlotVariables)
            {
                toPlot = toPlot.OrderBy(z => z.Peak.Index).ToList();
                toPlot.RemoveRange(core.Options.MaxPlotVariables, toPlot.Count - core.Options.MaxPlotVariables);
            }

            int numClusterCentres = 0;
            HashSet<GroupInfo> groups = p.Assignments.List.Select(z => z.Vector.Group).Unique(); // array containing groups or null
            bool isMultiGroup = groups.Count != 1;
            GroupInfo[] groupOrder = _core.Groups.OrderBy(GroupInfo.GroupOrderBy).ToArray();

            // --- PLOT PATTERN ASSIGNMENTS ---
            // Iterate variables in cluster
            for (int assignmentIndex = 0; assignmentIndex < toPlot.Count; assignmentIndex++)
            {
                Assignment assignment = toPlot[assignmentIndex];
                Vector vec = assignment.Vector;
                Peak peak = vec.Peak;
                Dictionary<GroupInfo, Series> vecSeries = new Dictionary<GroupInfo, Series>();

                double[] vector = vec.Values;

                ///////////////////
                // PLOT THE POINT
                // Okay! Now plot the values pertinent to this peak & type
                if (vec.Conditions != null)
                {
                    IEnumerable<int> order = vec.Conditions.WhichOrder(ConditionInfo.GroupTimeOrder);

                    foreach (int index in order)
                    {
                        // Peak, by condition
                        ConditionInfo cond = vec.Conditions[index];
                        GroupInfo group = cond.Group;
                        Series series = GetOrCreateSeries(vecSeries, group, vec, sp);
                        int originalIndex = index;

                        int typeOffset = GetTypeOffset(groupOrder, group, isMultiGroup);

                        int x = typeOffset + cond.Time;
                        double v = vector[originalIndex];
                        DataPoint cdp = new DataPoint(x, v);
                        cdp.Tag = new IntensityInfo(cond.Time, null, cond.Group, v);
                        series.Points.Add(cdp);
                    }
                }
                else if (vec.Observations != null)
                {
                    IEnumerable<int> order = vec.Observations.WhichOrder(ObservationInfo.GroupTimeReplicateOrder);

                    foreach (int index in order)
                    {
                        // Peak, by observation
                        ObservationInfo obs = vec.Observations[index];
                        GroupInfo group = obs.Group;
                        Series series = GetOrCreateSeries(vecSeries, group, vec, sp);

                        int originalIndex = index;

                        int typeOffset = GetTypeOffset(groupOrder, group, isMultiGroup);

                        int x = typeOffset + obs.Time;
                        double v = vector[originalIndex];
                        DataPoint cdp = new DataPoint(x, v);
                        cdp.Tag = new IntensityInfo(obs.Time, obs.Rep, obs.Group, v);
                        series.Points.Add(cdp);
                    }
                }
            } // For group  

            // --- PLOT PATTERN CENTRES ---
            if (!sp.IsPreview && core.Options.ShowCentres && p.Centres.Count < 100)
            {
                var templateAssignment = p.Assignments.List.First();

                foreach (double[] centre in p.Centres)
                {
                    var series = _chart.Series.Add("Cluster centre #" + (++numClusterCentres));
                    series.ChartType = SeriesChartType.Line;
                    series.Color = colourSettings.ClusterCentre;
                    series.IsVisibleInLegend = false;
                    series.BorderDashStyle = ChartDashStyle.Dash;
                    series.MarkerStyle = MarkerStyle.Diamond;
                    series.MarkerSize = 8;
                    series.BorderWidth = 2;

                    if (templateAssignment.Vector.Conditions != null)
                    {
                        IEnumerable<int> order = templateAssignment.Vector.Conditions.WhichOrder(ConditionInfo.GroupTimeOrder);

                        foreach (int index in order)
                        {
                            // Centre, by condition
                            ConditionInfo cond = templateAssignment.Vector.Conditions[index];
                            double dp = centre[index];
                            int x = GetTypeOffset(groupOrder, cond.Group, isMultiGroup) + cond.Time;
                            DataPoint cdp = new DataPoint(x, dp);
                            cdp.Tag = new IntensityInfo(cond.Time, null, cond.Group, dp);
                            series.Points.Add(cdp);
                        }
                    }
                    else
                    {
                        IEnumerable<int> order = templateAssignment.Vector.Observations.WhichOrder(ObservationInfo.GroupTimeReplicateOrder);

                        foreach (int index in order)
                        {
                            // Centre, by observation
                            ObservationInfo cond = templateAssignment.Vector.Observations[index];
                            double dp = centre[index];
                            int x = GetTypeOffset(groupOrder, cond.Group, isMultiGroup) + cond.Time;
                            DataPoint cdp = new DataPoint(x, dp);
                            cdp.Tag = new IntensityInfo(cond.Time, cond.Rep, cond.Group, dp);
                            series.Points.Add(cdp);
                        }
                    }
                }
            }

            // LABELS
            DrawLabels(!isMultiGroup, groupOrder);
        }

        private Series GetOrCreateSeries(Dictionary<GroupInfo, Series> serieses, GroupInfo group, Vector vector, StylisedCluster sp)
        {
            Series series;

            if (serieses.TryGetValue(group, out series))
            {
                return series;
            }

            Peak peak = vector.Peak;
            Dictionary<Peak, LineInfo> colours = sp != null ? sp.Colours : null;

            // Each peak + condition gets its own series (yes we end up with lots of series)
            series = _chart.Series.Add(vector.ToString() + " | " + group.Name);

            // If the parameters specify a colour for this peak use that, else use the default
            if (colours != null && colours.ContainsKey(peak))
            {
                series.Color = colours[peak].Colour;
                series.BorderDashStyle = colours[peak].DashStyle;
                series.Name = colours[peak].SeriesName + " | " + group.Name;
            }
            else
            {
                series.Color = group.Colour;
            }

            series.BorderWidth = sp.Source == peak ? 2 : 1;
            series.ChartType = SeriesChartType.Line;
            series.IsVisibleInLegend = false;
            series.Tag = peak;

            if (sp.Highlight != null)
            {
                foreach (var highlight in sp.Highlight)
                {
                    if (highlight.Peak == vector.Peak
                        && (highlight.Group == null || highlight.Group == group))
                    {
                        series.Color = Color.Red;
                        break;
                    }
                }
            }

            serieses.Add(group, series);

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

        public static Bitmap CreatePlaceholderBitmap(IVisualisable x, Size sz)
        {
            int width = sz.Width;
            int height = sz.Height;
            Bitmap result = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(result))
            {
                g.Clear(Color.LightGray);
                g.DrawLine(Pens.Silver, 0, 0, width, height);
                g.DrawLine(Pens.Silver, width, 0, 0, height);
                Image icon = UiControls.GetImage(x.GetIcon(), true);
                Rectangle rect = new Rectangle(sz.Width / 2 - icon.Width / 2, sz.Height / 2 - icon.Height / 2, icon.Width, icon.Height);
                g.FillRectangle(Brushes.White, rect);
                g.DrawImage(icon, rect);
                g.DrawRectangle(Pens.Silver, rect);
            }

            return result;
        }
    }
}
