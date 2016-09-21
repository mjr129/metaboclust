using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Controls.Lists;
using MetaboliteLevels.Data.Session.Singular;
using MetaboliteLevels.Types.UI;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Forms.Selection
{
    internal partial class FrmSelectList : Form
    {
        const string MISSING_LABEL = "𝑴𝒊𝒔𝒔𝒊𝒏𝒈";

        private IFormList _handler;
        private IDataSet _opts; 
        private bool _multiSelect;
        private readonly List<object> _objects = new List<object>();
        private readonly List<int> _invalidIndices = new List<int>();
        private object[] _result;

        private static IEnumerable<T> Show<T>(Form owner, IFormList handler, DataSet<T> opts, bool multiSelect, IEnumerable<T> defaultSelection)
        {
            using (FrmSelectList frm = new FrmSelectList(handler, opts, multiSelect, defaultSelection, opts.Core))
            {
                if (UiControls.ShowWithDim(owner, frm) == DialogResult.OK)
                {
                    return frm._result.Cast<T>();
                }

                return null;
            }
        }

        private FrmSelectList(IFormList handler, IDataSet opts, bool multiSelect, IEnumerable defaultSelection, Core core)
            : this()
        {
            this.Text = opts.Title;
            this.ctlTitleBar1.Text = opts.Title;
            this.ctlTitleBar1.SubText = opts.SubTitle;
            this._btnEdit.Visible = opts.ListSupportsChanges;

            this._handler = handler;                               
            this._opts = opts;
            this._multiSelect = multiSelect;            
            _flpSelectAll.Visible = multiSelect;

            this._handler.Initialise(this, core);

            RefreshList(defaultSelection);
        }

        interface IFormList
        {
            void Initialise(FrmSelectList form, Core core);
            void ClearItems();
            void Ready();
            bool GetState(int n);
            void SetState(int n, bool state);
            void AddItem(object item, string text, string description);
        }

        class FormListBigListBox : IFormList
        {
            private ListView _listBox;
            CtlAutoList _lvh;
            List<object> _list = new List<object>();
            Type _dataType;

            public FormListBigListBox( Type dataType )
            {
                _dataType = dataType;
            }

            public void AddItem(object item, string text, string description )
            {
                _list.Add(item);
            }

            public void ClearItems()
            {
                _lvh.Clear();
            }

            public bool GetState(int n)
            {
                return _lvh.SelectedIndex == n;
            }

            public void Initialise(FrmSelectList form, Core core)
            {
                _listBox = new ListView();
                _listBox.Dock = DockStyle.Fill;
                _listBox.Margin = new Padding(8, 8, 8, 8);
                _listBox.Visible = true;

                form.panel1.Controls.Add(_listBox);
                _lvh = new CtlAutoList(_listBox, core, null);
            }

            public void Ready()
            {
                _lvh.DivertList(_list, _dataType);
            }

            public void SetState(int n, bool state)
            {
                if (state)
                {
                    _lvh.SelectedIndex = n;
                }
            }
        }

        class FormListListBox : IFormList
        {
            protected ListView _listBox;

            public virtual void Initialise(FrmSelectList form, Core core)
            {
                _listBox = new ListView();

                _listBox.Columns.Add(new ColumnHeader());

                _listBox.View = View.Details;
                _listBox.HeaderStyle = ColumnHeaderStyle.None;
                _listBox.FullRowSelect = true;
                _listBox.GridLines = true;
                _listBox.MultiSelect = false;
                _listBox.ShowItemToolTips = true;

                _listBox.Dock = DockStyle.Fill;
                _listBox.Margin = new Padding(8, 8, 8, 8);
                _listBox.Visible = true;

                form.panel1.Controls.Add(_listBox);
            }

            void IFormList.Ready()
            {
                _listBox.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            }

            void IFormList.ClearItems()
            {
                _listBox.Items.Clear();
            }

            bool IFormList.GetState(int n)
            {
                return _listBox.Items[n].Selected;
            }

            void IFormList.SetState(int n, bool state)
            {
                _listBox.Items[n].Selected = state;
            }

            void IFormList.AddItem(object item, string text, string description)
            {
                ListViewItem lvi = new ListViewItem(text);
                lvi.ToolTipText = description;
                _listBox.Items.Add(lvi);
            }
        }

        class FormListCheckList : FormListListBox, IFormList
        {
            public override void Initialise(FrmSelectList form, Core core)
            {
                base.Initialise(form, core);
                base._listBox.CheckBoxes = true;
            }

            bool IFormList.GetState(int n)
            {
                return _listBox.Items[n].Checked;
            }

            void IFormList.SetState(int n, bool state)
            {
                _listBox.Items[n].Checked = state;
            }
        }

        abstract class FormListControlArray<T> : IFormList
            where T : Control, new()
        {
            protected FrmSelectList _form;
            FlowLayoutPanel _listBox;
            ToolTip _toolTip;
            protected List<T> _checkBoxes = new List<T>();

            void IFormList.Initialise(FrmSelectList form, Core core)
            {
                _form = form;

                _listBox = new FlowLayoutPanel();
                _listBox.FlowDirection = FlowDirection.TopDown;
                _listBox.Dock = DockStyle.Fill;
                _listBox.WrapContents = false;
                _listBox.AutoScroll = true;
                _listBox.Location = new System.Drawing.Point(0, 0);
                _listBox.Margin = new Padding(0, 0, 0, 0);
                _listBox.Visible = true;

                _toolTip = new ToolTip();
                _toolTip.Active = true;

                form.panel1.Controls.Add(_listBox);
            }

            void IFormList.ClearItems()
            {
                _listBox.Controls.Clear();
                _checkBoxes.Clear();
            }

            void IFormList.Ready()
            {
                // NA
            }

            bool IFormList.GetState(int n)
            {
                return GetState(_checkBoxes[n]);
            }

            void IFormList.SetState(int n, bool value)
            {
                SetState(_checkBoxes[n], value);
            }

            public abstract bool GetState(T control);
            public abstract void SetState(T control, bool state);

            void IFormList.AddItem(object item, string text, string description)
            {
                T cb = new T();
                cb.AutoSize = true;
                cb.Text = text;
                cb.Visible = true;
                cb.Margin = new Padding(8, 8, 8, 0);
                _toolTip.SetToolTip(cb, text);
                _listBox.Controls.Add(cb);
                _checkBoxes.Add(cb);
                InitialiseItem(cb);

                Label lb = new Label();
                lb.Text = description;
                lb.AutoSize = true;
                lb.Margin = new Padding(64, 0, 8, 8);
                lb.ForeColor = System.Drawing.Color.SteelBlue;
                lb.Font = new System.Drawing.Font(lb.Font.FontFamily.Name, 8);
                _listBox.Controls.Add(lb);
            }

            protected virtual void InitialiseItem(T item)
            {
                // NA
            }   
        }

        class FormListCheckBoxArray : FormListControlArray<CheckBox>
        {
            public override bool GetState(CheckBox control)
            {
                return control.Checked;
            }

            public override void SetState(CheckBox control, bool state)
            {
                control.Checked = state;
            }
        }

        class FormListButtonArray : FormListControlArray<Button>
        {
            public override bool GetState(Button control)
            {
                return control.Tag != null;
            }

            public override void SetState(Button control, bool state)
            {
                // NA
            }

            protected override void InitialiseItem(Button control)
            {
                control.FlatStyle = FlatStyle.Flat;
                control.FlatAppearance.BorderSize = 0; 
                control.ForeColor = System.Drawing.Color.Blue;
                control.Click += cb_Click;
                control.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            }

            void cb_Click(object sender, EventArgs e)
            {
                ((Button)sender).Tag = 1;
                _form.CommitSelection();
            }
        }

        class FormListRadioButtonArray : FormListControlArray<RadioButton>
        {
            public override bool GetState(RadioButton control)
            {
                return control.Checked;
            }

            public override void SetState(RadioButton control, bool state)
            {
                control.Checked = state;
            }
        }

        private void RefreshList(IEnumerable selectedItems)
        {
            // Clear existing items
            _handler.ClearItems();

            // Get the current items, and the selected items
            int n = 0;
            IEnumerable<object> source = _opts.UntypedGetList(true).Cast<object>();
            IEnumerable<object> selected = selectedItems != null ? selectedItems.Cast<object>() : new object[0];
            HashSet<int> selectedIndices = new HashSet<int>();

            // Add the new items  
            foreach (object item in source)
            {
                _objects.Add( item );
                _handler.AddItem(item, _opts.UntypedName(item), _opts.UntypedDescription(item));

                if (selected.Contains( item ))
                {
                    selectedIndices.Add( n );
                }

                n++;
            }

            // Add any selected items that aren't in the list (if _allowNewEntries is set)
            foreach (object item in selected)
            {
                if (!source.Contains(item))
                {
                    if (_opts.DynamicEntries)
                    {
                        _handler.AddItem( item, _opts.UntypedName( item ), _opts.UntypedDescription( item ) );
                        _objects.Add( item );
                    }
                    else
                    {
                        _handler.AddItem( item, MISSING_LABEL + " " + _opts.UntypedName( item ), _opts.UntypedDescription( item ) );
                        _invalidIndices.Add( n );
                    }

                    selectedIndices.Add( n );  
                    n++;
                }
            }

            // Finalise any controls
            // UiControls.CompensateForVisualStyles(this);
            _handler.Ready();

            // Set the states
            for(int m = 0; m < n; m++)
            {            
                _handler.SetState( m, selectedIndices.Contains( m ) );
            }
        }

        private IEnumerable<bool> GetStates()
        {
            bool[] r = new bool[_objects.Count];

            for (int n = 0; n < _objects.Count; n++)
            {
                r[n] = _handler.GetState(n);
            }

            return r;
        }

        private FrmSelectList()
        {
            InitializeComponent();

            _flpSelectAll.BackColor = UiControls.TitleBackColour;
            _flpSelectAll.ForeColor = UiControls.TitleForeColour;

            UiControls.SetIcon(this);
        }

        private void _btnEdit_Click(object sender, EventArgs e)
        {
            _opts.ShowListEditor(this);

            RefreshList(null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CommitSelection();
        }

        private bool CommitSelection()
        {
            foreach (int invalidIndex in _invalidIndices)
            {
                if (_handler.GetState( invalidIndex ))
                {
                    FrmMsgBox.ShowError( this, $"One or more items marked \"{MISSING_LABEL}\" have been selected. Please review and deselect these items before continuing." );
                    return false;
                }
            }

            if (!_multiSelect)
            {
                if (!GetStates().Any( z => z ))
                {
                    FrmMsgBox.ShowError( this, "Please make a selection before continuing." );
                    return false;
                }
            }

            _result = _objects.At( GetStates() ).ToArray();
            DialogResult = DialogResult.OK;
            Close();
            return true;
        }

        internal static T ShowList<T>(Form owner, DataSet<T> listValueSet, T defaultSelection)
        {
            return Show<T>(owner, new FormListBigListBox(typeof(T)), listValueSet, false, AsArray(defaultSelection, listValueSet)).FirstOrDefault(listValueSet.CancelValue);
        }

        private static IEnumerable<T> AsArray<T>(T defaultSelection, DataSet<T> listValueSet)
        {
            if (defaultSelection == null || defaultSelection.Equals(listValueSet.CancelValue))
            {
                return new T[0];
            }
            else
            {
                return new T[] { defaultSelection };
            }
        }

        public static T ShowRadio<T>(Form owner, DataSet<T> listValueSet, T defaultSelection)
        {
            Debug.Assert(listValueSet.TypedGetList(true).Count() < 50, "When list count is large you might be better using a different view method.");
            return Show<T>(owner, new FormListRadioButtonArray(), listValueSet, false, AsArray(defaultSelection, listValueSet)).FirstOrDefault(listValueSet.CancelValue);
        }

        public static T ShowButtons<T>(Form owner, DataSet<T> listValueSet, T defaultSelection)
        {
            Debug.Assert(listValueSet.TypedGetList(true).Count() < 50, "When list count is large you might be better using a different view method.");
            return Show<T>(owner, new FormListButtonArray(), listValueSet, false, AsArray(defaultSelection, listValueSet)).FirstOrDefault(listValueSet.CancelValue);
        }

        public static IEnumerable<T> ShowCheckList<T>(Form owner, DataSet<T> listValueSet, IEnumerable<T> defaultSelection)
        {
            return Show<T>(owner, new FormListCheckList(), listValueSet, true, defaultSelection);
        }

        public static IEnumerable<T> ShowCheckBox<T>(Form owner, DataSet<T> listValueSet, IEnumerable<T> defaultSelection)
        {
            Debug.Assert(listValueSet.TypedGetList(true).Count() < 50, "When list count is large you might be better using a different view method.");
            return Show<T>(owner, new FormListCheckBoxArray(), listValueSet, true, defaultSelection);
        }

        private void _btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int n = 0; n < _objects.Count; n++)
            {
                _handler.SetState(n, true);
            }
        }

        private void _btnSelectNone_Click(object sender, EventArgs e)
        {
            for (int n = 0; n < _objects.Count; n++)
            {
                _handler.SetState(n, false);
            }
        }
    }
}
