using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Gui.Controls;

namespace MetaboliteLevels.Gui.Forms.Selection
{
    partial class FrmSelectAlgorithmType
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
            this.components = new System.ComponentModel.Container();
            this._btnTrend = new System.Windows.Forms.Button();
            this._btnStatistics = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._btnCorrections = new System.Windows.Forms.Button();
            this._btnMetrics = new System.Windows.Forms.Button();
            this._btnClusterers = new System.Windows.Forms.Button();
            this._btnShowAll = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ctlTitleBar1 = new CtlTitleBar();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this._btnCancel = new CtlButton();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _btnTrend
            // 
            this._btnTrend.BackColor = System.Drawing.Color.LightSteelBlue;
            this._btnTrend.FlatAppearance.BorderColor = System.Drawing.Color.LightSteelBlue;
            this._btnTrend.FlatAppearance.BorderSize = 0;
            this._btnTrend.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSteelBlue;
            this._btnTrend.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSlateGray;
            this._btnTrend.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this._btnTrend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnTrend.ForeColor = System.Drawing.Color.Blue;
            this._btnTrend.Image = global::MetaboliteLevels.Properties.Resources.IconScriptTrend;
            this._btnTrend.Location = new System.Drawing.Point(192, 32);
            this._btnTrend.Margin = new System.Windows.Forms.Padding(16, 32, 16, 16);
            this._btnTrend.Name = "_btnTrend";
            this._btnTrend.Padding = new System.Windows.Forms.Padding(8);
            this._btnTrend.Size = new System.Drawing.Size(128, 128);
            this._btnTrend.TabIndex = 0;
            this._btnTrend.Text = "Trends";
            this._btnTrend.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this._btnTrend.UseVisualStyleBackColor = false;
            this._btnTrend.Click += new System.EventHandler(this._chkTrend_Click);
            // 
            // _btnStatistics
            // 
            this._btnStatistics.BackColor = System.Drawing.Color.LightSteelBlue;
            this._btnStatistics.FlatAppearance.BorderColor = System.Drawing.Color.LightSteelBlue;
            this._btnStatistics.FlatAppearance.BorderSize = 0;
            this._btnStatistics.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSteelBlue;
            this._btnStatistics.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSlateGray;
            this._btnStatistics.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this._btnStatistics.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnStatistics.ForeColor = System.Drawing.Color.Blue;
            this._btnStatistics.Image = global::MetaboliteLevels.Properties.Resources.IconScriptStatistic;
            this._btnStatistics.Location = new System.Drawing.Point(352, 32);
            this._btnStatistics.Margin = new System.Windows.Forms.Padding(16, 32, 16, 16);
            this._btnStatistics.Name = "_btnStatistics";
            this._btnStatistics.Padding = new System.Windows.Forms.Padding(8);
            this._btnStatistics.Size = new System.Drawing.Size(128, 128);
            this._btnStatistics.TabIndex = 0;
            this._btnStatistics.Text = "Statistics";
            this._btnStatistics.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this._btnStatistics.UseVisualStyleBackColor = false;
            this._btnStatistics.Click += new System.EventHandler(this._chkStat_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this._btnCorrections);
            this.flowLayoutPanel1.Controls.Add(this._btnTrend);
            this.flowLayoutPanel1.Controls.Add(this._btnStatistics);
            this.flowLayoutPanel1.Controls.Add(this._btnMetrics);
            this.flowLayoutPanel1.Controls.Add(this._btnClusterers);
            this.flowLayoutPanel1.Controls.Add(this._btnShowAll);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 87);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(831, 181);
            this.flowLayoutPanel1.TabIndex = 4;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // _btnCorrections
            // 
            this._btnCorrections.BackColor = System.Drawing.Color.LightSteelBlue;
            this._btnCorrections.FlatAppearance.BorderColor = System.Drawing.Color.LightSteelBlue;
            this._btnCorrections.FlatAppearance.BorderSize = 0;
            this._btnCorrections.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSteelBlue;
            this._btnCorrections.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSlateGray;
            this._btnCorrections.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this._btnCorrections.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnCorrections.ForeColor = System.Drawing.Color.Blue;
            this._btnCorrections.Image = global::MetaboliteLevels.Properties.Resources.IconScriptCorrect;
            this._btnCorrections.Location = new System.Drawing.Point(32, 32);
            this._btnCorrections.Margin = new System.Windows.Forms.Padding(32, 32, 16, 16);
            this._btnCorrections.Name = "_btnCorrections";
            this._btnCorrections.Padding = new System.Windows.Forms.Padding(8);
            this._btnCorrections.Size = new System.Drawing.Size(128, 128);
            this._btnCorrections.TabIndex = 0;
            this._btnCorrections.Text = "Corrections";
            this._btnCorrections.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this._btnCorrections.UseVisualStyleBackColor = false;
            this._btnCorrections.Click += new System.EventHandler(this._chkCor_Click);
            // 
            // _btnMetrics
            // 
            this._btnMetrics.BackColor = System.Drawing.Color.LightSteelBlue;
            this._btnMetrics.FlatAppearance.BorderColor = System.Drawing.Color.LightSteelBlue;
            this._btnMetrics.FlatAppearance.BorderSize = 0;
            this._btnMetrics.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSteelBlue;
            this._btnMetrics.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSlateGray;
            this._btnMetrics.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this._btnMetrics.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnMetrics.ForeColor = System.Drawing.Color.Blue;
            this._btnMetrics.Image = global::MetaboliteLevels.Properties.Resources.IconScriptStatistic;
            this._btnMetrics.Location = new System.Drawing.Point(512, 32);
            this._btnMetrics.Margin = new System.Windows.Forms.Padding(16, 32, 16, 16);
            this._btnMetrics.Name = "_btnMetrics";
            this._btnMetrics.Padding = new System.Windows.Forms.Padding(8);
            this._btnMetrics.Size = new System.Drawing.Size(128, 128);
            this._btnMetrics.TabIndex = 2;
            this._btnMetrics.Text = "Metrics";
            this._btnMetrics.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this._btnMetrics.UseVisualStyleBackColor = false;
            this._btnMetrics.Click += new System.EventHandler(this.button1_Click);
            // 
            // _btnClusterers
            // 
            this._btnClusterers.BackColor = System.Drawing.Color.LightSteelBlue;
            this._btnClusterers.FlatAppearance.BorderColor = System.Drawing.Color.LightSteelBlue;
            this._btnClusterers.FlatAppearance.BorderSize = 0;
            this._btnClusterers.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSteelBlue;
            this._btnClusterers.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSlateGray;
            this._btnClusterers.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this._btnClusterers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnClusterers.ForeColor = System.Drawing.Color.Blue;
            this._btnClusterers.Image = global::MetaboliteLevels.Properties.Resources.IconScriptCluster;
            this._btnClusterers.Location = new System.Drawing.Point(672, 32);
            this._btnClusterers.Margin = new System.Windows.Forms.Padding(16, 32, 16, 16);
            this._btnClusterers.Name = "_btnClusterers";
            this._btnClusterers.Padding = new System.Windows.Forms.Padding(8);
            this._btnClusterers.Size = new System.Drawing.Size(128, 128);
            this._btnClusterers.TabIndex = 1;
            this._btnClusterers.Text = "Clusters";
            this._btnClusterers.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this._btnClusterers.UseVisualStyleBackColor = false;
            this._btnClusterers.Click += new System.EventHandler(this._chkClus_Click);
            // 
            // _btnShowAll
            // 
            this._btnShowAll.BackColor = System.Drawing.Color.LightSteelBlue;
            this._btnShowAll.FlatAppearance.BorderColor = System.Drawing.Color.LightSteelBlue;
            this._btnShowAll.FlatAppearance.BorderSize = 0;
            this._btnShowAll.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSteelBlue;
            this._btnShowAll.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSlateGray;
            this._btnShowAll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this._btnShowAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnShowAll.ForeColor = System.Drawing.Color.Blue;
            this._btnShowAll.Image = global::MetaboliteLevels.Properties.Resources.IconList;
            this._btnShowAll.Location = new System.Drawing.Point(832, 32);
            this._btnShowAll.Margin = new System.Windows.Forms.Padding(16, 32, 16, 16);
            this._btnShowAll.Name = "_btnShowAll";
            this._btnShowAll.Padding = new System.Windows.Forms.Padding(8);
            this._btnShowAll.Size = new System.Drawing.Size(128, 128);
            this._btnShowAll.TabIndex = 3;
            this._btnShowAll.Text = "Show all";
            this._btnShowAll.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this._btnShowAll.UseVisualStyleBackColor = false;
            this._btnShowAll.Click += new System.EventHandler(this.button2_Click);
            // 
            // ctlTitleBar1
            // 
            this.ctlTitleBar1.AutoSize = true;
            this.ctlTitleBar1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ctlTitleBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlTitleBar1.HelpText = null;
            this.ctlTitleBar1.Location = new System.Drawing.Point(0, 0);
            this.ctlTitleBar1.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.ctlTitleBar1.MinimumSize = new System.Drawing.Size(576, 0);
            this.ctlTitleBar1.Name = "ctlTitleBar1";
            this.ctlTitleBar1.Size = new System.Drawing.Size(831, 87);
            this.ctlTitleBar1.SubText = "Select an algorithm library";
            this.ctlTitleBar1.TabIndex = 3;
            this.ctlTitleBar1.Text = "New algorithm";
            this.ctlTitleBar1.WarningText = null;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel2.Controls.Add(this._btnCancel);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 268);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(831, 56);
            this.flowLayoutPanel2.TabIndex = 5;
            // 
            // _btnCancel
            // 
            this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnCancel.Image = global::MetaboliteLevels.Properties.Resources.MnuCancel;
            this._btnCancel.Location = new System.Drawing.Point(695, 8);
            this._btnCancel.Margin = new System.Windows.Forms.Padding(8);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(128, 40);
            this._btnCancel.TabIndex = 0;
            this._btnCancel.Text = "Cancel";
            this._btnCancel.UseDefaultSize = true;
            this._btnCancel.UseVisualStyleBackColor = true;
            // 
            // FrmNewAlgorithm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this._btnCancel;
            this.ClientSize = new System.Drawing.Size(831, 324);
            this.ControlBox = false;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.ctlTitleBar1);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmNewAlgorithm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _btnTrend;
        private System.Windows.Forms.Button _btnStatistics;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button _btnCorrections;
        private System.Windows.Forms.Button _btnClusterers;
        private System.Windows.Forms.ToolTip toolTip1;
        private Controls.CtlTitleBar ctlTitleBar1;
        private System.Windows.Forms.Button _btnMetrics;
        private System.Windows.Forms.Button _btnShowAll;
        private Controls.CtlButton _btnCancel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
    }
}