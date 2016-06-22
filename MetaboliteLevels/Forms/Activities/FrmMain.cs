﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
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
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Viewers.Charts;
using MetaboliteLevels.Viewers.Lists;
using System.Drawing.Imaging;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Algorithms.Statistics.Arguments;
using MGui.Helpers;
using MGui.Controls;
using System.Reflection;
using MetaboliteLevels.Forms.Activities;

namespace MetaboliteLevels.Forms
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
        private readonly List<VisualisableSelection> _viewHistory = new List<VisualisableSelection>();
        private readonly List<ICoreWatcher> _coreWatchers = new List<ICoreWatcher>();
        private bool _autoChangingSelection;
        private string _printTitle;
        private int _waitCounter;

        // Selection
        VisualisableSelection _selection;
        private IVisualisable _selectionMenuOpenedFromList;
        private ListViewHelper<Assignment> _assignmentList;
        private readonly ListViewHelper<Data.Visualisables.Annotation> _annotationList2;
        private readonly ListViewHelper<Data.Visualisables.Annotation> _annotationList;

        public VisualisableSelection Selection
        {
            get
            {
                return _selection ?? new VisualisableSelection(null);
            }
            set
            {
                CommitSelection(value);
            }
        }


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
            _core = FrmEditDataFileNames.Show(this);

            if (_core == null)
            {
                _ignoreConfirmClose = true;
                this.BeginInvoke((MethodInvoker)this.Close);
                return;
            }

            // Load image list
            UiControls.PopulateImageList(_imgList);

            // Main menu colours
            _mnuMain.BackColor = UiControls.TitleBackColour;
            toolStrip1.BackColor = UiControls.TitleBackColour;

            foreach (ToolStripItem tsi in _mnuMain.Items.Cast<ToolStripItem>().Concat( toolStrip1.Items.Cast<ToolStripItem>() ))
            {
                tsi.Image = UiControls.RecolourImage( tsi.Image );
                tsi.ForeColor = UiControls.TitleForeColour;
                tsi.Text = tsi.Text.ToUpper();
            } 

            // Charts
            _chartPeak = new ChartHelperForPeaks(this, _core, splitContainer3.Panel1);
            _chartCluster = new ChartHelperForClusters(this, _core, splitContainer3.Panel2);
            _chartPeakForPrinting = new ChartHelperForPeaks(null, _core, null);
            _chartClusterForPrinting = new ChartHelperForClusters(null, _core, null);

            _chartCluster.SelectionChanged += _chartCluster_SelectionChanged;

            _coreWatchers.Add(_chartPeak);
            _coreWatchers.Add(_chartCluster);
            _coreWatchers.Add(_chartPeakForPrinting);
            _coreWatchers.Add(_chartClusterForPrinting);

            // Primary lists
            _peakList = CreatePrimaryList<Peak>(_lstVariables, z => z.Peaks);
            _clusterList = CreatePrimaryList<Cluster>(_lstClusters, z => z.Clusters);
            _adductList = CreatePrimaryList<Adduct>(_lstAdducts, z => z.Adducts);
            _compoundList = CreatePrimaryList<Compound>(_lstCompounds, z => z.Compounds);
            _pathwayList = CreatePrimaryList<Pathway>(_lstPathways, z => z.Pathways);
            _assignmentList = CreatePrimaryList<Assignment>(_lstAssignments, z => z.Assignments);
            _annotationList = CreatePrimaryList<Annotation>(_lstAnnotations, z => z.Annotations);

            // Secondary lists
            _peakList2 = CreateSecondaryList<Peak>(_lst2Peaks);
            _clusterList2 = CreateSecondaryList<Cluster>(_lst2Clusters);
            _adductList2 = CreateSecondaryList<Adduct>(_lst2Adducts);
            _compoundList2 = CreateSecondaryList<Compound>(_lst2Compounds);
            _pathwayList2 = CreateSecondaryList<Pathway>(_lst2Pathways);
            _assignmentList2 = CreateSecondaryList<Assignment>(_lst2Assignments);
            _annotationList2 = CreateSecondaryList<Annotation>(_lstSubAnnots);

            // Pagers
            _pgrMain = new PagerControl(ref tabControl1);
            _pgrMain.BindToButtons(_btnMain0, _btnMain1, _btnMain2, _btnMain3, _btnMain4, _btnMain5, _btnMainAnnots);


            _pgrSub = new PagerControl(ref _tabSubinfo);
            _pgrSub.BindToButtons(_btnSubInfo, _btnSubStat, _btnSubPeak, _btnSubPat, _btnSubComp, _btnSubAdd, _btnSubPath, _btnSubAss, _btnSubAnnot);

            _subDisplay = new SubListHandler(this, _core, _lst2Info, _lst2Stats);
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

            Dictionary<string, ToolStripMenuItem> dict = new Dictionary<string, ToolStripMenuItem>();

            // Database menu
            foreach (EDataSet dm in Enum.GetValues( typeof( EDataSet ) ))
            {
                string[] text = dm.ToUiString().Split('\\');

                ToolStripMenuItem tsi = dict.GetOrCreate( text[0], z => (ToolStripMenuItem)databaseToolStripMenuItem.DropDownItems.Add( z ) );

                ToolStripMenuItem tsmi = new ToolStripMenuItem( text[1], Resources.MnuViewList, dataManagerMenuItem );
                tsmi.Tag = dm;

                tsi.DropDownItems.Add( tsmi );
            }
        }

        /// <summary>
        /// Handles menu click.
        /// </summary>aram>
        private void dataManagerMenuItem( object sender, EventArgs e )
        {
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
            EDataSet tag = (EDataSet)tsmi.Tag;
            ShowEditor( tag );
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
        Image IPreviewProvider.ProvidePreview(Size s, object utTarget, object utHighlight)
        {
            IAssociational target = utTarget as IAssociational;
            IAssociational highlight = utHighlight as IAssociational;

            if (target == null)
            {
                return null;
            }

            bool small = true;

            BeginWait("Creating preview for " + target.DisplayName);
            Bitmap result;

            try
            {
                switch (target.VisualClass)
                {
                    case VisualClass.Adduct:
                        {
                            result = null;
                        }
                        break;

                    case VisualClass.Compound:
                        {
                            StylisedCluster p = ((Compound)target).CreateStylisedCluster(_core, highlight);
                            p.IsPreview = small;
                            result = _chartClusterForPrinting.CreateBitmap(s.Width, s.Height, p);
                        }
                        break;

                    case VisualClass.Pathway:
                        {
                            StylisedCluster p = ((Pathway)target).CreateStylisedCluster(_core, highlight);
                            p.IsPreview = small;
                            result = _chartClusterForPrinting.CreateBitmap(s.Width, s.Height, p);
                        }
                        break;

                    case VisualClass.Cluster:
                        {
                            StylisedCluster p = ((Cluster)target).CreateStylisedCluster(_core, highlight);
                            p.IsPreview = small;
                            result = _chartClusterForPrinting.CreateBitmap(s.Width, s.Height, p);
                        }
                        break;

                    case VisualClass.Peak:
                        {
                            StylisedPeak p = new StylisedPeak((Peak)target);
                            p.IsPreview = small;
                            result = _chartPeakForPrinting.CreateBitmap(s.Width, s.Height, p);
                        }
                        break;

                    case VisualClass.Assignment:
                        {
                            StylisedCluster p = ((Assignment)target).CreateStylisedCluster(_core, highlight);
                            p.IsPreview = small;
                            result = _chartClusterForPrinting.CreateBitmap(s.Width, s.Height, p);
                        }
                        break;

                    default:
                        {
                            throw new InvalidOperationException("Invalid switch: " + target.VisualClass);
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
        private void primaryList_Activate(object sender, ListViewItemEventArgs e)
        {
            CommitSelection(new VisualisableSelection(e.Selection));
        }


        /// <summary>
        /// Item selected from secondary list.
        /// </summary>
        private void secondaryList_Activate(object sender, ListViewItemEventArgs e)
        {
            CommitSelection(new VisualisableSelection(Selection.Primary, e.Selection));
        }

        /// <summary>
        /// Handle changing of "_core".
        /// </summary>
        private void HandleCoreChange()
        {
            // Update stuff
            _coreWatchers.ForEach(z => z.ChangeCore(_core));

            // Clear selection
            CommitSelection(new VisualisableSelection(null));

            // Set new options
            _txtGuid.Text = _core.CoreGuid.ToString();
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
                if (Debugger.IsAttached)
                {
                    if (FrmMsgBox.Show(this, "Error", null, "There was a problem initialising R (you have a debugger attached so you can ignore this - not recommended).\r\n\r\n" + ex.Message, Resources.MsgError, new[] { new FrmMsgBox.ButtonSet("Configure", Resources.MnuEdit, DialogResult.Cancel), new FrmMsgBox.ButtonSet("Ignore", Resources.MnuWarning, DialogResult.Ignore) }) == DialogResult.Ignore)
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
            if (!_autoChangingSelection)
            {
                CommitSelection(new VisualisableSelection(this._chartCluster.SelectedCluster?.ActualElement, e.peak));
            }
        }


        /// <summary>
        /// Selects something.
        /// </summary>
        internal void CommitSelection(VisualisableSelection selection)
        {
            try
            {
                BeginWait("Updating displays");

                // Add to history
                AddToHistory(selection);

                // Set the selected object
                _selection = selection;

                // Update the lists
                _subDisplay.Activate(selection.Primary as IAssociational);

                // Icons
                if (selection.Primary != null)
                {
                    _btnSelection.Text = selection.Primary.DisplayName;
                    _btnSelection.Image = UiControls.GetImage(selection.Primary.GetIcon(), true);
                    _btnSelection.Visible = true;
                }
                else
                {
                    _btnSelection.Visible = false;
                }

                if (selection.Secondary != null)
                {
                    _btnSelectionExterior.Text = selection.Secondary.DisplayName;
                    _btnSelectionExterior.Image = UiControls.GetImage(selection.Secondary.GetIcon(), true);
                    _btnSelectionExterior.Visible = true;
                    _btnExterior.Visible = true;
                }
                else
                {
                    _btnSelectionExterior.Visible = false;
                    _btnExterior.Visible = false;
                }

                // Null selection?
                if (selection.Primary == null)
                {
                    PlotPeak(null);
                    PlotCluster(null);
                    return;
                }

                // For assignments plot the peak (and implicitly the cluster)
                //if (selection.Primary.VisualClass == VisualClass.Assignment)
                //{
                //    selection = new VisualisableSelection(((Assignment)selection.Primary).Peak, selection.Secondary);
                //}

                // Get the selection
                Peak peak = selection.Primary as Peak ?? selection.Secondary as Peak;
                IAssociational cluster = (((!(selection.Primary is Peak)) ? selection.Primary : selection.Secondary) ?? peak.Assignments.Clusters.FirstOrDefault()) as IAssociational;
                StylisedCluster sCluster;

                // Plot that!
                if (cluster == null)
                {
                    sCluster = null;
                }
                else
                {
                    switch (cluster.VisualClass)
                    {
                        case VisualClass.Assignment:
                            sCluster = ((Assignment)cluster).CreateStylisedCluster(_core, selection.Secondary as IAssociational);
                            break;

                        case VisualClass.Cluster:
                            sCluster = ((Cluster)cluster).CreateStylisedCluster(_core, selection.Secondary as IAssociational );
                            break;

                        case VisualClass.Compound:
                            sCluster = ((Compound)cluster).CreateStylisedCluster(_core, selection.Secondary as IAssociational );
                            break;

                        case VisualClass.Pathway:
                            sCluster = ((Pathway)cluster).CreateStylisedCluster(_core, selection.Secondary as IAssociational );
                            break;

                        default:
                            sCluster = null;
                            break;
                    }
                }

                // Plot stuffs
                PlotPeak(peak);
                PlotCluster(sCluster);

                if (sCluster != null)
                {
                    // Make sure the current peak is selected in that cluster
                    _autoChangingSelection = true;
                    _chartCluster.SelectSeries(peak);
                    _autoChangingSelection = false;
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
        private void AddToHistory(VisualisableSelection selection)
        {
            if (selection != null)
            {
                _viewHistory.Remove(selection);
                _viewHistory.Insert(0, selection);

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

            _chartCluster.Plot(p);

            _autoChangingSelection = false;
            EndWait();
        }

        /// <summary>
        /// Hides the please wait bar
        /// </summary>
        private void EndWait()
        {
            _waitCounter--;
            UiControls.Assert(_waitCounter >= 0, "EndWait called when not waiting.");

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
        /// Plots the specified peak.
        /// </summary>
        private void PlotPeak(Peak peak)
        {
            BeginWait("Plotting peak");

            // Select it in the peaks list
            _autoChangingSelection = true;

            // The peak might be hidden due to a filter!
            if (_peakList.GetVisible().Contains(peak)) // Todo - this is show, there should be a tryselect
            {
                _peakList.Selection = peak; // make sure it is selected
            }

            StylisedPeak sp = new StylisedPeak(peak);
            _chartPeak.Plot(sp);

            _autoChangingSelection = false;

            EndWait();
        }

        /// <summary>
        /// Menu: Load
        /// </summary>
        private void loadDataSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Core newCore = FrmEditDataFileNames.Show(this);

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
            _btnSession.Text = _core.FileNames.GetShortTitle();
            Text = UiControls.Title + " - " + _btnSession.Text;

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

            foreach (GroupInfo ti in _core.Groups.WhereEnabled().OrderBy(z => z.DisplayPriority))
            {
                bool e = _core.Options.ViewTypes.Contains(ti);

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
                    ForeColor = ti.Colour
                };

                tsmi.Click += experimentTypeMenuItem_Click;
                tsmi2.Click += experimentTypeMenuItem_Click;
                tsmi2.MouseDown += experimentTypeMenuItem_MouseDown;

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

            if (core.Options.ViewTypes.Contains( type ))
            {
                core.Options.ViewTypes.Remove( type );
            }
            else
            {
                core.Options.ViewTypes.Add( type );
                core.Options.ViewTypes.Sort( ( x, y ) => x.DisplayPriority.CompareTo( y.DisplayPriority ) );
            }

            RegenerateExperimentalGroupIcon( type );
        }

        private void RegenerateExperimentalGroupIcon( GroupInfo type )
        {
            bool @checked = _core.Options.ViewTypes.Contains( type );
            ToolStripButton tsb = toolStrip1.Items.OfType<ToolStripButton>().First( z => z.Tag == type );
            ToolStripMenuItem tsmi = viewToolStripMenuItem.DropDownItems.OfType<ToolStripMenuItem>().First( z => z.Tag == type );
            tsb.Image.Dispose();
            tsb.Image = UiControls.CreateExperimentalGroupImage( @checked, type, true );
            tsmi.Image.Dispose();
            tsmi.Image = UiControls.CreateExperimentalGroupImage( @checked, type, false );
            Replot();
        }

        private void experimentTypeMenuItem_MouseDown( object sender, MouseEventArgs e )
        {
            ToolStripItem senderr = (ToolStripItem)sender;
            GroupInfo type = (GroupInfo)senderr.Tag;

            if (e.Button == MouseButtons.Right)
            {
                if (FrmEditGroupBase.Show( this, type, false ))
                {
                    RegenerateExperimentalGroupIcon( type );
                }
            }
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
                FrmWait.Show(this, "Saving session", null, z => _core.Save(fileName, z));

                success = true;
            }
            catch (Exception ex)
            {
                FrmMsgBox.ShowError(this, ex);
                success = false;
            }

            EndWait();

            // Remember recent files
            MainSettings.Instance.AddRecentSession(_core);
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
            CommitSelection(new VisualisableSelection(Selection.Primary, Selection.Secondary));
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

            string pt = FrmInputMultiLine.Show(this, "Printing", "Print", "Enter or change the title for the print", _printTitle);

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
                using (Bitmap bmp = GenerateClusterBitmap(x, y))
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

                    _chartClusterForPrinting.Plot(sp);

                    Rectangle r = new Rectangle(ofx + x * xi, ofy + TITLE_MARGIN + y * yi, xi, yi - yim);

                    if (isPrinter)
                    {
                        UiControls.Assert(g.PageUnit == GraphicsUnit.Display, "pageunit should be Display");

                        g.PageUnit = GraphicsUnit.Pixel;
                        Rectangle rP = new Rectangle(r.X * (int)(g.DpiX / 100f),
                                                    r.Y * (int)(g.DpiY / 100f),
                                                    r.Width * (int)(g.DpiX / 100f),
                                                    r.Height * (int)(g.DpiY / 100f));

                        using (Bitmap bmp = _chartClusterForPrinting.Chart.DrawToBitmap(r.Width, r.Height))
                        {
                            g.DrawImage(bmp, rP);
                        }

                        g.PageUnit = GraphicsUnit.Display;
                    }
                    else
                    {
                        using (Bitmap bmp = _chartClusterForPrinting.Chart.DrawToBitmap(r.Width, r.Height))
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
           
        private void exportClustersToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // RM
        }

        /// <summary>
        /// Peak list: Keypress --> Set/clear comment flags
        /// </summary>
        private void _lstVariables_KeyDown(object sender, KeyEventArgs e)
        {
            if (_core.Options.EnablePeakFlagging)
            {
                foreach (var f in _core.Options.PeakFlags)
                {
                    if (f.Key == (char)e.KeyCode)
                    {
                        NativeMethods.Beep(f.BeepFrequency, f.BeepDuration);

                        if (e.Control)
                        {
                            bool add = !_peakList.Selection.CommentFlags.Contains(f);

                            if (FrmMsgBox.ShowOkCancel(this, f.ToString(), (add ? "Apply this flag to" : "Remove this flag from") + " all peaks shown in list?"))
                            {
                                foreach (Peak peak in _peakList.GetVisible())
                                {
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

                                _peakList.Rebuild(EListInvalids.ValuesChanged);
                            }
                        }
                        else
                        {
                            _peakList.Selection.ToggleCommentFlag(f);
                            _peakList.Update(_peakList.Selection);
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

            if (!_ignoreConfirmClose)
            {
                switch (FrmSelectClosure.Show(this, _core))
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
            FrmEditCoreOptions.Show(this, _core, false);
            UpdateVisualOptions();
            UpdateAll("Changed options", EListInvalids.SourceChanged, EListInvalids.SourceChanged, EListInvalids.SourceChanged, EListInvalids.SourceChanged);
            Replot();
        }

        /// <summary>
        /// Menu: Cluster wizard
        /// </summary>
        private void autogenerateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmWizConfigurationCluster.Show(this, _core))
            {
                UpdateAll("Autogenerated clusters", EListInvalids.ValuesChanged, EListInvalids.ContentsChanged, EListInvalids.ContentsChanged, EListInvalids.None);
            }
        }

        /// <summary>
        /// Menu: Session information
        /// </summary>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UiControls.ShowSessionInfo(this, _core.FileNames);
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
            if (_viewHistory.Count == 0)
            {
                return;
            }

            CommitSelection(new VisualisableSelection(_viewHistory[0].Primary, _viewHistory[0].Secondary));
        }

        /// <summary>
        /// Selection Toolstrip: Selection history open sub items
        /// </summary>
        private void _btnBack_DropDownOpening(object sender, EventArgs e)
        {
            _btnBack.DropDownItems.Clear();

            foreach (VisualisableSelection o in _viewHistory)
            {
                Image image = o.Primary != null ? UiControls.GetImage(o.Primary.GetIcon(), true) : Resources.IconTransparent;
                ToolStripButton historyButton = new ToolStripButton(o.ToString(), image);
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
            VisualisableSelection tag = (VisualisableSelection)control.Tag;
            CommitSelection(tag);
        }

        /// <summary>
        /// Menu: Reset do not show again
        /// </summary>
        // TODO: Why is this unused?
        private void resetdoNotShowAgainItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainSettings.Instance.DoNotShowAgain.Clear();
            MainSettings.Instance.Save();

            FrmMsgBox.ShowCompleted(this, "Show Messages", "\"Do not show again\" items have been reset. You may need to restart the program to see changes.");
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

            FrmEditINameable.Show(this, _selectionMenuOpenedFromList, false);

            // TODO: Lazy - what's actually changed?
            // Probably doesn't matter these are all fast refreshes
            UpdateAll("Comment changed", EListInvalids.ValuesChanged, EListInvalids.ValuesChanged, EListInvalids.ValuesChanged, EListInvalids.ValuesChanged);
        }

        /// <summary>
        /// Selection menu: View online
        /// </summary>
        private void viewOnlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string url;

            if (_selectionMenuOpenedFromList is Compound)
            {
                Compound c = (Compound)_selectionMenuOpenedFromList;
                url = c.Url;
            }
            else if (_selectionMenuOpenedFromList is Pathway)
            {
                Pathway p = (Pathway)_selectionMenuOpenedFromList;
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
            PopulateMenu(_cmsSelectionButton.Items);
        }

        private void PopulateMenu(ToolStripItemCollection items)
        {
            var t = _selectionMenuOpenedFromList;

            items.Clear();

            if (t != null)
            {
                items.Add("&Navigate to " + t.DisplayName, null, openToolStripMenuItem_Click_1);
                items.Add("&Edit name and comments", null, addCommentsToolStripMenuItem_Click);
            }
            else
            {
                items.Add("&Navigate to ...", null, null).Enabled = false;
                return;
            }

            if (t is Compound || t is Pathway)
            {
                items.Add("&View online", null, viewOnlineToolStripMenuItem_Click);
            }

            if (t is Cluster)
            {
                items.Add("&Break up large cluster...", null, breakUpLargeClusterToolStripMenuItem_Click);
            }

            if (t is Peak)
            {
                items.Add("&Compare to this peak...", null, compareToThisPeakToolStripMenuItem_Click);
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

            return frm.Selection.Primary as Peak ?? frm._peakList.Selection ?? frm._chartPeak.SelectedPeak ?? frm._core.Peaks[0];
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
            ShowEditor( EDataSet.Trends);
        }

        /// <summary>
        /// Menu: Create clusters
        /// </summary>
        private void createclustersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowEditor( EDataSet.Clusterers);
        }

        /// <summary>
        /// Menu: Edit corrections
        /// </summary>
        private void editCorrectionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowEditor( EDataSet.Corrections);
        }

        /// <summary>
        /// Menu: Edit statictics
        /// </summary>
        private void editStatisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowEditor( EDataSet.Statistics);
        }

        /// <summary>
        /// Menu: Edit experimental groups
        /// </summary>
        private void experimentalGroupsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowEditor( EDataSet.Groups);
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
            CommitSelection(new VisualisableSelection(_selectionMenuOpenedFromList));
        }

        /// <summary>
        /// Menu: Break up selection
        /// </summary>
        private void breakUpLargeClusterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cluster clu = (Cluster)_selectionMenuOpenedFromList;
            PeakFilter filter = null;

            foreach (PeakFilter pf in _core.AllPeakFilters)
            {
                if (pf.Conditions.Count == 1)
                {
                    PeakFilter.ConditionCluster x = pf.Conditions[0] as PeakFilter.ConditionCluster;

                    if (x != null)
                    {
                        if (x.Clusters.Count == 1 && x.Clusters[0].GetTarget() == clu && x.ClustersOp == Filter.ESetOperator.AnyXinY && x.CombiningOperator == Filter.ELogicOperator.And && x.Negate == false)
                        {
                            filter = pf;
                            break;
                        }
                    }
                }
            }

            if (filter == null)
            {
                PeakFilter.Condition condition = new PeakFilter.ConditionCluster(Filter.ELogicOperator.And, false, Filter.ESetOperator.AnyXinY, new[] { clu });
                filter = new PeakFilter(null, null, new[] { condition });
                _core.AddPeakFilter(filter);
            }

            ConfigurationClusterer template = new ConfigurationClusterer(null, null, null, new ArgsClusterer(filter, null, EAlgoSourceMode.None, null, false, EClustererStatistics.None, null));

            if (DataSet.ForClusterers(_core).ShowListEditor(this, FrmBigList.EShow.Default, template) != null)
            {
                UpdateAll("Clusters changed", EListInvalids.ValuesChanged, EListInvalids.ContentsChanged, EListInvalids.ContentsChanged, EListInvalids.None);
            }
        }

        /// <summary>
        /// Menu: Compare to selection
        /// </summary>
        private void compareToThisPeakToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurationStatistic template = new ConfigurationStatistic(null, null, null, new ArgsStatistic(EAlgoSourceMode.None, null, EAlgoInputBSource.AltPeak, null, (Peak)_selectionMenuOpenedFromList, null));

            DataSet.ForStatistics(_core).ShowListEditor(this, FrmBigList.EShow.Default, template);
        }

        /// <summary>
        /// Menu: Change peak filters
        /// </summary>
        private void peakFiltersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowEditor( EDataSet.PeakFilters);
        }

        /// <summary>
        /// Menu: Change obs. filters
        /// </summary>
        private void observationFiltersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowEditor( EDataSet.ObservationFilters);
        }

        /// <summary>
        /// Menu: Clusterer optimiser
        /// </summary>
        private void clustererOptimiserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmActEvaluate.Show(this, _core, null);
        }

        /// <summary>
        /// Menu: Selection button
        /// </summary>            
        private void _btnSelection_DropDownOpening(object sender, EventArgs e)
        {
            _selectionMenuOpenedFromList = Selection.Primary;
            PopulateMenu(_btnSelection.DropDownItems);
        }

        /// <summary>
        /// Menu: Exterior selection button
        /// </summary>            
        private void _btnSelectionExterior_DropDownOpening(object sender, EventArgs e)
        {
            _selectionMenuOpenedFromList = Selection.Secondary;
            PopulateMenu(_btnSelectionExterior.DropDownItems);
        }                              

        private void ShowEditor( EDataSet dataSetId)
        {
            IDataSet dataSet = DataSet.For( dataSetId, _core );

            if (dataSet.ShowListEditor( this ))
            {
                switch (dataSetId)
                {
                    case EDataSet.Groups:
                        UpdateVisualOptions();
                        UpdateAll( "Experimental groups changed",
                            peak:           EListInvalids.ValuesChanged, 
                            cluster:        EListInvalids.ValuesChanged,
                            assignments:    EListInvalids.ValuesChanged,
                            compounds:      EListInvalids.None );
                        break;

                    case EDataSet.Statistics:
                        UpdateAll( "Statistics changed", 
                            peak:           EListInvalids.SourceChanged, 
                            cluster:        EListInvalids.ContentsChanged, 
                            assignments:    EListInvalids.ContentsChanged,
                            compounds:      EListInvalids.None );
                        break;


                    case EDataSet.Clusterers:
                    case EDataSet.Trends:
                    case EDataSet.Corrections:
                        UpdateAll( "Database changes",
                            peak:           EListInvalids.ValuesChanged, 
                            cluster:        EListInvalids.ContentsChanged, 
                            assignments:    EListInvalids.ContentsChanged, 
                            compounds:      EListInvalids.None );
                        break;
                }
            }
        }

        private void sessionInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aboutToolStripMenuItem.PerformClick();
        }

        private void _btnExterior_Click(object sender, EventArgs e)
        {
            CommitSelection(new VisualisableSelection(_selection.Secondary, _selection.Primary));
        }

        private void dataToolStripMenuItem_Click( object sender, EventArgs e )
        {
            FrmActExport.Show( this, _core );
        }

        private void correlationMapToolStripMenuItem_Click( object sender, EventArgs e )
        {
            ValueMatrix vm = FrmWait.Show( this, "Creating value matrix", null,
                z => ValueMatrix.Create( _peakList.GetVisible().Cast<Peak>().ToArray(), false, _core, null, false, z ) );

            ArgsMetric args = new ArgsMetric(new object[0]  );
            ConfigurationMetric metric = new ConfigurationMetric( "Temporary", null, Algo.ID_METRIC_PEARSONDISTANCE, args );

            DistanceMatrix dm = FrmWait.Show( this, "Creating value matrix", null,
                z => DistanceMatrix.Create( _core, vm, metric, z ) );

            FrmActHeatMap.Show( _core, _peakList, dm );
        }
    }
}
