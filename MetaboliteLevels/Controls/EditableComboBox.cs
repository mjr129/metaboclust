using MetaboliteLevels.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Forms.Editing;

namespace MetaboliteLevels.Controls
{
    internal class EditableComboBox
    {
        public static EditableComboBox<ObsFilter> ForObsFilter(ComboBox box, Button editButton, Core core)
        {
            return new EditableComboBox<ObsFilter>(box, editButton, core.AllObsFilters, null,
                                                   () => FrmBigList.ShowObsFilters(box.FindForm(), core), null, true, "All");
        }

        internal static EditableComboBox<PeakFilter> ForPeakFilter(ComboBox box, Button editButton, Core core)
        {
            return new EditableComboBox<PeakFilter>(box, editButton, core.AllPeakFilters, null,
                                                  () => FrmBigList.ShowPeakFilters(box.FindForm(), core), null, true, "All");
        }
    }

    class EditableComboBox<T>
    {
        public readonly ComboBox _box;
        private Button _button;
        private Func<T> _action;
        private Converter<T, string> _namer;
        private IEnumerable<T> _source;
        private Func<bool> _action2;
        private bool _allowNewItems;
        private string _includeNull;
        private NamedItem<T> _none;

        public EditableComboBox(ComboBox box, Button editButton, IEnumerable<T> source, Func<T> editAction, Func<bool> editAction2, Converter<T, string> namer, bool allowNewItems, string includeNull)
        {
            _box = box;
            _button = editButton;
            _action = editAction;
            _action2 = editAction2;
            Converter<T, string> f = z => z.ToString();
            _namer = namer ?? f;
            _source = source;
            _allowNewItems = allowNewItems;
            _includeNull = includeNull;

            if (_button != null)
            {
                _button.Click += _button_Click;
            }

            UpdateItems();
        }

        private void UpdateItems()
        {
            _box.Items.Clear();

            if (_includeNull != null)
            {
                _none = new NamedItem<T>(default(T), _includeNull);
                _box.Items.Add(_none);
            }

            _box.Items.AddRange(NamedItem.GetRange(_source, _namer).ToArray());
        }

        void _button_Click(object sender, EventArgs e)
        {
            if (_action != null)
            {
                T result = _action();

                if (result != null)
                {
                    UpdateItems();
                    SelectedItem = result;
                }
            }
            else if (_action2 != null)
            {
                if (_action2())
                {
                    T selection = SelectedItem;
                    UpdateItems();

                    if ((selection == null && (_includeNull != null)) || _box.Items.Contains(selection))
                    {
                        SelectedItem = selection;
                    }
                }
            }
        }

        /// <summary>
        /// Returns if an item is selected (including the null item).
        /// </summary>
        public bool HasSelection
        {
            get { return _box.SelectedItem != null; }
        }

        /// <summary>
        /// Clears the selection.
        /// </summary>
        public void ClearSelection()
        {
            _box.SelectedItem = null;
        }

        /// <summary>
        /// Selected item
        /// 
        /// Note: Setting to NULL when _includeNull is active will set this to the null item rather than clear the selection.
        /// In this case use ClearSelection() to clear the selection.
        /// 
        /// Null will be returned if the selection is clear OR if the null item is selected, HasSelection can be used to
        /// determine a selection has been made.
        /// </summary>
        public T SelectedItem
        {
            get
            {
                return NamedItem<T>.Extract(_box.SelectedItem);
            }
            set
            {
                if (value == null)
                {
                    if ((_includeNull != null))
                    {
                        _box.SelectedItem = _none;
                        return;
                    }
                    else
                    {
                        ClearSelection();
                        return;
                    }
                }

                if (_allowNewItems && value != null && !_box.Items.Contains(value))
                {
                    _box.Items.Insert(0, new NamedItem<T>(value, _namer));
                }

                _box.SelectedItem = value;
            }
        }

        public bool Enabled
        {
            get
            {
                return _box.Enabled && _button.Enabled;
            }
            set
            {
                _box.Enabled = value;
                _button.Enabled = value;
            }
        }

        public bool Visible
        {
            get
            {
                return _box.Visible && _button.Visible;
            }
            set
            {
                _box.Visible = value;
                _button.Visible = value;
            }
        }
    }
}
