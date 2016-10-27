using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MCharting;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Session.Main;
using MetaboliteLevels.Gui.Datatypes;
using MetaboliteLevels.Gui.Forms.Activities;
using MetaboliteLevels.Gui.Forms.Editing;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Utilities;
using MGui.Datatypes;
using MGui.Helpers;

namespace MetaboliteLevels.Gui.Controls.Charts
{
    /// <summary>
    /// Base class for charts (plot of things)
    /// </summary>
    class ChartHelper : ICoreWatcher
    {
        private const int                       GROUP_SEPARATION = 2;

        public event EventHandler<ChartSelectionEventArgs> SelectionChanged;

        protected Core                           _core;
        protected readonly MChart                _chart;
        protected readonly ISelectionHolder      _selector;
                                                                 
        private readonly CaptionBar              _captionBar;
        private readonly ToolStrip               _menuBar;
        private readonly ToolStripDropDownButton _mnuPlot;
        private readonly ToolStripDropDownButton _mnuCustomText;
        private readonly ToolStripItem           _btnNavigateToSelected;
        private readonly ToolStripDropDownButton _mnuSelectedReplicate;
        private readonly ToolStripDropDownButton _mnuSelectedTime;
        private readonly ToolStripDropDownButton _mnuSelectedIntensity;
        private readonly ToolStripDropDownButton _mnuSelectedGroup;
        private readonly ToolStripDropDownButton _mnuSelectedPeak;
        private readonly ToolStripDropDownButton _mnuSelectedSeries;
        private readonly ToolStripMenuItem       _mnuSelectionDisplay;
        private readonly ToolStripMenuItem       _chkShowPeak;
        private readonly ToolStripMenuItem       _chkShowSeries;
        private readonly ToolStripMenuItem       _chkShowIntensity;
        private readonly ToolStripMenuItem       _chkShowGroup;
        private readonly ToolStripMenuItem       _chkShowReplicate;
        private readonly ToolStripMenuItem       _chkShowTime;
        private readonly ToolStripMenuItem       _chkShowCustom;

        public virtual Visualisable CurrentPlot
        {
            get { return null; }
        }

        protected void SetCaption(string format, params Associational[] namableItems)
        {
            if (this._captionBar != null)
            {
                format = format.Replace("{HIGHLIGHTED}", "highlighted in " + ColourHelper.ColourToName(this._core.Options.Colours.NotableHighlight).ToLower());

                this._captionBar.SetText(format, namableItems);
            }
        }

        public void ChangeCore(Core newCore)
        {
            this.PrepareNewPlot(false, null, null);
            this._core = newCore;
        }

        public ChartHelper( ISelectionHolder selector, Core core, Control targetSite, bool describePeaks)
        {
            this._selector = selector;
            this._core = core;

            Color c = core.Options.Colours.MinorGrid;
            Color c2 = core.Options.Colours.MajorGrid;


            // CHART
            this._chart      = new MChart();
            this._chart.Name = "_CHART_IN_" + (targetSite != null ? targetSite.Name : "NOTHING");

            this._chart.Style.Animate           = false;
            this._chart.Style.SelectionColour   = Color.Blue;
            this._chart.Style.HighlightColour   = Color.Black;
            this._chart.Style.HighlightMinWidth = 5;

            this._chart.SelectionChanged += this._chart_SelectionChanged;

            if (targetSite == null)
            {
                return;
            }

            this._chart.Dock = DockStyle.Fill;
            targetSite.Controls.Add(this._chart);

            // CAPTION BAR
            this._captionBar = new CaptionBar(targetSite, selector);

            // MENU BAR
            this._menuBar = new ToolStrip();
            //_menuBar.Font = FontHelper.RegularFont;
            //_menuBar.ImageScalingSize = new Size(24, 24);
            this._menuBar.AutoSize  = true;
            this._menuBar.Padding   = new Padding(0, 0, 0, 0);
            this._menuBar.Dock      = DockStyle.Top;
            this._menuBar.GripStyle = ToolStripGripStyle.Hidden;
            this._menuBar.BackColor = Color.FromKnownColor(KnownColor.Control);
            targetSite.Controls.Add(this._menuBar);
            this._menuBar.SendToBack();

            // PLOT BUTTON
            this._mnuPlot = new ToolStripDropDownButton("...");
            this._mnuPlot.Visible           = false;
            this._mnuPlot.AutoSize          = true;
            this._mnuPlot.ShowDropDownArrow = false;
            this._menuBar.Items.Add(this._mnuPlot);

            // USER DETAILS BUTTON
            this._mnuCustomText = new ToolStripDropDownButton(Resources.IconInformation);
            this._mnuCustomText.DropDownItems.Add("Copy", Resources.MnuCopy, this._userDetailsCopyButton_Clicked);
            this._mnuCustomText.DropDownItems.Add( "Configure...", null, this._userDetailsButton_Clicked );
            this._mnuCustomText.ShowDropDownArrow = false;
            this._mnuCustomText.Visible           = false;
            this._mnuCustomText.AutoSize          = true;
            this._menuBar.Items.Add(this._mnuCustomText);

            // GENERIC CHART BAR
            this._chart.AddControls( this._menuBar );

            // SELECTION BUTTONS
            this._mnuSelectedIntensity = this.CreateSelectionButton(Resources.IconIntensity);
            this._mnuSelectedReplicate = this.CreateSelectionButton(Resources.IconReplicate);
            this._mnuSelectedTime      = this.CreateSelectionButton(Resources.IconTime);
            this._mnuSelectedGroup     = this.CreateSelectionButton(Resources.MnuWarning);
            this._mnuSelectedPeak      = this.CreateSelectionButton(Resources.IconPeak);
            this._mnuSelectedSeries    = this.CreateSelectionButton(Resources.MnuWarning);

            // PLOT BUTTON ITEMS       
            this._mnuPlot.DropDownOpening += this._menuButtonMenu_Opening;

            /**** MENU *****/
            /* select xxxx */ this._btnNavigateToSelected = this._mnuPlot.DropDownItems.Add("Select this", null, this._menu_selectThis);
            /* ----------- */  this._mnuPlot.DropDownItems.Add(new ToolStripSeparator());
            /* popout      */ this._mnuPlot.DropDownItems .Add("Popout", Resources.MnuEnlarge, this.menu_popout_Click);
            /* export      */ this._mnuPlot.DropDownItems .Add("Export...", null, this._menu_exportImage);
            /* display:    */ this._mnuSelectionDisplay   = (ToolStripMenuItem)this._mnuPlot.DropDownItems.Add("Selection display", null, null);
            /*   custom    */ this._chkShowCustom         = this.AddCheckButton("Custom text", true);
            /*   peak      */ this._chkShowPeak           = this.AddCheckButton("Peak", describePeaks);
            /*   series    */ this._chkShowSeries         = this.AddCheckButton("Series", true);
            /*   group     */ this._chkShowGroup          = this.AddCheckButton("Group", describePeaks);
            /*   replicate */ this._chkShowReplicate      = this.AddCheckButton("Replicate", !describePeaks);
            /*   time      */ this._chkShowTime           = this.AddCheckButton("Time", !describePeaks);
            /*   intensity */ this._chkShowIntensity      = this.AddCheckButton("Intensity", !describePeaks);                                    

            this._btnNavigateToSelected.Font = new Font(this._btnNavigateToSelected.Font, FontStyle.Bold);
            this._btnNavigateToSelected.Enabled = this._selector != null;
        }   

        private void menu_popout_Click(object sender, EventArgs e)
        {
            FrmPopoutPlot.Show(this.Chart.FindForm(), this._selector, this._core, this);
        }

        private ToolStripMenuItem AddCheckButton(string v, bool isChecked)
        {
            ToolStripMenuItem result = new ToolStripMenuItem(v);
            this._mnuSelectionDisplay.DropDownItems.Add(result);
            result.Checked           = isChecked;
            result.Visible           = true;
            result.CheckOnClick      = true;
            result.CheckedChanged += this.Result_CheckedChanged;
            return result;
        }

        private void Result_CheckedChanged(object sender, EventArgs e)
        {
            this.PerformSelectionActions();
        }

        private ToolStripDropDownButton CreateSelectionButton(Image image)
        {
            ToolStripDropDownButton result = new ToolStripDropDownButton("", image);
            result.DropDownOpening        += this._selectionButtonMenu_Opening;
            result.Alignment               = ToolStripItemAlignment.Right;
            result.Visible                 = false;
            result.AutoSize                = true;
            result.DisplayStyle            = ToolStripItemDisplayStyle.ImageAndText;
            result.ShowDropDownArrow       = false;
            this._menuBar.Items.Add(result);
            return result;
        }

        private void _chart_SelectionChanged(object sender, EventArgs e)
        {
            this.PerformSelectionActions();
        }

        private void _userDetailsCopyButton_Clicked( object sender, EventArgs e )
        {                            
            Clipboard.SetText( this._mnuCustomText.Text );
        }

        private void _userDetailsButton_Clicked(object sender, EventArgs e)
        {
            FrmEditCoreOptions.Show(this._chart.FindForm(), this._core, false);
        }

        private void _menuButtonMenu_Opening(object sender, EventArgs e)
        {
            this._btnNavigateToSelected.Text = "Navigate to \"" + new VisualisableSelection( this.CurrentPlot ) + "\"";
        }

        private void _selectionButtonMenu_Opening(object senderu, EventArgs e)
        {
            ToolStripDropDownButton sender = (ToolStripDropDownButton)senderu;

            sender.DropDownItems.Clear();

            HashSet<Visualisable> items = new HashSet<Visualisable>();

            foreach (MCharting.Series series in this._chart.SelectedItem.Series)
            {
                Visualisable visualisable = series.Tag as Visualisable;

                if (visualisable == null || items.Contains(visualisable))
                {
                    continue;
                }

                items.Add(visualisable);
                ToolStripItem mnuSelectSelection = sender.DropDownItems.Add("Navigate to " + visualisable.DisplayName, null, this._menu_navigateTo);
                mnuSelectSelection.Font          = new Font(mnuSelectSelection.Font, FontStyle.Bold);
                mnuSelectSelection.Tag           = visualisable;
                mnuSelectSelection.Enabled       = this._selector != null;
            }
        }

        private void _menu_navigateTo(object sender, EventArgs e)
        {
            ToolStripItem tsender = (ToolStripItem)sender;
            this._selector.Selection = new VisualisableSelection((Visualisable)tsender.Tag, null);
        }

        private void _menu_selectThis(object sender, EventArgs e)
        {
            this._selector.Selection = new VisualisableSelection(this.CurrentPlot);
        }  

        private void _menu_exportImage(object sender, EventArgs e)
        {
            FrmActExportImage.Show(this._chart.FindForm(), this._core, this, this.CurrentPlot.DisplayName, this._captionBar.Text);
        }

        public MChart Chart
        {
            get
            {
                return this._chart;
            }
        }

        public void ClearPlot()
        {
            this._chart.SetPlot(this.PrepareNewPlot(false, null, null));
        }

        protected void CompleteNewPlot( MCharting.Plot plot)
        {
            this._chart.SetPlot(plot);
        }

        /// <summary>
        /// Creates the MChart.Plot object
        /// </summary>
        /// <param name="axes">Include axis text (i.e. not a preview)</param>
        /// <param name="toPlot">What will be plotted</param>
        /// <returns>New MChart.Plot object</returns>
        protected MCharting.Plot PrepareNewPlot(bool axes, Associational toPlot, IntensityMatrix sourceMatrix)
        {
            MCharting.Plot plot = new MCharting.Plot();

            PlotSetup setup = this._core.Options.GetPlotSetup(this._core, toPlot);

            if (this._mnuPlot != null)
            {
                if (toPlot != null)
                {
                    this._mnuPlot.Text  = toPlot.DisplayName;
                    this._mnuPlot.Image = UiControls.GetImage(toPlot.Icon, true);

                    if (ParseElementCollection.IsNullOrEmpty(setup.Information))
                    {
                        this._mnuCustomText.Text = "(Click here to configure text)";
                    }
                    else
                    {
                        this._mnuCustomText.Text = setup.Information.ConvertToString(toPlot, this._core);
                    }

                    this._mnuPlot.Visible = true;
                    this._mnuCustomText.Visible = this._chkShowCustom.Checked;
                }
                else
                {
                    this._mnuPlot.Text          = "No selection";
                    this._mnuPlot.Image         = Resources.IconTransparent;
                    this._mnuPlot.Visible       = false;
                    this._mnuCustomText.Text    = "";
                    this._mnuCustomText.Visible = false;
                }
            }

            if (this._core.Options.NoAxes)
            {
                axes = false;                                               
            }

            if (axes)
            {
                plot.Style.BackColour = this._core.Options.Colours.PlotBackground;

                if (!ParseElementCollection.IsNullOrEmpty(setup.AxisX))
                {
                    plot.XLabel = setup.AxisX.ConvertToString(toPlot, this._core);
                }

                if (!ParseElementCollection.IsNullOrEmpty(setup.AxisY))
                {
                    plot.YLabel = setup.AxisY.ConvertToString(toPlot, this._core);
                }

                if (!ParseElementCollection.IsNullOrEmpty(setup.Title))
                {
                    plot.Title = setup.Title.ConvertToString(toPlot, this._core);
                }

                if (!ParseElementCollection.IsNullOrEmpty(setup.SubTitle))
                {
                    plot.SubTitle = setup.SubTitle.ConvertToString(toPlot, this._core);
                }

                plot.Style.AutoTickX = false;
                plot.Style.GridStyle = new Pen(this._core.Options.Colours.MinorGrid, this._core.Options.LineWidth);
                plot.Style.TickStyle = new Pen(this._core.Options.Colours.MajorGrid, this._core.Options.LineWidth);
                plot.Style.AxisText  = new SolidBrush(this._core.Options.Colours.AxisTitle);
                this.Chart.Style.Margin   = new Padding(this._core.Options.Margin);
            }
            else
            {
                plot.Style.BackColour = this._core.Options.Colours.PreviewBackground;
                plot.Style.AutoTickX  = false;
                plot.Style.AutoTickY  = false;
                plot.Style.GridStyle  = null;
                plot.Style.TickStyle  = null;
                plot.Style.AxisStyle = null;
                this.Chart.Style.Margin = Padding.Empty;
            }

            plot.Style.XMin = setup.RangeXMin.GetValue();
            plot.Style.XMax = setup.RangeXMax.GetValue();
            plot.Style.YMin = setup.RangeYMin.GetValue();
            plot.Style.YMax = setup.RangeYMax.GetValue();
                            
            // Min/max of IM
            if (sourceMatrix != null)
            {
                if (setup.RangeYMin.Mode == EAxisRange.General)
                {
                    if (setup.RangeYMax.Mode == EAxisRange.General)
                    {
                        var range = sourceMatrix.AllValues.Range();
                        plot.Style.YMin = range.Min;
                        plot.Style.YMax = range.Max;
                    }
                    else
                    {
                        plot.Style.YMin = sourceMatrix.AllValues.Min();
                    }
                }
                else if (setup.RangeYMax.Mode == EAxisRange.General)
                {
                    plot.Style.YMax = sourceMatrix.AllValues.Max();
                }
            }

            switch (setup.RangeXMin.Mode)
            {
                case EAxisRange.Fixed:
                    plot.Style.XMin = setup.RangeXMin.Value;
                    break;

                case EAxisRange.Automatic:
                case EAxisRange.General:
                    plot.Style.XMin = null;
                    break;
            }

            
            //plot.Style.XMin = setup.RangeXMin.Get( toPlot );
            //plot.Style.XMax = setup.RangeXMax.Get( toPlot );
            //plot.Style.YMin = setup.RangeYMin.Get( toPlot );
            //plot.Style.YMax = setup.RangeYMax.Get( toPlot );

            return plot;
        }

        private void PerformSelectionActions()
        {
            // Series are tagged with the variables they represent
            // When multiple series are selected they all have the same variable so no worries about using the first one
            Peak peak = this._chart.SelectedItem.SelectedSeries != null ? (Peak)this._chart.SelectedItem.SelectedSeries.Tag : null;

            // Points are tagged with the observation
            IntensityInfo dataPoint;

            if (this._chart.SelectedItem.DataPoint != null)
            {
                if (this._chart.SelectedItem.DataPoint.Tag is IntensityInfo)
                {
                    dataPoint = (IntensityInfo)this._chart.SelectedItem.DataPoint.Tag;
                }
                else if (this._chart.SelectedItem.DataPoint.Tag is IntensityInfo[])
                {
                    IntensityInfo[] dataPointArray = (IntensityInfo[])this._chart.SelectedItem.DataPoint.Tag;
                    dataPoint = dataPointArray != null ? dataPointArray[this._chart.SelectedItem.YIndex] : default(IntensityInfo);
                }
                else
                {
                    UiControls.Assert(false, "Unexpected data point format.");
                    dataPoint = null;
                }
            }
            else
            {
                // Clear selection
                dataPoint = null;
            }

            if (this.SelectionChanged != null)
            {
                string name;

                if (this._chart.SelectedItem.Series.Length != 0)
                {
                    // Select the first series names as all series are usually similar
                    name = this._chart.SelectedItem.Series[0].Name;

                    // Trim off everything after the | as this is just used so the chart doesn't complain
                    if (name.Contains("|"))
                    {
                        name = name.Substring(0, name.LastIndexOf('|')).Trim();
                    }
                }
                else
                {
                    name = null;
                }

                ChartSelectionEventArgs e = new ChartSelectionEventArgs(peak, dataPoint, name);
                this.SelectionChanged(this, e);
            }

            // Update text
            if (this._mnuSelectedPeak != null)
            {
                if (peak != null && this._chkShowPeak.Checked)
                {
                    this._mnuSelectedPeak.Text = peak.DisplayName;
                    this._mnuSelectedPeak.Image = UiControls.GetImage(((Visualisable)peak).Icon, true);
                    this._mnuSelectedPeak.Visible = true;
                }
                else
                {
                    this._mnuSelectedPeak.Visible = false;
                }

                if (dataPoint != null)
                {
                    if (dataPoint.Rep.HasValue && this._chkShowReplicate.Checked)
                    {
                        this._mnuSelectedReplicate.Text = dataPoint.Rep.Value.ToString();
                        this._mnuSelectedReplicate.Visible = true;
                    }
                    else
                    {
                        this._mnuSelectedReplicate.Visible = false;
                    }

                    if (dataPoint.Time.HasValue && this._chkShowTime.Checked)
                    {
                        this._mnuSelectedTime.Text = dataPoint.Time.Value.ToString();
                        this._mnuSelectedTime.Visible = true;
                    }
                    else
                    {
                        this._mnuSelectedTime.Visible = false;
                    }


                    if (this._chkShowIntensity.Checked)
                    {
                        this._mnuSelectedIntensity.Text = dataPoint.Intensity.ToString();
                        this._mnuSelectedIntensity.Visible = true;
                    }
                    else
                    {
                        this._mnuSelectedIntensity.Visible = false;
                    }

                    if (dataPoint.Group != null && this._chkShowGroup.Checked)
                    {
                        this._mnuSelectedGroup.Text    = dataPoint.Group.DisplayName;
                        this._mnuSelectedGroup.Image   = UiControls.CreateExperimentalGroupImage(true, dataPoint.Group, false);
                        this._mnuSelectedGroup.Visible = true;
                    }
                    else
                    {
                        this._mnuSelectedGroup.Visible = false;
                    }

                    if (this._chkShowSeries.Checked && this._chart.SelectedItem.Series.Length != 0)
                    {
                        this._mnuSelectedSeries.Text    = this._chart.SelectedItem.Series[0].Name;
                        this._mnuSelectedSeries.Image   = this._chart.SelectedItem.Series[0].DrawLegendKey(this._menuBar.ImageScalingSize.Width, this._menuBar.ImageScalingSize.Height);
                        this._mnuSelectedSeries.Visible = true;
                    }
                    else
                    {
                        this._mnuSelectedSeries.Visible = false;
                    }
                }
                else
                {
                    this._mnuSelectedReplicate.Visible = false;
                    this._mnuSelectedTime.Visible      = false;
                    this._mnuSelectedIntensity.Visible = false;
                    this._mnuSelectedSeries.Visible    = false;
                    this._mnuSelectedGroup.Visible     = false;
                }
            }

            // Perform derived-class-specific actions
            this.OnSelection(peak, dataPoint);
        }

        protected virtual void OnSelection(Peak v, IntensityInfo dp)
        {
            // No action
        }

        public Bitmap CreateBitmap(int width, int height)
        {
            return this._chart.DrawToBitmap(width, height);
        }

        protected Dictionary<GroupInfoBase, MCharting.Series> DrawLegend( MCharting.Plot plot, IEnumerable<GroupInfoBase> viewTypes)
        {
            Dictionary<GroupInfoBase, MCharting.Series> result = new Dictionary<GroupInfoBase, MCharting.Series>();

            foreach (GroupInfoBase group in viewTypes)
            {
                MCharting.Series legendEntry    = new MCharting.Series();
                legendEntry.Name             = group.DisplayName;
                legendEntry.Style.DrawVBands = new SolidBrush(group.Colour);
                plot.LegendEntries.Add(legendEntry);
                result.Add(group, legendEntry);
            }

            return result;
        }

        protected void DrawLabels( MCharting.Plot plot, bool bConditionsSideBySide, IEnumerable<GroupInfoBase> orderOfGroups, bool includeText)
        {
            if (bConditionsSideBySide)
            {
                foreach (GroupInfoBase ti in orderOfGroups)
                {
                    int x;

                    if (ti is GroupInfo)
                    {
                        x = this.GetTypeOffset((IEnumerable<GroupInfo>)orderOfGroups, (GroupInfo)ti);
                    }
                    else // if (ti is BatchInfo)
                    {
                        x = this.GetBatchOffset((IEnumerable<BatchInfo>)orderOfGroups, (BatchInfo)ti);
                    }

                    int minX    = ti.Range.Min;
                    int maxX    = ti.Range.Max;
                    string text = includeText ? ti.DisplayName : string.Empty;

                    this.DrawAxisBar(plot, x, minX, maxX, text);
                }
            }
            else
            {
                int minX = this._core.TimeRange.Min;
                int maxX = this._core.TimeRange.Max;
                int x = 0;
                string text = includeText ? StringHelper.ArrayToString( orderOfGroups, z => z.DisplayName, ", " ) : string.Empty;

                this.DrawAxisBar(plot, x, minX, maxX, text);
            }
        }

        private void DrawAxisBar(MCharting.Plot plot, int x, int min, int max, string text)
        {
            if (min == max)
            {
                plot.XTicks.Add(new MCharting.Tick(text, x + min, -3, 1));
                plot.XTicks.Add(new MCharting.Tick(null, x + min, 2, 0));
                return;
            }

            plot.XTicks.Add(new MCharting.Tick(text, x + (min + max) / 2, -3, 0));

            plot.XTicks.Add(new MCharting.Tick(min.ToString(), x + min, 2, 1));

            plot.XTicks.Add(new MCharting.Tick(max.ToString(), x + max, 2, 1));

            for (int n = min + 1; n < max; n++)
            {
                plot.XTicks.Add(new MCharting.Tick(null, x + n, 1, 0));
            }
        }

        protected int GetTypeOffset(IEnumerable<GroupInfo> orderOfTypes, GroupInfo typeToGet)
        {
            int x = 0;

            foreach (var t in orderOfTypes)
            {
                if (t == typeToGet)
                {
                    return x - t.Range.Min;
                }

                x += t.Range.Count + GROUP_SEPARATION;
            }

            throw new InvalidOperationException("GetTypeOffset: Could not find type: " + typeToGet);
        }

        protected int GetBatchOffset(IEnumerable<BatchInfo> orderOfBatches, BatchInfo batchToGet)
        {
            int x = 0;

            foreach (var b in orderOfBatches)
            {
                if (b == batchToGet)
                {
                    return x - b.Range.Min;
                }

                x += b.Range.Count + GROUP_SEPARATION;
            }

            throw new InvalidOperationException("GetBatchOffset: Could not find batch: " + batchToGet);
        }
    }

    class ChartSelectionEventArgs : EventArgs
    {
        public readonly Peak _peak;
        public readonly IntensityInfo _dataPoint;
        public readonly string _seriesName;

        public ChartSelectionEventArgs(Peak variable, IntensityInfo dataPoint, string seriesName)
        {
            this._peak       = variable;
            this._dataPoint  = dataPoint;
            this._seriesName = seriesName;
        }
    }
}
