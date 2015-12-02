using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MetaboliteLevels.Data;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Forms.Editing;
using MetaboliteLevels.Forms.Generic;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Viewers.Charts;

namespace MetaboliteLevels.Forms.Tree_Explorer
{
    public partial class FrmTreeView : Form
    {
        FrmMain _owner;
        Core _core;
        ChartHelperForClusters _patChart;
        ChartHelperForPeaks _varChart;
        string _findText;

        /// <summary>
        /// Constructor.
        /// </summary>
        public FrmTreeView()
        {
            InitializeComponent();
            UiControls.SetIcon(this);
            UiControls.PopulateImageList(imageList1);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        private FrmTreeView(FrmMain owner, Core core, TreeNode tn)
            : this()
        {
            this._owner = owner;
            this._core = core;
            treeView1.Nodes.Add(tn);

            _patChart = new ChartHelperForClusters(chart1, core, null);
            _varChart = new ChartHelperForPeaks(chart2, core, null);

            _varChart.SelectionChanged += varChart_SelectionChanged;
            _patChart.SelectionChanged += patChart_SelectionChanged;

            UiControls.CompensateForVisualStyles(this);
        }

        void varChart_SelectionChanged(object sender, ChartSelectionEventArgs e)
        {
            _lblSeries.Text = e.seriesName; // +"\r\n" + (e.dataPoint != null ? e.dataPoint.ToString() : "");
            _lblSeries.Visible = true;
        }

        void patChart_SelectionChanged(object sender, ChartSelectionEventArgs e)
        {
            _lblSeries.Text = e.seriesName; // +"\r\n" + (e.dataPoint != null ? e.dataPoint.ToString() : "");
            _lblSeries.Visible = true;
        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNodeConvertor.ExpandNodeIfNeeded(e.Node);
        }

        internal static void Show(FrmMain owner, Core core, object toSelect)
        {
            TreeNode root = TreeNodeConvertor.Create(core);

            using (FrmTreeView frm = new FrmTreeView(owner, core, root))
            {
                TreeNodeConvertor.SelectNode(frm.treeView1, toSelect);
                UiControls.ShowWithDim(owner, frm);
            }
        }

        private T GetTag<T>(TreeNode node) where T : class
        {
            while ((node.Tag as T) == null && node.Parent != null)
            {
                node = node.Parent;
            }

            return (T)node.Tag;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            object x = GetTag<object>(e.Node);

            _lblSeries.Visible = false;

            if (x == null)
            {
                _lblTitle.Text = "";
                _lblSubtitle.Text = "";
                button1.Text = "";
                button1.Enabled = false;
                webBrowser1.Visible = false;
                chart1.Visible = false;
                chart2.Visible = false;
                return;
            }

            if (x is string)
            {
                string text = (string)x;
                string subTitle = Maths.SplitEquals(ref text, "/");
                _lblTitle.Text = text;
                _lblSubtitle.Text = subTitle;
                button1.Text = "";
                button1.Enabled = false;
                webBrowser1.Visible = false;
                chart1.Visible = false;
                chart2.Visible = false;
                return;
            }
            else if (x is Peak)
            {
                button1.Text = "Click here to view " + ((Peak)x).DisplayName + " in the main window";
                button1.Tag = x;

                webBrowser1.Visible = false;
                chart1.Visible = false;
                chart2.Visible = true;

                var v = (Peak)x;
                _lblTitle.Text = v.DisplayName;
                _lblSubtitle.Text = "Plot of " + v.DisplayName;
                _varChart.Plot(new StylisedPeak(v));
            }
            else if (x is Cluster)
            {
                button1.Text = "Click here to view " + ((Cluster)x).DisplayName + " in the main window";
                button1.Tag = x;

                webBrowser1.Visible = false;
                chart1.Visible = true;
                chart2.Visible = false;

                var p = (Cluster)x;
                _lblTitle.Text = p.DisplayName;
                _lblSubtitle.Text = "Plot of " + p.Assignments.Count + " variables in cluster " + p.DisplayName;
                _patChart.Plot(new StylisedCluster(p));
            }
            else if (x is Compound)
            {
                button1.Text = "Click here to view " + ((Compound)x).Name + " online";

                button1.Tag = x;

                //webBrowser1.Visible = true;
                //pictureBox1.Visible = false;
                //webBrowser1.Navigate(url);

                Compound c = (Compound)x;
                Cluster pp = GetTag<Cluster>(e.Node);
                var fakeCluster = c.CreateStylisedCluster(_core, pp);

                webBrowser1.Visible = false;
                chart1.Visible = true;
                chart2.Visible = false;

                _lblTitle.Text = c.Name;
                _lblSubtitle.Text = "Plot of " + c.Annotations.Select(z => z.Peak).Unique().Count + " variables potentially representing " + c.Name + ".";

                if (pp != null)
                {
                    _lblSubtitle.Text += " Variables in the \"" + pp.DisplayName + "\" cluster are shown in red, others in black";
                }

                _patChart.Plot(fakeCluster);
            }
            else if (x is Pathway)
            {
                button1.Text = "Click here to view " + ((Pathway)x).Name + " online";

                button1.Tag = x;

                /*webBrowser1.Visible = true;
                pictureBox1.Visible = false;
                webBrowser1.Navigate(url);*/

                Pathway p = (Pathway)x;
                var fakeCluster = p.CreateStylisedCluster(_core, null);

                webBrowser1.Visible = false;
                chart1.Visible = true;
                chart2.Visible = false;

                _lblTitle.Text = p.Name;
                _lblSubtitle.Text = "Plot of " + fakeCluster.Cluster.Assignments.Count + " variables potentially representing compounds in the " + p.Name + " pathway.\r\nEach compound or group of compounds are shown in a different colour.";
                _patChart.Plot(fakeCluster);
            }

            button1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IVisualisable x = button1.Tag as IVisualisable;

            if (x == null)
            {
                return;
            }

            if (x is Peak)
            {
                _owner.Activate(x, FrmMain.EActivateOrigin.External);
            }
            else if (x is Cluster)
            {
                _owner.Activate(x, FrmMain.EActivateOrigin.External);
            }
            else if (x is Pathway)
            {
                string url = "http://mediccyc.noble.org/MEDIC/new-image?type=PATHWAY&object=" + ((Pathway)x).Id;
                System.Diagnostics.Process.Start(url);
            }
            else if (x is Compound)
            {
                string url = "http://mediccyc.noble.org/MEDIC/new-image?type=COMPOUND&object=" + ((Compound)x).Id;
                System.Diagnostics.Process.Start(url);
            }
        }

        private void visualOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmOptions.Show(this, "Visual options - " + _core.FileNames.Title, _core.Options);
        }

        private void legendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmViewTypes.Show(this, _core);
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            //var node = treeView1.GetNodeAt(e.X, e.Y);
            //var bounds = node.Bounds;

            //if (e.X < bounds.Left && e.X > bounds.Left - imageList1.ImageSize.Width)
            //{
            //    node.Toggle();
            //}
        }

        private void viewCompoundListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            object x = button1.Tag;

            if (!(x is Pathway))
            {
                return;
            }

            string url = "http://mediccyc.noble.org/apixml?fn=compounds-of-pathway&id=MEDIC:" + ((Compound)x).Id + "&detail=low";
            System.Diagnostics.Process.Start(url);
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && button1.Tag is Pathway)
            {
                contextMenuStrip1.Show(button1, 0, button1.Height);
            }
        }

        private void treeView1_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            if (e.Bounds.Top <= 0 && e.Bounds.Left <= 0)
            {
                return;
            }

            var g = e.Graphics;

            string text = e.Node.Text;
            string text2 = Maths.SplitEquals(ref text, "\t");

            var size = g.MeasureString(text, treeView1.Font);
            g.DrawString(text, treeView1.Font, Brushes.Black, e.Bounds.Left, e.Bounds.Top + e.Bounds.Height / 2 - size.Height / 2);
            g.DrawString(text2, treeView1.Font, Brushes.SteelBlue, e.Bounds.Left + size.Width, e.Bounds.Top + e.Bounds.Height / 2 - size.Height / 2);
        }

        private void findTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string newText = FrmInput.Show(this, "Search", "Find in treeview", "Enter text to search for", _findText);

            if (newText != null)
            {
                // Got text, find next
                _findText = newText;
                findNextToolStripMenuItem_Click(sender, e);
            }
        }

        private void findNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_findText == null)
            {
                // Need text to find
                findTextToolStripMenuItem_Click(sender, e);
                return;
            }

            TreeNode startNode = treeView1.SelectedNode ?? treeView1.Nodes[0];
            TreeNode resultNode;

            if (startNode.Parent != null)
            {
                resultNode = FindNext(startNode.Parent.Nodes, startNode.Index);
            }
            else
            {
                resultNode = FindNext(treeView1.Nodes, startNode.Index);
            }

            if (resultNode == null)
            {
                FrmMsgBox.ShowInfo(this, "Find text", "No matches for text \"" + _findText + "\". Try expanding the tree.");
            }
            else if (resultNode == startNode)
            {
                FrmMsgBox.ShowInfo(this, "Find text", "No more results for text \"" + _findText + "\". Try expanding the tree.");
            }
            else
            {
                treeView1.SelectedNode = resultNode;
            }
        }

        private TreeNode FindNext(TreeNodeCollection toSearch, int startSearch)
        {
            // Search children
            if (startSearch != -1)
            {
                if (toSearch[startSearch].IsExpanded)
                {
                    TreeNode n = FindNext(toSearch[startSearch].Nodes, -1);

                    if (n != null)
                    {
                        return n;
                    }
                }
            }

            // Search nodes after startSearch, then before startSearch, then startSearch itself
            for (int j = 0; j < toSearch.Count; j++)
            {
                int i = (startSearch + j + 1) % toSearch.Count;

                if (toSearch[i].Text.ToUpper().Contains(_findText.ToUpper()))
                {
                    return toSearch[i];
                }

                if (i != startSearch) // searched startSearch's children already
                {
                    if (toSearch[i].IsExpanded) // only search expanded nodes
                    {
                        TreeNode n = FindNext(toSearch[i].Nodes, -1);

                        if (n != null)
                        {
                            return n;
                        }
                    }
                }
            }

            // No result
            return null;
        }
    }
}
