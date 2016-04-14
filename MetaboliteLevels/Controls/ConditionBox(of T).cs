using MetaboliteLevels.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.DataInfo;
using MetaboliteLevels.Forms;
using System.Drawing;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Forms.Generic;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Settings;

namespace MetaboliteLevels.Controls
{
    /// <summary>
    /// A multiple choice list displayed as a textbox and button.
    /// </summary>
    internal class ConditionBox<T>
    {
        /// <summary>Dataset</summary>
        private readonly DataSet<T> _args;    
        /// <summary>The textbox</summary>
        public readonly TextBox TextBox;
        /// <summary>The button</summary>
        public readonly Button Button;
        /// <summary>The selection</summary>
        private HashSet<T> _selection = new HashSet<T>();
        /// <summary>If the selection is valid</summary>
        private bool _isSelectionValid;   

        /// <summary>
        /// Constructor.
        /// </summary>
        public ConditionBox(DataSet<T> args, TextBox textBox, Button button)
        {
            this._args = args;                                                         
            this.TextBox = textBox;
            this.Button = button;               

            textBox.TextChanged += _textBox_TextChanged;
            textBox.Validating += _textBox_Validating;
            button.Click += _button_Click;
        }
        /// <summary>
        /// Show list button clicked.
        /// </summary>
        void _button_Click(object sender, EventArgs e)
        {
            // Get available options
            DataSet<T> sel = _args.Clone();
                                       
            IEnumerable<T> newSelected;

            if (_args.TypedGetList(true).Count() < 10)
            {
                newSelected = sel.ShowCheckBox(TextBox.FindForm(), _selection);
            }
            else
            {
                newSelected = sel.ShowCheckList(TextBox.FindForm(), _selection);
            }

            if (newSelected != null)
            {
                _selection = new HashSet<T>(newSelected);
                _isSelectionValid = true;

                UpdateTextFromSelection();
            }
        }

        /// <summary>
        /// Update the list as we change.
        /// </summary>                   
        private void _textBox_TextChanged( object sender, EventArgs e )
        {
            UpdateListFromText();
        }                    

        /// <summary>
        /// Handler.
        /// </summary>
        void _textBox_Validating(object sender, CancelEventArgs e)
        {       
            // Nicely format selection (can only do if valuid)  
            if (_isSelectionValid)
            {   
                UpdateTextFromSelection();
            }
        }

        /// <summary>
        /// Gets a copy of the selected items set, or throws an error for an invalid selection.
        /// </summary>
        public HashSet<T> GetSelectedItemsOrThrow()
        {
            if (!_isSelectionValid)
            {
                throw new InvalidOperationException("Invalid selection.");
            }

            return new HashSet<T>(_selection);
        }

        /// <summary>
        /// Gets a copy of the selection items set, or returns NULL if the selection is invalid.
        /// </summary>
        public HashSet<T> GetSelectionOrNull()
        {
            if (!_isSelectionValid)
            {
                return null;
            }

            return new HashSet<T>( _selection );
        }

        /// <summary>
        /// Is the selection valid?
        /// </summary>
        public bool SelectionValid
        {
            get { return _isSelectionValid; }
        }

        /// <summary>
        /// Gets or sets the most recent valid selected items.
        /// 
        /// Returns NULL if the selection is invalid.
        /// Setting this to NULL will clear the selection.
        /// </summary>
        public IEnumerable<T> SelectedItems
        {
            get
            {   
                return GetSelectionOrNull();
            }
            set
            {
                if (value == null)
                {
                    _selection = new HashSet<T>();
                    _isSelectionValid = true;
                }
                else
                {
                    _selection = new HashSet<T>(value);
                    _isSelectionValid = true;
                }                                   

                UpdateTextFromSelection();
            }
        }

        /// <summary>
        /// Updates the textbox text to reflect the current selection.
        /// </summary>
        private void UpdateTextFromSelection()
        {
            if (_args.IntegerBehaviour)
            {
                TextBox.Text = StringHelper.ArrayToStringInt(_selection.Cast<int>());
            }
            else
            {
                StringBuilder sb = new StringBuilder();

                HashSet<T> sel = new HashSet<T>(_selection);

                foreach (var choice in _args.TypedGetList(true))
                {
                    if (sel.Contains(choice))
                    {
                        if (sb.Length != 0)
                        {
                            sb.Append(", ");
                        }

                        sb.Append( _args.ItemNameProvider( choice ));
                        sel.Remove(choice);
                    }
                }

                // Choices not in list
                foreach (T extraChoice in sel)
                {
                    if (sb.Length != 0)
                    {
                        sb.Append(", ");
                    }

                    sb.Append( _args.ItemNameProvider( extraChoice ) );
                }

                TextBox.Text = sb.ToString();
            }
        }        

        /// <summary>
        /// Updates the selection list to reflect the current text.
        /// </summary>
        private void UpdateListFromText()
        {
            IEnumerable<T> possibilities = _args.TypedGetList(true);
            _isSelectionValid = true;

            if (_args.IntegerBehaviour)
            {
                List<int> selected = StringHelper.StringToArrayInt(TextBox.Text, EErrorHandler.ReturnNull);

                if (selected == null || selected.Any( z => !possibilities.Cast<int>().Contains( z ) ))
                {
                    _isSelectionValid = false;
                }  

                _selection = new HashSet<T>(selected.Cast<T>()); 
            }
            else
            {
                string[] elements = TextBox.Text.Split(",".ToCharArray());

                HashSet<T> result = new HashSet<T>();

                foreach (string e in elements)
                {
                    if (!string.IsNullOrWhiteSpace(e))
                    {
                        T choice;

                        if (!TryGetChoice( possibilities, e, out choice ))
                        {
                            _isSelectionValid = false;
                        }
                        else
                        {
                            if (!_args.AllowNewEntries && !possibilities.Contains( choice ))
                            {
                                _isSelectionValid = false;
                            }

                            result.Add( choice );
                        }
                    }
                }

                _selection = result;            
            }
        }

        /// <summary>
        /// Trys to determine the choice based on the text.
        /// </summary>
        private bool TryGetChoice(IEnumerable<T> choices, string name, out T result)
        {
            name = name.ToUpper().Trim();

            if (_args.NewItemRetriever != null)
            {
                if (_args.NewItemRetriever(name, out result))
                {
                    return true;
                }
            }

            if (_args.StringComparator != null)
            {
                foreach (var i in choices)
                {
                    if (_args.StringComparator(name, i))
                    {
                        result = i;
                        return true;
                    }
                }
            }

            foreach (var choice in choices)
            {
                if (_args.ItemNameProvider != null && _args.ItemNameProvider(choice).ToUpper() == name)
                {
                    result = choice;
                    return true;
                }
                else if (choice.ToString().ToUpper() == name)
                {
                    result = choice;
                    return true;
                }
            }

            result = default(T);
            return false;
        }

        public bool Enabled
        {
            get
            {
                return TextBox.Enabled && Button.Enabled;
            }
            set
            {
                TextBox.Enabled = value;
                Button.Enabled = value;
            }
        }

        public bool Visible
        {
            get
            {
                return TextBox.Visible && Button.Visible;
            }
            set
            {
                TextBox.Visible = value;
                Button.Visible = value;
            }
        }
    }
}
