using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Algorithms.Definitions.Base.Misc;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Gui.Controls;
using MetaboliteLevels.Gui.Controls.Lists;
using MetaboliteLevels.Gui.Forms.Editing;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Data.Database
{
    /// <summary>
    /// This provides weak wrappers around DataSet(of T).
    /// See DataSet(of T) for the real descriptions.
    /// </summary>
    internal interface IDataSet : IIconProvider, IExportProvider
    {
        string ListTitle { get; set; }

        [XColumn(EColumn.Visible)]
        string Title { get; }

        [XColumn]
        string SubTitle { get; }

        IEnumerable UntypedGetList(bool onlyEnabled);

        string UntypedName(object x);
        string UntypedDescription(object x);

        [XColumn("List editable")]
        bool ListSupportsChanges { get; }

        [XColumn( "Items editable" )]
        bool HasItemEditor { get; }

        object UntypedEdit(Form owner, object @default, bool readOnly, bool workOnCopy);
        void UntypedAfterApply(Form owner, IEnumerable list);
        void UntypedApplyChanges(IEnumerable newList, ProgressReporter prog, bool transient );
        BigListResult<object> ShowListEditor(Form owner);   
        bool ListIsSelfUpdating { get; }
        bool ListSupportsReorder { get; }

        void UntypedBeforeReplace(Form owner, object remove, object create);
        bool DynamicEntries { get; }

        bool ItemsReferenceList { get; }

        [XColumn( EColumn.Visible )]
        Type DataType { get; }

        EditableComboBox UntypedCreateComboBox( ComboBox l, Button b, EditableComboBox.EFlags nullItemName );
    }
}
