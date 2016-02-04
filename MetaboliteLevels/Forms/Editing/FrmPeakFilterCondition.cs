using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Controls;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Forms.Generic;
using MetaboliteLevels.Settings;
using MetaboliteLevels.Utilities;
using System.Collections.Generic;

namespace MetaboliteLevels.Forms.Editing
{
    /// <summary>
    /// SigFilter editor form.
    /// </summary>
    public partial class FrmPeakFilterCondition : Form
    {
        Core _core;
        private ConditionBox<Peak> _cbPeaks;
        private ConditionBox<PeakFlag> _cbFlags;
        private ConditionBox<Cluster> _cbClusters;
        private EnumComboBox<PeakFilter.ESetOperator> _lsoFlags;
        private EnumComboBox<PeakFilter.ESetOperator> _lsoPats;
        private EnumComboBox<PeakFilter.EElementOperator> _lsoPeaks;
        private EnumComboBox<PeakFilter.EStatOperator> _lsoStats;
        private bool _isInitialised;
        private EnumComboBox<Filter.EElementOperator> _lsoFilter;
        private EditableComboBox<PeakFilter> _ecbFilter;
        private readonly bool _readOnly;

        /// <summary>
        /// Shows the form.
        /// </summary>
        /// <param name="autoSave">Controls whether to apply to core.DefaultStatistics</param>
        internal static PeakFilter.Condition Show(Form owner, Core core, PeakFilter.Condition defaults, bool readOnly)
        {
            using (FrmPeakFilterCondition frm = new FrmPeakFilterCondition(owner, core, defaults, readOnly))
            {
                if (UiControls.ShowWithDim(owner, frm) == DialogResult.OK)
                {
                    string err;
                    var r = frm.GetSelection(out err);
                    UiControls.Assert(r != null, err);

                    return r;
                }

                return null;
            }
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        public FrmPeakFilterCondition()
        {
            InitializeComponent();

            _lblPreviewTitle.BackColor = UiControls.PreviewBackColour;
            _lblPreviewTitle.ForeColor = UiControls.PreviewForeColour;

            DoubleBuffered = true;
            UiControls.SetIcon(this);
            UiControls.CompensateForVisualStyles(this);
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        private FrmPeakFilterCondition(Form owner, Core core, PeakFilter.Condition defaults, bool readOnly)
            : this()
        {
            this._core = core;
            this._readOnly = readOnly;

            ctlTitleBar1.Text = readOnly ? "View Condition" : "Edit Condition";

            // Setup boxes
            _cbPeaks = ListValueSet.ForPeaks(core).CreateConditionBox(_txtIsInSet, _btnIsInSet);
            _cbFlags = ListValueSet.ForPeakFlags(core).CreateConditionBox(_txtIsFlaggedWith, _btnIsFlaggedWith);
            _cbClusters = ListValueSet.ForClusters(core).CreateConditionBox(_txtIsInCluster, _btnIsInCluster);

            _lsoFlags = EnumComboBox.Create(this._lstFlagComparator, Filter.ESetOperator.AnyXinY);
            _lsoPats = EnumComboBox.Create(this._lstClusterComparator, Filter.ESetOperator.AnyXinY);
            _lsoPeaks = EnumComboBox.Create(this._lstPeakComparator, Filter.EElementOperator.Is);
            _lsoFilter = EnumComboBox.Create(this._lstFilterOp, Filter.EElementOperator.Is);
            _lsoStats = EnumComboBox.Create(this._lstStatisticComparator, Filter.EStatOperator.LessThan);
            _lstIsStatistic.Items.AddRange(core.ActiveStatistics.ToArray());

            _ecbFilter = EditableComboBox.ForPeakFilter(_lstFilter, null, core);

            _isInitialised = true;

            if (defaults == null)
            {
                checkBox1.Checked = false;
                _radAnd.Checked = true;
                _txtComp_TextChanged(null, null);
            }
            else
            {
                // Not
                checkBox1.Checked = defaults.Negate;
                _radAnd.Checked = defaults.CombiningOperator == Filter.ELogicOperator.And;
                _radOr.Checked = defaults.CombiningOperator == Filter.ELogicOperator.Or;

                if (defaults is PeakFilter.ConditionCluster)
                {
                    PeakFilter.ConditionCluster def = (PeakFilter.ConditionCluster)defaults;

                    List<Cluster> strong;

                    if (!def.Clusters.TryGetStrong(out strong))
                    {
                        ShowWeakFailureMessage("clusters");
                    }

                    _chkIsInCluster.Checked = true;
                    _lsoPats.SelectedItem = def.ClustersOp;
                    _cbClusters.SelectedItems = strong;
                }
                else if (defaults is PeakFilter.ConditionPeak)
                {
                    PeakFilter.ConditionPeak def = (PeakFilter.ConditionPeak)defaults;

                    List<Peak> strong;

                    if (!def.Peaks.TryGetStrong(out strong))
                    {
                        ShowWeakFailureMessage("peaks");
                    }

                    _chkIsInSet.Checked = true;
                    _lsoPeaks.SelectedItem = def.PeaksOp;
                    _cbPeaks.SelectedItems = strong;
                }
                else if (defaults is PeakFilter.ConditionFlags)
                {
                    PeakFilter.ConditionFlags def = (PeakFilter.ConditionFlags)defaults;

                    List<PeakFlag> strong;

                    if (!def.Flags.TryGetStrong(out strong))
                    {
                        ShowWeakFailureMessage("peaks");
                    }

                    _chkIsFlaggedWith.Checked = true;
                    _lsoFlags.SelectedItem = def.FlagsOp;
                    _cbFlags.SelectedItems = strong;
                }
                else if (defaults is PeakFilter.ConditionStatistic)
                {
                    PeakFilter.ConditionStatistic def = (PeakFilter.ConditionStatistic)defaults;

                    ConfigurationStatistic strong;

                    if (!def.Statistic.TryGetTarget(out strong))
                    {
                        ShowWeakFailureMessage("statistics");
                    }

                    ConfigurationStatistic stat = def.Statistic.GetTarget();

                    if (stat == null)
                    {
                        FrmMsgBox.ShowError(this, "The statistic specified when this condition was created has since been removed. Please select a different statistic.");
                    }

                    _chkIsStatistic.Checked = true;
                    _lsoStats.SelectedItem = def.StatisticOp;
                    _lstIsStatistic.SelectedItem = stat;
                    _txtStatisticValue.Text = def.StatisticValue.ToString();
                }
                else if (defaults is PeakFilter.ConditionFilter)
                {
                    PeakFilter.ConditionFilter def = (PeakFilter.ConditionFilter)defaults;

                    _radFilter.Checked = true;
                    _lsoFilter.SelectedItem = def.FilterOp ? Filter.EElementOperator.Is : Filter.EElementOperator.IsNot;
                    _ecbFilter.SelectedItem = def.Filter;
                }
                else
                {
                    throw new SwitchException(defaults.GetType());
                }
            }

            if (readOnly)
            {
                UiControls.MakeReadOnly(this);
            }
        }

        private void ShowWeakFailureMessage(string p)
        {
            FrmMsgBox.ShowWarning(this, "Missing information",
                                  "One of more of the " + p + " used in this condition have since been removed and can no longer be accessed.");
        }

        private void _btnOk_Click(object sender, EventArgs e)
        {
            // rm
        }

        private PeakFilter.Condition GetSelection(out string error)
        {
            if (!_radAnd.Checked && !_radOr.Checked)
            {
                error = "Select AND or OR";
                return null;
            }

            PeakFilter.ELogicOperator op = _radAnd.Checked ? PeakFilter.ELogicOperator.And : PeakFilter.ELogicOperator.Or;

            bool negate = checkBox1.Checked;

            if (this._chkIsInCluster.Checked)
            {
                var sel = _cbClusters.GetSelection();

                if (sel == null)
                {
                    error = "Select one or more clusters";
                    return null;
                }

                var en = _lsoPats.SelectedItem;

                if (!en.HasValue)
                {
                    error = "Select the cluster set operator";
                    return null;
                }

                error = null;
                return new PeakFilter.ConditionCluster(op, negate, en.Value, sel);
            }
            else if (this._chkIsInSet.Checked)
            {
                var sel = _cbPeaks.GetSelection();

                if (sel == null)
                {
                    error = "Select one or more PEAKs";
                    return null;
                }

                var en = _lsoPeaks.SelectedItem;

                if (!en.HasValue)
                {
                    error = "Select the PEAK element operator";
                    return null;
                }

                error = null;
                return new PeakFilter.ConditionPeak(op, negate, sel, en.Value);
            }
            else if (this._chkIsFlaggedWith.Checked)
            {
                var sel = _cbFlags.GetSelection();

                if (sel == null)
                {
                    error = "Select one or more FLAGs";
                    return null;
                }

                var en = _lsoFlags.SelectedItem;

                if (!en.HasValue)
                {
                    error = "Select the FLAG set operator";
                    return null;
                }

                error = null;
                return new PeakFilter.ConditionFlags(op, negate, en.Value, sel);
            }
            else if (this._chkIsStatistic.Checked)
            {
                var sel = (ConfigurationStatistic)_lstIsStatistic.SelectedItem;

                if (sel == null)
                {
                    error = "Select the STATISTIC";
                    return null;
                }

                var en = _lsoStats.SelectedItem;

                if (!en.HasValue)
                {
                    error = "Select the STATISTIC operator";
                    return null;
                }

                double va;

                if (!double.TryParse(_txtStatisticValue.Text, out va))
                {
                    error = "Enter a valid value to compare against";
                    return null;
                }

                error = null;
                return new PeakFilter.ConditionStatistic(op, negate, en.Value, sel, va);
            }
            else if (_radFilter.Checked)
            {
                if (!_ecbFilter.HasSelection)
                {
                    error = "Select the FILTER";
                    return null;
                }

                PeakFilter sel =  _ecbFilter.SelectedItem;

                var en = _lsoFilter.SelectedItem;

                if (!en.HasValue)
                {
                    error = "Select the FILTER operator";
                    return null;
                }

                error = null;
                return new PeakFilter.ConditionFilter(op, negate, sel, en.Value == Filter.EElementOperator.Is);
            }
            else
            {
                error = "Select a comparison";
                return null;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //
        }

        private void _txtComp_TextChanged(object sender, EventArgs e)
        {
            if (!_isInitialised)
            {
                return;
            }

            //this.SuspendDrawing();

            string err;
            var sel = GetSelection(out err);
            label2.Text = err;
            label2.Visible = !string.IsNullOrEmpty(err);
            _btnOk.Enabled = sel != null;

            _lsoPeaks.Visible = _chkIsInSet.Checked;
            _cbPeaks.Visible = _chkIsInSet.Checked;

            _lsoFlags.Visible = _chkIsFlaggedWith.Checked;
            _cbFlags.Visible = _chkIsFlaggedWith.Checked;

            _lsoPats.Visible = _chkIsInCluster.Checked;
            _cbClusters.Visible = _chkIsInCluster.Checked;

            _lsoFilter.Visible = _radFilter.Checked;
            _lstFilter.Visible = _radFilter.Checked;

            _lstIsStatistic.Visible = _chkIsStatistic.Checked;
            _lsoStats.Visible = _chkIsStatistic.Checked;
            _txtStatisticValue.Visible = _chkIsStatistic.Checked;

            UpdatePreview(sel);

            //this.ResumeDrawing();
        }

        private void UpdatePreview(PeakFilter.Condition r)
        {
            if (r != null)
            {
                int passed = _core.Peaks.Count(r.Preview);
                int failed = _core.Peaks.Count - passed;
                _lblSigPeaks.Text = "True: " + passed;
                _lblInsigPeaks.Text = "False: " + failed;
                _lblSigPeaks.ForeColor = (failed < passed) ? Color.Blue : ForeColor;
                _lblInsigPeaks.ForeColor = (failed > passed) ? Color.Blue : ForeColor;
                _lblSigPeaks.Visible = true;
                _lblInsigPeaks.Visible = true;
            }
            else
            {
                _lblSigPeaks.Visible = false;
                _lblInsigPeaks.Visible = false;
            }
        }
    }
}
