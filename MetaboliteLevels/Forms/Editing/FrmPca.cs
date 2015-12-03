using MCharting;
using MetaboliteLevels.Algorithms;
using MetaboliteLevels.Data.Visualisables;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Algorithms.Statistics.Arguments;
using MetaboliteLevels.Data.DataInfo;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Forms.Algorithms;
using MetaboliteLevels.Settings;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Forms.Generic;
using MetaboliteLevels.Viewers.Lists;
using MetaboliteLevels.Properties;
using System.Collections.ObjectModel;

namespace MetaboliteLevels.Forms.Editing
{
    /// <summary>
    /// Shows a PCA plot.
    /// </summary>
    public partial class FrmPca : Form
    {
        private Core core;

        private ObsFilter _obsFilter;
        private PeakFilter _peakFilter;
        private bool _transposeToShowPeaks;
        private bool _trend;

        private int _component;

        private double[,] _scores;   
        private Column _colourBy;

        internal static void Show(Form owner, Core core)
        {
            using (FrmPca frm = new FrmPca(core))
            {
                UiControls.ShowWithDim(owner, frm);
            }
        }

        private FrmPca()
        {
            InitializeComponent();
        }

        private FrmPca(Core core)
            : this()
        {
            this.core = core;

            _peakFilter = PeakFilter.Empty;
            _obsFilter = ObsFilter.Empty;

            UpdateScores();
        }

        private void UpdateScores()
        {
            StringBuilder ft = new StringBuilder();

            var peaks = _peakFilter.Test(core.Peaks).Passed;

            double[,] valueMatrix = null;

            _mnuTranspose.Text = _transposeToShowPeaks ? "Peaks" : "Observations";
            _mnuTrend.Text = _trend ? "Trend" : "Full data";
            _mnuObsFilter.Text = _obsFilter.ToString();
            _mnuPeakFilter.Text = _peakFilter.ToString();

            IEnumerable conds;
            IEnumerable<int> which;

            if (_trend)
            {
                which = core.Conditions.Which(_obsFilter.Test);
                conds = core.Conditions.In(which);
            }
            else
            {
                which = core.Observations.Which(_obsFilter.Test);
                conds = core.Observations.In(which);
            }

            for (int peakIndex = 0; peakIndex < peaks.Count; peakIndex++)
            {
                IEnumerable<double> vals;
                Peak peak = peaks[peakIndex];

                if (_trend)
                {
                    vals = peak.Observations.Trend.In(which);
                }
                else
                {
                    vals = peak.Observations.Raw.In(which);
                }

                if (valueMatrix == null)
                {
                    valueMatrix = _transposeToShowPeaks ? new double[peaks.Count, vals.Count()] : new double[vals.Count(), peaks.Count];
                }

                int obsIndex = 0;

                foreach (double d in vals)
                {
                    if (_transposeToShowPeaks)
                    {
                        valueMatrix[peakIndex, obsIndex] = d;
                    }
                    else
                    {
                        valueMatrix[obsIndex, peakIndex] = d;
                    }

                    ++obsIndex;
                }
            }

            this._pcaPeaks = peaks;
            this._pcaObservations = conds;

            Arr.Instance.Pca(valueMatrix, out this._scores, out this._loadings);

            UpdatePlot();
        }

        private void UpdatePlot()
        {
            MChart.Plot plot = new MChart.Plot();

            // Get the "rows"
            IEnumerator enSources;

            if (WhatPlotting == EPlotting.Peaks)
            {
                enSources = this._pcaPeaks.GetEnumerator();
            }
            else
            {
                enSources = this._pcaObservations.GetEnumerator();
            }

            // Get the "columns"
            double[,] plotPoints;

            if (_plotSource == EPlotSource.Loadings)
            {
                plotPoints = _loadings;
            }
            else
            {
                plotPoints = _scores;
            }                                                           

            // Get the component
            if (_component < 0)
            {
                _component = 0;
            }

            if (_component >= plotPoints.GetLength(1))
            {
                _component = plotPoints.GetLength(1) - 1;
            }

            _btnPrevPc.Enabled = _component != 0;
            _btnNextPc.Enabled = _component != plotPoints.GetLength(1) - 1;

            _btnScoresOrLoadings.Text = _plotSource == EPlotSource.Loadings ? "Loadings" : "Scores";
            _lblComp.Text = "PC" + (_component + 1).ToString() + " / " + plotPoints.GetLength(1);

            if (WhatPlotting == EPlotting.Peaks)
            {
                _btnColour.Text = _colourBy != null ? _colourBy.ToString() : "Cluster";
            }
            else
            {
                _btnColour.Text = _colourBy2.ToUiString();
            }

            // Iterate scores
            for (int r = 0; r < plotPoints.GetLength(0); r++)
            {
                enSources.MoveNext();

                MChart.Series series;

                // Create series
                if (WhatPlotting == EPlotting.Peaks)
                {
                    // Points are peaks
                    series = CreateSeriesKeyForPeak(plot, (Peak)enSources.Current);
                }
                else
                {
                    // Points are observations
                    series = CreateSeriesKeyForObservation(plot, enSources.Current);
                }

                var coord = new MChart.Coord(plotPoints[r, _component], plotPoints[r, _component + 1]);
                coord.Tag = enSources.Current;

                series.Data.Add(coord);
            }

            if (WhatPlotting == EPlotting.Observations && _colourBy2 == EColourBy.Group)
            {
                foreach (MChart.Series series in plot.Series)
                {
                    series.Style = new MChart.SeriesStyle()
                    {
                        DrawPoints = new SolidBrush(((GroupInfo)series.Tag).Colour)
                    };
                }
            }
            else
            {
                var colours = PlotCreator.AutoColour(plot.Series);

                foreach (var colour in colours)
                {
                    colour.Key.Style = new MChart.SeriesStyle()
                    {
                        DrawPoints = new SolidBrush(colour.Value)
                    };
                }
            }

            _chart.Style.Animate = true;
            _chart.Style.SelectionColour = Color.Yellow;

            _chart.Set(plot);
        }

        private static void FormatSeriesForPeak(MChart.Series series)
        {
            series.Name = (string)series.Tag;
        }

        private MChart.Series CreateSeriesKeyForPeak(MChart.Plot plot, Peak peak)
        {
            object value;

            if (this._colourBy != null)
            {
                value = _colourBy.GetRow(peak);
            }
            else
            {
                value = StringHelper.ArrayToString(peak.Assignments.Clusters);
            }

            return GetOrCreateSeriesForValue(plot, value);
        }

        private EColourBy _colourBy2 = EColourBy.Group;
        private EPlotSource _plotSource;
        private double[,] _loadings;
        private ReadOnlyCollection<Peak> _pcaPeaks;
        private IEnumerable _pcaObservations;

        private static MChart.Series GetOrCreateSeriesForValue(MChart.Plot plot, object value)
        {
            MChart.Series series = plot.Series.FirstOrDefault(z => z.Tag.Equals(value));

            if (series == null)
            {
                series = new MChart.Series();
                series.Style = new MChart.SeriesStyle();
                series.Name = value.ToString();
                series.Tag = value;

                plot.Series.Add(series);
            }

            return series;
        }

        private MChart.Series CreateSeriesKeyForObservation(MChart.Plot plot, object observation)
        {
            object value;

            if (observation is ConditionInfo)
            {
                switch (_colourBy2)
                {
                    case EColourBy.Group:
                        value = ((ConditionInfo)observation).Group;
                        break;

                    case EColourBy.Replicate:
                        value = 0;
                        break;

                    case EColourBy.Time:
                        value = ((ConditionInfo)observation).Time;
                        break;

                    default:
                        throw new SwitchException(_colourBy2);
                }

            }
            else if (observation is ObservationInfo)
            {
                switch (_colourBy2)
                {
                    case EColourBy.Group:
                        value = ((ObservationInfo)observation).Group;
                        break;

                    case EColourBy.Replicate:
                        value = ((ObservationInfo)observation).Rep;
                        break;

                    case EColourBy.Time:
                        value = ((ObservationInfo)observation).Time;
                        break;

                    default:
                        throw new SwitchException(_colourBy2);
                }
            }
            else
            {
                throw new InvalidOperationException("GetGroup: Unknown type: " + observation);
            }

            return GetOrCreateSeriesForValue(plot, value);
        }

        private void toolStripDropDownButton3_DropDownOpening(object sender, EventArgs e)
        {
            ClearCmsFilter(_mnuObsFilter);
            AddFilters(_mnuObsFilter, core.ObsFilters, true);
        }

        private void AddFilters(ToolStripDropDownButton button, IEnumerable source, bool isObservationFilter)
        {
            // Add "no filter"
            Filter @default = isObservationFilter ? (Filter)ObsFilter.Empty : PeakFilter.Empty;
            ToolStripMenuItem tsmi = new ToolStripMenuItem(@default.ToString()) { Tag = @default };
            tsmi.Click += setFilter_Click;
            tsmi.Image = MetaboliteLevels.Properties.Resources.MnuClear;
            tsmi.ImageScaling = ToolStripItemImageScaling.None;
            button.DropDownItems.Add(tsmi);

            // Add session filters
            foreach (var filter in source)
            {
                tsmi = new ToolStripMenuItem(filter.ToString()) { Tag = filter };
                tsmi.Click += setFilter_Click;
                button.DropDownItems.Add(tsmi);
            }

            // Add edit command
            tsmi = new ToolStripMenuItem("Edit " + (isObservationFilter ? "observation" : "peak") + " filters...");
            tsmi.Click += isObservationFilter ? (EventHandler)editObsFilters_Click : editPeakFilters_Click;
            tsmi.Image = MetaboliteLevels.Properties.Resources.MnuEdit;
            tsmi.ImageScaling = ToolStripItemImageScaling.None;
            button.DropDownItems.Add(tsmi);
        }

        private void toolStripDropDownButton1_DropDownOpening(object sender, EventArgs e)
        {
            ClearCmsFilter(_mnuPeakFilter);
            AddFilters(_mnuPeakFilter, core.PeakFilters, false);
        }

        private void editPeakFilters_Click(object sender, EventArgs e)
        {
            FrmBigList.ShowPeakFilters(this, core);
        }

        private void editObsFilters_Click(object sender, EventArgs e)
        {
            FrmBigList.ShowObsFilters(this, core);
        }

        void setFilter_Click(object sender, EventArgs e)
        {
            object tag = (((ToolStripMenuItem)sender).Tag);

            if (tag is ObsFilter)
            {
                _obsFilter = (ObsFilter)tag;
            }
            else
            {
                _peakFilter = (PeakFilter)tag;
            }

            UpdateScores();
        }

        private void ClearCmsFilter(ToolStripDropDownButton btn)
        {
            ArrayList toDispose = new ArrayList(btn.DropDownItems);

            foreach (ToolStripItem x in toDispose)
            {
                x.Dispose();
            }

            _cmsFilter.Items.Clear();
        }

        private void _chart_SelectionChanged(object sender, EventArgs e)
        {
            var sel = _chart.SelectedItem;

            if (sel == null || sel.Coordinate == null)
            {
                _lblSelection.Text = "No selection";
            }
            else
            {
                _lblSelection.Text = sel.Series.Name.ToString() + ": " + sel.Coordinate.Tag + " = (" + sel.X + ", " + sel.Y + ")";
            }
        }

        private void _btnNextPc_Click(object sender, EventArgs e)
        {
            _component++;
            UpdatePlot();
        }

        private void _btnPrevPc_Click(object sender, EventArgs e)
        {
            _component--;
            UpdatePlot();
        }

        private void transposeToShowObservationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _transposeToShowPeaks = false;
            UpdateScores();
        }

        private void transposeToShowPeaksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _transposeToShowPeaks = true;
            UpdateScores();
        }

        private void usetrendLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _trend = true;
            UpdateScores();
        }

        private void useAllobservationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _trend = false;
            UpdateScores();
        }

        private void _mnuObsFilter_Click(object sender, EventArgs e)
        {

        }

        private void _btnColour_Click(object sender, EventArgs e)
        {
            if (WhatPlotting == EPlotting.Peaks)
            {
                IEnumerable<Column> columns = core.Peaks.First().GetColumns(core);

                Column selected = ListValueSet.ForColumns(columns).IncludeMessage("Select the item to colour by").ShowList(this);

                if (selected != null && selected != _colourBy)
                {
                    if (selected.Id == Peak.COLNAME_CLUSTERS_UNIQUE)
                    {
                        _colourBy = null;
                    }
                    else
                    {
                        _colourBy = selected;
                    }

                    UpdatePlot();
                }
            }
            else
            {
                EColourBy colourBy = ListValueSet.ForDiscreteEnum<EColourBy>("Colour by", _colourBy2).ShowButtons(this);

                if (colourBy != _colourBy2)
                {
                    _colourBy2 = colourBy;
                    UpdatePlot();
                }
            }
        }

        private enum EColourBy
        {
            Group,
            Time,
            Replicate
        }

        private enum EPlotting
        {
            Peaks,
            Observations,
        }

        private enum EPlotSource
        {
            Scores,
            Loadings,
        }

        private EPlotting WhatPlotting
        {
            get
            {
                if (_transposeToShowPeaks)
                {
                    return (_plotSource == EPlotSource.Scores) ? EPlotting.Peaks : EPlotting.Observations;
                }
                else
                {
                    return (_plotSource == EPlotSource.Scores) ? EPlotting.Observations : EPlotting.Peaks;
                }
            }
        }

        private void _btnScores_Click(object sender, EventArgs e)
        {
            _plotSource = EPlotSource.Scores;
            UpdatePlot();
        }

        private void _btnLoadings_Click(object sender, EventArgs e)
        {
            _plotSource = EPlotSource.Loadings;
            UpdatePlot();
        }
    }
}
