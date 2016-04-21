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
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Settings;

namespace MetaboliteLevels.Forms.Generic
{
    /// <summary>
    /// Represents a collection of objects, with additional information on how to represent those
    /// objects in the GUI and how to edit them.
    /// 
    /// The concrete implementations can be found in the static DataSet helper class.
    /// </summary>
    internal class DataSet<T> : IDataSet
    {
        public delegate bool ComparatorDelegate(string name, T item);
        public delegate bool RetrieverDelegate(string name, out T item);

        /// <summary>
        /// (MANDATORY) Title of the dataset
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// (OPTIONAL) Subtitle of the dataset
        /// </summary>
        public string SubTitle { get; set; }

        /// <summary>
        /// (MANDATORY) Where to find the items
        /// </summary>
        public IEnumerable<T> List { private get; set; }

        /// <summary>
        /// (MANDATORY) How the items should be named in simple lists
        /// </summary>
        public Converter<T, string> ItemNameProvider { get; set; }

        /// <summary>
        /// (MANDATORY) How the items should be described in simple lists
        /// </summary>
        public Converter<T, string> ItemDescriptionProvider { get; set; }

        /// <summary>
        /// (MANDATORY) How to get icons for the list
        /// </summary>
        public Converter<T, UiControls.ImageListOrder> ItemIconProvider { get; set; }

        /// <summary>
        /// (MANDATORY) The core (only a convenience for data which needs a reference to the core to be meaningful).
        /// </summary>
        public Core Core { get; set; }

        /// <summary>
        /// (OPTIONAL) Called before the source list is changed via a call to ListModifier.
        /// </summary>
        public Converter<BeforeApplyArgs, bool> BeforeListChangesApplied { get; set; }

        /// <summary>
        /// (OPTIONAL) Called after the source list is changed via a call to ListModifier.
        /// </summary>
        public Action<AfterApplyArgs> AfterListChangesApplied { get; set; }

        /// <summary>
        /// Called to apply changes to the source list.
        /// NULL if the source list cannot be modified (e.g. temporary or fixed lists).
        /// </summary>
        public Action<ApplyArgs> ListChangeApplicator { get; set; }

        /// <summary>
        /// (OPTIONAL) How to edit items in the list
        /// NULL if items cannot be modified.
        /// </summary>
        public Converter<EditItemArgs, T> ItemEditor { get; set; }

        /// <summary>
        /// Gets or sets the Integerbehaviour (for conversion to/from text only).
        /// When set the user can enter integer ranges (e.g. 5-7) instead of individual items (5, 6, 7) in text entry.
        /// </summary>
        public bool IntegerBehaviour { get; set; }

        /// <summary>
        /// Used to denote an empty selection in single-selection mode.
        /// This is usually NULL.
        /// </summary>
        public T CancelValue { get; set; }

        /// <summary>
        /// Identifies if a string corresponds to an item for text-lookups.
        /// NOTE: This is only for cases where string->x is not the same as x->string.
        ///       x.ToString() and Namer(x) are always checked regardless.
        /// </summary>
        public ComparatorDelegate StringComparator { get; set; }

        /// <summary>
        /// Retrieves an item with the specified name.
        /// NOTE: This is only for cases where the item is not in the source list and therefore
        /// allows users to specify new entries.
        ///       x.ToString(), Namer(x) and StringComparator are always checked regardless.
        /// </summary>
        public RetrieverDelegate NewItemRetriever { get; set; }

        /// <summary>
        /// Returns if there is a NewItemRetriever.
        /// </summary>
        public bool AllowNewEntries => NewItemRetriever != null;

        /// <summary>
        /// When set signifies the list changes when items are edited.
        /// ListChangeApplicator is ignored and changes to the list cannot be cancelled.
        /// </summary>
        public bool ListChangesOnEdit { get; set; }

        /// <summary>
        /// When set signifies the list can be reordered.
        /// This makes no sense if set with ListChangesOnEdit since the user cannot control the list.
        /// </summary>
        public bool ListSupportsReorder { get; set; }

        /// <summary>
        /// When items are added, replaced or removed in the list this is called.
        /// If ListChangesOnEdit is true this can handle remove events.
        /// </summary>                                                           
        public Action<Form, T, T> BeforeItemChanged { get; set; }

        public class BeforeApplyArgs
        {
            /// <summary>
            /// Form to show dialogues on
            /// </summary>
            public readonly Form Owner;

            /// <summary>
            /// New list that will be applied
            /// </summary>
            public readonly IEnumerable<T> List;

            /// <summary>
            /// Arbitrary object the receiver can send to Apply and AfterApply.
            /// </summary>
            public object Status;

            public BeforeApplyArgs(Form owner, IEnumerable<T> list)
            {
                this.Owner = owner;
                this.List = list;
            }
        }

        public class ApplyArgs
        {
            /// <summary>
            /// New list to apply
            /// </summary>
            public readonly IEnumerable<T> List;

            /// <summary>
            /// Where to send progress reports to.
            /// </summary>
            public readonly ProgressReporter Progress;

            /// <summary>
            /// Arbitrary object from BeforeApply
            /// </summary>
            public readonly object Status;

            public ApplyArgs(IEnumerable<T> newList, ProgressReporter prog, object status)
            {
                this.List = newList;
                this.Progress = prog;
                this.Status = status;
            }
        }

        public class AfterApplyArgs
        {
            /// <summary>
            /// Form to show dialogues on
            /// </summary>
            public readonly Form owner;

            /// <summary>
            /// Status of the list
            /// </summary>
            public readonly IEnumerable<T> List;

            /// <summary>
            /// Arbitrary object from BeforeApply
            /// </summary>
            public readonly object Status;

            public AfterApplyArgs(Form owner, IEnumerable<T> list, object status)
            {
                this.owner = owner;
                this.List = list;
                this.Status = status;
            }
        }

        public class EditItemArgs
        {
            /// <summary>
            /// Form to show dialogues on
            /// </summary>
            public readonly Form Owner;

            /// <summary>
            /// Default value
            /// </summary>
            public readonly T DefaultValue;

            /// <summary>
            /// Whether to disallow editing
            /// </summary>
            public readonly bool ReadOnly;

            /// <summary>
            /// When set the caller intends to create a new item, when not set the caller intends
            /// to replace DefaultValue. Always false when DefaultValue is NULL.
            /// 
            /// Used to avoid filename conflicts and ensure a deep copy is taken, if the editor
            /// only uses a shallow copy.
            /// </summary>
            public readonly bool WorkOnCopy;

            public EditItemArgs(Form owner, T @default, bool readOnly, bool workOnCopy)
            {
                this.Owner = owner;
                this.DefaultValue = @default;
                this.ReadOnly = readOnly;
                this.WorkOnCopy = workOnCopy;
            }

            public DataSet<T2>.EditItemArgs Cast<T2>()
            {
                return new DataSet<T2>.EditItemArgs( Owner, (T2)(object)DefaultValue, ReadOnly, WorkOnCopy );
            }
        }

        /// <summary>
        /// Clone method.
        /// </summary>
        public DataSet<T> Clone()
        {
            return (DataSet<T>)this.MemberwiseClone();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public DataSet()
        {
            ItemNameProvider = z => z.ToString();
            ItemDescriptionProvider = z => null;
            CancelValue = typeof(T) == typeof(int) ? (T)(object)int.MinValue : default(T);
        }

        /// <summary>
        /// Wrapper to untyped object.
        /// </summary>
        IEnumerable IDataSet.UntypedGetList(bool onlyEnabled)
        {
            return TypedGetList(onlyEnabled);
        }

        /// <summary>
        /// Wrapper to untyped object.
        /// </summary>
        public IEnumerable<T> TypedGetList(bool onlyEnabled)
        {
            if (onlyEnabled)
            {
                return this.List.Where(EnabledFitler);
            }

            return this.List;
        }

        /// <summary>
        /// Helper: Only allows enabled items
        /// </summary>                       
        private bool EnabledFitler(T arg)
        {
            var x = arg as IVisualisable;

            return x == null || x.Enabled;
        }

        /// <summary>
        /// Wrapper to untyped Namer delegate.
        /// </summary>
        string IDataSet.UntypedName(object x)
        {
            return ItemNameProvider((T)x);
        }

        /// <summary>
        /// Wrapper to untyped Describer delegate.
        /// </summary>
        string IDataSet.UntypedDescription(object x)
        {
            return ItemDescriptionProvider((T)x);
        }

        /// <summary>
        /// Shows the big list (FrmBigList).
        /// </summary>         
        public IEnumerable<T> ShowListEditor(Form owner, FrmBigList.EShow show, object automaticAddTemplate)
        {
            IEnumerable result = FrmBigList.Show(owner, Core, this, show, automaticAddTemplate);

            if (result == null)
            {
                return null;
            }

            return result.Cast<T>();
        }

        /// <summary>
        /// Shows the big list (FrmBigList).
        /// The list is editable if a ListChangeAcceptor is set.
        /// </summary>        
        /// <returns>If the list was modified</returns>
        public bool ShowListEditor(Form owner)
        {
            return ShowListEditor(owner, FrmBigList.EShow.Default, null) != null;
        }

        /// <summary>
        /// Shows the list selection form (FrmList, single selection).
        /// </summary>
        public T ShowList(Form owner, T defaultSelection)
        {
            return FrmList.ShowList(owner, this, defaultSelection);
        }

        /// <summary>
        /// Shows the radio button selection form (FrmList, single selection).
        /// </summary>
        public T ShowRadio(Form owner, T defaultSelection)
        {
            return FrmList.ShowRadio(owner, this, defaultSelection);
        }

        /// <summary>
        /// Shows the button selection form (FrmList, single selection).
        /// </summary>
        public T ShowButtons(Form owner, T defaultSelection)
        {
            return FrmList.ShowButtons(owner, this, defaultSelection);
        }

        /// <summary>
        /// Shows the list selection form (FrmList, multiple selection).
        /// </summary>
        public IEnumerable<T> ShowCheckList(Form owner, IEnumerable<T> defaultSelection)
        {
            return FrmList.ShowCheckList(owner, this, defaultSelection);
        }

        /// <summary>
        /// Shows the checkbox selection form (FrmList, single selection).
        /// </summary>
        public IEnumerable<T> ShowCheckBox(Form owner, IEnumerable<T> defaultSelection)
        {
            return FrmList.ShowCheckBox(owner, this, defaultSelection);
        }

        /// <summary>
        /// Creates a ConditionBox (textbox and browse button) from the list.
        /// </summary>
        public ConditionBox<T> CreateConditionBox(CtlTextBox textBox, Button button)
        {
            return new ConditionBox<T>(this, textBox, button);
        }

        /// <summary>
        /// Creates a ComboBox (dropdown list and edit button) from the list.
        /// </summary>        
        internal EditableComboBox<T> CreateComboBox(ComboBox l, Button b, ENullItemName nullItemName)
        {
            return new EditableComboBox<T>(l, b, this, nullItemName);
        }

        /// <summary>
        /// Fluent interface for [SubTitle].
        /// </summary>
        internal DataSet<T> IncludeMessage(string subTitle)
        {
            SubTitle = subTitle;
            return this;
        }

        /// <summary>
        /// IMPLEMENTS IListValueSet.
        /// </summary>               
        object IDataSet.UntypedEdit(Form owner, object @default, bool readOnly, bool workOnCopy)
        {
            return this.ItemEditor(new EditItemArgs(owner, (T)@default, readOnly, workOnCopy));
        }

        /// <summary>
        /// IMPLEMENTS IListValueSet.
        /// </summary>               
        bool IDataSet.UntypedPrepareForApply(Form owner, IEnumerable list, out object status)
        {
            if (BeforeListChangesApplied == null)
            {
                status = null;
                return true;
            }

            var args = new BeforeApplyArgs(owner, list.Cast<T>());
            bool success = this.BeforeListChangesApplied(args);
            status = args.Status;
            return success;
        }

        /// <summary>
        /// IMPLEMENTS IListValueSet.
        /// </summary>               
        void IDataSet.UntypedAfterApply(Form owner, IEnumerable list, object status)
        {
            if (AfterListChangesApplied == null)
            {
                return;
            }

            this.AfterListChangesApplied(new AfterApplyArgs(owner, list.Cast<T>(), status));
        }

        /// <summary>
        /// IMPLEMENTS IListValueSet.
        /// </summary>               
        void IDataSet.UntypedApplyChanges(IEnumerable list, ProgressReporter prog, object status)
        {
            if (this.ListChangeApplicator == null)
            {
                return;
            }

            this.ListChangeApplicator(new ApplyArgs(list.Cast<T>(), prog, status));
        }

        /// <summary>
        /// IMPLEMENTS IListValueSet.
        /// </summary>    
        string IDataSet.Title
        {
            get
            {
                return this.Title;
            }
        }

        /// <summary>
        /// IMPLEMENTS IListValueSet.
        /// </summary>      
        string IDataSet.SubTitle
        {
            get
            {
                return this.SubTitle;
            }
        }

        /// <summary>
        /// IMPLEMENTS IListValueSet.
        /// </summary>    
        bool IDataSet.ListSupportsChanges
        {
            get
            {
                return this.ListChangeApplicator != null;
            }
        }

        /// <summary>
        /// IMPLEMENTS IListValueSet.
        /// </summary>    
        bool IDataSet.HasItemEditor
        {
            get
            {
                return this.ItemEditor != null;
            }
        }

        /// <summary>
        /// IMPLEMENTS IListValueSet.
        /// </summary>    
        void IDataSet.UntypedBeforeReplace(Form owner, object remove, object create)
        {
            if (BeforeItemChanged != null)
            {
                this.BeforeItemChanged(owner, (T)remove, (T)create);
            }
        }
    }
}