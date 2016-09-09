using MCharting;
using MetaboliteLevels.Algorithms;
using MetaboliteLevels.Data.Visualisables;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Algorithms.Statistics.Arguments;
using MetaboliteLevels.Data.DataInfo;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Forms.Algorithms;
using MetaboliteLevels.Settings;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Forms.Generic;
using MetaboliteLevels.Viewers.Lists;
using MetaboliteLevels.Properties;
using System.Collections.ObjectModel;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Data.Algorithms.Definitions.Configurations;
using MetaboliteLevels.Data.General;
using MGui.Helpers;
using MGui.Datatypes;
using MetaboliteLevels.Viewers.Charts;
using MetaboliteLevels.Data.Session.Associational;

namespace MetaboliteLevels.Forms.Editing
{
    /// <summary>
    /// Shows a PCA plot.
    /// </summary>
    public partial class FrmPca : Form
    {
        private Core _core;

        private ObsFilter _obsFilter;
        private PeakFilter _peakFilter;
        private bool _transposeToShowPeaks;

        private int _component;

        private double[,] _scores;
        

        private EPlotSource _plotSource;
        private double[,] _loadings;
        private Peak[] _pcaPeaks;
        private ObservationInfo[] _pcaObservations;
        private IProvider<IntensityMatrix> _selectedCorrection;


        SourceSet _colourBy = new SourceSet();
        SourceSet _regressAgainst = new SourceSet();


        private string _errorMessage;
        private EMethod _method;
        private readonly ISelectionHolder _frmMain;

        class SourceSet
        {
            public  Column _colourByPeak;
            public  Column _colourByObervation;
        }

        internal static void Show(FrmMain owner, Core core)
        {
            //using (FrmPca frm = new FrmPca(core, owner))
            //{
            FrmPca frm = new FrmPca( core, owner );
            frm.Show( owner );
            //}
        }    

        private FrmPca(Core core, FrmMain frmMain)
        {
            InitializeComponent();
            UiControls.SetIcon( this );
            UiControls.ColourMenuButtons( this.toolStrip1 );

            this._frmMain = frmMain;
            this._core = core;

            _peakFilter = PeakFilter.Empty;
            _obsFilter = ObsFilter.Empty;

            this._selectedCorrection = IVisualisableExtensions.WhereEnabled(core.AllCorrections).Any() ? IVisualisableExtensions.WhereEnabled(core.AllCorrections).Last() : null;

            _colourBy._colourByPeak = ColumnManager.GetColumns<Peak>(_core).First(z => z.Id == Peak.ID_COLUMN_CLUSTERCOMBINATION);
            _colourBy._colourByObervation = ColumnManager.GetColumns<ObservationInfo>(_core).First(z => z.Id == ObservationInfo.ID_COLNAME_GROUP);
            _regressAgainst._colourByPeak = _colourBy._colourByPeak;
            _regressAgainst._colourByObervation = _colourBy._colourByObervation;

            this._chart.AddControls( this.toolStripDropDownButton1);

            UpdateScores();
        }

        class Factoriser
        {
            readonly Dictionary<object, int> _factors = new Dictionary<object, int>();

            internal double Factor( object row )
            {
                if (row == null)
                {
                    return -1;
                }

                if (!(row is string) && (row is IConvertible))
                {
                    return ((IConvertible)row).ToDouble( null );
                }

                int f;

                if (_factors.TryGetValue( row, out f ))
                {
                    return f;
                }

                f = _factors.Count;
                _factors.Add( row, f );
                return f;
            }
        }

        private void UpdateScores()
        {
            StringBuilder title = new StringBuilder();

            IntensityMatrix sourceMatrix = _selectedCorrection.Provide;
            IntensityMatrix source = sourceMatrix.Subset( _peakFilter, _obsFilter );
            
            double[] plsrResponseMatrix = null;             

            _lblPcaSource.Text = _transposeToShowPeaks ? "Peaks" : "Observations";
            transposeToShowObservationsToolStripMenuItem.Checked = !_transposeToShowPeaks;
            transposeToShowPeaksToolStripMenuItem.Checked = _transposeToShowPeaks;           

            _lblObs.Text = _obsFilter.ToString();
            _lblPeaks.Text = _peakFilter.ToString();
            _lblCorrections.Text = _selectedCorrection.ToString();

            _lblMethod.Text = _method.ToString().ToUpper();
            ctlTitleBar1.Text = _method.ToUiString();

            _lblPlsrSource.Visible = _mnuPlsrSource.Visible = _method == EMethod.Plsr;

            int corIndex = IVisualisableExtensions.WhereEnabled( this._core.AllCorrections ).IndexOf( this._selectedCorrection );
                                  
            Column plsrColumn;
            GetSource( _regressAgainst,  out plsrColumn );

            _lblPlsrSource.Text = plsrColumn.DisplayName;

            double[,] valueMatrix = _transposeToShowPeaks ? new double[source.NumRows, source.NumCols] : new double[source.NumCols, source.NumRows];

            for (int row = 0; row < source.NumRows; row++)
            {
                Vector vector= source.Vectors[row];                                       

                int obsIndex = 0;

                for (int col = 0; col < source.NumCols; col++)
                {
                    if (_transposeToShowPeaks)
                    {
                        valueMatrix[row, col] = source.Values[row][col];
                    }
                    else
                    {
                        valueMatrix[col, row] = source.Values[row][col];
                    }

                    ++obsIndex;
                }
            }

            this._pcaPeaks = source.Rows.Select(z=> z.Peak ).ToArray();
            this._pcaObservations = source.Columns.Select(z=> z.Observation ).ToArray();

            if (_method == EMethod.Plsr)
            {
                plsrResponseMatrix = new double[valueMatrix.GetLength( 0 )];

                IEnumerable rows;

                if (_transposeToShowPeaks)
                {
                    rows = _pcaPeaks;
                }
                else
                {
                    rows = _pcaObservations;
                }

                IEnumerator en= rows.GetEnumerator();
                Factoriser fac = new Factoriser();

                for (int n = 0; n < plsrResponseMatrix.Length; n++)
                {
                    en.MoveNext();
                    object row = plsrColumn.GetRow((IVisualisable) en.Current );
                    plsrResponseMatrix[n] = fac.Factor( row );
                }
            }

            try
            {
                switch (_method)
                {
                    case EMethod.Pca:
                        FrmWait.Show( this, "PCA", "Updating scores", () =>
                            Arr.Instance.Pca( valueMatrix, out this._scores, out this._loadings ) );
                        break;

                    case EMethod.Plsr:
                        FrmWait.Show( this, "PLSR", "Updating scores", () =>
                            Arr.Instance.Plsr( valueMatrix, plsrResponseMatrix, out this._scores, out this._loadings ) );
                        break;

                    default:
                        throw new SwitchException( _method );
                }
                
                this._errorMessage = null;
            }
            catch (Exception ex)
            {
                this._errorMessage = ex.Message;
                this._scores = null;
                this._loadings = null;
            }

            UpdatePlot();
        }

        private void UpdatePlot()
        {
            if (_scores == null)
            {
                _lblSelection.Text = this._errorMessage;
                this._chart.Visible = false;
                return;
            }

            this._chart.Visible = true;
            _lblSelection.Text = "";

            MCharting.Plot plot = new MCharting.Plot();

            plot.Title = $"{_lblMethod.Text} of {_core.FileNames.Title}";
            plot.SubTitle = $"Source: {_lblPcaSource.Text}, View: {_lblPlotView.Text}, Legend: {_lblLegend.Text}, Corrections: {_lblCorrections.Text}, Aspect: {_lblAspect.Text}, Observations: {_lblObs.Text}, Peaks: {_lblPeaks.Text}";

            switch (_method)
            {
                case EMethod.Pca:
                    plot.XLabel = $"PC{_component + 1}";
                    plot.YLabel = $"PC{_component + 2}";
                    break;

                case EMethod.Plsr:
                    plot.XLabel = $"Component {_component + 1}";
                    plot.YLabel = $"Component {_component + 2}";
                    plot.SubTitle += ", PLSR Response: " + _lblPlsrSource.Text;
                    break;
            }

            this._chart.Style.Margin = new Padding( 48, 48, 48, 48 );
            plot.Style.GridStyle = new Pen( Color.FromArgb( 224, 224, 224 ) );

            // Get the "rows"
            IEnumerator enSources;
            Column column;
            GetSource( _colourBy, out enSources, out column );

            // Get the "columns"
            double[,] plotPoints;

            if (_plotSource == EPlotSource.Loadings)
            {
                plotPoints = _loadings;
            }
            else
            {
                plotPoints = _scores;
            }

            // Get the component
            if (_component < 0)
            {
                _component = 0;
            }

            if (_component >= plotPoints.GetLength( 1 ))
            {
                _component = plotPoints.GetLength( 1 ) - 1;
            }

            _btnPrevPc.Enabled = _component != 0;
            _btnNextPc.Enabled = _component != plotPoints.GetLength( 1 ) - 1;

            _lblPlotView.Text = _plotSource == EPlotSource.Loadings ? "Loadings" : "Scores";
            _btnScores.Checked = _plotSource == EPlotSource.Scores;
            _btnLoadings.Checked = _plotSource == EPlotSource.Loadings;

            _lblPcNumber.Text = "PC" + (_component + 1).ToString() + " / " + plotPoints.GetLength( 1 );

            _lblLegend.Text = column.DisplayName;

            // Iterate scores
            for (int r = 0; r < plotPoints.GetLength( 0 ); r++)
            {
                enSources.MoveNext();

                MCharting.Series series = GetOrCreateSeriesForValue( plot, column, (IVisualisable)enSources.Current );

                var coord = new MCharting.DataPoint( plotPoints[r, _component], plotPoints[r, _component + 1] );
                coord.Tag = enSources.Current;

                series.Points.Add( coord );
            }

            // Assign colours     
            if (!column.HasColourSupport)
            {
                foreach (var colour in PlotCreator.AutoColour( plot.Series ))
                {
                    colour.Key.Style.DrawPoints = new SolidBrush( colour.Value );
                }
            }

            _chart.Style.Animate = true;
            _chart.Style.SelectionColour = Color.Yellow;

            _chart.SetPlot( plot );
        }

        private void GetSource( SourceSet ss, out Column column )
        {
            switch (WhatPlotting)
            {
                case EPlotting.Peaks:                              
                    column = ss._colourByPeak;
                    break;      

                case EPlotting.Observations:                            
                    column = ss._colourByObervation;
                    break;

                default:
                    throw new SwitchException( WhatPlotting );
            }
        }

        private void GetSource( SourceSet ss, out IEnumerator enSources, out Column column )
        {
            GetSource( ss, out column );

            switch (WhatPlotting)
            {
                case EPlotting.Peaks:
                    enSources = this._pcaPeaks.GetEnumerator();
                    break;            

                case EPlotting.Observations:
                    enSources = this._pcaObservations.GetEnumerator();
                    break;

                default:
                    throw new SwitchException( WhatPlotting );
            }
        }

        private static MCharting.Series GetOrCreateSeriesForValue( MCharting.Plot plot, Column column, IVisualisable vis)
        {
            object value = column.GetRow(vis);
            MCharting.Series series = plot.Series.FirstOrDefault(z => (z.Tag == null && value == null) || (z.Tag != null && z.Tag.Equals(value)));

            if (series == null)
            {
                series = new MCharting.Series();
                series.Name = Column.AsString(value, column.DisplayMode);
                series.Tag = value; 

                if (value is GroupInfoBase)
                {
                    GroupInfoBase group = (GroupInfoBase)value;
                    UiControls.CreateIcon(  series, group );                                                
                }
                else
                {
                    series.Style.DrawPoints = new SolidBrush( column.GetColour( vis ) );
                }

                plot.Series.Add(series);
            }

            return series;
        }

        private void toolStripDropDownButton3_DropDownOpening(object sender, EventArgs e)
        {
            ClearCmsFilter(_mnuObsFilter);
            AddFilters(_mnuObsFilter, _core.AllObsFilters, true);
        }

        private void AddFilters(ToolStripDropDownButton button, IEnumerable source, bool isObservationFilter)
        {
            // Add "no filter"
            Filter @default = isObservationFilter ? (Filter)ObsFilter.Empty : PeakFilter.Empty;
            ToolStripMenuItem tsmi = new ToolStripMenuItem(@default.ToString()) { Tag = @default };
            tsmi.Click += setFilter_Click;
            tsmi.Image = MetaboliteLevels.Properties.Resources.MnuClear;
            tsmi.ImageScaling = ToolStripItemImageScaling.None;
            button.DropDownItems.Add(tsmi);

            // Add session filters
            foreach (var filter in source)
            {
                tsmi = new ToolStripMenuItem(filter.ToString()) { Tag = filter };
                tsmi.Click += setFilter_Click;
                button.DropDownItems.Add(tsmi);
            }

            // Add edit command
            tsmi = new ToolStripMenuItem("Edit " + (isObservationFilter ? "observation" : "peak") + " filters...");
            tsmi.Click += isObservationFilter ? (EventHandler)editObsFilters_Click : editPeakFilters_Click;
            tsmi.Image = MetaboliteLevels.Properties.Resources.MnuEdit;
            tsmi.ImageScaling = ToolStripItemImageScaling.None;
            button.DropDownItems.Add(tsmi);
        }

        private void toolStripDropDownButton1_DropDownOpening(object sender, EventArgs e)
        {
            ClearCmsFilter(_mnuPeakFilter);
            AddFilters(_mnuPeakFilter, _core.AllPeakFilters, false);
        }

        private void editPeakFilters_Click(object sender, EventArgs e)
        {
            Generic.DataSet.ForPeakFilter(this._core).ShowListEditor(this);
        }

        private void editObsFilters_Click(object sender, EventArgs e)
        {
            Generic.DataSet.ForObsFilter(this._core).ShowListEditor(this);
        }

        void setFilter_Click(object sender, EventArgs e)
        {
            object tag = (((ToolStripMenuItem)sender).Tag);

            if (tag is ObsFilter)
            {
                _obsFilter = (ObsFilter)tag;
            }
            else
            {
                _peakFilter = (PeakFilter)tag;
            }

            UpdateScores();
        }

        private void ClearCmsFilter(ToolStripDropDownButton btn)
        {
            ArrayList toDispose = new ArrayList(btn.DropDownItems);

            foreach (ToolStripItem x in toDispose)
            {
                x.Dispose();
            }

            _cmsFilter.Items.Clear();
        }

        private void _chart_SelectionChanged(object sender, EventArgs e)
        {
            var sel = _chart.SelectedItem;

            if (sel.SelectedSeries == null || sel.DataPoint == null)
            {
                _lblSelection.Text = "No selection";
                _lblOutlier.Text = "(no selection)";
                _btnMarkAsOutlier.Enabled = false;
                _btnNavigate.Enabled = false;
            }
            else
            {
                _lblSelection.Text = sel.SelectedSeries.Name.ToString() + ": " + sel.DataPoint.Tag + " = (" + sel.X + ", " + sel.Y + ")";
                _lblOutlier.Text = sel.SelectedSeries.Name.ToString() + ": " + sel.DataPoint.Tag;
                _btnMarkAsOutlier.Enabled = true;
                _btnNavigate.Enabled = true;
            }
        }

        private void _btnNextPc_Click(object sender, EventArgs e)
        {
            _component++;
            UpdatePlot();
        }

        private void _btnPrevPc_Click(object sender, EventArgs e)
        {
            _component--;
            UpdatePlot();
        }

        private void transposeToShowObservationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _transposeToShowPeaks = false;
            UpdateScores();
        }

        private void transposeToShowPeaksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _transposeToShowPeaks = true;
            UpdateScores();
        } 

        private enum EPlotting
        {
            Peaks,
            Observations,
        }

        private enum EPlotSource
        {
            Scores,
            Loadings,
        }

        private EPlotting WhatPlotting
        {
            get
            {
                if (_transposeToShowPeaks)
                {
                    return (_plotSource == EPlotSource.Scores) ? EPlotting.Peaks : EPlotting.Observations;
                }
                else
                {
                    return (_plotSource == EPlotSource.Loadings) ? EPlotting.Peaks : EPlotting.Observations;
                }
            }
        }

        private void _btnScores_Click(object sender, EventArgs e)
        {
            _plotSource = EPlotSource.Scores;
            UpdatePlot();
        }

        private void _btnLoadings_Click(object sender, EventArgs e)
        {
            _plotSource = EPlotSource.Loadings;
            UpdatePlot();
        }

        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fileName = UiControls.BrowseForFile(this, null, UiControls.EFileExtension.PngOrEmf, FileDialogMode.SaveAs, UiControls.EInitialFolder.SavedImages);

            if (fileName != null)
            {
                try
                {
                    _chart.DrawToBitmap().Save(fileName);
                }
                catch (Exception ex)
                {
                    FrmMsgBox.ShowError(this, ex);
                }
            }
        }

        private void _btnMarkAsOutlier_Click(object sender, EventArgs e)
        {
            object item = _chart.SelectedItem.DataPoint.Tag;
            ObservationInfo observationInfo = item as ObservationInfo;
            Peak peak = item as Peak;

            bool safeToReplace = false;

            if (peak != null)
            {
                List<PeakFilter.Condition> filters = new List<PeakFilter.Condition>(_peakFilter.Conditions.Cast<PeakFilter.Condition>());
                bool needsNewFilter = true;

                foreach (PeakFilter.Condition filter in filters)
                {
                    PeakFilter.ConditionPeak klalkq = filter as PeakFilter.ConditionPeak;

                    if (klalkq != null
                        && klalkq.CombiningOperator == Filter.ELogicOperator.And
                        && klalkq.Negate == false
                        && klalkq.PeaksOp == Filter.EElementOperator.IsNot)
                    {
                        needsNewFilter = false;

                        List<Peak> strong;

                        if (!klalkq.Peaks.TryGetStrong(out strong))
                        {
                            FrmMsgBox.ShowError(this, "Couldn't modify current filter because it contains expired data.");
                            return;
                        }

                        filters.Remove(klalkq);
                        filters.Add(new PeakFilter.ConditionPeak(Filter.ELogicOperator.And, false, strong.ConcatSingle(peak), Filter.EElementOperator.IsNot));
                        safeToReplace = filters.Count == 1;
                        break;
                    }
                }

                if (needsNewFilter)
                {
                    filters.Add(new PeakFilter.ConditionPeak(Filter.ELogicOperator.And, false, new[] { peak }, Filter.EElementOperator.IsNot));
                }

                PeakFilter newFilter = new PeakFilter(null, null, filters);

                if (safeToReplace)
                {
                    _core.SetPeakFilters(_core.AllPeakFilters.ReplaceSingle(_peakFilter, newFilter).ToArray());
                }
                else
                {
                    _core.SetPeakFilters(_core.AllPeakFilters.ConcatSingle(newFilter).ToArray());
                }

                _peakFilter = newFilter;

                UpdateScores();
                return;
            }

            if (observationInfo == null)
            {
                bool needsNewFilter2 = true;

                List<ObsFilter.Condition> filters2 = new List<ObsFilter.Condition>( _obsFilter.Conditions.Cast<ObsFilter.Condition>() );

                foreach (ObsFilter.ConditionObservation fikaklq in filters2)
                {
                    ObsFilter.ConditionObservation qkklqq = fikaklq as ObsFilter.ConditionObservation;

                    if (qkklqq != null
                        && qkklqq.CombiningOperator == Filter.ELogicOperator.And
                        && qkklqq.Negate == false
                        && qkklqq.Operator == Filter.EElementOperator.IsNot)
                    {
                        needsNewFilter2 = false;

                        filters2.Remove( qkklqq );
                        filters2.Add( new ObsFilter.ConditionObservation( Filter.ELogicOperator.And, false, Filter.EElementOperator.IsNot, qkklqq.Possibilities.ConcatSingle( observationInfo ) ) );
                        safeToReplace = filters2.Count == 1;
                        break;
                    }
                }

                if (needsNewFilter2)
                {
                    filters2.Add( new ObsFilter.ConditionObservation( Filter.ELogicOperator.And, false, Filter.EElementOperator.IsNot, new[] { observationInfo } ) );
                }

                ObsFilter obsFilter = new ObsFilter( null, null, filters2 );

                if (safeToReplace)
                {
                    _core.SetObsFilters( _core.AllObsFilters.ReplaceSingle( _obsFilter, obsFilter ).ToArray() );
                }
                else
                {
                    _core.SetObsFilters( _core.AllObsFilters.ConcatSingle( obsFilter ).ToArray() );
                }

                _obsFilter = obsFilter;

                UpdateScores();
            }
        }

        private void _mnuTrend_DropDownOpening(object sender, EventArgs e)
        {

        }

        private void _mnuCorrections_correction_Click(object sender, EventArgs e)
        {
            _selectedCorrection = (IProvider<IntensityMatrix>)((ToolStripMenuItem)sender).Tag;

            UpdateScores();
        }

        private void _mnuCorrections_DropDownOpening(object sender, EventArgs e)
        {
            _mnuCorrections.DropDownItems.Clear();

            foreach (IProvider<IntensityMatrix> provider in _core.Matrices)
            {
                ToolStripMenuItem tsmi = new ToolStripMenuItem( provider.ToString() ) { Tag = provider };
                tsmi.Click += this._mnuCorrections_correction_Click;

                this._mnuCorrections.DropDownItems.Add( tsmi );
            }
        }

        private void _btnColour_DropDownOpening(object sender, EventArgs e)
        {
            ToolStripDropDownButton tsddb = (ToolStripDropDownButton)sender;

            tsddb.DropDownItems.Clear();

            IEnumerable<Column> columns;
            Column selected;

            bool isColour = tsddb == _btnColour;

            SourceSet ss = isColour ? _colourBy : _regressAgainst;

            switch (WhatPlotting)
            {
                case EPlotting.Peaks:
                    columns = ColumnManager.GetColumns<Peak>(_core);
                    selected = ss._colourByPeak;
                    break;

                case EPlotting.Observations:
                    columns = ColumnManager.GetColumns<ObservationInfo>(_core);
                    selected = ss._colourByObervation;
                    break;       

                default:
                    throw new SwitchException(WhatPlotting);
            }   

            foreach (Column column in columns)
            {
                ToolStripMenuItem tsmi = new ToolStripMenuItem(column.Id) { Tag = column };
                if (isColour)
                {
                    tsmi.Click += _btnColour_column_Click;
                }
                else
                {
                    tsmi.Click += Tsmi_Click;
                }

                tsmi.Checked = column == selected;

                tsddb.DropDownItems.Add(tsmi);
            }
        }

        private void _btnColour_column_Click(object sender, EventArgs e)
        {
            Column selected = (Column)((ToolStripMenuItem)sender).Tag;
            ColourBy( _colourBy, selected );
            UpdatePlot();
        }

        private void ColourBy( SourceSet ss, Column selected )
        {
            switch (WhatPlotting)
            {
                case EPlotting.Peaks:
                    ss._colourByPeak = selected;
                    break;

                case EPlotting.Observations:
                    ss._colourByObervation = selected;
                    break;   

                default:
                    throw new SwitchException( WhatPlotting );
            }  
        }

        enum EMethod
        {
            [Name("Principal components analysis")]
            Pca,

            [Name( "Partial least squares regression" )]
            Plsr,
        }

        private void toolStripMenuItem1_Click( object sender, EventArgs e )
        {
            _method = EMethod.Pca;
            UpdateScores();
        }

        private void toolStripMenuItem2_Click( object sender, EventArgs e )
        {
            FrmMsgBox.ShowOkCancel( this, Text, "Note: PLSR requires the R library \"pls\" to function.", FrmMsgBox.EDontShowAgainId.PlsrMode, DialogResult.OK );

            _method = EMethod.Plsr;
            UpdateScores();
        }

        private void _mnuPlsrSource_DropDownOpening( object sender, EventArgs e )
        {

        }

        private void Tsmi_Click( object sender, EventArgs e )
        {
            Column selected = (Column)((ToolStripMenuItem)sender).Tag;

            ColourBy( _regressAgainst, selected );
            ColourBy( _colourBy, selected );
            UpdateScores();
        }

        private void _mnuPlsrSource_Click( object sender, EventArgs e )
        {

        }

        private void _btnNavigate_Click( object sender, EventArgs e )
        {
            _frmMain.Selection = new VisualisableSelection( _chart.SelectedItem.DataPoint.Tag as IVisualisable );
        }
    }
}
