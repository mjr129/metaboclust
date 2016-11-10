using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaboliteLevels.Gui.Forms.Activities
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
            if (disposing && (this.components != null))
            {                       
                this.components.Dispose();
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
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this._btnSession = new System.Windows.Forms.ToolStripDropDownButton();
            this.editNameAndCommentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sessionInformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._tssInsertViewsEnd = new System.Windows.Forms.ToolStripLabel();
            this._tssInsertViews = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton8 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this._lstTrendCb = new System.Windows.Forms.ToolStripComboBox();
            this._lstTrend = new System.Windows.Forms.ToolStripButton();
            this._lstDatasetCb = new System.Windows.Forms.ToolStripComboBox();
            this._lstMatrix = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.splitContainer1 = new MGui.Controls.CtlSplitter();
            this.splitContainer2 = new MGui.Controls.CtlSplitter();
            this.panel3 = new System.Windows.Forms.Panel();
            this._lstPrimary = new System.Windows.Forms.ListView();
            this._tsDatasetsPrimary = new System.Windows.Forms.ToolStrip();
            this._btnPrimPeak = new System.Windows.Forms.ToolStripButton();
            this._btnPrimClust = new System.Windows.Forms.ToolStripButton();
            this._btnPrimAssig = new System.Windows.Forms.ToolStripButton();
            this._btnPrimAnnot = new System.Windows.Forms.ToolStripButton();
            this._btnPrimComp = new System.Windows.Forms.ToolStripButton();
            this._btnPrimAdduct = new System.Windows.Forms.ToolStripButton();
            this._btnPrimPath = new System.Windows.Forms.ToolStripButton();
            this._btnPrimOther = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this._lstSecondary = new System.Windows.Forms.ListView();
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
            this._btnSubOther = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this._btnBack = new System.Windows.Forms.ToolStripSplitButton();
            this._btnPrimarySelection = new System.Windows.Forms.ToolStripDropDownButton();
            this._btnSwapSelections = new System.Windows.Forms.ToolStripButton();
            this._btnSecondarySelection = new System.Windows.Forms.ToolStripDropDownButton();
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
            this._tsDatasetsPrimary.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
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
            this.pCAToolStripMenuItem.Text = "&PCA / PLSR";
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
            this.toolStripStatusLabel2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel2.ForeColor = System.Drawing.Color.Black;
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
            this.toolStrip1.CanOverflow = false;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._btnOpen,
            this.toolStripButton1,
            this.toolStripButton5,
            this.toolStripLabel1,
            this.toolStripButton7,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripButton4,
            this.toolStripButton6,
            this._btnSession,
            this._tssInsertViewsEnd,
            this._tssInsertViews,
            this.toolStripButton8,
            this.toolStripLabel4,
            this._lstTrendCb,
            this._lstTrend,
            this._lstDatasetCb,
            this._lstMatrix,
            this.toolStripLabel2});
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
            this._btnOpen.ToolTipText = "Create or open a session";
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
            this.toolStripButton1.ToolTipText = "Save the current session";
            this.toolStripButton1.Click += new System.EventHandler(this.saveExemplarsToolStripMenuItem_Click);
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
            this.toolStripButton5.ToolTipText = "Program preferences";
            this.toolStripButton5.Click += new System.EventHandler(this.visualOptionsToolStripMenuItem_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Image = global::MetaboliteLevels.Properties.Resources.Separator;
            this.toolStripLabel1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.toolStripLabel1.Size = new System.Drawing.Size(32, 43);
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.ForeColor = System.Drawing.Color.Purple;
            this.toolStripButton7.Image = global::MetaboliteLevels.Properties.Resources.IconPca;
            this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(37, 43);
            this.toolStripButton7.Text = "MVA";
            this.toolStripButton7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton7.ToolTipText = "PCA / PLSR";
            this.toolStripButton7.Click += new System.EventHandler(this.pCAToolStripMenuItem_Click);
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
            this.toolStripButton2.ToolTipText = "Define data corrections";
            this.toolStripButton2.Click += new System.EventHandler(this.editCorrectionsToolStripMenuItem_Click);
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
            this.toolStripButton3.ToolTipText = "Define trend functions";
            this.toolStripButton3.Click += new System.EventHandler(this.edittrendToolStripMenuItem_Click);
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
            this.toolStripButton4.ToolTipText = "Define statistics";
            this.toolStripButton4.Click += new System.EventHandler(this.editStatisticsToolStripMenuItem_Click);
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
            this.toolStripButton6.ToolTipText = "Define clustering algorithms";
            this.toolStripButton6.Click += new System.EventHandler(this.createclustersToolStripMenuItem_Click);
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
            this._btnSession.ToolTipText = "Shows the current session, click to edit";
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
            // _tssInsertViewsEnd
            // 
            this._tssInsertViewsEnd.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this._tssInsertViewsEnd.Image = global::MetaboliteLevels.Properties.Resources.Separator;
            this._tssInsertViewsEnd.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._tssInsertViewsEnd.Name = "_tssInsertViewsEnd";
            this._tssInsertViewsEnd.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this._tssInsertViewsEnd.Size = new System.Drawing.Size(32, 43);
            // 
            // _tssInsertViews
            // 
            this._tssInsertViews.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this._tssInsertViews.AutoSize = false;
            this._tssInsertViews.Name = "_tssInsertViews";
            this._tssInsertViews.Size = new System.Drawing.Size(6, 36);
            // 
            // toolStripButton8
            // 
            this.toolStripButton8.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton8.ForeColor = System.Drawing.Color.Purple;
            this.toolStripButton8.Image = global::MetaboliteLevels.Properties.Resources.IconGroups;
            this.toolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton8.Name = "toolStripButton8";
            this.toolStripButton8.Size = new System.Drawing.Size(49, 43);
            this.toolStripButton8.Text = "Groups";
            this.toolStripButton8.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton8.ToolTipText = "Modify experimental group information";
            this.toolStripButton8.Click += new System.EventHandler(this.experimentalGroupsToolStripMenuItem_Click);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel4.Image = global::MetaboliteLevels.Properties.Resources.Separator;
            this.toolStripLabel4.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.toolStripLabel4.Size = new System.Drawing.Size(32, 43);
            // 
            // _lstTrendCb
            // 
            this._lstTrendCb.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this._lstTrendCb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstTrendCb.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._lstTrendCb.Name = "_lstTrendCb";
            this._lstTrendCb.Size = new System.Drawing.Size(121, 46);
            this._lstTrendCb.ToolTipText = "Select the trend algorithm you\'d like to use in the plots.\r\n(Note that cluster pl" +
    "ots will always use the trend function they were created with)";
            this._lstTrendCb.SelectedIndexChanged += new System.EventHandler(this._lstTrendCb_SelectedIndexChanged);
            // 
            // _lstTrend
            // 
            this._lstTrend.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this._lstTrend.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this._lstTrend.ForeColor = System.Drawing.Color.Purple;
            this._lstTrend.Image = global::MetaboliteLevels.Properties.Resources.ListIconResultMatrix;
            this._lstTrend.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._lstTrend.Name = "_lstTrend";
            this._lstTrend.Size = new System.Drawing.Size(39, 43);
            this._lstTrend.Text = "Trend";
            this._lstTrend.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._lstTrend.ToolTipText = "Select the trend algorithm you\'d like to use in the plots.\r\n(Note that cluster pl" +
    "ots will always use the trend function they were created with)";
            this._lstTrend.Click += new System.EventHandler(this._lstTrend_Click);
            // 
            // _lstDatasetCb
            // 
            this._lstDatasetCb.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this._lstDatasetCb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstDatasetCb.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._lstDatasetCb.Name = "_lstDatasetCb";
            this._lstDatasetCb.Size = new System.Drawing.Size(121, 46);
            this._lstDatasetCb.ToolTipText = "Select the intensity matrix you\'d like to use in the plots.\r\n(Note that clusters " +
    "will always display using the intensity matrix they were created with)\r\n";
            this._lstDatasetCb.SelectedIndexChanged += new System.EventHandler(this._lstDatasetCb_SelectedIndexChanged);
            // 
            // _lstMatrix
            // 
            this._lstMatrix.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this._lstMatrix.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this._lstMatrix.ForeColor = System.Drawing.Color.Purple;
            this._lstMatrix.Image = global::MetaboliteLevels.Properties.Resources.ListIconResultMatrix;
            this._lstMatrix.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._lstMatrix.Name = "_lstMatrix";
            this._lstMatrix.Size = new System.Drawing.Size(48, 43);
            this._lstMatrix.Text = "Dataset";
            this._lstMatrix.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._lstMatrix.ToolTipText = "Select the intensity matrix you\'d like to use in the plots.\r\n(Note that clusters " +
    "will always display using the intensity matrix they were created with)";
            this._lstMatrix.Click += new System.EventHandler(this._lstMatrix_Click);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel2.Image = global::MetaboliteLevels.Properties.Resources.Separator;
            this.toolStripLabel2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.toolStripLabel2.Size = new System.Drawing.Size(32, 43);
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
            this.splitContainer2.Panel1.Controls.Add(this._tsDatasetsPrimary);
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
            this.panel3.Controls.Add(this._lstPrimary);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 44);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(457, 308);
            this.panel3.TabIndex = 11;
            // 
            // _lstPrimary
            // 
            this._lstPrimary.AllowColumnReorder = true;
            this._lstPrimary.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lstPrimary.FullRowSelect = true;
            this._lstPrimary.GridLines = true;
            this._lstPrimary.Location = new System.Drawing.Point(0, 0);
            this._lstPrimary.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this._lstPrimary.MultiSelect = false;
            this._lstPrimary.Name = "_lstPrimary";
            this._lstPrimary.Size = new System.Drawing.Size(457, 308);
            this._lstPrimary.TabIndex = 5;
            this._lstPrimary.UseCompatibleStateImageBehavior = false;
            this._lstPrimary.View = System.Windows.Forms.View.Details;                                       
            // 
            // _tsDatasetsPrimary
            // 
            this._tsDatasetsPrimary.BackColor = System.Drawing.SystemColors.Control;
            this._tsDatasetsPrimary.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._tsDatasetsPrimary.ImageScalingSize = new System.Drawing.Size(24, 24);
            this._tsDatasetsPrimary.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._btnPrimPeak,
            this._btnPrimClust,
            this._btnPrimAssig,
            this._btnPrimAnnot,
            this._btnPrimComp,
            this._btnPrimAdduct,
            this._btnPrimPath,
            this._btnPrimOther});
            this._tsDatasetsPrimary.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this._tsDatasetsPrimary.Location = new System.Drawing.Point(0, 0);
            this._tsDatasetsPrimary.Name = "_tsDatasetsPrimary";
            this._tsDatasetsPrimary.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this._tsDatasetsPrimary.Size = new System.Drawing.Size(457, 44);
            this._tsDatasetsPrimary.TabIndex = 19;
            this._tsDatasetsPrimary.Text = "toolStrip7";
            // 
            // _btnPrimPeak
            // 
            this._btnPrimPeak.AutoSize = false;
            this._btnPrimPeak.Font = new System.Drawing.Font("Segoe UI Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnPrimPeak.ForeColor = System.Drawing.Color.Black;
            this._btnPrimPeak.Image = global::MetaboliteLevels.Properties.Resources.IconPeak;
            this._btnPrimPeak.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnPrimPeak.Margin = new System.Windows.Forms.Padding(0);
            this._btnPrimPeak.Name = "_btnPrimPeak";
            this._btnPrimPeak.Size = new System.Drawing.Size(44, 44);
            this._btnPrimPeak.Text = "Peak";
            this._btnPrimPeak.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._btnPrimPeak.ToolTipText = "Peaks";
            this._btnPrimPeak.Click += new System.EventHandler(this._btnPrimPeak_Click);
            // 
            // _btnPrimClust
            // 
            this._btnPrimClust.AutoSize = false;
            this._btnPrimClust.Font = new System.Drawing.Font("Segoe UI Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnPrimClust.ForeColor = System.Drawing.Color.Black;
            this._btnPrimClust.Image = global::MetaboliteLevels.Properties.Resources.IconCluster;
            this._btnPrimClust.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnPrimClust.Margin = new System.Windows.Forms.Padding(0);
            this._btnPrimClust.Name = "_btnPrimClust";
            this._btnPrimClust.Size = new System.Drawing.Size(44, 44);
            this._btnPrimClust.Text = "Clust";
            this._btnPrimClust.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._btnPrimClust.ToolTipText = "Clusters";
            this._btnPrimClust.Click += new System.EventHandler(this._btnPrimPeak_Click);
            // 
            // _btnPrimAssig
            // 
            this._btnPrimAssig.AutoSize = false;
            this._btnPrimAssig.Font = new System.Drawing.Font("Segoe UI Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnPrimAssig.ForeColor = System.Drawing.Color.Black;
            this._btnPrimAssig.Image = global::MetaboliteLevels.Properties.Resources.IconVector;
            this._btnPrimAssig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnPrimAssig.Margin = new System.Windows.Forms.Padding(0);
            this._btnPrimAssig.Name = "_btnPrimAssig";
            this._btnPrimAssig.Size = new System.Drawing.Size(44, 44);
            this._btnPrimAssig.Text = "Assig";
            this._btnPrimAssig.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._btnPrimAssig.ToolTipText = "Assignments";
            this._btnPrimAssig.Click += new System.EventHandler(this._btnPrimPeak_Click);
            // 
            // _btnPrimAnnot
            // 
            this._btnPrimAnnot.AutoSize = false;
            this._btnPrimAnnot.Font = new System.Drawing.Font("Segoe UI Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnPrimAnnot.ForeColor = System.Drawing.Color.Black;
            this._btnPrimAnnot.Image = global::MetaboliteLevels.Properties.Resources.IconAnnotation;
            this._btnPrimAnnot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnPrimAnnot.Margin = new System.Windows.Forms.Padding(0);
            this._btnPrimAnnot.Name = "_btnPrimAnnot";
            this._btnPrimAnnot.Size = new System.Drawing.Size(44, 44);
            this._btnPrimAnnot.Text = "Annot";
            this._btnPrimAnnot.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._btnPrimAnnot.ToolTipText = "Annotations";
            this._btnPrimAnnot.Click += new System.EventHandler(this._btnPrimPeak_Click);
            // 
            // _btnPrimComp
            // 
            this._btnPrimComp.AutoSize = false;
            this._btnPrimComp.Font = new System.Drawing.Font("Segoe UI Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnPrimComp.ForeColor = System.Drawing.Color.Black;
            this._btnPrimComp.Image = global::MetaboliteLevels.Properties.Resources.IconCompound;
            this._btnPrimComp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnPrimComp.Margin = new System.Windows.Forms.Padding(0);
            this._btnPrimComp.Name = "_btnPrimComp";
            this._btnPrimComp.Size = new System.Drawing.Size(44, 44);
            this._btnPrimComp.Text = "Comp";
            this._btnPrimComp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._btnPrimComp.ToolTipText = "Compounds";
            this._btnPrimComp.Click += new System.EventHandler(this._btnPrimPeak_Click);
            // 
            // _btnPrimAdduct
            // 
            this._btnPrimAdduct.AutoSize = false;
            this._btnPrimAdduct.Font = new System.Drawing.Font("Segoe UI Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnPrimAdduct.ForeColor = System.Drawing.Color.Black;
            this._btnPrimAdduct.Image = global::MetaboliteLevels.Properties.Resources.IconAdduct;
            this._btnPrimAdduct.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnPrimAdduct.Margin = new System.Windows.Forms.Padding(0);
            this._btnPrimAdduct.Name = "_btnPrimAdduct";
            this._btnPrimAdduct.Size = new System.Drawing.Size(44, 44);
            this._btnPrimAdduct.Text = "Adduct";
            this._btnPrimAdduct.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._btnPrimAdduct.ToolTipText = "Adducts";
            this._btnPrimAdduct.Click += new System.EventHandler(this._btnPrimPeak_Click);
            // 
            // _btnPrimPath
            // 
            this._btnPrimPath.AutoSize = false;
            this._btnPrimPath.Font = new System.Drawing.Font("Segoe UI Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnPrimPath.ForeColor = System.Drawing.Color.Black;
            this._btnPrimPath.Image = global::MetaboliteLevels.Properties.Resources.IconPathway;
            this._btnPrimPath.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnPrimPath.Margin = new System.Windows.Forms.Padding(0);
            this._btnPrimPath.Name = "_btnPrimPath";
            this._btnPrimPath.Size = new System.Drawing.Size(44, 44);
            this._btnPrimPath.Text = "Path";
            this._btnPrimPath.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._btnPrimPath.ToolTipText = "Pathways";
            this._btnPrimPath.Click += new System.EventHandler(this._btnPrimPeak_Click);
            // 
            // _btnPrimOther
            // 
            this._btnPrimOther.AutoSize = false;
            this._btnPrimOther.Font = new System.Drawing.Font("Segoe UI Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnPrimOther.ForeColor = System.Drawing.Color.Black;
            this._btnPrimOther.Image = global::MetaboliteLevels.Properties.Resources.IconList;
            this._btnPrimOther.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnPrimOther.Margin = new System.Windows.Forms.Padding(0);
            this._btnPrimOther.Name = "_btnPrimOther";
            this._btnPrimOther.Size = new System.Drawing.Size(44, 44);
            this._btnPrimOther.Text = "Other";
            this._btnPrimOther.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._btnPrimOther.ToolTipText = "Pathways";
            this._btnPrimOther.Click += new System.EventHandler(this._btnPrimOther_Click);
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
            this.panel4.Controls.Add(this._lstSecondary);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 76);
            this.panel4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(457, 277);
            this.panel4.TabIndex = 21;
            // 
            // _lstSecondary
            // 
            this._lstSecondary.AllowColumnReorder = true;
            this._lstSecondary.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lstSecondary.FullRowSelect = true;
            this._lstSecondary.GridLines = true;
            this._lstSecondary.Location = new System.Drawing.Point(0, 0);
            this._lstSecondary.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this._lstSecondary.MultiSelect = false;
            this._lstSecondary.Name = "_lstSecondary";
            this._lstSecondary.Size = new System.Drawing.Size(457, 277);
            this._lstSecondary.TabIndex = 6;
            this._lstSecondary.UseCompatibleStateImageBehavior = false;
            this._lstSecondary.View = System.Windows.Forms.View.Details;
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
            this._btnSubPath,
            this._btnSubOther});
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
            this._btnSubInfo.Click += new System.EventHandler(this._btnSubPeak_Click);
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
            this._btnSubStat.Click += new System.EventHandler(this._btnSubPeak_Click);
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
            this._btnSubPeak.Click += new System.EventHandler(this._btnSubPeak_Click);
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
            this._btnSubPat.Click += new System.EventHandler(this._btnSubPeak_Click);
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
            this._btnSubAss.Click += new System.EventHandler(this._btnSubPeak_Click);
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
            this._btnSubAnnot.Click += new System.EventHandler(this._btnSubPeak_Click);
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
            this._btnSubComp.Click += new System.EventHandler(this._btnSubPeak_Click);
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
            this._btnSubAdd.Click += new System.EventHandler(this._btnSubPeak_Click);
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
            this._btnSubPath.Click += new System.EventHandler(this._btnSubPeak_Click);
            // 
            // _btnSubOther
            // 
            this._btnSubOther.AutoSize = false;
            this._btnSubOther.Font = new System.Drawing.Font("Segoe UI Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnSubOther.ForeColor = System.Drawing.Color.Black;
            this._btnSubOther.Image = global::MetaboliteLevels.Properties.Resources.IconList;
            this._btnSubOther.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnSubOther.Margin = new System.Windows.Forms.Padding(0);
            this._btnSubOther.Name = "_btnSubOther";
            this._btnSubOther.Size = new System.Drawing.Size(44, 44);
            this._btnSubOther.Text = "Other";
            this._btnSubOther.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._btnSubOther.ToolTipText = "Pathways";
            this._btnSubOther.Click += new System.EventHandler(this._btnSubOther_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip2.CanOverflow = false;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._btnBack,
            this._btnPrimarySelection,
            this._btnSwapSelections,
            this._btnSecondarySelection});
            this.toolStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
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
            this._btnBack.Margin = new System.Windows.Forms.Padding(8, 4, 0, 2);
            this._btnBack.Name = "_btnBack";
            this._btnBack.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this._btnBack.Size = new System.Drawing.Size(32, 20);
            this._btnBack.Text = "Back to previous selection";
            this._btnBack.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._btnBack.ToolTipText = "Selection history";
            this._btnBack.ButtonClick += new System.EventHandler(this._btnBack_ButtonClick);
            this._btnBack.DropDownOpening += new System.EventHandler(this._btnBack_DropDownOpening);
            // 
            // _btnPrimarySelection
            // 
            this._btnPrimarySelection.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnPrimarySelection.Image = global::MetaboliteLevels.Properties.Resources.IconCore;
            this._btnPrimarySelection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnPrimarySelection.Name = "_btnPrimarySelection";
            this._btnPrimarySelection.ShowDropDownArrow = false;
            this._btnPrimarySelection.Size = new System.Drawing.Size(89, 29);
            this._btnPrimarySelection.Text = "VALUE";
            this._btnPrimarySelection.ToolTipText = "Primary selection";
            this._btnPrimarySelection.DropDownOpening += new System.EventHandler(this._btnSelection_DropDownOpening);
            // 
            // _btnSwapSelections
            // 
            this._btnSwapSelections.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._btnSwapSelections.Image = ((System.Drawing.Image)(resources.GetObject("_btnSwapSelections.Image")));
            this._btnSwapSelections.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnSwapSelections.Name = "_btnSwapSelections";
            this._btnSwapSelections.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this._btnSwapSelections.Size = new System.Drawing.Size(23, 28);
            this._btnSwapSelections.Text = "toolStripButton11";
            this._btnSwapSelections.ToolTipText = "Swap primary and secondary selections";
            this._btnSwapSelections.Click += new System.EventHandler(this._btnExterior_Click);
            // 
            // _btnSecondarySelection
            // 
            this._btnSecondarySelection.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnSecondarySelection.Image = global::MetaboliteLevels.Properties.Resources.IconCore;
            this._btnSecondarySelection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnSecondarySelection.Name = "_btnSecondarySelection";
            this._btnSecondarySelection.ShowDropDownArrow = false;
            this._btnSecondarySelection.Size = new System.Drawing.Size(89, 29);
            this._btnSecondarySelection.Text = "VALUE";
            this._btnSecondarySelection.ToolTipText = "Secondary selection";
            this._btnSecondarySelection.DropDownOpening += new System.EventHandler(this._btnSelectionExterior_DropDownOpening);
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
            this._tsDatasetsPrimary.ResumeLayout(false);
            this._tsDatasetsPrimary.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
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
        private System.Windows.Forms.ListView _lstPrimary;
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
        private System.Windows.Forms.ToolStrip _tsDatasetsPrimary;
        private System.Windows.Forms.ToolStripButton _btnPrimAssig;
        private System.Windows.Forms.ToolStripButton _btnPrimComp;
        private System.Windows.Forms.ToolStripButton _btnPrimAdduct;
        private System.Windows.Forms.ToolStripButton _btnPrimPath;
        private System.Windows.Forms.ToolStripButton _btnSubInfo;
        private System.Windows.Forms.ToolStripButton _btnSubStat;
        private System.Windows.Forms.ToolStripButton _btnSubPeak;
        private System.Windows.Forms.ToolStripButton _btnSubPat;
        private System.Windows.Forms.ToolStripButton _btnSubComp;
        private System.Windows.Forms.ToolStripButton _btnSubAdd;
        private System.Windows.Forms.ToolStripButton _btnSubPath;
        private System.Windows.Forms.ListView _lstSecondary;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ToolTip _toolTipMain;
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
        private System.Windows.Forms.ToolStripSeparator _tssInsertViews;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripButton toolStripButton7;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripButton toolStripButton8;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem peakFiltersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem observationFiltersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autogenerateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pCAToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem10;
        private System.Windows.Forms.ToolStripButton _btnSubAss;
        private System.Windows.Forms.ToolStripButton _btnPrimPeak;
        private System.Windows.Forms.ToolStripButton _btnPrimClust;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem peakidentificationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton _btnSubAnnot;
        private System.Windows.Forms.ToolStripButton _btnPrimAnnot;
        private System.Windows.Forms.ToolStripMenuItem clusterParameterOptimiserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editNameAndCommentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton _btnSession;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripDropDownButton _btnPrimarySelection;
        private System.Windows.Forms.ToolStripDropDownButton _btnSecondarySelection;
        private System.Windows.Forms.ToolStripSplitButton _btnBack;
        private System.Windows.Forms.ToolStripMenuItem sessionInformationToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton _btnSwapSelections;
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
        private System.Windows.Forms.ToolStripButton _btnPrimOther;
        private System.Windows.Forms.ToolStripButton _btnSubOther;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripLabel _tssInsertViewsEnd;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
    }
}

