using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Gui.Controls;

namespace MetaboliteLevels.Gui.Forms.Activities
{
    partial class FrmActOldData
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
            this._lblSmallChange = new System.Windows.Forms.Label();
            this._btnBigChange = new System.Windows.Forms.LinkLabel();
            this.button1 = new CtlButton();
            this.button2 = new CtlButton();
            this._btnDetails = new System.Windows.Forms.LinkLabel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this._chkNotAgain = new System.Windows.Forms.CheckBox();
            this.ctlTitleBar1 = new CtlTitleBar();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _lblSmallChange
            // 
            this._lblSmallChange.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this._lblSmallChange, 2);
            this._lblSmallChange.Location = new System.Drawing.Point(8, 72);
            this._lblSmallChange.Margin = new System.Windows.Forms.Padding(8);
            this._lblSmallChange.Name = "_lblSmallChange";
            this._lblSmallChange.Size = new System.Drawing.Size(526, 21);
            this._lblSmallChange.TabIndex = 21;
            this._lblSmallChange.Text = "Please check your data - if necessary the session may need to be recreated.";
            // 
            // _btnBigChange
            // 
            this._btnBigChange.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this._btnBigChange, 2);
            this._btnBigChange.LinkArea = new System.Windows.Forms.LinkArea(10, 23);
            this._btnBigChange.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this._btnBigChange.LinkColor = System.Drawing.Color.Blue;
            this._btnBigChange.Location = new System.Drawing.Point(8, 8);
            this._btnBigChange.Margin = new System.Windows.Forms.Padding(8);
            this._btnBigChange.Name = "_btnBigChange";
            this._btnBigChange.Size = new System.Drawing.Size(790, 48);
            this._btnBigChange.TabIndex = 23;
            this._btnBigChange.TabStop = true;
            this._btnBigChange.Text = "There are known incompatibilities between the version used to create the file and" +
    " the current version of the application. Is is recommended that the session be r" +
    "ecreated.";
            this._btnBigChange.UseCompatibleTextRendering = true;
            this._btnBigChange.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._btnIncompat_LinkClicked);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Image = global::MetaboliteLevels.Properties.Resources.MnuAccept;
            this.button1.Location = new System.Drawing.Point(8, 8);
            this.button1.Margin = new System.Windows.Forms.Padding(8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(144, 42);
            this.button1.TabIndex = 25;
            this.button1.Text = "Proceed";
            this.button1.UseDefaultSize = true;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Image = global::MetaboliteLevels.Properties.Resources.MnuCancel;
            this.button2.Location = new System.Drawing.Point(168, 8);
            this.button2.Margin = new System.Windows.Forms.Padding(8);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(144, 42);
            this.button2.TabIndex = 25;
            this.button2.Text = "Cancel";
            this.button2.UseDefaultSize = true;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // _btnDetails
            // 
            this._btnDetails.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this._btnDetails, 2);
            this._btnDetails.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this._btnDetails.Location = new System.Drawing.Point(8, 109);
            this._btnDetails.Margin = new System.Windows.Forms.Padding(8);
            this._btnDetails.Name = "_btnDetails";
            this._btnDetails.Size = new System.Drawing.Size(151, 21);
            this._btnDetails.TabIndex = 28;
            this._btnDetails.TabStop = true;
            this._btnDetails.Text = "Click here for details";
            this._btnDetails.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._btnDetails_LinkClicked);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this._btnDetails, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this._lblSmallChange, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel2, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this._btnBigChange, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this._chkNotAgain, 0, 4);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 87);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(866, 245);
            this.tableLayoutPanel2.TabIndex = 27;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel2.Controls.Add(this.button1);
            this.flowLayoutPanel2.Controls.Add(this.button2);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(543, 184);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(320, 58);
            this.flowLayoutPanel2.TabIndex = 0;
            this.flowLayoutPanel2.WrapContents = false;
            // 
            // _chkNotAgain
            // 
            this._chkNotAgain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._chkNotAgain.AutoSize = true;
            this._chkNotAgain.Location = new System.Drawing.Point(8, 204);
            this._chkNotAgain.Margin = new System.Windows.Forms.Padding(8);
            this._chkNotAgain.Name = "_chkNotAgain";
            this._chkNotAgain.Padding = new System.Windows.Forms.Padding(4);
            this._chkNotAgain.Size = new System.Drawing.Size(261, 33);
            this._chkNotAgain.TabIndex = 27;
            this._chkNotAgain.Text = "Do not show this message again";
            this._chkNotAgain.UseVisualStyleBackColor = true;
            this._chkNotAgain.CheckedChanged += new System.EventHandler(this._chkNotAgain_CheckedChanged);
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
            this.ctlTitleBar1.Size = new System.Drawing.Size(866, 87);
            this.ctlTitleBar1.SubText = "This file was created using an {older} version of {product}.";
            this.ctlTitleBar1.TabIndex = 28;
            this.ctlTitleBar1.Text = "Version Notice";
            this.ctlTitleBar1.WarningText = null;
            // 
            // FrmOldData
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(866, 332);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.ctlTitleBar1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmOldData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmOldData";
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _lblSmallChange;
        private System.Windows.Forms.LinkLabel _btnBigChange;
        private CtlButton button1;
        private CtlButton button2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.LinkLabel _btnDetails;
        private System.Windows.Forms.CheckBox _chkNotAgain;
        private Controls.CtlTitleBar ctlTitleBar1;

    }
}