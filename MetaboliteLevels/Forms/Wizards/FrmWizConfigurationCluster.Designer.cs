namespace MetaboliteLevels.Forms.Wizards
{
    partial class FrmWizConfigurationCluster
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this._lstFilters = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this._chkClusterIndividually = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this._btnEditFilters = new MetaboliteLevels.Controls.CtlButton();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this._lblSeedCurrent = new System.Windows.Forms.LinkLabel();
            this._radSeedLowest = new System.Windows.Forms.RadioButton();
            this._lblSeedPearson = new System.Windows.Forms.LinkLabel();
            this._radSeedHighest = new System.Windows.Forms.RadioButton();
            this._lblSeedStudent = new System.Windows.Forms.LinkLabel();
            this._radSeedCurrent = new System.Windows.Forms.RadioButton();
            this._lstStat = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this._lstGroups = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.label8 = new System.Windows.Forms.Label();
            this._lstPeakFilter = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this._btnPeakFilter = new MetaboliteLevels.Controls.CtlButton();
            this.label10 = new System.Windows.Forms.Label();
            this._btnDistanceParams = new MetaboliteLevels.Controls.CtlButton();
            this._txtDistanceParams = new MGui.Controls.CtlTextBox();
            this._lstDistanceMeasure = new System.Windows.Forms.ComboBox();
            this._btnDistanceMeasure = new MetaboliteLevels.Controls.CtlButton();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this._txtStopD = new MGui.Controls.CtlTextBox();
            this._radStopN = new System.Windows.Forms.RadioButton();
            this._radStopD = new System.Windows.Forms.RadioButton();
            this._txtStopN = new MGui.Controls.CtlTextBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this._radFinishStop = new System.Windows.Forms.RadioButton();
            this._radFinishK = new System.Windows.Forms.RadioButton();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this._lstSource = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this._btnSource = new MetaboliteLevels.Controls.CtlButton();
            this.tabControl1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage7);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Location = new System.Drawing.Point(8, 8);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(888, 664);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.tableLayoutPanel2);
            this.tabPage4.Location = new System.Drawing.Point(4, 30);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(880, 630);
            this.tabPage4.TabIndex = 6;
            this.tabPage4.Text = "Experimental Groups";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this._lstFilters, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this._chkClusterIndividually, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this._btnEditFilters, 1, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.MinimumSize = new System.Drawing.Size(8, 8);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(874, 624);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // _lstFilters
            // 
            this._lstFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this._lstFilters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstFilters.FormattingEnabled = true;
            this._lstFilters.Location = new System.Drawing.Point(24, 61);
            this._lstFilters.Margin = new System.Windows.Forms.Padding(24, 8, 8, 8);
            this._lstFilters.Name = "_lstFilters";
            this._lstFilters.Size = new System.Drawing.Size(797, 29);
            this._lstFilters.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label2.Location = new System.Drawing.Point(8, 8);
            this.label2.Margin = new System.Windows.Forms.Padding(8);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(8);
            this.label2.Size = new System.Drawing.Size(542, 37);
            this.label2.TabIndex = 1;
            this.label2.Text = "Specify the experimental groups that the clustering will be based upon";
            // 
            // _chkClusterIndividually
            // 
            this._chkClusterIndividually.AutoSize = true;
            this._chkClusterIndividually.Location = new System.Drawing.Point(24, 159);
            this._chkClusterIndividually.Margin = new System.Windows.Forms.Padding(24, 8, 8, 8);
            this._chkClusterIndividually.Name = "_chkClusterIndividually";
            this._chkClusterIndividually.Size = new System.Drawing.Size(162, 25);
            this._chkClusterIndividually.TabIndex = 3;
            this._chkClusterIndividually.Text = "Cluster individually";
            this._chkClusterIndividually.UseVisualStyleBackColor = true;
            this._chkClusterIndividually.CheckedChanged += new System.EventHandler(this._chkClusterIndividually_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label4.Location = new System.Drawing.Point(8, 106);
            this.label4.Margin = new System.Windows.Forms.Padding(8);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(8);
            this.label4.Size = new System.Drawing.Size(710, 37);
            this.label4.TabIndex = 1;
            this.label4.Text = "Do you want to base the clustering across all of the groups, or cluster each grou" +
    "p individually?";
            // 
            // _btnEditFilters
            // 
            this._btnEditFilters.Image = global::MetaboliteLevels.Properties.Resources.MnuViewList;
            this._btnEditFilters.Location = new System.Drawing.Point(837, 61);
            this._btnEditFilters.Margin = new System.Windows.Forms.Padding(8);
            this._btnEditFilters.Name = "_btnEditFilters";
            this.tableLayoutPanel2.SetRowSpan(this._btnEditFilters, 2);
            this._btnEditFilters.Size = new System.Drawing.Size(29, 29);
            this._btnEditFilters.TabIndex = 2;
            this._btnEditFilters.Text = "";
            this._btnEditFilters.UseDefaultSize = true;
            this._btnEditFilters.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 30);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage1.Size = new System.Drawing.Size(880, 630);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Seed generation";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._lblSeedCurrent, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this._radSeedLowest, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this._lblSeedPearson, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this._radSeedHighest, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this._lblSeedStudent, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this._radSeedCurrent, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this._lstStat, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this._lstGroups, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 2, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 5);
            this.tableLayoutPanel1.MinimumSize = new System.Drawing.Size(8, 8);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(872, 620);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label1, 3);
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new System.Drawing.Point(4, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(8);
            this.label1.Size = new System.Drawing.Size(364, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "Choose a seed peak to act as the first \"cluster\"";
            // 
            // _lblSeedCurrent
            // 
            this._lblSeedCurrent.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._lblSeedCurrent.AutoSize = true;
            this._lblSeedCurrent.Enabled = false;
            this._lblSeedCurrent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblSeedCurrent.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this._lblSeedCurrent.Location = new System.Drawing.Point(119, 191);
            this._lblSeedCurrent.Name = "_lblSeedCurrent";
            this._lblSeedCurrent.Padding = new System.Windows.Forms.Padding(8);
            this._lblSeedCurrent.Size = new System.Drawing.Size(69, 32);
            this._lblSeedCurrent.TabIndex = 3;
            this._lblSeedCurrent.TabStop = true;
            this._lblSeedCurrent.Text = "LN0000";
            this._lblSeedCurrent.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._lblSeedCurrent_LinkClicked);
            // 
            // _radSeedLowest
            // 
            this._radSeedLowest.AutoSize = true;
            this._radSeedLowest.Enabled = false;
            this._radSeedLowest.Location = new System.Drawing.Point(3, 93);
            this._radSeedLowest.Name = "_radSeedLowest";
            this._radSeedLowest.Padding = new System.Windows.Forms.Padding(16, 8, 8, 8);
            this._radSeedLowest.Size = new System.Drawing.Size(101, 41);
            this._radSeedLowest.TabIndex = 1;
            this._radSeedLowest.Text = "Lowest";
            this._radSeedLowest.UseVisualStyleBackColor = true;
            this._radSeedLowest.CheckedChanged += new System.EventHandler(this.OptionChanged);
            // 
            // _lblSeedPearson
            // 
            this._lblSeedPearson.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._lblSeedPearson.AutoSize = true;
            this._lblSeedPearson.Enabled = false;
            this._lblSeedPearson.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblSeedPearson.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this._lblSeedPearson.Location = new System.Drawing.Point(119, 144);
            this._lblSeedPearson.Name = "_lblSeedPearson";
            this._lblSeedPearson.Padding = new System.Windows.Forms.Padding(8);
            this._lblSeedPearson.Size = new System.Drawing.Size(69, 32);
            this._lblSeedPearson.TabIndex = 3;
            this._lblSeedPearson.TabStop = true;
            this._lblSeedPearson.Text = "LN0000";
            this._lblSeedPearson.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._lblSeedPearson_LinkClicked);
            // 
            // _radSeedHighest
            // 
            this._radSeedHighest.AutoSize = true;
            this._radSeedHighest.Enabled = false;
            this._radSeedHighest.Location = new System.Drawing.Point(3, 140);
            this._radSeedHighest.Name = "_radSeedHighest";
            this._radSeedHighest.Padding = new System.Windows.Forms.Padding(16, 8, 8, 8);
            this._radSeedHighest.Size = new System.Drawing.Size(105, 41);
            this._radSeedHighest.TabIndex = 1;
            this._radSeedHighest.Text = "Highest";
            this._radSeedHighest.UseVisualStyleBackColor = true;
            this._radSeedHighest.CheckedChanged += new System.EventHandler(this.OptionChanged);
            // 
            // _lblSeedStudent
            // 
            this._lblSeedStudent.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._lblSeedStudent.AutoSize = true;
            this._lblSeedStudent.Enabled = false;
            this._lblSeedStudent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblSeedStudent.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this._lblSeedStudent.Location = new System.Drawing.Point(119, 97);
            this._lblSeedStudent.Name = "_lblSeedStudent";
            this._lblSeedStudent.Padding = new System.Windows.Forms.Padding(8);
            this._lblSeedStudent.Size = new System.Drawing.Size(69, 32);
            this._lblSeedStudent.TabIndex = 3;
            this._lblSeedStudent.TabStop = true;
            this._lblSeedStudent.Text = "LN0000";
            this._lblSeedStudent.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._lblSeedStudent_LinkClicked);
            // 
            // _radSeedCurrent
            // 
            this._radSeedCurrent.AutoSize = true;
            this._radSeedCurrent.Enabled = false;
            this._radSeedCurrent.Location = new System.Drawing.Point(3, 187);
            this._radSeedCurrent.Name = "_radSeedCurrent";
            this._radSeedCurrent.Padding = new System.Windows.Forms.Padding(16, 8, 8, 8);
            this._radSeedCurrent.Size = new System.Drawing.Size(110, 41);
            this._radSeedCurrent.TabIndex = 1;
            this._radSeedCurrent.Text = "Selected";
            this._radSeedCurrent.UseVisualStyleBackColor = true;
            this._radSeedCurrent.CheckedChanged += new System.EventHandler(this.OptionChanged);
            // 
            // _lstStat
            // 
            this.tableLayoutPanel1.SetColumnSpan(this._lstStat, 3);
            this._lstStat.Dock = System.Windows.Forms.DockStyle.Top;
            this._lstStat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstStat.FormattingEnabled = true;
            this._lstStat.Location = new System.Drawing.Point(19, 49);
            this._lstStat.Margin = new System.Windows.Forms.Padding(19, 12, 11, 12);
            this._lstStat.Name = "_lstStat";
            this._lstStat.Size = new System.Drawing.Size(842, 29);
            this._lstStat.TabIndex = 4;
            this._lstStat.SelectedIndexChanged += new System.EventHandler(this._lstStat_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label7, 3);
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label7.Location = new System.Drawing.Point(4, 530);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Padding = new System.Windows.Forms.Padding(8);
            this.label7.Size = new System.Drawing.Size(777, 37);
            this.label7.TabIndex = 0;
            this.label7.Text = "If you selected \"cluster individually\" on the previous screen you will also need " +
    "to select the seed group.";
            // 
            // _lstGroups
            // 
            this.tableLayoutPanel1.SetColumnSpan(this._lstGroups, 3);
            this._lstGroups.Dock = System.Windows.Forms.DockStyle.Top;
            this._lstGroups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstGroups.Enabled = false;
            this._lstGroups.FormattingEnabled = true;
            this._lstGroups.Location = new System.Drawing.Point(19, 579);
            this._lstGroups.Margin = new System.Windows.Forms.Padding(19, 12, 11, 12);
            this._lstGroups.Name = "_lstGroups";
            this._lstGroups.Size = new System.Drawing.Size(842, 29);
            this._lstGroups.TabIndex = 4;
            this._lstGroups.SelectedIndexChanged += new System.EventHandler(this._lstStat_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(191, 90);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.tableLayoutPanel1.SetRowSpan(this.panel1, 4);
            this.panel1.Size = new System.Drawing.Size(681, 440);
            this.panel1.TabIndex = 5;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel6);
            this.tabPage2.Location = new System.Drawing.Point(4, 30);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage2.Size = new System.Drawing.Size(880, 630);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Additional options";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 3;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this._lstPeakFilter, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.label9, 0, 2);
            this.tableLayoutPanel6.Controls.Add(this._btnPeakFilter, 2, 1);
            this.tableLayoutPanel6.Controls.Add(this.label10, 0, 4);
            this.tableLayoutPanel6.Controls.Add(this._btnDistanceParams, 2, 4);
            this.tableLayoutPanel6.Controls.Add(this._txtDistanceParams, 1, 4);
            this.tableLayoutPanel6.Controls.Add(this._lstDistanceMeasure, 0, 3);
            this.tableLayoutPanel6.Controls.Add(this._btnDistanceMeasure, 2, 3);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(4, 5);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 5;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.Size = new System.Drawing.Size(872, 620);
            this.tableLayoutPanel6.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.tableLayoutPanel6.SetColumnSpan(this.label8, 3);
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label8.Location = new System.Drawing.Point(8, 8);
            this.label8.Margin = new System.Windows.Forms.Padding(8);
            this.label8.Name = "label8";
            this.label8.Padding = new System.Windows.Forms.Padding(8);
            this.label8.Size = new System.Drawing.Size(287, 37);
            this.label8.TabIndex = 2;
            this.label8.Text = "Shold insignificant peaks be filtered";
            // 
            // _lstPeakFilter
            // 
            this.tableLayoutPanel6.SetColumnSpan(this._lstPeakFilter, 2);
            this._lstPeakFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this._lstPeakFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstPeakFilter.FormattingEnabled = true;
            this._lstPeakFilter.Location = new System.Drawing.Point(24, 61);
            this._lstPeakFilter.Margin = new System.Windows.Forms.Padding(24, 8, 8, 8);
            this._lstPeakFilter.Name = "_lstPeakFilter";
            this._lstPeakFilter.Size = new System.Drawing.Size(795, 29);
            this._lstPeakFilter.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.tableLayoutPanel6.SetColumnSpan(this.label9, 3);
            this.label9.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label9.Location = new System.Drawing.Point(8, 106);
            this.label9.Margin = new System.Windows.Forms.Padding(8);
            this.label9.Name = "label9";
            this.label9.Padding = new System.Windows.Forms.Padding(8);
            this.label9.Size = new System.Drawing.Size(154, 37);
            this.label9.TabIndex = 2;
            this.label9.Text = "Distance measure";
            // 
            // _btnPeakFilter
            // 
            this._btnPeakFilter.Image = global::MetaboliteLevels.Properties.Resources.MnuViewList;
            this._btnPeakFilter.Location = new System.Drawing.Point(835, 61);
            this._btnPeakFilter.Margin = new System.Windows.Forms.Padding(8);
            this._btnPeakFilter.Name = "_btnPeakFilter";
            this._btnPeakFilter.Size = new System.Drawing.Size(29, 29);
            this._btnPeakFilter.TabIndex = 4;
            this._btnPeakFilter.Text = "";
            this._btnPeakFilter.UseDefaultSize = true;
            this._btnPeakFilter.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(24, 204);
            this.label10.Margin = new System.Windows.Forms.Padding(24, 8, 8, 8);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 21);
            this.label10.TabIndex = 2;
            this.label10.Text = "parameters";
            // 
            // _btnDistanceParams
            // 
            this._btnDistanceParams.Image = global::MetaboliteLevels.Properties.Resources.MnuEnlargeList;
            this._btnDistanceParams.Location = new System.Drawing.Point(835, 204);
            this._btnDistanceParams.Margin = new System.Windows.Forms.Padding(8);
            this._btnDistanceParams.Name = "_btnDistanceParams";
            this._btnDistanceParams.Size = new System.Drawing.Size(29, 29);
            this._btnDistanceParams.TabIndex = 4;
            this._btnDistanceParams.Text = "";
            this._btnDistanceParams.UseDefaultSize = true;
            this._btnDistanceParams.UseVisualStyleBackColor = true;
            // 
            // _txtDistanceParams
            // 
            this._txtDistanceParams.Dock = System.Windows.Forms.DockStyle.Top;
            this._txtDistanceParams.Location = new System.Drawing.Point(129, 204);
            this._txtDistanceParams.Margin = new System.Windows.Forms.Padding(8);
            this._txtDistanceParams.Name = "_txtDistanceParams";
            this._txtDistanceParams.Size = new System.Drawing.Size(690, 29);
            this._txtDistanceParams.TabIndex = 2;
            this._txtDistanceParams.Watermark = null;
            // 
            // _lstDistanceMeasure
            // 
            this.tableLayoutPanel6.SetColumnSpan(this._lstDistanceMeasure, 2);
            this._lstDistanceMeasure.Dock = System.Windows.Forms.DockStyle.Top;
            this._lstDistanceMeasure.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstDistanceMeasure.FormattingEnabled = true;
            this._lstDistanceMeasure.Location = new System.Drawing.Point(24, 159);
            this._lstDistanceMeasure.Margin = new System.Windows.Forms.Padding(24, 8, 8, 8);
            this._lstDistanceMeasure.Name = "_lstDistanceMeasure";
            this._lstDistanceMeasure.Size = new System.Drawing.Size(795, 29);
            this._lstDistanceMeasure.TabIndex = 3;
            // 
            // _btnDistanceMeasure
            // 
            this._btnDistanceMeasure.Image = global::MetaboliteLevels.Properties.Resources.MnuViewList;
            this._btnDistanceMeasure.Location = new System.Drawing.Point(835, 159);
            this._btnDistanceMeasure.Margin = new System.Windows.Forms.Padding(8);
            this._btnDistanceMeasure.Name = "_btnDistanceMeasure";
            this._btnDistanceMeasure.Size = new System.Drawing.Size(29, 29);
            this._btnDistanceMeasure.TabIndex = 4;
            this._btnDistanceMeasure.Text = "";
            this._btnDistanceMeasure.UseDefaultSize = true;
            this._btnDistanceMeasure.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tableLayoutPanel3);
            this.tabPage3.Location = new System.Drawing.Point(4, 30);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(880, 630);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Stopping conditions";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this._txtStopD, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this._radStopN, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this._radStopD, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this._txtStopN, 1, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(874, 624);
            this.tableLayoutPanel3.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label3.Location = new System.Drawing.Point(8, 8);
            this.label3.Margin = new System.Windows.Forms.Padding(8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(303, 21);
            this.label3.TabIndex = 1;
            this.label3.Text = "How many clusters should be generated";
            // 
            // _txtStopD
            // 
            this._txtStopD.Enabled = false;
            this._txtStopD.Location = new System.Drawing.Point(492, 81);
            this._txtStopD.Name = "_txtStopD";
            this._txtStopD.Size = new System.Drawing.Size(100, 29);
            this._txtStopD.TabIndex = 5;
            this._txtStopD.Watermark = null;
            this._txtStopD.TextChanged += new System.EventHandler(this.OptionChanged);
            // 
            // _radStopN
            // 
            this._radStopN.AutoSize = true;
            this._radStopN.Location = new System.Drawing.Point(16, 45);
            this._radStopN.Margin = new System.Windows.Forms.Padding(16, 8, 8, 8);
            this._radStopN.Name = "_radStopN";
            this._radStopN.Size = new System.Drawing.Size(152, 25);
            this._radStopN.TabIndex = 3;
            this._radStopN.Text = "A specific number";
            this._radStopN.UseVisualStyleBackColor = true;
            this._radStopN.CheckedChanged += new System.EventHandler(this._radStopN_CheckedChanged);
            // 
            // _radStopD
            // 
            this._radStopD.AutoSize = true;
            this._radStopD.Location = new System.Drawing.Point(16, 86);
            this._radStopD.Margin = new System.Windows.Forms.Padding(16, 8, 8, 8);
            this._radStopD.Name = "_radStopD";
            this._radStopD.Size = new System.Drawing.Size(465, 25);
            this._radStopD.TabIndex = 3;
            this._radStopD.Text = "Until all variables are within a specified distance of an exemplar";
            this._radStopD.UseVisualStyleBackColor = true;
            this._radStopD.CheckedChanged += new System.EventHandler(this._radStopD_CheckedChanged);
            // 
            // _txtStopN
            // 
            this._txtStopN.Enabled = false;
            this._txtStopN.Location = new System.Drawing.Point(492, 40);
            this._txtStopN.Name = "_txtStopN";
            this._txtStopN.Size = new System.Drawing.Size(100, 29);
            this._txtStopN.TabIndex = 5;
            this._txtStopN.Watermark = null;
            this._txtStopN.TextChanged += new System.EventHandler(this.OptionChanged);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.tableLayoutPanel4);
            this.tabPage5.Location = new System.Drawing.Point(4, 30);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(880, 630);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Centre handling";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this._radFinishStop, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this._radFinishK, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(874, 624);
            this.tableLayoutPanel4.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label5.Location = new System.Drawing.Point(8, 8);
            this.label5.Margin = new System.Windows.Forms.Padding(8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(363, 21);
            this.label5.TabIndex = 2;
            this.label5.Text = "What should we do when we have the exemplars";
            // 
            // _radFinishStop
            // 
            this._radFinishStop.AutoSize = true;
            this._radFinishStop.Location = new System.Drawing.Point(16, 86);
            this._radFinishStop.Margin = new System.Windows.Forms.Padding(16, 8, 8, 8);
            this._radFinishStop.Name = "_radFinishStop";
            this._radFinishStop.Size = new System.Drawing.Size(85, 25);
            this._radFinishStop.TabIndex = 4;
            this._radFinishStop.Text = "Nothing";
            this._radFinishStop.UseVisualStyleBackColor = true;
            this._radFinishStop.CheckedChanged += new System.EventHandler(this.OptionChanged);
            // 
            // _radFinishK
            // 
            this._radFinishK.AutoSize = true;
            this._radFinishK.Location = new System.Drawing.Point(16, 45);
            this._radFinishK.Margin = new System.Windows.Forms.Padding(16, 8, 8, 8);
            this._radFinishK.Name = "_radFinishK";
            this._radFinishK.Size = new System.Drawing.Size(88, 25);
            this._radFinishK.TabIndex = 4;
            this._radFinishK.Text = "k-means";
            this._radFinishK.UseVisualStyleBackColor = true;
            this._radFinishK.CheckedChanged += new System.EventHandler(this.OptionChanged);
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.tableLayoutPanel5);
            this.tabPage6.Location = new System.Drawing.Point(4, 30);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(880, 630);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Generate clusters";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(874, 624);
            this.tableLayoutPanel5.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label6.Location = new System.Drawing.Point(8, 8);
            this.label6.Margin = new System.Windows.Forms.Padding(8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(688, 25);
            this.label6.TabIndex = 0;
            this.label6.Text = "Click OK to generate the clusters. Any existing clusters will be lost.";
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.tableLayoutPanel7);
            this.tabPage7.Location = new System.Drawing.Point(4, 30);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(880, 630);
            this.tabPage7.TabIndex = 7;
            this.tabPage7.Text = "Data";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.Controls.Add(this._lstSource, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.label11, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this._btnSource, 1, 1);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel7.MinimumSize = new System.Drawing.Size(8, 8);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 2;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.Size = new System.Drawing.Size(874, 624);
            this.tableLayoutPanel7.TabIndex = 1;
            // 
            // _lstSource
            // 
            this._lstSource.Dock = System.Windows.Forms.DockStyle.Top;
            this._lstSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lstSource.FormattingEnabled = true;
            this._lstSource.Location = new System.Drawing.Point(24, 61);
            this._lstSource.Margin = new System.Windows.Forms.Padding(24, 8, 8, 8);
            this._lstSource.Name = "_lstSource";
            this._lstSource.Size = new System.Drawing.Size(797, 29);
            this._lstSource.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label11.Location = new System.Drawing.Point(8, 8);
            this.label11.Margin = new System.Windows.Forms.Padding(8);
            this.label11.Name = "label11";
            this.label11.Padding = new System.Windows.Forms.Padding(8);
            this.label11.Size = new System.Drawing.Size(339, 37);
            this.label11.TabIndex = 1;
            this.label11.Text = "Select the data you would like to work with";
            // 
            // _btnSource
            // 
            this._btnSource.Image = global::MetaboliteLevels.Properties.Resources.MnuViewList;
            this._btnSource.Location = new System.Drawing.Point(837, 61);
            this._btnSource.Margin = new System.Windows.Forms.Padding(8);
            this._btnSource.Name = "_btnSource";
            this._btnSource.Size = new System.Drawing.Size(29, 29);
            this._btnSource.TabIndex = 2;
            this._btnSource.Text = "";
            this._btnSource.UseDefaultSize = true;
            this._btnSource.UseVisualStyleBackColor = true;
            // 
            // FrmWizConfigurationCluster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(988, 747);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmWizConfigurationCluster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create Clusters";
            this.tabControl1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tabPage7.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.LinkLabel _lblSeedCurrent;
        private System.Windows.Forms.LinkLabel _lblSeedPearson;
        private System.Windows.Forms.LinkLabel _lblSeedStudent;
        private System.Windows.Forms.RadioButton _radSeedCurrent;
        private System.Windows.Forms.RadioButton _radSeedHighest;
        private System.Windows.Forms.RadioButton _radSeedLowest;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private MGui.Controls.CtlTextBox _txtStopD;
        private MGui.Controls.CtlTextBox _txtStopN;
        private System.Windows.Forms.RadioButton _radStopD;
        private System.Windows.Forms.RadioButton _radStopN;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.RadioButton _radFinishStop;
        private System.Windows.Forms.RadioButton _radFinishK;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.ComboBox _lstStat;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ComboBox _lstFilters;
        private System.Windows.Forms.Label label2;
        private Controls.CtlButton _btnEditFilters;
        private System.Windows.Forms.CheckBox _chkClusterIndividually;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox _lstGroups;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox _lstPeakFilter;
        private System.Windows.Forms.Label label9;
        private Controls.CtlButton _btnPeakFilter;
        private System.Windows.Forms.Label label10;
        private Controls.CtlButton _btnDistanceParams;
        private MGui.Controls.CtlTextBox _txtDistanceParams;
        private System.Windows.Forms.ComboBox _lstDistanceMeasure;
        private Controls.CtlButton _btnDistanceMeasure;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.ComboBox _lstSource;
        private System.Windows.Forms.Label label11;
        private Controls.CtlButton _btnSource;
    }
}