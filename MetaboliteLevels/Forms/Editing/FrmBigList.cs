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
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Forms.Algorithms;
using MetaboliteLevels.Forms.Algorithms.ClusterEvaluation;
using MetaboliteLevels.Forms.Generic;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Settings;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Viewers.Lists;
using MGui.Helpers;

namespace MetaboliteLevels.Forms.Editing
{
    /// <summary>
    ///     Displays the list of statistics available for viewing.
    /// </summary>
    internal partial class FrmBigList : Form
    {
        private sealed class OriginalStatus
        {
            public bool Required;
            public string OriginalComment;
            public bool OriginalEnabled;
            public string OriginalOverrideDisplayName;

            public OriginalStatus(string name, string comments, bool enabled)
            {
                this.OriginalOverrideDisplayName = name;
                this.OriginalComment = comments;
                this.OriginalEnabled = enabled;
            }
        }

        private object _automaticAddTemplate;
        private readonly IDataSet _config;
        private List<IVisualisable> _list;
        private readonly Dictionary<IVisualisable, OriginalStatus> _originalStatuses = new Dictionary<IVisualisable, OriginalStatus>();
        private bool _activated;
        private readonly ListViewHelper<IVisualisable> _listViewHelper;
        private bool _keepChanges;

        public enum EShow
        {
            /// <summary>
            /// Default behaviour - the list is editable if the DataSet has a ListChangeAcceptor function set.
            /// </summary>
            Default,

            /// <summary>
            /// Read-only behaviour - neither the list nor items are editable
            /// </summary>
            ReadOnly,

            /// <summary>
            /// Acceptor behaviour - the list is always editable (it is assumed the caller of the form does something with the result itself).
            /// ListChangeAcceptor is still called if present.
            /// </summary>
            Acceptor
        }                   

        private FrmBigList(Core core, IDataSet config, EShow show, object automaticAddTemplate)
        {
            InitializeComponent();
            UiControls.SetIcon( this );
            UiControls.PopulateImageList( imageList1 );

            _listViewHelper = new ListViewHelper<IVisualisable>(listView1, core, null, null);
            listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;
            _listViewHelper.Activate += listView1_ItemActivate;

            _config = config;

            _automaticAddTemplate = automaticAddTemplate;

            Text = "List Editor";
            ctlTitleBar1.Text = config.Title;
            ctlTitleBar1.SubText = config.SubTitle;

            UpdateListFromSource();

            if (config.ListChangesOnEdit)
            {
                _btnCancel.Visible = false;
            }

            if (!config.HasItemEditor)
            {
                _btnAdd.Visible = false;
                _btnEdit.Visible = false;
                _btnView.Visible = false;
                _btnDuplicate.Visible = false;
            }

            if (!config.ListSupportsReorder)
            {
                _btnUp.Visible = false;
                _btnDown.Visible = false;
            }

            if ((!config.ListSupportsChanges && !config.ListChangesOnEdit) && show != EShow.Acceptor)
            {
                _btnAdd.Visible = false;
                _btnDuplicate.Visible = false;
                _btnRemove.Visible = false;
                _btnOk.Visible = false;
                _btnCancel.Text = "Close";
            }

            if (show == EShow.ReadOnly) // || !hasEditor
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
            try
            {
                _list = new List<IVisualisable>(_config.UntypedGetList(false).Cast<IVisualisable>());
            }
            catch
            {
                _list = new List<IVisualisable>(_config.UntypedGetList(false).Cast<object>().Select(z => new VisualisableWrapper(_config, z)));
            }

            _listViewHelper.DivertList(_list);
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
                foreach (var kvp in this._originalStatuses)
                {
                    if (kvp.Value.Required)
                    {
                        kvp.Key.OverrideDisplayName = kvp.Value.OriginalOverrideDisplayName;
                        kvp.Key.Comment = kvp.Value.OriginalComment;
                        kvp.Key.Enabled = kvp.Value.OriginalEnabled;
                    }
                }
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
                    IVisualisable o = (IVisualisable)_config.UntypedEdit(this, _automaticAddTemplate, false, true);

                    if (o != null)
                    {
                        Replace(null, o);
                    }
                }
            }
        }

        private void _btnAdd_Click(object sender, EventArgs e)
        {
            IVisualisable o = (IVisualisable)(_config.UntypedEdit( this, null, false, false ));

            if (o != null)
            {
                Replace(null, o);
            }
        }

        private void Rename(IVisualisable item)
        {
            OriginalStatus origStatus = Get(item);

            string name = item.OverrideDisplayName;
            string comment = item.Comment;

            if (FrmEditINameable.Show(this, item.DefaultDisplayName, "Rename", item.ToString(), item.DefaultDisplayName, ref name, ref comment, false, item))
            {
                item.OverrideDisplayName = name;
                item.Comment = comment;

                origStatus.Required = true;

                _listViewHelper.Rebuild(EListInvalids.ValuesChanged);
            }
        }

        private OriginalStatus Get(IVisualisable o)
        {
            return _originalStatuses.GetOrCreate<IVisualisable, OriginalStatus>(o, GetUnchanged);
        }

        private OriginalStatus GetUnchanged(IVisualisable input)
        {
            return new OriginalStatus(input.OverrideDisplayName, input.Comment, input.Enabled);
        }

        private void Replace(IVisualisable remove, IVisualisable create)
        {
            _config.UntypedBeforeReplace(this, remove, create);

            if (!_config.ListChangesOnEdit)
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
            IVisualisable o = GetSelected();

            if (o != null)
            {
                _config.UntypedEdit(this, o, true, true);
            }
        }

        private IVisualisable GetSelected()
        {
            return _listViewHelper.Selection;
        }

        private void _btnEdit_Click(object sender, EventArgs e)
        {
            IVisualisable original = GetSelected();
            IVisualisable toEdit = original;

            if (original == null)
            {
                return;
            }

            IVisualisable modified = (IVisualisable)_config.UntypedEdit(this, toEdit, false, false);

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
            IVisualisable p = GetSelected();

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
            IVisualisable stat = GetSelected();

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
            IVisualisable sel = GetSelected();

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
            object status;

            if (!_config.UntypedPrepareForApply(this, _list, out status))
            {
                return;
            }

            FrmWait.Show(this, "Applying changes", null, z => _config.UntypedApplyChanges(_list, z, status));

            _config.UntypedAfterApply(this, _list, status);

            DialogResult = DialogResult.OK;
        }

        private void _btnEnableDisable_Click(object sender, EventArgs e)
        {
            IVisualisable first = GetSelected();

            if (first == null)
            {
                return;
            }

            OriginalStatus change = Get(first);

            first.Enabled = !first.Enabled;
            change.Required = true;

            _listViewHelper.Rebuild(EListInvalids.ValuesChanged);
            _listViewHelper.Selection = first;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs ev)
        {
            IVisualisable p = GetSelected();

            bool itemSelected = p != null;

            _btnRemove.Enabled = itemSelected;
            _btnView.Enabled = itemSelected;
            _btnEdit.Enabled = itemSelected;
            _btnRename.Enabled = itemSelected && IVisualisableExtensions.SupportsRename(p);
            _btnUp.Enabled = itemSelected;
            _btnDown.Enabled = itemSelected;
            _btnDuplicate.Enabled = itemSelected;
            _btnEnableDisable.Enabled = itemSelected && IVisualisableExtensions.SupportsDisable(p);

            if (itemSelected && Get(p).OriginalEnabled)
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
            IVisualisable p = GetSelected();

            if (p == null)
            {
                return;
            }

            IVisualisable o = (IVisualisable)_config.UntypedEdit(this, p, false, true);

            if (o != null)
            {
                Replace(null, o);
            }
        }
    }
}