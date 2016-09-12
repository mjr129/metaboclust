namespace MetaboliteLevels.Controls
{
    partial class CtlWizard
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._lblOrder = new System.Windows.Forms.Label();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this._btnCancel = new MetaboliteLevels.Controls.CtlButton();
            this._btnBack = new MetaboliteLevels.Controls.CtlButton();
            this._btnNext = new MetaboliteLevels.Controls.CtlButton();
            this._btnOk = new MetaboliteLevels.Controls.CtlButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ctlTitleBar1 = new MetaboliteLevels.Controls.CtlTitleBar();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this._lblOrder, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.ctlTitleBar1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(756, 395);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // _lblOrder
            // 
            this._lblOrder.BackColor = System.Drawing.Color.White;
            this._lblOrder.Dock = System.Windows.Forms.DockStyle.Top;
            this._lblOrder.ForeColor = System.Drawing.Color.Purple;
            this._lblOrder.Location = new System.Drawing.Point(0, 66);
            this._lblOrder.Margin = new System.Windows.Forms.Padding(0);
            this._lblOrder.Name = "_lblOrder";
            this._lblOrder.Size = new System.Drawing.Size(756, 28);
            this._lblOrder.TabIndex = 3;
            this._lblOrder.Text = "Order ➜ [Order] ➜ Order";
            this._lblOrder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._lblOrder.Paint += new System.Windows.Forms.PaintEventHandler(this._lblOrder_Paint);
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel3.AutoSize = true;
            this.flowLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel3.Controls.Add(this._btnCancel);
            this.flowLayoutPanel3.Controls.Add(this._btnBack);
            this.flowLayoutPanel3.Controls.Add(this._btnNext);
            this.flowLayoutPanel3.Controls.Add(this._btnOk);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(212, 339);
            this.flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(544, 56);
            this.flowLayoutPanel3.TabIndex = 18;
            this.flowLayoutPanel3.WrapContents = false;
            // 
            // _btnCancel
            // 
            this._btnCancel.Image = global::MetaboliteLevels.Properties.Resources.MnuCancel;
            this._btnCancel.Location = new System.Drawing.Point(4, 8);
            this._btnCancel.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(128, 40);
            this._btnCancel.TabIndex = 17;
            this._btnCancel.Text = "Cancel";
            this._btnCancel.UseDefaultSize = true;
            this._btnCancel.UseVisualStyleBackColor = true;
            this._btnCancel.Click += new System.EventHandler(this._btnCancel_Click_2);
            // 
            // _btnBack
            // 
            this._btnBack.Image = global::MetaboliteLevels.Properties.Resources.MnuBack;
            this._btnBack.Location = new System.Drawing.Point(140, 8);
            this._btnBack.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
            this._btnBack.Name = "_btnBack";
            this._btnBack.Size = new System.Drawing.Size(128, 40);
            this._btnBack.TabIndex = 13;
            this._btnBack.Text = "Back";
            this._btnBack.UseDefaultSize = true;
            this._btnBack.UseVisualStyleBackColor = true;
            this._btnBack.Click += new System.EventHandler(this._btnCancel_Click);
            // 
            // _btnNext
            // 
            this._btnNext.Image = global::MetaboliteLevels.Properties.Resources.MnuNext;
            this._btnNext.Location = new System.Drawing.Point(276, 8);
            this._btnNext.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
            this._btnNext.Name = "_btnNext";
            this._btnNext.Size = new System.Drawing.Size(128, 40);
            this._btnNext.TabIndex = 16;
            this._btnNext.Text = "Next";
            this._btnNext.UseDefaultSize = true;
            this._btnNext.UseVisualStyleBackColor = true;
            this._btnNext.Click += new System.EventHandler(this._btnNext_Click);
            // 
            // _btnOk
            // 
            this._btnOk.Image = global::MetaboliteLevels.Properties.Resources.MnuAccept;
            this._btnOk.Location = new System.Drawing.Point(412, 8);
            this._btnOk.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
            this._btnOk.Name = "_btnOk";
            this._btnOk.Size = new System.Drawing.Size(128, 40);
            this._btnOk.TabIndex = 12;
            this._btnOk.Text = "OK";
            this._btnOk.UseDefaultSize = true;
            this._btnOk.UseVisualStyleBackColor = true;
            this._btnOk.Click += new System.EventHandler(this._btnOk_Click);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 94);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(756, 245);
            this.panel1.TabIndex = 21;
            // 
            // ctlTitleBar1
            // 
            this.ctlTitleBar1.AutoSize = true;
            this.ctlTitleBar1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ctlTitleBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlTitleBar1.DrawHBar = false;
            this.ctlTitleBar1.HelpText = "";
            this.ctlTitleBar1.Location = new System.Drawing.Point(0, 0);
            this.ctlTitleBar1.Margin = new System.Windows.Forms.Padding(0);
            this.ctlTitleBar1.MinimumSize = new System.Drawing.Size(384, 0);
            this.ctlTitleBar1.Name = "ctlTitleBar1";
            this.ctlTitleBar1.Size = new System.Drawing.Size(756, 66);
            this.ctlTitleBar1.SubText = "";
            this.ctlTitleBar1.TabIndex = 22;
            this.ctlTitleBar1.Text = "Title";
            this.ctlTitleBar1.WarningText = null;
            this.ctlTitleBar1.HelpClicked += new System.ComponentModel.CancelEventHandler(this.ctlTitleBar1_HelpClicked);
            // 
            // CtlWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "CtlWizard";
            this.Size = new System.Drawing.Size(756, 395);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private MetaboliteLevels.Controls.CtlButton _btnBack;
        private MetaboliteLevels.Controls.CtlButton _btnNext;
        private MetaboliteLevels.Controls.CtlButton _btnOk;
        private System.Windows.Forms.Panel panel1;
        private MetaboliteLevels.Controls.CtlButton _btnCancel;
        private Controls.CtlTitleBar ctlTitleBar1;
        private System.Windows.Forms.Label _lblOrder;
    }
}
