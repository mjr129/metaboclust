using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MCharting;
using MetaboliteLevels.Algorithms.Statistics;
using MetaboliteLevels.Algorithms.Statistics.Arguments;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Algorithms.Statistics.Results;
using MetaboliteLevels.Controls;
using MetaboliteLevels.Data.General;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Forms.Algorithms.ClusterEvaluation;
using MetaboliteLevels.Forms.Editing;
using MetaboliteLevels.Forms.Generic;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Settings;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Viewers.Charts;
using MetaboliteLevels.Viewers.Lists;
using MGui.Helpers;
using MSerialisers;
using MSerialisers.Serialisers;

namespace MetaboliteLevels.Forms.Algorithms
{
    /// <summary>
    /// Allows the user to evaluate clustering
    /// </summary>
    internal partial class FrmActEvaluate : Form
    {
        private readonly ListViewHelper<ClusterEvaluationParameterResult> _lvhConfigs;
        private readonly ListViewHelper<Cluster> _lvhClusters;
        private readonly ChartHelperForClusters _chcClusters;
        private readonly ListViewHelper<ColumnWrapper> _lvhStatistics;

        private readonly Core _core;
        private readonly ConfigurationClusterer _templateConfig;
        private ClusterEvaluationResults _selectedResults;

        /// <summary>
        /// Shows the form
        /// </summary>                        
        internal static void Show(Form owner, Core core, ConfigurationClusterer config)
        {
            using (FrmActEvaluate frm = new FrmActEvaluate(core, config))
            {
                UiControls.ShowWithDim(owner, frm);
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
        }

        public FrmActEvaluate()
        {
            InitializeComponent();

            UiControls.SetIcon(this);
            UiControls.PopulateImageList(imageList1);
        }

        internal FrmActEvaluate(Core core, ConfigurationClusterer config)
            : this()
        {
            this._core = core;
            this._templateConfig = config;

            _lvhClusters = new ListViewHelper<Cluster>(_lstClusters, core, null, null);
            _lvhConfigs = new ListViewHelper<ClusterEvaluationParameterResult>(_lstParams, core, null, null);
            _lvhStatistics = new ListViewHelper<ColumnWrapper>(_lstStatistics, core, null, null);
            _lvhStatistics.Visible = false;
            _chcClusters = new ChartHelperForClusters(null, core, panel6);

            _lvhConfigs.Activate += _lstParams_SelectedIndexChanged;
            _lvhClusters.Activate += _lstClusters_SelectedIndexChanged;
            _lvhStatistics.Activate += _lstStats_SelectedIndexChanged;

            // UiControls.CompensateForVisualStyles(this);
        }

        class ColumnWrapper : IVisualisable
        {
            public string OverrideDisplayName { get; set; }

            private readonly ClusterEvaluationResults Results;
            public readonly Column<ClusterEvaluationParameterResult> Column;

            public ColumnWrapper(ClusterEvaluationResults rs, Column<ClusterEvaluationParameterResult> col)
            {
                this.Results = rs;
                this.Column = col;
            }

            string INameable.DefaultDisplayName { get { return Column.Id; } }

            bool INameable.Enabled { get { return true; } set { } }

            public string DisplayName
            {
                get { return IVisualisableExtensions.FormatDisplayName(this); }
            }

            UiControls.ImageListOrder IVisualisable.GetIcon()
            {
                return UiControls.ImageListOrder.Statistic;
            }     

            public string Comment { get; set; }

            IEnumerable<Column> IVisualisable.GetColumns(Core core)
            {
                List<Column<ColumnWrapper>> cols = new List<Column<ColumnWrapper>>();

                cols.Add("Name", EColumn.Visible, z => z.DisplayName);

                foreach (ClusterEvaluationParameterResult v in Results.Results)
                {
                    var closure = v;
                    cols.Add(Results.Configuration.ParameterName + " = " + closure.DisplayName, EColumn.None, z => z.Column.GetRow(closure));
                }

                return cols;
            }    
        }

        private void SelectResults(string fileName, ClusterEvaluationResults config)
        {
            _selectedResults = config;

            _lvhClusters.Clear();
            _chartParameters.Clear();
            _tvStatistics.Nodes.Clear();
            _lvhConfigs.Clear();
            _chcClusters.ClearPlot();
            _labelCluster.Text = "Cluster";
            _lblPlot.Text = "Plot";

            if (config == null)
            {
                this.Text = "Evaluate Clustering";
                ctlTitleBar1.SubText = string.Empty;
                return;
            }

            ctlTitleBar1.SubText = config.ToString();
            this.Text = "Evaluate Clustering - " + fileName;
            _lvhConfigs.DivertList(config.Results);

            List<ColumnWrapper> cols2 = new List<ColumnWrapper>();

            ClusterEvaluationParameterResult def = config.Results.FirstOrDefault();

            if (def != null)
            {
                var cols = ColumnManager.GetColumns(_core, def);

                foreach (Column<ClusterEvaluationParameterResult> col in cols)
                {
                    if (col.Provider != null)
                    {
                        cols2.Add(new ColumnWrapper(config, col));
                    }
                }
            }

            _lstSel.Items.Clear();
            _lstSel.Items.Add("(All test repetitions)");

            for (int n = 0; n < config.Configuration.NumberOfRepeats; n++)
            {
                _lstSel.Items.Add("Test " + (n + 1));
            }

            _lvhStatistics.DivertList(cols2);
            PopulateTreeView(config, cols2);

            _infoLabel.Text = "Loaded results";
        }

        private void PopulateTreeView(ClusterEvaluationResults rs, List<ColumnWrapper> cols2)
        {
            var order = cols2.OrderBy(z => z.Column.Id.Count(zz => zz == '\\'));

            foreach (var v in order)
            {
                string[] elems = v.Column.Id.Split('\\');

                TreeNodeCollection col = _tvStatistics.Nodes;
                TreeNode node = null;

                foreach (string elem in elems)
                {
                    node = col.Find(elem, false).FirstOrDefault();

                    if (node == null)
                    {
                        node = new TreeNode(elem);
                        node.ImageIndex = (int)UiControls.ImageListOrder.Line;
                        node.SelectedImageIndex = node.ImageIndex;
                        node.Name = elem;
                        col.Add(node);
                    }

                    col = node.Nodes;
                }

                node.ImageIndex = (int)UiControls.ImageListOrder.Statistic;
                node.SelectedImageIndex = node.ImageIndex;
                node.Tag = v.Column;
            }
        }

        void BeginWait(TableLayoutPanel label)
        {
            label.BackColor = Color.Orange;
            label.Refresh();
        }

        void EndWait(TableLayoutPanel label)
        {
            label.BackColor = BackColor;
            label.Refresh();
        }

        private void _lstParams_SelectedIndexChanged(object sender, EventArgs e)
        {
            BeginWait(_tlpHeaderClusters);

            ClusterEvaluationParameterResult sel = _lvhConfigs.Selection;

            if (sel != null)
            {
                label2.Text = "Clusters (" + sel.Owner.ParameterName + " = " + sel.DisplayName + ")";

                if (_lstSel.SelectedIndex <= 0)
                {
                    List<Cluster> all = new List<Cluster>();
                    bool success = false;
                    int minCount = int.MaxValue;
                    int maxCount = int.MinValue;

                    foreach (var rep in sel.Repetitions)
                    {
                        if (rep.Clusters != null)
                        {
                            all.AddRange(rep.Clusters);
                            success = true;

                            minCount = Math.Min(rep.Clusters.Length, minCount);
                            maxCount = Math.Max(rep.Clusters.Length, maxCount);
                        }
                    }

                    _lvhClusters.Visible = false;
                    _lvhClusters.DivertList(all);
                    _lvhClusters.Visible = true;
                    panel2.Visible = success;

                    if (success)
                    {
                        _infoLabel.Text = "Between " + minCount + " and " + maxCount + " clusters were generated for each of the " + sel.Repetitions.Count + " tests when " + sel.Owner.ParameterName + " = " + sel.DisplayName;
                    }
                    else
                    {
                        _infoLabel.Text = "Clustering results for this test have been removed by the user and are not available to display";
                    }
                }
                else
                {
                    ResultClusterer rep = sel.Repetitions[_lstSel.SelectedIndex - 1];

                    _lstClusters.Visible = false;
                    _lvhClusters.DivertList(rep.Clusters);
                    _lstClusters.Visible = true;

                    _infoLabel.Text = "There are " + rep.Clusters.Length + " clusters when " + sel.Owner.ParameterName + " = " + sel.DisplayName + " for " + _lstSel.SelectedItem;
                }
            }
            else
            {
                label2.Text = "Clusters";
                _infoLabel.Text = "No parameter selected";
            }

            EndWait(_tlpHeaderClusters);
        }

        private void _lstClusters_SelectedIndexChanged(object sender, EventArgs e)
        {
            BeginWait(_tlpHeaderCluster);

            if (_lvhClusters.HasSelection)
            {
                var toPlot = _lvhClusters.Selection.CreateStylisedCluster(_core, null);
                ClusterEvaluationParameterResult sel = _lvhConfigs.Selection;

                _chcClusters.Plot(toPlot);
                _labelCluster.Text = "Cluster (" + sel.Owner.ParameterName + " = " + sel.DisplayName + " / cluster " + _lvhClusters.Selection.ShortName + ")";
                _infoLabel.Text = "Cluster " + _lvhClusters.Selection.ShortName + " contains " + _lvhClusters.Selection.Assignments.Count + " assignments";
            }
            else
            {
                _labelCluster.Text = "Cluster";
                _infoLabel.Text = "No cluster selected";
            }

            EndWait(_tlpHeaderCluster);
        }

        private void _lstStats_SelectedIndexChanged(object sender, EventArgs e)
        {
            BeginWait(_tlpHeaderPlot);

            if (_lvhStatistics.HasSelection)
            {
                Column<ClusterEvaluationParameterResult> col = _lvhStatistics.Selection.Column;

                CreatePlot(col);
            }
            else
            {
                _lblPlot.Text = "Plot";
                _infoLabel.Text = "No statistic selected";
            }

            EndWait(_tlpHeaderPlot);
        }

        private void CreatePlot(Column<ClusterEvaluationParameterResult> col)
        {
            MCharting.Plot plot = new MCharting.Plot();
            //plot.Style = new MChart.PlotStyle();
            //plot.Style.ShowZero = false;

            MCharting.Series series = new MCharting.Series();
            series.Style.DrawPoints = new SolidBrush(Color.Black);
            series.Style.DrawLines = new Pen(Color.Black);

            double minY = double.MaxValue;
            object minX = null;
            double maxY = double.MinValue;
            object maxX = null;

            foreach (ClusterEvaluationParameterResult res in _selectedResults.Results)
            {
                object yv = col.Provider(res);

                object value = _selectedResults.Configuration.ParameterValues[res.ValueIndex];
                double x = Convert.ToDouble(value);
                double y;

                if (yv is double)
                {
                    y = (double)yv;
                }
                else if (yv is int)
                {
                    y = (double)(int)yv;
                }
                else
                {
                    y = 0;
                }

                if (y < minY)
                {
                    minY = y;
                    minX = value;
                }

                if (y > maxY)
                {
                    maxY = y;
                    maxX = value;
                }

                series.Points.Add(new MCharting.DataPoint(x, y));
            }

            plot.Series.Add(series);

            _chartParameters.SetPlot(plot);

            string plotName = _selectedResults.Configuration.ParameterName + " against " + col.ToString();
            _lblPlot.Text = "Plot (" + plotName + ")";
            _infoLabel.Text = string.Format(
                "For the plot of {0} against {1} the minimum {1} is {2} at {0} = {3} and the maximum {1} is {4} at {0} = {5}.",
                _selectedResults.Configuration.ParameterName, col, minY.SignificantDigits(), minX, maxY.SignificantDigits(), maxX);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Control ctl = (Control)sender;

            string txt = toolTip1.GetToolTip(ctl);

            FrmMsgBox.ShowInfo(this, Text, txt);
        }

        private void _tvStatistics_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                BeginWait(_tlpHeaderPlot);

                Column<ClusterEvaluationParameterResult> col = (Column<ClusterEvaluationParameterResult>)e.Node.Tag;

                CreatePlot(col);

                EndWait(_tlpHeaderPlot);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (_tvStatistics.Visible)
            {
                button6.Text = "Tree view";
                _tvStatistics.Visible = false;
                _lvhStatistics.Visible = true;
            }
            else
            {
                button6.Text = "List view";
                _tvStatistics.Visible = true;
                _lvhStatistics.Visible = false;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.ShowDropDown(sender);
        }

        private void copyToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Bitmap bitmap = _chartParameters.DrawToBitmap())
            {
                Clipboard.SetImage(bitmap);
            }
        }

        private bool Paranoid
        {
            get
            {
                return paranoidModeToolStripMenuItem.Checked;
            }
        }

        /// <summary>
        /// Handles button: New test
        /// </summary>              
        private void _btnNewTest_Click(object sender, EventArgs e)
        {
            if (!Generic.DataSet.ForTests(this._core).ShowListEditor(this))
            {
                return;
            }

            bool loop2 = false;

            while (_core.EvaluationResultFiles.Any(z => !z.HasResults && z.Enabled))
            {
                var toRun = _core.EvaluationResultFiles.Where(z => !z.HasResults && z.Enabled).ToArray();

                if (toRun.Length == 0)
                {
                    return;
                }

                FrmMsgBox.ButtonSet[] buttons;
                string msg;

                if (loop2)
                {
                    buttons = new FrmMsgBox.ButtonSet[]
                        {
                            new FrmMsgBox.ButtonSet("Continue", Resources.MnuAccept, DialogResult.Yes),
                            new FrmMsgBox.ButtonSet("Later", Resources.MnuAdd, DialogResult.No),
                            new FrmMsgBox.ButtonSet("Discard", Resources.MnuDelete, DialogResult.Abort),
                       };

                    msg = "An error occured but " + toRun.Length.ToString() + " tests are still queued. You can continue running them now, save them for later, or discard them.";
                }
                else
                {
                    buttons = new FrmMsgBox.ButtonSet[]
                         {
                            new FrmMsgBox.ButtonSet("Now", Resources.MnuAccept, DialogResult.Yes),
                            new FrmMsgBox.ButtonSet("Later", Resources.MnuAdd, DialogResult.No)
                       };

                    msg = toRun.Length.ToString() + " tests are ready to run. You can save these for later or run them now.";
                }

                switch (FrmMsgBox.Show(this, "Save Tests", null, msg, Resources.MsgWarning, buttons))
                {
                    case DialogResult.Yes:
                        break;

                    case DialogResult.No:
                    default:
                        return;

                    case DialogResult.Abort:
                        _core.EvaluationResultFiles.RemoveAll(test => !test.HasResults);
                        return;
                }

                if (Paranoid)
                {
                    if (string.IsNullOrWhiteSpace(_core.FileNames.Session))
                    {
                        FrmMsgBox.ShowError(this, "The evaluations cannot be conducted because the session has not yet been saved. Please return to the main screen and save the session before continuing.");
                        return;
                    }
                    else if (!FrmMsgBox.ShowOkCancel(this, "Save Session", "To avoid data loss the current session must be saved. The current session is \"" + _core.FileNames.Session + "\".", FrmMsgBox.EDontShowAgainId.SaveBetweenEvaluations, DialogResult.OK))
                    {
                        return;
                    }
                }

                BeginWait(_tlpHeaderParams);

                try
                {
                    FrmWait.Show(this, "Please wait", null, z => RunTests(_core, toRun, z, Paranoid));
                }
                catch (Exception ex)
                {
                    FrmMsgBox.ShowError(this, ex);
                }
                finally
                {
                    EndWait(_tlpHeaderParams);
                }

                loop2 = true;
            }
        }

        private static bool RunTests(Core core, ClusterEvaluationPointer[] toRun, ProgressReporter proggy, bool paranoid)
        {
            // Save the session because each test-config has a test-GUID that allows the user to 
            // resume testing even if the previous results are lost. Saving the session assures 
            // the user won't loose the test-configs.
            // The session may also be saved if serialisation-GUIDs have changed, so this way
            // we also ensure consistancy in that the session is ALWAYS saved.
            if (paranoid)
            {
                SaveSession(core, proggy);
            }

            for (int n = 0; n < toRun.Length; n++)
            {
                var test = toRun[n];
                ClusterEvaluationPointer pointerToFile;

                try
                {
                    pointerToFile = RunTest(core, test, test.Configuration, proggy, paranoid, n, toRun.Length);
                }
                catch (Exception)
                {
                    test.Configuration.Enabled = false;
                    throw;
                }

                core.EvaluationResultFiles.Remove(test);
                core.EvaluationResultFiles.Add(pointerToFile);

                // Test swapped for results!
                if (paranoid)
                {
                    SaveSession(core, proggy);
                }
            }

            return true;
        }

        /// <summary>
        /// Actual test running
        /// </summary>             
        private static ClusterEvaluationPointer RunTest(Core core, ClusterEvaluationPointer origPointer, ClusterEvaluationConfiguration test, ProgressReporter proggy, bool paranoid, int index, int of)
        {
            UiControls.Assert(core.FileNames.Session != null, "Didn't expect the session filename to be null for cluster evaluation.");

            List<ClusterEvaluationParameterResult> results = new List<ClusterEvaluationParameterResult>();

            // Iterate over parameters
            for (int valueIndex = 0; valueIndex < test.ParameterValues.Length; valueIndex++)
            {
                object value = test.ParameterValues[valueIndex];

                List<ResultClusterer> repetitions = new List<ResultClusterer>();

                // Iterate over repetitions
                for (int repetition = 0; repetition < test.NumberOfRepeats; repetition++)
                {
                    proggy.Enter("Test " + index + "/" + of + ", parameter " + valueIndex + "/" + test.ParameterValues.Length + ", repetition " + repetition + "/" + test.NumberOfRepeats);

                    // Create config
                    string newName = AlgoParameterCollection.ParamToString(false, null, value) + " " + StringHelper.Circle(repetition + 1);
                    object[] copyOfParameters = test.ClustererConfiguration.Args.Parameters.ToArray();
                    copyOfParameters[test.ParameterIndex] = value;
                    ArgsClusterer copyOfArgs = new ArgsClusterer(
                        test.ClustererConfiguration.Args.Source,
                        test.ClustererConfiguration.Args.PeakFilter,
                        test.ClustererConfiguration.Args.Distance, 
                        test.ClustererConfiguration.Args.ObsFilter, 
                        test.ClustererConfiguration.Args.SplitGroups,
                        test.ClustererConfiguration.Args.Statistics,
                        copyOfParameters);
                    var copyOfConfig = new ConfigurationClusterer(newName, test.ClustererConfiguration.Comment, test.ClustererConfiguration.Id, copyOfArgs);

                    // Try load previus result
                    ResultClusterer result = null;

                    if (paranoid)
                    {
                        result = TryLoadIntermediateResult(core, test.Guid, valueIndex, repetition, proggy);
                    }

                    if (result == null)
                    {
                        // DO CLUSTERING!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                        proggy.Enter("Clustering");
                        result = copyOfConfig.Cluster(core, 0, proggy);
                        proggy.Leave();

                        if (paranoid)
                        {
                            SaveIntermediateResult(core, test.Guid, valueIndex, repetition, result, proggy);
                        }
                    }

                    // Add result
                    repetitions.Add(result);

                    string name = AlgoParameterCollection.ParamToString(false, null, value);

                    results.Add(new ClusterEvaluationParameterResult(name, test, valueIndex, repetitions));

                    proggy.Leave();
                }
            }

            ClusterEvaluationResults final = new ClusterEvaluationResults(core, test, results);

            string folder = UiControls.GetOrCreateFixedFolder(UiControls.EInitialFolder.Evaluations);
            string sessionName = Path.GetFileNameWithoutExtension(core.FileNames.Session);
            string fileName = core.Options.ClusteringEvaluationResultsFileName;
            fileName = fileName.Replace("{SESSION}", sessionName);
            fileName = fileName.Replace("{RESULTS}", folder + "\\");
            fileName = UiControls.GetNewFile(fileName);

            return SaveResults(core, fileName, origPointer, final, proggy);
        }

        /// <summary>
        /// Handles button: View script
        /// </summary>              
        private void _btnViewScript_Click(object sender, EventArgs e)
        {
            if (_selectedResults != null)
            {
                FrmEditConfigurationCluster.Show(this, _core, _selectedResults.Configuration.ClustererConfiguration, true, true);
            }
        }

        /// <summary>
        /// Handles button: Save results
        /// </summary>                  
        private void _btnSave_Click(object sender, EventArgs e)
        {
            if (_selectedResults == null)
            {
                return;
            }

            string fn = UiControls.BrowseForFile(this, null, UiControls.EFileExtension.EvaluationResults, FileDialogMode.SaveAs, UiControls.EInitialFolder.Evaluations);

            if (fn == null)
            {
                return;
            }

            FrmWait.Show(this, "Please wait", null, z => SaveResults(_core, fn, null, _selectedResults, z));

            FrmMsgBox.ShowInfo(this, "Export Notice", "Results have been exported. Exported data will only be compatible with the current data set.", FrmMsgBox.EDontShowAgainId.ExportDataNotice);
        }

        /// <summary>
        /// Saves results to file
        /// 
        /// Returns pointer (unless originalPointer is NULL, in which case the action is assumed to be an export and is ignored).
        /// </summary>          
        private static ClusterEvaluationPointer SaveResults(Core core, string fileName, ClusterEvaluationPointer originalPointer, ClusterEvaluationResults results, ProgressReporter proggy)
        {
            LookupByGuidSerialiser guidS = core.GetLookups();

            proggy.Enter("Saving results");
            XmlSettings.Save<ClusterEvaluationResults>( fileName, results, guidS, proggy);

            if (core.SetLookups(guidS))
            {
                // UIDs have changed
                SaveSession(core, proggy);
            }

            proggy.Leave();

            if (originalPointer == null)
            {
                return null;
            }

            return new ClusterEvaluationPointer(fileName, originalPointer.Configuration);
        }

        /// <summary>
        /// I wrote this when the program crashed after an hour.
        /// Intermediate results can now be saved to avoid losing data.
        /// </summary>          
        private static void SaveIntermediateResult(Core core, Guid guid, int value, int repetition, ResultClusterer result, ProgressReporter proggy)
        {
            string fileName = UiControls.GetTemporaryFile("." + value + "." + repetition + ".intermediate.dat", guid);

            LookupByGuidSerialiser guidS = core.GetLookups();

            proggy.Enter("Saving intermediate results");
            XmlSettings.Save<ResultClusterer>(new FileDescriptor( fileName, SerialisationFormat.MSerialiserCompactBinary), result, guidS, proggy);

            if (core.SetLookups(guidS))
            {
                // UIDs have changed
                SaveSession(core, proggy);
            }

            proggy.Leave();
        }

        /// <summary>
        /// Saves the core after GUIDs have changed.
        /// </summary>
        private static void SaveSession(Core core, ProgressReporter proggy)
        {
            proggy.Enter("Saving session");
            core.Save(core.FileNames.Session, proggy);
            proggy.Leave();
        }

        /// <summary>
        /// I wrote this when the program crashed after an hour.
        /// Intermediate results can now be saved to avoid losing data.
        /// </summary>          
        private static ResultClusterer TryLoadIntermediateResult(Core core, Guid guid, int value, int repetition, ProgressReporter proggy)
        {
            string fileName = UiControls.GetTemporaryFile("." + value + "." + repetition + ".intermediate.dat", guid);

            if (!File.Exists(fileName))
            {
                return null;
            }

            LookupByGuidSerialiser guidS = core.GetLookups();

            proggy.Enter("Loading saved results");
            ResultClusterer result;

            try
            {
                result = XmlSettings.LoadOrDefault<ResultClusterer>(new FileDescriptor(  fileName, SerialisationFormat.MSerialiserCompactBinary), null, guidS, proggy );
            }
            catch
            {
                proggy.Leave(); // Result will probably be NULL rather than throwing an exception
                return null;
            }

            UiControls.Assert(!guidS.HasLookupTableChanged, "Didn't expect the GUID lookup table to change when loading data.");
            proggy.Leave();

            return result;
        }

        /// <summary>
        /// Loads results from file.
        /// </summary>              
        private ClusterEvaluationResults LoadResults(string fileName, ProgressReporter z)
        {
            return LoadResults(_core, fileName, z);
        }

        /// <summary>
        /// Loads results from file.
        /// </summary>       
        public static ClusterEvaluationResults LoadResults(Core core, string fileName, ProgressReporter z)
        {
            LookupByGuidSerialiser guidS = core.GetLookups();
            ClusterEvaluationResults set;

            set = XmlSettings.LoadOrDefault<ClusterEvaluationResults>(fileName,null, guidS, z );

            if (set != null)
            {
                if (set.CoreGuid != core.CoreGuid)
                {
                    throw new InvalidOperationException("Wrong Session - The result set selected was not created using the current session. In order to view these results you must load the relevant session.\r\n\r\nCurrent session: " + core.CoreGuid + "\r\nResults session: " + set.CoreGuid);
                }
            }

            return set;
        }

        /// <summary>
        /// Button: Import
        /// </summary>  
        private void _btnImport_Click(object sender, EventArgs e)
        {
            string fn = UiControls.BrowseForFile(this, null, UiControls.EFileExtension.EvaluationResults, FileDialogMode.Open, UiControls.EInitialFolder.Evaluations);

            if (fn == null)
            {
                return;
            }

            _core.EvaluationResultFiles.Add(new ClusterEvaluationPointer(fn));

            FrmMsgBox.ShowInfo(this, "Import Results", "The file(s) have been imported into the current session. The complete details for these will not be available until the results are loaded.", FrmMsgBox.EDontShowAgainId.ImportResultsNotice);
        }

        /// <summary>
        /// Button: Load
        /// </summary>  
        private void _btnLoad_Click_1(object sender, EventArgs e)
        {
            ClusterEvaluationPointer res = PickResults();

            if (res == null)
            {
                return;
            }

            ClusterEvaluationResults set = FrmWait.Show(this, "Loading results", null, z => LoadResults(res.FileName, z));

            if (res.Configuration == null)
            {
                // The results didn't have known config, they do now!
                _core.EvaluationResultFiles.Remove(res);
                _core.EvaluationResultFiles.Add(new ClusterEvaluationPointer(res.FileName, set.Configuration));

                FrmMsgBox.ShowInfo(this, "Imported results", "The details on this result set have been imported into the current session. You will have to save the session to view these details in future.", FrmMsgBox.EDontShowAgainId.ImportResultsDetailNotice);
            }

            if (set != null)
            {
                SelectResults(res.FileName, set);
            }
        }

        private ClusterEvaluationPointer PickResults()
        {
            if (_core.EvaluationResultFiles.Count == 0)
            {
                FrmMsgBox.ButtonSet[] buttons =
                    {
                        new FrmMsgBox.ButtonSet("Create", Resources.MnuAccept, DialogResult.Yes),
                        new FrmMsgBox.ButtonSet("Import", Resources.MnuFile, DialogResult.No),
                        new FrmMsgBox.ButtonSet("Cancel", Resources.MnuAccept, DialogResult.Cancel),
                    };

                switch (FrmMsgBox.Show(this, "Select Test", null, "It appears you don't have any tests! Would you like to create a new one?", Resources.MsgHelp, buttons))
                {
                    case DialogResult.Yes:
                        _btnNew.PerformClick();
                        return null;

                    case DialogResult.No:
                        importToolStripMenuItem.PerformClick();
                        return null;

                    default:
                    case DialogResult.Cancel:
                        return null;
                }
            }

            ClusterEvaluationPointer res = DataSet.ForTests(this._core).ShowButtons(this, null);

            if (res == null)
            {
                return null;
            }

            if (!res.HasResults)
            {
                FrmMsgBox.ShowError(this, "Load Data", "This test has not yet been run!");
                return null;
            }

            return res;
        }

        private void _lstSel_SelectedIndexChanged(object sender, EventArgs e)
        {
            _lstParams_SelectedIndexChanged(sender, e);
        }

        [Flags]
        enum EUpdateResults
        {
            None = 0,

            [Name("Resave")]
            Resave = 1,

            [Name("Export CSV")]
            Csv = 2,

            [Name("Export text")]
            Information = 4,

            [Name("Recalculate statistics")]
            Statistics = 8,
        }

        private void updateResultsDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EUpdateResults options = EnumHelper.SumEnum(Generic.DataSet.ForFlagsEnum<EUpdateResults>("Batch Options").ShowCheckBox(this, null));

            if (options == EUpdateResults.None)
            {
                return;
            }

            IEnumerable<ClusterEvaluationPointer> tests = Generic.DataSet.ForTests(this._core).ShowCheckBox(this, null);

            if (tests == null)
            {
                return;
            }

            EClustererStatistics stats;

            if (options.Has(EUpdateResults.Statistics))
            {
                stats = EnumHelper.SumEnum(Generic.DataSet.ForFlagsEnum<EClustererStatistics>("Statistics").ShowCheckBox(this, null));
            }
            else
            {
                stats = EClustererStatistics.None;
            }

            string log = FrmWait.Show<string>(this, "Batch Process", null, proggy => BatchProcess(options, tests, stats, proggy));

            FrmInputMultiLine.ShowFixed(this, this.Text, "Batch Process", "Results of the batch process", log);
        }

        private string BatchProcess(EUpdateResults options, IEnumerable<ClusterEvaluationPointer> tests, EClustererStatistics stats, ProgressReporter proggy)
        {
            StringBuilder sb = new StringBuilder();

            foreach (ClusterEvaluationPointer res in tests)
            {
                sb.AppendLine("CONFIG: " + res.Configuration.ParameterConfigAsString);
                sb.AppendLine("PARAMS: " + res.Configuration.ParameterValuesAsString);
                sb.AppendLine("NAME: " + res.OverrideDisplayName);
                sb.AppendLine("FILE: " + res.FileName);

                if (!res.HasResults)
                {
                    sb.AppendLine(" - No results.");
                    continue;
                }

                Stopwatch timer = Stopwatch.StartNew();
                proggy.Enter("Loading results");
                bool load = options.Has(EUpdateResults.Csv | EUpdateResults.Statistics | EUpdateResults.Resave);
                ClusterEvaluationResults set = load ? LoadResults(res.FileName, proggy) : null;
                proggy.Leave();

                if (load && set == null)
                {
                    sb.AppendLine(" - Load failed.");
                    continue;
                }

                sb.AppendLine(" - LOAD-TIME: " + timer.Elapsed);
                sb.AppendLine();

                if (options.Has(EUpdateResults.Csv))
                {
                    timer.Restart();
                    proggy.Enter("Selecting results");
                    proggy.SetProgressMarquee();
                    this.Invoke((MethodInvoker)(() => SelectResults(res.FileName, set)));
                    proggy.Leave();
                    sb.AppendLine(" - DISPLAY-TIME: " + timer.Elapsed);

                    string csvFileName = Path.Combine(Path.GetDirectoryName(res.FileName), Path.GetFileNameWithoutExtension(res.FileName) + ".csv");
                    csvFileName = UiControls.GetNewFile(csvFileName, checkOriginal: true);

                    try
                    {
                        timer.Restart();
                        proggy.Enter("Exporting CSV");
                        proggy.SetProgressMarquee();
                        this.Invoke((MethodInvoker)(() =>
                        {
                            using (StreamWriter sw = new StreamWriter( csvFileName ))
                            {
                                _lvhStatistics.WriteItems( sw, true );
                            }
                        }));
                        proggy.Leave();
                        sb.AppendLine(" - EXPORT-TIME: " + timer.Elapsed);
                        sb.AppendLine(" - EXPORT: " + csvFileName);
                    }
                    catch (Exception ex)
                    {
                        sb.AppendLine(" - Export failed: " + ex.Message);
                    }

                    sb.AppendLine();
                }

                if (options.Has(EUpdateResults.Statistics))
                {
                    foreach (ClusterEvaluationParameterResult rep in set.Results)
                    {
                        rep.RecalculateStatistics(_core, stats, proggy);
                    }
                }

                if (options.Has(EUpdateResults.Information))
                {
                    proggy.Enter("Exporting information");

                    string infoFileName = Path.Combine(Path.GetDirectoryName(res.FileName), Path.GetFileNameWithoutExtension(res.FileName) + ".txt");
                    infoFileName = UiControls.GetNewFile(infoFileName, checkOriginal: true);

                    StringBuilder info = new StringBuilder();
                    info.Append(res.DisplayName);
                    info.AppendLine(res.Configuration.ParameterConfigAsString);
                    info.AppendLine(res.Configuration.ParameterValuesAsString);

                    File.WriteAllText(infoFileName, info.ToString());

                    sb.AppendLine(" - INFORMATION: " + infoFileName);
                    sb.AppendLine();
                }

                if (options.Has(EUpdateResults.Resave))
                {
                    string bakFileName = Path.Combine(Path.GetDirectoryName(res.FileName), Path.GetFileNameWithoutExtension(res.FileName) + ".old");
                    bakFileName = UiControls.GetNewFile(bakFileName, checkOriginal: true);
                    timer.Restart();
                    proggy.Enter("Backing up original");
                    proggy.SetProgressMarquee();
                    File.Copy(res.FileName, bakFileName, false);
                    proggy.Leave();
                    sb.AppendLine(" - BACKUP-TIME: " + timer.Elapsed);
                    sb.AppendLine(" - BACKUP: " + bakFileName);

                    timer.Restart();
                    proggy.Enter("Saving in latest format");
                    SaveResults(_core, res.FileName, null, set, proggy);
                    proggy.Leave();
                    sb.AppendLine(" - SAVE-TIME: " + timer.Elapsed);
                    sb.AppendLine(" - SAVE: " + res.FileName);
                    sb.AppendLine();
                }
            }

            return sb.ToString();
        }

        private void findInexplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClusterEvaluationPointer res = PickResults();

            if (res == null)
            {
                return;
            }

            UiControls.ExploreTo(this, res.FileName);
        }
    }
}
