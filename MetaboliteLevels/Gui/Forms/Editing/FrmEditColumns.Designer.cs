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
            this.components = new System.ComponentModel.Container();
            this.treeView1 = new MGui.Controls.CtlTreeView();
            this._btnOk = new MetaboliteLevels.Gui.Controls.CtlButton();
            this._btnCancel = new MetaboliteLevels.Gui.Controls.CtlButton();
            this._btnDefaults = new MetaboliteLevels.Gui.Controls.CtlButton();
            this.ctlTitleBar1 = new MetaboliteLevels.Gui.Controls.CtlTitleBar();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._chkFolders = new System.Windows.Forms.ToolStripButton();
            this._chkNormal = new System.Windows.Forms.ToolStripButton();
            this._chkDefault = new System.Windows.Forms.ToolStripButton();
            this._chkStatistics = new System.Windows.Forms.ToolStripButton();
            this._chkMetaFields = new System.Windows.Forms.ToolStripButton();
            this._chkAdvanced = new System.Windows.Forms.ToolStripButton();
            this._chkProperties = new System.Windows.Forms.ToolStripButton();
            this.ctlContextHelp1 = new MetaboliteLevels.Gui.Controls.CtlContextHelp(this.components);
            this.flowLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.CheckBoxes = true;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 91);
            this.treeView1.Margin = new System.Windows.Forms.Padding(0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(901, 549);
            this.treeView1.TabIndex = 1;
            this.treeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCheck);
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // _btnOk
            // 
            this._btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._btnOk.Image = global::MetaboliteLevels.Properties.Resources.MnuAccept;
            this._btnOk.Location = new System.Drawing.Point(477, 8);
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
            this._btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnCancel.Image = global::MetaboliteLevels.Properties.Resources.MnuCancel;
            this._btnCancel.Location = new System.Drawing.Point(621, 8);
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
            this._btnDefaults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnDefaults.Image = global::MetaboliteLevels.Properties.Resources.MnuUndo;
            this._btnDefaults.Location = new System.Drawing.Point(765, 8);
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
            this.ctlTitleBar1.Text = "Available fields";
            this.ctlTitleBar1.WarningText = null;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this._btnDefaults);
            this.flowLayoutPanel1.Controls.Add(this._btnCancel);
            this.flowLayoutPanel1.Controls.Add(this._btnOk);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 640);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(901, 56);
            this.flowLayoutPanel1.TabIndex = 7;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._chkFolders,
            this._chkNormal,
            this._chkDefault,
            this._chkStatistics,
            this._chkMetaFields,
            this._chkAdvanced,
            this._chkProperties});
            this.toolStrip1.Location = new System.Drawing.Point(0, 66);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(901, 25);
            this.toolStrip1.TabIndex = 8;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _chkFolders
            // 
            this._chkFolders.CheckOnClick = true;
            this._chkFolders.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._chkFolders.Name = "_chkFolders";
            this._chkFolders.Size = new System.Drawing.Size(49, 22);
            this._chkFolders.Text = "Folders";
            this._chkFolders.Click += new System.EventHandler(this._chkFolders_Click);
            // 
            // _chkNormal
            // 
            this._chkNormal.CheckOnClick = true;
            this._chkNormal.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._chkNormal.Name = "_chkNormal";
            this._chkNormal.Size = new System.Drawing.Size(51, 22);
            this._chkNormal.Text = "Normal";
            this._chkNormal.Click += new System.EventHandler(this._chkFolders_Click);
            // 
            // _chkDefault
            // 
            this._chkDefault.CheckOnClick = true;
            this._chkDefault.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._chkDefault.Name = "_chkDefault";
            this._chkDefault.Size = new System.Drawing.Size(49, 22);
            this._chkDefault.Text = "Default";
            this._chkDefault.Click += new System.EventHandler(this._chkFolders_Click);
            // 
            // _chkStatistics
            // 
            this._chkStatistics.CheckOnClick = true;
            this._chkStatistics.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._chkStatistics.Name = "_chkStatistics";
            this._chkStatistics.Size = new System.Drawing.Size(57, 22);
            this._chkStatistics.Text = "Statistics";
            this._chkStatistics.Click += new System.EventHandler(this._chkFolders_Click);
            // 
            // _chkMetaFields
            // 
            this._chkMetaFields.CheckOnClick = true;
            this._chkMetaFields.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._chkMetaFields.Name = "_chkMetaFields";
            this._chkMetaFields.Size = new System.Drawing.Size(71, 22);
            this._chkMetaFields.Text = "Meta-fields";
            this._chkMetaFields.Click += new System.EventHandler(this._chkFolders_Click);
            // 
            // _chkAdvanced
            // 
            this._chkAdvanced.CheckOnClick = true;
            this._chkAdvanced.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._chkAdvanced.Name = "_chkAdvanced";
            this._chkAdvanced.Size = new System.Drawing.Size(64, 22);
            this._chkAdvanced.Text = "Advanced";
            this._chkAdvanced.Click += new System.EventHandler(this._chkFolders_Click);
            // 
            // _chkProperties
            // 
            this._chkProperties.CheckOnClick = true;
            this._chkProperties.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._chkProperties.Name = "_chkProperties";
            this._chkProperties.Size = new System.Drawing.Size(59, 22);
            this._chkProperties.Text = "Raw data";
            this._chkProperties.Click += new System.EventHandler(this._chkFolders_Click);
            // 
            // FrmEditColumns
            // 
            this.AcceptButton = this._btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._btnCancel;
            this.ClientSize = new System.Drawing.Size(901, 696);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.ctlTitleBar1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmEditColumns";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Properties";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MGui.Controls.CtlTreeView treeView1;
        private Controls.CtlButton _btnOk;
        private Controls.CtlButton _btnDefaults;
        private Controls.CtlButton _btnCancel;
        private Controls.CtlTitleBar ctlTitleBar1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton _chkProperties;
        private System.Windows.Forms.ToolStripButton _chkFolders;
        private System.Windows.Forms.ToolStripButton _chkDefault;
        private System.Windows.Forms.ToolStripButton _chkAdvanced;
        private System.Windows.Forms.ToolStripButton _chkStatistics;
        private System.Windows.Forms.ToolStripButton _chkMetaFields;
        private System.Windows.Forms.ToolStripButton _chkNormal;
        private CtlContextHelp ctlContextHelp1;
    }
}