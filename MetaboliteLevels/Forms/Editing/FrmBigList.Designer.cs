namespace MetaboliteLevels.Forms.Editing
{
    partial class FrmBigList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBigList));
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._btnOk = new MetaboliteLevels.Controls.CtlButton();
            this._btnCancel = new MetaboliteLevels.Controls.CtlButton();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this._btnAdd = new MetaboliteLevels.Controls.CtlButton();
            this._btnRemove = new MetaboliteLevels.Controls.CtlButton();
            this._btnView = new MetaboliteLevels.Controls.CtlButton();
            this._btnEdit = new MetaboliteLevels.Controls.CtlButton();
            this._btnRename = new MetaboliteLevels.Controls.CtlButton();
            this._btnUp = new MetaboliteLevels.Controls.CtlButton();
            this._btnDown = new MetaboliteLevels.Controls.CtlButton();
            this._btnEnableDisable = new MetaboliteLevels.Controls.CtlButton();
            this.ctlTitleBar1 = new MetaboliteLevels.Controls.CtlTitleBar();
            this._btnDuplicate = new MetaboliteLevels.Controls.CtlButton();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.listView1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel2, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 87);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(960, 651);
            this.tableLayoutPanel2.TabIndex = 23;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.LargeImageList = this.imageList1;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Margin = new System.Windows.Forms.Padding(0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(810, 595);
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ItemActivate += new System.EventHandler(this.listView1_ItemActivate);
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 480;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Algorithm";
            this.columnHeader2.Width = 0;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Configuration";
            this.columnHeader3.Width = 0;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.SetColumnSpan(this.flowLayoutPanel1, 2);
            this.flowLayoutPanel1.Controls.Add(this._btnOk);
            this.flowLayoutPanel1.Controls.Add(this._btnCancel);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(672, 595);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(288, 56);
            this.flowLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // _btnOk
            // 
            this._btnOk.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnOk.Image = ((System.Drawing.Image)(resources.GetObject("_btnOk.Image")));
            this._btnOk.Location = new System.Drawing.Point(8, 8);
            this._btnOk.Margin = new System.Windows.Forms.Padding(8);
            this._btnOk.Name = "_btnOk";
            this._btnOk.Size = new System.Drawing.Size(128, 40);
            this._btnOk.TabIndex = 2;
            this._btnOk.Text = "OK";
            this._btnOk.UseDefaultSize = true;
            this._btnOk.UseVisualStyleBackColor = true;
            this._btnOk.Click += new System.EventHandler(this._btnOk_Click);
            // 
            // _btnCancel
            // 
            this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnCancel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("_btnCancel.Image")));
            this._btnCancel.Location = new System.Drawing.Point(152, 8);
            this._btnCancel.Margin = new System.Windows.Forms.Padding(8);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(128, 40);
            this._btnCancel.TabIndex = 3;
            this._btnCancel.Text = "Cancel";
            this._btnCancel.UseDefaultSize = true;
            this._btnCancel.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel2.Controls.Add(this._btnAdd);
            this.flowLayoutPanel2.Controls.Add(this._btnDuplicate);
            this.flowLayoutPanel2.Controls.Add(this._btnRemove);
            this.flowLayoutPanel2.Controls.Add(this._btnView);
            this.flowLayoutPanel2.Controls.Add(this._btnEdit);
            this.flowLayoutPanel2.Controls.Add(this._btnRename);
            this.flowLayoutPanel2.Controls.Add(this._btnUp);
            this.flowLayoutPanel2.Controls.Add(this._btnDown);
            this.flowLayoutPanel2.Controls.Add(this._btnEnableDisable);
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(813, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(144, 400);
            this.flowLayoutPanel2.TabIndex = 3;
            this.flowLayoutPanel2.WrapContents = false;
            // 
            // _btnAdd
            // 
            this._btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this._btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("_btnAdd.Image")));
            this._btnAdd.Location = new System.Drawing.Point(8, 8);
            this._btnAdd.Margin = new System.Windows.Forms.Padding(8, 8, 8, 0);
            this._btnAdd.Name = "_btnAdd";
            this._btnAdd.Size = new System.Drawing.Size(128, 40);
            this._btnAdd.TabIndex = 1;
            this._btnAdd.Text = "New";
            this._btnAdd.UseDefaultSize = true;
            this._btnAdd.UseVisualStyleBackColor = false;
            this._btnAdd.Click += new System.EventHandler(this._btnAdd_Click);
            // 
            // _btnRemove
            // 
            this._btnRemove.Enabled = false;
            this._btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this._btnRemove.Image = ((System.Drawing.Image)(resources.GetObject("_btnRemove.Image")));
            this._btnRemove.Location = new System.Drawing.Point(8, 88);
            this._btnRemove.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this._btnRemove.Name = "_btnRemove";
            this._btnRemove.Size = new System.Drawing.Size(128, 40);
            this._btnRemove.TabIndex = 1;
            this._btnRemove.Text = "Remove";
            this._btnRemove.UseDefaultSize = true;
            this._btnRemove.UseVisualStyleBackColor = false;
            this._btnRemove.Click += new System.EventHandler(this._btnRemove_Click);
            // 
            // _btnView
            // 
            this._btnView.Enabled = false;
            this._btnView.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this._btnView.Image = ((System.Drawing.Image)(resources.GetObject("_btnView.Image")));
            this._btnView.Location = new System.Drawing.Point(8, 136);
            this._btnView.Margin = new System.Windows.Forms.Padding(8, 8, 8, 0);
            this._btnView.Name = "_btnView";
            this._btnView.Size = new System.Drawing.Size(128, 40);
            this._btnView.TabIndex = 1;
            this._btnView.Text = "View";
            this._btnView.UseDefaultSize = true;
            this._btnView.UseVisualStyleBackColor = false;
            this._btnView.Click += new System.EventHandler(this._btnView_Click);
            // 
            // _btnEdit
            // 
            this._btnEdit.Enabled = false;
            this._btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this._btnEdit.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("_btnEdit.Image")));
            this._btnEdit.Location = new System.Drawing.Point(8, 176);
            this._btnEdit.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this._btnEdit.Name = "_btnEdit";
            this._btnEdit.Size = new System.Drawing.Size(128, 40);
            this._btnEdit.TabIndex = 1;
            this._btnEdit.Text = "Edit";
            this._btnEdit.UseDefaultSize = true;
            this._btnEdit.UseVisualStyleBackColor = false;
            this._btnEdit.Click += new System.EventHandler(this._btnEdit_Click);
            // 
            // _btnRename
            // 
            this._btnRename.Enabled = false;
            this._btnRename.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this._btnRename.Image = ((System.Drawing.Image)(resources.GetObject("_btnRename.Image")));
            this._btnRename.Location = new System.Drawing.Point(8, 216);
            this._btnRename.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this._btnRename.Name = "_btnRename";
            this._btnRename.Size = new System.Drawing.Size(128, 40);
            this._btnRename.TabIndex = 2;
            this._btnRename.Text = "Rename";
            this._btnRename.UseDefaultSize = true;
            this._btnRename.UseVisualStyleBackColor = false;
            this._btnRename.Click += new System.EventHandler(this._btnRename_Click);
            // 
            // _btnUp
            // 
            this._btnUp.Enabled = false;
            this._btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this._btnUp.Image = ((System.Drawing.Image)(resources.GetObject("_btnUp.Image")));
            this._btnUp.Location = new System.Drawing.Point(8, 264);
            this._btnUp.Margin = new System.Windows.Forms.Padding(8, 8, 8, 0);
            this._btnUp.Name = "_btnUp";
            this._btnUp.Size = new System.Drawing.Size(128, 40);
            this._btnUp.TabIndex = 3;
            this._btnUp.Text = "Up";
            this._btnUp.UseDefaultSize = true;
            this._btnUp.UseVisualStyleBackColor = false;
            this._btnUp.Click += new System.EventHandler(this._btnUp_Click);
            // 
            // _btnDown
            // 
            this._btnDown.Enabled = false;
            this._btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this._btnDown.Image = ((System.Drawing.Image)(resources.GetObject("_btnDown.Image")));
            this._btnDown.Location = new System.Drawing.Point(8, 304);
            this._btnDown.Margin = new System.Windows.Forms.Padding(8, 0, 8, 8);
            this._btnDown.Name = "_btnDown";
            this._btnDown.Size = new System.Drawing.Size(128, 40);
            this._btnDown.TabIndex = 4;
            this._btnDown.Text = "Down";
            this._btnDown.UseDefaultSize = true;
            this._btnDown.UseVisualStyleBackColor = false;
            this._btnDown.Click += new System.EventHandler(this._btnDown_Click);
            // 
            // _btnEnableDisable
            // 
            this._btnEnableDisable.Enabled = false;
            this._btnEnableDisable.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this._btnEnableDisable.Image = ((System.Drawing.Image)(resources.GetObject("_btnEnableDisable.Image")));
            this._btnEnableDisable.Location = new System.Drawing.Point(8, 352);
            this._btnEnableDisable.Margin = new System.Windows.Forms.Padding(8, 0, 8, 8);
            this._btnEnableDisable.Name = "_btnEnableDisable";
            this._btnEnableDisable.Size = new System.Drawing.Size(128, 40);
            this._btnEnableDisable.TabIndex = 5;
            this._btnEnableDisable.Text = "Disable";
            this._btnEnableDisable.UseDefaultSize = true;
            this._btnEnableDisable.UseVisualStyleBackColor = false;
            this._btnEnableDisable.Click += new System.EventHandler(this._btnEnableDisable_Click);
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
            this.ctlTitleBar1.Size = new System.Drawing.Size(960, 87);
            this.ctlTitleBar1.SubText = "And here";
            this.ctlTitleBar1.TabIndex = 24;
            this.ctlTitleBar1.Text = "Text goes here";
            this.ctlTitleBar1.WarningText = null;
            // 
            // _btnDuplicate
            // 
            this._btnDuplicate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this._btnDuplicate.Image = ((System.Drawing.Image)(resources.GetObject("_btnDuplicate.Image")));
            this._btnDuplicate.Location = new System.Drawing.Point(8, 48);
            this._btnDuplicate.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this._btnDuplicate.Name = "_btnDuplicate";
            this._btnDuplicate.Size = new System.Drawing.Size(128, 40);
            this._btnDuplicate.TabIndex = 6;
            this._btnDuplicate.Text = "Duplicate";
            this._btnDuplicate.UseDefaultSize = true;
            this._btnDuplicate.UseVisualStyleBackColor = false;
            this._btnDuplicate.Click += new System.EventHandler(this._btnDuplicate_Click);
            // 
            // FrmStatisticsList
            // 
            this.AcceptButton = this._btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._btnCancel;
            this.ClientSize = new System.Drawing.Size(960, 738);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.ctlTitleBar1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmStatisticsList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Title goes here";
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private MetaboliteLevels.Controls.CtlButton _btnOk;
        private Controls.CtlTitleBar ctlTitleBar1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private MetaboliteLevels.Controls.CtlButton _btnAdd;
        private MetaboliteLevels.Controls.CtlButton _btnView;
        private MetaboliteLevels.Controls.CtlButton _btnEdit;
        private MetaboliteLevels.Controls.CtlButton _btnRemove;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private MetaboliteLevels.Controls.CtlButton _btnRename;
        private MetaboliteLevels.Controls.CtlButton _btnCancel;
        private Controls.CtlButton _btnUp;
        private Controls.CtlButton _btnDown;
        private Controls.CtlButton _btnEnableDisable;
        private System.Windows.Forms.ImageList imageList1;
        private Controls.CtlButton _btnDuplicate;
    }
}