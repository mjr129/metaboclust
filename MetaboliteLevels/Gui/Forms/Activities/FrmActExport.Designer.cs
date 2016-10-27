using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Gui.Controls;

namespace MetaboliteLevels.Gui.Forms.Activities
{
    partial class FrmActExport
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmActExport));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._txtClusters = new System.Windows.Forms.TextBox();
            this._txtObservations = new System.Windows.Forms.TextBox();
            this._txtPeaks = new System.Windows.Forms.TextBox();
            this._btnData = new CtlButton();
            this._btnObs = new CtlButton();
            this._btnPeaks = new CtlButton();
            this._chkObs = new System.Windows.Forms.CheckBox();
            this._chkPeaks = new System.Windows.Forms.CheckBox();
            this._chkClusters = new System.Windows.Forms.CheckBox();
            this._btnClusters = new CtlButton();
            this._chkOther = new System.Windows.Forms.CheckBox();
            this._txtOther = new System.Windows.Forms.TextBox();
            this._btnOther = new CtlButton();
            this._btnIntensitySource = new CtlButton();
            this._chkData = new System.Windows.Forms.CheckBox();
            this._txtData = new System.Windows.Forms.TextBox();
            this._lstIntensitySource = new System.Windows.Forms.ComboBox();
            this.ctlLabel1 = new CtlLabel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._btnCancel = new CtlButton();
            this._btnOk = new CtlButton();
            this.ctlTitleBar1 = new CtlTitleBar();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this._txtClusters, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this._txtObservations, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this._txtPeaks, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this._btnData, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this._btnObs, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this._btnPeaks, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this._chkObs, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this._chkPeaks, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this._chkClusters, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this._btnClusters, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this._chkOther, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this._txtOther, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this._btnOther, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this._btnIntensitySource, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this._chkData, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this._txtData, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this._lstIntensitySource, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.ctlLabel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 87);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(879, 270);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // _txtClusters
            // 
            this._txtClusters.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtClusters.Enabled = false;
            this._txtClusters.Location = new System.Drawing.Point(193, 188);
            this._txtClusters.Margin = new System.Windows.Forms.Padding(8);
            this._txtClusters.Name = "_txtClusters";
            this._txtClusters.Size = new System.Drawing.Size(633, 29);
            this._txtClusters.TabIndex = 4;
            // 
            // _txtObservations
            // 
            this._txtObservations.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtObservations.Enabled = false;
            this._txtObservations.Location = new System.Drawing.Point(193, 98);
            this._txtObservations.Margin = new System.Windows.Forms.Padding(8);
            this._txtObservations.Name = "_txtObservations";
            this._txtObservations.Size = new System.Drawing.Size(633, 29);
            this._txtObservations.TabIndex = 1;
            // 
            // _txtPeaks
            // 
            this._txtPeaks.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtPeaks.Enabled = false;
            this._txtPeaks.Location = new System.Drawing.Point(193, 143);
            this._txtPeaks.Margin = new System.Windows.Forms.Padding(8);
            this._txtPeaks.Name = "_txtPeaks";
            this._txtPeaks.Size = new System.Drawing.Size(633, 29);
            this._txtPeaks.TabIndex = 1;
            // 
            // _btnData
            // 
            this._btnData.Enabled = false;
            this._btnData.Image = global::MetaboliteLevels.Properties.Resources.MnuEnlargeList;
            this._btnData.Location = new System.Drawing.Point(842, 8);
            this._btnData.Margin = new System.Windows.Forms.Padding(8);
            this._btnData.Name = "_btnData";
            this._btnData.Size = new System.Drawing.Size(29, 29);
            this._btnData.TabIndex = 2;
            this._btnData.Text = "";
            this._btnData.UseDefaultSize = true;
            this._btnData.UseVisualStyleBackColor = true;
            this._btnData.Click += new System.EventHandler(this._btnData_Click);
            // 
            // _btnObs
            // 
            this._btnObs.Enabled = false;
            this._btnObs.Image = global::MetaboliteLevels.Properties.Resources.MnuEnlargeList;
            this._btnObs.Location = new System.Drawing.Point(842, 98);
            this._btnObs.Margin = new System.Windows.Forms.Padding(8);
            this._btnObs.Name = "_btnObs";
            this._btnObs.Size = new System.Drawing.Size(29, 29);
            this._btnObs.TabIndex = 2;
            this._btnObs.Text = "";
            this._btnObs.UseDefaultSize = true;
            this._btnObs.UseVisualStyleBackColor = true;
            this._btnObs.Click += new System.EventHandler(this._btnObs_Click);
            // 
            // _btnPeaks
            // 
            this._btnPeaks.Enabled = false;
            this._btnPeaks.Image = global::MetaboliteLevels.Properties.Resources.MnuEnlargeList;
            this._btnPeaks.Location = new System.Drawing.Point(842, 143);
            this._btnPeaks.Margin = new System.Windows.Forms.Padding(8);
            this._btnPeaks.Name = "_btnPeaks";
            this._btnPeaks.Size = new System.Drawing.Size(29, 29);
            this._btnPeaks.TabIndex = 2;
            this._btnPeaks.Text = "";
            this._btnPeaks.UseDefaultSize = true;
            this._btnPeaks.UseVisualStyleBackColor = true;
            this._btnPeaks.Click += new System.EventHandler(this._btnPeaks_Click);
            // 
            // _chkObs
            // 
            this._chkObs.AutoSize = true;
            this._chkObs.Location = new System.Drawing.Point(8, 98);
            this._chkObs.Margin = new System.Windows.Forms.Padding(8);
            this._chkObs.Name = "_chkObs";
            this._chkObs.Size = new System.Drawing.Size(121, 25);
            this._chkObs.TabIndex = 3;
            this._chkObs.Text = "Observations";
            this._chkObs.UseVisualStyleBackColor = true;
            this._chkObs.CheckedChanged += new System.EventHandler(this._chkObs_CheckedChanged);
            // 
            // _chkPeaks
            // 
            this._chkPeaks.AutoSize = true;
            this._chkPeaks.Location = new System.Drawing.Point(8, 143);
            this._chkPeaks.Margin = new System.Windows.Forms.Padding(8);
            this._chkPeaks.Name = "_chkPeaks";
            this._chkPeaks.Size = new System.Drawing.Size(69, 25);
            this._chkPeaks.TabIndex = 3;
            this._chkPeaks.Text = "Peaks";
            this._chkPeaks.UseVisualStyleBackColor = true;
            this._chkPeaks.CheckedChanged += new System.EventHandler(this._chkPeaks_CheckedChanged);
            // 
            // _chkClusters
            // 
            this._chkClusters.AutoSize = true;
            this._chkClusters.Location = new System.Drawing.Point(8, 188);
            this._chkClusters.Margin = new System.Windows.Forms.Padding(8);
            this._chkClusters.Name = "_chkClusters";
            this._chkClusters.Size = new System.Drawing.Size(169, 25);
            this._chkClusters.TabIndex = 3;
            this._chkClusters.Text = "Cluster assignments";
            this._chkClusters.UseVisualStyleBackColor = true;
            this._chkClusters.CheckedChanged += new System.EventHandler(this._chkClusters_CheckedChanged);
            // 
            // _btnClusters
            // 
            this._btnClusters.Enabled = false;
            this._btnClusters.Image = global::MetaboliteLevels.Properties.Resources.MnuEnlargeList;
            this._btnClusters.Location = new System.Drawing.Point(842, 188);
            this._btnClusters.Margin = new System.Windows.Forms.Padding(8);
            this._btnClusters.Name = "_btnClusters";
            this._btnClusters.Size = new System.Drawing.Size(29, 29);
            this._btnClusters.TabIndex = 2;
            this._btnClusters.Text = "";
            this._btnClusters.UseDefaultSize = true;
            this._btnClusters.UseVisualStyleBackColor = true;
            this._btnClusters.Click += new System.EventHandler(this._btnClusters_Click);
            // 
            // _chkOther
            // 
            this._chkOther.AutoSize = true;
            this._chkOther.Location = new System.Drawing.Point(8, 233);
            this._chkOther.Margin = new System.Windows.Forms.Padding(8);
            this._chkOther.Name = "_chkOther";
            this._chkOther.Size = new System.Drawing.Size(92, 25);
            this._chkOther.TabIndex = 3;
            this._chkOther.Text = "Other (...)";
            this._chkOther.UseVisualStyleBackColor = true;
            this._chkOther.CheckedChanged += new System.EventHandler(this._chkOther_CheckedChanged);
            // 
            // _txtOther
            // 
            this._txtOther.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtOther.Enabled = false;
            this._txtOther.Location = new System.Drawing.Point(193, 233);
            this._txtOther.Margin = new System.Windows.Forms.Padding(8);
            this._txtOther.Name = "_txtOther";
            this._txtOther.ReadOnly = true;
            this._txtOther.Size = new System.Drawing.Size(633, 29);
            this._txtOther.TabIndex = 4;
            // 
            // _btnOther
            // 
            this._btnOther.Enabled = false;
            this._btnOther.Image = global::MetaboliteLevels.Properties.Resources.MnuEnlargeList;
            this._btnOther.Location = new System.Drawing.Point(842, 233);
            this._btnOther.Margin = new System.Windows.Forms.Padding(8);
            this._btnOther.Name = "_btnOther";
            this._btnOther.Size = new System.Drawing.Size(29, 29);
            this._btnOther.TabIndex = 2;
            this._btnOther.Text = "";
            this._btnOther.UseDefaultSize = true;
            this._btnOther.UseVisualStyleBackColor = true;
            this._btnOther.Click += new System.EventHandler(this._btnOther_Click);
            // 
            // _btnIntensitySource
            // 
            this._btnIntensitySource.Enabled = false;
            this._btnIntensitySource.Image = ((System.Drawing.Image)(resources.GetObject("_btnIntensitySource.Image")));
            this._btnIntensitySource.Location = new System.Drawing.Point(842, 53);
            this._btnIntensitySource.Margin = new System.Windows.Forms.Padding(8);
            this._btnIntensitySource.Name = "_btnIntensitySource";
            this._btnIntensitySource.Size = new System.Drawing.Size(29, 29);
            this._btnIntensitySource.TabIndex = 2;
            this._btnIntensitySource.Text = "";
            this._btnIntensitySource.UseDefaultSize = true;
            this._btnIntensitySource.UseVisualStyleBackColor = true;
            // 
            // _chkData
            // 
            this._chkData.AutoSize = true;
            this._chkData.Location = new System.Drawing.Point(8, 53);
            this._chkData.Margin = new System.Windows.Forms.Padding(8);
            this._chkData.Name = "_chkData";
            this._chkData.Size = new System.Drawing.Size(99, 25);
            this._chkData.TabIndex = 3;
            this._chkData.Text = "Intensities";
            this._chkData.UseVisualStyleBackColor = true;
            this._chkData.CheckedChanged += new System.EventHandler(this._chkData_CheckedChanged);
            // 
            // _txtData
            // 
            this._txtData.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtData.Enabled = false;
            this._txtData.Location = new System.Drawing.Point(193, 53);
            this._txtData.Margin = new System.Windows.Forms.Padding(8);
            this._txtData.Name = "_txtData";
            this._txtData.Size = new System.Drawing.Size(633, 29);
            this._txtData.TabIndex = 1;
            // 
            // _lstIntensitySource
            // 
            this._lstIntensitySource.Dock = System.Windows.Forms.DockStyle.Top;
            this._lstIntensitySource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstIntensitySource.FormattingEnabled = true;
            this._lstIntensitySource.Location = new System.Drawing.Point(193, 8);
            this._lstIntensitySource.Margin = new System.Windows.Forms.Padding(8);
            this._lstIntensitySource.Name = "_lstIntensitySource";
            this._lstIntensitySource.Size = new System.Drawing.Size(633, 29);
            this._lstIntensitySource.TabIndex = 5;
            // 
            // ctlLabel1
            // 
            this.ctlLabel1.AutoSize = true;
            this.ctlLabel1.Location = new System.Drawing.Point(8, 8);
            this.ctlLabel1.Margin = new System.Windows.Forms.Padding(8);
            this.ctlLabel1.Name = "ctlLabel1";
            this.ctlLabel1.Size = new System.Drawing.Size(50, 21);
            this.ctlLabel1.TabIndex = 6;
            this.ctlLabel1.Text = "Using";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this._btnCancel);
            this.flowLayoutPanel1.Controls.Add(this._btnOk);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 549);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(879, 56);
            this.flowLayoutPanel1.TabIndex = 5;
            // 
            // _btnCancel
            // 
            this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnCancel.Image = global::MetaboliteLevels.Properties.Resources.MnuCancel;
            this._btnCancel.Location = new System.Drawing.Point(743, 8);
            this._btnCancel.Margin = new System.Windows.Forms.Padding(8);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(128, 40);
            this._btnCancel.TabIndex = 0;
            this._btnCancel.Text = "Cancel";
            this._btnCancel.UseDefaultSize = true;
            this._btnCancel.UseVisualStyleBackColor = true;
            // 
            // _btnOk
            // 
            this._btnOk.Image = global::MetaboliteLevels.Properties.Resources.MnuAccept;
            this._btnOk.Location = new System.Drawing.Point(599, 8);
            this._btnOk.Margin = new System.Windows.Forms.Padding(8);
            this._btnOk.Name = "_btnOk";
            this._btnOk.Size = new System.Drawing.Size(128, 40);
            this._btnOk.TabIndex = 0;
            this._btnOk.Text = "OK";
            this._btnOk.UseDefaultSize = true;
            this._btnOk.UseVisualStyleBackColor = true;
            this._btnOk.Click += new System.EventHandler(this._btnOk_Click);
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
            this.ctlTitleBar1.Size = new System.Drawing.Size(879, 87);
            this.ctlTitleBar1.SubText = "Select the data you\'d like to export";
            this.ctlTitleBar1.TabIndex = 6;
            this.ctlTitleBar1.Text = "Export";
            this.ctlTitleBar1.WarningText = null;
            // 
            // FrmActExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 605);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.ctlTitleBar1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmActExport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Export Data";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox _txtClusters;
        private System.Windows.Forms.TextBox _txtData;
        private System.Windows.Forms.TextBox _txtObservations;
        private System.Windows.Forms.TextBox _txtPeaks;
        private Controls.CtlButton _btnData;
        private Controls.CtlButton _btnObs;
        private Controls.CtlButton _btnPeaks;
        private System.Windows.Forms.CheckBox _chkData;
        private System.Windows.Forms.CheckBox _chkObs;
        private System.Windows.Forms.CheckBox _chkPeaks;
        private System.Windows.Forms.CheckBox _chkClusters;
        private Controls.CtlButton _btnClusters;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Controls.CtlButton _btnCancel;
        private Controls.CtlButton _btnOk;
        private Controls.CtlTitleBar ctlTitleBar1;
        private System.Windows.Forms.CheckBox _chkOther;
        private System.Windows.Forms.TextBox _txtOther;
        private Controls.CtlButton _btnOther;
        private System.Windows.Forms.ComboBox _lstIntensitySource;
        private Controls.CtlButton _btnIntensitySource;
        private Controls.CtlLabel ctlLabel1;
    }
}