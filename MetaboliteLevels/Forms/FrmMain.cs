using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MetaboliteLevels.Controls;
using MetaboliteLevels.Data;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Settings;
using MetaboliteLevels.Viewers;
using MetaboliteLevels.Algorithms;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Data.DataInfo;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Forms.Algorithms;
using MetaboliteLevels.Forms.Wizards;
using MetaboliteLevels.Forms.Editing;
using MetaboliteLevels.Forms.Generic;
using MetaboliteLevels.Forms.Startup;
using MetaboliteLevels.Forms.Tree_Explorer;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Viewers.Charts;
using MetaboliteLevels.Viewers.Lists;

namespace MetaboliteLevels.Forms
{
    // This is a comment, please remove it
    
    /// <summary>
    /// Main form.
    /// </summary>
    public partial class FrmMain : Form, IPreviewProvider
    {
        // Core - this holds all the data
        //private readonly CoreLink _coreLink = CoreLink.Instance;
        private Core _core;

        // Helpers
        private readonly ChartHelperForPeaks _varChart;
        private readonly ChartHelperForClusters _patChart;
        private readonly ChartHelperForPeaks _varChartForPrinting;
        private readonly ChartHelperForClusters _patChartForPrinting;

        // The list views
        private readonly ListViewHelper<Peak> _peakList;
        private readonly ListViewHelper<Cluster> _clusterList;
        private readonly ListViewHelper<Adduct> _adductList;
        private readonly ListViewHelper<Compound> _compoundList;
        private readonly ListViewHelper<Pathway> _pathwayList;

        private readonly ListViewHelper<Peak> _peakList2;
        private readonly ListViewHelper<Cluster> _clusterList2;
        private readonly ListViewHelper<Adduct> _adductList2;
        private readonly ListViewHelper<Compound> _compoundList2;
        private readonly ListViewHelper<Pathway> _pathwayList2;
        private readonly ListViewHelper<Assignment> _assignmentList2;

        // Pager controls
        private readonly PagerControl _pgrMain;
        private readonly PagerControl _pgrSub;

        // Selection dependent lists (bottom left)
        private readonly SubListHandler _subDisplay;

        // Whether to force close the form
        private readonly bool _ignoreConfirmClose;

        private readonly Stopwatch _lastProgressTime = Stopwatch.StartNew();
        private readonly List<IVisualisable> _viewHistory = new List<IVisualisable>();
        private readonly List<ICoreWatcher> _coreWatchers = new List<ICoreWatcher>();
        private bool _autoChangingSelection;
        private string _printTitle;
        private bool _preventExpand;
        private int _waitCounter;

        // Selection
        IVisualisable _selectedObject;
        IVisualisable _selectedSourceObject;
        private IVisualisable _selectionMenuOpenedFromList;
        private ListViewHelper<Assignment> _assignmentList;
        private readonly ListViewHelper<Data.Visualisables.Annotation> _annotationList2;
        private readonly ListViewHelper<Data.Visualisables.Annotation> _annotationList;


        /// <summary>
        /// Constructor
        /// </summary>
        public FrmMain()
        {
            // Create this form
            InitializeComponent();
            UiControls.SetIcon(this);

            // Initialise R
            if (!InitialiseR())
            {
                _ignoreConfirmClose = true;
                this.BeginInvoke((MethodInvoker)this.Close);
                return;
            }

            // Load core
            _core = FrmDataLoadQuery.Show(this);

            if (_core == null)
            {
                _ignoreConfirmClose = true;
                this.BeginInvoke((MethodInvoker)this.Close);
                return;
            }

            // Load image list
            UiControls.PopulateImageList(_imgList);

            // Charts
            _varChart = new ChartHelperForPeaks(_chartPeak, _core, _btnGraphVar);
            _patChart = new ChartHelperForClusters(_chartPat, _core, _btnGraphPat);
            _varChartForPrinting = new ChartHelperForPeaks(null, _core, null);
            _patChartForPrinting = new ChartHelperForClusters(null, _core, null);

            _patChart.SelectionChanged += patChart_SelectionChanged;
            _varChart.SelectionChanged += varChart_SelectionChanged;

            _coreWatchers.Add(_varChart);
            _coreWatchers.Add(_patChart);
            _coreWatchers.Add(_varChartForPrinting);
            _coreWatchers.Add(_patChartForPrinting);

            // Primary lists
            _peakList = CreatePrimaryList<Peak>(_lstVariables, z => z.Peaks);
            _clusterList = CreatePrimaryList<Cluster>(_lstClusters, z => z.Clusters);
            _adductList = CreatePrimaryList<Adduct>(_lstAdducts, z => z.Adducts);
            _compoundList = CreatePrimaryList<Compound>(_lstCompounds, z => z.Compounds);
            _pathwayList = CreatePrimaryList<Pathway>(_lstPathways, z => z.Pathways);
            _assignmentList = CreatePrimaryList<Assignment>(_lstAssignments, z => z.Assignments);
            _annotationList = CreatePrimaryList<Data.Visualisables.Annotation>(_lstAnnotations, z => z.Annotations);

            // Secondary lists
            _peakList2 = CreateSecondaryList<Peak>(_lst2Peaks);
            _clusterList2 = CreateSecondaryList<Cluster>(_lst2Clusters);
            _adductList2 = CreateSecondaryList<Adduct>(_lst2Adducts);
            _compoundList2 = CreateSecondaryList<Compound>(_lst2Compounds);
            _pathwayList2 = CreateSecondaryList<Pathway>(_lst2Pathways);
            _assignmentList2 = CreateSecondaryList<Assignment>(_lst2Assignments);
            _annotationList2 = CreateSecondaryList<Data.Visualisables.Annotation>(_lstSubAnnots);

            // Pagers
            _pgrMain = new PagerControl(ref tabControl1);
            _pgrMain.BindToButtons(_btnMain0, _btnMain1, _btnMain2, _btnMain3, _btnMain4, _btnMain5, _btnMainAnnots);

            _pgrSub = new PagerControl(ref _tabSubinfo);
            _pgrSub.BindToButtons(_btnSubInfo, _btnSubStat, _btnSubPeak, _btnSubPat, _btnSubComp, _btnSubAdd, _btnSubPath, _btnSubAss, _btnSubAnnot);

            _subDisplay = new SubListHandler(_core, _lst2Info, _lst2Stats);
            _subDisplay.Add(_adductList2, VisualClass.Adduct, _btnSubAdd);
            _subDisplay.Add(_compoundList2, VisualClass.Compound, _btnSubComp);
            _subDisplay.Add(_pathwayList2, VisualClass.Pathway, _btnSubPath);
            _subDisplay.Add(_clusterList2, VisualClass.Cluster, _btnSubPat);
            _subDisplay.Add(_peakList2, VisualClass.Peak, _btnSubPeak);
            _subDisplay.Add(_assignmentList2, VisualClass.Assignment, _btnSubAss);
            _subDisplay.Add(_annotationList2, VisualClass.Annotation, _btnSubAnnot);
            _coreWatchers.Add(_subDisplay);

            // General stuff
            HandleCoreChange();
        }

        /// <summary>
        /// Sets up one of the lists
        /// </summary>
        private ListViewHelper<T> CreatePrimaryList<T>(ListView view, Func<Core, IEnumerable<T>> contentRetriever) where T : class, IVisualisable
        {
            var r = new ListViewHelper<T>(view, _core, contentRetriever, this);
            r.Activate += primaryList_Activate;
            r.ShowContextMenu += list_ShowContextMenu;
            _coreWatchers.Add(r);
            return r;
        }

        /// <summary>
        /// Sets up one of the lists
        /// </summary>
        private ListViewHelper<T> CreateSecondaryList<T>(ListView view) where T : class, IVisualisable
        {
            var r = new ListViewHelper<T>(view, _core, null, this);
            r.Activate += secondaryList_Activate;
            r.ShowContextMenu += list_ShowContextMenu;
            _coreWatchers.Add(r);
            return r;
        }

        /// <summary>
        /// Any of the lists showing their context menu
        /// </summary>
        private void list_ShowContextMenu(object sender, ShowContextMenuEventArgs e)
        {
            _selectionMenuOpenedFromList = e.selection;
            _cmsSelectionButton.Show(e.control, e.x, e.y);
        }

        /// <summary>
        /// Implements IPreviewProvider
        /// </summary>
        Image IPreviewProvider.ProvidePreview(Size s, IVisualisable o, IVisualisable o2)
        {
            bool small = s.Width < 96;

            BeginWait("Creating preview for " + o.DisplayName);
            Bitmap result;

            try
            {
                switch (o.VisualClass)
                {
                    case VisualClass.Adduct:
                        {
                            result = null;
                        }
                        break;

                    case VisualClass.Compound:
                        {
                            StylisedCluster p = ((Compound)o).CreateStylisedCluster(_core, o2);
                            p.IsPreview = small;
                            result = _patChartForPrinting.CreateBitmap(s.Width, s.Height, p);
                        }
                        break;

                    case VisualClass.Pathway:
                        {
                            StylisedCluster p = ((Pathway)o).CreateStylisedCluster(_core, o2);
                            p.IsPreview = small;
                            result = _patChartForPrinting.CreateBitmap(s.Width, s.Height, p);
                        }
                        break;

                    case VisualClass.Cluster:
                        {
                            StylisedCluster p = new StylisedCluster((Cluster)o);
                            p.IsPreview = small;
                            result = _patChartForPrinting.CreateBitmap(s.Width, s.Height, p);
                        }
                        break;

                    case VisualClass.Peak:
                        {
                            StylisedPeak p = new StylisedPeak((Peak)o);
                            p.IsPreview = small;
                            result = _varChartForPrinting.CreateBitmap(s.Width, s.Height, p);
                        }
                        break;

                    default:
                        {
                            throw new InvalidOperationException("Invalid switch: " + o.VisualClass);
                        }
                }
            }
            finally
            {
                EndWait();
            }

            return result;
        }

        /// <summary>
        /// Item selected from primary list.
        /// </summary>
        void primaryList_Activate(object sender, ListViewItemEventArgs e)
        {
            Activate(e.Selection, EActivateOrigin.None);
        }


        /// <summary>
        /// Item selected from secondary list.
        /// </summary>
        void secondaryList_Activate(object sender, ListViewItemEventArgs e)
        {
            Activate(e.Selection, _selectedSourceObject ?? _selectedObject, EActivateOrigin.TreeView);
        }

        /// <summary>
        /// Handle changing of "_core".
        /// </summary>
        void HandleCoreChange()
        {
            // Update stuff
            _coreWatchers.ForEach(z => z.ChangeCore(_core));

            // Clear selection
            Activate(null, EActivateOrigin.None);

            // Set new options
            UpdateVisualOptions();

            // Refresh lists
            UpdateAll("Data loaded", EListInvalids.SourceChanged, EListInvalids.SourceChanged, EListInvalids.SourceChanged, EListInvalids.SourceChanged);
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
                FrmMsgBox.ShowError(this, "There was a problem initialising R.\r\n\r\n" + ex.Message);
                forceShow = true;
                goto __RETRY__;
            }

            Algo.Initialise();

            return true;
        }

        /// <summary>
        /// Handles: varChart.SelectionChanged
        /// </summary>
        void varChart_SelectionChanged(object sender, ChartSelectionEventArgs e)
        {
            if (e.seriesName != null)
            {
                if (e.dataPoint != null)
                {
                    _lblVarObservation.Text = e.seriesName + ", " + e.dataPoint.ToString();
                }
                else
                {
                    _lblVarObservation.Text = e.seriesName;
                }

                _btnGraphVar2.Image = UiControls.CreateSolidColourImage(true, e.dataPoint.Group);
                _lblVarObservation.Visible = true;
                _btnGraphVar2.Visible = true;
            }
            else
            {
                _lblVarObservation.Visible = false;
                _btnGraphVar2.Visible = false;
            }
        }

        /// <summary>
        /// Handles: patChart.SelectionChanged
        /// </summary>
        void patChart_SelectionChanged(object sender, ChartSelectionEventArgs e)
        {
            if (e.seriesName != null)
            {
                if (e.dataPoint != null)
                {
                    _lblPatObs.Text = e.seriesName + ", " + e.dataPoint.ToString();
                }
                else
                {
                    _lblPatObs.Text = e.seriesName;
                }

                if (e.variable != null)
                {
                    _btnGraphPat2.Image = e.variable.DisplayIcon;
                }
                else
                {
                    _btnGraphPat2.Image = Resources.ObjPoint;
                }

                _lblPatObs.Visible = true;
                _btnGraphPat2.Visible = true;
            }
            else
            {
                _btnGraphPat2.Visible = false;
                _lblPatObs.Visible = false;
            }

            if (!_autoChangingSelection)
            {
                Activate(e.variable, EActivateOrigin.ClusterPlot);
            }
        }

        internal enum EActivateOrigin
        {
            None, // lists, replot, core change
            External,
            ClusterPlot,
            TreeView,
        }

        /// <summary>
        /// Selects something.
        /// </summary>
        internal void Activate(IVisualisable newSelection, EActivateOrigin origin)
        {
            Activate(newSelection, null, origin);
        }

        /// <summary>
        /// Selects something.
        /// </summary>
        internal void Activate(IVisualisable newSelection, IVisualisable overSelection, EActivateOrigin origin)
        {
            try
            {
                BeginWait("Updating displays");

                // What do we need?
                bool needsNode = origin != EActivateOrigin.TreeView;
                bool keepPatPlot = origin == EActivateOrigin.ClusterPlot;

                // Add to history
                AddToHistory(newSelection);

                // Set the selected object
                _selectedObject = newSelection;
                _selectedSourceObject = overSelection;

                // Update the secondary lists (unless we selected from there)
                if (origin != EActivateOrigin.TreeView)
                {
                    UiControls.Assert(_selectedSourceObject == null);
                    _subDisplay.Activate(newSelection);
                }

                // Icons
                IVisualisable object1;
                IVisualisable object2;

                if (_selectedSourceObject == null)
                {
                    object1 = _selectedObject;
                    object2 = null;
                }
                else
                {
                    object1 = _selectedSourceObject;
                    object2 = _selectedObject;
                }

                if (object1 != null)
                {
                    _lblCurrentSel.Text = object1.DisplayName;
                    _btnSel.Image = object1.DisplayIcon;
                }
                else
                {
                    _lblCurrentSel.Text = "No selection";
                    _btnSel.Image = Resources.ObjNone;
                }


                if (object2 != null)
                {
                    _lblSel2.Text = object2.DisplayName;
                    _btnSel2.Image = object2.DisplayIcon;
                    _lblSel2.Visible = true;
                    _btnSel2.Visible = true;
                }
                else
                {
                    _lblSel2.Visible = false;
                    _btnSel2.Visible = false;
                }

                // Null selection? (clear)
                if (newSelection == null)
                {
                    PlotVariable(null);

                    if (!keepPatPlot)
                    {
                        PlotCluster(null);
                    }

                    return;
                }

                // For assignments plot the peak (and implicitly the cluster)
                if (newSelection.VisualClass == VisualClass.Assignment)
                {
                    newSelection = ((Assignment)newSelection).Peak;
                }

                // Plot that!
                if (newSelection.VisualClass == VisualClass.Peak)
                {
                    Peak v = (Peak)newSelection;

                    PlotVariable(v);

                    if (overSelection != null && overSelection.VisualClass == VisualClass.Compound)
                    {
                        PlotCluster(((Compound)overSelection).CreateStylisedCluster(_core, v));
                    }
                    else if (overSelection != null && overSelection.VisualClass == VisualClass.Pathway)
                    {
                        PlotCluster(((Pathway)overSelection).CreateStylisedCluster(_core, v));
                    }
                    else if (!keepPatPlot)
                    {
                        if (v.Assignments.Count != 0)
                        {
                            Cluster clusterToPlot = v.Assignments.List[0].Cluster; // Plot the first by default - the user can select the cluster from the cluster submenu
                            PlotCluster(clusterToPlot.CreateStylisedCluster(_core, v));
                        }
                        else
                        {
                            PlotCluster(null);
                        }
                        _autoChangingSelection = true;
                        _patChart.SelectSeries(v);
                        _autoChangingSelection = false;
                    }
                }
                else if (newSelection.VisualClass == VisualClass.Cluster)
                {
                    Cluster p = (Cluster)newSelection;

                    // If the cluster has been deleted set a null selection
                    if (!_core.Clusters.Contains(p))
                    {
                        Debug.WriteLine("Warning: Selected cluster no longer available: " + p.DisplayName);
                        Activate(null, EActivateOrigin.None);
                        return;
                    }

                    PlotVariable(null);
                    PlotCluster(p.CreateStylisedCluster(_core, overSelection));

                    if (overSelection is Peak)
                    {
                        _autoChangingSelection = true;
                        _patChart.SelectSeries((Peak)overSelection);
                        _autoChangingSelection = false;
                    }
                }
                else if (newSelection.VisualClass == VisualClass.Adduct)
                {
                    var a = (Adduct)newSelection;

                    PlotVariable(null);
                    PlotCluster(null);
                }
                else if (newSelection.VisualClass == VisualClass.Pathway)
                {
                    var p = (Pathway)newSelection;
                    var fp = p.CreateStylisedCluster(_core, overSelection);

                    PlotVariable(null);
                    PlotCluster(fp);
                }
                else if (newSelection.VisualClass == VisualClass.Compound)
                {
                    var c = (Compound)newSelection;
                    var fp = c.CreateStylisedCluster(_core, overSelection);

                    PlotVariable(null);
                    PlotCluster(fp);
                }
                else
                {
                    throw new InvalidOperationException("Cannot activate object of type \"" + newSelection.GetType().Name + "\" with VisualClass \"" + newSelection.VisualClass + "\".");
                }
            }
            finally
            {
                EndWait();
            }
        }

        /// <summary>
        /// Ads the selection to the selection history
        /// </summary>
        private void AddToHistory(IVisualisable newSelection)
        {
            if (newSelection != null)
            {
                _viewHistory.Remove(newSelection);
                _viewHistory.Insert(0, newSelection);

                while (_viewHistory.Count > 10)
                {
                    _viewHistory.RemoveAt(_viewHistory.Count - 1);
                }
            }
        }

        /// <summary>
        /// Plots the specified cluster.
        /// </summary>
        private void PlotCluster(StylisedCluster p)
        {
            BeginWait("Plotting cluster");

            // Select it in the clusters list
            _autoChangingSelection = true;

            if (p == null || p.IsFake)
            {
                _clusterList.Selection = null;
            }
            else
            {
                _clusterList.Selection = p.Cluster;
            }

            if (p == null)
            {
                _btnGraphPat.Image = Resources.ObjNone;
            }
            else
            {
                _btnGraphPat.Image = p.ActualElement.DisplayIcon;
            }

            _patChart.Plot(p);

            _autoChangingSelection = false;
            UpdateClusterLabels();
            EndWait();
        }

        /// <summary>
        /// Hides the please wait bar
        /// </summary>
        private void EndWait()
        {
            _waitCounter--;
            UiControls.Assert(_waitCounter >= 0);

            if (_waitCounter == 0)
            {
                toolStripStatusLabel2.Visible = false;
                UseWaitCursor = false;
                _lblChanges.Visible = true;
                _statusMain.BackColor = BackColor;
                toolStripProgressBar1.Visible = false;
            }
        }

        /// <summary>
        /// Shows the "please wait" bar
        /// </summary>
        /// <param name="p">Please wait text</param>
        /// <param name="pb">Whether the progress bar is shown (use with UpdateProgressBar)</param>
        private void BeginWait(string p, bool pb = false)
        {
            _waitCounter++;
            toolStripStatusLabel2.Text = p;
            toolStripStatusLabel2.Visible = true;
            _lblChanges.Visible = false;
            _lastProgressTime.Restart();
            _statusMain.BackColor = Color.Red;
            _statusMain.Refresh();

            if (pb)
            {
                toolStripProgressBar1.Visible = true;
            }
        }

        /// <summary>
        /// Updates the progress bar
        /// </summary>
        private void UpdateProgressBar(int value)
        {
            UpdateProgressBar(value, 100);
        }

        /// <summary>
        /// Updates the progress bar
        /// </summary>
        private void UpdateProgressBar(int value, int max)
        {
            toolStripProgressBar1.Maximum = max;
            toolStripProgressBar1.Value = value;

            if (_lastProgressTime.ElapsedMilliseconds > 1000)
            {
                _lastProgressTime.Restart();
                _statusMain.BackColor = _statusMain.BackColor == Color.Red ? Color.Orange : Color.Red;
            }

            _statusMain.Refresh();
        }

        /// <summary>
        /// Plots the specified variable.
        /// </summary>
        private void PlotVariable(Peak peak)
        {
            BeginWait("Plotting peak");

            // Select it in the variables list
            _autoChangingSelection = true;

            if (peak == null)
            {
                _btnGraphVar.Image = Resources.ObjNone;
            }
            else
            {
                _btnGraphVar.Image = peak.DisplayIcon;
            }

            _peakList.Selection = peak; // make sure it is selected

            StylisedPeak sp = new StylisedPeak(peak);
            _varChart.Plot(sp);

            _autoChangingSelection = false;

            UpdateVariableLabels();
            EndWait();
        }

        /// <summary>
        /// Sets the labels above the cluster plot accordingly.
        /// </summary>
        private void UpdateClusterLabels()
        {
            Cluster p = _patChart.SelectedCluster;

            if (p == null)
            {
                _lblPatComment.Text = "";
                _lblPatName.Visible = false;
                return;
            }

            _lblPatComment.Text = _core.Options.ClusterDisplay.ConvertToString(p, _core);
            _lblPatName.Text = p.DisplayName;
            _lblPatName.Visible = true;
        }

        /// <summary>
        /// Sets the labels above the variable plot accordingly.
        /// </summary>
        private void UpdateVariableLabels()
        {
            Peak v = _varChart.SelectedPeak;

            if (v == null)
            {
                _lblVarName.Visible = false;
                _lblVarComment.Text = "";
                return;
            }

            _lblVarName.Visible = true;
            _lblVarName.Text = v.DisplayName;
            _lblVarComment.Text = _core.Options.VariableDisplay.ConvertToString(v, _core);
        }

        /// <summary>
        /// Menu: Load
        /// </summary>
        private void loadDataSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Core newCore = FrmDataLoadQuery.Show(this);

            if (newCore != null)
            {
                _core = newCore;
                HandleCoreChange();
            }
        }

        /// <summary>
        /// Updates the main view menu with the checkboxes for the types
        /// </summary>
        private void UpdateVisualOptions()
        {
            // Title
            _lblTitle.Text = _core.FileNames.GetShortTitle();
            Text = UiControls.Title + " - " + _lblTitle.Text;

            // Remove existing items
            List<ToolStripMenuItem> toClear = new List<ToolStripMenuItem>();

            foreach (object item in viewToolStripMenuItem.DropDownItems)
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
                viewToolStripMenuItem.DropDownItems.Remove(tsmi);
                tsmi.Dispose();
            }

            List<ToolStripButton> toClear2 = new List<ToolStripButton>();

            foreach (object item in toolStrip1.Items)
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
                toolStrip1.Items.Remove(tsmi);
                tsmi.Dispose();
            }

            // Add new items
            int index2 = toolStrip1.Items.IndexOf(_tssInsertViews);

            foreach (GroupInfo ti in _core.Groups.OrderBy(z => z.Id))
            {
                bool e = _core.Options.ViewTypes.Contains(ti);

                var tsmi = new ToolStripMenuItem("Display " + ti.Name)
                           {
                               Tag = ti,
                               Image = UiControls.CreateSolidColourImage(e, ti)
                           };

                var tsmi2 = new ToolStripButton(ti.Name)
                {
                    Tag = ti,
                    Image = UiControls.CreateSolidColourImage(e, ti),
                    TextImageRelation = TextImageRelation.ImageAboveText
                };

                tsmi.Click += experimentTypeMenuItem_Click;
                tsmi2.Click += experimentTypeMenuItem_Click;

                viewToolStripMenuItem.DropDownItems.Add(tsmi);
                toolStrip1.Items.Insert(index2++, tsmi2);
            }
        }

        /// <summary>
        /// Menu: Experimental group visibility toggle
        /// </summary>
        void experimentTypeMenuItem_Click(object sender, EventArgs e)
        {
            Core core = _core;
            ToolStripItem senderr = (ToolStripItem)sender;
            GroupInfo type = (GroupInfo)senderr.Tag;

            ToolStripButton tsb = toolStrip1.Items.OfType<ToolStripButton>().First(z => z.Tag == type);
            ToolStripMenuItem tsmi = viewToolStripMenuItem.DropDownItems.OfType<ToolStripMenuItem>().First(z => z.Tag == type);

            bool @checked;

            if (core.Options.ViewTypes.Contains(type))
            {
                core.Options.ViewTypes.Remove(type);
                @checked = false;
            }
            else
            {
                core.Options.ViewTypes.Add(type);
                core.Options.ViewTypes.Sort((x, y) => x.Id.CompareTo(y.Id));
                @checked = true;
            }

            tsb.Image.Dispose();
            tsb.Image = UiControls.CreateSolidColourImage(@checked, type);
            tsmi.Image.Dispose();
            tsmi.Image = UiControls.CreateSolidColourImage(@checked, type);

            Replot();
        }

        /// <summary>
        /// Meanu: Save
        /// </summary>
        private void saveExemplarsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PromptSaveData(FileDialogMode.Save);
        }

        /// <summary>
        /// Prompts to save data.
        /// </summary>
        private bool PromptSaveData(FileDialogMode mode)
        {
            // Force save as?
            if (mode == FileDialogMode.Save && _core.FileNames.ForceSaveAs)
            {
                mode = FileDialogMode.SaveAs;
            }

            string fileName = this.BrowseForFile(_core.FileNames.Session, UiControls.EFileExtension.Sessions, mode, UiControls.EInitialFolder.Sessions);

            if (string.IsNullOrWhiteSpace(fileName))
            {
                return false;
            }

            // Save the file
            _core.FileNames.Session = fileName;
            _core.FileNames.ForceSaveAs = false;
            _core.FileNames.AppVersion = UiControls.Version;
            BeginWait("Saving");
            bool success;

            try
            {
                FrmWait.Show(this, "Saving session", null, () => _core.Save(fileName));

                success = true;
            }
            catch (Exception ex)
            {
                FrmMsgBox.ShowError(this, ex);
                success = false;
            }

            EndWait();

            // Remember recent files
            MainSettings.Instance.AddRecentSession(fileName, _core.FileNames.Title);
            MainSettings.Instance.Save();

            // Display the filesize
            if (success)
            {
                FileInfo fi = new FileInfo(fileName);
                double mb = fi.Length / (1024d * 1024d);
                _lblChanges.Text = "Saved data, " + (mb.ToString("F01") + "Mb");
            }
            else
            {
                _lblChanges.Text = "FAILED TO SAVE DATA";
            }

            return true;
        }

        /// <summary>
        /// Menu: Save as
        /// </summary>
        private void saveSessionAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PromptSaveData(FileDialogMode.SaveAs);
        }

        /// <summary>
        /// Replots whatever is already plotted.
        /// </summary>
        private void Replot()
        {
            Activate(_selectedObject, EActivateOrigin.None);
        }

        /// <summary>
        /// Menu: Exit
        /// </summary>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Menu: Refresh all
        /// </summary>
        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateAll("Manual refresh", EListInvalids.SourceChanged, EListInvalids.SourceChanged, EListInvalids.SourceChanged, EListInvalids.SourceChanged);
        }

        /// <summary>
        /// Updates stuff.
        /// </summary>
        private void UpdateAll(string reason, EListInvalids peak, EListInvalids cluster, EListInvalids assignments, EListInvalids compounds)
        {
            this.UseWaitCursor = true;
            _lblChanges.Text = "PLEASE WAIT...";

            _peakList.Rebuild(peak);
            _clusterList.Rebuild(cluster);
            _assignmentList.Rebuild(assignments);

            _adductList.Rebuild(compounds);
            _compoundList.Rebuild(compounds);
            _pathwayList.Rebuild(compounds);

            Replot();

            this.UseWaitCursor = false;
            _lblChanges.Text = reason;
        }

        /// <summary>
        /// Menu: Print all clusters
        /// </summary>
        private void printClusterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!PromptPrintTitle())
            {
                return;
            }

            PrintDocument pd = new PrintDocument();
            pd.PrintPage += printDocument_PrintPage;

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
            Core core = _core;
            _printTitle = core.FileNames.Title + "\r\n" + core.Clusters.Count + " clusters, " + core.Peaks.Count + " variables";

            string pt = FrmInputLarge.Show(this, "Printing", "Print", "Enter or change the title for the print", _printTitle);

            if (pt == null)
            {
                return false;
            }

            _printTitle = pt;
            return true;
        }

        /// <summary>
        /// Menu: Save cluster image (TODO: Obsolete? This is an option on the cluster plot menu as well)
        /// </summary>
        private void saveClusterImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!PromptPrintTitle())
            {
                return;
            }

            string imageSize = FrmInput.Show(this, "Save cluster image", "Save image", "Specify the image size (x, y)", "1536, 2048");

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
                using (Bitmap bmp = GenerateClusterBitmap(x, y))
                {
                    bmp.Save(fileName);
                }
            }
        }

        /// <summary>
        /// Cluster plot menu: Save image
        /// </summary>
        private void saveclusterImageToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveChart(_patChart);
        }

        /// <summary>
        /// Peak plot menu: Save image
        /// </summary>
        private void savepeakImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveChart(_varChart);
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
                chart.Chart.SaveImage(fileName, fileName.ToUpper().EndsWith(".EMF") ? ChartImageFormat.Emf : ChartImageFormat.Png);
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
                DrawAllClusters(width, height, 0, 0, g);
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
            List<Cluster> clusters = new List<Cluster>(_core.Clusters);
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

                foreach (Cluster p in _clusterList.GetVisible())
                {
                    StylisedCluster sp = new StylisedCluster(p);

                    _patChartForPrinting.Plot(sp);

                    Rectangle r = new Rectangle(ofx + x * xi, ofy + TITLE_MARGIN + y * yi, xi, yi - yim);

                    if (isPrinter)
                    {
                        UiControls.Assert(g.PageUnit == GraphicsUnit.Display);

                        g.PageUnit = GraphicsUnit.Pixel;
                        Rectangle rP = new Rectangle(r.X * (int)(g.DpiX / 100f),
                                                    r.Y * (int)(g.DpiY / 100f),
                                                    r.Width * (int)(g.DpiX / 100f),
                                                    r.Height * (int)(g.DpiY / 100f));

                        _patChartForPrinting.Chart.Printing.PrintPaint(g, rP);
                        g.PageUnit = GraphicsUnit.Display;
                    }
                    else
                    {
                        _patChartForPrinting.Chart.Printing.PrintPaint(g, r);
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

                        UiControls.Assert(y < ya || x == 0);
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
            using (Bitmap bmp = GenerateClusterBitmap(e.MarginBounds.Width * 2, e.MarginBounds.Height * 2))
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

        /// <summary>
        /// Menu: Export clusters as CSV
        /// </summary>
        private void exportClustersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fileName = this.BrowseForFile(null, UiControls.EFileExtension.Csv, FileDialogMode.SaveAs, UiControls.EInitialFolder.ExportedData);

            if (fileName != null)
            {
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    sw.WriteLine(",name,clusters,clusterindices,mz,rt,compounds");

                    foreach (Peak v in _core.Peaks)
                    {
                        IEnumerable<string> rta = _core._peakMeta.GetValue(v.MetaInfo, "rt");

                        string rt = Maths.ArrayToString(rta);

                        StringBuilder sb = new StringBuilder();
                        foreach (var pc in v.Annotations)
                        {
                            if (sb.Length != 0)
                            {
                                sb.Append(", ");
                            }
                            sb.Append(pc.Compound.Name + " [" + pc.Adduct.Name + "]");
                        }

                        string patNames = Maths.ArrayToString(v.Assignments.Clusters, "; ");
                        string patIndices = Maths.ArrayToString(v.Assignments.Clusters, z => _core.Clusters.IndexOf(z).ToString(), "; ");
                        sw.WriteLine(v.DisplayName + "," + v.DisplayName + ",\"" + patNames + "\",\"" + patIndices + "\"," + v.Mz + "," + rt + ",\"" + sb.ToString() + "\"");
                    }
                }
            }
        }

        /// <summary>
        /// Menu: Show explorer
        /// </summary>
        private void clusterBreakdownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTreeView.Show(this, _core, null);
        }

        /// <summary>
        /// Peak list: Keypress --> Set/clear comment flags
        /// </summary>
        private void _lstVariables_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (_core.Options.EnablePeakFlagging)
            {
                char c = char.ToUpper(e.KeyChar);

                foreach (var f in _core.Options.PeakFlags)
                {
                    if (f.Key == c)
                    {
                        NativeMethods.Beep(f.BeepFrequency, f.BeepDuration);
                        _peakList.Selection.ToggleCommentFlag(f);
                        _peakList.Update(_peakList.Selection);
                        e.Handled = true;
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

            if (!_ignoreConfirmClose)
            {
                switch (FrmClosing.Show(this, _core))
                {
                    case true:
                        e.Cancel = !PromptSaveData(FileDialogMode.Save);
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
            FrmOptions.Show(this, "Visual options - " + _core.FileNames.Title, _core.Options);
            UpdateVisualOptions();
            UpdateAll("Changed options", EListInvalids.SourceChanged, EListInvalids.SourceChanged, EListInvalids.SourceChanged, EListInvalids.SourceChanged);
            Replot();
        }

        /// <summary>
        /// Menu: Cluster wizard
        /// </summary>
        private void autogenerateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmClusterWizard.Show(this, _core))
            {
                UpdateAll("Autogenerated clusters", EListInvalids.ValuesChanged, EListInvalids.ContentsChanged, EListInvalids.ContentsChanged, EListInvalids.None);
            }
        }

        /// <summary>
        /// Menu: Session information
        /// </summary>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmInputLarge.ShowFixed(this, UiControls.Title, "Current session information", Path.GetFileName(_core.FileNames.Session), _core.FileNames.GetDetails());
        }

        /// <summary>
        /// Explorer tree: Node select
        /// </summary>
        private void _tvwDetails_AfterSelect(object sender, TreeViewEventArgs e)
        {
            IVisualisable tag = e.Node.Tag as IVisualisable;

            if (tag != null) // could be string
            {
                Activate(tag, EActivateOrigin.TreeView);
            }
        }

        /// <summary>
        /// Explorer tree: Node expand
        /// </summary>
        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (_preventExpand)
            {
                //e.Cancel = true;
                //return;
            }

            TreeNodeConvertor.ExpandNodeIfNeeded(e.Node);
        }

        /// <summary>
        /// Explorer tree: Node click
        /// </summary>
        private void _tvwDetails_MouseDown(object sender, MouseEventArgs e)
        {
            _preventExpand = e.Clicks > 1;
        }

        /// <summary>
        /// Explorer tree: Node collapse.
        /// </summary>
        private void _tvwDetails_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            if (_preventExpand)
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Menu: Help | About
        /// </summary>
        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmMsgBox.ShowInfo(this, "About " + UiControls.Title, UiControls.GetManText("copyright").Replace("{productname}", UiControls.Title).Replace("{version}", UiControls.VersionString));
        }

        /// <summary>
        /// Selection Toolstrip: Selection history click (main item)
        /// </summary>
        private void _btnBack_ButtonClick(object sender, EventArgs e)
        {
            if (_viewHistory.Count == 0)
            {
                return;
            }

            Activate(_viewHistory[0], EActivateOrigin.None);
        }

        /// <summary>
        /// Selection Toolstrip: Selection history open sub items
        /// </summary>
        private void _btnBack_DropDownOpening(object sender, EventArgs e)
        {
            _btnBack.DropDownItems.Clear();

            foreach (IVisualisable o in _viewHistory)
            {
                ToolStripButton historyButton = new ToolStripButton(o.DisplayName, o.DisplayIcon);
                historyButton.Click += historyButton_Click;
                historyButton.Tag = o;
                _btnBack.DropDownItems.Add(historyButton);
            }
        }

        /// <summary>
        /// Selection Toolstrip: Selection history click (sub item)
        /// </summary>
        void historyButton_Click(object sender, EventArgs e)
        {
            var control = (ToolStripButton)sender;
            IVisualisable tag = (IVisualisable)control.Tag;
            Activate(tag, EActivateOrigin.None);
        }

        /// <summary>
        /// Menu: Reset do not show again
        /// </summary>
        private void resetdoNotShowAgainItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainSettings.Instance.DoNotShowAgain.Clear();
            MainSettings.Instance.Save();

            FrmMsgBox.ShowCompleted(this, "Show Messages", "\"Do not show again\" items have been reset. You may need to restart the program to see changes.");
        }

        /// <summary>
        /// Session button --> Show session menu
        /// </summary>
        private void _btnSession_Click(object sender, EventArgs e)
        {
            _cmsCoreButton.ShowDropDown(_btnSession);
        }

        /// <summary>
        /// Selection button --> Show selection menu
        /// </summary>
        private void _btnCurrentSel_Click(object sender, EventArgs e)
        {
            _selectionMenuOpenedFromList = _selectedObject;
            _cmsSelectionButton.ShowDropDown(_btnSel);
        }

        /// <summary>
        /// Session menu: Add comments
        /// </summary>
        private void editNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSession.Show(this, _core);
            UpdateVisualOptions();
        }

        /// <summary>
        /// Selection menu: Add comments
        /// </summary>
        private void addCommentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectionMenuOpenedFromList == null)
            {
                return;
            }

            switch (_selectionMenuOpenedFromList.VisualClass)
            {
                case VisualClass.Cluster:
                    Cluster p = (Cluster)_selectionMenuOpenedFromList;
                    FrmEditCluster.Show(this, _core, p);
                    _clusterList.Update(p);
                    break;

                default:
                    var s = _selectionMenuOpenedFromList;
                    string newComment = FrmInput.Show(this, Text, s.DisplayName, "Enter a comment for the " + s.VisualClass.ToUiString().ToLower(), s.Comment);

                    if (newComment != null)
                    {
                        s.Comment = newComment;

                        // TODO: Lazy - what's actually changed?
                        // Probably doesn't matter these are all fast refreshes
                        UpdateAll("Comment changed", EListInvalids.ValuesChanged, EListInvalids.ValuesChanged, EListInvalids.ValuesChanged, EListInvalids.ValuesChanged);
                    }
                    break;
            }
        }

        /// <summary>
        /// Selection menu: View online
        /// </summary>
        private void viewOnlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectionMenuOpenedFromList != null)
            {
                switch (_selectionMenuOpenedFromList.VisualClass)
                {
                    case VisualClass.Compound:
                        Compound c = (Compound)_selectionMenuOpenedFromList;
                        Process.Start(c.Url);
                        break;

                    case VisualClass.Pathway:
                        Pathway p = (Pathway)_selectionMenuOpenedFromList;
                        Process.Start(p.Url);
                        break;

                    default:
                        FrmMsgBox.ShowInfo(this, "View Online", "Only pathways and compounds taken from the online database can be viewed online.");
                        break;
                }
            }
        }

        /// <summary>
        /// Handles opening of the SELECTION button menu
        /// </summary>
        private void _cmsSelectionButton_Opening(object sender, CancelEventArgs e)
        {
            var t = _selectionMenuOpenedFromList;

            openToolStripMenuItem.Enabled = t != null;

            if (t != null)
            {
                openToolStripMenuItem.Text = "&Open " + t.DisplayName;
            }
            else
            {
                openToolStripMenuItem.Text = "&Open";
            }

            addCommentsToolStripMenuItem.Enabled = t != null;
            viewOnlineToolStripMenuItem.Visible = t != null && (t is Compound || t is Pathway);
            breakUpLargeClusterToolStripMenuItem.Visible = t is Cluster;
            compareToThisPeakToolStripMenuItem.Visible = t is Peak;
        }

        /// <summary>
        /// Menu: Export data in R format
        /// </summary>
        private void dataInRFormatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fn = this.BrowseForFile(null, UiControls.EFileExtension.RData, FileDialogMode.SaveAs, UiControls.EInitialFolder.ExportedData);

            if (fn != null)
            {
                Arr.Instance.Export(_core, fn);
            }
        }

        /// <summary>
        /// Menu: Debug
        /// </summary>
        private void experimentalOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmDebug.Show(this, _core))
            {
                UpdateAll("Potential changes", EListInvalids.SourceChanged, EListInvalids.SourceChanged, EListInvalids.SourceChanged, EListInvalids.SourceChanged);
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

            return frm._selectedObject as Peak ?? frm._peakList.Selection ?? frm._varChart.SelectedPeak ?? frm._core.Peaks[0];
        }

        /// <summary>
        /// Like SearchForSelectedPeak() but sets the new peak.
        /// </summary>
        internal static void SearchForAndSetSelectedPeak(Form current, Peak newSelection)
        {
            FrmMain frm = GetFrmMain(current);

            if (frm == null)
            {
                return;
            }

            frm.Activate(newSelection, EActivateOrigin.External);
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
            if (FrmBigList.ShowAlgorithms(this, _core, FrmBigList.EAlgorithmType.Trend, null))
            {
                UpdateAll("Trends changed", EListInvalids.ValuesChanged, EListInvalids.ContentsChanged, EListInvalids.ContentsChanged, EListInvalids.None);
            }
        }

        /// <summary>
        /// Menu: Create clusters
        /// </summary>
        private void createclustersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmBigList.ShowAlgorithms(this, _core, FrmBigList.EAlgorithmType.Clusters, null))
            {
                UpdateAll("Clusters changed", EListInvalids.ValuesChanged, EListInvalids.ContentsChanged, EListInvalids.ContentsChanged, EListInvalids.None);
            }
        }

        /// <summary>
        /// Menu: Edit corrections
        /// </summary>
        private void editCorrectionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmBigList.ShowAlgorithms(this, _core, FrmBigList.EAlgorithmType.Corrections, null))
            {
                UpdateAll("Statistics changed", EListInvalids.ValuesChanged, EListInvalids.ContentsChanged, EListInvalids.ContentsChanged, EListInvalids.None);
            }
        }

        /// <summary>
        /// Menu: Edit statictics
        /// </summary>
        private void editStatisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmBigList.ShowAlgorithms(this, _core, FrmBigList.EAlgorithmType.Statistics, null))
            {
                UpdateAll("Statistics changed", EListInvalids.SourceChanged, EListInvalids.ContentsChanged, EListInvalids.ContentsChanged, EListInvalids.None);
            }
        }

        /// <summary>
        /// Menu: Edit experimental groups
        /// </summary>
        private void experimentalGroupsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool changes = false;

        repeat:
            var group = ListValueSet.ForGroups(_core).IncludeMessage("View or modify experimental groups").ShowList(this);

            if (group != null)
            {
                FrmEditExpGroup.Show(this, group);
                UpdateVisualOptions();
                changes = true;
                goto repeat;
            }

            if (changes)
            {
                UpdateAll("Experimental groups changed", EListInvalids.ValuesChanged, EListInvalids.ValuesChanged, EListInvalids.ValuesChanged, EListInvalids.None);
            }
        }

        /// <summary>
        /// Menu: PCA
        /// </summary>
        private void pCAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPca.Show(this, _core);
        }

        /// <summary>
        /// Menu: Open selection
        /// </summary>
        private void openToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Activate(_selectionMenuOpenedFromList, EActivateOrigin.None);
        }

        /// <summary>
        /// Menu: Open selection in data explorer
        /// </summary>
        private void openInDataexplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTreeView.Show(this, _core, _selectionMenuOpenedFromList);
        }

        /// <summary>
        /// Menu: Break up selection
        /// </summary>
        private void breakUpLargeClusterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmBigList.ShowAlgorithms(this, _core, FrmBigList.EAlgorithmType.Clusters, _selectionMenuOpenedFromList))
            {
                UpdateAll("Clusters changed", EListInvalids.ValuesChanged, EListInvalids.ContentsChanged, EListInvalids.ContentsChanged, EListInvalids.None);
            }
        }

        /// <summary>
        /// Menu: Compare to selection
        /// </summary>
        private void compareToThisPeakToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmBigList.ShowAlgorithms(this, _core, FrmBigList.EAlgorithmType.Statistics, _selectionMenuOpenedFromList);
        }

        /// <summary>
        /// Menu: Change peak filters
        /// </summary>
        private void peakFiltersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmBigList.ShowPeakFilters(this, _core);
        }

        /// <summary>
        /// Menu: Change obs. filters
        /// </summary>
        private void observationFiltersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmBigList.ShowObsFilters(this, _core);
        }

        /// <summary>
        /// Menu: Clusterer optimiser
        /// </summary>
        private void clustererOptimiserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmEvaluateClustering.Show(this, _core, null);
        }

        /// <summary>
        /// Menu: Cluster optimiser
        /// </summary>
        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            clustererOptimiserToolStripMenuItem.PerformClick();
        }

        private void peakidentificationsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
