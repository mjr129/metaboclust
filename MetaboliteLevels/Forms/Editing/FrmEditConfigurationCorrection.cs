using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Controls;
using MetaboliteLevels.Controls.Charts;
using MetaboliteLevels.Data.Algorithms.Definitions.Base;
using MetaboliteLevels.Data.Algorithms.Definitions.Configurations;
using MetaboliteLevels.Data.Algorithms.Definitions.Corrections;
using MetaboliteLevels.Data.Algorithms.Definitions.Trends;
using MetaboliteLevels.Data.Algorithms.General;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Session.Singular;
using MetaboliteLevels.Forms.Activities;
using MetaboliteLevels.Forms.Selection;
using MetaboliteLevels.Forms.Text;
using MetaboliteLevels.Types.UI;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Forms.Editing
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
            InitializeComponent();
            UiControls.SetIcon( this );

            _lblPreviewTitle.BackColor = UiControls.PreviewBackColour;
            _lblPreviewTitle.ForeColor = UiControls.PreviewForeColour;
            _flpPreviewButtons.BackColor = UiControls.PreviewBackColour;
            _flpPreviewButtons.ForeColor = UiControls.PreviewForeColour;

            _core = core;
            _readOnly = readOnly;
            _selectedPeak = previewPeak;

            // Charts
            _chartOrig = new ChartHelperForPeaks( null, _core, panel1 );
            _chartChanged = new ChartHelperForPeaks( null, _core, panel2 );

            // Choicelists
            _ecbFilter = DataSet.ForObsFilter( core ).CreateComboBox( _lstFilter, _btnFilter, ENullItemName.All );
            _ecbMethod = DataSet.ForTrendAndCorrectionAlgorithms( core ).CreateComboBox( _lstMethod, _btnNewStatistic, ENullItemName.NoNullItem );
            _ecbTypes = DataSet.ForGroups( _core ).CreateComboBox( _lstTypes, _btnEditTypes, ENullItemName.NoNullItem );
            _ecbSource = DataSet.ForMatrixProviders( _core ).CreateComboBox( _lstSource, _btnSource, ENullItemName.NoNullItem );

            // Buttons
            GenerateTypeButtons();

            // Default
            if (def != null)
            {
                _txtName.Text = def.OverrideDisplayName;
                _txtParameters.Text = AlgoParameterCollection.ParamsToReversableString( def.Parameters, core );
                _ecbMethod.SelectedItem = def.GetAlgorithmOrNull();
                _comments = def.Comment;

                if (def.IsUsingTrend)
                {                                                                   
                    _radBatch.Checked = def.Mode == ECorrectionMode.Batch;
                    _radType.Checked = def.Mode == ECorrectionMode.Control;
                    _radDivide.Checked = def.Method == ECorrectionMethod.Divide;
                    _radSubtract.Checked = def.Method == ECorrectionMethod.Subtract;
                    _ecbTypes.SelectedItem = def.ControlGroup;
                    _ecbFilter.SelectedItem = def.Constraint;
                }     
            }

            CheckAndChange();

            if (readOnly)
            {
                UiControls.MakeReadOnly( this, _tlpPreview );
            }                                             
        }     

        private ArgsCorrection GetSelection()
        {
            _checker.Clear();

            IMatrixProvider source = _ecbSource.SelectedItem;

            _checker.Check( _ecbSource.ComboBox, source != null, "Select a source" );

            // Algo
            AlgoBase algo = _ecbMethod.SelectedItem;

            // Params
            object[] parameters;

            if (algo != null)
            {
                parameters = algo.Parameters.TryStringToParams( _core, _txtParameters.Text );

                _checker.Check( _txtParameters, parameters != null, "Specify valid parameters for the algorithm." );
            }
            else
            {
                parameters = null;
                _checker.Check( _ecbMethod.ComboBox, false, "Select a correction method" );
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
                    _checker.Check( _radSubtract, false, "Select a method" );
                    _checker.Check( _radDivide, false, "Select a method" );
                    met = default( ECorrectionMethod );
                }

                // Mode
                ECorrectionMode mode;
                GroupInfo controlGroup;
                ObsFilter filter;

                if (_radBatch.Checked)
                {
                    mode = ECorrectionMode.Batch;
                    controlGroup = null;

                    _checker.Check( _ecbFilter.ComboBox, _ecbFilter.HasSelection, "Select a filter" );
                    filter = _ecbFilter.SelectedItem;
                }
                else if (_radType.Checked)
                {
                    mode = ECorrectionMode.Control;
                    controlGroup = _ecbTypes.SelectedItem;
                    filter = null;
                }
                else
                {
                    _checker.Check( _radBatch, false, "Select a mode" );
                    _checker.Check( _radType, false, "Select a mode" );
                    controlGroup = default( GroupInfo );
                    filter = default( ObsFilter );
                    mode = default( ECorrectionMode );
                }

                if (_checker.HasErrors)
                {
                    return null;
                }

                ArgsCorrection args = new ArgsCorrection( ((TrendBase)algo).Id, source, parameters, mode, met, controlGroup, filter )
                {
                    OverrideDisplayName = _txtName.Text,
                    Comment = _comments
                };
                return args;
            }
            else if (algo is CorrectionBase)
            {
                if (_checker.HasErrors)
                {
                    return null;
                }

                ArgsCorrection args = new ArgsCorrection( ((CorrectionBase)algo).Id, source, parameters, ECorrectionMode.None, ECorrectionMethod.None, null, null )
                {
                    OverrideDisplayName = _txtName.Text,
                    Comment = _comments
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
            AlgoBase trend = (AlgoBase)_ecbMethod.SelectedItem;

            bool paramsVisible = trend != null && trend.Parameters.HasCustomisableParams;

            _lblParams.Visible = paramsVisible;
            _txtParameters.Visible = paramsVisible;
            _btnEditParameters.Visible = paramsVisible;
            _lblParams.Text = paramsVisible ? trend.Parameters.ParamNames() : "Parameters";

            bool usingTrend = trend is TrendBase;
            bool correctorVisible = usingTrend;

            _lblCorrector.Visible = correctorVisible;
            tableLayoutPanel3.Visible = correctorVisible;
            _lstTypes.Visible = correctorVisible && _radType.Checked;
            _btnEditTypes.Visible = correctorVisible;
            _btnBatchInfo2.Visible = correctorVisible;

            bool filterVisible = correctorVisible && _radBatch.Checked;

            _lblSepFilter.Visible = filterVisible;
            _lblAVec.Visible = filterVisible;
            _ecbFilter.Visible = filterVisible;

            bool operatorVisible = correctorVisible && ((_radType.Checked && _ecbTypes.HasSelection) || _radBatch.Checked);

            _lblCorrector2.Visible = operatorVisible;
            tableLayoutPanel4.Visible = operatorVisible;

            bool readyToGo = (usingTrend && operatorVisible && (_radDivide.Checked || _radSubtract.Checked)) || (!usingTrend);

            ArgsCorrection sel = GetSelection();
            _txtName.Watermark = sel != null ? sel.DefaultDisplayName : Resx.Texts.default_name;
            bool valid = sel != null;

            _tlpPreview.Visible = valid;
            _btnOk.Enabled = valid;

            _flpGroupButtons.Visible = (!usingTrend || _radType.Checked);
            _flpBatchButtons.Visible = (usingTrend && _radBatch.Checked);

            if (valid)
            {
                GeneratePreview( sel );
            }
        }  

        private void _btnSelectPreview_Click( object sender, EventArgs e )
        {
            var sel = DataSet.ForPeaks( _core ).ShowList( this, _selectedPeak );

            if (sel != null)
            {
                _selectedPeak = sel;
                CheckAndChange();
            }
        }

        /// <summary>
        /// Generates the preview image.
        /// </summary>
        /// <param name="sel">Correction to generate the preview for</param>
        private void GeneratePreview( ArgsCorrection sel )
        {
            // No error unless otherwise
            _lnkError.Visible = false;

            // No selection, no preview
            if (sel == null)
            {
                SetPreviewError( "Nothing to preview", null );   
                return;
            }

            // No peak, no preview
            if (_selectedPeak == null)
            {
                SetPreviewError( "No peak selected", null );          
                return;
            }

            // Title
            _lblPreviewTitle.Text = "Preview (" + _selectedPeak.DisplayName + ")";

            // Get source matrix
            IntensityMatrix source = sel.SourceMatrix;

            if (source == null)
            {                                      
                SetPreviewError( StrRes.NoPreview, StrRes.MissingSourceDetails( sel ) );
                return;
            }                

            // Get vectors
            Vector original = source.Find( _selectedPeak );
            Vector trend;
            Vector corrected;
            
            ConfigurationCorrection temp = new ConfigurationCorrection() { Args = sel };

            try
            {
                IReadOnlyList<ObservationInfo> order;
                double[] trendData = temp.ExtractTrend( _core, original.Values, out order );
                trend = (trendData != null) ? new Vector( trendData, original.Header, order.Select( z => new IntensityMatrix.ColumnHeader( z ) ).ToArray() ) : null;
                corrected = new Vector( temp.Calculate( _core, original.Values ), original.Header, original.ColHeaders );
            }
            catch (Exception ex)
            {
                _chartOrig.Plot( null );
                _chartChanged.Plot( null );
                _lnkError.Text = StrRes.PreviewError;
                _lnkError.Tag = ex.ToString();
                _lnkError.Visible = true;
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
            StylisedPeak oldDisplay = new StylisedPeak( _selectedPeak )
            {
                OverrideDefaultOptions = new StylisedPeakOptions( _core )
                {
                    ShowAcqisition = isBatchMode,
                    ViewBatches = _vBatches,
                    ViewGroups = _vTypes,
                    ConditionsSideBySide = true,
                    ShowPoints = true,
                    ShowTrend = sel.IsUsingTrend,
                    ShowRanges = false,
                },

                ForceTrend = trend
            };

            StylisedPeak newDisplay = new StylisedPeak( _selectedPeak )
            {
                OverrideDefaultOptions = new StylisedPeakOptions( oldDisplay.OverrideDefaultOptions )
                {
                    ShowTrend = false
                },

                ForceObservations = corrected
            };                               

            // Draw it!
            _chartOrig.Plot( oldDisplay );
            _chartChanged.Plot( newDisplay );
        }

        /// <summary>
        /// Sets the preview error text and clears the preview window.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="details"></param>
        private void SetPreviewError( string message, string details )
        {
            _lnkError.Text = message;
            _lnkError.Tag = details;
            _lnkError.Visible = true;
            _chartOrig.ClearPlot();
            _chartChanged.ClearPlot();
        }

        private void GenerateTypeButtons()
        {
            _vTypes.AddRange( _core.Groups );
            _vBatches.Add( _core.Batches.OrderBy( z => z.DisplayPriority ).First() );

            _flpBatchButtons = new FlowLayoutPanel();
            _flpGroupButtons = new FlowLayoutPanel();

            _flpBatchButtons.Margin = _btnPreviousPreview.Margin;
            _flpGroupButtons.Margin = _btnPreviousPreview.Margin;

            _flpBatchButtons.AutoSize = true;
            _flpGroupButtons.AutoSize = true;

            _flpBatchButtons.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            _flpGroupButtons.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            _flpPreviewButtons.Controls.Add( _flpBatchButtons );
            _flpPreviewButtons.Controls.Add( _flpGroupButtons );

            foreach (var ti in _core.Groups.OrderBy( z => z.DisplayPriority ))
            {
                _flpGroupButtons.Controls.Add( GenerateTypeButton( ti ) );
            }

            foreach (var ti in _core.Batches.OrderBy( z => z.DisplayPriority ))
            {
                _flpBatchButtons.Controls.Add( GenerateTypeButton( ti ) );
            }
        }

        private CtlButton GenerateTypeButton( GroupInfoBase ti )
        {
            CtlButton btn = new CtlButton();
            btn.Tag = ti;
            UpdateImage( btn );
            btn.UseDefaultSize = true;
            btn.Click += btn_Click;
            btn.Margin = new Padding( 0, 0, 0, 0 );
            btn.Visible = true;
            toolTip1.SetToolTip( btn, "View " + ti.DisplayName );
            return btn;
        }

        void btn_Click( object sender, EventArgs e )
        {
            CtlButton btn = (CtlButton)sender;
            GroupInfo g = btn.Tag as GroupInfo;

            if (g != null)
            {
                if (_vTypes.Contains( g ))
                {
                    _vTypes.Remove( g );
                }
                else
                {
                    _vTypes.Add( g );
                }
            }
            else
            {
                BatchInfo b = (BatchInfo)btn.Tag;

                if (_vBatches.Contains( b ))
                {
                    _vBatches.Remove( b );
                }
                else
                {
                    _vBatches.Add( b );
                }
            }

            UpdateImage( btn );
            this.GeneratePreview( GetSelection() );
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
                vis = _vTypes.Contains( g );
            }
            else
            {
                BatchInfo b = (BatchInfo)btn.Tag;
                vis = _vBatches.Contains( b );
            }

            btn.Image = UiControls.CreateExperimentalGroupImage( vis, (GroupInfoBase)btn.Tag, false );
        }

        private void _btnOk_Click( object sender, EventArgs e )
        {

        }

        private void _btnComment_Click( object sender, EventArgs e )
        {
            if (_readOnly)
            {
                FrmInputMultiLine.ShowFixed( this, Text, "View Comments", _txtName.Text, _comments );
            }
            else
            {
                string newComments = FrmInputMultiLine.Show( this, Text, "Edit Comments", _txtName.Text, _comments );

                if (newComments != null)
                {
                    _comments = newComments;
                }
            }
        }

        private void anything_SomethingChanged( object sender, EventArgs e )
        {
            CheckAndChange();
        }

        private void _btnPreviousPreview_Click( object sender, EventArgs e )
        {
            PagePreview( -1 );
        }

        private void PagePreview( int direction )
        {
            int index = _core.Peaks.IndexOf( _selectedPeak ) + direction;

            if (index <= -1)
            {
                index = _core.Peaks.Count - 1;
            }

            if (index >= _core.Peaks.Count)
            {
                index = 0;
            }

            _selectedPeak = _core.Peaks[index];

            GeneratePreview( GetSelection() );
        }

        private void _btnNextPreview_Click( object sender, EventArgs e )
        {
            PagePreview( 1 );
        }

        private void _btnEditParameters_Click( object sender, EventArgs e )
        {
            AlgoBase trend = (AlgoBase)_ecbMethod.SelectedItem;
            FrmEditParameters.Show( trend, _txtParameters, _core, _readOnly );
        }

        private void _btnBatchInfo_Click( object sender, EventArgs e )
        {
            DataSet.ForBatches( _core ).ShowListEditor( this );
        }

        private void _radType_CheckedChanged( object sender, EventArgs e )
        {
            if (_radType.Checked)
            {
                _radBatch.Checked = false;
            }

            anything_SomethingChanged( sender, e );
        }

        private void _radBatch_CheckedChanged( object sender, EventArgs e )
        {
            if (_radBatch.Checked)
            {
                _radType.Checked = false;
            }

            anything_SomethingChanged( sender, e );
        }

        private void _lnkError_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e )
        {
            FrmMsgBox.ShowInfo( this, "Error details", _lnkError.Tag as string );
        }
    }
}
