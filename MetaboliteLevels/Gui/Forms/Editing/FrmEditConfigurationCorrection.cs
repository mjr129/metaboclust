using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Algorithms.Definitions.Base;
using MetaboliteLevels.Data.Algorithms.Definitions.Base.Misc;
using MetaboliteLevels.Data.Algorithms.Definitions.Corrections;
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
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Gui.Forms.Editing
{
    public partial class FrmEditConfigurationCorrection : Form
    {
        private readonly Core _core;
        private readonly ChartHelperForPeaks _chartOrig;
        private readonly ChartHelperForPeaks _chartChanged;
        private readonly bool _readOnly;

        private Peak _selectedPeak;
        private string _comments;

        private List<GroupInfo> _vTypes = new List<GroupInfo>();
        private List<BatchInfo> _vBatches = new List<BatchInfo>();
        private FlowLayoutPanel _flpBatchButtons;
        private FlowLayoutPanel _flpGroupButtons;
        private EditableComboBox<ObsFilter> _ecbFilter;
        private EditableComboBox<AlgoBase> _ecbMethod;
        private EditableComboBox<GroupInfo> _ecbTypes;
        private EditableComboBox<IMatrixProvider> _ecbSource;

        internal static ArgsCorrection Show( Form owner, Core core, ArgsCorrection def, bool readOnly )
        {
            using (FrmEditConfigurationCorrection frm = new FrmEditConfigurationCorrection( core, def, readOnly, FrmMain.SearchForSelectedPeak( owner ) ))
            {
                if (UiControls.ShowWithDim( owner, frm ) == DialogResult.OK)
                {
                    return frm.GetSelection();
                }

                return null;
            }
        }

        internal FrmEditConfigurationCorrection( Core core, ArgsCorrection def, bool readOnly, Peak previewPeak )
        {
            this.InitializeComponent();
            UiControls.SetIcon( this );

            this._lblPreviewTitle.BackColor = UiControls.PreviewBackColour;
            this._lblPreviewTitle.ForeColor = UiControls.PreviewForeColour;
            this._flpPreviewButtons.BackColor = UiControls.PreviewBackColour;
            this._flpPreviewButtons.ForeColor = UiControls.PreviewForeColour;

            this._core = core;
            this._readOnly = readOnly;
            this._selectedPeak = previewPeak;

            // Charts
            this._chartOrig = new ChartHelperForPeaks( null, this._core, this.panel1 );
            this._chartChanged = new ChartHelperForPeaks( null, this._core, this.panel2 );

            // Choicelists
            this._ecbFilter = DataSet.ForObsFilter( core ).CreateComboBox( this._lstFilter, this._btnFilter, EditableComboBox.EFlags.IncludeAll );
            this._ecbMethod = DataSet.ForTrendAndCorrectionAlgorithms( core ).CreateComboBox( this._lstMethod, this._btnNewStatistic, EditableComboBox.EFlags.None );
            this._ecbTypes = DataSet.ForGroups( this._core ).CreateComboBox( this._lstTypes, this._btnEditTypes, EditableComboBox.EFlags.None );
            this._ecbSource = DataSet.ForMatrixProviders( this._core ).CreateComboBox( this._lstSource, this._btnSource, EditableComboBox.EFlags.None );

            // Buttons
            this.GenerateTypeButtons();

            // Default
            if (def != null)
            {
                this._txtName.Text = def.OverrideDisplayName;
                this._txtParameters.Text = AlgoParameterCollection.ParamsToReversableString( def.Parameters, core );
                this._ecbMethod.SelectedItem = def.GetAlgorithmOrNull();
                this._comments = def.Comment;

                if (def.IsUsingTrend)
                {                                                                   
                    this._radBatch.Checked = def.Mode == ECorrectionMode.Batch;
                    this._radType.Checked = def.Mode == ECorrectionMode.Control;
                    this._radDivide.Checked = def.Method == ECorrectionMethod.Divide;
                    this._radSubtract.Checked = def.Method == ECorrectionMethod.Subtract;
                    this._ecbTypes.SelectedItem = def.ControlGroup;
                    this._ecbFilter.SelectedItem = def.Constraint;
                }     
            }

            this.CheckAndChange();

            if (readOnly)
            {
                UiControls.MakeReadOnly( this, this._tlpPreview );
            }                                             
        }     

        private ArgsCorrection GetSelection()
        {
            this._checker.Clear();

            IMatrixProvider source = this._ecbSource.SelectedItem;

            this._checker.Check( this._ecbSource.ComboBox, source != null, "Select a source" );

            // Algo
            AlgoBase algo = this._ecbMethod.SelectedItem;

            // Params
            object[] parameters;

            if (algo != null)
            {
                string error;
                parameters = algo.Parameters.TryStringToParams( this._core, this._txtParameters.Text, out error );

                this._checker.Check( this._txtParameters, parameters != null, error ?? "error" );
            }
            else
            {
                parameters = null;
                this._checker.Check( this._ecbMethod.ComboBox, false, "Select a correction method" );
            }

            if (algo is TrendBase)
            {
                // Method
                ECorrectionMethod met;

                if (this._radSubtract.Checked)
                {
                    met = ECorrectionMethod.Subtract;
                }
                else if (this._radDivide.Checked)
                {
                    met = ECorrectionMethod.Divide;
                }
                else
                {
                    this._checker.Check( this._radSubtract, false, "Select a method" );
                    this._checker.Check( this._radDivide, false, "Select a method" );
                    met = default( ECorrectionMethod );
                }

                // Mode
                ECorrectionMode mode;
                GroupInfo controlGroup;
                ObsFilter filter;

                if (this._radBatch.Checked)
                {
                    mode = ECorrectionMode.Batch;
                    controlGroup = null;

                    this._checker.Check( this._ecbFilter.ComboBox, this._ecbFilter.HasSelection, "Select a filter" );
                    filter = this._ecbFilter.SelectedItem;
                }
                else if (this._radType.Checked)
                {
                    mode = ECorrectionMode.Control;
                    controlGroup = this._ecbTypes.SelectedItem;
                    filter = null;
                }
                else
                {
                    this._checker.Check( this._radBatch, false, "Select a mode" );
                    this._checker.Check( this._radType, false, "Select a mode" );
                    controlGroup = default( GroupInfo );
                    filter = default( ObsFilter );
                    mode = default( ECorrectionMode );
                }

                if (this._checker.HasErrors)
                {
                    return null;
                }

                ArgsCorrection args = new ArgsCorrection( ((TrendBase)algo).Id, source, parameters, mode, met, controlGroup, filter )
                {
                    OverrideDisplayName = this._txtName.Text,
                    Comment = this._comments
                };
                return args;
            }
            else if (algo is CorrectionBase)
            {
                if (this._checker.HasErrors)
                {
                    return null;
                }

                ArgsCorrection args = new ArgsCorrection( ((CorrectionBase)algo).Id, source, parameters, ECorrectionMode.None, ECorrectionMethod.None, null, null )
                {
                    OverrideDisplayName = this._txtName.Text,
                    Comment = this._comments
                };
                return args;
            }
            else
            {
                return null;
            }
        }

        void CheckAndChange()
        {
            AlgoBase trend = (AlgoBase)this._ecbMethod.SelectedItem;

            bool paramsVisible = trend != null && trend.Parameters.HasCustomisableParams;

            this._lblParams.Visible = paramsVisible;
            this._txtParameters.Visible = paramsVisible;
            this._btnEditParameters.Visible = paramsVisible;
            this._lblParams.Text = paramsVisible ? trend.Parameters.ParamNames() : "Parameters";

            bool usingTrend = trend is TrendBase;
            bool correctorVisible = usingTrend;

            this._lblCorrector.Visible = correctorVisible;
            this.tableLayoutPanel3.Visible = correctorVisible;
            this._lstTypes.Visible = correctorVisible && this._radType.Checked;
            this._btnEditTypes.Visible = correctorVisible;
            this._btnBatchInfo2.Visible = correctorVisible;

            bool filterVisible = correctorVisible && this._radBatch.Checked;

            this._lblSepFilter.Visible = filterVisible;
            this._lblAVec.Visible = filterVisible;
            this._ecbFilter.Visible = filterVisible;

            bool operatorVisible = correctorVisible && ((this._radType.Checked && this._ecbTypes.HasSelection) || this._radBatch.Checked);

            this._lblCorrector2.Visible = operatorVisible;
            this.tableLayoutPanel4.Visible = operatorVisible;

            bool readyToGo = (usingTrend && operatorVisible && (this._radDivide.Checked || this._radSubtract.Checked)) || (!usingTrend);

            ArgsCorrection sel = this.GetSelection();
            this._txtName.Watermark = sel != null ? sel.DefaultDisplayName : Resx.Texts.default_name;
            bool valid = sel != null;

            this._tlpPreview.Visible = valid;
            this._btnOk.Enabled = valid;

            this._flpGroupButtons.Visible = (!usingTrend || this._radType.Checked);
            this._flpBatchButtons.Visible = (usingTrend && this._radBatch.Checked);

            if (valid)
            {
                this.GeneratePreview( sel );
            }
        }  

        private void _btnSelectPreview_Click( object sender, EventArgs e )
        {
            var sel = DataSet.ForPeaks( this._core ).ShowList( this, this._selectedPeak );

            if (sel != null)
            {
                this._selectedPeak = sel;
                this.CheckAndChange();
            }
        }

        /// <summary>
        /// Generates the preview image.
        /// </summary>
        /// <param name="sel">Correction to generate the preview for</param>
        private void GeneratePreview( ArgsCorrection sel )
        {
            // No error unless otherwise
            this._lnkError.Visible = false;

            // No selection, no preview
            if (sel == null)
            {
                this.SetPreviewError( "Nothing to preview", null );
                return;
            }

            // No peak, no preview
            if (this._selectedPeak == null)
            {
                this.SetPreviewError( "No peak selected", null );
                return;
            }

            // Title
            this._lblPreviewTitle.Text = "Preview (" + this._selectedPeak.DisplayName + ")";

            // Get source matrix
            IntensityMatrix source = sel.SourceProvider?.Provide;

            if (source == null)
            {
                this.SetPreviewError( StrRes.NoPreview, StrRes.MissingSourceDetails( sel ) );
                return;
            }

            // Get vectors
            Vector original = source.Find( this._selectedPeak );
            Vector trend;
            Vector corrected;

            ConfigurationCorrection temp = new ConfigurationCorrection() { Args = sel };

            try
            {
                IReadOnlyList<ObservationInfo> order;
                double[] trendData = temp.ExtractTrend( this._core, original.Values, out order );
                trend = (trendData != null) ? new Vector( trendData, original.Header, order.Select( z => new IntensityMatrix.ColumnHeader( z ) ).ToArray() ) : null;
                corrected = new Vector( temp.Calculate( this._core, original.Values ), original.Header, original.ColHeaders );
            }
            catch (Exception ex)
            {
                this._chartOrig.Plot( null );
                this._chartChanged.Plot( null );
                this._lnkError.Text = StrRes.PreviewError;
                this._lnkError.Tag = ex.ToString();
                this._lnkError.Visible = true;
                return;
            }

            // Special flag for batch ordering
            bool isBatchMode;

            if (sel.IsUsingTrend)
            {
                isBatchMode = sel.Mode == ECorrectionMode.Batch;
            }
            else
            {
                isBatchMode = false;
            }

            // Create displays
            StylisedPeak oldDisplay = new StylisedPeak( this._selectedPeak )
            {
                OverrideDefaultOptions = new StylisedPeakOptions( this._core )
                {
                    ShowAcqisition = isBatchMode,
                    ViewBatches = this._vBatches,
                    ViewGroups = this._vTypes,
                    ConditionsSideBySide = true,
                    ShowPoints = true,
                    ShowTrend = sel.IsUsingTrend,
                    ShowRanges = false,
                },

                ForceTrend = trend
            };

            StylisedPeak newDisplay = new StylisedPeak( this._selectedPeak )
            {
                OverrideDefaultOptions = oldDisplay.OverrideDefaultOptions.Clone(),
                ForceObservations = corrected
            };

            newDisplay.OverrideDefaultOptions.ShowTrend = false;

            // Draw it!
            this._chartOrig.Plot( oldDisplay );
            this._chartChanged.Plot( newDisplay );
        }

        /// <summary>
        /// Sets the preview error text and clears the preview window.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="details"></param>
        private void SetPreviewError( string message, string details )
        {
            this._lnkError.Text = message;
            this._lnkError.Tag = details;
            this._lnkError.Visible = true;
            this._chartOrig.ClearPlot();
            this._chartChanged.ClearPlot();
        }

        private void GenerateTypeButtons()
        {
            this._vTypes.AddRange( this._core.Groups );
            this._vBatches.Add( this._core.Batches.OrderBy( z => z.DisplayPriority ).First() );

            this._flpBatchButtons = new FlowLayoutPanel();
            this._flpGroupButtons = new FlowLayoutPanel();

            this._flpBatchButtons.Margin = this._btnPreviousPreview.Margin;
            this._flpGroupButtons.Margin = this._btnPreviousPreview.Margin;

            this._flpBatchButtons.AutoSize = true;
            this._flpGroupButtons.AutoSize = true;

            this._flpBatchButtons.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this._flpGroupButtons.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            this._flpPreviewButtons.Controls.Add( this._flpBatchButtons );
            this._flpPreviewButtons.Controls.Add( this._flpGroupButtons );

            foreach (var ti in this._core.Groups.OrderBy( z => z.DisplayPriority ))
            {
                this._flpGroupButtons.Controls.Add( this.GenerateTypeButton( ti ) );
            }

            foreach (var ti in this._core.Batches.OrderBy( z => z.DisplayPriority ))
            {
                this._flpBatchButtons.Controls.Add( this.GenerateTypeButton( ti ) );
            }
        }

        private CtlButton GenerateTypeButton( GroupInfoBase ti )
        {
            CtlButton btn = new CtlButton();
            btn.Tag = ti;
            this.UpdateImage( btn );
            btn.UseDefaultSize = true;
            btn.Click += this.btn_Click;
            btn.Margin = new Padding( 0, 0, 0, 0 );
            btn.Visible = true;
            this.toolTip1.SetToolTip( btn, "View " + ti.DisplayName );
            return btn;
        }

        void btn_Click( object sender, EventArgs e )
        {
            CtlButton btn = (CtlButton)sender;
            GroupInfo g = btn.Tag as GroupInfo;

            if (g != null)
            {
                if (this._vTypes.Contains( g ))
                {
                    this._vTypes.Remove( g );
                }
                else
                {
                    this._vTypes.Add( g );
                }
            }
            else
            {
                BatchInfo b = (BatchInfo)btn.Tag;

                if (this._vBatches.Contains( b ))
                {
                    this._vBatches.Remove( b );
                }
                else
                {
                    this._vBatches.Add( b );
                }
            }

            this.UpdateImage( btn );
            this.GeneratePreview( this.GetSelection() );
        }

        private void UpdateImage( CtlButton btn )
        {
            if (btn.Image != null)
            {
                btn.Image.Dispose();
            }

            GroupInfo g = btn.Tag as GroupInfo;

            bool vis;

            if (g != null)
            {
                vis = this._vTypes.Contains( g );
            }
            else
            {
                BatchInfo b = (BatchInfo)btn.Tag;
                vis = this._vBatches.Contains( b );
            }

            btn.Image = UiControls.CreateExperimentalGroupImage( vis, (GroupInfoBase)btn.Tag, false );
        }

        private void _btnOk_Click( object sender, EventArgs e )
        {

        }

        private void _btnComment_Click( object sender, EventArgs e )
        {
            if (this._readOnly)
            {
                FrmInputMultiLine.ShowFixed( this, this.Text, "View Comments", this._txtName.Text, this._comments );
            }
            else
            {
                string newComments = FrmInputMultiLine.Show( this, this.Text, "Edit Comments", this._txtName.Text, this._comments );

                if (newComments != null)
                {
                    this._comments = newComments;
                }
            }
        }

        private void anything_SomethingChanged( object sender, EventArgs e )
        {
            this.CheckAndChange();
        }

        private void _btnPreviousPreview_Click( object sender, EventArgs e )
        {
            this.PagePreview( -1 );
        }

        private void PagePreview( int direction )
        {
            int index = this._core.Peaks.IndexOf( this._selectedPeak ) + direction;

            if (index <= -1)
            {
                index = this._core.Peaks.Count - 1;
            }

            if (index >= this._core.Peaks.Count)
            {
                index = 0;
            }

            this._selectedPeak = this._core.Peaks[index];

            this.GeneratePreview( this.GetSelection() );
        }

        private void _btnNextPreview_Click( object sender, EventArgs e )
        {
            this.PagePreview( 1 );
        }

        private void _btnEditParameters_Click( object sender, EventArgs e )
        {
            AlgoBase trend = (AlgoBase)this._ecbMethod.SelectedItem;
            FrmEditParameters.Show( trend, this._txtParameters, this._core, this._readOnly );
        }

        private void _btnBatchInfo_Click( object sender, EventArgs e )
        {
            DataSet.ForBatches( this._core ).ShowListEditor( this );
        }

        private void _radType_CheckedChanged( object sender, EventArgs e )
        {
            if (this._radType.Checked)
            {
                this._radBatch.Checked = false;
            }

            this.anything_SomethingChanged( sender, e );
        }

        private void _radBatch_CheckedChanged( object sender, EventArgs e )
        {
            if (this._radBatch.Checked)
            {
                this._radType.Checked = false;
            }

            this.anything_SomethingChanged( sender, e );
        }

        private void _lnkError_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e )
        {
            FrmMsgBox.ShowInfo( this, "Error details", this._lnkError.Tag as string );
        }
    }
}
