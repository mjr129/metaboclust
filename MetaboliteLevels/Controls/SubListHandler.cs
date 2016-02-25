using MetaboliteLevels.Data;
using MetaboliteLevels.Viewers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Viewers.Lists;
using MetaboliteLevels.Forms;

namespace MetaboliteLevels.Controls
{
    /// <summary>
    /// Manages the collection of sub-lists at the bottom left of the main window.
    /// </summary>
    class SubListHandler : ICoreWatcher
    {
        /// <summary>
        /// Mapping for an individual list.
        /// </summary>
        class Mapper
        {
            private readonly ListViewHelper _listView;  // listbox
            private readonly CaptionBar _captionBar;    // caption
            private readonly VisualClass _visualClass;  // what we show
            private readonly ToolStripItem _controls;   // where the button goes
            private readonly Image _okImage;            // populated image for button
            private readonly Image _notImage;           // not populated image for button

            public Mapper(ISelectionHolder selector, ListViewHelper to, VisualClass type, ToolStripItem controls)
            {
                this._listView = to;
                this._visualClass = type;
                this._listView.DivertList(ContentsRequest.Empty);
                this._captionBar = new CaptionBar(to.ListView.Parent, selector);
                this._controls = controls;
                _okImage = controls.Image;
                _notImage = UiControls.Crossout(_okImage);
                _controls.Image = _notImage;
                _controls.Enabled = false;
            }

            public void Activate(IVisualisable selection)
            {
                if (selection == null)
                {
                    this._captionBar.SetText("No selection: " + _visualClass.ToString());
                    this._listView.DivertList(ContentsRequest.Empty);
                    _controls.Image = _notImage;
                    _controls.Enabled = false;
                    return;
                }

                ContentsRequest hs = selection.GetContents(_listView._core, _visualClass);

                this._listView.DivertList(hs);

                if (hs != null && hs.Text != null)
                {
                    this._captionBar.SetText(hs.Text, selection);
                    _controls.Image = hs.Contents.Count == 0 ? _notImage : _okImage;
                    _controls.Enabled = true;
                }
                else
                {
                    this._captionBar.SetText("List of " + _visualClass.ToString() + "s not available for {0}.", selection);
                    _controls.Image = _notImage;
                    _controls.Enabled = false;
                }
            }
        }

        private Core _core;    // link to the core
        private readonly List<Mapper> _mappers = new List<Mapper>();    // collection of basic sublists and captions
        private readonly ListView _lstInfo; // special case sublist (info)
        private readonly ListView _lstStats; // special case sublist (statistics)
        private readonly CaptionBar _capInfo; // special case caption (info)
        private readonly CaptionBar _capStats; // special case caption (statistics)
        private readonly ISelectionHolder _selector;

        public SubListHandler(ISelectionHolder selector, Core core, ListView _lst2Info, ListView _lst2Stats)
        {
            this._core = core;
            this._lstInfo = _lst2Info;
            this._lstStats = _lst2Stats;
            this._capInfo = new CaptionBar(_lst2Info.Parent, selector);
            this._capStats = new CaptionBar(_lst2Stats.Parent, selector);
            this._selector = selector;
        }

        public void Add(ListViewHelper to, VisualClass type, ToolStripItem controls)
        {
            _mappers.Add(new Mapper(_selector, to, type, controls));
        }

        public void Activate(IVisualisable v)
        {
            foreach (Mapper mapper in _mappers)
            {
                mapper.Activate(v);
            }

            _capInfo.SetText("Information for {0}", v);
            _capStats.SetText("Statistics for {0}", v);

            FillList(_lstInfo, v, false);
            FillList(_lstStats, v, true);
        }

        private void FillList(ListView list, IVisualisable v, bool statistics)
        {
            list.Items.Clear();

            if (v == null)
            {
                return;
            }

            foreach (Column column in ColumnManager.GetColumns(_core, v))
            {
                if (statistics != (column.Special == EColumn.Statistic))
                {
                    continue;
                }

                ListViewItem lvi = new ListViewItem(column.DisplayName);
                string value = column.GetRowAsString(v);
                                                      
                lvi.SubItems.Add(value);

                if (statistics)
                {
                    lvi.ImageIndex = (int)UiControls.ImageListOrder.Statistic;
                }
                else
                {
                    lvi.ImageIndex = (int)(column.Special == EColumn.Meta ? UiControls.ImageListOrder.InfoU : UiControls.ImageListOrder.Info);
                    lvi.ForeColor = column.Special == EColumn.Meta ? Color.DarkBlue : Color.Black;
                }

                list.Items.Add(lvi);
            }
        }

        public void ChangeCore(Core newCore)
        {
            _core = newCore;
        }
    }
}
