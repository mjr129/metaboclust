using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
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
        protected Chart _chart;

        protected List<Series> _selectedSeriesList = new List<Series>();
        protected Dictionary<Series, Tuple<Color, int>> _selectedSeriesColours = new Dictionary<Series, Tuple<Color, int>>();
        protected DataPoint _selectedPoint;
        protected int _selectedPointIndex;

        public event EventHandler<ChartSelectionEventArgs> SelectionChanged;

        Point _mouseDownStart;
        protected bool _enableHighlightSeries;

        private readonly CaptionBar _captionBar;
        private readonly ToolStrip _menuBar;
        private readonly ToolStripSplitButton _plotButton;
        private readonly ToolStripSplitButton _userDetailsButton;
        private readonly ToolStripSplitButton _selectionButton;

        private const int GROUP_SEPARATION = 2;
        private Label _selectionLabel;

        private Label _menuLabel;
        private readonly ISelectionHolder _selector;
        private readonly ToolStripItem _btnNavigateToPlot;



        protected abstract IVisualisable CurrentPlot
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
            this._chart = new Chart();
            this._chart.ChartAreas.Add(new ChartArea());
            Color c = core.Options.Colours.MinorGrid;
            Color c2 = core.Options.Colours.MajorGrid;
            this._chart.ChartAreas[0].AxisX.LineColor = c;
            this._chart.ChartAreas[0].AxisX.MajorGrid.LineColor = c;
            this._chart.ChartAreas[0].AxisX.MinorGrid.LineColor = c;
            this._chart.ChartAreas[0].AxisY.LineColor = c;
            this._chart.ChartAreas[0].AxisY.MajorGrid.LineColor = c;
            this._chart.ChartAreas[0].AxisY.MinorGrid.LineColor = c;
            this._chart.ChartAreas[0].AxisX.MaximumAutoSize = 10;
            this._chart.ChartAreas[0].AxisY.MaximumAutoSize = 10;
            this._chart.ChartAreas[0].CursorX.SelectionColor = c2;
            this._chart.ChartAreas[0].CursorY.SelectionColor = c2;
            this._chart.ChartAreas[0].CursorX.LineColor = c2;
            this._chart.ChartAreas[0].CursorY.LineColor = c2;

            this._chart.MouseDown += chart_MouseDown;
            this._chart.MouseMove += chart_MouseMove;
            this._chart.MouseUp += chart_MouseUp;

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
            targetSite.Controls.Add(_menuBar);
            _menuBar.SendToBack();

            // PLOT BUTTON
            _plotButton = new ToolStripSplitButton("No selection");
            _plotButton.Enabled = false;
            this._menuBar.Items.Add(_plotButton);

            // USER DETAILS BUTTON
            _userDetailsButton = new ToolStripSplitButton("...");
            _userDetailsButton.Click += (x, y) => _userDetailsButton.ShowDropDown();
            _userDetailsButton.DropDownItems.Add("Configure...", null, _userDetailsButton_Clicked);
            this._menuBar.Items.Add(_userDetailsButton);

            // SELECTION BUTTON
            _selectionButton = new ToolStripSplitButton("No selection");
            _selectionButton.Alignment = ToolStripItemAlignment.Right;
            _selectionButton.Enabled = false;
            this._menuBar.Items.Add(_selectionButton);

            // PLOT BUTTON ITEMS       
            _plotButton.DropDownOpening += _menuButtonMenu_Opening;
            _plotButton.Click += (x, y) => _plotButton.ShowDropDown();
            _btnNavigateToPlot = _plotButton.DropDownItems.Add("Select this", null, SelectThis_Click);
            _plotButton.DropDownItems.Add(new ToolStripSeparator());
            _plotButton.DropDownItems.Add("Toggle legend", null, ToggleLegend_Click);
            _plotButton.DropDownItems.Add("Reset scale\tMMB", null, ResetScale_Click);
            _plotButton.DropDownItems.Add("Clear selection\tMMB", null, ResetSel_Click);
            _plotButton.DropDownItems.Add("Copy caption text to clipboard", null, ShowCaption_Click);
            _plotButton.DropDownItems.Add("Display caption text...", null, ShowCaption2_Click);
            _plotButton.DropDownItems.Add("Copy image to clipboard", null, ShowImage_Click);
            _plotButton.DropDownItems.Add("Save image...", null, SaveImage_Click);

            _btnNavigateToPlot.Font = new Font(_btnNavigateToPlot.Font, FontStyle.Bold);
            _btnNavigateToPlot.Enabled = _selector != null;

            // SELECTION BUTTON ITEMS
            _selectionButton.DropDownOpening += _selectionButtonMenu_Opening;
            _selectionButton.Click += (x, y) => _selectionButton.ShowDropDown();
        }

        private void _userDetailsButton_Clicked(object sender, EventArgs e)
        {
            FrmOptions.Show(_chart.FindForm(), _core);
        }

        private void _menuButtonMenu_Opening(object sender, EventArgs e)
        {
            _btnNavigateToPlot.Text = "Navigate to " + new VisualisableSelection(CurrentPlot, EActivateOrigin.External);
        }

        private void _selectionButtonMenu_Opening(object sender, EventArgs e)
        {
            _selectionButton.DropDownItems.Clear();

            HashSet<IVisualisable> items = new HashSet<IVisualisable>();

            foreach (Series series in _selectedSeriesList)
            {
                IVisualisable visualisable = series.Tag as IVisualisable;

                if (visualisable == null || items.Contains(visualisable))
                {
                    continue;
                }

                items.Add(visualisable);
                ToolStripItem mnuSelectSelection = _selectionButton.DropDownItems.Add("Navigate to " + visualisable.DisplayName, null, SelectThisSelection_Click);
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

        private void ToggleLegend_Click(object sender, EventArgs e)
        {
            foreach (Series s in _chart.Series)
            {
                s.IsVisibleInLegend = !s.IsVisibleInLegend;
            }
        }

        private void ResetScale_Click(object sender, EventArgs e)
        {
            ResetChartAreas();
        }

        private void ResetSel_Click(object sender, EventArgs e)
        {
            ClearSelection();
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
                _chart.SaveImage(memStream, ChartImageFormat.Png);

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
                _chart.SaveImage(fn, fn.ToLower().EndsWith("emf") ? ChartImageFormat.Emf : ChartImageFormat.Png);
            }
        }

        public Chart Chart
        {
            get
            {
                return _chart;
            }
        }

        public void ResetChartAreas()
        {
            _chart.ChartAreas[0].AxisX.Minimum = double.NaN;
            _chart.ChartAreas[0].AxisX.Maximum = double.NaN;
            _chart.ChartAreas[0].AxisY.Minimum = double.NaN;
            _chart.ChartAreas[0].AxisY.Maximum = double.NaN;
            ClearCursor();
        }

        private void ClearCursor()
        {
            _chart.ChartAreas[0].CursorX.SelectionStart = -1;
            _chart.ChartAreas[0].CursorX.SelectionEnd = -1;
            _chart.ChartAreas[0].CursorY.SelectionStart = -1;
            _chart.ChartAreas[0].CursorY.SelectionEnd = -1;
            _chart.ChartAreas[0].CursorX.Position = 100000;
            _chart.ChartAreas[0].CursorY.Position = 100000;
        }

        public void ClearPlot()
        {
            PrepareNewPlot(false, null);
        }

        protected void PrepareNewPlot(bool axes, IVisualisable toPlot)
        {
            ResetChartAreas();
            ClearSelection();

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

            _chart.Series.Clear();
            _chart.ChartAreas[0].AxisX.CustomLabels.Clear();

            _chart.ChartAreas[0].AxisX.Enabled = axes ? AxisEnabled.True : AxisEnabled.False;
            _chart.ChartAreas[0].AxisY.Enabled = axes ? AxisEnabled.Auto : AxisEnabled.False;

            if (axes)
            {
                if (!ParseElementCollection.IsNullOrEmpty(userComments.AxisX))
                {
                    _chart.ChartAreas[0].AxisX.Title = userComments.AxisX.ConvertToString(toPlot, _core);
                }
                else
                {
                    _chart.ChartAreas[0].AxisX.Title = string.Empty;
                }

                if (!ParseElementCollection.IsNullOrEmpty(userComments.AxisY))
                {
                    _chart.ChartAreas[0].AxisY.Title = userComments.AxisY.ConvertToString(toPlot, _core);
                }
                else
                {
                    _chart.ChartAreas[0].AxisY.Title = string.Empty;
                }
            }
            else
            {
                _chart.ChartAreas[0].AxisX.Title = string.Empty;
                _chart.ChartAreas[0].AxisY.Title = string.Empty;
            }

            _chart.Titles.Clear();

            if (!ParseElementCollection.IsNullOrEmpty(userComments.Title))
            {
                _chart.Titles.Add(new Title(userComments.Title.ConvertToString(toPlot, _core), Docking.Top, FontHelper.LargeBoldFont, _core.Options.Colours.AxisTitle));
            }

            if (!ParseElementCollection.IsNullOrEmpty(userComments.SubTitle))
            {
                _chart.Titles.Add(new Title(userComments.SubTitle.ConvertToString(toPlot, _core), Docking.Top, FontHelper.ItalicFont, _core.Options.Colours.AxisTitle));
            }
        }

        private void chart_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                SelectClosestDataPoint(e.X, e.Y);
            }
            else if (e.Button == MouseButtons.Right)
            {
                _mouseDownStart = e.Location;

                double x = _chart.ChartAreas[0].AxisX.PixelPositionToValue(e.X);
                double y = _chart.ChartAreas[0].AxisY.PixelPositionToValue(e.Y);

                _chart.ChartAreas[0].CursorX.SelectionStart = x;
                _chart.ChartAreas[0].CursorY.SelectionStart = y;
                _chart.ChartAreas[0].CursorX.SelectionEnd = x;
                _chart.ChartAreas[0].CursorY.SelectionEnd = y;
            }
            else if (e.Button == MouseButtons.Middle)
            {
                ResetChartAreas();
                ClearSelection();
            }
        }

        private void chart_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                try
                {
                    double x = _chart.ChartAreas[0].AxisX.PixelPositionToValue(e.X);
                    double y = _chart.ChartAreas[0].AxisY.PixelPositionToValue(e.Y);

                    _chart.ChartAreas[0].CursorX.SelectionEnd = x;
                    _chart.ChartAreas[0].CursorY.SelectionEnd = y;
                }
                catch
                {
                    // Ignore
                }
            }
        }

        private void chart_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                double x1;
                double y1;
                double x2;
                double y2;

                try
                {
                    x1 = _chart.ChartAreas[0].AxisX.PixelPositionToValue(_mouseDownStart.X);
                    y1 = _chart.ChartAreas[0].AxisY.PixelPositionToValue(_mouseDownStart.Y);
                    x2 = _chart.ChartAreas[0].AxisX.PixelPositionToValue(e.X);
                    y2 = _chart.ChartAreas[0].AxisY.PixelPositionToValue(e.Y);
                }
                catch
                {
                    return;
                }

                // Ignore small mouse movements
                if (Math.Abs(_mouseDownStart.X - e.X) < 4 || Math.Abs(_mouseDownStart.Y - e.Y) < 4)
                {
                    return;
                }

                _chart.ChartAreas[0].AxisX.Minimum = Math.Min(x1, x2);
                _chart.ChartAreas[0].AxisX.Maximum = Math.Max(x1, x2);
                _chart.ChartAreas[0].AxisY.Minimum = Math.Min(y1, y2);
                _chart.ChartAreas[0].AxisY.Maximum = Math.Max(y1, y2);

                ClearCursor();
            }
        }

        private void SelectClosestDataPoint(int x, int y)
        {
            if (_chart.Series.Count == 0)
            {
                return;
            }

            ChartArea chartArea = _chart.ChartAreas[0];

            double cd = double.MaxValue;
            int seriesIndex = -1;
            int pointIndex = -1;
            int yIndex = -1;

            for (int s = 0; s < _chart.Series.Count; s++)
            {
                for (int p = 0; p < _chart.Series[s].Points.Count; p++)
                {
                    var dp = _chart.Series[s].Points[p];

                    var pointX = chartArea.AxisX.ValueToPixelPosition(dp.XValue);

                    for (int yi = 0; yi < dp.YValues.Length; yi++)
                    {
                        double yVal = dp.YValues[yi];
                        var pointY = chartArea.AxisY.ValueToPixelPosition(yVal);

                        double distSquared = Math.Pow(x - pointX, 2) + Math.Pow(y - pointY, 2);

                        if (distSquared < cd)
                        {
                            cd = distSquared;
                            seriesIndex = s;
                            pointIndex = p;
                            yIndex = yi;
                        }
                    }
                }
            }

            if (seriesIndex == -1)
            {
                return;
            }

            Series newSeries = _chart.Series[seriesIndex];
            DataPoint newPoint = _chart.Series[seriesIndex].Points[pointIndex];
            int newPointIndex = yIndex;

            SetSelection(newSeries, newPoint, newPointIndex);
        }

        protected void SetSelection(Series series, DataPoint dataPoint, int dataPointIndex)
        {
            SetSelection(new List<Series> { series }, dataPoint, dataPointIndex);
        }

        protected void ClearSelection()
        {
            SetSelection(new List<Series>(), null, -1);
        }

        protected void SetSelection(List<Series> series, DataPoint dataPoint, int dataPointIndex)
        {
            // Restore previous series
            foreach (Series oldSeries in _selectedSeriesList)
            {
                oldSeries.Color = _selectedSeriesColours[oldSeries].Item1;
                oldSeries.BorderWidth = _selectedSeriesColours[oldSeries].Item2;
            }

            if (_selectedPoint != null)
            {
                _selectedPoint.Label = null;
            }

            BeforeSelectSeries(series);

            // Save data on new series
            _selectedSeriesList = new List<Series>(series);
            _selectedSeriesColours.Clear();
            _selectedPoint = dataPoint;
            _selectedPointIndex = dataPointIndex;

            foreach (Series newSeries in _selectedSeriesList)
            {
                Tuple<Color, int> colours = new Tuple<Color, int>(newSeries.Color, newSeries.BorderWidth);
                _selectedSeriesColours.Add(newSeries, colours);

                // Setup new series
                _chart.Series.Remove(newSeries);
                _chart.Series.Add(newSeries);

                if (_enableHighlightSeries)
                {
                    Color hc = _core.Options.Colours.SelectedSeries;
                    newSeries.Color = Color.FromArgb(newSeries.Color.A, hc.R, hc.G, hc.B);
                    newSeries.BorderWidth = 4;
                }
                else if (newSeries.ChartType == SeriesChartType.Point || newSeries.ChartType == SeriesChartType.Line)
                {
                    newSeries.Color = Color.FromArgb(newSeries.Color.A, 0, 0, 0);
                }
                else
                {
                    newSeries.Color = Color.FromArgb(newSeries.Color.A, Math.Max(newSeries.Color.R / 2, 0), Math.Max(newSeries.Color.G / 2, 0), Math.Max(newSeries.Color.B / 2, 0));
                }
            }

            // Set cursor to point
            if (_selectedPoint != null)
            {
                ChartArea chartArea = _chart.ChartAreas[0];

                double px = chartArea.AxisX.ValueToPixelPosition(_selectedPoint.XValue);
                double py = chartArea.AxisY.ValueToPixelPosition(_selectedPoint.YValues[_selectedPointIndex]);

                try
                {
                    chartArea.CursorX.SelectionStart = chartArea.AxisX.PixelPositionToValue(px - 4);
                    chartArea.CursorX.SelectionEnd = chartArea.AxisX.PixelPositionToValue(px + 4);
                    chartArea.CursorY.SelectionStart = chartArea.AxisY.PixelPositionToValue(py - 4);
                    chartArea.CursorY.SelectionEnd = chartArea.AxisY.PixelPositionToValue(py + 4);
                }
                catch
                {
                    // Ignore error
                }
            }

            // Perform specific actions
            PerformSelectionActions();
        }

        protected virtual void BeforeSelectSeries(List<Series> series)
        {
            // No action
        }

        private void PerformSelectionActions()
        {
            // Series are tagged with the variables they represent
            // When multiple series are selected they all have the same variable so no worries about using the first one
            Peak peak = _selectedSeriesList.Count != 0 ? (Peak)_selectedSeriesList[0].Tag : null;

            // Points are tagged with the observation
            IntensityInfo dataPoint;

            if (_selectedPoint != null)
            {
                if (_selectedPoint.Tag is IntensityInfo)
                {
                    dataPoint = (IntensityInfo)_selectedPoint.Tag;
                }
                else if (_selectedPoint.Tag is IntensityInfo[])
                {
                    IntensityInfo[] dataPointArray = (IntensityInfo[])_selectedPoint.Tag;
                    dataPoint = dataPointArray != null ? dataPointArray[_selectedPointIndex] : default(IntensityInfo);
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

                if (_selectedSeriesList.Count != 0)
                {
                    // Select the first series names as all series are usually similar
                    name = _selectedSeriesList[0].Name;

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
            if (_selectionButton != null)
            {
                if (dataPoint != null)
                {
                    _selectionButton.Text = _selectedSeriesList[0].Name + " " + dataPoint.ToString();
                    _selectionButton.Image = UiControls.CreateSolidColourImage(true, dataPoint.Group);
                    _selectionButton.Enabled = true;
                }
                else
                {
                    _selectionButton.Text = "No selection";
                    _selectionButton.Image = Resources.ObjNone;
                    _selectionButton.Enabled = false;
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
            Bitmap bmp = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                Rectangle rc = new Rectangle(0, 0, bmp.Width, bmp.Height);

                _chart.Printing.PrintPaint(g, rc);
            }

            return bmp;
        }

        protected void DrawLabels(bool bConditionsSideBySide, IEnumerable<GroupInfoBase> orderOfGroups)
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

                    DrawAxisBar(x, minX, maxX, text);
                }
            }
            else
            {
                int minX = _core.TimeRange.Min;
                int maxX = _core.TimeRange.Max;
                int x = 0;
                string text = StringHelper.ArrayToString(orderOfGroups, z => z.Name, ", ");

                DrawAxisBar(x, minX, maxX, text);
            }
        }

        private void DrawAxisBar(int x, int min, int max, string text)
        {
            CustomLabel cl = new CustomLabel(x + min, x + max, text, 0, LabelMarkStyle.None);
            cl.GridTicks = GridTickTypes.None;
            _chart.ChartAreas[0].AxisX.CustomLabels.Add(cl);

            cl = new CustomLabel(x + min - 0.5, x + min + 0.5, min.ToString(), 0, LabelMarkStyle.None);
            cl.GridTicks = GridTickTypes.All;
            _chart.ChartAreas[0].AxisX.CustomLabels.Add(cl);

            cl = new CustomLabel(x + max - 0.5, x + max + 0.5, max.ToString(), 0, LabelMarkStyle.None);
            cl.GridTicks = GridTickTypes.All;
            _chart.ChartAreas[0].AxisX.CustomLabels.Add(cl);

            for (int n = min + 1; n < max; n++)
            {
                cl = new CustomLabel(x + n - 0.5, x + n + 0.5, "", 0, LabelMarkStyle.None);
                cl.GridTicks = GridTickTypes.TickMark;
                _chart.ChartAreas[0].AxisX.CustomLabels.Add(cl);
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
