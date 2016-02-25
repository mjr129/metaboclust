using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MetaboliteLevels.Algorithms;
using MetaboliteLevels.Algorithms.Statistics.Arguments;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Algorithms.Statistics.Trends;
using MetaboliteLevels.Controls;
using MetaboliteLevels.Data;
using MetaboliteLevels.Data.DataInfo;
using MetaboliteLevels.Data.General;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Forms.Generic;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Viewers.Charts;
using MetaboliteLevels.Algorithms.Statistics;
using MetaboliteLevels.Algorithms.Statistics.Corrections;
using MetaboliteLevels.Settings;
using MetaboliteLevels.Forms.Editing;

namespace MetaboliteLevels.Forms.Algorithms
{
    public partial class FrmAlgoCorrection : Form
    {
        private readonly Core _core;
        private readonly ChartHelperForPeaks _chartOrig;
        private readonly ChartHelperForPeaks _chartChanged;
        private readonly bool _readOnly;

        private Peak _selectedPeak;
        private string _comments;

        private List<GroupInfo> vTypes = new List<GroupInfo>();
        private List<BatchInfo> vBatches = new List<BatchInfo>();
        private FlowLayoutPanel _flpBatchButtons;
        private FlowLayoutPanel _flpGroupButtons;
        private EditableComboBox<ObsFilter> _ecbFilter;

        private ConfigurationCorrection GetSelection()
        {
            // Algo
            AlgoBase algo = (AlgoBase)_lstMethod.SelectedItem;

            // Params
            object[] parameters;

            if (algo == null)
            {
                return null;
            }

            if (algo.Parameters.HasCustomisableParams)
            {
                if (!algo.Parameters.TryStringToParams(_core, _txtParameters.Text, out parameters))
                {
                    return null;
                }
            }
            else
            {
                parameters = null;
            }

            if (algo is TrendBase)
            {
                // Method
                ECorrectionMethod met;

                if (_radSubtract.Checked)
                {
                    met = ECorrectionMethod.Subtract;
                }
                else if (_radDivide.Checked)
                {
                    met = ECorrectionMethod.Divide;
                }
                else
                {
                    return null;
                }

                // Mode
                ECorrectionMode mode;
                GroupInfo controlGroup;
                ObsFilter filter;

                if (_radBatch.Checked)
                {
                    mode = ECorrectionMode.Batch;
                    controlGroup = null;

                    if (_ecbFilter.HasSelection)
                    {
                        filter = _ecbFilter.SelectedItem;
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (_radType.Checked)
                {
                    mode = ECorrectionMode.Control;
                    controlGroup = NamedItem<GroupInfo>.Extract(_lstTypes.SelectedItem);
                    filter = null;
                }
                else
                {
                    return null;
                }

                ArgsTrendAsCorrection args = new ArgsTrendAsCorrection(mode, met, controlGroup, filter, parameters);
                return new ConfigurationCorrection(_txtName.Text, _comments, (TrendBase)algo, args);
            }
            else if (algo is CorrectionBase)
            {
                ArgsCorrection args = new ArgsCorrection(parameters);
                return new ConfigurationCorrection(_txtName.Text, _comments, (CorrectionBase)algo, args);
            }
            else
            {
                throw new InvalidOperationException("Unknown type on switch: " + algo.ToString());
            }
        }

        void CheckAndChange()
        {
            AlgoBase trend = (AlgoBase)_lstMethod.SelectedItem;

            bool paramsVisible = trend != null && trend.Parameters.HasCustomisableParams;

            _lblParams.Visible = paramsVisible;
            _txtParameters.Visible = paramsVisible;
            _btnEditParameters.Visible = paramsVisible;
            _lblParams.Text = paramsVisible ? trend.Parameters.ParamNames() : "Parameters";

            object[] tmp;
            bool paramsValid = trend != null && (!trend.Parameters.HasCustomisableParams || trend.Parameters.TryStringToParams(_core, _txtParameters.Text, out tmp));
            bool usingTrend = trend is TrendBase;
            bool correctorVisible = paramsValid && usingTrend;

            _lblCorrector.Visible = correctorVisible;
            _flpCorrector.Visible = correctorVisible;
            _lstTypes.Visible = correctorVisible && _radType.Checked;

            bool filterVisible = correctorVisible && _radBatch.Checked;

            _lblSepFilter.Visible = filterVisible;
            _lblAVec.Visible = filterVisible;
            _ecbFilter.Visible = filterVisible;

            bool operatorVisible = correctorVisible && ((_radType.Checked && _lstTypes.SelectedItem != null) || _radBatch.Checked);

            _lblCorrector2.Visible = operatorVisible;
            _flpCorrector2.Visible = operatorVisible;

            bool readyToGo = (usingTrend && operatorVisible && (_radDivide.Checked || _radSubtract.Checked)) || (!usingTrend && paramsValid);

            ConfigurationCorrection sel = readyToGo ? GetSelection() : null;
            bool valid = sel != null;

            _tlpPreview.Visible = valid;
            _btnOk.Enabled = valid;

            _flpGroupButtons.Visible = (!usingTrend || _radType.Checked);
            _flpBatchButtons.Visible = (usingTrend && _radBatch.Checked);

            if (valid)
            {
                GeneratePreview(sel);
            }
        }

        internal static ConfigurationCorrection Show(Form owner, Core core, ConfigurationCorrection def, bool readOnly)
        {
            using (FrmAlgoCorrection frm = new FrmAlgoCorrection(core, def, readOnly, FrmMain.SearchForSelectedPeak(owner)))
            {
                if (UiControls.ShowWithDim(owner, frm) == DialogResult.OK)
                {
                    return frm.GetSelection();
                }

                return null;
            }
        }

        internal FrmAlgoCorrection(Core core, ConfigurationCorrection def, bool readOnly, Peak previewPeak)
            : this()
        {
            _core = core;
            _readOnly = readOnly;
            _selectedPeak = previewPeak;

            // Charts
            _chartOrig = new ChartHelperForPeaks(null, _core, panel1);
            _chartChanged = new ChartHelperForPeaks(null, _core, panel2);

            // Choicelists
            _ecbFilter = ListValueSet.ForObsFilter(core, true).CreateComboBox(_lstFilter, _btnFilter, core);

            // Lists
            _lstMethod.Items.AddRange(Algo.Instance.Trends.ToArray());
            _lstMethod.Items.AddRange(Algo.Instance.Corrections.ToArray());
            _lstTypes.Items.AddRange(NamedItem.GetRange(_core.Groups.WhereEnabled(), z => z.DisplayName).ToArray());

            // Buttons
            GenerateTypeButtons();

            // Default
            if (def != null)
            {
                _txtName.Text = def.OverrideDisplayName;
                _txtParameters.Text = AlgoParameterCollection.ParamsToReversableString(def.Args.Parameters, core);
                _lstMethod.SelectedItem = def.Cached;
                _comments = def.Comment;

                if (def.IsUsingTrend)
                {
                    ArgsTrendAsCorrection args = (ArgsTrendAsCorrection)def.Args;
                    _radBatch.Checked = args.Mode == ECorrectionMode.Batch;
                    _radType.Checked = args.Mode == ECorrectionMode.Control;
                    _radDivide.Checked = args.Method == ECorrectionMethod.Divide;
                    _radSubtract.Checked = args.Method == ECorrectionMethod.Subtract;
                    _lstTypes.SelectedItem = args.ControlGroup;
                    _ecbFilter.SelectedItem = args.Constraint;
                }
                else
                {
                    ArgsCorrection args = (ArgsCorrection)def.Args;
                    // NA
                }
            }

            CheckAndChange();

            if (readOnly)
            {
                UiControls.MakeReadOnly(this, _tlpPreview);
            }

            UiControls.CompensateForVisualStyles(this);
        }

        public FrmAlgoCorrection()
        {
            InitializeComponent();
            UiControls.SetIcon(this);

            _lblPreviewTitle.BackColor = UiControls.PreviewBackColour;
            _lblPreviewTitle.ForeColor = UiControls.PreviewForeColour;
            _flpPreviewButtons.BackColor = UiControls.PreviewBackColour;
            _flpPreviewButtons.ForeColor = UiControls.PreviewForeColour;
        }

        private void _btnSelectPreview_Click(object sender, EventArgs e)
        {
            var sel = ListValueSet.ForPeaks(_core, true).Select(_selectedPeak).ShowList(this);

            if (sel != null)
            {
                _selectedPeak = sel;
                CheckAndChange();
            }
        }

        /// <summary>
        /// Generates the preview image.
        /// </summary>
        private void GeneratePreview(ConfigurationCorrection sel)
        {
            if (sel == null)
            {
                return;
            }

            if (_selectedPeak == null)
            {
                _lblPreviewTitle.Text = "Preview (please select peak)";
                _chartOrig.ClearPlot();
                _chartChanged.ClearPlot();
                return;
            }

            _lblPreviewTitle.Text = "Preview (" + _selectedPeak.DisplayName + ")";

            var orig = new StylisedPeak(_selectedPeak);
            var changed = new StylisedPeak(_selectedPeak);

            IEnumerable trendOrder;
            double[] trend;
            double[] corrected;

            try
            {
                trend = sel.ExtractTrend(_core, _selectedPeak.Observations.Raw, out trendOrder);
                corrected = sel.Calculate(_core, _selectedPeak.Observations.Raw);
            }
            catch
            {
                _chartOrig.Plot(null);
                _chartChanged.Plot(null);
                return;
            }

            bool isBatchMode;

            if (sel.IsUsingTrend)
            {
                isBatchMode = sel.ArgsT.Mode == ECorrectionMode.Batch;
            }
            else
            {
                isBatchMode = false;
            }

            orig.OverrideDefaultOptions = new StylisedPeakOptions(_core)
            {
                ShowAcqisition = isBatchMode,
                ViewBatches = vBatches,
                ViewTypes = vTypes,
                ConditionsSideBySide = true,
                ShowPoints = true,
                ShowTrend = sel.IsUsingTrend,
                ShowRanges = false,
                ViewAlternativeObservations = false
            };

            changed.OverrideDefaultOptions = new StylisedPeakOptions(orig.OverrideDefaultOptions)
            {
                ShowTrend = false
            };

            orig.ForceTrend = trend;
            orig.ForceTrendOrder = trendOrder;

            changed.ForceObservations = new PeakValueSet(_core, corrected);

            _chartOrig.Plot(orig);
            _chartChanged.Plot(changed);
        }

        private void GenerateTypeButtons()
        {
            vTypes.AddRange(_core.Groups);
            vBatches.Add(_core.Batches.OrderBy(z => z.Id).First());

            _flpBatchButtons = new FlowLayoutPanel();
            _flpGroupButtons = new FlowLayoutPanel();

            _flpBatchButtons.Margin = _btnPreviousPreview.Margin;
            _flpGroupButtons.Margin = _btnPreviousPreview.Margin;

            _flpBatchButtons.AutoSize = true;
            _flpGroupButtons.AutoSize = true;

            _flpBatchButtons.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            _flpGroupButtons.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            _flpPreviewButtons.Controls.Add(_flpBatchButtons);
            _flpPreviewButtons.Controls.Add(_flpGroupButtons);

            foreach (var ti in _core.Groups.OrderBy(z => z.Id))
            {
                _flpGroupButtons.Controls.Add(GenerateTypeButton(ti));
            }

            foreach (var ti in _core.Batches.OrderBy(z => z.Id))
            {
                _flpBatchButtons.Controls.Add(GenerateTypeButton(ti));
            }
        }

        private CtlButton GenerateTypeButton(GroupInfoBase ti)
        {
            CtlButton btn = new CtlButton();
            btn.Tag = ti;
            UpdateImage(btn);
            btn.UseDefaultSize = true;
            btn.Click += btn_Click;
            btn.Margin = new Padding(0, 0, 0, 0);
            btn.Visible = true;
            toolTip1.SetToolTip(btn, "View " + ti.DisplayName);
            return btn;
        }

        void btn_Click(object sender, EventArgs e)
        {
            CtlButton btn = (CtlButton)sender;
            GroupInfo g = btn.Tag as GroupInfo;

            if (g != null)
            {
                if (vTypes.Contains(g))
                {
                    vTypes.Remove(g);
                }
                else
                {
                    vTypes.Add(g);
                }
            }
            else
            {
                BatchInfo b = (BatchInfo)btn.Tag;

                if (vBatches.Contains(b))
                {
                    vBatches.Remove(b);
                }
                else
                {
                    vBatches.Add(b);
                }
            }

            UpdateImage(btn);
            this.GeneratePreview(GetSelection());
        }

        private void UpdateImage(CtlButton btn)
        {
            if (btn.Image != null)
            {
                btn.Image.Dispose();
            }

            GroupInfo g = btn.Tag as GroupInfo;

            bool vis;

            if (g != null)
            {
                vis = vTypes.Contains(g);
            }
            else
            {
                BatchInfo b = (BatchInfo)btn.Tag;
                vis = vBatches.Contains(b);
            }

            btn.Image = UiControls.CreateSolidColourImage(vis, (GroupInfoBase)btn.Tag);
        }

        private void _btnOk_Click(object sender, EventArgs e)
        {

        }

        private void _btnComment_Click(object sender, EventArgs e)
        {
            if (_readOnly)
            {
                FrmInputLarge.ShowFixed(this, Text, "View Comments", _txtName.Text, _comments);
            }
            else
            {
                string newComments = FrmInputLarge.Show(this, Text, "Edit Comments", _txtName.Text, _comments);

                if (newComments != null)
                {
                    _comments = newComments;
                }
            }
        }

        private void anything_SomethingChanged(object sender, EventArgs e)
        {
            CheckAndChange();
        }

        private void _btnPreviousPreview_Click(object sender, EventArgs e)
        {
            PagePreview(-1);
        }

        private void PagePreview(int direction)
        {
            int index = _core.Peaks.IndexOf(_selectedPeak) + direction;

            if (index <= -1)
            {
                index = _core.Peaks.Count - 1;
            }

            if (index >= _core.Peaks.Count)
            {
                index = 0;
            }

            _selectedPeak = _core.Peaks[index];

            GeneratePreview(GetSelection());
        }

        private void _btnNextPreview_Click(object sender, EventArgs e)
        {
            PagePreview(1);
        }

        private void _btnEditParameters_Click(object sender, EventArgs e)
        {
            AlgoBase trend = (AlgoBase)_lstMethod.SelectedItem;
            FrmEditParameters.Show(trend, _txtParameters, _core, _readOnly);
        }

        private void _btnBatchInfo_Click(object sender, EventArgs e)
        {
            ListValueSet.ForBatches(_core, true).ShowBigList<BatchInfo>(this, _core, EViewMode.ReadAndComment);
        }
    }
}
