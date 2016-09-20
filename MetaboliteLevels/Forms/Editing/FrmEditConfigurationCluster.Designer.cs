using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaboliteLevels.Forms.Editing
{
    partial class FrmEditConfigurationCluster
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEditConfigurationCluster));
            this.newMetricToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._lblApply = new System.Windows.Forms.Label();
            this.newStatisticToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._lblAVec = new System.Windows.Forms.Label();
            this._btnNewStatistic = new MetaboliteLevels.Controls.CtlButton();
            this._btnComment = new MetaboliteLevels.Controls.CtlButton();
            this._btnObsFilter = new MetaboliteLevels.Controls.CtlButton();
            this.label1 = new System.Windows.Forms.Label();
            this._lstMethod = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this._txtName = new MGui.Controls.CtlTextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this._lstMeasure = new System.Windows.Forms.ComboBox();
            this._btnSource = new MetaboliteLevels.Controls.CtlButton();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this._btnParameterOptimiser = new MetaboliteLevels.Controls.CtlButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._btnCancel = new MetaboliteLevels.Controls.CtlButton();
            this._btnOk = new MetaboliteLevels.Controls.CtlButton();
            this._btnEditParameters = new MetaboliteLevels.Controls.CtlButton();
            this._txtMeasureParams = new MGui.Controls.CtlTextBox();
            this._chkSepGroups = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this._txtParams = new MGui.Controls.CtlTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this._lblPeaks = new System.Windows.Forms.Label();
            this._btnPeakFilter = new MetaboliteLevels.Controls.CtlButton();
            this.label3 = new System.Windows.Forms.Label();
            this._lstObsFilter = new System.Windows.Forms.ComboBox();
            this._lstPeakFilter = new System.Windows.Forms.ComboBox();
            this._btnNewDistance = new MetaboliteLevels.Controls.CtlButton();
            this._btnEditDistanceParameters = new MetaboliteLevels.Controls.CtlButton();
            this._lblMeasure2 = new System.Windows.Forms.Label();
            this._lblMeasureParams = new System.Windows.Forms.Label();
            this._lblParams = new System.Windows.Forms.Label();
            this._txtStatistics = new MGui.Controls.CtlTextBox();
            this._btnSetStatistics = new MetaboliteLevels.Controls.CtlButton();
            this.label2 = new System.Windows.Forms.Label();
            this._btnExperimentalGroups = new MetaboliteLevels.Controls.CtlButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ctlTitleBar1 = new MetaboliteLevels.Controls.CtlTitleBar();
            this._checker = new MGui.Controls.CtlError(this.components);
            this._lstSource = new System.Windows.Forms.ComboBox();
            this.contextMenuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // newMetricToolStripMenuItem
            // 
            this.newMetricToolStripMenuItem.Name = "newMetricToolStripMenuItem";
            this.newMetricToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.newMetricToolStripMenuItem.Text = "&New metric...";
            // 
            // _lblApply
            // 
            this._lblApply.AutoSize = true;
            this._lblApply.Location = new System.Drawing.Point(8, 397);
            this._lblApply.Margin = new System.Windows.Forms.Padding(8);
            this._lblApply.Name = "_lblApply";
            this._lblApply.Size = new System.Drawing.Size(58, 21);
            this._lblApply.TabIndex = 2;
            this._lblApply.Text = "Source";
            this.toolTip1.SetToolTip(this._lblApply, "Select the source for the clustering vectors - either all observations or just th" +
        "e trendline.\r\nUse the buttons to the right to view the data source.");
            // 
            // newStatisticToolStripMenuItem
            // 
            this.newStatisticToolStripMenuItem.Name = "newStatisticToolStripMenuItem";
            this.newStatisticToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.newStatisticToolStripMenuItem.Text = "&New statistic...";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newStatisticToolStripMenuItem,
            this.newMetricToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(151, 48);
            // 
            // _lblAVec
            // 
            this._lblAVec.AutoSize = true;
            this._lblAVec.Location = new System.Drawing.Point(8, 479);
            this._lblAVec.Margin = new System.Windows.Forms.Padding(8);
            this._lblAVec.Name = "_lblAVec";
            this._lblAVec.Size = new System.Drawing.Size(102, 21);
            this._lblAVec.TabIndex = 4;
            this._lblAVec.Text = "Observations";
            this.toolTip1.SetToolTip(this._lblAVec, "Select the set of observations (or conditions for a trend) to enter into the clus" +
        "tering vectors.\r\nUse the button to the right to add or edit the sets of observat" +
        "ions.");
            // 
            // _btnNewStatistic
            // 
            this._btnNewStatistic.Image = global::MetaboliteLevels.Properties.Resources.MnuViewList;
            this._btnNewStatistic.Location = new System.Drawing.Point(931, 53);
            this._btnNewStatistic.Margin = new System.Windows.Forms.Padding(8);
            this._btnNewStatistic.Name = "_btnNewStatistic";
            this._btnNewStatistic.Size = new System.Drawing.Size(29, 29);
            this._btnNewStatistic.TabIndex = 16;
            this.toolTip1.SetToolTip(this._btnNewStatistic, "Select the clustering algorithm.\r\nUse the button to the right to open the databas" +
        "e and add new algorithms.");
            this._btnNewStatistic.UseDefaultSize = true;
            this._btnNewStatistic.UseVisualStyleBackColor = true;
            // 
            // _btnComment
            // 
            this._btnComment.Image = global::MetaboliteLevels.Properties.Resources.MnuComment;
            this._btnComment.Location = new System.Drawing.Point(931, 8);
            this._btnComment.Margin = new System.Windows.Forms.Padding(8);
            this._btnComment.Name = "_btnComment";
            this._btnComment.Size = new System.Drawing.Size(29, 29);
            this._btnComment.TabIndex = 16;
            this.toolTip1.SetToolTip(this._btnComment, "Enter the title of the algorithm (this will also be the title of the resultant cl" +
        "usters).\r\nIf you don\'t enter a title one will be chosen for you.\r\nUse the button" +
        " to the right to add comments.");
            this._btnComment.UseDefaultSize = true;
            this._btnComment.UseVisualStyleBackColor = true;
            this._btnComment.Click += new System.EventHandler(this._btnComment_Click);
            // 
            // _btnObsFilter
            // 
            this._btnObsFilter.Image = global::MetaboliteLevels.Properties.Resources.MnuViewList;
            this._btnObsFilter.Location = new System.Drawing.Point(931, 479);
            this._btnObsFilter.Margin = new System.Windows.Forms.Padding(8);
            this._btnObsFilter.Name = "_btnObsFilter";
            this._btnObsFilter.Size = new System.Drawing.Size(29, 29);
            this._btnObsFilter.TabIndex = 14;
            this.toolTip1.SetToolTip(this._btnObsFilter, "Select the set of observations (or conditions for a trend) to enter into the clus" +
        "tering vectors.\r\nUse the button to the right to add or edit the sets of observat" +
        "ions.");
            this._btnObsFilter.UseDefaultSize = true;
            this._btnObsFilter.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 53);
            this.label1.Margin = new System.Windows.Forms.Padding(8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Method";
            this.toolTip1.SetToolTip(this.label1, "Select the clustering algorithm.\r\nUse the button to the right to open the databas" +
        "e and add new algorithms.");
            // 
            // _lstMethod
            // 
            this._lstMethod.Dock = System.Windows.Forms.DockStyle.Top;
            this._lstMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstMethod.FormattingEnabled = true;
            this._lstMethod.Location = new System.Drawing.Point(126, 53);
            this._lstMethod.Margin = new System.Windows.Forms.Padding(8);
            this._lstMethod.Name = "_lstMethod";
            this._lstMethod.Size = new System.Drawing.Size(789, 29);
            this._lstMethod.TabIndex = 1;
            this.toolTip1.SetToolTip(this._lstMethod, "Select the clustering algorithm.\r\nUse the button to the right to open the databas" +
        "e and add new algorithms.");
            this._lstMethod.SelectedIndexChanged += new System.EventHandler(this.CheckAndChange);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 8);
            this.label4.Margin = new System.Windows.Forms.Padding(8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 21);
            this.label4.TabIndex = 0;
            this.label4.Text = "Title";
            this.toolTip1.SetToolTip(this.label4, "Enter the title of the algorithm (this will also be the title of the resultant cl" +
        "usters).\r\nIf you don\'t enter a title one will be chosen for you.\r\nUse the button" +
        " to the right to add comments.");
            // 
            // _txtName
            // 
            this._txtName.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtName.ForeColor = System.Drawing.Color.Blue;
            this._txtName.Location = new System.Drawing.Point(126, 8);
            this._txtName.Margin = new System.Windows.Forms.Padding(8);
            this._txtName.Name = "_txtName";
            this._txtName.Size = new System.Drawing.Size(789, 29);
            this._txtName.TabIndex = 6;
            this.toolTip1.SetToolTip(this._txtName, "Enter the title of the algorithm (this will also be the title of the resultant cl" +
        "usters).\r\nIf you don\'t enter a title one will be chosen for you.\r\nUse the button" +
        " to the right to add comments.");
            this._txtName.Watermark = "Default";
            this._txtName.TextChanged += new System.EventHandler(this.Check);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this._btnSource, 2, 9);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 13);
            this.tableLayoutPanel1.Controls.Add(this._btnEditParameters, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this._txtMeasureParams, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this._chkSepGroups, 1, 12);
            this.tableLayoutPanel1.Controls.Add(this._btnNewStatistic, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this._lblAVec, 0, 11);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this._lstMethod, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._txtName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this._btnComment, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this._btnObsFilter, 2, 11);
            this.tableLayoutPanel1.Controls.Add(this.label15, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this._txtParams, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this._lblPeaks, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this._btnPeakFilter, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this._lblApply, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this._lstObsFilter, 1, 11);
            this.tableLayoutPanel1.Controls.Add(this._lstPeakFilter, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this._btnNewDistance, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this._btnEditDistanceParameters, 2, 6);
            this.tableLayoutPanel1.Controls.Add(this._lblMeasure2, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this._lblMeasureParams, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this._lblParams, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this._txtStatistics, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this._btnSetStatistics, 2, 7);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this._btnExperimentalGroups, 2, 12);
            this.tableLayoutPanel1.Controls.Add(this._lstSource, 1, 9);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 87);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 14;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(968, 802);
            this.tableLayoutPanel1.TabIndex = 14;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.linkLabel1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this._lstMeasure, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(118, 217);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(805, 45);
            this.tableLayoutPanel2.TabIndex = 15;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.linkLabel1.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.Image = global::MetaboliteLevels.Properties.Resources.MnuWarning;
            this.linkLabel1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel1.LinkArea = new System.Windows.Forms.LinkArea(39, 4);
            this.linkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabel1.Location = new System.Drawing.Point(544, 10);
            this.linkLabel1.Margin = new System.Windows.Forms.Padding(8, 10, 8, 8);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(253, 20);
            this.linkLabel1.TabIndex = 21;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "       Distance metric not used, click here for details.";
            this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.linkLabel1, "Select the distance metric algorithm.\r\nUse the button to the right to add or edit" +
        " the distance metric algorithms.");
            this.linkLabel1.UseCompatibleTextRendering = true;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // _lstMeasure
            // 
            this._lstMeasure.Dock = System.Windows.Forms.DockStyle.Top;
            this._lstMeasure.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstMeasure.FormattingEnabled = true;
            this._lstMeasure.Location = new System.Drawing.Point(8, 8);
            this._lstMeasure.Margin = new System.Windows.Forms.Padding(8);
            this._lstMeasure.Name = "_lstMeasure";
            this._lstMeasure.Size = new System.Drawing.Size(520, 29);
            this._lstMeasure.TabIndex = 1;
            this.toolTip1.SetToolTip(this._lstMeasure, "Select the distance metric algorithm.\r\nUse the button to the right to add or edit" +
        " the distance metric algorithms.");
            this._lstMeasure.SelectedIndexChanged += new System.EventHandler(this.CheckAndChange);
            // 
            // _btnSource
            // 
            this._btnSource.Image = global::MetaboliteLevels.Properties.Resources.MnuViewList;
            this._btnSource.Location = new System.Drawing.Point(931, 397);
            this._btnSource.Margin = new System.Windows.Forms.Padding(8);
            this._btnSource.Name = "_btnSource";
            this._btnSource.Size = new System.Drawing.Size(29, 29);
            this._btnSource.TabIndex = 33;
            this.toolTip1.SetToolTip(this._btnSource, "Select the source for the clustering vectors - either all observations or just th" +
        "e trendline.\r\nUse the buttons to the right to view the data source.");
            this._btnSource.UseDefaultSize = true;
            this._btnSource.UseVisualStyleBackColor = true;
            this._btnSource.Click += new System.EventHandler(this._btnObs_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel3, 3);
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this._btnParameterOptimiser, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.flowLayoutPanel1, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 565);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(968, 237);
            this.tableLayoutPanel3.TabIndex = 16;
            // 
            // _btnParameterOptimiser
            // 
            this._btnParameterOptimiser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._btnParameterOptimiser.AutoSize = true;
            this._btnParameterOptimiser.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._btnParameterOptimiser.ForeColor = System.Drawing.Color.Black;
            this._btnParameterOptimiser.Image = ((System.Drawing.Image)(resources.GetObject("_btnParameterOptimiser.Image")));
            this._btnParameterOptimiser.Location = new System.Drawing.Point(8, 190);
            this._btnParameterOptimiser.Margin = new System.Windows.Forms.Padding(8);
            this._btnParameterOptimiser.Name = "_btnParameterOptimiser";
            this._btnParameterOptimiser.Size = new System.Drawing.Size(203, 39);
            this._btnParameterOptimiser.TabIndex = 3;
            this._btnParameterOptimiser.Text = "Parameter optimiser...";
            this.toolTip1.SetToolTip(this._btnParameterOptimiser, "Click to test the effects of multiple parameters on your clustering algorithm.");
            this._btnParameterOptimiser.UseVisualStyleBackColor = false;
            this._btnParameterOptimiser.Click += new System.EventHandler(this.ctlButton1_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this._btnCancel);
            this.flowLayoutPanel1.Controls.Add(this._btnOk);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(692, 185);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(272, 48);
            this.flowLayoutPanel1.TabIndex = 13;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // _btnCancel
            // 
            this._btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnCancel.Image = global::MetaboliteLevels.Properties.Resources.MnuCancel;
            this._btnCancel.Location = new System.Drawing.Point(140, 4);
            this._btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(128, 40);
            this._btnCancel.TabIndex = 19;
            this._btnCancel.Text = "  Cancel";
            this.toolTip1.SetToolTip(this._btnCancel, "Go back, discarding any changes you have made.");
            this._btnCancel.UseDefaultSize = true;
            this._btnCancel.UseVisualStyleBackColor = true;
            // 
            // _btnOk
            // 
            this._btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._btnOk.Enabled = false;
            this._btnOk.Image = global::MetaboliteLevels.Properties.Resources.MnuAccept;
            this._btnOk.Location = new System.Drawing.Point(4, 4);
            this._btnOk.Margin = new System.Windows.Forms.Padding(4);
            this._btnOk.Name = "_btnOk";
            this._btnOk.Size = new System.Drawing.Size(128, 40);
            this._btnOk.TabIndex = 18;
            this._btnOk.Text = "  OK";
            this.toolTip1.SetToolTip(this._btnOk, "Click to apply your changes.\r\nYou will have a chance to make changes before your " +
        "algorithhm is run.");
            this._btnOk.UseDefaultSize = true;
            this._btnOk.UseVisualStyleBackColor = true;
            // 
            // _btnEditParameters
            // 
            this._btnEditParameters.Image = global::MetaboliteLevels.Properties.Resources.MnuEnlargeList;
            this._btnEditParameters.Location = new System.Drawing.Point(931, 98);
            this._btnEditParameters.Margin = new System.Windows.Forms.Padding(8);
            this._btnEditParameters.Name = "_btnEditParameters";
            this._btnEditParameters.Size = new System.Drawing.Size(29, 29);
            this._btnEditParameters.TabIndex = 16;
            this.toolTip1.SetToolTip(this._btnEditParameters, "Select the parameters (if required) for the clustering algorithm.\r\nUse the button" +
        " to the right to open up the parameter editor.");
            this._btnEditParameters.UseDefaultSize = true;
            this._btnEditParameters.UseVisualStyleBackColor = true;
            this._btnEditParameters.Click += new System.EventHandler(this._btnEditParameters_Click);
            // 
            // _txtMeasureParams
            // 
            this._txtMeasureParams.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtMeasureParams.ForeColor = System.Drawing.Color.Blue;
            this._txtMeasureParams.Location = new System.Drawing.Point(126, 270);
            this._txtMeasureParams.Margin = new System.Windows.Forms.Padding(8);
            this._txtMeasureParams.Name = "_txtMeasureParams";
            this._txtMeasureParams.Size = new System.Drawing.Size(789, 29);
            this._txtMeasureParams.TabIndex = 6;
            this.toolTip1.SetToolTip(this._txtMeasureParams, "Select the parameters for the distance metric (if required).\r\nSeparate your param" +
        "eters with commas.\r\nUse the button to the right to open the parameter editor.");
            this._txtMeasureParams.Watermark = null;
            this._txtMeasureParams.TextChanged += new System.EventHandler(this.Check);
            // 
            // _chkSepGroups
            // 
            this._chkSepGroups.AutoSize = true;
            this._chkSepGroups.Location = new System.Drawing.Point(126, 524);
            this._chkSepGroups.Margin = new System.Windows.Forms.Padding(8);
            this._chkSepGroups.Name = "_chkSepGroups";
            this._chkSepGroups.Padding = new System.Windows.Forms.Padding(4);
            this._chkSepGroups.Size = new System.Drawing.Size(280, 33);
            this._chkSepGroups.TabIndex = 15;
            this._chkSepGroups.Text = "One vector per experimental group";
            this.toolTip1.SetToolTip(this._chkSepGroups, "Select this option to generate one clustering vector per experimental group, per " +
        "peak.\r\nIf not selected one vector will be created per peak, comprising all exper" +
        "imental groups.");
            this._chkSepGroups.UseVisualStyleBackColor = false;
            this._chkSepGroups.CheckedChanged += new System.EventHandler(this.CheckAndChange);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(8, 442);
            this.label15.Margin = new System.Windows.Forms.Padding(8);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(14, 21);
            this.label15.TabIndex = 4;
            this.label15.Text = " ";
            // 
            // _txtParams
            // 
            this._txtParams.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtParams.ForeColor = System.Drawing.Color.Blue;
            this._txtParams.Location = new System.Drawing.Point(126, 98);
            this._txtParams.Margin = new System.Windows.Forms.Padding(8);
            this._txtParams.Name = "_txtParams";
            this._txtParams.Size = new System.Drawing.Size(789, 29);
            this._txtParams.TabIndex = 6;
            this.toolTip1.SetToolTip(this._txtParams, "Select the parameters (if required) for the clustering algorithm.\r\nSeparate your " +
        "parameters with commas.\r\nUse the button to the right to open up the parameter ed" +
        "itor.");
            this._txtParams.Watermark = null;
            this._txtParams.TextChanged += new System.EventHandler(this.CheckAndChange);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 143);
            this.label7.Margin = new System.Windows.Forms.Padding(8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 21);
            this.label7.TabIndex = 4;
            this.label7.Text = " ";
            // 
            // _lblPeaks
            // 
            this._lblPeaks.AutoSize = true;
            this._lblPeaks.Location = new System.Drawing.Point(8, 180);
            this._lblPeaks.Margin = new System.Windows.Forms.Padding(8);
            this._lblPeaks.Name = "_lblPeaks";
            this._lblPeaks.Size = new System.Drawing.Size(50, 21);
            this._lblPeaks.TabIndex = 2;
            this._lblPeaks.Text = "Peaks";
            this.toolTip1.SetToolTip(this._lblPeaks, "Select the filter to generate the set of peaks you\'d like to cluster.\r\nSelect the" +
        " button to the right to add or edit peak filters.");
            // 
            // _btnPeakFilter
            // 
            this._btnPeakFilter.Image = global::MetaboliteLevels.Properties.Resources.MnuViewList;
            this._btnPeakFilter.Location = new System.Drawing.Point(931, 180);
            this._btnPeakFilter.Margin = new System.Windows.Forms.Padding(8);
            this._btnPeakFilter.Name = "_btnPeakFilter";
            this._btnPeakFilter.Size = new System.Drawing.Size(29, 29);
            this._btnPeakFilter.TabIndex = 16;
            this.toolTip1.SetToolTip(this._btnPeakFilter, "Select the filter to generate the set of peaks you\'d like to cluster.\r\nSelect the" +
        " button to the right to add or edit peak filters.");
            this._btnPeakFilter.UseDefaultSize = true;
            this._btnPeakFilter.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 360);
            this.label3.Margin = new System.Windows.Forms.Padding(8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 21);
            this.label3.TabIndex = 4;
            this.label3.Text = " ";
            // 
            // _lstObsFilter
            // 
            this._lstObsFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstObsFilter.FormattingEnabled = true;
            this._lstObsFilter.Location = new System.Drawing.Point(126, 479);
            this._lstObsFilter.Margin = new System.Windows.Forms.Padding(8);
            this._lstObsFilter.Name = "_lstObsFilter";
            this._lstObsFilter.Size = new System.Drawing.Size(786, 29);
            this._lstObsFilter.TabIndex = 1;
            this.toolTip1.SetToolTip(this._lstObsFilter, "Select the set of observations (or conditions for a trend) to enter into the clus" +
        "tering vectors.\r\nUse the button to the right to add or edit the sets of observat" +
        "ions.");
            this._lstObsFilter.SelectedIndexChanged += new System.EventHandler(this.CheckAndChange);
            // 
            // _lstPeakFilter
            // 
            this._lstPeakFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this._lstPeakFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstPeakFilter.FormattingEnabled = true;
            this._lstPeakFilter.Location = new System.Drawing.Point(126, 180);
            this._lstPeakFilter.Margin = new System.Windows.Forms.Padding(8);
            this._lstPeakFilter.Name = "_lstPeakFilter";
            this._lstPeakFilter.Size = new System.Drawing.Size(789, 29);
            this._lstPeakFilter.TabIndex = 1;
            this.toolTip1.SetToolTip(this._lstPeakFilter, "Select the filter to generate the set of peaks you\'d like to cluster.\r\nSelect the" +
        " button to the right to add or edit peak filters.");
            this._lstPeakFilter.SelectedIndexChanged += new System.EventHandler(this.CheckAndChange);
            // 
            // _btnNewDistance
            // 
            this._btnNewDistance.Image = global::MetaboliteLevels.Properties.Resources.MnuViewList;
            this._btnNewDistance.Location = new System.Drawing.Point(931, 225);
            this._btnNewDistance.Margin = new System.Windows.Forms.Padding(8);
            this._btnNewDistance.Name = "_btnNewDistance";
            this._btnNewDistance.Size = new System.Drawing.Size(29, 29);
            this._btnNewDistance.TabIndex = 16;
            this.toolTip1.SetToolTip(this._btnNewDistance, "Select the distance metric algorithm.\r\nUse the button to the right to add or edit" +
        " the distance metric algorithms.");
            this._btnNewDistance.UseDefaultSize = true;
            this._btnNewDistance.UseVisualStyleBackColor = true;
            // 
            // _btnEditDistanceParameters
            // 
            this._btnEditDistanceParameters.Image = global::MetaboliteLevels.Properties.Resources.MnuEnlargeList;
            this._btnEditDistanceParameters.Location = new System.Drawing.Point(931, 270);
            this._btnEditDistanceParameters.Margin = new System.Windows.Forms.Padding(8);
            this._btnEditDistanceParameters.Name = "_btnEditDistanceParameters";
            this._btnEditDistanceParameters.Size = new System.Drawing.Size(29, 29);
            this._btnEditDistanceParameters.TabIndex = 16;
            this.toolTip1.SetToolTip(this._btnEditDistanceParameters, "Select the parameters for the distance metric (if required).\r\nSeparate your param" +
        "eters with commas.\r\nUse the button to the right to open the parameter editor.");
            this._btnEditDistanceParameters.UseDefaultSize = true;
            this._btnEditDistanceParameters.UseVisualStyleBackColor = true;
            this._btnEditDistanceParameters.Click += new System.EventHandler(this._btnEditDistanceParameters_Click);
            // 
            // _lblMeasure2
            // 
            this._lblMeasure2.AutoSize = true;
            this._lblMeasure2.Location = new System.Drawing.Point(8, 225);
            this._lblMeasure2.Margin = new System.Windows.Forms.Padding(8);
            this._lblMeasure2.Name = "_lblMeasure2";
            this._lblMeasure2.Size = new System.Drawing.Size(69, 21);
            this._lblMeasure2.TabIndex = 2;
            this._lblMeasure2.Text = "Distance";
            this.toolTip1.SetToolTip(this._lblMeasure2, "Select the distance metric algorithm.\r\nUse the button to the right to add or edit" +
        " the distance metric algorithms.");
            // 
            // _lblMeasureParams
            // 
            this._lblMeasureParams.AutoSize = true;
            this._lblMeasureParams.Location = new System.Drawing.Point(8, 270);
            this._lblMeasureParams.Margin = new System.Windows.Forms.Padding(8);
            this._lblMeasureParams.Name = "_lblMeasureParams";
            this._lblMeasureParams.Size = new System.Drawing.Size(83, 21);
            this._lblMeasureParams.TabIndex = 0;
            this._lblMeasureParams.Text = "Where k =";
            this.toolTip1.SetToolTip(this._lblMeasureParams, "Select the parameters for the distance metric (if required).\r\nSeparate your param" +
        "eters with commas.\r\nUse the button to the right to open the parameter editor.");
            // 
            // _lblParams
            // 
            this._lblParams.AutoSize = true;
            this._lblParams.Location = new System.Drawing.Point(8, 98);
            this._lblParams.Margin = new System.Windows.Forms.Padding(8);
            this._lblParams.Name = "_lblParams";
            this._lblParams.Size = new System.Drawing.Size(83, 21);
            this._lblParams.TabIndex = 0;
            this._lblParams.Text = "Where k =";
            this.toolTip1.SetToolTip(this._lblParams, "Select the parameters (if required) for the clustering algorithm.\r\nUse the button" +
        " to the right to open up the parameter editor.");
            // 
            // _txtStatistics
            // 
            this._txtStatistics.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtStatistics.ForeColor = System.Drawing.Color.Blue;
            this._txtStatistics.Location = new System.Drawing.Point(126, 315);
            this._txtStatistics.Margin = new System.Windows.Forms.Padding(8);
            this._txtStatistics.Name = "_txtStatistics";
            this._txtStatistics.Size = new System.Drawing.Size(789, 29);
            this._txtStatistics.TabIndex = 6;
            this.toolTip1.SetToolTip(this._txtStatistics, "Select the set of statistics to generate on the clusters.\r\nEnter the values as a " +
        "comma delimited list.\r\nUse the button to the right to show the list editor.");
            this._txtStatistics.Watermark = null;
            this._txtStatistics.TextChanged += new System.EventHandler(this.Check);
            // 
            // _btnSetStatistics
            // 
            this._btnSetStatistics.Image = global::MetaboliteLevels.Properties.Resources.MnuEnlargeList;
            this._btnSetStatistics.Location = new System.Drawing.Point(931, 315);
            this._btnSetStatistics.Margin = new System.Windows.Forms.Padding(8);
            this._btnSetStatistics.Name = "_btnSetStatistics";
            this._btnSetStatistics.Size = new System.Drawing.Size(29, 29);
            this._btnSetStatistics.TabIndex = 16;
            this.toolTip1.SetToolTip(this._btnSetStatistics, "Select the set of statistics to generate on the clusters.\r\nEnter the values as a " +
        "comma delimited list.\r\nUse the button to the right to show the list editor.");
            this._btnSetStatistics.UseDefaultSize = true;
            this._btnSetStatistics.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 315);
            this.label2.Margin = new System.Windows.Forms.Padding(8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "Statistics";
            this.toolTip1.SetToolTip(this.label2, "Select the set of statistics to generate on the clusters.\r\nEnter the values as a " +
        "comma delimited list.\r\nUse the button to the right to show the list editor.");
            // 
            // _btnExperimentalGroups
            // 
            this._btnExperimentalGroups.Image = global::MetaboliteLevels.Properties.Resources.MnuViewList;
            this._btnExperimentalGroups.Location = new System.Drawing.Point(931, 524);
            this._btnExperimentalGroups.Margin = new System.Windows.Forms.Padding(8);
            this._btnExperimentalGroups.Name = "_btnExperimentalGroups";
            this._btnExperimentalGroups.Size = new System.Drawing.Size(29, 29);
            this._btnExperimentalGroups.TabIndex = 34;
            this._btnExperimentalGroups.UseDefaultSize = true;
            this._btnExperimentalGroups.UseVisualStyleBackColor = true;
            this._btnExperimentalGroups.Click += new System.EventHandler(this._btnExperimentalGroups_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // ctlTitleBar1
            // 
            this.ctlTitleBar1.AutoSize = true;
            this.ctlTitleBar1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ctlTitleBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlTitleBar1.HelpText = "Select the method and parameters for your clustering algorithm.\r\n\r\nYou can test v" +
    "arious parameter settings by using the button at the bottom of the window.";
            this.ctlTitleBar1.Location = new System.Drawing.Point(0, 0);
            this.ctlTitleBar1.Margin = new System.Windows.Forms.Padding(9, 13, 9, 13);
            this.ctlTitleBar1.MinimumSize = new System.Drawing.Size(864, 0);
            this.ctlTitleBar1.Name = "ctlTitleBar1";
            this.ctlTitleBar1.Size = new System.Drawing.Size(968, 87);
            this.ctlTitleBar1.SubText = "Select the options for your clustering algorithm";
            this.ctlTitleBar1.TabIndex = 13;
            this.ctlTitleBar1.Text = "Text goes here";
            this.toolTip1.SetToolTip(this.ctlTitleBar1, "Click the ? button for more help.");
            this.ctlTitleBar1.WarningText = null;
            // 
            // _lstSource
            // 
            this._lstSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstSource.FormattingEnabled = true;
            this._lstSource.Location = new System.Drawing.Point(126, 397);
            this._lstSource.Margin = new System.Windows.Forms.Padding(8);
            this._lstSource.Name = "_lstSource";
            this._lstSource.Size = new System.Drawing.Size(786, 29);
            this._lstSource.TabIndex = 1;
            this._lstSource.SelectedIndexChanged += new System.EventHandler(this.CheckAndChange);
            // 
            // FrmEditConfigurationCluster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 889);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.ctlTitleBar1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmEditConfigurationCluster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Clustering";
            this.contextMenuStrip1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.CtlTitleBar ctlTitleBar1;
        private System.Windows.Forms.ToolStripMenuItem newMetricToolStripMenuItem;
        private System.Windows.Forms.Label _lblApply;
        private System.Windows.Forms.ToolStripMenuItem newStatisticToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label _lblAVec;
        private Controls.CtlButton _btnNewStatistic;
        private Controls.CtlButton _btnComment;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox _lstMethod;
        private System.Windows.Forms.Label label4;
        private MGui.Controls.CtlTextBox _txtName;
        private Controls.CtlButton _btnObsFilter;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label _lblParams;
        private MGui.Controls.CtlTextBox _txtParams;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label _lblPeaks;
        private Controls.CtlButton _btnPeakFilter;
        private System.Windows.Forms.Label _lblMeasure2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox _lstMeasure;
        private System.Windows.Forms.Label _lblMeasureParams;
        private MGui.Controls.CtlTextBox _txtMeasureParams;
        private System.Windows.Forms.ComboBox _lstObsFilter;
        private System.Windows.Forms.ComboBox _lstPeakFilter;
        private System.Windows.Forms.CheckBox _chkSepGroups;
        private Controls.CtlButton _btnEditParameters;
        private Controls.CtlButton _btnEditDistanceParameters;
        private Controls.CtlButton _btnNewDistance;
        private System.Windows.Forms.ToolTip toolTip1;
        private Controls.CtlButton _btnParameterOptimiser;
        private MGui.Controls.CtlTextBox _txtStatistics;
        private Controls.CtlButton _btnSetStatistics;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Controls.CtlButton _btnCancel;
        private Controls.CtlButton _btnOk;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private MGui.Controls.CtlError _checker;
        private Controls.CtlButton _btnSource;
        private Controls.CtlButton _btnExperimentalGroups;
        private System.Windows.Forms.ComboBox _lstSource;
    }
}