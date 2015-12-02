using MetaboliteLevels.Controls;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Forms
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                UiControls.Dispose(_pathwayList);
                UiControls.Dispose(_peakList);
                UiControls.Dispose(_compoundList);
                UiControls.Dispose(_adductList);
                UiControls.Dispose(_clusterList);

                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this._mnuMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadDataSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.saveExemplarsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSessionAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataInRFormatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportClustersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveClusterImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.printClusterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.visualOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.experimentalGroupsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.clusterBreakdownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.experimentalOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clustererOptimiserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.clusteringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pCAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripSeparator();
            this.editCorrectionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.edittrendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editStatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createclustersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripSeparator();
            this.peakFiltersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.observationFiltersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.autogenerateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this._chartPeak = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this._lblVarName = new System.Windows.Forms.Label();
            this._lstVariables = new System.Windows.Forms.ListView();
            this._imgList = new System.Windows.Forms.ImageList(this.components);
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this._btnGraphVar = new System.Windows.Forms.Button();
            this._btnGraphVar2 = new System.Windows.Forms.Button();
            this._lblVarObservation = new System.Windows.Forms.Label();
            this._lblVarComment = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.quickHelpBar2 = new MetaboliteLevels.Controls.CtlHelpBar();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this._lstClusters = new System.Windows.Forms.ListView();
            this._imgListClusters = new System.Windows.Forms.ImageList(this.components);
            this.quickHelpBar3 = new MetaboliteLevels.Controls.CtlHelpBar();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this._lstCompounds = new System.Windows.Forms.ListView();
            this.quickHelpBar4 = new MetaboliteLevels.Controls.CtlHelpBar();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this._lstAdducts = new System.Windows.Forms.ListView();
            this.quickHelpBar8 = new MetaboliteLevels.Controls.CtlHelpBar();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this._lstPathways = new System.Windows.Forms.ListView();
            this.quickHelpBar5 = new MetaboliteLevels.Controls.CtlHelpBar();
            this.tabPage15 = new System.Windows.Forms.TabPage();
            this._lstAssignments = new System.Windows.Forms.ListView();
            this.ctlHelpBar1 = new MetaboliteLevels.Controls.CtlHelpBar();
            this._tsBarBrowser = new System.Windows.Forms.ToolStrip();
            this._btnMain0 = new System.Windows.Forms.ToolStripButton();
            this._btnMain1 = new System.Windows.Forms.ToolStripButton();
            this._btnMain5 = new System.Windows.Forms.ToolStripButton();
            this._btnMain2 = new System.Windows.Forms.ToolStripButton();
            this._btnMain3 = new System.Windows.Forms.ToolStripButton();
            this._btnMain4 = new System.Windows.Forms.ToolStripButton();
            this.panel5 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this._lblTitle = new System.Windows.Forms.Label();
            this._btnSession = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this._tabSubinfo = new System.Windows.Forms.TabControl();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this._lst2Info = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this._lst2Stats = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage10 = new System.Windows.Forms.TabPage();
            this._lst2Peaks = new System.Windows.Forms.ListView();
            this.tabPage11 = new System.Windows.Forms.TabPage();
            this._lst2Clusters = new System.Windows.Forms.ListView();
            this.tabPage12 = new System.Windows.Forms.TabPage();
            this._lst2Compounds = new System.Windows.Forms.ListView();
            this.tabPage13 = new System.Windows.Forms.TabPage();
            this._lst2Adducts = new System.Windows.Forms.ListView();
            this.tabPage14 = new System.Windows.Forms.TabPage();
            this._lst2Pathways = new System.Windows.Forms.ListView();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this._lst2Assignments = new System.Windows.Forms.ListView();
            this.quickHelpBar7 = new MetaboliteLevels.Controls.CtlHelpBar();
            this._tsBarSelection = new System.Windows.Forms.ToolStrip();
            this._btnBack = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._btnSubInfo = new System.Windows.Forms.ToolStripButton();
            this._btnSubStat = new System.Windows.Forms.ToolStripButton();
            this._btnSubPeak = new System.Windows.Forms.ToolStripButton();
            this._btnSubPat = new System.Windows.Forms.ToolStripButton();
            this._btnSubAss = new System.Windows.Forms.ToolStripButton();
            this._btnSubComp = new System.Windows.Forms.ToolStripButton();
            this._btnSubAdd = new System.Windows.Forms.ToolStripButton();
            this._btnSubPath = new System.Windows.Forms.ToolStripButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._btnSel = new System.Windows.Forms.Button();
            this._lblCurrentSel = new System.Windows.Forms.Label();
            this._btnSel2 = new System.Windows.Forms.Button();
            this._lblSel2 = new System.Windows.Forms.Label();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this._chartPat = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this._lblPatName = new System.Windows.Forms.Label();
            this._btnGraphPat = new System.Windows.Forms.Button();
            this._btnGraphPat2 = new System.Windows.Forms.Button();
            this._lblPatObs = new System.Windows.Forms.Label();
            this._lblPatComment = new System.Windows.Forms.Label();
            this.quickHelpBar1 = new MetaboliteLevels.Controls.CtlHelpBar();
            this._cmsSelectionButton = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addCommentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.viewOnlineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openInDataexplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.breakUpLargeClusterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compareToThisPeakToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._statusMain = new System.Windows.Forms.StatusStrip();
            this._lblChanges = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this._toolTipMain = new System.Windows.Forms.ToolTip(this.components);
            this._cmsCoreButton = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton8 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton9 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton10 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this._tssInsertViews = new System.Windows.Forms.ToolStripSeparator();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.peakidentificationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._btnSubAnnot = new System.Windows.Forms.ToolStripButton();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this._lstSubAnnots = new System.Windows.Forms.ListView();
            this._btnMainAnnots = new System.Windows.Forms.ToolStripButton();
            this.tabPage16 = new System.Windows.Forms.TabPage();
            this._lstAnnotations = new System.Windows.Forms.ListView();
            this._mnuMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._chartPeak)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage15.SuspendLayout();
            this._tsBarBrowser.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this._tabSubinfo.SuspendLayout();
            this.tabPage8.SuspendLayout();
            this.tabPage9.SuspendLayout();
            this.tabPage10.SuspendLayout();
            this.tabPage11.SuspendLayout();
            this.tabPage12.SuspendLayout();
            this.tabPage13.SuspendLayout();
            this.tabPage14.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this._tsBarSelection.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._chartPat)).BeginInit();
            this.tableLayoutPanel6.SuspendLayout();
            this._cmsSelectionButton.SuspendLayout();
            this._statusMain.SuspendLayout();
            this._cmsCoreButton.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.tabPage16.SuspendLayout();
            this.SuspendLayout();
            // 
            // _mnuMain
            // 
            this._mnuMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this._mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.clusteringToolStripMenuItem,
            this.helpToolStripMenuItem});
            this._mnuMain.Location = new System.Drawing.Point(0, 0);
            this._mnuMain.Name = "_mnuMain";
            this._mnuMain.Size = new System.Drawing.Size(1224, 24);
            this._mnuMain.TabIndex = 0;
            this._mnuMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadDataSetToolStripMenuItem,
            this.toolStripMenuItem5,
            this.saveExemplarsToolStripMenuItem,
            this.saveSessionAsToolStripMenuItem,
            this.importToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.toolStripMenuItem8,
            this.printClusterToolStripMenuItem,
            this.toolStripMenuItem4,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // loadDataSetToolStripMenuItem
            // 
            this.loadDataSetToolStripMenuItem.Image = global::MetaboliteLevels.Properties.Resources.MnuOpen;
            this.loadDataSetToolStripMenuItem.Name = "loadDataSetToolStripMenuItem";
            this.loadDataSetToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.loadDataSetToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.loadDataSetToolStripMenuItem.Text = "&Open...";
            this.loadDataSetToolStripMenuItem.Click += new System.EventHandler(this.loadDataSetToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(189, 6);
            // 
            // saveExemplarsToolStripMenuItem
            // 
            this.saveExemplarsToolStripMenuItem.Image = global::MetaboliteLevels.Properties.Resources.MnuSave;
            this.saveExemplarsToolStripMenuItem.Name = "saveExemplarsToolStripMenuItem";
            this.saveExemplarsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveExemplarsToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.saveExemplarsToolStripMenuItem.Text = "&Save ";
            this.saveExemplarsToolStripMenuItem.Click += new System.EventHandler(this.saveExemplarsToolStripMenuItem_Click);
            // 
            // saveSessionAsToolStripMenuItem
            // 
            this.saveSessionAsToolStripMenuItem.Name = "saveSessionAsToolStripMenuItem";
            this.saveSessionAsToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.saveSessionAsToolStripMenuItem.Text = "&Save as...";
            this.saveSessionAsToolStripMenuItem.Click += new System.EventHandler(this.saveSessionAsToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dataInRFormatToolStripMenuItem,
            this.exportClustersToolStripMenuItem,
            this.saveClusterImageToolStripMenuItem});
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.exportToolStripMenuItem.Text = "&Export";
            // 
            // dataInRFormatToolStripMenuItem
            // 
            this.dataInRFormatToolStripMenuItem.Image = global::MetaboliteLevels.Properties.Resources.MnuR;
            this.dataInRFormatToolStripMenuItem.Name = "dataInRFormatToolStripMenuItem";
            this.dataInRFormatToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.dataInRFormatToolStripMenuItem.Text = "&RData (all data)...";
            this.dataInRFormatToolStripMenuItem.Click += new System.EventHandler(this.dataInRFormatToolStripMenuItem_Click);
            // 
            // exportClustersToolStripMenuItem
            // 
            this.exportClustersToolStripMenuItem.Name = "exportClustersToolStripMenuItem";
            this.exportClustersToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.exportClustersToolStripMenuItem.Text = "&CSV (cluster assignments)...";
            this.exportClustersToolStripMenuItem.Click += new System.EventHandler(this.exportClustersToolStripMenuItem_Click);
            // 
            // saveClusterImageToolStripMenuItem
            // 
            this.saveClusterImageToolStripMenuItem.Name = "saveClusterImageToolStripMenuItem";
            this.saveClusterImageToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.saveClusterImageToolStripMenuItem.Text = "&PNG (all cluster plots)...";
            this.saveClusterImageToolStripMenuItem.Click += new System.EventHandler(this.saveClusterImageToolStripMenuItem_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(189, 6);
            // 
            // printClusterToolStripMenuItem
            // 
            this.printClusterToolStripMenuItem.Image = global::MetaboliteLevels.Properties.Resources.MnuPrint;
            this.printClusterToolStripMenuItem.Name = "printClusterToolStripMenuItem";
            this.printClusterToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.printClusterToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.printClusterToolStripMenuItem.Text = "&Print clusters...";
            this.printClusterToolStripMenuItem.Click += new System.EventHandler(this.printClusterToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(189, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem,
            this.visualOptionsToolStripMenuItem,
            this.experimentalGroupsToolStripMenuItem,
            this.toolStripMenuItem1,
            this.clusterBreakdownToolStripMenuItem,
            this.experimentalOptionsToolStripMenuItem,
            this.clustererOptimiserToolStripMenuItem,
            this.toolStripMenuItem6});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Image = global::MetaboliteLevels.Properties.Resources.MnuRefresh;
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F5)));
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.refreshToolStripMenuItem.Text = "&Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // visualOptionsToolStripMenuItem
            // 
            this.visualOptionsToolStripMenuItem.Image = global::MetaboliteLevels.Properties.Resources.MnuPreferences;
            this.visualOptionsToolStripMenuItem.Name = "visualOptionsToolStripMenuItem";
            this.visualOptionsToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.visualOptionsToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.visualOptionsToolStripMenuItem.Text = "&Preferences...";
            this.visualOptionsToolStripMenuItem.Click += new System.EventHandler(this.visualOptionsToolStripMenuItem_Click);
            // 
            // experimentalGroupsToolStripMenuItem
            // 
            this.experimentalGroupsToolStripMenuItem.Image = global::MetaboliteLevels.Properties.Resources.type_16xLG;
            this.experimentalGroupsToolStripMenuItem.Name = "experimentalGroupsToolStripMenuItem";
            this.experimentalGroupsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.experimentalGroupsToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.experimentalGroupsToolStripMenuItem.Text = "&Experimental groups...";
            this.experimentalGroupsToolStripMenuItem.Click += new System.EventHandler(this.experimentalGroupsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(228, 6);
            // 
            // clusterBreakdownToolStripMenuItem
            // 
            this.clusterBreakdownToolStripMenuItem.Name = "clusterBreakdownToolStripMenuItem";
            this.clusterBreakdownToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.clusterBreakdownToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.clusterBreakdownToolStripMenuItem.Text = "&Data explorer...";
            this.clusterBreakdownToolStripMenuItem.Click += new System.EventHandler(this.clusterBreakdownToolStripMenuItem_Click);
            // 
            // experimentalOptionsToolStripMenuItem
            // 
            this.experimentalOptionsToolStripMenuItem.Name = "experimentalOptionsToolStripMenuItem";
            this.experimentalOptionsToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.experimentalOptionsToolStripMenuItem.Text = "&Advanced options...";
            this.experimentalOptionsToolStripMenuItem.Click += new System.EventHandler(this.experimentalOptionsToolStripMenuItem_Click);
            // 
            // clustererOptimiserToolStripMenuItem
            // 
            this.clustererOptimiserToolStripMenuItem.Name = "clustererOptimiserToolStripMenuItem";
            this.clustererOptimiserToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.clustererOptimiserToolStripMenuItem.Text = "&Clusterer optimiser...";
            this.clustererOptimiserToolStripMenuItem.Click += new System.EventHandler(this.clustererOptimiserToolStripMenuItem_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(228, 6);
            // 
            // clusteringToolStripMenuItem
            // 
            this.clusteringToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pCAToolStripMenuItem,
            this.toolStripMenuItem10,
            this.editCorrectionsToolStripMenuItem,
            this.edittrendToolStripMenuItem,
            this.editStatisticsToolStripMenuItem,
            this.createclustersToolStripMenuItem,
            this.toolStripMenuItem9,
            this.peakFiltersToolStripMenuItem,
            this.observationFiltersToolStripMenuItem,
            this.toolStripMenuItem2,
            this.autogenerateToolStripMenuItem});
            this.clusteringToolStripMenuItem.Name = "clusteringToolStripMenuItem";
            this.clusteringToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.clusteringToolStripMenuItem.Text = "&Workflow";
            // 
            // pCAToolStripMenuItem
            // 
            this.pCAToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pCAToolStripMenuItem.Image")));
            this.pCAToolStripMenuItem.Name = "pCAToolStripMenuItem";
            this.pCAToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.P)));
            this.pCAToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.pCAToolStripMenuItem.Text = "&PCA";
            this.pCAToolStripMenuItem.Click += new System.EventHandler(this.pCAToolStripMenuItem_Click);
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(228, 6);
            // 
            // editCorrectionsToolStripMenuItem
            // 
            this.editCorrectionsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("editCorrectionsToolStripMenuItem.Image")));
            this.editCorrectionsToolStripMenuItem.Name = "editCorrectionsToolStripMenuItem";
            this.editCorrectionsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
            this.editCorrectionsToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.editCorrectionsToolStripMenuItem.Text = "&Corrections...";
            this.editCorrectionsToolStripMenuItem.Click += new System.EventHandler(this.editCorrectionsToolStripMenuItem_Click);
            // 
            // edittrendToolStripMenuItem
            // 
            this.edittrendToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("edittrendToolStripMenuItem.Image")));
            this.edittrendToolStripMenuItem.Name = "edittrendToolStripMenuItem";
            this.edittrendToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
            this.edittrendToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.edittrendToolStripMenuItem.Text = "&Trends...";
            this.edittrendToolStripMenuItem.Click += new System.EventHandler(this.edittrendToolStripMenuItem_Click);
            // 
            // editStatisticsToolStripMenuItem
            // 
            this.editStatisticsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("editStatisticsToolStripMenuItem.Image")));
            this.editStatisticsToolStripMenuItem.Name = "editStatisticsToolStripMenuItem";
            this.editStatisticsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D3)));
            this.editStatisticsToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.editStatisticsToolStripMenuItem.Text = "&Statistics...";
            this.editStatisticsToolStripMenuItem.Click += new System.EventHandler(this.editStatisticsToolStripMenuItem_Click);
            // 
            // createclustersToolStripMenuItem
            // 
            this.createclustersToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("createclustersToolStripMenuItem.Image")));
            this.createclustersToolStripMenuItem.Name = "createclustersToolStripMenuItem";
            this.createclustersToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D4)));
            this.createclustersToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.createclustersToolStripMenuItem.Text = "&Clusters...";
            this.createclustersToolStripMenuItem.Click += new System.EventHandler(this.createclustersToolStripMenuItem_Click);
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(228, 6);
            // 
            // peakFiltersToolStripMenuItem
            // 
            this.peakFiltersToolStripMenuItem.Name = "peakFiltersToolStripMenuItem";
            this.peakFiltersToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D5)));
            this.peakFiltersToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.peakFiltersToolStripMenuItem.Text = "&Peak filters...";
            this.peakFiltersToolStripMenuItem.Click += new System.EventHandler(this.peakFiltersToolStripMenuItem_Click);
            // 
            // observationFiltersToolStripMenuItem
            // 
            this.observationFiltersToolStripMenuItem.Name = "observationFiltersToolStripMenuItem";
            this.observationFiltersToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D6)));
            this.observationFiltersToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.observationFiltersToolStripMenuItem.Text = "&Observation filters...";
            this.observationFiltersToolStripMenuItem.Click += new System.EventHandler(this.observationFiltersToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(228, 6);
            // 
            // autogenerateToolStripMenuItem
            // 
            this.autogenerateToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("autogenerateToolStripMenuItem.Image")));
            this.autogenerateToolStripMenuItem.Name = "autogenerateToolStripMenuItem";
            this.autogenerateToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.C)));
            this.autogenerateToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.autogenerateToolStripMenuItem.Text = "&d-k-means++...";
            this.autogenerateToolStripMenuItem.Click += new System.EventHandler(this.autogenerateToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.aboutToolStripMenuItem1});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Image = global::MetaboliteLevels.Properties.Resources.MnuSessionInfo;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F2)));
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.aboutToolStripMenuItem.Text = "&Session information";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(225, 22);
            this.aboutToolStripMenuItem1.Text = "&About";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // _chartPeak
            // 
            this._chartPeak.BorderSkin.BorderColor = System.Drawing.Color.Gray;
            chartArea1.Name = "ChartArea1";
            this._chartPeak.ChartAreas.Add(chartArea1);
            this._chartPeak.Cursor = System.Windows.Forms.Cursors.Cross;
            this._chartPeak.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this._chartPeak.Legends.Add(legend1);
            this._chartPeak.Location = new System.Drawing.Point(3, 49);
            this._chartPeak.Name = "_chartPeak";
            this._chartPeak.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            this._chartPeak.PaletteCustomColors = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128))))),
        System.Drawing.Color.Blue,
        System.Drawing.Color.Red,
        System.Drawing.Color.Green,
        System.Drawing.Color.Olive};
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series1.YValuesPerPoint = 4;
            this._chartPeak.Series.Add(series1);
            this._chartPeak.Size = new System.Drawing.Size(772, 249);
            this._chartPeak.TabIndex = 2;
            this._chartPeak.Text = "chart1";
            // 
            // _lblVarName
            // 
            this._lblVarName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._lblVarName.AutoSize = true;
            this._lblVarName.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblVarName.Location = new System.Drawing.Point(39, 5);
            this._lblVarName.Name = "_lblVarName";
            this._lblVarName.Size = new System.Drawing.Size(124, 30);
            this._lblVarName.TabIndex = 4;
            this._lblVarName.Text = "Peak Name";
            // 
            // _lstVariables
            // 
            this._lstVariables.AllowColumnReorder = true;
            this._lstVariables.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lstVariables.FullRowSelect = true;
            this._lstVariables.GridLines = true;
            this._lstVariables.LargeImageList = this._imgList;
            this._lstVariables.Location = new System.Drawing.Point(3, 78);
            this._lstVariables.MultiSelect = false;
            this._lstVariables.Name = "_lstVariables";
            this._lstVariables.Size = new System.Drawing.Size(428, 147);
            this._lstVariables.SmallImageList = this._imgList;
            this._lstVariables.TabIndex = 5;
            this._lstVariables.UseCompatibleStateImageBehavior = false;
            this._lstVariables.View = System.Windows.Forms.View.Details;
            this._lstVariables.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._lstVariables_KeyPress);
            // 
            // _imgList
            // 
            this._imgList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this._imgList.ImageSize = new System.Drawing.Size(24, 24);
            this._imgList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this._chartPeak, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(778, 301);
            this.tableLayoutPanel2.TabIndex = 9;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.CornflowerBlue;
            this.tableLayoutPanel3.ColumnCount = 5;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this._lblVarName, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this._btnGraphVar, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this._btnGraphVar2, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this._lblVarObservation, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this._lblVarComment, 2, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel3.ForeColor = System.Drawing.Color.White;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(772, 40);
            this.tableLayoutPanel3.TabIndex = 5;
            // 
            // _btnGraphVar
            // 
            this._btnGraphVar.AutoSize = true;
            this._btnGraphVar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._btnGraphVar.BackColor = System.Drawing.Color.White;
            this._btnGraphVar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this._btnGraphVar.Image = ((System.Drawing.Image)(resources.GetObject("_btnGraphVar.Image")));
            this._btnGraphVar.Location = new System.Drawing.Point(3, 3);
            this._btnGraphVar.Name = "_btnGraphVar";
            this._btnGraphVar.Size = new System.Drawing.Size(30, 30);
            this._btnGraphVar.TabIndex = 2;
            this._toolTipMain.SetToolTip(this._btnGraphVar, "Show peak plot options");
            this._btnGraphVar.UseVisualStyleBackColor = false;
            // 
            // _btnGraphVar2
            // 
            this._btnGraphVar2.AutoSize = true;
            this._btnGraphVar2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._btnGraphVar2.BackColor = System.Drawing.Color.White;
            this._btnGraphVar2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this._btnGraphVar2.Image = ((System.Drawing.Image)(resources.GetObject("_btnGraphVar2.Image")));
            this._btnGraphVar2.Location = new System.Drawing.Point(739, 3);
            this._btnGraphVar2.Name = "_btnGraphVar2";
            this._btnGraphVar2.Size = new System.Drawing.Size(30, 30);
            this._btnGraphVar2.TabIndex = 2;
            this._btnGraphVar2.UseVisualStyleBackColor = false;
            // 
            // _lblVarObservation
            // 
            this._lblVarObservation.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this._lblVarObservation.AutoSize = true;
            this._lblVarObservation.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblVarObservation.Location = new System.Drawing.Point(633, 9);
            this._lblVarObservation.Margin = new System.Windows.Forms.Padding(4);
            this._lblVarObservation.Name = "_lblVarObservation";
            this._lblVarObservation.Size = new System.Drawing.Size(99, 21);
            this._lblVarObservation.TabIndex = 7;
            this._lblVarObservation.Text = "Observation";
            // 
            // _lblVarComment
            // 
            this._lblVarComment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._lblVarComment.AutoSize = true;
            this._lblVarComment.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblVarComment.Location = new System.Drawing.Point(169, 21);
            this._lblVarComment.Margin = new System.Windows.Forms.Padding(3);
            this._lblVarComment.Name = "_lblVarComment";
            this._lblVarComment.Size = new System.Drawing.Size(69, 16);
            this._lblVarComment.TabIndex = 6;
            this._lblVarComment.Text = "User defined";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 70);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Panel2.Controls.Add(this.quickHelpBar1);
            this.splitContainer1.Panel2.ForeColor = System.Drawing.Color.Black;
            this.splitContainer1.Size = new System.Drawing.Size(1224, 677);
            this.splitContainer1.SplitterDistance = 442;
            this.splitContainer1.TabIndex = 5;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.panel3);
            this.splitContainer2.Panel1.Controls.Add(this._tsBarBrowser);
            this.splitContainer2.Panel1.Controls.Add(this.panel5);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.panel1);
            this.splitContainer2.Size = new System.Drawing.Size(442, 677);
            this.splitContainer2.SplitterDistance = 336;
            this.splitContainer2.TabIndex = 11;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tabControl1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 82);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(442, 254);
            this.panel3.TabIndex = 11;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage15);
            this.tabControl1.Controls.Add(this.tabPage16);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(442, 254);
            this.tabControl1.TabIndex = 10;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this._lstVariables);
            this.tabPage1.Controls.Add(this.quickHelpBar2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(434, 228);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Peaks";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // quickHelpBar2
            // 
            this.quickHelpBar2.AutoSize = true;
            this.quickHelpBar2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.quickHelpBar2.Dock = System.Windows.Forms.DockStyle.Top;
            this.quickHelpBar2.Location = new System.Drawing.Point(3, 3);
            this.quickHelpBar2.MinimumSize = new System.Drawing.Size(128, 64);
            this.quickHelpBar2.Name = "quickHelpBar2";
            this.quickHelpBar2.Padding = new System.Windows.Forms.Padding(8);
            this.quickHelpBar2.Size = new System.Drawing.Size(428, 75);
            this.quickHelpBar2.TabIndex = 12;
            this.quickHelpBar2.Text = "Double-click on peaks to select them and display their information. You can sort " +
    "the list by clicking the headers.";
            this.quickHelpBar2.Title = "Peaks";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this._lstClusters);
            this.tabPage2.Controls.Add(this.quickHelpBar3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(434, 228);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Clusters";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // _lstClusters
            // 
            this._lstClusters.AllowColumnReorder = true;
            this._lstClusters.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lstClusters.FullRowSelect = true;
            this._lstClusters.GridLines = true;
            this._lstClusters.LargeImageList = this._imgListClusters;
            this._lstClusters.Location = new System.Drawing.Point(3, 67);
            this._lstClusters.MultiSelect = false;
            this._lstClusters.Name = "_lstClusters";
            this._lstClusters.Size = new System.Drawing.Size(428, 158);
            this._lstClusters.SmallImageList = this._imgList;
            this._lstClusters.TabIndex = 5;
            this._lstClusters.UseCompatibleStateImageBehavior = false;
            this._lstClusters.View = System.Windows.Forms.View.Details;
            // 
            // _imgListClusters
            // 
            this._imgListClusters.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this._imgListClusters.ImageSize = new System.Drawing.Size(24, 24);
            this._imgListClusters.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // quickHelpBar3
            // 
            this.quickHelpBar3.AutoSize = true;
            this.quickHelpBar3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.quickHelpBar3.Dock = System.Windows.Forms.DockStyle.Top;
            this.quickHelpBar3.Location = new System.Drawing.Point(3, 3);
            this.quickHelpBar3.MinimumSize = new System.Drawing.Size(128, 64);
            this.quickHelpBar3.Name = "quickHelpBar3";
            this.quickHelpBar3.Padding = new System.Windows.Forms.Padding(8);
            this.quickHelpBar3.Size = new System.Drawing.Size(428, 64);
            this.quickHelpBar3.TabIndex = 13;
            this.quickHelpBar3.Text = "You can assign peaks to clusters from the clustering menu.";
            this.quickHelpBar3.Title = "Clusters";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this._lstCompounds);
            this.tabPage3.Controls.Add(this.quickHelpBar4);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(434, 228);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Compounds";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // _lstCompounds
            // 
            this._lstCompounds.AllowColumnReorder = true;
            this._lstCompounds.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lstCompounds.FullRowSelect = true;
            this._lstCompounds.GridLines = true;
            this._lstCompounds.LargeImageList = this._imgList;
            this._lstCompounds.Location = new System.Drawing.Point(3, 78);
            this._lstCompounds.MultiSelect = false;
            this._lstCompounds.Name = "_lstCompounds";
            this._lstCompounds.Size = new System.Drawing.Size(428, 147);
            this._lstCompounds.SmallImageList = this._imgList;
            this._lstCompounds.TabIndex = 6;
            this._lstCompounds.UseCompatibleStateImageBehavior = false;
            this._lstCompounds.View = System.Windows.Forms.View.Details;
            // 
            // quickHelpBar4
            // 
            this.quickHelpBar4.AutoSize = true;
            this.quickHelpBar4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.quickHelpBar4.Dock = System.Windows.Forms.DockStyle.Top;
            this.quickHelpBar4.Location = new System.Drawing.Point(3, 3);
            this.quickHelpBar4.MinimumSize = new System.Drawing.Size(128, 64);
            this.quickHelpBar4.Name = "quickHelpBar4";
            this.quickHelpBar4.Padding = new System.Windows.Forms.Padding(8);
            this.quickHelpBar4.Size = new System.Drawing.Size(428, 75);
            this.quickHelpBar4.TabIndex = 14;
            this.quickHelpBar4.Text = "Selecting (double click) compounds displays the graph of the variables potentiall" +
    "y representing them.";
            this.quickHelpBar4.Title = "Compounds";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this._lstAdducts);
            this.tabPage4.Controls.Add(this.quickHelpBar8);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(434, 228);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Adducts";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // _lstAdducts
            // 
            this._lstAdducts.AllowColumnReorder = true;
            this._lstAdducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lstAdducts.FullRowSelect = true;
            this._lstAdducts.GridLines = true;
            this._lstAdducts.LargeImageList = this._imgList;
            this._lstAdducts.Location = new System.Drawing.Point(3, 67);
            this._lstAdducts.MultiSelect = false;
            this._lstAdducts.Name = "_lstAdducts";
            this._lstAdducts.Size = new System.Drawing.Size(428, 158);
            this._lstAdducts.SmallImageList = this._imgList;
            this._lstAdducts.TabIndex = 6;
            this._lstAdducts.UseCompatibleStateImageBehavior = false;
            this._lstAdducts.View = System.Windows.Forms.View.Details;
            // 
            // quickHelpBar8
            // 
            this.quickHelpBar8.AutoSize = true;
            this.quickHelpBar8.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.quickHelpBar8.Dock = System.Windows.Forms.DockStyle.Top;
            this.quickHelpBar8.Location = new System.Drawing.Point(3, 3);
            this.quickHelpBar8.MinimumSize = new System.Drawing.Size(128, 64);
            this.quickHelpBar8.Name = "quickHelpBar8";
            this.quickHelpBar8.Padding = new System.Windows.Forms.Padding(8);
            this.quickHelpBar8.Size = new System.Drawing.Size(428, 64);
            this.quickHelpBar8.TabIndex = 13;
            this.quickHelpBar8.Text = "Click on adducts to view their information";
            this.quickHelpBar8.Title = "Adducts";
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this._lstPathways);
            this.tabPage5.Controls.Add(this.quickHelpBar5);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(434, 228);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Pathways";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // _lstPathways
            // 
            this._lstPathways.AllowColumnReorder = true;
            this._lstPathways.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lstPathways.FullRowSelect = true;
            this._lstPathways.GridLines = true;
            this._lstPathways.LargeImageList = this._imgList;
            this._lstPathways.Location = new System.Drawing.Point(3, 78);
            this._lstPathways.MultiSelect = false;
            this._lstPathways.Name = "_lstPathways";
            this._lstPathways.Size = new System.Drawing.Size(428, 147);
            this._lstPathways.SmallImageList = this._imgList;
            this._lstPathways.TabIndex = 6;
            this._lstPathways.UseCompatibleStateImageBehavior = false;
            this._lstPathways.View = System.Windows.Forms.View.Details;
            // 
            // quickHelpBar5
            // 
            this.quickHelpBar5.AutoSize = true;
            this.quickHelpBar5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.quickHelpBar5.Dock = System.Windows.Forms.DockStyle.Top;
            this.quickHelpBar5.Location = new System.Drawing.Point(3, 3);
            this.quickHelpBar5.MinimumSize = new System.Drawing.Size(128, 64);
            this.quickHelpBar5.Name = "quickHelpBar5";
            this.quickHelpBar5.Padding = new System.Windows.Forms.Padding(8);
            this.quickHelpBar5.Size = new System.Drawing.Size(428, 75);
            this.quickHelpBar5.TabIndex = 15;
            this.quickHelpBar5.Text = "Selecting (double click) pathways displays the graph of the variables potentially" +
    " representing the compounds within them.";
            this.quickHelpBar5.Title = "Pathways";
            // 
            // tabPage15
            // 
            this.tabPage15.Controls.Add(this._lstAssignments);
            this.tabPage15.Controls.Add(this.ctlHelpBar1);
            this.tabPage15.Location = new System.Drawing.Point(4, 22);
            this.tabPage15.Name = "tabPage15";
            this.tabPage15.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage15.Size = new System.Drawing.Size(434, 228);
            this.tabPage15.TabIndex = 5;
            this.tabPage15.Text = "Assignments";
            this.tabPage15.UseVisualStyleBackColor = true;
            // 
            // _lstAssignments
            // 
            this._lstAssignments.AllowColumnReorder = true;
            this._lstAssignments.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lstAssignments.FullRowSelect = true;
            this._lstAssignments.GridLines = true;
            this._lstAssignments.LargeImageList = this._imgList;
            this._lstAssignments.Location = new System.Drawing.Point(3, 78);
            this._lstAssignments.MultiSelect = false;
            this._lstAssignments.Name = "_lstAssignments";
            this._lstAssignments.Size = new System.Drawing.Size(428, 147);
            this._lstAssignments.SmallImageList = this._imgList;
            this._lstAssignments.TabIndex = 17;
            this._lstAssignments.UseCompatibleStateImageBehavior = false;
            this._lstAssignments.View = System.Windows.Forms.View.Details;
            // 
            // ctlHelpBar1
            // 
            this.ctlHelpBar1.AutoSize = true;
            this.ctlHelpBar1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ctlHelpBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlHelpBar1.Location = new System.Drawing.Point(3, 3);
            this.ctlHelpBar1.MinimumSize = new System.Drawing.Size(128, 64);
            this.ctlHelpBar1.Name = "ctlHelpBar1";
            this.ctlHelpBar1.Padding = new System.Windows.Forms.Padding(8);
            this.ctlHelpBar1.Size = new System.Drawing.Size(428, 75);
            this.ctlHelpBar1.TabIndex = 18;
            this.ctlHelpBar1.Text = "Shows the assignments of vectors (peaks and groups) to clusters.";
            this.ctlHelpBar1.Title = "Assignments";
            // 
            // _tsBarBrowser
            // 
            this._tsBarBrowser.BackColor = System.Drawing.Color.CornflowerBlue;
            this._tsBarBrowser.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._tsBarBrowser.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._tsBarBrowser.ImageScalingSize = new System.Drawing.Size(24, 24);
            this._tsBarBrowser.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._btnMain0,
            this._btnMain1,
            this._btnMain5,
            this._btnMainAnnots,
            this._btnMain2,
            this._btnMain3,
            this._btnMain4});
            this._tsBarBrowser.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this._tsBarBrowser.Location = new System.Drawing.Point(0, 36);
            this._tsBarBrowser.Name = "_tsBarBrowser";
            this._tsBarBrowser.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this._tsBarBrowser.Size = new System.Drawing.Size(442, 46);
            this._tsBarBrowser.TabIndex = 19;
            this._tsBarBrowser.Text = "toolStrip7";
            // 
            // _btnMain0
            // 
            this._btnMain0.ForeColor = System.Drawing.Color.White;
            this._btnMain0.Image = global::MetaboliteLevels.Properties.Resources.ObjLVariableU;
            this._btnMain0.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnMain0.Name = "_btnMain0";
            this._btnMain0.Size = new System.Drawing.Size(37, 43);
            this._btnMain0.Text = "Peaks";
            this._btnMain0.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // _btnMain1
            // 
            this._btnMain1.ForeColor = System.Drawing.Color.White;
            this._btnMain1.Image = global::MetaboliteLevels.Properties.Resources.ObjLCluster;
            this._btnMain1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnMain1.Name = "_btnMain1";
            this._btnMain1.Size = new System.Drawing.Size(49, 43);
            this._btnMain1.Text = "Clusters";
            this._btnMain1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // _btnMain5
            // 
            this._btnMain5.ForeColor = System.Drawing.Color.White;
            this._btnMain5.Image = global::MetaboliteLevels.Properties.Resources.ObjLAssignment;
            this._btnMain5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnMain5.Name = "_btnMain5";
            this._btnMain5.Size = new System.Drawing.Size(42, 43);
            this._btnMain5.Text = "Assigs";
            this._btnMain5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // _btnMain2
            // 
            this._btnMain2.ForeColor = System.Drawing.Color.White;
            this._btnMain2.Image = global::MetaboliteLevels.Properties.Resources.ObjLCompoundU;
            this._btnMain2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnMain2.Name = "_btnMain2";
            this._btnMain2.Size = new System.Drawing.Size(44, 43);
            this._btnMain2.Text = "Comps";
            this._btnMain2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // _btnMain3
            // 
            this._btnMain3.ForeColor = System.Drawing.Color.White;
            this._btnMain3.Image = global::MetaboliteLevels.Properties.Resources.ObjLAdduct;
            this._btnMain3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnMain3.Name = "_btnMain3";
            this._btnMain3.Size = new System.Drawing.Size(49, 43);
            this._btnMain3.Text = "Adducts";
            this._btnMain3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // _btnMain4
            // 
            this._btnMain4.ForeColor = System.Drawing.Color.White;
            this._btnMain4.Image = global::MetaboliteLevels.Properties.Resources.ObjLPathway;
            this._btnMain4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnMain4.Name = "_btnMain4";
            this._btnMain4.Size = new System.Drawing.Size(36, 43);
            this._btnMain4.Text = "Paths";
            this._btnMain4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // panel5
            // 
            this.panel5.AutoSize = true;
            this.panel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel5.BackColor = System.Drawing.Color.CornflowerBlue;
            this.panel5.Controls.Add(this.tableLayoutPanel4);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.ForeColor = System.Drawing.Color.White;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Margin = new System.Windows.Forms.Padding(0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(442, 36);
            this.panel5.TabIndex = 20;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this._lblTitle, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this._btnSession, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(442, 36);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // _lblTitle
            // 
            this._lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._lblTitle.AutoSize = true;
            this._lblTitle.Font = new System.Drawing.Font("Segoe UI", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblTitle.Location = new System.Drawing.Point(39, 3);
            this._lblTitle.Name = "_lblTitle";
            this._lblTitle.Size = new System.Drawing.Size(85, 30);
            this._lblTitle.TabIndex = 1;
            this._lblTitle.Text = "Session";
            this._lblTitle.Click += new System.EventHandler(this._btnSession_Click);
            // 
            // _btnSession
            // 
            this._btnSession.AutoSize = true;
            this._btnSession.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._btnSession.BackColor = System.Drawing.Color.White;
            this._btnSession.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this._btnSession.Image = global::MetaboliteLevels.Properties.Resources.ObjLCore;
            this._btnSession.Location = new System.Drawing.Point(3, 3);
            this._btnSession.Name = "_btnSession";
            this._btnSession.Size = new System.Drawing.Size(30, 30);
            this._btnSession.TabIndex = 2;
            this._toolTipMain.SetToolTip(this._btnSession, "Show session editing options");
            this._btnSession.UseVisualStyleBackColor = false;
            this._btnSession.Click += new System.EventHandler(this._btnSession_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.quickHelpBar7);
            this.panel1.Controls.Add(this._tsBarSelection);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(442, 337);
            this.panel1.TabIndex = 7;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this._tabSubinfo);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 146);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(442, 191);
            this.panel4.TabIndex = 21;
            // 
            // _tabSubinfo
            // 
            this._tabSubinfo.Controls.Add(this.tabPage8);
            this._tabSubinfo.Controls.Add(this.tabPage9);
            this._tabSubinfo.Controls.Add(this.tabPage10);
            this._tabSubinfo.Controls.Add(this.tabPage11);
            this._tabSubinfo.Controls.Add(this.tabPage12);
            this._tabSubinfo.Controls.Add(this.tabPage13);
            this._tabSubinfo.Controls.Add(this.tabPage14);
            this._tabSubinfo.Controls.Add(this.tabPage6);
            this._tabSubinfo.Controls.Add(this.tabPage7);
            this._tabSubinfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tabSubinfo.Location = new System.Drawing.Point(0, 0);
            this._tabSubinfo.Multiline = true;
            this._tabSubinfo.Name = "_tabSubinfo";
            this._tabSubinfo.SelectedIndex = 0;
            this._tabSubinfo.Size = new System.Drawing.Size(442, 191);
            this._tabSubinfo.TabIndex = 20;
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this._lst2Info);
            this.tabPage8.Location = new System.Drawing.Point(4, 40);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage8.Size = new System.Drawing.Size(434, 147);
            this.tabPage8.TabIndex = 1;
            this.tabPage8.Text = "Information";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // _lst2Info
            // 
            this._lst2Info.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this._lst2Info.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lst2Info.GridLines = true;
            this._lst2Info.LargeImageList = this._imgList;
            this._lst2Info.Location = new System.Drawing.Point(3, 3);
            this._lst2Info.Name = "_lst2Info";
            this._lst2Info.Size = new System.Drawing.Size(428, 141);
            this._lst2Info.SmallImageList = this._imgList;
            this._lst2Info.TabIndex = 0;
            this._lst2Info.UseCompatibleStateImageBehavior = false;
            this._lst2Info.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Field";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Value";
            // 
            // tabPage9
            // 
            this.tabPage9.Controls.Add(this._lst2Stats);
            this.tabPage9.Location = new System.Drawing.Point(4, 40);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage9.Size = new System.Drawing.Size(434, 147);
            this.tabPage9.TabIndex = 2;
            this.tabPage9.Text = "Statistics";
            this.tabPage9.UseVisualStyleBackColor = true;
            // 
            // _lst2Stats
            // 
            this._lst2Stats.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
            this._lst2Stats.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lst2Stats.GridLines = true;
            this._lst2Stats.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this._lst2Stats.LargeImageList = this._imgList;
            this._lst2Stats.Location = new System.Drawing.Point(3, 3);
            this._lst2Stats.Name = "_lst2Stats";
            this._lst2Stats.Size = new System.Drawing.Size(428, 141);
            this._lst2Stats.SmallImageList = this._imgList;
            this._lst2Stats.TabIndex = 1;
            this._lst2Stats.UseCompatibleStateImageBehavior = false;
            this._lst2Stats.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Field";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Value";
            // 
            // tabPage10
            // 
            this.tabPage10.Controls.Add(this._lst2Peaks);
            this.tabPage10.Location = new System.Drawing.Point(4, 40);
            this.tabPage10.Name = "tabPage10";
            this.tabPage10.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage10.Size = new System.Drawing.Size(434, 147);
            this.tabPage10.TabIndex = 3;
            this.tabPage10.Text = "Peaks";
            this.tabPage10.UseVisualStyleBackColor = true;
            // 
            // _lst2Peaks
            // 
            this._lst2Peaks.AllowColumnReorder = true;
            this._lst2Peaks.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lst2Peaks.FullRowSelect = true;
            this._lst2Peaks.GridLines = true;
            this._lst2Peaks.LargeImageList = this._imgList;
            this._lst2Peaks.Location = new System.Drawing.Point(3, 3);
            this._lst2Peaks.MultiSelect = false;
            this._lst2Peaks.Name = "_lst2Peaks";
            this._lst2Peaks.Size = new System.Drawing.Size(428, 141);
            this._lst2Peaks.SmallImageList = this._imgList;
            this._lst2Peaks.TabIndex = 6;
            this._lst2Peaks.UseCompatibleStateImageBehavior = false;
            this._lst2Peaks.View = System.Windows.Forms.View.Details;
            // 
            // tabPage11
            // 
            this.tabPage11.Controls.Add(this._lst2Clusters);
            this.tabPage11.Location = new System.Drawing.Point(4, 40);
            this.tabPage11.Name = "tabPage11";
            this.tabPage11.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage11.Size = new System.Drawing.Size(434, 147);
            this.tabPage11.TabIndex = 4;
            this.tabPage11.Text = "Clusters";
            this.tabPage11.UseVisualStyleBackColor = true;
            // 
            // _lst2Clusters
            // 
            this._lst2Clusters.AllowColumnReorder = true;
            this._lst2Clusters.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lst2Clusters.FullRowSelect = true;
            this._lst2Clusters.GridLines = true;
            this._lst2Clusters.LargeImageList = this._imgList;
            this._lst2Clusters.Location = new System.Drawing.Point(3, 3);
            this._lst2Clusters.MultiSelect = false;
            this._lst2Clusters.Name = "_lst2Clusters";
            this._lst2Clusters.Size = new System.Drawing.Size(428, 141);
            this._lst2Clusters.SmallImageList = this._imgList;
            this._lst2Clusters.TabIndex = 6;
            this._lst2Clusters.UseCompatibleStateImageBehavior = false;
            this._lst2Clusters.View = System.Windows.Forms.View.Details;
            // 
            // tabPage12
            // 
            this.tabPage12.Controls.Add(this._lst2Compounds);
            this.tabPage12.Location = new System.Drawing.Point(4, 40);
            this.tabPage12.Name = "tabPage12";
            this.tabPage12.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage12.Size = new System.Drawing.Size(434, 147);
            this.tabPage12.TabIndex = 5;
            this.tabPage12.Text = "Compounds";
            this.tabPage12.UseVisualStyleBackColor = true;
            // 
            // _lst2Compounds
            // 
            this._lst2Compounds.AllowColumnReorder = true;
            this._lst2Compounds.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lst2Compounds.FullRowSelect = true;
            this._lst2Compounds.GridLines = true;
            this._lst2Compounds.LargeImageList = this._imgList;
            this._lst2Compounds.Location = new System.Drawing.Point(3, 3);
            this._lst2Compounds.MultiSelect = false;
            this._lst2Compounds.Name = "_lst2Compounds";
            this._lst2Compounds.Size = new System.Drawing.Size(428, 141);
            this._lst2Compounds.SmallImageList = this._imgList;
            this._lst2Compounds.TabIndex = 7;
            this._lst2Compounds.UseCompatibleStateImageBehavior = false;
            this._lst2Compounds.View = System.Windows.Forms.View.Details;
            // 
            // tabPage13
            // 
            this.tabPage13.Controls.Add(this._lst2Adducts);
            this.tabPage13.Location = new System.Drawing.Point(4, 40);
            this.tabPage13.Name = "tabPage13";
            this.tabPage13.Size = new System.Drawing.Size(434, 147);
            this.tabPage13.TabIndex = 6;
            this.tabPage13.Text = "Adducts";
            this.tabPage13.UseVisualStyleBackColor = true;
            // 
            // _lst2Adducts
            // 
            this._lst2Adducts.AllowColumnReorder = true;
            this._lst2Adducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lst2Adducts.FullRowSelect = true;
            this._lst2Adducts.GridLines = true;
            this._lst2Adducts.Location = new System.Drawing.Point(0, 0);
            this._lst2Adducts.MultiSelect = false;
            this._lst2Adducts.Name = "_lst2Adducts";
            this._lst2Adducts.Size = new System.Drawing.Size(434, 147);
            this._lst2Adducts.TabIndex = 7;
            this._lst2Adducts.UseCompatibleStateImageBehavior = false;
            this._lst2Adducts.View = System.Windows.Forms.View.Details;
            // 
            // tabPage14
            // 
            this.tabPage14.Controls.Add(this._lst2Pathways);
            this.tabPage14.Location = new System.Drawing.Point(4, 40);
            this.tabPage14.Name = "tabPage14";
            this.tabPage14.Size = new System.Drawing.Size(434, 147);
            this.tabPage14.TabIndex = 7;
            this.tabPage14.Text = "Pathways";
            this.tabPage14.UseVisualStyleBackColor = true;
            // 
            // _lst2Pathways
            // 
            this._lst2Pathways.AllowColumnReorder = true;
            this._lst2Pathways.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lst2Pathways.FullRowSelect = true;
            this._lst2Pathways.GridLines = true;
            this._lst2Pathways.LargeImageList = this._imgList;
            this._lst2Pathways.Location = new System.Drawing.Point(0, 0);
            this._lst2Pathways.MultiSelect = false;
            this._lst2Pathways.Name = "_lst2Pathways";
            this._lst2Pathways.Size = new System.Drawing.Size(434, 147);
            this._lst2Pathways.SmallImageList = this._imgList;
            this._lst2Pathways.TabIndex = 7;
            this._lst2Pathways.UseCompatibleStateImageBehavior = false;
            this._lst2Pathways.View = System.Windows.Forms.View.Details;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this._lst2Assignments);
            this.tabPage6.Location = new System.Drawing.Point(4, 40);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(434, 147);
            this.tabPage6.TabIndex = 8;
            this.tabPage6.Text = "Assignments";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // _lst2Assignments
            // 
            this._lst2Assignments.AllowColumnReorder = true;
            this._lst2Assignments.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lst2Assignments.FullRowSelect = true;
            this._lst2Assignments.GridLines = true;
            this._lst2Assignments.LargeImageList = this._imgList;
            this._lst2Assignments.Location = new System.Drawing.Point(3, 3);
            this._lst2Assignments.MultiSelect = false;
            this._lst2Assignments.Name = "_lst2Assignments";
            this._lst2Assignments.Size = new System.Drawing.Size(428, 141);
            this._lst2Assignments.SmallImageList = this._imgList;
            this._lst2Assignments.TabIndex = 18;
            this._lst2Assignments.UseCompatibleStateImageBehavior = false;
            this._lst2Assignments.View = System.Windows.Forms.View.Details;
            // 
            // quickHelpBar7
            // 
            this.quickHelpBar7.AutoSize = true;
            this.quickHelpBar7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.quickHelpBar7.Dock = System.Windows.Forms.DockStyle.Top;
            this.quickHelpBar7.Location = new System.Drawing.Point(0, 82);
            this.quickHelpBar7.MinimumSize = new System.Drawing.Size(128, 64);
            this.quickHelpBar7.Name = "quickHelpBar7";
            this.quickHelpBar7.Padding = new System.Windows.Forms.Padding(8);
            this.quickHelpBar7.Size = new System.Drawing.Size(442, 64);
            this.quickHelpBar7.TabIndex = 22;
            this.quickHelpBar7.Text = "Browsable information about the current selection is displayed here";
            this.quickHelpBar7.Title = "Explorer";
            // 
            // _tsBarSelection
            // 
            this._tsBarSelection.BackColor = System.Drawing.Color.CornflowerBlue;
            this._tsBarSelection.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._tsBarSelection.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._tsBarSelection.ImageScalingSize = new System.Drawing.Size(24, 24);
            this._tsBarSelection.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._btnBack,
            this.toolStripSeparator1,
            this._btnSubInfo,
            this._btnSubStat,
            this._btnSubPeak,
            this._btnSubPat,
            this._btnSubAss,
            this._btnSubAnnot,
            this._btnSubComp,
            this._btnSubAdd,
            this._btnSubPath});
            this._tsBarSelection.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this._tsBarSelection.Location = new System.Drawing.Point(0, 36);
            this._tsBarSelection.Name = "_tsBarSelection";
            this._tsBarSelection.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this._tsBarSelection.Size = new System.Drawing.Size(442, 46);
            this._tsBarSelection.TabIndex = 18;
            this._tsBarSelection.Text = "toolStrip4";
            // 
            // _btnBack
            // 
            this._btnBack.ForeColor = System.Drawing.Color.White;
            this._btnBack.Image = global::MetaboliteLevels.Properties.Resources.MnuBack;
            this._btnBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnBack.Name = "_btnBack";
            this._btnBack.Size = new System.Drawing.Size(45, 43);
            this._btnBack.Text = "Back";
            this._btnBack.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._btnBack.ButtonClick += new System.EventHandler(this._btnBack_ButtonClick);
            this._btnBack.DropDownOpening += new System.EventHandler(this._btnBack_DropDownOpening);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 42);
            // 
            // _btnSubInfo
            // 
            this._btnSubInfo.ForeColor = System.Drawing.Color.White;
            this._btnSubInfo.Image = global::MetaboliteLevels.Properties.Resources.ObjLInfo;
            this._btnSubInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnSubInfo.Name = "_btnSubInfo";
            this._btnSubInfo.Size = new System.Drawing.Size(29, 43);
            this._btnSubInfo.Text = "Info";
            this._btnSubInfo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // _btnSubStat
            // 
            this._btnSubStat.ForeColor = System.Drawing.Color.White;
            this._btnSubStat.Image = global::MetaboliteLevels.Properties.Resources.ObjLStatistics;
            this._btnSubStat.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnSubStat.Name = "_btnSubStat";
            this._btnSubStat.Size = new System.Drawing.Size(33, 43);
            this._btnSubStat.Text = "Stats";
            this._btnSubStat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // _btnSubPeak
            // 
            this._btnSubPeak.ForeColor = System.Drawing.Color.White;
            this._btnSubPeak.Image = global::MetaboliteLevels.Properties.Resources.ObjLVariableU;
            this._btnSubPeak.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnSubPeak.Name = "_btnSubPeak";
            this._btnSubPeak.Size = new System.Drawing.Size(37, 43);
            this._btnSubPeak.Text = "Peaks";
            this._btnSubPeak.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // _btnSubPat
            // 
            this._btnSubPat.ForeColor = System.Drawing.Color.White;
            this._btnSubPat.Image = global::MetaboliteLevels.Properties.Resources.ObjLCluster;
            this._btnSubPat.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnSubPat.Name = "_btnSubPat";
            this._btnSubPat.Size = new System.Drawing.Size(49, 43);
            this._btnSubPat.Text = "Clusters";
            this._btnSubPat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // _btnSubAss
            // 
            this._btnSubAss.ForeColor = System.Drawing.Color.White;
            this._btnSubAss.Image = global::MetaboliteLevels.Properties.Resources.ObjLAssignment;
            this._btnSubAss.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnSubAss.Name = "_btnSubAss";
            this._btnSubAss.Size = new System.Drawing.Size(42, 43);
            this._btnSubAss.Text = "Assigs";
            this._btnSubAss.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // _btnSubComp
            // 
            this._btnSubComp.ForeColor = System.Drawing.Color.White;
            this._btnSubComp.Image = global::MetaboliteLevels.Properties.Resources.ObjLCompoundU;
            this._btnSubComp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnSubComp.Name = "_btnSubComp";
            this._btnSubComp.Size = new System.Drawing.Size(44, 43);
            this._btnSubComp.Text = "Comps";
            this._btnSubComp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // _btnSubAdd
            // 
            this._btnSubAdd.ForeColor = System.Drawing.Color.White;
            this._btnSubAdd.Image = global::MetaboliteLevels.Properties.Resources.ObjLAdduct;
            this._btnSubAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnSubAdd.Name = "_btnSubAdd";
            this._btnSubAdd.Size = new System.Drawing.Size(49, 43);
            this._btnSubAdd.Text = "Adducts";
            this._btnSubAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // _btnSubPath
            // 
            this._btnSubPath.ForeColor = System.Drawing.Color.White;
            this._btnSubPath.Image = global::MetaboliteLevels.Properties.Resources.ObjLPathway;
            this._btnSubPath.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnSubPath.Name = "_btnSubPath";
            this._btnSubPath.Size = new System.Drawing.Size(36, 43);
            this._btnSubPath.Text = "Paths";
            this._btnSubPath.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.BackColor = System.Drawing.Color.CornflowerBlue;
            this.panel2.Controls.Add(this.tableLayoutPanel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.ForeColor = System.Drawing.Color.White;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(442, 36);
            this.panel2.TabIndex = 19;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this._btnSel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._lblCurrentSel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this._btnSel2, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this._lblSel2, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(442, 36);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // _btnSel
            // 
            this._btnSel.AutoSize = true;
            this._btnSel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._btnSel.BackColor = System.Drawing.Color.White;
            this._btnSel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this._btnSel.Image = ((System.Drawing.Image)(resources.GetObject("_btnSel.Image")));
            this._btnSel.Location = new System.Drawing.Point(3, 3);
            this._btnSel.Name = "_btnSel";
            this._btnSel.Size = new System.Drawing.Size(30, 30);
            this._btnSel.TabIndex = 3;
            this._toolTipMain.SetToolTip(this._btnSel, "Show selection editing options");
            this._btnSel.UseVisualStyleBackColor = false;
            this._btnSel.Click += new System.EventHandler(this._btnCurrentSel_Click);
            // 
            // _lblCurrentSel
            // 
            this._lblCurrentSel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._lblCurrentSel.AutoSize = true;
            this._lblCurrentSel.Font = new System.Drawing.Font("Segoe UI", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblCurrentSel.Location = new System.Drawing.Point(39, 3);
            this._lblCurrentSel.Name = "_lblCurrentSel";
            this._lblCurrentSel.Size = new System.Drawing.Size(103, 30);
            this._lblCurrentSel.TabIndex = 1;
            this._lblCurrentSel.Text = "Selection";
            this._lblCurrentSel.Click += new System.EventHandler(this._btnCurrentSel_Click);
            // 
            // _btnSel2
            // 
            this._btnSel2.AutoSize = true;
            this._btnSel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._btnSel2.BackColor = System.Drawing.Color.White;
            this._btnSel2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this._btnSel2.Image = ((System.Drawing.Image)(resources.GetObject("_btnSel2.Image")));
            this._btnSel2.Location = new System.Drawing.Point(409, 3);
            this._btnSel2.Name = "_btnSel2";
            this._btnSel2.Size = new System.Drawing.Size(30, 30);
            this._btnSel2.TabIndex = 3;
            this._btnSel2.UseVisualStyleBackColor = false;
            this._btnSel2.Click += new System.EventHandler(this._btnCurrentSel_Click);
            // 
            // _lblSel2
            // 
            this._lblSel2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this._lblSel2.AutoSize = true;
            this._lblSel2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblSel2.Location = new System.Drawing.Point(325, 7);
            this._lblSel2.Name = "_lblSel2";
            this._lblSel2.Size = new System.Drawing.Size(78, 21);
            this._lblSel2.TabIndex = 1;
            this._lblSel2.Text = "Selection";
            this._lblSel2.Click += new System.EventHandler(this._btnCurrentSel_Click);
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this._chartPat);
            this.splitContainer3.Panel1.Controls.Add(this.tableLayoutPanel6);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.tableLayoutPanel2);
            this.splitContainer3.Size = new System.Drawing.Size(778, 602);
            this.splitContainer3.SplitterDistance = 297;
            this.splitContainer3.TabIndex = 10;
            // 
            // _chartPat
            // 
            this._chartPat.BorderSkin.BorderColor = System.Drawing.Color.Gray;
            chartArea2.Name = "ChartArea1";
            this._chartPat.ChartAreas.Add(chartArea2);
            this._chartPat.Cursor = System.Windows.Forms.Cursors.Cross;
            this._chartPat.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this._chartPat.Legends.Add(legend2);
            this._chartPat.Location = new System.Drawing.Point(0, 40);
            this._chartPat.Name = "_chartPat";
            this._chartPat.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            this._chartPat.PaletteCustomColors = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128))))),
        System.Drawing.Color.Blue,
        System.Drawing.Color.Red,
        System.Drawing.Color.Green,
        System.Drawing.Color.Olive};
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            series2.YValuesPerPoint = 4;
            this._chartPat.Series.Add(series2);
            this._chartPat.Size = new System.Drawing.Size(778, 257);
            this._chartPat.TabIndex = 2;
            this._chartPat.Text = "chart1";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel6.BackColor = System.Drawing.Color.CornflowerBlue;
            this.tableLayoutPanel6.ColumnCount = 5;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.Controls.Add(this._lblPatName, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this._btnGraphPat, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this._btnGraphPat2, 4, 0);
            this.tableLayoutPanel6.Controls.Add(this._lblPatObs, 3, 0);
            this.tableLayoutPanel6.Controls.Add(this._lblPatComment, 2, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel6.ForeColor = System.Drawing.Color.White;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.Size = new System.Drawing.Size(778, 40);
            this.tableLayoutPanel6.TabIndex = 5;
            // 
            // _lblPatName
            // 
            this._lblPatName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._lblPatName.AutoSize = true;
            this._lblPatName.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblPatName.Location = new System.Drawing.Point(39, 5);
            this._lblPatName.Name = "_lblPatName";
            this._lblPatName.Size = new System.Drawing.Size(145, 30);
            this._lblPatName.TabIndex = 4;
            this._lblPatName.Text = "Cluster Name";
            // 
            // _btnGraphPat
            // 
            this._btnGraphPat.AutoSize = true;
            this._btnGraphPat.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._btnGraphPat.BackColor = System.Drawing.Color.White;
            this._btnGraphPat.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this._btnGraphPat.Image = ((System.Drawing.Image)(resources.GetObject("_btnGraphPat.Image")));
            this._btnGraphPat.Location = new System.Drawing.Point(3, 3);
            this._btnGraphPat.Name = "_btnGraphPat";
            this._btnGraphPat.Size = new System.Drawing.Size(30, 30);
            this._btnGraphPat.TabIndex = 2;
            this._toolTipMain.SetToolTip(this._btnGraphPat, "Show cluster plot options");
            this._btnGraphPat.UseVisualStyleBackColor = false;
            // 
            // _btnGraphPat2
            // 
            this._btnGraphPat2.AutoSize = true;
            this._btnGraphPat2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._btnGraphPat2.BackColor = System.Drawing.Color.White;
            this._btnGraphPat2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this._btnGraphPat2.Image = ((System.Drawing.Image)(resources.GetObject("_btnGraphPat2.Image")));
            this._btnGraphPat2.Location = new System.Drawing.Point(744, 3);
            this._btnGraphPat2.Name = "_btnGraphPat2";
            this._btnGraphPat2.Size = new System.Drawing.Size(30, 30);
            this._btnGraphPat2.TabIndex = 2;
            this._btnGraphPat2.UseVisualStyleBackColor = false;
            // 
            // _lblPatObs
            // 
            this._lblPatObs.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this._lblPatObs.AutoSize = true;
            this._lblPatObs.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblPatObs.Location = new System.Drawing.Point(638, 9);
            this._lblPatObs.Margin = new System.Windows.Forms.Padding(4);
            this._lblPatObs.Name = "_lblPatObs";
            this._lblPatObs.Size = new System.Drawing.Size(99, 21);
            this._lblPatObs.TabIndex = 7;
            this._lblPatObs.Text = "Observation";
            // 
            // _lblPatComment
            // 
            this._lblPatComment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._lblPatComment.AutoSize = true;
            this._lblPatComment.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblPatComment.Location = new System.Drawing.Point(190, 21);
            this._lblPatComment.Margin = new System.Windows.Forms.Padding(3);
            this._lblPatComment.MinimumSize = new System.Drawing.Size(4, 4);
            this._lblPatComment.Name = "_lblPatComment";
            this._lblPatComment.Size = new System.Drawing.Size(69, 16);
            this._lblPatComment.TabIndex = 6;
            this._lblPatComment.Text = "User defined";
            // 
            // quickHelpBar1
            // 
            this.quickHelpBar1.AutoSize = true;
            this.quickHelpBar1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.quickHelpBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.quickHelpBar1.Location = new System.Drawing.Point(0, 602);
            this.quickHelpBar1.MinimumSize = new System.Drawing.Size(128, 64);
            this.quickHelpBar1.Name = "quickHelpBar1";
            this.quickHelpBar1.Padding = new System.Windows.Forms.Padding(8);
            this.quickHelpBar1.Size = new System.Drawing.Size(778, 75);
            this.quickHelpBar1.TabIndex = 11;
            this.quickHelpBar1.Text = resources.GetString("quickHelpBar1.Text");
            this.quickHelpBar1.Title = "Graphs";
            // 
            // _cmsSelectionButton
            // 
            this._cmsSelectionButton.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.addCommentsToolStripMenuItem,
            this.toolStripMenuItem7,
            this.viewOnlineToolStripMenuItem,
            this.openInDataexplorerToolStripMenuItem,
            this.toolStripMenuItem3,
            this.breakUpLargeClusterToolStripMenuItem,
            this.compareToThisPeakToolStripMenuItem});
            this._cmsSelectionButton.Name = "_cmsSelectionButton";
            this._cmsSelectionButton.Size = new System.Drawing.Size(197, 148);
            this._cmsSelectionButton.Opening += new System.ComponentModel.CancelEventHandler(this._cmsSelectionButton_Opening);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click_1);
            // 
            // addCommentsToolStripMenuItem
            // 
            this.addCommentsToolStripMenuItem.Name = "addCommentsToolStripMenuItem";
            this.addCommentsToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.addCommentsToolStripMenuItem.Text = "&Edit...";
            this.addCommentsToolStripMenuItem.Click += new System.EventHandler(this.addCommentsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(193, 6);
            // 
            // viewOnlineToolStripMenuItem
            // 
            this.viewOnlineToolStripMenuItem.Name = "viewOnlineToolStripMenuItem";
            this.viewOnlineToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.viewOnlineToolStripMenuItem.Text = "&View online...";
            this.viewOnlineToolStripMenuItem.Click += new System.EventHandler(this.viewOnlineToolStripMenuItem_Click);
            // 
            // openInDataexplorerToolStripMenuItem
            // 
            this.openInDataexplorerToolStripMenuItem.Name = "openInDataexplorerToolStripMenuItem";
            this.openInDataexplorerToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.openInDataexplorerToolStripMenuItem.Text = "Open in data &explorer...";
            this.openInDataexplorerToolStripMenuItem.Click += new System.EventHandler(this.openInDataexplorerToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(193, 6);
            // 
            // breakUpLargeClusterToolStripMenuItem
            // 
            this.breakUpLargeClusterToolStripMenuItem.Image = global::MetaboliteLevels.Properties.Resources.TestCluster;
            this.breakUpLargeClusterToolStripMenuItem.Name = "breakUpLargeClusterToolStripMenuItem";
            this.breakUpLargeClusterToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.breakUpLargeClusterToolStripMenuItem.Text = "&Break up large cluster...";
            this.breakUpLargeClusterToolStripMenuItem.Click += new System.EventHandler(this.breakUpLargeClusterToolStripMenuItem_Click);
            // 
            // compareToThisPeakToolStripMenuItem
            // 
            this.compareToThisPeakToolStripMenuItem.Image = global::MetaboliteLevels.Properties.Resources.TestUnivariate;
            this.compareToThisPeakToolStripMenuItem.Name = "compareToThisPeakToolStripMenuItem";
            this.compareToThisPeakToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.compareToThisPeakToolStripMenuItem.Text = "&Compare to this peak...";
            this.compareToThisPeakToolStripMenuItem.Click += new System.EventHandler(this.compareToThisPeakToolStripMenuItem_Click);
            // 
            // _statusMain
            // 
            this._statusMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this._statusMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._lblChanges,
            this.toolStripStatusLabel2,
            this.toolStripProgressBar1});
            this._statusMain.Location = new System.Drawing.Point(0, 747);
            this._statusMain.Name = "_statusMain";
            this._statusMain.Size = new System.Drawing.Size(1224, 22);
            this._statusMain.TabIndex = 6;
            this._statusMain.Text = "statusStrip1";
            // 
            // _lblChanges
            // 
            this._lblChanges.Name = "_lblChanges";
            this._lblChanges.Size = new System.Drawing.Size(71, 17);
            this._lblChanges.Text = "_lblChanges";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.BackColor = System.Drawing.Color.Red;
            this.toolStripStatusLabel2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel2.ForeColor = System.Drawing.Color.White;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(38, 17);
            this.toolStripStatusLabel2.Text = "BUSY";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBar1.Visible = false;
            // 
            // _cmsCoreButton
            // 
            this._cmsCoreButton.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editNameToolStripMenuItem});
            this._cmsCoreButton.Name = "_cmsCoreButton";
            this._cmsCoreButton.Size = new System.Drawing.Size(220, 26);
            // 
            // editNameToolStripMenuItem
            // 
            this.editNameToolStripMenuItem.Name = "editNameToolStripMenuItem";
            this.editNameToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.editNameToolStripMenuItem.Text = "&Edit name and comments...";
            this.editNameToolStripMenuItem.Click += new System.EventHandler(this.editNameToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripSeparator4,
            this.toolStripButton5,
            this.toolStripButton8,
            this.toolStripButton7,
            this.toolStripSeparator2,
            this.toolStripButton2,
            this.toolStripLabel2,
            this.toolStripButton3,
            this.toolStripLabel3,
            this.toolStripButton4,
            this.toolStripLabel1,
            this.toolStripButton6,
            this.toolStripSeparator7,
            this.toolStripButton9,
            this.toolStripButton10,
            this.toolStripSeparator3,
            this._tssInsertViews});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip1.Size = new System.Drawing.Size(1224, 46);
            this.toolStrip1.TabIndex = 7;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = global::MetaboliteLevels.Properties.Resources.ObjSave;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(35, 43);
            this.toolStripButton1.Text = "Save";
            this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton1.Click += new System.EventHandler(this.saveExemplarsToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.AutoSize = false;
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 36);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.Image = global::MetaboliteLevels.Properties.Resources.ObjPrefs;
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(37, 43);
            this.toolStripButton5.Text = "Prefs";
            this.toolStripButton5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton5.Click += new System.EventHandler(this.visualOptionsToolStripMenuItem_Click);
            // 
            // toolStripButton8
            // 
            this.toolStripButton8.Image = global::MetaboliteLevels.Properties.Resources.ObjLGroups;
            this.toolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton8.Name = "toolStripButton8";
            this.toolStripButton8.Size = new System.Drawing.Size(49, 43);
            this.toolStripButton8.Text = "Groups";
            this.toolStripButton8.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton8.Click += new System.EventHandler(this.experimentalGroupsToolStripMenuItem_Click);
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.Image = global::MetaboliteLevels.Properties.Resources.ObjLPca;
            this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(34, 43);
            this.toolStripButton7.Text = "PCA";
            this.toolStripButton7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton7.Click += new System.EventHandler(this.pCAToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 36);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = global::MetaboliteLevels.Properties.Resources.ObjLScriptCorrect;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(50, 43);
            this.toolStripButton2.Text = "Correct";
            this.toolStripButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton2.Click += new System.EventHandler(this.editCorrectionsToolStripMenuItem_Click);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripLabel2.Image = global::MetaboliteLevels.Properties.Resources._112_RightArrowShort_Grey_16x16_72;
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(24, 24);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Image = global::MetaboliteLevels.Properties.Resources.ObjLScriptTrend;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(42, 43);
            this.toolStripButton3.Text = "Trend";
            this.toolStripButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton3.Click += new System.EventHandler(this.edittrendToolStripMenuItem_Click);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripLabel3.Image = global::MetaboliteLevels.Properties.Resources._112_RightArrowShort_Grey_16x16_72;
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(24, 24);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Image = global::MetaboliteLevels.Properties.Resources.ObjLScriptStatistic;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(36, 43);
            this.toolStripButton4.Text = "Stats";
            this.toolStripButton4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton4.Click += new System.EventHandler(this.editStatisticsToolStripMenuItem_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripLabel1.Image = global::MetaboliteLevels.Properties.Resources._112_RightArrowShort_Grey_16x16_72;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(24, 24);
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.Image = global::MetaboliteLevels.Properties.Resources.ObjLScriptCluster;
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(48, 43);
            this.toolStripButton6.Text = "Cluster";
            this.toolStripButton6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton6.Click += new System.EventHandler(this.createclustersToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.AutoSize = false;
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 36);
            // 
            // toolStripButton9
            // 
            this.toolStripButton9.Image = global::MetaboliteLevels.Properties.Resources.ObjLScriptCluster;
            this.toolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton9.Name = "toolStripButton9";
            this.toolStripButton9.Size = new System.Drawing.Size(47, 43);
            this.toolStripButton9.Text = "Wizard";
            this.toolStripButton9.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton9.ToolTipText = "Cluster Wizard";
            this.toolStripButton9.Click += new System.EventHandler(this.autogenerateToolStripMenuItem_Click);
            // 
            // toolStripButton10
            // 
            this.toolStripButton10.Image = global::MetaboliteLevels.Properties.Resources.ObjLScriptCluster;
            this.toolStripButton10.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton10.Name = "toolStripButton10";
            this.toolStripButton10.Size = new System.Drawing.Size(59, 43);
            this.toolStripButton10.Text = "Optimise";
            this.toolStripButton10.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton10.ToolTipText = "Cluster Wizard";
            this.toolStripButton10.Click += new System.EventHandler(this.toolStripButton10_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.AutoSize = false;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 36);
            // 
            // _tssInsertViews
            // 
            this._tssInsertViews.AutoSize = false;
            this._tssInsertViews.Name = "_tssInsertViews";
            this._tssInsertViews.Size = new System.Drawing.Size(6, 36);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.peakidentificationsToolStripMenuItem});
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.importToolStripMenuItem.Text = "&Import";
            // 
            // peakidentificationsToolStripMenuItem
            // 
            this.peakidentificationsToolStripMenuItem.Name = "peakidentificationsToolStripMenuItem";
            this.peakidentificationsToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.peakidentificationsToolStripMenuItem.Text = "Peak &identifications...";
            this.peakidentificationsToolStripMenuItem.Click += new System.EventHandler(this.peakidentificationsToolStripMenuItem_Click);
            // 
            // _btnSubAnnot
            // 
            this._btnSubAnnot.ForeColor = System.Drawing.Color.White;
            this._btnSubAnnot.Image = global::MetaboliteLevels.Properties.Resources.ObjLCompound;
            this._btnSubAnnot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnSubAnnot.Name = "_btnSubAnnot";
            this._btnSubAnnot.Size = new System.Drawing.Size(44, 43);
            this._btnSubAnnot.Text = "Annots";
            this._btnSubAnnot.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this._lstSubAnnots);
            this.tabPage7.Location = new System.Drawing.Point(4, 40);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(434, 147);
            this.tabPage7.TabIndex = 9;
            this.tabPage7.Text = "Annotations";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // _lstSubAnnots
            // 
            this._lstSubAnnots.AllowColumnReorder = true;
            this._lstSubAnnots.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lstSubAnnots.FullRowSelect = true;
            this._lstSubAnnots.GridLines = true;
            this._lstSubAnnots.LargeImageList = this._imgList;
            this._lstSubAnnots.Location = new System.Drawing.Point(3, 3);
            this._lstSubAnnots.MultiSelect = false;
            this._lstSubAnnots.Name = "_lstSubAnnots";
            this._lstSubAnnots.Size = new System.Drawing.Size(428, 141);
            this._lstSubAnnots.SmallImageList = this._imgList;
            this._lstSubAnnots.TabIndex = 8;
            this._lstSubAnnots.UseCompatibleStateImageBehavior = false;
            this._lstSubAnnots.View = System.Windows.Forms.View.Details;
            // 
            // _btnMainAnnots
            // 
            this._btnMainAnnots.ForeColor = System.Drawing.Color.White;
            this._btnMainAnnots.Image = global::MetaboliteLevels.Properties.Resources.ObjLCompound;
            this._btnMainAnnots.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnMainAnnots.Name = "_btnMainAnnots";
            this._btnMainAnnots.Size = new System.Drawing.Size(44, 43);
            this._btnMainAnnots.Text = "Annots";
            this._btnMainAnnots.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tabPage16
            // 
            this.tabPage16.Controls.Add(this._lstAnnotations);
            this.tabPage16.Location = new System.Drawing.Point(4, 22);
            this.tabPage16.Name = "tabPage16";
            this.tabPage16.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage16.Size = new System.Drawing.Size(434, 228);
            this.tabPage16.TabIndex = 6;
            this.tabPage16.Text = "Annotations";
            this.tabPage16.UseVisualStyleBackColor = true;
            // 
            // _lstAnnotations
            // 
            this._lstAnnotations.AllowColumnReorder = true;
            this._lstAnnotations.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lstAnnotations.FullRowSelect = true;
            this._lstAnnotations.GridLines = true;
            this._lstAnnotations.LargeImageList = this._imgList;
            this._lstAnnotations.Location = new System.Drawing.Point(3, 3);
            this._lstAnnotations.MultiSelect = false;
            this._lstAnnotations.Name = "_lstAnnotations";
            this._lstAnnotations.Size = new System.Drawing.Size(428, 222);
            this._lstAnnotations.SmallImageList = this._imgList;
            this._lstAnnotations.TabIndex = 7;
            this._lstAnnotations.UseCompatibleStateImageBehavior = false;
            this._lstAnnotations.View = System.Windows.Forms.View.Details;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1224, 769);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this._statusMain);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this._mnuMain);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this._mnuMain;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Metabolite Clusters";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this._mnuMain.ResumeLayout(false);
            this._mnuMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._chartPeak)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tabPage15.ResumeLayout(false);
            this.tabPage15.PerformLayout();
            this._tsBarBrowser.ResumeLayout(false);
            this._tsBarBrowser.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this._tabSubinfo.ResumeLayout(false);
            this.tabPage8.ResumeLayout(false);
            this.tabPage9.ResumeLayout(false);
            this.tabPage10.ResumeLayout(false);
            this.tabPage11.ResumeLayout(false);
            this.tabPage12.ResumeLayout(false);
            this.tabPage13.ResumeLayout(false);
            this.tabPage14.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this._tsBarSelection.ResumeLayout(false);
            this._tsBarSelection.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._chartPat)).EndInit();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this._cmsSelectionButton.ResumeLayout(false);
            this._statusMain.ResumeLayout(false);
            this._statusMain.PerformLayout();
            this._cmsCoreButton.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabPage7.ResumeLayout(false);
            this.tabPage16.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip _mnuMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.DataVisualization.Charting.Chart _chartPeak;
        private System.Windows.Forms.ToolStripMenuItem loadDataSetToolStripMenuItem;
        private System.Windows.Forms.Label _lblVarName;
        private System.Windows.Forms.ListView _lstVariables;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem saveExemplarsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label _lblVarComment;
        private System.Windows.Forms.Label _lblVarObservation;
        private System.Windows.Forms.StatusStrip _statusMain;
        private System.Windows.Forms.ToolStripStatusLabel _lblChanges;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.DataVisualization.Charting.Chart _chartPat;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Label _lblPatObs;
        private System.Windows.Forms.Label _lblPatComment;
        private System.Windows.Forms.Label _lblPatName;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripMenuItem printClusterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportClustersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveClusterImageToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListView _lstCompounds;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ListView _lstAdducts;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.ListView _lstPathways;
        private System.Windows.Forms.ImageList _imgList;
        private System.Windows.Forms.ToolStripMenuItem clusteringToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clusterBreakdownToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem visualOptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ToolStripMenuItem saveSessionAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private CtlHelpBar quickHelpBar1;
        private CtlHelpBar quickHelpBar2;
        private CtlHelpBar quickHelpBar3;
        private CtlHelpBar quickHelpBar4;
        private CtlHelpBar quickHelpBar5;
        private CtlHelpBar quickHelpBar8;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStrip _tsBarSelection;
        private System.Windows.Forms.ToolStripSplitButton _btnBack;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label _lblCurrentSel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ToolStrip _tsBarBrowser;
        private System.Windows.Forms.ToolStripButton _btnMain5;
        private System.Windows.Forms.ToolStripButton _btnMain2;
        private System.Windows.Forms.ToolStripButton _btnMain3;
        private System.Windows.Forms.ToolStripButton _btnMain4;
        private System.Windows.Forms.ToolStripButton _btnSubInfo;
        private System.Windows.Forms.ToolStripButton _btnSubStat;
        private System.Windows.Forms.ToolStripButton _btnSubPeak;
        private System.Windows.Forms.ToolStripButton _btnSubPat;
        private System.Windows.Forms.ToolStripButton _btnSubComp;
        private System.Windows.Forms.ToolStripButton _btnSubAdd;
        private System.Windows.Forms.ToolStripButton _btnSubPath;
        private System.Windows.Forms.TabControl _tabSubinfo;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.ListView _lst2Info;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TabPage tabPage9;
        private System.Windows.Forms.ListView _lst2Stats;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.TabPage tabPage10;
        private System.Windows.Forms.ListView _lst2Peaks;
        private System.Windows.Forms.TabPage tabPage11;
        private System.Windows.Forms.ListView _lst2Clusters;
        private System.Windows.Forms.TabPage tabPage12;
        private System.Windows.Forms.ListView _lst2Compounds;
        private System.Windows.Forms.TabPage tabPage14;
        private System.Windows.Forms.TabPage tabPage13;
        private System.Windows.Forms.ListView _lst2Adducts;
        private System.Windows.Forms.ListView _lst2Pathways;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ListView _lstClusters;
        private CtlHelpBar quickHelpBar7;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label _lblTitle;
        private System.Windows.Forms.Button _btnSession;
        private System.Windows.Forms.Button _btnSel;
        private System.Windows.Forms.Button _btnGraphVar;
        private System.Windows.Forms.Button _btnGraphPat;
        private System.Windows.Forms.ToolTip _toolTipMain;
        private System.Windows.Forms.ImageList _imgListClusters;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip _cmsCoreButton;
        private System.Windows.Forms.ContextMenuStrip _cmsSelectionButton;
        private System.Windows.Forms.ToolStripMenuItem editNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addCommentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewOnlineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataInRFormatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem experimentalOptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem editStatisticsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editCorrectionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem edittrendToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createclustersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem experimentalGroupsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripButton toolStripButton7;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripSeparator _tssInsertViews;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton8;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem openInDataexplorerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem breakUpLargeClusterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem compareToThisPeakToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem peakFiltersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem observationFiltersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autogenerateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pCAToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem10;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton toolStripButton9;
        private System.Windows.Forms.Button _btnGraphVar2;
        private System.Windows.Forms.Button _btnSel2;
        private System.Windows.Forms.Label _lblSel2;
        private System.Windows.Forms.Button _btnGraphPat2;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.ListView _lst2Assignments;
        private System.Windows.Forms.ToolStripButton _btnSubAss;
        private System.Windows.Forms.TabPage tabPage15;
        private System.Windows.Forms.ListView _lstAssignments;
        private CtlHelpBar ctlHelpBar1;
        private System.Windows.Forms.ToolStripButton _btnMain0;
        private System.Windows.Forms.ToolStripButton _btnMain1;
        private System.Windows.Forms.ToolStripMenuItem clustererOptimiserToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton10;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem peakidentificationsToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.ListView _lstSubAnnots;
        private System.Windows.Forms.ToolStripButton _btnSubAnnot;
        private System.Windows.Forms.TabPage tabPage16;
        private System.Windows.Forms.ListView _lstAnnotations;
        private System.Windows.Forms.ToolStripButton _btnMainAnnots;
    }
}

