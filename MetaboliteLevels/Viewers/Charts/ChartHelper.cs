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
    abstract class ChartHelper : ICoreWatcher
    {
        protected Core _core;
        protected MChart _chart;

        public event EventHandler<ChartSelectionEventArgs> SelectionChanged;

        Point _mouseDownStart;
        protected bool _enableHighlightSeries;

        private readonly CaptionBar _captionBar;
        private readonly ToolStrip _menuBar;
        private readonly ToolStripSplitButton _plotButton;
        private readonly ToolStripMenuItem _userDetailsButton;
        //private readonly ToolStripSplitButton _selectionButton;

        private const int GROUP_SEPARATION = 2;
        private Label _selectionLabel;

        private Label _menuLabel;
        private readonly ISelectionHolder _selector;
        private readonly ToolStripItem _btnNavigateToPlot;
        private readonly ToolStripMenuItem _selectionButtonRep;
        private readonly ToolStripMenuItem _selectionButtonTime;
        private readonly ToolStripMenuItem _selectionButtonIntensity;
        private readonly ToolStripMenuItem _selectionButtonGroup;
        private readonly ToolStripMenuItem _selectionButtonPeak;
        private readonly ToolStripMenuItem _selectionButtonSeries;

        public abstract IVisualisable CurrentPlot
        {
            get;
        }

        protected void SetCaption(string format, params IVisualisable[] p)
        {
            if (_captionBar != null)
            {
                _captionBar.SetText(format, p);
            }
        }

        public void ChangeCore(Core newCore)
        {
            PrepareNewPlot(false, null);
            this._core = newCore;
        }

        public ChartHelper(ISelectionHolder selector, Core core, Control targetSite)
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
            _menuBar.ImageScalingSize = new Size(24, 24);
            targetSite.Controls.Add(_menuBar);
            _menuBar.SendToBack();

            // PLOT BUTTON
            _plotButton = new ToolStripSplitButton("No selection");
            _plotButton.Enabled = false;
            _plotButton.AutoSize = true;
            this._menuBar.Items.Add(_plotButton);

            // USER DETAILS BUTTON
            _userDetailsButton = new ToolStripMenuItem("...");                      
            _userDetailsButton.DropDownItems.Add("Configure...", null, _userDetailsButton_Clicked);
            _userDetailsButton.AutoSize = true;
            this._menuBar.Items.Add(_userDetailsButton);

            // SELECTION BUTTONS
            _selectionButtonIntensity = CreateSelectionButton(Resources.ObjLIntensity);
            _selectionButtonRep = CreateSelectionButton(Resources.ObjLReplicate);
            _selectionButtonTime = CreateSelectionButton(Resources.ObjLTime);
            _selectionButtonGroup = CreateSelectionButton(Resources.ObjPoint);
            _selectionButtonPeak = CreateSelectionButton(Resources.ObjLVariableU);
            _selectionButtonSeries = CreateSelectionButton(Resources.ObjLSeries);

            // PLOT BUTTON ITEMS       
            _plotButton.DropDownOpening += _menuButtonMenu_Opening;
            _plotButton.Click += (x, y) => _plotButton.ShowDropDown();
            _btnNavigateToPlot = _plotButton.DropDownItems.Add("Select this", null, SelectThis_Click);
            _plotButton.DropDownItems.Add(new ToolStripSeparator());
            //_plotButton.DropDownItems.Add("Toggle legend", null, ToggleLegend_Click);
            _plotButton.DropDownItems.Add("Reset scale (MMB)", null, ResetScale_Click);
            _plotButton.DropDownItems.Add("Clear selection (MMB)", null, ResetSel_Click);
            _plotButton.DropDownItems.Add("Copy caption text to clipboard", null, ShowCaption_Click);
            _plotButton.DropDownItems.Add("Display caption text...", null, ShowCaption2_Click);
            _plotButton.DropDownItems.Add("Copy image to clipboard", null, ShowImage_Click);
            _plotButton.DropDownItems.Add("Save image...", null, SaveImage_Click);

            _btnNavigateToPlot.Font = new Font(_btnNavigateToPlot.Font, FontStyle.Bold);
            _btnNavigateToPlot.Enabled = _selector != null;        
        }

        private ToolStripMenuItem CreateSelectionButton(Image image)
        {               
            ToolStripMenuItem result = new ToolStripMenuItem("", image, _selectionButtonMenu_Opening);
            result.Alignment = ToolStripItemAlignment.Right;
            result.Visible = false;
            result.AutoSize = true;
            result.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            this._menuBar.Items.Add(result);
            return result;
        }

        private void _chart_SelectionChanged(object sender, EventArgs e)
        {
            PerformSelectionActions();
        }

        private void _userDetailsButton_Clicked(object sender, EventArgs e)
        {
            FrmOptions.Show(_chart.FindForm(), _core);
        }

        private void _menuButtonMenu_Opening(object sender, EventArgs e)
        {
            _btnNavigateToPlot.Text = "Navigate to " + new VisualisableSelection(CurrentPlot, EActivateOrigin.External);
        }

        private void _selectionButtonMenu_Opening(object senderu, EventArgs e)
        {
            ToolStripMenuItem sender = (ToolStripMenuItem)senderu;

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
            _selector.Selection = new VisualisableSelection((IVisualisable)tsender.Tag, null, EActivateOrigin.External);
        }

        private void SelectThis_Click(object sender, EventArgs e)
        {
            _selector.Selection = new VisualisableSelection(CurrentPlot, EActivateOrigin.External);
        }

        // TODO: Put back in
        //private void ToggleLegend_Click(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

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
                _chart.DrawToBitmap().Save(memStream, ImageFormat.Png);

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
                _chart.DrawToBitmap().Save(fn, fn.ToLower().EndsWith("emf") ? ImageFormat.Emf : ImageFormat.Png);
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

        protected MChart.Plot PrepareNewPlot(bool axes, IVisualisable toPlot)
        {
            MChart.Plot plot = new MChart.Plot();

            CoreOptions.PlotSetup userComments = _core.Options.GetUserText(_core, toPlot);

            if (_plotButton != null)
            {
                if (toPlot != null)
                {
                    _plotButton.Text = toPlot.DisplayName;
                    _plotButton.Image = UiControls.GetImage(toPlot.GetIcon(), true);

                    if (ParseElementCollection.IsNullOrEmpty(userComments.Information))
                    {
                        _userDetailsButton.Text = "(Click here to configure text)";
                    }
                    else
                    {
                        _userDetailsButton.Text = userComments.Information.ConvertToString(toPlot, _core);
                    }

                    _plotButton.Enabled = true;
                    _userDetailsButton.Enabled = true;
                }
                else
                {
                    _plotButton.Text = "No selection";
                    _plotButton.Image = Resources.ObjNone;
                    _plotButton.Enabled = false;
                    _userDetailsButton.Text = "";
                    _userDetailsButton.Enabled = false;
                }
            }


            if (axes)
            {
                if (!ParseElementCollection.IsNullOrEmpty(userComments.AxisX))
                {
                    plot.XLabel = userComments.AxisX.ConvertToString(toPlot, _core);
                }

                if (!ParseElementCollection.IsNullOrEmpty(userComments.AxisY))
                {
                    plot.YLabel = userComments.AxisY.ConvertToString(toPlot, _core);
                }
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
            if (_selectionButtonPeak != null)
            {
                if (peak != null)
                {
                    _selectionButtonPeak.Text = peak.DisplayName;
                    _selectionButtonPeak.Image = UiControls.GetImage(((IVisualisable)peak).GetIcon(), true);
                    _selectionButtonPeak.Visible = true;
                }
                else
                {
                    _selectionButtonPeak.Visible = false;
                }

                if (dataPoint != null)
                {
                    if (dataPoint.Rep.HasValue)
                    {
                        _selectionButtonRep.Text = dataPoint.Rep.Value.ToString();
                        _selectionButtonRep.Visible = true;
                    }
                    else
                    {
                        _selectionButtonRep.Visible = false;
                    }

                    if (dataPoint.Time.HasValue)
                    {
                        _selectionButtonTime.Text = dataPoint.Time.Value.ToString();
                        _selectionButtonTime.Visible = true;
                    }
                    else
                    {
                        _selectionButtonTime.Visible = false;
                    }


                    _selectionButtonIntensity.Text = dataPoint.Intensity.ToString();
                    _selectionButtonIntensity.Visible = true;

                    if (dataPoint.Group != null)
                    {
                        _selectionButtonGroup.Text = dataPoint.Group.Name;
                        _selectionButtonGroup.Image = UiControls.CreateSolidColourImage(true, dataPoint.Group);
                        _selectionButtonGroup.Visible = true;
                    }
                    else
                    {
                        _selectionButtonGroup.Visible = false;
                    }

                    _selectionButtonSeries.Text = _chart.SelectedItem.Series[0].Name;
                    _selectionButtonSeries.Visible = true;
                }
                else
                {
                    _selectionButtonRep.Visible = false;
                    _selectionButtonTime.Visible = false;
                    _selectionButtonIntensity.Visible = false;
                    _selectionButtonSeries.Visible = false;
                    _selectionButtonGroup.Visible = false;
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
                    string text = ti.Name;

                    DrawAxisBar(plot, x, minX, maxX, text);
                }
            }
            else
            {
                int minX = _core.TimeRange.Min;
                int maxX = _core.TimeRange.Max;
                int x = 0;
                string text = StringHelper.ArrayToString(orderOfGroups, z => z.Name, ", ");

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
