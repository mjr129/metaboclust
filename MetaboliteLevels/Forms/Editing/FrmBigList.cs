using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Controls.Lists;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Session.Singular;
using MetaboliteLevels.Forms.Activities;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Types.UI;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Forms.Editing
{
    /// <summary>
    /// Displays a list of any object for editing
    /// </summary>
    internal partial class FrmBigList : Form
    {
        private BackupManager _backups = new BackupManager();
        private object _automaticAddTemplate;
        private readonly IDataSet _config;
        private List<object> _originalList;
        private List<object> _list;
        private bool _activated;
        private readonly CtlAutoList _listViewHelper;
        private bool _keepChanges;

        public enum EShow
        {
            /// <summary>
            /// Default behaviour - the list is editable if the DataSet has a ListChangeAcceptor function set.
            /// </summary>
            Default,

            /// <summary>
            /// Read-only behaviour - neither the list nor its items are editable
            /// </summary>
            ReadOnly,

            /// <summary>
            /// Acceptor behaviour - the list is always editable (it is assumed the caller of the form does something with the result itself so the list doesn't need a ListChangeAcceptor).
            /// However, ListChangeAcceptor is still called if present.
            /// </summary>
            Acceptor
        }                   

        private FrmBigList(Core core, IDataSet config, EShow show, object automaticAddTemplate)
        {
            InitializeComponent();
            UiControls.SetIcon( this );
            UiControls.PopulateImageList( imageList1 );

            _listViewHelper = new CtlAutoList(listView1, core, null);
            listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;
            _listViewHelper.Activate += listView1_ItemActivate;

            _config = config;

            _automaticAddTemplate = automaticAddTemplate;

            Text = "List Editor";
            ctlTitleBar1.Text = config.Title;
            ctlTitleBar1.SubText = config.SubTitle;

            UpdateListFromSource();

            if (config.ListIsSelfUpdating)
            {
                _btnCancel.Visible = false;
            }

            if (!config.HasItemEditor)  // Required for ADD EDIT VIEW DUPLICATE
            {
                _btnAdd.Visible = false;
                _btnEdit.Visible = false;
                _btnView.Visible = false;
                _btnDuplicate.Visible = false;
            }

            if (!config.ListSupportsReorder) // Required for UP DOWN
            {
                _btnUp.Visible = false;
                _btnDown.Visible = false;
            }

            if ((!config.ListSupportsChanges && !config.ListIsSelfUpdating) && show != EShow.Acceptor) // Required for ANY MODIFICATION
            {
                _btnAdd.Visible = false;
                _btnDuplicate.Visible = false;
                _btnRemove.Visible = false;
                _btnOk.Visible = false;
                _btnCancel.Text = "Close";
            }

            if (show == EShow.ReadOnly) // || Required for ANY MODIFICATION
            {
                _btnAdd.Visible = false;
                _btnEdit.Visible = false;
                _btnDuplicate.Visible = false;
                _btnRemove.Visible = false;
                _btnEnableDisable.Visible = false;
                _btnUp.Visible = false;
                _btnDown.Visible = false;
                _btnOk.Visible = false;
                _btnCancel.Text = "Close";
            }

            // UiControls.CompensateForVisualStyles(this);
        }

        private void UpdateListFromSource()
        {
            _list = new List<object>( _config.UntypedGetList( false ).Cast<object>() );
            _listViewHelper.DivertList(_list, _config.DataType);
            _originalList = new List<object>( _list );
        }               

        public static IEnumerable Show(Form owner, Core core, IDataSet config, EShow show, object automaticAddTemplate)
        {
            using (var frm = new FrmBigList(core, config, show, automaticAddTemplate))
            {
                if (owner is FrmBigList)
                {
                    frm.Size = new Size(Math.Max(128, owner.Width - 32), Math.Max(128, owner.Height - 32));
                }

                if (UiControls.ShowWithDim(owner, frm) != DialogResult.OK)
                {
                    return null;
                }

                return frm._list;
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            if (!_keepChanges)
            {
                // Restore any backups (only needed if items are mutable)
                _backups.RestoreAll();

                // Restore the original list (only needed if source list was modified)
                if (_config.ItemsReferenceList)
                {
                    FrmWait.Show( this, "Reverting changes", null, z => _config.UntypedApplyChanges( _originalList, z, true ) );
                }
            }
        }

        private void HandleBackreferencing()
        {
            if (_config.ItemsReferenceList)
            {    
                FrmWait.Show( this, "Updating source", null, z => _config.UntypedApplyChanges( _list, z, true ) );
            }
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            if (!_activated)
            {
                _activated = true;

                if (_automaticAddTemplate != null)
                {
                    Visualisable o = (Visualisable)_config.UntypedEdit(this, _automaticAddTemplate, false, true);

                    if (o != null)
                    {
                        Replace(null, o);
                    }
                }
            }
        }

        private void _btnAdd_Click(object sender, EventArgs e)
        {
            HandleBackreferencing();

            Visualisable o = (Visualisable)(_config.UntypedEdit( this, null, false, false ));

            if (o != null)
            {
                Replace(null, o);
            }
        }

        private void Rename( Visualisable item )
        {   
            string name = item.OverrideDisplayName;
            string comment = item.Comment;

            if (FrmEditINameable.Show(this, item.DefaultDisplayName, "Rename", item.ToString(), item.DefaultDisplayName, ref name, ref comment, false, item))
            {
                _backups.Backup( item , "Name changed" );

                item.OverrideDisplayName = name;
                item.Comment = comment;        

                _listViewHelper.Rebuild(EListInvalids.ValuesChanged);
            }
        }     

        private void Replace(object remove, object create )
        {
            _config.UntypedBeforeReplace(this, remove, create);

            if (!_config.ListIsSelfUpdating)
            {
                if (remove != null)
                {
                    _list.Remove(remove);
                }

                if (create != null)
                {
                    _list.Add(create);
                }

                _listViewHelper.Rebuild(EListInvalids.ContentsChanged);
            }
            else
            {
                UpdateListFromSource();
            }

            if (create != null)
            {
                _listViewHelper.Selection = create;
            }
        }

        private void _btnView_Click(object sender, EventArgs e)
        {
            object o = GetSelected();

            if (o != null)
            {
                _config.UntypedEdit(this, o, true, true);
            }
        }

        private object GetSelected()
        {
            return _listViewHelper.Selection;
        }

        private void _btnEdit_Click(object sender, EventArgs e)
        {
            object original = GetSelected();
            object toEdit = original;

            if (original == null)
            {
                return;
            }

            HandleBackreferencing();
            _backups.Backup( toEdit, "Edited" );

            Visualisable modified = (Visualisable)_config.UntypedEdit(this, toEdit, false, false);

            if (modified == original)
            {
                // If they are the same object then only values have changed
                _listViewHelper.Rebuild(EListInvalids.ValuesChanged);
                _listViewHelper.Selection = modified;
                return;
            }
            else if (modified != null)
            {
                Replace(original, modified);
            }
        }

        private void _btnRemove_Click(object sender, EventArgs e)
        {
            object p = GetSelected();

            if (p == null)
            {
                return;
            }

            Replace(p, null);
        }

        private void listView1_ItemActivate(object sender, ListViewItemEventArgs e)
        {
            _btnEdit.PerformClick();
        }

        private void _btnRename_Click(object sender, EventArgs e)
        {
            Visualisable stat = GetSelected() as Visualisable;

            if (stat == null)
            {
                return;
            }

            Rename(stat);

            _listViewHelper.Rebuild(EListInvalids.ValuesChanged);
            _listViewHelper.Selection = stat;
        }

        private void _btnUp_Click(object sender, EventArgs e)
        {
            MoveItem(-1);
        }

        private void MoveItem(int direction)
        {
            object sel = GetSelected();

            if (sel == null)
            {
                return;
            }

            int newIndex = _list.IndexOf(sel) + direction;

            if (newIndex < 0)
            {
                newIndex = 0;
            }

            if (newIndex >= _list.Count)
            {
                newIndex = _list.Count - 1;
            }

            _list.Remove(sel);
            _list.Insert(newIndex, sel);

            _listViewHelper.Rebuild(EListInvalids.ContentsChanged);
        }

        private void _btnDown_Click(object sender, EventArgs e)
        {
            MoveItem(1);
        }

        private void _btnOk_Click(object sender, EventArgs e)
        {
            _keepChanges = true;  

            FrmWait.Show(this, "Applying changes", null, z => _config.UntypedApplyChanges(_list, z, false));

            _config.UntypedAfterApply(this, _list);

            DialogResult = DialogResult.OK;
        }

        private void _btnEnableDisable_Click(object sender, EventArgs e)
        {
            Visualisable first = GetSelected() as Visualisable;

            if (first == null)
            {
                return;
            }

            _backups.Backup( first, "Toggled enabled" );
            first.Hidden = !first.Hidden;         

            _listViewHelper.Rebuild(EListInvalids.ValuesChanged);
            _listViewHelper.Selection = first;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs ev)
        {
            object p = GetSelected();

            bool itemSelected = p != null;

            _btnRemove.Enabled = itemSelected;
            _btnView.Enabled = itemSelected;
            _btnEdit.Enabled = itemSelected;
            _btnRename.Enabled = itemSelected && IVisualisableExtensions.SupportsRename(p as Visualisable);
            _btnUp.Enabled = itemSelected;
            _btnDown.Enabled = itemSelected;
            _btnDuplicate.Enabled = itemSelected;
            _btnEnableDisable.Enabled = itemSelected && IVisualisableExtensions.SupportsDisable( p as Visualisable );

            if ((p as Visualisable)?.Hidden ?? false)
            {
                _btnEnableDisable.Text = "Disable";
                _btnEnableDisable.Image = Resources.MnuDisable;
            }
            else
            {
                _btnEnableDisable.Text = "Enable";
                _btnEnableDisable.Image = Resources.MnuEnable;
            }
        }

        private void _btnDuplicate_Click(object sender, EventArgs e)
        {
            object p = GetSelected();

            if (p == null)
            {
                return;
            }

            HandleBackreferencing();

            Visualisable o = (Visualisable)_config.UntypedEdit(this, p, false, true);

            if (o != null)
            {
                Replace(null, o);
            }
        }

        private void _btnCancel_Click( object sender, EventArgs e )
        {

        }
    }
}