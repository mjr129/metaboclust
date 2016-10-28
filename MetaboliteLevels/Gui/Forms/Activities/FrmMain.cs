using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Algorithms.Definitions.Base.Misc;
using MetaboliteLevels.Data.Algorithms.Definitions.Clusterers;
using MetaboliteLevels.Data.Algorithms.Definitions.Metrics;
using MetaboliteLevels.Data.Algorithms.Definitions.Statistics;
using MetaboliteLevels.Data.Algorithms.Definitions.Trends;
using MetaboliteLevels.Data.Algorithms.General;
using MetaboliteLevels.Data.Database;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Session.Main;
using MetaboliteLevels.Gui.Controls;
using MetaboliteLevels.Gui.Controls.Charts;
using MetaboliteLevels.Gui.Controls.Lists;
using MetaboliteLevels.Gui.Datatypes;
using MetaboliteLevels.Gui.Forms.Editing;
using MetaboliteLevels.Gui.Forms.Selection;
using MetaboliteLevels.Gui.Forms.Setup;
using MetaboliteLevels.Gui.Forms.Text;
using MetaboliteLevels.Gui.Forms.Wizards;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Utilities;
using MGui.Datatypes;
using MGui.Helpers;

namespace MetaboliteLevels.Gui.Forms.Activities
{
    // This is a comment, please remove it

    /// <summary>
    /// Main form.
    /// </summary>
    internal partial class FrmMain : Form, IPreviewProvider, ISelectionHolder
    {
        // Core - this holds all the data
        //private readonly CoreLink _coreLink = CoreLink.Instance;
        private Core _core;

        // Helpers
        private readonly ChartHelperForPeaks _chartPeak;
        private readonly ChartHelperForClusters _chartCluster;
        private readonly ChartHelperForPeaks _chartPeakForPrinting;
        private readonly ChartHelperForClusters _chartClusterForPrinting;

        // The list views
        private readonly CtlAutoList _primaryList; 
        private readonly CtlAutoList _secondaryList;

        // Whether to force close the form
        private readonly bool _ignoreConfirmClose;

        private readonly Stopwatch _lastProgressTime = Stopwatch.StartNew();
        private readonly List<VisualisableSelection> _viewHistory = new List<VisualisableSelection>();
        private readonly List<ICoreWatcher> _coreWatchers = new List<ICoreWatcher>();
        private bool _autoChangingSelection;
        private string _printTitle;
        private int _waitCounter;

        Either<IDataSet, DataSet.Provider> _primaryListView;
        EVisualClass _secondaryListView;

        // Selection
        VisualisableSelection _selection;
        private object _selectionMenuOpenedFromList;
        private readonly CaptionBar _captSecondary;

        public VisualisableSelection Selection
        {
            get
            {
                return this._selection ?? new VisualisableSelection(null);
            }
            set
            {
                this.CommitSelection(value);
            }
        }


        /// <summary>
        /// Constructor
        /// </summary>
        public FrmMain()
        {
            // Create this form
            this.InitializeComponent();
            UiControls.SetIcon(this);

            this._captSecondary = new CaptionBar( this.panel4, this );

            // Initialise R
            if (!this.InitialiseR())
            {
                this._ignoreConfirmClose = true;
                this.BeginInvoke((MethodInvoker)this.Close);
                return;
            }

            // Load core
            this._core = FrmEditDataFileNames.Show(this);

            if (this._core == null)
            {
                this._ignoreConfirmClose = true;
                this.BeginInvoke((MethodInvoker)this.Close);
                return;
            }                                      

            // Main menu colours
            this._mnuMain.BackColor = UiControls.TitleBackColour;
            this.toolStrip1.BackColor = UiControls.TitleBackColour;

            UiControls.ColourMenuButtons(this._mnuMain);
            UiControls.ColourMenuButtons( this.toolStrip1 );

            // Charts
            this._chartPeak = new ChartHelperForPeaks(this, this._core, this.splitContainer3.Panel1);
            this._chartCluster = new ChartHelperForClusters(this, this._core, this.splitContainer3.Panel2);
            this._chartPeakForPrinting = new ChartHelperForPeaks(null, this._core, null);
            this._chartClusterForPrinting = new ChartHelperForClusters(null, this._core, null);
            this._chartCluster.SelectionChanged += this._chartCluster_SelectionChanged;
            this._coreWatchers.Add(this._chartPeak);
            this._coreWatchers.Add(this._chartCluster);
            this._coreWatchers.Add(this._chartPeakForPrinting);
            this._coreWatchers.Add(this._chartClusterForPrinting);

            // Primary lists
            this._primaryList = new CtlAutoList(this._lstPrimary, this._core, this);
            this._primaryList.Activate += this.primaryList_Activate;
            this._primaryList.ShowContextMenu += this.list_ShowContextMenu;
            this._coreWatchers.Add( this._primaryList );

            // Secondary lists
            this._secondaryList = new CtlAutoList( this._lstSecondary, this._core, this );
            this._secondaryList.Activate += this.secondaryList_Activate;
            this._secondaryList.ShowContextMenu += this.list_ShowContextMenu;
            this._coreWatchers.Add( this._secondaryList );

            // Pagers
            this._btnPrimPeak.Tag = new DataSet.Provider( DataSet.ForPeaks );
            this._btnPrimPath.Tag = new DataSet.Provider( DataSet.ForPathways );
            this._btnPrimComp.Tag = new DataSet.Provider( DataSet.ForCompounds );
            this._btnPrimAssig.Tag = new DataSet.Provider( DataSet.ForAssignments );
            this._btnPrimClust.Tag = new DataSet.Provider( DataSet.ForClusters );
            this._btnPrimAdduct.Tag = new DataSet.Provider( DataSet.ForAdducts );
            this._btnPrimAnnot.Tag = new DataSet.Provider( DataSet.ForAnnotations );

            this._btnSubAdd.Tag = EVisualClass.Adduct;
            this._btnSubAnnot.Tag = EVisualClass.Annotation;
            this._btnSubAss.Tag = EVisualClass.Assignment;
            this._btnSubComp.Tag = EVisualClass.Compound;
            this._btnSubInfo.Tag = EVisualClass.SpecialMeta;
            this._btnSubStat.Tag = EVisualClass.SpecialStatistic;
            this._btnSubPat.Tag = EVisualClass.Cluster;
            this._btnSubPeak.Tag = EVisualClass.Peak;
            this._btnSubPath.Tag = EVisualClass.Pathway;

            // General stuff
            this.HandleCoreChange();

            Dictionary<string, ToolStripMenuItem> dict = new Dictionary<string, ToolStripMenuItem>();

            // Database menu
            foreach (EDataSet dm in Enum.GetValues( typeof( EDataSet ) ))
            {
                string[] text = dm.ToUiString().Split('\\');

                ToolStripMenuItem tsi = dict.GetOrCreate( text[0], z => (ToolStripMenuItem)this.databaseToolStripMenuItem.DropDownItems.Add( z ) );

                ToolStripMenuItem tsmi = new ToolStripMenuItem( text[1], Resources.MnuViewList, this.DataManagerMenuItem );
                tsmi.Tag = dm;

                tsi.DropDownItems.Add( tsmi );
            }
        }

        private void _btnPrimary_Click<T>( object sender, EventArgs e )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Handles menu click.
        /// </summary>aram>
        private void DataManagerMenuItem( object sender, EventArgs e )
        {
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
            EDataSet tag = (EDataSet)tsmi.Tag;
            this.ShowEditor( tag );
        }     

        /// <summary>
        /// Any of the lists showing their context menu
        /// </summary>
        private void list_ShowContextMenu(object sender, ShowContextMenuEventArgs e)
        {
            this._selectionMenuOpenedFromList = e.Selection;
            this._cmsSelectionButton.Show(e.Control, e.X, e.Y);
        }

        /// <summary>
        /// Implements IPreviewProvider
        /// </summary>
        Image IPreviewProvider.ProvidePreview(Size s, object @object)
        {
            IAssociation wrapped = @object as IAssociation;
            Associational primaryTarget;
            Associational secondaryTarget;

            if (wrapped != null)
            {
                secondaryTarget = wrapped.OriginalRequest.Owner as Associational;
                primaryTarget = wrapped.Associated as Associational; // Meta fields provide stuff like strings, let's assume we only draw Associational associations!
            }
            else
            {
                secondaryTarget = null;
                primaryTarget = @object as Associational;
            }                                       

            if (primaryTarget == null)
            {
                return null;
            }

            bool small = true;

            this.BeginWait("Creating preview for " + primaryTarget.DisplayName);
            Bitmap result;

            try
            {
                switch (primaryTarget.AssociationalClass)
                {
                    case EVisualClass.Adduct:
                        {
                            result = null;
                        }
                        break;

                    case EVisualClass.Compound:
                        {
                            StylisedCluster p = ((Compound)primaryTarget).CreateStylisedCluster(this._core, this.SelectedTrend.Results.Matrix,  secondaryTarget);
                            p.IsPreview = small;
                            result = this._chartClusterForPrinting.CreateBitmap(s.Width, s.Height, p);
                        }
                        break;

                    case EVisualClass.Pathway:
                        {
                            StylisedCluster p = ((Pathway)primaryTarget).CreateStylisedCluster(this._core, this.SelectedTrend.Results.Matrix, secondaryTarget );
                            p.IsPreview = small;
                            result = this._chartClusterForPrinting.CreateBitmap(s.Width, s.Height, p);
                        }
                        break;

                    case EVisualClass.Cluster:
                        {
                            StylisedCluster p = ((Cluster)primaryTarget).CreateStylisedCluster(this._core, secondaryTarget );
                            p.IsPreview = small;
                            result = this._chartClusterForPrinting.CreateBitmap(s.Width, s.Height, p);
                        }
                        break;

                    case EVisualClass.Peak:
                        {
                            StylisedPeak p = new StylisedPeak((Peak)primaryTarget);
                            p.IsPreview = small;
                            result = this._chartPeakForPrinting.CreateBitmap(s.Width, s.Height, p);
                        }
                        break;

                    case EVisualClass.Assignment:
                        {
                            StylisedCluster p = ((Assignment)primaryTarget).CreateStylisedCluster(this._core, secondaryTarget);
                            p.IsPreview = small;
                            result = this._chartClusterForPrinting.CreateBitmap(s.Width, s.Height, p);
                        }
                        break;

                    default:
                        {
                            throw new InvalidOperationException("Invalid switch: " + primaryTarget.AssociationalClass);
                        }
                }
            }
            finally
            {
                this.EndWait();
            }

            return result;
        }

        /// <summary>
        /// Item selected from primary list.
        /// </summary>
        private void primaryList_Activate(object sender, ListViewItemEventArgs e)
        {
            this.CommitSelection(new VisualisableSelection(e.SelectedItem));
        }


        /// <summary>
        /// Item selected from secondary list.
        /// </summary>
        private void secondaryList_Activate(object sender, ListViewItemEventArgs e)
        {
            this.CommitSelection(new VisualisableSelection(this.Selection.Primary, e.SelectedItem));
        }

        /// <summary>
        /// Handle changing of "_core".
        /// </summary>
        private void HandleCoreChange()
        {   
            // Update stuff
            this._coreWatchers.ForEach(z => z.ChangeCore(this._core));
            this._primaryListView = new DataSet.Provider( DataSet.ForPeaks );
            this._secondaryListView = EVisualClass.SpecialMeta;

            // Clear selection
            this.CommitSelection(new VisualisableSelection(null));

            // Set new options
            this._txtGuid.Text = this._core.CoreGuid.ToString();
            this.UpdateVisualOptions();

            // Refresh lists
            this.UpdateAll("Data loaded");
        }

        /// <summary>
        /// Initialises R
        /// </summary>
        private bool InitialiseR()
        {
            bool forceShow = false;

            __RETRY__:

            if (!FrmInitialSetup.Show(this, forceShow))
            {
                return false;
            }

            try
            {
                Arr.Initialize(MainSettings.Instance.General.RBinPath);
            }
            catch (Exception ex)
            {
                if (Debugger.IsAttached)
                {
                    if (FrmMsgBox.Show(this, "Error", null, "There was a problem initialising R (you have a debugger attached so you can ignore this - not recommended).\r\n\r\n" + ex.Message, Resources.MsgError, new[] { new MsgBoxButton("Configure", Resources.MnuEdit, DialogResult.Cancel), new MsgBoxButton("Ignore", Resources.MnuWarning, DialogResult.Ignore) }) == DialogResult.Ignore)
                    {
                        Algo.Initialise();
                        return true;
                    }
                }

                FrmMsgBox.ShowError(this, "There was a problem initialising R.\r\n\r\n" + ex.Message);
                forceShow = true;
                goto __RETRY__;
            }

            Algo.Initialise();

            return true;
        }

        /// <summary>
        /// Handles: patChart.SelectionChanged
        /// </summary>
        private void _chartCluster_SelectionChanged(object sender, ChartSelectionEventArgs e)
        {
            if (!this._autoChangingSelection)
            {
                this.CommitSelection(new VisualisableSelection(this._chartCluster.SelectedCluster?.ActualElement, e._peak));
            }
        }


        /// <summary>
        /// Selects something.
        /// </summary>
        internal void CommitSelection(VisualisableSelection selection)
        {
            this.BeginWait( "Updating displays" );

            try
            {
                // Add to history
                this.AddToHistory( selection );

                // Set the selected object
                this._selection = selection;

                // Update the lists
                this.UpdateSecondaryList();

                // Icons
                if (selection?.Primary != null)
                {
                    this._btnPrimarySelection.Text = this.LimitLength( selection.Primary );
                    this._btnPrimarySelection.Image = UiControls.GetImage( selection.Primary, true );
                    this._btnPrimarySelection.Visible = true;
                }
                else
                {
                    this._btnPrimarySelection.Visible = false;
                }

                if (selection?.Secondary != null)
                {
                    this._btnSecondarySelection.Text = this.LimitLength( selection.Secondary );
                    this._btnSecondarySelection.Image = UiControls.GetImage( selection.Secondary, true );
                    this._btnSecondarySelection.Visible = true;
                    this._btnSwapSelections.Visible = true;
                }
                else
                {
                    this._btnSecondarySelection.Visible = false;
                    this._btnSwapSelections.Visible = false;
                }

                // Null selection?
                if (selection?.Primary == null)
                {
                    this.PlotPeak( null );
                    this.PlotCluster( null );
                    return;
                }

                // For assignments plot the peak (and implicitly the cluster)
                //if (selection.Primary.VisualClass == VisualClass.Assignment)
                //{
                //    selection = new VisualisableSelection(((Assignment)selection.Primary).Peak, selection.Secondary);
                //}

                // Get the selection
                Peak peak = selection.Primary as Peak ?? selection.Secondary as Peak;
                Associational cluster = (((!(selection.Primary is Peak)) ? selection.Primary : selection.Secondary) ?? peak.FindAssignments( this._core ).Select( z => z.Cluster ).FirstOrDefault()) as Associational;
                StylisedCluster sCluster;
                string error = null;
                Associational seca;

                if (selection.Secondary is IAssociation)
                {
                    seca = ((IAssociation)selection.Secondary).Associated as Associational;
                }
                else
                {
                    seca = selection.Secondary as Associational;
                }

                // Plot that!
                if (cluster == null)
                {
                    sCluster = null;
                }
                else
                {
                    switch (cluster.AssociationalClass)
                    {
                        case EVisualClass.Assignment:
                            sCluster = ((Assignment)cluster).CreateStylisedCluster( this._core, seca );
                            break;

                        case EVisualClass.Cluster:
                            sCluster = ((Cluster)cluster).CreateStylisedCluster( this._core, seca );
                            break;

                        case EVisualClass.Compound:
                            if (this.SelectedTrend.Results != null)
                            {
                                sCluster = ((Compound)cluster).CreateStylisedCluster( this._core, this.SelectedTrend.Results.Matrix, seca );
                            }
                            else
                            {
                                sCluster = null;
                                error = "A trend must be created and selected in order to plot compounds.";
                            }
                            break;

                        case EVisualClass.Pathway:
                            if (this.SelectedTrend.Results != null)
                            {
                                sCluster = ((Pathway)cluster).CreateStylisedCluster( this._core, this.SelectedTrend.Results.Matrix, seca );
                            }
                            else
                            {
                                sCluster = null;
                                error = "A trend must be created and selected in order to plot compounds.";
                            }
                            break;

                        default:
                            sCluster = null;
                            break;
                    }
                }

                // Plot stuffs
                this.PlotPeak( peak );
                this.PlotCluster( sCluster );

                if (sCluster != null)
                {
                    // Make sure the current peak is selected in that cluster
                    this._autoChangingSelection = true;
                    this._chartCluster.SelectSeries( peak );
                    this._autoChangingSelection = false;
                }

                if (error != null)
                {
                    this.SetChangeLabel( $"WARNING: {error}", EChangeLabelFx.Warning );
                }
                else
                {
                    this.SetChangeLabel( $"Plotted {{{selection}}}", EChangeLabelFx.Information );
                }
            }
            catch (Exception ex)
            {
                // Error: Normal case is the target has been deleted, but it could be anything, either way there is no need to crash
                this.CommitSelection( null );

                this.SetChangeLabel( "ERROR: " + ex.Message, EChangeLabelFx.Error );
            }
            finally
            {
                this.EndWait();
            }
        }

        private string LimitLength( object x )
        {
            string text = Column.AsString( x );

            if (text.Length >= 17)
            {
                text = text.Substring( 0, 16 ).TrimEnd() + "…";
            }

            return text;
        }

        private enum EChangeLabelFx
        {
            Information,
            Warning,
            Error
        }

        private void SetChangeLabel(string text, EChangeLabelFx fx )
        {
            this._lblChanges.Text = text;

            switch (fx)
            {
                case EChangeLabelFx.Error:
                case EChangeLabelFx.Warning:
                    this._lblChanges.BackColor = Color.Red;
                    this._lblChanges.ForeColor = Color.White;
                    break;

                case EChangeLabelFx.Information:
                    this._lblChanges.BackColor = Color.Transparent;
                    this._lblChanges.ForeColor = Color.Black;
                    break;
            }
        }

        private void UpdatePrimaryList()
        {
            IDataSet dataSet = this.GetPrimaryDataSet();

            this.HighlightByTag( this._tsDatasetsPrimary, this._primaryListView.Item2, dataSet.Title, this._btnPrimOther );

            this._primaryList.DivertList( dataSet );
        }

        private IDataSet GetPrimaryDataSet()
        {
            return this._primaryListView.Item1 ?? this._primaryListView.Item2?.Invoke( this._core );
        }

        private void HighlightByTag( ToolStrip bar, object sel, string selText, ToolStripButton other )
        {
            bool any = false;

            foreach (ToolStripItem tsi in bar.Items)
            {
                bool x = object.Equals( sel, tsi.Tag );
                tsi.BackgroundImage = x ? Resources.TabSel : Resources.TabUnsel;
                any |= x;     
            }

            if (!any)
            {
                other.BackgroundImage = Resources.TabSel;
                other.Text = selText;
            }
            else
            {
                other.Text = "Other";
            }
        }

        private void UpdateSecondaryList()
        {
            this.HighlightByTag( this._tsBarSelection, this._secondaryListView, this._secondaryListView.ToUiString(), this._btnSubOther );

            object selection = this._selection?.Primary;

            if (selection == null)
            {
                this._captSecondary.SetText( "No selection.");
                this._secondaryList.Clear();
                return;
            }

            ContentsRequest request = Associational.FindAssociations( selection, this._core, this._secondaryListView );      
            this._secondaryList.DivertList( request.Results, typeof(Association<>).MakeGenericType(request.TypeAsType));     

            if (request != null && request.Text != null)
            {
                this._captSecondary.SetText( request.Text, selection );
            }
            else
            {
                this._captSecondary.SetText( "No data - " + this._secondaryListView.ToString() + " unavailable for {0}.", selection );
            }
        }

        /// <summary>
        /// Ads the selection to the selection history
        /// </summary>
        private void AddToHistory(VisualisableSelection selection)
        {
            if (selection != null)
            {
                this._viewHistory.Remove(selection);
                this._viewHistory.Insert(0, selection);

                while (this._viewHistory.Count > 10)
                {
                    this._viewHistory.RemoveAt(this._viewHistory.Count - 1);
                }
            }
        }

        /// <summary>
        /// Plots the specified cluster.
        /// </summary>
        private void PlotCluster(StylisedCluster p)
        {
            this.BeginWait("Plotting cluster");

            try
            {
                // Select it in the clusters list
                this._autoChangingSelection = true;   

                this._chartCluster.Plot( p );

                this._autoChangingSelection = false;
            }
            finally
            {
                this.EndWait();
            }
        }

        /// <summary>
        /// Hides the please wait bar
        /// </summary>
        private void EndWait()
        {
            this._waitCounter--;
            UiControls.Assert(this._waitCounter >= 0, "EndWait called when not waiting.");

            if (this._waitCounter == 0)
            {
                this.toolStripStatusLabel2.Visible = false;
                this.UseWaitCursor = false;
                this._lblChanges.Visible = true;
                this._statusMain.BackColor = this.BackColor;
                this.toolStripProgressBar1.Visible = false;
            }
        }

        /// <summary>
        /// Shows the "please wait" bar
        /// </summary>
        /// <param name="p">Please wait text</param>
        /// <param name="pb">Whether the progress bar is shown (use with UpdateProgressBar)</param>
        private void BeginWait(string p, bool pb = false)
        {
            this._waitCounter++;
            this.toolStripStatusLabel2.Text = p;
            this.toolStripStatusLabel2.Visible = true;
            this._lblChanges.Visible = false;
            this._lastProgressTime.Restart();
            this._statusMain.BackColor = Color.Orange;
            this._statusMain.Refresh();

            if (pb)
            {
                this.toolStripProgressBar1.Visible = true;
            }
        }

        /// <summary>
        /// Updates the progress bar
        /// </summary>
        private void UpdateProgressBar(int value, int max)
        {
            this.toolStripProgressBar1.Maximum = max;
            this.toolStripProgressBar1.Value = value;

            if (this._lastProgressTime.ElapsedMilliseconds > 1000)
            {
                this._lastProgressTime.Restart();
                this._statusMain.BackColor = this._statusMain.BackColor == Color.Red ? Color.Orange : Color.Red;
            }

            this._statusMain.Refresh();
        }

        /// <summary>
        /// Plots the specified peak.
        /// </summary>
        private void PlotPeak(Peak peak)
        {
            this.BeginWait("Plotting peak");

            try
            {
                // Select it in the peaks list
                this._autoChangingSelection = true; 

                StylisedPeak sp = new StylisedPeak( peak );
                this._chartPeak.Plot( sp );

                this._autoChangingSelection = false;
            }
            finally
            {
                this.EndWait();
            }
        }

        /// <summary>
        /// Menu: Load
        /// </summary>
        private void loadDataSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Core newCore = FrmEditDataFileNames.Show(this);

            if (newCore != null)
            {
                this._core = newCore;
                this.HandleCoreChange();
            }
        }

        /// <summary>
        /// Updates the main view menu with the checkboxes for the types
        /// </summary>
        private void UpdateVisualOptions()
        {
            // Title
            this._btnSession.Text = this._core.FileNames.GetShortTitle();
            this.Text = UiControls.Title + " - " + this._btnSession.Text;

            // Remove existing items
            List<ToolStripMenuItem> toClear = new List<ToolStripMenuItem>();

            foreach (object item in this.viewToolStripMenuItem.DropDownItems)
            {
                if (item is ToolStripMenuItem)
                {
                    ToolStripMenuItem tsmi = (ToolStripMenuItem)item;

                    if (tsmi.Tag is GroupInfo)
                    {
                        toClear.Add(tsmi);
                    }
                }
            }

            foreach (var tsmi in toClear)
            {
                this.viewToolStripMenuItem.DropDownItems.Remove(tsmi);
                tsmi.Dispose();
            }

            List<ToolStripButton> toClear2 = new List<ToolStripButton>();

            foreach (object item in this.toolStrip1.Items)
            {
                if (item is ToolStripButton)
                {
                    ToolStripButton tsmi = (ToolStripButton)item;

                    if (tsmi.Tag is GroupInfo)
                    {
                        toClear2.Add(tsmi);
                    }
                }
            }

            foreach (var tsmi in toClear2)
            {
                this.toolStrip1.Items.Remove(tsmi);
                tsmi.Dispose();
            }

            // Add new items
            int index2 = this.toolStrip1.Items.IndexOf(this._tssInsertViews);

            foreach (GroupInfo ti in this._core.Groups.WhereEnabled().OrderBy(z => z.DisplayPriority))
            {
                bool e = this._core.Options.ViewTypes.Contains(ti);

                var tsmi = new ToolStripMenuItem("Display " + ti.DisplayName)
                {
                    Tag = ti,
                    Image = UiControls.CreateExperimentalGroupImage(e, ti, false)
                };

                var tsmi2 = new ToolStripButton( ti.DisplayName.ToUpper() )
                {
                    Tag = ti,
                    Image = UiControls.CreateExperimentalGroupImage( e, ti, true ),
                    TextImageRelation = TextImageRelation.ImageAboveText,
                    ForeColor = ti.Colour,
                    Alignment = ToolStripItemAlignment.Right,
                };

                tsmi.Click += this.experimentTypeMenuItem_Click;
                tsmi2.Click += this.experimentTypeMenuItem_Click;
                tsmi2.MouseDown += this.experimentTypeMenuItem_MouseDown;

                this.viewToolStripMenuItem.DropDownItems.Add(tsmi);
                this.toolStrip1.Items.Insert(index2, tsmi2);
                index2++;
            }

            // These are essentially "fake" items since Strings cannot be selected
            this._lstDatasetCb.Items.Clear();
            this._lstDatasetCb.Items.AddRange( this._core.Matrices.Cast<object>().ToArray() );
            this._lstDatasetCb.SelectedItem = this._core.Options.SelectedMatrixProvider;

            this._lstTrendCb.Items.Clear();
            this._lstTrendCb.Items.AddRange( this._core.Trends.Cast<object>().ToArray() );
            this._lstTrendCb.SelectedItem = this._core.Options.SelectedTrend;
        }

        /// <summary>
        /// Menu: Experimental group visibility toggle
        /// </summary>
        void experimentTypeMenuItem_Click(object sender, EventArgs e)
        {
            Core core = this._core;
            ToolStripItem senderr = (ToolStripItem)sender;
            GroupInfo type = (GroupInfo)senderr.Tag;

            if (core.Options.ViewTypes.Contains( type ))
            {
                core.Options.ViewTypes.Remove( type );
            }
            else
            {
                core.Options.ViewTypes.Add( type );
                core.Options.ViewTypes.Sort( ( x, y ) => x.DisplayPriority.CompareTo( y.DisplayPriority ) );
            }

            this.RegenerateExperimentalGroupIcon( type );
        }

        private void RegenerateExperimentalGroupIcon( GroupInfo type )
        {
            bool @checked = this._core.Options.ViewTypes.Contains( type );
            ToolStripButton tsb = this.toolStrip1.Items.OfType<ToolStripButton>().First( z => z.Tag == type );
            ToolStripMenuItem tsmi = this.viewToolStripMenuItem.DropDownItems.OfType<ToolStripMenuItem>().First( z => z.Tag == type );
            tsb.Image.Dispose();
            tsb.Image = UiControls.CreateExperimentalGroupImage( @checked, type, true );
            tsmi.Image.Dispose();
            tsmi.Image = UiControls.CreateExperimentalGroupImage( @checked, type, false );
            this.Replot();
        }

        private void experimentTypeMenuItem_MouseDown( object sender, MouseEventArgs e )
        {
            ToolStripItem senderr = (ToolStripItem)sender;
            GroupInfo type = (GroupInfo)senderr.Tag;

            if (e.Button == MouseButtons.Right)
            {
                if (FrmEditGroupBase.Show( this, type, false ))
                {
                    this.RegenerateExperimentalGroupIcon( type );
                }
            }
        }

        /// <summary>
        /// Meanu: Save
        /// </summary>
        private void saveExemplarsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.PromptSaveData(FileDialogMode.Save);
        }

        /// <summary>
        /// Prompts to save data.
        /// </summary>
        /// <returns>If the data was saved successfully</returns>
        private bool PromptSaveData(FileDialogMode mode)
        {
            // Force save as?
            if (mode == FileDialogMode.Save && this._core.FileNames.ForceSaveAs)
            {
                mode = FileDialogMode.SaveAs;
            }

            string fileName = this.BrowseForFile( this._core.FileNames.Session, UiControls.EFileExtension.Sessions, mode, UiControls.EInitialFolder.Sessions );

            if (string.IsNullOrWhiteSpace( fileName ))
            {
                return false;
            }

            try
            {
                FrmWait.Show( this, "Saving session", null, this.SaveCore, fileName );

                FileInfo fi = new FileInfo( fileName );
                double mb = fi.Length / (1024d * 1024d);
                this.SetChangeLabel( $"Saved data, {mb.ToString( "F01" )}Mb", EChangeLabelFx.Information);

                return true;
            }
            catch (Exception ex)
            {
                FrmMsgBox.ShowError( this, ex );
                this.SetChangeLabel( "FAILED TO SAVE DATA", EChangeLabelFx.Error );
                return false;
            }                                             
        }

        private void SaveCore( ProgressReporter prog, string fileName )
        {
            // Save the file
            this._core.FileNames.Session = fileName;
            this._core.FileNames.ForceSaveAs = false;
            this._core.FileNames.AppVersion = UiControls.Version;                      
            this._core.Save( fileName, prog ); 

            // Remember recent files
            using (prog.Section( "Adding to sessions list" ))
            {
                MainSettings.Instance.AddRecentSession( this._core );
                MainSettings.Instance.Save( MainSettings.EFlags.RecentSessions);
            }                  
        }

        /// <summary>
        /// Menu: Save as
        /// </summary>
        private void saveSessionAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.PromptSaveData(FileDialogMode.SaveAs);
        }

        /// <summary>
        /// Replots whatever is already plotted.
        /// </summary>
        private void Replot()
        {
            this.CommitSelection(new VisualisableSelection(this.Selection.Primary, this.Selection.Secondary));
        }

        /// <summary>
        /// Menu: Exit
        /// </summary>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Menu: Refresh all
        /// </summary>
        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.UpdateAll("Manual refresh");
        }

        /// <summary>
        /// Updates stuff.
        /// </summary>
        private void UpdateAll(string reason)
        {                             
            this.BeginWait( "PLEASE WAIT..." );

            this.UpdatePrimaryList();
            this.UpdateSecondaryList();         
            this.Replot();

            this.EndWait();
            this.SetChangeLabel( reason, EChangeLabelFx.Information );
        }

        /// <summary>
        /// Menu: Print all clusters
        /// </summary>
        private void printClusterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!this.PromptPrintTitle())
            {
                return;
            }

            PrintDocument pd = new PrintDocument();
            pd.PrintPage += this.printDocument_PrintPage;

            using (PrintDialog pdl = new PrintDialog())
            {
                if (UiControls.ShowWithDim(this, pdl) == DialogResult.OK)
                {
                    pd.PrinterSettings = pdl.PrinterSettings;

                    // PREVIEW
                    /*
                    using (PrintPreviewDialog ppd = new PrintPreviewDialog())
                    {
                        ppd.Document = pd;

                        try
                        {
                            UiControls.ShowWithDim(this, ppd);
                        }
                        catch (Exception ex)
                        {
                            this.ShowError("Print preview dialog crashed, gave this error, that's all I know:\r\n\r\n" + ex.ToString());
                        }
                    }
                     */

                    // NOPREVIEW
                    try
                    {
                        pd.Print();
                    }
                    catch (Exception ex)
                    {
                        FrmMsgBox.ShowError(this, "Something went wrong printing, this is the message:\r\n\r\n" + ex.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// Requests the user to specify the print title.
        /// </summary>
        private bool PromptPrintTitle()
        {
            Core core = this._core;
            this._printTitle = core.FileNames.Title + "\r\n" + core.Clusters.Count() + " clusters, " + core.Peaks.Count + " variables";

            string pt = FrmInputMultiLine.Show(this, "Printing", "Print", "Enter or change the title for the print", this._printTitle);

            if (pt == null)
            {
                return false;
            }

            this._printTitle = pt;
            return true;
        }

        /// <summary>
        /// Menu: Save cluster image (TODO: Obsolete? This is an option on the cluster plot menu as well)
        /// </summary>
        private void saveClusterImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!this.PromptPrintTitle())
            {
                return;
            }

            string imageSize = FrmInputSingleLine.Show(this, "Save cluster image", "Save image", "Specify the image size (x, y)", "1536, 2048");

            if (imageSize == null)
            {
                return;
            }

            var i = imageSize.Split(",".ToCharArray(), 2);
            int x = int.Parse(i[0]);
            int y = int.Parse(i[1]);

            string fileName = this.BrowseForFile(null, UiControls.EFileExtension.PngOrEmf, FileDialogMode.SaveAs, UiControls.EInitialFolder.SavedImages);

            if (fileName != null)
            {
                using (Bitmap bmp = this.GenerateClusterBitmap(x, y))
                {
                    bmp.Save(fileName);
                }
            }
        }

        /// <summary>
        /// Prompts the user to saves the specified chart to an image file
        /// </summary>
        /// <param name="chart">Chart to save</param>
        private void SaveChart(ChartHelper chart)
        {
            string fileName = this.BrowseForFile(null, UiControls.EFileExtension.PngOrEmf, FileDialogMode.SaveAs, UiControls.EInitialFolder.SavedImages);

            if (fileName != null)
            {
                chart.Chart.DrawToBitmap().Save(fileName, fileName.ToUpper().EndsWith(".EMF") ? ImageFormat.Emf : ImageFormat.Png);
            }
        }

        /// <summary>
        /// Generates a bitmap containing all clusters.
        /// </summary>
        Bitmap GenerateClusterBitmap(int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                this.DrawAllClusters(width, height, 0, 0, g);
            }

            return bmp;
        }

        /// <summary>
        /// Draws all clusters to the specified graphics device
        /// </summary>
        /// <param name="width">Width of image to draw</param>
        /// <param name="height">Height of image to draw</param>
        /// <param name="ofx">X offset of image to draw</param>
        /// <param name="ofy">Y offset of image to draw</param>
        /// <param name="g">Where to draw the image to</param>
        private void DrawAllClusters(int width, int height, int ofx, int ofy, Graphics g)
        {
            List<Cluster> clusters = new List<Cluster>(this._core.Clusters);
            int nvars = clusters.Count;
            int xa = (int)Math.Sqrt(nvars);
            int ya = (int)Math.Ceiling((double)nvars / xa);

            const int TITLE_MARGIN = 128;
            int xi = width / xa;
            int yi = (height - TITLE_MARGIN) / ya;

            // if DPI = 600 that's a printer (else it should be ~96)
            bool isPrinter = g.DpiX >= 600;

            using (Font titleFont = new Font("Arial", isPrinter ? 8 : 16, FontStyle.Regular, GraphicsUnit.Pixel))
            {
                //g.DrawString(_printTitle.Replace("|", "\r\n"), titleFont, Brushes.Black, new RectangleF(ofx, ofy, width, titleMargin));

                int x = 0;
                int y = 0;
                int yim = (int)(g.MeasureString("X", titleFont).Height) * 2;

                foreach (object xx in this._primaryList.GetVisible())
                {
                    Cluster p = xx as Cluster;

                    if (p == null)
                    {
                        continue;
                    }

                    StylisedCluster sp = new StylisedCluster(p);

                    this._chartClusterForPrinting.Plot(sp);

                    Rectangle r = new Rectangle(ofx + x * xi, ofy + TITLE_MARGIN + y * yi, xi, yi - yim);

                    if (isPrinter)
                    {
                        UiControls.Assert(g.PageUnit == GraphicsUnit.Display, "pageunit should be Display");

                        g.PageUnit = GraphicsUnit.Pixel;
                        Rectangle rP = new Rectangle(r.X * (int)(g.DpiX / 100f),
                                                    r.Y * (int)(g.DpiY / 100f),
                                                    r.Width * (int)(g.DpiX / 100f),
                                                    r.Height * (int)(g.DpiY / 100f));

                        using (Bitmap bmp = this._chartClusterForPrinting.Chart.DrawToBitmap(r.Width, r.Height))
                        {
                            g.DrawImage(bmp, rP);
                        }

                        g.PageUnit = GraphicsUnit.Display;
                    }
                    else
                    {
                        using (Bitmap bmp = this._chartClusterForPrinting.Chart.DrawToBitmap(r.Width, r.Height))
                        {
                            g.DrawImage(bmp, r);
                        }
                    }

                    //int strWid = (int)(g.MeasureString(p.Name, titleFont).Width / 2);
                    //g.DrawString(p.Name, titleFont, Brushes.Black, r.X + xi / 2 - strWid, r.Y);
                    //string sub = "n = " + p.Assignments.Count;
                    //strWid = (int)(g.MeasureString(sub, titleFont).Width / 2);
                    //g.DrawString(sub, titleFont, Brushes.Black, r.X + xi / 2 - strWid, r.Bottom);

                    if (p.States == Cluster.EStates.Insignificants)
                    {
                        using (Pen pen = new Pen(Color.Black, 4))
                        {
                            g.DrawLine(pen, r.Left, r.Top, r.Right, r.Bottom);
                            g.DrawLine(pen, r.Right, r.Top, r.Left, r.Bottom);
                        }
                    }

                    x++;

                    if (x >= xa)
                    {
                        x = 0;
                        y++;

                        UiControls.Assert(y < ya || x == 0,"Plotted outside expected area" );
                    }
                }
            }
        }

        /// <summary>
        /// Handles print cluster image
        /// </summary>
        void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Bitmap printing
            using (Bitmap bmp = this.GenerateClusterBitmap(e.MarginBounds.Width * 2, e.MarginBounds.Height * 2))
            {
                try
                {
                    e.Graphics.DrawImage(bmp, e.MarginBounds);
                }
                catch (Exception ex)
                {
                    FrmMsgBox.ShowError(this, "Printer driver crashed, gave this error:\r\n\r\n" + ex.ToString());
                }
            }

            // Vector printing
            // DrawAllClusters(e.MarginBounds.Width, e.MarginBounds.Height, e.MarginBounds.Left, e.MarginBounds.Top, e.Graphics);

            e.HasMorePages = false;
        }
           
        private void exportClustersToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // RM
        }

        /// <summary>
        /// Peak list: Keypress --> Set/clear comment flags
        /// </summary>
        private void _lstVariables_KeyDown(object sender, KeyEventArgs e)
        {
            Peak peakk = this._primaryList.Selection as Peak;

            if (peakk == null)
            {
                return;
            }

            if (this._core.Options.EnablePeakFlagging)
            {
                foreach (var f in this._core.Options.PeakFlags)
                {
                    if (f.Key == (char)e.KeyCode)
                    {
                        NativeMethods.Beep(f.BeepFrequency, f.BeepDuration);

                        if (e.Control)
                        {            
                            bool add = !peakk.CommentFlags.Contains(f);

                            if (FrmMsgBox.ShowOkCancel(this, f.ToString(), (add ? "Apply this flag to" : "Remove this flag from") + " all peaks shown in list?"))
                            {
                                foreach (object xx in this._primaryList.GetVisible())
                                {
                                    Peak peak = xx as Peak;

                                    if (peak == null)
                                    {
                                        continue;
                                    }

                                    if (add)
                                    {
                                        if (!peak.CommentFlags.Contains(f))
                                        {
                                            peak.CommentFlags.Add(f);
                                        }
                                    }

                                    if (!add)
                                    {
                                        if (peak.CommentFlags.Contains(f))
                                        {
                                            peak.CommentFlags.Remove(f);
                                        }
                                    }
                                }

                                this._primaryList.Rebuild(EListInvalids.ValuesChanged);
                            }
                        }
                        else
                        {
                            peakk.ToggleCommentFlag(f);
                            this._primaryList.Update( peakk);
                            e.Handled = true;
                        }

                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Handles: Form closing
        /// </summary>
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            if (!this._ignoreConfirmClose)
            {
                switch (FrmSelectClosure.Show(this, this._core))
                {
                    case true:
                        e.Cancel = !this.PromptSaveData(FileDialogMode.Save);
                        break;

                    case false:
                        break;

                    case null:
                        e.Cancel = true;
                        break;
                }
            }
        }

        /// <summary>
        /// Menu: Visual options
        /// </summary>
        private void visualOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmEditCoreOptions.Show(this, this._core, false);
            this.UpdateVisualOptions();
            this.UpdateAll("Changed options");
            this.Replot();
        }

        /// <summary>
        /// Menu: Cluster wizard
        /// </summary>
        private void autogenerateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmWizConfigurationCluster.Show(this, this._core))
            {
                this.UpdateAll("Autogenerated clusters");
            }
        }

        /// <summary>
        /// Menu: Session information
        /// </summary>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UiControls.ShowSessionInfo(this, this._core.FileNames);
        }

        /// <summary>
        /// Menu: Help | About
        /// </summary>
        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            UiControls.ShowAbout( this );
        }

        /// <summary>
        /// Selection Toolstrip: Selection history click (main item)
        /// </summary>
        private void _btnBack_ButtonClick(object sender, EventArgs e)
        {
            if (this._viewHistory.Count == 0)
            {
                return;
            }

            this.CommitSelection(new VisualisableSelection(this._viewHistory[0].Primary, this._viewHistory[0].Secondary));
        }

        /// <summary>
        /// Selection Toolstrip: Selection history open sub items
        /// </summary>
        private void _btnBack_DropDownOpening(object sender, EventArgs e)
        {
            this._btnBack.DropDownItems.Clear();

            foreach (VisualisableSelection o in this._viewHistory)
            {
                Image image = o.Primary != null ? UiControls.GetImage(o.Primary, true) : Resources.IconTransparent;
                ToolStripButton historyButton = new ToolStripButton(o.ToString(), image);
                historyButton.Click += this.historyButton_Click;
                historyButton.Tag = o;
                this._btnBack.DropDownItems.Add(historyButton);
            }
        }

        /// <summary>
        /// Selection Toolstrip: Selection history click (sub item)
        /// </summary>
        void historyButton_Click(object sender, EventArgs e)
        {
            var control = (ToolStripButton)sender;
            VisualisableSelection tag = (VisualisableSelection)control.Tag;
            this.CommitSelection(tag);
        }

        /// <summary>
        /// Menu: Reset do not show again
        /// </summary>
        // TODO: Why is this unused?
        private void resetdoNotShowAgainItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainSettings.Instance.DoNotShowAgain.Clear();
            MainSettings.Instance.Save(MainSettings.EFlags.DoNotShowAgain);

            FrmMsgBox.ShowCompleted(this, "Show Messages", "\"Do not show again\" items have been reset. You may need to restart the program to see changes.");
        }

        /// <summary>
        /// Session menu: Add comments
        /// </summary>
        private void editNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSession.Show(this, this._core);
            this.UpdateVisualOptions();
        }

        /// <summary>
        /// Selection menu: Add comments
        /// </summary>
        private void addCommentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this._selectionMenuOpenedFromList == null)
            {
                return;
            }

            var x = this._selectionMenuOpenedFromList as Visualisable;

            if (x == null)
            {
                new MsgBox()
                {                
                    Title = "Selection",
                    Message = "This selection cannot be modified.\r\n\r\nData type = " + this._selectionMenuOpenedFromList.GetType().ToUiString() + "\r\nValue = " + this._selectionMenuOpenedFromList,
                    HelpText = "The selection cannot be modified since it is not in the database, it is probably a temporary value; such as the result of a calculation, a compound value; associating two or more other items, or a fixed value; such as a field title.",
                    Level = ELogLevel.Information,
                }.Show(this);

                return;
            }

            FrmEditINameable.Show(this, x, false);

            this.UpdateAll("Comment changed");
        }

        /// <summary>
        /// Selection menu: View online
        /// </summary>
        private void viewOnlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string url;

            if (this._selectionMenuOpenedFromList is Compound)
            {
                Compound c = (Compound)this._selectionMenuOpenedFromList;
                url = c.Url;
            }
            else if (this._selectionMenuOpenedFromList is Pathway)
            {
                Pathway p = (Pathway)this._selectionMenuOpenedFromList;
                url = p.Url;
            }
            else
            {
                url = null;
            }

            if (!string.IsNullOrEmpty( url ))
            {
                UiControls.StartProcess(this, url );
            }
            else
            {
                FrmMsgBox.ShowInfo( this, "View Online", "Only pathways and compounds taken from the online database can be viewed online." );
            }
        }

        /// <summary>
        /// Handles opening of the SELECTION button menu
        /// </summary>
        private void _cmsSelectionButton_Opening(object sender, CancelEventArgs e)
        {
            this.PopulateMenu(this._cmsSelectionButton.Items);
        }

        private void PopulateMenu(ToolStripItemCollection items)
        {
            var t = this._selectionMenuOpenedFromList;

            items.Clear();

            if (t != null)
            {
                items.Add("&Navigate to " + t.ToString(), null, this.openToolStripMenuItem_Click_1);
                items.Add("&Edit name and comments", null, this.addCommentsToolStripMenuItem_Click);
            }
            else
            {
                items.Add("&Navigate to ...", null, null).Enabled = false;
                return;
            }

            if (t is Compound || t is Pathway)
            {
                items.Add("&View online", null, this.viewOnlineToolStripMenuItem_Click);
            }

            if (t is Cluster)
            {
                items.Add("&Break up large cluster...", null, this.breakUpLargeClusterToolStripMenuItem_Click);
            }

            if (t is Peak)
            {
                items.Add("&Compare to this peak...", null, this.compareToThisPeakToolStripMenuItem_Click);
            }

            foreach (ToolStripItem x in items)
            {
                x.Font = this.fileToolStripMenuItem.Font;
            }

            items[0].Font = new Font(items[0].Font, FontStyle.Bold);
        }        

        /// <summary>
        /// Menu: Debug
        /// </summary>
        private void experimentalOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmDebug.Show(this, this._core))
            {
                this.UpdateAll("Potential changes");
            }
        }

        /// <summary>
        /// For getting the current peak, etc.
        /// 
        /// Looks for a peak the user will be expecting (e.g. 
        /// * the peak they have selected, or 
        /// * the one on the graph
        /// * the last one they selected in the list
        /// * or failing that the first peak of the Core
        /// 
        /// This method takes a form and searches backwards to find the owner FrmMain.
        /// </summary>
        internal static Peak SearchForSelectedPeak(Form current)
        {
            FrmMain frm = GetFrmMain(current);

            if (frm == null)
            {
                return null;
            }

            return frm.Selection.Primary as Peak ?? frm._primaryList.Selection as Peak ?? frm._chartPeak.SelectedPeak ?? frm._core.Peaks[0];
        }

        /// <summary>
        /// Gets the main form by iterating back from the current form.
        /// </summary>
        private static FrmMain GetFrmMain(Form current)
        {
            while (!(current is FrmMain))
            {
                if (current == null)
                {
                    return null;
                }

                current = current.Owner;
            }

            return (FrmMain)current;
        }

        /// <summary>
        /// Menu: Edit trend
        /// </summary>
        private void edittrendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowEditor( EDataSet.Trends);
        }

        /// <summary>
        /// Menu: Create clusters
        /// </summary>
        private void createclustersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowEditor( EDataSet.Clusterers);
        }

        /// <summary>
        /// Menu: Edit corrections
        /// </summary>
        private void editCorrectionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowEditor( EDataSet.Corrections);
        }

        /// <summary>
        /// Menu: Edit statictics
        /// </summary>
        private void editStatisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowEditor( EDataSet.Statistics);
        }

        /// <summary>
        /// Menu: Edit experimental groups
        /// </summary>
        private void experimentalGroupsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowEditor( EDataSet.Groups);
        }

        /// <summary>
        /// Menu: PCA
        /// </summary>
        private void pCAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPca.Show(this, this._core);
        }

        /// <summary>
        /// Menu: Open selection
        /// </summary>
        private void openToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.CommitSelection(new VisualisableSelection(this._selectionMenuOpenedFromList));
        }

        /// <summary>
        /// Menu: Break up selection
        /// </summary>
        private void breakUpLargeClusterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cluster clu = (Cluster)this._selectionMenuOpenedFromList;
            PeakFilter filter = null;

            foreach (PeakFilter pf in this._core.PeakFilters)
            {
                if (pf.Conditions.Count == 1)
                {
                    PeakFilter.ConditionCluster x = pf.Conditions[0] as PeakFilter.ConditionCluster;

                    if (x != null)
                    {
                        if (x.Clusters.Count == 1 && x.Clusters[0].GetTarget() == clu && x.ClustersOp == Filter.ELimitedSetOperator.Any && x.CombiningOperator == Filter.ELogicOperator.And && x.Negate == false)
                        {
                            filter = pf;
                            break;
                        }
                    }
                }
            }

            if (filter == null)
            {
                PeakFilter.Condition condition = new PeakFilter.ConditionCluster(Filter.ELogicOperator.And, false, Filter.ELimitedSetOperator.Any, new[] { clu });
                filter = new PeakFilter(null, null, new[] { condition });
                this._core.AddPeakFilter(filter);
            }

            ConfigurationClusterer template = new ConfigurationClusterer() { Args = new ArgsClusterer( null, null, filter, null, null, false, EClustererStatistics.None, null ) };

            if (DataSet.ForClusterers(this._core).ShowListEditor(this, FrmBigList.EShow.Default, template) != null)
            {
                this.UpdateAll("Clusters changed");
            }
        }

        /// <summary>
        /// Menu: Compare to selection
        /// </summary>
        private void compareToThisPeakToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurationStatistic template = new ConfigurationStatistic( );
            template.Args = new ArgsStatistic( null, this._core.Options.SelectedMatrixProvider, null, EAlgoInputBSource.AltPeak, null, (Peak)this._selectionMenuOpenedFromList, null );

            DataSet.ForStatistics(this._core).ShowListEditor(this, FrmBigList.EShow.Default, template);
        }

        /// <summary>
        /// Menu: Change peak filters
        /// </summary>
        private void peakFiltersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowEditor( EDataSet.PeakFilters);
        }

        /// <summary>
        /// Menu: Change obs. filters
        /// </summary>
        private void observationFiltersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowEditor( EDataSet.ObservationFilters);
        }

        /// <summary>
        /// Menu: Clusterer optimiser
        /// </summary>
        private void clustererOptimiserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmActEvaluate.Show(this, this._core, null);
        }

        /// <summary>
        /// Menu: Selection button
        /// </summary>            
        private void _btnSelection_DropDownOpening(object sender, EventArgs e)
        {
            this._selectionMenuOpenedFromList = this.Selection.Primary;
            this.PopulateMenu(this._btnPrimarySelection.DropDownItems);
        }

        /// <summary>
        /// Menu: Exterior selection button
        /// </summary>            
        private void _btnSelectionExterior_DropDownOpening(object sender, EventArgs e)
        {
            this._selectionMenuOpenedFromList = this.Selection.Secondary;
            this.PopulateMenu(this._btnSecondarySelection.DropDownItems);
        }                              

        private void ShowEditor( EDataSet dataSetId)
        {
            IDataSet dataSet = DataSet.For( dataSetId, this._core );

            if (dataSet.ShowListEditor( this ))
            {
                switch (dataSetId)
                {
                    case EDataSet.Groups:
                        this.UpdateVisualOptions();
                        this.UpdateAll( "Experimental groups changed" );
                        break;

                    case EDataSet.Statistics:
                        this.UpdateAll( "Statistics changed" );
                        break;


                    case EDataSet.Clusterers:
                    case EDataSet.Trends:
                    case EDataSet.Corrections:
                        this.UpdateAll( "Database changes");
                        break;
                }
            }

            this.UpdateVisualOptions();
        }

        private void sessionInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.aboutToolStripMenuItem.PerformClick();
        }

        private void _btnExterior_Click(object sender, EventArgs e)
        {
            this.CommitSelection(new VisualisableSelection(this._selection.Secondary, this._selection.Primary));
        }

        private void dataToolStripMenuItem_Click( object sender, EventArgs e )
        {
            FrmExport2.Show( this, this._core );
        }

        private void correlationMapToolStripMenuItem_Click( object sender, EventArgs e )
        {                                
            ArgsMetric args = new ArgsMetric( Algo.ID_METRIC_PEARSONDISTANCE, this._core.Options.SelectedMatrixProvider, new object[0]  );
            ConfigurationMetric metric = new ConfigurationMetric(  );
            metric.Args = args;

            DistanceMatrix dm = FrmWait.Show( this, "Creating value matrix", null,
                z => DistanceMatrix.Create( this._core, this._core.Options.SelectedMatrixProvider.Provide , metric, z ) );

            FrmActHeatMap.Show( this._core, this._primaryList, dm );
        }

        private void peakidentificationsToolStripMenuItem_Click( object sender, EventArgs e )
        {
            EAnnotation annotation = DataSet.ForDiscreteEnum<EAnnotation>( this._core, "Default annotation status", (EAnnotation)( - 1) ).ShowRadio( this, EAnnotation.Confirmed );

            if (annotation == (EAnnotation) (- 1))
            {
                return;
            }

            string idFile = UiControls.BrowseForFile( this, null, UiControls.EFileExtension.Csv, FileDialogMode.Open, UiControls.EInitialFolder.None );

            if (idFile == null)
            {
                return;
            }

            FileLoadInfo fileLoadInfo = XmlSettings.LoadAndResave<FileLoadInfo>( FileId.FileLoadInfo, ProgressReporter.GetEmpty(), null );

            List<string> warnings = new List<string>();

            try
            {
                FrmWait.Show( this, "Loading identifications", null, z => FrmActDataLoad.Load_5_UserIdentifications( fileLoadInfo, this._core._annotationsMeta, this._core.Peaks, this._core.Compounds, this._core.Adducts, idFile, annotation, warnings, z ) );
            }
            catch (Exception ex)
            {
                FrmMsgBox.ShowError( this, ex );
                return;
            }

            if (warnings.Count != 0)
            {
                FrmInputMultiLine.ShowFixed( this, "Load identifications", "Warnings", "One or more warnings were reported", warnings.JoinAsString( "\r\n" ) );
            }

            this.UpdateAll( "Identifications loaded" );
        }

        private void _lstMatrix_Click( object sender, EventArgs e )
        {
            var sel = DataSet.ForMatrixProviders( this._core ).ShowRadio( this, this._core.Options.SelectedMatrixProvider );

            if (sel != null)
            {
                this._lstDatasetCb.SelectedItem = sel;
            }
        }

        private void _lstTrend_Click( object sender, EventArgs e )
        {
            if (this._core.Trends.Count == 0)
            {
                FrmMsgBox.ShowInfo( this, "Trends", "No trends have been defined, the default trend (median) will be used in the interim." );
                return;
            }

            var sel = DataSet.ForTrends( this._core ).ShowRadio( this, this._core.Options.SelectedTrend );

            if (sel != null)
            {
                this._lstTrendCb.SelectedItem = sel;
            }
        }

        // TODO: Obsolete
        public IntensityMatrix SelectedMatrix => this._core.Options.SelectedMatrix;

        // TODO: Obsolete
        public ConfigurationTrend SelectedTrend => this._core.Options.SelectedTrend;     

        private void _lstDatasetCb_SelectedIndexChanged( object sender, EventArgs e )
        {
            Debug.WriteLine( this._lstDatasetCb.SelectedItem );
            var sel = this._lstDatasetCb.SelectedItem as IMatrixProvider;

            if (sel != null && sel != this._core.Options.SelectedMatrixProvider)
            {
                this._core.Options.SelectedMatrixProvider = sel;

                this.UpdateAll( "Data matrix changed" );
                this.UpdateVisualOptions();
                this.Replot();
            }
        }              

        private void _lstTrendCb_SelectedIndexChanged( object sender, EventArgs e )
        {
            var sel = this._lstDatasetCb.SelectedItem as ConfigurationTrend;

            if (sel != null && sel != this._core.Options.SelectedTrend)
            {
                this._core.Options.SelectedTrend = sel;

                this.UpdateVisualOptions();
                this.Replot();
            }                         
        }

        private void _btnPrimPeak_Click( object sender, EventArgs e )
        {
            ToolStripItem tsmi = (ToolStripItem)sender;
            this._primaryListView = (DataSet.Provider)tsmi.Tag;
            this.UpdatePrimaryList();
        }

        private void _btnPrimOther_Click( object sender, EventArgs e )
        {
            IDataSet dataSet = DataSet.ForDatasetProviders( this._core ).ShowList( this, this.GetPrimaryDataSet() ); ;

            if (dataSet == null)
            {
                return;
            }

            this._primaryListView = new Either<IDataSet, DataSet.Provider>( dataSet );
            this.UpdatePrimaryList();
        }

        private void _btnSubPeak_Click( object sender, EventArgs e )
        {
            ToolStripItem tsmi = (ToolStripItem)sender;
            this._secondaryListView = (EVisualClass)tsmi.Tag;
            this.UpdateSecondaryList();
        }

        private void _btnSubOther_Click( object sender, EventArgs e )
        {
            EVisualClass dataSet = DataSet.ForDiscreteEnum<EVisualClass>( this._core, "Associations", (EVisualClass)(-1) ).ShowRadio( this, (EVisualClass)(-1) );

            if (dataSet == (EVisualClass)(-1))
            {
                return;
            }

            this._secondaryListView = dataSet;
            this.UpdateSecondaryList();
        }                                    
    }
}
