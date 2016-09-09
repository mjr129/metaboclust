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
using MetaboliteLevels.Data.Algorithms.Definitions.Configurations;
using MetaboliteLevels.Data.DataInfo;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Forms.Generic;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Settings;
using MetaboliteLevels.Forms.Editing;
using MGui.Helpers;
using MetaboliteLevels.Data.Session.Associational;

namespace MetaboliteLevels.Forms.Algorithms
{
    public partial class FrmEditConfigurationStatistic : Form
    {
        private readonly Core _core;
        private readonly bool _readOnly;

        private string _comments;
        private Peak _previewPeak;
        private EditableComboBox<ObsFilter> _ecbFilter1;
        private EditableComboBox<ObsFilter> _ecbFilter2;
        private EditableComboBox<StatisticBase> _ecbMeasure;
        private readonly EditableComboBox<MatrixProducer> _ecbSource;

        internal static ConfigurationStatistic Show(Form owner, ConfigurationStatistic def, Core core, bool readOnly)
        {
            if (ShowCannotEditError(owner, def)) return null;

            using (FrmEditConfigurationStatistic frm = new FrmEditConfigurationStatistic(core, def, FrmMain.SearchForSelectedPeak(owner), readOnly))
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
            StatisticBase sel = this._ecbMeasure.SelectedItem;
            MatrixProducer src;
            EAlgoInputBSource bsrc;
            ObsFilter filter1;
            ObsFilter filter2;
            Peak bpeak;
            string title;

            _checker.Clear(); 

            // Title / comments
            title = string.IsNullOrWhiteSpace(_txtName.Text) ? null : _txtName.Text;

            // Parameters
            object[] parameters;

            if (sel!=null )
            {
                if (sel.Parameters.HasCustomisableParams)
                {
                    parameters = sel.Parameters.TryStringToParams( _core, _txtParams.Text );

                    _checker.Check( _txtParams, parameters != null, "Specify valid parameters for the method." );
                }
                else
                {
                    parameters = null;
                }
            }
            else
            {
                parameters = null;
                _checker.Check( _ecbMeasure.ComboBox, false, "Select a method" );
            }

            if (sel==null || !sel.SupportsInputFilters)
            {
                filter1 = null;
                filter2 = null;
                src = null;
                bsrc = EAlgoInputBSource.None;
                bpeak = null;
            }
            else
            {
                // Obs source
                src = _ecbSource.SelectedItem;

                _checker.Check( _ecbSource.ComboBox, src!=null, "Select a source" );

                // Vector A
                filter1 = _ecbFilter1.SelectedItem;
                _checker.Check( _ecbFilter1.ComboBox, _ecbFilter1.HasSelection, "Select a filter" );

                // Vector B
                if (sel==null || !sel.IsMetric)
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
                    _checker.Check( _lstDiffPeak, bpeak != null, "Select a peak" );
                    filter2 = filter1;
                }
                else if (this._radSamePeak.Checked)
                {
                    // Otherwise we are comparing against the same peak, get what we are comparing against
                    bsrc = EAlgoInputBSource.SamePeak;
                    bpeak = null;
                    filter2 = this._ecbFilter2.SelectedItem;
                    _checker.Check( _ecbFilter2.ComboBox, this._ecbFilter2.HasSelection, "Select a peak" );
                }
                else
                {
                    // What are we comparing against?
                    _checker.Check( _radBCorTime, false, "Select a comparison" );
                    _checker.Check( _radBDiffPeak, false, "Select a comparison" );
                    _checker.Check( _radSamePeak, false, "Select a comparison" );
                    bsrc = default( EAlgoInputBSource );
                    bpeak = default( Peak );
                    filter2 = default( ObsFilter );
                }
            }

            if (_checker.HasErrors)
            {
                return null;
            }

            // Result
            ArgsStatistic args = new ArgsStatistic(src, filter1, bsrc, filter2, bpeak, parameters);
            ConfigurationStatistic result = new ConfigurationStatistic(title, _comments, sel.Id, args);
            return result;
        }

        public FrmEditConfigurationStatistic()
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
            bool previewSucceeded;

            try
            {
                ConfigurationStatistic sel = GetSelection();
                GeneratePreview(sel);
                previewSucceeded = sel != null;
                _txtName.Watermark = sel != null ? sel.DefaultDisplayName : "Default";
            }
            catch
            {
                previewSucceeded = false;
            }

            _btnOk.Enabled = previewSucceeded;
            _tlpPreivew.Visible = previewSucceeded;
        }

        private FrmEditConfigurationStatistic(Core core, ConfigurationStatistic defaultSelection, Peak defaultPeak, bool readOnly)
            : this()
        {
            this._core = core;

            _previewPeak = defaultPeak;

            _ecbFilter1 = DataSet.ForObsFilter(core).CreateComboBox(_lstFilter1, _btnFilter1,  ENullItemName.All);
            _ecbFilter2 = DataSet.ForObsFilter(core).CreateComboBox(_lstFilter2, _btnFilter2,  ENullItemName.All);
            _ecbSource = DataSet.ForIntensityMatrices( core ).CreateComboBox( _lstSource, _btnSource, ENullItemName.NoNullItem );
            _ecbMeasure = DataSet.ForStatisticsAlgorithms(core).CreateComboBox(_lstMethod, _btnNewStatistic, ENullItemName.NoNullItem);

            _lstDiffPeak.Items.AddRange(NamedItem.GetRange(_core.Peaks, z => z.DisplayName).ToArray());
            _lstDiffPeak.SelectedItem = defaultPeak;  

            if (defaultSelection != null)
            {
                _txtName.Text = defaultSelection.OverrideDisplayName;
                ctlTitleBar1.SubText = defaultSelection.AlgoName;
                _comments = defaultSelection.Comment;
                _ecbMeasure.SelectedItem = defaultSelection.Cached;
                _txtParams.Text = AlgoParameterCollection.ParamsToReversableString(defaultSelection.Args.Parameters, _core);

                _ecbSource.SelectedItem = defaultSelection.Args.Source;
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
            else
            {
                ctlTitleBar1.Text = "New Statistic";
            }

            // UiControls.CompensateForVisualStyles(this);
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
                FrmInputMultiLine.ShowFixed(this, Text, "View Comments", _txtName.Text, _comments);
            }
            else
            {
                string comment = FrmInputMultiLine.Show(this, Text, "Edit Comments", _txtName.Text, _comments);

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
            StatisticBase stat = _ecbMeasure.SelectedItem;
            bool m = stat != null;

            bool p = m && stat.Parameters.HasCustomisableParams;
            _txtParams.Visible = p;
            _btnEditParameters.Visible = p;
            _lblParams.Visible = p;
            _lblParams.Text = p ? stat.Parameters.ParamNames() : "Parameters";
                           
            bool s = m && stat.SupportsInputFilters;

            _lblApply.Visible = s;
            _ecbSource.Visible = s;

            bool a = s && _ecbSource.HasSelection;

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
            var def = NamedItem<Peak>.Extract(_lstDiffPeak.SelectedItem);
            var newPeak = DataSet.ForPeaks(_core).ShowList(this, def);

            if (newPeak != null)
            {
                _lstDiffPeak.SelectedItem = newPeak;
            }
        }

        private void _btnSelectPreview_Click(object sender, EventArgs e)
        {
            var newPreview = DataSet.ForPeaks(_core).ShowList(this, _previewPeak);

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
                _lblPreview2.ForeColor = System.Drawing.Color.Gray;
            }
            catch (Exception ex)
            {
                _lblPreview2.Text = ex.Message;
                _lblPreview2.ForeColor = System.Drawing.Color.Red;
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

        private void _btnEditParameters_Click(object sender, EventArgs e)
        {
            StatisticBase stat = _ecbMeasure.SelectedItem;
            FrmEditParameters.Show(stat, _txtParams, _core, _readOnly);
        }

        private void _btnObs_Click(object sender, EventArgs e)
        {
            DataSet.ForObservations(_core).ShowListEditor(this);
        }

        private void _btnTrend_Click(object sender, EventArgs e)
        {
            DataSet.ForTrends(_core).ShowListEditor(this);
        }
    }
}
