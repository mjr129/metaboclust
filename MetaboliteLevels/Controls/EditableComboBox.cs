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
using MGui.Helpers;

namespace MetaboliteLevels.Controls
{
    class EditableComboBox<T>
    {
        public readonly ComboBox ComboBox;
        private Button _button;
        private DataSet<T> _config;          
        private ENullItemName _includeNull;
        private NamedItem<T> _none;

        public EditableComboBox(ComboBox box, Button editButton, DataSet<T> items, ENullItemName includeNull)
        {
            ComboBox = box;
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
            ComboBox.Items.Clear();

            if (_includeNull != ENullItemName.NoNullItem)
            {
                _none = new NamedItem<T>(default(T), _includeNull.ToUiString());
                ComboBox.Items.Add(_none);
            }

            ComboBox.Items.AddRange(NamedItem.GetRange<T>(_config.TypedGetList(true), _config.ItemTitle).ToArray());
        }

        void _button_Click(object sender, EventArgs e)
        {
            T orig = SelectedItem;

            if (_config.ShowListEditor(ComboBox.FindForm()))
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
            get { return ComboBox.SelectedItem != null; }
        }

        /// <summary>
        /// Clears the selection.
        /// </summary>
        public void ClearSelection()
        {
            ComboBox.SelectedItem = null;
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
                return NamedItem<T>.Extract(ComboBox.SelectedItem);
            }
            set
            {
                if (value == null)
                {
                    if ((_includeNull != ENullItemName.NoNullItem))
                    {
                        ComboBox.SelectedItem = _none;
                        return;
                    }
                    else
                    {
                        ClearSelection();
                        return;
                    }
                }         

                ComboBox.SelectedItem = value;
            }
        }

        public bool Enabled
        {
            get
            {
                return ComboBox.Enabled && _button.Enabled;
            }
            set
            {
                ComboBox.Enabled = value;

                if (_button != null)
                {
                    _button.Enabled = value;
                }
            }
        }

        public bool Visible
        {
            get
            {
                return ComboBox.Visible && _button.Visible;
            }
            set
            {
                ComboBox.Visible = value;

                if (_button != null)
                {
                    _button.Visible = value;
                }
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
