namespace MetaboliteLevels.Forms.Startup
{
    partial class FrmSetupWorkspace
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSetupWorkspace));
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this.label9 = new System.Windows.Forms.Label();
            this.ctlButton4 = new MetaboliteLevels.Controls.CtlButton();
            this.label10 = new System.Windows.Forms.Label();
            this._txtDataFolder = new MGui.Controls.CtlTextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this._btnOk = new MetaboliteLevels.Controls.CtlButton();
            this._btnCancel = new MetaboliteLevels.Controls.CtlButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.ctlButton1 = new MetaboliteLevels.Controls.CtlButton();
            this.label2 = new System.Windows.Forms.Label();
            this._radLocal = new System.Windows.Forms.RadioButton();
            this._radUser = new System.Windows.Forms.RadioButton();
            this._radMachine = new System.Windows.Forms.RadioButton();
            this._radNone = new System.Windows.Forms.RadioButton();
            this._btnSetDataFolder = new MetaboliteLevels.Controls.CtlButton();
            this.ctlTitleBar1 = new MetaboliteLevels.Controls.CtlTitleBar();
            this.flowLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
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
            this.flowLayoutPanel5.Size = new System.Drawing.Size(188, 37);
            this.flowLayoutPanel5.TabIndex = 12;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(8, 8);
            this.label9.Margin = new System.Windows.Forms.Padding(8);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(143, 21);
            this.label9.TabIndex = 7;
            this.label9.Text = "Working directory";
            // 
            // ctlButton4
            // 
            this.ctlButton4.FlatAppearance.BorderSize = 0;
            this.ctlButton4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ctlButton4.Image = global::MetaboliteLevels.Properties.Resources.MnuDescribe;
            this.ctlButton4.Location = new System.Drawing.Point(159, 8);
            this.ctlButton4.Margin = new System.Windows.Forms.Padding(0, 8, 8, 8);
            this.ctlButton4.Name = "ctlButton4";
            this.ctlButton4.Size = new System.Drawing.Size(21, 21);
            this.ctlButton4.TabIndex = 17;
            this.ctlButton4.Text = "";
            this.ctlButton4.UseVisualStyleBackColor = true;
            this.ctlButton4.Click += new System.EventHandler(this.ctlButton4_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label10.Location = new System.Drawing.Point(24, 45);
            this.label10.Margin = new System.Windows.Forms.Padding(24, 8, 8, 8);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(659, 32);
            this.label10.TabIndex = 10;
            this.label10.Text = "This indicates where this application should store its settings and scripts, and " +
    "where your data files are placed by default. This is normally the application\'s " +
    "own folder.";
            this.label10.Visible = false;
            // 
            // _txtDataFolder
            // 
            this._txtDataFolder.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtDataFolder.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._txtDataFolder.ForeColor = System.Drawing.Color.Blue;
            this._txtDataFolder.Location = new System.Drawing.Point(24, 93);
            this._txtDataFolder.Margin = new System.Windows.Forms.Padding(24, 8, 8, 8);
            this._txtDataFolder.Name = "_txtDataFolder";
            this._txtDataFolder.ReadOnly = true;
            this._txtDataFolder.Size = new System.Drawing.Size(667, 29);
            this._txtDataFolder.TabIndex = 11;
            this._txtDataFolder.TextChanged += new System.EventHandler(this._txtDataFolder_TextChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel5, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._txtDataFolder, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel3, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this._radLocal, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this._radUser, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this._radMachine, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this._radNone, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this._btnSetDataFolder, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 87);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 11;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(843, 557);
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
            this.flowLayoutPanel3.Location = new System.Drawing.Point(451, 501);
            this.flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(392, 56);
            this.flowLayoutPanel3.TabIndex = 19;
            this.flowLayoutPanel3.WrapContents = false;
            // 
            // _btnOk
            // 
            this._btnOk.Enabled = false;
            this._btnOk.Image = global::MetaboliteLevels.Properties.Resources.MnuRefresh;
            this._btnOk.Location = new System.Drawing.Point(8, 8);
            this._btnOk.Margin = new System.Windows.Forms.Padding(8);
            this._btnOk.Name = "_btnOk";
            this._btnOk.Size = new System.Drawing.Size(240, 40);
            this._btnOk.TabIndex = 12;
            this._btnOk.Text = "Accept changes and restart";
            this._btnOk.UseVisualStyleBackColor = true;
            this._btnOk.Click += new System.EventHandler(this._btnOk_Click);
            // 
            // _btnCancel
            // 
            this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnCancel.Image = global::MetaboliteLevels.Properties.Resources.MnuCancel;
            this._btnCancel.Location = new System.Drawing.Point(256, 8);
            this._btnCancel.Margin = new System.Windows.Forms.Padding(0, 8, 8, 8);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(128, 40);
            this._btnCancel.TabIndex = 16;
            this._btnCancel.Text = "Cancel";
            this._btnCancel.UseDefaultSize = true;
            this._btnCancel.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.ctlButton1);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 141);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(160, 37);
            this.flowLayoutPanel1.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Margin = new System.Windows.Forms.Padding(8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 21);
            this.label1.TabIndex = 7;
            this.label1.Text = "Who is this for";
            // 
            // ctlButton1
            // 
            this.ctlButton1.FlatAppearance.BorderSize = 0;
            this.ctlButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ctlButton1.Image = global::MetaboliteLevels.Properties.Resources.MnuDescribe;
            this.ctlButton1.Location = new System.Drawing.Point(131, 8);
            this.ctlButton1.Margin = new System.Windows.Forms.Padding(0, 8, 8, 8);
            this.ctlButton1.Name = "ctlButton1";
            this.ctlButton1.Size = new System.Drawing.Size(21, 21);
            this.ctlButton1.TabIndex = 17;
            this.ctlButton1.Text = "";
            this.ctlButton1.UseVisualStyleBackColor = true;
            this.ctlButton1.Click += new System.EventHandler(this.ctlButton1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(24, 186);
            this.label2.Margin = new System.Windows.Forms.Padding(24, 8, 8, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(665, 32);
            this.label2.TabIndex = 10;
            this.label2.Text = "This is how the application should find the working directory. Use a file or the " +
    "Windows Registry depending on how you use the application.";
            this.label2.Visible = false;
            // 
            // _radLocal
            // 
            this._radLocal.AutoSize = true;
            this._radLocal.Location = new System.Drawing.Point(24, 234);
            this._radLocal.Margin = new System.Windows.Forms.Padding(24, 8, 8, 8);
            this._radLocal.Name = "_radLocal";
            this._radLocal.Size = new System.Drawing.Size(242, 25);
            this._radLocal.TabIndex = 20;
            this._radLocal.Text = "Local file - this application only";
            this._radLocal.UseVisualStyleBackColor = true;
            // 
            // _radUser
            // 
            this._radUser.AutoSize = true;
            this._radUser.Location = new System.Drawing.Point(24, 275);
            this._radUser.Margin = new System.Windows.Forms.Padding(24, 8, 8, 8);
            this._radUser.Name = "_radUser";
            this._radUser.Size = new System.Drawing.Size(158, 25);
            this._radUser.TabIndex = 20;
            this._radUser.Text = "Registry - this user";
            this._radUser.UseVisualStyleBackColor = true;
            // 
            // _radMachine
            // 
            this._radMachine.AutoSize = true;
            this._radMachine.Location = new System.Drawing.Point(24, 316);
            this._radMachine.Margin = new System.Windows.Forms.Padding(24, 8, 8, 8);
            this._radMachine.Name = "_radMachine";
            this._radMachine.Size = new System.Drawing.Size(187, 25);
            this._radMachine.TabIndex = 20;
            this._radMachine.Text = "Registry - this machine";
            this._radMachine.UseVisualStyleBackColor = true;
            // 
            // _radNone
            // 
            this._radNone.AutoSize = true;
            this._radNone.Location = new System.Drawing.Point(24, 357);
            this._radNone.Margin = new System.Windows.Forms.Padding(24, 8, 8, 8);
            this._radNone.Name = "_radNone";
            this._radNone.Size = new System.Drawing.Size(330, 25);
            this._radNone.TabIndex = 20;
            this._radNone.Text = "None - remove changes and use the default";
            this._radNone.UseVisualStyleBackColor = true;
            this._radNone.CheckedChanged += new System.EventHandler(this._radNone_CheckedChanged);
            // 
            // _btnSetDataFolder
            // 
            this._btnSetDataFolder.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnSetDataFolder.Image = global::MetaboliteLevels.Properties.Resources.MnuOpen;
            this._btnSetDataFolder.Location = new System.Drawing.Point(707, 93);
            this._btnSetDataFolder.Margin = new System.Windows.Forms.Padding(8);
            this._btnSetDataFolder.Name = "_btnSetDataFolder";
            this._btnSetDataFolder.Size = new System.Drawing.Size(128, 40);
            this._btnSetDataFolder.TabIndex = 13;
            this._btnSetDataFolder.Text = "Browse";
            this._btnSetDataFolder.UseDefaultSize = true;
            this._btnSetDataFolder.UseVisualStyleBackColor = true;
            this._btnSetDataFolder.Click += new System.EventHandler(this._btnSetDataFolder_Click);
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
            this.ctlTitleBar1.Size = new System.Drawing.Size(843, 87);
            this.ctlTitleBar1.SubText = "Changing the working directory will cause the application to restart";
            this.ctlTitleBar1.TabIndex = 15;
            this.ctlTitleBar1.Text = "Working Directory";
            this.ctlTitleBar1.WarningText = null;
            // 
            // FrmSetWorkspace
            // 
            this.AcceptButton = this._btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._btnCancel;
            this.ClientSize = new System.Drawing.Size(843, 644);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.ctlTitleBar1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSetWorkspace";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Set Workspace";
            this.flowLayoutPanel5.ResumeLayout(false);
            this.flowLayoutPanel5.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.Label label9;
        private Controls.CtlButton ctlButton4;
        private System.Windows.Forms.Label label10;
        private MGui.Controls.CtlTextBox _txtDataFolder;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private Controls.CtlButton _btnOk;
        private Controls.CtlButton _btnCancel;
        private Controls.CtlTitleBar ctlTitleBar1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private Controls.CtlButton ctlButton1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton _radLocal;
        private System.Windows.Forms.RadioButton _radUser;
        private System.Windows.Forms.RadioButton _radMachine;
        private Controls.CtlButton _btnSetDataFolder;
        private System.Windows.Forms.RadioButton _radNone;
    }
}