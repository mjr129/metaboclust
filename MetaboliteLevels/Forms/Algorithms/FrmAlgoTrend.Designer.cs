namespace MetaboliteLevels.Forms.Algorithms
{
    partial class FrmAlgoTrend
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
            this.newMetricToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newStatisticToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this._btnNewStatistic = new MetaboliteLevels.Controls.CtlButton();
            this._btnComment = new MetaboliteLevels.Controls.CtlButton();
            this.ctlButton1 = new MetaboliteLevels.Controls.CtlButton();
            this.ctlButton2 = new MetaboliteLevels.Controls.CtlButton();
            this.ctlButton3 = new MetaboliteLevels.Controls.CtlButton();
            this._btnCancel = new MetaboliteLevels.Controls.CtlButton();
            this._btnOk = new MetaboliteLevels.Controls.CtlButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._lblError = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._lstMethod = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this._txtName = new MetaboliteLevels.Controls.CtlTextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._btnEditParameters = new MetaboliteLevels.Controls.CtlButton();
            this.label16 = new System.Windows.Forms.Label();
            this._lblParams = new System.Windows.Forms.Label();
            this._txtParams = new MetaboliteLevels.Controls.CtlTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this._tlpPreview = new System.Windows.Forms.TableLayoutPanel();
            this._lblPreviewTitle = new System.Windows.Forms.Label();
            this._flpPreviewButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ctlTitleBar1 = new MetaboliteLevels.Controls.CtlTitleBar();
            this._checker = new MetaboliteLevels.Controls.CtlError(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this._tlpPreview.SuspendLayout();
            this._flpPreviewButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // newMetricToolStripMenuItem
            // 
            this.newMetricToolStripMenuItem.Name = "newMetricToolStripMenuItem";
            this.newMetricToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.newMetricToolStripMenuItem.Text = "&New metric...";
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
            // _btnNewStatistic
            // 
            this._btnNewStatistic.Image = global::MetaboliteLevels.Properties.Resources.MnuViewList;
            this._btnNewStatistic.Location = new System.Drawing.Point(1025, 53);
            this._btnNewStatistic.Margin = new System.Windows.Forms.Padding(8);
            this._btnNewStatistic.Name = "_btnNewStatistic";
            this._btnNewStatistic.Size = new System.Drawing.Size(29, 29);
            this._btnNewStatistic.TabIndex = 16;
            this.toolTip1.SetToolTip(this._btnNewStatistic, "New");
            this._btnNewStatistic.UseDefaultSize = true;
            this._btnNewStatistic.UseVisualStyleBackColor = true;
            // 
            // _btnComment
            // 
            this._btnComment.Image = global::MetaboliteLevels.Properties.Resources.CommentHS;
            this._btnComment.Location = new System.Drawing.Point(1025, 8);
            this._btnComment.Margin = new System.Windows.Forms.Padding(8);
            this._btnComment.Name = "_btnComment";
            this._btnComment.Size = new System.Drawing.Size(29, 29);
            this._btnComment.TabIndex = 16;
            this.toolTip1.SetToolTip(this._btnComment, "Comments");
            this._btnComment.UseDefaultSize = true;
            this._btnComment.UseVisualStyleBackColor = true;
            this._btnComment.Click += new System.EventHandler(this._btnComment_Click);
            // 
            // ctlButton1
            // 
            this.ctlButton1.BackColor = System.Drawing.SystemColors.Control;
            this.ctlButton1.Image = global::MetaboliteLevels.Properties.Resources.MnuPreviewSelect;
            this.ctlButton1.Location = new System.Drawing.Point(2, 2);
            this.ctlButton1.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.ctlButton1.Name = "ctlButton1";
            this.ctlButton1.Size = new System.Drawing.Size(29, 29);
            this.ctlButton1.TabIndex = 2;
            this.ctlButton1.Text = null;
            this.toolTip1.SetToolTip(this.ctlButton1, "Select preview peak");
            this.ctlButton1.UseDefaultSize = true;
            this.ctlButton1.UseVisualStyleBackColor = true;
            this.ctlButton1.Click += new System.EventHandler(this._btnSelectPreview_Click);
            // 
            // ctlButton2
            // 
            this.ctlButton2.BackColor = System.Drawing.SystemColors.Control;
            this.ctlButton2.Image = global::MetaboliteLevels.Properties.Resources.MnuPreviewPrevious;
            this.ctlButton2.Location = new System.Drawing.Point(39, 2);
            this.ctlButton2.Margin = new System.Windows.Forms.Padding(0);
            this.ctlButton2.Name = "ctlButton2";
            this.ctlButton2.Size = new System.Drawing.Size(29, 29);
            this.ctlButton2.TabIndex = 2;
            this.ctlButton2.Text = null;
            this.toolTip1.SetToolTip(this.ctlButton2, "Previous peak");
            this.ctlButton2.UseDefaultSize = true;
            this.ctlButton2.UseVisualStyleBackColor = true;
            this.ctlButton2.Click += new System.EventHandler(this._btnPreviousPreview_Click);
            // 
            // ctlButton3
            // 
            this.ctlButton3.BackColor = System.Drawing.SystemColors.Control;
            this.ctlButton3.Image = global::MetaboliteLevels.Properties.Resources.MnuPreviewNext;
            this.ctlButton3.Location = new System.Drawing.Point(68, 2);
            this.ctlButton3.Margin = new System.Windows.Forms.Padding(0);
            this.ctlButton3.Name = "ctlButton3";
            this.ctlButton3.Size = new System.Drawing.Size(29, 29);
            this.ctlButton3.TabIndex = 2;
            this.ctlButton3.Text = null;
            this.toolTip1.SetToolTip(this.ctlButton3, "Next peak");
            this.ctlButton3.UseDefaultSize = true;
            this.ctlButton3.UseVisualStyleBackColor = true;
            this.ctlButton3.Click += new System.EventHandler(this.ctlButton3_Click);
            // 
            // _btnCancel
            // 
            this._btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnCancel.Image = global::MetaboliteLevels.Properties.Resources.MnuCancel;
            this._btnCancel.Location = new System.Drawing.Point(336, 5);
            this._btnCancel.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(128, 40);
            this._btnCancel.TabIndex = 19;
            this._btnCancel.Text = "Cancel";
            this._btnCancel.UseDefaultSize = true;
            this._btnCancel.UseVisualStyleBackColor = true;
            // 
            // _btnOk
            // 
            this._btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._btnOk.Enabled = false;
            this._btnOk.Image = global::MetaboliteLevels.Properties.Resources.MnuAccept;
            this._btnOk.Location = new System.Drawing.Point(202, 5);
            this._btnOk.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this._btnOk.Name = "_btnOk";
            this._btnOk.Size = new System.Drawing.Size(128, 40);
            this._btnOk.TabIndex = 18;
            this._btnOk.Text = "OK";
            this._btnOk.UseDefaultSize = true;
            this._btnOk.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel1, 4);
            this.flowLayoutPanel1.Controls.Add(this._btnCancel);
            this.flowLayoutPanel1.Controls.Add(this._btnOk);
            this.flowLayoutPanel1.Controls.Add(this._lblError);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(592, 667);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(467, 50);
            this.flowLayoutPanel1.TabIndex = 13;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // _lblError
            // 
            this._lblError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._lblError.AutoSize = true;
            this._lblError.BackColor = System.Drawing.Color.White;
            this._lblError.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._lblError.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblError.ForeColor = System.Drawing.Color.Red;
            this._lblError.Location = new System.Drawing.Point(8, 19);
            this._lblError.Margin = new System.Windows.Forms.Padding(8, 0, 8, 8);
            this._lblError.Name = "_lblError";
            this._lblError.Size = new System.Drawing.Size(183, 23);
            this._lblError.TabIndex = 4;
            this._lblError.Text = "###################";
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
            // 
            // _lstMethod
            // 
            this.tableLayoutPanel1.SetColumnSpan(this._lstMethod, 2);
            this._lstMethod.Dock = System.Windows.Forms.DockStyle.Top;
            this._lstMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstMethod.ForeColor = System.Drawing.Color.Blue;
            this._lstMethod.FormattingEnabled = true;
            this._lstMethod.Location = new System.Drawing.Point(88, 53);
            this._lstMethod.Margin = new System.Windows.Forms.Padding(8);
            this._lstMethod.Name = "_lstMethod";
            this._lstMethod.Size = new System.Drawing.Size(921, 29);
            this._lstMethod.TabIndex = 1;
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
            // 
            // _txtName
            // 
            this.tableLayoutPanel1.SetColumnSpan(this._txtName, 2);
            this._txtName.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtName.ForeColor = System.Drawing.Color.Blue;
            this._txtName.Location = new System.Drawing.Point(88, 8);
            this._txtName.Margin = new System.Windows.Forms.Padding(8);
            this._txtName.Name = "_txtName";
            this._txtName.Size = new System.Drawing.Size(921, 29);
            this._txtName.TabIndex = 6;
            this._txtName.Watermark = null;
            this._txtName.TextChanged += new System.EventHandler(this.Check);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this._btnEditParameters, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this._btnNewStatistic, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this._lstMethod, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._txtName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this._btnComment, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label16, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this._lblParams, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this._txtParams, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this._tlpPreview, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 87);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1062, 720);
            this.tableLayoutPanel1.TabIndex = 14;
            // 
            // _btnEditParameters
            // 
            this._btnEditParameters.Image = global::MetaboliteLevels.Properties.Resources.MnuEnlargeList;
            this._btnEditParameters.Location = new System.Drawing.Point(1025, 98);
            this._btnEditParameters.Margin = new System.Windows.Forms.Padding(8);
            this._btnEditParameters.Name = "_btnEditParameters";
            this._btnEditParameters.Size = new System.Drawing.Size(29, 29);
            this._btnEditParameters.TabIndex = 16;
            this._btnEditParameters.UseDefaultSize = true;
            this._btnEditParameters.UseVisualStyleBackColor = true;
            this._btnEditParameters.Click += new System.EventHandler(this._btnEditParameters_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(8, 143);
            this.label16.Margin = new System.Windows.Forms.Padding(8);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(14, 21);
            this.label16.TabIndex = 4;
            this.label16.Text = " ";
            // 
            // _lblParams
            // 
            this._lblParams.AutoSize = true;
            this._lblParams.Location = new System.Drawing.Point(88, 98);
            this._lblParams.Margin = new System.Windows.Forms.Padding(8);
            this._lblParams.Name = "_lblParams";
            this._lblParams.Size = new System.Drawing.Size(46, 21);
            this._lblParams.TabIndex = 0;
            this._lblParams.Text = "####";
            // 
            // _txtParams
            // 
            this._txtParams.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtParams.ForeColor = System.Drawing.Color.Blue;
            this._txtParams.Location = new System.Drawing.Point(150, 98);
            this._txtParams.Margin = new System.Windows.Forms.Padding(8);
            this._txtParams.Name = "_txtParams";
            this._txtParams.Size = new System.Drawing.Size(859, 29);
            this._txtParams.TabIndex = 6;
            this._txtParams.Watermark = null;
            this._txtParams.TextChanged += new System.EventHandler(this.CheckAndChange);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 635);
            this.label2.Margin = new System.Windows.Forms.Padding(8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 21);
            this.label2.TabIndex = 4;
            this.label2.Text = " ";
            // 
            // _tlpPreview
            // 
            this._tlpPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this._tlpPreview.ColumnCount = 2;
            this.tableLayoutPanel1.SetColumnSpan(this._tlpPreview, 4);
            this._tlpPreview.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._tlpPreview.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this._tlpPreview.Controls.Add(this._lblPreviewTitle, 0, 0);
            this._tlpPreview.Controls.Add(this._flpPreviewButtons, 1, 0);
            this._tlpPreview.Controls.Add(this.panel1, 0, 1);
            this._tlpPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tlpPreview.ForeColor = System.Drawing.Color.Black;
            this._tlpPreview.Location = new System.Drawing.Point(8, 180);
            this._tlpPreview.Margin = new System.Windows.Forms.Padding(8);
            this._tlpPreview.Name = "_tlpPreview";
            this._tlpPreview.RowCount = 2;
            this._tlpPreview.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._tlpPreview.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._tlpPreview.Size = new System.Drawing.Size(1046, 439);
            this._tlpPreview.TabIndex = 15;
            // 
            // _lblPreviewTitle
            // 
            this._lblPreviewTitle.BackColor = System.Drawing.Color.LightSteelBlue;
            this._lblPreviewTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lblPreviewTitle.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblPreviewTitle.ForeColor = System.Drawing.Color.Black;
            this._lblPreviewTitle.Location = new System.Drawing.Point(0, 0);
            this._lblPreviewTitle.Margin = new System.Windows.Forms.Padding(0);
            this._lblPreviewTitle.Name = "_lblPreviewTitle";
            this._lblPreviewTitle.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this._lblPreviewTitle.Size = new System.Drawing.Size(947, 33);
            this._lblPreviewTitle.TabIndex = 0;
            this._lblPreviewTitle.Text = "Preview";
            this._lblPreviewTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _flpPreviewButtons
            // 
            this._flpPreviewButtons.AutoSize = true;
            this._flpPreviewButtons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._flpPreviewButtons.BackColor = System.Drawing.Color.LightSteelBlue;
            this._flpPreviewButtons.Controls.Add(this.ctlButton1);
            this._flpPreviewButtons.Controls.Add(this.ctlButton2);
            this._flpPreviewButtons.Controls.Add(this.ctlButton3);
            this._flpPreviewButtons.ForeColor = System.Drawing.Color.Black;
            this._flpPreviewButtons.Location = new System.Drawing.Point(947, 0);
            this._flpPreviewButtons.Margin = new System.Windows.Forms.Padding(0);
            this._flpPreviewButtons.Name = "_flpPreviewButtons";
            this._flpPreviewButtons.Padding = new System.Windows.Forms.Padding(2);
            this._flpPreviewButtons.Size = new System.Drawing.Size(99, 33);
            this._flpPreviewButtons.TabIndex = 19;
            this._flpPreviewButtons.WrapContents = false;
            // 
            // panel1
            // 
            this._tlpPreview.SetColumnSpan(this.panel1, 2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 33);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1046, 406);
            this.panel1.TabIndex = 20;
            // 
            // ctlTitleBar1
            // 
            this.ctlTitleBar1.AutoSize = true;
            this.ctlTitleBar1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ctlTitleBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlTitleBar1.HelpText = "Select the method and parameters for your smoothing algorithm.\r\n\r\nOnce configured" +
    " you can preview the effects of your algorithm on individual peaks before commit" +
    "ting it.";
            this.ctlTitleBar1.Location = new System.Drawing.Point(0, 0);
            this.ctlTitleBar1.Margin = new System.Windows.Forms.Padding(9, 13, 9, 13);
            this.ctlTitleBar1.MinimumSize = new System.Drawing.Size(864, 0);
            this.ctlTitleBar1.Name = "ctlTitleBar1";
            this.ctlTitleBar1.Size = new System.Drawing.Size(1062, 87);
            this.ctlTitleBar1.SubText = "Select the options for your smoothing algorithm";
            this.ctlTitleBar1.TabIndex = 13;
            this.ctlTitleBar1.Text = "Select Smoothing Algorithm";
            this.ctlTitleBar1.WarningText = null;
            // 
            // FrmAlgoTrend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1062, 807);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.ctlTitleBar1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmAlgoTrend";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Smoothing";
            this.contextMenuStrip1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this._tlpPreview.ResumeLayout(false);
            this._tlpPreview.PerformLayout();
            this._flpPreviewButtons.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.CtlTitleBar ctlTitleBar1;
        private System.Windows.Forms.ToolStripMenuItem newMetricToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newStatisticToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolTip toolTip1;
        private Controls.CtlButton _btnNewStatistic;
        private Controls.CtlButton _btnComment;
        private Controls.CtlButton _btnCancel;
        private Controls.CtlButton _btnOk;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox _lstMethod;
        private System.Windows.Forms.Label label4;
        private Controls.CtlTextBox _txtName;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label _lblParams;
        private Controls.CtlTextBox _txtParams;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label _lblError;
        private Controls.CtlButton _btnEditParameters;
        private System.Windows.Forms.TableLayoutPanel _tlpPreview;
        private System.Windows.Forms.Label _lblPreviewTitle;
        private System.Windows.Forms.FlowLayoutPanel _flpPreviewButtons;
        private Controls.CtlButton ctlButton1;
        private Controls.CtlButton ctlButton2;
        private Controls.CtlButton ctlButton3;
        private System.Windows.Forms.Panel panel1;
        private Controls.CtlError _checker;
    }
}