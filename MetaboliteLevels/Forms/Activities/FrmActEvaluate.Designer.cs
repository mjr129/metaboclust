namespace MetaboliteLevels.Forms.Algorithms
{
    partial class FrmActEvaluate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmActEvaluate));
            MCharting.Selection selection1 = new MCharting.Selection();
            this.splitContainer1 = new MGui.Controls.CtlSplitter();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this._lstClusters = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this._tlpHeaderClusters = new System.Windows.Forms.TableLayoutPanel();
            this._lstSel = new System.Windows.Forms.ComboBox();
            this.button2 = new MetaboliteLevels.Controls.CtlButton();
            this.label2 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this._lstParams = new System.Windows.Forms.ListView();
            this._tlpHeaderParams = new System.Windows.Forms.TableLayoutPanel();
            this._btnValuesHelp = new MetaboliteLevels.Controls.CtlButton();
            this.label5 = new System.Windows.Forms.Label();
            this._btnViewScript = new MetaboliteLevels.Controls.CtlButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this._tvStatistics = new System.Windows.Forms.TreeView();
            this._lstStatistics = new System.Windows.Forms.ListView();
            this._tlpHeaderStatistics = new System.Windows.Forms.TableLayoutPanel();
            this.button3 = new MetaboliteLevels.Controls.CtlButton();
            this.label3 = new System.Windows.Forms.Label();
            this.button6 = new MetaboliteLevels.Controls.CtlButton();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this._chartParameters = new MCharting.MChart();
            this._tlpHeaderPlot = new System.Windows.Forms.TableLayoutPanel();
            this.button4 = new MetaboliteLevels.Controls.CtlButton();
            this._lblPlot = new System.Windows.Forms.Label();
            this.button7 = new MetaboliteLevels.Controls.CtlButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this._tlpHeaderCluster = new System.Windows.Forms.TableLayoutPanel();
            this._labelCluster = new System.Windows.Forms.Label();
            this.button5 = new MetaboliteLevels.Controls.CtlButton();
            this.button1 = new MetaboliteLevels.Controls.CtlButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._btnNew = new System.Windows.Forms.ToolStripButton();
            this._btnLoad = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.updateResultsDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findInexplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.paranoidModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this._infoLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ctlTitleBar1 = new MetaboliteLevels.Controls.CtlTitleBar();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this._tlpHeaderClusters.SuspendLayout();
            this.panel5.SuspendLayout();
            this._tlpHeaderParams.SuspendLayout();
            this.panel1.SuspendLayout();
            this._tlpHeaderStatistics.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this._tlpHeaderPlot.SuspendLayout();
            this.panel3.SuspendLayout();
            this._tlpHeaderCluster.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 128);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel2);
            this.splitContainer1.Size = new System.Drawing.Size(996, 739);
            this.splitContainer1.SplitterDistance = 366;
            this.splitContainer1.TabIndex = 12;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel5, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(996, 366);
            this.tableLayoutPanel1.TabIndex = 14;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this._lstClusters);
            this.panel2.Controls.Add(this._tlpHeaderClusters);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(332, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(332, 366);
            this.panel2.TabIndex = 8;
            // 
            // _lstClusters
            // 
            this._lstClusters.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lstClusters.LargeImageList = this.imageList1;
            this._lstClusters.Location = new System.Drawing.Point(0, 30);
            this._lstClusters.Name = "_lstClusters";
            this._lstClusters.Size = new System.Drawing.Size(332, 336);
            this._lstClusters.SmallImageList = this.imageList1;
            this._lstClusters.TabIndex = 0;
            this._lstClusters.UseCompatibleStateImageBehavior = false;
            this._lstClusters.View = System.Windows.Forms.View.Details;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(24, 24);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // _tlpHeaderClusters
            // 
            this._tlpHeaderClusters.AutoSize = true;
            this._tlpHeaderClusters.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._tlpHeaderClusters.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this._tlpHeaderClusters.ColumnCount = 3;
            this._tlpHeaderClusters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this._tlpHeaderClusters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._tlpHeaderClusters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this._tlpHeaderClusters.Controls.Add(this._lstSel, 1, 0);
            this._tlpHeaderClusters.Controls.Add(this.button2, 2, 0);
            this._tlpHeaderClusters.Controls.Add(this.label2, 0, 0);
            this._tlpHeaderClusters.Dock = System.Windows.Forms.DockStyle.Top;
            this._tlpHeaderClusters.ForeColor = System.Drawing.Color.Black;
            this._tlpHeaderClusters.Location = new System.Drawing.Point(0, 0);
            this._tlpHeaderClusters.Margin = new System.Windows.Forms.Padding(0);
            this._tlpHeaderClusters.Name = "_tlpHeaderClusters";
            this._tlpHeaderClusters.RowCount = 1;
            this._tlpHeaderClusters.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._tlpHeaderClusters.Size = new System.Drawing.Size(332, 30);
            this._tlpHeaderClusters.TabIndex = 17;
            // 
            // _lstSel
            // 
            this._lstSel.Dock = System.Windows.Forms.DockStyle.Top;
            this._lstSel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstSel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this._lstSel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lstSel.FormattingEnabled = true;
            this._lstSel.Location = new System.Drawing.Point(90, 5);
            this._lstSel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 3);
            this._lstSel.Name = "_lstSel";
            this._lstSel.Size = new System.Drawing.Size(208, 21);
            this._lstSel.TabIndex = 15;
            this._lstSel.SelectedIndexChanged += new System.EventHandler(this._lstSel_SelectedIndexChanged);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Image = global::MetaboliteLevels.Properties.Resources.IcoHelp;
            this.button2.Location = new System.Drawing.Point(302, 0);
            this.button2.Margin = new System.Windows.Forms.Padding(0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(30, 30);
            this.button2.TabIndex = 11;
            this.toolTip1.SetToolTip(this.button2, resources.GetString("button2.ToolTip"));
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button5_Click);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Clusters";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this._lstParams);
            this.panel5.Controls.Add(this._tlpHeaderParams);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Margin = new System.Windows.Forms.Padding(0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(332, 366);
            this.panel5.TabIndex = 10;
            // 
            // _lstParams
            // 
            this._lstParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lstParams.LargeImageList = this.imageList1;
            this._lstParams.Location = new System.Drawing.Point(0, 30);
            this._lstParams.Name = "_lstParams";
            this._lstParams.Size = new System.Drawing.Size(332, 336);
            this._lstParams.SmallImageList = this.imageList1;
            this._lstParams.TabIndex = 0;
            this._lstParams.UseCompatibleStateImageBehavior = false;
            this._lstParams.View = System.Windows.Forms.View.Details;
            // 
            // _tlpHeaderParams
            // 
            this._tlpHeaderParams.AutoSize = true;
            this._tlpHeaderParams.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._tlpHeaderParams.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this._tlpHeaderParams.ColumnCount = 3;
            this._tlpHeaderParams.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this._tlpHeaderParams.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this._tlpHeaderParams.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._tlpHeaderParams.Controls.Add(this._btnValuesHelp, 2, 0);
            this._tlpHeaderParams.Controls.Add(this.label5, 1, 0);
            this._tlpHeaderParams.Controls.Add(this._btnViewScript, 0, 0);
            this._tlpHeaderParams.Dock = System.Windows.Forms.DockStyle.Top;
            this._tlpHeaderParams.ForeColor = System.Drawing.Color.Black;
            this._tlpHeaderParams.Location = new System.Drawing.Point(0, 0);
            this._tlpHeaderParams.Margin = new System.Windows.Forms.Padding(0);
            this._tlpHeaderParams.Name = "_tlpHeaderParams";
            this._tlpHeaderParams.RowCount = 1;
            this._tlpHeaderParams.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._tlpHeaderParams.Size = new System.Drawing.Size(332, 30);
            this._tlpHeaderParams.TabIndex = 16;
            // 
            // _btnValuesHelp
            // 
            this._btnValuesHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnValuesHelp.FlatAppearance.BorderSize = 0;
            this._btnValuesHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnValuesHelp.Image = global::MetaboliteLevels.Properties.Resources.IcoHelp;
            this._btnValuesHelp.Location = new System.Drawing.Point(302, 0);
            this._btnValuesHelp.Margin = new System.Windows.Forms.Padding(0);
            this._btnValuesHelp.Name = "_btnValuesHelp";
            this._btnValuesHelp.Size = new System.Drawing.Size(30, 30);
            this._btnValuesHelp.TabIndex = 11;
            this.toolTip1.SetToolTip(this._btnValuesHelp, "Lists the values of your selected parameter that were tested. Select a value to s" +
        "how the clusters this setting created.");
            this._btnValuesHelp.UseVisualStyleBackColor = false;
            this._btnValuesHelp.Click += new System.EventHandler(this.button5_Click);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(33, 2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(126, 25);
            this.label5.TabIndex = 2;
            this.label5.Text = "Values tested";
            // 
            // _btnViewScript
            // 
            this._btnViewScript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnViewScript.FlatAppearance.BorderSize = 0;
            this._btnViewScript.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnViewScript.Image = global::MetaboliteLevels.Properties.Resources.IconScriptCluster;
            this._btnViewScript.Location = new System.Drawing.Point(0, 0);
            this._btnViewScript.Margin = new System.Windows.Forms.Padding(0);
            this._btnViewScript.Name = "_btnViewScript";
            this._btnViewScript.Size = new System.Drawing.Size(30, 30);
            this._btnViewScript.TabIndex = 11;
            this.toolTip1.SetToolTip(this._btnViewScript, "View configuration");
            this._btnViewScript.UseVisualStyleBackColor = false;
            this._btnViewScript.Click += new System.EventHandler(this._btnViewScript_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._tvStatistics);
            this.panel1.Controls.Add(this._lstStatistics);
            this.panel1.Controls.Add(this._tlpHeaderStatistics);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(664, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(332, 366);
            this.panel1.TabIndex = 11;
            // 
            // _tvStatistics
            // 
            this._tvStatistics.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tvStatistics.ImageIndex = 0;
            this._tvStatistics.ImageList = this.imageList1;
            this._tvStatistics.Location = new System.Drawing.Point(0, 30);
            this._tvStatistics.Name = "_tvStatistics";
            this._tvStatistics.SelectedImageIndex = 0;
            this._tvStatistics.ShowPlusMinus = false;
            this._tvStatistics.Size = new System.Drawing.Size(332, 336);
            this._tvStatistics.TabIndex = 14;
            this._tvStatistics.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this._tvStatistics_NodeMouseDoubleClick);
            // 
            // _lstStatistics
            // 
            this._lstStatistics.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lstStatistics.LargeImageList = this.imageList1;
            this._lstStatistics.Location = new System.Drawing.Point(0, 30);
            this._lstStatistics.Name = "_lstStatistics";
            this._lstStatistics.Size = new System.Drawing.Size(332, 336);
            this._lstStatistics.SmallImageList = this.imageList1;
            this._lstStatistics.TabIndex = 18;
            this._lstStatistics.UseCompatibleStateImageBehavior = false;
            this._lstStatistics.View = System.Windows.Forms.View.Details;
            // 
            // _tlpHeaderStatistics
            // 
            this._tlpHeaderStatistics.AutoSize = true;
            this._tlpHeaderStatistics.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._tlpHeaderStatistics.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this._tlpHeaderStatistics.ColumnCount = 3;
            this._tlpHeaderStatistics.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this._tlpHeaderStatistics.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this._tlpHeaderStatistics.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._tlpHeaderStatistics.Controls.Add(this.button3, 2, 0);
            this._tlpHeaderStatistics.Controls.Add(this.label3, 1, 0);
            this._tlpHeaderStatistics.Controls.Add(this.button6, 0, 0);
            this._tlpHeaderStatistics.Dock = System.Windows.Forms.DockStyle.Top;
            this._tlpHeaderStatistics.ForeColor = System.Drawing.Color.Black;
            this._tlpHeaderStatistics.Location = new System.Drawing.Point(0, 0);
            this._tlpHeaderStatistics.Margin = new System.Windows.Forms.Padding(0);
            this._tlpHeaderStatistics.Name = "_tlpHeaderStatistics";
            this._tlpHeaderStatistics.RowCount = 1;
            this._tlpHeaderStatistics.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._tlpHeaderStatistics.Size = new System.Drawing.Size(332, 30);
            this._tlpHeaderStatistics.TabIndex = 17;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Image = global::MetaboliteLevels.Properties.Resources.IcoHelp;
            this.button3.Location = new System.Drawing.Point(302, 0);
            this.button3.Margin = new System.Windows.Forms.Padding(0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(30, 30);
            this.button3.TabIndex = 11;
            this.toolTip1.SetToolTip(this.button3, resources.GetString("button3.ToolTip"));
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button5_Click);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(33, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 25);
            this.label3.TabIndex = 3;
            this.label3.Text = "Statistics";
            // 
            // button6
            // 
            this.button6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button6.FlatAppearance.BorderSize = 0;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.ForeColor = System.Drawing.Color.Black;
            this.button6.Location = new System.Drawing.Point(0, 0);
            this.button6.Margin = new System.Windows.Forms.Padding(0);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(30, 30);
            this.button6.TabIndex = 14;
            this.button6.Text = "Ξ";
            this.toolTip1.SetToolTip(this.button6, "Toggle tree/list");
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel3, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(996, 367);
            this.tableLayoutPanel2.TabIndex = 10;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this._chartParameters);
            this.panel4.Controls.Add(this._tlpHeaderPlot);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(498, 367);
            this.panel4.TabIndex = 9;
            // 
            // _chartParameters
            // 
            this._chartParameters.Dock = System.Windows.Forms.DockStyle.Fill;
            this._chartParameters.Location = new System.Drawing.Point(0, 30);
            this._chartParameters.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this._chartParameters.Name = "_chartParameters";
            this._chartParameters.SelectedItem = selection1;
            this._chartParameters.Size = new System.Drawing.Size(498, 337);
            this._chartParameters.TabIndex = 2;
            // 
            // _tlpHeaderPlot
            // 
            this._tlpHeaderPlot.AutoSize = true;
            this._tlpHeaderPlot.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._tlpHeaderPlot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this._tlpHeaderPlot.ColumnCount = 3;
            this._tlpHeaderPlot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this._tlpHeaderPlot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this._tlpHeaderPlot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._tlpHeaderPlot.Controls.Add(this.button4, 2, 0);
            this._tlpHeaderPlot.Controls.Add(this._lblPlot, 1, 0);
            this._tlpHeaderPlot.Controls.Add(this.button7, 0, 0);
            this._tlpHeaderPlot.Dock = System.Windows.Forms.DockStyle.Top;
            this._tlpHeaderPlot.ForeColor = System.Drawing.Color.Black;
            this._tlpHeaderPlot.Location = new System.Drawing.Point(0, 0);
            this._tlpHeaderPlot.Margin = new System.Windows.Forms.Padding(0);
            this._tlpHeaderPlot.Name = "_tlpHeaderPlot";
            this._tlpHeaderPlot.RowCount = 1;
            this._tlpHeaderPlot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._tlpHeaderPlot.Size = new System.Drawing.Size(498, 30);
            this._tlpHeaderPlot.TabIndex = 15;
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Image = global::MetaboliteLevels.Properties.Resources.IcoHelp;
            this.button4.Location = new System.Drawing.Point(468, 0);
            this.button4.Margin = new System.Windows.Forms.Padding(0);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(30, 30);
            this.button4.TabIndex = 11;
            this.toolTip1.SetToolTip(this.button4, "Shows the plot of changing parameter values. Create this plot by selecting a stat" +
        "istic from the statistics list.");
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button5_Click);
            // 
            // _lblPlot
            // 
            this._lblPlot.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._lblPlot.AutoSize = true;
            this._lblPlot.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblPlot.Location = new System.Drawing.Point(33, 2);
            this._lblPlot.Name = "_lblPlot";
            this._lblPlot.Size = new System.Drawing.Size(46, 25);
            this._lblPlot.TabIndex = 2;
            this._lblPlot.Text = "Plot";
            // 
            // button7
            // 
            this.button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button7.FlatAppearance.BorderSize = 0;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Image = global::MetaboliteLevels.Properties.Resources.IconGraph;
            this.button7.Location = new System.Drawing.Point(0, 0);
            this.button7.Margin = new System.Windows.Forms.Padding(0);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(30, 30);
            this.button7.TabIndex = 11;
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Controls.Add(this._tlpHeaderCluster);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(498, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(498, 367);
            this.panel3.TabIndex = 14;
            // 
            // panel6
            // 
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 30);
            this.panel6.Margin = new System.Windows.Forms.Padding(0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(498, 337);
            this.panel6.TabIndex = 18;
            // 
            // _tlpHeaderCluster
            // 
            this._tlpHeaderCluster.AutoSize = true;
            this._tlpHeaderCluster.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._tlpHeaderCluster.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this._tlpHeaderCluster.ColumnCount = 3;
            this._tlpHeaderCluster.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this._tlpHeaderCluster.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this._tlpHeaderCluster.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._tlpHeaderCluster.Controls.Add(this._labelCluster, 1, 0);
            this._tlpHeaderCluster.Controls.Add(this.button5, 2, 0);
            this._tlpHeaderCluster.Controls.Add(this.button1, 0, 0);
            this._tlpHeaderCluster.Dock = System.Windows.Forms.DockStyle.Top;
            this._tlpHeaderCluster.ForeColor = System.Drawing.Color.Black;
            this._tlpHeaderCluster.Location = new System.Drawing.Point(0, 0);
            this._tlpHeaderCluster.Margin = new System.Windows.Forms.Padding(0);
            this._tlpHeaderCluster.Name = "_tlpHeaderCluster";
            this._tlpHeaderCluster.RowCount = 1;
            this._tlpHeaderCluster.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._tlpHeaderCluster.Size = new System.Drawing.Size(498, 30);
            this._tlpHeaderCluster.TabIndex = 17;
            // 
            // _labelCluster
            // 
            this._labelCluster.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._labelCluster.AutoSize = true;
            this._labelCluster.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelCluster.Location = new System.Drawing.Point(33, 2);
            this._labelCluster.Name = "_labelCluster";
            this._labelCluster.Size = new System.Drawing.Size(72, 25);
            this._labelCluster.TabIndex = 3;
            this._labelCluster.Text = "Cluster";
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Image = global::MetaboliteLevels.Properties.Resources.IcoHelp;
            this.button5.Location = new System.Drawing.Point(468, 0);
            this.button5.Margin = new System.Windows.Forms.Padding(0);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(30, 30);
            this.button5.TabIndex = 11;
            this.toolTip1.SetToolTip(this.button5, "Shows a plot of a cluster. Create this plot by selecting a cluster from the clust" +
        "ers list.");
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::MetaboliteLevels.Properties.Resources.IconGraph;
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(30, 30);
            this.button1.TabIndex = 11;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.White;
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._btnNew,
            this._btnLoad,
            this.toolStripDropDownButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 87);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.toolStrip1.Size = new System.Drawing.Size(996, 41);
            this.toolStrip1.TabIndex = 13;
            // 
            // _btnNew
            // 
            this._btnNew.ForeColor = System.Drawing.Color.Purple;
            this._btnNew.Image = global::MetaboliteLevels.Properties.Resources.MnuNewEvaluation;
            this._btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnNew.Margin = new System.Windows.Forms.Padding(4);
            this._btnNew.Name = "_btnNew";
            this._btnNew.Size = new System.Drawing.Size(65, 25);
            this._btnNew.Text = "Tests";
            this._btnNew.Click += new System.EventHandler(this._btnNewTest_Click);
            // 
            // _btnLoad
            // 
            this._btnLoad.ForeColor = System.Drawing.Color.Purple;
            this._btnLoad.Image = ((System.Drawing.Image)(resources.GetObject("_btnLoad.Image")));
            this._btnLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnLoad.Margin = new System.Windows.Forms.Padding(4);
            this._btnLoad.Name = "_btnLoad";
            this._btnLoad.Size = new System.Drawing.Size(71, 25);
            this._btnLoad.Text = "Select";
            this._btnLoad.Click += new System.EventHandler(this._btnLoad_Click_1);
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.toolStripMenuItem1,
            this.updateResultsDataToolStripMenuItem,
            this.findInexplorerToolStripMenuItem,
            this.toolStripMenuItem2,
            this.paranoidModeToolStripMenuItem});
            this.toolStripDropDownButton2.ForeColor = System.Drawing.Color.Purple;
            this.toolStripDropDownButton2.Image = global::MetaboliteLevels.Properties.Resources.IconSave;
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Margin = new System.Windows.Forms.Padding(4);
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(76, 25);
            this.toolStripDropDownButton2.Text = "More";
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Image = global::MetaboliteLevels.Properties.Resources.MnuOpen;
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(197, 26);
            this.importToolStripMenuItem.Text = "&Import...";
            this.importToolStripMenuItem.Click += new System.EventHandler(this._btnImport_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Image = global::MetaboliteLevels.Properties.Resources.MnuSave;
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(197, 26);
            this.exportToolStripMenuItem.Text = "&Export...";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this._btnSave_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(194, 6);
            // 
            // updateResultsDataToolStripMenuItem
            // 
            this.updateResultsDataToolStripMenuItem.Name = "updateResultsDataToolStripMenuItem";
            this.updateResultsDataToolStripMenuItem.Size = new System.Drawing.Size(197, 26);
            this.updateResultsDataToolStripMenuItem.Text = "Batch process...";
            this.updateResultsDataToolStripMenuItem.Click += new System.EventHandler(this.updateResultsDataToolStripMenuItem_Click);
            // 
            // findInexplorerToolStripMenuItem
            // 
            this.findInexplorerToolStripMenuItem.Image = global::MetaboliteLevels.Properties.Resources.MnuOpenColour;
            this.findInexplorerToolStripMenuItem.Name = "findInexplorerToolStripMenuItem";
            this.findInexplorerToolStripMenuItem.Size = new System.Drawing.Size(197, 26);
            this.findInexplorerToolStripMenuItem.Text = "Find in &explorer...";
            this.findInexplorerToolStripMenuItem.Click += new System.EventHandler(this.findInexplorerToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(194, 6);
            // 
            // paranoidModeToolStripMenuItem
            // 
            this.paranoidModeToolStripMenuItem.Checked = true;
            this.paranoidModeToolStripMenuItem.CheckOnClick = true;
            this.paranoidModeToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.paranoidModeToolStripMenuItem.Image = global::MetaboliteLevels.Properties.Resources.MnuSaveAll;
            this.paranoidModeToolStripMenuItem.Name = "paranoidModeToolStripMenuItem";
            this.paranoidModeToolStripMenuItem.Size = new System.Drawing.Size(197, 26);
            this.paranoidModeToolStripMenuItem.Text = "&Paranoid mode";
            this.paranoidModeToolStripMenuItem.ToolTipText = resources.GetString("paranoidModeToolStripMenuItem.ToolTipText");
            // 
            // toolTip1
            // 
            this.toolTip1.Active = false;
            this.toolTip1.AutomaticDelay = 10000;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "Information";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToClipboardToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(170, 26);
            // 
            // copyToClipboardToolStripMenuItem
            // 
            this.copyToClipboardToolStripMenuItem.Name = "copyToClipboardToolStripMenuItem";
            this.copyToClipboardToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.copyToClipboardToolStripMenuItem.Text = "&Copy to clipboard";
            this.copyToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copyToClipboardToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._infoLabel,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 867);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(996, 22);
            this.statusStrip1.TabIndex = 14;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // _infoLabel
            // 
            this._infoLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this._infoLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._infoLabel.Name = "_infoLabel";
            this._infoLabel.Size = new System.Drawing.Size(158, 17);
            this._infoLabel.Text = "Create or load a test to begin";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(823, 17);
            this.toolStripStatusLabel1.Spring = true;
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
            this.ctlTitleBar1.Size = new System.Drawing.Size(996, 87);
            this.ctlTitleBar1.SubText = "Select a test to begin";
            this.ctlTitleBar1.TabIndex = 15;
            this.ctlTitleBar1.Text = "Evaluate clustering";
            this.ctlTitleBar1.WarningText = null;
            // 
            // FrmActEvaluate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(996, 889);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.ctlTitleBar1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmActEvaluate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Evaluate Clustering";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this._tlpHeaderClusters.ResumeLayout(false);
            this._tlpHeaderClusters.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this._tlpHeaderParams.ResumeLayout(false);
            this._tlpHeaderParams.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this._tlpHeaderStatistics.ResumeLayout(false);
            this._tlpHeaderStatistics.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this._tlpHeaderPlot.ResumeLayout(false);
            this._tlpHeaderPlot.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this._tlpHeaderCluster.ResumeLayout(false);
            this._tlpHeaderCluster.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MGui.Controls.CtlSplitter splitContainer1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ListView _lstParams;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListView _lstClusters;
        private System.Windows.Forms.Panel panel4;
        private MCharting.MChart _chartParameters;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TableLayoutPanel _tlpHeaderParams;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TableLayoutPanel _tlpHeaderClusters;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel _tlpHeaderStatistics;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel _tlpHeaderPlot;
        private System.Windows.Forms.Label _lblPlot;
        private System.Windows.Forms.TableLayoutPanel _tlpHeaderCluster;
        private Controls.CtlButton button1;
        private System.Windows.Forms.Label _labelCluster;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel3;
        private Controls.CtlButton button2;
        private System.Windows.Forms.ToolTip toolTip1;
        private Controls.CtlButton _btnValuesHelp;
        private Controls.CtlButton button3;
        private Controls.CtlButton button4;
        private Controls.CtlButton button5;
        private System.Windows.Forms.TreeView _tvStatistics;
        private System.Windows.Forms.ListView _lstStatistics;
        private Controls.CtlButton button6;
        private Controls.CtlButton button7;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem copyToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton _btnNew;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel _infoLabel;
        private Controls.CtlButton _btnViewScript;
        private System.Windows.Forms.ToolStripButton _btnLoad;
        private System.Windows.Forms.ComboBox _lstSel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem paranoidModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateResultsDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem findInexplorerToolStripMenuItem;
        private Controls.CtlTitleBar ctlTitleBar1;
    }
}