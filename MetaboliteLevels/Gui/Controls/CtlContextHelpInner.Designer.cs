using System;
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
            this.ctlTitleBar1 = new CtlTitleBar();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Info;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.textBox1.Location = new System.Drawing.Point(8, 77);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(253, 215);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // ctlTitleBar1
            // 
            this.ctlTitleBar1.AutoSize = true;
            this.ctlTitleBar1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ctlTitleBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlTitleBar1.HelpIcon = CtlTitleBar.EHelpIcon.HideBar;
            this.ctlTitleBar1.HelpText = null;
            this.ctlTitleBar1.Location = new System.Drawing.Point(8, 0);
            this.ctlTitleBar1.MinimumSize = new System.Drawing.Size(256, 0);
            this.ctlTitleBar1.Name = "ctlTitleBar1";
            this.ctlTitleBar1.Size = new System.Drawing.Size(256, 77);
            this.ctlTitleBar1.SubText = "Context sensitive help";
            this.ctlTitleBar1.TabIndex = 1;
            this.ctlTitleBar1.Text = "Help";
            this.ctlTitleBar1.WarningText = null;
            this.ctlTitleBar1.HelpClicked += new System.ComponentModel.CancelEventHandler(this.ctlTitleBar1_HelpClicked);
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.Black;
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(8, 292);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // CtlContextHelpInner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.ctlTitleBar1);
            this.Controls.Add(this.splitter1);
            this.Name = "CtlContextHelpInner";
            this.Size = new System.Drawing.Size(261, 292);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private CtlTitleBar ctlTitleBar1;
        private System.Windows.Forms.Splitter splitter1;
    }
}
