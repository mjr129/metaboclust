using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Gui.Controls;

namespace MetaboliteLevels.Gui.Forms.Text
{
    partial class FrmInputScript
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._btnOk = new Controls.CtlButton();
            this.btnCancel = new Controls.CtlButton();
            this.textBox1 = new MGui.Controls.CtlTextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this._mnuStrip = new System.Windows.Forms.MenuStrip();
            this._mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._titleBar = new CtlTitleBar();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this._mnuStrip.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this._btnOk);
            this.flowLayoutPanel1.Controls.Add(this.btnCancel);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(659, 478);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(288, 56);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // _btnOk
            // 
            this._btnOk.Image = global::MetaboliteLevels.Properties.Resources.MnuAccept;
            this._btnOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._btnOk.Location = new System.Drawing.Point(8, 8);
            this._btnOk.Margin = new System.Windows.Forms.Padding(8);
            this._btnOk.Name = "_btnOk";
            this._btnOk.Padding = new System.Windows.Forms.Padding(6);
            this._btnOk.Size = new System.Drawing.Size(128, 40);
            this._btnOk.TabIndex = 0;
            this._btnOk.Text = "  OK";
            this._btnOk.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._btnOk.UseVisualStyleBackColor = true;
            this._btnOk.Click += new System.EventHandler(this._btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::MetaboliteLevels.Properties.Resources.MnuCancel;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(152, 8);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(6);
            this.btnCancel.Size = new System.Drawing.Size(128, 40);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "  Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(8, 8);
            this.textBox1.Margin = new System.Windows.Forms.Padding(8);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(931, 462);
            this.textBox1.TabIndex = 2;                                                     
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.textBox1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 111);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(947, 534);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // _mnuStrip
            // 
            this._mnuStrip.BackColor = System.Drawing.Color.LightSteelBlue;
            this._mnuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._mnuFile});
            this._mnuStrip.Location = new System.Drawing.Point(0, 87);
            this._mnuStrip.Name = "_mnuStrip";
            this._mnuStrip.Size = new System.Drawing.Size(947, 24);
            this._mnuStrip.TabIndex = 7;
            this._mnuStrip.Text = "menuStrip1";
            // 
            // _mnuFile
            // 
            this._mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem});
            this._mnuFile.ForeColor = System.Drawing.Color.Black;
            this._mnuFile.Name = "_mnuFile";
            this._mnuFile.Size = new System.Drawing.Size(37, 20);
            this._mnuFile.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.openToolStripMenuItem.Text = "&Import text...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.saveToolStripMenuItem.Text = "&Export text...";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // _titleBar
            // 
            this._titleBar.AutoSize = true;
            this._titleBar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._titleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this._titleBar.HelpText = "";
            this._titleBar.Location = new System.Drawing.Point(0, 0);
            this._titleBar.Margin = new System.Windows.Forms.Padding(9, 13, 9, 13);
            this._titleBar.MinimumSize = new System.Drawing.Size(384, 0);
            this._titleBar.Name = "_titleBar";
            this._titleBar.Size = new System.Drawing.Size(947, 87);
            this._titleBar.SubText = "Based on R script";
            this._titleBar.TabIndex = 4;
            this._titleBar.Text = "Clustering";
            this._titleBar.WarningText = null;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 645);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(947, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // FrmRScript
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(947, 667);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this._mnuStrip);
            this.Controls.Add(this._titleBar);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this._mnuStrip;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmRScript";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmRScript";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this._mnuStrip.ResumeLayout(false);
            this._mnuStrip.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Controls.CtlButton _btnOk;
        private Controls.CtlButton btnCancel;
        private MGui.Controls.CtlTextBox textBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Controls.CtlTitleBar _titleBar;
        private System.Windows.Forms.MenuStrip _mnuStrip;
        private System.Windows.Forms.ToolStripMenuItem _mnuFile;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}