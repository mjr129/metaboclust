﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Gui.Controls;

namespace MetaboliteLevels.Gui.Forms.Editing
{
    partial class FrmEditGroupBase
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
            this.ctlTitleBar1 = new CtlTitleBar();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this._txtId = new MGui.Controls.CtlTextBox();
            this._lblTimeRange = new System.Windows.Forms.Label();
            this._txtTimeRange = new MGui.Controls.CtlTextBox();
            this._txtDisplayOrder = new System.Windows.Forms.NumericUpDown();
            this._btnEditId = new CtlButton();
            this._txtTitle = new MGui.Controls.CtlTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._txtAbvTitle = new MGui.Controls.CtlTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this._txtComments = new MGui.Controls.CtlTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this._btnColour = new CtlButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._btnOk = new CtlButton();
            this._btnCancel = new CtlButton();
            this.label7 = new System.Windows.Forms.Label();
            this._lstIcon = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this._lstStyle = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._txtDisplayOrder)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
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
            this.ctlTitleBar1.Size = new System.Drawing.Size(939, 87);
            this.ctlTitleBar1.SubText = "Subtitle";
            this.ctlTitleBar1.TabIndex = 0;
            this.ctlTitleBar1.Text = "ctlTitleBar1";
            this.ctlTitleBar1.WarningText = null;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 8);
            this.tableLayoutPanel2.Controls.Add(this._txtTitle, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this._txtAbvTitle, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this._txtComments, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this._btnColour, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel1, 0, 9);
            this.tableLayoutPanel2.Controls.Add(this.label7, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this._lstIcon, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.label8, 2, 4);
            this.tableLayoutPanel2.Controls.Add(this._lstStyle, 2, 5);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 87);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 10;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(939, 685);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel2.SetColumnSpan(this.tableLayoutPanel1, 3);
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.label6, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._txtId, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this._lblTimeRange, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this._txtTimeRange, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this._txtDisplayOrder, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this._btnEditId, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 581);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(933, 45);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(276, 8);
            this.label6.Margin = new System.Windows.Forms.Padding(8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 21);
            this.label6.TabIndex = 2;
            this.label6.Text = "Display order";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 8);
            this.label5.Margin = new System.Windows.Forms.Padding(8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 21);
            this.label5.TabIndex = 1;
            this.label5.Text = "ID";
            // 
            // _txtId
            // 
            this._txtId.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtId.Location = new System.Drawing.Point(49, 8);
            this._txtId.Margin = new System.Windows.Forms.Padding(8);
            this._txtId.Name = "_txtId";
            this._txtId.ReadOnly = true;
            this._txtId.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this._txtId.Size = new System.Drawing.Size(174, 29);
            this._txtId.TabIndex = 0;
            this._txtId.Watermark = null;
            this._txtId.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // _lblTimeRange
            // 
            this._lblTimeRange.AutoSize = true;
            this._lblTimeRange.Location = new System.Drawing.Point(585, 8);
            this._lblTimeRange.Margin = new System.Windows.Forms.Padding(8);
            this._lblTimeRange.Name = "_lblTimeRange";
            this._lblTimeRange.Size = new System.Drawing.Size(148, 21);
            this._lblTimeRange.TabIndex = 1;
            this._lblTimeRange.Text = "Experimental Range";
            // 
            // _txtTimeRange
            // 
            this._txtTimeRange.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtTimeRange.Location = new System.Drawing.Point(749, 8);
            this._txtTimeRange.Margin = new System.Windows.Forms.Padding(8);
            this._txtTimeRange.Name = "_txtTimeRange";
            this._txtTimeRange.ReadOnly = true;
            this._txtTimeRange.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this._txtTimeRange.Size = new System.Drawing.Size(176, 29);
            this._txtTimeRange.TabIndex = 0;
            this._txtTimeRange.Watermark = null;
            this._txtTimeRange.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // _txtDisplayOrder
            // 
            this._txtDisplayOrder.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtDisplayOrder.Location = new System.Drawing.Point(395, 8);
            this._txtDisplayOrder.Margin = new System.Windows.Forms.Padding(8);
            this._txtDisplayOrder.Name = "_txtDisplayOrder";
            this._txtDisplayOrder.Size = new System.Drawing.Size(174, 29);
            this._txtDisplayOrder.TabIndex = 0;
            this._txtDisplayOrder.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // _btnEditId
            // 
            this._btnEditId.Image = global::MetaboliteLevels.Properties.Resources.MnuEdit;
            this._btnEditId.Location = new System.Drawing.Point(231, 8);
            this._btnEditId.Margin = new System.Windows.Forms.Padding(0, 8, 8, 8);
            this._btnEditId.Name = "_btnEditId";
            this._btnEditId.Size = new System.Drawing.Size(29, 29);
            this._btnEditId.TabIndex = 3;
            this._btnEditId.Text = "";
            this._btnEditId.UseDefaultSize = true;
            this._btnEditId.UseVisualStyleBackColor = true;
            this._btnEditId.Click += new System.EventHandler(this._btnEditId_Click);
            // 
            // _txtTitle
            // 
            this.tableLayoutPanel2.SetColumnSpan(this._txtTitle, 3);
            this._txtTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtTitle.Location = new System.Drawing.Point(8, 45);
            this._txtTitle.Margin = new System.Windows.Forms.Padding(8);
            this._txtTitle.Name = "_txtTitle";
            this._txtTitle.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this._txtTitle.Size = new System.Drawing.Size(923, 29);
            this._txtTitle.TabIndex = 0;
            this._txtTitle.Watermark = null;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.label1, 2);
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Margin = new System.Windows.Forms.Padding(8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Title";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.label2, 2);
            this.label2.Location = new System.Drawing.Point(8, 90);
            this.label2.Margin = new System.Windows.Forms.Padding(8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "Symbol";
            // 
            // _txtAbvTitle
            // 
            this.tableLayoutPanel2.SetColumnSpan(this._txtAbvTitle, 3);
            this._txtAbvTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtAbvTitle.Location = new System.Drawing.Point(8, 127);
            this._txtAbvTitle.Margin = new System.Windows.Forms.Padding(8);
            this._txtAbvTitle.Name = "_txtAbvTitle";
            this._txtAbvTitle.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this._txtAbvTitle.Size = new System.Drawing.Size(923, 29);
            this._txtAbvTitle.TabIndex = 0;
            this._txtAbvTitle.Watermark = null;
            this._txtAbvTitle.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.label3, 2);
            this.label3.Location = new System.Drawing.Point(8, 254);
            this.label3.Margin = new System.Windows.Forms.Padding(8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 21);
            this.label3.TabIndex = 1;
            this.label3.Text = "Comments";
            // 
            // _txtComments
            // 
            this.tableLayoutPanel2.SetColumnSpan(this._txtComments, 3);
            this._txtComments.Dock = System.Windows.Forms.DockStyle.Fill;
            this._txtComments.Location = new System.Drawing.Point(8, 291);
            this._txtComments.Margin = new System.Windows.Forms.Padding(8);
            this._txtComments.Multiline = true;
            this._txtComments.Name = "_txtComments";
            this._txtComments.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this._txtComments.Size = new System.Drawing.Size(923, 279);
            this._txtComments.TabIndex = 0;
            this._txtComments.Watermark = null;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 172);
            this.label4.Margin = new System.Windows.Forms.Padding(8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 21);
            this.label4.TabIndex = 1;
            this.label4.Text = "Colour";
            // 
            // _btnColour
            // 
            this._btnColour.Dock = System.Windows.Forms.DockStyle.Top;
            this._btnColour.Location = new System.Drawing.Point(8, 209);
            this._btnColour.Margin = new System.Windows.Forms.Padding(8);
            this._btnColour.Name = "_btnColour";
            this._btnColour.Size = new System.Drawing.Size(297, 29);
            this._btnColour.TabIndex = 2;
            this._btnColour.Click += new System.EventHandler(this.button3_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.SetColumnSpan(this.flowLayoutPanel1, 3);
            this.flowLayoutPanel1.Controls.Add(this._btnOk);
            this.flowLayoutPanel1.Controls.Add(this._btnCancel);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(651, 629);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(288, 56);
            this.flowLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // _btnOk
            // 
            this._btnOk.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnOk.Image = global::MetaboliteLevels.Properties.Resources.MnuAccept;
            this._btnOk.Location = new System.Drawing.Point(8, 8);
            this._btnOk.Margin = new System.Windows.Forms.Padding(8);
            this._btnOk.Name = "_btnOk";
            this._btnOk.Size = new System.Drawing.Size(128, 40);
            this._btnOk.TabIndex = 1;
            this._btnOk.Text = "OK";
            this._btnOk.UseDefaultSize = true;
            this._btnOk.UseVisualStyleBackColor = true;
            this._btnOk.Click += new System.EventHandler(this.button1_Click);
            // 
            // _btnCancel
            // 
            this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnCancel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(321, 172);
            this.label7.Margin = new System.Windows.Forms.Padding(8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 21);
            this.label7.TabIndex = 1;
            this.label7.Text = "Icon";
            // 
            // _lstIcon
            // 
            this._lstIcon.Dock = System.Windows.Forms.DockStyle.Top;
            this._lstIcon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstIcon.FormattingEnabled = true;
            this._lstIcon.Location = new System.Drawing.Point(321, 209);
            this._lstIcon.Margin = new System.Windows.Forms.Padding(8);
            this._lstIcon.Name = "_lstIcon";
            this._lstIcon.Size = new System.Drawing.Size(297, 29);
            this._lstIcon.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(634, 172);
            this.label8.Margin = new System.Windows.Forms.Padding(8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(30, 21);
            this.label8.TabIndex = 1;
            this.label8.Text = "Fill";
            // 
            // _lstStyle
            // 
            this._lstStyle.Dock = System.Windows.Forms.DockStyle.Top;
            this._lstStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstStyle.FormattingEnabled = true;
            this._lstStyle.Location = new System.Drawing.Point(634, 209);
            this._lstStyle.Margin = new System.Windows.Forms.Padding(8);
            this._lstStyle.Name = "_lstStyle";
            this._lstStyle.Size = new System.Drawing.Size(297, 29);
            this._lstStyle.TabIndex = 7;
            // 
            // FrmEditGroupBase
            // 
            this.AcceptButton = this._btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._btnCancel;
            this.ClientSize = new System.Drawing.Size(939, 772);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.ctlTitleBar1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmEditGroupBase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Experimental Group";
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._txtDisplayOrder)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.CtlTitleBar ctlTitleBar1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private MGui.Controls.CtlTextBox _txtTitle;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Controls.CtlButton _btnOk;
        private Controls.CtlButton _btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private MGui.Controls.CtlTextBox _txtAbvTitle;
        private System.Windows.Forms.Label label3;
        private MGui.Controls.CtlTextBox _txtComments;
        private System.Windows.Forms.Label label4;
        private CtlButton _btnColour;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label _lblTimeRange;
        private MGui.Controls.CtlTextBox _txtId;
        private MGui.Controls.CtlTextBox _txtTimeRange;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown _txtDisplayOrder;
        private Controls.CtlButton _btnEditId;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox _lstIcon;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox _lstStyle;
    }
}