namespace MetaboliteLevels.Forms.Startup
{
    partial class FrmSetWorkspace
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSetWorkspace));
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this.label9 = new System.Windows.Forms.Label();
            this.ctlButton4 = new MetaboliteLevels.Controls.CtlButton();
            this.label10 = new System.Windows.Forms.Label();
            this._txtDataFolder = new System.Windows.Forms.TextBox();
            this._btnSetDataFolder = new MetaboliteLevels.Controls.CtlButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this._btnOk = new MetaboliteLevels.Controls.CtlButton();
            this._btnCancel = new MetaboliteLevels.Controls.CtlButton();
            this.ctlTitleBar1 = new MetaboliteLevels.Controls.CtlTitleBar();
            this._btnExit = new MetaboliteLevels.Controls.CtlButton();
            this._btnRestart = new MetaboliteLevels.Controls.CtlButton();
            this.flowLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel5
            // 
            this.flowLayoutPanel5.AutoSize = true;
            this.flowLayoutPanel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel5.Controls.Add(this.label9);
            this.flowLayoutPanel5.Controls.Add(this.ctlButton4);
            this.flowLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.flowLayoutPanel5.Size = new System.Drawing.Size(136, 37);
            this.flowLayoutPanel5.TabIndex = 12;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(8, 8);
            this.label9.Margin = new System.Windows.Forms.Padding(8);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(91, 21);
            this.label9.TabIndex = 7;
            this.label9.Text = "Workspace";
            // 
            // ctlButton4
            // 
            this.ctlButton4.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ctlButton4.FlatAppearance.BorderSize = 0;
            this.ctlButton4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ctlButton4.Image = global::MetaboliteLevels.Properties.Resources.MnuDescribe;
            this.ctlButton4.Location = new System.Drawing.Point(107, 8);
            this.ctlButton4.Margin = new System.Windows.Forms.Padding(0, 8, 8, 8);
            this.ctlButton4.Name = "ctlButton4";
            this.ctlButton4.Size = new System.Drawing.Size(21, 21);
            this.ctlButton4.TabIndex = 17;
            this.ctlButton4.Text = "";
            this.ctlButton4.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label10.Location = new System.Drawing.Point(24, 45);
            this.label10.Margin = new System.Windows.Forms.Padding(24, 8, 8, 8);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(551, 32);
            this.label10.TabIndex = 10;
            this.label10.Text = "This indicates where this application should store its settings and scripts, and " +
    "where your data files are placed by default. This is normally the application\'s " +
    "own folder.";
            this.label10.Visible = false;
            // 
            // _txtDataFolder
            // 
            this._txtDataFolder.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._txtDataFolder.ForeColor = System.Drawing.Color.Blue;
            this._txtDataFolder.Location = new System.Drawing.Point(24, 93);
            this._txtDataFolder.Margin = new System.Windows.Forms.Padding(24, 8, 8, 8);
            this._txtDataFolder.Name = "_txtDataFolder";
            this._txtDataFolder.Size = new System.Drawing.Size(555, 29);
            this._txtDataFolder.TabIndex = 11;
            this._txtDataFolder.TextChanged += new System.EventHandler(this._txtDataFolder_TextChanged);
            // 
            // _btnSetDataFolder
            // 
            this._btnSetDataFolder.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnSetDataFolder.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnSetDataFolder.Image = ((System.Drawing.Image)(resources.GetObject("_btnSetDataFolder.Image")));
            this._btnSetDataFolder.Location = new System.Drawing.Point(595, 93);
            this._btnSetDataFolder.Margin = new System.Windows.Forms.Padding(8);
            this._btnSetDataFolder.Name = "_btnSetDataFolder";
            this._btnSetDataFolder.Size = new System.Drawing.Size(128, 40);
            this._btnSetDataFolder.TabIndex = 13;
            this._btnSetDataFolder.Text = "Browse";
            this._btnSetDataFolder.UseDefaultSize = true;
            this._btnSetDataFolder.UseVisualStyleBackColor = true;
            this._btnSetDataFolder.Click += new System.EventHandler(this._btnSetDataFolder_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel5, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._btnSetDataFolder, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this._txtDataFolder, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel3, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 66);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(731, 230);
            this.tableLayoutPanel1.TabIndex = 14;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel3.AutoSize = true;
            this.flowLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel3, 2);
            this.flowLayoutPanel3.Controls.Add(this._btnOk);
            this.flowLayoutPanel3.Controls.Add(this._btnCancel);
            this.flowLayoutPanel3.Controls.Add(this._btnExit);
            this.flowLayoutPanel3.Controls.Add(this._btnRestart);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(179, 174);
            this.flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(552, 56);
            this.flowLayoutPanel3.TabIndex = 19;
            this.flowLayoutPanel3.WrapContents = false;
            // 
            // _btnOk
            // 
            this._btnOk.Enabled = false;
            this._btnOk.Image = global::MetaboliteLevels.Properties.Resources.MnuAccept;
            this._btnOk.Location = new System.Drawing.Point(8, 8);
            this._btnOk.Margin = new System.Windows.Forms.Padding(8);
            this._btnOk.Name = "_btnOk";
            this._btnOk.Size = new System.Drawing.Size(128, 40);
            this._btnOk.TabIndex = 12;
            this._btnOk.Text = "Apply";
            this._btnOk.UseDefaultSize = true;
            this._btnOk.UseVisualStyleBackColor = true;
            this._btnOk.Click += new System.EventHandler(this._btnOk_Click);
            // 
            // _btnCancel
            // 
            this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnCancel.Image = global::MetaboliteLevels.Properties.Resources.MnuBack;
            this._btnCancel.Location = new System.Drawing.Point(144, 8);
            this._btnCancel.Margin = new System.Windows.Forms.Padding(0, 8, 8, 8);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(128, 40);
            this._btnCancel.TabIndex = 16;
            this._btnCancel.Text = "Cancel";
            this._btnCancel.UseDefaultSize = true;
            this._btnCancel.UseVisualStyleBackColor = true;
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
            this.ctlTitleBar1.Size = new System.Drawing.Size(731, 66);
            this.ctlTitleBar1.SubText = "";
            this.ctlTitleBar1.TabIndex = 15;
            this.ctlTitleBar1.Text = "Change Workspace";
            this.ctlTitleBar1.WarningText = null;
            // 
            // _btnExit
            // 
            this._btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnExit.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnExit.Image = global::MetaboliteLevels.Properties.Resources.MnuCancel;
            this._btnExit.Location = new System.Drawing.Point(280, 8);
            this._btnExit.Margin = new System.Windows.Forms.Padding(0, 8, 8, 8);
            this._btnExit.Name = "_btnExit";
            this._btnExit.Size = new System.Drawing.Size(128, 40);
            this._btnExit.TabIndex = 17;
            this._btnExit.Text = "Exit";
            this._btnExit.UseDefaultSize = true;
            this._btnExit.UseVisualStyleBackColor = true;
            this._btnExit.Click += new System.EventHandler(this._btnExit_Click);
            // 
            // _btnRestart
            // 
            this._btnRestart.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnRestart.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnRestart.Image = global::MetaboliteLevels.Properties.Resources.MnuRefresh;
            this._btnRestart.Location = new System.Drawing.Point(416, 8);
            this._btnRestart.Margin = new System.Windows.Forms.Padding(0, 8, 8, 8);
            this._btnRestart.Name = "_btnRestart";
            this._btnRestart.Size = new System.Drawing.Size(128, 40);
            this._btnRestart.TabIndex = 18;
            this._btnRestart.Text = "Restart";
            this._btnRestart.UseDefaultSize = true;
            this._btnRestart.UseVisualStyleBackColor = true;
            this._btnRestart.Click += new System.EventHandler(this._btnRestart_Click);
            // 
            // FrmSetWorkspace
            // 
            this.AcceptButton = this._btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._btnCancel;
            this.ClientSize = new System.Drawing.Size(731, 296);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.ctlTitleBar1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSetWorkspace";
            this.Text = "Set Workspace";
            this.flowLayoutPanel5.ResumeLayout(false);
            this.flowLayoutPanel5.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.Label label9;
        private Controls.CtlButton ctlButton4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox _txtDataFolder;
        private Controls.CtlButton _btnSetDataFolder;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private Controls.CtlButton _btnOk;
        private Controls.CtlButton _btnCancel;
        private Controls.CtlTitleBar ctlTitleBar1;
        private Controls.CtlButton _btnExit;
        private Controls.CtlButton _btnRestart;
    }
}