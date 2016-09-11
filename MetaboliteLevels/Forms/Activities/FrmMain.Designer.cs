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
            this._mnuMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadDataSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.saveExemplarsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSessionAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.peakidentificationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveClusterImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.printClusterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.visualOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.experimentalGroupsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.experimentalOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.correlationMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.databaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.clusterParameterOptimiserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autogenerateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this._imgList = new System.Windows.Forms.ImageList(this.components);
            this._imgListClusters = new System.Windows.Forms.ImageList(this.components);
            this._cmsSelectionButton = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pLACEHOLDERToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._statusMain = new System.Windows.Forms.StatusStrip();
            this._lblChanges = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this._txtGuid = new System.Windows.Forms.ToolStripStatusLabel();
            this._toolTipMain = new System.Windows.Forms.ToolTip(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._btnOpen = new System.Windows.Forms.ToolStripButton();
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
            this._lstMatrix = new System.Windows.Forms.ToolStripButton();
            this._lstDatasetCb = new System.Windows.Forms.ToolStripComboBox();
            this._lstTrend = new System.Windows.Forms.ToolStripButton();
            this._lstTrendCb = new System.Windows.Forms.ToolStripComboBox();
            this._btnSession = new System.Windows.Forms.ToolStripDropDownButton();
            this.editNameAndCommentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sessionInformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new MGui.Controls.CtlSplitter();
            this.splitContainer2 = new MGui.Controls.CtlSplitter();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this._lstVariables = new System.Windows.Forms.ListView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this._lstClusters = new System.Windows.Forms.ListView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this._lstCompounds = new System.Windows.Forms.ListView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this._lstAdducts = new System.Windows.Forms.ListView();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this._lstPathways = new System.Windows.Forms.ListView();
            this.tabPage15 = new System.Windows.Forms.TabPage();
            this._lstAssignments = new System.Windows.Forms.ListView();
            this.tabPage16 = new System.Windows.Forms.TabPage();
            this._lstAnnotations = new System.Windows.Forms.ListView();
            this._tsBarBrowser = new System.Windows.Forms.ToolStrip();
            this._btnMain0 = new System.Windows.Forms.ToolStripButton();
            this._btnMain1 = new System.Windows.Forms.ToolStripButton();
            this._btnMain5 = new System.Windows.Forms.ToolStripButton();
            this._btnMainAnnots = new System.Windows.Forms.ToolStripButton();
            this._btnMain2 = new System.Windows.Forms.ToolStripButton();
            this._btnMain3 = new System.Windows.Forms.ToolStripButton();
            this._btnMain4 = new System.Windows.Forms.ToolStripButton();
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
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this._lstSubAnnots = new System.Windows.Forms.ListView();
            this._tsBarSelection = new System.Windows.Forms.ToolStrip();
            this._btnSubInfo = new System.Windows.Forms.ToolStripButton();
            this._btnSubStat = new System.Windows.Forms.ToolStripButton();
            this._btnSubPeak = new System.Windows.Forms.ToolStripButton();
            this._btnSubPat = new System.Windows.Forms.ToolStripButton();
            this._btnSubAss = new System.Windows.Forms.ToolStripButton();
            this._btnSubAnnot = new System.Windows.Forms.ToolStripButton();
            this._btnSubComp = new System.Windows.Forms.ToolStripButton();
            this._btnSubAdd = new System.Windows.Forms.ToolStripButton();
            this._btnSubPath = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this._btnBack = new System.Windows.Forms.ToolStripSplitButton();
            this._btnSelection = new System.Windows.Forms.ToolStripDropDownButton();
            this._btnExterior = new System.Windows.Forms.ToolStripButton();
            this._btnSelectionExterior = new System.Windows.Forms.ToolStripDropDownButton();
            this.splitContainer3 = new MGui.Controls.CtlSplitter();
            this._mnuMain.SuspendLayout();
            this._cmsSelectionButton.SuspendLayout();
            this._statusMain.SuspendLayout();
            this.toolStrip1.SuspendLayout();
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
            this.tabPage16.SuspendLayout();
            this._tsBarBrowser.SuspendLayout();
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
            this.tabPage7.SuspendLayout();
            this._tsBarSelection.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // _mnuMain
            // 
            this._mnuMain.BackColor = System.Drawing.SystemColors.Control;
            this._mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.databaseToolStripMenuItem,
            this.clusteringToolStripMenuItem,
            this.helpToolStripMenuItem});
            this._mnuMain.Location = new System.Drawing.Point(0, 0);
            this._mnuMain.Name = "_mnuMain";
            this._mnuMain.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this._mnuMain.Size = new System.Drawing.Size(1185, 25);
            this._mnuMain.TabIndex = 0;
            this._mnuMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
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
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.Purple;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 19);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.loadDataSetToolStripMenuItem_Click);
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
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dataToolStripMenuItem,
            this.saveClusterImageToolStripMenuItem});
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.exportToolStripMenuItem.Text = "&Export";
            // 
            // dataToolStripMenuItem
            // 
            this.dataToolStripMenuItem.Name = "dataToolStripMenuItem";
            this.dataToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.dataToolStripMenuItem.Text = "&Data...";
            this.dataToolStripMenuItem.Click += new System.EventHandler(this.dataToolStripMenuItem_Click);
            // 
            // saveClusterImageToolStripMenuItem
            // 
            this.saveClusterImageToolStripMenuItem.Name = "saveClusterImageToolStripMenuItem";
            this.saveClusterImageToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.saveClusterImageToolStripMenuItem.Text = "&Image (all cluster plots)...";
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
            this.experimentalOptionsToolStripMenuItem,
            this.correlationMapToolStripMenuItem,
            this.toolStripMenuItem6});
            this.viewToolStripMenuItem.ForeColor = System.Drawing.Color.Purple;
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 19);
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
            this.experimentalGroupsToolStripMenuItem.Image = global::MetaboliteLevels.Properties.Resources.MnuExperimentalGroups;
            this.experimentalGroupsToolStripMenuItem.Name = "experimentalGroupsToolStripMenuItem";
            this.experimentalGroupsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.experimentalGroupsToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.experimentalGroupsToolStripMenuItem.Text = "&Experimental groups...";
            this.experimentalGroupsToolStripMenuItem.Click += new System.EventHandler(this.experimentalGroupsToolStripMenuItem_Click);
            // 
            // experimentalOptionsToolStripMenuItem
            // 
            this.experimentalOptionsToolStripMenuItem.Name = "experimentalOptionsToolStripMenuItem";
            this.experimentalOptionsToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.experimentalOptionsToolStripMenuItem.Text = "&Miscellaneous functions...";
            this.experimentalOptionsToolStripMenuItem.Click += new System.EventHandler(this.experimentalOptionsToolStripMenuItem_Click);
            // 
            // correlationMapToolStripMenuItem
            // 
            this.correlationMapToolStripMenuItem.Name = "correlationMapToolStripMenuItem";
            this.correlationMapToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.correlationMapToolStripMenuItem.Text = "&View peaks as map";
            this.correlationMapToolStripMenuItem.Click += new System.EventHandler(this.correlationMapToolStripMenuItem_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(228, 6);
            // 
            // databaseToolStripMenuItem
            // 
            this.databaseToolStripMenuItem.ForeColor = System.Drawing.Color.Purple;
            this.databaseToolStripMenuItem.Name = "databaseToolStripMenuItem";
            this.databaseToolStripMenuItem.Size = new System.Drawing.Size(67, 19);
            this.databaseToolStripMenuItem.Text = "&Database";
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
            this.clusterParameterOptimiserToolStripMenuItem,
            this.autogenerateToolStripMenuItem});
            this.clusteringToolStripMenuItem.ForeColor = System.Drawing.Color.Purple;
            this.clusteringToolStripMenuItem.Name = "clusteringToolStripMenuItem";
            this.clusteringToolStripMenuItem.Size = new System.Drawing.Size(70, 19);
            this.clusteringToolStripMenuItem.Text = "&Workflow";
            // 
            // pCAToolStripMenuItem
            // 
            this.pCAToolStripMenuItem.Image = global::MetaboliteLevels.Properties.Resources.IconPca;
            this.pCAToolStripMenuItem.Name = "pCAToolStripMenuItem";
            this.pCAToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.P)));
            this.pCAToolStripMenuItem.Size = new System.Drawing.Size(308, 22);
            this.pCAToolStripMenuItem.Text = "&PCA";
            this.pCAToolStripMenuItem.Click += new System.EventHandler(this.pCAToolStripMenuItem_Click);
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(305, 6);
            // 
            // editCorrectionsToolStripMenuItem
            // 
            this.editCorrectionsToolStripMenuItem.Image = global::MetaboliteLevels.Properties.Resources.IconScriptCorrect;
            this.editCorrectionsToolStripMenuItem.Name = "editCorrectionsToolStripMenuItem";
            this.editCorrectionsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
            this.editCorrectionsToolStripMenuItem.Size = new System.Drawing.Size(308, 22);
            this.editCorrectionsToolStripMenuItem.Text = "&Corrections...";
            this.editCorrectionsToolStripMenuItem.Click += new System.EventHandler(this.editCorrectionsToolStripMenuItem_Click);
            // 
            // edittrendToolStripMenuItem
            // 
            this.edittrendToolStripMenuItem.Image = global::MetaboliteLevels.Properties.Resources.IconScriptTrend;
            this.edittrendToolStripMenuItem.Name = "edittrendToolStripMenuItem";
            this.edittrendToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
            this.edittrendToolStripMenuItem.Size = new System.Drawing.Size(308, 22);
            this.edittrendToolStripMenuItem.Text = "&Trends...";
            this.edittrendToolStripMenuItem.Click += new System.EventHandler(this.edittrendToolStripMenuItem_Click);
            // 
            // editStatisticsToolStripMenuItem
            // 
            this.editStatisticsToolStripMenuItem.Image = global::MetaboliteLevels.Properties.Resources.IconScriptStatistic;
            this.editStatisticsToolStripMenuItem.Name = "editStatisticsToolStripMenuItem";
            this.editStatisticsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D3)));
            this.editStatisticsToolStripMenuItem.Size = new System.Drawing.Size(308, 22);
            this.editStatisticsToolStripMenuItem.Text = "&Statistics...";
            this.editStatisticsToolStripMenuItem.Click += new System.EventHandler(this.editStatisticsToolStripMenuItem_Click);
            // 
            // createclustersToolStripMenuItem
            // 
            this.createclustersToolStripMenuItem.Image = global::MetaboliteLevels.Properties.Resources.IconScriptCluster;
            this.createclustersToolStripMenuItem.Name = "createclustersToolStripMenuItem";
            this.createclustersToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D4)));
            this.createclustersToolStripMenuItem.Size = new System.Drawing.Size(308, 22);
            this.createclustersToolStripMenuItem.Text = "&Clusters...";
            this.createclustersToolStripMenuItem.Click += new System.EventHandler(this.createclustersToolStripMenuItem_Click);
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(305, 6);
            // 
            // peakFiltersToolStripMenuItem
            // 
            this.peakFiltersToolStripMenuItem.Image = global::MetaboliteLevels.Properties.Resources.MnuFilter;
            this.peakFiltersToolStripMenuItem.Name = "peakFiltersToolStripMenuItem";
            this.peakFiltersToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D5)));
            this.peakFiltersToolStripMenuItem.Size = new System.Drawing.Size(308, 22);
            this.peakFiltersToolStripMenuItem.Text = "&Peak filters...";
            this.peakFiltersToolStripMenuItem.Click += new System.EventHandler(this.peakFiltersToolStripMenuItem_Click);
            // 
            // observationFiltersToolStripMenuItem
            // 
            this.observationFiltersToolStripMenuItem.Image = global::MetaboliteLevels.Properties.Resources.MnuFilter;
            this.observationFiltersToolStripMenuItem.Name = "observationFiltersToolStripMenuItem";
            this.observationFiltersToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D6)));
            this.observationFiltersToolStripMenuItem.Size = new System.Drawing.Size(308, 22);
            this.observationFiltersToolStripMenuItem.Text = "&Observation filters...";
            this.observationFiltersToolStripMenuItem.Click += new System.EventHandler(this.observationFiltersToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(305, 6);
            // 
            // clusterParameterOptimiserToolStripMenuItem
            // 
            this.clusterParameterOptimiserToolStripMenuItem.Image = global::MetaboliteLevels.Properties.Resources.IconScriptCluster;
            this.clusterParameterOptimiserToolStripMenuItem.Name = "clusterParameterOptimiserToolStripMenuItem";
            this.clusterParameterOptimiserToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.F4)));
            this.clusterParameterOptimiserToolStripMenuItem.Size = new System.Drawing.Size(308, 22);
            this.clusterParameterOptimiserToolStripMenuItem.Text = "&Cluster parameter optimiser...";
            this.clusterParameterOptimiserToolStripMenuItem.Click += new System.EventHandler(this.clustererOptimiserToolStripMenuItem_Click);
            // 
            // autogenerateToolStripMenuItem
            // 
            this.autogenerateToolStripMenuItem.Image = global::MetaboliteLevels.Properties.Resources.IconScriptCluster;
            this.autogenerateToolStripMenuItem.Name = "autogenerateToolStripMenuItem";
            this.autogenerateToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.C)));
            this.autogenerateToolStripMenuItem.Size = new System.Drawing.Size(308, 22);
            this.autogenerateToolStripMenuItem.Text = "&d-k-means++ wizard...";
            this.autogenerateToolStripMenuItem.Click += new System.EventHandler(this.autogenerateToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.aboutToolStripMenuItem1});
            this.helpToolStripMenuItem.ForeColor = System.Drawing.Color.Purple;
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 19);
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
            // _imgList
            // 
            this._imgList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this._imgList.ImageSize = new System.Drawing.Size(24, 24);
            this._imgList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // _imgListClusters
            // 
            this._imgListClusters.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this._imgListClusters.ImageSize = new System.Drawing.Size(24, 24);
            this._imgListClusters.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // _cmsSelectionButton
            // 
            this._cmsSelectionButton.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pLACEHOLDERToolStripMenuItem});
            this._cmsSelectionButton.Name = "_cmsSelectionButton";
            this._cmsSelectionButton.Size = new System.Drawing.Size(155, 26);
            this._cmsSelectionButton.Opening += new System.ComponentModel.CancelEventHandler(this._cmsSelectionButton_Opening);
            // 
            // pLACEHOLDERToolStripMenuItem
            // 
            this.pLACEHOLDERToolStripMenuItem.Name = "pLACEHOLDERToolStripMenuItem";
            this.pLACEHOLDERToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.pLACEHOLDERToolStripMenuItem.Text = "PLACEHOLDER";
            // 
            // _statusMain
            // 
            this._statusMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this._statusMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._lblChanges,
            this.toolStripStatusLabel2,
            this.toolStripProgressBar1,
            this.toolStripStatusLabel3,
            this._txtGuid});
            this._statusMain.Location = new System.Drawing.Point(0, 782);
            this._statusMain.Name = "_statusMain";
            this._statusMain.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this._statusMain.Size = new System.Drawing.Size(1185, 22);
            this._statusMain.TabIndex = 6;
            this._statusMain.Text = "statusStrip1";
            // 
            // _lblChanges
            // 
            this._lblChanges.Name = "_lblChanges";
            this._lblChanges.Size = new System.Drawing.Size(42, 17);
            this._lblChanges.Text = "VALUE";
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
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(1052, 17);
            this.toolStripStatusLabel3.Spring = true;
            // 
            // _txtGuid
            // 
            this._txtGuid.Enabled = false;
            this._txtGuid.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._txtGuid.Name = "_txtGuid";
            this._txtGuid.Size = new System.Drawing.Size(30, 17);
            this._txtGuid.Text = "VALUE";
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._btnOpen,
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
            this._tssInsertViews,
            this._lstMatrix,
            this._lstDatasetCb,
            this._lstTrend,
            this._lstTrendCb,
            this._btnSession});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip1.Size = new System.Drawing.Size(1185, 46);
            this.toolStrip1.TabIndex = 7;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _btnOpen
            // 
            this._btnOpen.ForeColor = System.Drawing.Color.Purple;
            this._btnOpen.Image = global::MetaboliteLevels.Properties.Resources.IconOpen;
            this._btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnOpen.Margin = new System.Windows.Forms.Padding(8, 1, 0, 2);
            this._btnOpen.Name = "_btnOpen";
            this._btnOpen.Size = new System.Drawing.Size(40, 43);
            this._btnOpen.Text = "Open";
            this._btnOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._btnOpen.Click += new System.EventHandler(this.loadDataSetToolStripMenuItem_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.ForeColor = System.Drawing.Color.Purple;
            this.toolStripButton1.Image = global::MetaboliteLevels.Properties.Resources.IconSave;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Margin = new System.Windows.Forms.Padding(8, 1, 0, 2);
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
            this.toolStripButton5.ForeColor = System.Drawing.Color.Purple;
            this.toolStripButton5.Image = global::MetaboliteLevels.Properties.Resources.IconPreferences;
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(37, 43);
            this.toolStripButton5.Text = "Prefs";
            this.toolStripButton5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton5.Click += new System.EventHandler(this.visualOptionsToolStripMenuItem_Click);
            // 
            // toolStripButton8
            // 
            this.toolStripButton8.ForeColor = System.Drawing.Color.Purple;
            this.toolStripButton8.Image = global::MetaboliteLevels.Properties.Resources.IconGroups;
            this.toolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton8.Name = "toolStripButton8";
            this.toolStripButton8.Size = new System.Drawing.Size(49, 43);
            this.toolStripButton8.Text = "Groups";
            this.toolStripButton8.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton8.Click += new System.EventHandler(this.experimentalGroupsToolStripMenuItem_Click);
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.ForeColor = System.Drawing.Color.Purple;
            this.toolStripButton7.Image = global::MetaboliteLevels.Properties.Resources.IconPca;
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
            this.toolStripButton2.ForeColor = System.Drawing.Color.Purple;
            this.toolStripButton2.Image = global::MetaboliteLevels.Properties.Resources.IconScriptCorrect;
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
            this.toolStripLabel2.ForeColor = System.Drawing.Color.Purple;
            this.toolStripLabel2.Image = global::MetaboliteLevels.Properties.Resources.MnuWorkflowSeparator;
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(24, 43);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.ForeColor = System.Drawing.Color.Purple;
            this.toolStripButton3.Image = global::MetaboliteLevels.Properties.Resources.IconScriptTrend;
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
            this.toolStripLabel3.ForeColor = System.Drawing.Color.Purple;
            this.toolStripLabel3.Image = global::MetaboliteLevels.Properties.Resources.MnuWorkflowSeparator;
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(24, 43);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.ForeColor = System.Drawing.Color.Purple;
            this.toolStripButton4.Image = global::MetaboliteLevels.Properties.Resources.IconScriptStatistic;
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
            this.toolStripLabel1.Image = global::MetaboliteLevels.Properties.Resources.MnuWorkflowSeparator;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(24, 43);
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.ForeColor = System.Drawing.Color.Purple;
            this.toolStripButton6.Image = global::MetaboliteLevels.Properties.Resources.IconScriptCluster;
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
            this.toolStripButton9.ForeColor = System.Drawing.Color.Purple;
            this.toolStripButton9.Image = global::MetaboliteLevels.Properties.Resources.IconScriptCluster;
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
            this.toolStripButton10.ForeColor = System.Drawing.Color.Purple;
            this.toolStripButton10.Image = global::MetaboliteLevels.Properties.Resources.IconScriptCluster;
            this.toolStripButton10.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton10.Name = "toolStripButton10";
            this.toolStripButton10.Size = new System.Drawing.Size(59, 43);
            this.toolStripButton10.Text = "Optimise";
            this.toolStripButton10.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton10.ToolTipText = "Cluster Wizard";
            this.toolStripButton10.Click += new System.EventHandler(this.clustererOptimiserToolStripMenuItem_Click);
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
            // _lstMatrix
            // 
            this._lstMatrix.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this._lstMatrix.ForeColor = System.Drawing.Color.Purple;
            this._lstMatrix.Image = global::MetaboliteLevels.Properties.Resources.IconMatrix;
            this._lstMatrix.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._lstMatrix.Name = "_lstMatrix";
            this._lstMatrix.Size = new System.Drawing.Size(48, 43);
            this._lstMatrix.Text = "Dataset";
            this._lstMatrix.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._lstMatrix.ToolTipText = "Cluster Wizard";
            this._lstMatrix.Click += new System.EventHandler(this._lstMatrix_Click);
            // 
            // _lstDatasetCb
            // 
            this._lstDatasetCb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstDatasetCb.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._lstDatasetCb.Name = "_lstDatasetCb";
            this._lstDatasetCb.Size = new System.Drawing.Size(121, 46);                                     
            this._lstDatasetCb.SelectedIndexChanged += new System.EventHandler(this._lstDatasetCb_SelectedIndexChanged);
            // 
            // _lstTrend
            // 
            this._lstTrend.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this._lstTrend.ForeColor = System.Drawing.Color.Purple;
            this._lstTrend.Image = global::MetaboliteLevels.Properties.Resources.IconMatrix;
            this._lstTrend.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._lstTrend.Name = "_lstTrend";
            this._lstTrend.Size = new System.Drawing.Size(39, 43);
            this._lstTrend.Text = "Trend";
            this._lstTrend.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._lstTrend.ToolTipText = "Cluster Wizard";
            this._lstTrend.Click += new System.EventHandler(this._lstTrend_Click);
            // 
            // _lstTrendCb
            // 
            this._lstTrendCb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstTrendCb.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._lstTrendCb.Name = "_lstTrendCb";
            this._lstTrendCb.Size = new System.Drawing.Size(121, 46);                                      
            this._lstTrendCb.SelectedIndexChanged += new System.EventHandler(this._lstTrendCb_SelectedIndexChanged);
            // 
            // _btnSession
            // 
            this._btnSession.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this._btnSession.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editNameAndCommentsToolStripMenuItem,
            this.sessionInformationToolStripMenuItem});
            this._btnSession.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnSession.Image = global::MetaboliteLevels.Properties.Resources.IconCore;
            this._btnSession.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnSession.Name = "_btnSession";
            this._btnSession.ShowDropDownArrow = false;
            this._btnSession.Size = new System.Drawing.Size(97, 43);
            this._btnSession.Text = "VALUE";
            // 
            // editNameAndCommentsToolStripMenuItem
            // 
            this.editNameAndCommentsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editNameAndCommentsToolStripMenuItem.Name = "editNameAndCommentsToolStripMenuItem";
            this.editNameAndCommentsToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
            this.editNameAndCommentsToolStripMenuItem.Text = "&Edit name and comments...";
            this.editNameAndCommentsToolStripMenuItem.Click += new System.EventHandler(this.editNameToolStripMenuItem_Click);
            // 
            // sessionInformationToolStripMenuItem
            // 
            this.sessionInformationToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sessionInformationToolStripMenuItem.Name = "sessionInformationToolStripMenuItem";
            this.sessionInformationToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
            this.sessionInformationToolStripMenuItem.Text = "&Session information...";
            this.sessionInformationToolStripMenuItem.Click += new System.EventHandler(this.sessionInformationToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 71);
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
            this.splitContainer1.Panel2.ForeColor = System.Drawing.Color.Black;
            this.splitContainer1.Size = new System.Drawing.Size(1185, 711);
            this.splitContainer1.SplitterDistance = 457;
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
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.panel1);
            this.splitContainer2.Size = new System.Drawing.Size(457, 711);
            this.splitContainer2.SplitterDistance = 352;
            this.splitContainer2.TabIndex = 11;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tabControl1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 44);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(457, 308);
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
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(457, 308);
            this.tabControl1.TabIndex = 10;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this._lstVariables);
            this.tabPage1.Location = new System.Drawing.Point(4, 56);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage1.Size = new System.Drawing.Size(449, 248);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Peaks";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // _lstVariables
            // 
            this._lstVariables.AllowColumnReorder = true;
            this._lstVariables.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lstVariables.FullRowSelect = true;
            this._lstVariables.GridLines = true;
            this._lstVariables.LargeImageList = this._imgList;
            this._lstVariables.Location = new System.Drawing.Point(4, 5);
            this._lstVariables.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this._lstVariables.MultiSelect = false;
            this._lstVariables.Name = "_lstVariables";
            this._lstVariables.Size = new System.Drawing.Size(441, 238);
            this._lstVariables.SmallImageList = this._imgList;
            this._lstVariables.TabIndex = 5;
            this._lstVariables.UseCompatibleStateImageBehavior = false;
            this._lstVariables.View = System.Windows.Forms.View.Details;
            this._lstVariables.KeyDown += new System.Windows.Forms.KeyEventHandler(this._lstVariables_KeyDown);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this._lstClusters);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage2.Size = new System.Drawing.Size(449, 282);
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
            this._lstClusters.Location = new System.Drawing.Point(4, 5);
            this._lstClusters.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this._lstClusters.MultiSelect = false;
            this._lstClusters.Name = "_lstClusters";
            this._lstClusters.Size = new System.Drawing.Size(441, 272);
            this._lstClusters.SmallImageList = this._imgList;
            this._lstClusters.TabIndex = 5;
            this._lstClusters.UseCompatibleStateImageBehavior = false;
            this._lstClusters.View = System.Windows.Forms.View.Details;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this._lstCompounds);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage3.Size = new System.Drawing.Size(449, 282);
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
            this._lstCompounds.Location = new System.Drawing.Point(4, 5);
            this._lstCompounds.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this._lstCompounds.MultiSelect = false;
            this._lstCompounds.Name = "_lstCompounds";
            this._lstCompounds.Size = new System.Drawing.Size(441, 272);
            this._lstCompounds.SmallImageList = this._imgList;
            this._lstCompounds.TabIndex = 6;
            this._lstCompounds.UseCompatibleStateImageBehavior = false;
            this._lstCompounds.View = System.Windows.Forms.View.Details;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this._lstAdducts);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage4.Size = new System.Drawing.Size(449, 282);
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
            this._lstAdducts.Location = new System.Drawing.Point(4, 5);
            this._lstAdducts.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this._lstAdducts.MultiSelect = false;
            this._lstAdducts.Name = "_lstAdducts";
            this._lstAdducts.Size = new System.Drawing.Size(441, 272);
            this._lstAdducts.SmallImageList = this._imgList;
            this._lstAdducts.TabIndex = 6;
            this._lstAdducts.UseCompatibleStateImageBehavior = false;
            this._lstAdducts.View = System.Windows.Forms.View.Details;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this._lstPathways);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage5.Size = new System.Drawing.Size(449, 282);
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
            this._lstPathways.Location = new System.Drawing.Point(4, 5);
            this._lstPathways.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this._lstPathways.MultiSelect = false;
            this._lstPathways.Name = "_lstPathways";
            this._lstPathways.Size = new System.Drawing.Size(441, 272);
            this._lstPathways.SmallImageList = this._imgList;
            this._lstPathways.TabIndex = 6;
            this._lstPathways.UseCompatibleStateImageBehavior = false;
            this._lstPathways.View = System.Windows.Forms.View.Details;
            // 
            // tabPage15
            // 
            this.tabPage15.Controls.Add(this._lstAssignments);
            this.tabPage15.Location = new System.Drawing.Point(4, 22);
            this.tabPage15.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage15.Name = "tabPage15";
            this.tabPage15.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage15.Size = new System.Drawing.Size(449, 282);
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
            this._lstAssignments.Location = new System.Drawing.Point(4, 5);
            this._lstAssignments.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this._lstAssignments.MultiSelect = false;
            this._lstAssignments.Name = "_lstAssignments";
            this._lstAssignments.Size = new System.Drawing.Size(441, 272);
            this._lstAssignments.SmallImageList = this._imgList;
            this._lstAssignments.TabIndex = 17;
            this._lstAssignments.UseCompatibleStateImageBehavior = false;
            this._lstAssignments.View = System.Windows.Forms.View.Details;
            // 
            // tabPage16
            // 
            this.tabPage16.Controls.Add(this._lstAnnotations);
            this.tabPage16.Location = new System.Drawing.Point(4, 22);
            this.tabPage16.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage16.Name = "tabPage16";
            this.tabPage16.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage16.Size = new System.Drawing.Size(449, 282);
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
            this._lstAnnotations.Location = new System.Drawing.Point(4, 5);
            this._lstAnnotations.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this._lstAnnotations.MultiSelect = false;
            this._lstAnnotations.Name = "_lstAnnotations";
            this._lstAnnotations.Size = new System.Drawing.Size(441, 272);
            this._lstAnnotations.SmallImageList = this._imgList;
            this._lstAnnotations.TabIndex = 7;
            this._lstAnnotations.UseCompatibleStateImageBehavior = false;
            this._lstAnnotations.View = System.Windows.Forms.View.Details;
            // 
            // _tsBarBrowser
            // 
            this._tsBarBrowser.BackColor = System.Drawing.SystemColors.Control;
            this._tsBarBrowser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this._tsBarBrowser.Location = new System.Drawing.Point(0, 0);
            this._tsBarBrowser.Name = "_tsBarBrowser";
            this._tsBarBrowser.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this._tsBarBrowser.Size = new System.Drawing.Size(457, 44);
            this._tsBarBrowser.TabIndex = 19;
            this._tsBarBrowser.Text = "toolStrip7";
            // 
            // _btnMain0
            // 
            this._btnMain0.AutoSize = false;
            this._btnMain0.Font = new System.Drawing.Font("Segoe UI Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnMain0.ForeColor = System.Drawing.Color.Black;
            this._btnMain0.Image = global::MetaboliteLevels.Properties.Resources.IconPeak;
            this._btnMain0.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnMain0.Margin = new System.Windows.Forms.Padding(0);
            this._btnMain0.Name = "_btnMain0";
            this._btnMain0.Size = new System.Drawing.Size(44, 44);
            this._btnMain0.Text = "Peak";
            this._btnMain0.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._btnMain0.ToolTipText = "Peaks";
            // 
            // _btnMain1
            // 
            this._btnMain1.AutoSize = false;
            this._btnMain1.Font = new System.Drawing.Font("Segoe UI Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnMain1.ForeColor = System.Drawing.Color.Black;
            this._btnMain1.Image = global::MetaboliteLevels.Properties.Resources.IconCluster;
            this._btnMain1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnMain1.Margin = new System.Windows.Forms.Padding(0);
            this._btnMain1.Name = "_btnMain1";
            this._btnMain1.Size = new System.Drawing.Size(44, 44);
            this._btnMain1.Text = "Clust";
            this._btnMain1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._btnMain1.ToolTipText = "Clusters";
            // 
            // _btnMain5
            // 
            this._btnMain5.AutoSize = false;
            this._btnMain5.Font = new System.Drawing.Font("Segoe UI Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnMain5.ForeColor = System.Drawing.Color.Black;
            this._btnMain5.Image = global::MetaboliteLevels.Properties.Resources.IconVector;
            this._btnMain5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnMain5.Margin = new System.Windows.Forms.Padding(0);
            this._btnMain5.Name = "_btnMain5";
            this._btnMain5.Size = new System.Drawing.Size(44, 44);
            this._btnMain5.Text = "Assig";
            this._btnMain5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._btnMain5.ToolTipText = "Assignments";
            // 
            // _btnMainAnnots
            // 
            this._btnMainAnnots.AutoSize = false;
            this._btnMainAnnots.Font = new System.Drawing.Font("Segoe UI Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnMainAnnots.ForeColor = System.Drawing.Color.Black;
            this._btnMainAnnots.Image = global::MetaboliteLevels.Properties.Resources.IconAnnotation;
            this._btnMainAnnots.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnMainAnnots.Margin = new System.Windows.Forms.Padding(0);
            this._btnMainAnnots.Name = "_btnMainAnnots";
            this._btnMainAnnots.Size = new System.Drawing.Size(44, 44);
            this._btnMainAnnots.Text = "Annot";
            this._btnMainAnnots.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._btnMainAnnots.ToolTipText = "Annotations";
            // 
            // _btnMain2
            // 
            this._btnMain2.AutoSize = false;
            this._btnMain2.Font = new System.Drawing.Font("Segoe UI Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnMain2.ForeColor = System.Drawing.Color.Black;
            this._btnMain2.Image = global::MetaboliteLevels.Properties.Resources.IconCompound;
            this._btnMain2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnMain2.Margin = new System.Windows.Forms.Padding(0);
            this._btnMain2.Name = "_btnMain2";
            this._btnMain2.Size = new System.Drawing.Size(44, 44);
            this._btnMain2.Text = "Comp";
            this._btnMain2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._btnMain2.ToolTipText = "Compounds";
            // 
            // _btnMain3
            // 
            this._btnMain3.AutoSize = false;
            this._btnMain3.Font = new System.Drawing.Font("Segoe UI Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnMain3.ForeColor = System.Drawing.Color.Black;
            this._btnMain3.Image = global::MetaboliteLevels.Properties.Resources.IconAdduct;
            this._btnMain3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnMain3.Margin = new System.Windows.Forms.Padding(0);
            this._btnMain3.Name = "_btnMain3";
            this._btnMain3.Size = new System.Drawing.Size(44, 44);
            this._btnMain3.Text = "Adduct";
            this._btnMain3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._btnMain3.ToolTipText = "Adducts";
            // 
            // _btnMain4
            // 
            this._btnMain4.AutoSize = false;
            this._btnMain4.Font = new System.Drawing.Font("Segoe UI Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnMain4.ForeColor = System.Drawing.Color.Black;
            this._btnMain4.Image = global::MetaboliteLevels.Properties.Resources.IconPathway;
            this._btnMain4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnMain4.Margin = new System.Windows.Forms.Padding(0);
            this._btnMain4.Name = "_btnMain4";
            this._btnMain4.Size = new System.Drawing.Size(44, 44);
            this._btnMain4.Text = "Path";
            this._btnMain4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._btnMain4.ToolTipText = "Pathways";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this._tsBarSelection);
            this.panel1.Controls.Add(this.toolStrip2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(457, 353);
            this.panel1.TabIndex = 7;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this._tabSubinfo);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 76);
            this.panel4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(457, 277);
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
            this._tabSubinfo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this._tabSubinfo.Multiline = true;
            this._tabSubinfo.Name = "_tabSubinfo";
            this._tabSubinfo.SelectedIndex = 0;
            this._tabSubinfo.Size = new System.Drawing.Size(457, 277);
            this._tabSubinfo.TabIndex = 20;
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this._lst2Info);
            this.tabPage8.Location = new System.Drawing.Point(4, 56);
            this.tabPage8.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage8.Size = new System.Drawing.Size(449, 217);
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
            this._lst2Info.Location = new System.Drawing.Point(4, 5);
            this._lst2Info.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this._lst2Info.Name = "_lst2Info";
            this._lst2Info.Size = new System.Drawing.Size(441, 207);
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
            this.tabPage9.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage9.Size = new System.Drawing.Size(449, 233);
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
            this._lst2Stats.Location = new System.Drawing.Point(4, 5);
            this._lst2Stats.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this._lst2Stats.Name = "_lst2Stats";
            this._lst2Stats.Size = new System.Drawing.Size(441, 223);
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
            this.tabPage10.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage10.Name = "tabPage10";
            this.tabPage10.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage10.Size = new System.Drawing.Size(449, 233);
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
            this._lst2Peaks.Location = new System.Drawing.Point(4, 5);
            this._lst2Peaks.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this._lst2Peaks.MultiSelect = false;
            this._lst2Peaks.Name = "_lst2Peaks";
            this._lst2Peaks.Size = new System.Drawing.Size(441, 223);
            this._lst2Peaks.SmallImageList = this._imgList;
            this._lst2Peaks.TabIndex = 6;
            this._lst2Peaks.UseCompatibleStateImageBehavior = false;
            this._lst2Peaks.View = System.Windows.Forms.View.Details;
            // 
            // tabPage11
            // 
            this.tabPage11.Controls.Add(this._lst2Clusters);
            this.tabPage11.Location = new System.Drawing.Point(4, 40);
            this.tabPage11.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage11.Name = "tabPage11";
            this.tabPage11.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage11.Size = new System.Drawing.Size(449, 233);
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
            this._lst2Clusters.Location = new System.Drawing.Point(4, 5);
            this._lst2Clusters.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this._lst2Clusters.MultiSelect = false;
            this._lst2Clusters.Name = "_lst2Clusters";
            this._lst2Clusters.Size = new System.Drawing.Size(441, 223);
            this._lst2Clusters.SmallImageList = this._imgList;
            this._lst2Clusters.TabIndex = 6;
            this._lst2Clusters.UseCompatibleStateImageBehavior = false;
            this._lst2Clusters.View = System.Windows.Forms.View.Details;
            // 
            // tabPage12
            // 
            this.tabPage12.Controls.Add(this._lst2Compounds);
            this.tabPage12.Location = new System.Drawing.Point(4, 40);
            this.tabPage12.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage12.Name = "tabPage12";
            this.tabPage12.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage12.Size = new System.Drawing.Size(449, 233);
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
            this._lst2Compounds.Location = new System.Drawing.Point(4, 5);
            this._lst2Compounds.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this._lst2Compounds.MultiSelect = false;
            this._lst2Compounds.Name = "_lst2Compounds";
            this._lst2Compounds.Size = new System.Drawing.Size(441, 223);
            this._lst2Compounds.SmallImageList = this._imgList;
            this._lst2Compounds.TabIndex = 7;
            this._lst2Compounds.UseCompatibleStateImageBehavior = false;
            this._lst2Compounds.View = System.Windows.Forms.View.Details;
            // 
            // tabPage13
            // 
            this.tabPage13.Controls.Add(this._lst2Adducts);
            this.tabPage13.Location = new System.Drawing.Point(4, 40);
            this.tabPage13.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage13.Name = "tabPage13";
            this.tabPage13.Size = new System.Drawing.Size(449, 233);
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
            this._lst2Adducts.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this._lst2Adducts.MultiSelect = false;
            this._lst2Adducts.Name = "_lst2Adducts";
            this._lst2Adducts.Size = new System.Drawing.Size(449, 233);
            this._lst2Adducts.TabIndex = 7;
            this._lst2Adducts.UseCompatibleStateImageBehavior = false;
            this._lst2Adducts.View = System.Windows.Forms.View.Details;
            // 
            // tabPage14
            // 
            this.tabPage14.Controls.Add(this._lst2Pathways);
            this.tabPage14.Location = new System.Drawing.Point(4, 40);
            this.tabPage14.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage14.Name = "tabPage14";
            this.tabPage14.Size = new System.Drawing.Size(449, 233);
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
            this._lst2Pathways.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this._lst2Pathways.MultiSelect = false;
            this._lst2Pathways.Name = "_lst2Pathways";
            this._lst2Pathways.Size = new System.Drawing.Size(449, 233);
            this._lst2Pathways.SmallImageList = this._imgList;
            this._lst2Pathways.TabIndex = 7;
            this._lst2Pathways.UseCompatibleStateImageBehavior = false;
            this._lst2Pathways.View = System.Windows.Forms.View.Details;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this._lst2Assignments);
            this.tabPage6.Location = new System.Drawing.Point(4, 40);
            this.tabPage6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage6.Size = new System.Drawing.Size(449, 233);
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
            this._lst2Assignments.Location = new System.Drawing.Point(4, 5);
            this._lst2Assignments.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this._lst2Assignments.MultiSelect = false;
            this._lst2Assignments.Name = "_lst2Assignments";
            this._lst2Assignments.Size = new System.Drawing.Size(441, 223);
            this._lst2Assignments.SmallImageList = this._imgList;
            this._lst2Assignments.TabIndex = 18;
            this._lst2Assignments.UseCompatibleStateImageBehavior = false;
            this._lst2Assignments.View = System.Windows.Forms.View.Details;
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this._lstSubAnnots);
            this.tabPage7.Location = new System.Drawing.Point(4, 40);
            this.tabPage7.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage7.Size = new System.Drawing.Size(449, 233);
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
            this._lstSubAnnots.Location = new System.Drawing.Point(4, 5);
            this._lstSubAnnots.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this._lstSubAnnots.MultiSelect = false;
            this._lstSubAnnots.Name = "_lstSubAnnots";
            this._lstSubAnnots.Size = new System.Drawing.Size(441, 223);
            this._lstSubAnnots.SmallImageList = this._imgList;
            this._lstSubAnnots.TabIndex = 8;
            this._lstSubAnnots.UseCompatibleStateImageBehavior = false;
            this._lstSubAnnots.View = System.Windows.Forms.View.Details;
            // 
            // _tsBarSelection
            // 
            this._tsBarSelection.BackColor = System.Drawing.SystemColors.Control;
            this._tsBarSelection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._tsBarSelection.ImageScalingSize = new System.Drawing.Size(24, 24);
            this._tsBarSelection.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this._tsBarSelection.Location = new System.Drawing.Point(0, 32);
            this._tsBarSelection.Name = "_tsBarSelection";
            this._tsBarSelection.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this._tsBarSelection.Size = new System.Drawing.Size(457, 44);
            this._tsBarSelection.TabIndex = 18;
            this._tsBarSelection.Text = "toolStrip4";
            // 
            // _btnSubInfo
            // 
            this._btnSubInfo.AutoSize = false;
            this._btnSubInfo.Font = new System.Drawing.Font("Segoe UI Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnSubInfo.ForeColor = System.Drawing.Color.Black;
            this._btnSubInfo.Image = global::MetaboliteLevels.Properties.Resources.IconInformation;
            this._btnSubInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnSubInfo.Margin = new System.Windows.Forms.Padding(0);
            this._btnSubInfo.Name = "_btnSubInfo";
            this._btnSubInfo.Size = new System.Drawing.Size(44, 44);
            this._btnSubInfo.Text = "Info";
            this._btnSubInfo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._btnSubInfo.ToolTipText = "Information";
            // 
            // _btnSubStat
            // 
            this._btnSubStat.AutoSize = false;
            this._btnSubStat.Font = new System.Drawing.Font("Segoe UI Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnSubStat.ForeColor = System.Drawing.Color.Black;
            this._btnSubStat.Image = global::MetaboliteLevels.Properties.Resources.IconStatistics;
            this._btnSubStat.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnSubStat.Margin = new System.Windows.Forms.Padding(0);
            this._btnSubStat.Name = "_btnSubStat";
            this._btnSubStat.Size = new System.Drawing.Size(44, 44);
            this._btnSubStat.Text = "Stat";
            this._btnSubStat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._btnSubStat.ToolTipText = "Statistics";
            // 
            // _btnSubPeak
            // 
            this._btnSubPeak.AutoSize = false;
            this._btnSubPeak.Font = new System.Drawing.Font("Segoe UI Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnSubPeak.ForeColor = System.Drawing.Color.Black;
            this._btnSubPeak.Image = global::MetaboliteLevels.Properties.Resources.IconPeak;
            this._btnSubPeak.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnSubPeak.Margin = new System.Windows.Forms.Padding(0);
            this._btnSubPeak.Name = "_btnSubPeak";
            this._btnSubPeak.Size = new System.Drawing.Size(44, 44);
            this._btnSubPeak.Text = "Peak";
            this._btnSubPeak.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._btnSubPeak.ToolTipText = "Peaks";
            // 
            // _btnSubPat
            // 
            this._btnSubPat.AutoSize = false;
            this._btnSubPat.Font = new System.Drawing.Font("Segoe UI Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnSubPat.ForeColor = System.Drawing.Color.Black;
            this._btnSubPat.Image = global::MetaboliteLevels.Properties.Resources.IconCluster;
            this._btnSubPat.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnSubPat.Margin = new System.Windows.Forms.Padding(0);
            this._btnSubPat.Name = "_btnSubPat";
            this._btnSubPat.Size = new System.Drawing.Size(44, 44);
            this._btnSubPat.Text = "Clust";
            this._btnSubPat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._btnSubPat.ToolTipText = "Clusters";
            // 
            // _btnSubAss
            // 
            this._btnSubAss.AutoSize = false;
            this._btnSubAss.Font = new System.Drawing.Font("Segoe UI Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnSubAss.ForeColor = System.Drawing.Color.Black;
            this._btnSubAss.Image = global::MetaboliteLevels.Properties.Resources.IconVector;
            this._btnSubAss.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnSubAss.Margin = new System.Windows.Forms.Padding(0);
            this._btnSubAss.Name = "_btnSubAss";
            this._btnSubAss.Size = new System.Drawing.Size(44, 44);
            this._btnSubAss.Text = "Assig";
            this._btnSubAss.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._btnSubAss.ToolTipText = "Assignments";
            // 
            // _btnSubAnnot
            // 
            this._btnSubAnnot.AutoSize = false;
            this._btnSubAnnot.Font = new System.Drawing.Font("Segoe UI Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnSubAnnot.ForeColor = System.Drawing.Color.Black;
            this._btnSubAnnot.Image = global::MetaboliteLevels.Properties.Resources.IconAnnotation;
            this._btnSubAnnot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnSubAnnot.Margin = new System.Windows.Forms.Padding(0);
            this._btnSubAnnot.Name = "_btnSubAnnot";
            this._btnSubAnnot.Size = new System.Drawing.Size(44, 44);
            this._btnSubAnnot.Text = "Annot";
            this._btnSubAnnot.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._btnSubAnnot.ToolTipText = "Annotations";
            // 
            // _btnSubComp
            // 
            this._btnSubComp.AutoSize = false;
            this._btnSubComp.Font = new System.Drawing.Font("Segoe UI Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnSubComp.ForeColor = System.Drawing.Color.Black;
            this._btnSubComp.Image = global::MetaboliteLevels.Properties.Resources.IconCompound;
            this._btnSubComp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnSubComp.Margin = new System.Windows.Forms.Padding(0);
            this._btnSubComp.Name = "_btnSubComp";
            this._btnSubComp.Size = new System.Drawing.Size(44, 44);
            this._btnSubComp.Text = "Comp";
            this._btnSubComp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._btnSubComp.ToolTipText = "Compounds";
            // 
            // _btnSubAdd
            // 
            this._btnSubAdd.AutoSize = false;
            this._btnSubAdd.Font = new System.Drawing.Font("Segoe UI Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnSubAdd.ForeColor = System.Drawing.Color.Black;
            this._btnSubAdd.Image = global::MetaboliteLevels.Properties.Resources.IconAdduct;
            this._btnSubAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnSubAdd.Margin = new System.Windows.Forms.Padding(0);
            this._btnSubAdd.Name = "_btnSubAdd";
            this._btnSubAdd.Size = new System.Drawing.Size(44, 44);
            this._btnSubAdd.Text = "Adduct";
            this._btnSubAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._btnSubAdd.ToolTipText = "Adducts";
            // 
            // _btnSubPath
            // 
            this._btnSubPath.AutoSize = false;
            this._btnSubPath.Font = new System.Drawing.Font("Segoe UI Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnSubPath.ForeColor = System.Drawing.Color.Black;
            this._btnSubPath.Image = global::MetaboliteLevels.Properties.Resources.IconPathway;
            this._btnSubPath.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnSubPath.Margin = new System.Windows.Forms.Padding(0);
            this._btnSubPath.Name = "_btnSubPath";
            this._btnSubPath.Size = new System.Drawing.Size(44, 44);
            this._btnSubPath.Text = "Path";
            this._btnSubPath.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._btnSubPath.ToolTipText = "Pathways";
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._btnBack,
            this._btnSelection,
            this._btnExterior,
            this._btnSelectionExterior});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(457, 32);
            this.toolStrip2.TabIndex = 23;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // _btnBack
            // 
            this._btnBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._btnBack.Image = global::MetaboliteLevels.Properties.Resources.IconBack;
            this._btnBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnBack.Margin = new System.Windows.Forms.Padding(8, 1, 0, 2);
            this._btnBack.Name = "_btnBack";
            this._btnBack.Size = new System.Drawing.Size(32, 29);
            this._btnBack.Text = "Back to previous selection";
            this._btnBack.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._btnBack.ToolTipText = "Click here to view and go back to previous selections.";
            this._btnBack.ButtonClick += new System.EventHandler(this._btnBack_ButtonClick);
            this._btnBack.DropDownOpening += new System.EventHandler(this._btnBack_DropDownOpening);
            // 
            // _btnSelection
            // 
            this._btnSelection.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnSelection.Image = global::MetaboliteLevels.Properties.Resources.IconCore;
            this._btnSelection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnSelection.Name = "_btnSelection";
            this._btnSelection.ShowDropDownArrow = false;
            this._btnSelection.Size = new System.Drawing.Size(89, 29);
            this._btnSelection.Text = "VALUE";
            this._btnSelection.ToolTipText = "The currently selected item is described here.";
            this._btnSelection.DropDownOpening += new System.EventHandler(this._btnSelection_DropDownOpening);
            // 
            // _btnExterior
            // 
            this._btnExterior.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._btnExterior.Image = ((System.Drawing.Image)(resources.GetObject("_btnExterior.Image")));
            this._btnExterior.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnExterior.Name = "_btnExterior";
            this._btnExterior.Size = new System.Drawing.Size(23, 29);
            this._btnExterior.Text = "toolStripButton11";
            this._btnExterior.ToolTipText = "If you have selected one item \"inside\" another you can click here to swap the sel" +
    "ection and display the items relationship from the second items perspective.\r\n";
            this._btnExterior.Click += new System.EventHandler(this._btnExterior_Click);
            // 
            // _btnSelectionExterior
            // 
            this._btnSelectionExterior.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnSelectionExterior.Image = global::MetaboliteLevels.Properties.Resources.IconCore;
            this._btnSelectionExterior.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnSelectionExterior.Name = "_btnSelectionExterior";
            this._btnSelectionExterior.ShowDropDownArrow = false;
            this._btnSelectionExterior.Size = new System.Drawing.Size(89, 29);
            this._btnSelectionExterior.Text = "VALUE";
            this._btnSelectionExterior.ToolTipText = "If you have selected one item \"inside\" another, that item is displayed here.\r\nThi" +
    "s item will be described using the perspective of the first item.\r\nClick the swa" +
    "p button to reverse the perspective.";
            this._btnSelectionExterior.DropDownOpening += new System.EventHandler(this._btnSelectionExterior_DropDownOpening);
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitContainer3.Size = new System.Drawing.Size(722, 711);
            this.splitContainer3.SplitterDistance = 349;
            this.splitContainer3.TabIndex = 10;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1185, 804);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this._statusMain);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this._mnuMain);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this._mnuMain;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Metabolite Clusters";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this._mnuMain.ResumeLayout(false);
            this._mnuMain.PerformLayout();
            this._cmsSelectionButton.ResumeLayout(false);
            this._statusMain.ResumeLayout(false);
            this._statusMain.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
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
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage15.ResumeLayout(false);
            this.tabPage16.ResumeLayout(false);
            this._tsBarBrowser.ResumeLayout(false);
            this._tsBarBrowser.PerformLayout();
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
            this.tabPage7.ResumeLayout(false);
            this._tsBarSelection.ResumeLayout(false);
            this._tsBarSelection.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip _mnuMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadDataSetToolStripMenuItem;
        private System.Windows.Forms.ListView _lstVariables;
        private MGui.Controls.CtlSplitter splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem saveExemplarsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.StatusStrip _statusMain;
        private System.Windows.Forms.ToolStripStatusLabel _lblChanges;
        private MGui.Controls.CtlSplitter splitContainer3;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripMenuItem printClusterToolStripMenuItem;
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
        private System.Windows.Forms.ToolStripMenuItem visualOptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private MGui.Controls.CtlSplitter splitContainer2;
        private System.Windows.Forms.ToolStripMenuItem saveSessionAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStrip _tsBarSelection;
        private System.Windows.Forms.Panel panel1;
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
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ListView _lstClusters;
        private System.Windows.Forms.ToolTip _toolTipMain;
        private System.Windows.Forms.ImageList _imgListClusters;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip _cmsSelectionButton;
        private System.Windows.Forms.ToolStripMenuItem experimentalOptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem editStatisticsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editCorrectionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem edittrendToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createclustersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem experimentalGroupsToolStripMenuItem;
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
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem peakFiltersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem observationFiltersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autogenerateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pCAToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem10;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton toolStripButton9;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.ListView _lst2Assignments;
        private System.Windows.Forms.ToolStripButton _btnSubAss;
        private System.Windows.Forms.TabPage tabPage15;
        private System.Windows.Forms.ListView _lstAssignments;
        private System.Windows.Forms.ToolStripButton _btnMain0;
        private System.Windows.Forms.ToolStripButton _btnMain1;
        private System.Windows.Forms.ToolStripButton toolStripButton10;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem peakidentificationsToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.ListView _lstSubAnnots;
        private System.Windows.Forms.ToolStripButton _btnSubAnnot;
        private System.Windows.Forms.TabPage tabPage16;
        private System.Windows.Forms.ListView _lstAnnotations;
        private System.Windows.Forms.ToolStripButton _btnMainAnnots;
        private System.Windows.Forms.ToolStripMenuItem clusterParameterOptimiserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editNameAndCommentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton _btnSession;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripDropDownButton _btnSelection;
        private System.Windows.Forms.ToolStripDropDownButton _btnSelectionExterior;
        private System.Windows.Forms.ToolStripSplitButton _btnBack;
        private System.Windows.Forms.ToolStripMenuItem sessionInformationToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton _btnExterior;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel _txtGuid;
        private System.Windows.Forms.ToolStripMenuItem pLACEHOLDERToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem databaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem correlationMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton _btnOpen;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton _lstTrend;
        private System.Windows.Forms.ToolStripButton _lstMatrix;
        private System.Windows.Forms.ToolStripComboBox _lstDatasetCb;
        private System.Windows.Forms.ToolStripComboBox _lstTrendCb;
    }
}

