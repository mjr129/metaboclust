using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using MetaboliteLevels.Algorithms;
using MetaboliteLevels.Algorithms.Statistics;
using MetaboliteLevels.Algorithms.Statistics.Arguments;
using MetaboliteLevels.Algorithms.Statistics.Clusterers;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Algorithms.Statistics.Metrics;
using MetaboliteLevels.Algorithms.Statistics.Statistics;
using MetaboliteLevels.Controls;
using MetaboliteLevels.Data.DataInfo;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Forms.Editing;
using MetaboliteLevels.Forms.Generic;
using MetaboliteLevels.Settings;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Data.Visualisables;
using System.Text;
using MetaboliteLevels.Algorithms.Statistics.Results;
using MetaboliteLevels.Data.General;

namespace MetaboliteLevels.Forms.Algorithms
{
    public partial class FrmAlgoCluster : Form
    {
        private Core _core;
        private string _comment;
        private EditableComboBox<PeakFilter> _ecbPeakFilter;
        private EditableComboBox<ObsFilter> _ecbObsFilter;
        private ConditionBox<EClustererStatistics> _cbStatistics;

        internal static ConfigurationClusterer Show(Form owner, Core core, ConfigurationClusterer def, bool readOnly, Cluster toBreakUp, bool hideOptimise)
        {
            using (FrmAlgoCluster frm = new FrmAlgoCluster(core, def, readOnly, toBreakUp, hideOptimise))
            {
                if (UiControls.ShowWithDim(owner, frm) == DialogResult.OK)
                {
                    UiControls.Assert(!readOnly);
                    return frm.GetSelection();
                }

                return null;
            }
        }

        private FrmAlgoCluster()
        {
            InitializeComponent();
            UiControls.SetIcon(this);

            RebuildUsing(null);
            RebuildDistanceUsing(null);
        }

        private FrmAlgoCluster(Core core, ConfigurationClusterer def, bool readOnly, Cluster toBreakUp, bool hideOptimise)
            : this()
        {
            _core = core;
            _ecbPeakFilter = EditableComboBox.ForPeakFilter(_lstPeakFilter, _btnPeakFilter, core);
            _ecbObsFilter = EditableComboBox.ForObsFilter(_lstObsFilter, _btnObsFilter, core);
            _cbStatistics = ListValueSet.ForFlagsEnum<EClustererStatistics>("Cluster Statistics").CreateConditionBox(_txtStatistics, _btnSetStatistics);

            if (toBreakUp != null)
            {
                FrmMsgBox.ShowError(this, "Breaking up clusters is not yet supported.");
            }

            if (def != null)
            {
                // Name
                _txtName.Text = def.OverrideDisplayName;

                // Comment
                _comment = def.Comment;

                // Method
                _lstMethod.SelectedItem = def.Cached;

                // Params
                _txtParams.Text = AlgoParameterCollection.ParamsToReversableString(def.Args.Parameters, core);

                // PeakFilter
                _ecbPeakFilter.SelectedItem = def.Args.PeakFilter;

                // Distance
                _lstMeasure.SelectedItem = def.Args.Distance != null ? def.Args.Distance.Cached : null;

                // Distance params
                _txtMeasureParams.Text = StringHelper.ArrayToString(def.Args.Distance.Args.Parameters);

                // Suppress distance
                _cbStatistics.SelectedItems = EnumHelper.SplitEnum<EClustererStatistics>(def.Args.Statistics);

                // Input vector
                _radObs.Checked = def.Args.SourceMode == EAlgoSourceMode.Full;
                _radTrend.Checked = def.Args.SourceMode == EAlgoSourceMode.Trend;

                // ObsFilter
                _ecbObsFilter.SelectedItem = def.Args.ObsFilter;

                // Seperate groups
                _chkSepGroups.Checked = def.Args.SplitGroups;
            }

            if (readOnly)
            {
                UiControls.EnumerateControls<TextBox>(this, z => z.ReadOnly = true);
                UiControls.EnumerateControls<CheckBox>(this, z => z.AutoCheck = false);
                UiControls.EnumerateControls<RadioButton>(this, z => z.AutoCheck = false);
                UiControls.EnumerateControls<Button>(this, z => z.Enabled = false);
                _lstMethod.Enabled = false;

                _btnComment.Enabled = true;
                _btnOk.Visible = false;
                _btnCancel.Enabled = true;
                _btnCancel.Text = "Close";

                ctlTitleBar1.Text = "View Clustering Algorithm";
            }
            else if (def != null)
            {
                ctlTitleBar1.Text = "Edit Clustering Algorithm";
            }
            else if (toBreakUp != null)
            {
                ctlTitleBar1.Text = "New Clustering Algorithm";
                ctlTitleBar1.SubText = "Specify the algorithm used to break up " + toBreakUp.DisplayName;
            }
            else
            {
                ctlTitleBar1.Text = "New Clustering Algorithm";
            }

            CheckAndChange(null, null);

            UiControls.CompensateForVisualStyles(this);

            if (hideOptimise)
            {
                ctlButton1.Visible = false;
                _btnOk.Text = "Continue";
            }
        }

        private ConfigurationClusterer GetSelection()
        {
            ClustererBase sel = (ClustererBase)this._lstMethod.SelectedItem;
            EAlgoSourceMode src;
            PeakFilter peakFilter;
            ObsFilter obsFilter;
            string title;

            errorProvider1.Clear();

            // Selection
            if (sel == null)
            {
                errorProvider1.SetError(_lstMethod, "Select a method");
                return null;
            }

            // Title / comments
            title = string.IsNullOrWhiteSpace(_txtName.Text) ? null : _txtName.Text;

            // Parameters
            object[] parameters;

            if (sel.Parameters.HasCustomisableParams)
            {
                if (!sel.Parameters.TryStringToParams(_core, _txtParams.Text, out parameters))
                {
                    errorProvider1.SetError(_txtParams, "Enter a set of valid parameters for your selected method");
                    return null;
                }
            }
            else
            {
                parameters = null;
            }

            // Peak filter
            if (_ecbPeakFilter.HasSelection)
            {
                peakFilter = _ecbPeakFilter.SelectedItem;
            }
            else
            {
                errorProvider1.SetError(_ecbPeakFilter._box, "Select a valid observation filter");
                return null;
            }

            // Suppress metric
            if (!_cbStatistics.SelectionValid)
            {
                errorProvider1.SetError(_txtStatistics, "Select a valid set of statistics");
                return null;
            }

            EClustererStatistics suppressMetric = (EClustererStatistics)_cbStatistics.SelectedItems.Cast<int>().Sum();

            // Distance metric
            MetricBase dMet;

            dMet = (MetricBase)_lstMeasure.SelectedItem;

            if (dMet == null)
            {
                errorProvider1.SetError(_lstMeasure, "Specify a distance measure");
                return null;
            }

            // Distance metric params
            object[] dMetParams;

            if (dMet != null && dMet.Parameters.HasCustomisableParams)
            {
                if (!dMet.Parameters.TryStringToParams(_core, _txtMeasureParams.Text, out dMetParams))
                {
                    errorProvider1.SetError(_txtMeasureParams, "Specify a set of valid parameters for your selected distance measure");
                    return null;
                }
            }
            else
            {
                dMetParams = null;
            }

            // Obs source
            if (!sel.SupportsObservationFilters)
            {
                src = EAlgoSourceMode.None;
            }
            else if (this._radObs.Checked)
            {
                src = EAlgoSourceMode.Full;
            }
            else if (this._radTrend.Checked)
            {
                src = EAlgoSourceMode.Trend;
            }
            else
            {
                errorProvider1.SetError(_radObs, "Select a valid input vector");
                return null;
            }

            // Vector A
            if (!sel.SupportsObservationFilters)
            {
                obsFilter = null;
            }
            else if (this._ecbObsFilter.HasSelection)
            {
                obsFilter = _ecbObsFilter.SelectedItem;
            }
            else
            {
                errorProvider1.SetError(_ecbObsFilter._box, "Select a valid observation filter");
                return null;
            }

            // Result
            ConfigurationMetric df = dMet != null ? new ConfigurationMetric(dMet.Name, string.Empty, dMet.Id, new ArgsMetric(dMetParams)) : null;
            ArgsClusterer args = new ArgsClusterer(peakFilter, df, src, obsFilter, _chkSepGroups.Checked, suppressMetric, parameters);
            ConfigurationClusterer result = new ConfigurationClusterer(title, _comment, sel.Id, args);
            return result;
        }

        private void CheckAndChange(object sender, EventArgs e)
        {
            ClustererBase stat = (ClustererBase)_lstMethod.SelectedItem;
            MetricBase met = _lstMeasure.SelectedItem as MetricBase;
            object[] tmp;

            // Stat selected?
            bool s = stat != null;

            // Stat has params?
            bool paramsVisible = s && stat.Parameters.HasCustomisableParams;
            _txtParams.Enabled = paramsVisible;
            _btnEditParameters.Enabled = paramsVisible;
            _lblParams.Enabled = paramsVisible;
            _lblParams.Text = paramsVisible ? stat.Parameters.ParamNames() : "Parameters";

            // Stat is valid?
            bool peakFilterVisible = s && (!stat.Parameters.HasCustomisableParams || stat.Parameters.TryStringToParams(_core, _txtParams.Text, out tmp));
            _lblPeaks.Enabled = peakFilterVisible;
            _ecbPeakFilter.Enabled = peakFilterVisible;

            // Performance
            bool performanceVisible = peakFilterVisible;
            _cbStatistics.Enabled = performanceVisible;

            // Distance
            bool distanceVisible = performanceVisible;
            _lblMeasure2.Enabled = distanceVisible;
            _lstMeasure.Enabled = distanceVisible;
            _btnNewDistance.Enabled = distanceVisible;
            linkLabel1.Visible = distanceVisible && !stat.SupportsDistanceMetrics;

            // Distance params
            bool distParamsVisible = performanceVisible && met != null && met.Parameters.HasCustomisableParams;
            _txtMeasureParams.Enabled = distParamsVisible;
            _btnEditDistanceParameters.Enabled = distParamsVisible;
            _lblMeasureParams.Enabled = distParamsVisible;
            _lblMeasureParams.Text = distParamsVisible ? met.Parameters.ParamNames() : "Parameters";

            // Input vector
            bool obsFilterVisible = peakFilterVisible && stat.SupportsObservationFilters;
            _lblApply.Visible = obsFilterVisible;
            _radObs.Enabled = obsFilterVisible;
            _radTrend.Enabled = obsFilterVisible;
            _btnTrendHelp.Enabled = obsFilterVisible;

            // Obs filter
            bool u = obsFilterVisible && (_radObs.Checked || _radTrend.Checked);
            _lblAVec.Enabled = u;
            _ecbObsFilter.Enabled = u;
            _chkSepGroups.Enabled = u;

            // Is OK?
            Check(null, null);
        }

        private void Check(object sender, EventArgs e)
        {
            try
            {
                _btnOk.Enabled = (GetSelection() != null);
            }
            catch
            {
                _btnOk.Enabled = false;
            }
        }

        private void _btnNewStatistic_Click(object sender, EventArgs e)
        {
            string fn = FrmRScript.Show(this, Text, "New Clustering Algorithm", ClustererScript.INPUTS, UiControls.EInitialFolder.FOLDER_CLUSTERERS, @"RScript Editor", FrmRScript.SaveMode.ReturnFileName | FrmRScript.SaveMode.SaveToFolderMandatory);

            if (fn != null)
            {
                Algo.Instance.Rebuild();
                RebuildUsing(Algo.GetId(UiControls.EInitialFolder.FOLDER_CLUSTERERS, fn));
            }
        }

        private void RebuildDistanceUsing(string selectedId)
        {
            _lstMeasure.Items.Clear();
            _lstMeasure.Items.AddRange(Algo.Instance.Metrics.ToArray());

            if (selectedId != null)
            {
                _lstMeasure.SelectedItem = Algo.Instance.All.Get(selectedId);
            }
        }

        private void RebuildUsing(string selectedId)
        {
            _lstMethod.Items.Clear();
            _lstMethod.Items.AddRange(Algo.Instance.Clusterers.ToArray());

            if (selectedId != null)
            {
                _lstMethod.SelectedItem = Algo.Instance.All.Get(selectedId);
            }
        }

        private void _btnTrendHelp_Click(object sender, EventArgs e)
        {
            FrmMsgBox.ShowHelp(this, "Default Trend", UiControls.GetDefaultTrendMessage(_core));
        }

        private void _btnEditParameters_Click(object sender, EventArgs e)
        {
            ClustererBase stat = (ClustererBase)_lstMethod.SelectedItem;
            FrmEditParameters.Show(stat, _txtParams, _core);
        }

        private void _btnEditDistanceParameters_Click(object sender, EventArgs e)
        {
            var dMet = (MetricBase)_lstMeasure.SelectedItem;
            FrmEditParameters.Show(dMet, _txtParams, _core);
        }

        private void _btnNewDistance_Click(object sender, EventArgs e)
        {
            string fn = FrmRScript.Show(this, Text, "New Metric", MetricScript.INPUTS, UiControls.EInitialFolder.FOLDER_METRICS, @"RScript Editor", FrmRScript.SaveMode.ReturnFileName | FrmRScript.SaveMode.SaveToFolderMandatory);

            if (fn != null)
            {
                Algo.Instance.Rebuild();
                RebuildDistanceUsing(Algo.GetId(UiControls.EInitialFolder.FOLDER_METRICS, fn));
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmMsgBox.ShowInfo(this, "Distance Metric Not Supported",
                               "This clustering algorithm either does not use a distance metric, or it uses its own internal metric. The metric you specify will only be used for clustering performance (e.g. silhouette width) calculations. Disabling performance evaluations may noticeably decrease cluster generation time in this case.");
        }

        private void ctlButton1_Click(object sender, EventArgs e)
        {
            var sel = this.GetSelection();

            if (sel == null)
            {
                FrmMsgBox.ShowError(this, "Parameter Optimiser", "A valid configuration must be selected prior to parameter optimisation.");
                return;
            }

            FrmEvaluateClustering.Show(this, _core, sel);

            return;
        }

        private void _btnComment_Click(object sender, EventArgs e)
        {
            string newComment = FrmInputLarge.Show(this, Text, "Edit Comments", "Enter comments for your algorithm", _comment);

            if (newComment != null)
            {
                _comment = newComment;
            }
        }
    }
}
