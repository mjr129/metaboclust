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
            this.ctlButton1 = new MetaboliteLevels.Gui.Controls.CtlButton();
            this.ctlButton2 = new MetaboliteLevels.Gui.Controls.CtlButton();
            this.ctlButton3 = new MetaboliteLevels.Gui.Controls.CtlButton();
            this.ctlButton4 = new MetaboliteLevels.Gui.Controls.CtlButton();
            this.ctlButton5 = new MetaboliteLevels.Gui.Controls.CtlButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
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
            this.tableLayoutPanel1.Controls.Add(this.label47, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label48, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this._xRmin, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this._xrMax, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this._yrMin, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this._yrMax, 2, 6);
            this.tableLayoutPanel1.Controls.Add(this.ctlButton1, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.ctlButton2, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.ctlButton3, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.ctlButton4, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.ctlButton5, 3, 5);
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(512, 299);
            this.tableLayoutPanel1.TabIndex = 16;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 8);
            this.label12.Margin = new System.Windows.Forms.Padding(8);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(148, 21);
            this.label12.TabIndex = 0;
            this.label12.Text = "Information bar text";
            // 
            // _txtClusterYAxis
            // 
            this.tableLayoutPanel1.SetColumnSpan(this._txtClusterYAxis, 2);
            this._txtClusterYAxis.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtClusterYAxis.Location = new System.Drawing.Point(172, 225);
            this._txtClusterYAxis.Margin = new System.Windows.Forms.Padding(8);
            this._txtClusterYAxis.Name = "_txtClusterYAxis";
            this._txtClusterYAxis.Size = new System.Drawing.Size(286, 29);
            this._txtClusterYAxis.TabIndex = 14;
            this._txtClusterYAxis.Watermark = null;
            // 
            // _txtClusterInfo
            // 
            this.tableLayoutPanel1.SetColumnSpan(this._txtClusterInfo, 2);
            this._txtClusterInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtClusterInfo.Location = new System.Drawing.Point(172, 8);
            this._txtClusterInfo.Margin = new System.Windows.Forms.Padding(8);
            this._txtClusterInfo.Name = "_txtClusterInfo";
            this._txtClusterInfo.Size = new System.Drawing.Size(286, 29);
            this._txtClusterInfo.TabIndex = 6;
            this._txtClusterInfo.Watermark = null;
            // 
            // _txtClusterXAxis
            // 
            this.tableLayoutPanel1.SetColumnSpan(this._txtClusterXAxis, 2);
            this._txtClusterXAxis.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtClusterXAxis.Location = new System.Drawing.Point(172, 143);
            this._txtClusterXAxis.Margin = new System.Windows.Forms.Padding(8);
            this._txtClusterXAxis.Name = "_txtClusterXAxis";
            this._txtClusterXAxis.Size = new System.Drawing.Size(286, 29);
            this._txtClusterXAxis.TabIndex = 12;
            this._txtClusterXAxis.Watermark = null;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(8, 225);
            this.label16.Margin = new System.Windows.Forms.Padding(8);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(81, 21);
            this.label16.TabIndex = 13;
            this.label16.Text = "Y-axis title";
            // 
            // _txtClusterSubtitle
            // 
            this.tableLayoutPanel1.SetColumnSpan(this._txtClusterSubtitle, 2);
            this._txtClusterSubtitle.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtClusterSubtitle.Location = new System.Drawing.Point(172, 98);
            this._txtClusterSubtitle.Margin = new System.Windows.Forms.Padding(8);
            this._txtClusterSubtitle.Name = "_txtClusterSubtitle";
            this._txtClusterSubtitle.Size = new System.Drawing.Size(286, 29);
            this._txtClusterSubtitle.TabIndex = 10;
            this._txtClusterSubtitle.Watermark = null;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 53);
            this.label13.Margin = new System.Windows.Forms.Padding(8);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(67, 21);
            this.label13.TabIndex = 7;
            this.label13.Text = "Plot title";
            // 
            // _txtClusterTitle
            // 
            this.tableLayoutPanel1.SetColumnSpan(this._txtClusterTitle, 2);
            this._txtClusterTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtClusterTitle.Location = new System.Drawing.Point(172, 53);
            this._txtClusterTitle.Margin = new System.Windows.Forms.Padding(8);
            this._txtClusterTitle.Name = "_txtClusterTitle";
            this._txtClusterTitle.Size = new System.Drawing.Size(286, 29);
            this._txtClusterTitle.TabIndex = 8;
            this._txtClusterTitle.Watermark = null;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(8, 98);
            this.label15.Margin = new System.Windows.Forms.Padding(8);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(92, 21);
            this.label15.TabIndex = 9;
            this.label15.Text = "Plot subtitle";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(8, 143);
            this.label14.Margin = new System.Windows.Forms.Padding(8);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(81, 21);
            this.label14.TabIndex = 11;
            this.label14.Text = "X-axis title";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(8, 188);
            this.label47.Margin = new System.Windows.Forms.Padding(8);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(95, 21);
            this.label47.TabIndex = 11;
            this.label47.Text = "X-axis range";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(8, 270);
            this.label48.Margin = new System.Windows.Forms.Padding(8);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(95, 21);
            this.label48.TabIndex = 11;
            this.label48.Text = "Y-axis range";
            // 
            // _xRmin
            // 
            this._xRmin.Dock = System.Windows.Forms.DockStyle.Top;
            this._xRmin.FormattingEnabled = true;
            this._xRmin.Location = new System.Drawing.Point(172, 188);
            this._xRmin.Margin = new System.Windows.Forms.Padding(8);
            this._xRmin.Name = "_xRmin";
            this._xRmin.Size = new System.Drawing.Size(135, 29);
            this._xRmin.TabIndex = 15;
            // 
            // _xrMax
            // 
            this.tableLayoutPanel1.SetColumnSpan(this._xrMax, 2);
            this._xrMax.Dock = System.Windows.Forms.DockStyle.Top;
            this._xrMax.FormattingEnabled = true;
            this._xrMax.Location = new System.Drawing.Point(323, 188);
            this._xrMax.Margin = new System.Windows.Forms.Padding(8);
            this._xrMax.Name = "_xrMax";
            this._xrMax.Size = new System.Drawing.Size(181, 29);
            this._xrMax.TabIndex = 15;
            // 
            // _yrMin
            // 
            this._yrMin.Dock = System.Windows.Forms.DockStyle.Top;
            this._yrMin.FormattingEnabled = true;
            this._yrMin.Location = new System.Drawing.Point(172, 270);
            this._yrMin.Margin = new System.Windows.Forms.Padding(8);
            this._yrMin.Name = "_yrMin";
            this._yrMin.Size = new System.Drawing.Size(135, 29);
            this._yrMin.TabIndex = 15;
            // 
            // _yrMax
            // 
            this.tableLayoutPanel1.SetColumnSpan(this._yrMax, 2);
            this._yrMax.Dock = System.Windows.Forms.DockStyle.Top;
            this._yrMax.FormattingEnabled = true;
            this._yrMax.Location = new System.Drawing.Point(323, 270);
            this._yrMax.Margin = new System.Windows.Forms.Padding(8);
            this._yrMax.Name = "_yrMax";
            this._yrMax.Size = new System.Drawing.Size(181, 29);
            this._yrMax.TabIndex = 15;
            // 
            // ctlButton1
            // 
            this.ctlButton1.Image = global::MetaboliteLevels.Properties.Resources.MnuEnlargeList;
            this.ctlButton1.Location = new System.Drawing.Point(474, 8);
            this.ctlButton1.Margin = new System.Windows.Forms.Padding(8);
            this.ctlButton1.Name = "ctlButton1";
            this.ctlButton1.Size = new System.Drawing.Size(29, 29);
            this.ctlButton1.TabIndex = 16;
            this.ctlButton1.Text = "";
            this.ctlButton1.UseDefaultSize = true;
            this.ctlButton1.UseVisualStyleBackColor = true;
            this.ctlButton1.Click += new System.EventHandler(this.ctlButton1_Click);
            // 
            // ctlButton2
            // 
            this.ctlButton2.Image = global::MetaboliteLevels.Properties.Resources.MnuEnlargeList;
            this.ctlButton2.Location = new System.Drawing.Point(474, 53);
            this.ctlButton2.Margin = new System.Windows.Forms.Padding(8);
            this.ctlButton2.Name = "ctlButton2";
            this.ctlButton2.Size = new System.Drawing.Size(29, 29);
            this.ctlButton2.TabIndex = 16;
            this.ctlButton2.Text = "";
            this.ctlButton2.UseDefaultSize = true;
            this.ctlButton2.UseVisualStyleBackColor = true;
            this.ctlButton2.Click += new System.EventHandler(this.ctlButton2_Click);
            // 
            // ctlButton3
            // 
            this.ctlButton3.Image = global::MetaboliteLevels.Properties.Resources.MnuEnlargeList;
            this.ctlButton3.Location = new System.Drawing.Point(474, 98);
            this.ctlButton3.Margin = new System.Windows.Forms.Padding(8);
            this.ctlButton3.Name = "ctlButton3";
            this.ctlButton3.Size = new System.Drawing.Size(29, 29);
            this.ctlButton3.TabIndex = 16;
            this.ctlButton3.Text = "";
            this.ctlButton3.UseDefaultSize = true;
            this.ctlButton3.UseVisualStyleBackColor = true;
            this.ctlButton3.Click += new System.EventHandler(this.ctlButton3_Click);
            // 
            // ctlButton4
            // 
            this.ctlButton4.Image = global::MetaboliteLevels.Properties.Resources.MnuEnlargeList;
            this.ctlButton4.Location = new System.Drawing.Point(474, 143);
            this.ctlButton4.Margin = new System.Windows.Forms.Padding(8);
            this.ctlButton4.Name = "ctlButton4";
            this.ctlButton4.Size = new System.Drawing.Size(29, 29);
            this.ctlButton4.TabIndex = 16;
            this.ctlButton4.Text = "";
            this.ctlButton4.UseDefaultSize = true;
            this.ctlButton4.UseVisualStyleBackColor = true;
            this.ctlButton4.Click += new System.EventHandler(this.ctlButton4_Click);
            // 
            // ctlButton5
            // 
            this.ctlButton5.Image = global::MetaboliteLevels.Properties.Resources.MnuEnlargeList;
            this.ctlButton5.Location = new System.Drawing.Point(474, 225);
            this.ctlButton5.Margin = new System.Windows.Forms.Padding(8);
            this.ctlButton5.Name = "ctlButton5";
            this.ctlButton5.Size = new System.Drawing.Size(29, 29);
            this.ctlButton5.TabIndex = 16;
            this.ctlButton5.Text = "";
            this.ctlButton5.UseDefaultSize = true;
            this.ctlButton5.UseVisualStyleBackColor = true;
            this.ctlButton5.Click += new System.EventHandler(this.ctlButton5_Click);
            // 
            // CtlEditPlotSetup
            // 
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(512, 0);
            this.Name = "CtlEditPlotSetup";
            this.Size = new System.Drawing.Size(512, 299);
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
        private CtlButton ctlButton1;
        private CtlButton ctlButton2;
        private CtlButton ctlButton3;
        private CtlButton ctlButton4;
        private CtlButton ctlButton5;
    }
}
