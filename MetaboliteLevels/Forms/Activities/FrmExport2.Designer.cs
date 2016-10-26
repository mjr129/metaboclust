namespace MetaboliteLevels.Forms.Activities
{
    partial class FrmExport2
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
            if (disposing && (components != null))
            {
                components.Dispose();
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
            this.ctlTitleBar1 = new MetaboliteLevels.Controls.CtlTitleBar();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._btnCancel = new MetaboliteLevels.Controls.CtlButton();
            this._btnOk = new MetaboliteLevels.Controls.CtlButton();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.ctlButton1 = new MetaboliteLevels.Controls.CtlButton();
            this.ctlButton6 = new MetaboliteLevels.Controls.CtlButton();
            this.ctlButton2 = new MetaboliteLevels.Controls.CtlButton();
            this.ctlLabel1 = new MetaboliteLevels.Controls.CtlLabel();
            this.ctlLabel2 = new MetaboliteLevels.Controls.CtlLabel();
            this.ctlLabel4 = new MetaboliteLevels.Controls.CtlLabel();
            this.ctlLabel5 = new MetaboliteLevels.Controls.CtlLabel();
            this._txtDir = new MGui.Controls.CtlTextBox();
            this._txtFile = new MGui.Controls.CtlTextBox();
            this.ctlButton3 = new MetaboliteLevels.Controls.CtlButton();
            this._btnBrowseDir = new MetaboliteLevels.Controls.CtlButton();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
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
            this.ctlTitleBar1.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.ctlTitleBar1.MinimumSize = new System.Drawing.Size(576, 0);
            this.ctlTitleBar1.Name = "ctlTitleBar1";
            this.ctlTitleBar1.Size = new System.Drawing.Size(776, 87);
            this.ctlTitleBar1.SubText = "Select the data you\'d like to export";
            this.ctlTitleBar1.TabIndex = 7;
            this.ctlTitleBar1.Text = "Export";
            this.ctlTitleBar1.WarningText = null;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this._btnCancel);
            this.flowLayoutPanel1.Controls.Add(this._btnOk);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 596);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(776, 56);
            this.flowLayoutPanel1.TabIndex = 8;
            // 
            // _btnCancel
            // 
            this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnCancel.Image = global::MetaboliteLevels.Properties.Resources.MnuCancel;
            this._btnCancel.Location = new System.Drawing.Point(640, 8);
            this._btnCancel.Margin = new System.Windows.Forms.Padding(8);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(128, 40);
            this._btnCancel.TabIndex = 0;
            this._btnCancel.Text = "Cancel";
            this._btnCancel.UseDefaultSize = true;
            this._btnCancel.UseVisualStyleBackColor = true;
            // 
            // _btnOk
            // 
            this._btnOk.Enabled = false;
            this._btnOk.Image = global::MetaboliteLevels.Properties.Resources.MnuAccept;
            this._btnOk.Location = new System.Drawing.Point(496, 8);
            this._btnOk.Margin = new System.Windows.Forms.Padding(8);
            this._btnOk.Name = "_btnOk";
            this._btnOk.Size = new System.Drawing.Size(128, 40);
            this._btnOk.TabIndex = 0;
            this._btnOk.Text = "OK";
            this._btnOk.UseDefaultSize = true;
            this._btnOk.UseVisualStyleBackColor = true;
            this._btnOk.Click += new System.EventHandler(this._btnOk_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(98, 8);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(8);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(625, 30);
            this.comboBox1.TabIndex = 9;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBox2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(98, 54);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(8);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(625, 30);
            this.comboBox2.TabIndex = 9;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.ctlButton2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.comboBox1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.comboBox2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.ctlLabel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.ctlLabel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.ctlLabel4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.ctlLabel5, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this._txtDir, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this._txtFile, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.ctlButton3, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this._btnBrowseDir, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.listBox1, 0, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 87);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(776, 509);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel2, 3);
            this.flowLayoutPanel2.Controls.Add(this.ctlButton1);
            this.flowLayoutPanel2.Controls.Add(this.ctlButton6);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 185);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(366, 56);
            this.flowLayoutPanel2.TabIndex = 11;
            // 
            // ctlButton1
            // 
            this.ctlButton1.Image = global::MetaboliteLevels.Properties.Resources.MnuNext;
            this.ctlButton1.Location = new System.Drawing.Point(8, 8);
            this.ctlButton1.Margin = new System.Windows.Forms.Padding(8);
            this.ctlButton1.Name = "ctlButton1";
            this.ctlButton1.Size = new System.Drawing.Size(206, 40);
            this.ctlButton1.TabIndex = 0;
            this.ctlButton1.Text = "Add to export list";
            this.ctlButton1.UseVisualStyleBackColor = true;
            this.ctlButton1.Click += new System.EventHandler(this.ctlButton1_Click);
            // 
            // ctlButton6
            // 
            this.ctlButton6.Image = global::MetaboliteLevels.Properties.Resources.MnuDelete;
            this.ctlButton6.Location = new System.Drawing.Point(230, 8);
            this.ctlButton6.Margin = new System.Windows.Forms.Padding(8);
            this.ctlButton6.Name = "ctlButton6";
            this.ctlButton6.Size = new System.Drawing.Size(128, 40);
            this.ctlButton6.TabIndex = 0;
            this.ctlButton6.Text = "Remove";
            this.ctlButton6.UseDefaultSize = true;
            this.ctlButton6.UseVisualStyleBackColor = true;
            this.ctlButton6.Click += new System.EventHandler(this.ctlButton6_Click);
            // 
            // ctlButton2
            // 
            this.ctlButton2.Image = global::MetaboliteLevels.Properties.Resources.MnuEnlargeList;
            this.ctlButton2.Location = new System.Drawing.Point(739, 8);
            this.ctlButton2.Margin = new System.Windows.Forms.Padding(8);
            this.ctlButton2.Name = "ctlButton2";
            this.ctlButton2.Size = new System.Drawing.Size(29, 29);
            this.ctlButton2.TabIndex = 11;
            this.ctlButton2.Text = "";
            this.ctlButton2.UseDefaultSize = true;
            this.ctlButton2.UseVisualStyleBackColor = true;
            // 
            // ctlLabel1
            // 
            this.ctlLabel1.AutoSize = true;
            this.ctlLabel1.Location = new System.Drawing.Point(8, 8);
            this.ctlLabel1.Margin = new System.Windows.Forms.Padding(8);
            this.ctlLabel1.Name = "ctlLabel1";
            this.ctlLabel1.Size = new System.Drawing.Size(42, 21);
            this.ctlLabel1.TabIndex = 10;
            this.ctlLabel1.Text = "Data";
            // 
            // ctlLabel2
            // 
            this.ctlLabel2.AutoSize = true;
            this.ctlLabel2.Location = new System.Drawing.Point(8, 54);
            this.ctlLabel2.Margin = new System.Windows.Forms.Padding(8);
            this.ctlLabel2.Name = "ctlLabel2";
            this.ctlLabel2.Size = new System.Drawing.Size(41, 21);
            this.ctlLabel2.TabIndex = 10;
            this.ctlLabel2.Text = "Item";
            // 
            // ctlLabel4
            // 
            this.ctlLabel4.AutoSize = true;
            this.ctlLabel4.Location = new System.Drawing.Point(8, 100);
            this.ctlLabel4.Margin = new System.Windows.Forms.Padding(8);
            this.ctlLabel4.Name = "ctlLabel4";
            this.ctlLabel4.Size = new System.Drawing.Size(74, 21);
            this.ctlLabel4.TabIndex = 10;
            this.ctlLabel4.Text = "Directory";
            // 
            // ctlLabel5
            // 
            this.ctlLabel5.AutoSize = true;
            this.ctlLabel5.Location = new System.Drawing.Point(8, 145);
            this.ctlLabel5.Margin = new System.Windows.Forms.Padding(8);
            this.ctlLabel5.Name = "ctlLabel5";
            this.ctlLabel5.Size = new System.Drawing.Size(34, 21);
            this.ctlLabel5.TabIndex = 10;
            this.ctlLabel5.Text = "File";
            // 
            // _txtDir
            // 
            this._txtDir.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtDir.Location = new System.Drawing.Point(98, 100);
            this._txtDir.Margin = new System.Windows.Forms.Padding(8);
            this._txtDir.Name = "_txtDir";
            this._txtDir.Size = new System.Drawing.Size(625, 29);
            this._txtDir.TabIndex = 11;
            this._txtDir.Watermark = null;
            // 
            // _txtFile
            // 
            this._txtFile.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtFile.Location = new System.Drawing.Point(98, 145);
            this._txtFile.Margin = new System.Windows.Forms.Padding(8);
            this._txtFile.Name = "_txtFile";
            this._txtFile.Size = new System.Drawing.Size(625, 29);
            this._txtFile.TabIndex = 11;
            this._txtFile.Watermark = "Leave blank to use default";
            // 
            // ctlButton3
            // 
            this.ctlButton3.Image = global::MetaboliteLevels.Properties.Resources.MnuEnlargeList;
            this.ctlButton3.Location = new System.Drawing.Point(739, 54);
            this.ctlButton3.Margin = new System.Windows.Forms.Padding(8);
            this.ctlButton3.Name = "ctlButton3";
            this.ctlButton3.Size = new System.Drawing.Size(29, 29);
            this.ctlButton3.TabIndex = 11;
            this.ctlButton3.Text = "";
            this.ctlButton3.UseDefaultSize = true;
            this.ctlButton3.UseVisualStyleBackColor = true;
            // 
            // _btnBrowseDir
            // 
            this._btnBrowseDir.Image = global::MetaboliteLevels.Properties.Resources.MnuEnlargeList;
            this._btnBrowseDir.Location = new System.Drawing.Point(739, 100);
            this._btnBrowseDir.Margin = new System.Windows.Forms.Padding(8);
            this._btnBrowseDir.Name = "_btnBrowseDir";
            this._btnBrowseDir.Size = new System.Drawing.Size(29, 29);
            this._btnBrowseDir.TabIndex = 11;
            this._btnBrowseDir.Text = "";
            this._btnBrowseDir.UseDefaultSize = true;
            this._btnBrowseDir.UseVisualStyleBackColor = true;
            this._btnBrowseDir.Click += new System.EventHandler(this._btnBrowseDir_Click);
            // 
            // listBox1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.listBox1, 3);
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 21;
            this.listBox1.Location = new System.Drawing.Point(8, 252);
            this.listBox1.Margin = new System.Windows.Forms.Padding(8);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(760, 249);
            this.listBox1.TabIndex = 11;
            // 
            // FrmExport2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 652);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.ctlTitleBar1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmExport2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Export";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.CtlTitleBar ctlTitleBar1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Controls.CtlButton _btnCancel;
        private Controls.CtlButton _btnOk;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Controls.CtlButton ctlButton1;
        private Controls.CtlLabel ctlLabel1;
        private Controls.CtlLabel ctlLabel2;
        private Controls.CtlButton ctlButton2;
        private Controls.CtlLabel ctlLabel4;
        private Controls.CtlLabel ctlLabel5;
        private MGui.Controls.CtlTextBox _txtDir;
        private MGui.Controls.CtlTextBox _txtFile;
        private Controls.CtlButton ctlButton3;
        private Controls.CtlButton _btnBrowseDir;
        private System.Windows.Forms.ListBox listBox1;
        private Controls.CtlButton ctlButton6;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
    }
}