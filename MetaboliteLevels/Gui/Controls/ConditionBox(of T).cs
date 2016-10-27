using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Database;
using MGui.Controls;
using MGui.Datatypes;
using MGui.Helpers;

namespace MetaboliteLevels.Gui.Controls
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
        private HashSet<T> _selection;
        /// <summary>If the selection is valid</summary>
        private bool _isSelectionValid;   

        /// <summary>
        /// Constructor.
        /// </summary>
        public ConditionBox(DataSet<T> args, CtlTextBox textBox, Button button)
        {
            this._args = args;                                                         
            this.TextBox = textBox;
            this.Button = button;               

            textBox.TextChanged += this._textBox_TextChanged;
            textBox.Validating += this._textBox_Validating;
            textBox.Watermark = "(None)";
            button.Click += this._button_Click;

            this._selection = new HashSet<T>();
            this._isSelectionValid = true;
        }

        /// <summary>
        /// Show list button clicked.
        /// </summary>
        void _button_Click(object sender, EventArgs e)
        {
            // Get available options
            DataSet<T> sel = this._args.Clone();
                                       
            IEnumerable<T> newSelected;

            if (this._args.TypedGetList(true).Count() < 10)
            {
                newSelected = sel.ShowCheckBox(this.TextBox.FindForm(), this._selection);
            }
            else
            {
                newSelected = sel.ShowCheckList(this.TextBox.FindForm(), this._selection);
            }

            if (newSelected != null)
            {
                this._selection = new HashSet<T>(newSelected);
                this._isSelectionValid = true;

                this.UpdateText();
            }
        }

        /// <summary>
        /// Update the list as we change.
        /// </summary>                   
        private void _textBox_TextChanged( object sender, EventArgs e )
        {
            this.UpdateListFromText();
        }                    

        /// <summary>
        /// Handler.
        /// </summary>
        void _textBox_Validating(object sender, CancelEventArgs e)
        {       
            // Nicely format selection (can only do if valuid)  
            if (this._isSelectionValid)
            {   
                this.UpdateText();
            }
        }

        /// <summary>
        /// Gets a copy of the selected items set, or throws an error for an invalid selection.
        /// </summary>
        public HashSet<T> GetSelectedItemsOrThrow()
        {
            if (!this._isSelectionValid)
            {
                throw new InvalidOperationException("Invalid selection.");
            }

            return new HashSet<T>(this._selection);
        }

        /// <summary>
        /// Gets a copy of the selection items set, or returns NULL if the selection is invalid.
        /// </summary>
        public HashSet<T> GetSelectionOrNull()
        {
            if (!this._isSelectionValid)
            {
                return null;
            }

            return new HashSet<T>( this._selection );
        }

        /// <summary>
        /// Is the selection valid?
        /// </summary>
        public bool SelectionValid
        {
            get { return this._isSelectionValid; }
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
                return this.GetSelectionOrNull();
            }
            set
            {
                if (value == null)
                {
                    this._selection = new HashSet<T>();
                    this._isSelectionValid = true;
                }
                else
                {
                    this._selection = new HashSet<T>(value);
                    this._isSelectionValid = true;
                }                                   

                this.UpdateText();
            }
        }

        /// <summary>
        /// Updates the textbox text to reflect the current selection.
        /// 
        /// Can be called externally, for instance if item names have changed.
        /// </summary>
        public void UpdateText()
        {
            if (this._args.IntegerBehaviour)
            {
                this.TextBox.Text = StringHelper.ArrayToStringInt(this._selection.Cast<int>());
            }
            else
            {
                StringBuilder sb = new StringBuilder();

                HashSet<T> sel = new HashSet<T>(this._selection);

                foreach (var choice in this._args.TypedGetList(true))
                {
                    if (sel.Contains(choice))
                    {
                        if (sb.Length != 0)
                        {
                            sb.Append(", ");
                        }

                        sb.Append( this._args.ItemTitle( choice ));
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

                    sb.Append( this._args.ItemTitle( extraChoice ) );
                }

                this.TextBox.Text = sb.ToString();
            }
        }        

        /// <summary>
        /// Updates the selection list to reflect the current text.
        /// </summary>
        private void UpdateListFromText()
        {
            IEnumerable<T> possibilities = this._args.TypedGetList(true);
            this._isSelectionValid = true;

            if (this._args.IntegerBehaviour)
            {
                List<int> selected = StringHelper.StringToArrayInt(this.TextBox.Text, EErrorHandler.ReturnNull);

                if (selected == null || selected.Any( z => !possibilities.Cast<int>().Contains( z ) ))
                {
                    this._isSelectionValid = false;
                }  

                this._selection = new HashSet<T>(selected.Cast<T>()); 
            }
            else
            {
                string[] elements = this.TextBox.Text.Split(",".ToCharArray());

                HashSet<T> result = new HashSet<T>();

                foreach (string e in elements)
                {
                    if (!string.IsNullOrWhiteSpace(e))
                    {
                        T choice;

                        if (!this.TryGetChoice( possibilities, e, out choice ))
                        {
                            this._isSelectionValid = false;
                        }
                        else
                        {
                            if (!this._args.DynamicEntries && !possibilities.Contains( choice ))
                            {
                                this._isSelectionValid = false;
                            }

                            result.Add( choice );
                        }
                    }
                }

                this._selection = result;            
            }
        }

        /// <summary>
        /// Trys to determine the choice based on the text.
        /// </summary>
        private bool TryGetChoice(IEnumerable<T> choices, string name, out T result)
        {
            name = name.ToUpper().Trim();

            if (this._args.DynamicItemRetriever != null)
            {
                if (this._args.DynamicItemRetriever(name, out result))
                {
                    return true;
                }
            }

            if (this._args.StringComparator != null)
            {
                foreach (var i in choices)
                {
                    if (this._args.StringComparator(name, i))
                    {
                        result = i;
                        return true;
                    }
                }
            }

            foreach (var choice in choices)
            {
                if (this._args.ItemTitle != null && this._args.ItemTitle(choice).ToUpper() == name)
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
                return this.TextBox.Enabled && this.Button.Enabled;
            }
            set
            {
                this.TextBox.Enabled = value;
                this.Button.Enabled = value;
            }
        }

        public bool Visible
        {
            get
            {
                return this.TextBox.Visible && this.Button.Visible;
            }
            set
            {
                this.TextBox.Visible = value;
                this.Button.Visible = value;
            }
        }             
    }
}
