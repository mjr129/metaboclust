using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MetaboliteLevels.Algorithms;
using MetaboliteLevels.Algorithms.Statistics;
using MetaboliteLevels.Algorithms.Statistics.Arguments;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Algorithms.Statistics.Metrics;
using MetaboliteLevels.Algorithms.Statistics.Statistics;
using MetaboliteLevels.Controls;
using MetaboliteLevels.Data.DataInfo;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Forms.Generic;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Settings;
using MetaboliteLevels.Forms.Editing;

namespace MetaboliteLevels.Forms.Algorithms
{
    public partial class FrmAlgoStatistic : Form
    {
        private readonly Core _core;
        private readonly bool _readOnly;

        private string _comments;
        private Peak _previewPeak;
        private EditableComboBox<Settings.ObsFilter> _ecbFilter1;
        private EditableComboBox<Settings.ObsFilter> _ecbFilter2;

        internal static ConfigurationStatistic Show(Form owner, ConfigurationStatistic def, Core core, bool readOnly, Peak comparison)
        {
            if (ShowCannotEditError(owner, def)) return null;

            using (FrmAlgoStatistic frm = new FrmAlgoStatistic(core, def, FrmMain.SearchForSelectedPeak(owner), readOnly, comparison))
            {
                if (UiControls.ShowWithDim(owner, frm) == DialogResult.OK)
                {
                    return frm.GetSelection();
                }

                return null;
            }
        }

        internal static bool ShowCannotEditError(Form owner, ConfigurationBase def)
        {
            if (def != null && !def.IsAvailable)
            {
                FrmMsgBox.ShowWarning(owner, "Missing algorithm",
                                      "This algorithm uses an algorithm not installed on this machine \"" + def.Id
                                      + "\"  and its parameters cannot be modified.");

                return true;
            }
            return false;
        }

        private ConfigurationStatistic GetSelection()
        {
            StatisticBase sel = NamedItem<StatisticBase>.Extract(this._lstMethod.SelectedItem);
            EAlgoSourceMode src;
            EAlgoInputBSource bsrc;
            ObsFilter filter1;
            ObsFilter filter2;
            Peak bpeak;
            string title;

            // Selection
            if (sel == null)
            {
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
                    return null;
                }
            }
            else
            {
                parameters = null;
            }

            if (!sel.SupportsInputFilters)
            {
                filter1 = null;
                filter2 = null;
                src = EAlgoSourceMode.None;
                bsrc = EAlgoInputBSource.None;
                bpeak = null;
            }
            else
            {
                // Obs source
                if (this._radObs.Checked)
                {
                    src = EAlgoSourceMode.Full;
                }
                else if (this._radTrend.Checked)
                {
                    src = EAlgoSourceMode.Trend;
                }
                else
                {
                    //throw new InvalidOperationException("Missing SourceMode");
                    return null;
                }

                // Vector A
                if (_ecbFilter1.HasSelection)
                {
                    filter1 = _ecbFilter1.SelectedItem;
                }
                else
                {
                    return null;
                }

                // Vector B
                if (!sel.IsMetric)
                {
                    // If the stat isn't comparing anything there is nothing to set
                    bsrc = EAlgoInputBSource.None;
                    filter2 = null;
                    bpeak = null;
                }
                else if (_radBCorTime.Checked)
                {
                    // Use time is checked then we are comparing against time - but use the same X points
                    bsrc = EAlgoInputBSource.Time;
                    filter2 = filter1;
                    bpeak = null;
                }
                else if (this._radBDiffPeak.Checked)
                {
                    // Use alt peak is checked then we are comparing against another peak - use the same X points
                    bsrc = EAlgoInputBSource.AltPeak;
                    bpeak = NamedItem<Peak>.Extract(_lstDiffPeak.SelectedItem);

                    if (bpeak == null)
                    {
                        return null;
                    }

                    filter2 = filter1;
                }
                else if (this._radSamePeak.Checked)
                {
                    // Otherwise we are comparing against the same peak, get what we are comparing against
                    bsrc = EAlgoInputBSource.SamePeak;
                    bpeak = null;

                    if (this._ecbFilter2.HasSelection)
                    {
                        filter2 = this._ecbFilter2.SelectedItem;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    // What are we comparing against?
                    return null;
                }
            }

            // Result
            ArgsStatistic args = new ArgsStatistic(src, filter1, bsrc, filter2, bpeak, parameters);
            ConfigurationStatistic result = new ConfigurationStatistic(title, _comments, sel.Id, args);
            return result;
        }

        public FrmAlgoStatistic()
        {
            InitializeComponent();
            UiControls.SetIcon(this);

            _lblPreviewTitle.BackColor = UiControls.PreviewBackColour;
            _lblPreviewTitle.ForeColor = UiControls.PreviewForeColour;
            _flpPreviewButtons.BackColor = UiControls.PreviewBackColour;
            _flpPreviewButtons.ForeColor = UiControls.PreviewForeColour;
        }

        private void Validate(object sender, EventArgs e)
        {
            bool v;

            try
            {
                ConfigurationStatistic sel = GetSelection();
                GeneratePreview(sel);
                v = sel != null;
            }
            catch
            {
                v = false;
            }

            _btnOk.Enabled = v;
            _tlpPreivew.Visible = v;
        }

        private FrmAlgoStatistic(Core core, ConfigurationStatistic defaultSelection, Peak defaultPeak, bool readOnly, Peak comparison)
            : this()
        {
            this._core = core;

            _previewPeak = defaultPeak;

            _ecbFilter1 = EditableComboBox.ForObsFilter(_lstFilter1, _btnFilter1, core);
            _ecbFilter2 = EditableComboBox.ForObsFilter(_lstFilter2, _btnFilter2, core);

            _lstDiffPeak.Items.AddRange(NamedItem.GetRange(_core.Peaks, z => z.DisplayName).ToArray());
            _lstDiffPeak.SelectedItem = defaultPeak;

            RebuildUsing(null);

            if (comparison != null)
            {
                _radBDiffPeak.Checked = true;
                _lstDiffPeak.SelectedItem = comparison;
                _radBDiffPeak.Enabled = false;
                _radSamePeak.Enabled = false;
                _radBCorTime.Enabled = false;
                _radBDiffPeak.Enabled = false;
            }

            if (defaultSelection != null)
            {
                _txtName.Text = defaultSelection.OverrideDisplayName;
                ctlTitleBar1.SubText = defaultSelection.AlgoName;
                _comments = defaultSelection.Comment;
                _lstMethod.SelectedItem = defaultSelection.Cached;
                _txtParams.Text = AlgoParameterCollection.ParamsToReversableString(defaultSelection.Args.Parameters, _core);

                _radObs.Checked = defaultSelection.Args.SourceMode == EAlgoSourceMode.Full;
                _radTrend.Checked = defaultSelection.Args.SourceMode == EAlgoSourceMode.Trend;
                _radBCorTime.Checked = defaultSelection.Args.VectorBSource == EAlgoInputBSource.Time;
                _radBDiffPeak.Checked = defaultSelection.Args.VectorBSource == EAlgoInputBSource.AltPeak;
                _radSamePeak.Checked = defaultSelection.Args.VectorBSource == EAlgoInputBSource.SamePeak;

                _ecbFilter1.SelectedItem = defaultSelection.Args.VectorAConstraint;
                _ecbFilter2.SelectedItem = defaultSelection.Args.VectorBConstraint;

                if (defaultSelection.Args.VectorBPeak != null)
                {
                    _lstDiffPeak.SelectedItem = defaultSelection.Args.VectorBPeak;
                }
            }

            SetStatuses();

            _readOnly = readOnly;

            if (readOnly)
            {
                UiControls.MakeReadOnly(this, _tlpPreivew);

                _btnComment.Enabled = true;   

                ctlTitleBar1.Text = "View Statistic";
            }
            else if (defaultSelection != null)
            {
                ctlTitleBar1.Text = "Edit Statistic";
            }
            else if (comparison != null)
            {
                ctlTitleBar1.Text = "New Comparison Statistic";
                ctlTitleBar1.SubText = "Select the algorithm for the comparison against " + comparison.DisplayName;
            }
            else
            {
                ctlTitleBar1.Text = "New Statistic";
            }

            UiControls.CompensateForVisualStyles(this);
        }

        private void _btnNewStatistic_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(_btnNewStatistic, 0, _btnNewStatistic.Height);
        }

        private void newStatisticToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fn = FrmRScript.Show(this, Text, "New Statistic", StatisticScript.INPUTS, UiControls.EInitialFolder.FOLDER_STATISTICS, @"RScript Editor", FrmRScript.SaveMode.ReturnFileName | FrmRScript.SaveMode.SaveToFolderMandatory);

            if (fn != null)
            {
                Algo.Instance.Rebuild();
                RebuildUsing(Algo.GetId(UiControls.EInitialFolder.FOLDER_STATISTICS, fn));
            }
        }

        private void newMetricToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fn = FrmRScript.Show(this, Text, "New Metric", MetricScript.INPUTS, UiControls.EInitialFolder.FOLDER_METRICS, @"RScript Editor", FrmRScript.SaveMode.ReturnFileName | FrmRScript.SaveMode.SaveToFolderMandatory);

            if (fn != null)
            {
                Algo.Instance.Rebuild();
                RebuildUsing(Algo.GetId(UiControls.EInitialFolder.FOLDER_METRICS, fn));
            }
        }

        private void RebuildUsing(string selectedId)
        {
            _lstMethod.Items.Clear();
            _lstMethod.Items.AddRange(NamedItem.GetRange(Algo.Instance.Statistics, GetStatName).ToArray());

            if (selectedId != null)
            {
                _lstMethod.SelectedItem = Algo.Instance.All.Get(selectedId);
            }
        }

        private string GetStatName(StatisticBase input)
        {
            // ⇉ Metric with two input vectors (e.g. t-test)
            // → Statistic with one input vector (e.g. mean)
            // ↣ Calculated from other statistics (e.g. most significant t-test)

            if (input is MetricBase) // check in this order since all metrics are statistics
            {
                MetricBase m = (MetricBase)input;
                return " ⇉    " + input.Name;
            }
            else if (input is StatisticConsumer)
            {
                StatisticBase s = (StatisticBase)input;
                return " ↣   " + input.Name;
            }
            else
            {
                return " →    " + input.Name;
            }
        }

        private void _btnComment_Click(object sender, EventArgs e)
        {
            if (_readOnly)
            {
                FrmInputLarge.ShowFixed(this, Text, "View Comments", _txtName.Text, _comments);
            }
            else
            {
                string comment = FrmInputLarge.Show(this, Text, "Edit Comments", _txtName.Text, _comments);

                if (comment != null)
                {
                    _comments = comment;
                }
            }
        }

        private void SetStatuses()
        {
            CheckAndChange(null, null);
        }

        private void CheckAndChange(object sender, EventArgs e)
        {
            StatisticBase stat = NamedItem<StatisticBase>.Extract(_lstMethod.SelectedItem);
            bool m = stat != null;

            bool p = m && stat.Parameters.HasCustomisableParams;
            _txtParams.Visible = p;
            _btnEditParameters.Visible = p;
            _lblParams.Visible = p;
            _lblParams.Text = p ? stat.Parameters.ParamNames() : "Parameters";

            object[] tmp;
            bool s = m
                    && (!stat.Parameters.HasCustomisableParams || stat.Parameters.TryStringToParams(_core, _txtParams.Text, out tmp))
                    && stat.SupportsInputFilters;

            _lblApply.Visible = s;
            _radObs.Visible = s;
            _radTrend.Visible = s;
            _btnTrendHelp.Visible = s;

            bool a = s && (_radObs.Checked || _radTrend.Checked);

            _lblAVec.Visible = a;
            _ecbFilter1.Visible = a;

            if (stat != null)
            {
                _lblAVec.Text = stat.IsMetric ? "Compare" : "For";
            }

            bool b = a && stat.IsMetric;

            _radBCorTime.Visible = b;
            _radSamePeak.Visible = b;
            _radBDiffPeak.Visible = b;
            _lblBVec.Visible = b;

            _lstDiffPeak.Visible = b && _radBDiffPeak.Checked;
            _btnSelectDiffPeak.Visible = b && _radBDiffPeak.Checked;

            bool t = b && _radSamePeak.Checked;

            _ecbFilter2.Visible = t;

            Validate(null, null);
        }

        private void _txtName_TextChanged(object sender, EventArgs e)
        {
            Validate(null, null);
        }

        private void _btnSelectDiffPeak_Click(object sender, EventArgs e)
        {
            var newPeak = ListValueSet.ForPeaks(_core).Select(NamedItem<Peak>.Extract(_lstDiffPeak.SelectedItem)).ShowList(this);

            if (newPeak != null)
            {
                _lstDiffPeak.SelectedItem = newPeak;
            }
        }

        private void _btnSelectPreview_Click(object sender, EventArgs e)
        {
            var newPreview = ListValueSet.ForPeaks(_core).Select(_previewPeak).ShowList(this); 

            if (newPreview != null)
            {
                _previewPeak = newPreview;
                GeneratePreview(GetSelection());
            }
        }

        private void GeneratePreview(ConfigurationStatistic sel)
        {
            if (sel == null)
            {
                return;
            }

            if (_previewPeak == null)
            {
                return;
            }

            _lblPreview.Text = _previewPeak.DisplayName + ": ";

            try
            {
                double v = sel.Calculate(_core, _previewPeak);

                _lblPreview2.Text = v.ToString();
            }
            catch (Exception ex)
            {
                _lblPreview2.Text = ex.Message;
            }
        }

        private void _btnPreviousPreview_Click(object sender, EventArgs e)
        {
            PagePreview(-1);
        }

        private void _btnNextPreview_Click(object sender, EventArgs e)
        {
            PagePreview(1);
        }

        private void PagePreview(int direction)
        {
            int index = _core.Peaks.IndexOf(_previewPeak) + direction;

            if (index <= -1)
            {
                index = _core.Peaks.Count - 1;
            }

            if (index >= _core.Peaks.Count)
            {
                index = 0;
            }

            _previewPeak = _core.Peaks[index];

            GeneratePreview(GetSelection());
        }

        private void _btnTrendHelp_Click(object sender, EventArgs e)
        {
            FrmMsgBox.ShowHelp(this, "Default Trend", UiControls.GetDefaultTrendMessage(_core));
        }

        private void _btnEditParameters_Click(object sender, EventArgs e)
        {
            StatisticBase stat = NamedItem<StatisticBase>.Extract(_lstMethod.SelectedItem);
            FrmEditParameters.Show(stat, _txtParams, _core, _readOnly);
        }
    }
}
