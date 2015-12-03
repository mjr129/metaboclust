using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Forms.Editing;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Viewers.Charts;
using System.Text;
using System.Linq;
using MetaboliteLevels.Forms.Generic;

namespace MetaboliteLevels.Viewers.Lists
{
    /// <summary>
    /// Attaches to a listview and manages its contents, columns and menu options.
    /// </summary>
    abstract partial class ListViewHelper : IDisposable
    {
        public Core _core;
        protected ListView _listView; // listview control to use
        private Dictionary<object, int> _imgListPreviewIndexes = new Dictionary<object, int>(); // cached thumbnails
        private IPreviewProvider _previewProvider; // thumbnail provider
        private ImageList _imgListNormal; // image list
        private ImageList _imgListPreviews; // image list for thumbnails
        private bool _enablePreviews; // if in thumbnail mode
        protected readonly ToolStrip _toolStrip;

        public event EventHandler<ShowContextMenuEventArgs> ShowContextMenu;
        public event EventHandler<ListViewItemEventArgs> Activate;

        private List<Column> _availableColumns = new List<Column>();

        private ContextMenuStrip _cmsColumns;
        private ToolStripMenuItem _mnuSortAscending;
        private ToolStripMenuItem _mnuSortDescending;
        private ToolStripMenuItem _mnuFilterColumn;
        private ToolStripMenuItem _mnuHideColumn;
        private ToolStripMenuItem _mnuRenameColumn;
        private ToolStripMenuItem _mnuColumnHelp;

        private ToolStripDropDownButton _btnColumns;
        private ToolStripItem _lblFilter;

        private bool _disableColumnMenuRebuild;

        private bool _isCreatingColumns;
        private Column _clickedColumn;

        private IComparer<IVisualisable> _sortOrder;
        private ColumnFilter _filter;

        protected List<IVisualisable> _filteredList;

        private bool _emptyList;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="lv">Listview to handle</param>
        /// <param name="ts">Toolstrip to create options on</param>
        /// <param name="previewProvider">Provider of thumbnails</param>
        protected ListViewHelper(ListView lv, Core core, IPreviewProvider previewProvider)
        {
            this._listView = lv;
            this._previewProvider = previewProvider;
            this._core = core;

            _imgListNormal = lv.SmallImageList;
            _imgListPreviews = new ImageList();
            _imgListPreviews.ImageSize = new Size(64, 64);

            // Setup listview
            lv.VirtualMode = true;

            // Set events
            lv.RetrieveVirtualItem += _listView_RetrieveVirtualItem;
            lv.ItemActivate += _listView_ItemActivate;
            lv.MouseDown += _listView_MouseDown;

            lv.View = View.Details;
            lv.HeaderStyle = ColumnHeaderStyle.Clickable;
            lv.AllowColumnReorder = true;
            lv.FullRowSelect = true;
            lv.GridLines = true;

            if (lv.SmallImageList == null)
            {
                lv.SmallImageList = new ImageList();
                lv.SmallImageList.ImageSize = new Size(24, 24);
                UiControls.PopulateImageList(lv.SmallImageList);
            }

            if (lv.LargeImageList == null)
            {
                lv.LargeImageList = lv.SmallImageList;
            }

            _toolStrip = new ToolStrip();
            lv.Parent.Controls.Add(_toolStrip);
            _toolStrip.BringToFront();
            lv.BringToFront();

            _listView.ColumnClick += listView_ColumnClick;
            _listView.ColumnReordered += _listView_ColumnReordered;
            _listView.ColumnWidthChanged += _listView_ColumnWidthChanged;

            // Menu: Export visible
            ToolStripButton tsSave = new ToolStripButton("Export (visible columns)", Resources.MnuSave);
            tsSave.Click += tsExportVisible_Click;
            tsSave.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _toolStrip.Items.Insert(0, tsSave);

            // Menu: Export all
            ToolStripButton tsSaveAll = new ToolStripButton("Export (include hidden columns)", Resources.MnuSaveAll);
            tsSaveAll.Click += tsExportAll_Click;
            tsSaveAll.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _toolStrip.Items.Insert(0, tsSaveAll);

            // Menu: Copy to clipboard
            ToolStripButton tsSave2 = new ToolStripButton("Copy to clipboard", Resources.MnuCopy);
            tsSave2.Click += tsCopyToClipboard_Click;
            tsSave2.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _toolStrip.Items.Insert(0, tsSave2);

            // Thumbnails
            if (previewProvider != null)
            {
                ToolStripButton tsExpand = new ToolStripButton("Expand", Resources.MnuEnlarge);
                tsExpand.Click += tsExpand_Click;
                _toolStrip.Items.Insert(0, tsExpand);

                ToolStripButton tsThumbNails = new ToolStripButton("Thumbnails", Resources.MnuPreview);
                tsThumbNails.Click += tsThumbNails_Click;
                _toolStrip.Items.Insert(0, tsThumbNails);
            }

            // Create column menu
            _cmsColumns = new ContextMenuStrip();
            _mnuSortAscending = (ToolStripMenuItem)_cmsColumns.Items.Add("Sort ascending");
            _mnuSortDescending = (ToolStripMenuItem)_cmsColumns.Items.Add("Sort descending");
            _cmsColumns.Items.Add(new ToolStripSeparator());
            _mnuFilterColumn = (ToolStripMenuItem)_cmsColumns.Items.Add(@"TEXT GOES HERE (ADD/REMOVE FILTER)");
            _mnuHideColumn = (ToolStripMenuItem)_cmsColumns.Items.Add("Hide column");
            _mnuRenameColumn = (ToolStripMenuItem)_cmsColumns.Items.Add("Rename column");
            _mnuColumnHelp = (ToolStripMenuItem)_cmsColumns.Items.Add("Help on this column...");
            _mnuSortAscending.Image = Resources.MnuSortAscending;
            _mnuSortDescending.Image = Resources.MnuSortDescending;

            // Handle menu items
            _mnuColumnHelp.Image = Resources.MnuHelp;
            _mnuSortAscending.Click += sortAscending_Click;
            _mnuSortDescending.Click += sortAscending_Click;
            _mnuFilterColumn.Click += _filterm_Click;
            _mnuColumnHelp.Click += _descm_Click;
            _mnuRenameColumn.Click += _mnuRenameColumn_Click;
            _mnuHideColumn.Click += _hidecol_Click;

            // Create columns button
            _btnColumns = new ToolStripDropDownButton("Columns", Resources.MnuColumn);
            _btnColumns.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _btnColumns.DropDownOpening += columnSelectMenu_DropDownOpening;
            _toolStrip.Items.Insert(0, _btnColumns);

            // Create filter label
            _lblFilter = _toolStrip.Items.Add("FILTER");
            _lblFilter.DisplayStyle = ToolStripItemDisplayStyle.Text;
            _lblFilter.ForeColor = Color.Red;
            _lblFilter.Visible = false;
            _lblFilter.Click += _filterm_Click;
        }

        /// <summary>
        /// Item clicked
        /// </summary>
        void _listView_ItemActivate(object sender, EventArgs e)
        {
            ActivateByIndex(SelectedIndex);
        }

        private void ActivateByIndex(int index)
        {
            if (index != -1)
            {
                OnActivate(_filteredList[index]);
            }
        }

        private void OnShowContextMenu(IVisualisable selection, Control control, int x, int y)
        {
            if (ShowContextMenu != null)
            {
                ShowContextMenuEventArgs e = new ShowContextMenuEventArgs(selection, control, x, y);
                ShowContextMenu(this, e);
            }
        }

        void _listView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ListViewItem lvi = _listView.GetItemAt(e.X, e.Y);

                if (lvi != null)
                {
                    lvi.Selected = true;
                    lvi.Focused = true;
                    lvi.EnsureVisible();

                    OnShowContextMenu((IVisualisable)lvi.Tag, _listView, e.X, e.Y);
                }
            }
        }

        /// <summary>
        /// Saves columns to preferences.
        /// </summary>
        private void SaveColumnUserPreferences()
        {
            foreach (Column col in _availableColumns)
            {
                _core.Options.OpenColumn(true, _listView.Name, col.Id, ref col.DisplayName, ref col.Visible, ref col.Width, ref col.DisplayIndex);
            }
        }

        /// <summary>
        /// Loads columns from preferences.
        /// </summary>
        private void LoadColumnUserPreferences()
        {
            foreach (Column col in _availableColumns)
            {
                _core.Options.OpenColumn(false, _listView.Name, col.Id, ref col.DisplayName, ref col.Visible, ref col.Width, ref col.DisplayIndex);
            }
        }

        /// <summary>
        /// Toggle column clicked.
        /// </summary>
        void toggleColumn_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem s = (ToolStripMenuItem)sender;
            Column c = (Column)s.Tag;

            c.Visible = !c.Visible;
            s.Checked = c.Visible;

            if (c.Visible)
            {
                c.DisplayIndex = _listView.Columns.Count;
            }

            SaveColumnUserPreferences();

            Rebuild(EListInvalids.ToggleColumn);

            _disableColumnMenuRebuild = true;
            _btnColumns.ShowDropDown();

            ShowMenu(s.OwnerItem as ToolStripMenuItem);
            _disableColumnMenuRebuild = false;
        }

        private Column GetColumnDefinition(int headerIndex)
        {
            var header = _listView.Columns[headerIndex];

            foreach (Column column in _availableColumns)
            {
                if (column.Header == header)
                {
                    return column;
                }
            }

            throw new KeyNotFoundException("GetSortColumn: Column not found: headerIndex = " + headerIndex);
        }

        private void ShowMenu(ToolStripMenuItem toolStripItem)
        {
            if (toolStripItem != null)
            {
                ShowMenu(toolStripItem.OwnerItem as ToolStripMenuItem);
                toolStripItem.ShowDropDown();
            }
        }

        void _listView_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            if (!_isCreatingColumns) // otherwise hasn't had the header set yet and GetColumnDefinition changes
            {
                var column = GetColumnDefinition(e.ColumnIndex);
                column.Width = _listView.Columns[e.ColumnIndex].Width;
                SaveColumnUserPreferences();
            }
        }

        void _listView_ColumnReordered(object sender, ColumnReorderedEventArgs e)
        {
            if (!_isCreatingColumns) // not sure if required
            {
                // This gets called before columns are reordered, so we must reproduce the reorder to find out the new one
                List<ColumnHeader> list = new List<ColumnHeader>(_listView.Columns.Cast<ColumnHeader>());
                list.OrderBy(λ => λ.DisplayIndex);
                ColumnHeader item = list[e.OldDisplayIndex];
                list.RemoveAt(e.OldDisplayIndex);
                list.Insert(e.NewDisplayIndex, item);

                for (int n = 0; n < list.Count; n++)
                {
                    Column col = (Column)list[n].Tag;
                    col.DisplayIndex = n;
                }

                SaveColumnUserPreferences();
            }
        }

        /// <summary>
        /// Column header clicked.
        /// </summary>
        void listView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Dispose previous
            Column col = GetColumnDefinition(e.Column);
            _clickedColumn = col;
            SortByColumn sbc = _sortOrder as SortByColumn;

            _mnuSortAscending.Checked = sbc != null && sbc.col == col && sbc.ascending;
            _mnuSortAscending.Enabled = !col.DisableMenu;

            _mnuSortDescending.Checked = sbc != null && sbc.col == col && !sbc.ascending;
            _mnuSortDescending.Enabled = !col.DisableMenu;

            _mnuFilterColumn.Text = _filter != null ? "Remove filter" : "Create filter...";
            _mnuFilterColumn.Tag = col;
            _mnuFilterColumn.Enabled = !col.DisableMenu || _filter != null;

            _mnuHideColumn.Enabled = !col.DisableMenu;

            _mnuColumnHelp.Visible = !string.IsNullOrWhiteSpace(col.Description);

            _cmsColumns.Show(Cursor.Position);
        }

        /// <summary>
        /// Column select menu.
        /// </summary>
        void columnSelectMenu_DropDownOpening(object sender, EventArgs e)
        {
            if (_disableColumnMenuRebuild)
            {
                return;
            }

            ToolStripDropDownButton ctrl = (ToolStripDropDownButton)sender;

            // Dispose previous
            var x = new ArrayList(ctrl.DropDownItems);

            foreach (ToolStripMenuItem c2 in x)
            {
                c2.Click -= toggleColumn_Click;
                c2.Dispose();
            }

            // If many columns then show a listbox instead
            if (_availableColumns.Count >= 50)
            {
                IEnumerable<Column> selected = ListValueSet.ForColumns(_availableColumns).Select(z => z.Visible).ShowCheckList(this.ListView.FindForm());

                if (selected != null)
                {
                    _availableColumns.ForEach(z => z.Visible = z.IsAlwaysEmpty || selected.Contains(z));
                }

                SaveColumnUserPreferences();
                Rebuild(EListInvalids.ToggleColumn);
                return;
            }

            // Create new
            Dictionary<string, ToolStripDropDownItem> folders = new Dictionary<string, ToolStripDropDownItem>();

            foreach (Column col in _availableColumns)
            {
                if (!col.DisableMenu)
                {
                    string colName = col.Id;
                    ToolStripDropDownItem menuTarget = ctrl;

                    if (colName.Contains("\\"))
                    {
                        string[] elems = colName.Split('\\');
                        colName = elems[elems.Length - 1];
                        string colFolderName = "";

                        for (int n = 0; n < elems.Length - 1; n++)
                        {
                            colFolderName += elems[n] + "\\";

                            ToolStripDropDownItem newMenuTarget;

                            if (!folders.TryGetValue(colFolderName, out newMenuTarget))
                            {
                                newMenuTarget = new ToolStripMenuItem("• " + elems[n]);
                                menuTarget.DropDownItems.Add(newMenuTarget);
                                folders.Add(colFolderName, newMenuTarget);
                                menuTarget = newMenuTarget;
                            }
                            else
                            {
                                menuTarget = newMenuTarget;
                            }
                        }
                    }

                    var tsmi = new ToolStripMenuItem(colName);
                    tsmi.Tag = col;
                    tsmi.Click += toggleColumn_Click;

                    int addAt = 0;

                    for (int i = 0; i < menuTarget.DropDownItems.Count; i++)
                    {
                        if (((ToolStripMenuItem)menuTarget.DropDownItems[i]).DropDownItems.Count != 0)
                        {
                            addAt = i;
                            break;
                        }
                    }

                    menuTarget.DropDownItems.Insert(addAt, tsmi);

                    tsmi.Checked = col.Visible;
                }
            }
        }


        /// <summary>
        /// Menu: Column help
        /// </summary>
        void _descm_Click(object sender, EventArgs e)
        {
            FrmInputLarge.ShowFixed(ListView.FindForm(), "Help", _clickedColumn.Id, null, _clickedColumn.Description);
        }

        void _filterm_Click(object sender, EventArgs e)
        {
            if (_filter != null)
            {
                _filter = null;
                _lblFilter.Visible = false;
                Rebuild(EListInvalids.Filter);
            }
            else
            {
                var op = ListVieweHelper.EOperator.TextContains;
                string va = "";

                if (FrmFilter.Show(_listView.FindForm(), _clickedColumn.Id, ref op, ref va))
                {
                    _filter = new ColumnFilter(this, _clickedColumn, op, va);
                    _lblFilter.Visible = true;
                    Rebuild(EListInvalids.Filter);
                }
            }
        }

        /// <summary>
        /// Menu: Hide column
        /// </summary>
        void _hidecol_Click(object sender, EventArgs e)
        {
            _clickedColumn.Visible = !_clickedColumn.Visible;
            SaveColumnUserPreferences();
            Rebuild(EListInvalids.ToggleColumn);
        }

        /// <summary>
        /// Sort ascending clicked.
        /// </summary>
        void sortAscending_Click(object sender, EventArgs e)
        {
            _sortOrder = new SortByColumn(_clickedColumn, sender == _mnuSortAscending);

            Rebuild(EListInvalids.Sorted);
        }

        /// <summary>
        /// Menu: Rename column
        /// </summary>
        void _mnuRenameColumn_Click(object sender, EventArgs e)
        {
            string newName = FrmInput.Show(_listView.FindForm(), "Rename column", _clickedColumn.Id, "Enter a new name for this column", _clickedColumn.DisplayName);

            if (newName != null)
            {
                _clickedColumn.DisplayName = newName;
                _clickedColumn.Header.Text = _clickedColumn.ToString();
                SaveColumnUserPreferences();
            }
        }

        /// <summary>
        /// Menu: Export
        /// </summary>
        void tsExportVisible_Click(object sender, EventArgs e)
        {
            string fn = _listView.FindForm().BrowseForFile(null, UiControls.EFileExtension.Csv  , FileDialogMode.SaveAs, UiControls.EInitialFolder.ExportedData);

            if (fn != null)
            {
                using (StreamWriter sw = new StreamWriter(fn))
                {
                    SaveItems(sw);
                }
            }
        }

        /// <summary>
        /// Menu: Export all
        /// </summary>
        void tsExportAll_Click(object sender, EventArgs e)
        {
            string fn = _listView.FindForm().BrowseForFile(null, UiControls.EFileExtension.Csv, FileDialogMode.SaveAs, UiControls.EInitialFolder.ExportedData);

            if (fn != null)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(fn))
                    {
                        SaveItems(sw, true);
                    }
                }
                catch (Exception ex)
                {
                    FrmMsgBox.ShowError(_listView.FindForm(), ex);
                }
            }
        }

        /// <summary>
        /// Menu: Copy to clipboard
        /// </summary>
        void tsCopyToClipboard_Click(object sender, EventArgs e)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (StreamWriter sw = new StreamWriter(ms))
                {
                    SaveItems(sw);
                }

                string text = Encoding.UTF8.GetString(ms.ToArray());
                Clipboard.SetText(text);
            }
        }

        /// <summary>
        /// Sets the list to source from a ContentsRequest object.
        /// </summary>
        public abstract void DivertList(ContentsRequest diversion);

        /// <summary>
        /// Memory cleanup.
        /// </summary>
        public void Dispose()
        {
            OnDisposing(true);
        }

        protected void OnActivate(IVisualisable item)
        {
            if (Activate != null)
            {
                Activate(this, new ListViewItemEventArgs(item));
            }
        }

        /// <summary>
        /// Memory cleanup handler.
        /// </summary>
        protected virtual void OnDisposing(bool isDisposing)
        {
            if (isDisposing)
            {
                _toolStrip.Dispose();
                _imgListPreviews.Dispose();
                _cmsColumns.Dispose();
            }
        }

        /// <summary>
        /// Listview handled by this object.
        /// </summary>
        public ListView ListView
        {
            get
            {
                return _listView;
            }
        }

        /// <summary>
        /// Expand menu button clicked.
        /// Shows the expansion form.
        /// </summary>
        void tsExpand_Click(object sender, EventArgs e)
        {
            IVisualisable highlight = GetOwner();

            int selected = FrmClusterSheet.Show(_listView.FindForm(), this.SourceList, this._previewProvider, highlight);

            if (selected != -1)
            {
                SelectedIndex = selected;
                ActivateByIndex(selected);
            }
        }

        /// <summary>
        /// Whether an item is selected in the listview.
        /// </summary>
        public bool HasSelection
        {
            get
            {
                return _listView.SelectedIndices.Count != 0;
            }
        }

        /// <summary>
        /// The selected index (-1 for none).
        /// </summary>
        public int SelectedIndex
        {
            get
            {
                if (_listView.SelectedIndices.Count == 0)
                {
                    return -1;
                }

                return _listView.SelectedIndices[0];
            }
            set
            {
                _listView.SelectedIndices.Clear();

                if (value == -1)
                {
                    return;
                }

                _listView.SelectedIndices.Add(value);
                _listView.EnsureVisible(value);
            }
        }

        protected int GeneratePreviewImage(IVisualisable item)
        {
            int index;

            if (!_imgListPreviewIndexes.TryGetValue(item, out index))
            {
                IVisualisable highlight = GetOwner();

                Image img = _previewProvider.ProvidePreview(_imgListPreviews.ImageSize, item, highlight);

                if (img == null)
                {
                    img = ChartHelperForClusters.CreatePlaceholderBitmap(item, _imgListPreviews.ImageSize);
                }

                index = _imgListPreviews.Images.Count;

                _imgListPreviews.Images.Add(item.DisplayName, img);
                _imgListPreviewIndexes.Add(item, index);
            }

            return index;
        }

        protected abstract IVisualisable GetOwner();

        void tsThumbNails_Click(object sender, EventArgs e)
        {
            ToolStripButton tsmi2 = (ToolStripButton)sender;

            tsmi2.Checked = !tsmi2.Checked;
            EnablePreviews = tsmi2.Checked;
        }

        protected void SetFont()
        {
            _listView.Font = _enablePreviews ? UiControls.largeFont : UiControls.normalFont;
        }

        protected void ClearPreviewList()
        {
            if (_enablePreviews)
            {
                _imgListPreviews.Images.Clear();
                _imgListPreviewIndexes.Clear();
            }
        }

        public bool EnablePreviews
        {
            get
            {
                return _enablePreviews;
            }
            set
            {
                if (_previewProvider == null)
                {
                    // Cannot enable previews with no preview provider
                    return;
                }

                if (_enablePreviews != value)
                {
                    _enablePreviews = value;
                    this._listView.LargeImageList = value ? _imgListPreviews : _imgListNormal;
                    this._listView.SmallImageList = value ? _imgListPreviews : _imgListNormal;
                    _imgListPreviews.ImageSize = new Size(64, 64);

                    Rebuild(EListInvalids.PreviewsChanged);
                }
            }
        }

        protected string AsString(object result)
        {
            if (result == null)
            {
                return "";
            }

            if (result is string)
            {
                return (string)result;
            }

            if (result is IEnumerable)
            {
                ICollection col = result as ICollection;
                IEnumerable enu = (IEnumerable)result;
                int count;

                if (col != null)
                {
                    count = col.Count;
                }
                else
                {
                    count = enu.CountAll();
                }

                switch (_core.Options.ListDisplayMode)
                {
                    case Settings.EListDisplayMode.Smart:
                        if (count == 1)
                        {
                            return AsString(enu.FirstOrDefault2());
                        }
                        else if (count == 0)
                        {
                            return "";
                        }
                        else
                        {
                            return "(" + count.ToString() + ")";
                        }

                    case Settings.EListDisplayMode.Count:
                        return count.ToString();

                    case Settings.EListDisplayMode.CountAndContent:
                        return "(" + count.ToString() + ") " + StringHelper.ArrayToString(enu, AsString);

                    case Settings.EListDisplayMode.Content:
                        return StringHelper.ArrayToString(enu, AsString);
                }
            }

            if (result is IVisualisable)
            {
                IVisualisable v = (IVisualisable)result;

                return v.DisplayName;
            }

            if (result is double)
            {
                double d = (double)result;

                if (double.IsNaN(d))
                {
                    return "";
                }

                return Maths.SignificantDigits(d);
            }

            if (result is int)
            {
                if ((int)result == 0)
                {
                    return "";
                }
                else
                {
                    return result.ToString();
                }
            }

            return result.ToString();
        }

        private void CreateListViewColumnHeaders()
        {
            _listView.Columns.Clear();

            _isCreatingColumns = true;

            foreach (Column c in _availableColumns.OrderBy(λ => λ.DisplayIndex))
            {
                if (c.Visible)
                {
                    var h = _listView.Columns.Add(c.ToString());
                    c.Header = h;
                    h.Tag = c;
                }
            }

            foreach (ColumnHeader h in _listView.Columns)
            {
                Column c = (Column)h.Tag;
                h.Width = c.Width;
            }

            _isCreatingColumns = false;
        }

        public void Rebuild(EListInvalids checks)
        {
            // since we now use a virtual list when the list changes we can just refresh the source
            if (checks.HasFlag(EListInvalids._SourceChanged) | checks.HasFlag(EListInvalids._ContentsChanged))
            {
                checks = GetFilteredList(checks);
            }

            // If we have not yet got the columns we always need to get them regardless
            if (_availableColumns.Count == 0)
            {
                checks |= EListInvalids._ColumnsChanged | EListInvalids._ColumnVisibilitiesChanged;
            }

            if (checks.HasFlag(EListInvalids._ColumnsChanged))
            {
                GetAvailableColumns();
            }

            if (checks.HasFlag(EListInvalids._ColumnVisibilitiesChanged))
            {
                CreateListViewColumnHeaders();
            }

            if (checks.HasFlag(EListInvalids._ValuesChanged))
            {
                // Since we now use a virtual list the only thing we need to update are the previews, the rest will update when we redraw
                ClearPreviewList();
            }

            // Redraw everything
            if (_listView.VirtualListSize != 0)
            {
                _listView.RedrawItems(0, _listView.VirtualListSize - 1, true);
            }
        }

        private IEnumerable<Column> GetVisualisablesColumns()
        {
            IVisualisable t = GetSourceContent().FirstOrDefault();

            if (t != null)
            {
                return t.GetColumns(_core);
            }

            return null;
        }

        protected abstract IEnumerable<IVisualisable> GetSourceContent();

        /// <summary>
        /// Gets the source list, accounting for sort-order, filter, etc.
        /// </summary>
        protected EListInvalids GetFilteredList(EListInvalids checks)
        {
            this._filteredList = new List<IVisualisable>(GetSourceContent());

            if (_emptyList && this._filteredList.Count != 0)
            {
                // List was empty, now is not - make sure to rebuild columns since we might have new ones
                checks |= EListInvalids._ColumnsChanged | EListInvalids._ColumnVisibilitiesChanged;
            }

            _emptyList = this._filteredList.Count == 0;

            if (this._filter != null)
            {
                int remCount = this._filteredList.RemoveAll(this._filter.FilterRemove);
                _lblFilter.Text = remCount.ToString() + " hidden by filter";
            }

            if (this._sortOrder != null)
            {
                this._filteredList.Sort(this._sortOrder);
            }

            this._listView.VirtualListSize = this._filteredList.Count;
            return checks;
        }

        private int GetListViewIndex(IVisualisable itemToFind)
        {
            int r = _filteredList.IndexOf(itemToFind);

            if (r == -1)
            {
                throw new KeyNotFoundException("Cannot find item \"" + itemToFind.ToString() + "\" in list.");
            }

            return r;
        }

        private ListViewItem CreateNewListViewItem(IVisualisable tag)
        {
            ListViewItem lvi = new ListViewItem("");
            lvi.Tag = tag;
            lvi.UseItemStyleForSubItems = false;
            return lvi;
        }

        public IVisualisable Selection
        {
            get
            {
                if (_listView.SelectedIndices.Count == 0)
                {
                    return null;
                }

                return _filteredList[_listView.SelectedIndices[0]];
            }
            set
            {
                if (value == null)
                {
                    _listView.SelectedIndices.Clear();
                    return;
                }

                int lvi = GetListViewIndex(value);

                SelectedIndex = lvi;
            }
        }

        /// <summary>
        /// Populates "_availableColumns"
        /// </summary>
        private void GetAvailableColumns()
        {
            _availableColumns.Clear();

            // Get columns for the type "T"
            var cols = GetVisualisablesColumns();

            if (cols == null)
            {
                // Not yet available
                return;
            }

            // Get the icon column
            var iconColumn = new Column<IVisualisable>("", true, null);
            iconColumn.Width = 32;
            iconColumn.DisableMenu = true;

            // Add the columns
            _availableColumns.Add(iconColumn);
            _availableColumns.AddRange(cols);

            // Add any custom columns
            AddCustomColumns(_availableColumns);

            // Apply preferences
            LoadColumnUserPreferences();
        }

        protected abstract void AddCustomColumns(List<Column> availableColumns);

        void _listView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            IVisualisable tag = _filteredList[e.ItemIndex];
            e.Item = CreateNewListViewItem(tag);
            DoUpdate(e.Item, tag);
            UiControls.Assert(e.Item.SubItems.Count == _listView.Columns.Count);
        }

        /// <summary>
        /// Updates the item
        /// </summary>
        private void DoUpdate(ListViewItem lvi, IVisualisable tag)
        {
            // Update icon
            if (EnablePreviews)
            {
                lvi.ImageIndex = GeneratePreviewImage(tag);
            }
            else
            {
                lvi.ImageIndex = tag.GetIcon();
            }

            // Update columns
            foreach (ColumnHeader h in _listView.Columns)
            {
                Column c = (Column)h.Tag;

                System.Diagnostics.Debug.Assert(c.Visible);

                if (!c.IsAlwaysEmpty)
                {
                    ListViewItem.ListViewSubItem lvsi = new ListViewItem.ListViewSubItem();

                    object result = c.GetRow(tag);
                    lvsi.Text = AsString(result);
                    lvsi.ForeColor = c.GetColour(tag);

                    lvi.SubItems.Add(lvsi);
                }
            }

            // Invert selection?
            if (lvi.Selected)
            {
                Invert(lvi);
            }
        }

        private void Invert(ListViewItem lvi)
        {
            foreach (ListViewItem.ListViewSubItem lvsi in lvi.SubItems)
            {
                var x = lvsi.ForeColor;
                lvsi.ForeColor = lvsi.BackColor;
                lvsi.BackColor = x;
            }
        }

        protected static void SetItem(ListViewItem lvi, int col, string text, Color back, Color fore)
        {
            lvi.SubItems[col].Text = text;
            lvi.SubItems[col].ForeColor = back;
            lvi.SubItems[col].BackColor = fore;
        }

        public bool Visible
        {
            get
            {
                return _listView.Visible;
            }
            set
            {
                _listView.Visible = value;
                _toolStrip.Visible = value;
            }
        }

        internal IEnumerable<IVisualisable> GetVisible()
        {
            return _filteredList;
        }

        public void Update(IVisualisable itemToFind)
        {
            int i = GetListViewIndex(itemToFind);
            _listView.RedrawItems(i, i, false);

            //DoUpdate(, itemToFind);
        }

        /// <summary>
        /// Saves the grid as a CSV file.
        /// </summary>
        protected void SaveItems(StreamWriter sw, bool includeHiddenColumns = false)
        {
            bool needsComma = false;

            IEnumerable<Column> columns;

            if (includeHiddenColumns)
            {
                columns = this._availableColumns;
            }
            else
            {
                columns = _listView.Columns.Cast<ColumnHeader>().Select(z => (Column)z.Tag).ToArray();
            }

            // Write headers
            foreach (Column c in columns)
            {
                if (!c.IsAlwaysEmpty) // Ignore icon column
                {
                    if (needsComma)
                    {
                        sw.Write(",");
                    }
                    else
                    {
                        needsComma = true;
                    }

                    sw.Write("\"");
                    sw.Write(c.ToString());
                    sw.Write("\"");
                }
            }

            // Write data
            foreach (IVisualisable item in _filteredList)
            {
                needsComma = false;

                foreach (Column c in columns)
                {
                    if (!c.IsAlwaysEmpty) // Ignore icon column
                    {
                        if (needsComma)
                        {
                            sw.Write(",");
                        }
                        else
                        {
                            needsComma = true;
                        }

                        object v = c.GetRow(item);
                        string txt = AsString(v);

                        sw.Write("\"");
                        sw.Write(txt);
                        sw.Write("\"");
                    }
                }

                sw.WriteLine();
            }
        }

        protected IEnumerable SourceList
        {
            get { return _filteredList; }
        }

        public void ChangeCore(Core newCore)
        {
            _core = newCore;
            Rebuild(EListInvalids.SourceChanged);
        }
    }
}
