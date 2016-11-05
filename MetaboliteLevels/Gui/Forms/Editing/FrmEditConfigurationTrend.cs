using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Algorithms.Definitions.Base.Misc;
using MetaboliteLevels.Data.Algorithms.Definitions.Trends;
using MetaboliteLevels.Data.Algorithms.General;
using MetaboliteLevels.Data.Database;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Session.Main;
using MetaboliteLevels.Gui.Controls;
using MetaboliteLevels.Gui.Controls.Charts;
using MetaboliteLevels.Gui.Datatypes;
using MetaboliteLevels.Gui.Forms.Activities;
using MetaboliteLevels.Gui.Forms.Selection;
using MetaboliteLevels.Gui.Forms.Text;
using MetaboliteLevels.Resx;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Gui.Forms.Editing
{
    /// <summary>
    /// Editor for <see cref="ArgsTrend"/>
    /// </summary>
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
            this._core = core;
            this._previewPeak = previewPeak;

            this._ecbMethod = DataSet.ForTrendAlgorithms(core).CreateComboBox(this._lstMethod, this._btnNewStatistic, EditableComboBox.EFlags.None);
            this._ecbSource = DataSet.ForMatrixProviders( core ).CreateComboBox( this._lstSource, this._btnSource, EditableComboBox.EFlags.None );

            this._chart1 = new ChartHelperForPeaks(null, core, this.panel1);

            if (def != null)
            {
                this._txtName.Text = def.OverrideDisplayName;
                this._ecbMethod.SelectedItem = (TrendBase)def.GetAlgorithmOrNull();
                this._comments = def.Comment;
                this._txtParams.Text = AlgoParameterCollection.ParamsToReversableString(def.Parameters, core);
                this._ecbSource.SelectedItem = def.SourceProvider;
            }

            this.CheckAndChange(null, null);

            this._readOnly = readOnly;

            if (readOnly)
            {
                UiControls.MakeReadOnly(this, this._tlpPreview);

                this._btnComment.Enabled = true;

                this.ctlTitleBar1.Text = "View Trend";
            }
            else if (def != null)
            {
                this.ctlTitleBar1.Text = "Edit Trend";
            }
            else
            {
                this.ctlTitleBar1.Text = "New Trend";
            }

            // UiControls.CompensateForVisualStyles(this);
        }


        private ArgsTrend GetSelection()
        {
            TrendBase sel = (TrendBase)this._ecbMethod.SelectedItem;
            string title;
            object[] args;

            this._checker.Clear();

            // Title / comments
            title = string.IsNullOrWhiteSpace(this._txtName.Text) ? null : this._txtName.Text;

            // Parameters
            if (sel !=null)
            {
                if (sel.Parameters.HasCustomisableParams)
                {
                    string error;
                    args = sel.Parameters.TryStringToParams( this._core, this._txtParams.Text, out error );

                    this._checker.Check( this._txtParams, args != null, error ?? "error" );
                }
                else
                {
                    args = null;
                }
            }
            else
            {
                args = null;
                this._checker.Check( this._ecbMethod.ComboBox, false, "Select a method" );
            }

            IMatrixProvider src = this._ecbSource.SelectedItem;

            this._checker.Check( this._ecbSource.ComboBox, src != null, "Select a source" );

            if (this._checker.HasErrors)
            {
                return null;
            }

            return new ArgsTrend( sel.Id, src, args ) { OverrideDisplayName = title, Comment = this._comments };
        }

        public FrmEditConfigurationTrend()
        {
            this.InitializeComponent();
            UiControls.SetIcon(this);

            this._lblPreviewTitle.BackColor = UiControls.PreviewBackColour;
            this._lblPreviewTitle.ForeColor = UiControls.PreviewForeColour;
            this._flpPreviewButtons.BackColor = UiControls.PreviewBackColour;
            this._flpPreviewButtons.ForeColor = UiControls.PreviewForeColour; 
        }

        private void Check(object sender, EventArgs e)
        {
            ArgsTrend sel;

            try
            {
                sel = this.GetSelection();
            }
            catch
            {
                sel = null;
            }

            bool valid = sel != null;
            this._txtName.Watermark = sel != null ? sel.DefaultDisplayName : Texts.default_name;

            this._lnkError.Visible = false;

            if (sel != null)
            {
                StylisedPeak sPeak = new StylisedPeak(this._previewPeak);

                ConfigurationTrend temporary = new ConfigurationTrend() { Args = sel };
                IntensityMatrix source = this._ecbSource.SelectedItem.Provide;

                if (this._previewPeak != null)
                {
                    try
                    {                                                        
                        sPeak.OverrideDefaultOptions = new StylisedPeakOptions( this._core )
                        {
                            SelectedTrend = temporary
                        };
                        this._chart1.Plot(sPeak);                         
                    }
                    catch (Exception ex)
                    {
                        this._lnkError.Visible = true;
                        this._lnkError.Tag = ex.ToString();
                        sPeak.OverrideDefaultOptions = null;
                        this._chart1.Plot( sPeak );
                    }
                }
            }

            this._btnOk.Enabled = valid;
            this._tlpPreview.Visible = valid;
        }

        private void CheckAndChange(object sender, EventArgs e)
        {
            var sm = (TrendBase)this._ecbMethod.SelectedItem;
            bool s = sm != null && sm.Parameters.HasCustomisableParams;

            this._lblParams.Visible = s;
            this._txtParams.Visible = s;
            this._btnEditParameters.Visible = s;
            this._lblParams.Text = s ? sm.Parameters.ParamNames() : "Parameters";

            this.Check(sender, e);
        }

        private void _btnComment_Click(object sender, EventArgs e)
        {
            if (this._readOnly)
            {
                FrmInputMultiLine.ShowFixed(this, this.Text, "View Comments", this._txtName.Text, this._comments);
            }
            else
            {
                string newComments = FrmInputMultiLine.Show(this, this.Text, "Edit Comments", this._txtName.Text, this._comments);

                if (newComments != null)
                {
                    this._comments = newComments;
                }
            }
        }   

        private void _btnSelectPreview_Click(object sender, EventArgs e)
        {
            var newSel = DataSet.ForPeaks(this._core).ShowList(this, this._previewPeak);

            if (newSel != null)
            {
                this._previewPeak = newSel;
                this.Check(sender, e);
            }
        }

        private void _btnPreviousPreview_Click(object sender, EventArgs e)
        {
            this.PagePreview(-1);
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

            this.Check(null, null);
        }

        private void _btnEditParameters_Click(object sender, EventArgs e)
        {
            var sm = (TrendBase)this._ecbMethod.SelectedItem;
            FrmEditParameters.Show(sm, this._txtParams, this._core, this._readOnly);
        }

        private void ctlButton3_Click( object sender, EventArgs e )
        {
            this.PagePreview( 1 );
        }

        private void _lnkError_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e )
        {
            FrmMsgBox.ShowInfo( this, "Error", this._lnkError.Tag as string );
        }
    }
}
