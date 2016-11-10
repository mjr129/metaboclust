using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Database;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Session.Main;
using MetaboliteLevels.Gui.Controls.Charts;
using MetaboliteLevels.Gui.Datatypes;
using MetaboliteLevels.Gui.Forms.Activities;
using MetaboliteLevels.Gui.Forms.Editing;
using MetaboliteLevels.Gui.Forms.Selection;
using MetaboliteLevels.Gui.Forms.Text;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Gui.Controls.Lists
{
    /// <summary>
    /// Attaches to a listview and manages its contents, columns and menu options.
    /// </summary>
    internal partial class CtlAutoList : IDisposable, ICoreWatcher
    {                 
        public Core _core;  // link to core
        private IDataSet _source;

        protected ListView _listView; // listview control to use                                                    
        private IPreviewProvider _previewProvider; // thumbnail provider
        private bool _enablePreviews; // if in thumbnail mode
        protected readonly ToolStrip _toolStrip;

        public event EventHandler<ShowContextMenuEventArgs> ShowContextMenu; // right clicked item
        public event EventHandler<ListViewItemEventArgs> Activate; // double clicked item

        private List<Column> _availableColumns = new List<Column>();

        private ContextMenuStrip _cmsColumns;
        private ToolStripMenuItem _mnuSortAscending;
        private ToolStripMenuItem _mnuSortDescending;
        private ToolStripMenuItem _mnuFilterColumn;
        private ToolStripMenuItem _mnuHideColumn;
        private ToolStripMenuItem _mnuRenameColumn;
        private ToolStripMenuItem _mnuColumnHelp;

        private ToolStripButton _btnColumns;
        private ToolStripItem _lblFilter;
        private ToolStripItem _lblHidden;

        private bool _isCreatingColumns;
        private Column _clickedColumn;

        private SortByColumn _sortOrder;
        private ColumnFilter _filter;
        private bool _showHidden;

        protected List<object> _filteredList;

        private bool _emptyList;
        private ToolStripMenuItem _mnuDisplayColumn;
        private readonly ToolStripMenuItem _tsThumbNails;
        private string _listViewOptionsKey;
        private readonly ToolStripMenuItem _mnuViewAsHeatMap;
        private bool _suspendVirtual;
        private ImageListHelper _imageList;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="listView">Listview to handle</param>
        /// <param name="ts">Toolstrip to create options on</param>
        /// <param name="previewProvider">Provider of thumbnails</param>
        public CtlAutoList(ListView listView, Core core, IPreviewProvider previewProvider)
        {
            this._listView = listView;
            this._previewProvider = previewProvider;
            this._core = core;                    

            // Setup listview
            listView.VirtualMode = true;

            // Set events
            listView.RetrieveVirtualItem += this._listView_RetrieveVirtualItem;
            listView.ItemActivate += this._listView_ItemActivate;
            listView.MouseDown += this._listView_MouseDown;
            listView.KeyDown += ListView_KeyDown;
            listView.SearchForVirtualItem += ListView_SearchForVirtualItem;

            listView.View = View.Details;
            listView.HeaderStyle = ColumnHeaderStyle.Clickable;
            listView.AllowColumnReorder = true;
            listView.FullRowSelect = true;
            listView.GridLines = true;
            listView.HideSelection = false;

            Debug.Assert( listView.SmallImageList == null, "Didn't expect the listView to possess an imageList." );
            Debug.Assert( listView.LargeImageList == null, "Didn't expect the listView to possess an imageList." );

            listView.SmallImageList = new ImageList();
            listView.SmallImageList.ImageSize = new Size( 24, 24 );
            this._imageList = new ImageListHelper( listView.SmallImageList );
            listView.LargeImageList = listView.SmallImageList;

            this._toolStrip = new ToolStrip();
            this._toolStrip.Dock = DockStyle.Top;
            this._toolStrip.GripStyle = ToolStripGripStyle.Hidden;
            this._toolStrip.BackColor = Color.FromKnownColor(KnownColor.Control);
            listView.Parent.Controls.Add(this._toolStrip);
            this._toolStrip.BringToFront();
            listView.BringToFront();

            this._listView.ColumnClick += this.listView_ColumnClick;
            this._listView.ColumnReordered += this._listView_ColumnReordered;
            this._listView.ColumnWidthChanged += this._listView_ColumnWidthChanged;

            // Create columns button
            this._btnColumns = new ToolStripButton("Columns", Resources.MnuColumn);
            this._btnColumns.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            this._btnColumns.Click += this.EditColumnsAsList_Click;
            this._toolStrip.Items.Add(this._btnColumns);

            // Menu: Export
            ToolStripDropDownButton tsSaveMain = new ToolStripDropDownButton("Export", Resources.MnuSaveList);
            tsSaveMain.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            this._toolStrip.Items.Add(tsSaveMain);

            // Menu: Export\Save
            ToolStripMenuItem tsSave = new ToolStripMenuItem("Visible columns to file...", Resources.MnuSave, this.tsExportVisible_Click);
            tsSaveMain.DropDownItems.Add(tsSave);

            // Menu: Export\Save all
            ToolStripMenuItem tsSaveAll = new ToolStripMenuItem("All columns to file...", null, this.tsExportAll_Click);
            tsSaveMain.DropDownItems.Add(tsSaveAll);

            // Menu: Export\Copy
            ToolStripMenuItem tsSave2 = new ToolStripMenuItem("Visible columns to clipboard", Resources.MnuCopy, this.menu_export_copy);
            tsSaveMain.DropDownItems.Add(tsSave2);

            // Menu: Export\Copy all
            ToolStripMenuItem tsSave3 = new ToolStripMenuItem( "All columns to clipboard", null, this.menu_export_copyAll );
            tsSaveMain.DropDownItems.Add( tsSave3 );

            // Thumbnails
            if (previewProvider != null)
            {
                ToolStripDropDownButton tsView = new ToolStripDropDownButton("View", Resources.MnuViewMode);
                tsView.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                this._toolStrip.Items.Add(tsView);

                ToolStripMenuItem tsExpand = new ToolStripMenuItem("Popout", Resources.MnuEnlarge, this.tsExpand_Click);
                tsView.DropDownItems.Add(tsExpand);

                this._tsThumbNails = new ToolStripMenuItem("Thumbnails", Resources.MnuPreview, this.tsThumbNails_Click);
                tsView.DropDownItems.Add(this._tsThumbNails);
            }

            ////////////////////////
            // CREATE COLUMN MENU
            this._cmsColumns = new ContextMenuStrip();
            this._mnuSortAscending = (ToolStripMenuItem)this._cmsColumns.Items.Add("Sort ascending", Resources.MnuSortAscending, this.sortAscendingDescending_Click);
            this._mnuSortDescending = (ToolStripMenuItem)this._cmsColumns.Items.Add("Sort descending", Resources.MnuSortDescending, this.sortAscendingDescending_Click);
            this._cmsColumns.Items.Add(new ToolStripSeparator());
            this._mnuFilterColumn = (ToolStripMenuItem)this._cmsColumns.Items.Add(@"=ADD/REMOVE FILTER", null, this._filterm_Click);
            this._mnuHideColumn = (ToolStripMenuItem)this._cmsColumns.Items.Add("Hide column", null, this._hidecol_Click);
            this._mnuRenameColumn = (ToolStripMenuItem)this._cmsColumns.Items.Add("Rename column", null, this._mnuRenameColumn_Click);
            this._mnuDisplayColumn = (ToolStripMenuItem)this._cmsColumns.Items.Add(@"=DISPLAY MODE");
            this._mnuColumnHelp = (ToolStripMenuItem)this._cmsColumns.Items.Add("Help on this column...", Resources.MnuHelp, this._descm_Click);
            this._mnuViewAsHeatMap = (ToolStripMenuItem)this._cmsColumns.Items.Add( "View as heatmap", Resources.MnuView, this._mnuViewAsHeatMap_Click );

            // Display mode buttons
            foreach (EListDisplayMode displayMode in EnumHelper.GetEnumValues<EListDisplayMode>())
            {
                this._mnuDisplayColumn.DropDownItems.Add(displayMode.ToUiString(), null, this._mnuDisplayColumnItem_Click).Tag = displayMode;
            }

            // Create filter label
            this._lblFilter = this._toolStrip.Items.Add("FILTER");
            this._lblFilter.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this._lblFilter.ForeColor = Color.Red;
            this._lblFilter.Visible = false;
            this._lblFilter.Click += this._filterm_Click;

            // Create filter label
            this._lblHidden = this._toolStrip.Items.Add( "FILTER" );
            this._lblHidden.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this._lblHidden.ForeColor = Color.Green;
            this._lblHidden.Visible = false;
            this._lblHidden.Click += this._hiddenm_Click;
        }

        private void ListView_SearchForVirtualItem( object sender, SearchForVirtualItemEventArgs e )
        {
            for (int n = e.StartIndex + 1; n < _filteredList.Count; ++n)
            {
                if (SearchForVirtualItem( n, e ))
                {
                    return;
                }
            }

            for (int n = 0; n < e.StartIndex; ++n)
            {
                if (SearchForVirtualItem( n, e ))
                {
                    return;
                }
            }
        }

        private bool SearchForVirtualItem( int n, SearchForVirtualItemEventArgs e )
        {
            if (_filteredList[n].ToString().ToUpper().StartsWith( e.Text.ToUpper() ))
            {
                e.Index = n;
                return true;
            }

            return false;
        }

        private void ListView_KeyDown( object sender, KeyEventArgs e )
        {
            if (e.KeyCode == Keys.Delete)
            {
                Visualisable selection = this.Selection as Visualisable;

                if (selection != null)
                {
                    selection.Hidden = !selection.Hidden;
                    e.Handled = true;
                    return;
                }
            }

            if (this._core.Options.EnablePeakFlagging)
            {
                foreach (UserFlag f in this._core.Options.UserFlags)
                {
                    if (f.Key == (char)e.KeyCode)
                    {
                        IFlaggable flaggable = this.Selection as IFlaggable;

                        if (flaggable == null)
                        {
                            FrmMsgBox.ShowInfo( _listView.FindForm(), "Toggle flag", $"You have bound the key \"{f.Key}\" to toggle the \"{f.DisplayName}\" flag, but the selection in the list does not support flags.", FrmMsgBox.EDontShowAgainId.ToggleFlagFailed);
                            return;
                        }

                        NativeMethods.Beep( f.BeepFrequency, f.BeepDuration );

                        if (e.Control)
                        {
                            bool add = !flaggable.UserFlags.Contains( f );

                            if (FrmMsgBox.ShowOkCancel( _listView.FindForm(), f.ToString(), (add ? "Apply this flag to" : "Remove this flag from") + " all peaks shown in list?" ))
                            {
                                foreach (object xx in this.GetVisible())
                                {
                                    IFlaggable peak = xx as IFlaggable;

                                    if (peak == null)
                                    {
                                        continue;
                                    }

                                    if (add)
                                    {
                                        if (!peak.UserFlags.Contains( f ))
                                        {
                                            peak.UserFlags.Add( f );
                                        }
                                    }

                                    if (!add)
                                    {
                                        if (peak.UserFlags.Contains( f ))
                                        {
                                            peak.UserFlags.Remove( f );
                                        }
                                    }
                                }

                                this.Rebuild( EListInvalids.ValuesChanged );
                            }
                        }
                        else
                        {
                            if (!flaggable.UserFlags.Contains( f ))
                            {
                                flaggable.UserFlags.Add( f );
                            }
                            else
                            {
                                flaggable.UserFlags.Remove( f );  
                            }

                            this.Update( flaggable );
                        }

                        e.Handled = true;
                        return;
                    }
                }
            }
        }

        internal void DivertList<T>( IEnumerable<T> results ) 
        {                        
            this.DivertList( new DataSet<T>()
            {
                Core = this._core,
                ListTitle = "List",
                ListSource = results,
                ItemTitle = z => z.ToString(),
                ItemDescription = z => (z as Visualisable)?.Comment,
                Icon = Resources.IconData,
            } );
        }

        internal void DivertList( IEnumerable results, Type type )
        {
            this.DivertList( new DataSet<object>()
            {
                Core = this._core,
                ListTitle = "List",
                ListSource = results.Cast<object>(),
                ItemTitle = z => z.ToString(),
                ItemDescription = z => (z as Visualisable)?.Comment,
                Icon = Resources.IconData,
                DataType = type,
            } );
        }

        /// <summary>
        /// Column menu: View as heatmap
        /// </summary>                  
        private void _mnuViewAsHeatMap_Click( object sender, EventArgs e )
        {
            FrmActHeatMap.Show( this._core, this, this._clickedColumn );
        }

        /// <summary>
        /// Menu: Column display mode.
        /// </summary>                
        private void _mnuDisplayColumnItem_Click(object sender, EventArgs e)
        {
            EListDisplayMode tag = (EListDisplayMode)((ToolStripMenuItem)sender).Tag;
            this._clickedColumn.DisplayMode = tag;
            this.Rebuild(EListInvalids.ValuesChanged);
        }

        /// <summary>
        /// List item clicked
        /// </summary>
        void _listView_ItemActivate(object sender, EventArgs e)
        {
            this.ActivateByIndex(this.SelectedIndex);
        }

        private void ActivateByIndex(int index)
        {
            if (index != -1)
            {
                this.OnActivate(this._filteredList[index]);
            }
        }

        private void OnShowContextMenu(object selection, Control control, int x, int y)
        {
            if (this.ShowContextMenu != null)
            {
                ShowContextMenuEventArgs e = new ShowContextMenuEventArgs(selection, control, x, y);
                this.ShowContextMenu(this, e);
            }
        }

        /// <summary>
        /// Right clicked.
        /// </summary>    
        void _listView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ListViewItem lvi = this._listView.GetItemAt(e.X, e.Y);

                if (lvi != null)
                {
                    lvi.Selected = true;
                    lvi.Focused = true;
                    lvi.EnsureVisible();

                    this.OnShowContextMenu(lvi.Tag, this._listView, e.X, e.Y);
                }
            }
        }

        /// <summary>
        /// Saves columns to preferences.
        /// </summary>
        private void SaveColumnUserPreferences()
        {
            if (this._core == null)
            {
                return;
            }

            foreach (Column col in this._availableColumns)
            {
                this._core.Options.OpenColumn(true, this._listViewOptionsKey, col);
            }
        }

        /// <summary>
        /// Loads columns from preferences.
        /// </summary>
        private void LoadColumnUserPreferences()
        {
            // If we have no core just return the default
            if (this._core == null)
            {
                return;
            }

            foreach (Column col in this._availableColumns)
            {
                this._core.Options.OpenColumn(false, this._listViewOptionsKey, col);
            }
        }       

        private Column GetColumnDefinition(int headerIndex)
        {
            var header = this._listView.Columns[headerIndex];

            foreach (Column column in this._availableColumns)
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
                this.ShowMenu(toolStripItem.OwnerItem as ToolStripMenuItem);
                toolStripItem.ShowDropDown();
            }
        }

        void _listView_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            if (!this._isCreatingColumns) // otherwise hasn't had the header set yet and GetColumnDefinition changes
            {
                var column = this.GetColumnDefinition(e.ColumnIndex);
                column.Width = this._listView.Columns[e.ColumnIndex].Width;
                this.SaveColumnUserPreferences();
            }
        }

        void _listView_ColumnReordered(object sender, ColumnReorderedEventArgs e)
        {
            if (!this._isCreatingColumns) // not sure if required
            {
                // This gets called before columns are reordered, so we must reproduce the reorder to find out the new one
                List<ColumnHeader> list = new List<ColumnHeader>(this._listView.Columns.Cast<ColumnHeader>());
                list.OrderBy(λ => λ.DisplayIndex);
                ColumnHeader item = list[e.OldDisplayIndex];
                list.RemoveAt(e.OldDisplayIndex);
                list.Insert(e.NewDisplayIndex, item);

                for (int n = 0; n < list.Count; n++)
                {
                    Column col = (Column)list[n].Tag;
                    col.DisplayIndex = n;
                }

                this.SaveColumnUserPreferences();
            }
        }

        /// <summary>
        /// Column header clicked.
        /// </summary>
        void listView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Dispose previous
            Column col = this.GetColumnDefinition(e.Column);
            this._clickedColumn = col;

            this._mnuSortAscending.Checked = this._sortOrder != null && this._sortOrder._col == col && this._sortOrder._ascending;
            this._mnuSortAscending.Enabled = !col.DisableMenu;

            this._mnuSortDescending.Checked = this._sortOrder != null && this._sortOrder._col == col && !this._sortOrder._ascending;
            this._mnuSortDescending.Enabled = !col.DisableMenu;

            this._mnuFilterColumn.Text = this._filter != null ? "Remove filter" : "Create filter...";
            this._mnuFilterColumn.Tag = col;
            this._mnuFilterColumn.Enabled = !col.DisableMenu || this._filter != null;

            this._mnuHideColumn.Enabled = !col.DisableMenu;

            this._mnuColumnHelp.Visible = !string.IsNullOrWhiteSpace(col.Comment);

            this._mnuDisplayColumn.Text = "List display mode (" + col.DisplayMode.ToUiString() + ")";

            foreach (ToolStripMenuItem displayItem in this._mnuDisplayColumn.DropDownItems)
            {
                displayItem.Checked = (EListDisplayMode)(displayItem.Tag) == col.DisplayMode;
            }

            this._cmsColumns.Show(Cursor.Position);
        }

        private void EditColumnsAsList_Click(object sender, EventArgs e)
        {
            var avail = this._availableColumns.Where( z => !z.IsAlwaysEmpty ).ToArray();
            IEnumerable<Column> selected = FrmEditColumns.Show( this.ListView.FindForm(), avail, avail.Where( z => z.Visible ), "Visible columns" );

            if (selected == null)
            {
                return;                                                                                  
            }

            this._availableColumns.ForEach( z => z.Visible = z.IsAlwaysEmpty || selected.Contains( z ) );
            this.SaveColumnUserPreferences();
            this.Rebuild(EListInvalids.ToggleColumn);
        }             

        /// <summary>
        /// Menu: Column help
        /// </summary>
        void _descm_Click(object sender, EventArgs e)
        {
            FrmInputMultiLine.ShowFixed(this.ListView.FindForm(), "Help", "Column Help", this._clickedColumn.Id, this._clickedColumn.Comment);
        }

        void _hiddenm_Click( object sender, EventArgs e )
        {
            _showHidden = !_showHidden;
            this.Rebuild( EListInvalids.Filter );

            if (_showHidden)
            {
                this._lblHidden.Text = "Showing hidden entries";
                this._lblHidden.Visible = true;
            }
        }

        void _filterm_Click(object sender, EventArgs e)
        {
            if (this._filter != null)
            {
                this._filter = null;
                this._lblFilter.Visible = false;
                this.Rebuild(EListInvalids.Filter);
            }
            else
            {
                var op = ListVieweHelper.EOperator.TextContains;
                string va = "";

                if (FrmEditColumnFilter.Show(this._listView.FindForm(), this._clickedColumn.Id, ref op, ref va))
                {
                    this._filter = new ColumnFilter(this, this._clickedColumn, op, va);
                    this._lblFilter.Visible = true;
                    this.Rebuild(EListInvalids.Filter);
                }
            }
        }

        /// <summary>
        /// Menu: Hide column
        /// </summary>
        void _hidecol_Click(object sender, EventArgs e)
        {
            this._clickedColumn.Visible = !this._clickedColumn.Visible;
            this.SaveColumnUserPreferences();
            this.Rebuild(EListInvalids.ToggleColumn);
        }

        /// <summary>
        /// Sort ascending clicked.
        /// </summary>
        void sortAscendingDescending_Click(object sender, EventArgs e)
        {
            bool ascending = sender == this._mnuSortAscending;

            if (this._sortOrder != null && this._sortOrder._col == this._clickedColumn && this._sortOrder._ascending == ascending)
            {
                this._sortOrder = null;
            }
            else
            {
                this._sortOrder = new SortByColumn(this._clickedColumn, ascending);
            }

            this.Rebuild(EListInvalids.Sorted);
        }

        /// <summary>
        /// Menu: Rename column
        /// </summary>
        void _mnuRenameColumn_Click(object sender, EventArgs e)
        {
            string newName = FrmInputSingleLine.Show(this._listView.FindForm(), "Rename column", this._clickedColumn.Id, "Enter a new name for this column", this._clickedColumn.OverrideDisplayName);

            if (newName != null)
            {
                this._clickedColumn.OverrideDisplayName = newName;
                this._clickedColumn.Header.Text = this._clickedColumn.ToString();
                this.SaveColumnUserPreferences();
            }
        }

        /// <summary>
        /// Menu: Export
        /// </summary>
        void tsExportVisible_Click(object sender, EventArgs e)
        {
            this.SaveItems( false );
        }

        /// <summary>
        /// Menu: Export all
        /// </summary>
        void tsExportAll_Click(object sender, EventArgs e)
        {
            this.SaveItems( true );
        }

        private void SaveItems(bool all)
        {
            string fn = this._listView.FindForm().BrowseForFile( null, UiControls.EFileExtension.Csv, FileDialogMode.SaveAs, UiControls.EInitialFolder.ExportedData );

            if (fn != null)
            {
                using (StreamWriter sw = new StreamWriter( fn ))
                {
                    this.WriteItems( sw, all );
                }
            }
        }      

        /// <summary>
        /// Menu: Export - Copy to clipboard
        /// </summary>
        void menu_export_copy(object sender, EventArgs e)
        {
            this.CopyItems( false );
        }

        /// <summary>
        /// Menu: Export - Copy all to clipboard
        /// </summary>
        void menu_export_copyAll( object sender, EventArgs e )
        {
            this.CopyItems( true );
        }

        void CopyItems(bool all)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (StreamWriter sw = new StreamWriter( ms ))
                {
                    this.WriteItems( sw, all );
                }

                string text = Encoding.UTF8.GetString( ms.ToArray() );

                if (!string.IsNullOrWhiteSpace( text ))
                {
                    Clipboard.SetText( text );
                }
            }
        }

        /// <summary>
        /// Sets the list to source from a ContentsRequest object.
        /// </summary>
        public void DivertList( IDataSet target )
        {
            this._source = target;               
            this.Rebuild( EListInvalids.SourceChanged );
        }

        /// <summary>
        /// Memory cleanup.
        /// </summary>
        public void Dispose()
        {         
            this.OnDisposing(true);
        }

        protected void OnActivate(object item)
        {
            if (this.Activate != null)
            {
                this.Activate(this, new ListViewItemEventArgs(item));
            }
        }

        /// <summary>
        /// Memory cleanup handler.
        /// </summary>
        protected virtual void OnDisposing(bool isDisposing)
        {
            if (isDisposing)
            {
                this._toolStrip.Dispose();
                this._imageList.Dispose();
                this._cmsColumns.Dispose();
            }
        }

        /// <summary>
        /// Listview handled by this object.
        /// </summary>
        public ListView ListView
        {
            get
            {
                return this._listView;
            }
        }

        /// <summary>
        /// Expand menu button clicked.
        /// Shows the expansion form.
        /// </summary>
        void tsExpand_Click(object sender, EventArgs e)
        {               
            int selected = FrmPopoutClusterSheet.Show( this._listView.FindForm(), new Size( this._core.Options.PopoutThumbnailSize, this._core.Options.PopoutThumbnailSize), this.SourceList, this._previewProvider);

            if (selected != -1)
            {
                this.SelectedIndex = selected;
                this.ActivateByIndex(selected);
            }
        }

        /// <summary>
        /// Whether an item is selected in the listview.
        /// </summary>
        public bool HasSelection
        {
            get
            {
                return this._listView.SelectedIndices.Count != 0;
            }
        }

        /// <summary>
        /// The selected index (-1 for none).
        /// </summary>
        public int SelectedIndex
        {
            get
            {
                if (this._listView.SelectedIndices.Count == 0)
                {
                    return -1;
                }

                return this._listView.SelectedIndices[0];
            }
            set
            {
                this._listView.SelectedIndices.Clear();

                if (value == -1)
                {
                    return;    
                }

                this._listView.SelectedIndices.Add(value);
                this._listView.EnsureVisible(value);
            }
        }

        private Image GeneratePreviewImage(object item)
        {
            if (!this.EnablePreviews)
            {
                return UiControls.GetImage( item );
            }

            Image img = this._previewProvider.ProvidePreview( this._imageList.ImageList.ImageSize, item);

            if (img == null)
            {
                return ChartHelperForClusters.CreatePlaceholderBitmap(item, this._imageList.ImageList.ImageSize);
            }

            return img;
        }                                             

        void tsThumbNails_Click(object sender, EventArgs e)
        {
            this.EnablePreviews = !this._enablePreviews;
            this._tsThumbNails.Checked = this.EnablePreviews;
        }             

        public bool EnablePreviews
        {
            get
            {
                return this._enablePreviews;
            }
            set
            {
                if (this._previewProvider == null)
                {
                    // Cannot enable previews with no preview provider
                    return;
                }

                if (this._enablePreviews != value)
                {
                    this._enablePreviews = value;
                    this._imageList.Clear();
                    this._imageList.ImageList.ImageSize = value ? new Size( 64, 64 ) : new Size( 24, 24 );
                    this.Rebuild(EListInvalids.PreviewsChanged);
                }
            }
        }

        private void CreateListViewColumnHeaders()
        {
            this._listView.Columns.Clear();

            this._isCreatingColumns = true;

            foreach (Column c in this._availableColumns.OrderBy(λ => λ.DisplayIndex))
            {
                if (c.Visible)
                {
                    var h = this._listView.Columns.Add(c.ToString());
                    c.Header = h;
                    h.Tag = c;
                }
            }

            if (this._listView.Columns.Count == 0 && this._availableColumns.Count != 0)
            {
                // If the user hides all the columns we won't be able to create new items
                // Then we won't know the item type.
                // Then we won't be able to show the column selector
                // So the user can never get the columns back
                // In short, don't hide all the columns.
                var h = this._listView.Columns.Add( "" );
                this._availableColumns[0].Header = h;
                h.Tag = this._availableColumns[0];
            }

            foreach (ColumnHeader h in this._listView.Columns)
            {
                Column c = (Column)h.Tag;
                h.Width = c.Width;

                if (this._sortOrder != null && this._sortOrder._col == c)
                {
                    if (this._sortOrder._ascending)
                    {
                        h.ImageIndex = this._imageList.GetAssociatedImageIndex( "ListIconSortUp", () => Resources.ListIconSortUp );
                    }
                    else
                    {
                        h.ImageIndex = this._imageList.GetAssociatedImageIndex( "ListIconSortDown", () => Resources.ListIconSortDown );
                    }
                }
                else if (this._filter != null && this._filter._column == c)
                {
                    h.ImageIndex = this._imageList.GetAssociatedImageIndex( "ListIconFilter", () => Resources.ListIconFilter );
                }
            }

            this._isCreatingColumns = false;
        }

        /// <summary>
        /// Reinitialises the list.
        /// </summary>
        /// <param name="checks">What to reinitialise</param>
        public void Rebuild(EListInvalids checks)
        {
            // Suspend updates otherwise we'll get columns for the wrong items (or vice versa)
            this._suspendVirtual = true;

            // Since we now use a virtual list when the list changes we can just refresh the source
            if (checks.HasFlag(EListInvalids._SourceChanged) | checks.HasFlag(EListInvalids._ContentsChanged))
            {
                checks = this.Rebuild__GetFilteredList(checks);
            }

            // If we have not yet got the columns we always need to get them regardless
            if (this._availableColumns.Count == 0)
            {
                checks |= EListInvalids._ColumnsChanged | EListInvalids._ColumnVisibilitiesChanged;
            }

            if (checks.HasFlag(EListInvalids._ColumnsChanged))
            {
                this.GetAvailableColumns();
            }

            if (checks.HasFlag(EListInvalids._ColumnVisibilitiesChanged))
            {
                this.CreateListViewColumnHeaders();
            }

            if (checks.HasFlag(EListInvalids._ValuesChanged))
            {
                // Since we now use a virtual list the only thing we need to update are the previews, the rest will update when we redraw
                this._imageList.Clear();
            }

            // Resume updates
            this._suspendVirtual = false;


            // Redraw everything
            if (this._listView.VirtualListSize != 0)
            {   
                this._listView.RedrawItems(0, this._listView.VirtualListSize - 1, true);
            }
        }

        private IEnumerable GetSourceContent()
        {
            return this._source?.UntypedGetList(false) ?? new object[0];
        }

        /// <summary>
        /// Gets the source list, accounting for sort-order, filter, etc.
        /// </summary>
        private EListInvalids Rebuild__GetFilteredList(EListInvalids checks)
        {
            this._filteredList = this.GetSourceContent().Cast<object>().ToList();

            if (!_showHidden && _source != null && typeof(Visualisable).IsAssignableFrom( _source.DataType ))
            {
                int remCount = this._filteredList.RemoveAll( z => ((Visualisable)z).Hidden );
                this._lblHidden.Text = remCount + " hidden";
                this._lblHidden.Visible = remCount != 0;
            }  

            if (this._emptyList && this._filteredList.Count != 0)
            {
                // List was empty, now is not - make sure to rebuild columns since we might have new ones
                checks |= EListInvalids._ColumnsChanged | EListInvalids._ColumnVisibilitiesChanged;
            }

            this._emptyList = this._filteredList.Count == 0;

            if (this._filter != null)
            {
                int remCount = this._filteredList.RemoveAll(this._filter.FilterRemove);
                this._lblFilter.Text = remCount + " filtered";
            }

            if (this._sortOrder != null)
            {
                this._filteredList.Sort(this._sortOrder);
            }
            
            this._listView.VirtualListSize = this._filteredList.Count;
            return checks;
        }

        private int GetListViewIndex( object itemToFind )
        {
            int r = this._filteredList.IndexOf(itemToFind);

            if (r == -1)
            {
                throw new KeyNotFoundException("Cannot find item \"" + itemToFind.ToString() + "\" in list.");
            }

            return r;
        }

        private ListViewItem CreateNewListViewItem(object tag)
        {
            ListViewItem lvi = new ListViewItem("");
            lvi.Tag = tag;
            lvi.UseItemStyleForSubItems = false;
            return lvi;
        }

        public void ActivateItem( object item )
        {
            this.Selection = item;
            this.OnActivate( item );
        }

        public object Selection
        {
            get
            {
                if (this._listView.SelectedIndices.Count == 0)
                {
                    return null;
                }

                return this._filteredList[this._listView.SelectedIndices[0]];
            }
            set
            {
                if (value == null)
                {
                    this._listView.SelectedIndices.Clear();
                    return;
                }

                int lvi = this.GetListViewIndex(value);

                this.SelectedIndex = lvi;
            }
        }

        /// <summary>
        /// Populates "_availableColumns"
        /// </summary>
        private void GetAvailableColumns()
        {
            this._availableColumns.Clear();

            // Get the type
            Type dataType = this._source?.DataType;

            if (dataType == null) // TODO: Is this ever the case?
            {
                this._listViewOptionsKey = null;
                return;
            }

            // Save the ID (this is used to load/save columns)
            this._listViewOptionsKey = this._listView.FindForm().Name + "\\" + this._listView.Name + "\\" + dataType.Name;

            // Get columns for the type "T"
            IEnumerable<Column> cols = ColumnManager.GetColumns( dataType, this._core );

            if (cols == null)
            {
                // Not yet available
                return;
            }

            // Get the icon column
            var iconColumn = new Column<Visualisable>("", EColumn.Visible, "This column contains the object icon", null, null);
            iconColumn.Width = 32;
            iconColumn.DisableMenu = true;

            // Add the columns
            this._availableColumns.Add(iconColumn);
            this._availableColumns.AddRange(cols);   

            // Apply preferences
            this.LoadColumnUserPreferences();
        }   

        void _listView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (this._suspendVirtual)
            {
                e.Item = new ListViewItem( new string[this._listView.Columns.Count] );
                return;
            }

            object tag = this._filteredList[e.ItemIndex];
            e.Item = this.CreateNewListViewItem(tag);
            this.DoUpdate(e.Item, tag);
            UiControls.Assert(e.Item.SubItems.Count == this._listView.Columns.Count, "ListViewItem doesn't have the expected number of subitems.");
        }

        /// <summary>
        /// Updates the item
        /// </summary>
        private void DoUpdate(ListViewItem lvi, object tag)
        {                       
            Visualisable vis = tag as Visualisable;

            // Update icon                                                                                    
            lvi.ImageIndex = this._imageList.GetAssociatedImageIndex( tag, () => this.GeneratePreviewImage( tag ) );

            // Update columns
            foreach (ColumnHeader h in this._listView.Columns)
            {
                Column c = (Column)h.Tag;

                System.Diagnostics.Debug.Assert(c.Visible );

                if (!c.IsAlwaysEmpty)
                {
                    ListViewItem.ListViewSubItem lvsi = new ListViewItem.ListViewSubItem();

                    string result = c.GetRowAsString(tag);
                    Color color;

                    if (vis?.Hidden ?? false)
                    {
                        lvsi.Font = FontHelper.StrikeFont;
                        color = Color.Gray;
                    }
                    else
                    {
                        lvsi.Font = FontHelper.RegularFont;
                        color = c.GetColour(tag);
                    }

                    lvsi.Text = result;
                    lvsi.ForeColor = color;

                    lvi.SubItems.Add(lvsi);
                }
            }

            // Invert selection?
            if (lvi.Selected)
            {
                this.Invert(lvi);
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
                return this._listView.Visible;
            }
            set
            {
                this._listView.Visible = value;
                this._toolStrip.Visible = value;
            }
        }

        internal IEnumerable<object> GetVisible()
        {
            return this._filteredList;
        }

        public void Update(object itemToFind)
        {
            int i = this.GetListViewIndex(itemToFind);
            this._listView.RedrawItems(i, i, false);

            //DoUpdate(, itemToFind);
        }

        /// <summary>
        /// Saves the grid as a CSV file.
        /// </summary>
        public void WriteItems(StreamWriter sw, bool includeHiddenColumns)
        {
            bool needsComma = false;

            IEnumerable<Column> columns;

            if (includeHiddenColumns)
            {
                columns = this._availableColumns;
            }
            else
            {
                columns = this._listView.Columns.Cast<ColumnHeader>().Select(z => (Column)z.Tag).ToArray();
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
            foreach (Visualisable item in this._filteredList)
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
            get { return this._filteredList; }
        }

        public void ChangeCore(Core newCore)
        {
            this._core = newCore;
            this.Rebuild(EListInvalids.SourceChanged);
        }
    }

    class ListViewItemEventArgs : EventArgs
    {
        public readonly object SelectedItem;

        public ListViewItemEventArgs( object item )
        {
            this.SelectedItem = item;
        }
    }
}
