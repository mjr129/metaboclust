﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Gui.Controls;

namespace MetaboliteLevels.Gui.Forms.Wizards
{
    partial class FrmEditDataFileNames
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEditDataFileNames));
            this._cmsRecentWorkspaces = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._mnuDebug = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editPathsAndLibrariesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cSVManipulatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exploreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearRPathrequiresRestartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._tipSideBar = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this._tabWelcome = new System.Windows.Forms.TabPage();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._imgPhotograph = new System.Windows.Forms.PictureBox();
            this._lbl32BitWarning = new System.Windows.Forms.Label();
            this._lblProgramDescription = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this._radRecentWorkspace = new System.Windows.Forms.RadioButton();
            this._radEmptyWorkspace = new System.Windows.Forms.RadioButton();
            this._txtPreviousConfig = new System.Windows.Forms.TextBox();
            this._tabSessionName = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this._txtTitle = new MGui.Controls.CtlTextBox();
            this._tabSelectData = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this._txtCondInfo = new MGui.Controls.CtlTextBox();
            this._lstLcmsMode = new System.Windows.Forms.ComboBox();
            this._chkCondInfo = new System.Windows.Forms.CheckBox();
            this._txtDataSetData = new MGui.Controls.CtlTextBox();
            this._txtDataSetObs = new MGui.Controls.CtlTextBox();
            this._txtDataSetVar = new MGui.Controls.CtlTextBox();
            this._chkAltVals = new System.Windows.Forms.CheckBox();
            this._txtAltVals = new MGui.Controls.CtlTextBox();
            this._tabConditions = new System.Windows.Forms.TabPage();
            this._pnlConditions = new System.Windows.Forms.TableLayoutPanel();
            this._chkConditions = new System.Windows.Forms.CheckBox();
            this._txtControls = new MGui.Controls.CtlTextBox();
            this._txtExps = new MGui.Controls.CtlTextBox();
            this._tabStatistics = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._chkAutoTTest = new System.Windows.Forms.CheckBox();
            this._lblTTUnavail = new System.Windows.Forms.Label();
            this._chkAutoPearson = new System.Windows.Forms.CheckBox();
            this._lblPearsonUnavail = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this._chkAutoMeanTrend = new System.Windows.Forms.CheckBox();
            this._chkAutoMedianTrend = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this._chkAutoUvSC = new System.Windows.Forms.CheckBox();
            this._tabCompounds = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this._lstAdducts = new System.Windows.Forms.ListBox();
            this._lstAvailableAdducts = new System.Windows.Forms.ListBox();
            this._lstCompounds = new System.Windows.Forms.ListBox();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this._lstAvailCompounds = new System.Windows.Forms.ListBox();
            this._tabAnnotations = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this._chkAutoIdentify = new System.Windows.Forms.CheckBox();
            this._chkPeakPeakMatch = new System.Windows.Forms.CheckBox();
            this._chkIdentifications = new System.Windows.Forms.CheckBox();
            this._lstTolerance = new System.Windows.Forms.ComboBox();
            this._lblPeakPeakMatchUnavail = new System.Windows.Forms.Label();
            this._numTolerance = new System.Windows.Forms.NumericUpDown();
            this._txtIdentifications = new MGui.Controls.CtlTextBox();
            this._manualFlag = new System.Windows.Forms.ComboBox();
            this._lblMzMatchUnavail = new System.Windows.Forms.Label();
            this._automaticFlag = new System.Windows.Forms.ComboBox();
            this._tabReady = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this._chkAlarm = new System.Windows.Forms.CheckBox();
            this._txtHelp = new MGui.Controls.CtlTextBox();
            this._cmsRecentSessions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._tipPopup = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new MGui.Controls.CtlSplitter();
            this._btnShowFf = new System.Windows.Forms.Button();
            this._checker = new MGui.Controls.CtlError(this.components);
            this.label15 = new CtlLabel();
            this._btnMostRecent = new CtlButton();
            this._btnNewSession = new CtlButton();
            this._btnReturnToSession = new CtlButton();
            this._btnReconfigure = new CtlButton();
            this.ctlLabel6 = new CtlLabel();
            this.ctlLabel7 = new CtlLabel();
            this._btnDeleteWorkspace = new CtlButton();
            this._btnRecentWorkspace = new CtlButton();
            this._lblTitle = new CtlLabel();
            this.label2 = new CtlLabel();
            this._btnCondInfo = new CtlButton();
            this._lblLcmsMode = new CtlLabel();
            this._btnDataSetVar = new CtlButton();
            this._btnDataSetObs = new CtlButton();
            this._btnDataSetData = new CtlButton();
            this._lblDataSetData = new CtlLabel();
            this._lblDataSetObs = new CtlLabel();
            this._lblDataSetVar = new CtlLabel();
            this._btnAltVals = new CtlButton();
            this.label7 = new CtlLabel();
            this.label10 = new CtlLabel();
            this.label5 = new CtlLabel();
            this._lblConditions = new CtlLabel();
            this._btnBrowseContCond = new CtlButton();
            this._btnBrowseExpCond = new CtlButton();
            this.label3 = new CtlLabel();
            this.label14 = new CtlLabel();
            this.label4 = new CtlLabel();
            this.ctlLabel2 = new CtlLabel();
            this.ctlLabel4 = new CtlLabel();
            this._btnAddAdduct = new CtlButton();
            this._btnBrowseAdducts = new CtlButton();
            this._btnDelAdduct = new CtlButton();
            this.label9 = new CtlLabel();
            this._btnAddCompound = new CtlButton();
            this._btnAddAllCompounds = new CtlButton();
            this._btnAddCompoundLibrary = new CtlButton();
            this._btnRemoveLibrary = new CtlButton();
            this._lblAdducts = new CtlLabel();
            this.label6 = new CtlLabel();
            this._btnIdentifications = new CtlButton();
            this.label11 = new CtlLabel();
            this._lblTolerance = new CtlLabel();
            this.ctlLabel3 = new CtlLabel();
            this.ctlLabel5 = new CtlLabel();
            this.ctlLabel1 = new CtlLabel();
            this.label12 = new CtlLabel();
            this._lblOrder = new CtlLabel();
            this.ctlTitleBar1 = new CtlTitleBar();
            this._mnuDebug.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this._tabWelcome.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._imgPhotograph)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this._tabSessionName.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this._tabSelectData.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this._tabConditions.SuspendLayout();
            this._pnlConditions.SuspendLayout();
            this._tabStatistics.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel5.SuspendLayout();
            this._tabCompounds.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this._tabAnnotations.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._numTolerance)).BeginInit();
            this._tabReady.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _cmsRecentWorkspaces
            // 
            this._cmsRecentWorkspaces.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._cmsRecentWorkspaces.Name = "contextMenuStrip1";
            this._cmsRecentWorkspaces.Size = new System.Drawing.Size(61, 4);
            this._cmsRecentWorkspaces.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // _mnuDebug
            // 
            this._mnuDebug.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editPathsAndLibrariesToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exploreToolStripMenuItem,
            this.clearRPathrequiresRestartToolStripMenuItem,
            this.restartToolStripMenuItem});
            this._mnuDebug.Name = "_mnuDebug";
            this._mnuDebug.Size = new System.Drawing.Size(209, 120);
            // 
            // editPathsAndLibrariesToolStripMenuItem
            // 
            this.editPathsAndLibrariesToolStripMenuItem.Image = global::MetaboliteLevels.Properties.Resources.MnuSessionInfo;
            this.editPathsAndLibrariesToolStripMenuItem.Name = "editPathsAndLibrariesToolStripMenuItem";
            this.editPathsAndLibrariesToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.editPathsAndLibrariesToolStripMenuItem.Text = "&Edit paths and libraries...";
            this.editPathsAndLibrariesToolStripMenuItem.Click += new System.EventHandler(this.editPathsAndLibrariesToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cSVManipulatorToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // cSVManipulatorToolStripMenuItem
            // 
            this.cSVManipulatorToolStripMenuItem.Name = "cSVManipulatorToolStripMenuItem";
            this.cSVManipulatorToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.cSVManipulatorToolStripMenuItem.Text = "&Data convertor";
            this.cSVManipulatorToolStripMenuItem.Click += new System.EventHandler(this.cSVManipulatorToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(205, 6);
            // 
            // exploreToolStripMenuItem
            // 
            this.exploreToolStripMenuItem.Name = "exploreToolStripMenuItem";
            this.exploreToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.exploreToolStripMenuItem.Text = "&Explore application folder";
            this.exploreToolStripMenuItem.Click += new System.EventHandler(this.exploreToolStripMenuItem_Click);
            // 
            // clearRPathrequiresRestartToolStripMenuItem
            // 
            this.clearRPathrequiresRestartToolStripMenuItem.Name = "clearRPathrequiresRestartToolStripMenuItem";
            this.clearRPathrequiresRestartToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.clearRPathrequiresRestartToolStripMenuItem.Text = "&Restore default settings";
            this.clearRPathrequiresRestartToolStripMenuItem.Click += new System.EventHandler(this.clearRPathrequiresRestartToolStripMenuItem_Click);
            // 
            // restartToolStripMenuItem
            // 
            this.restartToolStripMenuItem.Name = "restartToolStripMenuItem";
            this.restartToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.restartToolStripMenuItem.Text = "&Restart application";
            this.restartToolStripMenuItem.Click += new System.EventHandler(this.restartToolStripMenuItem_Click);
            // 
            // _tipSideBar
            // 
            this._tipSideBar.Active = false;
            this._tipSideBar.AutomaticDelay = 1;
            this._tipSideBar.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTip1_Popup);
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Right;
            this.tabControl1.Controls.Add(this._tabWelcome);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this._tabSessionName);
            this.tabControl1.Controls.Add(this._tabSelectData);
            this.tabControl1.Controls.Add(this._tabConditions);
            this.tabControl1.Controls.Add(this._tabStatistics);
            this.tabControl1.Controls.Add(this._tabCompounds);
            this.tabControl1.Controls.Add(this._tabAnnotations);
            this.tabControl1.Controls.Add(this._tabReady);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(669, 698);
            this.tabControl1.TabIndex = 0;
            this._tipSideBar.SetToolTip(this.tabControl1, "*");
            // 
            // _tabWelcome
            // 
            this._tabWelcome.Controls.Add(this.label15);
            this._tabWelcome.Controls.Add(this.linkLabel1);
            this._tabWelcome.Controls.Add(this.pictureBox1);
            this._tabWelcome.Controls.Add(this.tableLayoutPanel1);
            this._tabWelcome.Controls.Add(this._lblProgramDescription);
            this._tabWelcome.Location = new System.Drawing.Point(4, 4);
            this._tabWelcome.Margin = new System.Windows.Forms.Padding(0);
            this._tabWelcome.Name = "_tabWelcome";
            this._tabWelcome.Size = new System.Drawing.Size(605, 690);
            this._tabWelcome.TabIndex = 5;
            this._tabWelcome.Text = "Welcome";
            this._tabWelcome.UseVisualStyleBackColor = true;
            // 
            // linkLabel1
            // 
            this.linkLabel1.ActiveLinkColor = System.Drawing.Color.Gray;
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.BackColor = System.Drawing.Color.White;
            this.linkLabel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.linkLabel1.ForeColor = System.Drawing.Color.DimGray;
            this.linkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabel1.LinkColor = System.Drawing.Color.DimGray;
            this.linkLabel1.Location = new System.Drawing.Point(0, 666);
            this.linkLabel1.Margin = new System.Windows.Forms.Padding(0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.linkLabel1.Size = new System.Drawing.Size(130, 24);
            this.linkLabel1.TabIndex = 4;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "#############";
            this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this._tipPopup.SetToolTip(this.linkLabel1, "Show about dialogue");
            this.linkLabel1.VisitedLinkColor = System.Drawing.Color.DimGray;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3421, 491);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(167, 210);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 20;
            this.pictureBox1.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this._imgPhotograph, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this._lbl32BitWarning, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this._btnMostRecent, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this._btnNewSession, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._btnReturnToSession, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this._btnReconfigure, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 38);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(605, 652);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // _imgPhotograph
            // 
            this._imgPhotograph.Image = global::MetaboliteLevels.Properties.Resources.StartLogo2;
            this._imgPhotograph.Location = new System.Drawing.Point(395, 0);
            this._imgPhotograph.Margin = new System.Windows.Forms.Padding(8, 0, 8, 8);
            this._imgPhotograph.Name = "_imgPhotograph";
            this.tableLayoutPanel1.SetRowSpan(this._imgPhotograph, 3);
            this._imgPhotograph.Size = new System.Drawing.Size(202, 239);
            this._imgPhotograph.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this._imgPhotograph.TabIndex = 3;
            this._imgPhotograph.TabStop = false;
            this._tipPopup.SetToolTip(this._imgPhotograph, "A photograph of Medicago truncatula A17 showing the shoot with leaves and seed po" +
        "ds.\r\nNinjatacoshell 2009. Modified.\r\n");
            // 
            // _lbl32BitWarning
            // 
            this._lbl32BitWarning.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._lbl32BitWarning.AutoSize = true;
            this._lbl32BitWarning.BackColor = System.Drawing.Color.Red;
            this._lbl32BitWarning.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._lbl32BitWarning.ForeColor = System.Drawing.Color.White;
            this._lbl32BitWarning.Location = new System.Drawing.Point(10, 432);
            this._lbl32BitWarning.Name = "_lbl32BitWarning";
            this._lbl32BitWarning.Padding = new System.Windows.Forms.Padding(8);
            this._lbl32BitWarning.Size = new System.Drawing.Size(367, 102);
            this._lbl32BitWarning.TabIndex = 20;
            this._lbl32BitWarning.Text = "This application is running in 32 bit mode.\r\n\r\nFor very large datasets or cluster" +
    " optimisation 64 bit mode is recommended.";
            this._lbl32BitWarning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _lblProgramDescription
            // 
            this._lblProgramDescription.Dock = System.Windows.Forms.DockStyle.Top;
            this._lblProgramDescription.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblProgramDescription.Location = new System.Drawing.Point(0, 0);
            this._lblProgramDescription.Margin = new System.Windows.Forms.Padding(0);
            this._lblProgramDescription.Name = "_lblProgramDescription";
            this._lblProgramDescription.Padding = new System.Windows.Forms.Padding(41, 0, 0, 0);
            this._lblProgramDescription.Size = new System.Drawing.Size(605, 38);
            this._lblProgramDescription.TabIndex = 2;
            this._lblProgramDescription.Text = "Description goes here";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel8);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(605, 690);
            this.tabPage1.TabIndex = 9;
            this.tabPage1.Text = "Template";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.AutoSize = true;
            this.tableLayoutPanel8.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel8.ColumnCount = 3;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel8.Controls.Add(this._radRecentWorkspace, 0, 3);
            this.tableLayoutPanel8.Controls.Add(this.ctlLabel6, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.ctlLabel7, 0, 1);
            this.tableLayoutPanel8.Controls.Add(this._radEmptyWorkspace, 0, 2);
            this.tableLayoutPanel8.Controls.Add(this._btnDeleteWorkspace, 2, 4);
            this.tableLayoutPanel8.Controls.Add(this._btnRecentWorkspace, 1, 4);
            this.tableLayoutPanel8.Controls.Add(this._txtPreviousConfig, 0, 4);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel8.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.Padding = new System.Windows.Forms.Padding(16);
            this.tableLayoutPanel8.RowCount = 6;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(605, 690);
            this.tableLayoutPanel8.TabIndex = 1;
            this._tipSideBar.SetToolTip(this.tableLayoutPanel8, "*");
            // 
            // _radRecentWorkspace
            // 
            this._radRecentWorkspace.AutoCheck = false;
            this._radRecentWorkspace.AutoSize = true;
            this.tableLayoutPanel8.SetColumnSpan(this._radRecentWorkspace, 3);
            this._radRecentWorkspace.Location = new System.Drawing.Point(40, 135);
            this._radRecentWorkspace.Margin = new System.Windows.Forms.Padding(24, 8, 8, 8);
            this._radRecentWorkspace.Name = "_radRecentWorkspace";
            this._radRecentWorkspace.Size = new System.Drawing.Size(185, 25);
            this._radRecentWorkspace.TabIndex = 3;
            this._radRecentWorkspace.TabStop = true;
            this._radRecentWorkspace.Text = "Previous configuration";
            this._tipSideBar.SetToolTip(this._radRecentWorkspace, global::MetaboliteLevels.Resx.Manual.RecentSessions);
            this._radRecentWorkspace.UseVisualStyleBackColor = true;
            this._radRecentWorkspace.CheckedChanged += new System.EventHandler(this._radRecentWorkspace_CheckedChanged);
            this._radRecentWorkspace.Click += new System.EventHandler(this.radioButton2_Click);
            // 
            // _radEmptyWorkspace
            // 
            this._radEmptyWorkspace.AutoCheck = false;
            this._radEmptyWorkspace.AutoSize = true;
            this.tableLayoutPanel8.SetColumnSpan(this._radEmptyWorkspace, 3);
            this._radEmptyWorkspace.Location = new System.Drawing.Point(40, 94);
            this._radEmptyWorkspace.Margin = new System.Windows.Forms.Padding(24, 8, 8, 8);
            this._radEmptyWorkspace.Name = "_radEmptyWorkspace";
            this._radEmptyWorkspace.Size = new System.Drawing.Size(131, 25);
            this._radEmptyWorkspace.TabIndex = 2;
            this._radEmptyWorkspace.TabStop = true;
            this._radEmptyWorkspace.Text = "Blank template";
            this._tipSideBar.SetToolTip(this._radEmptyWorkspace, global::MetaboliteLevels.Resx.Manual.RecentSessions);
            this._radEmptyWorkspace.UseVisualStyleBackColor = true;
            this._radEmptyWorkspace.Click += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // _txtPreviousConfig
            // 
            this._txtPreviousConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtPreviousConfig.Location = new System.Drawing.Point(48, 176);
            this._txtPreviousConfig.Margin = new System.Windows.Forms.Padding(32, 8, 8, 8);
            this._txtPreviousConfig.Name = "_txtPreviousConfig";
            this._txtPreviousConfig.ReadOnly = true;
            this._txtPreviousConfig.Size = new System.Drawing.Size(444, 29);
            this._txtPreviousConfig.TabIndex = 4;
            this._tipSideBar.SetToolTip(this._txtPreviousConfig, global::MetaboliteLevels.Resx.Manual.RecentSessions);
            // 
            // _tabSessionName
            // 
            this._tabSessionName.Controls.Add(this.tableLayoutPanel6);
            this._tabSessionName.Location = new System.Drawing.Point(4, 4);
            this._tabSessionName.Margin = new System.Windows.Forms.Padding(4);
            this._tabSessionName.Name = "_tabSessionName";
            this._tabSessionName.Padding = new System.Windows.Forms.Padding(4);
            this._tabSessionName.Size = new System.Drawing.Size(605, 690);
            this._tabSessionName.TabIndex = 4;
            this._tabSessionName.Text = "Session name";
            this._tabSessionName.ToolTipText = "*";
            this._tabSessionName.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.AutoSize = true;
            this.tableLayoutPanel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this._lblTitle, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this._txtTitle, 0, 2);
            this.tableLayoutPanel6.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.Padding = new System.Windows.Forms.Padding(16);
            this.tableLayoutPanel6.RowCount = 3;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(597, 682);
            this.tableLayoutPanel6.TabIndex = 0;
            this._tipSideBar.SetToolTip(this.tableLayoutPanel6, "*");
            // 
            // _txtTitle
            // 
            this._txtTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtTitle.ForeColor = System.Drawing.Color.Blue;
            this._txtTitle.Location = new System.Drawing.Point(40, 95);
            this._txtTitle.Margin = new System.Windows.Forms.Padding(24, 7, 4, 7);
            this._txtTitle.Name = "_txtTitle";
            this._txtTitle.Size = new System.Drawing.Size(537, 29);
            this._txtTitle.TabIndex = 2;
            this._tipSideBar.SetToolTip(this._txtTitle, global::MetaboliteLevels.Resx.Manual.Session);
            this._txtTitle.Watermark = null;
            // 
            // _tabSelectData
            // 
            this._tabSelectData.Controls.Add(this.tableLayoutPanel2);
            this._tabSelectData.Location = new System.Drawing.Point(4, 4);
            this._tabSelectData.Margin = new System.Windows.Forms.Padding(4);
            this._tabSelectData.Name = "_tabSelectData";
            this._tabSelectData.Padding = new System.Windows.Forms.Padding(4);
            this._tabSelectData.Size = new System.Drawing.Size(605, 690);
            this._tabSelectData.TabIndex = 0;
            this._tabSelectData.Text = "Select data";
            this._tabSelectData.ToolTipText = "*";
            this._tabSelectData.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this._btnCondInfo, 1, 13);
            this.tableLayoutPanel2.Controls.Add(this._txtCondInfo, 0, 13);
            this.tableLayoutPanel2.Controls.Add(this._lstLcmsMode, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this._chkCondInfo, 0, 12);
            this.tableLayoutPanel2.Controls.Add(this._lblLcmsMode, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this._btnDataSetVar, 1, 9);
            this.tableLayoutPanel2.Controls.Add(this._btnDataSetObs, 1, 7);
            this.tableLayoutPanel2.Controls.Add(this._btnDataSetData, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this._txtDataSetData, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this._txtDataSetObs, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this._lblDataSetData, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this._lblDataSetObs, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this._lblDataSetVar, 0, 8);
            this.tableLayoutPanel2.Controls.Add(this._txtDataSetVar, 0, 9);
            this.tableLayoutPanel2.Controls.Add(this._chkAltVals, 0, 10);
            this.tableLayoutPanel2.Controls.Add(this._txtAltVals, 0, 11);
            this.tableLayoutPanel2.Controls.Add(this._btnAltVals, 1, 11);
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label10, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(16);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(16);
            this.tableLayoutPanel2.RowCount = 14;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(597, 682);
            this.tableLayoutPanel2.TabIndex = 0;
            this._tipSideBar.SetToolTip(this.tableLayoutPanel2, "*");
            // 
            // _txtCondInfo
            // 
            this._txtCondInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtCondInfo.Enabled = false;
            this._txtCondInfo.ForeColor = System.Drawing.Color.Blue;
            this._txtCondInfo.Location = new System.Drawing.Point(40, 497);
            this._txtCondInfo.Margin = new System.Windows.Forms.Padding(24, 7, 4, 7);
            this._txtCondInfo.Name = "_txtCondInfo";
            this._txtCondInfo.Size = new System.Drawing.Size(501, 29);
            this._txtCondInfo.TabIndex = 17;
            this._tipSideBar.SetToolTip(this._txtCondInfo, global::MetaboliteLevels.Resx.Manual.ConditionNames);
            this._txtCondInfo.Watermark = null;
            // 
            // _lstLcmsMode
            // 
            this._lstLcmsMode.BackColor = System.Drawing.SystemColors.Window;
            this._lstLcmsMode.Dock = System.Windows.Forms.DockStyle.Top;
            this._lstLcmsMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstLcmsMode.ForeColor = System.Drawing.Color.Blue;
            this._lstLcmsMode.FormattingEnabled = true;
            this._lstLcmsMode.Items.AddRange(new object[] {
            "Negative (-)",
            "Mixed (+/-)",
            "Positive (+)",
            "None"});
            this._lstLcmsMode.Location = new System.Drawing.Point(64, 137);
            this._lstLcmsMode.Margin = new System.Windows.Forms.Padding(48, 7, 4, 7);
            this._lstLcmsMode.Name = "_lstLcmsMode";
            this._lstLcmsMode.Size = new System.Drawing.Size(477, 29);
            this._lstLcmsMode.TabIndex = 3;
            this._tipSideBar.SetToolTip(this._lstLcmsMode, global::MetaboliteLevels.Resx.Manual.LcMsMode);
            this._lstLcmsMode.SelectedIndexChanged += new System.EventHandler(this._lstLcmsMode_SelectedIndexChanged);
            // 
            // _chkCondInfo
            // 
            this._chkCondInfo.AutoSize = true;
            this._chkCondInfo.Location = new System.Drawing.Point(20, 458);
            this._chkCondInfo.Margin = new System.Windows.Forms.Padding(4, 9, 4, 7);
            this._chkCondInfo.Name = "_chkCondInfo";
            this._chkCondInfo.Size = new System.Drawing.Size(201, 25);
            this._chkCondInfo.TabIndex = 16;
            this._chkCondInfo.Text = "Provide condition names";
            this._tipSideBar.SetToolTip(this._chkCondInfo, global::MetaboliteLevels.Resx.Manual.ConditionNames);
            this._chkCondInfo.UseVisualStyleBackColor = true;
            this._chkCondInfo.CheckedChanged += new System.EventHandler(this._chkCondInfo_CheckedChanged);
            // 
            // _txtDataSetData
            // 
            this._txtDataSetData.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtDataSetData.ForeColor = System.Drawing.Color.Blue;
            this._txtDataSetData.Location = new System.Drawing.Point(64, 201);
            this._txtDataSetData.Margin = new System.Windows.Forms.Padding(48, 7, 4, 7);
            this._txtDataSetData.Name = "_txtDataSetData";
            this._txtDataSetData.Size = new System.Drawing.Size(477, 29);
            this._txtDataSetData.TabIndex = 5;
            this._tipSideBar.SetToolTip(this._txtDataSetData, global::MetaboliteLevels.Resx.Manual.Intensities);
            this._txtDataSetData.Watermark = null;
            this._txtDataSetData.TextChanged += new System.EventHandler(this._txtDataSetData_TextChanged);
            // 
            // _txtDataSetObs
            // 
            this._txtDataSetObs.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtDataSetObs.ForeColor = System.Drawing.Color.Blue;
            this._txtDataSetObs.Location = new System.Drawing.Point(64, 265);
            this._txtDataSetObs.Margin = new System.Windows.Forms.Padding(48, 7, 4, 7);
            this._txtDataSetObs.Name = "_txtDataSetObs";
            this._txtDataSetObs.Size = new System.Drawing.Size(477, 29);
            this._txtDataSetObs.TabIndex = 8;
            this._tipSideBar.SetToolTip(this._txtDataSetObs, global::MetaboliteLevels.Resx.Manual.Observations);
            this._txtDataSetObs.Watermark = null;
            this._txtDataSetObs.TextChanged += new System.EventHandler(this._txtDataSetObs_TextChanged);
            // 
            // _txtDataSetVar
            // 
            this._txtDataSetVar.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtDataSetVar.ForeColor = System.Drawing.Color.Blue;
            this._txtDataSetVar.Location = new System.Drawing.Point(64, 329);
            this._txtDataSetVar.Margin = new System.Windows.Forms.Padding(48, 7, 4, 7);
            this._txtDataSetVar.Name = "_txtDataSetVar";
            this._txtDataSetVar.Size = new System.Drawing.Size(477, 29);
            this._txtDataSetVar.TabIndex = 11;
            this._tipSideBar.SetToolTip(this._txtDataSetVar, global::MetaboliteLevels.Resx.Manual.Variables);
            this._txtDataSetVar.Watermark = null;
            // 
            // _chkAltVals
            // 
            this._chkAltVals.AutoSize = true;
            this._chkAltVals.Location = new System.Drawing.Point(20, 374);
            this._chkAltVals.Margin = new System.Windows.Forms.Padding(4, 9, 4, 7);
            this._chkAltVals.Name = "_chkAltVals";
            this._chkAltVals.Size = new System.Drawing.Size(230, 25);
            this._chkAltVals.TabIndex = 13;
            this._chkAltVals.Text = "Include alternative intensities";
            this._tipSideBar.SetToolTip(this._chkAltVals, global::MetaboliteLevels.Resx.Manual.AlternativeValues);
            this._chkAltVals.UseVisualStyleBackColor = true;
            this._chkAltVals.CheckedChanged += new System.EventHandler(this._chkAltVals_CheckedChanged);
            // 
            // _txtAltVals
            // 
            this._txtAltVals.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtAltVals.Enabled = false;
            this._txtAltVals.ForeColor = System.Drawing.Color.Blue;
            this._txtAltVals.Location = new System.Drawing.Point(40, 413);
            this._txtAltVals.Margin = new System.Windows.Forms.Padding(24, 7, 4, 7);
            this._txtAltVals.Name = "_txtAltVals";
            this._txtAltVals.Size = new System.Drawing.Size(501, 29);
            this._txtAltVals.TabIndex = 14;
            this._tipSideBar.SetToolTip(this._txtAltVals, global::MetaboliteLevels.Resx.Manual.AlternativeValues);
            this._txtAltVals.Watermark = null;
            // 
            // _tabConditions
            // 
            this._tabConditions.Controls.Add(this._pnlConditions);
            this._tabConditions.Location = new System.Drawing.Point(4, 4);
            this._tabConditions.Name = "_tabConditions";
            this._tabConditions.Padding = new System.Windows.Forms.Padding(3);
            this._tabConditions.Size = new System.Drawing.Size(605, 690);
            this._tabConditions.TabIndex = 10;
            this._tabConditions.Text = "Conditions";
            this._tabConditions.UseVisualStyleBackColor = true;
            // 
            // _pnlConditions
            // 
            this._pnlConditions.ColumnCount = 2;
            this._pnlConditions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._pnlConditions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this._pnlConditions.Controls.Add(this._chkConditions, 0, 1);
            this._pnlConditions.Controls.Add(this.label5, 0, 0);
            this._pnlConditions.Controls.Add(this._lblConditions, 0, 2);
            this._pnlConditions.Controls.Add(this._btnBrowseContCond, 1, 5);
            this._pnlConditions.Controls.Add(this._txtControls, 0, 5);
            this._pnlConditions.Controls.Add(this._btnBrowseExpCond, 1, 3);
            this._pnlConditions.Controls.Add(this._txtExps, 0, 3);
            this._pnlConditions.Controls.Add(this.label3, 0, 4);
            this._pnlConditions.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pnlConditions.Location = new System.Drawing.Point(3, 3);
            this._pnlConditions.Margin = new System.Windows.Forms.Padding(4);
            this._pnlConditions.Name = "_pnlConditions";
            this._pnlConditions.Padding = new System.Windows.Forms.Padding(16);
            this._pnlConditions.RowCount = 6;
            this._pnlConditions.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._pnlConditions.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._pnlConditions.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._pnlConditions.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._pnlConditions.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._pnlConditions.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._pnlConditions.Size = new System.Drawing.Size(599, 684);
            this._pnlConditions.TabIndex = 31;
            // 
            // _chkConditions
            // 
            this._chkConditions.AutoSize = true;
            this._chkConditions.Location = new System.Drawing.Point(20, 62);
            this._chkConditions.Margin = new System.Windows.Forms.Padding(4, 9, 4, 7);
            this._chkConditions.Name = "_chkConditions";
            this._chkConditions.Size = new System.Drawing.Size(155, 25);
            this._chkConditions.TabIndex = 1;
            this._chkConditions.Text = "Specify conditions";
            this._tipSideBar.SetToolTip(this._chkConditions, global::MetaboliteLevels.Resx.Manual.AlternativeValues);
            this._chkConditions.UseVisualStyleBackColor = true;
            this._chkConditions.CheckedChanged += new System.EventHandler(this._chkConditions_CheckedChanged);
            // 
            // _txtControls
            // 
            this._txtControls.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtControls.ForeColor = System.Drawing.Color.Blue;
            this._txtControls.Location = new System.Drawing.Point(64, 186);
            this._txtControls.Margin = new System.Windows.Forms.Padding(48, 7, 4, 7);
            this._txtControls.Name = "_txtControls";
            this._txtControls.Size = new System.Drawing.Size(479, 29);
            this._txtControls.TabIndex = 6;
            this._tipSideBar.SetToolTip(this._txtControls, global::MetaboliteLevels.Resx.Manual.ControlConditions);
            this._txtControls.Watermark = null;
            // 
            // _txtExps
            // 
            this._txtExps.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtExps.ForeColor = System.Drawing.Color.Blue;
            this._txtExps.Location = new System.Drawing.Point(64, 122);
            this._txtExps.Margin = new System.Windows.Forms.Padding(48, 7, 4, 7);
            this._txtExps.Name = "_txtExps";
            this._txtExps.Size = new System.Drawing.Size(479, 29);
            this._txtExps.TabIndex = 3;
            this._tipSideBar.SetToolTip(this._txtExps, global::MetaboliteLevels.Resx.Manual.ExperimentalConditions);
            this._txtExps.Watermark = null;
            // 
            // _tabStatistics
            // 
            this._tabStatistics.Controls.Add(this.tableLayoutPanel5);
            this._tabStatistics.Location = new System.Drawing.Point(4, 4);
            this._tabStatistics.Name = "_tabStatistics";
            this._tabStatistics.Size = new System.Drawing.Size(605, 690);
            this._tabStatistics.TabIndex = 6;
            this._tabStatistics.Text = "Statistics";
            this._tabStatistics.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.Controls.Add(this.label14, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.flowLayoutPanel1, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.ctlLabel2, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.flowLayoutPanel2, 0, 4);
            this.tableLayoutPanel5.Controls.Add(this.ctlLabel4, 0, 5);
            this.tableLayoutPanel5.Controls.Add(this.flowLayoutPanel5, 0, 6);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.Padding = new System.Windows.Forms.Padding(16);
            this.tableLayoutPanel5.RowCount = 7;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.Size = new System.Drawing.Size(605, 690);
            this.tableLayoutPanel5.TabIndex = 0;
            this.tableLayoutPanel5.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel5_Paint);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this._chkAutoTTest);
            this.flowLayoutPanel1.Controls.Add(this._lblTTUnavail);
            this.flowLayoutPanel1.Controls.Add(this._chkAutoPearson);
            this.flowLayoutPanel1.Controls.Add(this._lblPearsonUnavail);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(16, 88);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(361, 104);
            this.flowLayoutPanel1.TabIndex = 30;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // _chkAutoTTest
            // 
            this._chkAutoTTest.AutoSize = true;
            this._chkAutoTTest.Checked = true;
            this._chkAutoTTest.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkAutoTTest.Location = new System.Drawing.Point(24, 7);
            this._chkAutoTTest.Margin = new System.Windows.Forms.Padding(24, 7, 4, 7);
            this._chkAutoTTest.Name = "_chkAutoTTest";
            this._chkAutoTTest.Size = new System.Drawing.Size(72, 25);
            this._chkAutoTTest.TabIndex = 0;
            this._chkAutoTTest.Text = "t-tests";
            this._tipSideBar.SetToolTip(this._chkAutoTTest, "Select this option to create t-tests for each of the experimental groups against " +
        "the control group (p[CX]), as well as a statistic representing the lowest of the" +
        "se tests (p[min]).");
            this._chkAutoTTest.UseVisualStyleBackColor = true;
            this._chkAutoTTest.CheckedChanged += new System.EventHandler(this._chkStatT_CheckedChanged);
            // 
            // _lblTTUnavail
            // 
            this._lblTTUnavail.AutoSize = true;
            this._lblTTUnavail.Font = new System.Drawing.Font("Segoe UI Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblTTUnavail.Location = new System.Drawing.Point(32, 39);
            this._lblTTUnavail.Margin = new System.Windows.Forms.Padding(32, 0, 0, 0);
            this._lblTTUnavail.Name = "_lblTTUnavail";
            this._lblTTUnavail.Size = new System.Drawing.Size(329, 13);
            this._lblTTUnavail.TabIndex = 1;
            this._lblTTUnavail.Text = "Not available - requires control conditions and experimental conditions";
            // 
            // _chkAutoPearson
            // 
            this._chkAutoPearson.AutoSize = true;
            this._chkAutoPearson.Checked = true;
            this._chkAutoPearson.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkAutoPearson.Location = new System.Drawing.Point(24, 59);
            this._chkAutoPearson.Margin = new System.Windows.Forms.Padding(24, 7, 4, 7);
            this._chkAutoPearson.Name = "_chkAutoPearson";
            this._chkAutoPearson.Size = new System.Drawing.Size(184, 25);
            this._chkAutoPearson.TabIndex = 2;
            this._chkAutoPearson.Text = "Correlations (Pearson)";
            this._tipSideBar.SetToolTip(this._chkAutoPearson, "Select this option to create Pearson correlation  tests for each of the groups ag" +
        "ainst time (r[X]) as well as a statistic representing the highest of the absolut" +
        "e values (r[max]).");
            this._chkAutoPearson.UseVisualStyleBackColor = true;
            // 
            // _lblPearsonUnavail
            // 
            this._lblPearsonUnavail.AutoSize = true;
            this._lblPearsonUnavail.Font = new System.Drawing.Font("Segoe UI Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblPearsonUnavail.Location = new System.Drawing.Point(32, 91);
            this._lblPearsonUnavail.Margin = new System.Windows.Forms.Padding(32, 0, 0, 0);
            this._lblPearsonUnavail.Name = "_lblPearsonUnavail";
            this._lblPearsonUnavail.Size = new System.Drawing.Size(225, 13);
            this._lblPearsonUnavail.TabIndex = 3;
            this._lblPearsonUnavail.Text = "Not available - requires experimental conditions";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel2.Controls.Add(this._chkAutoMeanTrend);
            this.flowLayoutPanel2.Controls.Add(this._chkAutoMedianTrend);
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(16, 229);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(109, 78);
            this.flowLayoutPanel2.TabIndex = 30;
            this.flowLayoutPanel2.WrapContents = false;
            // 
            // _chkAutoMeanTrend
            // 
            this._chkAutoMeanTrend.AutoSize = true;
            this._chkAutoMeanTrend.Checked = true;
            this._chkAutoMeanTrend.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkAutoMeanTrend.Location = new System.Drawing.Point(24, 7);
            this._chkAutoMeanTrend.Margin = new System.Windows.Forms.Padding(24, 7, 4, 7);
            this._chkAutoMeanTrend.Name = "_chkAutoMeanTrend";
            this._chkAutoMeanTrend.Size = new System.Drawing.Size(68, 25);
            this._chkAutoMeanTrend.TabIndex = 0;
            this._chkAutoMeanTrend.Text = "Mean";
            this._chkAutoMeanTrend.UseVisualStyleBackColor = true;
            this._chkAutoMeanTrend.CheckedChanged += new System.EventHandler(this._chkStatT_CheckedChanged);
            // 
            // _chkAutoMedianTrend
            // 
            this._chkAutoMedianTrend.AutoSize = true;
            this._chkAutoMedianTrend.Checked = true;
            this._chkAutoMedianTrend.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkAutoMedianTrend.Location = new System.Drawing.Point(24, 46);
            this._chkAutoMedianTrend.Margin = new System.Windows.Forms.Padding(24, 7, 4, 7);
            this._chkAutoMedianTrend.Name = "_chkAutoMedianTrend";
            this._chkAutoMedianTrend.Size = new System.Drawing.Size(81, 25);
            this._chkAutoMedianTrend.TabIndex = 1;
            this._chkAutoMedianTrend.Text = "Median";
            this._chkAutoMedianTrend.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel5
            // 
            this.flowLayoutPanel5.AutoSize = true;
            this.flowLayoutPanel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel5.Controls.Add(this._chkAutoUvSC);
            this.flowLayoutPanel5.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel5.Location = new System.Drawing.Point(16, 344);
            this.flowLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.flowLayoutPanel5.Size = new System.Drawing.Size(193, 39);
            this.flowLayoutPanel5.TabIndex = 30;
            this.flowLayoutPanel5.WrapContents = false;
            // 
            // _chkAutoUvSC
            // 
            this._chkAutoUvSC.AutoSize = true;
            this._chkAutoUvSC.Checked = true;
            this._chkAutoUvSC.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkAutoUvSC.Location = new System.Drawing.Point(24, 7);
            this._chkAutoUvSC.Margin = new System.Windows.Forms.Padding(24, 7, 4, 7);
            this._chkAutoUvSC.Name = "_chkAutoUvSC";
            this._chkAutoUvSC.Size = new System.Drawing.Size(165, 25);
            this._chkAutoUvSC.TabIndex = 0;
            this._chkAutoUvSC.Text = "UV scale and centre";
            this._chkAutoUvSC.UseVisualStyleBackColor = true;
            this._chkAutoUvSC.CheckedChanged += new System.EventHandler(this._chkStatT_CheckedChanged);
            // 
            // _tabCompounds
            // 
            this._tabCompounds.Controls.Add(this.tableLayoutPanel7);
            this._tabCompounds.Location = new System.Drawing.Point(4, 4);
            this._tabCompounds.Margin = new System.Windows.Forms.Padding(4);
            this._tabCompounds.Name = "_tabCompounds";
            this._tabCompounds.Padding = new System.Windows.Forms.Padding(4);
            this._tabCompounds.Size = new System.Drawing.Size(605, 690);
            this._tabCompounds.TabIndex = 2;
            this._tabCompounds.Text = "Compounds";
            this._tabCompounds.ToolTipText = "*";
            this._tabCompounds.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 3;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Controls.Add(this.flowLayoutPanel4, 1, 4);
            this.tableLayoutPanel7.Controls.Add(this.label9, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this._lstAdducts, 0, 4);
            this.tableLayoutPanel7.Controls.Add(this._lstAvailableAdducts, 2, 4);
            this.tableLayoutPanel7.Controls.Add(this._lstCompounds, 0, 2);
            this.tableLayoutPanel7.Controls.Add(this.flowLayoutPanel3, 1, 2);
            this.tableLayoutPanel7.Controls.Add(this._lstAvailCompounds, 2, 2);
            this.tableLayoutPanel7.Controls.Add(this._lblAdducts, 0, 3);
            this.tableLayoutPanel7.Controls.Add(this.label6, 0, 1);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel7.Margin = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.Padding = new System.Windows.Forms.Padding(16);
            this.tableLayoutPanel7.RowCount = 5;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(597, 682);
            this.tableLayoutPanel7.TabIndex = 19;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.AutoSize = true;
            this.flowLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel4.Controls.Add(this._btnAddAdduct);
            this.flowLayoutPanel4.Controls.Add(this._btnBrowseAdducts);
            this.flowLayoutPanel4.Controls.Add(this._btnDelAdduct);
            this.flowLayoutPanel4.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel4.Location = new System.Drawing.Point(276, 396);
            this.flowLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(44, 111);
            this.flowLayoutPanel4.TabIndex = 6;
            this.flowLayoutPanel4.WrapContents = false;
            // 
            // _lstAdducts
            // 
            this._lstAdducts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._lstAdducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lstAdducts.ForeColor = System.Drawing.Color.Blue;
            this._lstAdducts.FormattingEnabled = true;
            this._lstAdducts.ItemHeight = 21;
            this._lstAdducts.Location = new System.Drawing.Point(24, 396);
            this._lstAdducts.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this._lstAdducts.Name = "_lstAdducts";
            this._lstAdducts.Size = new System.Drawing.Size(252, 270);
            this._lstAdducts.TabIndex = 5;
            this._tipPopup.SetToolTip(this._lstAdducts, "Selected libraries");
            this._tipSideBar.SetToolTip(this._lstAdducts, global::MetaboliteLevels.Resx.Manual.Adducts);
            // 
            // _lstAvailableAdducts
            // 
            this._lstAvailableAdducts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._lstAvailableAdducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lstAvailableAdducts.ForeColor = System.Drawing.Color.Blue;
            this._lstAvailableAdducts.FormattingEnabled = true;
            this._lstAvailableAdducts.ItemHeight = 21;
            this._lstAvailableAdducts.Location = new System.Drawing.Point(320, 396);
            this._lstAvailableAdducts.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this._lstAvailableAdducts.Name = "_lstAvailableAdducts";
            this._lstAvailableAdducts.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this._lstAvailableAdducts.Size = new System.Drawing.Size(253, 270);
            this._lstAvailableAdducts.TabIndex = 6;
            this._tipPopup.SetToolTip(this._lstAvailableAdducts, "Available libraries");
            this._tipSideBar.SetToolTip(this._lstAvailableAdducts, global::MetaboliteLevels.Resx.Manual.Adducts);
            // 
            // _lstCompounds
            // 
            this._lstCompounds.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._lstCompounds.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lstCompounds.ForeColor = System.Drawing.Color.Blue;
            this._lstCompounds.FormattingEnabled = true;
            this._lstCompounds.ItemHeight = 21;
            this._lstCompounds.Location = new System.Drawing.Point(24, 90);
            this._lstCompounds.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this._lstCompounds.Name = "_lstCompounds";
            this._lstCompounds.Size = new System.Drawing.Size(252, 269);
            this._lstCompounds.TabIndex = 2;
            this._tipSideBar.SetToolTip(this._lstCompounds, global::MetaboliteLevels.Resx.Manual.Compounds);
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.AutoSize = true;
            this.flowLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel3.Controls.Add(this._btnAddCompound);
            this.flowLayoutPanel3.Controls.Add(this._btnAddAllCompounds);
            this.flowLayoutPanel3.Controls.Add(this._btnAddCompoundLibrary);
            this.flowLayoutPanel3.Controls.Add(this._btnRemoveLibrary);
            this.flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(276, 90);
            this.flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(44, 148);
            this.flowLayoutPanel3.TabIndex = 6;
            this.flowLayoutPanel3.WrapContents = false;
            // 
            // _lstAvailCompounds
            // 
            this._lstAvailCompounds.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._lstAvailCompounds.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lstAvailCompounds.ForeColor = System.Drawing.Color.Blue;
            this._lstAvailCompounds.FormattingEnabled = true;
            this._lstAvailCompounds.ItemHeight = 21;
            this._lstAvailCompounds.Location = new System.Drawing.Point(320, 90);
            this._lstAvailCompounds.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this._lstAvailCompounds.Name = "_lstAvailCompounds";
            this._lstAvailCompounds.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this._lstAvailCompounds.Size = new System.Drawing.Size(253, 269);
            this._lstAvailCompounds.TabIndex = 3;
            this._tipSideBar.SetToolTip(this._lstAvailCompounds, global::MetaboliteLevels.Resx.Manual.Compounds);
            // 
            // _tabAnnotations
            // 
            this._tabAnnotations.Controls.Add(this.tableLayoutPanel9);
            this._tabAnnotations.Location = new System.Drawing.Point(4, 4);
            this._tabAnnotations.Name = "_tabAnnotations";
            this._tabAnnotations.Padding = new System.Windows.Forms.Padding(3);
            this._tabAnnotations.Size = new System.Drawing.Size(605, 690);
            this._tabAnnotations.TabIndex = 8;
            this._tabAnnotations.Text = "Annotations";
            this._tabAnnotations.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 4;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel9.Controls.Add(this._btnIdentifications, 3, 10);
            this.tableLayoutPanel9.Controls.Add(this.label11, 0, 0);
            this.tableLayoutPanel9.Controls.Add(this._chkAutoIdentify, 0, 2);
            this.tableLayoutPanel9.Controls.Add(this._chkPeakPeakMatch, 0, 5);
            this.tableLayoutPanel9.Controls.Add(this._chkIdentifications, 0, 9);
            this.tableLayoutPanel9.Controls.Add(this._lstTolerance, 2, 8);
            this.tableLayoutPanel9.Controls.Add(this._lblPeakPeakMatchUnavail, 0, 6);
            this.tableLayoutPanel9.Controls.Add(this._lblTolerance, 0, 7);
            this.tableLayoutPanel9.Controls.Add(this._numTolerance, 0, 8);
            this.tableLayoutPanel9.Controls.Add(this._txtIdentifications, 0, 10);
            this.tableLayoutPanel9.Controls.Add(this.ctlLabel3, 0, 11);
            this.tableLayoutPanel9.Controls.Add(this._manualFlag, 1, 11);
            this.tableLayoutPanel9.Controls.Add(this.ctlLabel5, 0, 1);
            this.tableLayoutPanel9.Controls.Add(this._lblMzMatchUnavail, 0, 3);
            this.tableLayoutPanel9.Controls.Add(this.ctlLabel1, 0, 4);
            this.tableLayoutPanel9.Controls.Add(this._automaticFlag, 1, 4);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel9.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.Padding = new System.Windows.Forms.Padding(16);
            this.tableLayoutPanel9.RowCount = 12;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel9.Size = new System.Drawing.Size(599, 684);
            this.tableLayoutPanel9.TabIndex = 0;
            // 
            // _chkAutoIdentify
            // 
            this._chkAutoIdentify.AutoSize = true;
            this.tableLayoutPanel9.SetColumnSpan(this._chkAutoIdentify, 4);
            this._chkAutoIdentify.Location = new System.Drawing.Point(40, 98);
            this._chkAutoIdentify.Margin = new System.Windows.Forms.Padding(24, 8, 8, 8);
            this._chkAutoIdentify.Name = "_chkAutoIdentify";
            this._chkAutoIdentify.Size = new System.Drawing.Size(329, 25);
            this._chkAutoIdentify.TabIndex = 2;
            this._chkAutoIdentify.Text = "Perform m/z based automatic identification";
            this._tipSideBar.SetToolTip(this._chkAutoIdentify, "Select this option to automatically annotate Peaks based on their m/z.\r\n\r\nThis wi" +
        "ll only function if the requisite information has been selected.");
            this._chkAutoIdentify.UseVisualStyleBackColor = true;
            this._chkAutoIdentify.CheckedChanged += new System.EventHandler(this._chkAutoIdentify_CheckedChanged);
            // 
            // _chkPeakPeakMatch
            // 
            this._chkPeakPeakMatch.AutoSize = true;
            this.tableLayoutPanel9.SetColumnSpan(this._chkPeakPeakMatch, 4);
            this._chkPeakPeakMatch.Location = new System.Drawing.Point(40, 197);
            this._chkPeakPeakMatch.Margin = new System.Windows.Forms.Padding(24, 8, 8, 8);
            this._chkPeakPeakMatch.Name = "_chkPeakPeakMatch";
            this._chkPeakPeakMatch.Size = new System.Drawing.Size(231, 25);
            this._chkPeakPeakMatch.TabIndex = 6;
            this._chkPeakPeakMatch.Text = "Perform peak-peak matching";
            this._tipSideBar.SetToolTip(this._chkPeakPeakMatch, "Select this option to automatically annotate peaks with similar peaks.\r\n\r\nThis wi" +
        "ll only function if the requisite information has been selected.\r\n");
            this._chkPeakPeakMatch.UseVisualStyleBackColor = true;
            this._chkPeakPeakMatch.CheckedChanged += new System.EventHandler(this._chkAutoIdentify_CheckedChanged);
            // 
            // _chkIdentifications
            // 
            this._chkIdentifications.AutoSize = true;
            this.tableLayoutPanel9.SetColumnSpan(this._chkIdentifications, 4);
            this._chkIdentifications.Location = new System.Drawing.Point(24, 333);
            this._chkIdentifications.Margin = new System.Windows.Forms.Padding(8);
            this._chkIdentifications.Name = "_chkIdentifications";
            this._chkIdentifications.Size = new System.Drawing.Size(205, 25);
            this._chkIdentifications.TabIndex = 11;
            this._chkIdentifications.Text = "Load manual annotations";
            this._tipSideBar.SetToolTip(this._chkIdentifications, global::MetaboliteLevels.Resx.Manual.Identifications);
            this._chkIdentifications.UseVisualStyleBackColor = true;
            this._chkIdentifications.CheckedChanged += new System.EventHandler(this._chkIdentifications_CheckedChanged);
            // 
            // _lstTolerance
            // 
            this._lstTolerance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstTolerance.Enabled = false;
            this._lstTolerance.FormattingEnabled = true;
            this._lstTolerance.Location = new System.Drawing.Point(410, 288);
            this._lstTolerance.Margin = new System.Windows.Forms.Padding(8);
            this._lstTolerance.Name = "_lstTolerance";
            this._lstTolerance.Size = new System.Drawing.Size(121, 29);
            this._lstTolerance.TabIndex = 10;
            this._tipPopup.SetToolTip(this._lstTolerance, "Select units");
            // 
            // _lblPeakPeakMatchUnavail
            // 
            this._lblPeakPeakMatchUnavail.AutoSize = true;
            this.tableLayoutPanel9.SetColumnSpan(this._lblPeakPeakMatchUnavail, 4);
            this._lblPeakPeakMatchUnavail.Font = new System.Drawing.Font("Segoe UI Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblPeakPeakMatchUnavail.Location = new System.Drawing.Point(64, 230);
            this._lblPeakPeakMatchUnavail.Margin = new System.Windows.Forms.Padding(48, 0, 0, 0);
            this._lblPeakPeakMatchUnavail.Name = "_lblPeakPeakMatchUnavail";
            this._lblPeakPeakMatchUnavail.Size = new System.Drawing.Size(102, 13);
            this._lblPeakPeakMatchUnavail.TabIndex = 7;
            this._lblPeakPeakMatchUnavail.Text = "<TEXT GOES HERE>";
            // 
            // _numTolerance
            // 
            this.tableLayoutPanel9.SetColumnSpan(this._numTolerance, 2);
            this._numTolerance.Dock = System.Windows.Forms.DockStyle.Top;
            this._numTolerance.Enabled = false;
            this._numTolerance.Location = new System.Drawing.Point(64, 288);
            this._numTolerance.Margin = new System.Windows.Forms.Padding(48, 8, 8, 8);
            this._numTolerance.Maximum = new decimal(new int[] {
            276447232,
            23283,
            0,
            0});
            this._numTolerance.Name = "_numTolerance";
            this._numTolerance.Size = new System.Drawing.Size(330, 29);
            this._numTolerance.TabIndex = 9;
            // 
            // _txtIdentifications
            // 
            this.tableLayoutPanel9.SetColumnSpan(this._txtIdentifications, 3);
            this._txtIdentifications.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtIdentifications.Enabled = false;
            this._txtIdentifications.ForeColor = System.Drawing.Color.Blue;
            this._txtIdentifications.Location = new System.Drawing.Point(64, 374);
            this._txtIdentifications.Margin = new System.Windows.Forms.Padding(48, 8, 8, 8);
            this._txtIdentifications.Name = "_txtIdentifications";
            this._txtIdentifications.Size = new System.Drawing.Size(467, 29);
            this._txtIdentifications.TabIndex = 12;
            this._tipSideBar.SetToolTip(this._txtIdentifications, global::MetaboliteLevels.Resx.Manual.Identifications);
            this._txtIdentifications.Watermark = null;
            // 
            // _manualFlag
            // 
            this.tableLayoutPanel9.SetColumnSpan(this._manualFlag, 2);
            this._manualFlag.Dock = System.Windows.Forms.DockStyle.Top;
            this._manualFlag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._manualFlag.Enabled = false;
            this._manualFlag.FormattingEnabled = true;
            this._manualFlag.Location = new System.Drawing.Point(174, 419);
            this._manualFlag.Margin = new System.Windows.Forms.Padding(8);
            this._manualFlag.Name = "_manualFlag";
            this._manualFlag.Size = new System.Drawing.Size(357, 29);
            this._manualFlag.TabIndex = 15;
            // 
            // _lblMzMatchUnavail
            // 
            this._lblMzMatchUnavail.AutoSize = true;
            this._lblMzMatchUnavail.Font = new System.Drawing.Font("Segoe UI Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblMzMatchUnavail.Location = new System.Drawing.Point(64, 131);
            this._lblMzMatchUnavail.Margin = new System.Windows.Forms.Padding(48, 0, 0, 0);
            this._lblMzMatchUnavail.Name = "_lblMzMatchUnavail";
            this._lblMzMatchUnavail.Size = new System.Drawing.Size(102, 13);
            this._lblMzMatchUnavail.TabIndex = 3;
            this._lblMzMatchUnavail.Text = "<TEXT GOES HERE>";
            // 
            // _automaticFlag
            // 
            this.tableLayoutPanel9.SetColumnSpan(this._automaticFlag, 2);
            this._automaticFlag.Dock = System.Windows.Forms.DockStyle.Top;
            this._automaticFlag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._automaticFlag.Enabled = false;
            this._automaticFlag.FormattingEnabled = true;
            this._automaticFlag.Location = new System.Drawing.Point(174, 152);
            this._automaticFlag.Margin = new System.Windows.Forms.Padding(8);
            this._automaticFlag.Name = "_automaticFlag";
            this._automaticFlag.Size = new System.Drawing.Size(357, 29);
            this._automaticFlag.TabIndex = 5;
            // 
            // _tabReady
            // 
            this._tabReady.Controls.Add(this.tableLayoutPanel3);
            this._tabReady.Location = new System.Drawing.Point(4, 4);
            this._tabReady.Name = "_tabReady";
            this._tabReady.Padding = new System.Windows.Forms.Padding(3);
            this._tabReady.Size = new System.Drawing.Size(605, 690);
            this._tabReady.TabIndex = 7;
            this._tabReady.Text = "Ready";
            this._tabReady.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this._chkAlarm, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label12, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.Padding = new System.Windows.Forms.Padding(16);
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(599, 684);
            this.tableLayoutPanel3.TabIndex = 5;
            // 
            // _chkAlarm
            // 
            this._chkAlarm.AutoSize = true;
            this._chkAlarm.Image = global::MetaboliteLevels.Properties.Resources.TeaAlarm;
            this._chkAlarm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._chkAlarm.Location = new System.Drawing.Point(19, 625);
            this._chkAlarm.Name = "_chkAlarm";
            this._chkAlarm.Padding = new System.Windows.Forms.Padding(4);
            this._chkAlarm.Size = new System.Drawing.Size(427, 40);
            this._chkAlarm.TabIndex = 5;
            this._chkAlarm.Text = "Make a sound when the data has finished loading";
            this._chkAlarm.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._tipSideBar.SetToolTip(this._chkAlarm, "Set to play a brief sound when the dataset is created (Windows only).");
            this._tipPopup.SetToolTip(this._chkAlarm, "Set to play a brief sound when the dataset is created (Windows only).");
            this._chkAlarm.UseVisualStyleBackColor = true;
            // 
            // _txtHelp
            // 
            this._txtHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this._txtHelp.Location = new System.Drawing.Point(0, 94);
            this._txtHelp.Margin = new System.Windows.Forms.Padding(4);
            this._txtHelp.Multiline = true;
            this._txtHelp.Name = "_txtHelp";
            this._txtHelp.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._txtHelp.Size = new System.Drawing.Size(301, 542);
            this._txtHelp.TabIndex = 1;
            this._txtHelp.Text = "Click a control for help.";
            this._tipSideBar.SetToolTip(this._txtHelp, "Click a control for help.\r\n\r\nNot this one.");
            this._txtHelp.Watermark = null;
            // 
            // _cmsRecentSessions
            // 
            this._cmsRecentSessions.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._cmsRecentSessions.Name = "contextMenuStrip2";
            this._cmsRecentSessions.Size = new System.Drawing.Size(61, 4);
            // 
            // _tipPopup
            // 
            this._tipPopup.AutomaticDelay = 200;
            this._tipPopup.AutoPopDelay = 5000;
            this._tipPopup.InitialDelay = 200;
            this._tipPopup.ReshowDelay = 40;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.splitContainer1, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 730F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1008, 730);
            this.tableLayoutPanel4.TabIndex = 19;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.ForeColor = System.Drawing.Color.Black;
            this.splitContainer1.Location = new System.Drawing.Point(16, 16);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(16);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._txtHelp);
            this.splitContainer1.Panel2.Controls.Add(this._lblOrder);
            this.splitContainer1.Panel2.Controls.Add(this._btnShowFf);
            this.splitContainer1.Panel2.Controls.Add(this.ctlTitleBar1);
            this.splitContainer1.Size = new System.Drawing.Size(976, 698);
            this.splitContainer1.SplitterDistance = 669;
            this.splitContainer1.TabIndex = 18;
            // 
            // _btnShowFf
            // 
            this._btnShowFf.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._btnShowFf.FlatAppearance.BorderColor = System.Drawing.Color.Purple;
            this._btnShowFf.FlatAppearance.BorderSize = 2;
            this._btnShowFf.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Thistle;
            this._btnShowFf.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Plum;
            this._btnShowFf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnShowFf.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnShowFf.ForeColor = System.Drawing.Color.Black;
            this._btnShowFf.Location = new System.Drawing.Point(0, 636);
            this._btnShowFf.Name = "_btnShowFf";
            this._btnShowFf.Size = new System.Drawing.Size(301, 62);
            this._btnShowFf.TabIndex = 0;
            this._btnShowFf.Text = "File format details -->";
            this._btnShowFf.UseVisualStyleBackColor = false;
            this._btnShowFf.Visible = false;
            this._btnShowFf.Click += new System.EventHandler(this._btnShowFf_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(536, 9);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(106, 21);
            this.label15.TabIndex = 23;
            this.label15.Text = "^^^[NOBAR]";
            // 
            // _btnMostRecent
            // 
            this._btnMostRecent.BackColor = System.Drawing.Color.LightSteelBlue;
            this._btnMostRecent.Dock = System.Windows.Forms.DockStyle.Top;
            this._btnMostRecent.FlatAppearance.BorderColor = System.Drawing.Color.LightSteelBlue;
            this._btnMostRecent.FlatAppearance.BorderSize = 8;
            this._btnMostRecent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnMostRecent.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnMostRecent.Image = ((System.Drawing.Image)(resources.GetObject("_btnMostRecent.Image")));
            this._btnMostRecent.Location = new System.Drawing.Point(41, 230);
            this._btnMostRecent.Margin = new System.Windows.Forms.Padding(41, 10, 10, 10);
            this._btnMostRecent.MaximumSize = new System.Drawing.Size(658, 672);
            this._btnMostRecent.Name = "_btnMostRecent";
            this._btnMostRecent.Size = new System.Drawing.Size(336, 74);
            this._btnMostRecent.TabIndex = 2;
            this._btnMostRecent.Text = "    Last file";
            this._tipPopup.SetToolTip(this._btnMostRecent, "Load last used session");
            this._btnMostRecent.UseVisualStyleBackColor = false;
            this._btnMostRecent.Click += new System.EventHandler(this._btnMostRecent_Click);
            // 
            // _btnNewSession
            // 
            this._btnNewSession.BackColor = System.Drawing.Color.LightSteelBlue;
            this._btnNewSession.Dock = System.Windows.Forms.DockStyle.Top;
            this._btnNewSession.FlatAppearance.BorderColor = System.Drawing.Color.LightSteelBlue;
            this._btnNewSession.FlatAppearance.BorderSize = 8;
            this._btnNewSession.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnNewSession.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnNewSession.Image = ((System.Drawing.Image)(resources.GetObject("_btnNewSession.Image")));
            this._btnNewSession.Location = new System.Drawing.Point(41, 42);
            this._btnNewSession.Margin = new System.Windows.Forms.Padding(41, 42, 10, 10);
            this._btnNewSession.MaximumSize = new System.Drawing.Size(658, 672);
            this._btnNewSession.Name = "_btnNewSession";
            this._btnNewSession.Size = new System.Drawing.Size(336, 74);
            this._btnNewSession.TabIndex = 0;
            this._btnNewSession.Text = "    Create a new session";
            this._tipPopup.SetToolTip(this._btnNewSession, "Show the new session creation wizard");
            this._btnNewSession.UseVisualStyleBackColor = false;
            this._btnNewSession.Click += new System.EventHandler(this.button1_Click);
            // 
            // _btnReturnToSession
            // 
            this._btnReturnToSession.BackColor = System.Drawing.Color.LightSteelBlue;
            this._btnReturnToSession.Dock = System.Windows.Forms.DockStyle.Top;
            this._btnReturnToSession.FlatAppearance.BorderColor = System.Drawing.Color.LightSteelBlue;
            this._btnReturnToSession.FlatAppearance.BorderSize = 8;
            this._btnReturnToSession.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnReturnToSession.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnReturnToSession.Image = ((System.Drawing.Image)(resources.GetObject("_btnReturnToSession.Image")));
            this._btnReturnToSession.Location = new System.Drawing.Point(41, 136);
            this._btnReturnToSession.Margin = new System.Windows.Forms.Padding(41, 10, 10, 10);
            this._btnReturnToSession.MaximumSize = new System.Drawing.Size(658, 672);
            this._btnReturnToSession.Name = "_btnReturnToSession";
            this._btnReturnToSession.Size = new System.Drawing.Size(336, 74);
            this._btnReturnToSession.TabIndex = 1;
            this._btnReturnToSession.Text = "    Return to an existing session";
            this._tipPopup.SetToolTip(this._btnReturnToSession, "Show list of recent session or open the file browser to select a session from dis" +
        "k");
            this._btnReturnToSession.UseVisualStyleBackColor = false;
            this._btnReturnToSession.Click += new System.EventHandler(this.button2_Click);
            // 
            // _btnReconfigure
            // 
            this._btnReconfigure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnReconfigure.AutoSize = true;
            this._btnReconfigure.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._btnReconfigure.FlatAppearance.BorderSize = 0;
            this._btnReconfigure.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnReconfigure.ForeColor = System.Drawing.Color.Silver;
            this._btnReconfigure.Image = ((System.Drawing.Image)(resources.GetObject("_btnReconfigure.Image")));
            this._btnReconfigure.Location = new System.Drawing.Point(557, 604);
            this._btnReconfigure.Margin = new System.Windows.Forms.Padding(41, 10, 10, 10);
            this._btnReconfigure.MaximumSize = new System.Drawing.Size(658, 672);
            this._btnReconfigure.Name = "_btnReconfigure";
            this._btnReconfigure.Size = new System.Drawing.Size(38, 38);
            this._btnReconfigure.TabIndex = 3;
            this._tipPopup.SetToolTip(this._btnReconfigure, "Show settings menu");
            this._btnReconfigure.Click += new System.EventHandler(this._btnReconfigure_Click);
            // 
            // ctlLabel6
            // 
            this.ctlLabel6.AutoSize = true;
            this.tableLayoutPanel8.SetColumnSpan(this.ctlLabel6, 3);
            this.ctlLabel6.LabelStyle = ELabelStyle.Caption;
            this.ctlLabel6.Location = new System.Drawing.Point(20, 23);
            this.ctlLabel6.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.ctlLabel6.Name = "ctlLabel6";
            this.ctlLabel6.Size = new System.Drawing.Size(524, 21);
            this.ctlLabel6.TabIndex = 0;
            this.ctlLabel6.Text = "^^Start with a blank configuration or use a previous session as a template";
            this._tipSideBar.SetToolTip(this.ctlLabel6, global::MetaboliteLevels.Resx.Manual.RecentSessions);
            // 
            // ctlLabel7
            // 
            this.ctlLabel7.AutoSize = true;
            this.tableLayoutPanel8.SetColumnSpan(this.ctlLabel7, 3);
            this.ctlLabel7.Location = new System.Drawing.Point(20, 58);
            this.ctlLabel7.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.ctlLabel7.Name = "ctlLabel7";
            this.ctlLabel7.Size = new System.Drawing.Size(74, 21);
            this.ctlLabel7.TabIndex = 1;
            this.ctlLabel7.Text = "Template";
            this._tipSideBar.SetToolTip(this.ctlLabel7, global::MetaboliteLevels.Resx.Manual.RecentSessions);
            // 
            // _btnDeleteWorkspace
            // 
            this._btnDeleteWorkspace.Image = global::MetaboliteLevels.Properties.Resources.MnuDeleteWorkspace;
            this._btnDeleteWorkspace.Location = new System.Drawing.Point(552, 176);
            this._btnDeleteWorkspace.Margin = new System.Windows.Forms.Padding(8);
            this._btnDeleteWorkspace.Name = "_btnDeleteWorkspace";
            this._btnDeleteWorkspace.Size = new System.Drawing.Size(29, 29);
            this._btnDeleteWorkspace.TabIndex = 6;
            this._btnDeleteWorkspace.Text = "";
            this._tipSideBar.SetToolTip(this._btnDeleteWorkspace, global::MetaboliteLevels.Resx.Manual.RecentSessions);
            this._tipPopup.SetToolTip(this._btnDeleteWorkspace, "Removes a previous configuration from the session history");
            this._btnDeleteWorkspace.UseDefaultSize = true;
            this._btnDeleteWorkspace.UseVisualStyleBackColor = true;
            this._btnDeleteWorkspace.Click += new System.EventHandler(this.ctlButton3_Click_1);
            // 
            // _btnRecentWorkspace
            // 
            this._btnRecentWorkspace.Image = global::MetaboliteLevels.Properties.Resources.MnuOpen;
            this._btnRecentWorkspace.Location = new System.Drawing.Point(508, 176);
            this._btnRecentWorkspace.Margin = new System.Windows.Forms.Padding(8);
            this._btnRecentWorkspace.Name = "_btnRecentWorkspace";
            this._btnRecentWorkspace.Size = new System.Drawing.Size(28, 29);
            this._btnRecentWorkspace.TabIndex = 5;
            this._tipSideBar.SetToolTip(this._btnRecentWorkspace, global::MetaboliteLevels.Resx.Manual.RecentSessions);
            this._tipPopup.SetToolTip(this._btnRecentWorkspace, "Show drop down menu");
            this._btnRecentWorkspace.UseVisualStyleBackColor = true;
            this._btnRecentWorkspace.Click += new System.EventHandler(this.radioButton2_Click);
            // 
            // _lblTitle
            // 
            this._lblTitle.AutoSize = true;
            this._lblTitle.Location = new System.Drawing.Point(20, 60);
            this._lblTitle.Margin = new System.Windows.Forms.Padding(4, 9, 4, 7);
            this._lblTitle.Name = "_lblTitle";
            this._lblTitle.Size = new System.Drawing.Size(106, 21);
            this._lblTitle.TabIndex = 1;
            this._lblTitle.Text = "Session name";
            this._tipSideBar.SetToolTip(this._lblTitle, global::MetaboliteLevels.Resx.Manual.Session);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.LabelStyle = ELabelStyle.Caption;
            this.label2.Location = new System.Drawing.Point(20, 23);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(337, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "^^Provide a descriptive name for your session.";
            this._tipSideBar.SetToolTip(this.label2, global::MetaboliteLevels.Resx.Manual.Session);
            // 
            // _btnCondInfo
            // 
            this._btnCondInfo.Enabled = false;
            this._btnCondInfo.Image = global::MetaboliteLevels.Properties.Resources.MnuOpen;
            this._btnCondInfo.Location = new System.Drawing.Point(549, 497);
            this._btnCondInfo.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this._btnCondInfo.Name = "_btnCondInfo";
            this._btnCondInfo.Size = new System.Drawing.Size(28, 29);
            this._btnCondInfo.TabIndex = 18;
            this._tipSideBar.SetToolTip(this._btnCondInfo, global::MetaboliteLevels.Resx.Manual.ConditionNames);
            this._tipPopup.SetToolTip(this._btnCondInfo, "Browse for file");
            this._btnCondInfo.UseVisualStyleBackColor = true;
            this._btnCondInfo.Click += new System.EventHandler(this._btnCondInfo_Click);
            // 
            // _lblLcmsMode
            // 
            this._lblLcmsMode.AutoSize = true;
            this._lblLcmsMode.Location = new System.Drawing.Point(40, 109);
            this._lblLcmsMode.Margin = new System.Windows.Forms.Padding(24, 0, 4, 0);
            this._lblLcmsMode.Name = "_lblLcmsMode";
            this._lblLcmsMode.Size = new System.Drawing.Size(58, 21);
            this._lblLcmsMode.TabIndex = 2;
            this._lblLcmsMode.Text = "Source";
            this._tipSideBar.SetToolTip(this._lblLcmsMode, global::MetaboliteLevels.Resx.Manual.LcMsMode);
            // 
            // _btnDataSetVar
            // 
            this._btnDataSetVar.Image = global::MetaboliteLevels.Properties.Resources.MnuOpen;
            this._btnDataSetVar.Location = new System.Drawing.Point(549, 329);
            this._btnDataSetVar.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this._btnDataSetVar.Name = "_btnDataSetVar";
            this._btnDataSetVar.Size = new System.Drawing.Size(28, 29);
            this._btnDataSetVar.TabIndex = 12;
            this._tipSideBar.SetToolTip(this._btnDataSetVar, global::MetaboliteLevels.Resx.Manual.Variables);
            this._tipPopup.SetToolTip(this._btnDataSetVar, "Browse for file");
            this._btnDataSetVar.UseVisualStyleBackColor = true;
            this._btnDataSetVar.Click += new System.EventHandler(this._btnDataSetVar_Click);
            // 
            // _btnDataSetObs
            // 
            this._btnDataSetObs.Image = global::MetaboliteLevels.Properties.Resources.MnuOpen;
            this._btnDataSetObs.Location = new System.Drawing.Point(549, 265);
            this._btnDataSetObs.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this._btnDataSetObs.Name = "_btnDataSetObs";
            this._btnDataSetObs.Size = new System.Drawing.Size(28, 29);
            this._btnDataSetObs.TabIndex = 9;
            this._tipSideBar.SetToolTip(this._btnDataSetObs, global::MetaboliteLevels.Resx.Manual.Observations);
            this._tipPopup.SetToolTip(this._btnDataSetObs, "Browse for file");
            this._btnDataSetObs.UseVisualStyleBackColor = true;
            this._btnDataSetObs.Click += new System.EventHandler(this._btnDataSetObs_Click);
            // 
            // _btnDataSetData
            // 
            this._btnDataSetData.Image = global::MetaboliteLevels.Properties.Resources.MnuOpen;
            this._btnDataSetData.Location = new System.Drawing.Point(549, 201);
            this._btnDataSetData.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this._btnDataSetData.Name = "_btnDataSetData";
            this._btnDataSetData.Size = new System.Drawing.Size(28, 29);
            this._btnDataSetData.TabIndex = 6;
            this._tipSideBar.SetToolTip(this._btnDataSetData, global::MetaboliteLevels.Resx.Manual.Intensities);
            this._tipPopup.SetToolTip(this._btnDataSetData, "Browse for file");
            this._btnDataSetData.UseVisualStyleBackColor = true;
            this._btnDataSetData.Click += new System.EventHandler(this._btnDataSet_Click);
            // 
            // _lblDataSetData
            // 
            this._lblDataSetData.AutoSize = true;
            this._lblDataSetData.Location = new System.Drawing.Point(40, 173);
            this._lblDataSetData.Margin = new System.Windows.Forms.Padding(24, 0, 4, 0);
            this._lblDataSetData.Name = "_lblDataSetData";
            this._lblDataSetData.Size = new System.Drawing.Size(80, 21);
            this._lblDataSetData.TabIndex = 4;
            this._lblDataSetData.Text = "Intensities";
            this._tipSideBar.SetToolTip(this._lblDataSetData, global::MetaboliteLevels.Resx.Manual.Intensities);
            this._lblDataSetData.Click += new System.EventHandler(this._lblDataSetData_Click);
            // 
            // _lblDataSetObs
            // 
            this._lblDataSetObs.AutoSize = true;
            this._lblDataSetObs.Location = new System.Drawing.Point(40, 237);
            this._lblDataSetObs.Margin = new System.Windows.Forms.Padding(24, 0, 4, 0);
            this._lblDataSetObs.Name = "_lblDataSetObs";
            this._lblDataSetObs.Size = new System.Drawing.Size(102, 21);
            this._lblDataSetObs.TabIndex = 7;
            this._lblDataSetObs.Text = "Observations";
            this._tipSideBar.SetToolTip(this._lblDataSetObs, global::MetaboliteLevels.Resx.Manual.Observations);
            // 
            // _lblDataSetVar
            // 
            this._lblDataSetVar.AutoSize = true;
            this._lblDataSetVar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblDataSetVar.Location = new System.Drawing.Point(40, 301);
            this._lblDataSetVar.Margin = new System.Windows.Forms.Padding(24, 0, 4, 0);
            this._lblDataSetVar.Name = "_lblDataSetVar";
            this._lblDataSetVar.Size = new System.Drawing.Size(50, 21);
            this._lblDataSetVar.TabIndex = 10;
            this._lblDataSetVar.Text = "Peaks";
            this._tipSideBar.SetToolTip(this._lblDataSetVar, global::MetaboliteLevels.Resx.Manual.Variables);
            // 
            // _btnAltVals
            // 
            this._btnAltVals.Enabled = false;
            this._btnAltVals.Image = global::MetaboliteLevels.Properties.Resources.MnuOpen;
            this._btnAltVals.Location = new System.Drawing.Point(549, 413);
            this._btnAltVals.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this._btnAltVals.Name = "_btnAltVals";
            this._btnAltVals.Size = new System.Drawing.Size(28, 29);
            this._btnAltVals.TabIndex = 15;
            this._tipSideBar.SetToolTip(this._btnAltVals, global::MetaboliteLevels.Resx.Manual.AlternativeValues);
            this._tipPopup.SetToolTip(this._btnAltVals, "Browse for file");
            this._btnAltVals.UseVisualStyleBackColor = true;
            this._btnAltVals.Click += new System.EventHandler(this._btnAltVals_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.LabelStyle = ELabelStyle.Caption;
            this.label7.Location = new System.Drawing.Point(20, 23);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(504, 42);
            this.label7.TabIndex = 0;
            this.label7.Text = "^^Select the data files you want to work with.\r\nInformation about supported file " +
    "formats can be found by viewing help.";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(20, 81);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 9, 4, 7);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(66, 21);
            this.label10.TabIndex = 1;
            this.label10.Text = "Data set";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.LabelStyle = ELabelStyle.Caption;
            this.label5.Location = new System.Drawing.Point(20, 25);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 9, 4, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(258, 21);
            this.label5.TabIndex = 0;
            this.label5.Text = "^^Specify the conditions of interest";
            // 
            // _lblConditions
            // 
            this._lblConditions.AutoSize = true;
            this._lblConditions.Location = new System.Drawing.Point(40, 94);
            this._lblConditions.Margin = new System.Windows.Forms.Padding(24, 0, 4, 0);
            this._lblConditions.Name = "_lblConditions";
            this._lblConditions.Size = new System.Drawing.Size(309, 21);
            this._lblConditions.TabIndex = 2;
            this._lblConditions.Text = "Group(s) of interest (i.e. not control or QCs)";
            this._tipSideBar.SetToolTip(this._lblConditions, global::MetaboliteLevels.Resx.Manual.ExperimentalConditions);
            // 
            // _btnBrowseContCond
            // 
            this._btnBrowseContCond.Image = global::MetaboliteLevels.Properties.Resources.MnuEnlargeList;
            this._btnBrowseContCond.Location = new System.Drawing.Point(551, 186);
            this._btnBrowseContCond.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this._btnBrowseContCond.Name = "_btnBrowseContCond";
            this._btnBrowseContCond.Size = new System.Drawing.Size(28, 29);
            this._btnBrowseContCond.TabIndex = 7;
            this._tipSideBar.SetToolTip(this._btnBrowseContCond, global::MetaboliteLevels.Resx.Manual.ControlConditions);
            this._tipPopup.SetToolTip(this._btnBrowseContCond, "Show options in new window");
            this._btnBrowseContCond.UseVisualStyleBackColor = true;
            // 
            // _btnBrowseExpCond
            // 
            this._btnBrowseExpCond.Image = global::MetaboliteLevels.Properties.Resources.MnuEnlargeList;
            this._btnBrowseExpCond.Location = new System.Drawing.Point(551, 122);
            this._btnBrowseExpCond.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this._btnBrowseExpCond.Name = "_btnBrowseExpCond";
            this._btnBrowseExpCond.Size = new System.Drawing.Size(28, 29);
            this._btnBrowseExpCond.TabIndex = 4;
            this._tipSideBar.SetToolTip(this._btnBrowseExpCond, global::MetaboliteLevels.Resx.Manual.ExperimentalConditions);
            this._tipPopup.SetToolTip(this._btnBrowseExpCond, "Show options in new window");
            this._btnBrowseExpCond.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 158);
            this.label3.Margin = new System.Windows.Forms.Padding(24, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 21);
            this.label3.TabIndex = 5;
            this.label3.Text = "Control group(s)";
            this._tipSideBar.SetToolTip(this.label3, global::MetaboliteLevels.Resx.Manual.ControlConditions);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.LabelStyle = ELabelStyle.Caption;
            this.label14.Location = new System.Drawing.Point(20, 23);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(548, 21);
            this.label14.TabIndex = 0;
            this.label14.Text = "^^Select the defult condiguration - you can always add or remove these later.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 60);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 9, 4, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(154, 21);
            this.label4.TabIndex = 1;
            this.label4.Text = "Auto-create statistics";
            // 
            // ctlLabel2
            // 
            this.ctlLabel2.AutoSize = true;
            this.ctlLabel2.Location = new System.Drawing.Point(20, 201);
            this.ctlLabel2.Margin = new System.Windows.Forms.Padding(4, 9, 4, 7);
            this.ctlLabel2.Name = "ctlLabel2";
            this.ctlLabel2.Size = new System.Drawing.Size(139, 21);
            this.ctlLabel2.TabIndex = 2;
            this.ctlLabel2.Text = "Auto-create trends";
            // 
            // ctlLabel4
            // 
            this.ctlLabel4.AutoSize = true;
            this.ctlLabel4.Location = new System.Drawing.Point(20, 316);
            this.ctlLabel4.Margin = new System.Windows.Forms.Padding(4, 9, 4, 7);
            this.ctlLabel4.Name = "ctlLabel4";
            this.ctlLabel4.Size = new System.Drawing.Size(148, 21);
            this.ctlLabel4.TabIndex = 3;
            this.ctlLabel4.Text = "Perform corrections";
            // 
            // _btnAddAdduct
            // 
            this._btnAddAdduct.Image = global::MetaboliteLevels.Properties.Resources.MnuMoveToList;
            this._btnAddAdduct.Location = new System.Drawing.Point(8, 8);
            this._btnAddAdduct.Margin = new System.Windows.Forms.Padding(8, 8, 8, 0);
            this._btnAddAdduct.Name = "_btnAddAdduct";
            this._btnAddAdduct.Size = new System.Drawing.Size(28, 29);
            this._btnAddAdduct.TabIndex = 0;
            this._btnAddAdduct.Text = "";
            this._tipSideBar.SetToolTip(this._btnAddAdduct, global::MetaboliteLevels.Resx.Manual.Adducts);
            this._tipPopup.SetToolTip(this._btnAddAdduct, "Add selected library");
            this._btnAddAdduct.UseVisualStyleBackColor = true;
            this._btnAddAdduct.Click += new System.EventHandler(this._btnAddAdduct_Click);
            // 
            // _btnBrowseAdducts
            // 
            this._btnBrowseAdducts.Enabled = false;
            this._btnBrowseAdducts.Image = global::MetaboliteLevels.Properties.Resources.MnuOpen;
            this._btnBrowseAdducts.Location = new System.Drawing.Point(8, 45);
            this._btnBrowseAdducts.Margin = new System.Windows.Forms.Padding(8, 8, 8, 0);
            this._btnBrowseAdducts.Name = "_btnBrowseAdducts";
            this._btnBrowseAdducts.Size = new System.Drawing.Size(28, 29);
            this._btnBrowseAdducts.TabIndex = 1;
            this._tipSideBar.SetToolTip(this._btnBrowseAdducts, global::MetaboliteLevels.Resx.Manual.Adducts);
            this._tipPopup.SetToolTip(this._btnBrowseAdducts, "Browse for library");
            this._btnBrowseAdducts.UseVisualStyleBackColor = true;
            this._btnBrowseAdducts.Click += new System.EventHandler(this._btnBrowseAdducts_Click);
            // 
            // _btnDelAdduct
            // 
            this._btnDelAdduct.Image = global::MetaboliteLevels.Properties.Resources.MnuClear;
            this._btnDelAdduct.Location = new System.Drawing.Point(8, 82);
            this._btnDelAdduct.Margin = new System.Windows.Forms.Padding(8, 8, 8, 0);
            this._btnDelAdduct.Name = "_btnDelAdduct";
            this._btnDelAdduct.Size = new System.Drawing.Size(28, 29);
            this._btnDelAdduct.TabIndex = 2;
            this._tipSideBar.SetToolTip(this._btnDelAdduct, global::MetaboliteLevels.Resx.Manual.Adducts);
            this._tipPopup.SetToolTip(this._btnDelAdduct, "Remove selected library");
            this._btnDelAdduct.UseVisualStyleBackColor = true;
            this._btnDelAdduct.Click += new System.EventHandler(this.ctlButton3_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.tableLayoutPanel7.SetColumnSpan(this.label9, 3);
            this.label9.LabelStyle = ELabelStyle.Caption;
            this.label9.Location = new System.Drawing.Point(24, 24);
            this.label9.Margin = new System.Windows.Forms.Padding(8);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(238, 21);
            this.label9.TabIndex = 0;
            this.label9.Text = "^^Select the compound libraries";
            // 
            // _btnAddCompound
            // 
            this._btnAddCompound.Image = global::MetaboliteLevels.Properties.Resources.MnuMoveToList;
            this._btnAddCompound.Location = new System.Drawing.Point(8, 8);
            this._btnAddCompound.Margin = new System.Windows.Forms.Padding(8, 8, 8, 0);
            this._btnAddCompound.Name = "_btnAddCompound";
            this._btnAddCompound.Size = new System.Drawing.Size(28, 29);
            this._btnAddCompound.TabIndex = 0;
            this._btnAddCompound.Text = "";
            this._tipSideBar.SetToolTip(this._btnAddCompound, global::MetaboliteLevels.Resx.Manual.Compounds);
            this._tipPopup.SetToolTip(this._btnAddCompound, "Add selected library");
            this._btnAddCompound.UseVisualStyleBackColor = true;
            this._btnAddCompound.Click += new System.EventHandler(this._btnIdentifications_Click);
            // 
            // _btnAddAllCompounds
            // 
            this._btnAddAllCompounds.Image = global::MetaboliteLevels.Properties.Resources.MnuAll;
            this._btnAddAllCompounds.Location = new System.Drawing.Point(8, 45);
            this._btnAddAllCompounds.Margin = new System.Windows.Forms.Padding(8, 8, 8, 0);
            this._btnAddAllCompounds.Name = "_btnAddAllCompounds";
            this._btnAddAllCompounds.Size = new System.Drawing.Size(28, 29);
            this._btnAddAllCompounds.TabIndex = 1;
            this._btnAddAllCompounds.Text = "";
            this._tipSideBar.SetToolTip(this._btnAddAllCompounds, global::MetaboliteLevels.Resx.Manual.Compounds);
            this._tipPopup.SetToolTip(this._btnAddAllCompounds, "Add all libraries");
            this._btnAddAllCompounds.UseVisualStyleBackColor = true;
            this._btnAddAllCompounds.Click += new System.EventHandler(this._btnAddAllCompounds_Click);
            // 
            // _btnAddCompoundLibrary
            // 
            this._btnAddCompoundLibrary.Image = global::MetaboliteLevels.Properties.Resources.MnuOpen;
            this._btnAddCompoundLibrary.Location = new System.Drawing.Point(8, 82);
            this._btnAddCompoundLibrary.Margin = new System.Windows.Forms.Padding(8, 8, 8, 0);
            this._btnAddCompoundLibrary.Name = "_btnAddCompoundLibrary";
            this._btnAddCompoundLibrary.Size = new System.Drawing.Size(28, 29);
            this._btnAddCompoundLibrary.TabIndex = 2;
            this._btnAddCompoundLibrary.Text = "";
            this._tipSideBar.SetToolTip(this._btnAddCompoundLibrary, global::MetaboliteLevels.Resx.Manual.Compounds);
            this._tipPopup.SetToolTip(this._btnAddCompoundLibrary, "Browse for librar");
            this._btnAddCompoundLibrary.UseVisualStyleBackColor = true;
            this._btnAddCompoundLibrary.Click += new System.EventHandler(this._btnAddCompoundLibrary_Click);
            // 
            // _btnRemoveLibrary
            // 
            this._btnRemoveLibrary.Image = global::MetaboliteLevels.Properties.Resources.MnuClear;
            this._btnRemoveLibrary.Location = new System.Drawing.Point(8, 119);
            this._btnRemoveLibrary.Margin = new System.Windows.Forms.Padding(8, 8, 8, 0);
            this._btnRemoveLibrary.Name = "_btnRemoveLibrary";
            this._btnRemoveLibrary.Size = new System.Drawing.Size(28, 29);
            this._btnRemoveLibrary.TabIndex = 3;
            this._tipSideBar.SetToolTip(this._btnRemoveLibrary, global::MetaboliteLevels.Resx.Manual.Compounds);
            this._tipPopup.SetToolTip(this._btnRemoveLibrary, "Remove selected library");
            this._btnRemoveLibrary.UseVisualStyleBackColor = true;
            this._btnRemoveLibrary.Click += new System.EventHandler(this.ctlButton2_Click);
            // 
            // _lblAdducts
            // 
            this._lblAdducts.AutoSize = true;
            this.tableLayoutPanel7.SetColumnSpan(this._lblAdducts, 3);
            this._lblAdducts.Location = new System.Drawing.Point(24, 367);
            this._lblAdducts.Margin = new System.Windows.Forms.Padding(8);
            this._lblAdducts.Name = "_lblAdducts";
            this._lblAdducts.Size = new System.Drawing.Size(214, 21);
            this._lblAdducts.TabIndex = 4;
            this._lblAdducts.Text = "Adduct libraries (LC-MS only)";
            this._tipSideBar.SetToolTip(this._lblAdducts, global::MetaboliteLevels.Resx.Manual.Adducts);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.tableLayoutPanel7.SetColumnSpan(this.label6, 3);
            this.label6.Location = new System.Drawing.Point(24, 61);
            this.label6.Margin = new System.Windows.Forms.Padding(8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(148, 21);
            this.label6.TabIndex = 1;
            this.label6.Text = "Compound libraries";
            this._tipSideBar.SetToolTip(this.label6, global::MetaboliteLevels.Resx.Manual.Compounds);
            // 
            // _btnIdentifications
            // 
            this._btnIdentifications.Enabled = false;
            this._btnIdentifications.Image = global::MetaboliteLevels.Properties.Resources.MnuOpen;
            this._btnIdentifications.Location = new System.Drawing.Point(547, 374);
            this._btnIdentifications.Margin = new System.Windows.Forms.Padding(8);
            this._btnIdentifications.Name = "_btnIdentifications";
            this._btnIdentifications.Size = new System.Drawing.Size(28, 29);
            this._btnIdentifications.TabIndex = 13;
            this._tipSideBar.SetToolTip(this._btnIdentifications, global::MetaboliteLevels.Resx.Manual.Identifications);
            this._tipPopup.SetToolTip(this._btnIdentifications, "Browse for file");
            this._btnIdentifications.UseVisualStyleBackColor = true;
            this._btnIdentifications.Click += new System.EventHandler(this._btnIdentifications_Click_1);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.tableLayoutPanel9.SetColumnSpan(this.label11, 4);
            this.label11.LabelStyle = ELabelStyle.Caption;
            this.label11.Location = new System.Drawing.Point(24, 24);
            this.label11.Margin = new System.Windows.Forms.Padding(8);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(321, 21);
            this.label11.TabIndex = 0;
            this.label11.Text = "^^Select methods of annotating compounds";
            // 
            // _lblTolerance
            // 
            this._lblTolerance.AutoSize = true;
            this._lblTolerance.Enabled = false;
            this._lblTolerance.Location = new System.Drawing.Point(24, 251);
            this._lblTolerance.Margin = new System.Windows.Forms.Padding(8);
            this._lblTolerance.Name = "_lblTolerance";
            this._lblTolerance.Size = new System.Drawing.Size(121, 21);
            this._lblTolerance.TabIndex = 8;
            this._lblTolerance.Text = "Match tolerance";
            // 
            // ctlLabel3
            // 
            this.ctlLabel3.AutoSize = true;
            this.ctlLabel3.Enabled = false;
            this.ctlLabel3.Location = new System.Drawing.Point(64, 419);
            this.ctlLabel3.Margin = new System.Windows.Forms.Padding(48, 8, 8, 8);
            this.ctlLabel3.Name = "ctlLabel3";
            this.ctlLabel3.Size = new System.Drawing.Size(68, 42);
            this.ctlLabel3.TabIndex = 14;
            this.ctlLabel3.Text = "Flag as\r\n(default)";
            // 
            // ctlLabel5
            // 
            this.ctlLabel5.AutoSize = true;
            this.tableLayoutPanel9.SetColumnSpan(this.ctlLabel5, 4);
            this.ctlLabel5.Enabled = false;
            this.ctlLabel5.Location = new System.Drawing.Point(24, 61);
            this.ctlLabel5.Margin = new System.Windows.Forms.Padding(8);
            this.ctlLabel5.Name = "ctlLabel5";
            this.ctlLabel5.Size = new System.Drawing.Size(73, 21);
            this.ctlLabel5.TabIndex = 1;
            this.ctlLabel5.Text = "Annotate";
            // 
            // ctlLabel1
            // 
            this.ctlLabel1.AutoSize = true;
            this.ctlLabel1.Enabled = false;
            this.ctlLabel1.Location = new System.Drawing.Point(64, 152);
            this.ctlLabel1.Margin = new System.Windows.Forms.Padding(48, 8, 8, 8);
            this.ctlLabel1.Name = "ctlLabel1";
            this.ctlLabel1.Size = new System.Drawing.Size(58, 21);
            this.ctlLabel1.TabIndex = 4;
            this.ctlLabel1.Text = "Flag as";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.LabelStyle = ELabelStyle.Caption;
            this.label12.Location = new System.Drawing.Point(64, 64);
            this.label12.Margin = new System.Windows.Forms.Padding(48);
            this.label12.Name = "label12";
            this.label12.Padding = new System.Windows.Forms.Padding(8);
            this.label12.Size = new System.Drawing.Size(471, 121);
            this.label12.TabIndex = 4;
            this.label12.Text = "^^Click OK to create your session.\r\n\r\nThis might take a couple of minutes.\r\nSavin" +
    "g your session from the main menu will allow it to be loaded quickly in future.";
            // 
            // _lblOrder
            // 
            this._lblOrder.Dock = System.Windows.Forms.DockStyle.Top;
            this._lblOrder.Location = new System.Drawing.Point(0, 66);
            this._lblOrder.Margin = new System.Windows.Forms.Padding(0);
            this._lblOrder.Name = "_lblOrder";
            this._lblOrder.Size = new System.Drawing.Size(301, 28);
            this._lblOrder.TabIndex = 4;
            this._lblOrder.Text = " ";
            this._lblOrder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ctlTitleBar1
            // 
            this.ctlTitleBar1.AutoSize = true;
            this.ctlTitleBar1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ctlTitleBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlTitleBar1.HelpIcon = CtlTitleBar.EHelpIcon.HideBar;
            this.ctlTitleBar1.HelpText = "";
            this.ctlTitleBar1.Location = new System.Drawing.Point(0, 0);
            this.ctlTitleBar1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ctlTitleBar1.MinimumSize = new System.Drawing.Size(256, 0);
            this.ctlTitleBar1.Name = "ctlTitleBar1";
            this.ctlTitleBar1.Size = new System.Drawing.Size(301, 66);
            this.ctlTitleBar1.SubText = "";
            this.ctlTitleBar1.TabIndex = 2;
            this.ctlTitleBar1.Text = "Help";
            this.ctlTitleBar1.WarningText = null;
            this.ctlTitleBar1.HelpClicked += new System.ComponentModel.CancelEventHandler(this.ctlTitleBar1_HelpClicked);
            // 
            // FrmEditDataFileNames
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.tableLayoutPanel4);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.Name = "FrmEditDataFileNames";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Load Session";
            this._mnuDebug.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this._tabWelcome.ResumeLayout(false);
            this._tabWelcome.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._imgPhotograph)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this._tabSessionName.ResumeLayout(false);
            this._tabSessionName.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this._tabSelectData.ResumeLayout(false);
            this._tabSelectData.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this._tabConditions.ResumeLayout(false);
            this._pnlConditions.ResumeLayout(false);
            this._pnlConditions.PerformLayout();
            this._tabStatistics.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel5.ResumeLayout(false);
            this.flowLayoutPanel5.PerformLayout();
            this._tabCompounds.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            this._tabAnnotations.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._numTolerance)).EndInit();
            this._tabReady.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MGui.Controls.CtlTextBox _txtDataSetData;
        private CtlButton _btnDataSetData;
        private MGui.Controls.CtlTextBox _txtDataSetObs;
        private MGui.Controls.CtlTextBox _txtDataSetVar;
        private CtlButton _btnDataSetObs;
        private CtlButton _btnDataSetVar;
        private CtlLabel _lblDataSetData;
        private CtlLabel _lblDataSetObs;
        private CtlLabel _lblDataSetVar;
        private System.Windows.Forms.ComboBox _lstLcmsMode;
        private CtlLabel _lblLcmsMode;
        private System.Windows.Forms.ContextMenuStrip _cmsRecentWorkspaces;
        private MGui.Controls.CtlTextBox _txtTitle;
        private System.Windows.Forms.CheckBox _chkAltVals;
        private System.Windows.Forms.CheckBox _chkCondInfo;
        private MGui.Controls.CtlTextBox _txtAltVals;
        private MGui.Controls.CtlTextBox _txtCondInfo;
        private CtlButton _btnAltVals;
        private CtlButton _btnCondInfo;
        private CtlLabel _lblConditions;
        private MGui.Controls.CtlTextBox _txtExps;
        private CtlLabel label3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage _tabSelectData;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private CtlLabel label5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TabPage _tabSessionName;
        private System.Windows.Forms.TabPage _tabCompounds;
        private System.Windows.Forms.ToolTip _tipSideBar;
        private MGui.Controls.CtlSplitter splitContainer1;
        private MGui.Controls.CtlTextBox _txtHelp;
        private System.Windows.Forms.TabPage _tabWelcome;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Controls.CtlButton _btnNewSession;
        private Controls.CtlButton _btnReturnToSession;
        private CtlLabel label9;
        private CtlLabel label10;
        private System.Windows.Forms.ContextMenuStrip _mnuDebug;
        private System.Windows.Forms.ToolStripMenuItem exploreToolStripMenuItem;
        private CtlLabel label7;
        private System.Windows.Forms.ToolStripMenuItem clearRPathrequiresRestartToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip _cmsRecentSessions;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Controls.CtlButton _btnMostRecent;
        private System.Windows.Forms.PictureBox _imgPhotograph;
        private CtlButton _btnBrowseExpCond;
        private System.Windows.Forms.TabPage _tabStatistics;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private CtlLabel label14;
        private System.Windows.Forms.ToolTip _tipPopup;
        private CtlLabel label4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.CheckBox _chkAutoTTest;
        private System.Windows.Forms.CheckBox _chkAutoPearson;
        private System.Windows.Forms.TabPage _tabReady;
        private CtlLabel label12;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.ToolStripMenuItem restartToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel _pnlConditions;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.CheckBox _chkAlarm;
        private CtlLabel label2;
        private CtlLabel label6;
        private System.Windows.Forms.ListBox _lstCompounds;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private Controls.CtlButton _btnAddCompound;
        private Controls.CtlButton _btnRemoveLibrary;
        private Controls.CtlButton _btnBrowseAdducts;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.ListBox _lstAvailCompounds;
        private Controls.CtlButton _btnAddCompoundLibrary;
        private System.Windows.Forms.ListBox _lstAdducts;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private Controls.CtlButton _btnAddAdduct;
        private Controls.CtlButton _btnDelAdduct;
        private System.Windows.Forms.ListBox _lstAvailableAdducts;
        private CtlLabel _lblAdducts;
        private System.Windows.Forms.TabPage _tabAnnotations;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private CtlLabel label11;
        private Controls.CtlButton _btnIdentifications;
        private System.Windows.Forms.CheckBox _chkAutoIdentify;
        private MGui.Controls.CtlTextBox _txtIdentifications;
        private System.Windows.Forms.CheckBox _chkIdentifications;
        private Controls.CtlButton _btnReconfigure;
        private System.Windows.Forms.ToolStripMenuItem editPathsAndLibrariesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private Controls.CtlButton _btnAddAllCompounds;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private CtlLabel label15;
        private System.Windows.Forms.Label _lblProgramDescription;
        private System.Windows.Forms.Label _lbl32BitWarning;
        private CtlLabel _lblTolerance;
        private System.Windows.Forms.NumericUpDown _numTolerance;
        private System.Windows.Forms.ComboBox _lstTolerance;
        private MGui.Controls.CtlError _checker;
        private Controls.CtlTitleBar ctlTitleBar1;
        private CtlLabel _lblOrder;
        private System.Windows.Forms.Button _btnShowFf;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cSVManipulatorToolStripMenuItem;
        private System.Windows.Forms.CheckBox _chkPeakPeakMatch;
        private Controls.CtlLabel ctlLabel1;
        private System.Windows.Forms.ComboBox _automaticFlag;
        private System.Windows.Forms.ComboBox _manualFlag;
        private Controls.CtlLabel ctlLabel3;
        private Controls.CtlLabel ctlLabel2;
        private Controls.CtlLabel _lblTitle;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.RadioButton _radEmptyWorkspace;
        private System.Windows.Forms.RadioButton _radRecentWorkspace;
        private Controls.CtlLabel ctlLabel6;
        private Controls.CtlButton _btnDeleteWorkspace;
        private Controls.CtlLabel ctlLabel7;
        private Controls.CtlButton _btnRecentWorkspace;
        private System.Windows.Forms.TextBox _txtPreviousConfig;
        private System.Windows.Forms.TabPage _tabConditions;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.CheckBox _chkAutoMeanTrend;
        private System.Windows.Forms.CheckBox _chkAutoMedianTrend;
        private Controls.CtlLabel ctlLabel4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.CheckBox _chkAutoUvSC;
        private System.Windows.Forms.CheckBox _chkConditions;
        private Controls.CtlButton _btnBrowseContCond;
        private MGui.Controls.CtlTextBox _txtControls;
        private System.Windows.Forms.Label _lblTTUnavail;
        private System.Windows.Forms.Label _lblPearsonUnavail;
        private System.Windows.Forms.Label _lblPeakPeakMatchUnavail;
        private Controls.CtlLabel ctlLabel5;
        private System.Windows.Forms.Label _lblMzMatchUnavail;
    }
}