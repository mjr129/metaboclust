using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Database;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Gui.Controls.Lists;
using MetaboliteLevels.Gui.Forms.Activities;
using MetaboliteLevels.Gui.Forms.Selection;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Utilities;
using Visualisable = MetaboliteLevels.Data.Session.Associational.Visualisable;

namespace MetaboliteLevels.Gui.Forms.Editing
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
            this.InitializeComponent();
            UiControls.SetIcon( this );                

            this._listViewHelper = new CtlAutoList(this.listView1, core, null);
            this.listView1.SelectedIndexChanged += this.listView1_SelectedIndexChanged;
            this._listViewHelper.Activate += this.listView1_ItemActivate;

            this._config = config;

            this._automaticAddTemplate = automaticAddTemplate;

            this.Text = "List Editor";
            this.ctlTitleBar1.Text = config.Title;
            this.ctlTitleBar1.SubText = config.SubTitle;

            this.UpdateListFromSource();

            if (config.ListIsSelfUpdating)
            {
                this._btnCancel.Visible = false;
            }

            if (!config.HasItemEditor)  // Required for ADD EDIT VIEW DUPLICATE
            {
                this._btnAdd.Visible = false;
                this._btnEdit.Visible = false;
                this._btnView.Visible = false;
                this._btnDuplicate.Visible = false;
            }

            if (!config.ListSupportsReorder) // Required for UP DOWN
            {
                this._btnUp.Visible = false;
                this._btnDown.Visible = false;
            }

            if ((!config.ListSupportsChanges && !config.ListIsSelfUpdating) && show != EShow.Acceptor) // Required for ANY MODIFICATION
            {
                this._btnAdd.Visible = false;
                this._btnDuplicate.Visible = false;
                this._btnRemove.Visible = false;
                this._btnOk.Visible = false;
                this._btnCancel.Text = "Close";
            }

            if (show == EShow.ReadOnly) // || Required for ANY MODIFICATION
            {
                this._btnAdd.Visible = false;
                this._btnEdit.Visible = false;
                this._btnDuplicate.Visible = false;
                this._btnRemove.Visible = false;
                this._btnEnableDisable.Visible = false;
                this._btnUp.Visible = false;
                this._btnDown.Visible = false;
                this._btnOk.Visible = false;
                this._btnCancel.Text = "Close";
            }

            // UiControls.CompensateForVisualStyles(this);
        }

        private void UpdateListFromSource()
        {
            this._list = new List<object>( this._config.UntypedGetList( false ).Cast<object>() );
            this._listViewHelper.DivertList(this._list, this._config.DataType);
            this._originalList = new List<object>( this._list );
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

            if (!this._keepChanges)
            {
                // Restore any backups (only needed if items are mutable)
                this._backups.RestoreAll();

                // Restore the original list (only needed if source list was modified)
                if (this._config.ItemsReferenceList)
                {
                    FrmWait.Show( this, "Reverting changes", null, z => this._config.UntypedApplyChanges( this._originalList, z, true ) );
                }
            }
        }

        private void HandleBackreferencing()
        {
            if (this._config.ItemsReferenceList)
            {    
                FrmWait.Show( this, "Updating source", null, z => this._config.UntypedApplyChanges( this._list, z, true ) );
            }
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            if (!this._activated)
            {
                this._activated = true;

                if (this._automaticAddTemplate != null)
                {
                    Visualisable o = (Visualisable)this._config.UntypedEdit(this, this._automaticAddTemplate, false, true);

                    if (o != null)
                    {
                        this.Replace(null, o);
                    }
                }
            }
        }

        private void _btnAdd_Click(object sender, EventArgs e)
        {
            this.HandleBackreferencing();

            Visualisable o = (Visualisable)(this._config.UntypedEdit( this, null, false, false ));

            if (o != null)
            {
                this.Replace(null, o);
            }
        }

        private void Rename( Visualisable item )
        {   
            string name = item.OverrideDisplayName;
            string comment = item.Comment;

            if (FrmEditINameable.Show(this, item.DefaultDisplayName, "Rename", item.ToString(), item.DefaultDisplayName, ref name, ref comment, false, item))
            {
                this._backups.Backup( item , "Name changed" );

                item.OverrideDisplayName = name;
                item.Comment = comment;        

                this._listViewHelper.Rebuild(EListInvalids.ValuesChanged);
            }
        }     

        private void Replace(object remove, object create )
        {
            this._config.UntypedBeforeReplace(this, remove, create);

            if (!this._config.ListIsSelfUpdating)
            {
                if (remove != null)
                {
                    this._list.Remove(remove);
                }

                if (create != null)
                {
                    this._list.Add(create);
                }

                this._listViewHelper.Rebuild(EListInvalids.ContentsChanged);
            }
            else
            {
                this.UpdateListFromSource();
            }

            if (create != null)
            {
                this._listViewHelper.Selection = create;
            }
        }

        private void _btnView_Click(object sender, EventArgs e)
        {
            object o = this.GetSelected();

            if (o != null)
            {
                this._config.UntypedEdit(this, o, true, true);
            }
        }

        private object GetSelected()
        {
            return this._listViewHelper.Selection;
        }

        private void _btnEdit_Click(object sender, EventArgs e)
        {
            object original = this.GetSelected();
            object toEdit = original;

            if (original == null)
            {
                return;
            }

            this.HandleBackreferencing();
            this._backups.Backup( toEdit, "Edited" );

            Visualisable modified = (Visualisable)this._config.UntypedEdit(this, toEdit, false, false);

            if (modified == original)
            {
                // If they are the same object then only values have changed
                this._listViewHelper.Rebuild(EListInvalids.ValuesChanged);
                this._listViewHelper.Selection = modified;
                return;
            }
            else if (modified != null)
            {
                this.Replace(original, modified);
            }
        }

        private void _btnRemove_Click(object sender, EventArgs e)
        {
            object p = this.GetSelected();

            if (p == null)
            {
                return;
            }

            this.Replace(p, null);
        }

        private void listView1_ItemActivate(object sender, ListViewItemEventArgs e)
        {
            this._btnEdit.PerformClick();
        }

        private void _btnRename_Click(object sender, EventArgs e)
        {
            Visualisable stat = this.GetSelected() as Visualisable;

            if (stat == null)
            {
                return;
            }

            this.Rename(stat);

            this._listViewHelper.Rebuild(EListInvalids.ValuesChanged);
            this._listViewHelper.Selection = stat;
        }

        private void _btnUp_Click(object sender, EventArgs e)
        {
            this.MoveItem(-1);
        }

        private void MoveItem(int direction)
        {
            object sel = this.GetSelected();

            if (sel == null)
            {
                return;
            }

            int newIndex = this._list.IndexOf(sel) + direction;

            if (newIndex < 0)
            {
                newIndex = 0;
            }

            if (newIndex >= this._list.Count)
            {
                newIndex = this._list.Count - 1;
            }

            this._list.Remove(sel);
            this._list.Insert(newIndex, sel);

            this._listViewHelper.Rebuild(EListInvalids.ContentsChanged);
        }

        private void _btnDown_Click(object sender, EventArgs e)
        {
            this.MoveItem(1);
        }

        private void _btnOk_Click(object sender, EventArgs e)
        {
            this._keepChanges = true;

            try
            {
                FrmWait.Show( this, "Applying changes", null, z => this._config.UntypedApplyChanges( this._list, z, false ) );
            }
            catch (TaskCanceledException ex)
            {
                FrmMsgBox.ShowError( this, ex );
                return;
            }

            this._config.UntypedAfterApply(this, this._list);

            this.DialogResult = DialogResult.OK;
        }

        private void _btnEnableDisable_Click(object sender, EventArgs e)
        {
            Visualisable first = this.GetSelected() as Visualisable;

            if (first == null)
            {
                return;
            }

            this._backups.Backup( first, "Toggled enabled" );
            first.Hidden = !first.Hidden;         

            this._listViewHelper.Rebuild(EListInvalids.ValuesChanged);
            this._listViewHelper.Selection = first;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs ev)
        {
            object p = this.GetSelected();

            bool itemSelected = p != null;

            this._btnRemove.Enabled = itemSelected;
            this._btnView.Enabled = itemSelected;
            this._btnEdit.Enabled = itemSelected;
            this._btnRename.Enabled = itemSelected && IVisualisableExtensions.SupportsRename(p as Visualisable);
            this._btnUp.Enabled = itemSelected;
            this._btnDown.Enabled = itemSelected;
            this._btnDuplicate.Enabled = itemSelected;
            this._btnEnableDisable.Enabled = itemSelected && IVisualisableExtensions.SupportsDisable( p as Visualisable );

            if ((p as Visualisable)?.Hidden ?? false)
            {
                this._btnEnableDisable.Text = "Unhide";
                this._btnEnableDisable.Image = Resources.MnuEnable;
            }
            else
            {
                this._btnEnableDisable.Text = "Hide";
                this._btnEnableDisable.Image = Resources.MnuDisable;
            }
        }

        private void _btnDuplicate_Click(object sender, EventArgs e)
        {
            object p = this.GetSelected();

            if (p == null)
            {
                return;
            }

            this.HandleBackreferencing();

            Visualisable o = (Visualisable)this._config.UntypedEdit(this, p, false, true);

            if (o != null)
            {
                this.Replace(null, o);
            }
        }

        private void _btnCancel_Click( object sender, EventArgs e )
        {

        }
    }
}