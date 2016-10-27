using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Database;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Gui.Controls.Lists;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Gui.Forms.Selection
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
            this._flpSelectAll.Visible = multiSelect;

            this._handler.Initialise(this, core);

            this.RefreshList(defaultSelection);
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
                this._dataType = dataType;
            }

            public void AddItem(object item, string text, string description )
            {
                this._list.Add(item);
            }

            public void ClearItems()
            {
                this._lvh.Clear();
            }

            public bool GetState(int n)
            {
                return this._lvh.SelectedIndex == n;
            }

            public void Initialise(FrmSelectList form, Core core)
            {
                this._listBox = new ListView();
                this._listBox.Dock = DockStyle.Fill;
                this._listBox.Margin = new Padding(8, 8, 8, 8);
                this._listBox.Visible = true;

                form.panel1.Controls.Add(this._listBox);
                this._lvh = new CtlAutoList(this._listBox, core, null);
            }

            public void Ready()
            {
                this._lvh.DivertList(this._list, this._dataType);
            }

            public void SetState(int n, bool state)
            {
                if (state)
                {
                    this._lvh.SelectedIndex = n;
                }
            }
        }

        class FormListListBox : IFormList
        {
            protected ListView _listBox;

            public virtual void Initialise(FrmSelectList form, Core core)
            {
                this._listBox = new ListView();

                this._listBox.Columns.Add(new ColumnHeader());

                this._listBox.View = View.Details;
                this._listBox.HeaderStyle = ColumnHeaderStyle.None;
                this._listBox.FullRowSelect = true;
                this._listBox.GridLines = true;
                this._listBox.MultiSelect = false;
                this._listBox.ShowItemToolTips = true;

                this._listBox.Dock = DockStyle.Fill;
                this._listBox.Margin = new Padding(8, 8, 8, 8);
                this._listBox.Visible = true;

                form.panel1.Controls.Add(this._listBox);
            }

            void IFormList.Ready()
            {
                this._listBox.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            }

            void IFormList.ClearItems()
            {
                this._listBox.Items.Clear();
            }

            bool IFormList.GetState(int n)
            {
                return this._listBox.Items[n].Selected;
            }

            void IFormList.SetState(int n, bool state)
            {
                this._listBox.Items[n].Selected = state;
            }

            void IFormList.AddItem(object item, string text, string description)
            {
                ListViewItem lvi = new ListViewItem(text);
                lvi.ToolTipText = description;
                this._listBox.Items.Add(lvi);
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
                return this._listBox.Items[n].Checked;
            }

            void IFormList.SetState(int n, bool state)
            {
                this._listBox.Items[n].Checked = state;
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
                this._form = form;

                this._listBox = new FlowLayoutPanel();
                this._listBox.FlowDirection = FlowDirection.TopDown;
                this._listBox.Dock = DockStyle.Fill;
                this._listBox.WrapContents = false;
                this._listBox.AutoScroll = true;
                this._listBox.Location = new System.Drawing.Point(0, 0);
                this._listBox.Margin = new Padding(0, 0, 0, 0);
                this._listBox.Visible = true;

                this._toolTip = new ToolTip();
                this._toolTip.Active = true;

                form.panel1.Controls.Add(this._listBox);
            }

            void IFormList.ClearItems()
            {
                this._listBox.Controls.Clear();
                this._checkBoxes.Clear();
            }

            void IFormList.Ready()
            {
                // NA
            }

            bool IFormList.GetState(int n)
            {
                return this.GetState(this._checkBoxes[n]);
            }

            void IFormList.SetState(int n, bool value)
            {
                this.SetState(this._checkBoxes[n], value);
            }

            public abstract bool GetState(T control);
            public abstract void SetState(T control, bool state);

            void IFormList.AddItem(object item, string text, string description)
            {
                T control = new T();
                control.AutoSize = true;
                control.Text = text;
                control.Visible = true;
                control.Margin = new Padding(8, 8, 8, 0);
                this._toolTip.SetToolTip(control, text);
                this._listBox.Controls.Add(control);
                this._checkBoxes.Add(control);
                this.InitialiseItem(control);

                if (description != null)
                {
                    Label lb = new Label();
                    lb.Text = description;
                    lb.AutoSize = true;
                    lb.Margin = new Padding( 64, 0, 8, 8 );
                    lb.ForeColor = System.Drawing.Color.SteelBlue;
                    lb.Font = new System.Drawing.Font( lb.Font.FontFamily.Name, 8 );
                    this._listBox.Controls.Add( lb );
                }
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
                //control.FlatStyle = FlatStyle.Flat;
                //control.FlatAppearance.BorderSize = 0; 
                //control.ForeColor = System.Drawing.Color.Blue;
                control.Click += this.cb_Click;
                control.Dock = DockStyle.Top;
                control.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            }

            void cb_Click(object sender, EventArgs e)
            {
                ((Button)sender).Tag = 1;
                this._form.CommitSelection();
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

            protected override void InitialiseItem( RadioButton item )
            {
                base.InitialiseItem( item );

                MethodInfo m = typeof( RadioButton ).GetMethod( "SetStyle", BindingFlags.Instance | BindingFlags.NonPublic );

                if (m != null)
                {
                    m.Invoke( item, new object[] { ControlStyles.StandardClick | ControlStyles.StandardDoubleClick, true } );
                }

                item.DoubleClick += this.item_DoubleClick;
            }

            private void item_DoubleClick( object sender, EventArgs e )
            {
                this._form.CommitSelection();
            }
        }

        private void RefreshList(IEnumerable selectedItems)
        {
            // Clear existing items
            this._handler.ClearItems();

            // Get the current items, and the selected items
            int n = 0;
            IEnumerable<object> source = this._opts.UntypedGetList(true).Cast<object>();
            IEnumerable<object> selected = selectedItems != null ? selectedItems.Cast<object>() : new object[0];
            HashSet<int> selectedIndices = new HashSet<int>();

            // Add the new items  
            foreach (object item in source)
            {
                this._objects.Add( item );
                this._handler.AddItem(item, this._opts.UntypedName(item), this._opts.UntypedDescription(item));

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
                    if (this._opts.DynamicEntries)
                    {
                        this._handler.AddItem( item, this._opts.UntypedName( item ), this._opts.UntypedDescription( item ) );
                        this._objects.Add( item );
                    }
                    else
                    {
                        this._handler.AddItem( item, MISSING_LABEL + " " + this._opts.UntypedName( item ), this._opts.UntypedDescription( item ) );
                        this._invalidIndices.Add( n );
                    }

                    selectedIndices.Add( n );  
                    n++;
                }
            }

            // Finalise any controls
            // UiControls.CompensateForVisualStyles(this);
            this._handler.Ready();

            // Set the states
            for(int m = 0; m < n; m++)
            {            
                this._handler.SetState( m, selectedIndices.Contains( m ) );
            }
        }

        private IEnumerable<bool> GetStates()
        {
            bool[] r = new bool[this._objects.Count];

            for (int n = 0; n < this._objects.Count; n++)
            {
                r[n] = this._handler.GetState(n);
            }

            return r;
        }

        private FrmSelectList()
        {
            this.InitializeComponent();

            this._flpSelectAll.BackColor = UiControls.TitleBackColour;
            this._flpSelectAll.ForeColor = UiControls.TitleForeColour;

            UiControls.SetIcon(this);
        }

        private void _btnEdit_Click(object sender, EventArgs e)
        {
            this._opts.ShowListEditor(this);

            this.RefreshList(null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.CommitSelection();
        }

        private bool CommitSelection()
        {
            foreach (int invalidIndex in this._invalidIndices)
            {
                if (this._handler.GetState( invalidIndex ))
                {
                    FrmMsgBox.ShowError( this, $"One or more items marked \"{MISSING_LABEL}\" have been selected. Please review and deselect these items before continuing." );
                    return false;
                }
            }

            if (!this._multiSelect)
            {
                if (!this.GetStates().Any( z => z ))
                {
                    FrmMsgBox.ShowError( this, "Please make a selection before continuing." );
                    return false;
                }
            }

            this._result = this._objects.At( this.GetStates() ).ToArray();
            this.DialogResult = DialogResult.OK;
            this.Close();
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
            for (int n = 0; n < this._objects.Count; n++)
            {
                this._handler.SetState(n, true);
            }
        }

        private void _btnSelectNone_Click(object sender, EventArgs e)
        {
            for (int n = 0; n < this._objects.Count; n++)
            {
                this._handler.SetState(n, false);
            }
        }
    }
}
