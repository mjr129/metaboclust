namespace MetaboliteLevels.Forms.Editing
{
    partial class FrmEditUpdate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEditUpdate));
            this.ctlTitleBar1 = new MetaboliteLevels.Controls.CtlTitleBar();
            this._chkStat = new System.Windows.Forms.CheckBox();
            this._chkTrend = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._chkCor = new System.Windows.Forms.CheckBox();
            this._chkClus = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.ctlButton1 = new MetaboliteLevels.Controls.CtlButton();
            this.button1 = new MetaboliteLevels.Controls.CtlButton();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
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
            this.ctlTitleBar1.Size = new System.Drawing.Size(817, 87);
            this.ctlTitleBar1.SubText = "and here";
            this.ctlTitleBar1.TabIndex = 0;
            this.ctlTitleBar1.Text = "Text goes here";
            this.ctlTitleBar1.WarningText = null;
            // 
            // _chkStat
            // 
            this._chkStat.AutoSize = true;
            this._chkStat.ForeColor = System.Drawing.Color.Blue;
            this._chkStat.Location = new System.Drawing.Point(8, 90);
            this._chkStat.Margin = new System.Windows.Forms.Padding(8);
            this._chkStat.Name = "_chkStat";
            this._chkStat.Size = new System.Drawing.Size(87, 25);
            this._chkStat.TabIndex = 0;
            this._chkStat.Text = "_chkStat";
            this._chkStat.UseVisualStyleBackColor = true;
            this._chkStat.CheckedChanged += new System.EventHandler(this._chkClus_CheckedChanged);
            // 
            // _chkTrend
            // 
            this._chkTrend.AutoSize = true;
            this._chkTrend.ForeColor = System.Drawing.Color.Blue;
            this._chkTrend.Location = new System.Drawing.Point(8, 49);
            this._chkTrend.Margin = new System.Windows.Forms.Padding(8);
            this._chkTrend.Name = "_chkTrend";
            this._chkTrend.Size = new System.Drawing.Size(100, 25);
            this._chkTrend.TabIndex = 0;
            this._chkTrend.Text = "_chkTrend";
            this._chkTrend.UseVisualStyleBackColor = true;
            this._chkTrend.CheckedChanged += new System.EventHandler(this._chkClus_CheckedChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this._chkCor);
            this.flowLayoutPanel1.Controls.Add(this._chkTrend);
            this.flowLayoutPanel1.Controls.Add(this._chkStat);
            this.flowLayoutPanel1.Controls.Add(this._chkClus);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 87);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(817, 199);
            this.flowLayoutPanel1.TabIndex = 1;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // _chkCor
            // 
            this._chkCor.AutoSize = true;
            this._chkCor.ForeColor = System.Drawing.Color.Blue;
            this._chkCor.Location = new System.Drawing.Point(8, 8);
            this._chkCor.Margin = new System.Windows.Forms.Padding(8);
            this._chkCor.Name = "_chkCor";
            this._chkCor.Size = new System.Drawing.Size(85, 25);
            this._chkCor.TabIndex = 0;
            this._chkCor.Text = "_chkCor";
            this._chkCor.UseVisualStyleBackColor = true;
            this._chkCor.CheckedChanged += new System.EventHandler(this._chkClus_CheckedChanged);
            // 
            // _chkClus
            // 
            this._chkClus.AutoSize = true;
            this._chkClus.ForeColor = System.Drawing.Color.Blue;
            this._chkClus.Location = new System.Drawing.Point(8, 131);
            this._chkClus.Margin = new System.Windows.Forms.Padding(8);
            this._chkClus.Name = "_chkClus";
            this._chkClus.Size = new System.Drawing.Size(90, 25);
            this._chkClus.TabIndex = 1;
            this._chkClus.Text = "_chkClus";
            this._chkClus.UseVisualStyleBackColor = true;
            this._chkClus.CheckedChanged += new System.EventHandler(this._chkClus_CheckedChanged);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel2.Controls.Add(this.ctlButton1);
            this.flowLayoutPanel2.Controls.Add(this.button1);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 286);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(817, 56);
            this.flowLayoutPanel2.TabIndex = 2;
            // 
            // ctlButton1
            // 
            this.ctlButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ctlButton1.Image = ((System.Drawing.Image)(resources.GetObject("ctlButton1.Image")));
            this.ctlButton1.Location = new System.Drawing.Point(681, 8);
            this.ctlButton1.Margin = new System.Windows.Forms.Padding(8);
            this.ctlButton1.Name = "ctlButton1";
            this.ctlButton1.Size = new System.Drawing.Size(128, 40);
            this.ctlButton1.TabIndex = 0;
            this.ctlButton1.Text = "Cancel";
            this.ctlButton1.UseDefaultSize = true;
            this.ctlButton1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(537, 8);
            this.button1.Margin = new System.Windows.Forms.Padding(8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 40);
            this.button1.TabIndex = 0;
            this.button1.Text = "OK";
            this.button1.UseDefaultSize = true;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // FrmEditUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(817, 342);
            this.ControlBox = false;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.ctlTitleBar1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmEditUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.CtlTitleBar ctlTitleBar1;
        private System.Windows.Forms.CheckBox _chkStat;
        private System.Windows.Forms.CheckBox _chkTrend;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.CheckBox _chkCor;
        private System.Windows.Forms.CheckBox _chkClus;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private MetaboliteLevels.Controls.CtlButton button1;
        private Controls.CtlButton ctlButton1;
    }
}