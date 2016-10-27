using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Gui.Controls;

namespace MetaboliteLevels.Gui.Forms.Editing
{
    partial class FrmEditColumns
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
            this.treeView1 = new MGui.Controls.CtlTreeView();
            this._chkShowAdvanced = new System.Windows.Forms.CheckBox();
            this._btnOk = new CtlButton();
            this._btnCancel = new CtlButton();
            this._btnDefaults = new CtlButton();
            this.ctlTitleBar1 = new CtlTitleBar();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.CheckBoxes = true;
            this.tableLayoutPanel1.SetColumnSpan(this.treeView1, 4);
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Margin = new System.Windows.Forms.Padding(0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(901, 574);
            this.treeView1.TabIndex = 1;
            this.treeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCheck);
            // 
            // _chkShowAdvanced
            // 
            this._chkShowAdvanced.AutoSize = true;
            this._chkShowAdvanced.Location = new System.Drawing.Point(8, 582);
            this._chkShowAdvanced.Margin = new System.Windows.Forms.Padding(8);
            this._chkShowAdvanced.Name = "_chkShowAdvanced";
            this._chkShowAdvanced.Size = new System.Drawing.Size(213, 25);
            this._chkShowAdvanced.TabIndex = 2;
            this._chkShowAdvanced.Text = "Show advanced properties";
            this._chkShowAdvanced.UseVisualStyleBackColor = true;
            this._chkShowAdvanced.CheckedChanged += new System.EventHandler(this._chkShowAdvanced_CheckedChanged);
            // 
            // _btnOk
            // 
            this._btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._btnOk.Image = global::MetaboliteLevels.Properties.Resources.MnuAccept;
            this._btnOk.Location = new System.Drawing.Point(477, 582);
            this._btnOk.Margin = new System.Windows.Forms.Padding(8);
            this._btnOk.Name = "_btnOk";
            this._btnOk.Size = new System.Drawing.Size(128, 40);
            this._btnOk.TabIndex = 3;
            this._btnOk.Text = "OK";
            this._btnOk.UseDefaultSize = true;
            this._btnOk.UseVisualStyleBackColor = true;
            // 
            // _btnCancel
            // 
            this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnCancel.Image = global::MetaboliteLevels.Properties.Resources.MnuCancel;
            this._btnCancel.Location = new System.Drawing.Point(621, 582);
            this._btnCancel.Margin = new System.Windows.Forms.Padding(8);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(128, 40);
            this._btnCancel.TabIndex = 4;
            this._btnCancel.Text = "Cancel";
            this._btnCancel.UseDefaultSize = true;
            this._btnCancel.UseVisualStyleBackColor = true;
            // 
            // _btnDefaults
            // 
            this._btnDefaults.Image = global::MetaboliteLevels.Properties.Resources.MnuUndo;
            this._btnDefaults.Location = new System.Drawing.Point(765, 582);
            this._btnDefaults.Margin = new System.Windows.Forms.Padding(8);
            this._btnDefaults.Name = "_btnDefaults";
            this._btnDefaults.Size = new System.Drawing.Size(128, 40);
            this._btnDefaults.TabIndex = 4;
            this._btnDefaults.Text = "Defaults";
            this._btnDefaults.UseDefaultSize = true;
            this._btnDefaults.UseVisualStyleBackColor = true;
            this._btnDefaults.Click += new System.EventHandler(this._btnDefaults_Click);
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
            this.ctlTitleBar1.Size = new System.Drawing.Size(901, 66);
            this.ctlTitleBar1.SubText = "";
            this.ctlTitleBar1.TabIndex = 5;
            this.ctlTitleBar1.Text = "Select Properties";
            this.ctlTitleBar1.WarningText = null;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.treeView1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._btnDefaults, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this._btnCancel, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this._btnOk, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this._chkShowAdvanced, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 66);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(901, 630);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // FrmEditColumns
            // 
            this.AcceptButton = this._btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._btnCancel;
            this.ClientSize = new System.Drawing.Size(901, 696);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.ctlTitleBar1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmEditColumns";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Properties";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MGui.Controls.CtlTreeView treeView1;
        private System.Windows.Forms.CheckBox _chkShowAdvanced;
        private Controls.CtlButton _btnOk;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Controls.CtlButton _btnDefaults;
        private Controls.CtlButton _btnCancel;
        private Controls.CtlTitleBar ctlTitleBar1;
    }
}