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
using MetaboliteLevels.Settings;

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

        private SortByColumn _sortOrder;
        private ColumnFilter _filter;

        protected List<IVisualisable> _filteredList;

        private bool _emptyList;
        private ToolStripMenuItem _mnuDisplayColumn;
        private readonly ToolStripMenuItem _tsThumbNails;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="listView">Listview to handle</param>
        /// <param name="ts">Toolstrip to create options on</param>
        /// <param name="previewProvider">Provider of thumbnails</param>
        protected ListViewHelper(ListView listView, Core core, IPreviewProvider previewProvider)
        {
            this._listView = listView;
            this._previewProvider = previewProvider;
            this._core = core;

            _imgListNormal = listView.SmallImageList;
            _imgListPreviews = new ImageList();

            // Setup listview
            listView.VirtualMode = true;

            // Set events
            listView.RetrieveVirtualItem += _listView_RetrieveVirtualItem;
            listView.ItemActivate += _listView_ItemActivate;
            listView.MouseDown += _listView_MouseDown;

            listView.View = View.Details;
            listView.HeaderStyle = ColumnHeaderStyle.Clickable;
            listView.AllowColumnReorder = true;
            listView.FullRowSelect = true;
            listView.GridLines = true;
            listView.HideSelection = false;

            if (listView.SmallImageList == null)
            {
                listView.SmallImageList = new ImageList();
                listView.SmallImageList.ImageSize = new Size(24, 24);
                UiControls.PopulateImageList(listView.SmallImageList);
            }

            if (listView.LargeImageList == null)
            {
                listView.LargeImageList = listView.SmallImageList;
            }

            _toolStrip = new ToolStrip();
            _toolStrip.Dock = DockStyle.Top;
            _toolStrip.GripStyle = ToolStripGripStyle.Hidden;
            _toolStrip.BackColor = Color.FromKnownColor(KnownColor.Control);
            listView.Parent.Controls.Add(_toolStrip);
            _toolStrip.BringToFront();
            listView.BringToFront();

            _listView.ColumnClick += listView_ColumnClick;
            _listView.ColumnReordered += _listView_ColumnReordered;
            _listView.ColumnWidthChanged += _listView_ColumnWidthChanged;

            // Create columns button
            _btnColumns = new ToolStripDropDownButton("Columns", Resources.MnuColumn);
            _btnColumns.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _btnColumns.DropDownOpening += columnSelectMenu_DropDownOpening;
            _toolStrip.Items.Add(_btnColumns);

            // Menu: Export
            ToolStripDropDownButton tsSaveMain = new ToolStripDropDownButton("Export", Resources.MnuSaveList);
            tsSaveMain.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _toolStrip.Items.Add(tsSaveMain);

            // Menu: Export visible
            ToolStripMenuItem tsSave = new ToolStripMenuItem("Visible columns to file...", Resources.MnuSave, tsExportVisible_Click);
            tsSaveMain.DropDownItems.Add(tsSave);

            // Menu: Export all
            ToolStripMenuItem tsSaveAll = new ToolStripMenuItem("All columns to file...", Resources.MnuSaveAll, tsExportAll_Click);
            tsSaveMain.DropDownItems.Add(tsSaveAll);

            // Menu: Copy to clipboard
            ToolStripMenuItem tsSave2 = new ToolStripMenuItem("Visible columns to clipboard", Resources.MnuCopy, tsCopyToClipboard_Click);
            tsSaveMain.DropDownItems.Add(tsSave2);

            // Thumbnails
            if (previewProvider != null)
            {
                ToolStripDropDownButton tsView = new ToolStripDropDownButton("View", Resources.MnuViewMode);
                tsView.DisplayStyle = ToolStripItemDisplayStyle.Image;
                _toolStrip.Items.Add(tsView);

                ToolStripMenuItem tsExpand = new ToolStripMenuItem("Popout", Resources.MnuEnlarge, tsExpand_Click);
                tsView.DropDownItems.Add(tsExpand);

                _tsThumbNails = new ToolStripMenuItem("Thumbnails", Resources.MnuPreview, tsThumbNails_Click);
                tsView.DropDownItems.Add(_tsThumbNails);
            }

            // Create column menu
            _cmsColumns = new ContextMenuStrip();
            _mnuSortAscending = (ToolStripMenuItem)_cmsColumns.Items.Add("Sort ascending", Resources.MnuSortAscending, sortAscendingDescending_Click);
            _mnuSortDescending = (ToolStripMenuItem)_cmsColumns.Items.Add("Sort descending", Resources.MnuSortDescending, sortAscendingDescending_Click);
            _cmsColumns.Items.Add(new ToolStripSeparator());
            _mnuFilterColumn = (ToolStripMenuItem)_cmsColumns.Items.Add(@"=ADD/REMOVE FILTER", null, _filterm_Click);
            _mnuHideColumn = (ToolStripMenuItem)_cmsColumns.Items.Add("Hide column", null, _hidecol_Click);
            _mnuRenameColumn = (ToolStripMenuItem)_cmsColumns.Items.Add("Rename column", null, _mnuRenameColumn_Click);
            _mnuDisplayColumn = (ToolStripMenuItem)_cmsColumns.Items.Add(@"=DISPLAY MODE");
            _mnuColumnHelp = (ToolStripMenuItem)_cmsColumns.Items.Add("Help on this column...", Resources.MnuHelp, _descm_Click);

            // Display mode buttons
            foreach (EListDisplayMode displayMode in EnumHelper.GetEnumValues<EListDisplayMode>())
            {
                _mnuDisplayColumn.DropDownItems.Add(displayMode.ToUiString(), null, _mnuDisplayColumnItem_Click).Tag = displayMode;
            }

            // Create filter label
            _lblFilter = _toolStrip.Items.Add("FILTER");
            _lblFilter.DisplayStyle = ToolStripItemDisplayStyle.Text;
            _lblFilter.ForeColor = Color.Red;
            _lblFilter.Visible = false;
            _lblFilter.Click += _filterm_Click;
        }

        /// <summary>
        /// Menu: Column display mode.
        /// </summary>                
        private void _mnuDisplayColumnItem_Click(object sender, EventArgs e)
        {
            EListDisplayMode tag = (EListDisplayMode)((ToolStripMenuItem)sender).Tag;
            _clickedColumn.DisplayMode = tag;
            Rebuild(EListInvalids.ValuesChanged);
        }

        /// <summary>
        /// List item clicked
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

        /// <summary>
        /// Right clicked.
        /// </summary>    
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
                _core.Options.OpenColumn(true, _listView.Name, col.Id, ref col.OverrideDisplayName, ref col.Visible, ref col.Width, ref col.DisplayIndex);
            }
        }

        /// <summary>
        /// Loads columns from preferences.
        /// </summary>
        private void LoadColumnUserPreferences()
        {
            foreach (Column col in _availableColumns)
            {
                _core.Options.OpenColumn(false, _listView.Name, col.Id, ref col.OverrideDisplayName, ref col.Visible, ref col.Width, ref col.DisplayIndex);
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

            _mnuSortAscending.Checked = _sortOrder != null && _sortOrder.col == col && _sortOrder.ascending;
            _mnuSortAscending.Enabled = !col.DisableMenu;

            _mnuSortDescending.Checked = _sortOrder != null && _sortOrder.col == col && !_sortOrder.ascending;
            _mnuSortDescending.Enabled = !col.DisableMenu;

            _mnuFilterColumn.Text = _filter != null ? "Remove filter" : "Create filter...";
            _mnuFilterColumn.Tag = col;
            _mnuFilterColumn.Enabled = !col.DisableMenu || _filter != null;

            _mnuHideColumn.Enabled = !col.DisableMenu;

            _mnuColumnHelp.Visible = !string.IsNullOrWhiteSpace(col.Description);

            _mnuDisplayColumn.Text = "List display mode (" + col.DisplayMode.ToUiString() + ")";

            foreach (ToolStripMenuItem displayItem in _mnuDisplayColumn.DropDownItems)
            {
                displayItem.Checked = (EListDisplayMode)(displayItem.Tag) == col.DisplayMode;
            }

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

            // If many columns then allow the user to show a listbox instead
            if (_availableColumns.Count >= 50)
            {
                ToolStripMenuItem editColumnsAsList = new ToolStripMenuItem("(Column editor...)", Resources.MnuEdit, EditColumnsAsList_Click);
                ctrl.DropDownItems.Add(editColumnsAsList);
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

        private void EditColumnsAsList_Click(object sender, EventArgs e)
        {
            IEnumerable<Column> selected = DataSet.ForColumns(_availableColumns).ShowCheckList(this.ListView.FindForm(), _availableColumns.Where(z => !z.IsAlwaysEmpty && z.Visible));

            if (selected != null)
            {
                _availableColumns.ForEach(z => z.Visible = z.IsAlwaysEmpty || selected.Contains(z));
            }

            SaveColumnUserPreferences();
            Rebuild(EListInvalids.ToggleColumn);
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
        void sortAscendingDescending_Click(object sender, EventArgs e)
        {
            bool ascending = sender == _mnuSortAscending;

            if (_sortOrder != null && _sortOrder.col == _clickedColumn && _sortOrder.ascending == ascending)
            {
                _sortOrder = null;
            }
            else
            {
                _sortOrder = new SortByColumn(_clickedColumn, ascending);
            }

            Rebuild(EListInvalids.Sorted);
        }

        /// <summary>
        /// Menu: Rename column
        /// </summary>
        void _mnuRenameColumn_Click(object sender, EventArgs e)
        {
            string newName = FrmInput.Show(_listView.FindForm(), "Rename column", _clickedColumn.Id, "Enter a new name for this column", _clickedColumn.OverrideDisplayName);

            if (newName != null)
            {
                _clickedColumn.OverrideDisplayName = newName;
                _clickedColumn.Header.Text = _clickedColumn.ToString();
                SaveColumnUserPreferences();
            }
        }

        /// <summary>
        /// Menu: Export
        /// </summary>
        void tsExportVisible_Click(object sender, EventArgs e)
        {
            string fn = _listView.FindForm().BrowseForFile(null, UiControls.EFileExtension.Csv, FileDialogMode.SaveAs, UiControls.EInitialFolder.ExportedData);

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
                    ExportAll(fn);
                }
                catch (Exception ex)
                {
                    FrmMsgBox.ShowError(_listView.FindForm(), ex);
                }
            }
        }

        /// <summary>
        /// Exports all data
        /// </summary>                     
        public void ExportAll(string fileName)
        {
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                SaveItems(sw, true);
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

                if (!string.IsNullOrWhiteSpace(text))
                {
                    Clipboard.SetText(text);
                }
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

            int selected = FrmClusterSheet.Show(_listView.FindForm(), new Size(_core.Options.PopoutThumbnailSize, _core.Options.PopoutThumbnailSize), this.SourceList, this._previewProvider, highlight);

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
            EnablePreviews = !_enablePreviews;
            _tsThumbNails.Checked = EnablePreviews;
        }

        protected void ClearPreviewList()
        {
            if (_enablePreviews)
            {
                _imgListPreviews.Images.Clear();
                _imgListPreviews.ImageSize = new Size(_core.Options.ThumbnailSize, _core.Options.ThumbnailSize);
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

                if (_sortOrder != null && _sortOrder.col == c)
                {
                    if (_sortOrder.ascending)
                    {
                        h.ImageIndex = (int)UiControls.ImageListOrder.ListSortUp;
                    }
                    else
                    {
                        h.ImageIndex = (int)UiControls.ImageListOrder.ListSortDown;
                    }
                }
                else if (_filter != null && _filter.column == c)
                {
                    h.ImageIndex = (int)UiControls.ImageListOrder.ListFilter;
                }
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

        /// <summary>
        /// Gets the columns associated with the items in this list.
        /// </summary>                                              
        private IEnumerable<Column> GetVisualisablesColumns()
        {
            IVisualisable firstElement = GetSourceContent().FirstOrDefault();

            if (firstElement != null)
            {
                return firstElement.GetColumns(_core);
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
            var iconColumn = new Column<IVisualisable>("", EColumn.Visible, "This column contains the object icon", null);
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
                lvi.ImageIndex = (int)tag.GetIcon();
            }

            // Update columns
            foreach (ColumnHeader h in _listView.Columns)
            {
                Column c = (Column)h.Tag;

                System.Diagnostics.Debug.Assert(c.Visible);

                if (!c.IsAlwaysEmpty)
                {
                    ListViewItem.ListViewSubItem lvsi = new ListViewItem.ListViewSubItem();

                    string result = c.GetRowAsString(tag);
                    Color color = c.GetColour(tag);

                    if (!tag.Enabled)
                    {
                        lvsi.Font = FontHelper.StrikeFont;
                        color = Color.Gray;
                    }
                    else
                    {
                        lvsi.Font = FontHelper.RegularFont;
                    }

                    lvsi.Text = result;
                    lvsi.ForeColor = color;

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
                        sw.Write(", ");
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

            sw.WriteLine();

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
                            sw.Write(", ");
                        }
                        else
                        {
                            needsComma = true;
                        }

                        object v = c.GetRow(item);

                        if (v.GetType().IsPrimitive && !(v is char))
                        {
                            sw.Write(v);
                        }
                        else
                        {
                            string txt = Column.AsString(v, c.DisplayMode);

                            sw.Write("\"");
                            sw.Write(txt);
                            sw.Write("\"");
                        }
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
