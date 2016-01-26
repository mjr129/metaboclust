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
using MetaboliteLevels.Algorithms.Statistics.Configurations;

namespace MetaboliteLevels.Forms.Editing
{
    /// <summary>
    /// Shows a PCA plot.
    /// </summary>
    public partial class FrmPca : Form
    {
        private Core _core;

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
            this._core = core;

            _peakFilter = PeakFilter.Empty;
            _obsFilter = ObsFilter.Empty;

            _selectedCorrection = core.Corrections.Last();

            UpdateScores();
        }

        private void UpdateScores()
        {
            StringBuilder ft = new StringBuilder();

            var peaks = _peakFilter.Test(_core.Peaks).Passed;

            double[,] valueMatrix = null;

            _mnuTranspose.Text = _transposeToShowPeaks ? "Peaks" : "Observations";
            _mnuTrend.Text = _trend ? "Trend" : "Full data";
            _mnuObsFilter.Text = _obsFilter.ToString();
            _mnuPeakFilter.Text = _peakFilter.ToString();
            _mnuCorrections.Text = _selectedCorrection != null ? _selectedCorrection.DisplayName : "Original data";

            int corIndex = _core.Corrections.IndexOf(_selectedCorrection);

            IEnumerable conds;
            IEnumerable<int> which;

            if (_trend)
            {
                which = _core.Conditions.Which(_obsFilter.Test);
                conds = _core.Conditions.In(which);
            }
            else
            {
                which = _core.Observations.Which(_obsFilter.Test);
                conds = _core.Observations.In(which);
            }

            for (int peakIndex = 0; peakIndex < peaks.Count; peakIndex++)
            {
                IEnumerable<double> vals;
                Peak peak = peaks[peakIndex];

                var src = corIndex == -1 ? peak.OriginalObservations : peak.CorrectionChain[corIndex];
                          
                if (_trend)
                {
                    vals = src.Trend.In(which);
                }
                else
                {
                    vals = src.Raw.In(which);
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
            _lblPcNumber.Text = "PC" + (_component + 1).ToString() + " / " + plotPoints.GetLength(1);

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
        private ConfigurationCorrection _selectedCorrection;

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
            AddFilters(_mnuObsFilter, _core.ObsFilters, true);
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
            AddFilters(_mnuPeakFilter, _core.PeakFilters, false);
        }

        private void editPeakFilters_Click(object sender, EventArgs e)
        {
            FrmBigList.ShowPeakFilters(this, _core);
        }

        private void editObsFilters_Click(object sender, EventArgs e)
        {
            FrmBigList.ShowObsFilters(this, _core);
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
                _btnMarkAsOutlier.Text = "Mark as outlier";
                _btnMarkAsOutlier.Enabled = false;
            }
            else
            {
                _lblSelection.Text = sel.Series.Name.ToString() + ": " + sel.Coordinate.Tag + " = (" + sel.X + ", " + sel.Y + ")";
                _btnMarkAsOutlier.Text = sel.Series.Name.ToString() + ": " + sel.Coordinate.Tag;
                _btnMarkAsOutlier.Enabled = true;
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

        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fileName = UiControls.BrowseForFile(this, null, UiControls.EFileExtension.PngOrEmf, FileDialogMode.SaveAs, UiControls.EInitialFolder.SavedImages);

            if (fileName != null)
            {
                try
                {
                    _chart.DrawToBitmap().Save(fileName);
                }
                catch (Exception ex)
                {
                    FrmMsgBox.ShowError(this, ex);
                }
            }
        }

        private void _btnMarkAsOutlier_Click(object sender, EventArgs e)
        {
            object item = _chart.SelectedItem.Coordinate.Tag;

            Peak peak = item as Peak;

            bool safeToReplace = false;

            if (peak != null)
            {
                List<PeakFilter.Condition> filters = new List<PeakFilter.Condition>(_peakFilter.Conditions.Cast<PeakFilter.Condition>());
                bool needsNewFilter = true;

                foreach (PeakFilter.Condition filter in filters)
                {
                    PeakFilter.ConditionPeak klalkq = filter as PeakFilter.ConditionPeak;

                    if (klalkq != null
                        && klalkq.CombiningOperator == Filter.ELogicOperator.And
                        && klalkq.Negate == false
                        && klalkq.PeaksOp == Filter.EElementOperator.IsNot)
                    {
                        needsNewFilter = false;

                        List<Peak> strong;

                        if (!klalkq.Peaks.TryGetStrong(out strong))
                        {
                            FrmMsgBox.ShowError(this, "Couldn't modify current filter because it contains expired data.");
                            return;
                        }

                        filters.Remove(klalkq);
                        filters.Add(new PeakFilter.ConditionPeak(Filter.ELogicOperator.And, false, strong.ConcatSingle(peak), Filter.EElementOperator.IsNot));
                        safeToReplace = filters.Count == 1;
                        break;
                    }
                }

                if (needsNewFilter)
                {
                    filters.Add(new PeakFilter.ConditionPeak(Filter.ELogicOperator.And, false, new[] { peak }, Filter.EElementOperator.IsNot));
                }

                PeakFilter newFilter = new PeakFilter(null, null, filters);

                if (safeToReplace)
                {
                    _core.SetPeakFilters(_core.PeakFiltersComplete.ReplaceSingle(_peakFilter, newFilter).ToArray());
                }
                else
                {
                    _core.SetPeakFilters(_core.PeakFiltersComplete.ConcatSingle(newFilter).ToArray());
                }

                _peakFilter = newFilter;

                UpdateScores();
                return;
            }

            ObservationInfo observationInfo = item as ObservationInfo;

            if (observationInfo != null)
            {
                bool needsNewFilter2 = true;

                List<ObsFilter.Condition> filters2 = new List<ObsFilter.Condition>(_obsFilter.Conditions.Cast<ObsFilter.Condition>());

                foreach (ObsFilter.ConditionObservation fikaklq in filters2)
                {
                    ObsFilter.ConditionObservation qkklqq = fikaklq as ObsFilter.ConditionObservation;

                    if (qkklqq != null
                        && qkklqq.CombiningOperator == Filter.ELogicOperator.And
                         && qkklqq.Negate == false
                         && qkklqq.Operator == Filter.EElementOperator.IsNot)
                    {
                        needsNewFilter2 = false;

                        filters2.Remove(qkklqq);
                        filters2.Add(new ObsFilter.ConditionObservation(Filter.ELogicOperator.And, false, Filter.EElementOperator.IsNot, qkklqq.Possibilities.ConcatSingle(observationInfo)));
                        safeToReplace = filters2.Count == 1;
                        break;
                    }
                }

                if (needsNewFilter2)
                {
                    filters2.Add(new ObsFilter.ConditionObservation(Filter.ELogicOperator.And, false, Filter.EElementOperator.IsNot, new[] { observationInfo }));
                }

                ObsFilter obsFilter = new ObsFilter(null, null, filters2);

                if (safeToReplace)
                {
                    _core.SetObsFilters(_core.ObsFiltersComplete.ReplaceSingle(_obsFilter, obsFilter).ToArray());
                }
                else
                {
                    _core.SetObsFilters(_core.ObsFiltersComplete.ConcatSingle(obsFilter).ToArray());
                }

                _obsFilter = obsFilter;

                UpdateScores();
            }


            ConditionInfo conditionInfo = item as ConditionInfo;

            if (conditionInfo != null)
            {
                List<ObsFilter.Condition> filters3 = new List<ObsFilter.Condition>(_obsFilter.Conditions.Cast<ObsFilter.Condition>());

                bool needsNewFilter3 = true;

                foreach (ObsFilter.Condition sklqklklq in filters3)
                {
                    ObsFilter.ConditionCondition wklqklklq = sklqklklq as ObsFilter.ConditionCondition;

                    if (wklqklklq != null
                        && wklqklklq.CombiningOperator == Filter.ELogicOperator.And
                         && wklqklklq.Negate == false
                         && wklqklklq.Operator == Filter.EElementOperator.IsNot)
                    {
                        needsNewFilter3 = false;

                        filters3.Remove(wklqklklq);
                        filters3.Add(new ObsFilter.ConditionCondition(Filter.ELogicOperator.And, false, Filter.EElementOperator.IsNot, wklqklklq.Possibilities.ConcatSingle(conditionInfo)));
                        safeToReplace = filters3.Count == 1;
                        break;
                    }
                }

                if (needsNewFilter3)
                {
                    filters3.Add(new ObsFilter.ConditionObservation(Filter.ELogicOperator.And, false, Filter.EElementOperator.IsNot, new[] { observationInfo }));
                }

                ObsFilter obsFilter = new ObsFilter(null, null, filters3);

                if (safeToReplace)
                {
                    _core.SetObsFilters(_core.ObsFiltersComplete.ReplaceSingle(_obsFilter, obsFilter).ToArray());
                }
                else
                {
                    _core.SetObsFilters(_core.ObsFiltersComplete.ConcatSingle(obsFilter).ToArray());
                }

                _obsFilter = obsFilter;

                UpdateScores();
            }
        }

        private void _mnuTrend_DropDownOpening(object sender, EventArgs e)
        {

        }

        private void _mnuCorrections_correction_Click(object sender, EventArgs e)
        {
            _selectedCorrection = (ConfigurationCorrection)((ToolStripMenuItem)sender).Tag;

            UpdateScores();
        }

        private void _mnuCorrections_DropDownOpening(object sender, EventArgs e)
        {
            _mnuCorrections.DropDownItems.Clear();

            foreach (ConfigurationCorrection correction in _core.Corrections)
            {
                ToolStripMenuItem tsmi = new ToolStripMenuItem(correction.ToString()) { Tag = correction };
                tsmi.Click += _mnuCorrections_correction_Click;

                _mnuCorrections.DropDownItems.Add(tsmi);
            }
        }

        private void _btnColour_DropDownOpening(object sender, EventArgs e)
        {
            _btnColour.DropDownItems.Clear();

            if (WhatPlotting == EPlotting.Peaks)
            {
                IEnumerable<Column> columns = _core.Peaks.First().GetColumns(_core);

                foreach (Column Column in columns)
                {
                    ToolStripMenuItem tsmi = new ToolStripMenuItem(Column.Id) { Tag = Column };
                    tsmi.Click += _btnColour_column_Click;

                    _btnColour.DropDownItems.Add(tsmi);
                }
            }
            else
            {
                IEnumerable<EColourBy> colourBy = Enum.GetValues(typeof(EColourBy)).Cast<EColourBy>();

                foreach (EColourBy en in colourBy)
                {
                    ToolStripMenuItem tsmi = new ToolStripMenuItem(en.ToUiString()) { Tag = en };
                    tsmi.Click += _btnColour_colourBy_Click;

                    _btnColour.DropDownItems.Add(tsmi);
                }
            }
        }

        private void _btnColour_colourBy_Click(object sender, EventArgs e)
        {
            EColourBy colourBy = (EColourBy)((ToolStripMenuItem)sender).Tag;

            if (colourBy != _colourBy2)
            {
                _colourBy2 = colourBy;
                UpdatePlot();
            }
        }

        private void _btnColour_column_Click(object sender, EventArgs e)
        {
            Column selected = (Column)((ToolStripMenuItem)sender).Tag;

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
    }
}
