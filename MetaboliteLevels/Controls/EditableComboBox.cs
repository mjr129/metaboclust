using MetaboliteLevels.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Forms.Editing;
using MetaboliteLevels.Forms.Generic;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Controls
{
    class EditableComboBox<T>
    {
        public readonly ComboBox _box;
        private Button _button;
        private DataSet<T> _config;          
        private ENullItemName _includeNull;
        private NamedItem<T> _none;

        public EditableComboBox(ComboBox box, Button editButton, DataSet<T> items, ENullItemName includeNull)
        {
            _box = box;
            _button = editButton;             
            _includeNull = includeNull;
            _config = items;

            if (_button != null)
            {
                _button.Click += _button_Click;
            }

            UpdateItems();
        }

        private void UpdateItems()
        {
            _box.Items.Clear();

            if (_includeNull != ENullItemName.NoNullItem)
            {
                _none = new NamedItem<T>(default(T), _includeNull.ToUiString());
                _box.Items.Add(_none);
            }

            _box.Items.AddRange(NamedItem.GetRange<T>(_config.TypedGetList(true), _config.ItemNameProvider).ToArray());
        }

        void _button_Click(object sender, EventArgs e)
        {
            T orig = SelectedItem;

            if (_config.ShowListEditor(_box.FindForm()))
            {
                UpdateItems();
                SelectedItem = orig;
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
                    if ((_includeNull != ENullItemName.NoNullItem))
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

    public enum ENullItemName
    {
        NoNullItem,
        All,
        None,
    }
}
