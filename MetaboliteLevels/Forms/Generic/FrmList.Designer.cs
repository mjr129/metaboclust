namespace MetaboliteLevels.Forms.Generic
{
    partial class FrmList
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._btnEdit = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this._flpSelectAll = new System.Windows.Forms.FlowLayoutPanel();
            this._btnSelectNone = new System.Windows.Forms.Button();
            this._btnSelectAll = new System.Windows.Forms.Button();
            this.ctlTitleBar1 = new MetaboliteLevels.Controls.CtlTitleBar();
            this.tableLayoutPanel1.SuspendLayout();
            this._flpSelectAll.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Image = global::MetaboliteLevels.Properties.Resources.MnuAccept;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(383, 8);
            this.button1.Margin = new System.Windows.Forms.Padding(8);
            this.button1.Name = "button1";
            this.button1.Padding = new System.Windows.Forms.Padding(4);
            this.button1.Size = new System.Drawing.Size(128, 40);
            this.button1.TabIndex = 1;
            this.button1.Text = "  OK";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Image = global::MetaboliteLevels.Properties.Resources.MnuCancel;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(527, 8);
            this.button2.Margin = new System.Windows.Forms.Padding(8);
            this.button2.Name = "button2";
            this.button2.Padding = new System.Windows.Forms.Padding(4);
            this.button2.Size = new System.Drawing.Size(128, 40);
            this.button2.TabIndex = 2;
            this.button2.Text = "  Cancel";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.button1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.button2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this._btnEdit, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 644);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(663, 56);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // _btnEdit
            // 
            this._btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnEdit.Image = global::MetaboliteLevels.Properties.Resources.MnuEdit;
            this._btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._btnEdit.Location = new System.Drawing.Point(8, 8);
            this._btnEdit.Margin = new System.Windows.Forms.Padding(8);
            this._btnEdit.Name = "_btnEdit";
            this._btnEdit.Padding = new System.Windows.Forms.Padding(4);
            this._btnEdit.Size = new System.Drawing.Size(128, 40);
            this._btnEdit.TabIndex = 1;
            this._btnEdit.Text = "  Edit list";
            this._btnEdit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._btnEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._btnEdit.UseVisualStyleBackColor = true;
            this._btnEdit.Click += new System.EventHandler(this._btnEdit_Click);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 134);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(663, 510);
            this.panel1.TabIndex = 7;
            // 
            // _flpSelectAll
            // 
            this._flpSelectAll.AutoScroll = true;
            this._flpSelectAll.AutoSize = true;
            this._flpSelectAll.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._flpSelectAll.BackColor = System.Drawing.Color.LightSteelBlue;
            this._flpSelectAll.Controls.Add(this._btnSelectNone);
            this._flpSelectAll.Controls.Add(this._btnSelectAll);
            this._flpSelectAll.Dock = System.Windows.Forms.DockStyle.Top;
            this._flpSelectAll.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._flpSelectAll.ForeColor = System.Drawing.Color.Black;
            this._flpSelectAll.Location = new System.Drawing.Point(0, 87);
            this._flpSelectAll.Name = "_flpSelectAll";
            this._flpSelectAll.Padding = new System.Windows.Forms.Padding(8);
            this._flpSelectAll.Size = new System.Drawing.Size(663, 47);
            this._flpSelectAll.TabIndex = 0;
            this._flpSelectAll.WrapContents = false;
            // 
            // _btnSelectNone
            // 
            this._btnSelectNone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnSelectNone.AutoSize = true;
            this._btnSelectNone.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._btnSelectNone.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnSelectNone.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._btnSelectNone.Location = new System.Drawing.Point(562, 8);
            this._btnSelectNone.Margin = new System.Windows.Forms.Padding(0);
            this._btnSelectNone.Name = "_btnSelectNone";
            this._btnSelectNone.Padding = new System.Windows.Forms.Padding(4);
            this._btnSelectNone.Size = new System.Drawing.Size(85, 31);
            this._btnSelectNone.TabIndex = 2;
            this._btnSelectNone.Text = "Select none";
            this._btnSelectNone.UseVisualStyleBackColor = false;
            this._btnSelectNone.Click += new System.EventHandler(this._btnSelectNone_Click);
            // 
            // _btnSelectAll
            // 
            this._btnSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnSelectAll.AutoSize = true;
            this._btnSelectAll.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._btnSelectAll.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnSelectAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._btnSelectAll.Location = new System.Drawing.Point(492, 8);
            this._btnSelectAll.Margin = new System.Windows.Forms.Padding(0);
            this._btnSelectAll.Name = "_btnSelectAll";
            this._btnSelectAll.Padding = new System.Windows.Forms.Padding(4);
            this._btnSelectAll.Size = new System.Drawing.Size(70, 31);
            this._btnSelectAll.TabIndex = 1;
            this._btnSelectAll.Text = "Select all";
            this._btnSelectAll.UseVisualStyleBackColor = false;
            this._btnSelectAll.Click += new System.EventHandler(this._btnSelectAll_Click);
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
            this.ctlTitleBar1.Size = new System.Drawing.Size(663, 87);
            this.ctlTitleBar1.SubText = "Subtitle";
            this.ctlTitleBar1.TabIndex = 6;
            this.ctlTitleBar1.Text = "ctlTitleBar1";
            this.ctlTitleBar1.WarningText = null;
            // 
            // FrmList
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(663, 700);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this._flpSelectAll);
            this.Controls.Add(this.ctlTitleBar1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmList";
            this.tableLayoutPanel1.ResumeLayout(false);
            this._flpSelectAll.ResumeLayout(false);
            this._flpSelectAll.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button2;
        private Controls.CtlTitleBar ctlTitleBar1;
        private System.Windows.Forms.Button _btnEdit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.FlowLayoutPanel _flpSelectAll;
        private System.Windows.Forms.Button _btnSelectNone;
        private System.Windows.Forms.Button _btnSelectAll;

    }
}