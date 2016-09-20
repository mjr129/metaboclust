using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Forms.Editing;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Forms.Generic
{
    /// <summary>
    /// This provides weak wrappers around DataSet(of T).
    /// See DataSet(of T) for the real descriptions.
    /// </summary>
    internal interface IDataSet
    {
        string Title { get; }
        string SubTitle { get; }
        IEnumerable UntypedGetList(bool onlyEnabled);

        string UntypedName(object x);
        string UntypedDescription(object x);

        bool ListSupportsChanges { get; }
        bool HasItemEditor { get; }

        object UntypedEdit(Form owner, object @default, bool readOnly, bool workOnCopy);
        void UntypedAfterApply(Form owner, IEnumerable list);
        void UntypedApplyChanges(IEnumerable newList, ProgressReporter prog, bool transient );
        bool ShowListEditor(Form owner);   
        bool ListIsSelfUpdating { get; }
        bool ListSupportsReorder { get; }

        void UntypedBeforeReplace(Form owner, object remove, object create);
        bool DynamicEntries { get; }

        bool ItemsReferenceList { get; }
    }
}
