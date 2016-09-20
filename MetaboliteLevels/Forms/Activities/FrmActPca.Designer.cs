using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaboliteLevels.Forms.Activities
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
            MCharting.Selection selection1 = new MCharting.Selection();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this._lblMethod = new System.Windows.Forms.ToolStripLabel();
            this._mnuPlsrSource = new System.Windows.Forms.ToolStripDropDownButton();
            this._lblPlsrSource = new System.Windows.Forms.ToolStripLabel();
            this._mnuTranspose = new System.Windows.Forms.ToolStripDropDownButton();
            this.transposeToShowObservationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transposeToShowPeaksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._lblPcaSource = new System.Windows.Forms.ToolStripLabel();
            this._btnScoresOrLoadings = new System.Windows.Forms.ToolStripDropDownButton();
            this._btnScores = new System.Windows.Forms.ToolStripMenuItem();
            this._btnLoadings = new System.Windows.Forms.ToolStripMenuItem();
            this._lblPlotView = new System.Windows.Forms.ToolStripLabel();
            this._btnColour = new System.Windows.Forms.ToolStripDropDownButton();
            this._lblLegend = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._mnuCorrections = new System.Windows.Forms.ToolStripDropDownButton();
            this._lblCorrections = new System.Windows.Forms.ToolStripLabel();
            this._mnuTrend = new System.Windows.Forms.ToolStripDropDownButton();
            this.usetrendLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.useAllobservationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._lblAspect = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._mnuObsFilter = new System.Windows.Forms.ToolStripDropDownButton();
            this._lblObs = new System.Windows.Forms.ToolStripLabel();
            this._mnuPeakFilter = new System.Windows.Forms.ToolStripDropDownButton();
            this._lblPeaks = new System.Windows.Forms.ToolStripLabel();
            this._btnNavigate = new System.Windows.Forms.ToolStripButton();
            this._lblNavigate = new System.Windows.Forms.ToolStripLabel();
            this._btnMarkAsOutlier = new System.Windows.Forms.ToolStripButton();
            this._lblOutlier = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this._btnNextPc = new System.Windows.Forms.ToolStripButton();
            this._lblPcNumber = new System.Windows.Forms.ToolStripLabel();
            this._btnPrevPc = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this._lblSelection = new System.Windows.Forms.ToolStripStatusLabel();
            this._cmsFilter = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._chart = new MCharting.MChart();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ctlTitleBar1 = new MetaboliteLevels.Controls.CtlTitleBar();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.White;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton2,
            this._lblMethod,
            this._mnuPlsrSource,
            this._lblPlsrSource,
            this._mnuTranspose,
            this._lblPcaSource,
            this._btnScoresOrLoadings,
            this._lblPlotView,
            this._btnColour,
            this._lblLegend,
            this.toolStripSeparator2,
            this._mnuCorrections,
            this._lblCorrections,
            this._mnuTrend,
            this._lblAspect,
            this.toolStripSeparator1,
            this._mnuObsFilter,
            this._lblObs,
            this._mnuPeakFilter,
            this._lblPeaks,
            this._btnNavigate,
            this._lblNavigate,
            this._btnMarkAsOutlier,
            this._lblOutlier,
            this.toolStripSeparator5,
            this._btnNextPc,
            this._lblPcNumber,
            this._btnPrevPc,
            this.toolStripSeparator3,
            this.toolStripDropDownButton1});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 66);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(155, 755);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "Flag as outlier";
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
            this.toolStripDropDownButton2.ForeColor = System.Drawing.Color.Purple;
            this.toolStripDropDownButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton2.Image")));
            this.toolStripDropDownButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(152, 36);
            this.toolStripDropDownButton2.Text = "Method";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem1.Image")));
            this.toolStripMenuItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(102, 24);
            this.toolStripMenuItem1.Text = "&PCA";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem2.Image")));
            this.toolStripMenuItem2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(102, 24);
            this.toolStripMenuItem2.Text = "&PLSR";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // _lblMethod
            // 
            this._lblMethod.BackColor = System.Drawing.Color.Transparent;
            this._lblMethod.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblMethod.ForeColor = System.Drawing.Color.Teal;
            this._lblMethod.Margin = new System.Windows.Forms.Padding(48, -16, 0, 0);
            this._lblMethod.Name = "_lblMethod";
            this._lblMethod.Size = new System.Drawing.Size(104, 15);
            this._lblMethod.Text = "(value)";
            this._lblMethod.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _mnuPlsrSource
            // 
            this._mnuPlsrSource.ForeColor = System.Drawing.Color.Purple;
            this._mnuPlsrSource.Image = ((System.Drawing.Image)(resources.GetObject("_mnuPlsrSource.Image")));
            this._mnuPlsrSource.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._mnuPlsrSource.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._mnuPlsrSource.Name = "_mnuPlsrSource";
            this._mnuPlsrSource.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this._mnuPlsrSource.Size = new System.Drawing.Size(152, 36);
            this._mnuPlsrSource.Text = "&Response";
            this._mnuPlsrSource.ToolTipText = "Select PCA input";
            this._mnuPlsrSource.DropDownOpening += new System.EventHandler(this._btnColour_DropDownOpening);
            this._mnuPlsrSource.Click += new System.EventHandler(this._mnuPlsrSource_Click);
            // 
            // _lblPlsrSource
            // 
            this._lblPlsrSource.BackColor = System.Drawing.Color.Transparent;
            this._lblPlsrSource.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblPlsrSource.ForeColor = System.Drawing.Color.Teal;
            this._lblPlsrSource.Margin = new System.Windows.Forms.Padding(48, -16, 0, 0);
            this._lblPlsrSource.Name = "_lblPlsrSource";
            this._lblPlsrSource.Size = new System.Drawing.Size(104, 15);
            this._lblPlsrSource.Text = "(value)";
            this._lblPlsrSource.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _mnuTranspose
            // 
            this._mnuTranspose.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.transposeToShowObservationsToolStripMenuItem,
            this.transposeToShowPeaksToolStripMenuItem});
            this._mnuTranspose.ForeColor = System.Drawing.Color.Purple;
            this._mnuTranspose.Image = ((System.Drawing.Image)(resources.GetObject("_mnuTranspose.Image")));
            this._mnuTranspose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._mnuTranspose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._mnuTranspose.Name = "_mnuTranspose";
            this._mnuTranspose.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this._mnuTranspose.Size = new System.Drawing.Size(152, 36);
            this._mnuTranspose.Text = "&Source";
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
            // _lblPcaSource
            // 
            this._lblPcaSource.BackColor = System.Drawing.Color.Transparent;
            this._lblPcaSource.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblPcaSource.ForeColor = System.Drawing.Color.Teal;
            this._lblPcaSource.Margin = new System.Windows.Forms.Padding(48, -16, 0, 0);
            this._lblPcaSource.Name = "_lblPcaSource";
            this._lblPcaSource.Size = new System.Drawing.Size(104, 15);
            this._lblPcaSource.Text = "(value)";
            this._lblPcaSource.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _btnScoresOrLoadings
            // 
            this._btnScoresOrLoadings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._btnScores,
            this._btnLoadings});
            this._btnScoresOrLoadings.ForeColor = System.Drawing.Color.Purple;
            this._btnScoresOrLoadings.Image = ((System.Drawing.Image)(resources.GetObject("_btnScoresOrLoadings.Image")));
            this._btnScoresOrLoadings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._btnScoresOrLoadings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnScoresOrLoadings.Name = "_btnScoresOrLoadings";
            this._btnScoresOrLoadings.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this._btnScoresOrLoadings.Size = new System.Drawing.Size(152, 36);
            this._btnScoresOrLoadings.Text = "&View";
            this._btnScoresOrLoadings.ToolTipText = "Select plot mode";
            // 
            // _btnScores
            // 
            this._btnScores.Image = ((System.Drawing.Image)(resources.GetObject("_btnScores.Image")));
            this._btnScores.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._btnScores.Name = "_btnScores";
            this._btnScores.Size = new System.Drawing.Size(143, 22);
            this._btnScores.Text = "&Plot scores";
            this._btnScores.Click += new System.EventHandler(this._btnScores_Click);
            // 
            // _btnLoadings
            // 
            this._btnLoadings.Image = ((System.Drawing.Image)(resources.GetObject("_btnLoadings.Image")));
            this._btnLoadings.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._btnLoadings.Name = "_btnLoadings";
            this._btnLoadings.Size = new System.Drawing.Size(143, 22);
            this._btnLoadings.Text = "&Plot loadings";
            this._btnLoadings.Click += new System.EventHandler(this._btnLoadings_Click);
            // 
            // _lblPlotView
            // 
            this._lblPlotView.BackColor = System.Drawing.Color.Transparent;
            this._lblPlotView.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblPlotView.ForeColor = System.Drawing.Color.Teal;
            this._lblPlotView.Margin = new System.Windows.Forms.Padding(48, -16, 0, 0);
            this._lblPlotView.Name = "_lblPlotView";
            this._lblPlotView.Size = new System.Drawing.Size(104, 15);
            this._lblPlotView.Text = "(value)";
            this._lblPlotView.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _btnColour
            // 
            this._btnColour.ForeColor = System.Drawing.Color.Purple;
            this._btnColour.Image = ((System.Drawing.Image)(resources.GetObject("_btnColour.Image")));
            this._btnColour.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._btnColour.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnColour.Name = "_btnColour";
            this._btnColour.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this._btnColour.Size = new System.Drawing.Size(152, 36);
            this._btnColour.Text = "Legend";
            this._btnColour.ToolTipText = "Select peak filter";
            this._btnColour.DropDownOpening += new System.EventHandler(this._btnColour_DropDownOpening);
            // 
            // _lblLegend
            // 
            this._lblLegend.BackColor = System.Drawing.Color.Transparent;
            this._lblLegend.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblLegend.ForeColor = System.Drawing.Color.Teal;
            this._lblLegend.Margin = new System.Windows.Forms.Padding(48, -16, 0, 0);
            this._lblLegend.Name = "_lblLegend";
            this._lblLegend.Size = new System.Drawing.Size(104, 15);
            this._lblLegend.Text = "(value)";
            this._lblLegend.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(152, 6);
            // 
            // _mnuCorrections
            // 
            this._mnuCorrections.ForeColor = System.Drawing.Color.Purple;
            this._mnuCorrections.Image = ((System.Drawing.Image)(resources.GetObject("_mnuCorrections.Image")));
            this._mnuCorrections.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._mnuCorrections.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._mnuCorrections.Name = "_mnuCorrections";
            this._mnuCorrections.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this._mnuCorrections.Size = new System.Drawing.Size(152, 36);
            this._mnuCorrections.Text = "Corrections";
            this._mnuCorrections.ToolTipText = "Select source data";
            this._mnuCorrections.DropDownOpening += new System.EventHandler(this._mnuCorrections_DropDownOpening);
            // 
            // _lblCorrections
            // 
            this._lblCorrections.BackColor = System.Drawing.Color.Transparent;
            this._lblCorrections.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblCorrections.ForeColor = System.Drawing.Color.Teal;
            this._lblCorrections.Margin = new System.Windows.Forms.Padding(48, -16, 0, 0);
            this._lblCorrections.Name = "_lblCorrections";
            this._lblCorrections.Size = new System.Drawing.Size(104, 15);
            this._lblCorrections.Text = "(value)";
            this._lblCorrections.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _mnuTrend
            // 
            this._mnuTrend.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.usetrendLineToolStripMenuItem,
            this.useAllobservationsToolStripMenuItem});
            this._mnuTrend.ForeColor = System.Drawing.Color.Purple;
            this._mnuTrend.Image = ((System.Drawing.Image)(resources.GetObject("_mnuTrend.Image")));
            this._mnuTrend.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._mnuTrend.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._mnuTrend.Name = "_mnuTrend";
            this._mnuTrend.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this._mnuTrend.Size = new System.Drawing.Size(152, 36);
            this._mnuTrend.Text = "Input";
            this._mnuTrend.ToolTipText = "Select source data";
            this._mnuTrend.DropDownOpening += new System.EventHandler(this._mnuTrend_DropDownOpening);
            // 
            // usetrendLineToolStripMenuItem
            // 
            this.usetrendLineToolStripMenuItem.Image = global::MetaboliteLevels.Properties.Resources.MnuTrend;
            this.usetrendLineToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.usetrendLineToolStripMenuItem.Name = "usetrendLineToolStripMenuItem";
            this.usetrendLineToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.usetrendLineToolStripMenuItem.Text = "OBSOLETE";                                                                  
            // 
            // useAllobservationsToolStripMenuItem
            // 
            this.useAllobservationsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("useAllobservationsToolStripMenuItem.Image")));
            this.useAllobservationsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.useAllobservationsToolStripMenuItem.Name = "useAllobservationsToolStripMenuItem";
            this.useAllobservationsToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.useAllobservationsToolStripMenuItem.Text = "OBSOLETE";                                                               
            // 
            // _lblAspect
            // 
            this._lblAspect.BackColor = System.Drawing.Color.Transparent;
            this._lblAspect.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblAspect.ForeColor = System.Drawing.Color.Teal;
            this._lblAspect.Margin = new System.Windows.Forms.Padding(48, -16, 0, 0);
            this._lblAspect.Name = "_lblAspect";
            this._lblAspect.Size = new System.Drawing.Size(104, 15);
            this._lblAspect.Text = "(value)";
            this._lblAspect.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(152, 6);
            // 
            // _mnuObsFilter
            // 
            this._mnuObsFilter.ForeColor = System.Drawing.Color.Purple;
            this._mnuObsFilter.Image = ((System.Drawing.Image)(resources.GetObject("_mnuObsFilter.Image")));
            this._mnuObsFilter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._mnuObsFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._mnuObsFilter.Name = "_mnuObsFilter";
            this._mnuObsFilter.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this._mnuObsFilter.Size = new System.Drawing.Size(152, 36);
            this._mnuObsFilter.Text = "Observations";
            this._mnuObsFilter.ToolTipText = "Select observation filter";
            this._mnuObsFilter.DropDownOpening += new System.EventHandler(this.toolStripDropDownButton3_DropDownOpening);
            // 
            // _lblObs
            // 
            this._lblObs.BackColor = System.Drawing.Color.Transparent;
            this._lblObs.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblObs.ForeColor = System.Drawing.Color.Teal;
            this._lblObs.Margin = new System.Windows.Forms.Padding(48, -16, 0, 0);
            this._lblObs.Name = "_lblObs";
            this._lblObs.Size = new System.Drawing.Size(104, 15);
            this._lblObs.Text = "(value)";
            this._lblObs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _mnuPeakFilter
            // 
            this._mnuPeakFilter.ForeColor = System.Drawing.Color.Purple;
            this._mnuPeakFilter.Image = ((System.Drawing.Image)(resources.GetObject("_mnuPeakFilter.Image")));
            this._mnuPeakFilter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._mnuPeakFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._mnuPeakFilter.Name = "_mnuPeakFilter";
            this._mnuPeakFilter.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this._mnuPeakFilter.Size = new System.Drawing.Size(152, 36);
            this._mnuPeakFilter.Text = "Peaks";
            this._mnuPeakFilter.ToolTipText = "Select peak filter";
            this._mnuPeakFilter.DropDownOpening += new System.EventHandler(this.toolStripDropDownButton1_DropDownOpening);
            // 
            // _lblPeaks
            // 
            this._lblPeaks.BackColor = System.Drawing.Color.Transparent;
            this._lblPeaks.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblPeaks.ForeColor = System.Drawing.Color.Teal;
            this._lblPeaks.Margin = new System.Windows.Forms.Padding(48, -16, 0, 0);
            this._lblPeaks.Name = "_lblPeaks";
            this._lblPeaks.Size = new System.Drawing.Size(104, 15);
            this._lblPeaks.Text = "(value)";
            this._lblPeaks.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _btnNavigate
            // 
            this._btnNavigate.ForeColor = System.Drawing.Color.Purple;
            this._btnNavigate.Image = ((System.Drawing.Image)(resources.GetObject("_btnNavigate.Image")));
            this._btnNavigate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._btnNavigate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnNavigate.Name = "_btnNavigate";
            this._btnNavigate.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this._btnNavigate.Size = new System.Drawing.Size(152, 36);
            this._btnNavigate.Text = "View on main";
            this._btnNavigate.Click += new System.EventHandler(this._btnNavigate_Click);
            // 
            // _lblNavigate
            // 
            this._lblNavigate.BackColor = System.Drawing.Color.Transparent;
            this._lblNavigate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblNavigate.ForeColor = System.Drawing.Color.Teal;
            this._lblNavigate.Margin = new System.Windows.Forms.Padding(48, -16, 0, 0);
            this._lblNavigate.Name = "_lblNavigate";
            this._lblNavigate.Size = new System.Drawing.Size(104, 15);
            this._lblNavigate.Text = "(value)";
            this._lblNavigate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _btnMarkAsOutlier
            // 
            this._btnMarkAsOutlier.ForeColor = System.Drawing.Color.Purple;
            this._btnMarkAsOutlier.Image = ((System.Drawing.Image)(resources.GetObject("_btnMarkAsOutlier.Image")));
            this._btnMarkAsOutlier.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._btnMarkAsOutlier.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnMarkAsOutlier.Name = "_btnMarkAsOutlier";
            this._btnMarkAsOutlier.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this._btnMarkAsOutlier.Size = new System.Drawing.Size(152, 36);
            this._btnMarkAsOutlier.Text = "Mark as outlier";
            this._btnMarkAsOutlier.Click += new System.EventHandler(this._btnMarkAsOutlier_Click);
            // 
            // _lblOutlier
            // 
            this._lblOutlier.BackColor = System.Drawing.Color.Transparent;
            this._lblOutlier.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblOutlier.ForeColor = System.Drawing.Color.Teal;
            this._lblOutlier.Margin = new System.Windows.Forms.Padding(48, -16, 0, 0);
            this._lblOutlier.Name = "_lblOutlier";
            this._lblOutlier.Size = new System.Drawing.Size(104, 15);
            this._lblOutlier.Text = "(value)";
            this._lblOutlier.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(152, 6);
            // 
            // _btnNextPc
            // 
            this._btnNextPc.ForeColor = System.Drawing.Color.Purple;
            this._btnNextPc.Image = ((System.Drawing.Image)(resources.GetObject("_btnNextPc.Image")));
            this._btnNextPc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._btnNextPc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnNextPc.Name = "_btnNextPc";
            this._btnNextPc.Size = new System.Drawing.Size(152, 36);
            this._btnNextPc.Text = "Next component";
            this._btnNextPc.Click += new System.EventHandler(this._btnNextPc_Click);
            // 
            // _lblPcNumber
            // 
            this._lblPcNumber.BackColor = System.Drawing.Color.Transparent;
            this._lblPcNumber.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblPcNumber.ForeColor = System.Drawing.Color.Teal;
            this._lblPcNumber.Margin = new System.Windows.Forms.Padding(48, -10, 0, 0);
            this._lblPcNumber.Name = "_lblPcNumber";
            this._lblPcNumber.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this._lblPcNumber.Size = new System.Drawing.Size(104, 15);
            this._lblPcNumber.Text = "(value)";
            this._lblPcNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _btnPrevPc
            // 
            this._btnPrevPc.ForeColor = System.Drawing.Color.Purple;
            this._btnPrevPc.Image = ((System.Drawing.Image)(resources.GetObject("_btnPrevPc.Image")));
            this._btnPrevPc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._btnPrevPc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnPrevPc.Margin = new System.Windows.Forms.Padding(1, -9, 0, 2);
            this._btnPrevPc.Name = "_btnPrevPc";
            this._btnPrevPc.Size = new System.Drawing.Size(151, 36);
            this._btnPrevPc.Text = "Previous component";
            this._btnPrevPc.Click += new System.EventHandler(this._btnPrevPc_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(152, 6);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.ForeColor = System.Drawing.Color.Purple;
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(152, 36);
            this.toolStripDropDownButton1.Text = "Plot options";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._lblSelection});
            this.statusStrip1.Location = new System.Drawing.Point(0, 821);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1124, 26);
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
            this._chart.Location = new System.Drawing.Point(155, 66);
            this._chart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this._chart.Name = "_chart";
            this._chart.SelectedItem = selection1;
            this._chart.Size = new System.Drawing.Size(969, 755);
            this._chart.TabIndex = 0;
            this._chart.SelectionChanged += new System.EventHandler(this._chart_SelectionChanged);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(155, 66);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(4, 755);
            this.panel1.TabIndex = 4;
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
            this.ctlTitleBar1.Size = new System.Drawing.Size(1124, 66);
            this.ctlTitleBar1.SubText = "";
            this.ctlTitleBar1.TabIndex = 1;
            this.ctlTitleBar1.Text = "#method name";
            this.ctlTitleBar1.WarningText = null;
            // 
            // FrmPca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1124, 847);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this._chart);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.ctlTitleBar1);
            this.Controls.Add(this.statusStrip1);
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
        private System.Windows.Forms.ToolStripButton _btnMarkAsOutlier;
        private System.Windows.Forms.ToolStripDropDownButton _mnuCorrections;
        private System.Windows.Forms.ToolStripDropDownButton _btnColour;
        private System.Windows.Forms.ToolStripLabel _lblPcNumber;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripDropDownButton _mnuPlsrSource;
        private System.Windows.Forms.ToolStripLabel _lblMethod;
        private System.Windows.Forms.ToolStripLabel _lblPlsrSource;
        private System.Windows.Forms.ToolStripLabel _lblPcaSource;
        private System.Windows.Forms.ToolStripLabel _lblOutlier;
        private System.Windows.Forms.ToolStripLabel _lblPlotView;
        private System.Windows.Forms.ToolStripLabel _lblLegend;
        private System.Windows.Forms.ToolStripLabel _lblCorrections;
        private System.Windows.Forms.ToolStripLabel _lblAspect;
        private System.Windows.Forms.ToolStripLabel _lblObs;
        private System.Windows.Forms.ToolStripLabel _lblPeaks;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripButton _btnNavigate;
        private System.Windows.Forms.ToolStripLabel _lblNavigate;
    }
}