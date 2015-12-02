using MetaboliteLevels.Controls;
using MetaboliteLevels.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Forms.Editing;

namespace MetaboliteLevels.Forms.Generic
{
    /// <summary>
    /// Represents a list of something - see ListValueSet(of T).
    /// </summary>
    interface IListValueSet
    {
        string Title { get; }
        string SubTitle { get; }
        IEnumerable UntypedList { get; }
        Action<Form> ListEditor { get; }
        IEnumerable<bool> SelectedStates { get; }

        string UntypedName(object x);
        string UntypedDescription(object x);
    }

    /// <summary>
    /// Represents a list of something with extra details for UI purposes.
    /// </summary>
    internal class ListValueSet<T> : IListValueSet
    {
        public delegate bool ComparatorDelegate(string name, T item);
        public delegate bool RetrieverDelegate(string name, out T item);
        public delegate T EditDelegate(Form owner, T defaultValue, bool readOnly);

        /// <summary>
        /// Title of the list
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Extra information about the selection (OPTIONAL)
        /// </summary>
        public string SubTitle { get; set; }

        /// <summary>
        /// Actual list of options
        /// </summary>
        public IEnumerable<T> List { get; set; }

        /// <summary>
        /// How the items are named
        /// </summary>
        public Converter<T, string> Namer { get; set; }

        /// <summary>
        /// How the items are described (e.g. tooltips)
        /// </summary>
        public Converter<T, string> Describer { get; set; }

        /// <summary>
        /// How to get icons for the list (OPTIONAL)
        /// </summary>
        public Converter<T, int> IconProvider { get; set; }

        /// <summary>
        /// How to modify the list (OPTIONAL)
        /// </summary>
        public Action<Form> ListEditor { get; set; }

        /// <summary>
        /// How to edit items in the list (OPTIONAL)
        /// </summary>
        public EditDelegate ItemEditor { get; set; }

        /// <summary>
        /// Which items in the list have been selected?
        /// </summary>
        private IEnumerable<bool> _selectedStates;

        /// <summary>
        /// Gets or sets the Integerbehaviour (for conversion to/from text only).
        /// Works in lieu of Namer.
        /// </summary>
        public bool IntegerBehaviour { get; set; }

        /// <summary>
        /// If no selection is made what is returned (usually null).
        /// </summary>
        public T CancelValue { get; set; }

        /// <summary>
        /// Comparator, identifies if a string corresponds to an item for text-lookups.
        /// x.ToString() and Namer(x) are automatically checked.
        /// </summary>
        public ComparatorDelegate Comparator { get; set; }

        /// <summary>
        /// Retriever, retrieves the item with the specified name.
        /// Lookups in the list are automatically performed, this is only for fast lookups
        /// or where the user can add extensions outside the list.
        /// </summary>
        public RetrieverDelegate Retriever { get; set; }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        public ListValueSet(ListValueSet<T> copyFrom)
        {
            Title = copyFrom.Title;
            SubTitle = copyFrom.SubTitle;
            List = copyFrom.List;
            Namer = copyFrom.Namer;
            Describer = copyFrom.Describer;
            ListEditor = copyFrom.ListEditor;
            _selectedStates = copyFrom._selectedStates;
            IntegerBehaviour = copyFrom.IntegerBehaviour;
            CancelValue = copyFrom.CancelValue;
            Comparator = copyFrom.Comparator;
            Retriever = copyFrom.Retriever;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public ListValueSet()
        {
            Namer = z => z.ToString();
            Describer = z => null;
            CancelValue = typeof(T) == typeof(int) ? (T)(object)int.MinValue : default(T);
        }

        /// <summary>
        /// Wrapper to untyped object.
        /// </summary>
        IEnumerable IListValueSet.UntypedList { get { return List; } }

        /// <summary>
        /// Wrapper to untyped Namer delegate.
        /// </summary>
        string IListValueSet.UntypedName(object x)
        {
            return Namer((T)x);
        }

        /// <summary>
        /// Wrapper to untyped Describer delegate.
        /// </summary>
        string IListValueSet.UntypedDescription(object x)
        {
            return Describer((T)x);
        }

        /// <summary>
        /// Which items in the list have been selected?
        /// </summary>
        public IEnumerable<bool> SelectedStates
        {
            get
            {
                if (_selectedStates == null)
                {
                    _selectedStates = Enumerable.Repeat<bool>(false, List.Count());
                }

                return _selectedStates;
            }
            set
            {
                _selectedStates = value;
            }
        }

        /// <summary>
        /// Marks the selected item as the only user selection.
        /// </summary>
        public T SelectedItem
        {
            get
            {
                return List.FirstOrDefault(CancelValue);
            }
            set
            {
                if (value == null)
                {
                    SelectedStates = List.Select(z => false);
                }
                else
                {
                    SelectedStates = List.Select(z => value.Equals(z));
                }
            }
        }

        /// <summary>
        /// Marks the selected items as the only user selection.
        /// </summary>
        public IEnumerable<T> SelectedItems
        {
            get
            {
                return List.Corresponding(SelectedStates);
            }
            set
            {
                SelectedStates = List.Select(value.Contains);
            }
        }

        /// <summary>
        /// Sets the user selection to all.
        /// </summary>
        internal bool? SelectedAll
        {
            get
            {
                if (SelectedStates.All(z => z))
                {
                    return true;
                }
                if (SelectedStates.All(z => !z))
                {
                    return false;
                }

                return null;
            }
            set
            {
                if (value.HasValue)
                {
                    SelectedStates = List.Select(z => value.Value);
                }
            }
        }

        /// <summary>
        /// Shows the list selection form (FrmList, single selection).
        /// </summary>
        public T ShowList(Form owner)
        {
            return FrmList.ShowList(owner, this);
        }

        /// <summary>
        /// Shows the big list (FrmBigList).
        /// </summary>         
        public List<T> ShowBigList(Form owner)
        {
            return FrmBigList.ShowGeneric(owner, this, false);
        }

        /// <summary>
        /// Shows the radio button selection form (FrmList, single selection).
        /// </summary>
        public T ShowRadio(Form owner)
        {
            return FrmList.ShowRadio(owner, this);
        }

        /// <summary>
        /// Shows the button selection form (FrmList, single selection).
        /// </summary>
        public T ShowButtons(Form owner)
        {
            return FrmList.ShowButtons(owner, this);
        }

        /// <summary>
        /// Shows the list selection form (FrmList, multiple selection).
        /// </summary>
        public IEnumerable<T> ShowCheckList(Form owner)
        {
            return FrmList.ShowCheckList(owner, this);
        }

        /// <summary>
        /// Shows the checkbox selection form (FrmList, single selection).
        /// </summary>
        public IEnumerable<T> ShowCheckBox(Form owner)
        {
            return FrmList.ShowCheckBox(owner, this);
        }

        /// <summary>
        /// Creates a ConditionBox (textbox and browse button) from the list.
        /// </summary>
        public ConditionBox<T> CreateConditionBox(TextBox textBox, Button button)
        {
            return new ConditionBox<T>(this, textBox, button);
        }

        /// <summary>
        /// Fluent interface for [SelectedAll].
        /// </summary>
        internal ListValueSet<T> SelectAll()
        {
            SelectedAll = true;
            return this;
        }

        /// <summary>
        /// Fluent interface for [SelectedItem].
        /// </summary>
        internal ListValueSet<T> Select(T def)
        {
            SelectedItem = def;
            return this;
        }

        /// <summary>
        /// Fluent interface for [SelectedItem].
        /// </summary>
        internal ListValueSet<T> Select(Func<T, bool> def)
        {
            return Select(this.List.Where(def));
        }

        /// <summary>
        /// Fluent interface for [SelectedItems].
        /// </summary>
        internal ListValueSet<T> Select(IEnumerable<T> def)
        {
            SelectedItems = def;
            return this;
        }

        /// <summary>
        /// Fluent interface for [SubTitle].
        /// </summary>
        internal ListValueSet<T> IncludeMessage(string subTitle)
        {
            SubTitle = subTitle;
            return this;
        }
    }
}
