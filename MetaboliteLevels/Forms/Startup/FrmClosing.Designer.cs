namespace MetaboliteLevels.Forms.Startup
{
    partial class FrmClosing
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.textBox1 = new Controls.CtlTextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._btnYes = new Controls.CtlButton();
            this._btnNo = new Controls.CtlButton();
            this._btnCancel = new Controls.CtlButton();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.comboBox1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(16);
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(846, 227);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Window;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tableLayoutPanel1.SetColumnSpan(this.textBox1, 2);
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.textBox1.Location = new System.Drawing.Point(24, 61);
            this.textBox1.Margin = new System.Windows.Forms.Padding(8);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(798, 22);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "c:\\folder\\file.txt";
            this.textBox1.Visible = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this._btnYes);
            this.flowLayoutPanel1.Controls.Add(this._btnNo);
            this.flowLayoutPanel1.Controls.Add(this._btnCancel);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(398, 155);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(432, 56);
            this.flowLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // _btnYes
            // 
            this._btnYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this._btnYes.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnYes.Image = global::MetaboliteLevels.Properties.Resources.MnuAccept;
            this._btnYes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._btnYes.Location = new System.Drawing.Point(8, 8);
            this._btnYes.Margin = new System.Windows.Forms.Padding(8);
            this._btnYes.Name = "_btnYes";
            this._btnYes.Padding = new System.Windows.Forms.Padding(4);
            this._btnYes.Size = new System.Drawing.Size(128, 40);
            this._btnYes.TabIndex = 5;
            this._btnYes.Text = "    Yes";
            this._btnYes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._btnYes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._btnYes.UseVisualStyleBackColor = true;
            // 
            // _btnNo
            // 
            this._btnNo.DialogResult = System.Windows.Forms.DialogResult.No;
            this._btnNo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnNo.Image = global::MetaboliteLevels.Properties.Resources.MnuCancel;
            this._btnNo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._btnNo.Location = new System.Drawing.Point(152, 8);
            this._btnNo.Margin = new System.Windows.Forms.Padding(8);
            this._btnNo.Name = "_btnNo";
            this._btnNo.Padding = new System.Windows.Forms.Padding(4);
            this._btnNo.Size = new System.Drawing.Size(128, 40);
            this._btnNo.TabIndex = 7;
            this._btnNo.Text = "    No";
            this._btnNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._btnNo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._btnNo.UseVisualStyleBackColor = true;
            // 
            // _btnCancel
            // 
            this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnCancel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnCancel.Image = global::MetaboliteLevels.Properties.Resources.MnuBack;
            this._btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._btnCancel.Location = new System.Drawing.Point(296, 8);
            this._btnCancel.Margin = new System.Windows.Forms.Padding(8);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Padding = new System.Windows.Forms.Padding(4);
            this._btnCancel.Size = new System.Drawing.Size(128, 40);
            this._btnCancel.TabIndex = 6;
            this._btnCancel.Text = "    Cancel";
            this._btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._btnCancel.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox1.BackColor = System.Drawing.SystemColors.Control;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Ask every time",
            "Remember my decision (this session)",
            "Remember my decision (always)"});
            this.comboBox1.Location = new System.Drawing.Point(24, 182);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(8);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(344, 29);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label1, 2);
            this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new System.Drawing.Point(24, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(338, 21);
            this.label1.TabIndex = 4;
            this.label1.Text = "Do you want to save the session before exiting?";
            // 
            // FrmClosing
            // 
            this.AcceptButton = this._btnYes;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this._btnCancel;
            this.ClientSize = new System.Drawing.Size(846, 227);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmClosing";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Controls.CtlButton _btnYes;
        private Controls.CtlButton _btnNo;
        private Controls.CtlButton _btnCancel;
        private System.Windows.Forms.ComboBox comboBox1;
        private Controls.CtlTextBox textBox1;
        private System.Windows.Forms.Label label1;
    }
}