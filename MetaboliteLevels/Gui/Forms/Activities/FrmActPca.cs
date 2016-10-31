using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MCharting;
using MetaboliteLevels.Data.Algorithms.Definitions.Base.Misc;
using MetaboliteLevels.Data.Algorithms.General;
using MetaboliteLevels.Data.Database;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Session.Main;
using MetaboliteLevels.Gui.Controls.Lists;
using MetaboliteLevels.Gui.Forms.Selection;
using MetaboliteLevels.Utilities;
using MGui.Datatypes;
using MGui.Helpers;

namespace MetaboliteLevels.Gui.Forms.Activities
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
        private IMatrixProvider _selectedCorrection;


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
            this.InitializeComponent();
            UiControls.SetIcon( this );
            UiControls.ColourMenuButtons( this.toolStrip1 );

            this._frmMain = frmMain;
            this._core = core;

            this._peakFilter = PeakFilter.Empty;
            this._obsFilter = ObsFilter.Empty;

            this._selectedCorrection = core.Options.SelectedMatrixProvider;

            this._colourBy._colourByPeak = ColumnManager.GetColumns<Peak>(this._core).First(z => z.Id == Peak.ID_COLUMN_CLUSTERCOMBINATION);
            this._colourBy._colourByObervation = ColumnManager.GetColumns<ObservationInfo>(this._core).First(z => z.Id == nameof(ObservationInfo.Group ));
            this._regressAgainst._colourByPeak = this._colourBy._colourByPeak;
            this._regressAgainst._colourByObervation = this._colourBy._colourByObervation;

            this._chart.AddControls( this.toolStripDropDownButton1);
            this._chart.Style.LegendDisplay = ELegendDisplay.Visible;

            this.UpdateScores();
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

                if (this._factors.TryGetValue( row, out f ))
                {
                    return f;
                }

                f = this._factors.Count;
                this._factors.Add( row, f );
                return f;
            }
        }

        private void UpdateScores()
        {
            StringBuilder title = new StringBuilder();

            IntensityMatrix sourceMatrix = this._selectedCorrection.Provide;
            IntensityMatrix source = sourceMatrix.Subset( this._peakFilter, this._obsFilter, ESubsetFlags.None );
            
            double[] plsrResponseMatrix = null;             

            this._lblPcaSource.Text = this._transposeToShowPeaks ? "Peaks" : "Observations";
            this.transposeToShowObservationsToolStripMenuItem.Checked = !this._transposeToShowPeaks;
            this.transposeToShowPeaksToolStripMenuItem.Checked = this._transposeToShowPeaks;           

            this._lblObs.Text = this._obsFilter.ToString();
            this._lblPeaks.Text = this._peakFilter.ToString();
            this._lblCorrections.Text = this._selectedCorrection.ToString();

            this._lblMethod.Text = this._method.ToString().ToUpper();
            this.ctlTitleBar1.Text = this._method.ToUiString();

            this._lblPlsrSource.Visible = this._mnuPlsrSource.Visible = this._method == EMethod.Plsr;

            int corIndex = IVisualisableExtensions.WhereEnabled( this._core.Corrections ).IndexOf( this._selectedCorrection );
                                  
            Column plsrColumn;
            this.GetSource( this._regressAgainst,  out plsrColumn );

            this._lblPlsrSource.Text = plsrColumn.DisplayName;

            double[,] valueMatrix = this._transposeToShowPeaks ? new double[source.NumRows, source.NumCols] : new double[source.NumCols, source.NumRows];

            for (int row = 0; row < source.NumRows; row++)
            {
                Vector vector= source.Vectors[row];                                       

                int obsIndex = 0;

                for (int col = 0; col < source.NumCols; col++)
                {
                    if (this._transposeToShowPeaks)
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

            if (this._method == EMethod.Plsr)
            {
                plsrResponseMatrix = new double[valueMatrix.GetLength( 0 )];

                IEnumerable rows;

                if (this._transposeToShowPeaks)
                {
                    rows = this._pcaPeaks;
                }
                else
                {
                    rows = this._pcaObservations;
                }

                IEnumerator en= rows.GetEnumerator();
                Factoriser fac = new Factoriser();

                for (int n = 0; n < plsrResponseMatrix.Length; n++)
                {
                    en.MoveNext();
                    object row = plsrColumn.GetRow((Visualisable) en.Current );
                    plsrResponseMatrix[n] = fac.Factor( row );
                }
            }

            try
            {
                switch (this._method)
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
                        throw new SwitchException( this._method );
                }
                
                this._errorMessage = null;
            }
            catch (Exception ex)
            {
                this._errorMessage = ex.Message;
                this._scores = null;
                this._loadings = null;
            }

            this.UpdatePlot();
        }

        private void UpdatePlot()
        {
            if (this._scores == null)
            {
                this._lblSelection.Text = this._errorMessage;
                this._chart.Visible = false;
                return;
            }

            this._chart.Visible = true;
            this._lblSelection.Text = "";

            MCharting.Plot plot = new MCharting.Plot();

            plot.Title = $"{this._lblMethod.Text} of {this._core.FileNames.Title}";
            plot.SubTitle = $"Source: {this._lblPcaSource.Text}, View: {this._lblPlotView.Text}, Legend: {this._lblLegend.Text}, Corrections: {this._lblCorrections.Text}, Aspect: {this._lblAspect.Text}, Observations: {this._lblObs.Text}, Peaks: {this._lblPeaks.Text}";

            switch (this._method)
            {
                case EMethod.Pca:
                    plot.XLabel = $"PC{this._component + 1}";
                    plot.YLabel = $"PC{this._component + 2}";
                    break;

                case EMethod.Plsr:
                    plot.XLabel = $"Component {this._component + 1}";
                    plot.YLabel = $"Component {this._component + 2}";
                    plot.SubTitle += ", PLSR Response: " + this._lblPlsrSource.Text;
                    break;
            }

            this._chart.Style.Margin = new Padding( 48, 48, 48, 48 );
            plot.Style.GridStyle = new Pen( Color.FromArgb( 224, 224, 224 ) );

            // Get the "rows"
            IEnumerator enSources;
            Column column;
            this.GetSource( this._colourBy, out enSources, out column );

            // Get the "columns"
            double[,] plotPoints;

            if (this._plotSource == EPlotSource.Loadings)
            {
                plotPoints = this._loadings;
            }
            else
            {
                plotPoints = this._scores;
            }

            // Get the component
            if (this._component < 0)
            {
                this._component = 0;
            }

            if (this._component >= plotPoints.GetLength( 1 ))
            {
                this._component = plotPoints.GetLength( 1 ) - 1;
            }

            this._btnPrevPc.Enabled = this._component != 0;
            this._btnNextPc.Enabled = this._component != plotPoints.GetLength( 1 ) - 1;

            this._lblPlotView.Text = this._plotSource == EPlotSource.Loadings ? "Loadings" : "Scores";
            this._btnScores.Checked = this._plotSource == EPlotSource.Scores;
            this._btnLoadings.Checked = this._plotSource == EPlotSource.Loadings;

            this._lblPcNumber.Text = "PC" + (this._component + 1).ToString() + " / " + plotPoints.GetLength( 1 );

            this._lblLegend.Text = column.DisplayName;

            bool isGroup = false;

            // Iterate scores
            for (int r = 0; r < plotPoints.GetLength( 0 ); r++)
            {
                enSources.MoveNext();

                MCharting.Series series = GetOrCreateSeriesForValue( plot, column, (Visualisable)enSources.Current, ref isGroup );

                var coord = new MCharting.DataPoint( plotPoints[r, this._component], plotPoints[r, this._component + 1] );
                coord.Tag = enSources.Current;

                series.Points.Add( coord );
            }

            // Assign colours     
            if (!column.HasColourSupport && !isGroup)
            {
                foreach (var colour in PlotCreator.AutoColour( plot.Series ))
                {
                    colour.Key.Style.DrawPoints = new SolidBrush( colour.Value );
                }
            }

            this._chart.Style.Animate = true;
            this._chart.Style.SelectionColour = Color.Yellow;

            this._chart.SetPlot( plot );
        }

        private void GetSource( SourceSet ss, out Column column )
        {
            switch (this.WhatPlotting)
            {
                case EPlotting.Peaks:                              
                    column = ss._colourByPeak;
                    break;      

                case EPlotting.Observations:                            
                    column = ss._colourByObervation;
                    break;

                default:
                    throw new SwitchException( this.WhatPlotting );
            }
        }

        private void GetSource( SourceSet ss, out IEnumerator enSources, out Column column )
        {
            this.GetSource( ss, out column );

            switch (this.WhatPlotting)
            {
                case EPlotting.Peaks:
                    enSources = this._pcaPeaks.GetEnumerator();
                    break;            

                case EPlotting.Observations:
                    enSources = this._pcaObservations.GetEnumerator();
                    break;

                default:
                    throw new SwitchException( this.WhatPlotting );
            }
        }

        private static MCharting.Series GetOrCreateSeriesForValue( MCharting.Plot plot, Column column, Visualisable vis, ref bool isGroup)
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
                    isGroup = true;
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
            this.ClearCmsFilter(this._mnuObsFilter);
            this.AddFilters(this._mnuObsFilter, this._core.ObsFilters, true);
        }

        private void AddFilters(ToolStripDropDownButton button, IEnumerable source, bool isObservationFilter)
        {
            // Add "no filter"
            Filter @default = isObservationFilter ? (Filter)ObsFilter.Empty : PeakFilter.Empty;
            ToolStripMenuItem tsmi = new ToolStripMenuItem(@default.ToString()) { Tag = @default };
            tsmi.Click += this.setFilter_Click;
            tsmi.Image = MetaboliteLevels.Properties.Resources.MnuClear;
            tsmi.ImageScaling = ToolStripItemImageScaling.None;
            button.DropDownItems.Add(tsmi);

            // Add session filters
            foreach (var filter in source)
            {
                tsmi = new ToolStripMenuItem(filter.ToString()) { Tag = filter };
                tsmi.Click += this.setFilter_Click;
                button.DropDownItems.Add(tsmi);
            }

            // Add edit command
            tsmi = new ToolStripMenuItem("Edit " + (isObservationFilter ? "observation" : "peak") + " filters...");
            tsmi.Click += isObservationFilter ? (EventHandler)this.editObsFilters_Click : this.editPeakFilters_Click;
            tsmi.Image = MetaboliteLevels.Properties.Resources.MnuEdit;
            tsmi.ImageScaling = ToolStripItemImageScaling.None;
            button.DropDownItems.Add(tsmi);
        }

        private void toolStripDropDownButton1_DropDownOpening(object sender, EventArgs e)
        {
            this.ClearCmsFilter(this._mnuPeakFilter);
            this.AddFilters(this._mnuPeakFilter, this._core.PeakFilters, false);
        }

        private void editPeakFilters_Click(object sender, EventArgs e)
        {
            DataSet.ForPeakFilter(this._core).ShowListEditor(this);
        }

        private void editObsFilters_Click(object sender, EventArgs e)
        {
            DataSet.ForObsFilter(this._core).ShowListEditor(this);
        }

        void setFilter_Click(object sender, EventArgs e)
        {
            object tag = (((ToolStripMenuItem)sender).Tag);

            if (tag is ObsFilter)
            {
                this._obsFilter = (ObsFilter)tag;
            }
            else
            {
                this._peakFilter = (PeakFilter)tag;
            }

            this.UpdateScores();
        }

        private void ClearCmsFilter(ToolStripDropDownButton btn)
        {
            ArrayList toDispose = new ArrayList(btn.DropDownItems);

            foreach (ToolStripItem x in toDispose)
            {
                x.Dispose();
            }

            this._cmsFilter.Items.Clear();
        }

        private void _chart_SelectionChanged(object sender, EventArgs e)
        {
            var sel = this._chart.SelectedItem;

            if (sel.SelectedSeries == null || sel.DataPoint == null)
            {
                this._lblSelection.Text = "No selection";
                this._lblOutlier.Text = "(no selection)";
                this._btnMarkAsOutlier.Enabled = false;
                this._btnNavigate.Enabled = false;
            }
            else
            {
                this._lblSelection.Text = sel.SelectedSeries.Name.ToString() + ": " + sel.DataPoint.Tag + " = (" + sel.X + ", " + sel.Y + ")";
                this._lblOutlier.Text = sel.SelectedSeries.Name.ToString() + ": " + sel.DataPoint.Tag;
                this._btnMarkAsOutlier.Enabled = true;
                this._btnNavigate.Enabled = true;
            }
        }

        private void _btnNextPc_Click(object sender, EventArgs e)
        {
            this._component++;
            this.UpdatePlot();
        }

        private void _btnPrevPc_Click(object sender, EventArgs e)
        {
            this._component--;
            this.UpdatePlot();
        }

        private void transposeToShowObservationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._transposeToShowPeaks = false;
            this.UpdateScores();
        }

        private void transposeToShowPeaksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._transposeToShowPeaks = true;
            this.UpdateScores();
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
                if (this._transposeToShowPeaks)
                {
                    return (this._plotSource == EPlotSource.Scores) ? EPlotting.Peaks : EPlotting.Observations;
                }
                else
                {
                    return (this._plotSource == EPlotSource.Loadings) ? EPlotting.Peaks : EPlotting.Observations;
                }
            }
        }

        private void _btnScores_Click(object sender, EventArgs e)
        {
            this._plotSource = EPlotSource.Scores;
            this.UpdatePlot();
        }

        private void _btnLoadings_Click(object sender, EventArgs e)
        {
            this._plotSource = EPlotSource.Loadings;
            this.UpdatePlot();
        }

        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fileName = UiControls.BrowseForFile(this, null, UiControls.EFileExtension.PngOrEmf, FileDialogMode.SaveAs, UiControls.EInitialFolder.SavedImages);

            if (fileName != null)
            {
                try
                {
                    this._chart.DrawToBitmap().Save(fileName);
                }
                catch (Exception ex)
                {
                    FrmMsgBox.ShowError(this, ex);
                }
            }
        }

        private void _btnMarkAsOutlier_Click(object sender, EventArgs e)
        {
            object item = this._chart.SelectedItem.DataPoint.Tag;
            ObservationInfo observationInfo = item as ObservationInfo;
            Peak peak = item as Peak;

            bool safeToReplace = false;

            if (peak != null)
            {
                List<PeakFilter.Condition> filters = new List<PeakFilter.Condition>(this._peakFilter.Conditions.Cast<PeakFilter.Condition>());
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
                    this._core.SetPeakFilters(this._core.PeakFilters.ReplaceSingle(this._peakFilter, newFilter).ToArray());
                }
                else
                {
                    this._core.SetPeakFilters(this._core.PeakFilters.ConcatSingle(newFilter).ToArray());
                }

                this._peakFilter = newFilter;

                this.UpdateScores();
                return;
            }

            if (observationInfo == null)
            {
                bool needsNewFilter2 = true;

                List<ObsFilter.Condition> filters2 = new List<ObsFilter.Condition>( this._obsFilter.Conditions.Cast<ObsFilter.Condition>() );

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
                    this._core.SetObsFilters( this._core.ObsFilters.ReplaceSingle( this._obsFilter, obsFilter ).ToArray() );
                }
                else
                {
                    this._core.SetObsFilters( this._core.ObsFilters.ConcatSingle( obsFilter ).ToArray() );
                }

                this._obsFilter = obsFilter;

                this.UpdateScores();
            }
        }

        private void _mnuTrend_DropDownOpening(object sender, EventArgs e)
        {

        }

        private void _mnuCorrections_correction_Click(object sender, EventArgs e)
        {
            this._selectedCorrection = (IMatrixProvider)((ToolStripMenuItem)sender).Tag;

            this.UpdateScores();
        }

        private void _mnuCorrections_DropDownOpening(object sender, EventArgs e)
        {
            this._mnuCorrections.DropDownItems.Clear();

            foreach (IMatrixProvider provider in this._core.Matrices)
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

            bool isColour = tsddb == this._btnColour;

            SourceSet ss = isColour ? this._colourBy : this._regressAgainst;

            switch (this.WhatPlotting)
            {
                case EPlotting.Peaks:
                    columns = ColumnManager.GetColumns<Peak>(this._core);
                    selected = ss._colourByPeak;
                    break;

                case EPlotting.Observations:
                    columns = ColumnManager.GetColumns<ObservationInfo>(this._core);
                    selected = ss._colourByObervation;
                    break;       

                default:
                    throw new SwitchException(this.WhatPlotting);
            }   

            foreach (Column column in columns)
            {
                ToolStripMenuItem tsmi = new ToolStripMenuItem(column.Id) { Tag = column };
                if (isColour)
                {
                    tsmi.Click += this._btnColour_column_Click;
                }
                else
                {
                    tsmi.Click += this.Tsmi_Click;
                }

                tsmi.Checked = column == selected;

                tsddb.DropDownItems.Add(tsmi);
            }
        }

        private void _btnColour_column_Click(object sender, EventArgs e)
        {
            Column selected = (Column)((ToolStripMenuItem)sender).Tag;
            this.ColourBy( this._colourBy, selected );
            this.UpdatePlot();
        }

        private void ColourBy( SourceSet ss, Column selected )
        {
            switch (this.WhatPlotting)
            {
                case EPlotting.Peaks:
                    ss._colourByPeak = selected;
                    break;

                case EPlotting.Observations:
                    ss._colourByObervation = selected;
                    break;   

                default:
                    throw new SwitchException( this.WhatPlotting );
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
            this._method = EMethod.Pca;
            this.UpdateScores();
        }

        private void toolStripMenuItem2_Click( object sender, EventArgs e )
        {
            FrmMsgBox.ShowOkCancel( this, this.Text, "Note: PLSR requires the R library \"pls\" to function.", FrmMsgBox.EDontShowAgainId.PlsrMode, DialogResult.OK );

            this._method = EMethod.Plsr;
            this.UpdateScores();
        }

        private void _mnuPlsrSource_DropDownOpening( object sender, EventArgs e )
        {

        }

        private void Tsmi_Click( object sender, EventArgs e )
        {
            Column selected = (Column)((ToolStripMenuItem)sender).Tag;

            this.ColourBy( this._regressAgainst, selected );
            this.ColourBy( this._colourBy, selected );
            this.UpdateScores();
        }

        private void _mnuPlsrSource_Click( object sender, EventArgs e )
        {

        }

        private void _btnNavigate_Click( object sender, EventArgs e )
        {
            this._frmMain.Selection = new VisualisableSelection( this._chart.SelectedItem.DataPoint.Tag as Visualisable );
        }
    }
}
