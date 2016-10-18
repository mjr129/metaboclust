using MetaboliteLevels.Data.Session.Singular;

namespace MetaboliteLevels.Forms.Editing
{
    partial class FrmEditCoreOptions
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
            this.label1 = new System.Windows.Forms.Label();
            this._numSizeLimit = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this._chkClusterCentres = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._numClusterMaxPlot = new System.Windows.Forms.NumericUpDown();
            this._chkPeakRanges = new System.Windows.Forms.CheckBox();
            this._chkPeakMean = new System.Windows.Forms.CheckBox();
            this._chkPeakData = new System.Windows.Forms.CheckBox();
            this._chkPeakTrend = new System.Windows.Forms.CheckBox();
            this._chkPeakFlag = new System.Windows.Forms.CheckBox();
            this._btnEditFlags = new MetaboliteLevels.Controls.CtlButton();
            this._txtEvalFilename = new MGui.Controls.CtlTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this._btnColourCentre = new MGui.Controls.CtlColourEditor();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this._btnColourSeries = new MGui.Controls.CtlColourEditor();
            this.label8 = new System.Windows.Forms.Label();
            this._btnColourHighlight = new MGui.Controls.CtlColourEditor();
            this.label9 = new System.Windows.Forms.Label();
            this._btnColourMinorGrid = new MGui.Controls.CtlColourEditor();
            this.label10 = new System.Windows.Forms.Label();
            this._btnColourMajorGrid = new MGui.Controls.CtlColourEditor();
            this.label11 = new System.Windows.Forms.Label();
            this._btnColourAxisTitle = new MGui.Controls.CtlColourEditor();
            this._txtClusterInfo = new MGui.Controls.CtlTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this._txtClusterTitle = new MGui.Controls.CtlTextBox();
            this.label13 = new System.Windows.Forms.Label();
            this._txtClusterXAxis = new MGui.Controls.CtlTextBox();
            this.label14 = new System.Windows.Forms.Label();
            this._txtClusterSubtitle = new MGui.Controls.CtlTextBox();
            this.label15 = new System.Windows.Forms.Label();
            this._txtClusterYAxis = new MGui.Controls.CtlTextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label37 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label17 = new System.Windows.Forms.Label();
            this._txtPeakYAxis = new MGui.Controls.CtlTextBox();
            this._txtPeakInfo = new MGui.Controls.CtlTextBox();
            this._txtPeakXAxis = new MGui.Controls.CtlTextBox();
            this.label18 = new System.Windows.Forms.Label();
            this._txtPeakSubtitle = new MGui.Controls.CtlTextBox();
            this.label19 = new System.Windows.Forms.Label();
            this._txtPeakTitle = new MGui.Controls.CtlTextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this._lstPeakData = new System.Windows.Forms.ComboBox();
            this.label33 = new System.Windows.Forms.Label();
            this._lstPeakOrder = new System.Windows.Forms.ComboBox();
            this.label34 = new System.Windows.Forms.Label();
            this._lstPeakPlotting = new System.Windows.Forms.ComboBox();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this._chkPeakMinMax = new System.Windows.Forms.CheckBox();
            this._chkGroupNames = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label22 = new System.Windows.Forms.Label();
            this._txtCompYAxis = new MGui.Controls.CtlTextBox();
            this._txtCompInfo = new MGui.Controls.CtlTextBox();
            this._txtCompXAxis = new MGui.Controls.CtlTextBox();
            this.label23 = new System.Windows.Forms.Label();
            this._txtCompSubtitle = new MGui.Controls.CtlTextBox();
            this.label24 = new System.Windows.Forms.Label();
            this._txtCompTitle = new MGui.Controls.CtlTextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label27 = new System.Windows.Forms.Label();
            this._txtPathYAxis = new MGui.Controls.CtlTextBox();
            this._txtPathInfo = new MGui.Controls.CtlTextBox();
            this._txtPathXAxis = new MGui.Controls.CtlTextBox();
            this.label28 = new System.Windows.Forms.Label();
            this._txtPathSubtitle = new MGui.Controls.CtlTextBox();
            this.label29 = new System.Windows.Forms.Label();
            this._txtPathTitle = new MGui.Controls.CtlTextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.label44 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this._btnColourBackground = new MGui.Controls.CtlColourEditor();
            this._btnColourPreviewBackground = new MGui.Controls.CtlColourEditor();
            this._btnColourUntypedElements = new MGui.Controls.CtlColourEditor();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.label41 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label38 = new System.Windows.Forms.Label();
            this._numThumbnail = new System.Windows.Forms.NumericUpDown();
            this.label39 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this._btnEditColumns = new MetaboliteLevels.Controls.CtlButton();
            this._btnEditDefaults = new MetaboliteLevels.Controls.CtlButton();
            this.label46 = new System.Windows.Forms.Label();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.label49 = new System.Windows.Forms.Label();
            this._btnHhMax = new MGui.Controls.CtlColourEditor();
            this.label51 = new System.Windows.Forms.Label();
            this._btnHhOor = new MGui.Controls.CtlColourEditor();
            this._btnHhMin = new MGui.Controls.CtlColourEditor();
            this._btnHhNan = new MGui.Controls.CtlColourEditor();
            this.label53 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._btnCancel = new MetaboliteLevels.Controls.CtlButton();
            this._btnOk = new MetaboliteLevels.Controls.CtlButton();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.ctlTitleBar1 = new MetaboliteLevels.Controls.CtlTitleBar();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.coreOptionsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.resetToDefaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label47 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this._txtClusterXRange = new MGui.Controls.CtlTextBox();
            this._txtClusterYRange = new MGui.Controls.CtlTextBox();
            this.label50 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this._txtPeakXRange = new MGui.Controls.CtlTextBox();
            this._txtPeakYRange = new MGui.Controls.CtlTextBox();
            this.label55 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this._txtCompXRange = new MGui.Controls.CtlTextBox();
            this._txtCompYRange = new MGui.Controls.CtlTextBox();
            this.label57 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this._txtPathXRange = new MGui.Controls.CtlTextBox();
            this._txtPathYRange = new MGui.Controls.CtlTextBox();
            ((System.ComponentModel.ISupportInitialize)(this._numSizeLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._numClusterMaxPlot)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._numThumbnail)).BeginInit();
            this.tabPage8.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.coreOptionsBindingSource)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Margin = new System.Windows.Forms.Padding(8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Matrix size limit";
            // 
            // _numSizeLimit
            // 
            this._numSizeLimit.Dock = System.Windows.Forms.DockStyle.Top;
            this._numSizeLimit.Location = new System.Drawing.Point(273, 8);
            this._numSizeLimit.Margin = new System.Windows.Forms.Padding(8);
            this._numSizeLimit.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this._numSizeLimit.Name = "_numSizeLimit";
            this._numSizeLimit.Size = new System.Drawing.Size(677, 29);
            this._numSizeLimit.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(966, 8);
            this.label2.Margin = new System.Windows.Forms.Padding(8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "Mb";
            // 
            // _chkClusterCentres
            // 
            this._chkClusterCentres.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this._chkClusterCentres, 2);
            this._chkClusterCentres.Location = new System.Drawing.Point(119, 323);
            this._chkClusterCentres.Margin = new System.Windows.Forms.Padding(8);
            this._chkClusterCentres.Name = "_chkClusterCentres";
            this._chkClusterCentres.Size = new System.Drawing.Size(132, 25);
            this._chkClusterCentres.TabIndex = 2;
            this._chkClusterCentres.Text = "Cluster centres";
            this._chkClusterCentres.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 364);
            this.label3.Margin = new System.Windows.Forms.Padding(8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 21);
            this.label3.TabIndex = 0;
            this.label3.Text = "At most plot";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(939, 364);
            this.label4.Margin = new System.Windows.Forms.Padding(8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 21);
            this.label4.TabIndex = 0;
            this.label4.Text = "vectors";
            // 
            // _numClusterMaxPlot
            // 
            this._numClusterMaxPlot.Dock = System.Windows.Forms.DockStyle.Top;
            this._numClusterMaxPlot.Location = new System.Drawing.Point(119, 364);
            this._numClusterMaxPlot.Margin = new System.Windows.Forms.Padding(8);
            this._numClusterMaxPlot.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this._numClusterMaxPlot.Name = "_numClusterMaxPlot";
            this._numClusterMaxPlot.Size = new System.Drawing.Size(804, 29);
            this._numClusterMaxPlot.TabIndex = 1;
            // 
            // _chkPeakRanges
            // 
            this._chkPeakRanges.AutoSize = true;
            this._chkPeakRanges.Location = new System.Drawing.Point(1015, 458);
            this._chkPeakRanges.Margin = new System.Windows.Forms.Padding(8);
            this._chkPeakRanges.Name = "_chkPeakRanges";
            this._chkPeakRanges.Size = new System.Drawing.Size(132, 25);
            this._chkPeakRanges.TabIndex = 2;
            this._chkPeakRanges.Text = "Range shading";
            this._chkPeakRanges.UseVisualStyleBackColor = true;
            // 
            // _chkPeakMean
            // 
            this._chkPeakMean.AutoSize = true;
            this._chkPeakMean.Location = new System.Drawing.Point(116, 499);
            this._chkPeakMean.Margin = new System.Windows.Forms.Padding(8);
            this._chkPeakMean.Name = "_chkPeakMean";
            this._chkPeakMean.Size = new System.Drawing.Size(158, 25);
            this._chkPeakMean.TabIndex = 2;
            this._chkPeakMean.Text = "Mean and SD lines";
            this._chkPeakMean.UseVisualStyleBackColor = true;
            // 
            // _chkPeakData
            // 
            this._chkPeakData.AutoSize = true;
            this._chkPeakData.Location = new System.Drawing.Point(116, 458);
            this._chkPeakData.Margin = new System.Windows.Forms.Padding(8);
            this._chkPeakData.Name = "_chkPeakData";
            this._chkPeakData.Size = new System.Drawing.Size(104, 25);
            this._chkPeakData.TabIndex = 2;
            this._chkPeakData.Text = "Datapoints";
            this._chkPeakData.UseVisualStyleBackColor = true;
            // 
            // _chkPeakTrend
            // 
            this._chkPeakTrend.AutoSize = true;
            this._chkPeakTrend.Location = new System.Drawing.Point(1015, 499);
            this._chkPeakTrend.Margin = new System.Windows.Forms.Padding(8);
            this._chkPeakTrend.Name = "_chkPeakTrend";
            this._chkPeakTrend.Size = new System.Drawing.Size(105, 25);
            this._chkPeakTrend.TabIndex = 2;
            this._chkPeakTrend.Text = "Trend lines";
            this._chkPeakTrend.UseVisualStyleBackColor = true;
            // 
            // _chkPeakFlag
            // 
            this._chkPeakFlag.AutoSize = true;
            this._chkPeakFlag.Location = new System.Drawing.Point(116, 540);
            this._chkPeakFlag.Margin = new System.Windows.Forms.Padding(8);
            this._chkPeakFlag.Name = "_chkPeakFlag";
            this._chkPeakFlag.Size = new System.Drawing.Size(182, 25);
            this._chkPeakFlag.TabIndex = 2;
            this._chkPeakFlag.Text = "Enabled peak flagging";
            this._chkPeakFlag.UseVisualStyleBackColor = true;
            // 
            // _btnEditFlags
            // 
            this._btnEditFlags.Image = global::MetaboliteLevels.Properties.Resources.MnuEnlargeList;
            this._btnEditFlags.Location = new System.Drawing.Point(116, 581);
            this._btnEditFlags.Margin = new System.Windows.Forms.Padding(8);
            this._btnEditFlags.Name = "_btnEditFlags";
            this._btnEditFlags.Size = new System.Drawing.Size(128, 40);
            this._btnEditFlags.TabIndex = 3;
            this._btnEditFlags.Text = "Edit flags...";
            this._btnEditFlags.UseDefaultSize = true;
            this._btnEditFlags.UseVisualStyleBackColor = true;
            this._btnEditFlags.Click += new System.EventHandler(this._btnEditFlags_Click);
            // 
            // _txtEvalFilename
            // 
            this.tableLayoutPanel5.SetColumnSpan(this._txtEvalFilename, 2);
            this._txtEvalFilename.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtEvalFilename.Location = new System.Drawing.Point(273, 53);
            this._txtEvalFilename.Margin = new System.Windows.Forms.Padding(8);
            this._txtEvalFilename.Name = "_txtEvalFilename";
            this._txtEvalFilename.Size = new System.Drawing.Size(726, 29);
            this._txtEvalFilename.TabIndex = 5;
            this._txtEvalFilename.Watermark = null;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 53);
            this.label5.Margin = new System.Windows.Forms.Padding(8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(249, 21);
            this.label5.TabIndex = 0;
            this.label5.Text = "Cluster evaluation results filename";
            // 
            // _btnColourCentre
            // 
            this._btnColourCentre.Location = new System.Drawing.Point(176, 8);
            this._btnColourCentre.Margin = new System.Windows.Forms.Padding(8);
            this._btnColourCentre.Name = "_btnColourCentre";
            this._btnColourCentre.SelectedColor = System.Drawing.Color.White;
            this._btnColourCentre.TabIndex = 4;
            this._btnColourCentre.UseVisualStyleBackColor = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 8);
            this.label6.Margin = new System.Windows.Forms.Padding(8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 21);
            this.label6.TabIndex = 0;
            this.label6.Text = "Cluster centre";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 98);
            this.label7.Margin = new System.Windows.Forms.Padding(8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(112, 21);
            this.label7.TabIndex = 0;
            this.label7.Text = "Selected series";
            // 
            // _btnColourSeries
            // 
            this._btnColourSeries.Location = new System.Drawing.Point(176, 98);
            this._btnColourSeries.Margin = new System.Windows.Forms.Padding(8);
            this._btnColourSeries.Name = "_btnColourSeries";
            this._btnColourSeries.SelectedColor = System.Drawing.Color.White;
            this._btnColourSeries.TabIndex = 4;
            this._btnColourSeries.UseVisualStyleBackColor = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 53);
            this.label8.Margin = new System.Windows.Forms.Padding(8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(131, 21);
            this.label8.TabIndex = 0;
            this.label8.Text = "Notable highlight";
            // 
            // _btnColourHighlight
            // 
            this._btnColourHighlight.Location = new System.Drawing.Point(176, 53);
            this._btnColourHighlight.Margin = new System.Windows.Forms.Padding(8);
            this._btnColourHighlight.Name = "_btnColourHighlight";
            this._btnColourHighlight.SelectedColor = System.Drawing.Color.White;
            this._btnColourHighlight.TabIndex = 4;
            this._btnColourHighlight.UseVisualStyleBackColor = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 143);
            this.label9.Margin = new System.Windows.Forms.Padding(8);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(84, 21);
            this.label9.TabIndex = 0;
            this.label9.Text = "Minor grid";
            // 
            // _btnColourMinorGrid
            // 
            this._btnColourMinorGrid.Location = new System.Drawing.Point(176, 143);
            this._btnColourMinorGrid.Margin = new System.Windows.Forms.Padding(8);
            this._btnColourMinorGrid.Name = "_btnColourMinorGrid";
            this._btnColourMinorGrid.SelectedColor = System.Drawing.Color.White;
            this._btnColourMinorGrid.TabIndex = 4;
            this._btnColourMinorGrid.UseVisualStyleBackColor = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 188);
            this.label10.Margin = new System.Windows.Forms.Padding(8);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(83, 21);
            this.label10.TabIndex = 0;
            this.label10.Text = "Major grid";
            // 
            // _btnColourMajorGrid
            // 
            this._btnColourMajorGrid.Location = new System.Drawing.Point(176, 188);
            this._btnColourMajorGrid.Margin = new System.Windows.Forms.Padding(8);
            this._btnColourMajorGrid.Name = "_btnColourMajorGrid";
            this._btnColourMajorGrid.SelectedColor = System.Drawing.Color.White;
            this._btnColourMajorGrid.TabIndex = 4;
            this._btnColourMajorGrid.UseVisualStyleBackColor = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 233);
            this.label11.Margin = new System.Windows.Forms.Padding(8);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(68, 21);
            this.label11.TabIndex = 0;
            this.label11.Text = "Axis title";
            // 
            // _btnColourAxisTitle
            // 
            this._btnColourAxisTitle.Location = new System.Drawing.Point(176, 233);
            this._btnColourAxisTitle.Margin = new System.Windows.Forms.Padding(8);
            this._btnColourAxisTitle.Name = "_btnColourAxisTitle";
            this._btnColourAxisTitle.SelectedColor = System.Drawing.Color.White;
            this._btnColourAxisTitle.TabIndex = 4;
            this._btnColourAxisTitle.UseVisualStyleBackColor = false;
            // 
            // _txtClusterInfo
            // 
            this.tableLayoutPanel1.SetColumnSpan(this._txtClusterInfo, 2);
            this._txtClusterInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtClusterInfo.Location = new System.Drawing.Point(119, 8);
            this._txtClusterInfo.Margin = new System.Windows.Forms.Padding(8);
            this._txtClusterInfo.Name = "_txtClusterInfo";
            this._txtClusterInfo.Size = new System.Drawing.Size(880, 29);
            this._txtClusterInfo.TabIndex = 6;
            this._txtClusterInfo.Watermark = null;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 8);
            this.label12.Margin = new System.Windows.Forms.Padding(8);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(92, 21);
            this.label12.TabIndex = 0;
            this.label12.Text = "Information";
            // 
            // _txtClusterTitle
            // 
            this.tableLayoutPanel1.SetColumnSpan(this._txtClusterTitle, 2);
            this._txtClusterTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtClusterTitle.Location = new System.Drawing.Point(119, 53);
            this._txtClusterTitle.Margin = new System.Windows.Forms.Padding(8);
            this._txtClusterTitle.Name = "_txtClusterTitle";
            this._txtClusterTitle.Size = new System.Drawing.Size(880, 29);
            this._txtClusterTitle.TabIndex = 8;
            this._txtClusterTitle.Watermark = null;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 53);
            this.label13.Margin = new System.Windows.Forms.Padding(8);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(39, 21);
            this.label13.TabIndex = 7;
            this.label13.Text = "Title";
            // 
            // _txtClusterXAxis
            // 
            this.tableLayoutPanel1.SetColumnSpan(this._txtClusterXAxis, 2);
            this._txtClusterXAxis.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtClusterXAxis.Location = new System.Drawing.Point(119, 143);
            this._txtClusterXAxis.Margin = new System.Windows.Forms.Padding(8);
            this._txtClusterXAxis.Name = "_txtClusterXAxis";
            this._txtClusterXAxis.Size = new System.Drawing.Size(880, 29);
            this._txtClusterXAxis.TabIndex = 12;
            this._txtClusterXAxis.Watermark = null;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(8, 143);
            this.label14.Margin = new System.Windows.Forms.Padding(8);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(51, 21);
            this.label14.TabIndex = 11;
            this.label14.Text = "X-axis";
            this.label14.Click += new System.EventHandler(this.label14_Click);
            // 
            // _txtClusterSubtitle
            // 
            this.tableLayoutPanel1.SetColumnSpan(this._txtClusterSubtitle, 2);
            this._txtClusterSubtitle.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtClusterSubtitle.Location = new System.Drawing.Point(119, 98);
            this._txtClusterSubtitle.Margin = new System.Windows.Forms.Padding(8);
            this._txtClusterSubtitle.Name = "_txtClusterSubtitle";
            this._txtClusterSubtitle.Size = new System.Drawing.Size(880, 29);
            this._txtClusterSubtitle.TabIndex = 10;
            this._txtClusterSubtitle.Watermark = null;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(8, 98);
            this.label15.Margin = new System.Windows.Forms.Padding(8);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(63, 21);
            this.label15.TabIndex = 9;
            this.label15.Text = "Subtitle";
            // 
            // _txtClusterYAxis
            // 
            this.tableLayoutPanel1.SetColumnSpan(this._txtClusterYAxis, 2);
            this._txtClusterYAxis.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtClusterYAxis.Location = new System.Drawing.Point(119, 233);
            this._txtClusterYAxis.Margin = new System.Windows.Forms.Padding(8);
            this._txtClusterYAxis.Name = "_txtClusterYAxis";
            this._txtClusterYAxis.Size = new System.Drawing.Size(880, 29);
            this._txtClusterYAxis.TabIndex = 14;
            this._txtClusterYAxis.Watermark = null;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(8, 233);
            this.label16.Margin = new System.Windows.Forms.Padding(8);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(51, 21);
            this.label16.TabIndex = 13;
            this.label16.Text = "Y-axis";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.label12, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._txtClusterYAxis, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this._txtClusterInfo, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this._txtClusterXAxis, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label16, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this._txtClusterSubtitle, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label13, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this._txtClusterTitle, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label15, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label14, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label4, 2, 8);
            this.tableLayoutPanel1.Controls.Add(this._numClusterMaxPlot, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this._chkClusterCentres, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.label37, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.label47, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label48, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this._txtClusterXRange, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this._txtClusterYRange, 1, 6);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 9;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1007, 583);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(8, 323);
            this.label37.Margin = new System.Windows.Forms.Padding(8);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(49, 21);
            this.label37.TabIndex = 13;
            this.label37.Text = "Show";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage7);
            this.tabControl1.Controls.Add(this.tabPage8);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 87);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1021, 623);
            this.tabControl1.TabIndex = 16;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 30);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1013, 589);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Cluster";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 30);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1013, 589);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Peaks";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.label17, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this._txtPeakYAxis, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this._txtPeakInfo, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this._txtPeakXAxis, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.label18, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this._txtPeakSubtitle, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.label19, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this._txtPeakTitle, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label20, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label21, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this._btnEditFlags, 1, 13);
            this.tableLayoutPanel2.Controls.Add(this.label32, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this._chkPeakFlag, 1, 12);
            this.tableLayoutPanel2.Controls.Add(this._lstPeakData, 1, 7);
            this.tableLayoutPanel2.Controls.Add(this.label33, 0, 8);
            this.tableLayoutPanel2.Controls.Add(this._lstPeakOrder, 1, 8);
            this.tableLayoutPanel2.Controls.Add(this._chkPeakMean, 1, 11);
            this.tableLayoutPanel2.Controls.Add(this.label34, 0, 9);
            this.tableLayoutPanel2.Controls.Add(this._lstPeakPlotting, 1, 9);
            this.tableLayoutPanel2.Controls.Add(this.label35, 0, 10);
            this.tableLayoutPanel2.Controls.Add(this.label36, 0, 12);
            this.tableLayoutPanel2.Controls.Add(this._chkPeakTrend, 2, 11);
            this.tableLayoutPanel2.Controls.Add(this._chkPeakMinMax, 3, 10);
            this.tableLayoutPanel2.Controls.Add(this._chkPeakRanges, 2, 10);
            this.tableLayoutPanel2.Controls.Add(this._chkPeakData, 1, 10);
            this.tableLayoutPanel2.Controls.Add(this._chkGroupNames, 3, 11);
            this.tableLayoutPanel2.Controls.Add(this.label50, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.label52, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this._txtPeakXRange, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this._txtPeakYRange, 1, 6);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
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
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1007, 583);
            this.tableLayoutPanel2.TabIndex = 16;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(8, 8);
            this.label17.Margin = new System.Windows.Forms.Padding(8);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(92, 21);
            this.label17.TabIndex = 0;
            this.label17.Text = "Information";
            // 
            // _txtPeakYAxis
            // 
            this.tableLayoutPanel2.SetColumnSpan(this._txtPeakYAxis, 3);
            this._txtPeakYAxis.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtPeakYAxis.Location = new System.Drawing.Point(116, 233);
            this._txtPeakYAxis.Margin = new System.Windows.Forms.Padding(8);
            this._txtPeakYAxis.Name = "_txtPeakYAxis";
            this._txtPeakYAxis.Size = new System.Drawing.Size(883, 29);
            this._txtPeakYAxis.TabIndex = 14;
            this._txtPeakYAxis.Watermark = null;
            // 
            // _txtPeakInfo
            // 
            this.tableLayoutPanel2.SetColumnSpan(this._txtPeakInfo, 3);
            this._txtPeakInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtPeakInfo.Location = new System.Drawing.Point(116, 8);
            this._txtPeakInfo.Margin = new System.Windows.Forms.Padding(8);
            this._txtPeakInfo.Name = "_txtPeakInfo";
            this._txtPeakInfo.Size = new System.Drawing.Size(883, 29);
            this._txtPeakInfo.TabIndex = 6;
            this._txtPeakInfo.Watermark = null;
            // 
            // _txtPeakXAxis
            // 
            this.tableLayoutPanel2.SetColumnSpan(this._txtPeakXAxis, 3);
            this._txtPeakXAxis.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtPeakXAxis.Location = new System.Drawing.Point(116, 143);
            this._txtPeakXAxis.Margin = new System.Windows.Forms.Padding(8);
            this._txtPeakXAxis.Name = "_txtPeakXAxis";
            this._txtPeakXAxis.Size = new System.Drawing.Size(883, 29);
            this._txtPeakXAxis.TabIndex = 12;
            this._txtPeakXAxis.Watermark = null;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(8, 233);
            this.label18.Margin = new System.Windows.Forms.Padding(8);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(51, 21);
            this.label18.TabIndex = 13;
            this.label18.Text = "Y-axis";
            // 
            // _txtPeakSubtitle
            // 
            this.tableLayoutPanel2.SetColumnSpan(this._txtPeakSubtitle, 3);
            this._txtPeakSubtitle.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtPeakSubtitle.Location = new System.Drawing.Point(116, 98);
            this._txtPeakSubtitle.Margin = new System.Windows.Forms.Padding(8);
            this._txtPeakSubtitle.Name = "_txtPeakSubtitle";
            this._txtPeakSubtitle.Size = new System.Drawing.Size(883, 29);
            this._txtPeakSubtitle.TabIndex = 10;
            this._txtPeakSubtitle.Watermark = null;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(8, 53);
            this.label19.Margin = new System.Windows.Forms.Padding(8);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(39, 21);
            this.label19.TabIndex = 7;
            this.label19.Text = "Title";
            // 
            // _txtPeakTitle
            // 
            this.tableLayoutPanel2.SetColumnSpan(this._txtPeakTitle, 3);
            this._txtPeakTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtPeakTitle.Location = new System.Drawing.Point(116, 53);
            this._txtPeakTitle.Margin = new System.Windows.Forms.Padding(8);
            this._txtPeakTitle.Name = "_txtPeakTitle";
            this._txtPeakTitle.Size = new System.Drawing.Size(883, 29);
            this._txtPeakTitle.TabIndex = 8;
            this._txtPeakTitle.Watermark = null;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(8, 98);
            this.label20.Margin = new System.Windows.Forms.Padding(8);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(63, 21);
            this.label20.TabIndex = 9;
            this.label20.Text = "Subtitle";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(8, 143);
            this.label21.Margin = new System.Windows.Forms.Padding(8);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(51, 21);
            this.label21.TabIndex = 11;
            this.label21.Text = "X-axis";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(8, 323);
            this.label32.Margin = new System.Windows.Forms.Padding(8);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(42, 21);
            this.label32.TabIndex = 0;
            this.label32.Text = "Data";
            // 
            // _lstPeakData
            // 
            this.tableLayoutPanel2.SetColumnSpan(this._lstPeakData, 3);
            this._lstPeakData.Dock = System.Windows.Forms.DockStyle.Top;
            this._lstPeakData.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstPeakData.FormattingEnabled = true;
            this._lstPeakData.Items.AddRange(new object[] {
            "Standard",
            "Alternate"});
            this._lstPeakData.Location = new System.Drawing.Point(116, 323);
            this._lstPeakData.Margin = new System.Windows.Forms.Padding(8);
            this._lstPeakData.Name = "_lstPeakData";
            this._lstPeakData.Size = new System.Drawing.Size(883, 29);
            this._lstPeakData.TabIndex = 15;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(8, 368);
            this.label33.Margin = new System.Windows.Forms.Padding(8);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(51, 21);
            this.label33.TabIndex = 0;
            this.label33.Text = "Order";
            // 
            // _lstPeakOrder
            // 
            this.tableLayoutPanel2.SetColumnSpan(this._lstPeakOrder, 3);
            this._lstPeakOrder.Dock = System.Windows.Forms.DockStyle.Top;
            this._lstPeakOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstPeakOrder.FormattingEnabled = true;
            this._lstPeakOrder.Items.AddRange(new object[] {
            "Experimental",
            "Batch"});
            this._lstPeakOrder.Location = new System.Drawing.Point(116, 368);
            this._lstPeakOrder.Margin = new System.Windows.Forms.Padding(8);
            this._lstPeakOrder.Name = "_lstPeakOrder";
            this._lstPeakOrder.Size = new System.Drawing.Size(883, 29);
            this._lstPeakOrder.TabIndex = 15;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(8, 413);
            this.label34.Margin = new System.Windows.Forms.Padding(8);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(64, 21);
            this.label34.TabIndex = 0;
            this.label34.Text = "Plotting";
            // 
            // _lstPeakPlotting
            // 
            this.tableLayoutPanel2.SetColumnSpan(this._lstPeakPlotting, 3);
            this._lstPeakPlotting.Dock = System.Windows.Forms.DockStyle.Top;
            this._lstPeakPlotting.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstPeakPlotting.FormattingEnabled = true;
            this._lstPeakPlotting.Items.AddRange(new object[] {
            "Overlaid",
            "Side-by-side"});
            this._lstPeakPlotting.Location = new System.Drawing.Point(116, 413);
            this._lstPeakPlotting.Margin = new System.Windows.Forms.Padding(8);
            this._lstPeakPlotting.Name = "_lstPeakPlotting";
            this._lstPeakPlotting.Size = new System.Drawing.Size(883, 29);
            this._lstPeakPlotting.TabIndex = 15;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(8, 458);
            this.label35.Margin = new System.Windows.Forms.Padding(8);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(49, 21);
            this.label35.TabIndex = 0;
            this.label35.Text = "Show";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(8, 540);
            this.label36.Margin = new System.Windows.Forms.Padding(8);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(46, 21);
            this.label36.TabIndex = 0;
            this.label36.Text = "Flags";
            // 
            // _chkPeakMinMax
            // 
            this._chkPeakMinMax.AutoSize = true;
            this._chkPeakMinMax.Location = new System.Drawing.Point(1163, 458);
            this._chkPeakMinMax.Margin = new System.Windows.Forms.Padding(8);
            this._chkPeakMinMax.Name = "_chkPeakMinMax";
            this._chkPeakMinMax.Size = new System.Drawing.Size(1, 25);
            this._chkPeakMinMax.TabIndex = 2;
            this._chkPeakMinMax.Text = "Min/max lines";
            this._chkPeakMinMax.UseVisualStyleBackColor = true;
            // 
            // _chkGroupNames
            // 
            this._chkGroupNames.AutoSize = true;
            this._chkGroupNames.Location = new System.Drawing.Point(1163, 499);
            this._chkGroupNames.Margin = new System.Windows.Forms.Padding(8);
            this._chkGroupNames.Name = "_chkGroupNames";
            this._chkGroupNames.Size = new System.Drawing.Size(1, 25);
            this._chkGroupNames.TabIndex = 2;
            this._chkGroupNames.Text = "Experimental group names";
            this._chkGroupNames.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tableLayoutPanel3);
            this.tabPage3.Location = new System.Drawing.Point(4, 30);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1013, 589);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Compounds";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.label22, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this._txtCompYAxis, 1, 5);
            this.tableLayoutPanel3.Controls.Add(this._txtCompInfo, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this._txtCompXAxis, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.label23, 0, 5);
            this.tableLayoutPanel3.Controls.Add(this._txtCompSubtitle, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.label24, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this._txtCompTitle, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.label25, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.label26, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.label55, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.label56, 0, 6);
            this.tableLayoutPanel3.Controls.Add(this._txtCompXRange, 1, 4);
            this.tableLayoutPanel3.Controls.Add(this._txtCompYRange, 1, 6);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 7;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1007, 583);
            this.tableLayoutPanel3.TabIndex = 16;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(8, 8);
            this.label22.Margin = new System.Windows.Forms.Padding(8);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(92, 21);
            this.label22.TabIndex = 0;
            this.label22.Text = "Information";
            // 
            // _txtCompYAxis
            // 
            this._txtCompYAxis.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtCompYAxis.Location = new System.Drawing.Point(116, 233);
            this._txtCompYAxis.Margin = new System.Windows.Forms.Padding(8);
            this._txtCompYAxis.Name = "_txtCompYAxis";
            this._txtCompYAxis.Size = new System.Drawing.Size(883, 29);
            this._txtCompYAxis.TabIndex = 14;
            this._txtCompYAxis.Watermark = null;
            // 
            // _txtCompInfo
            // 
            this._txtCompInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtCompInfo.Location = new System.Drawing.Point(116, 8);
            this._txtCompInfo.Margin = new System.Windows.Forms.Padding(8);
            this._txtCompInfo.Name = "_txtCompInfo";
            this._txtCompInfo.Size = new System.Drawing.Size(883, 29);
            this._txtCompInfo.TabIndex = 6;
            this._txtCompInfo.Watermark = null;
            // 
            // _txtCompXAxis
            // 
            this._txtCompXAxis.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtCompXAxis.Location = new System.Drawing.Point(116, 143);
            this._txtCompXAxis.Margin = new System.Windows.Forms.Padding(8);
            this._txtCompXAxis.Name = "_txtCompXAxis";
            this._txtCompXAxis.Size = new System.Drawing.Size(883, 29);
            this._txtCompXAxis.TabIndex = 12;
            this._txtCompXAxis.Watermark = null;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(8, 233);
            this.label23.Margin = new System.Windows.Forms.Padding(8);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(51, 21);
            this.label23.TabIndex = 13;
            this.label23.Text = "Y-axis";
            // 
            // _txtCompSubtitle
            // 
            this._txtCompSubtitle.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtCompSubtitle.Location = new System.Drawing.Point(116, 98);
            this._txtCompSubtitle.Margin = new System.Windows.Forms.Padding(8);
            this._txtCompSubtitle.Name = "_txtCompSubtitle";
            this._txtCompSubtitle.Size = new System.Drawing.Size(883, 29);
            this._txtCompSubtitle.TabIndex = 10;
            this._txtCompSubtitle.Watermark = null;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(8, 53);
            this.label24.Margin = new System.Windows.Forms.Padding(8);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(39, 21);
            this.label24.TabIndex = 7;
            this.label24.Text = "Title";
            // 
            // _txtCompTitle
            // 
            this._txtCompTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtCompTitle.Location = new System.Drawing.Point(116, 53);
            this._txtCompTitle.Margin = new System.Windows.Forms.Padding(8);
            this._txtCompTitle.Name = "_txtCompTitle";
            this._txtCompTitle.Size = new System.Drawing.Size(883, 29);
            this._txtCompTitle.TabIndex = 8;
            this._txtCompTitle.Watermark = null;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(8, 98);
            this.label25.Margin = new System.Windows.Forms.Padding(8);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(63, 21);
            this.label25.TabIndex = 9;
            this.label25.Text = "Subtitle";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(8, 143);
            this.label26.Margin = new System.Windows.Forms.Padding(8);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(51, 21);
            this.label26.TabIndex = 11;
            this.label26.Text = "X-axis";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.tableLayoutPanel4);
            this.tabPage4.Location = new System.Drawing.Point(4, 30);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(1013, 589);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Pathways";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.Controls.Add(this.label27, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this._txtPathYAxis, 1, 5);
            this.tableLayoutPanel4.Controls.Add(this._txtPathInfo, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this._txtPathXAxis, 1, 3);
            this.tableLayoutPanel4.Controls.Add(this.label28, 0, 5);
            this.tableLayoutPanel4.Controls.Add(this._txtPathSubtitle, 1, 2);
            this.tableLayoutPanel4.Controls.Add(this.label29, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this._txtPathTitle, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.label30, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.label31, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.label57, 0, 4);
            this.tableLayoutPanel4.Controls.Add(this.label58, 0, 6);
            this.tableLayoutPanel4.Controls.Add(this._txtPathXRange, 1, 4);
            this.tableLayoutPanel4.Controls.Add(this._txtPathYRange, 1, 6);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 7;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1007, 583);
            this.tableLayoutPanel4.TabIndex = 16;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(8, 8);
            this.label27.Margin = new System.Windows.Forms.Padding(8);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(92, 21);
            this.label27.TabIndex = 0;
            this.label27.Text = "Information";
            // 
            // _txtPathYAxis
            // 
            this._txtPathYAxis.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtPathYAxis.Location = new System.Drawing.Point(116, 233);
            this._txtPathYAxis.Margin = new System.Windows.Forms.Padding(8);
            this._txtPathYAxis.Name = "_txtPathYAxis";
            this._txtPathYAxis.Size = new System.Drawing.Size(883, 29);
            this._txtPathYAxis.TabIndex = 14;
            this._txtPathYAxis.Watermark = null;
            // 
            // _txtPathInfo
            // 
            this._txtPathInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtPathInfo.Location = new System.Drawing.Point(116, 8);
            this._txtPathInfo.Margin = new System.Windows.Forms.Padding(8);
            this._txtPathInfo.Name = "_txtPathInfo";
            this._txtPathInfo.Size = new System.Drawing.Size(883, 29);
            this._txtPathInfo.TabIndex = 6;
            this._txtPathInfo.Watermark = null;
            // 
            // _txtPathXAxis
            // 
            this._txtPathXAxis.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtPathXAxis.Location = new System.Drawing.Point(116, 143);
            this._txtPathXAxis.Margin = new System.Windows.Forms.Padding(8);
            this._txtPathXAxis.Name = "_txtPathXAxis";
            this._txtPathXAxis.Size = new System.Drawing.Size(883, 29);
            this._txtPathXAxis.TabIndex = 12;
            this._txtPathXAxis.Watermark = null;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(8, 233);
            this.label28.Margin = new System.Windows.Forms.Padding(8);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(51, 21);
            this.label28.TabIndex = 13;
            this.label28.Text = "Y-axis";
            // 
            // _txtPathSubtitle
            // 
            this._txtPathSubtitle.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtPathSubtitle.Location = new System.Drawing.Point(116, 98);
            this._txtPathSubtitle.Margin = new System.Windows.Forms.Padding(8);
            this._txtPathSubtitle.Name = "_txtPathSubtitle";
            this._txtPathSubtitle.Size = new System.Drawing.Size(883, 29);
            this._txtPathSubtitle.TabIndex = 10;
            this._txtPathSubtitle.Watermark = null;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(8, 53);
            this.label29.Margin = new System.Windows.Forms.Padding(8);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(39, 21);
            this.label29.TabIndex = 7;
            this.label29.Text = "Title";
            // 
            // _txtPathTitle
            // 
            this._txtPathTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtPathTitle.Location = new System.Drawing.Point(116, 53);
            this._txtPathTitle.Margin = new System.Windows.Forms.Padding(8);
            this._txtPathTitle.Name = "_txtPathTitle";
            this._txtPathTitle.Size = new System.Drawing.Size(883, 29);
            this._txtPathTitle.TabIndex = 8;
            this._txtPathTitle.Watermark = null;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(8, 98);
            this.label30.Margin = new System.Windows.Forms.Padding(8);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(63, 21);
            this.label30.TabIndex = 9;
            this.label30.Text = "Subtitle";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(8, 143);
            this.label31.Margin = new System.Windows.Forms.Padding(8);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(51, 21);
            this.label31.TabIndex = 11;
            this.label31.Text = "X-axis";
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.tableLayoutPanel5);
            this.tabPage5.Location = new System.Drawing.Point(4, 30);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(1013, 589);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Calculations";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 4;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this._txtEvalFilename, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this._numSizeLimit, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.label5, 0, 1);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1007, 583);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.tableLayoutPanel6);
            this.tabPage6.Location = new System.Drawing.Point(4, 30);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(1013, 589);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Colours";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 4;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.Controls.Add(this.label44, 0, 8);
            this.tableLayoutPanel6.Controls.Add(this.label43, 0, 7);
            this.tableLayoutPanel6.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this._btnColourAxisTitle, 1, 5);
            this.tableLayoutPanel6.Controls.Add(this._btnColourCentre, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.label11, 0, 5);
            this.tableLayoutPanel6.Controls.Add(this._btnColourMajorGrid, 1, 4);
            this.tableLayoutPanel6.Controls.Add(this.label8, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this._btnColourMinorGrid, 1, 3);
            this.tableLayoutPanel6.Controls.Add(this.label10, 0, 4);
            this.tableLayoutPanel6.Controls.Add(this._btnColourHighlight, 1, 1);
            this.tableLayoutPanel6.Controls.Add(this._btnColourSeries, 1, 2);
            this.tableLayoutPanel6.Controls.Add(this.label7, 0, 2);
            this.tableLayoutPanel6.Controls.Add(this.label9, 0, 3);
            this.tableLayoutPanel6.Controls.Add(this.label42, 0, 6);
            this.tableLayoutPanel6.Controls.Add(this._btnColourBackground, 1, 6);
            this.tableLayoutPanel6.Controls.Add(this._btnColourPreviewBackground, 1, 7);
            this.tableLayoutPanel6.Controls.Add(this._btnColourUntypedElements, 1, 8);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 9;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.Size = new System.Drawing.Size(1007, 583);
            this.tableLayoutPanel6.TabIndex = 0;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(8, 368);
            this.label44.Margin = new System.Windows.Forms.Padding(8);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(136, 21);
            this.label44.TabIndex = 9;
            this.label44.Text = "Untyped elements";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(8, 323);
            this.label43.Margin = new System.Windows.Forms.Padding(8);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(152, 21);
            this.label43.TabIndex = 7;
            this.label43.Text = "Preview background";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(8, 278);
            this.label42.Margin = new System.Windows.Forms.Padding(8);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(93, 21);
            this.label42.TabIndex = 5;
            this.label42.Text = "Background";
            // 
            // _btnColourBackground
            // 
            this._btnColourBackground.Location = new System.Drawing.Point(176, 278);
            this._btnColourBackground.Margin = new System.Windows.Forms.Padding(8);
            this._btnColourBackground.Name = "_btnColourBackground";
            this._btnColourBackground.SelectedColor = System.Drawing.Color.White;
            this._btnColourBackground.TabIndex = 6;
            this._btnColourBackground.UseVisualStyleBackColor = false;
            // 
            // _btnColourPreviewBackground
            // 
            this._btnColourPreviewBackground.Location = new System.Drawing.Point(176, 323);
            this._btnColourPreviewBackground.Margin = new System.Windows.Forms.Padding(8);
            this._btnColourPreviewBackground.Name = "_btnColourPreviewBackground";
            this._btnColourPreviewBackground.SelectedColor = System.Drawing.Color.White;
            this._btnColourPreviewBackground.TabIndex = 8;
            this._btnColourPreviewBackground.UseVisualStyleBackColor = false;
            // 
            // _btnColourUntypedElements
            // 
            this._btnColourUntypedElements.Location = new System.Drawing.Point(176, 368);
            this._btnColourUntypedElements.Margin = new System.Windows.Forms.Padding(8);
            this._btnColourUntypedElements.Name = "_btnColourUntypedElements";
            this._btnColourUntypedElements.SelectedColor = System.Drawing.Color.White;
            this._btnColourUntypedElements.TabIndex = 10;
            this._btnColourUntypedElements.UseVisualStyleBackColor = false;
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.tableLayoutPanel7);
            this.tabPage7.Location = new System.Drawing.Point(4, 30);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(1013, 589);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "General";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 4;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.Controls.Add(this.label41, 2, 1);
            this.tableLayoutPanel7.Controls.Add(this.numericUpDown2, 1, 1);
            this.tableLayoutPanel7.Controls.Add(this.label38, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this._numThumbnail, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.label39, 2, 0);
            this.tableLayoutPanel7.Controls.Add(this.label40, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.label45, 0, 2);
            this.tableLayoutPanel7.Controls.Add(this._btnEditColumns, 1, 2);
            this.tableLayoutPanel7.Controls.Add(this._btnEditDefaults, 1, 3);
            this.tableLayoutPanel7.Controls.Add(this.label46, 0, 3);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 4;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(1007, 583);
            this.tableLayoutPanel7.TabIndex = 1;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(950, 53);
            this.label41.Margin = new System.Windows.Forms.Padding(8);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(49, 21);
            this.label41.TabIndex = 3;
            this.label41.Text = "pixels";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Dock = System.Windows.Forms.DockStyle.Top;
            this.numericUpDown2.Location = new System.Drawing.Point(224, 53);
            this.numericUpDown2.Margin = new System.Windows.Forms.Padding(8);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(710, 29);
            this.numericUpDown2.TabIndex = 2;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(8, 8);
            this.label38.Margin = new System.Windows.Forms.Padding(8);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(148, 21);
            this.label38.TabIndex = 0;
            this.label38.Text = "Thumbnail size (list)";
            // 
            // _numThumbnail
            // 
            this._numThumbnail.Dock = System.Windows.Forms.DockStyle.Top;
            this._numThumbnail.Location = new System.Drawing.Point(224, 8);
            this._numThumbnail.Margin = new System.Windows.Forms.Padding(8);
            this._numThumbnail.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this._numThumbnail.Name = "_numThumbnail";
            this._numThumbnail.Size = new System.Drawing.Size(710, 29);
            this._numThumbnail.TabIndex = 1;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(950, 8);
            this.label39.Margin = new System.Windows.Forms.Padding(8);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(49, 21);
            this.label39.TabIndex = 0;
            this.label39.Text = "pixels";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(8, 53);
            this.label40.Margin = new System.Windows.Forms.Padding(8);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(200, 21);
            this.label40.TabIndex = 0;
            this.label40.Text = "Thumbnail size (windowed)";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(8, 98);
            this.label45.Margin = new System.Windows.Forms.Padding(8);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(72, 21);
            this.label45.TabIndex = 0;
            this.label45.Text = "Columns";
            // 
            // _btnEditColumns
            // 
            this._btnEditColumns.Image = global::MetaboliteLevels.Properties.Resources.MnuEnlargeList;
            this._btnEditColumns.Location = new System.Drawing.Point(224, 98);
            this._btnEditColumns.Margin = new System.Windows.Forms.Padding(8);
            this._btnEditColumns.Name = "_btnEditColumns";
            this._btnEditColumns.Size = new System.Drawing.Size(128, 40);
            this._btnEditColumns.TabIndex = 4;
            this._btnEditColumns.Text = "Edit...";
            this._btnEditColumns.UseDefaultSize = true;
            this._btnEditColumns.UseVisualStyleBackColor = true;
            this._btnEditColumns.Click += new System.EventHandler(this._btnEditColumns_Click);
            // 
            // _btnEditDefaults
            // 
            this._btnEditDefaults.Image = global::MetaboliteLevels.Properties.Resources.MnuEnlargeList;
            this._btnEditDefaults.Location = new System.Drawing.Point(224, 154);
            this._btnEditDefaults.Margin = new System.Windows.Forms.Padding(8);
            this._btnEditDefaults.Name = "_btnEditDefaults";
            this._btnEditDefaults.Size = new System.Drawing.Size(128, 40);
            this._btnEditDefaults.TabIndex = 4;
            this._btnEditDefaults.Text = "Edit...";
            this._btnEditDefaults.UseDefaultSize = true;
            this._btnEditDefaults.UseVisualStyleBackColor = true;
            this._btnEditDefaults.Click += new System.EventHandler(this._btnEditDefaults_Click);
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(8, 154);
            this.label46.Margin = new System.Windows.Forms.Padding(8);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(67, 21);
            this.label46.TabIndex = 0;
            this.label46.Text = "Defaults";
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this.tableLayoutPanel8);
            this.tabPage8.Location = new System.Drawing.Point(4, 30);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage8.Size = new System.Drawing.Size(1013, 589);
            this.tabPage8.TabIndex = 7;
            this.tabPage8.Text = "Heat maps";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 4;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel8.Controls.Add(this.label49, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this._btnHhMax, 1, 0);
            this.tableLayoutPanel8.Controls.Add(this.label51, 0, 1);
            this.tableLayoutPanel8.Controls.Add(this._btnHhOor, 1, 3);
            this.tableLayoutPanel8.Controls.Add(this._btnHhMin, 1, 1);
            this.tableLayoutPanel8.Controls.Add(this._btnHhNan, 1, 2);
            this.tableLayoutPanel8.Controls.Add(this.label53, 0, 2);
            this.tableLayoutPanel8.Controls.Add(this.label54, 0, 3);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 4;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel8.Size = new System.Drawing.Size(1007, 583);
            this.tableLayoutPanel8.TabIndex = 1;
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(8, 8);
            this.label49.Margin = new System.Windows.Forms.Padding(8);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(80, 21);
            this.label49.TabIndex = 0;
            this.label49.Text = "Maximum";
            // 
            // _btnHhMax
            // 
            this._btnHhMax.Location = new System.Drawing.Point(131, 8);
            this._btnHhMax.Margin = new System.Windows.Forms.Padding(8);
            this._btnHhMax.Name = "_btnHhMax";
            this._btnHhMax.SelectedColor = System.Drawing.Color.White;
            this._btnHhMax.TabIndex = 4;
            this._btnHhMax.UseVisualStyleBackColor = false;
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(8, 53);
            this.label51.Margin = new System.Windows.Forms.Padding(8);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(78, 21);
            this.label51.TabIndex = 0;
            this.label51.Text = "Minimum";
            // 
            // _btnHhOor
            // 
            this._btnHhOor.Location = new System.Drawing.Point(131, 143);
            this._btnHhOor.Margin = new System.Windows.Forms.Padding(8);
            this._btnHhOor.Name = "_btnHhOor";
            this._btnHhOor.SelectedColor = System.Drawing.Color.White;
            this._btnHhOor.TabIndex = 4;
            this._btnHhOor.UseVisualStyleBackColor = false;
            // 
            // _btnHhMin
            // 
            this._btnHhMin.Location = new System.Drawing.Point(131, 53);
            this._btnHhMin.Margin = new System.Windows.Forms.Padding(8);
            this._btnHhMin.Name = "_btnHhMin";
            this._btnHhMin.SelectedColor = System.Drawing.Color.White;
            this._btnHhMin.TabIndex = 4;
            this._btnHhMin.UseVisualStyleBackColor = false;
            // 
            // _btnHhNan
            // 
            this._btnHhNan.Location = new System.Drawing.Point(131, 98);
            this._btnHhNan.Margin = new System.Windows.Forms.Padding(8);
            this._btnHhNan.Name = "_btnHhNan";
            this._btnHhNan.SelectedColor = System.Drawing.Color.White;
            this._btnHhNan.TabIndex = 4;
            this._btnHhNan.UseVisualStyleBackColor = false;
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Location = new System.Drawing.Point(8, 98);
            this.label53.Margin = new System.Windows.Forms.Padding(8);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(107, 21);
            this.label53.TabIndex = 0;
            this.label53.Text = "Not a number";
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(8, 143);
            this.label54.Margin = new System.Windows.Forms.Padding(8);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(98, 21);
            this.label54.TabIndex = 0;
            this.label54.Text = "Out of range";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this._btnCancel);
            this.flowLayoutPanel1.Controls.Add(this._btnOk);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 710);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1021, 56);
            this.flowLayoutPanel1.TabIndex = 17;
            // 
            // _btnCancel
            // 
            this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnCancel.Image = global::MetaboliteLevels.Properties.Resources.MnuCancel;
            this._btnCancel.Location = new System.Drawing.Point(885, 8);
            this._btnCancel.Margin = new System.Windows.Forms.Padding(8);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(128, 40);
            this._btnCancel.TabIndex = 1;
            this._btnCancel.Text = "Cancel";
            this._btnCancel.UseDefaultSize = true;
            this._btnCancel.UseVisualStyleBackColor = true;
            // 
            // _btnOk
            // 
            this._btnOk.Image = global::MetaboliteLevels.Properties.Resources.MnuAccept;
            this._btnOk.Location = new System.Drawing.Point(741, 8);
            this._btnOk.Margin = new System.Windows.Forms.Padding(8);
            this._btnOk.Name = "_btnOk";
            this._btnOk.Size = new System.Drawing.Size(128, 40);
            this._btnOk.TabIndex = 0;
            this._btnOk.Text = "OK";
            this._btnOk.UseDefaultSize = true;
            this._btnOk.UseVisualStyleBackColor = true;
            this._btnOk.Click += new System.EventHandler(this._btnOk_Click);
            // 
            // ctlTitleBar1
            // 
            this.ctlTitleBar1.AutoSize = true;
            this.ctlTitleBar1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ctlTitleBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlTitleBar1.HelpText = null;
            this.ctlTitleBar1.Location = new System.Drawing.Point(0, 0);
            this.ctlTitleBar1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ctlTitleBar1.MinimumSize = new System.Drawing.Size(384, 0);
            this.ctlTitleBar1.Name = "ctlTitleBar1";
            this.ctlTitleBar1.Size = new System.Drawing.Size(1021, 87);
            this.ctlTitleBar1.SubText = "These preferences affect the active session only";
            this.ctlTitleBar1.TabIndex = 18;
            this.ctlTitleBar1.Text = "Preferences";
            this.ctlTitleBar1.WarningText = null;
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 50000;
            this.toolTip1.InitialDelay = 100;
            this.toolTip1.ReshowDelay = 100;
            // 
            // coreOptionsBindingSource
            // 
            this.coreOptionsBindingSource.DataSource = typeof(MetaboliteLevels.Data.Session.Singular.CoreOptions);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetToDefaultToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(157, 26);
            // 
            // resetToDefaultToolStripMenuItem
            // 
            this.resetToDefaultToolStripMenuItem.Name = "resetToDefaultToolStripMenuItem";
            this.resetToDefaultToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.resetToDefaultToolStripMenuItem.Text = "&Reset to default";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(8, 188);
            this.label47.Margin = new System.Windows.Forms.Padding(8);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(69, 21);
            this.label47.TabIndex = 11;
            this.label47.Text = "X-Range";
            this.label47.Click += new System.EventHandler(this.label14_Click);
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(8, 278);
            this.label48.Margin = new System.Windows.Forms.Padding(8);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(69, 21);
            this.label48.TabIndex = 11;
            this.label48.Text = "Y-Range";
            this.label48.Click += new System.EventHandler(this.label14_Click);
            // 
            // _txtClusterXRange
            // 
            this.tableLayoutPanel1.SetColumnSpan(this._txtClusterXRange, 2);
            this._txtClusterXRange.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtClusterXRange.Location = new System.Drawing.Point(119, 188);
            this._txtClusterXRange.Margin = new System.Windows.Forms.Padding(8);
            this._txtClusterXRange.Name = "_txtClusterXRange";
            this._txtClusterXRange.Size = new System.Drawing.Size(880, 29);
            this._txtClusterXRange.TabIndex = 12;
            this._txtClusterXRange.Watermark = null;
            // 
            // _txtClusterYRange
            // 
            this.tableLayoutPanel1.SetColumnSpan(this._txtClusterYRange, 2);
            this._txtClusterYRange.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtClusterYRange.Location = new System.Drawing.Point(119, 278);
            this._txtClusterYRange.Margin = new System.Windows.Forms.Padding(8);
            this._txtClusterYRange.Name = "_txtClusterYRange";
            this._txtClusterYRange.Size = new System.Drawing.Size(880, 29);
            this._txtClusterYRange.TabIndex = 12;
            this._txtClusterYRange.Watermark = null;
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(8, 188);
            this.label50.Margin = new System.Windows.Forms.Padding(8);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(65, 21);
            this.label50.TabIndex = 11;
            this.label50.Text = "X-range";
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(8, 278);
            this.label52.Margin = new System.Windows.Forms.Padding(8);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(65, 21);
            this.label52.TabIndex = 11;
            this.label52.Text = "Y-range";
            // 
            // _txtPeakXRange
            // 
            this._txtPeakXRange.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtPeakXRange.Location = new System.Drawing.Point(116, 188);
            this._txtPeakXRange.Margin = new System.Windows.Forms.Padding(8);
            this._txtPeakXRange.Name = "_txtPeakXRange";
            this._txtPeakXRange.Size = new System.Drawing.Size(883, 29);
            this._txtPeakXRange.TabIndex = 12;
            this._txtPeakXRange.Watermark = null;
            // 
            // _txtPeakYRange
            // 
            this._txtPeakYRange.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtPeakYRange.Location = new System.Drawing.Point(116, 278);
            this._txtPeakYRange.Margin = new System.Windows.Forms.Padding(8);
            this._txtPeakYRange.Name = "_txtPeakYRange";
            this._txtPeakYRange.Size = new System.Drawing.Size(883, 29);
            this._txtPeakYRange.TabIndex = 12;
            this._txtPeakYRange.Watermark = null;
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Location = new System.Drawing.Point(8, 188);
            this.label55.Margin = new System.Windows.Forms.Padding(8);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(65, 21);
            this.label55.TabIndex = 11;
            this.label55.Text = "X-range";
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Location = new System.Drawing.Point(8, 278);
            this.label56.Margin = new System.Windows.Forms.Padding(8);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(65, 21);
            this.label56.TabIndex = 11;
            this.label56.Text = "Y-range";
            // 
            // _txtCompXRange
            // 
            this._txtCompXRange.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtCompXRange.Location = new System.Drawing.Point(116, 188);
            this._txtCompXRange.Margin = new System.Windows.Forms.Padding(8);
            this._txtCompXRange.Name = "_txtCompXRange";
            this._txtCompXRange.Size = new System.Drawing.Size(883, 29);
            this._txtCompXRange.TabIndex = 12;
            this._txtCompXRange.Watermark = null;
            // 
            // _txtCompYRange
            // 
            this._txtCompYRange.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtCompYRange.Location = new System.Drawing.Point(116, 278);
            this._txtCompYRange.Margin = new System.Windows.Forms.Padding(8);
            this._txtCompYRange.Name = "_txtCompYRange";
            this._txtCompYRange.Size = new System.Drawing.Size(883, 29);
            this._txtCompYRange.TabIndex = 12;
            this._txtCompYRange.Watermark = null;
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(8, 188);
            this.label57.Margin = new System.Windows.Forms.Padding(8);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(65, 21);
            this.label57.TabIndex = 11;
            this.label57.Text = "X-range";
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(8, 278);
            this.label58.Margin = new System.Windows.Forms.Padding(8);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(65, 21);
            this.label58.TabIndex = 11;
            this.label58.Text = "Y-range";
            // 
            // _txtPathXRange
            // 
            this._txtPathXRange.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtPathXRange.Location = new System.Drawing.Point(116, 188);
            this._txtPathXRange.Margin = new System.Windows.Forms.Padding(8);
            this._txtPathXRange.Name = "_txtPathXRange";
            this._txtPathXRange.Size = new System.Drawing.Size(883, 29);
            this._txtPathXRange.TabIndex = 12;
            this._txtPathXRange.Watermark = null;
            // 
            // _txtPathYRange
            // 
            this._txtPathYRange.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtPathYRange.Location = new System.Drawing.Point(116, 278);
            this._txtPathYRange.Margin = new System.Windows.Forms.Padding(8);
            this._txtPathYRange.Name = "_txtPathYRange";
            this._txtPathYRange.Size = new System.Drawing.Size(883, 29);
            this._txtPathYRange.TabIndex = 12;
            this._txtPathYRange.Watermark = null;
            // 
            // FrmEditCoreOptions
            // 
            this.AcceptButton = this._btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._btnCancel;
            this.ClientSize = new System.Drawing.Size(1021, 766);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.ctlTitleBar1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmEditCoreOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Session Preferences";
            ((System.ComponentModel.ISupportInitialize)(this._numSizeLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._numClusterMaxPlot)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tabPage7.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._numThumbnail)).EndInit();
            this.tabPage8.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.coreOptionsBindingSource)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown _numSizeLimit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox _chkClusterCentres;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown _numClusterMaxPlot;
        private System.Windows.Forms.CheckBox _chkPeakRanges;
        private System.Windows.Forms.CheckBox _chkPeakMean;
        private System.Windows.Forms.CheckBox _chkPeakData;
        private System.Windows.Forms.CheckBox _chkPeakTrend;
        private System.Windows.Forms.CheckBox _chkPeakFlag;
        private Controls.CtlButton _btnEditFlags;
        private MGui.Controls.CtlTextBox _txtEvalFilename;
        private System.Windows.Forms.Label label5;
        private MGui.Controls.CtlColourEditor _btnColourCentre;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private MGui.Controls.CtlColourEditor _btnColourSeries;
        private System.Windows.Forms.Label label8;
        private MGui.Controls.CtlColourEditor _btnColourHighlight;
        private System.Windows.Forms.Label label9;
        private MGui.Controls.CtlColourEditor _btnColourMinorGrid;
        private System.Windows.Forms.Label label10;
        private MGui.Controls.CtlColourEditor _btnColourMajorGrid;
        private System.Windows.Forms.Label label11;
        private MGui.Controls.CtlColourEditor _btnColourAxisTitle;
        private MGui.Controls.CtlTextBox _txtClusterInfo;
        private System.Windows.Forms.Label label12;
        private MGui.Controls.CtlTextBox _txtClusterTitle;
        private System.Windows.Forms.Label label13;
        private MGui.Controls.CtlTextBox _txtClusterXAxis;
        private System.Windows.Forms.Label label14;
        private MGui.Controls.CtlTextBox _txtClusterSubtitle;
        private System.Windows.Forms.Label label15;
        private MGui.Controls.CtlTextBox _txtClusterYAxis;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label17;
        private MGui.Controls.CtlTextBox _txtPeakYAxis;
        private MGui.Controls.CtlTextBox _txtPeakInfo;
        private MGui.Controls.CtlTextBox _txtPeakXAxis;
        private System.Windows.Forms.Label label18;
        private MGui.Controls.CtlTextBox _txtPeakSubtitle;
        private System.Windows.Forms.Label label19;
        private MGui.Controls.CtlTextBox _txtPeakTitle;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.ComboBox _lstPeakData;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.ComboBox _lstPeakOrder;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.ComboBox _lstPeakPlotting;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label22;
        private MGui.Controls.CtlTextBox _txtCompYAxis;
        private MGui.Controls.CtlTextBox _txtCompInfo;
        private MGui.Controls.CtlTextBox _txtCompXAxis;
        private System.Windows.Forms.Label label23;
        private MGui.Controls.CtlTextBox _txtCompSubtitle;
        private System.Windows.Forms.Label label24;
        private MGui.Controls.CtlTextBox _txtCompTitle;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label label27;
        private MGui.Controls.CtlTextBox _txtPathYAxis;
        private MGui.Controls.CtlTextBox _txtPathInfo;
        private MGui.Controls.CtlTextBox _txtPathXAxis;
        private System.Windows.Forms.Label label28;
        private MGui.Controls.CtlTextBox _txtPathSubtitle;
        private System.Windows.Forms.Label label29;
        private MGui.Controls.CtlTextBox _txtPathTitle;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Controls.CtlButton _btnCancel;
        private Controls.CtlButton _btnOk;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.NumericUpDown _numThumbnail;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label42;
        private MGui.Controls.CtlColourEditor _btnColourBackground;
        private MGui.Controls.CtlColourEditor _btnColourPreviewBackground;
        private MGui.Controls.CtlColourEditor _btnColourUntypedElements;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private Controls.CtlTitleBar ctlTitleBar1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.BindingSource coreOptionsBindingSource;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem resetToDefaultToolStripMenuItem;
        private System.Windows.Forms.Label label45;
        private Controls.CtlButton _btnEditColumns;
        private Controls.CtlButton _btnEditDefaults;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.CheckBox _chkPeakMinMax;
        private System.Windows.Forms.CheckBox _chkGroupNames;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.Label label49;
        private MGui.Controls.CtlColourEditor _btnHhMax;
        private System.Windows.Forms.Label label51;
        private MGui.Controls.CtlColourEditor _btnHhOor;
        private MGui.Controls.CtlColourEditor _btnHhMin;
        private MGui.Controls.CtlColourEditor _btnHhNan;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label48;
        private MGui.Controls.CtlTextBox _txtClusterXRange;
        private MGui.Controls.CtlTextBox _txtClusterYRange;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label52;
        private MGui.Controls.CtlTextBox _txtPeakXRange;
        private MGui.Controls.CtlTextBox _txtPeakYRange;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Label label56;
        private MGui.Controls.CtlTextBox _txtCompXRange;
        private MGui.Controls.CtlTextBox _txtCompYRange;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.Label label58;
        private MGui.Controls.CtlTextBox _txtPathXRange;
        private MGui.Controls.CtlTextBox _txtPathYRange;
    }
}