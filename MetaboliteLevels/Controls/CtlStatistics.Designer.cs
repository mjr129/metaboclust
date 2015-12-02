namespace MetaboliteLevels.Controls
{
    partial class CtlStatistics
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this._btnFilter = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._lblParams = new System.Windows.Forms.Label();
            this._txtParams = new System.Windows.Forms.TextBox();
            this._lstFilter = new System.Windows.Forms.ComboBox();
            this._btnEditParameters = new System.Windows.Forms.Button();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this._btnEditParameters, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this._btnFilter, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.comboBox1, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this._lstFilter, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(916, 271);
            this.tableLayoutPanel2.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.label2, 2);
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label2.Location = new System.Drawing.Point(8, 8);
            this.label2.Margin = new System.Windows.Forms.Padding(8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(302, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "Should insignificant variables be filtered";
            // 
            // _btnFilter
            // 
            this._btnFilter.Image = global::MetaboliteLevels.Properties.Resources.MnuAdd;
            this._btnFilter.Location = new System.Drawing.Point(879, 45);
            this._btnFilter.Margin = new System.Windows.Forms.Padding(8);
            this._btnFilter.Name = "_btnFilter";
            this._btnFilter.Size = new System.Drawing.Size(29, 29);
            this._btnFilter.TabIndex = 3;
            this._btnFilter.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new System.Drawing.Point(8, 90);
            this.label1.Margin = new System.Windows.Forms.Padding(8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Distance measure";
            // 
            // comboBox1
            // 
            this.comboBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(8, 127);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(8);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(855, 29);
            this.comboBox1.TabIndex = 4;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this._lblParams, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._txtParams, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 164);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(871, 45);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // _lblParams
            // 
            this._lblParams.AutoSize = true;
            this._lblParams.Location = new System.Drawing.Point(24, 8);
            this._lblParams.Margin = new System.Windows.Forms.Padding(24, 8, 8, 8);
            this._lblParams.Name = "_lblParams";
            this._lblParams.Size = new System.Drawing.Size(52, 21);
            this._lblParams.TabIndex = 0;
            this._lblParams.Text = "label3";
            // 
            // _txtParams
            // 
            this._txtParams.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtParams.Location = new System.Drawing.Point(92, 8);
            this._txtParams.Margin = new System.Windows.Forms.Padding(8);
            this._txtParams.Name = "_txtParams";
            this._txtParams.Size = new System.Drawing.Size(771, 29);
            this._txtParams.TabIndex = 1;
            // 
            // _lstFilter
            // 
            this._lstFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this._lstFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstFilter.FormattingEnabled = true;
            this._lstFilter.Location = new System.Drawing.Point(8, 45);
            this._lstFilter.Margin = new System.Windows.Forms.Padding(8);
            this._lstFilter.Name = "_lstFilter";
            this._lstFilter.Size = new System.Drawing.Size(855, 29);
            this._lstFilter.TabIndex = 4;
            this._lstFilter.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // _btnEditParameters
            // 
            this._btnEditParameters.Image = global::MetaboliteLevels.Properties.Resources.MnuEnlargeList;
            this._btnEditParameters.Location = new System.Drawing.Point(879, 172);
            this._btnEditParameters.Margin = new System.Windows.Forms.Padding(8);
            this._btnEditParameters.Name = "_btnEditParameters";
            this._btnEditParameters.Size = new System.Drawing.Size(28, 28);
            this._btnEditParameters.TabIndex = 17;
            this._btnEditParameters.UseVisualStyleBackColor = true;
            this._btnEditParameters.Visible = false;
            this._btnEditParameters.Click += new System.EventHandler(this._btnEditParameters_Click);
            // 
            // CtlStatistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "CtlStatistics";
            this.Size = new System.Drawing.Size(916, 271);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button _btnFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label _lblParams;
        private System.Windows.Forms.TextBox _txtParams;
        private System.Windows.Forms.ComboBox _lstFilter;
        private System.Windows.Forms.Button _btnEditParameters;
    }
}
