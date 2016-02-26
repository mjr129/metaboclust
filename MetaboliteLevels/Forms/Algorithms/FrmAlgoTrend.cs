using System;
using System.Windows.Forms;
using MetaboliteLevels.Algorithms;
using MetaboliteLevels.Algorithms.Statistics;
using MetaboliteLevels.Algorithms.Statistics.Arguments;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Algorithms.Statistics.Trends;
using MetaboliteLevels.Data;
using MetaboliteLevels.Data.General;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Forms.Generic;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Viewers.Charts;
using MetaboliteLevels.Forms.Editing;
using MetaboliteLevels.Controls;

namespace MetaboliteLevels.Forms.Algorithms
{
    public partial class FrmAlgoTrend : Form
    {
        private readonly ChartHelperForPeaks _chart1;
        private readonly Core _core;
        private readonly bool _readOnly;

        private Peak _previewPeak;
        private string _comments;

        EditableComboBox<TrendBase> _ecbMethod;

        internal static ConfigurationTrend Show(Form owner, Core core, ConfigurationTrend def, bool readOnly)
        {
            if (FrmAlgoStatistic.ShowCannotEditError(owner, def))
            {
                return null;
            }

            using (FrmAlgoTrend frm = new FrmAlgoTrend(core, FrmMain.SearchForSelectedPeak(owner), def, readOnly))
            {
                if (UiControls.ShowWithDim(owner, frm) == DialogResult.OK)
                {
                    return frm.GetSelection();
                }

                return null;
            }
        }

        internal FrmAlgoTrend(Core core, Peak previewPeak, ConfigurationTrend def, bool readOnly)
            : this()
        {
            _core = core;
            _previewPeak = previewPeak;

            _ecbMethod = DataSet.ForTrendAlgorithms(core).CreateComboBox(_lstMethod, _btnNewStatistic, ENullItemName.NoNullItem);

            _chart1 = new ChartHelperForPeaks(null, core, panel1);

            if (def != null)
            {
                this._txtName.Text = def.OverrideDisplayName;
                this._ecbMethod.SelectedItem = def.Cached;
                this._comments = def.Comment;
                this._txtParams.Text = AlgoParameterCollection.ParamsToReversableString(def.Args.Parameters, core);
            }

            CheckAndChange(null, null);

            _readOnly = readOnly;

            if (readOnly)
            {
                UiControls.MakeReadOnly(this, _tlpPreview);

                _btnComment.Enabled = true;

                ctlTitleBar1.Text = "View Trend";
            }
            else if (def != null)
            {
                ctlTitleBar1.Text = "Edit Trend";
            }
            else
            {
                ctlTitleBar1.Text = "New Trend";
            }

            UiControls.CompensateForVisualStyles(this);
        }


        private ConfigurationTrend GetSelection()
        {
            TrendBase sel = (TrendBase)this._ecbMethod.SelectedItem;
            string title;
            object[] args;

            // Selection
            if (sel == null)
            {
                return null;
            }

            // Title / comments
            title = string.IsNullOrWhiteSpace(_txtName.Text) ? null : _txtName.Text;

            // Parameters
            if (sel.Parameters.HasCustomisableParams)
            {
                if (!sel.Parameters.TryStringToParams(_core, _txtParams.Text, out args))
                {
                    return null;
                }
            }
            else
            {
                args = null;
            }

            return new ConfigurationTrend(title, _comments, sel.Id, new ArgsTrend(args));
        }

        public FrmAlgoTrend()
        {
            InitializeComponent();
            UiControls.SetIcon(this);

            _lblPreviewTitle.BackColor = UiControls.PreviewBackColour;
            _lblPreviewTitle.ForeColor = UiControls.PreviewForeColour;
            _flpPreviewButtons.BackColor = UiControls.PreviewBackColour;
            _flpPreviewButtons.ForeColor = UiControls.PreviewForeColour; 
        }

        private void Check(object sender, EventArgs e)
        {
            ConfigurationTrend sel;

            try
            {
                sel = GetSelection();
            }
            catch
            {
                sel = null;
            }

            bool valid = sel != null;

            _lblError.Visible = false;

            if (sel != null)
            {
                StylisedPeak sPeak = new StylisedPeak(_previewPeak);

                if (_previewPeak != null)
                {
                    try
                    {
                        sPeak.ForceObservations = new PeakValueSet(_core, _previewPeak.Observations.Raw, sel);
                        _chart1.Plot(sPeak);
                    }
                    catch (Exception ex)
                    {
                        _lblError.Text = ex.Message;
                        _lblError.Visible = true;
                        _chart1.ClearPlot();
                    }
                }
            }

            _btnOk.Enabled = valid;
            _tlpPreview.Visible = valid;
        }

        private void CheckAndChange(object sender, EventArgs e)
        {
            var sm = (TrendBase)_ecbMethod.SelectedItem;
            bool s = sm != null && sm.Parameters.HasCustomisableParams;

            _lblParams.Visible = s;
            _txtParams.Visible = s;
            _btnEditParameters.Visible = s;
            _lblParams.Text = s ? sm.Parameters.ParamNames() : "Parameters";

            Check(sender, e);
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

        private void _btnSelectPreview_Click(object sender, EventArgs e)
        {
            var newSel = DataSet.ForPeaks(_core).ShowList(this, _previewPeak);

            if (newSel != null)
            {
                _previewPeak = newSel;
                Check(sender, e);
            }
        }

        private void _btnPreviousPreview_Click(object sender, EventArgs e)
        {
            PagePreview(-1);
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

            Check(null, null);
        }

        private void _btnEditParameters_Click(object sender, EventArgs e)
        {
            var sm = (TrendBase)_ecbMethod.SelectedItem;
            FrmEditParameters.Show(sm, _txtParams, _core, _readOnly);
        }
    }
}
