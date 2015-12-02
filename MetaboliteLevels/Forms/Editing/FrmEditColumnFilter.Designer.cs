namespace MetaboliteLevels.Forms.Editing
{
    partial class FrmFilter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFilter));
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.button2 = new MetaboliteLevels.Controls.CtlButton();
            this.button1 = new MetaboliteLevels.Controls.CtlButton();
            this._lstNumComp = new System.Windows.Forms.ComboBox();
            this._txtNumComp = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.ctlTitleBar1 = new MetaboliteLevels.Controls.CtlTitleBar();
            this.flowLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel4.AutoSize = true;
            this.flowLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel4.Controls.Add(this.button2);
            this.flowLayoutPanel4.Controls.Add(this.button1);
            this.flowLayoutPanel4.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel4.Location = new System.Drawing.Point(438, 101);
            this.flowLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(288, 56);
            this.flowLayoutPanel4.TabIndex = 4;
            this.flowLayoutPanel4.WrapContents = false;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Image = global::MetaboliteLevels.Properties.Resources.MnuCancel;
            this.button2.Location = new System.Drawing.Point(152, 8);
            this.button2.Margin = new System.Windows.Forms.Padding(8);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(128, 40);
            this.button2.TabIndex = 3;
            this.button2.Text = "Cancel";
            this.button2.UseDefaultSize = true;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Enabled = false;
            this.button1.Image = global::MetaboliteLevels.Properties.Resources.MnuAccept;
            this.button1.Location = new System.Drawing.Point(8, 8);
            this.button1.Margin = new System.Windows.Forms.Padding(8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 40);
            this.button1.TabIndex = 2;
            this.button1.Text = "OK";
            this.button1.UseDefaultSize = true;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // _lstNumComp
            // 
            this._lstNumComp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstNumComp.FormattingEnabled = true;
            this._lstNumComp.Location = new System.Drawing.Point(16, 16);
            this._lstNumComp.Margin = new System.Windows.Forms.Padding(16);
            this._lstNumComp.Name = "_lstNumComp";
            this._lstNumComp.Size = new System.Drawing.Size(268, 29);
            this._lstNumComp.TabIndex = 4;
            this._lstNumComp.SelectedIndexChanged += new System.EventHandler(this.something_Changed);
            // 
            // _txtNumComp
            // 
            this._txtNumComp.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtNumComp.Location = new System.Drawing.Point(316, 16);
            this._txtNumComp.Margin = new System.Windows.Forms.Padding(16);
            this._txtNumComp.Name = "_txtNumComp";
            this._txtNumComp.Size = new System.Drawing.Size(394, 29);
            this._txtNumComp.TabIndex = 0;
            this._txtNumComp.TextChanged += new System.EventHandler(this.something_Changed);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this._txtNumComp, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel4, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this._lstNumComp, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 87);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(726, 157);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // ctlTitleBar1
            // 
            this.ctlTitleBar1.AutoSize = true;
            this.ctlTitleBar1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ctlTitleBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlTitleBar1.HelpText = resources.GetString("ctlTitleBar1.HelpText");
            this.ctlTitleBar1.Location = new System.Drawing.Point(0, 0);
            this.ctlTitleBar1.Margin = new System.Windows.Forms.Padding(8, 11, 8, 11);
            this.ctlTitleBar1.MinimumSize = new System.Drawing.Size(256, 0);
            this.ctlTitleBar1.Name = "ctlTitleBar1";
            this.ctlTitleBar1.Size = new System.Drawing.Size(726, 87);
            this.ctlTitleBar1.SubText = "Specify the column filter";
            this.ctlTitleBar1.TabIndex = 10;
            this.ctlTitleBar1.Text = "Filter";
            this.ctlTitleBar1.WarningText = null;
            // 
            // FrmFilter
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(726, 244);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.ctlTitleBar1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmFilter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Filter";
            this.flowLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.CtlTitleBar ctlTitleBar1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private MetaboliteLevels.Controls.CtlButton button2;
        private MetaboliteLevels.Controls.CtlButton button1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox _txtNumComp;
        private System.Windows.Forms.ComboBox _lstNumComp;
    }
}