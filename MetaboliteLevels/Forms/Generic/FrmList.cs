using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MetaboliteLevels.Algorithms.Statistics;
using MetaboliteLevels.Algorithms.Statistics.Clusterers;
using MetaboliteLevels.Controls;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Forms.Editing;
using MetaboliteLevels.Settings;
using MetaboliteLevels.Data.DataInfo;
using System.Diagnostics;

namespace MetaboliteLevels.Forms.Generic
{
    internal partial class FrmList : Form
    {
        private IFormList _handler;
        private IListValueSet opts;
        private int _numFields;
        private bool _multiSelect;

        private static IEnumerable<T> Show<T>(Form owner, IFormList handler, ListValueSet<T> opts, bool multiSelect)
        {
            using (FrmList frm = new FrmList(handler, opts, multiSelect))
            {
                if (UiControls.ShowWithDim(owner, frm) == DialogResult.OK)
                {
                    return opts.List.Cast<T>().Corresponding(frm.GetStates());
                }

                return null;
            }
        }

        private FrmList(IFormList handler, IListValueSet opts, bool multiSelect)
            : this()
        {
            this.Text = opts.Title;
            this.ctlTitleBar1.Text = opts.Title;
            this.ctlTitleBar1.SubText = opts.SubTitle;
            this._btnEdit.Visible = opts.ListEditor != null;
            this._editAction = opts.ListEditor;

            this._handler = handler;
            this._numFields = opts.UntypedList.CountAll();
            this.opts = opts;
            this._multiSelect = multiSelect;
            _flpSelectAll.Visible = multiSelect;

            this._handler.Initialise(this);

            RefreshList();
        }

        interface IFormList
        {
            void Initialise(FrmList form);
            void ClearItems();
            void Ready();
            bool GetState(int n);
            void SetState(int n, bool state);
            void AddItem(string text, string description);
        }

        class FormListListBox : IFormList
        {
            protected ListView listBox;

            public virtual void Initialise(FrmList form)
            {
                listBox = new ListView();

                listBox.Columns.Add(new ColumnHeader());

                listBox.View = View.Details;
                listBox.HeaderStyle = ColumnHeaderStyle.None;
                listBox.FullRowSelect = true;
                listBox.GridLines = true;
                listBox.MultiSelect = false;
                listBox.ShowItemToolTips = true;

                listBox.Dock = DockStyle.Fill;
                listBox.Margin = new Padding(8, 8, 8, 8);
                listBox.Visible = true;

                form.panel1.Controls.Add(listBox);
            }

            void IFormList.Ready()
            {
                listBox.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            }

            void IFormList.ClearItems()
            {
                listBox.Items.Clear();
            }

            bool IFormList.GetState(int n)
            {
                return listBox.Items[n].Selected;
            }

            void IFormList.SetState(int n, bool state)
            {
                listBox.Items[n].Selected = state;
            }

            void IFormList.AddItem(string text, string description)
            {
                ListViewItem lvi = new ListViewItem(text);
                lvi.ToolTipText = description;
                listBox.Items.Add(lvi);
            }
        }

        class FormListCheckList : FormListListBox, IFormList
        {
            public override void Initialise(FrmList form)
            {
                base.Initialise(form);
                base.listBox.CheckBoxes = true;
            }

            bool IFormList.GetState(int n)
            {
                return listBox.Items[n].Checked;
            }

            void IFormList.SetState(int n, bool state)
            {
                listBox.Items[n].Checked = state;
            }
        }

        abstract class FormListControlArray<T> : IFormList
            where T : Control, new()
        {
            FlowLayoutPanel listBox;
            ToolTip toolTip;
            protected List<T> checkBoxes = new List<T>();

            void IFormList.Initialise(FrmList form)
            {
                listBox = new FlowLayoutPanel();
                listBox.FlowDirection = FlowDirection.TopDown;
                listBox.Dock = DockStyle.Fill;
                listBox.WrapContents = false;
                listBox.AutoScroll = true;
                listBox.Location = new System.Drawing.Point(0, 0);
                listBox.Margin = new Padding(0, 0, 0, 0);
                listBox.Visible = true;

                toolTip = new ToolTip();
                toolTip.Active = true;

                form.panel1.Controls.Add(listBox);
            }

            void IFormList.ClearItems()
            {
                listBox.Controls.Clear();
                checkBoxes.Clear();
            }

            void IFormList.Ready()
            {
                // NA
            }

            bool IFormList.GetState(int n)
            {
                return GetState(checkBoxes[n]);
            }

            void IFormList.SetState(int n, bool value)
            {
                SetState(checkBoxes[n], value);
            }

            public abstract bool GetState(T control);
            public abstract void SetState(T control, bool state);

            void IFormList.AddItem(string text, string description)
            {
                T cb = new T();
                cb.AutoSize = true;
                cb.Text = text;
                cb.Visible = true;
                cb.Margin = new Padding(8, 8, 8, 0);
                toolTip.SetToolTip(cb, text);
                listBox.Controls.Add(cb);
                checkBoxes.Add(cb);
                InitialiseItem(cb);

                Label lb = new Label();
                lb.Text = description;
                lb.AutoSize = true;
                lb.Margin = new Padding(64, 0, 8, 8);
                lb.ForeColor = System.Drawing.Color.SteelBlue;
                lb.Font = new System.Drawing.Font(lb.Font.FontFamily.Name, 8);
                listBox.Controls.Add(lb);
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
                control.DialogResult = DialogResult.OK;
                control.ForeColor = System.Drawing.Color.Blue;
                control.Click += cb_Click;
            }

            void cb_Click(object sender, EventArgs e)
            {
                ((Button)sender).Tag = sender;
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

        private void RefreshList()
        {
            _handler.ClearItems();

            foreach (object name in opts.UntypedList)
            {
                _handler.AddItem(opts.UntypedName(name), opts.UntypedDescription(name));
            }

            foreach (var n in opts.SelectedStates.IndicesAndObject())
            {
                _handler.SetState(n.Key, n.Value);
            }

            UiControls.CompensateForVisualStyles(this);

            _handler.Ready();
        }

        private IEnumerable<bool> GetStates()
        {
            bool[] r = new bool[_numFields];

            for (int n = 0; n < _numFields; n++)
            {
                r[n] = _handler.GetState(n);
            }

            return r;
        }

        private FrmList()
        {
            InitializeComponent();
            UiControls.SetIcon(this);
        }

        public Action<Form> _editAction { get; set; }

        private void _btnEdit_Click(object sender, EventArgs e)
        {
            _editAction(this);

            RefreshList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!_multiSelect)
            {
                if (!GetStates().Any(z => z))
                {
                    FrmMsgBox.ShowError(this, "Please make a selection before continuing.");
                    return;
                }
            }

            DialogResult = DialogResult.OK;
        }

        internal static T ShowList<T>(Form owner, ListValueSet<T> listValueSet)
        {
            return Show<T>(owner, new FormListListBox(), listValueSet, false).FirstOrDefault(listValueSet.CancelValue);
        }

        public static T ShowRadio<T>(Form owner, ListValueSet<T> listValueSet)
        {
            Debug.Assert(listValueSet.List.Count() < 50, "When list count is large you might be better using a different view method.");
            return Show<T>(owner, new FormListRadioButtonArray(), listValueSet, false).FirstOrDefault(listValueSet.CancelValue);
        }

        public static T ShowButtons<T>(Form owner, ListValueSet<T> listValueSet)
        {
            Debug.Assert(listValueSet.List.Count() < 50, "When list count is large you might be better using a different view method.");
            return Show<T>(owner, new FormListButtonArray(), listValueSet, false).FirstOrDefault(listValueSet.CancelValue);
        }

        public static IEnumerable<T> ShowCheckList<T>(Form owner, ListValueSet<T> listValueSet)
        {
            return Show<T>(owner, new FormListCheckList(), listValueSet, true);
        }

        public static IEnumerable<T> ShowCheckBox<T>(Form owner, ListValueSet<T> listValueSet)
        {
            Debug.Assert(listValueSet.List.Count() < 50, "When list count is large you might be better using a different view method.");
            return Show<T>(owner, new FormListCheckBoxArray(), listValueSet, true);
        }

        private void _btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int n = 0; n < _numFields; n++)
            {
                _handler.SetState(n, true);
            }
        }

        private void _btnSelectNone_Click(object sender, EventArgs e)
        {
            for (int n = 0; n < _numFields; n++)
            {
                _handler.SetState(n, false);
            }
        }
    }
}
