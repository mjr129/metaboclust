using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using MCharting;
using MetaboliteLevels.Controls;
using MetaboliteLevels.Data.DataInfo;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Forms;
using MetaboliteLevels.Forms.Editing;
using MetaboliteLevels.Forms.Generic;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Settings;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Viewers.Charts
{
    class ChartHelper : ICoreWatcher
    {
        private const int GROUP_SEPARATION = 2;

        public event EventHandler<ChartSelectionEventArgs> SelectionChanged;

        protected Core _core;
        protected readonly MChart _chart;
        private readonly ISelectionHolder _selector;


        private readonly CaptionBar _captionBar;
        private readonly ToolStrip _menuBar;
        private readonly ToolStripDropDownButton _mnuPlot;
        private readonly ToolStripDropDownButton _mnuCustomText;
        private readonly ToolStripItem _btnNavigateToSelected;
        private readonly ToolStripDropDownButton _mnuSelectedReplicate;
        private readonly ToolStripDropDownButton _mnuSelectedTime;
        private readonly ToolStripDropDownButton _mnuSelectedIntensity;
        private readonly ToolStripDropDownButton _mnuSelectedGroup;
        private readonly ToolStripDropDownButton _mnuSelectedPeak;
        private readonly ToolStripDropDownButton _mnuSelectedSeries;
        private readonly ToolStripMenuItem _mnuSelectionDisplay;
        private readonly ToolStripButton _chkShowPeak;
        private readonly ToolStripButton _chkShowSeries;
        private readonly ToolStripButton _chkShowIntensity;
        private readonly ToolStripButton _chkShowGroup;
        private readonly ToolStripButton _chkShowReplicate;
        private readonly ToolStripButton _chkShowTime;
        private readonly ToolStripButton _chkShowCustom;

        public virtual IVisualisable CurrentPlot
        {
            get { return null; }
        }

        protected void SetCaption(string format, params IVisualisable[] namableItems)
        {
            if (_captionBar != null)
            {
                format = format.Replace("{HIGHLIGHTED}", "highlighted in " + UiControls.ColourToName(_core.Options.Colours.NotableHighlight).ToLower());

                _captionBar.SetText(format, namableItems);
            }
        }

        public void ChangeCore(Core newCore)
        {
            PrepareNewPlot(false, null);
            this._core = newCore;
        }

        public ChartHelper(ISelectionHolder selector, Core core, Control targetSite, bool describePeaks)
        {
            this._selector = selector;
            this._core = core;

            // CHART
            this._chart = new MChart();
            this._chart.Name = "_CHART_IN_" + (targetSite != null ? targetSite.Name : "NOTHING");
            Color c = core.Options.Colours.MinorGrid;
            Color c2 = core.Options.Colours.MajorGrid;

            this._chart.Style.Animate = false;
            this._chart.Style.SelectionColour = Color.Blue;
            this._chart.Style.HighlightColour = Color.Black;
            this._chart.Style.HighlightMinWidth = 5;

            this._chart.SelectionChanged += _chart_SelectionChanged;

            if (targetSite == null)
            {
                return;
            }

            _chart.Dock = DockStyle.Fill;
            targetSite.Controls.Add(_chart);

            // CAPTION BAR
            this._captionBar = new CaptionBar(targetSite, selector);

            // MENU BAR
            this._menuBar = new ToolStrip();
            //_menuBar.Font = FontHelper.RegularFont;
            //_menuBar.ImageScalingSize = new Size(24, 24);
            _menuBar.AutoSize = true;
            _menuBar.Padding = new Padding(0, 0, 0, 0);
            _menuBar.Dock = DockStyle.Top;
            _menuBar.GripStyle = ToolStripGripStyle.Hidden;
            _menuBar.BackColor = Color.FromKnownColor(KnownColor.Control);
            targetSite.Controls.Add(_menuBar);
            _menuBar.SendToBack();

            // PLOT BUTTON
            _mnuPlot = new ToolStripDropDownButton("...");
            _mnuPlot.Visible = false;
            _mnuPlot.AutoSize = true;
            _mnuPlot.ShowDropDownArrow = false;
            this._menuBar.Items.Add(_mnuPlot);

            // USER DETAILS BUTTON
            _mnuCustomText = new ToolStripDropDownButton(Resources.ObjLInfo);
            _mnuCustomText.DropDownItems.Add("Configure...", null, _userDetailsButton_Clicked);
            _mnuCustomText.ShowDropDownArrow = false;
            _mnuCustomText.Visible = false;
            _mnuCustomText.AutoSize = true;
            this._menuBar.Items.Add(_mnuCustomText);

            // SELECTION BUTTONS
            _mnuSelectedIntensity = CreateSelectionButton(Resources.ObjLIntensity);
            _mnuSelectedReplicate = CreateSelectionButton(Resources.ObjLReplicate);
            _mnuSelectedTime = CreateSelectionButton(Resources.ObjLTime);
            _mnuSelectedGroup = CreateSelectionButton(Resources.MnuWarning);
            _mnuSelectedPeak = CreateSelectionButton(Resources.ObjLVariableU);
            _mnuSelectedSeries = CreateSelectionButton(Resources.MnuWarning);

            // PLOT BUTTON ITEMS       
            _mnuPlot.DropDownOpening += _menuButtonMenu_Opening;
            _btnNavigateToSelected = _mnuPlot.DropDownItems.Add("Select this", null, SelectThis_Click);
            _mnuPlot.DropDownItems.Add(new ToolStripSeparator());
            _mnuPlot.DropDownItems.Add("Popout", Resources.MnuEnlarge, Popout_Click);
            _mnuPlot.DropDownItems.Add("Toggle legend", null, ToggleLegend_Click);
            _mnuPlot.DropDownItems.Add("Reset scale (MMB)", null, ResetScale_Click);
            _mnuPlot.DropDownItems.Add("Clear selection (MMB)", null, ResetSel_Click);
            _mnuPlot.DropDownItems.Add("Copy caption text to clipboard", null, ShowCaption_Click);
            _mnuPlot.DropDownItems.Add("Display caption text...", null, ShowCaption2_Click);
            _mnuPlot.DropDownItems.Add("Copy image to clipboard", null, ShowImage_Click);
            _mnuPlot.DropDownItems.Add("Save image...", null, SaveImage_Click);
            _mnuSelectionDisplay = (ToolStripMenuItem)_mnuPlot.DropDownItems.Add("Selection display", null, null);
            _chkShowCustom = AddCheckButton("Custom text", true);
            _chkShowPeak = AddCheckButton("Peak", describePeaks);
            _chkShowSeries = AddCheckButton("Series", true);
            _chkShowGroup = AddCheckButton("Group", describePeaks);
            _chkShowReplicate = AddCheckButton("Replicate", !describePeaks);
            _chkShowTime = AddCheckButton("Time", !describePeaks);
            _chkShowIntensity = AddCheckButton("Intensity", !describePeaks);

            _btnNavigateToSelected.Font = new Font(_btnNavigateToSelected.Font, FontStyle.Bold);
            _btnNavigateToSelected.Enabled = _selector != null;
        }

        private void Popout_Click(object sender, EventArgs e)
        {
            FrmPopoutPlot.Show(this.Chart.FindForm(), _selector, _core, this);
        }

        private ToolStripButton AddCheckButton(string v, bool isChecked)
        {
            ToolStripButton result = new ToolStripButton(v);
            _mnuSelectionDisplay.DropDownItems.Add(result);
            result.Checked = isChecked;
            result.Visible = true;
            result.CheckOnClick = true;
            result.CheckedChanged += Result_CheckedChanged;
            return result;
        }

        private void Result_CheckedChanged(object sender, EventArgs e)
        {
            PerformSelectionActions();
        }

        private ToolStripDropDownButton CreateSelectionButton(Image image)
        {
            ToolStripDropDownButton result = new ToolStripDropDownButton("", image);
            result.DropDownOpening += _selectionButtonMenu_Opening;
            result.Alignment = ToolStripItemAlignment.Right;
            result.Visible = false;
            result.AutoSize = true;
            result.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            result.ShowDropDownArrow = false;
            this._menuBar.Items.Add(result);
            return result;
        }

        private void _chart_SelectionChanged(object sender, EventArgs e)
        {
            PerformSelectionActions();
        }

        private void _userDetailsButton_Clicked(object sender, EventArgs e)
        {
            FrmOptions2.Show(_chart.FindForm(), _core);
        }

        private void _menuButtonMenu_Opening(object sender, EventArgs e)
        {
            _btnNavigateToSelected.Text = "Navigate to " + new VisualisableSelection(CurrentPlot);
        }

        private void _selectionButtonMenu_Opening(object senderu, EventArgs e)
        {
            ToolStripDropDownButton sender = (ToolStripDropDownButton)senderu;

            sender.DropDownItems.Clear();

            HashSet<IVisualisable> items = new HashSet<IVisualisable>();

            foreach (MChart.Series series in _chart.SelectedItem.Series)
            {
                IVisualisable visualisable = series.Tag as IVisualisable;

                if (visualisable == null || items.Contains(visualisable))
                {
                    continue;
                }

                items.Add(visualisable);
                ToolStripItem mnuSelectSelection = sender.DropDownItems.Add("Navigate to " + visualisable.DisplayName, null, SelectThisSelection_Click);
                mnuSelectSelection.Font = new Font(mnuSelectSelection.Font, FontStyle.Bold);
                mnuSelectSelection.Tag = visualisable;
                mnuSelectSelection.Enabled = _selector != null;
            }
        }

        private void SelectThisSelection_Click(object sender, EventArgs e)
        {
            ToolStripItem tsender = (ToolStripItem)sender;
            _selector.Selection = new VisualisableSelection((IVisualisable)tsender.Tag, null);
        }

        private void SelectThis_Click(object sender, EventArgs e)
        {
            _selector.Selection = new VisualisableSelection(CurrentPlot);
        }

        private void ToggleLegend_Click(object sender, EventArgs e)
        {
            switch (_chart.Style.LegendDisplay)
            {
                case MChart.ELegendDisplay.Visible:
                    _chart.Style.LegendDisplay = MChart.ELegendDisplay.Hidden;
                    break;

                case MChart.ELegendDisplay.Hidden:
                    _chart.Style.LegendDisplay = MChart.ELegendDisplay.Selection;
                    break;

                case MChart.ELegendDisplay.Selection:
                    _chart.Style.LegendDisplay = MChart.ELegendDisplay.Visible;
                    break;
            }

            _chart.Redraw();
        }

        private void ResetScale_Click(object sender, EventArgs e)
        {
            _chart.RestoreZoom();
        }

        private void ResetSel_Click(object sender, EventArgs e)
        {
            _chart.SelectedItem = null;
        }

        private void ShowCaption_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(_captionBar.Text);
        }

        private void ShowCaption2_Click(object sender, EventArgs e)
        {
            Form f = _chart.FindForm();

            FrmInputLarge.ShowFixed(f, f.Text, "Plot caption", null, _captionBar.Text);
        }

        private void ShowImage_Click(object sender, EventArgs e)
        {
            using (System.IO.MemoryStream memStream = new System.IO.MemoryStream())
            {
                using (Bitmap bitmap = _chart.DrawToBitmap())
                {
                    UiControls.DrawWatermark(bitmap, _core, CurrentPlot.DisplayName);
                    bitmap.Save(memStream, ImageFormat.Png);
                }

                using (Image img = Image.FromStream(memStream))
                {
                    Clipboard.SetImage(img);
                }
            }
        }

        private void SaveImage_Click(object sender, EventArgs e)
        {
            Form f = _chart.FindForm();

            string fn = f.BrowseForFile(null, UiControls.EFileExtension.PngOrEmf, FileDialogMode.SaveAs, UiControls.EInitialFolder.SavedImages);

            if (fn != null)
            {
                using (Bitmap bitmap = _chart.DrawToBitmap())
                {
                    UiControls.DrawWatermark(bitmap, _core, CurrentPlot.DisplayName);

                    bitmap.Save(fn, fn.ToLower().EndsWith("emf") ? ImageFormat.Emf : ImageFormat.Png);
                }
            }
        }

        public MChart Chart
        {
            get
            {
                return _chart;
            }
        }

        public void ClearPlot()
        {
            _chart.SetPlot(PrepareNewPlot(false, null));
        }

        protected void CompleteNewPlot(MChart.Plot plot)
        {
            _chart.SetPlot(plot);
        }

        /// <summary>
        /// Creates the MChart.Plot object
        /// </summary>
        /// <param name="axes">Include axis text (i.e. not a preview)</param>
        /// <param name="toPlot">What will be plotted</param>
        /// <returns>New MChart.Plot object</returns>
        protected MChart.Plot PrepareNewPlot(bool axes, IVisualisable toPlot)
        {
            MChart.Plot plot = new MChart.Plot();

            CoreOptions.PlotSetup userComments = _core.Options.GetUserText(_core, toPlot);

            if (_mnuPlot != null)
            {
                if (toPlot != null)
                {
                    _mnuPlot.Text = toPlot.DisplayName;
                    _mnuPlot.Image = UiControls.GetImage(toPlot.GetIcon(), true);

                    if (ParseElementCollection.IsNullOrEmpty(userComments.Information))
                    {
                        _mnuCustomText.Text = "(Click here to configure text)";
                    }
                    else
                    {
                        _mnuCustomText.Text = userComments.Information.ConvertToString(toPlot, _core);
                    }

                    _mnuPlot.Visible = true;
                    _mnuCustomText.Visible = _chkShowCustom.Checked;
                }
                else
                {
                    _mnuPlot.Text = "No selection";
                    _mnuPlot.Image = Resources.ObjNone;
                    _mnuPlot.Visible = false;
                    _mnuCustomText.Text = "";
                    _mnuCustomText.Visible = false;
                }
            }

            if (axes)
            {
                plot.Style.BackColour = _core.Options.Colours.PlotBackground;

                if (!ParseElementCollection.IsNullOrEmpty(userComments.AxisX))
                {
                    plot.XLabel = userComments.AxisX.ConvertToString(toPlot, _core);
                }

                if (!ParseElementCollection.IsNullOrEmpty(userComments.AxisY))
                {
                    plot.YLabel = userComments.AxisY.ConvertToString(toPlot, _core);
                }

                if (!ParseElementCollection.IsNullOrEmpty(userComments.Title))
                {
                    plot.Title = userComments.Title.ConvertToString(toPlot, _core);
                }

                if (!ParseElementCollection.IsNullOrEmpty(userComments.SubTitle))
                {
                    plot.SubTitle = userComments.SubTitle.ConvertToString(toPlot, _core);
                }

                plot.Style.AutoTickX = false;
                plot.Style.GridStyle = new Pen(_core.Options.Colours.MinorGrid);
                plot.Style.TickStyle = new Pen(_core.Options.Colours.MajorGrid);
                plot.Style.AxisText = new SolidBrush(_core.Options.Colours.AxisTitle);
            }
            else
            {
                plot.Style.BackColour = _core.Options.Colours.PreviewBackground;
                plot.Style.AutoTickX = false;
                plot.Style.AutoTickY = false;
                plot.Style.GridStyle = null;
                plot.Style.TickStyle = null;
                plot.Style.AxisStyle = null;
            }

            return plot;
        }

        private void PerformSelectionActions()
        {
            // Series are tagged with the variables they represent
            // When multiple series are selected they all have the same variable so no worries about using the first one
            Peak peak = _chart.SelectedItem.SelectedSeries != null ? (Peak)_chart.SelectedItem.SelectedSeries.Tag : null;

            // Points are tagged with the observation
            IntensityInfo dataPoint;

            if (_chart.SelectedItem.DataPoint != null)
            {
                if (_chart.SelectedItem.DataPoint.Tag is IntensityInfo)
                {
                    dataPoint = (IntensityInfo)_chart.SelectedItem.DataPoint.Tag;
                }
                else if (_chart.SelectedItem.DataPoint.Tag is IntensityInfo[])
                {
                    IntensityInfo[] dataPointArray = (IntensityInfo[])_chart.SelectedItem.DataPoint.Tag;
                    dataPoint = dataPointArray != null ? dataPointArray[_chart.SelectedItem.DataIndex] : default(IntensityInfo);
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

            if (SelectionChanged != null)
            {
                string name;

                if (_chart.SelectedItem.Series.Length != 0)
                {
                    // Select the first series names as all series are usually similar
                    name = _chart.SelectedItem.Series[0].Name;

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
                SelectionChanged(this, e);
            }

            // Update text
            if (_mnuSelectedPeak != null)
            {
                if (peak != null && _chkShowPeak.Checked)
                {
                    _mnuSelectedPeak.Text = peak.DisplayName;
                    _mnuSelectedPeak.Image = UiControls.GetImage(((IVisualisable)peak).GetIcon(), true);
                    _mnuSelectedPeak.Visible = true;
                }
                else
                {
                    _mnuSelectedPeak.Visible = false;
                }

                if (dataPoint != null)
                {
                    if (dataPoint.Rep.HasValue && _chkShowReplicate.Checked)
                    {
                        _mnuSelectedReplicate.Text = dataPoint.Rep.Value.ToString();
                        _mnuSelectedReplicate.Visible = true;
                    }
                    else
                    {
                        _mnuSelectedReplicate.Visible = false;
                    }

                    if (dataPoint.Time.HasValue && _chkShowTime.Checked)
                    {
                        _mnuSelectedTime.Text = dataPoint.Time.Value.ToString();
                        _mnuSelectedTime.Visible = true;
                    }
                    else
                    {
                        _mnuSelectedTime.Visible = false;
                    }


                    if (_chkShowIntensity.Checked)
                    {
                        _mnuSelectedIntensity.Text = dataPoint.Intensity.ToString();
                        _mnuSelectedIntensity.Visible = true;
                    }
                    else
                    {
                        _mnuSelectedIntensity.Visible = false;
                    }

                    if (dataPoint.Group != null && _chkShowGroup.Checked)
                    {
                        _mnuSelectedGroup.Text = dataPoint.Group.DisplayName;
                        _mnuSelectedGroup.Image = UiControls.CreateSolidColourImage(true, dataPoint.Group);
                        _mnuSelectedGroup.Visible = true;
                    }
                    else
                    {
                        _mnuSelectedGroup.Visible = false;
                    }

                    if (_chkShowSeries.Checked && _chart.SelectedItem.Series.Length != 0)
                    {
                        _mnuSelectedSeries.Text = _chart.SelectedItem.Series[0].Name;
                        _mnuSelectedSeries.Image = _chart.SelectedItem.Series[0].DrawLegendKey(_menuBar.ImageScalingSize.Width, _menuBar.ImageScalingSize.Height);
                        _mnuSelectedSeries.Visible = true;
                    }
                    else
                    {
                        _mnuSelectedSeries.Visible = false;
                    }
                }
                else
                {
                    _mnuSelectedReplicate.Visible = false;
                    _mnuSelectedTime.Visible = false;
                    _mnuSelectedIntensity.Visible = false;
                    _mnuSelectedSeries.Visible = false;
                    _mnuSelectedGroup.Visible = false;
                }
            }

            // Perform derived-class-specific actions
            OnSelection(peak, dataPoint);
        }

        protected virtual void OnSelection(Peak v, IntensityInfo dp)
        {
            // No action
        }

        public Bitmap CreateBitmap(int width, int height)
        {
            return _chart.DrawToBitmap(width, height);
        }

        protected Dictionary<GroupInfoBase, MChart.Series> DrawLegend(MChart.Plot plot, IEnumerable<GroupInfoBase> viewTypes)
        {
            Dictionary<GroupInfoBase, MChart.Series> result = new Dictionary<GroupInfoBase, MChart.Series>();

            foreach (GroupInfoBase group in viewTypes)
            {
                MChart.Series legendEntry = new MChart.Series();
                legendEntry.Name = group.DisplayName;
                legendEntry.Style.DrawBands = new SolidBrush(group.Colour);
                plot.LegendEntries.Add(legendEntry);
                result.Add(group, legendEntry);
            }

            return result;
        }

        protected void DrawLabels(MChart.Plot plot, bool bConditionsSideBySide, IEnumerable<GroupInfoBase> orderOfGroups)
        {
            if (bConditionsSideBySide)
            {
                foreach (GroupInfoBase ti in orderOfGroups)
                {
                    int x;

                    if (ti is GroupInfo)
                    {
                        x = GetTypeOffset((IEnumerable<GroupInfo>)orderOfGroups, (GroupInfo)ti);
                    }
                    else
                    {
                        x = GetBatchOffset((IEnumerable<BatchInfo>)orderOfGroups, (BatchInfo)ti);
                    }

                    int minX = ti.Range.Min;
                    int maxX = ti.Range.Max;
                    string text = ti.DisplayName;

                    DrawAxisBar(plot, x, minX, maxX, text);
                }
            }
            else
            {
                int minX = _core.TimeRange.Min;
                int maxX = _core.TimeRange.Max;
                int x = 0;
                string text = StringHelper.ArrayToString(orderOfGroups, z => z.DisplayName, ", ");

                DrawAxisBar(plot, x, minX, maxX, text);
            }
        }

        private void DrawAxisBar(MChart.Plot plot, int x, int min, int max, string text)
        {
            if (min == max)
            {
                plot.XTicks.Add(new MChart.Tick(text, x + min, -3, 1));
                plot.XTicks.Add(new MChart.Tick(null, x + min, 2, 0));
                return;
            }

            plot.XTicks.Add(new MChart.Tick(text, x + (min + max) / 2, -3, 0));

            plot.XTicks.Add(new MChart.Tick(min.ToString(), x + min, 2, 1));

            plot.XTicks.Add(new MChart.Tick(max.ToString(), x + max, 2, 1));

            for (int n = min + 1; n < max; n++)
            {
                plot.XTicks.Add(new MChart.Tick(null, x + n, 1, 0));
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
        public readonly Peak peak;
        public readonly IntensityInfo dataPoint;
        public readonly string seriesName;

        public ChartSelectionEventArgs(Peak variable, IntensityInfo dataPoint, string seriesName)
        {
            this.peak = variable;
            this.dataPoint = dataPoint;
            this.seriesName = seriesName;
        }
    }
}
