namespace MetaboliteLevels.Forms.Algorithms
{
    partial class FrmEvaluateClustering
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEvaluateClustering));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this._lstClusters = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this._tlpHeaderClusters = new System.Windows.Forms.TableLayoutPanel();
            this._lstSel = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this._lstParams = new System.Windows.Forms.ListView();
            this._tlpHeaderParams = new System.Windows.Forms.TableLayoutPanel();
            this._btnValuesHelp = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this._btnViewScript = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this._tvStatistics = new System.Windows.Forms.TreeView();
            this._lstStatistics = new System.Windows.Forms.ListView();
            this._tlpHeaderStatistics = new System.Windows.Forms.TableLayoutPanel();
            this.button3 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this._chartParameters = new MCharting.MChart();
            this._tlpHeaderPlot = new System.Windows.Forms.TableLayoutPanel();
            this.button4 = new System.Windows.Forms.Button();
            this._lblPlot = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this._tlpHeaderCluster = new System.Windows.Forms.TableLayoutPanel();
            this._labelCluster = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this._btnNew = new System.Windows.Forms.ToolStripButton();
            this._btnLoad = new System.Windows.Forms.ToolStripButton();
            this._btnImport = new System.Windows.Forms.ToolStripButton();
            this._btnExport = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this._infoLabel = new System.Windows.Forms.ToolStripStatusLabel();
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
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this._tlpHeaderCluster.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 73);
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
            this.splitContainer1.Size = new System.Drawing.Size(996, 794);
            this.splitContainer1.SplitterDistance = 396;
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(996, 396);
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
            this.panel2.Size = new System.Drawing.Size(332, 396);
            this.panel2.TabIndex = 8;
            // 
            // _lstClusters
            // 
            this._lstClusters.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lstClusters.LargeImageList = this.imageList1;
            this._lstClusters.Location = new System.Drawing.Point(0, 30);
            this._lstClusters.Name = "_lstClusters";
            this._lstClusters.Size = new System.Drawing.Size(332, 366);
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
            this._tlpHeaderClusters.BackColor = System.Drawing.Color.CornflowerBlue;
            this._tlpHeaderClusters.ColumnCount = 3;
            this._tlpHeaderClusters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this._tlpHeaderClusters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._tlpHeaderClusters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this._tlpHeaderClusters.Controls.Add(this._lstSel, 1, 0);
            this._tlpHeaderClusters.Controls.Add(this.button2, 2, 0);
            this._tlpHeaderClusters.Controls.Add(this.label2, 0, 0);
            this._tlpHeaderClusters.Dock = System.Windows.Forms.DockStyle.Top;
            this._tlpHeaderClusters.ForeColor = System.Drawing.Color.White;
            this._tlpHeaderClusters.Location = new System.Drawing.Point(0, 0);
            this._tlpHeaderClusters.Name = "_tlpHeaderClusters";
            this._tlpHeaderClusters.RowCount = 1;
            this._tlpHeaderClusters.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._tlpHeaderClusters.Size = new System.Drawing.Size(332, 30);
            this._tlpHeaderClusters.TabIndex = 17;
            // 
            // _lstSel
            // 
            this._lstSel.BackColor = System.Drawing.Color.CornflowerBlue;
            this._lstSel.Dock = System.Windows.Forms.DockStyle.Top;
            this._lstSel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstSel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this._lstSel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lstSel.ForeColor = System.Drawing.Color.White;
            this._lstSel.FormattingEnabled = true;
            this._lstSel.Location = new System.Drawing.Point(98, 5);
            this._lstSel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 3);
            this._lstSel.Name = "_lstSel";
            this._lstSel.Size = new System.Drawing.Size(200, 21);
            this._lstSel.TabIndex = 15;
            this._lstSel.SelectedIndexChanged += new System.EventHandler(this._lstSel_SelectedIndexChanged);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.Color.CornflowerBlue;
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
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 30);
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
            this.panel5.Size = new System.Drawing.Size(332, 396);
            this.panel5.TabIndex = 10;
            // 
            // _lstParams
            // 
            this._lstParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lstParams.LargeImageList = this.imageList1;
            this._lstParams.Location = new System.Drawing.Point(0, 30);
            this._lstParams.Name = "_lstParams";
            this._lstParams.Size = new System.Drawing.Size(332, 366);
            this._lstParams.SmallImageList = this.imageList1;
            this._lstParams.TabIndex = 0;
            this._lstParams.UseCompatibleStateImageBehavior = false;
            this._lstParams.View = System.Windows.Forms.View.Details;
            // 
            // _tlpHeaderParams
            // 
            this._tlpHeaderParams.AutoSize = true;
            this._tlpHeaderParams.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._tlpHeaderParams.BackColor = System.Drawing.Color.CornflowerBlue;
            this._tlpHeaderParams.ColumnCount = 3;
            this._tlpHeaderParams.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this._tlpHeaderParams.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._tlpHeaderParams.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this._tlpHeaderParams.Controls.Add(this._btnValuesHelp, 1, 0);
            this._tlpHeaderParams.Controls.Add(this.label5, 0, 0);
            this._tlpHeaderParams.Controls.Add(this._btnViewScript, 2, 0);
            this._tlpHeaderParams.Dock = System.Windows.Forms.DockStyle.Top;
            this._tlpHeaderParams.ForeColor = System.Drawing.Color.White;
            this._tlpHeaderParams.Location = new System.Drawing.Point(0, 0);
            this._tlpHeaderParams.Name = "_tlpHeaderParams";
            this._tlpHeaderParams.RowCount = 1;
            this._tlpHeaderParams.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._tlpHeaderParams.Size = new System.Drawing.Size(332, 30);
            this._tlpHeaderParams.TabIndex = 16;
            // 
            // _btnValuesHelp
            // 
            this._btnValuesHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnValuesHelp.BackColor = System.Drawing.Color.CornflowerBlue;
            this._btnValuesHelp.FlatAppearance.BorderSize = 0;
            this._btnValuesHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnValuesHelp.Image = global::MetaboliteLevels.Properties.Resources.IcoHelp;
            this._btnValuesHelp.Location = new System.Drawing.Point(272, 0);
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
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(140, 30);
            this.label5.TabIndex = 2;
            this.label5.Text = "Values tested";
            // 
            // _btnViewScript
            // 
            this._btnViewScript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnViewScript.BackColor = System.Drawing.Color.CornflowerBlue;
            this._btnViewScript.FlatAppearance.BorderSize = 0;
            this._btnViewScript.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnViewScript.Image = global::MetaboliteLevels.Properties.Resources.ObjLScriptCluster;
            this._btnViewScript.Location = new System.Drawing.Point(302, 0);
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
            this.panel1.Size = new System.Drawing.Size(332, 396);
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
            this._tvStatistics.Size = new System.Drawing.Size(332, 366);
            this._tvStatistics.TabIndex = 14;
            this._tvStatistics.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this._tvStatistics_NodeMouseDoubleClick);
            // 
            // _lstStatistics
            // 
            this._lstStatistics.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lstStatistics.LargeImageList = this.imageList1;
            this._lstStatistics.Location = new System.Drawing.Point(0, 30);
            this._lstStatistics.Name = "_lstStatistics";
            this._lstStatistics.Size = new System.Drawing.Size(332, 366);
            this._lstStatistics.SmallImageList = this.imageList1;
            this._lstStatistics.TabIndex = 18;
            this._lstStatistics.UseCompatibleStateImageBehavior = false;
            this._lstStatistics.View = System.Windows.Forms.View.Details;
            // 
            // _tlpHeaderStatistics
            // 
            this._tlpHeaderStatistics.AutoSize = true;
            this._tlpHeaderStatistics.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._tlpHeaderStatistics.BackColor = System.Drawing.Color.CornflowerBlue;
            this._tlpHeaderStatistics.ColumnCount = 3;
            this._tlpHeaderStatistics.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this._tlpHeaderStatistics.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._tlpHeaderStatistics.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this._tlpHeaderStatistics.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this._tlpHeaderStatistics.Controls.Add(this.button3, 1, 0);
            this._tlpHeaderStatistics.Controls.Add(this.label3, 0, 0);
            this._tlpHeaderStatistics.Controls.Add(this.button6, 2, 0);
            this._tlpHeaderStatistics.Dock = System.Windows.Forms.DockStyle.Top;
            this._tlpHeaderStatistics.ForeColor = System.Drawing.Color.White;
            this._tlpHeaderStatistics.Location = new System.Drawing.Point(0, 0);
            this._tlpHeaderStatistics.Name = "_tlpHeaderStatistics";
            this._tlpHeaderStatistics.RowCount = 1;
            this._tlpHeaderStatistics.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._tlpHeaderStatistics.Size = new System.Drawing.Size(332, 30);
            this._tlpHeaderStatistics.TabIndex = 17;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.BackColor = System.Drawing.Color.CornflowerBlue;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Image = global::MetaboliteLevels.Properties.Resources.IcoHelp;
            this.button3.Location = new System.Drawing.Point(272, 0);
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
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 30);
            this.label3.TabIndex = 3;
            this.label3.Text = "Statistics";
            // 
            // button6
            // 
            this.button6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button6.FlatAppearance.BorderSize = 0;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.Location = new System.Drawing.Point(302, 0);
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
            this.tableLayoutPanel2.Size = new System.Drawing.Size(996, 394);
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
            this.panel4.Size = new System.Drawing.Size(498, 394);
            this.panel4.TabIndex = 9;
            // 
            // _chartParameters
            // 
            this._chartParameters.Dock = System.Windows.Forms.DockStyle.Fill;
            this._chartParameters.Location = new System.Drawing.Point(0, 30);
            this._chartParameters.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this._chartParameters.Name = "_chartParameters";
            this._chartParameters.SelectedItem = null;
            this._chartParameters.Size = new System.Drawing.Size(498, 364);
            this._chartParameters.TabIndex = 2;
            // 
            // _tlpHeaderPlot
            // 
            this._tlpHeaderPlot.AutoSize = true;
            this._tlpHeaderPlot.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._tlpHeaderPlot.BackColor = System.Drawing.Color.CornflowerBlue;
            this._tlpHeaderPlot.ColumnCount = 3;
            this._tlpHeaderPlot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this._tlpHeaderPlot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._tlpHeaderPlot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this._tlpHeaderPlot.Controls.Add(this.button4, 1, 0);
            this._tlpHeaderPlot.Controls.Add(this._lblPlot, 0, 0);
            this._tlpHeaderPlot.Controls.Add(this.button7, 2, 0);
            this._tlpHeaderPlot.Dock = System.Windows.Forms.DockStyle.Top;
            this._tlpHeaderPlot.ForeColor = System.Drawing.Color.White;
            this._tlpHeaderPlot.Location = new System.Drawing.Point(0, 0);
            this._tlpHeaderPlot.Name = "_tlpHeaderPlot";
            this._tlpHeaderPlot.RowCount = 1;
            this._tlpHeaderPlot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._tlpHeaderPlot.Size = new System.Drawing.Size(498, 30);
            this._tlpHeaderPlot.TabIndex = 15;
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.BackColor = System.Drawing.Color.CornflowerBlue;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Image = global::MetaboliteLevels.Properties.Resources.IcoHelp;
            this.button4.Location = new System.Drawing.Point(438, 0);
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
            this._lblPlot.AutoSize = true;
            this._lblPlot.Dock = System.Windows.Forms.DockStyle.Top;
            this._lblPlot.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblPlot.Location = new System.Drawing.Point(3, 0);
            this._lblPlot.Name = "_lblPlot";
            this._lblPlot.Size = new System.Drawing.Size(51, 30);
            this._lblPlot.TabIndex = 2;
            this._lblPlot.Text = "Plot";
            // 
            // button7
            // 
            this.button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button7.BackColor = System.Drawing.Color.CornflowerBlue;
            this.button7.FlatAppearance.BorderSize = 0;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Image = global::MetaboliteLevels.Properties.Resources.ObjLGraph;
            this.button7.Location = new System.Drawing.Point(468, 0);
            this.button7.Margin = new System.Windows.Forms.Padding(0);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(30, 30);
            this.button7.TabIndex = 11;
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.chart1);
            this.panel3.Controls.Add(this._tlpHeaderCluster);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(498, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(498, 394);
            this.panel3.TabIndex = 14;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(0, 30);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(498, 364);
            this.chart1.TabIndex = 3;
            this.chart1.Text = "chart1";
            // 
            // _tlpHeaderCluster
            // 
            this._tlpHeaderCluster.AutoSize = true;
            this._tlpHeaderCluster.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._tlpHeaderCluster.BackColor = System.Drawing.Color.CornflowerBlue;
            this._tlpHeaderCluster.ColumnCount = 3;
            this._tlpHeaderCluster.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this._tlpHeaderCluster.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._tlpHeaderCluster.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this._tlpHeaderCluster.Controls.Add(this._labelCluster, 0, 0);
            this._tlpHeaderCluster.Controls.Add(this.button5, 1, 0);
            this._tlpHeaderCluster.Controls.Add(this.button1, 2, 0);
            this._tlpHeaderCluster.Dock = System.Windows.Forms.DockStyle.Top;
            this._tlpHeaderCluster.ForeColor = System.Drawing.Color.White;
            this._tlpHeaderCluster.Location = new System.Drawing.Point(0, 0);
            this._tlpHeaderCluster.Name = "_tlpHeaderCluster";
            this._tlpHeaderCluster.RowCount = 1;
            this._tlpHeaderCluster.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._tlpHeaderCluster.Size = new System.Drawing.Size(498, 30);
            this._tlpHeaderCluster.TabIndex = 17;
            // 
            // _labelCluster
            // 
            this._labelCluster.AutoSize = true;
            this._labelCluster.Dock = System.Windows.Forms.DockStyle.Top;
            this._labelCluster.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelCluster.Location = new System.Drawing.Point(3, 0);
            this._labelCluster.Name = "_labelCluster";
            this._labelCluster.Size = new System.Drawing.Size(79, 30);
            this._labelCluster.TabIndex = 3;
            this._labelCluster.Text = "Cluster";
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button5.BackColor = System.Drawing.Color.CornflowerBlue;
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Image = global::MetaboliteLevels.Properties.Resources.IcoHelp;
            this.button5.Location = new System.Drawing.Point(438, 0);
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
            this.button1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::MetaboliteLevels.Properties.Resources.ObjLGraph;
            this.button1.Location = new System.Drawing.Point(468, 0);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(30, 30);
            this.button1.TabIndex = 11;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this._btnNew,
            this._btnLoad,
            this._btnImport,
            this._btnExport,
            this.toolStripLabel1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.toolStrip1.Size = new System.Drawing.Size(996, 73);
            this.toolStrip1.TabIndex = 13;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Font = new System.Drawing.Font("Segoe UI Semibold", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel2.ForeColor = System.Drawing.Color.White;
            this.toolStripLabel2.Margin = new System.Windows.Forms.Padding(8);
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(300, 49);
            this.toolStripLabel2.Text = "Evaluate Clustering";
            // 
            // _btnNew
            // 
            this._btnNew.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnNew.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this._btnNew.Image = global::MetaboliteLevels.Properties.Resources.NewPerformanceSession_8866;
            this._btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnNew.Margin = new System.Windows.Forms.Padding(8, 24, 0, 0);
            this._btnNew.Name = "_btnNew";
            this._btnNew.Size = new System.Drawing.Size(44, 41);
            this._btnNew.Text = "Edit";
            this._btnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._btnNew.Click += new System.EventHandler(this._btnNewTest_Click);
            // 
            // _btnLoad
            // 
            this._btnLoad.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnLoad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this._btnLoad.Image = ((System.Drawing.Image)(resources.GetObject("_btnLoad.Image")));
            this._btnLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnLoad.Margin = new System.Windows.Forms.Padding(8, 24, 0, 0);
            this._btnLoad.Name = "_btnLoad";
            this._btnLoad.Size = new System.Drawing.Size(60, 41);
            this._btnLoad.Text = "Select";
            this._btnLoad.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._btnLoad.Click += new System.EventHandler(this._btnLoad_Click_1);
            // 
            // _btnImport
            // 
            this._btnImport.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnImport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this._btnImport.Image = global::MetaboliteLevels.Properties.Resources.MnuOpen;
            this._btnImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnImport.Margin = new System.Windows.Forms.Padding(8, 24, 0, 0);
            this._btnImport.Name = "_btnImport";
            this._btnImport.Size = new System.Drawing.Size(66, 41);
            this._btnImport.Text = "Import";
            this._btnImport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._btnImport.Click += new System.EventHandler(this._btnImport_Click);
            // 
            // _btnExport
            // 
            this._btnExport.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnExport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this._btnExport.Image = global::MetaboliteLevels.Properties.Resources.MnuSave;
            this._btnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnExport.Margin = new System.Windows.Forms.Padding(8, 24, 0, 0);
            this._btnExport.Name = "_btnExport";
            this._btnExport.Size = new System.Drawing.Size(64, 41);
            this._btnExport.Text = "Export";
            this._btnExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._btnExport.Click += new System.EventHandler(this._btnSave_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel1.ForeColor = System.Drawing.Color.White;
            this.toolStripLabel1.Margin = new System.Windows.Forms.Padding(8, 24, 0, 0);
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(212, 41);
            this.toolStripLabel1.Text = "← Edit or select a test to begin";
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
            this._infoLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 867);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(996, 22);
            this.statusStrip1.TabIndex = 14;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // _infoLabel
            // 
            this._infoLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._infoLabel.Name = "_infoLabel";
            this._infoLabel.Size = new System.Drawing.Size(158, 17);
            this._infoLabel.Text = "Create or load a test to begin";
            // 
            // FrmEvaluateClustering
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(996, 889);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmEvaluateClustering";
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
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
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

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ListView _lstParams;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListView _lstClusters;
        private System.Windows.Forms.Panel panel4;
        private MCharting.MChart _chartParameters;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
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
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label _labelCluster;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button _btnValuesHelp;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TreeView _tvStatistics;
        private System.Windows.Forms.ListView _lstStatistics;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem copyToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton _btnNew;
        private System.Windows.Forms.ToolStripButton _btnExport;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel _infoLabel;
        private System.Windows.Forms.Button _btnViewScript;
        private System.Windows.Forms.ToolStripButton _btnImport;
        private System.Windows.Forms.ToolStripButton _btnLoad;
        private System.Windows.Forms.ComboBox _lstSel;
    }
}