using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaboliteLevels.Gui.Controls
{
    partial class CtlEditPlotSetup
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label12 = new System.Windows.Forms.Label();
            this._txtClusterYAxis = new MGui.Controls.CtlTextBox();
            this._txtClusterInfo = new MGui.Controls.CtlTextBox();
            this._txtClusterXAxis = new MGui.Controls.CtlTextBox();
            this.label16 = new System.Windows.Forms.Label();
            this._txtClusterSubtitle = new MGui.Controls.CtlTextBox();
            this.label13 = new System.Windows.Forms.Label();
            this._txtClusterTitle = new MGui.Controls.CtlTextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this._xRmin = new System.Windows.Forms.ComboBox();
            this._xrMax = new System.Windows.Forms.ComboBox();
            this._yrMin = new System.Windows.Forms.ComboBox();
            this._yrMax = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
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
            this.tableLayoutPanel1.Controls.Add(this.label47, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label48, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this._xRmin, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this._xrMax, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this._yrMin, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this._yrMax, 2, 6);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(256, 254);
            this.tableLayoutPanel1.TabIndex = 16;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 8);
            this.label12.Margin = new System.Windows.Forms.Padding(8);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(59, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "Information";
            // 
            // _txtClusterYAxis
            // 
            this.tableLayoutPanel1.SetColumnSpan(this._txtClusterYAxis, 2);
            this._txtClusterYAxis.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtClusterYAxis.Location = new System.Drawing.Point(83, 189);
            this._txtClusterYAxis.Margin = new System.Windows.Forms.Padding(8);
            this._txtClusterYAxis.Name = "_txtClusterYAxis";
            this._txtClusterYAxis.Size = new System.Drawing.Size(165, 20);
            this._txtClusterYAxis.TabIndex = 14;
            this._txtClusterYAxis.Watermark = null;
            // 
            // _txtClusterInfo
            // 
            this.tableLayoutPanel1.SetColumnSpan(this._txtClusterInfo, 2);
            this._txtClusterInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtClusterInfo.Location = new System.Drawing.Point(83, 8);
            this._txtClusterInfo.Margin = new System.Windows.Forms.Padding(8);
            this._txtClusterInfo.Name = "_txtClusterInfo";
            this._txtClusterInfo.Size = new System.Drawing.Size(165, 20);
            this._txtClusterInfo.TabIndex = 6;
            this._txtClusterInfo.Watermark = null;
            // 
            // _txtClusterXAxis
            // 
            this.tableLayoutPanel1.SetColumnSpan(this._txtClusterXAxis, 2);
            this._txtClusterXAxis.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtClusterXAxis.Location = new System.Drawing.Point(83, 116);
            this._txtClusterXAxis.Margin = new System.Windows.Forms.Padding(8);
            this._txtClusterXAxis.Name = "_txtClusterXAxis";
            this._txtClusterXAxis.Size = new System.Drawing.Size(165, 20);
            this._txtClusterXAxis.TabIndex = 12;
            this._txtClusterXAxis.Watermark = null;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(8, 189);
            this.label16.Margin = new System.Windows.Forms.Padding(8);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(35, 13);
            this.label16.TabIndex = 13;
            this.label16.Text = "Y-axis";
            // 
            // _txtClusterSubtitle
            // 
            this.tableLayoutPanel1.SetColumnSpan(this._txtClusterSubtitle, 2);
            this._txtClusterSubtitle.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtClusterSubtitle.Location = new System.Drawing.Point(83, 80);
            this._txtClusterSubtitle.Margin = new System.Windows.Forms.Padding(8);
            this._txtClusterSubtitle.Name = "_txtClusterSubtitle";
            this._txtClusterSubtitle.Size = new System.Drawing.Size(165, 20);
            this._txtClusterSubtitle.TabIndex = 10;
            this._txtClusterSubtitle.Watermark = null;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 44);
            this.label13.Margin = new System.Windows.Forms.Padding(8);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(27, 13);
            this.label13.TabIndex = 7;
            this.label13.Text = "Title";
            // 
            // _txtClusterTitle
            // 
            this.tableLayoutPanel1.SetColumnSpan(this._txtClusterTitle, 2);
            this._txtClusterTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtClusterTitle.Location = new System.Drawing.Point(83, 44);
            this._txtClusterTitle.Margin = new System.Windows.Forms.Padding(8);
            this._txtClusterTitle.Name = "_txtClusterTitle";
            this._txtClusterTitle.Size = new System.Drawing.Size(165, 20);
            this._txtClusterTitle.TabIndex = 8;
            this._txtClusterTitle.Watermark = null;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(8, 80);
            this.label15.Margin = new System.Windows.Forms.Padding(8);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(42, 13);
            this.label15.TabIndex = 9;
            this.label15.Text = "Subtitle";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(8, 116);
            this.label14.Margin = new System.Windows.Forms.Padding(8);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(35, 13);
            this.label14.TabIndex = 11;
            this.label14.Text = "X-axis";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(8, 152);
            this.label47.Margin = new System.Windows.Forms.Padding(8);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(49, 13);
            this.label47.TabIndex = 11;
            this.label47.Text = "X-Range";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(8, 225);
            this.label48.Margin = new System.Windows.Forms.Padding(8);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(49, 13);
            this.label48.TabIndex = 11;
            this.label48.Text = "Y-Range";
            // 
            // _xRmin
            // 
            this._xRmin.Dock = System.Windows.Forms.DockStyle.Top;
            this._xRmin.FormattingEnabled = true;
            this._xRmin.Location = new System.Drawing.Point(83, 152);
            this._xRmin.Margin = new System.Windows.Forms.Padding(8);
            this._xRmin.Name = "_xRmin";
            this._xRmin.Size = new System.Drawing.Size(74, 21);
            this._xRmin.TabIndex = 15;
            // 
            // _xrMax
            // 
            this._xrMax.Dock = System.Windows.Forms.DockStyle.Top;
            this._xrMax.FormattingEnabled = true;
            this._xrMax.Location = new System.Drawing.Point(173, 152);
            this._xrMax.Margin = new System.Windows.Forms.Padding(8);
            this._xrMax.Name = "_xrMax";
            this._xrMax.Size = new System.Drawing.Size(75, 21);
            this._xrMax.TabIndex = 15;
            // 
            // _yrMin
            // 
            this._yrMin.Dock = System.Windows.Forms.DockStyle.Top;
            this._yrMin.FormattingEnabled = true;
            this._yrMin.Location = new System.Drawing.Point(83, 225);
            this._yrMin.Margin = new System.Windows.Forms.Padding(8);
            this._yrMin.Name = "_yrMin";
            this._yrMin.Size = new System.Drawing.Size(74, 21);
            this._yrMin.TabIndex = 15;
            // 
            // _yrMax
            // 
            this._yrMax.Dock = System.Windows.Forms.DockStyle.Top;
            this._yrMax.FormattingEnabled = true;
            this._yrMax.Location = new System.Drawing.Point(173, 225);
            this._yrMax.Margin = new System.Windows.Forms.Padding(8);
            this._yrMax.Name = "_yrMax";
            this._yrMax.Size = new System.Drawing.Size(75, 21);
            this._yrMax.TabIndex = 15;
            // 
            // CtlEditPlotOptions
            // 
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(256, 0);
            this.Name = "CtlEditPlotOptions";
            this.Size = new System.Drawing.Size(256, 254);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label12;
        private MGui.Controls.CtlTextBox _txtClusterYAxis;
        private MGui.Controls.CtlTextBox _txtClusterInfo;
        private MGui.Controls.CtlTextBox _txtClusterXAxis;
        private System.Windows.Forms.Label label16;
        private MGui.Controls.CtlTextBox _txtClusterSubtitle;
        private System.Windows.Forms.Label label13;
        private MGui.Controls.CtlTextBox _txtClusterTitle;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.ComboBox _xRmin;
        private System.Windows.Forms.ComboBox _xrMax;
        private System.Windows.Forms.ComboBox _yrMin;
        private System.Windows.Forms.ComboBox _yrMax;
    }
}
