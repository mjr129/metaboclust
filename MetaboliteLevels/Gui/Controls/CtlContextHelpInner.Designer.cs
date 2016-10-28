﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaboliteLevels.Gui.Controls
{
    partial class CtlContextHelpInner
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ctlTitleBar1 = new MetaboliteLevels.Gui.Controls.CtlTitleBar();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._btnFileFormatDetails = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Info;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.textBox1.Location = new System.Drawing.Point(0, 77);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(261, 304);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // ctlTitleBar1
            // 
            this.ctlTitleBar1.AutoSize = true;
            this.ctlTitleBar1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ctlTitleBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlTitleBar1.HelpIcon = MetaboliteLevels.Gui.Controls.CtlTitleBar.EHelpIcon.HideBar;
            this.ctlTitleBar1.HelpText = null;
            this.ctlTitleBar1.Location = new System.Drawing.Point(0, 0);
            this.ctlTitleBar1.MinimumSize = new System.Drawing.Size(256, 0);
            this.ctlTitleBar1.Name = "ctlTitleBar1";
            this.ctlTitleBar1.Size = new System.Drawing.Size(261, 77);
            this.ctlTitleBar1.SubText = "Context sensitive help";
            this.ctlTitleBar1.TabIndex = 1;
            this.ctlTitleBar1.Text = "Help";
            this.ctlTitleBar1.WarningText = "";
            this.ctlTitleBar1.HelpClicked += new System.ComponentModel.CancelEventHandler(this.ctlTitleBar1_HelpClicked);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._btnFileFormatDetails});
            this.toolStrip1.Location = new System.Drawing.Point(0, 77);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(261, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.Visible = false;
            // 
            // _btnFileFormatDetails
            // 
            this._btnFileFormatDetails.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnFileFormatDetails.Image = global::MetaboliteLevels.Properties.Resources.MnuNext;
            this._btnFileFormatDetails.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnFileFormatDetails.Name = "_btnFileFormatDetails";
            this._btnFileFormatDetails.Size = new System.Drawing.Size(160, 22);
            this._btnFileFormatDetails.Text = "FILE FORMAT DETAILS...";
            this._btnFileFormatDetails.Click += new System.EventHandler(this._btnFileFormatDetails_Click);
            // 
            // CtlContextHelpInner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.ctlTitleBar1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "CtlContextHelpInner";
            this.Size = new System.Drawing.Size(261, 381);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private CtlTitleBar ctlTitleBar1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton _btnFileFormatDetails;
    }
}
