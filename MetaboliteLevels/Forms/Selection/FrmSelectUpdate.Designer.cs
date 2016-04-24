namespace MetaboliteLevels.Forms.Editing
{
    partial class FrmSelectUpdate
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
            this.components = new System.ComponentModel.Container();
            this._chkStat = new System.Windows.Forms.CheckBox();
            this._chkTrend = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._chkCor = new System.Windows.Forms.CheckBox();
            this._chkClus = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.ctlButton1 = new MetaboliteLevels.Controls.CtlButton();
            this.button1 = new MetaboliteLevels.Controls.CtlButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ctlTitleBar1 = new MetaboliteLevels.Controls.CtlTitleBar();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _chkStat
            // 
            this._chkStat.Appearance = System.Windows.Forms.Appearance.Button;
            this._chkStat.BackColor = System.Drawing.Color.LightSteelBlue;
            this._chkStat.FlatAppearance.BorderColor = System.Drawing.Color.LightSteelBlue;
            this._chkStat.FlatAppearance.BorderSize = 0;
            this._chkStat.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSteelBlue;
            this._chkStat.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSlateGray;
            this._chkStat.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this._chkStat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._chkStat.ForeColor = System.Drawing.Color.Blue;
            this._chkStat.Image = global::MetaboliteLevels.Properties.Resources.IconScriptStatistic;
            this._chkStat.Location = new System.Drawing.Point(352, 32);
            this._chkStat.Margin = new System.Windows.Forms.Padding(16, 32, 16, 16);
            this._chkStat.Name = "_chkStat";
            this._chkStat.Padding = new System.Windows.Forms.Padding(8);
            this._chkStat.Size = new System.Drawing.Size(128, 128);
            this._chkStat.TabIndex = 0;
            this._chkStat.Text = "Statistics";
            this._chkStat.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this._chkStat.UseVisualStyleBackColor = false;
            this._chkStat.CheckedChanged += new System.EventHandler(this._chkClus_CheckedChanged);
            // 
            // _chkTrend
            // 
            this._chkTrend.Appearance = System.Windows.Forms.Appearance.Button;
            this._chkTrend.BackColor = System.Drawing.Color.LightSteelBlue;
            this._chkTrend.FlatAppearance.BorderColor = System.Drawing.Color.LightSteelBlue;
            this._chkTrend.FlatAppearance.BorderSize = 0;
            this._chkTrend.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSteelBlue;
            this._chkTrend.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSlateGray;
            this._chkTrend.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this._chkTrend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._chkTrend.ForeColor = System.Drawing.Color.Blue;
            this._chkTrend.Image = global::MetaboliteLevels.Properties.Resources.IconScriptTrend;
            this._chkTrend.Location = new System.Drawing.Point(192, 32);
            this._chkTrend.Margin = new System.Windows.Forms.Padding(16, 32, 16, 16);
            this._chkTrend.Name = "_chkTrend";
            this._chkTrend.Padding = new System.Windows.Forms.Padding(8);
            this._chkTrend.Size = new System.Drawing.Size(128, 128);
            this._chkTrend.TabIndex = 0;
            this._chkTrend.Text = "Trends";
            this._chkTrend.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this._chkTrend.UseVisualStyleBackColor = false;
            this._chkTrend.CheckedChanged += new System.EventHandler(this._chkClus_CheckedChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this._chkCor);
            this.flowLayoutPanel1.Controls.Add(this._chkTrend);
            this.flowLayoutPanel1.Controls.Add(this._chkStat);
            this.flowLayoutPanel1.Controls.Add(this._chkClus);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 87);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(672, 185);
            this.flowLayoutPanel1.TabIndex = 1;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // _chkCor
            // 
            this._chkCor.Appearance = System.Windows.Forms.Appearance.Button;
            this._chkCor.BackColor = System.Drawing.Color.LightSteelBlue;
            this._chkCor.FlatAppearance.BorderColor = System.Drawing.Color.LightSteelBlue;
            this._chkCor.FlatAppearance.BorderSize = 0;
            this._chkCor.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSteelBlue;
            this._chkCor.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSlateGray;
            this._chkCor.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this._chkCor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._chkCor.ForeColor = System.Drawing.Color.Blue;
            this._chkCor.Image = global::MetaboliteLevels.Properties.Resources.IconScriptCorrect;
            this._chkCor.Location = new System.Drawing.Point(32, 32);
            this._chkCor.Margin = new System.Windows.Forms.Padding(32, 32, 16, 16);
            this._chkCor.Name = "_chkCor";
            this._chkCor.Padding = new System.Windows.Forms.Padding(8);
            this._chkCor.Size = new System.Drawing.Size(128, 128);
            this._chkCor.TabIndex = 0;
            this._chkCor.Text = "Corrections";
            this._chkCor.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this._chkCor.UseVisualStyleBackColor = false;
            this._chkCor.CheckedChanged += new System.EventHandler(this._chkClus_CheckedChanged);
            // 
            // _chkClus
            // 
            this._chkClus.Appearance = System.Windows.Forms.Appearance.Button;
            this._chkClus.BackColor = System.Drawing.Color.LightSteelBlue;
            this._chkClus.FlatAppearance.BorderColor = System.Drawing.Color.LightSteelBlue;
            this._chkClus.FlatAppearance.BorderSize = 0;
            this._chkClus.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSteelBlue;
            this._chkClus.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSlateGray;
            this._chkClus.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this._chkClus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._chkClus.ForeColor = System.Drawing.Color.Blue;
            this._chkClus.Image = global::MetaboliteLevels.Properties.Resources.IconScriptCluster;
            this._chkClus.Location = new System.Drawing.Point(512, 32);
            this._chkClus.Margin = new System.Windows.Forms.Padding(16, 32, 16, 16);
            this._chkClus.Name = "_chkClus";
            this._chkClus.Padding = new System.Windows.Forms.Padding(8);
            this._chkClus.Size = new System.Drawing.Size(128, 128);
            this._chkClus.TabIndex = 1;
            this._chkClus.Text = "Clusters";
            this._chkClus.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this._chkClus.UseVisualStyleBackColor = false;
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
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 272);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(672, 56);
            this.flowLayoutPanel2.TabIndex = 2;
            // 
            // ctlButton1
            // 
            this.ctlButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ctlButton1.Image = global::MetaboliteLevels.Properties.Resources.MnuCancel;
            this.ctlButton1.Location = new System.Drawing.Point(536, 8);
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
            this.button1.Image = global::MetaboliteLevels.Properties.Resources.MnuAccept;
            this.button1.Location = new System.Drawing.Point(392, 8);
            this.button1.Margin = new System.Windows.Forms.Padding(8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 40);
            this.button1.TabIndex = 0;
            this.button1.Text = "OK";
            this.button1.UseDefaultSize = true;
            this.button1.UseVisualStyleBackColor = true;
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
            this.ctlTitleBar1.Size = new System.Drawing.Size(672, 87);
            this.ctlTitleBar1.SubText = "and here";
            this.ctlTitleBar1.TabIndex = 0;
            this.ctlTitleBar1.Text = "Text goes here";
            this.ctlTitleBar1.WarningText = null;
            // 
            // FrmEditUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(672, 328);
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
        private System.Windows.Forms.ToolTip toolTip1;
    }
}