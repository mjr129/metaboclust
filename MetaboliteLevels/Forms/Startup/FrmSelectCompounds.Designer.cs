namespace MetaboliteLevels.Forms.Startup
{
    partial class FrmSelectCompounds
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
            if (disposing && (components != null))
            {
                components.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSelectCompounds));
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._btnCancel = new System.Windows.Forms.Button();
            this._btnOk = new System.Windows.Forms.Button();
            this._txtCompounds = new System.Windows.Forms.TextBox();
            this._btnCompounds = new MetaboliteLevels.Controls.CtlButton();
            this._btnPathways = new MetaboliteLevels.Controls.CtlButton();
            this._txtPathwayTools = new System.Windows.Forms.TextBox();
            this._btnPathwayTools = new MetaboliteLevels.Controls.CtlButton();
            this._txtPathways = new System.Windows.Forms.TextBox();
            this._radCsvFile = new System.Windows.Forms.RadioButton();
            this._radPathwayTools = new System.Windows.Forms.RadioButton();
            this._lblCompounds = new System.Windows.Forms.Label();
            this._lblPathways = new System.Windows.Forms.Label();
            this._lblPathwayTools = new System.Windows.Forms.Label();
            this.ctlTitleBar1 = new MetaboliteLevels.Controls.CtlTitleBar();
            this.tableLayoutPanel4.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.Controls.Add(this.flowLayoutPanel1, 0, 5);
            this.tableLayoutPanel4.Controls.Add(this._txtCompounds, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this._btnCompounds, 2, 1);
            this.tableLayoutPanel4.Controls.Add(this._btnPathways, 2, 2);
            this.tableLayoutPanel4.Controls.Add(this._txtPathwayTools, 1, 4);
            this.tableLayoutPanel4.Controls.Add(this._btnPathwayTools, 2, 4);
            this.tableLayoutPanel4.Controls.Add(this._txtPathways, 1, 2);
            this.tableLayoutPanel4.Controls.Add(this._radCsvFile, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this._radPathwayTools, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this._lblCompounds, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this._lblPathways, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this._lblPathwayTools, 0, 4);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 87);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.Padding = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanel4.RowCount = 6;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(899, 345);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel4.SetColumnSpan(this.flowLayoutPanel1, 3);
            this.flowLayoutPanel1.Controls.Add(this._btnCancel);
            this.flowLayoutPanel1.Controls.Add(this._btnOk);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(618, 282);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(268, 50);
            this.flowLayoutPanel1.TabIndex = 14;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // _btnCancel
            // 
            this._btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("_btnCancel.Image")));
            this._btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._btnCancel.Location = new System.Drawing.Point(137, 5);
            this._btnCancel.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this._btnCancel.Size = new System.Drawing.Size(128, 40);
            this._btnCancel.TabIndex = 19;
            this._btnCancel.Text = "  Cancel";
            this._btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._btnCancel.UseVisualStyleBackColor = true;
            // 
            // _btnOk
            // 
            this._btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._btnOk.Enabled = false;
            this._btnOk.Image = ((System.Drawing.Image)(resources.GetObject("_btnOk.Image")));
            this._btnOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._btnOk.Location = new System.Drawing.Point(3, 5);
            this._btnOk.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this._btnOk.Name = "_btnOk";
            this._btnOk.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this._btnOk.Size = new System.Drawing.Size(128, 40);
            this._btnOk.TabIndex = 18;
            this._btnOk.Text = "  OK";
            this._btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._btnOk.UseVisualStyleBackColor = true;
            // 
            // _txtCompounds
            // 
            this._txtCompounds.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtCompounds.Enabled = false;
            this._txtCompounds.ForeColor = System.Drawing.Color.Blue;
            this._txtCompounds.Location = new System.Drawing.Point(197, 59);
            this._txtCompounds.Margin = new System.Windows.Forms.Padding(8);
            this._txtCompounds.Name = "_txtCompounds";
            this._txtCompounds.Size = new System.Drawing.Size(640, 29);
            this._txtCompounds.TabIndex = 1;
            this._txtCompounds.Visible = false;
            this._txtCompounds.TextChanged += new System.EventHandler(this.anything_Changed);
            // 
            // _btnCompounds
            // 
            this._btnCompounds.Enabled = false;
            this._btnCompounds.Image = ((System.Drawing.Image)(resources.GetObject("_btnCompounds.Image")));
            this._btnCompounds.Location = new System.Drawing.Point(853, 59);
            this._btnCompounds.Margin = new System.Windows.Forms.Padding(8);
            this._btnCompounds.Name = "_btnCompounds";
            this._btnCompounds.Size = new System.Drawing.Size(28, 29);
            this._btnCompounds.TabIndex = 2;
            this._btnCompounds.UseVisualStyleBackColor = true;
            this._btnCompounds.Visible = false;
            this._btnCompounds.Click += new System.EventHandler(this._btnCompounds_Click_1);
            // 
            // _btnPathways
            // 
            this._btnPathways.Enabled = false;
            this._btnPathways.Image = ((System.Drawing.Image)(resources.GetObject("_btnPathways.Image")));
            this._btnPathways.Location = new System.Drawing.Point(853, 104);
            this._btnPathways.Margin = new System.Windows.Forms.Padding(8);
            this._btnPathways.Name = "_btnPathways";
            this._btnPathways.Size = new System.Drawing.Size(28, 29);
            this._btnPathways.TabIndex = 2;
            this._btnPathways.UseVisualStyleBackColor = true;
            this._btnPathways.Visible = false;
            this._btnPathways.Click += new System.EventHandler(this._btnPathways_Click_1);
            // 
            // _txtPathwayTools
            // 
            this._txtPathwayTools.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtPathwayTools.Enabled = false;
            this._txtPathwayTools.ForeColor = System.Drawing.Color.Blue;
            this._txtPathwayTools.Location = new System.Drawing.Point(197, 190);
            this._txtPathwayTools.Margin = new System.Windows.Forms.Padding(8);
            this._txtPathwayTools.Name = "_txtPathwayTools";
            this._txtPathwayTools.Size = new System.Drawing.Size(640, 29);
            this._txtPathwayTools.TabIndex = 1;
            this._txtPathwayTools.Visible = false;
            this._txtPathwayTools.TextChanged += new System.EventHandler(this.anything_Changed);
            // 
            // _btnPathwayTools
            // 
            this._btnPathwayTools.Enabled = false;
            this._btnPathwayTools.Image = ((System.Drawing.Image)(resources.GetObject("_btnPathwayTools.Image")));
            this._btnPathwayTools.Location = new System.Drawing.Point(853, 190);
            this._btnPathwayTools.Margin = new System.Windows.Forms.Padding(8);
            this._btnPathwayTools.Name = "_btnPathwayTools";
            this._btnPathwayTools.Size = new System.Drawing.Size(28, 29);
            this._btnPathwayTools.TabIndex = 2;
            this._btnPathwayTools.UseVisualStyleBackColor = true;
            this._btnPathwayTools.Visible = false;
            this._btnPathwayTools.Click += new System.EventHandler(this._btnPathwayTools_Click);
            // 
            // _txtPathways
            // 
            this._txtPathways.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtPathways.Enabled = false;
            this._txtPathways.ForeColor = System.Drawing.Color.Blue;
            this._txtPathways.Location = new System.Drawing.Point(197, 104);
            this._txtPathways.Margin = new System.Windows.Forms.Padding(8);
            this._txtPathways.Name = "_txtPathways";
            this._txtPathways.Size = new System.Drawing.Size(640, 29);
            this._txtPathways.TabIndex = 1;
            this._txtPathways.Visible = false;
            this._txtPathways.TextChanged += new System.EventHandler(this.anything_Changed);
            // 
            // _radCsvFile
            // 
            this._radCsvFile.AutoSize = true;
            this.tableLayoutPanel4.SetColumnSpan(this._radCsvFile, 3);
            this._radCsvFile.Location = new System.Drawing.Point(18, 18);
            this._radCsvFile.Margin = new System.Windows.Forms.Padding(8);
            this._radCsvFile.Name = "_radCsvFile";
            this._radCsvFile.Size = new System.Drawing.Size(85, 25);
            this._radCsvFile.TabIndex = 3;
            this._radCsvFile.TabStop = true;
            this._radCsvFile.Text = "CSV File";
            this._radCsvFile.UseVisualStyleBackColor = true;
            this._radCsvFile.CheckedChanged += new System.EventHandler(this.anything_Changed);
            // 
            // _radPathwayTools
            // 
            this._radPathwayTools.AutoSize = true;
            this.tableLayoutPanel4.SetColumnSpan(this._radPathwayTools, 3);
            this._radPathwayTools.Location = new System.Drawing.Point(18, 149);
            this._radPathwayTools.Margin = new System.Windows.Forms.Padding(8);
            this._radPathwayTools.Name = "_radPathwayTools";
            this._radPathwayTools.Size = new System.Drawing.Size(196, 25);
            this._radPathwayTools.TabIndex = 3;
            this._radPathwayTools.TabStop = true;
            this._radPathwayTools.Text = "Pathway Tools Database";
            this._radPathwayTools.UseVisualStyleBackColor = true;
            this._radPathwayTools.CheckedChanged += new System.EventHandler(this.anything_Changed);
            // 
            // _lblCompounds
            // 
            this._lblCompounds.AutoSize = true;
            this._lblCompounds.Location = new System.Drawing.Point(34, 59);
            this._lblCompounds.Margin = new System.Windows.Forms.Padding(24, 8, 8, 8);
            this._lblCompounds.Name = "_lblCompounds";
            this._lblCompounds.Size = new System.Drawing.Size(95, 21);
            this._lblCompounds.TabIndex = 4;
            this._lblCompounds.Text = "Compounds";
            this._lblCompounds.Visible = false;
            // 
            // _lblPathways
            // 
            this._lblPathways.AutoSize = true;
            this._lblPathways.Location = new System.Drawing.Point(34, 104);
            this._lblPathways.Margin = new System.Windows.Forms.Padding(24, 8, 8, 8);
            this._lblPathways.Name = "_lblPathways";
            this._lblPathways.Size = new System.Drawing.Size(147, 21);
            this._lblPathways.TabIndex = 4;
            this._lblPathways.Text = "Pathways (optional)";
            this._lblPathways.Visible = false;
            // 
            // _lblPathwayTools
            // 
            this._lblPathwayTools.AutoSize = true;
            this._lblPathwayTools.Location = new System.Drawing.Point(34, 190);
            this._lblPathwayTools.Margin = new System.Windows.Forms.Padding(24, 8, 8, 8);
            this._lblPathwayTools.Name = "_lblPathwayTools";
            this._lblPathwayTools.Size = new System.Drawing.Size(95, 21);
            this._lblPathwayTools.TabIndex = 4;
            this._lblPathwayTools.Text = "Compounds";
            this._lblPathwayTools.Visible = false;
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
            this.ctlTitleBar1.Size = new System.Drawing.Size(899, 87);
            this.ctlTitleBar1.SubText = "Select a compound library manually";
            this.ctlTitleBar1.TabIndex = 2;
            this.ctlTitleBar1.Text = "Select Compound Library";
            this.ctlTitleBar1.WarningText = null;
            // 
            // FrmSelectCompounds
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(899, 432);
            this.Controls.Add(this.tableLayoutPanel4);
            this.Controls.Add(this.ctlTitleBar1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmSelectCompounds";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Compounds";
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TextBox _txtCompounds;
        private Controls.CtlButton _btnCompounds;
        private Controls.CtlButton _btnPathways;
        private System.Windows.Forms.TextBox _txtPathwayTools;
        private Controls.CtlButton _btnPathwayTools;
        private System.Windows.Forms.TextBox _txtPathways;
        private System.Windows.Forms.RadioButton _radCsvFile;
        private System.Windows.Forms.RadioButton _radPathwayTools;
        private System.Windows.Forms.Label _lblCompounds;
        private System.Windows.Forms.Label _lblPathways;
        private System.Windows.Forms.Label _lblPathwayTools;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button _btnCancel;
        private System.Windows.Forms.Button _btnOk;
        private Controls.CtlTitleBar ctlTitleBar1;
    }
}