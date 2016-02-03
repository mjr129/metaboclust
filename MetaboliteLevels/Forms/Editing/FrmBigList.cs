using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
using MetaboliteLevels.Forms.Algorithms.ClusterEvaluation;
using MetaboliteLevels.Forms.Generic;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Settings;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Viewers.Lists;

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
            public abstract IVisualisable EditObject(Form owner, IVisualisable target, bool read);
            public abstract void ApplyChanges(ProgressReporter info, List<IVisualisable> alist);
            public virtual bool PrepareForApply(Form form, List<IVisualisable> arrayList, int numEnabled) { return true; }
            public virtual void AfterApply(Form form) { }
            public virtual bool AddAtInitialise() { return false; }
            public virtual EReplaceMode BeforeReplace(Form owner, IVisualisable remove, IVisualisable create) { return EReplaceMode.Default; }
        }

        /// <summary>
        /// Strongly types BigListConfig.
        /// </summary>
        private abstract class BigListConfig<T> : BigListConfig
            where T : IVisualisable
        {
            protected virtual string GetMissingName(T target) { return target.ToString(); }
            protected abstract T EditObject(Form owner, T target, bool read);
            protected abstract void ApplyChanges(ProgressReporter info, List<T> alist);
            protected virtual bool PrepareForApply(Form form, List<T> arrayList, int numEnabled) { return true; }
            protected virtual EReplaceMode BeforeReplace(Form owner, T remove, T create) { return EReplaceMode.Default; }

            public sealed override string GetMissingName(object target) { return GetMissingName((T)target); }
            public sealed override IVisualisable EditObject(Form owner, IVisualisable target, bool read) { return EditObject(owner, (T)target, read); }
            public sealed override bool PrepareForApply(Form form, List<IVisualisable> arrayList, int numEnabled) { return PrepareForApply(form, arrayList.Cast<T>().ToList(), numEnabled); }
            public sealed override void ApplyChanges(ProgressReporter info, List<IVisualisable> alist) { ApplyChanges(info, alist.Cast<T>().ToList()); }
            public sealed override EReplaceMode BeforeReplace(Form owner, IVisualisable remove, IVisualisable create) { return BeforeReplace(owner, (T)remove, (T)create); }
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
                    List: _obs ? _core.AllObsFilters.Cast<IVisualisable>() : _core.AllPeakFilters.Cast<IVisualisable>()
                );
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

            protected override void ApplyChanges(ProgressReporter info, List<Filter> alist)
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
        }

        private sealed class BigListConfigForListValueSet<T> : BigListConfig<T>
            where T : IVisualisable
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
                    List: _values.List.Cast<IVisualisable>().ToList(),
                    SubTitle: _values.SubTitle,
                    Title: _values.Title
                );
            }

            protected override void ApplyChanges(ProgressReporter info, List<T> alist)
            {
                Result = alist;
            }

            protected override T EditObject(Form owner, T target, bool read)
            {
                return _values.ItemEditor(owner, target, read);
            }
        }

        private sealed class BigListConfigForTests : BigListConfig<ClusterEvaluationPointer>
        {
            private Core core;

            public BigListConfigForTests(Core core)
            {
                this.core = core;
            }

            public override ConfigInit Initialise()
            {
                return new ConfigInit("Tests",
                    core.EvaluationResultFiles,
                    "Tests",
                    "Create or modify tests");
            }

            protected override void ApplyChanges(ProgressReporter info, List<ClusterEvaluationPointer> alist)
            {
                core.EvaluationResultFiles.ReplaceAll(alist);
            }

            protected override ClusterEvaluationPointer EditObject(Form owner, ClusterEvaluationPointer target, bool read)
            {
                return FrmEvaluateClusteringOptions.Show(owner, core, target, read);
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

            public override IVisualisable EditObject(Form owner, IVisualisable target, bool readOnly)
            {
                if (_obs)
                {
                    return FrmObservationFilterCondition.Show(owner, _core, (ObsFilter.Condition)target, readOnly);
                }
                else
                {
                    return FrmPeakFilterCondition.Show(owner, _core, (PeakFilter.Condition)target, readOnly);
                }
            }

            public override void ApplyChanges(ProgressReporter info, List<IVisualisable> alist)
            {
                if (_obs)
                {
                    Result = new ObsFilter(_filter.OverrideDisplayName, _filter.Comment, alist.Cast<ObsFilter.Condition>());
                }
                else
                {
                    Result = new PeakFilter(_filter.OverrideDisplayName, _filter.Comment, alist.Cast<PeakFilter.Condition>());
                }
            }

            public override ConfigInit Initialise()
            {
                return new ConfigInit
                (
                    Caption: "Filter Conditions",
                    Title: "Filter Conditions",
                    SubTitle: null,
                    List: _obs
                            ? (IEnumerable<IVisualisable>)((ObsFilter)_filter).Conditions
                            : (IEnumerable<IVisualisable>)((PeakFilter)_filter).Conditions
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

        private enum EReplaceMode
        {
            Replace,
            CreateNew,
            Cancel,
            Default = Replace,
        }

        private sealed class BigListConfigForAlgorithms : BigListConfig<ConfigurationBase>
        {
            private readonly Core _core;
            private readonly EAlgorithmType _mode;
            private IVisualisable _autoApply;
            private FrmEditUpdate.EChangeLevel _toUpdate;
            private bool _success;

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

            protected override EReplaceMode BeforeReplace(Form owner, ConfigurationBase remove, ConfigurationBase create)
            {
                if (remove != null && remove.HasResults)
                {
                    if (create != null)
                    {
                        string text1 = "The configuration to be modified has results associated with it";
                        string text2 = "Changing this configuration will result in the loss of the associated results. If needed the original configuration can be retained (the new configuration will still be created).";

                        FrmMsgBox.ButtonSet[] btns = {  new FrmMsgBox.ButtonSet( "Replace", Resources.MnuAccept, DialogResult.No),
                                                        new FrmMsgBox.ButtonSet( "Retain", Resources.MnuCopy, DialogResult.Yes),
                                                        new FrmMsgBox.ButtonSet( "Cancel", Resources.MnuCancel, DialogResult.Cancel)};

                        switch (FrmMsgBox.Show(owner, owner.Text, text1, text2, Resources.MsgHelp, btns, "FrmBigList.EditConfig", DialogResult.No))
                        {
                            case DialogResult.No:
                                return EReplaceMode.Replace;

                            case DialogResult.Yes:
                                return EReplaceMode.CreateNew;

                            case DialogResult.Cancel:
                                return EReplaceMode.Cancel;

                            default:
                                throw new SwitchException();
                        }
                    }
                }

                return base.BeforeReplace(owner, remove, create);
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

            protected override bool PrepareForApply(Form form, List<ConfigurationBase> list, int numEnabled) // TODO: Remove "numEnabled", it doesn't work anymore
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
                            int numEnabledX = list.Count(z => z.Enabled);

                            if (numEnabledX == 0)
                            {
                                FrmMsgBox.ShowError(form, "A trendline must be defined.");
                                return false;
                            }
                            else if (numEnabledX > 1)
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

            protected override void ApplyChanges(ProgressReporter info, List<ConfigurationBase> list)
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
                            List: _core.AllStatistics
                        );

                    case EAlgorithmType.Corrections:
                        return new ConfigInit
                        (
                            Caption: "Corrections",
                            Title: "Edit Data Corrections",
                            SubTitle: "Add, remove or reorder data correction methods",
                            List: _core.AllCorrections
                        );

                    case EAlgorithmType.Clusters:
                        return new ConfigInit
                        (
                            Caption: "Clustering",
                            Title: "Edit Clustering Methods",
                            SubTitle: "Add, remove or reorder data clustering methods",
                            List: _core.AllClusterers
                        );

                    case EAlgorithmType.Trend:
                        return new ConfigInit
                        (
                            Caption: "Trends",
                            Title: "Edit Trend Generation",
                            SubTitle: "Select trend generation method",
                            List: _core.AllTrends
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

                    switch (FrmMsgBox.Show(owner, "Error report", "Last time this configuration was run it reported an error", o.Error, Resources.MsgWarning, btns))
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
            public readonly IEnumerable<IVisualisable> List;
            public readonly string Caption;
            public readonly string SubTitle;
            public readonly string Title;

            public ConfigInit(string Caption, IEnumerable<IVisualisable> List, string Title, string SubTitle)
            {
                this.Caption = Caption;
                this.List = List;
                this.Title = Title;
                this.SubTitle = SubTitle;
            }
        }

        private sealed class OriginalStatus
        {
            public bool Required;
            public string OriginalComment;
            public bool OriginalEnabled;
            public string OriginalOverrideDisplayName;

            public OriginalStatus(string name, string comments, bool enabled)
            {
                this.OriginalOverrideDisplayName = name;
                this.OriginalComment = comments;
                this.OriginalEnabled = enabled;
            }
        }

        private readonly BigListConfig _config;
        private readonly List<IVisualisable> _list;
        private readonly Dictionary<IVisualisable, OriginalStatus> _originalStatuses = new Dictionary<IVisualisable, OriginalStatus>();
        private bool _activated;
        private readonly ListViewHelper<IVisualisable> _listViewHelper;
        private bool _keepChanges;

        private FrmBigList()
        {
            InitializeComponent();
            UiControls.SetIcon(this);
            UiControls.PopulateImageList(imageList1);
        }

        private FrmBigList(Core core, BigListConfig config, bool readOnly)
            : this()
        {
            _listViewHelper = new ListViewHelper<IVisualisable>(listView1, core, null, null);
            listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;
            _listViewHelper.Activate += listView1_ItemActivate;

            _config = config;

            ConfigInit init = _config.Initialise();

            Text = init.Caption;
            ctlTitleBar1.Text = init.Title;
            ctlTitleBar1.SubText = init.SubTitle;
            _list = new List<IVisualisable>(init.List);
            _listViewHelper.DivertList(_list);

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
                _btnRemove.Visible = false;
                _btnCancel.Text = "Close";
            }

            UiControls.CompensateForVisualStyles(this);
        }

        internal static bool ShowAlgorithms(Form owner, Core core, EAlgorithmType algoType, IVisualisable autoApply)
        {
            var config = new BigListConfigForAlgorithms(core, algoType, autoApply);
            return Show(owner, core, config, false);
        }

        internal static bool ShowPeakFilters(Form owner, Core core)
        {
            var config = new BigListConfigForPeakFilters(core, false);
            return Show(owner, core, config, false);
        }

        internal static bool ShowObsFilters(Form owner, Core core)
        {
            var config = new BigListConfigForPeakFilters(core, true);
            return Show(owner, core, config, false);
        }

        internal static bool ShowTests(Form owner, Core core)
        {
            var config = new BigListConfigForTests(core);
            return Show(owner, core, config, false);
        }

        internal static List<T> ShowGeneric<T>(Form owner, Core core, ListValueSet<T> list, bool readOnly)
            where T : IVisualisable
        {
            var config = new BigListConfigForListValueSet<T>(list);

            if (Show(owner, core, config, readOnly))
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

            if (Show(owner, core, config, readOnly))
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

            if (Show(owner, core, config, readOnly))
            {
                return (PeakFilter)config.Result;
            }

            return null;
        }

        private static bool Show(Form owner, Core core, BigListConfig config, bool readOnly)
        {
            using (var frm = new FrmBigList(core, config, readOnly))
            {
                if (owner is FrmBigList)
                {
                    frm.Size = new Size(Math.Max(128, owner.Width - 32), Math.Max(128, owner.Height - 32));
                }

                return (UiControls.ShowWithDim(owner, frm) == DialogResult.OK);
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            if (!_keepChanges)
            {
                foreach (var kvp in this._originalStatuses)
                {
                    if (kvp.Value.Required)
                    {
                        kvp.Key.OverrideDisplayName = kvp.Value.OriginalOverrideDisplayName;
                        kvp.Key.Comment = kvp.Value.OriginalComment;
                        kvp.Key.Enabled = kvp.Value.OriginalEnabled;
                    }
                }
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

        private void _btnAdd_Click(object sender, EventArgs e)
        {
            IVisualisable o = _config.EditObject(this, null, false);

            if (o != null)
            {
                Replace(null, o);              
            }
        }

        private void Rename(IVisualisable stat)
        {
            OriginalStatus origStatus = Get(stat);

            string name = stat.OverrideDisplayName;
            string comment = stat.Comment;

            if (FrmInput2.Show(this, stat.DefaultDisplayName, "Rename", stat.ToString(), ref name, ref comment, false))
            {
                stat.OverrideDisplayName = name;
                stat.Comment = comment;

                origStatus.Required = true;

                _listViewHelper.Rebuild(EListInvalids.ValuesChanged);
            }
        }

        private OriginalStatus Get(IVisualisable o)
        {
            return _originalStatuses.GetOrCreate<IVisualisable, OriginalStatus>(o, GetUnchanged);
        }

        private OriginalStatus GetUnchanged(IVisualisable input)
        {
            return new OriginalStatus(input.OverrideDisplayName, input.Comment, input.Enabled);
        }

        private void Replace(IVisualisable remove, IVisualisable create)
        {
            if (remove != null)
            {
                _list.Remove(remove);
            }

            if (create != null)
            {
                _list.Add(create);
            }

            _listViewHelper.Rebuild(EListInvalids.ContentsChanged);

            if (create != null)
            {
                _listViewHelper.Selection = create;
            }
        }

        private void _btnView_Click(object sender, EventArgs e)
        {
            IVisualisable o = GetSelected();

            if (o != null)
            {
                _config.EditObject(this, o, true);
            }
        }

        private IVisualisable GetSelected()
        {
            return _listViewHelper.Selection;
        }

        private void _btnEdit_Click(object sender, EventArgs e)
        {
            IVisualisable original = GetSelected();
            IVisualisable toEdit = original;

            if (original == null)
            {
                return;
            }

            while (true)
            {
                IVisualisable modified = _config.EditObject(this, toEdit, false);

                if (modified == original)
                {
                    // If they are the same object then only name/comments can have changed
                    _listViewHelper.Rebuild(EListInvalids.ValuesChanged);
                    _listViewHelper.Selection = modified;
                    return;
                }
                else if (modified != null)
                {
                    EReplaceMode replaceMode = _config.BeforeReplace(this, original, modified);

                    switch (replaceMode)
                    {
                        case EReplaceMode.CreateNew:
                            Replace(null, modified);
                            return;

                        case EReplaceMode.Replace:
                            Replace(original, modified);
                            return;

                        case EReplaceMode.Cancel:
                            toEdit = modified;
                            break;

                        default:
                            throw new SwitchException(replaceMode);
                    }
                }
                else
                {
                    return;
                }
            }
        }

        private void _btnRemove_Click(object sender, EventArgs e)
        {
            IVisualisable p = GetSelected();

            if (p == null)
            {
                return;
            }

            Replace(p, null);                 
        }

        private void listView1_ItemActivate(object sender, ListViewItemEventArgs e)
        {
            _btnEdit.PerformClick();
        }

        private void _btnRename_Click(object sender, EventArgs e)
        {
            IVisualisable stat = GetSelected();

            if (stat == null)
            {
                return;
            }

            Rename(stat);

            _listViewHelper.Rebuild(EListInvalids.ValuesChanged);
            _listViewHelper.Selection = stat;
        }

        private void _btnUp_Click(object sender, EventArgs e)
        {
            MoveItem(-1);
        }

        private void MoveItem(int direction)
        {
            IVisualisable sel = GetSelected();

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

            _listViewHelper.Rebuild(EListInvalids.ContentsChanged);
        }

        private void _btnDown_Click(object sender, EventArgs e)
        {
            MoveItem(1);
        }

        private void _btnOk_Click(object sender, EventArgs e)
        {
            _keepChanges = true;
            int numEnabled = _list.Cast<IVisualisable>().Count(z => Get(z).OriginalEnabled);

            if (!_config.PrepareForApply(this, _list, numEnabled))
            {
                return;
            }

            FrmWait.Show(this, "Applying changes", null, _config.ApplyChanges, _list);

            _config.AfterApply(this);

            DialogResult = DialogResult.OK;
        }

        private void _btnEnableDisable_Click(object sender, EventArgs e)
        {
            IVisualisable first = GetSelected();

            if (first == null)
            {
                return;
            }

            OriginalStatus change = Get(first);

            first.Enabled = !first.Enabled;
            change.Required = true;

            _listViewHelper.Rebuild(EListInvalids.ValuesChanged);
            _listViewHelper.Selection = first;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs ev)
        {
            IVisualisable p = GetSelected();

            bool e = p != null;
            bool f = e;

            _btnRemove.Enabled = e;
            _btnView.Enabled = e;
            _btnEdit.Enabled = e;
            _btnRename.Enabled = e;
            _btnUp.Enabled = e;
            _btnDown.Enabled = e;
            _btnDuplicate.Enabled = e;
            _btnEnableDisable.Enabled = f;

            if (f && Get(p).OriginalEnabled)
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
            IVisualisable p = GetSelected();

            if (p == null)
            {
                return;
            }

            IVisualisable o = _config.EditObject(this, p, false);

            if (o != null)
            {
                Replace(null, o);
            }
        }
    }
}