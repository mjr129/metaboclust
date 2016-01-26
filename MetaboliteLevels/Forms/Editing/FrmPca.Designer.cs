namespace MetaboliteLevels.Forms.Editing
{
    partial class FrmPca
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPca));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._mnuTranspose = new System.Windows.Forms.ToolStripDropDownButton();
            this.transposeToShowObservationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transposeToShowPeaksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._btnScoresOrLoadings = new System.Windows.Forms.ToolStripDropDownButton();
            this._btnScores = new System.Windows.Forms.ToolStripMenuItem();
            this._btnLoadings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._mnuTrend = new System.Windows.Forms.ToolStripDropDownButton();
            this.usetrendLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.useAllobservationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._mnuObsFilter = new System.Windows.Forms.ToolStripDropDownButton();
            this._mnuPeakFilter = new System.Windows.Forms.ToolStripDropDownButton();
            this._btnMarkAsOutlier = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this._btnNextPc = new System.Windows.Forms.ToolStripButton();
            this._btnPrevPc = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.saveImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this._lblSelection = new System.Windows.Forms.ToolStripStatusLabel();
            this._cmsFilter = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._chart = new MCharting.MChart();
            this.ctlTitleBar1 = new MetaboliteLevels.Controls.CtlTitleBar();
            this._mnuCorrections = new System.Windows.Forms.ToolStripDropDownButton();
            this._btnColour = new System.Windows.Forms.ToolStripDropDownButton();
            this._lblPcNumber = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._mnuTranspose,
            this._btnScoresOrLoadings,
            this.toolStripSeparator2,
            this._mnuCorrections,
            this._mnuTrend,
            this.toolStripSeparator1,
            this._mnuObsFilter,
            this._mnuPeakFilter,
            this._btnMarkAsOutlier,
            this.toolStripSeparator5,
            this._btnNextPc,
            this._lblPcNumber,
            this._btnPrevPc,
            this.toolStripSeparator4,
            this._btnColour,
            this.toolStripSeparator3,
            this.toolStripDropDownButton1});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(964, 66);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(129, 745);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "Flag as outlier";
            // 
            // _mnuTranspose
            // 
            this._mnuTranspose.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.transposeToShowObservationsToolStripMenuItem,
            this.transposeToShowPeaksToolStripMenuItem});
            this._mnuTranspose.Image = ((System.Drawing.Image)(resources.GetObject("_mnuTranspose.Image")));
            this._mnuTranspose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._mnuTranspose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._mnuTranspose.Name = "_mnuTranspose";
            this._mnuTranspose.Size = new System.Drawing.Size(126, 36);
            this._mnuTranspose.Text = "Text goes here";
            this._mnuTranspose.ToolTipText = "Select PCA input";
            // 
            // transposeToShowObservationsToolStripMenuItem
            // 
            this.transposeToShowObservationsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("transposeToShowObservationsToolStripMenuItem.Image")));
            this.transposeToShowObservationsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.transposeToShowObservationsToolStripMenuItem.Name = "transposeToShowObservationsToolStripMenuItem";
            this.transposeToShowObservationsToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.transposeToShowObservationsToolStripMenuItem.Text = "&Transpose to show observations";
            this.transposeToShowObservationsToolStripMenuItem.Click += new System.EventHandler(this.transposeToShowObservationsToolStripMenuItem_Click);
            // 
            // transposeToShowPeaksToolStripMenuItem
            // 
            this.transposeToShowPeaksToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("transposeToShowPeaksToolStripMenuItem.Image")));
            this.transposeToShowPeaksToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.transposeToShowPeaksToolStripMenuItem.Name = "transposeToShowPeaksToolStripMenuItem";
            this.transposeToShowPeaksToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.transposeToShowPeaksToolStripMenuItem.Text = "&Transpose to show peaks";
            this.transposeToShowPeaksToolStripMenuItem.Click += new System.EventHandler(this.transposeToShowPeaksToolStripMenuItem_Click);
            // 
            // _btnScoresOrLoadings
            // 
            this._btnScoresOrLoadings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._btnScores,
            this._btnLoadings});
            this._btnScoresOrLoadings.Image = ((System.Drawing.Image)(resources.GetObject("_btnScoresOrLoadings.Image")));
            this._btnScoresOrLoadings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._btnScoresOrLoadings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnScoresOrLoadings.Name = "_btnScoresOrLoadings";
            this._btnScoresOrLoadings.Size = new System.Drawing.Size(126, 36);
            this._btnScoresOrLoadings.Text = "Text goes here";
            this._btnScoresOrLoadings.ToolTipText = "Select plot mode";
            // 
            // _btnScores
            // 
            this._btnScores.Image = ((System.Drawing.Image)(resources.GetObject("_btnScores.Image")));
            this._btnScores.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._btnScores.Name = "_btnScores";
            this._btnScores.Size = new System.Drawing.Size(152, 22);
            this._btnScores.Text = "&Plot scores";
            this._btnScores.Click += new System.EventHandler(this._btnScores_Click);
            // 
            // _btnLoadings
            // 
            this._btnLoadings.Image = ((System.Drawing.Image)(resources.GetObject("_btnLoadings.Image")));
            this._btnLoadings.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._btnLoadings.Name = "_btnLoadings";
            this._btnLoadings.Size = new System.Drawing.Size(152, 22);
            this._btnLoadings.Text = "&Plot loadings";
            this._btnLoadings.Click += new System.EventHandler(this._btnLoadings_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(126, 6);
            // 
            // _mnuTrend
            // 
            this._mnuTrend.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.usetrendLineToolStripMenuItem,
            this.useAllobservationsToolStripMenuItem});
            this._mnuTrend.Image = ((System.Drawing.Image)(resources.GetObject("_mnuTrend.Image")));
            this._mnuTrend.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._mnuTrend.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._mnuTrend.Name = "_mnuTrend";
            this._mnuTrend.Size = new System.Drawing.Size(126, 36);
            this._mnuTrend.Text = "Text goes here";
            this._mnuTrend.ToolTipText = "Select source data";
            this._mnuTrend.DropDownOpening += new System.EventHandler(this._mnuTrend_DropDownOpening);
            // 
            // usetrendLineToolStripMenuItem
            // 
            this.usetrendLineToolStripMenuItem.Image = global::MetaboliteLevels.Properties.Resources.MnuTrend;
            this.usetrendLineToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.usetrendLineToolStripMenuItem.Name = "usetrendLineToolStripMenuItem";
            this.usetrendLineToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.usetrendLineToolStripMenuItem.Text = "Use &trend line";
            this.usetrendLineToolStripMenuItem.Click += new System.EventHandler(this.usetrendLineToolStripMenuItem_Click);
            // 
            // useAllobservationsToolStripMenuItem
            // 
            this.useAllobservationsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("useAllobservationsToolStripMenuItem.Image")));
            this.useAllobservationsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.useAllobservationsToolStripMenuItem.Name = "useAllobservationsToolStripMenuItem";
            this.useAllobservationsToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.useAllobservationsToolStripMenuItem.Text = "Use all &observations";
            this.useAllobservationsToolStripMenuItem.Click += new System.EventHandler(this.useAllobservationsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(126, 6);
            // 
            // _mnuObsFilter
            // 
            this._mnuObsFilter.Image = ((System.Drawing.Image)(resources.GetObject("_mnuObsFilter.Image")));
            this._mnuObsFilter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._mnuObsFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._mnuObsFilter.Name = "_mnuObsFilter";
            this._mnuObsFilter.Size = new System.Drawing.Size(126, 36);
            this._mnuObsFilter.Text = "Text goes here";
            this._mnuObsFilter.ToolTipText = "Select observation filter";
            this._mnuObsFilter.DropDownOpening += new System.EventHandler(this.toolStripDropDownButton3_DropDownOpening);
            this._mnuObsFilter.Click += new System.EventHandler(this._mnuObsFilter_Click);
            // 
            // _mnuPeakFilter
            // 
            this._mnuPeakFilter.Image = ((System.Drawing.Image)(resources.GetObject("_mnuPeakFilter.Image")));
            this._mnuPeakFilter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._mnuPeakFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._mnuPeakFilter.Name = "_mnuPeakFilter";
            this._mnuPeakFilter.Size = new System.Drawing.Size(126, 36);
            this._mnuPeakFilter.Text = "Text goes here";
            this._mnuPeakFilter.ToolTipText = "Select peak filter";
            this._mnuPeakFilter.DropDownOpening += new System.EventHandler(this.toolStripDropDownButton1_DropDownOpening);
            // 
            // _btnMarkAsOutlier
            // 
            this._btnMarkAsOutlier.Image = ((System.Drawing.Image)(resources.GetObject("_btnMarkAsOutlier.Image")));
            this._btnMarkAsOutlier.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._btnMarkAsOutlier.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnMarkAsOutlier.Name = "_btnMarkAsOutlier";
            this._btnMarkAsOutlier.Size = new System.Drawing.Size(126, 36);
            this._btnMarkAsOutlier.Click += new System.EventHandler(this._btnMarkAsOutlier_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(126, 6);
            // 
            // _btnNextPc
            // 
            this._btnNextPc.Image = ((System.Drawing.Image)(resources.GetObject("_btnNextPc.Image")));
            this._btnNextPc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._btnNextPc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnNextPc.Name = "_btnNextPc";
            this._btnNextPc.Size = new System.Drawing.Size(126, 36);
            this._btnNextPc.Click += new System.EventHandler(this._btnNextPc_Click);
            // 
            // _btnPrevPc
            // 
            this._btnPrevPc.Image = ((System.Drawing.Image)(resources.GetObject("_btnPrevPc.Image")));
            this._btnPrevPc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._btnPrevPc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnPrevPc.Margin = new System.Windows.Forms.Padding(0, -9, 0, 2);
            this._btnPrevPc.Name = "_btnPrevPc";
            this._btnPrevPc.Size = new System.Drawing.Size(126, 36);
            this._btnPrevPc.Click += new System.EventHandler(this._btnPrevPc_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(126, 6);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(126, 6);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveImageToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(126, 36);
            this.toolStripDropDownButton1.Text = "Image options";
            // 
            // saveImageToolStripMenuItem
            // 
            this.saveImageToolStripMenuItem.Name = "saveImageToolStripMenuItem";
            this.saveImageToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveImageToolStripMenuItem.Text = "&Save image...";
            this.saveImageToolStripMenuItem.Click += new System.EventHandler(this.saveImageToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._lblSelection});
            this.statusStrip1.Location = new System.Drawing.Point(0, 785);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(964, 26);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // _lblSelection
            // 
            this._lblSelection.Name = "_lblSelection";
            this._lblSelection.Size = new System.Drawing.Size(96, 21);
            this._lblSelection.Text = "No selection";
            // 
            // _cmsFilter
            // 
            this._cmsFilter.Name = "_cmsFilter";
            this._cmsFilter.Size = new System.Drawing.Size(61, 4);
            // 
            // _chart
            // 
            this._chart.Dock = System.Windows.Forms.DockStyle.Fill;
            this._chart.Location = new System.Drawing.Point(0, 66);
            this._chart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this._chart.Name = "_chart";
            this._chart.SelectedItem = null;
            this._chart.Size = new System.Drawing.Size(964, 719);
            this._chart.TabIndex = 0;
            this._chart.SelectionChanged += new System.EventHandler(this._chart_SelectionChanged);
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
            this.ctlTitleBar1.Size = new System.Drawing.Size(1093, 66);
            this.ctlTitleBar1.SubText = "";
            this.ctlTitleBar1.TabIndex = 1;
            this.ctlTitleBar1.Text = "Principal Components Analysis";
            this.ctlTitleBar1.WarningText = null;
            // 
            // _mnuCorrections
            // 
            this._mnuCorrections.Image = ((System.Drawing.Image)(resources.GetObject("_mnuCorrections.Image")));
            this._mnuCorrections.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._mnuCorrections.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._mnuCorrections.Name = "_mnuCorrections";
            this._mnuCorrections.Size = new System.Drawing.Size(126, 36);
            this._mnuCorrections.Text = "Text goes here";
            this._mnuCorrections.ToolTipText = "Select source data";
            this._mnuCorrections.DropDownOpening += new System.EventHandler(this._mnuCorrections_DropDownOpening);
            // 
            // _btnColour
            // 
            this._btnColour.Image = ((System.Drawing.Image)(resources.GetObject("_btnColour.Image")));
            this._btnColour.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._btnColour.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnColour.Name = "_btnColour";
            this._btnColour.Size = new System.Drawing.Size(126, 36);
            this._btnColour.Text = "Text goes here";
            this._btnColour.ToolTipText = "Select peak filter";
            this._btnColour.DropDownOpening += new System.EventHandler(this._btnColour_DropDownOpening);
            this._btnColour.Click += new System.EventHandler(this._btnColour_Click);
            // 
            // _lblPcNumber
            // 
            this._lblPcNumber.Margin = new System.Windows.Forms.Padding(28, -10, 0, 2);
            this._lblPcNumber.Name = "_lblPcNumber";
            this._lblPcNumber.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this._lblPcNumber.Size = new System.Drawing.Size(98, 15);
            this._lblPcNumber.Text = "Text goes here";
            // 
            // FrmPca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1093, 811);
            this.Controls.Add(this._chart);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.ctlTitleBar1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmPca";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PCA";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MCharting.MChart _chart;
        private Controls.CtlTitleBar ctlTitleBar1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton _btnNextPc;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel _lblSelection;
        private System.Windows.Forms.ToolStripButton _btnPrevPc;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ContextMenuStrip _cmsFilter;
        private System.Windows.Forms.ToolStripDropDownButton _mnuTranspose;
        private System.Windows.Forms.ToolStripMenuItem transposeToShowObservationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transposeToShowPeaksToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton _mnuTrend;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton _mnuObsFilter;
        private System.Windows.Forms.ToolStripDropDownButton _mnuPeakFilter;
        private System.Windows.Forms.ToolStripMenuItem usetrendLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem useAllobservationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton _btnScoresOrLoadings;
        private System.Windows.Forms.ToolStripMenuItem _btnScores;
        private System.Windows.Forms.ToolStripMenuItem _btnLoadings;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem saveImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton _btnMarkAsOutlier;
        private System.Windows.Forms.ToolStripDropDownButton _mnuCorrections;
        private System.Windows.Forms.ToolStripDropDownButton _btnColour;
        private System.Windows.Forms.ToolStripLabel _lblPcNumber;
    }
}