using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Forms.Editing;
using MetaboliteLevels.Forms.Generic;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Viewers.Lists
{
    partial class ListViewHelper<T> : ListViewHelper, ICoreWatcher
        where T : IVisualisable
    {
        private Func<Core, IEnumerable<T>> _sourceOption1;
        private ContentsRequest _sourceOption2;
        private IEnumerable<T> _sourceOption3;

        /// <summary>
        /// Constructor
        /// </summary>
        public ListViewHelper(ListView lv, Core core, Func<Core, IEnumerable<T>> listProvider, IPreviewProvider previewProvider)
            : base(lv, core, previewProvider)
        {
            _sourceOption1 = listProvider;
        }

        protected override IEnumerable<IVisualisable> GetSourceContent()
        {
            if (_sourceOption1 != null)
            {
                return (IEnumerable<IVisualisable>)_sourceOption1(_core);
            }
            else if (_sourceOption2 != null)
            {
                return (IEnumerable<IVisualisable>)_sourceOption2.Keys.Cast<T>();
            }
            else if (_sourceOption3 != null)
            {
                return (IEnumerable<IVisualisable>)_sourceOption3;
            }
            else
            {
                return new List<IVisualisable>();
            }
        }

        protected override void AddCustomColumns(List<Column> availableColumns)
        {
            if (_sourceOption2 != null && _sourceOption2.Owner != null)
            {
                string colFolder = "*" + _sourceOption2.Owner.VisualClass.ToUiString();

                for (int n = 0; n < _sourceOption2.ExtraColumns.Count; n++)
                {
                    var h = _sourceOption2.ExtraColumns[n];
                    int closure = n;
                    string description = string.Format(h.Description, _sourceOption2.Owner.SafeGetDisplayName());
                    var col = new Column<IVisualisable>(colFolder + "\\" + h.Name, EColumn.Visible, description, λ => GetDiversionColumnContent(λ, closure));
                    availableColumns.Add(col);
                }
            }
        }

        private object GetDiversionColumnContent(IVisualisable item, int col)
        {
            var item2 = _sourceOption2.Contents[item];

            if (item2 == null)
            {
                return "ERROR: _diversion.Contents[item] == null";
            }
            else if (col >= item2.Length)
            {
                return "ERROR: item >= _diversion.Contents.Length";
            }
            else
            {
                return item2[col];
            }
        }

        protected override IVisualisable GetOwner()
        {
            if (this._sourceOption2 != null)
            {
                return this._sourceOption2.Owner;
            }

            return null;
        }

        public void DivertList(Func<Core, IEnumerable<T>> x)
        {
            _sourceOption1 = x;
            _sourceOption2 = null;
            _sourceOption3 = null;
            Rebuild(EListInvalids.SourceChanged);
        }

        public override void DivertList(ContentsRequest x)
        {
            _sourceOption1 = null;
            _sourceOption2 = x;
            _sourceOption3 = null;
            Rebuild(EListInvalids.SourceChanged);
        }

        public void Clear()
        {
            _sourceOption1 = null;
            _sourceOption2 = null;
            _sourceOption3 = null;
            Rebuild(EListInvalids.SourceChanged);
        }

        public void DivertList(IEnumerable<T> x)
        {
            _sourceOption1 = null;
            _sourceOption2 = null;
            _sourceOption3 = x;
            Rebuild(EListInvalids.SourceChanged);
        }

        public new T Selection
        {
            get
            {
                return (T)base.Selection;
            }
            set
            {
                base.Selection = value;
            }
        }
    }

    class ListViewItemEventArgs : EventArgs
    {
        public readonly IVisualisable Selection;

        public ListViewItemEventArgs(IVisualisable selection)
        {
            this.Selection = selection;
        }
    }
}
