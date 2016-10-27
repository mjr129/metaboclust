using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Algorithms.Definitions.Base;
using MetaboliteLevels.Data.Algorithms.Definitions.Base.Misc;
using MetaboliteLevels.Data.Algorithms.Definitions.Metrics;
using MetaboliteLevels.Data.Algorithms.Definitions.Statistics;
using MetaboliteLevels.Data.Algorithms.Definitions.Statistics.Implementations;
using MetaboliteLevels.Data.Algorithms.General;
using MetaboliteLevels.Data.Database;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Session.Main;
using MetaboliteLevels.Gui.Controls;
using MetaboliteLevels.Gui.Datatypes;
using MetaboliteLevels.Gui.Forms.Activities;
using MetaboliteLevels.Gui.Forms.Selection;
using MetaboliteLevels.Gui.Forms.Text;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Gui.Forms.Editing
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
        private readonly EditableComboBox<IMatrixProvider> _ecbSource;
        private EditableComboBox<Peak> _ecbDiffPeak;

        internal static ArgsStatistic Show(Form owner, ArgsStatistic def, Core core, bool readOnly)
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

        internal static bool ShowCannotEditError(Form owner, ArgsBase def )
        {
            if (def != null && !def.CheckIsAvailable())
            {
                FrmMsgBox.ShowWarning(owner, "Missing algorithm",
                                      "This algorithm uses an algorithm not installed on this machine and its parameters cannot be modified.");

                return true;
            }
            return false;
        }

        private ArgsStatistic GetSelection()
        {
            StatisticBase sel = this._ecbMeasure.SelectedItem;
            IMatrixProvider src;
            EAlgoInputBSource bsrc;
            ObsFilter filter1;
            ObsFilter filter2;
            Peak bpeak;
            string title;

            this._checker.Clear(); 

            // Title / comments
            title = string.IsNullOrWhiteSpace(this._txtName.Text) ? null : this._txtName.Text;

            // Parameters
            object[] parameters;

            if (sel!=null )
            {
                if (sel.Parameters.HasCustomisableParams)
                {
                    string error;
                    parameters = sel.Parameters.TryStringToParams( this._core, this._txtParams.Text, out error );

                    this._checker.Check( this._txtParams, parameters != null, error ?? "error" );
                }
                else
                {
                    parameters = null;
                }
            }
            else
            {
                parameters = null;
                this._checker.Check( this._ecbMeasure.ComboBox, false, "Select a method" );
            }

            // Obs source
            src = this._ecbSource.SelectedItem;             
            this._checker.Check( this._ecbSource.ComboBox, src != null, "Select a source" );

            if (sel==null || !sel.SupportsInputFilters)
            {
                filter1 = null;
                filter2 = null;       
                bsrc = EAlgoInputBSource.None;
                bpeak = null;
            }
            else
            {   
                // Vector A
                filter1 = this._ecbFilter1.SelectedItem;
                this._checker.Check( this._ecbFilter1.ComboBox, this._ecbFilter1.HasSelection, "Select a filter" );

                // Vector B
                if (sel==null || !sel.IsMetric)
                {
                    // If the stat isn't comparing anything there is nothing to set
                    bsrc = EAlgoInputBSource.None;
                    filter2 = null;
                    bpeak = null;
                }
                else if (this._radBCorTime.Checked)
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
                    bpeak = this._ecbDiffPeak.SelectedItem;
                    this._checker.Check( this._lstDiffPeak, bpeak != null, "Select a peak" );
                    filter2 = filter1;
                }
                else if (this._radSamePeak.Checked)
                {
                    // Otherwise we are comparing against the same peak, get what we are comparing against
                    bsrc = EAlgoInputBSource.SamePeak;
                    bpeak = null;
                    filter2 = this._ecbFilter2.SelectedItem;
                    this._checker.Check( this._ecbFilter2.ComboBox, this._ecbFilter2.HasSelection, "Select a peak" );
                }
                else
                {
                    // What are we comparing against?
                    this._checker.Check( this._radBCorTime, false, "Select a comparison" );
                    this._checker.Check( this._radBDiffPeak, false, "Select a comparison" );
                    this._checker.Check( this._radSamePeak, false, "Select a comparison" );
                    bsrc = default( EAlgoInputBSource );
                    bpeak = default( Peak );
                    filter2 = default( ObsFilter );
                }
            }

            if (this._checker.HasErrors)
            {
                return null;
            }

            // Result
            ArgsStatistic args = new ArgsStatistic( sel.Id, src, filter1, bsrc, filter2, bpeak, parameters);
            args.OverrideDisplayName = title;
            args.Comment = this._comments;
            return args;
        }

        public FrmEditConfigurationStatistic()
        {
            this.InitializeComponent();
            UiControls.SetIcon(this);

            this._lblPreviewTitle.BackColor = UiControls.PreviewBackColour;
            this._lblPreviewTitle.ForeColor = UiControls.PreviewForeColour;
            this._flpPreviewButtons.BackColor = UiControls.PreviewBackColour;
            this._flpPreviewButtons.ForeColor = UiControls.PreviewForeColour;
        }

        private void Validate(object sender, EventArgs e)
        {
            bool previewSucceeded;

            try
            {
                ArgsStatistic sel = this.GetSelection();
                this.GeneratePreview(sel);
                previewSucceeded = sel != null;
                this._txtName.Watermark = sel != null ? sel.DefaultDisplayName : Resx.Texts.default_name;
            }
            catch
            {
                previewSucceeded = false;
            }

            this._btnOk.Enabled = previewSucceeded;
            this._tlpPreivew.Visible = previewSucceeded;
        }

        private FrmEditConfigurationStatistic(Core core, ArgsStatistic defaultSelection, Peak defaultPeak, bool readOnly)
            : this()
        {
            this._core = core;

            this._previewPeak = defaultPeak;

            this._ecbFilter1 = DataSet.ForObsFilter(core).CreateComboBox(this._lstFilter1, this._btnFilter1,  ENullItemName.RepresentingAll);
            this._ecbFilter2 = DataSet.ForObsFilter(core).CreateComboBox(this._lstFilter2, this._btnFilter2,  ENullItemName.RepresentingAll);
            this._ecbSource = DataSet.ForMatrixProviders( core ).CreateComboBox( this._lstSource, this._btnSource, ENullItemName.NoNullItem );
            this._ecbMeasure = DataSet.ForStatisticsAlgorithms(core).CreateComboBox(this._lstMethod, this._btnNewStatistic, ENullItemName.NoNullItem);
            this._ecbDiffPeak = DataSet.ForPeaks( core ).CreateComboBox( this._lstDiffPeak, this._btnSelectDiffPeak, ENullItemName.NoNullItem );
                        
            this._ecbDiffPeak.SelectedItem = defaultPeak;  

            if (defaultSelection != null)
            {
                this._txtName.Text = defaultSelection.OverrideDisplayName;
                this.ctlTitleBar1.SubText = defaultSelection.AlgoName;
                this._comments = defaultSelection.Comment;
                this._ecbMeasure.SelectedItem =(StatisticBase) defaultSelection.GetAlgorithmOrNull();
                this._txtParams.Text = AlgoParameterCollection.ParamsToReversableString(defaultSelection.Parameters, this._core);

                this._ecbSource.SelectedItem = defaultSelection.SourceProvider;
                this._radBCorTime.Checked = defaultSelection.VectorBSource == EAlgoInputBSource.Time;
                this._radBDiffPeak.Checked = defaultSelection.VectorBSource == EAlgoInputBSource.AltPeak;
                this._radSamePeak.Checked = defaultSelection.VectorBSource == EAlgoInputBSource.SamePeak;

                this._ecbFilter1.SelectedItem = defaultSelection.VectorAConstraint;
                this._ecbFilter2.SelectedItem = defaultSelection.VectorBConstraint;

                if (defaultSelection.VectorBPeak != null)
                {
                    this._ecbDiffPeak.SelectedItem = defaultSelection.VectorBPeak;
                }
            }

            this.SetStatuses();

            this._readOnly = readOnly;

            if (readOnly)
            {
                UiControls.MakeReadOnly(this, this._tlpPreivew);

                this._btnComment.Enabled = true;

                this.ctlTitleBar1.Text = "View Statistic";
            }
            else if (defaultSelection != null)
            {
                this.ctlTitleBar1.Text = "Edit Statistic";
            } 
            else
            {
                this.ctlTitleBar1.Text = "New Statistic";
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
                return " ⇉    " + input.DisplayName;
            }
            else if (input is StatisticConsumer)
            {
                StatisticBase s = (StatisticBase)input;
                return " ↣   " + input.DisplayName;
            }
            else
            {
                return " →    " + input.DisplayName;
            }
        }

        private void _btnComment_Click(object sender, EventArgs e)
        {
            if (this._readOnly)
            {
                FrmInputMultiLine.ShowFixed(this, this.Text, "View Comments", this._txtName.Text, this._comments);
            }
            else
            {
                string comment = FrmInputMultiLine.Show(this, this.Text, "Edit Comments", this._txtName.Text, this._comments);

                if (comment != null)
                {
                    this._comments = comment;
                }
            }
        }

        private void SetStatuses()
        {
            this.CheckAndChange(null, null);
        }

        private void CheckAndChange(object sender, EventArgs e)
        {
            StatisticBase stat = this._ecbMeasure.SelectedItem;
            bool m = stat != null;

            bool p = m && stat.Parameters.HasCustomisableParams;
            this._txtParams.Visible = p;
            this._btnEditParameters.Visible = p;
            this._lblParams.Visible = p;
            this._lblParams.Text = p ? stat.Parameters.ParamNames() : "Parameters";
            this._lblApply.Visible = m;
            this._ecbSource.Visible = m;
            this.linkLabel1.Visible = m && !stat.SupportsInputFilters;

            bool s = m && stat.SupportsInputFilters;


            bool a = s && this._ecbSource.HasSelection;

            this._lblAVec.Visible = a;
            this._ecbFilter1.Visible = a;

            if (stat != null)
            {
                this._lblAVec.Text = stat.IsMetric ? "Compare" : "For";
            }

            bool b = a && stat.IsMetric;

            this._radBCorTime.Visible = b;
            this._radSamePeak.Visible = b;
            this._radBDiffPeak.Visible = b;
            this._lblBVec.Visible = b;

            this._lstDiffPeak.Visible = b && this._radBDiffPeak.Checked;
            this._btnSelectDiffPeak.Visible = b && this._radBDiffPeak.Checked;

            bool t = b && this._radSamePeak.Checked;

            this._ecbFilter2.Visible = t;

            this.Validate(null, null);
        }

        private void _txtName_TextChanged(object sender, EventArgs e)
        {
            this.Validate(null, null);
        }

        private void _btnSelectDiffPeak_Click(object sender, EventArgs e)
        {
            var def = NamedItem<Peak>.Extract(this._lstDiffPeak.SelectedItem);
            var newPeak = DataSet.ForPeaks(this._core).ShowList(this, def);

            if (newPeak != null)
            {
                this._lstDiffPeak.SelectedItem = newPeak;
            }
        }

        private void _btnSelectPreview_Click(object sender, EventArgs e)
        {
            var newPreview = DataSet.ForPeaks(this._core).ShowList(this, this._previewPeak);

            if (newPreview != null)
            {
                this._previewPeak = newPreview;
                this.GeneratePreview(this.GetSelection());
            }
        }

        private void GeneratePreview( ArgsStatistic sel )
        {
            if (sel == null)
            {
                return;
            }

            if (this._previewPeak == null)
            {
                return;
            }

            this._lblPreview.Text = this._previewPeak.DisplayName + ": ";

            try
            {
                ConfigurationStatistic temp = new ConfigurationStatistic() { Args = sel };
                double v = temp.Calculate(this._core, this._previewPeak);

                this._lblPreview2.Text = v.ToString();
                this._lblPreview2.ForeColor = System.Drawing.Color.Gray;
            }
            catch (Exception ex)
            {
                this._lblPreview2.Text = ex.Message;
                this._lblPreview2.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void _btnPreviousPreview_Click(object sender, EventArgs e)
        {
            this.PagePreview(-1);
        }

        private void _btnNextPreview_Click(object sender, EventArgs e)
        {
            this.PagePreview(1);
        }

        private void PagePreview(int direction)
        {
            int index = this._core.Peaks.IndexOf(this._previewPeak) + direction;

            if (index <= -1)
            {
                index = this._core.Peaks.Count - 1;
            }

            if (index >= this._core.Peaks.Count)
            {
                index = 0;
            }

            this._previewPeak = this._core.Peaks[index];

            this.GeneratePreview(this.GetSelection());
        }      

        private void _btnEditParameters_Click(object sender, EventArgs e)
        {
            StatisticBase stat = this._ecbMeasure.SelectedItem;
            FrmEditParameters.Show(stat, this._txtParams, this._core, this._readOnly);
        }

        private void _btnObs_Click(object sender, EventArgs e)
        {
            DataSet.ForObservations(this._core).ShowListEditor(this);
        }

        private void _btnTrend_Click(object sender, EventArgs e)
        {
            DataSet.ForTrends(this._core).ShowListEditor(this);
        }

        private void linkLabel1_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e )
        {
            FrmMsgBox.ShowInfo( this, "Source", "The intensity matrix itself is not used for this algorithm, but a source must be specified to identify the peaks to calculate the statistic for." );
        }
    }
}
