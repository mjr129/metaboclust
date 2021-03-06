﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Gui.Controls;

namespace MetaboliteLevels.Gui.Forms.Editing
{
    partial class FrmEditConfigurationStatistic
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEditConfigurationStatistic));
            this.label1 = new System.Windows.Forms.Label();
            this._lstMethod = new System.Windows.Forms.ComboBox();
            this._lblApply = new System.Windows.Forms.Label();
            this._lblAVec = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._btnNewStatistic = new CtlButton();
            this.label4 = new System.Windows.Forms.Label();
            this._txtName = new MGui.Controls.CtlTextBox();
            this._btnComment = new CtlButton();
            this._btnFilter1 = new CtlButton();
            this._lblBVec = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this._lblParams = new System.Windows.Forms.Label();
            this._lstDiffPeak = new System.Windows.Forms.ComboBox();
            this._btnSelectDiffPeak = new CtlButton();
            this._radSamePeak = new System.Windows.Forms.RadioButton();
            this._btnFilter2 = new CtlButton();
            this._radBDiffPeak = new System.Windows.Forms.RadioButton();
            this._radBCorTime = new System.Windows.Forms.RadioButton();
            this._lstFilter2 = new System.Windows.Forms.ComboBox();
            this._lstFilter1 = new System.Windows.Forms.ComboBox();
            this._txtParams = new MGui.Controls.CtlTextBox();
            this._btnEditParameters = new CtlButton();
            this._lstSource = new System.Windows.Forms.ComboBox();
            this._btnSource = new CtlButton();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this._tlpPreivew = new System.Windows.Forms.TableLayoutPanel();
            this._lblPreviewTitle = new System.Windows.Forms.Label();
            this._flpPreviewButtons = new System.Windows.Forms.FlowLayoutPanel();
            this._btnSelectPreview = new CtlButton();
            this.ctlButton2 = new CtlButton();
            this.ctlButton3 = new CtlButton();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this._lblPreview = new System.Windows.Forms.Label();
            this._lblPreview2 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._btnCancel = new CtlButton();
            this._btnOk = new CtlButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ctlTitleBar1 = new CtlTitleBar();
            this._checker = new MGui.Controls.CtlError(this.components);
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this._tlpPreivew.SuspendLayout();
            this._flpPreviewButtons.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 53);
            this.label1.Margin = new System.Windows.Forms.Padding(8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "Method";
            // 
            // _lstMethod
            // 
            this.tableLayoutPanel1.SetColumnSpan(this._lstMethod, 3);
            this._lstMethod.Dock = System.Windows.Forms.DockStyle.Top;
            this._lstMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstMethod.ForeColor = System.Drawing.Color.Blue;
            this._lstMethod.FormattingEnabled = true;
            this._lstMethod.Location = new System.Drawing.Point(98, 53);
            this._lstMethod.Margin = new System.Windows.Forms.Padding(8);
            this._lstMethod.Name = "_lstMethod";
            this._lstMethod.Size = new System.Drawing.Size(565, 29);
            this._lstMethod.TabIndex = 4;
            this.toolTip1.SetToolTip(this._lstMethod, "Select the algorithm\r\n\r\n⇉ Metric with two input vectors (e.g. t-test)\r\n→ Statisti" +
        "c with one input vector (e.g. mean)\r\n↣ Calculated from other statistics (e.g. mo" +
        "st significant t-test)");
            this._lstMethod.SelectedIndexChanged += new System.EventHandler(this.CheckAndChange);
            // 
            // _lblApply
            // 
            this._lblApply.AutoSize = true;
            this._lblApply.Location = new System.Drawing.Point(8, 180);
            this._lblApply.Margin = new System.Windows.Forms.Padding(8);
            this._lblApply.Name = "_lblApply";
            this._lblApply.Size = new System.Drawing.Size(54, 21);
            this._lblApply.TabIndex = 9;
            this._lblApply.Text = "Target";
            // 
            // _lblAVec
            // 
            this._lblAVec.AutoSize = true;
            this._lblAVec.Location = new System.Drawing.Point(8, 262);
            this._lblAVec.Margin = new System.Windows.Forms.Padding(8);
            this._lblAVec.Name = "_lblAVec";
            this._lblAVec.Size = new System.Drawing.Size(74, 21);
            this._lblAVec.TabIndex = 14;
            this._lblAVec.Text = "Compare";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this._btnNewStatistic, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this._lblAVec, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this._lblApply, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this._lstMethod, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._txtName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this._btnComment, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this._btnFilter1, 4, 6);
            this.tableLayoutPanel1.Controls.Add(this._lblBVec, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.label14, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.label15, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label16, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this._lblParams, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this._lstDiffPeak, 2, 9);
            this.tableLayoutPanel1.Controls.Add(this._btnSelectDiffPeak, 4, 9);
            this.tableLayoutPanel1.Controls.Add(this._radSamePeak, 1, 10);
            this.tableLayoutPanel1.Controls.Add(this._btnFilter2, 4, 10);
            this.tableLayoutPanel1.Controls.Add(this._radBDiffPeak, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this._radBCorTime, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this._lstFilter2, 2, 10);
            this.tableLayoutPanel1.Controls.Add(this._lstFilter1, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this._txtParams, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this._btnEditParameters, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this._lstSource, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this._btnSource, 4, 4);
            this.tableLayoutPanel1.Controls.Add(this.linkLabel1, 1, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 87);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 12;
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(716, 657);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // _btnNewStatistic
            // 
            this._btnNewStatistic.Image = global::MetaboliteLevels.Properties.Resources.MnuViewList;
            this._btnNewStatistic.Location = new System.Drawing.Point(679, 53);
            this._btnNewStatistic.Margin = new System.Windows.Forms.Padding(8);
            this._btnNewStatistic.Name = "_btnNewStatistic";
            this._btnNewStatistic.Size = new System.Drawing.Size(29, 29);
            this._btnNewStatistic.TabIndex = 5;
            this.toolTip1.SetToolTip(this._btnNewStatistic, "New");
            this._btnNewStatistic.UseDefaultSize = true;
            this._btnNewStatistic.UseVisualStyleBackColor = true;
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
            this.tableLayoutPanel1.SetColumnSpan(this._txtName, 3);
            this._txtName.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtName.ForeColor = System.Drawing.Color.Blue;
            this._txtName.Location = new System.Drawing.Point(98, 8);
            this._txtName.Margin = new System.Windows.Forms.Padding(8);
            this._txtName.Name = "_txtName";
            this._txtName.Size = new System.Drawing.Size(565, 29);
            this._txtName.TabIndex = 1;
            this._txtName.Watermark = null;
            this._txtName.TextChanged += new System.EventHandler(this._txtName_TextChanged);
            // 
            // _btnComment
            // 
            this._btnComment.Image = global::MetaboliteLevels.Properties.Resources.MnuComment;
            this._btnComment.Location = new System.Drawing.Point(679, 8);
            this._btnComment.Margin = new System.Windows.Forms.Padding(8);
            this._btnComment.Name = "_btnComment";
            this._btnComment.Size = new System.Drawing.Size(29, 29);
            this._btnComment.TabIndex = 2;
            this.toolTip1.SetToolTip(this._btnComment, "Comments");
            this._btnComment.UseDefaultSize = true;
            this._btnComment.UseVisualStyleBackColor = true;
            this._btnComment.Click += new System.EventHandler(this._btnComment_Click);
            // 
            // _btnFilter1
            // 
            this._btnFilter1.Image = global::MetaboliteLevels.Properties.Resources.MnuViewList;
            this._btnFilter1.Location = new System.Drawing.Point(679, 262);
            this._btnFilter1.Margin = new System.Windows.Forms.Padding(8);
            this._btnFilter1.Name = "_btnFilter1";
            this._btnFilter1.Size = new System.Drawing.Size(29, 29);
            this._btnFilter1.TabIndex = 16;
            this.toolTip1.SetToolTip(this._btnFilter1, "Edit");
            this._btnFilter1.UseDefaultSize = true;
            this._btnFilter1.UseVisualStyleBackColor = true;
            // 
            // _lblBVec
            // 
            this._lblBVec.AutoSize = true;
            this._lblBVec.Location = new System.Drawing.Point(8, 344);
            this._lblBVec.Margin = new System.Windows.Forms.Padding(8);
            this._lblBVec.Name = "_lblBVec";
            this._lblBVec.Size = new System.Drawing.Size(62, 21);
            this._lblBVec.TabIndex = 18;
            this._lblBVec.Text = "Against";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(8, 307);
            this.label14.Margin = new System.Windows.Forms.Padding(8);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(14, 21);
            this.label14.TabIndex = 17;
            this.label14.Text = " ";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(8, 225);
            this.label15.Margin = new System.Windows.Forms.Padding(8);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(14, 21);
            this.label15.TabIndex = 12;
            this.label15.Text = " ";
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
            this._lblParams.Location = new System.Drawing.Point(98, 98);
            this._lblParams.Margin = new System.Windows.Forms.Padding(8);
            this._lblParams.Name = "_lblParams";
            this._lblParams.Size = new System.Drawing.Size(83, 21);
            this._lblParams.TabIndex = 6;
            this._lblParams.Text = "Where k =";
            // 
            // _lstDiffPeak
            // 
            this.tableLayoutPanel1.SetColumnSpan(this._lstDiffPeak, 2);
            this._lstDiffPeak.Dock = System.Windows.Forms.DockStyle.Top;
            this._lstDiffPeak.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstDiffPeak.ForeColor = System.Drawing.Color.Blue;
            this._lstDiffPeak.FormattingEnabled = true;
            this._lstDiffPeak.Location = new System.Drawing.Point(252, 385);
            this._lstDiffPeak.Margin = new System.Windows.Forms.Padding(8);
            this._lstDiffPeak.Name = "_lstDiffPeak";
            this._lstDiffPeak.Size = new System.Drawing.Size(411, 29);
            this._lstDiffPeak.TabIndex = 21;
            this._lstDiffPeak.SelectedIndexChanged += new System.EventHandler(this.CheckAndChange);
            // 
            // _btnSelectDiffPeak
            // 
            this._btnSelectDiffPeak.Image = global::MetaboliteLevels.Properties.Resources.MnuViewList;
            this._btnSelectDiffPeak.Location = new System.Drawing.Point(679, 385);
            this._btnSelectDiffPeak.Margin = new System.Windows.Forms.Padding(8);
            this._btnSelectDiffPeak.Name = "_btnSelectDiffPeak";
            this._btnSelectDiffPeak.Size = new System.Drawing.Size(29, 29);
            this._btnSelectDiffPeak.TabIndex = 22;
            this._btnSelectDiffPeak.UseDefaultSize = true;
            this._btnSelectDiffPeak.UseVisualStyleBackColor = true;
            // 
            // _radSamePeak
            // 
            this._radSamePeak.AutoSize = true;
            this._radSamePeak.Location = new System.Drawing.Point(98, 430);
            this._radSamePeak.Margin = new System.Windows.Forms.Padding(8);
            this._radSamePeak.Name = "_radSamePeak";
            this._radSamePeak.Size = new System.Drawing.Size(131, 25);
            this._radSamePeak.TabIndex = 23;
            this._radSamePeak.Text = "The same peak";
            this._radSamePeak.UseVisualStyleBackColor = true;
            this._radSamePeak.CheckedChanged += new System.EventHandler(this.CheckAndChange);
            // 
            // _btnFilter2
            // 
            this._btnFilter2.Image = global::MetaboliteLevels.Properties.Resources.MnuViewList;
            this._btnFilter2.Location = new System.Drawing.Point(679, 430);
            this._btnFilter2.Margin = new System.Windows.Forms.Padding(8);
            this._btnFilter2.Name = "_btnFilter2";
            this._btnFilter2.Size = new System.Drawing.Size(29, 29);
            this._btnFilter2.TabIndex = 25;
            this.toolTip1.SetToolTip(this._btnFilter2, "Edit");
            this._btnFilter2.UseDefaultSize = true;
            this._btnFilter2.UseVisualStyleBackColor = true;
            // 
            // _radBDiffPeak
            // 
            this._radBDiffPeak.AutoSize = true;
            this._radBDiffPeak.Location = new System.Drawing.Point(98, 385);
            this._radBDiffPeak.Margin = new System.Windows.Forms.Padding(8);
            this._radBDiffPeak.Name = "_radBDiffPeak";
            this._radBDiffPeak.Size = new System.Drawing.Size(138, 25);
            this._radBDiffPeak.TabIndex = 20;
            this._radBDiffPeak.Text = "A different peak";
            this._radBDiffPeak.UseVisualStyleBackColor = true;
            this._radBDiffPeak.CheckedChanged += new System.EventHandler(this.CheckAndChange);
            // 
            // _radBCorTime
            // 
            this._radBCorTime.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this._radBCorTime, 4);
            this._radBCorTime.Location = new System.Drawing.Point(98, 344);
            this._radBCorTime.Margin = new System.Windows.Forms.Padding(8);
            this._radBCorTime.Name = "_radBCorTime";
            this._radBCorTime.Size = new System.Drawing.Size(193, 25);
            this._radBCorTime.TabIndex = 19;
            this._radBCorTime.Text = "The corresponding time";
            this._radBCorTime.UseVisualStyleBackColor = true;
            this._radBCorTime.CheckedChanged += new System.EventHandler(this.CheckAndChange);
            // 
            // _lstFilter2
            // 
            this.tableLayoutPanel1.SetColumnSpan(this._lstFilter2, 2);
            this._lstFilter2.Dock = System.Windows.Forms.DockStyle.Top;
            this._lstFilter2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstFilter2.ForeColor = System.Drawing.Color.Blue;
            this._lstFilter2.FormattingEnabled = true;
            this._lstFilter2.Location = new System.Drawing.Point(252, 430);
            this._lstFilter2.Margin = new System.Windows.Forms.Padding(8);
            this._lstFilter2.Name = "_lstFilter2";
            this._lstFilter2.Size = new System.Drawing.Size(411, 29);
            this._lstFilter2.TabIndex = 24;
            this._lstFilter2.SelectedIndexChanged += new System.EventHandler(this.CheckAndChange);
            // 
            // _lstFilter1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this._lstFilter1, 3);
            this._lstFilter1.Dock = System.Windows.Forms.DockStyle.Top;
            this._lstFilter1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstFilter1.ForeColor = System.Drawing.Color.Blue;
            this._lstFilter1.FormattingEnabled = true;
            this._lstFilter1.Location = new System.Drawing.Point(98, 262);
            this._lstFilter1.Margin = new System.Windows.Forms.Padding(8);
            this._lstFilter1.Name = "_lstFilter1";
            this._lstFilter1.Size = new System.Drawing.Size(565, 29);
            this._lstFilter1.TabIndex = 15;
            this._lstFilter1.SelectedIndexChanged += new System.EventHandler(this.CheckAndChange);
            // 
            // _txtParams
            // 
            this.tableLayoutPanel1.SetColumnSpan(this._txtParams, 2);
            this._txtParams.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtParams.ForeColor = System.Drawing.Color.Blue;
            this._txtParams.Location = new System.Drawing.Point(252, 98);
            this._txtParams.Margin = new System.Windows.Forms.Padding(8);
            this._txtParams.Name = "_txtParams";
            this._txtParams.Size = new System.Drawing.Size(411, 29);
            this._txtParams.TabIndex = 7;
            this._txtParams.Watermark = null;
            this._txtParams.TextChanged += new System.EventHandler(this._txtName_TextChanged);
            // 
            // _btnEditParameters
            // 
            this._btnEditParameters.Image = global::MetaboliteLevels.Properties.Resources.MnuEnlargeList;
            this._btnEditParameters.Location = new System.Drawing.Point(679, 98);
            this._btnEditParameters.Margin = new System.Windows.Forms.Padding(8);
            this._btnEditParameters.Name = "_btnEditParameters";
            this._btnEditParameters.Size = new System.Drawing.Size(29, 29);
            this._btnEditParameters.TabIndex = 8;
            this._btnEditParameters.UseDefaultSize = true;
            this._btnEditParameters.UseVisualStyleBackColor = true;
            this._btnEditParameters.Click += new System.EventHandler(this._btnEditParameters_Click);
            // 
            // _lstSource
            // 
            this.tableLayoutPanel1.SetColumnSpan(this._lstSource, 3);
            this._lstSource.Dock = System.Windows.Forms.DockStyle.Top;
            this._lstSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstSource.ForeColor = System.Drawing.Color.Blue;
            this._lstSource.FormattingEnabled = true;
            this._lstSource.Location = new System.Drawing.Point(98, 180);
            this._lstSource.Margin = new System.Windows.Forms.Padding(8);
            this._lstSource.Name = "_lstSource";
            this._lstSource.Size = new System.Drawing.Size(565, 29);
            this._lstSource.TabIndex = 10;
            this._lstSource.SelectedIndexChanged += new System.EventHandler(this.CheckAndChange);
            // 
            // _btnSource
            // 
            this._btnSource.Image = global::MetaboliteLevels.Properties.Resources.MnuViewList;
            this._btnSource.Location = new System.Drawing.Point(679, 180);
            this._btnSource.Margin = new System.Windows.Forms.Padding(8);
            this._btnSource.Name = "_btnSource";
            this._btnSource.Size = new System.Drawing.Size(29, 29);
            this._btnSource.TabIndex = 11;
            this._btnSource.UseDefaultSize = true;
            this._btnSource.UseVisualStyleBackColor = true;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.linkLabel1, 4);
            this.linkLabel1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.linkLabel1.LinkArea = new System.Windows.Forms.LinkArea(31, 4);
            this.linkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabel1.Location = new System.Drawing.Point(93, 217);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(220, 19);
            this.linkLabel1.TabIndex = 13;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Intensity data not used, click here for details.";
            this.linkLabel1.UseCompatibleTextRendering = true;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // _tlpPreivew
            // 
            this._tlpPreivew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._tlpPreivew.AutoSize = true;
            this._tlpPreivew.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._tlpPreivew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this._tlpPreivew.ColumnCount = 2;
            this._tlpPreivew.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._tlpPreivew.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this._tlpPreivew.Controls.Add(this._lblPreviewTitle, 0, 0);
            this._tlpPreivew.Controls.Add(this._flpPreviewButtons, 1, 0);
            this._tlpPreivew.Controls.Add(this.flowLayoutPanel3, 0, 1);
            this._tlpPreivew.ForeColor = System.Drawing.Color.Black;
            this._tlpPreivew.Location = new System.Drawing.Point(8, 8);
            this._tlpPreivew.Margin = new System.Windows.Forms.Padding(8);
            this._tlpPreivew.Name = "_tlpPreivew";
            this._tlpPreivew.RowCount = 2;
            this._tlpPreivew.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._tlpPreivew.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._tlpPreivew.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this._tlpPreivew.Size = new System.Drawing.Size(227, 70);
            this._tlpPreivew.TabIndex = 13;
            // 
            // _lblPreviewTitle
            // 
            this._lblPreviewTitle.BackColor = System.Drawing.Color.LightSteelBlue;
            this._lblPreviewTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this._lblPreviewTitle.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblPreviewTitle.ForeColor = System.Drawing.Color.Black;
            this._lblPreviewTitle.Location = new System.Drawing.Point(0, 0);
            this._lblPreviewTitle.Margin = new System.Windows.Forms.Padding(0);
            this._lblPreviewTitle.Name = "_lblPreviewTitle";
            this._lblPreviewTitle.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this._lblPreviewTitle.Size = new System.Drawing.Size(128, 33);
            this._lblPreviewTitle.TabIndex = 1;
            this._lblPreviewTitle.Text = "Preview";
            this._lblPreviewTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _flpPreviewButtons
            // 
            this._flpPreviewButtons.AutoSize = true;
            this._flpPreviewButtons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._flpPreviewButtons.BackColor = System.Drawing.Color.LightSteelBlue;
            this._flpPreviewButtons.Controls.Add(this._btnSelectPreview);
            this._flpPreviewButtons.Controls.Add(this.ctlButton2);
            this._flpPreviewButtons.Controls.Add(this.ctlButton3);
            this._flpPreviewButtons.ForeColor = System.Drawing.Color.Black;
            this._flpPreviewButtons.Location = new System.Drawing.Point(128, 0);
            this._flpPreviewButtons.Margin = new System.Windows.Forms.Padding(0);
            this._flpPreviewButtons.Name = "_flpPreviewButtons";
            this._flpPreviewButtons.Padding = new System.Windows.Forms.Padding(2);
            this._flpPreviewButtons.Size = new System.Drawing.Size(99, 33);
            this._flpPreviewButtons.TabIndex = 20;
            this._flpPreviewButtons.WrapContents = false;
            // 
            // _btnSelectPreview
            // 
            this._btnSelectPreview.BackColor = System.Drawing.SystemColors.Control;
            this._btnSelectPreview.Image = global::MetaboliteLevels.Properties.Resources.MnuPreviewSelect;
            this._btnSelectPreview.Location = new System.Drawing.Point(2, 2);
            this._btnSelectPreview.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this._btnSelectPreview.Name = "_btnSelectPreview";
            this._btnSelectPreview.Size = new System.Drawing.Size(29, 29);
            this._btnSelectPreview.TabIndex = 0;
            this._btnSelectPreview.Text = null;
            this.toolTip1.SetToolTip(this._btnSelectPreview, "Select preview peak");
            this._btnSelectPreview.UseDefaultSize = true;
            this._btnSelectPreview.UseVisualStyleBackColor = false;
            this._btnSelectPreview.Click += new System.EventHandler(this._btnSelectPreview_Click);
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
            this.ctlButton2.UseVisualStyleBackColor = false;
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
            this.ctlButton3.UseVisualStyleBackColor = false;
            this.ctlButton3.Click += new System.EventHandler(this._btnNextPreview_Click);
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.AutoSize = true;
            this.flowLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._tlpPreivew.SetColumnSpan(this.flowLayoutPanel3, 2);
            this.flowLayoutPanel3.Controls.Add(this._lblPreview);
            this.flowLayoutPanel3.Controls.Add(this._lblPreview2);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(0, 33);
            this.flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(227, 37);
            this.flowLayoutPanel3.TabIndex = 19;
            this.flowLayoutPanel3.WrapContents = false;
            // 
            // _lblPreview
            // 
            this._lblPreview.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._lblPreview.AutoSize = true;
            this._lblPreview.ForeColor = System.Drawing.Color.Black;
            this._lblPreview.Location = new System.Drawing.Point(8, 8);
            this._lblPreview.Margin = new System.Windows.Forms.Padding(8, 8, 0, 8);
            this._lblPreview.Name = "_lblPreview";
            this._lblPreview.Size = new System.Drawing.Size(46, 21);
            this._lblPreview.TabIndex = 4;
            this._lblPreview.Text = "LN0: ";
            // 
            // _lblPreview2
            // 
            this._lblPreview2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._lblPreview2.AutoSize = true;
            this._lblPreview2.ForeColor = System.Drawing.Color.Gray;
            this._lblPreview2.Location = new System.Drawing.Point(54, 8);
            this._lblPreview2.Margin = new System.Windows.Forms.Padding(0, 8, 8, 8);
            this._lblPreview2.Name = "_lblPreview2";
            this._lblPreview2.Size = new System.Drawing.Size(94, 21);
            this._lblPreview2.TabIndex = 18;
            this._lblPreview2.Text = "1.23456789";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this._btnCancel);
            this.flowLayoutPanel1.Controls.Add(this._btnOk);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(445, 33);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(268, 50);
            this.flowLayoutPanel1.TabIndex = 14;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // _btnCancel
            // 
            this._btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("_btnCancel.Image")));
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
            this._btnOk.Image = ((System.Drawing.Image)(resources.GetObject("_btnOk.Image")));
            this._btnOk.Location = new System.Drawing.Point(3, 5);
            this._btnOk.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this._btnOk.Name = "_btnOk";
            this._btnOk.Size = new System.Drawing.Size(128, 40);
            this._btnOk.TabIndex = 0;
            this._btnOk.Text = "OK";
            this._btnOk.UseDefaultSize = true;
            this._btnOk.UseVisualStyleBackColor = true;
            // 
            // ctlTitleBar1
            // 
            this.ctlTitleBar1.AutoSize = true;
            this.ctlTitleBar1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ctlTitleBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlTitleBar1.HelpText = "Select the method and parameters for your statistic.\r\n\r\nOnce configured you can p" +
    "review your statistic on individual peaks before committing it.\r\n";
            this.ctlTitleBar1.Location = new System.Drawing.Point(0, 0);
            this.ctlTitleBar1.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.ctlTitleBar1.MinimumSize = new System.Drawing.Size(576, 0);
            this.ctlTitleBar1.Name = "ctlTitleBar1";
            this.ctlTitleBar1.Size = new System.Drawing.Size(716, 87);
            this.ctlTitleBar1.SubText = "Select the options for your statistic";
            this.ctlTitleBar1.TabIndex = 11;
            this.ctlTitleBar1.Text = "Select Statistic";
            this.ctlTitleBar1.WarningText = null;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this._tlpPreivew, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 744);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(716, 86);
            this.tableLayoutPanel2.TabIndex = 13;
            // 
            // FrmEditConfigurationStatistic
            // 
            this.AcceptButton = this._btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._btnCancel;
            this.ClientSize = new System.Drawing.Size(716, 830);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.ctlTitleBar1);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmEditConfigurationStatistic";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Statistics";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this._tlpPreivew.ResumeLayout(false);
            this._tlpPreivew.PerformLayout();
            this._flpPreviewButtons.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox _lstMethod;
        private System.Windows.Forms.Label _lblApply;
        private System.Windows.Forms.Label _lblAVec;
        private Controls.CtlTitleBar ctlTitleBar1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label4;
        private MGui.Controls.CtlTextBox _txtName;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label _lblBVec;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label _lblParams;
        private System.Windows.Forms.ComboBox _lstDiffPeak;
        private System.Windows.Forms.RadioButton _radSamePeak;
        private System.Windows.Forms.RadioButton _radBDiffPeak;
        private System.Windows.Forms.RadioButton _radBCorTime;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Label _lblPreview;
        private System.Windows.Forms.Label _lblPreview2;
        private System.Windows.Forms.ComboBox _lstFilter2;
        private System.Windows.Forms.ComboBox _lstFilter1;
        private MGui.Controls.CtlTextBox _txtParams;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Controls.CtlButton _btnCancel;
        private Controls.CtlButton _btnOk;
        private System.Windows.Forms.TableLayoutPanel _tlpPreivew;
        private System.Windows.Forms.Label _lblPreviewTitle;
        private System.Windows.Forms.FlowLayoutPanel _flpPreviewButtons;
        private Controls.CtlButton _btnSelectPreview;
        private Controls.CtlButton ctlButton2;
        private Controls.CtlButton ctlButton3;
        private MGui.Controls.CtlError _checker;
        private Controls.CtlButton _btnNewStatistic;
        private Controls.CtlButton _btnComment;
        private Controls.CtlButton _btnFilter1;
        private Controls.CtlButton _btnSelectDiffPeak;
        private Controls.CtlButton _btnFilter2;
        private Controls.CtlButton _btnEditParameters;
        private System.Windows.Forms.ComboBox _lstSource;
        private Controls.CtlButton _btnSource;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}