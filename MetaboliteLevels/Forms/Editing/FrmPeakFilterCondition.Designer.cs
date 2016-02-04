namespace MetaboliteLevels.Forms.Editing
{
    partial class FrmPeakFilterCondition
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPeakFilterCondition));
            this._lstIsStatistic = new System.Windows.Forms.ComboBox();
            this._lstStatisticComparator = new System.Windows.Forms.ComboBox();
            this._txtStatisticValue = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this._btnCancel = new MetaboliteLevels.Controls.CtlButton();
            this._btnOk = new MetaboliteLevels.Controls.CtlButton();
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this._lblPreviewTitle = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._lblSigPeaks = new System.Windows.Forms.Label();
            this._lblInsigPeaks = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this._chkIsStatistic = new System.Windows.Forms.RadioButton();
            this._chkIsFlaggedWith = new System.Windows.Forms.RadioButton();
            this._chkIsInCluster = new System.Windows.Forms.RadioButton();
            this._chkIsInSet = new System.Windows.Forms.RadioButton();
            this._txtIsInSet = new System.Windows.Forms.TextBox();
            this._btnIsInSet = new System.Windows.Forms.Button();
            this._txtIsInCluster = new System.Windows.Forms.TextBox();
            this._btnIsInCluster = new System.Windows.Forms.Button();
            this._txtIsFlaggedWith = new System.Windows.Forms.TextBox();
            this._btnIsFlaggedWith = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._radAnd = new System.Windows.Forms.RadioButton();
            this._radOr = new System.Windows.Forms.RadioButton();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this._lstFlagComparator = new System.Windows.Forms.ComboBox();
            this._lstClusterComparator = new System.Windows.Forms.ComboBox();
            this._lstPeakComparator = new System.Windows.Forms.ComboBox();
            this._radFilter = new System.Windows.Forms.RadioButton();
            this._lstFilter = new System.Windows.Forms.ComboBox();
            this._lstFilterOp = new System.Windows.Forms.ComboBox();
            this.ctlTitleBar1 = new MetaboliteLevels.Controls.CtlTitleBar();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.flowLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _lstIsStatistic
            // 
            this._lstIsStatistic.Dock = System.Windows.Forms.DockStyle.Top;
            this._lstIsStatistic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstIsStatistic.FormattingEnabled = true;
            this._lstIsStatistic.Location = new System.Drawing.Point(122, 245);
            this._lstIsStatistic.Margin = new System.Windows.Forms.Padding(8);
            this._lstIsStatistic.Name = "_lstIsStatistic";
            this._lstIsStatistic.Size = new System.Drawing.Size(239, 29);
            this._lstIsStatistic.TabIndex = 1;
            this._lstIsStatistic.SelectedIndexChanged += new System.EventHandler(this._txtComp_TextChanged);
            // 
            // _lstStatisticComparator
            // 
            this._lstStatisticComparator.Dock = System.Windows.Forms.DockStyle.Top;
            this._lstStatisticComparator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstStatisticComparator.FormattingEnabled = true;
            this._lstStatisticComparator.Location = new System.Drawing.Point(377, 245);
            this._lstStatisticComparator.Margin = new System.Windows.Forms.Padding(8);
            this._lstStatisticComparator.Name = "_lstStatisticComparator";
            this._lstStatisticComparator.Size = new System.Drawing.Size(239, 29);
            this._lstStatisticComparator.TabIndex = 1;
            this._lstStatisticComparator.SelectedIndexChanged += new System.EventHandler(this._txtComp_TextChanged);
            // 
            // _txtStatisticValue
            // 
            this._txtStatisticValue.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtStatisticValue.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._txtStatisticValue.Location = new System.Drawing.Point(632, 245);
            this._txtStatisticValue.Margin = new System.Windows.Forms.Padding(8);
            this._txtStatisticValue.Name = "_txtStatisticValue";
            this._txtStatisticValue.Size = new System.Drawing.Size(239, 30);
            this._txtStatisticValue.TabIndex = 2;
            this._txtStatisticValue.TextChanged += new System.EventHandler(this._txtComp_TextChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel4, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel5, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 87);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(926, 444);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel4.AutoSize = true;
            this.flowLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel4.Controls.Add(this._btnCancel);
            this.flowLayoutPanel4.Controls.Add(this._btnOk);
            this.flowLayoutPanel4.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel4.Location = new System.Drawing.Point(638, 388);
            this.flowLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(288, 56);
            this.flowLayoutPanel4.TabIndex = 4;
            this.flowLayoutPanel4.WrapContents = false;
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
            this._btnCancel.Click += new System.EventHandler(this.button2_Click);
            // 
            // _btnOk
            // 
            this._btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._btnOk.Image = global::MetaboliteLevels.Properties.Resources.MnuAccept;
            this._btnOk.Location = new System.Drawing.Point(8, 8);
            this._btnOk.Margin = new System.Windows.Forms.Padding(8);
            this._btnOk.Name = "_btnOk";
            this._btnOk.Size = new System.Drawing.Size(128, 40);
            this._btnOk.TabIndex = 0;
            this._btnOk.Text = "OK";
            this._btnOk.UseDefaultSize = true;
            this._btnOk.UseVisualStyleBackColor = true;
            this._btnOk.Click += new System.EventHandler(this._btnOk_Click);
            // 
            // flowLayoutPanel5
            // 
            this.flowLayoutPanel5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.flowLayoutPanel5.AutoSize = true;
            this.flowLayoutPanel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel5.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel5.Controls.Add(this._lblPreviewTitle);
            this.flowLayoutPanel5.Controls.Add(this.label2);
            this.flowLayoutPanel5.Controls.Add(this._lblSigPeaks);
            this.flowLayoutPanel5.Controls.Add(this._lblInsigPeaks);
            this.flowLayoutPanel5.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel5.ForeColor = System.Drawing.Color.Black;
            this.flowLayoutPanel5.Location = new System.Drawing.Point(8, 342);
            this.flowLayoutPanel5.Margin = new System.Windows.Forms.Padding(8);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.flowLayoutPanel5.Size = new System.Drawing.Size(257, 94);
            this.flowLayoutPanel5.TabIndex = 4;
            this.flowLayoutPanel5.WrapContents = false;
            // 
            // _lblPreviewTitle
            // 
            this._lblPreviewTitle.AutoSize = true;
            this._lblPreviewTitle.BackColor = System.Drawing.Color.LightSteelBlue;
            this._lblPreviewTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this._lblPreviewTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblPreviewTitle.ForeColor = System.Drawing.Color.Black;
            this._lblPreviewTitle.Location = new System.Drawing.Point(0, 0);
            this._lblPreviewTitle.Margin = new System.Windows.Forms.Padding(0);
            this._lblPreviewTitle.Name = "_lblPreviewTitle";
            this._lblPreviewTitle.Padding = new System.Windows.Forms.Padding(4);
            this._lblPreviewTitle.Size = new System.Drawing.Size(255, 29);
            this._lblPreviewTitle.TabIndex = 3;
            this._lblPreviewTitle.Text = "Results (this rule alone)";
            this._lblPreviewTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(3, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 21);
            this.label2.TabIndex = 4;
            this.label2.Text = "Errors";
            // 
            // _lblSigPeaks
            // 
            this._lblSigPeaks.AutoSize = true;
            this._lblSigPeaks.Location = new System.Drawing.Point(3, 50);
            this._lblSigPeaks.Name = "_lblSigPeaks";
            this._lblSigPeaks.Size = new System.Drawing.Size(249, 21);
            this._lblSigPeaks.TabIndex = 1;
            this._lblSigPeaks.Text = "Passes XXXXXXXXXXXXXXXXXXXXX";
            // 
            // _lblInsigPeaks
            // 
            this._lblInsigPeaks.AutoSize = true;
            this._lblInsigPeaks.Location = new System.Drawing.Point(3, 71);
            this._lblInsigPeaks.Name = "_lblInsigPeaks";
            this._lblInsigPeaks.Size = new System.Drawing.Size(41, 21);
            this._lblInsigPeaks.TabIndex = 2;
            this._lblInsigPeaks.Text = "Fails";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel2, 2);
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this._txtStatisticValue, 3, 5);
            this.tableLayoutPanel2.Controls.Add(this._lstStatisticComparator, 2, 5);
            this.tableLayoutPanel2.Controls.Add(this._lstIsStatistic, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this._chkIsStatistic, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this._chkIsFlaggedWith, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this._chkIsInCluster, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this._chkIsInSet, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this._txtIsInSet, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this._btnIsInSet, 4, 1);
            this.tableLayoutPanel2.Controls.Add(this._txtIsInCluster, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this._btnIsInCluster, 4, 2);
            this.tableLayoutPanel2.Controls.Add(this._txtIsFlaggedWith, 2, 3);
            this.tableLayoutPanel2.Controls.Add(this._btnIsFlaggedWith, 4, 3);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this._lstFlagComparator, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this._lstClusterComparator, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this._lstPeakComparator, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this._radFilter, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this._lstFilter, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this._lstFilterOp, 3, 4);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 6;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(926, 286);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // _chkIsStatistic
            // 
            this._chkIsStatistic.AutoSize = true;
            this._chkIsStatistic.Location = new System.Drawing.Point(24, 245);
            this._chkIsStatistic.Margin = new System.Windows.Forms.Padding(24, 8, 8, 16);
            this._chkIsStatistic.Name = "_chkIsStatistic";
            this._chkIsStatistic.Size = new System.Drawing.Size(82, 25);
            this._chkIsStatistic.TabIndex = 0;
            this._chkIsStatistic.Text = "Statistic";
            this._chkIsStatistic.UseVisualStyleBackColor = true;
            this._chkIsStatistic.CheckedChanged += new System.EventHandler(this._txtComp_TextChanged);
            // 
            // _chkIsFlaggedWith
            // 
            this._chkIsFlaggedWith.AutoSize = true;
            this._chkIsFlaggedWith.Location = new System.Drawing.Point(24, 147);
            this._chkIsFlaggedWith.Margin = new System.Windows.Forms.Padding(24, 8, 8, 16);
            this._chkIsFlaggedWith.Name = "_chkIsFlaggedWith";
            this._chkIsFlaggedWith.Size = new System.Drawing.Size(64, 25);
            this._chkIsFlaggedWith.TabIndex = 0;
            this._chkIsFlaggedWith.Text = "Flags";
            this._chkIsFlaggedWith.UseVisualStyleBackColor = true;
            this._chkIsFlaggedWith.CheckedChanged += new System.EventHandler(this._txtComp_TextChanged);
            // 
            // _chkIsInCluster
            // 
            this._chkIsInCluster.AutoSize = true;
            this._chkIsInCluster.Location = new System.Drawing.Point(24, 98);
            this._chkIsInCluster.Margin = new System.Windows.Forms.Padding(24, 8, 8, 16);
            this._chkIsInCluster.Name = "_chkIsInCluster";
            this._chkIsInCluster.Size = new System.Drawing.Size(77, 25);
            this._chkIsInCluster.TabIndex = 0;
            this._chkIsInCluster.Text = "Cluster";
            this._chkIsInCluster.UseVisualStyleBackColor = true;
            this._chkIsInCluster.CheckedChanged += new System.EventHandler(this._txtComp_TextChanged);
            // 
            // _chkIsInSet
            // 
            this._chkIsInSet.AutoSize = true;
            this._chkIsInSet.Location = new System.Drawing.Point(24, 49);
            this._chkIsInSet.Margin = new System.Windows.Forms.Padding(24, 8, 8, 16);
            this._chkIsInSet.Name = "_chkIsInSet";
            this._chkIsInSet.Size = new System.Drawing.Size(61, 25);
            this._chkIsInSet.TabIndex = 0;
            this._chkIsInSet.Text = "Peak";
            this._chkIsInSet.UseVisualStyleBackColor = true;
            this._chkIsInSet.CheckedChanged += new System.EventHandler(this._txtComp_TextChanged);
            // 
            // _txtIsInSet
            // 
            this.tableLayoutPanel2.SetColumnSpan(this._txtIsInSet, 2);
            this._txtIsInSet.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtIsInSet.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._txtIsInSet.Location = new System.Drawing.Point(377, 49);
            this._txtIsInSet.Margin = new System.Windows.Forms.Padding(8);
            this._txtIsInSet.Name = "_txtIsInSet";
            this._txtIsInSet.Size = new System.Drawing.Size(494, 30);
            this._txtIsInSet.TabIndex = 4;
            this._txtIsInSet.TextChanged += new System.EventHandler(this._txtComp_TextChanged);
            // 
            // _btnIsInSet
            // 
            this._btnIsInSet.Image = ((System.Drawing.Image)(resources.GetObject("_btnIsInSet.Image")));
            this._btnIsInSet.Location = new System.Drawing.Point(887, 49);
            this._btnIsInSet.Margin = new System.Windows.Forms.Padding(8);
            this._btnIsInSet.Name = "_btnIsInSet";
            this._btnIsInSet.Size = new System.Drawing.Size(30, 30);
            this._btnIsInSet.TabIndex = 5;
            this._btnIsInSet.UseVisualStyleBackColor = true;
            // 
            // _txtIsInCluster
            // 
            this.tableLayoutPanel2.SetColumnSpan(this._txtIsInCluster, 2);
            this._txtIsInCluster.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtIsInCluster.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._txtIsInCluster.Location = new System.Drawing.Point(377, 98);
            this._txtIsInCluster.Margin = new System.Windows.Forms.Padding(8);
            this._txtIsInCluster.Name = "_txtIsInCluster";
            this._txtIsInCluster.Size = new System.Drawing.Size(494, 30);
            this._txtIsInCluster.TabIndex = 6;
            this._txtIsInCluster.TextChanged += new System.EventHandler(this._txtComp_TextChanged);
            // 
            // _btnIsInCluster
            // 
            this._btnIsInCluster.Image = ((System.Drawing.Image)(resources.GetObject("_btnIsInCluster.Image")));
            this._btnIsInCluster.Location = new System.Drawing.Point(887, 98);
            this._btnIsInCluster.Margin = new System.Windows.Forms.Padding(8);
            this._btnIsInCluster.Name = "_btnIsInCluster";
            this._btnIsInCluster.Size = new System.Drawing.Size(30, 30);
            this._btnIsInCluster.TabIndex = 5;
            this._btnIsInCluster.UseVisualStyleBackColor = true;
            // 
            // _txtIsFlaggedWith
            // 
            this.tableLayoutPanel2.SetColumnSpan(this._txtIsFlaggedWith, 2);
            this._txtIsFlaggedWith.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtIsFlaggedWith.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._txtIsFlaggedWith.Location = new System.Drawing.Point(377, 147);
            this._txtIsFlaggedWith.Margin = new System.Windows.Forms.Padding(8);
            this._txtIsFlaggedWith.Name = "_txtIsFlaggedWith";
            this._txtIsFlaggedWith.Size = new System.Drawing.Size(494, 30);
            this._txtIsFlaggedWith.TabIndex = 7;
            this._txtIsFlaggedWith.TextChanged += new System.EventHandler(this._txtComp_TextChanged);
            // 
            // _btnIsFlaggedWith
            // 
            this._btnIsFlaggedWith.Image = ((System.Drawing.Image)(resources.GetObject("_btnIsFlaggedWith.Image")));
            this._btnIsFlaggedWith.Location = new System.Drawing.Point(887, 147);
            this._btnIsFlaggedWith.Margin = new System.Windows.Forms.Padding(8);
            this._btnIsFlaggedWith.Name = "_btnIsFlaggedWith";
            this._btnIsFlaggedWith.Size = new System.Drawing.Size(30, 30);
            this._btnIsFlaggedWith.TabIndex = 5;
            this._btnIsFlaggedWith.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.SetColumnSpan(this.flowLayoutPanel1, 5);
            this.flowLayoutPanel1.Controls.Add(this._radAnd);
            this.flowLayoutPanel1.Controls.Add(this._radOr);
            this.flowLayoutPanel1.Controls.Add(this.checkBox1);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(229, 41);
            this.flowLayoutPanel1.TabIndex = 8;
            // 
            // _radAnd
            // 
            this._radAnd.AutoSize = true;
            this._radAnd.Location = new System.Drawing.Point(8, 8);
            this._radAnd.Margin = new System.Windows.Forms.Padding(8);
            this._radAnd.Name = "_radAnd";
            this._radAnd.Size = new System.Drawing.Size(56, 25);
            this._radAnd.TabIndex = 0;
            this._radAnd.TabStop = true;
            this._radAnd.Text = "And";
            this._radAnd.UseVisualStyleBackColor = true;
            this._radAnd.CheckedChanged += new System.EventHandler(this._txtComp_TextChanged);
            // 
            // _radOr
            // 
            this._radOr.AutoSize = true;
            this._radOr.Location = new System.Drawing.Point(80, 8);
            this._radOr.Margin = new System.Windows.Forms.Padding(8);
            this._radOr.Name = "_radOr";
            this._radOr.Size = new System.Drawing.Size(46, 25);
            this._radOr.TabIndex = 0;
            this._radOr.TabStop = true;
            this._radOr.Text = "Or";
            this._radOr.UseVisualStyleBackColor = true;
            this._radOr.CheckedChanged += new System.EventHandler(this._txtComp_TextChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(142, 8);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(8);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(79, 25);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "Negate";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // _lstFlagComparator
            // 
            this._lstFlagComparator.Dock = System.Windows.Forms.DockStyle.Top;
            this._lstFlagComparator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstFlagComparator.FormattingEnabled = true;
            this._lstFlagComparator.Location = new System.Drawing.Point(122, 147);
            this._lstFlagComparator.Margin = new System.Windows.Forms.Padding(8);
            this._lstFlagComparator.Name = "_lstFlagComparator";
            this._lstFlagComparator.Size = new System.Drawing.Size(239, 29);
            this._lstFlagComparator.TabIndex = 1;
            this._lstFlagComparator.SelectedIndexChanged += new System.EventHandler(this._txtComp_TextChanged);
            // 
            // _lstClusterComparator
            // 
            this._lstClusterComparator.Dock = System.Windows.Forms.DockStyle.Top;
            this._lstClusterComparator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstClusterComparator.FormattingEnabled = true;
            this._lstClusterComparator.Location = new System.Drawing.Point(122, 98);
            this._lstClusterComparator.Margin = new System.Windows.Forms.Padding(8);
            this._lstClusterComparator.Name = "_lstClusterComparator";
            this._lstClusterComparator.Size = new System.Drawing.Size(239, 29);
            this._lstClusterComparator.TabIndex = 1;
            this._lstClusterComparator.SelectedIndexChanged += new System.EventHandler(this._txtComp_TextChanged);
            // 
            // _lstPeakComparator
            // 
            this._lstPeakComparator.Dock = System.Windows.Forms.DockStyle.Top;
            this._lstPeakComparator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstPeakComparator.FormattingEnabled = true;
            this._lstPeakComparator.Location = new System.Drawing.Point(122, 49);
            this._lstPeakComparator.Margin = new System.Windows.Forms.Padding(8);
            this._lstPeakComparator.Name = "_lstPeakComparator";
            this._lstPeakComparator.Size = new System.Drawing.Size(239, 29);
            this._lstPeakComparator.TabIndex = 1;
            this._lstPeakComparator.SelectedIndexChanged += new System.EventHandler(this._txtComp_TextChanged);
            // 
            // _radFilter
            // 
            this._radFilter.AutoSize = true;
            this._radFilter.Location = new System.Drawing.Point(24, 196);
            this._radFilter.Margin = new System.Windows.Forms.Padding(24, 8, 8, 16);
            this._radFilter.Name = "_radFilter";
            this._radFilter.Size = new System.Drawing.Size(63, 25);
            this._radFilter.TabIndex = 0;
            this._radFilter.Text = "Filter";
            this._radFilter.UseVisualStyleBackColor = true;
            this._radFilter.CheckedChanged += new System.EventHandler(this._txtComp_TextChanged);
            // 
            // _lstFilter
            // 
            this.tableLayoutPanel2.SetColumnSpan(this._lstFilter, 2);
            this._lstFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this._lstFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstFilter.FormattingEnabled = true;
            this._lstFilter.Location = new System.Drawing.Point(122, 196);
            this._lstFilter.Margin = new System.Windows.Forms.Padding(8);
            this._lstFilter.Name = "_lstFilter";
            this._lstFilter.Size = new System.Drawing.Size(494, 29);
            this._lstFilter.TabIndex = 1;
            this._lstFilter.SelectedIndexChanged += new System.EventHandler(this._txtComp_TextChanged);
            // 
            // _lstFilterOp
            // 
            this._lstFilterOp.Dock = System.Windows.Forms.DockStyle.Top;
            this._lstFilterOp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstFilterOp.FormattingEnabled = true;
            this._lstFilterOp.Location = new System.Drawing.Point(632, 196);
            this._lstFilterOp.Margin = new System.Windows.Forms.Padding(8);
            this._lstFilterOp.Name = "_lstFilterOp";
            this._lstFilterOp.Size = new System.Drawing.Size(239, 29);
            this._lstFilterOp.TabIndex = 1;
            this._lstFilterOp.SelectedIndexChanged += new System.EventHandler(this._txtComp_TextChanged);
            // 
            // ctlTitleBar1
            // 
            this.ctlTitleBar1.AutoSize = true;
            this.ctlTitleBar1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ctlTitleBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlTitleBar1.HelpText = resources.GetString("ctlTitleBar1.HelpText");
            this.ctlTitleBar1.Location = new System.Drawing.Point(0, 0);
            this.ctlTitleBar1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ctlTitleBar1.MinimumSize = new System.Drawing.Size(256, 0);
            this.ctlTitleBar1.Name = "ctlTitleBar1";
            this.ctlTitleBar1.Size = new System.Drawing.Size(926, 87);
            this.ctlTitleBar1.SubText = "Define the terms of this condition";
            this.ctlTitleBar1.TabIndex = 0;
            this.ctlTitleBar1.Text = "TEXT GOES HERE";
            this.ctlTitleBar1.WarningText = null;
            // 
            // FrmPeakFilterCondition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(926, 531);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.ctlTitleBar1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmPeakFilterCondition";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Peak filter";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel5.ResumeLayout(false);
            this.flowLayoutPanel5.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.CtlTitleBar ctlTitleBar1;
        private System.Windows.Forms.ComboBox _lstIsStatistic;
        private System.Windows.Forms.ComboBox _lstStatisticComparator;
        private System.Windows.Forms.TextBox _txtStatisticValue;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private MetaboliteLevels.Controls.CtlButton _btnOk;
        private MetaboliteLevels.Controls.CtlButton _btnCancel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.Label _lblSigPeaks;
        private System.Windows.Forms.Label _lblInsigPeaks;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.RadioButton _chkIsStatistic;
        private System.Windows.Forms.RadioButton _chkIsFlaggedWith;
        private System.Windows.Forms.RadioButton _chkIsInCluster;
        private System.Windows.Forms.RadioButton _chkIsInSet;
        private System.Windows.Forms.TextBox _txtIsInSet;
        private System.Windows.Forms.Button _btnIsInSet;
        private System.Windows.Forms.TextBox _txtIsInCluster;
        private System.Windows.Forms.Button _btnIsInCluster;
        private System.Windows.Forms.TextBox _txtIsFlaggedWith;
        private System.Windows.Forms.Button _btnIsFlaggedWith;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.RadioButton _radAnd;
        private System.Windows.Forms.RadioButton _radOr;
        private System.Windows.Forms.Label _lblPreviewTitle;
        private System.Windows.Forms.ComboBox _lstFlagComparator;
        private System.Windows.Forms.ComboBox _lstClusterComparator;
        private System.Windows.Forms.ComboBox _lstPeakComparator;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.RadioButton _radFilter;
        private System.Windows.Forms.ComboBox _lstFilter;
        private System.Windows.Forms.ComboBox _lstFilterOp;
    }
}