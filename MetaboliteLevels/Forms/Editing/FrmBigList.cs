using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Forms.Algorithms;
using MetaboliteLevels.Forms.Generic;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Settings;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Forms.Editing
{
    /// <summary>
    ///     Displays the list of statistics available for viewing.
    /// </summary>
    public partial class FrmBigList : Form
    {
        /// <summary>
        /// Basic list editor.
        /// </summary>
        private abstract class BigListConfig
        {
            public abstract ConfigInit Initialise();
            public virtual string GetMissingName(object target) { return target.ToString(); }
            public abstract object EditObject(Form owner, object target, bool read);
            public abstract void ApplyPendingChange(object target, PendingChange pendingChange);
            public abstract void ApplyChanges(IProgressReporter info, ArrayList alist);
            public virtual bool PrepareForApply(Form form, ArrayList arrayList, int numEnabled) { return true; }
            public abstract PendingChange GetUnchanged(object target);
            public virtual void AfterApply(Form form) { }
            public virtual bool AddAtInitialise() { return false; }
            public abstract void GetListViewItem(object target, out string col1, out string col2, out int img);
        }

        /// <summary>
        /// Strongly types BigListConfig.
        /// </summary>
        private abstract class BigListConfig<T> : BigListConfig
        {
            protected virtual string GetMissingName(T target) { return target.ToString(); }
            protected abstract T EditObject(Form owner, T target, bool read);
            protected abstract void ApplyPendingChange(T target, PendingChange pendingChange);
            protected abstract void ApplyChanges(IProgressReporter info, List<T> alist);
            protected virtual bool PrepareForApply(Form form, List<T> arrayList, int numEnabled) { return true; }
            protected abstract PendingChange GetUnchanged(T target);
            protected abstract void GetListViewItem(T target, out string col1, out string col2, out int img);

            public sealed override string GetMissingName(object target) { return GetMissingName((T)target); }
            public sealed override object EditObject(Form owner, object target, bool read) { return EditObject(owner, (T)target, read); }
            public sealed override void ApplyPendingChange(object target, PendingChange pendingChange) { ApplyPendingChange((T)target, pendingChange); }
            public sealed override bool PrepareForApply(Form form, ArrayList arrayList, int numEnabled) { return PrepareForApply(form, arrayList.Cast<T>().ToList(), numEnabled); }
            public sealed override PendingChange GetUnchanged(object target) { return GetUnchanged((T)target); }
            public sealed override void ApplyChanges(IProgressReporter info, ArrayList alist) { ApplyChanges(info, alist.Cast<T>().ToList()); }
            public sealed override void GetListViewItem(object target, out string col1, out string col2, out int img) { GetListViewItem((T)target, out col1, out col2, out img); }
        }

        /// <summary>
        /// For peak filters.
        /// </summary>
        private sealed class BigListConfigForPeakFilters : BigListConfig<Filter>
        {
            private readonly Core _core;
            private readonly bool _obs;

            public BigListConfigForPeakFilters(Core core, bool obs)
            {
                _obs = obs;
                _core = core;
            }

            public override ConfigInit Initialise()
            {
                return new ConfigInit
                (
                    Caption: "Filters",
                    Title: _obs ? "Observation Filters" : "Peak Filters",
                    SubTitle: "Add or remove filters",
                    List: _obs ? (ICollection)_core.ObsFilters : _core.PeakFilters,
                    SupportsRename: true,
                    SupportsDisable: true
                );
            }

            protected override void GetListViewItem(Filter target, out string col1, out string col2, out int img)
            {
                col1 = target.GetType().Name;
                col2 = target.ParamsAsString();
                img = UiControls.ImageListOrder.Filter;
            }

            protected override Filter EditObject(Form owner, Filter target, bool read)
            {
                if (_obs)
                {
                    return FrmBigList.ShowObsFilter(owner, _core, (ObsFilter)target, read);
                }
                else
                {
                    return FrmBigList.ShowPeakFilter(owner, _core, (PeakFilter)target, read);
                }
            }

            protected override void ApplyPendingChange(Filter target, PendingChange pendingChange)
            {
                target.Name = pendingChange.Name;
                target.Comments = pendingChange.Comments;
                target.Enabled = pendingChange.Enabled;
            }

            protected override void ApplyChanges(IProgressReporter info, List<Filter> alist)
            {
                if (_obs)
                {
                    _core.SetObsFilters(alist.Cast<ObsFilter>());
                }
                else
                {
                    _core.SetPeakFilters(alist.Cast<PeakFilter>());
                }
            }

            protected override PendingChange GetUnchanged(Filter target)
            {
                return new PendingChange(target.Name, target.Comments, target.Enabled);
            }
        }

        private sealed class BigListConfigForListValueSet<T> : BigListConfig<T>
        {
            private readonly ListValueSet<T> _values;
            public List<T> Result;

            public BigListConfigForListValueSet(ListValueSet<T> values)
            {
                _values = values;
            }

            public override ConfigInit Initialise()
            {
                return new ConfigInit
                (
                    Caption: _values.Title,
                    List: _values.List.ToList(),
                    SubTitle: _values.SubTitle,
                    SupportsRename: false,
                    Title: _values.Title,
                    SupportsDisable: false
                );
            }

            protected override void ApplyChanges(IProgressReporter info, List<T> alist)
            {
                Result = alist;
            }

            protected override void ApplyPendingChange(T target, PendingChange pendingChange)
            {
                // NA (SupportsRename is FALSE)
            }

            protected override T EditObject(Form owner, T target, bool read)
            {
                return _values.ItemEditor(owner, target, read);
            }

            protected override void GetListViewItem(T target, out string col1, out string col2, out int img)
            {
                col1 = _values.Namer(target);
                col2 = _values.Describer(target);
                img = _values.IconProvider(target);
            }

            protected override PendingChange GetUnchanged(T target)
            {
                return new PendingChange(_values.Namer(target), _values.Describer(target), true);
            }
        }

        /// <summary>
        /// For peak filters.
        /// </summary>
        private sealed class BigListConfigForFilter : BigListConfig
        {
            private readonly Filter _filter;
            private readonly Core _core;
            private readonly bool _obs;
            public Filter Result;

            public BigListConfigForFilter(Core core, Filter filter, bool obs)
            {
                Debug.Assert(core != null);
                Debug.Assert(filter != null);

                _core = core;
                _filter = filter;
                _obs = obs;
            }

            public override void GetListViewItem(object target, out string col1, out string col2, out int img)
            {
                col1 = target.GetType().Name;
                col2 = "-";
                img = UiControls.ImageListOrder.Filter;
            }

            public override object EditObject(Form owner, object target, bool read)
            {
                if (_obs)
                {
                    return FrmObservationFilterCondition.Show(owner, _core, (ObsFilter.Condition)target, read);
                }
                else
                {
                    return FrmPeakFilterCondition.Show(owner, _core, (PeakFilter.Condition)target, read);
                }
            }

            public override void ApplyPendingChange(object target, PendingChange pendingChange)
            {
                // Not supported
            }

            public override void ApplyChanges(IProgressReporter info, ArrayList alist)
            {
                if (_obs)
                {
                    Result = new ObsFilter(_filter.Name, _filter.Comments, alist.Cast<ObsFilter.Condition>());
                }
                else
                {
                    Result = new PeakFilter(_filter.Name, _filter.Comments, alist.Cast<PeakFilter.Condition>());
                }
            }

            public override PendingChange GetUnchanged(object target)
            {
                return new PendingChange(target.ToString(), string.Empty, true);
            }

            public override ConfigInit Initialise()
            {
                return new ConfigInit
                (
                    Caption: "Filter Conditions",
                    Title: "Edit Filter Conditions",
                    SubTitle: "Add or remove conditions",
                    List: _obs ? (ICollection)((ObsFilter)_filter).Conditions : ((PeakFilter)_filter).Conditions,
                    SupportsRename: false,
                    SupportsDisable: false
                );
            }
        }

        public enum EAlgorithmType
        {
            Trend,
            Clusters,
            Statistics,
            Corrections
        }

        private sealed class BigListConfigForAlgorithms : BigListConfig<ConfigurationBase>
        {
            private readonly Core _core;
            private readonly EAlgorithmType _mode;
            private IVisualisable _autoApply;
            private FrmEditUpdate.EChangeLevel _toUpdate;
            private bool _success;

            protected override void GetListViewItem(ConfigurationBase target, out string col1, out string col2, out int img)
            {
                col1 = target.Name;
                col2 = target.ArgsToString;

                if (target.HasError)
                {
                    img = UiControls.ImageListOrder.Warning;
                }
                else if (target.HasResults)
                {
                    img = UiControls.ImageListOrder.TestFull;
                }
                else
                {
                    img = UiControls.ImageListOrder.TestEmpty;
                }
            }

            public BigListConfigForAlgorithms(Core core, EAlgorithmType mode, IVisualisable autoApply)
            {
                _core = core;
                _mode = mode;
                _autoApply = autoApply;
            }

            public override bool AddAtInitialise()
            {
                return _autoApply != null;
            }

            public override void AfterApply(Form form)
            {
                if (!_success)
                {
                    FrmMsgBox.ShowError(form, "An error occured whilst processing your request. Please check your configurations to see what went wrong.");
                    return;
                }

                switch (_mode)
                {
                    case EAlgorithmType.Corrections:
                        FrmMsgBox.ShowCompleted(form, "Data Corrections", FrmEditUpdate.GetUpdateMessage(_toUpdate));
                        break;

                    case EAlgorithmType.Statistics:
                        FrmMsgBox.ShowCompleted(form, "Staticics", FrmEditUpdate.GetUpdateMessage(FrmEditUpdate.EChangeLevel.Statistic));
                        break;

                    case EAlgorithmType.Clusters:
                        FrmMsgBox.ShowCompleted(form, "Clustering", FrmEditUpdate.GetUpdateMessage(FrmEditUpdate.EChangeLevel.Cluster));
                        break;

                    case EAlgorithmType.Trend:
                        FrmMsgBox.ShowCompleted(form, "Trends", FrmEditUpdate.GetUpdateMessage(_toUpdate));
                        break;

                    default:
                        throw new SwitchException(_mode);
                }
            }

            protected override PendingChange GetUnchanged(ConfigurationBase z)
            {
                return new PendingChange(z.Name, z.Comments, z.Enabled);
            }

            protected override bool PrepareForApply(Form form, List<ConfigurationBase> list, int numEnabled)
            {
                switch (_mode)
                {
                    case EAlgorithmType.Corrections:
                        _toUpdate = FrmEditUpdate.ShowCorrectionsChanged(form);
                        break;

                    case EAlgorithmType.Statistics:
                        _toUpdate = FrmEditUpdate.EChangeLevel.Statistic;
                        break;

                    case EAlgorithmType.Clusters:
                        _toUpdate = FrmEditUpdate.EChangeLevel.Cluster;
                        break;

                    case EAlgorithmType.Trend:
                        {
                            if (numEnabled == 0)
                            {
                                FrmMsgBox.ShowError(form, "A trendline must be defined.");
                                return false;
                            }
                            else if (numEnabled > 1)
                            {
                                FrmMsgBox.ShowError(form, "Only one trend can be activated at once.");
                                return false;
                            }

                            _toUpdate = FrmEditUpdate.ShowTrendsChanged(form);
                        }
                        break;

                    default:
                        throw new SwitchException(_mode);
                }

                return true;
            }

            protected override void ApplyChanges(IProgressReporter info, List<ConfigurationBase> list)
            {
                switch (_mode)
                {
                    case EAlgorithmType.Corrections:
                        {
                            bool updateStats = _toUpdate.HasFlag(FrmEditUpdate.EChangeLevel.Statistic);
                            bool updateTrends = _toUpdate.HasFlag(FrmEditUpdate.EChangeLevel.Trend);
                            bool updateClusters = _toUpdate.HasFlag(FrmEditUpdate.EChangeLevel.Cluster);

                            _success = _core.SetCorrections(list.Cast<ConfigurationCorrection>().ToArray(), false, updateStats, updateTrends, updateClusters,
                                                               info);
                        }
                        break;

                    case EAlgorithmType.Statistics:
                        {
                            _success = _core.SetStatistics(list.Cast<ConfigurationStatistic>().ToArray(), false, info);
                        }
                        break;

                    case EAlgorithmType.Clusters:
                        {
                            _success = _core.SetClusterers(list.Cast<ConfigurationClusterer>().ToArray(), false, info);
                        }
                        break;

                    case EAlgorithmType.Trend:
                        {
                            bool updateStats = _toUpdate.HasFlag(FrmEditUpdate.EChangeLevel.Statistic);
                            bool updateClusters = _toUpdate.HasFlag(FrmEditUpdate.EChangeLevel.Cluster);
                            _success = _core.SetTrends(list.Cast<ConfigurationTrend>().ToArray(), false, updateStats, updateClusters, info);
                        }
                        break;

                    default:
                        throw new SwitchException(_mode);
                }
            }

            protected override void ApplyPendingChange(ConfigurationBase config, PendingChange ch)
            {
                config.Name = ch.Name;
                config.Enabled = ch.Enabled;
                config.Comments = ch.Comments;
            }

            public override ConfigInit Initialise()
            {
                switch (_mode)
                {
                    case EAlgorithmType.Statistics:
                        return new ConfigInit
                        (
                            Caption: "Statistics",
                            Title: "Edit Statistics",
                            SubTitle: "Add or remove statistics",
                            List: _core.StatisticsComplete,
                            SupportsRename: true,
                            SupportsDisable: true
                        );

                    case EAlgorithmType.Corrections:
                        return new ConfigInit
                        (
                            Caption: "Corrections",
                            Title: "Edit Data Corrections",
                            SubTitle: "Add, remove or reorder data correction methods",
                            List: _core.CorrectionsComplete,
                            SupportsRename: true,
                            SupportsDisable: true
                        );

                    case EAlgorithmType.Clusters:
                        return new ConfigInit
                        (
                            Caption: "Clustering",
                            Title: "Edit Clustering Methods",
                            SubTitle: "Add, remove or reorder data clustering methods",
                            List: _core.ClusterersComplete,
                            SupportsRename: true,
                            SupportsDisable: true
                        );

                    case EAlgorithmType.Trend:
                        return new ConfigInit
                        (
                            Caption: "Trends",
                            Title: "Edit Trend Generation",
                            SubTitle: "Select trend generation method",
                            List: _core.TrendsComplete,
                            SupportsRename: true,
                            SupportsDisable: true
                        );

                    default:
                        throw new SwitchException(_mode);
                }
            }

            protected override string GetMissingName(ConfigurationBase v)
            {
                return v.AlgoName + " " + v.ArgsToString;
            }

            protected override ConfigurationBase EditObject(Form owner, ConfigurationBase o, bool read)
            {
                if (o != null && o.HasError)
                {
                    FrmMsgBox.ButtonSet[] btns = {
                                                     new   FrmMsgBox.ButtonSet("Continue", Resources.MnuAccept, DialogResult.Yes),
                                                     new   FrmMsgBox.ButtonSet("Clear error", Resources.MnuDelete, DialogResult.No),
                                                     new   FrmMsgBox.ButtonSet("Cancel", Resources.MnuCancel, DialogResult.Cancel)};

                    switch (FrmMsgBox.Show(owner, "Error report", "Last time this configuration was run it reported an error", o.Error, Resources.MsgWarning, btns, 0, 2))
                    {
                        case DialogResult.Yes:
                            break;

                        case DialogResult.No:
                            o.ClearError();
                            break;

                        case DialogResult.Cancel:
                            return null;
                    }
                }

                if (o != null && o.HasResults)
                {
                    FrmMsgBox.ButtonSet[] btns = { new   FrmMsgBox.ButtonSet("Continue", Resources.MnuAccept, DialogResult.Yes),
                                                     new   FrmMsgBox.ButtonSet("Cancel", Resources.MnuCancel, DialogResult.Cancel)};

                    switch (FrmMsgBox.Show(owner, "Results report", "This configuration already has results associated with it", "Editing this configuration will result in the loss of the current results", Resources.MsgInfo, btns, 0, 2))
                    {
                        case DialogResult.Yes:
                            break;

                        case DialogResult.Cancel:
                            return null;
                    }
                }

                IVisualisable vis = _autoApply;
                _autoApply = null;

                switch (_mode)
                {
                    case EAlgorithmType.Statistics:
                        return FrmAlgoStatistic.Show(owner, (ConfigurationStatistic)o, _core, read, (Peak)vis);

                    case EAlgorithmType.Corrections:
                        return FrmAlgoCorrection.Show(owner, _core, (ConfigurationCorrection)o, read);

                    case EAlgorithmType.Clusters:
                        return FrmAlgoCluster.Show(owner, _core, (ConfigurationClusterer)o, read, (Cluster)vis, false);

                    case EAlgorithmType.Trend:
                        return FrmAlgoTrend.Show(owner, _core, (ConfigurationTrend)o, read);

                    default:
                        throw new SwitchException(_mode);
                }
            }
        }

        private sealed class ConfigInit
        {
            public readonly string Caption;
            public readonly ICollection List;
            public readonly string SubTitle;
            public readonly string Title;
            public readonly bool SupportsRename;
            public readonly bool SupportsDisable;

            public ConfigInit(string Caption, ICollection List, string Title, string SubTitle, bool SupportsRename, bool SupportsDisable)
            {
                this.Caption = Caption;
                this.List = List;
                this.Title = Title;
                this.SubTitle = SubTitle;
                this.SupportsRename = SupportsRename;
                this.SupportsDisable = SupportsDisable;
            }
        }

        private sealed class PendingChange
        {
            public bool Apply;
            public string Comments;
            public bool Enabled;
            public string Name;

            public PendingChange(string name, string comments, bool enabled)
            {
                this.Name = name;
                this.Comments = comments;
                this.Enabled = enabled;
            }
        }

        private readonly BigListConfig _config;
        private readonly ArrayList _list;
        private readonly Dictionary<object, PendingChange> _pendingChanges = new Dictionary<object, PendingChange>();
        private readonly Font _strikeFont;
        private bool _activated;

        private FrmBigList()
        {
            InitializeComponent();
            UiControls.SetIcon(this);
            _strikeFont = new Font(Font, FontStyle.Strikeout);
        }

        private FrmBigList(BigListConfig config, bool readOnly)
            : this()
        {
            _config = config;

            ConfigInit init = _config.Initialise();

            Text = init.Caption;
            ctlTitleBar1.Text = init.Title;
            ctlTitleBar1.SubText = init.SubTitle;
            _list = new ArrayList(init.List);

            UiControls.PopulateImageList(imageList1);

            UpdateList(null);

            UiControls.CompensateForVisualStyles(this);

            if (readOnly)
            {
                _btnEdit.Visible = false;
                _btnRename.Visible = false;
                _btnEnableDisable.Visible = false;
                _btnUp.Visible = false;
                _btnDown.Visible = false;
                _btnAdd.Visible = false;
                _btnDuplicate.Visible = false;
                _btnOk.Visible = false;
                _btnCancel.Text = "Close";
            }

            if (!init.SupportsRename)
            {
                _btnRename.Visible = false;
            }

            if (!init.SupportsDisable)
            {
                _btnEnableDisable.Visible = false;
            }
        }

        internal static bool ShowAlgorithms(Form owner, Core core, EAlgorithmType algoType, IVisualisable autoApply)
        {
            var config = new BigListConfigForAlgorithms(core, algoType, autoApply);
            return Show(owner, config, false);
        }

        internal static bool ShowPeakFilters(Form owner, Core core)
        {
            var config = new BigListConfigForPeakFilters(core, false);
            return Show(owner, config, false);
        }

        internal static bool ShowObsFilters(Form owner, Core core)
        {
            var config = new BigListConfigForPeakFilters(core, true);
            return Show(owner, config, false);
        }

        internal static List<T> ShowGeneric<T>(Form owner, ListValueSet<T> list, bool readOnly)
        {
            var config = new BigListConfigForListValueSet<T>(list);

            if (Show(owner, config, readOnly))
            {
                return (List<T>)config.Result;
            }

            return null;
        }

        internal static ObsFilter ShowObsFilter(Form owner, Core core, ObsFilter filter, bool readOnly)
        {
            if (filter == null)
            {
                filter = new ObsFilter(null, null, null);
            }

            var config = new BigListConfigForFilter(core, filter, true);

            if (Show(owner, config, readOnly))
            {
                return (ObsFilter)config.Result;
            }

            return null;
        }

        internal static PeakFilter ShowPeakFilter(Form owner, Core core, PeakFilter filter, bool readOnly)
        {
            if (filter == null)
            {
                filter = new PeakFilter(null, null, null);
            }

            var config = new BigListConfigForFilter(core, filter, false);

            if (Show(owner, config, readOnly))
            {
                return (PeakFilter)config.Result;
            }

            return null;
        }

        private static bool Show(Form owner, BigListConfig config, bool readOnly)
        {
            using (var frm = new FrmBigList(config, readOnly))
            {
                if (owner is FrmBigList)
                {
                    frm.Size = new Size(Math.Max(128, owner.Width - 32), Math.Max(128, owner.Height - 32));
                }

                return (UiControls.ShowWithDim(owner, frm) == DialogResult.OK);
            }
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            if (!_activated)
            {
                _activated = true;

                if (_config.AddAtInitialise())
                {
                    _btnAdd.PerformClick();
                }
            }
        }

        private void UpdateList(object selected)
        {
            this.listView1.Items.Clear();

            ListViewItem selItem = null;

            foreach (object config in _list)
            {
                ListViewItem lvi = CreateListViewItem(config);

                if (config == selected)
                {
                    selItem = lvi;
                }
            }

            if (selItem != null)
            {
                selItem.Selected = true;
                selItem.EnsureVisible();
            }
        }

        private ListViewItem CreateListViewItem(object v)
        {
            PendingChange change = Get(v);

            string name;

            if (string.IsNullOrEmpty(change.Name))
            {
                name = _config.GetMissingName(v);
            }
            else
            {
                name = change.Name;
            }

            var lvi = new ListViewItem(name);
            string col1;
            string col2;
            int img;
            _config.GetListViewItem(v, out col1, out col2, out img);
            lvi.SubItems.Add(col1);
            lvi.SubItems.Add(col2);
            lvi.Tag = v;
            lvi.ImageIndex = img;

            if (change.Enabled)
            {
                lvi.Font = Font;
                lvi.ForeColor = Color.Black;
            }
            else
            {
                lvi.Font = _strikeFont;
                lvi.ForeColor = Color.Gray;
            }

            listView1.Items.Add(lvi);
            return lvi;
        }

        private void _btnAdd_Click(object sender, EventArgs e)
        {
            object o = _config.EditObject(this, null, false);

            if (o != null)
            {
                Replace(null, o);
                UpdateList(o);
            }
        }

        private void Rename(object stat)
        {
            PendingChange change = Get(stat);

            string name = change.Name;
            string comment = change.Comments;

            if (FrmInput2.Show(this, stat.ToString(), "Rename", stat.ToString(), ref name, ref comment,
                               false))
            {
                change.Name = name;
                change.Comments = comment;
                change.Apply = true;

                UpdateList(stat);
            }
        }

        private PendingChange Get(object o)
        {
            return _pendingChanges.GetOrCreate(o, _config.GetUnchanged);
        }

        private void Replace(object remove, object create)
        {
            if (remove != null)
            {
                _list.Remove(remove);
            }

            if (create != null)
            {
                _list.Add(create);
            }
        }

        private void _btnView_Click(object sender, EventArgs e)
        {
            object o = GetSelected();

            if (o != null)
            {
                _config.EditObject(this, o, true);
            }
        }

        private object GetSelected()
        {
            if (listView1.SelectedItems.Count != 0)
            {
                return listView1.SelectedItems[0].Tag;
            }

            return null;
        }

        private void _btnEdit_Click(object sender, EventArgs e)
        {
            object p = GetSelected();

            if (p == null)
            {
                return;
            }

            object o = _config.EditObject(this, p, false);

            if (o == p)
            {
                // If they are the same object then only name/comments can have changed
            }
            else if (o != null)
            {
                Replace(p, o);
            }

            UpdateList(o);
        }

        private void _btnRemove_Click(object sender, EventArgs e)
        {
            object p = GetSelected();

            if (p == null)
            {
                return;
            }

            Replace(p, null);
            UpdateList(null);
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            _btnEdit.PerformClick();
        }

        private void _btnRename_Click(object sender, EventArgs e)
        {
            object stat = GetSelected();

            if (stat == null)
            {
                return;
            }

            Rename(stat);
        }

        private void _btnUp_Click(object sender, EventArgs e)
        {
            MoveItem(-1);
        }

        private void MoveItem(int direction)
        {
            object sel = GetSelected();

            if (sel == null)
            {
                return;
            }

            int newIndex = _list.IndexOf(sel) + direction;

            if (newIndex < 0)
            {
                newIndex = 0;
            }

            if (newIndex >= _list.Count)
            {
                newIndex = _list.Count - 1;
            }

            _list.Remove(sel);
            _list.Insert(newIndex, sel);

            UpdateList(sel);
        }

        private void _btnDown_Click(object sender, EventArgs e)
        {
            MoveItem(1);
        }

        private void _btnOk_Click(object sender, EventArgs e)
        {
            int numEnabled = _list.Cast<object>().Count(z => Get(z).Enabled);

            if (!_config.PrepareForApply(this, _list, numEnabled))
            {
                return;
            }

            // Pending changes (renames only)
            foreach (object i in _list)
            {
                PendingChange ch = Get(i);

                if (ch.Apply)
                {
                    _config.ApplyPendingChange(i, ch);
                }
            }

            FrmWait.Show(this, "Applying changes", null, _config.ApplyChanges, _list);

            _config.AfterApply(this);

            DialogResult = DialogResult.OK;
        }

        private void _btnEnableDisable_Click(object sender, EventArgs e)
        {
            object first = GetSelected();

            if (first != null)
            {
                PendingChange change = Get(first);

                bool newState = !change.Enabled;

                foreach (ListViewItem lvi in listView1.SelectedItems)
                {
                    object p = lvi.Tag;

                    change = Get(p);

                    change.Enabled = newState;
                    change.Apply = true;
                }

                UpdateList(first);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs ev)
        {
            object p = GetSelected();

            bool e = listView1.SelectedItems.Count == 1;
            bool f = listView1.SelectedItems.Count >= 1;

            _btnRemove.Enabled = e;
            _btnView.Enabled = e;
            _btnEdit.Enabled = e;
            _btnRename.Enabled = e;
            _btnUp.Enabled = e;
            _btnDown.Enabled = e;
            _btnDuplicate.Enabled = e;
            _btnEnableDisable.Enabled = f;

            if (f && Get(p).Enabled)
            {
                _btnEnableDisable.Text = "Disable";
                _btnEnableDisable.Image = Resources.MnuDisable;
            }
            else
            {
                _btnEnableDisable.Text = "Enable";
                _btnEnableDisable.Image = Resources.MnuEnable;
            }
        }

        private void _btnDuplicate_Click(object sender, EventArgs e)
        {
            object p = GetSelected();

            if (p == null)
            {
                return;
            }

            object o = _config.EditObject(this, p, false);

            if (o != null)
            {
                Replace(null, o);
                UpdateList(o);
            }
        }
    }
}