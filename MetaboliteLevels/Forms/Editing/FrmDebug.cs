using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Data.DataInfo;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Forms.Generic;
using MetaboliteLevels.Settings;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Algorithms;
using MetaboliteLevels.Algorithms.Statistics.Arguments;

// ReSharper disable UnusedMember.Local

namespace MetaboliteLevels.Forms.Editing
{
    /// <summary>
    /// Additional and debugging options.
    /// </summary>
    public partial class FrmDebug : Form, IProgressReceiver
    {
        private Stopwatch _lastProgressFlash = Stopwatch.StartNew();
        private int _waitCounter;
        private Core _core;
        private bool _result;

        private class InListAttribute : Attribute { }

        internal static bool Show(Form owner, Core core)
        {
            using (FrmDebug frm = new FrmDebug())
            {
                frm._core = core;
                UiControls.ShowWithDim(owner, frm);
                return frm._result;
            }
        }

        private Peak PickVariable()
        {
            return DataSet.ForPeaks(_core).ShowList(this, null);
        }

        private Cluster PickCluster()
        {
            return DataSet.ForClusters(_core).ShowList(this, null);
        }

        private void EndWait()
        {
            _waitCounter--;
            UiControls.Assert(_waitCounter >= 0, "EndWait called when no wait preceded.");

            if (_waitCounter == 0)
            {
                toolStripStatusLabel2.Visible = false;
                UseWaitCursor = false;
                _statusMain.BackColor = BackColor;
                toolStripProgressBar1.Visible = false;
            }
        }


        private void BeginWait(string p, bool pb = false)
        {
            _waitCounter++;
            toolStripStatusLabel2.Text = p;
            toolStripStatusLabel2.Visible = true;
            _lastProgressFlash.Restart();
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
        void IProgressReceiver.ReportProgressDetails(ProgressReporter.ProgInfo info)
        {
            toolStripStatusLabel2.Text = info.Text;

            if (info.Percent >= 0)
            {
                toolStripProgressBar1.Maximum = 100;
                toolStripProgressBar1.Style = ProgressBarStyle.Continuous;
                toolStripProgressBar1.Value = info.Percent;
            }
            else
            {
                toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
            }

            _statusMain.Refresh();

            if (_lastProgressFlash.ElapsedMilliseconds > 1000)
            {
                _lastProgressFlash.Restart();
                _statusMain.BackColor = _statusMain.BackColor == Color.Red ? Color.Orange : Color.Red;
            }
        }

        private FrmDebug()
        {
            InitializeComponent();
            UiControls.SetIcon(this);
            UiControls.CompensateForVisualStyles(this);

            var ms = this.GetType().GetMethods(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.DeclaredOnly);

            foreach (var m in ms)
            {
                InListAttribute[] attr = (InListAttribute[])m.GetCustomAttributes(typeof(InListAttribute), false);

                if (attr.Length != 0)
                {
                    DescriptionAttribute[] desc = (DescriptionAttribute[])m.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    MethodInvoker invoker = (MethodInvoker)m.CreateDelegate(typeof(MethodInvoker), this);

                    AddMethod(m.Name, invoker, desc.Length != 0 ? desc[0].Description : m.Name);
                }
            }
        }

        private void AddMethod(string p, MethodInvoker invoker, string description)
        {
            ListViewItem lvi = new ListViewItem(p);
            lvi.Tag = new object[] { invoker, description };
            lvi.ImageIndex = 0;
            lvi.SubItems.Add(description);
            lvi.UseItemStyleForSubItems = false;
            lvi.SubItems[1].ForeColor = Color.Gray;

            listView1.Items.Add(lvi);
        }

        [InList]
        [Description("Creates a cluster from each of the pathways, allowing you to explore pathways as you do with clusters.")]
        void create_clusters_from_pathways()
        {
            BeginWait("Create clusters from pathways", false);

            var args = new ArgsClusterer(null, null, EAlgoSourceMode.None, null, false, EClustererStatistics.None, null);

            ConfigurationClusterer config = new ConfigurationClusterer("create_clusters_from_pathways",
                                                                       "create_clusters_from_pathways", Algo.ID_PATFROMPATH, args);

            _core.AddClusterer(config, new ProgressReporter(this));

            EndWait();
        }

        [InList]
        [Description("(Debugging feature) Displays information on all of the experimental groups.")]
        void display_type_info()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < _core.Groups.Count; i++)
            {
                sb.AppendLine("========== " + i + " ==========");
                sb.AppendLine(_core.Groups[i].ToString());
            }

            FrmInputLarge.ShowFixed(this, "Debugging", "Type Info", null, sb.ToString());
        }

        [InList]
        [Description("Input and run R command and view the result")]
        void do_something_in_r()
        {
            string text = FrmInputLarge.Show(this, "R", "Enter command", null, "a = 42\r\na");

            if (text != null)
            {
                try
                {
                    string newText = Arr.Instance.Evaluate(text).ToString();

                    FrmInputLarge.ShowFixed(this, "R", "Result of user command", null, newText);
                }
                catch (Exception ex)
                {
                    FrmMsgBox.ShowError(this, ex.Message);
                }
            }
        }

        [InList]
        [Description("Finds the closest and most different peaks")]
        void find_distance_range()
        {
            ProgressReporter prog = new ProgressReporter(this);

            double smallest = double.MaxValue;
            double largest = double.MinValue;
            Tuple<Peak, Peak> smallestT = null;
            Tuple<Peak, Peak> largestT = null;
            ConfigurationMetric metric = new ConfigurationMetric(null, null, Algo.ID_METRIC_EUCLIDEAN, null);
            ValueMatrix vmatrix = ValueMatrix.Create(_core.Peaks, true, _core, ObsFilter.Empty, false, prog);
            DistanceMatrix dmatrix = DistanceMatrix.Create(_core, vmatrix, metric, prog);

            for (int peakIndex1 = 0; peakIndex1 < _core.Peaks.Count; peakIndex1++)
            {
                for (int peakIndex2 = 0; peakIndex2 < _core.Peaks.Count; peakIndex2++)
                {
                    if (peakIndex1 != peakIndex2)
                    {
                        double result = dmatrix.Values[peakIndex1, peakIndex2];

                        Peak peak1 = _core.Peaks[peakIndex1];
                        Peak peak2 = _core.Peaks[peakIndex2];

                        if (result > largest)
                        {
                            largest = result;
                            largestT = new Tuple<Peak, Peak>(peak1, peak2);
                        }

                        if (result < smallest)
                        {
                            smallest = result;
                            smallestT = new Tuple<Peak, Peak>(peak1, peak2);
                        }
                    }
                }
            }

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("| " + smallestT.Item1.DisplayName + " - " + smallestT.Item2.DisplayName + " | = " + smallest);
            sb.AppendLine("| " + largestT.Item1.DisplayName + " - " + largestT.Item2.DisplayName + " | = " + largest);

            FrmInputLarge.ShowFixed(this, "Find distance range", "Maximum and minimum differences", "Showing the closest and furthest peaks", sb.ToString());
        }

        [InList]
        [Description("Compares two peaks")]
        void compare_variables()
        {
            ProgressReporter prog = new ProgressReporter(this);
            ConfigurationMetric metric = new ConfigurationMetric(null, null, Algo.ID_METRIC_EUCLIDEAN, null);
            ValueMatrix vmatrix = ValueMatrix.Create(_core.Peaks, true, _core, ObsFilter.Empty, false, prog);
            DistanceMatrix dmatrix = DistanceMatrix.Create(_core, vmatrix, metric, prog);

            Peak v1 = PickVariable();

            if (v1 != null)
            {
                Peak v2 = PickVariable();

                if (v2 != null)
                {
                    double result = dmatrix.Find(v1, null, v2, null);

                    FrmInputLarge.ShowFixed(this, "Compare variables", v1.DisplayName + " - " + v2.DisplayName, "Showing the distance between two variables' vectors", result.ToString());
                }
            }
        }

        [InList]
        [Description("View peak raw intensity data")]
        void view_variable_full()
        {
            Peak v = PickVariable();

            if (v != null)
            {
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < _core.Observations.Count; i++)
                {
                    ObservationInfo obs = _core.Observations[i];
                    sb.AppendLine(obs.Time + obs.Group.DisplayShortName + obs.Rep + " = " + v.Observations.Raw[i]);
                }

                FrmInputLarge.ShowFixed(this, "View full variable", v.DisplayName, "Full variable information", sb.ToString());
            }
        }

        [InList]
        [Description("View peak intensity trend")]
        void view_variable_trend()
        {
            Peak v = PickVariable();

            if (v != null)
            {
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < _core.Conditions.Count; i++)
                {
                    ConditionInfo cond = _core.Conditions[i];
                    sb.AppendLine(cond.Time + cond.Group.DisplayShortName + " = " + v.Observations.Trend[i]);
                }

                FrmInputLarge.ShowFixed(this, "View variable averages", v.DisplayName, "Variable averages", sb.ToString());
            }
        }

        [InList]
        [Description("Shows statistics on the current clusters")]
        void evaluate_cluster_performance()
        {
            bool warning;
            double d;
            int di;
            double dz;
            int dzi;
            double best;
            double worst;
            int bwcount;
            List<Tuple<Peak, double>> all = new List<Tuple<Peak, double>>();
            _core.QuantifyAssignments(out warning, out d, out di, out dz, out dzi, out worst, out best, out bwcount, all);
            var all2 = all.Where(λ => λ.Item1.Assignments.Clusters.All(z => z.States == Cluster.EStates.None));

            StringBuilder sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine("Value = Σ(i) ( Distance( x(i) - c(i) ) )");
            sb.AppendLine("  Where x = values(i), Distance = Euclidean and c(x) = Closest centre in assigned cluster for i.");
            sb.AppendLine();
            sb.AppendLine("All clusters");
            sb.AppendLine("  Observations = " + di);
            sb.AppendLine("  Value        = " + d);
            sb.AppendLine("  Average      = " + (double)d / di);
            sb.AppendLine();
            sb.AppendLine("All significant clusters");
            sb.AppendLine("  Observations = " + dzi);
            sb.AppendLine("  Value        = " + dz);
            sb.AppendLine("  Average      = " + (double)dz / dzi);
            sb.AppendLine();
            sb.AppendLine("Best/worst 10% in significant clusters");
            sb.AppendLine("  Observations = " + bwcount);
            sb.AppendLine("  Best         = " + best);
            sb.AppendLine("  Worst        = " + worst);
            sb.AppendLine("  Avg. Best    = " + best / bwcount);
            sb.AppendLine("  Avg. Worst   = " + worst / bwcount);
            sb.AppendLine();
            sb.AppendLine("Distribution");
            sb.AppendLine("  all.names = c(" + StringHelper.ArrayToString(all, λ => "\"" + λ.Item1.DisplayName + "\"") + " )");
            sb.AppendLine("  all.values = c(" + StringHelper.ArrayToString(all, λ => λ.Item2.ToString()) + " )");
            sb.AppendLine("  signif.names = c(" + StringHelper.ArrayToString(all2, λ => λ.Item1.DisplayName) + " )");
            sb.AppendLine("  signif.values = c(" + StringHelper.ArrayToString(all2, λ => λ.Item2.ToString()) + " )");

            if (warning)
            {
                FrmMsgBox.ShowWarning(this, "Performance", "One or more clusters had no centres, their centres were set to the average of the assignments.");
            }

            FrmInputLarge.ShowFixed(this, "Performance", "Performance", null, sb.ToString());
        }

        [Description("(Debugging feature) Break into the debugger to execute a query")]
        [InList]
        void break_query()
        {
            Debugger.Break();

            FrmMsgBox.ShowInfo(this, "Message", "This is a message");

            FrmInputLarge.ShowFixed(this, "Break query", "Allow debugger to take control", "Showing text stored in temporary string", "");
        }

        [Description("View object fields")]
        [InList]
        void view_fields()
        {
            object vis = DataSet.ForEverything(_core).ShowList(this, null);

            if (vis is IVisualisable)
            {
                IEnumerable<string> selected = new DataSet<string>()
                {
                    Title = vis.ToString(),
                    SubTitle = "List of properties",
                    List = ((IVisualisable)vis).QueryProperties(_core)
                }.ShowCheckList(this, null);

                if (selected != null && !selected.IsEmpty())
                {
                    FrmInputLarge.ShowFixed(this, Text, "Property Descriptor", "Example property descriptor", StringHelper.ArrayToString(selected, z => z.Substring(z.IndexOf(':') + 1) + " = {" + z + "}", ", "));
                }
            }
        }

        [Description("Adds a meta-field to the names of all peaks")]
        [InList]
        void set_peak_names()
        {
            string header = FrmInput.Show(this, Text, "Peak names", "Enter the peak names", "{DisplayName}");

            if (header != null)
            {
                ParseElementCollection hc = new ParseElementCollection(header);

                foreach (Peak p in _core.Peaks)
                {
                    p.OverrideDisplayName = hc.ConvertToString(p, _core);
                }
            }
        }

        [Description("Find the cutoff for a chosen statistic that best groups peaks based on two specified flags of interest.")]
        [InList]
        void find_classifier()
        {
            Core core = _core;

            PeakFlag type1;
            PeakFlag type2;

            ConfigurationStatistic stat = DataSet.ForStatistics(_core).ShowList(this, null);

            if (stat == null)
            {
                FrmMsgBox.ShowError(this, "No stat with this name");
                return;
            }

            string sign = FrmInput.Show(this, "Classifier settings", "Find classifier", "Enter the cutoff, or 0 for for automatic", "0");
            double manCutoff;

            type1 = DataSet.ForPeakFlags(_core).IncludeMessage("Specify the comment flag signifying the first type").ShowList(this, null);

            if (type1 == null)
            {
                return;
            }

            type2 = DataSet.ForPeakFlags(_core).IncludeMessage("Specify the comment flag signifying the second type").ShowList(this, null);

            if (type2 == null)
            {
                return;
            }

            if (!double.TryParse(sign, out manCutoff))
            {
                return;
            }

            StringBuilder sb = new StringBuilder();

            // TID0 = Full
            // TID1..5 = Test/training
            // TID6..9 = Bootstrap
            for (int tid = 0; tid < 10; tid++)
            {
                // Get all significances
                List<double> sigs = new List<double>(core.Peaks.Select(λ => λ.GetStatistic(stat)));

                List<bool> inTrainingSet = new List<bool>(core.Peaks.Count);
                int co;

                // For the training only include 75%
                if (tid >= 1 && tid <= 5)
                {
                    co = (int)(core.Peaks.Count * 0.75d);
                }
                else
                {
                    co = core.Peaks.Count;
                }

                for (int n = 0; n < core.Peaks.Count; n++)
                {
                    inTrainingSet.Add(n < co);
                }

                inTrainingSet.Shuffle();

                // For the boot-strap shuffle the sigs
                if (tid >= 6)
                {
                    sigs.Shuffle();
                }

                Tuple<double, int, int, int, int> best = null;
                Tuple<double, int, int, int, int> bestTest = null;
                double cutoff = 0;

                // Find the best variable cutoff
                if (manCutoff == 0.0d)
                {
                    for (int n = 0; n < core.Peaks.Count; n++)
                    {
                        if (inTrainingSet[n])
                        {
                            var success = SimpleClassify(sigs[n], type1, type2, sigs, inTrainingSet, true);

                            if (best == null || success.Item1 > best.Item1)
                            {
                                best = success;
                                bestTest = SimpleClassify(sigs[n], type1, type2, sigs, inTrainingSet, false);
                                cutoff = sigs[n];
                            }
                        }
                    }
                }
                else
                {
                    best = SimpleClassify(manCutoff, type1, type2, sigs, inTrainingSet, true);
                    bestTest = SimpleClassify(manCutoff, type1, type2, sigs, inTrainingSet, false);
                    cutoff = manCutoff;
                }

                sb.AppendLine(tid == 0 ? "FULLDATA" : tid <= 5 ? "VALIDATION" : "BOOTSTRAP");
                sb.AppendLine();
                sb.AppendLine("    " + type1 + " <= " + cutoff + " < " + type2);
                sb.AppendLine();
                sb.AppendLine("    TRAINING SET (" + co + ")");
                sb.AppendLine("        " + type1 + " correct: " + StringHelper.AsFraction(best.Item2, best.Item2 + best.Item3));
                sb.AppendLine("        " + type2 + " correct: " + StringHelper.AsFraction(best.Item4, best.Item4 + best.Item5));
                sb.AppendLine("        Total correct: " + StringHelper.AsFraction(best.Item2 + best.Item4, best.Item2 + best.Item4 + best.Item3 + best.Item5));
                sb.AppendLine("        Variables used: " + StringHelper.AsFraction(best.Item2 + best.Item4 + best.Item3 + best.Item5, core.Peaks.Count));
                sb.AppendLine();
                if (co != core.Peaks.Count)
                {
                    sb.AppendLine("    TEST SET (" + (core.Peaks.Count - co) + ")");
                    sb.AppendLine("        " + type1 + " correct: " + StringHelper.AsFraction(bestTest.Item2, bestTest.Item2 + bestTest.Item3));
                    sb.AppendLine("        " + type2 + " correct: " + StringHelper.AsFraction(bestTest.Item4, bestTest.Item4 + bestTest.Item5));
                    sb.AppendLine("        Total correct: " + StringHelper.AsFraction(bestTest.Item2 + bestTest.Item4, bestTest.Item2 + bestTest.Item4 + bestTest.Item3 + bestTest.Item5));
                    sb.AppendLine("        Variables used: " + StringHelper.AsFraction(bestTest.Item2 + bestTest.Item4 + bestTest.Item3 + bestTest.Item5, core.Peaks.Count));
                    sb.AppendLine();
                    sb.AppendLine("    SCORE: " + (bestTest.Item1 * 100).ToString("F02"));
                }
                else
                {
                    sb.AppendLine("    SCORE: " + (best.Item1 * 100).ToString("F02"));
                }
                sb.AppendLine();
                sb.AppendLine("--------------------------------------------------------------------------------");
                sb.AppendLine();
            }

            FrmInputLarge.ShowFixed(this, "Find classifier", "Classifier results", "Best value to determine split between variables marked with \"" + type1 + "\" and \"" + type2 + "\" based on their significances", sb.ToString());
        }

        [InList]
        [Description("Prompts for a list of peaks and saves their raw and trend profiles to a file.")]
        private void extract_profiles()
        {
            string peakNames = FrmInputLarge.Show(this, "Extract profiles", "Specify peak names", null, null);

            if (peakNames == null)
            {
                return;
            }

            BeginWait("Extracting profiles");

            string[] e = peakNames.Split("\r\n\t,;".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            StringBuilder sa = new StringBuilder();
            StringBuilder sb = new StringBuilder();

            sa.AppendLine(_core.FileNames.Title);
            sa.AppendLine();
            sa.AppendLine("All intensities");

            sa.Append("Peak, ");
            sb.Append("Peak, ");

            sa.AppendLine(StringHelper.ArrayToString(_core.Observations, z => z.ToString()));
            sb.AppendLine(StringHelper.ArrayToString(_core.Conditions, z => z.ToString()));

            foreach (string elemm in e)
            {
                string elem = elemm.ToUpper().Trim();
                Peak p = _core.Peaks.FirstOrDefault(z => z.DisplayName.ToUpper() == elem);

                if (p != null)
                {
                    sa.Append(p.DisplayName + ", ");
                    sb.Append(p.DisplayName + ", ");

                    sa.AppendLine(StringHelper.ArrayToString(p.Observations.Raw));
                    sb.AppendLine(StringHelper.ArrayToString(p.Observations.Trend));
                }
            }

            sa.AppendLine();
            sa.AppendLine("Trendline intensities (" + _core.AvgSmoother.ToString() + ")");
            sa.Append(sb);

            EndWait();

            string fn = UiControls.BrowseForFile(this, null, UiControls.EFileExtension.Csv, FileDialogMode.SaveAs, UiControls.EInitialFolder.ExportedData);

            if (fn != null)
            {
                System.IO.File.WriteAllText(fn, sa.ToString());

                FrmMsgBox.ShowCompleted(this, "Extract Profiles", "Data saved to: " + fn);
            }
        }

        [InList]
        [Description("View information about the current dataset.")]
        private void view_statistics()
        {
            Core core = _core;
            StringBuilder sb = new StringBuilder();

            int vwpid = core.Peaks.Count(λ => λ.Annotations.Count != 0);
            double td = core.Peaks.Sum(λ => λ.Assignments.Count != 0 ? λ.Assignments.Scores.Average() : double.NaN) / core.Peaks.Count;
            int sigs = core.Peaks.Count(λ => λ.Assignments.Clusters.All(z => z.States == Cluster.EStates.None));

            sb.AppendLine("Observations:");
            sb.AppendLine("    № observations = " + core.Observations.Count);
            sb.AppendLine("    № conditions = " + core.Conditions.Count);
            sb.AppendLine("    № types = " + core.Groups.Count);
            sb.AppendLine();

            sb.AppendLine("Variables:");
            sb.AppendLine("    № variables = " + core.Peaks.Count);
            sb.AppendLine("    № with potential IDs = " + StringHelper.AsFraction(vwpid, core.Peaks.Count));
            sb.AppendLine("    № significant (based on cluster) = " + StringHelper.AsFraction(sigs, core.Peaks.Count));
            sb.AppendLine("    Average distance = " + td.ToString("F02"));
            sb.AppendLine();

            sb.AppendLine("Clusters:");
            sb.AppendLine("    № clusters = " + core.Clusters.Count);
            sb.AppendLine("    Active count = " + core.Clusters.Count(p => p.States == Cluster.EStates.None));
            sb.AppendLine();

            foreach (Cluster p in core.Clusters)
            {
                sb.AppendLine("    " + p.DisplayName + " = " + StringHelper.AsFraction(p.Assignments.Count, core.Peaks.Count));
            }
            sb.AppendLine();

            int pc = core.Pathways.Count(p => !p.Compounds.TrueForAll(c => c.Annotations.Count == 0));
            sb.AppendLine("Pathways:");
            sb.AppendLine("    № pathways = " + core.Pathways.Count);
            sb.AppendLine("    № with 1+ potential IDs = " + StringHelper.AsFraction(pc, core.Pathways.Count));
            sb.AppendLine();

            int cc = core.Compounds.Count(c => c.Annotations.Count != 0);
            sb.AppendLine("Compounds:");
            sb.AppendLine("    № compounds = " + core.Compounds.Count);
            sb.AppendLine("    № with potential variables = " + StringHelper.AsFraction(cc, core.Compounds.Count));
            sb.AppendLine();

            sb.AppendLine("Adducts:");
            sb.AppendLine("    № adducts = " + core.Adducts.Count);
            sb.AppendLine();

            sb.AppendLine("Flags:");
            HashSet<PeakFlag> flags = new HashSet<PeakFlag>();

            foreach (Peak v in core.Peaks)
            {
                foreach (PeakFlag flag in v.CommentFlags)
                {
                    flags.Add(flag);
                }
            }

            foreach (PeakFlag flag in flags)
            {
                int c = 0;

                foreach (Peak v in core.Peaks)
                {
                    if (v.CommentFlags.Contains(flag))
                    {
                        c++;
                    }
                }

                sb.AppendLine("    " + flag + " = " + StringHelper.AsFraction(c, core.Peaks.Count));
            }

            FrmInputLarge.ShowFixed(this, "View statistics", "Statistics summary", null, sb.ToString());
        }

        [InList]
        [Description("Find the peak closest the average for a particular cluster.")]
        private void find_archetype()
        {
            Cluster p = PickCluster();

            if (p == null)
            {
                return;
            }

            var average = p.GetCentre(ECentreMode.Average, ECandidateMode.Assignments)[0];

            Vector clV = null;
            double clD = double.MaxValue;

            foreach (Vector v in p.Assignments.Vectors)
            {
                double d = Maths.Euclidean(v.Values, average);

                if (d < clD)
                {
                    clV = v;
                    clD = d;
                }
            }

            FrmMsgBox.ShowCompleted(this, this.Text, "Info: The most similar variable to the Euclidean mean of cluster " + p.DisplayName + " is " + clV.ToString());
        }

        [InList]
        [Description("(legacy) Split a cluster in two.")]
        private void find_most_different()
        {
            FrmMsgBox.ShowError(this, "This method is not available.");

            /*
            Peak a = null;
            Peak b = null;
            double maxD = double.MinValue;

            Cluster p = PickCluster();

            if (p == null)
            {
                return;
            }

            for (int vi = 0; vi < p.Assignments.Count; vi++)
            {
                Peak v = p.Assignments.List[vi].Peak;

                for (int wi = vi + 1; wi < p.Assignments.Count; wi++)
                {
                    Peak w = p.Assignments.List[wi].Peak;

                    double d = Maths.Euclidean(v.Values, w.Values);

                    if (d > maxD)
                    {
                        a = v;
                        b = w;
                        maxD = d;
                    }
                }
            }

            _core.DeleteCluster(p);

            Cluster aP = new Cluster(p.Name + "-" + a.Name, "MostDifferent");
            ClusterAssignment.Make(a, aP, double.NaN);

            Cluster bP = new Cluster(p.Name + "-" + b.Name, "MostDifferent");
            ClusterAssignment.Make(b, bP, double.NaN);
             * */
        }

        private Tuple<double, int, int, int, int> SimpleClassify(double cutoff, PeakFlag type1, PeakFlag type2, List<double> sigs, List<bool> inTrainingSet, bool checkTrainingSet)
        {
            int correct1 = 0;
            int incorrect1 = 0;
            int correct2 = 0;
            int incorrect2 = 0;

            for (int vi = 0; vi < _core.Peaks.Count; vi++)
            {
                if (inTrainingSet[vi] == checkTrainingSet)
                {
                    Peak v = _core.Peaks[vi];
                    double sig = sigs[vi];

                    if (v.CommentFlags.Contains(type1))
                    {
                        if (sig <= cutoff)
                        {
                            correct1++;
                        }
                        else
                        {
                            incorrect1++;
                        }
                    }
                    else if (v.CommentFlags.Contains(type2))
                    {
                        if (sig <= cutoff)
                        {
                            incorrect2++;
                        }
                        else
                        {
                            correct2++;
                        }
                    }
                }
            }

            double score = (((double)correct1 / (correct1 + incorrect1)) + ((double)correct2 / (correct2 + incorrect2))) / 2;

            return new Tuple<double, int, int, int, int>(score, correct1, incorrect1, correct2, incorrect2);
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 0)
            {
                object[] args = (object[])listView1.SelectedItems[0].Tag;
                MethodInvoker mi = (MethodInvoker)args[0];

                mi();

                _result = true; // flag that things have changed, but allow the user to do more.
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 0)
            {
                object[] args = (object[])listView1.SelectedItems[0].Tag;
                string mi = (string)args[1];

                textBox1.Text = mi;
            }
        }
    }
}
