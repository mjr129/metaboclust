using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaboliteLevels.Gui.Controls
{
    partial class CtlTitleBar
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._lblTitle = new System.Windows.Forms.Label();
            this._lblSubTitle = new System.Windows.Forms.Label();
            this._btnHelp = new MetaboliteLevels.Gui.Controls.CtlButton();
            this._btnWarning = new MetaboliteLevels.Gui.Controls.CtlButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this._lblTitle, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._lblSubTitle, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(8);
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(509, 77);
            this.tableLayoutPanel1.TabIndex = 24;
            this.tableLayoutPanel1.SizeChanged += new System.EventHandler(this.tableLayoutPanel1_SizeChanged);
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // _lblTitle
            // 
            this._lblTitle.AutoSize = true;
            this._lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this._lblTitle.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblTitle.Location = new System.Drawing.Point(11, 8);
            this._lblTitle.Name = "_lblTitle";
            this._lblTitle.Size = new System.Drawing.Size(431, 40);
            this._lblTitle.TabIndex = 1;
            this._lblTitle.Text = "Main titleaaaaaaaaaaaa";
            // 
            // _lblSubTitle
            // 
            this._lblSubTitle.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this._lblSubTitle, 2);
            this._lblSubTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblSubTitle.Location = new System.Drawing.Point(11, 48);
            this._lblSubTitle.Name = "_lblSubTitle";
            this._lblSubTitle.Padding = new System.Windows.Forms.Padding(32, 0, 0, 0);
            this._lblSubTitle.Size = new System.Drawing.Size(430, 21);
            this._lblSubTitle.TabIndex = 1;
            this._lblSubTitle.Text = "Subtitle Subtitle Subtitle Subtitle Subtitle Subtitle Subtitle";
            // 
            // _btnHelp
            // 
            this._btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnHelp.AutoSize = true;
            this._btnHelp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._btnHelp.Cursor = System.Windows.Forms.Cursors.Hand;
            this._btnHelp.FlatAppearance.BorderColor = System.Drawing.Color.CornflowerBlue;
            this._btnHelp.FlatAppearance.BorderSize = 0;
            this._btnHelp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.CornflowerBlue;
            this._btnHelp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this._btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnHelp.Image = global::MetaboliteLevels.Properties.Resources.MnuHelp;
            this._btnHelp.Location = new System.Drawing.Point(31, 3);
            this._btnHelp.Name = "_btnHelp";
            this._btnHelp.Size = new System.Drawing.Size(22, 22);
            this._btnHelp.TabIndex = 2;
            this._btnHelp.UseVisualStyleBackColor = false;
            this._btnHelp.Visible = false;
            this._btnHelp.Click += new System.EventHandler(this._btnHelp_Click);
            // 
            // _btnWarning
            // 
            this._btnWarning.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnWarning.AutoSize = true;
            this._btnWarning.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._btnWarning.Cursor = System.Windows.Forms.Cursors.Hand;
            this._btnWarning.FlatAppearance.BorderColor = System.Drawing.Color.CornflowerBlue;
            this._btnWarning.FlatAppearance.BorderSize = 0;
            this._btnWarning.FlatAppearance.MouseDownBackColor = System.Drawing.Color.CornflowerBlue;
            this._btnWarning.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this._btnWarning.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnWarning.Image = global::MetaboliteLevels.Properties.Resources.IcoWarning;
            this._btnWarning.Location = new System.Drawing.Point(3, 3);
            this._btnWarning.Name = "_btnWarning";
            this._btnWarning.Size = new System.Drawing.Size(22, 22);
            this._btnWarning.TabIndex = 2;
            this._btnWarning.UseVisualStyleBackColor = false;
            this._btnWarning.Visible = false;
            this._btnWarning.Click += new System.EventHandler(this._btnWarning_Click);
            this._btnWarning.MouseEnter += new System.EventHandler(this._btnWarning_MouseEnter);
            this._btnWarning.MouseLeave += new System.EventHandler(this._btnWarning_MouseLeave);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this._btnWarning);
            this.flowLayoutPanel1.Controls.Add(this._btnHelp);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(445, 8);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(56, 28);
            this.flowLayoutPanel1.TabIndex = 25;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // CtlTitleBar
            // 
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "CtlTitleBar";
            this.Size = new System.Drawing.Size(509, 274);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label _lblTitle;
        private System.Windows.Forms.Label _lblSubTitle;
        private Controls.CtlButton _btnHelp;
        private Controls.CtlButton _btnWarning;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}
