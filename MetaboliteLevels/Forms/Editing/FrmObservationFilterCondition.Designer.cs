namespace MetaboliteLevels.Forms.Editing
{
    partial class FrmObservationFilterCondition
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmObservationFilterCondition));
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this._radAnd = new System.Windows.Forms.RadioButton();
            this._radOr = new System.Windows.Forms.RadioButton();
            this.ctlTitleBar1 = new MetaboliteLevels.Controls.CtlTitleBar();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this._chkRep = new System.Windows.Forms.RadioButton();
            this._chkTime = new System.Windows.Forms.RadioButton();
            this._chkGroup = new System.Windows.Forms.RadioButton();
            this._txtGroup = new Controls.CtlTextBox();
            this._btnGroup = new Controls.CtlButton();
            this._txtTime = new Controls.CtlTextBox();
            this._btnTime = new Controls.CtlButton();
            this._txtRep = new Controls.CtlTextBox();
            this._btnRep = new Controls.CtlButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._lstRep = new System.Windows.Forms.ComboBox();
            this._lstDay = new System.Windows.Forms.ComboBox();
            this._lstGroup = new System.Windows.Forms.ComboBox();
            this._chkBatch = new System.Windows.Forms.RadioButton();
            this._lstBatch = new System.Windows.Forms.ComboBox();
            this._txtBatch = new Controls.CtlTextBox();
            this._chkAq = new System.Windows.Forms.RadioButton();
            this._lstAq = new System.Windows.Forms.ComboBox();
            this._txtAq = new Controls.CtlTextBox();
            this._chkObs = new System.Windows.Forms.RadioButton();
            this._lstObs = new System.Windows.Forms.ComboBox();
            this._txtObs = new Controls.CtlTextBox();
            this._btnBatch = new Controls.CtlButton();
            this._btnObs = new Controls.CtlButton();
            this._btnAq = new Controls.CtlButton();
            this._chkCond = new System.Windows.Forms.RadioButton();
            this._lstCond = new System.Windows.Forms.ComboBox();
            this._txtCond = new Controls.CtlTextBox();
            this._btnCond = new Controls.CtlButton();
            this._lblSigPeaks = new System.Windows.Forms.Label();
            this._lblInsigPeaks = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this._btnCancel = new MetaboliteLevels.Controls.CtlButton();
            this._btnOk = new MetaboliteLevels.Controls.CtlButton();
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this._lblPreviewTitle = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._checker = new MetaboliteLevels.Controls.CtlError(this.components);
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.flowLayoutPanel5.SuspendLayout();
            this.SuspendLayout();
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
            this.checkBox1.CheckedChanged += new System.EventHandler(this.something_Changed);
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
            this._radAnd.CheckedChanged += new System.EventHandler(this.something_Changed);
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
            this._radOr.CheckedChanged += new System.EventHandler(this.something_Changed);
            // 
            // ctlTitleBar1
            // 
            this.ctlTitleBar1.AutoSize = true;
            this.ctlTitleBar1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ctlTitleBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlTitleBar1.HelpText = resources.GetString("ctlTitleBar1.HelpText");
            this.ctlTitleBar1.Location = new System.Drawing.Point(0, 0);
            this.ctlTitleBar1.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.ctlTitleBar1.MinimumSize = new System.Drawing.Size(384, 0);
            this.ctlTitleBar1.Name = "ctlTitleBar1";
            this.ctlTitleBar1.Size = new System.Drawing.Size(953, 87);
            this.ctlTitleBar1.SubText = "Define the terms of this condition";
            this.ctlTitleBar1.TabIndex = 5;
            this.ctlTitleBar1.Text = "TEXT GOES HERE";
            this.ctlTitleBar1.WarningText = null;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel2, 2);
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.66666F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this._chkRep, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this._chkTime, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this._chkGroup, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this._txtGroup, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this._btnGroup, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this._txtTime, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this._btnTime, 3, 2);
            this.tableLayoutPanel2.Controls.Add(this._txtRep, 2, 3);
            this.tableLayoutPanel2.Controls.Add(this._btnRep, 3, 3);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this._lstRep, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this._lstDay, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this._lstGroup, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this._chkBatch, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this._lstBatch, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this._txtBatch, 2, 4);
            this.tableLayoutPanel2.Controls.Add(this._chkAq, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this._lstAq, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this._txtAq, 2, 5);
            this.tableLayoutPanel2.Controls.Add(this._chkObs, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this._lstObs, 1, 6);
            this.tableLayoutPanel2.Controls.Add(this._txtObs, 2, 6);
            this.tableLayoutPanel2.Controls.Add(this._btnBatch, 3, 4);
            this.tableLayoutPanel2.Controls.Add(this._btnObs, 3, 6);
            this.tableLayoutPanel2.Controls.Add(this._btnAq, 3, 5);
            this.tableLayoutPanel2.Controls.Add(this._chkCond, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this._lstCond, 1, 7);
            this.tableLayoutPanel2.Controls.Add(this._txtCond, 2, 7);
            this.tableLayoutPanel2.Controls.Add(this._btnCond, 3, 7);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 8;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(953, 384);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // _chkRep
            // 
            this._chkRep.AutoSize = true;
            this._chkRep.Location = new System.Drawing.Point(24, 147);
            this._chkRep.Margin = new System.Windows.Forms.Padding(24, 8, 8, 16);
            this._chkRep.Name = "_chkRep";
            this._chkRep.Size = new System.Drawing.Size(91, 25);
            this._chkRep.TabIndex = 0;
            this._chkRep.Text = "Replicate";
            this._chkRep.UseVisualStyleBackColor = true;
            this._chkRep.CheckedChanged += new System.EventHandler(this.something_Changed);
            // 
            // _chkTime
            // 
            this._chkTime.AutoSize = true;
            this._chkTime.Location = new System.Drawing.Point(24, 98);
            this._chkTime.Margin = new System.Windows.Forms.Padding(24, 8, 8, 16);
            this._chkTime.Name = "_chkTime";
            this._chkTime.Size = new System.Drawing.Size(62, 25);
            this._chkTime.TabIndex = 0;
            this._chkTime.Text = "Time";
            this._chkTime.UseVisualStyleBackColor = true;
            this._chkTime.CheckedChanged += new System.EventHandler(this.something_Changed);
            // 
            // _chkGroup
            // 
            this._chkGroup.AutoSize = true;
            this._chkGroup.Location = new System.Drawing.Point(24, 49);
            this._chkGroup.Margin = new System.Windows.Forms.Padding(24, 8, 8, 16);
            this._chkGroup.Name = "_chkGroup";
            this._chkGroup.Size = new System.Drawing.Size(72, 25);
            this._chkGroup.TabIndex = 0;
            this._chkGroup.Text = "Group";
            this._chkGroup.UseVisualStyleBackColor = true;
            this._chkGroup.CheckedChanged += new System.EventHandler(this.something_Changed);
            // 
            // _txtGroup
            // 
            this._txtGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtGroup.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._txtGroup.Location = new System.Drawing.Point(407, 49);
            this._txtGroup.Margin = new System.Windows.Forms.Padding(8);
            this._txtGroup.Name = "_txtGroup";
            this._txtGroup.Size = new System.Drawing.Size(492, 30);
            this._txtGroup.TabIndex = 4;
            this._txtGroup.TextChanged += new System.EventHandler(this.something_Changed);
            // 
            // _btnGroup
            // 
            this._btnGroup.Image = global::MetaboliteLevels.Properties.Resources.MnuEnlargeList;
            this._btnGroup.Location = new System.Drawing.Point(915, 49);
            this._btnGroup.Margin = new System.Windows.Forms.Padding(8);
            this._btnGroup.Name = "_btnGroup";
            this._btnGroup.Size = new System.Drawing.Size(30, 30);
            this._btnGroup.TabIndex = 5;
            this._btnGroup.UseVisualStyleBackColor = true;
            // 
            // _txtTime
            // 
            this._txtTime.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtTime.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._txtTime.Location = new System.Drawing.Point(407, 98);
            this._txtTime.Margin = new System.Windows.Forms.Padding(8);
            this._txtTime.Name = "_txtTime";
            this._txtTime.Size = new System.Drawing.Size(492, 30);
            this._txtTime.TabIndex = 6;
            this._txtTime.TextChanged += new System.EventHandler(this.something_Changed);
            // 
            // _btnTime
            // 
            this._btnTime.Image = global::MetaboliteLevels.Properties.Resources.MnuEnlargeList;
            this._btnTime.Location = new System.Drawing.Point(915, 98);
            this._btnTime.Margin = new System.Windows.Forms.Padding(8);
            this._btnTime.Name = "_btnTime";
            this._btnTime.Size = new System.Drawing.Size(30, 30);
            this._btnTime.TabIndex = 5;
            this._btnTime.UseVisualStyleBackColor = true;
            // 
            // _txtRep
            // 
            this._txtRep.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtRep.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._txtRep.Location = new System.Drawing.Point(407, 147);
            this._txtRep.Margin = new System.Windows.Forms.Padding(8);
            this._txtRep.Name = "_txtRep";
            this._txtRep.Size = new System.Drawing.Size(492, 30);
            this._txtRep.TabIndex = 7;
            this._txtRep.TextChanged += new System.EventHandler(this.something_Changed);
            // 
            // _btnRep
            // 
            this._btnRep.Image = global::MetaboliteLevels.Properties.Resources.MnuEnlargeList;
            this._btnRep.Location = new System.Drawing.Point(915, 147);
            this._btnRep.Margin = new System.Windows.Forms.Padding(8);
            this._btnRep.Name = "_btnRep";
            this._btnRep.Size = new System.Drawing.Size(30, 30);
            this._btnRep.TabIndex = 5;
            this._btnRep.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.SetColumnSpan(this.flowLayoutPanel1, 4);
            this.flowLayoutPanel1.Controls.Add(this._radAnd);
            this.flowLayoutPanel1.Controls.Add(this._radOr);
            this.flowLayoutPanel1.Controls.Add(this.checkBox1);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(229, 41);
            this.flowLayoutPanel1.TabIndex = 8;
            // 
            // _lstRep
            // 
            this._lstRep.Dock = System.Windows.Forms.DockStyle.Top;
            this._lstRep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstRep.FormattingEnabled = true;
            this._lstRep.Location = new System.Drawing.Point(153, 147);
            this._lstRep.Margin = new System.Windows.Forms.Padding(8);
            this._lstRep.Name = "_lstRep";
            this._lstRep.Size = new System.Drawing.Size(238, 29);
            this._lstRep.TabIndex = 1;
            this._lstRep.SelectedIndexChanged += new System.EventHandler(this.something_Changed);
            // 
            // _lstDay
            // 
            this._lstDay.Dock = System.Windows.Forms.DockStyle.Top;
            this._lstDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstDay.FormattingEnabled = true;
            this._lstDay.Location = new System.Drawing.Point(153, 98);
            this._lstDay.Margin = new System.Windows.Forms.Padding(8);
            this._lstDay.Name = "_lstDay";
            this._lstDay.Size = new System.Drawing.Size(238, 29);
            this._lstDay.TabIndex = 1;
            this._lstDay.SelectedIndexChanged += new System.EventHandler(this.something_Changed);
            // 
            // _lstGroup
            // 
            this._lstGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this._lstGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstGroup.FormattingEnabled = true;
            this._lstGroup.Location = new System.Drawing.Point(153, 49);
            this._lstGroup.Margin = new System.Windows.Forms.Padding(8);
            this._lstGroup.Name = "_lstGroup";
            this._lstGroup.Size = new System.Drawing.Size(238, 29);
            this._lstGroup.TabIndex = 1;
            this._lstGroup.SelectedIndexChanged += new System.EventHandler(this.something_Changed);
            // 
            // _chkBatch
            // 
            this._chkBatch.AutoSize = true;
            this._chkBatch.Location = new System.Drawing.Point(24, 196);
            this._chkBatch.Margin = new System.Windows.Forms.Padding(24, 8, 8, 16);
            this._chkBatch.Name = "_chkBatch";
            this._chkBatch.Size = new System.Drawing.Size(66, 25);
            this._chkBatch.TabIndex = 0;
            this._chkBatch.Text = "Batch";
            this._chkBatch.UseVisualStyleBackColor = true;
            this._chkBatch.CheckedChanged += new System.EventHandler(this.something_Changed);
            // 
            // _lstBatch
            // 
            this._lstBatch.Dock = System.Windows.Forms.DockStyle.Top;
            this._lstBatch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstBatch.FormattingEnabled = true;
            this._lstBatch.Location = new System.Drawing.Point(153, 196);
            this._lstBatch.Margin = new System.Windows.Forms.Padding(8);
            this._lstBatch.Name = "_lstBatch";
            this._lstBatch.Size = new System.Drawing.Size(238, 29);
            this._lstBatch.TabIndex = 1;
            this._lstBatch.SelectedIndexChanged += new System.EventHandler(this.something_Changed);
            // 
            // _txtBatch
            // 
            this._txtBatch.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtBatch.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._txtBatch.Location = new System.Drawing.Point(407, 196);
            this._txtBatch.Margin = new System.Windows.Forms.Padding(8);
            this._txtBatch.Name = "_txtBatch";
            this._txtBatch.Size = new System.Drawing.Size(492, 30);
            this._txtBatch.TabIndex = 7;
            this._txtBatch.TextChanged += new System.EventHandler(this.something_Changed);
            // 
            // _chkAq
            // 
            this._chkAq.AutoSize = true;
            this._chkAq.Location = new System.Drawing.Point(24, 245);
            this._chkAq.Margin = new System.Windows.Forms.Padding(24, 8, 8, 16);
            this._chkAq.Name = "_chkAq";
            this._chkAq.Size = new System.Drawing.Size(105, 25);
            this._chkAq.TabIndex = 0;
            this._chkAq.Text = "Acquisition";
            this._chkAq.UseVisualStyleBackColor = true;
            this._chkAq.CheckedChanged += new System.EventHandler(this.something_Changed);
            // 
            // _lstAq
            // 
            this._lstAq.Dock = System.Windows.Forms.DockStyle.Top;
            this._lstAq.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstAq.FormattingEnabled = true;
            this._lstAq.Location = new System.Drawing.Point(153, 245);
            this._lstAq.Margin = new System.Windows.Forms.Padding(8);
            this._lstAq.Name = "_lstAq";
            this._lstAq.Size = new System.Drawing.Size(238, 29);
            this._lstAq.TabIndex = 1;
            this._lstAq.SelectedIndexChanged += new System.EventHandler(this.something_Changed);
            // 
            // _txtAq
            // 
            this._txtAq.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtAq.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._txtAq.Location = new System.Drawing.Point(407, 245);
            this._txtAq.Margin = new System.Windows.Forms.Padding(8);
            this._txtAq.Name = "_txtAq";
            this._txtAq.Size = new System.Drawing.Size(492, 30);
            this._txtAq.TabIndex = 7;
            this._txtAq.TextChanged += new System.EventHandler(this.something_Changed);
            // 
            // _chkObs
            // 
            this._chkObs.AutoSize = true;
            this._chkObs.Location = new System.Drawing.Point(24, 294);
            this._chkObs.Margin = new System.Windows.Forms.Padding(24, 8, 8, 16);
            this._chkObs.Name = "_chkObs";
            this._chkObs.Size = new System.Drawing.Size(113, 25);
            this._chkObs.TabIndex = 0;
            this._chkObs.Text = "Observation";
            this._chkObs.UseVisualStyleBackColor = true;
            this._chkObs.CheckedChanged += new System.EventHandler(this.something_Changed);
            // 
            // _lstObs
            // 
            this._lstObs.Dock = System.Windows.Forms.DockStyle.Top;
            this._lstObs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstObs.FormattingEnabled = true;
            this._lstObs.Location = new System.Drawing.Point(153, 294);
            this._lstObs.Margin = new System.Windows.Forms.Padding(8);
            this._lstObs.Name = "_lstObs";
            this._lstObs.Size = new System.Drawing.Size(238, 29);
            this._lstObs.TabIndex = 1;
            this._lstObs.SelectedIndexChanged += new System.EventHandler(this.something_Changed);
            // 
            // _txtObs
            // 
            this._txtObs.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtObs.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._txtObs.Location = new System.Drawing.Point(407, 294);
            this._txtObs.Margin = new System.Windows.Forms.Padding(8);
            this._txtObs.Name = "_txtObs";
            this._txtObs.Size = new System.Drawing.Size(492, 30);
            this._txtObs.TabIndex = 7;
            this._txtObs.TextChanged += new System.EventHandler(this.something_Changed);
            // 
            // _btnBatch
            // 
            this._btnBatch.Image = global::MetaboliteLevels.Properties.Resources.MnuEnlargeList;
            this._btnBatch.Location = new System.Drawing.Point(915, 196);
            this._btnBatch.Margin = new System.Windows.Forms.Padding(8);
            this._btnBatch.Name = "_btnBatch";
            this._btnBatch.Size = new System.Drawing.Size(30, 30);
            this._btnBatch.TabIndex = 5;
            this._btnBatch.UseVisualStyleBackColor = true;
            // 
            // _btnObs
            // 
            this._btnObs.Image = global::MetaboliteLevels.Properties.Resources.MnuEnlargeList;
            this._btnObs.Location = new System.Drawing.Point(915, 294);
            this._btnObs.Margin = new System.Windows.Forms.Padding(8);
            this._btnObs.Name = "_btnObs";
            this._btnObs.Size = new System.Drawing.Size(30, 30);
            this._btnObs.TabIndex = 5;
            this._btnObs.UseVisualStyleBackColor = true;
            // 
            // _btnAq
            // 
            this._btnAq.Image = global::MetaboliteLevels.Properties.Resources.MnuEnlargeList;
            this._btnAq.Location = new System.Drawing.Point(915, 245);
            this._btnAq.Margin = new System.Windows.Forms.Padding(8);
            this._btnAq.Name = "_btnAq";
            this._btnAq.Size = new System.Drawing.Size(30, 30);
            this._btnAq.TabIndex = 5;
            this._btnAq.UseVisualStyleBackColor = true;
            // 
            // _chkCond
            // 
            this._chkCond.AutoSize = true;
            this._chkCond.Location = new System.Drawing.Point(24, 343);
            this._chkCond.Margin = new System.Windows.Forms.Padding(24, 8, 8, 16);
            this._chkCond.Name = "_chkCond";
            this._chkCond.Size = new System.Drawing.Size(96, 25);
            this._chkCond.TabIndex = 0;
            this._chkCond.Text = "Condition";
            this._chkCond.UseVisualStyleBackColor = true;
            this._chkCond.CheckedChanged += new System.EventHandler(this.something_Changed);
            // 
            // _lstCond
            // 
            this._lstCond.Dock = System.Windows.Forms.DockStyle.Top;
            this._lstCond.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstCond.FormattingEnabled = true;
            this._lstCond.Location = new System.Drawing.Point(153, 343);
            this._lstCond.Margin = new System.Windows.Forms.Padding(8);
            this._lstCond.Name = "_lstCond";
            this._lstCond.Size = new System.Drawing.Size(238, 29);
            this._lstCond.TabIndex = 1;
            this._lstCond.SelectedIndexChanged += new System.EventHandler(this.something_Changed);
            // 
            // _txtCond
            // 
            this._txtCond.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtCond.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._txtCond.Location = new System.Drawing.Point(407, 343);
            this._txtCond.Margin = new System.Windows.Forms.Padding(8);
            this._txtCond.Name = "_txtCond";
            this._txtCond.Size = new System.Drawing.Size(492, 30);
            this._txtCond.TabIndex = 7;
            this._txtCond.TextChanged += new System.EventHandler(this.something_Changed);
            // 
            // _btnCond
            // 
            this._btnCond.Image = global::MetaboliteLevels.Properties.Resources.MnuEnlargeList;
            this._btnCond.Location = new System.Drawing.Point(915, 343);
            this._btnCond.Margin = new System.Windows.Forms.Padding(8);
            this._btnCond.Name = "_btnCond";
            this._btnCond.Size = new System.Drawing.Size(30, 30);
            this._btnCond.TabIndex = 5;
            this._btnCond.UseVisualStyleBackColor = true;
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(953, 515);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel4.AutoSize = true;
            this.flowLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel4.Controls.Add(this._btnCancel);
            this.flowLayoutPanel4.Controls.Add(this._btnOk);
            this.flowLayoutPanel4.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel4.Location = new System.Drawing.Point(665, 459);
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
            this.flowLayoutPanel5.Location = new System.Drawing.Point(8, 413);
            this.flowLayoutPanel5.Margin = new System.Windows.Forms.Padding(8);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.flowLayoutPanel5.Size = new System.Drawing.Size(257, 94);
            this.flowLayoutPanel5.TabIndex = 4;
            this.flowLayoutPanel5.WrapContents = false;
            // 
            // _lblPreviewTitle
            // 
            this._lblPreviewTitle.AutoSize = true;
            this._lblPreviewTitle.BackColor = System.Drawing.Color.CornflowerBlue;
            this._lblPreviewTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this._lblPreviewTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblPreviewTitle.ForeColor = System.Drawing.Color.White;
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
            this.label2.Location = new System.Drawing.Point(3, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 21);
            this.label2.TabIndex = 4;
            this.label2.Text = "Incomplete rule";
            // 
            // FrmObservationFilterCondition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(953, 602);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.ctlTitleBar1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmObservationFilterCondition";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Observation Filter";
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel5.ResumeLayout(false);
            this.flowLayoutPanel5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.RadioButton _radAnd;
        private System.Windows.Forms.RadioButton _radOr;
        private Controls.CtlTitleBar ctlTitleBar1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private Controls.CtlButton _btnCancel;
        private Controls.CtlButton _btnOk;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.Label _lblPreviewTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label _lblSigPeaks;
        private System.Windows.Forms.Label _lblInsigPeaks;
        private System.Windows.Forms.RadioButton _chkRep;
        private System.Windows.Forms.RadioButton _chkTime;
        private System.Windows.Forms.RadioButton _chkGroup;
        private Controls.CtlTextBox _txtGroup;
        private Controls.CtlButton _btnGroup;
        private Controls.CtlTextBox _txtTime;
        private Controls.CtlButton _btnTime;
        private Controls.CtlTextBox _txtRep;
        private Controls.CtlButton _btnRep;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ComboBox _lstRep;
        private System.Windows.Forms.ComboBox _lstDay;
        private System.Windows.Forms.ComboBox _lstGroup;
        private System.Windows.Forms.RadioButton _chkBatch;
        private System.Windows.Forms.ComboBox _lstBatch;
        private Controls.CtlTextBox _txtBatch;
        private System.Windows.Forms.RadioButton _chkAq;
        private System.Windows.Forms.ComboBox _lstAq;
        private Controls.CtlTextBox _txtAq;
        private System.Windows.Forms.RadioButton _chkObs;
        private System.Windows.Forms.ComboBox _lstObs;
        private Controls.CtlTextBox _txtObs;
        private Controls.CtlButton _btnBatch;
        private Controls.CtlButton _btnObs;
        private Controls.CtlButton _btnAq;
        private System.Windows.Forms.RadioButton _chkCond;
        private System.Windows.Forms.ComboBox _lstCond;
        private Controls.CtlTextBox _txtCond;
        private Controls.CtlButton _btnCond;
        private Controls.CtlError _checker;
    }
}