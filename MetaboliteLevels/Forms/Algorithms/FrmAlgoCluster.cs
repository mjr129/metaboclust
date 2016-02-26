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
        private EditableComboBox<ClustererBase> _ecbMethod;
        private EditableComboBox<MetricBase> _ecbMeasure;
        private ConditionBox<EClustererStatistics> _cbStatistics;
        private readonly bool _readOnly;


        internal static ConfigurationClusterer Show(Form owner, Core core, ConfigurationClusterer def, bool readOnly, bool hideOptimise)
        {
            using (FrmAlgoCluster frm = new FrmAlgoCluster(core, def, readOnly, hideOptimise))
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
        }

        private FrmAlgoCluster(Core core, ConfigurationClusterer def, bool readOnly, bool hideOptimise)
            : this()
        {
            _core = core;
            _ecbPeakFilter = DataSet.ForPeakFilter(core).CreateComboBox(_lstPeakFilter, _btnPeakFilter,  ENullItemName.All);
            _ecbObsFilter = DataSet.ForObsFilter(core).CreateComboBox(_lstObsFilter, _btnObsFilter,  ENullItemName.All);
            _ecbMethod = DataSet.ForClustererAlgorithms(core).CreateComboBox(_lstMethod, _btnNewStatistic, ENullItemName.None);
            _ecbMeasure = DataSet.ForMetricAlgorithms(core).CreateComboBox(_lstMeasure, _btnNewDistance, ENullItemName.None);
            _cbStatistics = DataSet.ForFlagsEnum<EClustererStatistics>("Cluster Statistics").CreateConditionBox(_txtStatistics, _btnSetStatistics);
            _readOnly = readOnly;

            if (def != null)
            {
                // Name
                _txtName.Text = def.OverrideDisplayName;

                // Comment
                _comment = def.Comment;

                // Method
                _ecbMethod.SelectedItem = def.Cached;

                // Params
                _txtParams.Text = AlgoParameterCollection.ParamsToReversableString(def.Args.Parameters, core);

                // PeakFilter
                _ecbPeakFilter.SelectedItem = def.Args.PeakFilter;

                // Distance
                _ecbMeasure.SelectedItem = def.Args.Distance != null ? def.Args.Distance.Cached : null;

                // Distance params
                _txtMeasureParams.Text = StringHelper.ArrayToString(def.Args.Distance?.Args.Parameters);

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
                UiControls.MakeReadOnly(this);

                _btnParameterOptimiser.Visible = false;
                _btnComment.Enabled = true;
                ctlTitleBar1.Text = "View Clustering Algorithm";
            }
            else if (def != null)
            {
                ctlTitleBar1.Text = "Edit Clustering Algorithm";
            }
            else
            {
                ctlTitleBar1.Text = "New Clustering Algorithm";
            }

            CheckAndChange(null, null);

            UiControls.CompensateForVisualStyles(this);

            if (hideOptimise)
            {
                _btnParameterOptimiser.Visible = false;
                _btnOk.Text = "Continue";
            }
        }

        private ConfigurationClusterer GetSelection()
        {
            ClustererBase sel = (ClustererBase)this._ecbMethod.SelectedItem;
            EAlgoSourceMode src;
            PeakFilter peakFilter;
            ObsFilter obsFilter;
            string title;

            errorProvider1.Clear();

            // Selection
            if (sel == null)
            {
                errorProvider1.SetError(_ecbMethod._box, "Select a method");
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

            dMet = (MetricBase)_ecbMeasure.SelectedItem;

            if (dMet == null)
            {
                errorProvider1.SetError(_ecbMeasure._box, "Specify a distance measure");
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
            ClustererBase stat = (ClustererBase)_ecbMethod.SelectedItem;
            MetricBase met = _ecbMeasure.SelectedItem as MetricBase;
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
            _ecbMeasure.Enabled = distanceVisible;
            linkLabel1.Visible = distanceVisible && !stat.SupportsDistanceMetrics;

            // Distance params
            bool distParamsVisible = performanceVisible && met != null && met.Parameters != null && met.Parameters.HasCustomisableParams;
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

        private void _btnTrendHelp_Click(object sender, EventArgs e)
        {
            FrmMsgBox.ShowHelp(this, "Default Trend", UiControls.GetDefaultTrendMessage(_core));
        }

        private void _btnEditParameters_Click(object sender, EventArgs e)
        {
            ClustererBase stat = (ClustererBase)_ecbMethod.SelectedItem;
            FrmEditParameters.Show(stat, _txtParams, _core, _readOnly);
        }

        private void _btnEditDistanceParameters_Click(object sender, EventArgs e)
        {
            var dMet = (MetricBase)_ecbMeasure.SelectedItem;
            FrmEditParameters.Show(dMet, _txtParams, _core, _readOnly);
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
