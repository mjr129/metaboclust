using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Controls;
using MetaboliteLevels.Controls.Charts;
using MetaboliteLevels.Data.Algorithms.Definitions.Configurations;
using MetaboliteLevels.Data.Algorithms.Definitions.Trends;
using MetaboliteLevels.Data.Algorithms.General;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.Singular;
using MetaboliteLevels.Forms.Activities;
using MetaboliteLevels.Forms.Selection;
using MetaboliteLevels.Forms.Text;
using MetaboliteLevels.Resx;
using MetaboliteLevels.Types.UI;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Forms.Editing
{
    public partial class FrmEditConfigurationTrend : Form
    {
        private readonly ChartHelperForPeaks _chart1;
        private readonly Core _core;
        private readonly bool _readOnly;

        private Peak _previewPeak;
        private string _comments;

        EditableComboBox<TrendBase> _ecbMethod;
        private EditableComboBox<IMatrixProvider> _ecbSource;

        internal static ArgsTrend Show(Form owner, Core core, ArgsTrend def, bool readOnly)
        {
            if (FrmEditConfigurationStatistic.ShowCannotEditError(owner, def))
            {
                return null;
            }

            using (FrmEditConfigurationTrend frm = new FrmEditConfigurationTrend(core, FrmMain.SearchForSelectedPeak(owner), def, readOnly))
            {
                if (UiControls.ShowWithDim(owner, frm) == DialogResult.OK)
                {
                    return frm.GetSelection();
                }

                return null;
            }
        }

        internal FrmEditConfigurationTrend(Core core, Peak previewPeak, ArgsTrend def, bool readOnly)
            : this()
        {
            _core = core;
            _previewPeak = previewPeak;

            _ecbMethod = DataSet.ForTrendAlgorithms(core).CreateComboBox(_lstMethod, _btnNewStatistic, ENullItemName.NoNullItem);
            _ecbSource = DataSet.ForMatrixProviders( core ).CreateComboBox( _lstSource, _btnSource, ENullItemName.NoNullItem );

            _chart1 = new ChartHelperForPeaks(null, core, panel1);

            if (def != null)
            {
                this._txtName.Text = def.OverrideDisplayName;
                this._ecbMethod.SelectedItem = (TrendBase)def.GetAlgorithmOrNull();
                this._comments = def.Comment;
                this._txtParams.Text = AlgoParameterCollection.ParamsToReversableString(def.Parameters, core);
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

            // UiControls.CompensateForVisualStyles(this);
        }


        private ArgsTrend GetSelection()
        {
            TrendBase sel = (TrendBase)this._ecbMethod.SelectedItem;
            string title;
            object[] args;

            _checker.Clear();

            // Title / comments
            title = string.IsNullOrWhiteSpace(_txtName.Text) ? null : _txtName.Text;

            // Parameters
            if (sel !=null)
            {
                if (sel.Parameters.HasCustomisableParams)
                {
                    args = sel.Parameters.TryStringToParams( _core, _txtParams.Text );

                    _checker.Check( _txtParams, args != null, "Specify valid parameters for the method" );
                }
                else
                {
                    args = null;
                }
            }
            else
            {
                args = null;
                _checker.Check( _ecbMethod.ComboBox, false, "Select a method" );
            }

            IMatrixProvider src = _ecbSource.SelectedItem;

            _checker.Check( _ecbSource.ComboBox, src != null, "Select a source" );

            if (_checker.HasErrors)
            {
                return null;
            }

            return new ArgsTrend( sel.Id, src, args ) { OverrideDisplayName = title, Comment = _comments };
        }

        public FrmEditConfigurationTrend()
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
            ArgsTrend sel;

            try
            {
                sel = GetSelection();
            }
            catch
            {
                sel = null;
            }

            bool valid = sel != null;
            _txtName.Watermark = sel != null ? sel.DefaultDisplayName : Texts.default_name;

            _lnkError.Visible = false;

            if (sel != null)
            {
                StylisedPeak sPeak = new StylisedPeak(_previewPeak);

                IntensityMatrix source = _ecbSource.SelectedItem.Provide;

                if (_previewPeak != null)
                {
                    try
                    {
                        sPeak.ForceObservations = source.Find( _previewPeak);
                        _chart1.Plot(sPeak);
                    }
                    catch (Exception ex)
                    {
                        _lnkError.Visible = true;
                        _lnkError.Tag = ex.ToString();
                        sPeak.ForceObservations = null;
                        _chart1.Plot( sPeak );
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
                FrmInputMultiLine.ShowFixed(this, Text, "View Comments", _txtName.Text, _comments);
            }
            else
            {
                string newComments = FrmInputMultiLine.Show(this, Text, "Edit Comments", _txtName.Text, _comments);

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

        private void ctlButton3_Click( object sender, EventArgs e )
        {
            PagePreview( 1 );
        }

        private void _lnkError_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e )
        {
            FrmMsgBox.ShowInfo( this, "Error", _lnkError.Tag as string );
        }
    }
}
