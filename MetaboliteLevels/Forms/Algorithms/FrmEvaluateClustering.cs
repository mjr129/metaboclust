using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
using MSerialisers;
using MSerialisers.Serialisers;

namespace MetaboliteLevels.Forms.Algorithms
{
    /// <summary>
    /// Allows the user to evaluate clustering
    /// </summary>
    internal partial class FrmEvaluateClustering : Form
    {
        private readonly ListViewHelper<ClusterEvaluationParameterResult> _lvhConfigs;
        private readonly ListViewHelper<Cluster> _lvhClusters;
        private readonly ChartHelperForClusters _chcClusters;
        private readonly ListViewHelper<ColumnWrapper> _lvhStatistics;

        /// <summary>
        /// Results - see [ResultSetPointer]
        /// </summary>
        private readonly List<ClusterEvaluationPointer> availableResults = new List<ClusterEvaluationPointer>();

        private readonly Core _core;
        private readonly ConfigurationClusterer _templateConfig;
        private ClusterEvaluationResults _selectedResults;

        /// <summary>
        /// Shows the form
        /// </summary>                        
        internal static void Show(Form owner, Core core, ConfigurationClusterer config)
        {
            using (FrmEvaluateClustering frm = new FrmEvaluateClustering(core, config))
            {
                UiControls.ShowWithDim(owner, frm);
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            _core.EvaluationResultFiles.ReplaceAll(availableResults);
        }

        public FrmEvaluateClustering()
        {
            InitializeComponent();

            UiControls.SetIcon(this);
            UiControls.PopulateImageList(imageList1);
        }

        internal FrmEvaluateClustering(Core core, ConfigurationClusterer config)
            : this()
        {
            this._core = core;
            this._templateConfig = config;

            _lvhClusters = new ListViewHelper<Cluster>(_lstClusters, core, null, null);
            _lvhConfigs = new ListViewHelper<ClusterEvaluationParameterResult>(_lstParams, core, null, null);
            _lvhStatistics = new ListViewHelper<ColumnWrapper>(_lstStatistics, core, null, null);
            _lvhStatistics.Visible = false;
            _chcClusters = new ChartHelperForClusters(this.chart1, core, button1);

            _lvhConfigs.Activate += _lstParams_SelectedIndexChanged;
            _lvhClusters.Activate += _lstClusters_SelectedIndexChanged;
            _lvhStatistics.Activate += _lstStats_SelectedIndexChanged;

            if (core.EvaluationResultFiles == null)
            {
                core.EvaluationResultFiles = new List<ClusterEvaluationPointer>();
            }

            availableResults = new List<ClusterEvaluationPointer>(core.EvaluationResultFiles); // core.EvaluationResults

            UiControls.CompensateForVisualStyles(this);
        }

        class ColumnWrapper : IVisualisable
        {
            public string Title { get; set; }
            public string Comments { get; set; }

            private readonly ClusterEvaluationResults Results;
            public readonly Column<ClusterEvaluationParameterResult> Column;

            public ColumnWrapper(ClusterEvaluationResults rs, Column<ClusterEvaluationParameterResult> col)
            {
                this.Results = rs;
                this.Column = col;
            }

            public string DisplayName
            {
                get { return Title ?? Column.Id; }
            }

            public Image DisplayIcon
            {
                get { return Resources.ObjLStatistics; }
            }

            public int GetIcon()
            {
                return UiControls.ImageListOrder.Statistic;
            }

            public VisualClass VisualClass
            {
                get { return VisualClass.None; }
            }

            public string Comment { get; set; }

            public IEnumerable<InfoLine> GetInformation(Core core)
            {
                return null;
            }

            public IEnumerable<InfoLine> GetStatistics(Core core)
            {
                return null;
            }

            public IEnumerable<Column> GetColumns(Core core)
            {
                List<Column<ColumnWrapper>> cols = new List<Column<ColumnWrapper>>();

                cols.Add("Name", true, z => z.DisplayName);

                foreach (var v in Results.Results)
                {
                    var closure = v;
                    cols.Add(Results.Configuration.ParameterName + " = " + closure.DisplayName, false, z => z.Column.GetRow(closure));
                }

                return cols;
            }

            public void RequestContents(ContentsRequest list)
            {
                // NA
            }
        }



        private void SelectResults(ClusterEvaluationResults config)
        {
            _selectedResults = config;

            _lvhClusters.Clear();
            _chartParameters.Clear();
            _tvStatistics.Nodes.Clear();
            _lvhConfigs.Clear();
            _chcClusters.ClearPlot(true, null);
            _labelCluster.Text = "Cluster";
            _lblPlot.Text = "Plot";

            if (config == null)
            {
                return;
            }

            toolStripLabel1.Text = config.ToString();
            _lvhConfigs.DivertList(config.Results);

            List<ColumnWrapper> cols2 = new List<ColumnWrapper>();

            ClusterEvaluationParameterResult def = config.Results.FirstOrDefault();

            if (def != null)
            {
                var cols = def.GetColumns(_core);

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
                        node.ImageIndex = UiControls.ImageListOrder.Line;
                        node.SelectedImageIndex = node.ImageIndex;
                        node.Name = elem;
                        col.Add(node);
                    }

                    col = node.Nodes;
                }

                node.ImageIndex = UiControls.ImageListOrder.Statistic;
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
            label.BackColor = Color.CornflowerBlue;
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
            MChart.Plot plot = new MChart.Plot();
            //plot.Style = new MChart.PlotStyle();
            //plot.Style.ShowZero = false;

            MChart.Series series = new MChart.Series();
            series.Style = new MChart.SeriesStyle();
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

                series.Data.Add(new MChart.Coord(x, y));
            }

            plot.Series.Add(series);

            _chartParameters.Set(plot);

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

        /// <summary>
        /// Handles button: New test
        /// </summary>              
        private void _btnNewTest_Click(object sender, EventArgs e)
        {
            if (!ListValueSet.EvaluationResultsEditor(this, _core, availableResults))
            {
                return;
            }

            bool hasError = false;

            while (availableResults.Any(z => !z.HasResults))
            {
                var toRun = availableResults.Where(z => !z.HasResults).ToArray();

                if (toRun.Length == 0)
                {
                    return;
                }

                FrmMsgBox.ButtonSet[] buttons;
                string msg;

                if (hasError)
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

                switch (FrmMsgBox.Show(this, "Save Tests", null, msg, Resources.MsgWarning, buttons, 0, 2))
                {
                    case DialogResult.Yes:
                        break;

                    case DialogResult.No:
                    default:
                        return;

                    case DialogResult.Abort:
                        availableResults.RemoveAll(test => !test.HasResults);
                        return;
                }

                foreach (var test in toRun)
                {
                    ClusterEvaluationPointer pointerToFile = RunTest(test.Configuration);

                    if (pointerToFile == null)
                    {
                        hasError = true;
                        break;
                    }

                    availableResults.Remove(test);
                    availableResults.Add(pointerToFile);
                }
            }
        }

        /// <summary>
        /// Actual test running
        /// </summary>             
        private ClusterEvaluationPointer RunTest(ClusterEvaluationConfiguration test)
        {
            try
            {
                BeginWait(_tlpHeaderParams);

                List<ClusterEvaluationParameterResult> resultss = new List<ClusterEvaluationParameterResult>();

                for (int valueIndex = 0; valueIndex < test.ParameterValues.Length; valueIndex++)
                {
                    object value = test.ParameterValues[valueIndex];

                    List<ResultClusterer> results = new List<ResultClusterer>();

                    for (int repetition = 0; repetition < test.NumberOfRepeats; repetition++)
                    {
                        string newName = AlgoParameters.ParamToString(false, null, value) + " " + UiControls.Circle(repetition + 1);

                        object[] copyOfParameters = test.ClustererConfiguration.Args.Parameters.ToArray();
                        copyOfParameters[test.ParameterIndex] = value;
                        ArgsClusterer copyOfArgs = new ArgsClusterer(test.ClustererConfiguration.Args.PeakFilter, test.ClustererConfiguration.Args.Distance, test.ClustererConfiguration.Args.SourceMode, test.ClustererConfiguration.Args.ObsFilter, test.ClustererConfiguration.Args.SplitGroups, test.ClustererConfiguration.Args.Statistics, copyOfParameters);
                        var copyOfConfig = new ConfigurationClusterer(newName, test.ClustererConfiguration.Comments, test.ClustererConfiguration.Id, copyOfArgs);

                        string title = "Generating Clusters (test " + (valueIndex + 1) + " of " + test.ParameterValues.Length + ")";

                        try
                        {
                            results.Add(FrmWait.Show<ResultClusterer>(this, title, "Please wait", z => copyOfConfig.Cluster(_core, 0, z)));
                        }
                        catch (Exception ex)
                        {
                            FrmMsgBox.ShowError(this, "Error performing clustering", ex);
                            test.Enabled = false;
                            return null;
                        }
                    }

                    string name = AlgoParameters.ParamToString(false, null, value);

                    resultss.Add(new ClusterEvaluationParameterResult(name, test, valueIndex, results));
                }

                ClusterEvaluationResults final = new ClusterEvaluationResults(_core, test, resultss);

                string folder = UiControls.GetOrCreateFixedFolder(UiControls.EInitialFolder.Evaluations);
                string sessionName = Path.GetFileNameWithoutExtension(_core.FileNames.Session);
                string fileName = _core.Options.ClusteringEvaluationResultsFileName;
                fileName = fileName.Replace("{SESSION}", sessionName);
                fileName = fileName.Replace("{RESULTS}", folder + "\\");

                try
                {
                    return SaveResults(fileName, final);
                }
                catch (Exception ex)
                {
                    test.Enabled = false;
                    FrmMsgBox.ShowError(this, "Error saving results", ex);
                    return null;
                }
            }
            finally
            {
                EndWait(_tlpHeaderParams);
            }
        }

        /// <summary>
        /// Handles button: View script
        /// </summary>              
        private void _btnViewScript_Click(object sender, EventArgs e)
        {
            if (_selectedResults != null)
            {
                if (_selectedResults.Configuration.ClustererConfiguration == null)
                {
                    FrmMsgBox.ShowInfo(this, "Configuration", _selectedResults.Configuration.ClustererDescription);
                }
                else
                {
                    FrmAlgoCluster.Show(this, _core, _selectedResults.Configuration.ClustererConfiguration, true, null, true);
                }
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

            SaveResults(fn, _selectedResults);

            FrmMsgBox.Show(this, "Export Notice", null, "Results have been exported. Exported data will only be compatible with the current data set.", Resources.MsgInfo, dontShowAgainId: "FrmEvaluateClustering.ExportNotice");
        }

        /// <summary>
        /// Saves results to file
        /// </summary>          
        private ClusterEvaluationPointer SaveResults(string fileName, ClusterEvaluationResults results)
        {
            GuidSerialiser guidS = _core.GetLookups();

            FrmWait.Show(this, "Saving results", null,
                () => XmlSettings.SaveToFile(fileName, results, SerialisationFormat.Infer, guidS));

            _core.SetLookups(guidS);

            return new ClusterEvaluationPointer(fileName, results);
        }

        /// <summary>
        /// Loads results from file.
        /// </summary>              
        private ClusterEvaluationResults LoadResults(string fileName)
        {
            return LoadResults(this, _core, fileName);
        }

        public static ClusterEvaluationResults LoadResults(Form owner, Core core, string fileName)
        {
            GuidSerialiser guidS = core.GetLookups();

            ClusterEvaluationResults set = FrmWait.Show(owner, "Loading results", null,
                z => XmlSettings.LoadFromFile<ClusterEvaluationResults>(fileName, SerialisationFormat.Infer, z, guidS));

            if (set.CoreGuid != core.CoreGuid)
            {
                if (FrmMsgBox.Show2(owner, "Error", null, "The result set loaded was not created using the current session", Resources.MsgError,
                    new FrmMsgBox.ButtonSet("Abort", Resources.MnuCancel, DialogResult.Cancel),
                    new FrmMsgBox.ButtonSet("Ignore", Resources.MnuWarning, DialogResult.Ignore),
                    null, 0, 0) == DialogResult.Cancel)
                {
                    return null;
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

            ClusterEvaluationResults set = LoadResults(fn);

            if (set != null)
            {
                this.availableResults.Add(new ClusterEvaluationPointer(fn, set));
                SelectResults(set);
            }
        }

        /// <summary>
        /// Button: Load
        /// </summary>  
        private void _btnLoad_Click_1(object sender, EventArgs e)
        {
            if (this.availableResults.Count == 0)
            {
                FrmMsgBox.ButtonSet[] buttons =
                    {
                        new FrmMsgBox.ButtonSet("Create", Resources.MnuAccept, DialogResult.Yes),
                        new FrmMsgBox.ButtonSet("Import", Resources.MnuFile, DialogResult.No),
                        new FrmMsgBox.ButtonSet("Cancel", Resources.MnuAccept, DialogResult.Cancel),
                    };

                switch (FrmMsgBox.Show(this, "Select Test", "It appears you don't have any tests! Would you like to create a new one?"))
                {
                    case DialogResult.Yes:
                        _btnNew.PerformClick();
                        return;

                    case DialogResult.No:
                        _btnImport.PerformClick();
                        return;

                    default:
                    case DialogResult.Cancel:
                        return;
                }
            }

            ClusterEvaluationPointer res = ListValueSet.ForTests(_core, this.availableResults).ShowButtons(this);

            if (res != null)
            {
                if (!res.HasResults)
                {
                    FrmMsgBox.ShowError(this, "Load Data", "This test has not yet been run.");
                    return;
                }

                ClusterEvaluationResults set = LoadResults(res.FileName);

                if (set != null)
                {
                    SelectResults(set);
                }
            }
        }

        private void _lstSel_SelectedIndexChanged(object sender, EventArgs e)
        {
            _lstParams_SelectedIndexChanged(sender, e);
        }
    }
}
