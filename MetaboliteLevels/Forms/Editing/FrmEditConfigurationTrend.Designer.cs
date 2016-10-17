using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaboliteLevels.Forms.Editing
{
    partial class FrmEditConfigurationTrend
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
            this.label1 = new System.Windows.Forms.Label();
            this._lstMethod = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this._txtName = new MGui.Controls.CtlTextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._btnEditParameters = new MetaboliteLevels.Controls.CtlButton();
            this.label16 = new System.Windows.Forms.Label();
            this._lblParams = new System.Windows.Forms.Label();
            this._txtParams = new MGui.Controls.CtlTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this._tlpPreview = new System.Windows.Forms.TableLayoutPanel();
            this._lblPreviewTitle = new System.Windows.Forms.Label();
            this._flpPreviewButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this._lnkError = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this._lstSource = new System.Windows.Forms.ComboBox();
            this._btnSource = new MetaboliteLevels.Controls.CtlButton();
            this.ctlTitleBar1 = new MetaboliteLevels.Controls.CtlTitleBar();
            this._checker = new MGui.Controls.CtlError(this.components);
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
            this._btnNewStatistic.Location = new System.Drawing.Point(1025, 98);
            this._btnNewStatistic.Margin = new System.Windows.Forms.Padding(8);
            this._btnNewStatistic.Name = "_btnNewStatistic";
            this._btnNewStatistic.Size = new System.Drawing.Size(29, 29);
            this._btnNewStatistic.TabIndex = 8;
            this.toolTip1.SetToolTip(this._btnNewStatistic, "New");
            this._btnNewStatistic.UseDefaultSize = true;
            this._btnNewStatistic.UseVisualStyleBackColor = true;
            // 
            // _btnComment
            // 
            this._btnComment.Image = global::MetaboliteLevels.Properties.Resources.MnuComment;
            this._btnComment.Location = new System.Drawing.Point(1025, 8);
            this._btnComment.Margin = new System.Windows.Forms.Padding(8);
            this._btnComment.Name = "_btnComment";
            this._btnComment.Size = new System.Drawing.Size(29, 29);
            this._btnComment.TabIndex = 2;
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
            this.ctlButton1.TabIndex = 0;
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
            this.ctlButton2.TabIndex = 1;
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
            this._btnCancel.Location = new System.Drawing.Point(137, 5);
            this._btnCancel.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(128, 40);
            this._btnCancel.TabIndex = 1;
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
            this._btnOk.Location = new System.Drawing.Point(3, 5);
            this._btnOk.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this._btnOk.Name = "_btnOk";
            this._btnOk.Size = new System.Drawing.Size(128, 40);
            this._btnOk.TabIndex = 0;
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
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(791, 712);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(268, 50);
            this.flowLayoutPanel1.TabIndex = 13;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 98);
            this.label1.Margin = new System.Windows.Forms.Padding(8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 21);
            this.label1.TabIndex = 6;
            this.label1.Text = "Method";
            // 
            // _lstMethod
            // 
            this.tableLayoutPanel1.SetColumnSpan(this._lstMethod, 2);
            this._lstMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstMethod.ForeColor = System.Drawing.Color.Blue;
            this._lstMethod.FormattingEnabled = true;
            this._lstMethod.Location = new System.Drawing.Point(88, 98);
            this._lstMethod.Margin = new System.Windows.Forms.Padding(8);
            this._lstMethod.Name = "_lstMethod";
            this._lstMethod.Size = new System.Drawing.Size(921, 29);
            this._lstMethod.TabIndex = 7;
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
            this._txtName.TabIndex = 1;
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
            this.tableLayoutPanel1.Controls.Add(this._btnEditParameters, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this._btnNewStatistic, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this._lstMethod, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._txtName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this._btnComment, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label16, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this._lblParams, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this._txtParams, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this._tlpPreview, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this._lstSource, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this._btnSource, 3, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 87);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1062, 720);
            this.tableLayoutPanel1.TabIndex = 14;
            // 
            // _btnEditParameters
            // 
            this._btnEditParameters.Image = global::MetaboliteLevels.Properties.Resources.MnuEnlargeList;
            this._btnEditParameters.Location = new System.Drawing.Point(1025, 143);
            this._btnEditParameters.Margin = new System.Windows.Forms.Padding(8);
            this._btnEditParameters.Name = "_btnEditParameters";
            this._btnEditParameters.Size = new System.Drawing.Size(29, 29);
            this._btnEditParameters.TabIndex = 11;
            this._btnEditParameters.UseDefaultSize = true;
            this._btnEditParameters.UseVisualStyleBackColor = true;
            this._btnEditParameters.Click += new System.EventHandler(this._btnEditParameters_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(8, 188);
            this.label16.Margin = new System.Windows.Forms.Padding(8);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(14, 21);
            this.label16.TabIndex = 12;
            this.label16.Text = " ";
            // 
            // _lblParams
            // 
            this._lblParams.AutoSize = true;
            this._lblParams.Location = new System.Drawing.Point(88, 143);
            this._lblParams.Margin = new System.Windows.Forms.Padding(8);
            this._lblParams.Name = "_lblParams";
            this._lblParams.Size = new System.Drawing.Size(46, 21);
            this._lblParams.TabIndex = 9;
            this._lblParams.Text = "####";
            // 
            // _txtParams
            // 
            this._txtParams.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtParams.ForeColor = System.Drawing.Color.Blue;
            this._txtParams.Location = new System.Drawing.Point(150, 143);
            this._txtParams.Margin = new System.Windows.Forms.Padding(8);
            this._txtParams.Name = "_txtParams";
            this._txtParams.Size = new System.Drawing.Size(859, 29);
            this._txtParams.TabIndex = 10;
            this._txtParams.Watermark = null;
            this._txtParams.TextChanged += new System.EventHandler(this.CheckAndChange);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 680);
            this.label2.Margin = new System.Windows.Forms.Padding(8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 21);
            this.label2.TabIndex = 13;
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
            this._tlpPreview.Controls.Add(this._lnkError, 0, 2);
            this._tlpPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tlpPreview.ForeColor = System.Drawing.Color.Black;
            this._tlpPreview.Location = new System.Drawing.Point(8, 225);
            this._tlpPreview.Margin = new System.Windows.Forms.Padding(8);
            this._tlpPreview.Name = "_tlpPreview";
            this._tlpPreview.RowCount = 3;
            this._tlpPreview.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._tlpPreview.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._tlpPreview.RowStyles.Add(new System.Windows.Forms.RowStyle());
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
            this.panel1.Size = new System.Drawing.Size(1046, 385);
            this.panel1.TabIndex = 0;
            // 
            // _lnkError
            // 
            this._lnkError.AutoSize = true;
            this._lnkError.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this._lnkError.LinkColor = System.Drawing.Color.Red;
            this._lnkError.Location = new System.Drawing.Point(3, 418);
            this._lnkError.Name = "_lnkError";
            this._lnkError.Size = new System.Drawing.Size(487, 21);
            this._lnkError.TabIndex = 1;
            this._lnkError.TabStop = true;
            this._lnkError.Text = "An error occured whilst generating the preview. Click here for details.";
            this._lnkError.Visible = false;
            this._lnkError.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._lnkError_LinkClicked);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 53);
            this.label3.Margin = new System.Windows.Forms.Padding(8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 21);
            this.label3.TabIndex = 3;
            this.label3.Text = "Source";
            // 
            // _lstSource
            // 
            this.tableLayoutPanel1.SetColumnSpan(this._lstSource, 2);
            this._lstSource.Dock = System.Windows.Forms.DockStyle.Top;
            this._lstSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstSource.ForeColor = System.Drawing.Color.Blue;
            this._lstSource.FormattingEnabled = true;
            this._lstSource.Location = new System.Drawing.Point(88, 53);
            this._lstSource.Margin = new System.Windows.Forms.Padding(8);
            this._lstSource.Name = "_lstSource";
            this._lstSource.Size = new System.Drawing.Size(921, 29);
            this._lstSource.TabIndex = 4;
            this._lstSource.SelectedIndexChanged += new System.EventHandler(this.CheckAndChange);
            // 
            // _btnSource
            // 
            this._btnSource.Image = global::MetaboliteLevels.Properties.Resources.MnuViewList;
            this._btnSource.Location = new System.Drawing.Point(1025, 53);
            this._btnSource.Margin = new System.Windows.Forms.Padding(8);
            this._btnSource.Name = "_btnSource";
            this._btnSource.Size = new System.Drawing.Size(29, 29);
            this._btnSource.TabIndex = 5;
            this._btnSource.UseDefaultSize = true;
            this._btnSource.UseVisualStyleBackColor = true;
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
            // FrmEditConfigurationTrend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1062, 807);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.ctlTitleBar1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmEditConfigurationTrend";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Smoothing";
            this.contextMenuStrip1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
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
        private MGui.Controls.CtlTextBox _txtName;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label _lblParams;
        private MGui.Controls.CtlTextBox _txtParams;
        private System.Windows.Forms.Label label2;
        private Controls.CtlButton _btnEditParameters;
        private System.Windows.Forms.TableLayoutPanel _tlpPreview;
        private System.Windows.Forms.Label _lblPreviewTitle;
        private System.Windows.Forms.FlowLayoutPanel _flpPreviewButtons;
        private Controls.CtlButton ctlButton1;
        private Controls.CtlButton ctlButton2;
        private Controls.CtlButton ctlButton3;
        private System.Windows.Forms.Panel panel1;
        private MGui.Controls.CtlError _checker;
        private System.Windows.Forms.LinkLabel _lnkError;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox _lstSource;
        private Controls.CtlButton _btnSource;
    }
}