namespace MetaboliteLevels.Forms.Algorithms
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
            this._lstParameters = new System.Windows.Forms.ComboBox();
            this._txtValues = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this._txtAlgorithm = new System.Windows.Forms.TextBox();
            this._btnSetAlgorithm = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this._btnCreateRange = new System.Windows.Forms.LinkLabel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this._txtStatistics = new System.Windows.Forms.TextBox();
            this._btnStatistics = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._btnOk = new MetaboliteLevels.Controls.CtlButton();
            this._btnCancel = new MetaboliteLevels.Controls.CtlButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ctlTitleBar1 = new MetaboliteLevels.Controls.CtlTitleBar();
            this._numNumTimes = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._numNumTimes)).BeginInit();
            this.SuspendLayout();
            // 
            // _lstParameters
            // 
            this._lstParameters.Dock = System.Windows.Forms.DockStyle.Top;
            this._lstParameters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstParameters.FormattingEnabled = true;
            this._lstParameters.Location = new System.Drawing.Point(24, 144);
            this._lstParameters.Margin = new System.Windows.Forms.Padding(24, 8, 24, 8);
            this._lstParameters.Name = "_lstParameters";
            this._lstParameters.Size = new System.Drawing.Size(864, 29);
            this._lstParameters.TabIndex = 0;
            this._lstParameters.SelectedIndexChanged += new System.EventHandler(this._lstParameters_SelectedIndexChanged);
            // 
            // _txtValues
            // 
            this._txtValues.AcceptsReturn = true;
            this._txtValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this._txtValues.Location = new System.Drawing.Point(24, 226);
            this._txtValues.Margin = new System.Windows.Forms.Padding(24, 8, 24, 8);
            this._txtValues.Multiline = true;
            this._txtValues.Name = "_txtValues";
            this._txtValues.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this._txtValues.Size = new System.Drawing.Size(864, 235);
            this._txtValues.TabIndex = 1;
            this._txtValues.TextChanged += new System.EventHandler(this._lstParameters_SelectedIndexChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this._numNumTimes, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this._lstParameters, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this._txtValues, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 66);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 11;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(912, 709);
            this.tableLayoutPanel1.TabIndex = 2;
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
            this.tableLayoutPanel4.Size = new System.Drawing.Size(912, 46);
            this.tableLayoutPanel4.TabIndex = 5;
            // 
            // _txtAlgorithm
            // 
            this._txtAlgorithm.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtAlgorithm.Location = new System.Drawing.Point(24, 8);
            this._txtAlgorithm.Margin = new System.Windows.Forms.Padding(24, 8, 8, 8);
            this._txtAlgorithm.Name = "_txtAlgorithm";
            this._txtAlgorithm.ReadOnly = true;
            this._txtAlgorithm.Size = new System.Drawing.Size(826, 29);
            this._txtAlgorithm.TabIndex = 7;
            // 
            // _btnSetAlgorithm
            // 
            this._btnSetAlgorithm.Image = global::MetaboliteLevels.Properties.Resources.MnuEnlargeList;
            this._btnSetAlgorithm.Location = new System.Drawing.Point(858, 8);
            this._btnSetAlgorithm.Margin = new System.Windows.Forms.Padding(0, 8, 24, 8);
            this._btnSetAlgorithm.Name = "_btnSetAlgorithm";
            this._btnSetAlgorithm.Size = new System.Drawing.Size(30, 30);
            this._btnSetAlgorithm.TabIndex = 8;
            this._btnSetAlgorithm.UseVisualStyleBackColor = true;
            this._btnSetAlgorithm.Click += new System.EventHandler(this._btnSetAlgorithm_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this._btnCreateRange, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 181);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(912, 37);
            this.tableLayoutPanel3.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 8);
            this.label2.Margin = new System.Windows.Forms.Padding(24, 8, 8, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(374, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "Which values do you want to use? Enter one per line.";
            // 
            // _btnCreateRange
            // 
            this._btnCreateRange.AutoSize = true;
            this._btnCreateRange.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this._btnCreateRange.Location = new System.Drawing.Point(789, 8);
            this._btnCreateRange.Margin = new System.Windows.Forms.Padding(8, 8, 24, 8);
            this._btnCreateRange.Name = "_btnCreateRange";
            this._btnCreateRange.Size = new System.Drawing.Size(99, 21);
            this._btnCreateRange.TabIndex = 4;
            this._btnCreateRange.TabStop = true;
            this._btnCreateRange.Text = "Create range";
            this._btnCreateRange.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._btnCreateRange_LinkClicked);
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
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 613);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(912, 46);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // _txtStatistics
            // 
            this._txtStatistics.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtStatistics.Location = new System.Drawing.Point(24, 8);
            this._txtStatistics.Margin = new System.Windows.Forms.Padding(24, 8, 8, 8);
            this._txtStatistics.Name = "_txtStatistics";
            this._txtStatistics.ReadOnly = true;
            this._txtStatistics.Size = new System.Drawing.Size(826, 29);
            this._txtStatistics.TabIndex = 7;
            this._txtStatistics.TextChanged += new System.EventHandler(this._txtStatistics_TextChanged);
            // 
            // _btnStatistics
            // 
            this._btnStatistics.Image = global::MetaboliteLevels.Properties.Resources.MnuEnlargeList;
            this._btnStatistics.Location = new System.Drawing.Point(858, 8);
            this._btnStatistics.Margin = new System.Windows.Forms.Padding(0, 8, 24, 8);
            this._btnStatistics.Name = "_btnStatistics";
            this._btnStatistics.Size = new System.Drawing.Size(30, 30);
            this._btnStatistics.TabIndex = 8;
            this._btnStatistics.UseVisualStyleBackColor = true;
            this._btnStatistics.Click += new System.EventHandler(this._btnStatistics_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 107);
            this.label1.Margin = new System.Windows.Forms.Padding(24, 8, 8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(293, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "What parameter do you want to explore?";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 477);
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
            this.flowLayoutPanel1.Location = new System.Drawing.Point(616, 661);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(296, 48);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // _btnOk
            // 
            this._btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._btnOk.Enabled = false;
            this._btnOk.Image = global::MetaboliteLevels.Properties.Resources.MnuAccept;
            this._btnOk.Location = new System.Drawing.Point(8, 24);
            this._btnOk.Margin = new System.Windows.Forms.Padding(8, 24, 8, 24);
            this._btnOk.Name = "_btnOk";
            this._btnOk.Size = new System.Drawing.Size(128, 40);
            this._btnOk.TabIndex = 0;
            this._btnOk.Text = "Run test";
            this._btnOk.UseDefaultSize = true;
            this._btnOk.UseVisualStyleBackColor = true;
            // 
            // _btnCancel
            // 
            this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnCancel.Image = global::MetaboliteLevels.Properties.Resources.MnuCancel;
            this._btnCancel.Location = new System.Drawing.Point(144, 24);
            this._btnCancel.Margin = new System.Windows.Forms.Padding(0, 24, 24, 24);
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
            this.label4.Location = new System.Drawing.Point(24, 573);
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
            // ctlTitleBar1
            // 
            this.ctlTitleBar1.AutoSize = true;
            this.ctlTitleBar1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ctlTitleBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlTitleBar1.HelpText = null;
            this.ctlTitleBar1.Location = new System.Drawing.Point(0, 0);
            this.ctlTitleBar1.MinimumSize = new System.Drawing.Size(256, 0);
            this.ctlTitleBar1.Name = "ctlTitleBar1";
            this.ctlTitleBar1.Size = new System.Drawing.Size(912, 66);
            this.ctlTitleBar1.SubText = "";
            this.ctlTitleBar1.TabIndex = 3;
            this.ctlTitleBar1.Text = "Explore clustering parameters";
            this.ctlTitleBar1.WarningText = null;
            // 
            // _numNumTimes
            // 
            this._numNumTimes.Dock = System.Windows.Forms.DockStyle.Top;
            this._numNumTimes.Location = new System.Drawing.Point(24, 525);
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
            this._numNumTimes.Size = new System.Drawing.Size(864, 29);
            this._numNumTimes.TabIndex = 4;
            this._numNumTimes.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // FrmEvaluateClusteringOptions
            // 
            this.AcceptButton = this._btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._btnCancel;
            this.ClientSize = new System.Drawing.Size(912, 775);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.ctlTitleBar1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmEvaluateClusteringOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Evaluate Clustering";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._numNumTimes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox _lstParameters;
        private System.Windows.Forms.TextBox _txtValues;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Controls.CtlButton _btnOk;
        private Controls.CtlButton _btnCancel;
        private Controls.CtlTitleBar ctlTitleBar1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.LinkLabel _btnCreateRange;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TextBox _txtAlgorithm;
        private System.Windows.Forms.Button _btnSetAlgorithm;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox _txtStatistics;
        private System.Windows.Forms.Button _btnStatistics;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown _numNumTimes;
    }
}