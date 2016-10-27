using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Gui.Controls;

namespace MetaboliteLevels.Gui.Forms.Editing
{
    partial class FrmEvaluateClusteringOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEvaluateClusteringOptions));
            this._lstParameters = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this._numNumTimes = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this._txtAlgorithm = new MGui.Controls.CtlTextBox();
            this._btnSetAlgorithm = new CtlButton();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this._txtStatistics = new MGui.Controls.CtlTextBox();
            this._btnStatistics = new CtlButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._btnOk = new CtlButton();
            this._btnCancel = new CtlButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this._btnAddRepeats = new CtlButton();
            this.label6 = new System.Windows.Forms.Label();
            this._btnAddRange = new CtlButton();
            this._numFrom = new System.Windows.Forms.NumericUpDown();
            this._numTo = new System.Windows.Forms.NumericUpDown();
            this._numStep = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this._txtValues = new MGui.Controls.CtlTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this._numCount = new System.Windows.Forms.NumericUpDown();
            this._txtValue = new MGui.Controls.CtlTextBox();
            this._btnClear = new CtlButton();
            this.label11 = new System.Windows.Forms.Label();
            this._txtNumberOfValues = new MGui.Controls.CtlTextBox();
            this.ctlTitleBar1 = new CtlTitleBar();
            this._checker = new MGui.Controls.CtlError(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._numNumTimes)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._numFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._numTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._numStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._numCount)).BeginInit();
            this.SuspendLayout();
            // 
            // _lstParameters
            // 
            this._lstParameters.Dock = System.Windows.Forms.DockStyle.Top;
            this._lstParameters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstParameters.FormattingEnabled = true;
            this._lstParameters.Location = new System.Drawing.Point(24, 143);
            this._lstParameters.Margin = new System.Windows.Forms.Padding(24, 8, 24, 8);
            this._lstParameters.Name = "_lstParameters";
            this._lstParameters.Size = new System.Drawing.Size(956, 29);
            this._lstParameters.TabIndex = 0;
            this._lstParameters.SelectedIndexChanged += new System.EventHandler(this._something_Changed);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this._numNumTimes, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this._lstParameters, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 0, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 87);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 11;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1004, 740);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 188);
            this.label2.Margin = new System.Windows.Forms.Padding(24, 8, 8, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(374, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "Which values do you want to use? Enter one per line.";
            // 
            // _numNumTimes
            // 
            this._numNumTimes.Dock = System.Windows.Forms.DockStyle.Top;
            this._numNumTimes.Location = new System.Drawing.Point(24, 565);
            this._numNumTimes.Margin = new System.Windows.Forms.Padding(24, 8, 24, 8);
            this._numNumTimes.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this._numNumTimes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._numNumTimes.Name = "_numNumTimes";
            this._numNumTimes.Size = new System.Drawing.Size(956, 29);
            this._numNumTimes.TabIndex = 4;
            this._numNumTimes.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._numNumTimes.ValueChanged += new System.EventHandler(this._something_Changed);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.Controls.Add(this._txtAlgorithm, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this._btnSetAlgorithm, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 53);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1004, 45);
            this.tableLayoutPanel4.TabIndex = 5;
            // 
            // _txtAlgorithm
            // 
            this._txtAlgorithm.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtAlgorithm.Location = new System.Drawing.Point(24, 8);
            this._txtAlgorithm.Margin = new System.Windows.Forms.Padding(24, 8, 8, 8);
            this._txtAlgorithm.Name = "_txtAlgorithm";
            this._txtAlgorithm.ReadOnly = true;
            this._txtAlgorithm.Size = new System.Drawing.Size(919, 29);
            this._txtAlgorithm.TabIndex = 7;
            this._txtAlgorithm.Watermark = null;
            // 
            // _btnSetAlgorithm
            // 
            this._btnSetAlgorithm.Image = global::MetaboliteLevels.Properties.Resources.MnuEnlargeList;
            this._btnSetAlgorithm.Location = new System.Drawing.Point(951, 8);
            this._btnSetAlgorithm.Margin = new System.Windows.Forms.Padding(0, 8, 24, 8);
            this._btnSetAlgorithm.Name = "_btnSetAlgorithm";
            this._btnSetAlgorithm.Size = new System.Drawing.Size(29, 29);
            this._btnSetAlgorithm.TabIndex = 8;
            this._btnSetAlgorithm.UseDefaultSize = true;
            this._btnSetAlgorithm.UseVisualStyleBackColor = true;
            this._btnSetAlgorithm.Click += new System.EventHandler(this._btnSetAlgorithm_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this._txtStatistics, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this._btnStatistics, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 639);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1004, 45);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // _txtStatistics
            // 
            this._txtStatistics.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtStatistics.Location = new System.Drawing.Point(24, 8);
            this._txtStatistics.Margin = new System.Windows.Forms.Padding(24, 8, 8, 8);
            this._txtStatistics.Name = "_txtStatistics";
            this._txtStatistics.ReadOnly = true;
            this._txtStatistics.Size = new System.Drawing.Size(919, 29);
            this._txtStatistics.TabIndex = 7;
            this._txtStatistics.Watermark = null;
            // 
            // _btnStatistics
            // 
            this._btnStatistics.Image = global::MetaboliteLevels.Properties.Resources.MnuEnlargeList;
            this._btnStatistics.Location = new System.Drawing.Point(951, 8);
            this._btnStatistics.Margin = new System.Windows.Forms.Padding(0, 8, 24, 8);
            this._btnStatistics.Name = "_btnStatistics";
            this._btnStatistics.Size = new System.Drawing.Size(29, 29);
            this._btnStatistics.TabIndex = 8;
            this._btnStatistics.UseDefaultSize = true;
            this._btnStatistics.UseVisualStyleBackColor = true;
            this._btnStatistics.Click += new System.EventHandler(this._btnSetAlgorithm_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 106);
            this.label1.Margin = new System.Windows.Forms.Padding(24, 8, 8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(293, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "What parameter do you want to explore?";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 528);
            this.label3.Margin = new System.Windows.Forms.Padding(24, 8, 8, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(335, 21);
            this.label3.TabIndex = 3;
            this.label3.Text = "How many times do you want to run each test?";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this._btnOk);
            this.flowLayoutPanel1.Controls.Add(this._btnCancel);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(716, 684);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(288, 56);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // _btnOk
            // 
            this._btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._btnOk.Enabled = false;
            this._btnOk.Image = global::MetaboliteLevels.Properties.Resources.MnuAccept;
            this._btnOk.Location = new System.Drawing.Point(8, 8);
            this._btnOk.Margin = new System.Windows.Forms.Padding(8);
            this._btnOk.Name = "_btnOk";
            this._btnOk.Size = new System.Drawing.Size(128, 40);
            this._btnOk.TabIndex = 0;
            this._btnOk.Text = "OK";
            this._btnOk.UseDefaultSize = true;
            this._btnOk.UseVisualStyleBackColor = true;
            // 
            // _btnCancel
            // 
            this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnCancel.Image = global::MetaboliteLevels.Properties.Resources.MnuCancel;
            this._btnCancel.Location = new System.Drawing.Point(152, 8);
            this._btnCancel.Margin = new System.Windows.Forms.Padding(8);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(128, 40);
            this._btnCancel.TabIndex = 1;
            this._btnCancel.Text = "Cancel";
            this._btnCancel.UseDefaultSize = true;
            this._btnCancel.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 610);
            this.label4.Margin = new System.Windows.Forms.Padding(24, 8, 8, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(296, 21);
            this.label4.TabIndex = 3;
            this.label4.Text = "Which statistics do you want to calculate?";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 24);
            this.label5.Margin = new System.Windows.Forms.Padding(24, 24, 8, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(269, 21);
            this.label5.TabIndex = 3;
            this.label5.Text = "Which algorithm do you want to use?";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 4;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.Controls.Add(this._btnAddRepeats, 1, 3);
            this.tableLayoutPanel5.Controls.Add(this.label6, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this._btnAddRange, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this._numFrom, 3, 0);
            this.tableLayoutPanel5.Controls.Add(this._numTo, 3, 1);
            this.tableLayoutPanel5.Controls.Add(this._numStep, 3, 2);
            this.tableLayoutPanel5.Controls.Add(this.label7, 2, 1);
            this.tableLayoutPanel5.Controls.Add(this.label8, 2, 2);
            this.tableLayoutPanel5.Controls.Add(this._txtValues, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.label9, 2, 3);
            this.tableLayoutPanel5.Controls.Add(this.label10, 2, 4);
            this.tableLayoutPanel5.Controls.Add(this._numCount, 3, 4);
            this.tableLayoutPanel5.Controls.Add(this._txtValue, 3, 3);
            this.tableLayoutPanel5.Controls.Add(this._btnClear, 1, 5);
            this.tableLayoutPanel5.Controls.Add(this.label11, 2, 5);
            this.tableLayoutPanel5.Controls.Add(this._txtNumberOfValues, 3, 5);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 217);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 7;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1004, 303);
            this.tableLayoutPanel5.TabIndex = 6;
            // 
            // _btnAddRepeats
            // 
            this._btnAddRepeats.Dock = System.Windows.Forms.DockStyle.Fill;
            this._btnAddRepeats.Image = global::MetaboliteLevels.Properties.Resources.MnuMoveToList;
            this._btnAddRepeats.Location = new System.Drawing.Point(652, 143);
            this._btnAddRepeats.Margin = new System.Windows.Forms.Padding(8);
            this._btnAddRepeats.Name = "_btnAddRepeats";
            this.tableLayoutPanel5.SetRowSpan(this._btnAddRepeats, 2);
            this._btnAddRepeats.Size = new System.Drawing.Size(128, 74);
            this._btnAddRepeats.TabIndex = 7;
            this._btnAddRepeats.Text = "Repeats";
            this._btnAddRepeats.UseDefaultSize = true;
            this._btnAddRepeats.Click += new System.EventHandler(this._btnAddRepeats_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(796, 8);
            this.label6.Margin = new System.Windows.Forms.Padding(8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 21);
            this.label6.TabIndex = 3;
            this.label6.Text = "From";
            // 
            // _btnAddRange
            // 
            this._btnAddRange.Dock = System.Windows.Forms.DockStyle.Fill;
            this._btnAddRange.Image = global::MetaboliteLevels.Properties.Resources.MnuMoveToList;
            this._btnAddRange.Location = new System.Drawing.Point(652, 8);
            this._btnAddRange.Margin = new System.Windows.Forms.Padding(8);
            this._btnAddRange.Name = "_btnAddRange";
            this.tableLayoutPanel5.SetRowSpan(this._btnAddRange, 3);
            this._btnAddRange.Size = new System.Drawing.Size(128, 119);
            this._btnAddRange.TabIndex = 1;
            this._btnAddRange.Text = "Range";
            this._btnAddRange.UseDefaultSize = true;
            this._btnAddRange.Click += new System.EventHandler(this._btnAddRange_Click);
            // 
            // _numFrom
            // 
            this._numFrom.Location = new System.Drawing.Point(868, 8);
            this._numFrom.Margin = new System.Windows.Forms.Padding(8);
            this._numFrom.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this._numFrom.MaximumSize = new System.Drawing.Size(128, 0);
            this._numFrom.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._numFrom.MinimumSize = new System.Drawing.Size(128, 0);
            this._numFrom.Name = "_numFrom";
            this._numFrom.Size = new System.Drawing.Size(128, 29);
            this._numFrom.TabIndex = 5;
            this._numFrom.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // _numTo
            // 
            this._numTo.Location = new System.Drawing.Point(868, 53);
            this._numTo.Margin = new System.Windows.Forms.Padding(8);
            this._numTo.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this._numTo.MaximumSize = new System.Drawing.Size(128, 0);
            this._numTo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._numTo.MinimumSize = new System.Drawing.Size(128, 0);
            this._numTo.Name = "_numTo";
            this._numTo.Size = new System.Drawing.Size(128, 29);
            this._numTo.TabIndex = 5;
            this._numTo.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // _numStep
            // 
            this._numStep.Location = new System.Drawing.Point(868, 98);
            this._numStep.Margin = new System.Windows.Forms.Padding(8);
            this._numStep.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this._numStep.MaximumSize = new System.Drawing.Size(128, 0);
            this._numStep.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._numStep.MinimumSize = new System.Drawing.Size(128, 0);
            this._numStep.Name = "_numStep";
            this._numStep.Size = new System.Drawing.Size(128, 29);
            this._numStep.TabIndex = 5;
            this._numStep.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(796, 53);
            this.label7.Margin = new System.Windows.Forms.Padding(8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(27, 21);
            this.label7.TabIndex = 3;
            this.label7.Text = "To";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(796, 98);
            this.label8.Margin = new System.Windows.Forms.Padding(8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 21);
            this.label8.TabIndex = 3;
            this.label8.Text = "Step";
            // 
            // _txtValues
            // 
            this._txtValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this._txtValues.Location = new System.Drawing.Point(24, 8);
            this._txtValues.Margin = new System.Windows.Forms.Padding(24, 8, 8, 8);
            this._txtValues.Multiline = true;
            this._txtValues.Name = "_txtValues";
            this.tableLayoutPanel5.SetRowSpan(this._txtValues, 7);
            this._txtValues.Size = new System.Drawing.Size(612, 287);
            this._txtValues.TabIndex = 6;
            this._txtValues.Watermark = null;
            this._txtValues.TextChanged += new System.EventHandler(this._something_Changed);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(796, 143);
            this.label9.Margin = new System.Windows.Forms.Padding(8);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 21);
            this.label9.TabIndex = 3;
            this.label9.Text = "Value";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(796, 188);
            this.label10.Margin = new System.Windows.Forms.Padding(8);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(52, 21);
            this.label10.TabIndex = 3;
            this.label10.Text = "Count";
            // 
            // _numCount
            // 
            this._numCount.Location = new System.Drawing.Point(868, 188);
            this._numCount.Margin = new System.Windows.Forms.Padding(8);
            this._numCount.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this._numCount.MaximumSize = new System.Drawing.Size(128, 0);
            this._numCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._numCount.MinimumSize = new System.Drawing.Size(128, 0);
            this._numCount.Name = "_numCount";
            this._numCount.Size = new System.Drawing.Size(128, 29);
            this._numCount.TabIndex = 5;
            this._numCount.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // _txtValue
            // 
            this._txtValue.Location = new System.Drawing.Point(868, 143);
            this._txtValue.Margin = new System.Windows.Forms.Padding(8);
            this._txtValue.MaximumSize = new System.Drawing.Size(128, 4);
            this._txtValue.MinimumSize = new System.Drawing.Size(128, 4);
            this._txtValue.Name = "_txtValue";
            this._txtValue.Size = new System.Drawing.Size(128, 4);
            this._txtValue.TabIndex = 8;
            this._txtValue.Text = "1";
            this._txtValue.Watermark = null;
            // 
            // _btnClear
            // 
            this._btnClear.Dock = System.Windows.Forms.DockStyle.Fill;
            this._btnClear.Image = global::MetaboliteLevels.Properties.Resources.MnuClear;
            this._btnClear.Location = new System.Drawing.Point(652, 233);
            this._btnClear.Margin = new System.Windows.Forms.Padding(8);
            this._btnClear.Name = "_btnClear";
            this._btnClear.Size = new System.Drawing.Size(128, 40);
            this._btnClear.TabIndex = 7;
            this._btnClear.Text = "Clear";
            this._btnClear.UseDefaultSize = true;
            this._btnClear.Click += new System.EventHandler(this._btnClear_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(796, 233);
            this.label11.Margin = new System.Windows.Forms.Padding(8);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(56, 21);
            this.label11.TabIndex = 9;
            this.label11.Text = "Values";
            // 
            // _txtNumberOfValues
            // 
            this._txtNumberOfValues.Location = new System.Drawing.Point(868, 233);
            this._txtNumberOfValues.Margin = new System.Windows.Forms.Padding(8);
            this._txtNumberOfValues.MaximumSize = new System.Drawing.Size(128, 4);
            this._txtNumberOfValues.MinimumSize = new System.Drawing.Size(128, 4);
            this._txtNumberOfValues.Name = "_txtNumberOfValues";
            this._txtNumberOfValues.ReadOnly = true;
            this._txtNumberOfValues.Size = new System.Drawing.Size(128, 4);
            this._txtNumberOfValues.TabIndex = 8;
            this._txtNumberOfValues.Text = "?";
            this._txtNumberOfValues.Watermark = null;
            // 
            // ctlTitleBar1
            // 
            this.ctlTitleBar1.AutoSize = true;
            this.ctlTitleBar1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ctlTitleBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlTitleBar1.HelpText = resources.GetString("ctlTitleBar1.HelpText");
            this.ctlTitleBar1.Location = new System.Drawing.Point(0, 0);
            this.ctlTitleBar1.MinimumSize = new System.Drawing.Size(256, 0);
            this.ctlTitleBar1.Name = "ctlTitleBar1";
            this.ctlTitleBar1.Size = new System.Drawing.Size(1004, 87);
            this.ctlTitleBar1.SubText = "Test clustering performance by trying various parameter values";
            this.ctlTitleBar1.TabIndex = 3;
            this.ctlTitleBar1.Text = "Explore clustering parameters";
            this.ctlTitleBar1.WarningText = null;
            // 
            // FrmEvaluateClusteringOptions
            // 
            this.AcceptButton = this._btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._btnCancel;
            this.ClientSize = new System.Drawing.Size(1004, 827);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.ctlTitleBar1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmEvaluateClusteringOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Evaluate Clustering";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._numNumTimes)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._numFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._numTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._numStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._numCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox _lstParameters;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Controls.CtlButton _btnOk;
        private Controls.CtlButton _btnCancel;
        private Controls.CtlTitleBar ctlTitleBar1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private MGui.Controls.CtlTextBox _txtAlgorithm;
        private Controls.CtlButton _btnSetAlgorithm;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private MGui.Controls.CtlTextBox _txtStatistics;
        private Controls.CtlButton _btnStatistics;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown _numNumTimes;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown _numFrom;
        private System.Windows.Forms.NumericUpDown _numTo;
        private System.Windows.Forms.NumericUpDown _numStep;
        private Controls.CtlButton _btnAddRepeats;
        private Controls.CtlButton _btnAddRange;
        private System.Windows.Forms.Label label8;
        private MGui.Controls.CtlTextBox _txtValues;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown _numCount;
        private MGui.Controls.CtlTextBox _txtValue;
        private Controls.CtlButton _btnClear;
        private System.Windows.Forms.Label label11;
        private MGui.Controls.CtlTextBox _txtNumberOfValues;
        private MGui.Controls.CtlError _checker;
    }
}