using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Algorithms.Definitions.Statistics;
using MetaboliteLevels.Data.Database;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Session.Main;
using MetaboliteLevels.Gui.Controls;
using MetaboliteLevels.Gui.Forms.Selection;
using MetaboliteLevels.Utilities;
using MGui.Datatypes;
using MGui.Helpers;

namespace MetaboliteLevels.Gui.Forms.Editing
{
    /// <summary>
    /// SigFilter editor form.
    /// </summary>
    public partial class FrmEditPeakFilterCondition : Form
    {
        Core _core;
        private ConditionBox<Peak> _cbPeaks;
        private ConditionBox<PeakFlag> _cbFlags;
        private ConditionBox<Cluster> _cbClusters;
        private EnumComboBox<PeakFilter.ESetOperator> _lsoFlags;
        private EnumComboBox<PeakFilter.ELimitedSetOperator> _lsoPats;
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
            using (FrmEditPeakFilterCondition frm = new FrmEditPeakFilterCondition(owner, core, defaults, readOnly))
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
        public FrmEditPeakFilterCondition()
        {
            this.InitializeComponent();

            this._lblPreviewTitle.BackColor = UiControls.PreviewBackColour;
            this._lblPreviewTitle.ForeColor = UiControls.PreviewForeColour;

            this.DoubleBuffered = true;
            UiControls.SetIcon(this);
            // UiControls.CompensateForVisualStyles(this);
        }

        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        private FrmEditPeakFilterCondition(Form owner, Core core, PeakFilter.Condition defaults, bool readOnly)
            : this()
        {
            this._core = core;
            this._readOnly = readOnly;

            this.ctlTitleBar1.Text = readOnly ? "View Condition" : "Edit Condition";

            // Setup boxes
            this._cbPeaks = DataSet.ForPeaks(core).CreateConditionBox(this._txtIsInSet, this._btnIsInSet);
            this._cbFlags = DataSet.ForPeakFlags(core).CreateConditionBox(this._txtIsFlaggedWith, this._btnIsFlaggedWith);
            this._cbClusters = DataSet.ForClusters(core).CreateConditionBox(this._txtIsInCluster, this._btnIsInCluster);

            this._lsoFlags = EnumComboBox.Create(this._lstFlagComparator, Filter.ESetOperator.Any);
            this._lsoPats = EnumComboBox.Create(this._lstClusterComparator, Filter.ELimitedSetOperator.Any);
            this._lsoPeaks = EnumComboBox.Create(this._lstPeakComparator, Filter.EElementOperator.Is);
            this._lsoFilter = EnumComboBox.Create(this._lstFilterOp, Filter.EElementOperator.Is);
            this._lsoStats = EnumComboBox.Create(this._lstStatisticComparator, Filter.EStatOperator.LessThan);
            this._lstIsStatistic.Items.AddRange(IVisualisableExtensions.WhereEnabled(core.Statistics).ToArray());

            this._ecbFilter = DataSet.ForPeakFilter(core).CreateComboBox(this._lstFilter, null, EditableComboBox.EFlags.IncludeAll);

            this._isInitialised = true;

            if (defaults == null)
            {
                this.checkBox1.Checked = false;
                this._radAnd.Checked = true;
                this._txtComp_TextChanged(null, null);
            }
            else
            {
                // Not
                this.checkBox1.Checked = defaults.Negate;
                this._radAnd.Checked = defaults.CombiningOperator == Filter.ELogicOperator.And;
                this._radOr.Checked = defaults.CombiningOperator == Filter.ELogicOperator.Or;

                if (defaults is PeakFilter.ConditionCluster)
                {
                    PeakFilter.ConditionCluster def = (PeakFilter.ConditionCluster)defaults;

                    List<Cluster> strong;

                    if (!def.Clusters.TryGetStrong(out strong))
                    {
                        this.ShowWeakFailureMessage("clusters");
                    }

                    this._chkIsInCluster.Checked = true;
                    this._lsoPats.SelectedItem = def.ClustersOp;
                    this._cbClusters.SelectedItems = strong;
                }
                else if (defaults is PeakFilter.ConditionPeak)
                {
                    PeakFilter.ConditionPeak def = (PeakFilter.ConditionPeak)defaults;

                    List<Peak> strong;

                    if (!def.Peaks.TryGetStrong(out strong))
                    {
                        this.ShowWeakFailureMessage("peaks");
                    }

                    this._chkIsInSet.Checked = true;
                    this._lsoPeaks.SelectedItem = def.PeaksOp;
                    this._cbPeaks.SelectedItems = strong;
                }
                else if (defaults is PeakFilter.ConditionFlags)
                {
                    PeakFilter.ConditionFlags def = (PeakFilter.ConditionFlags)defaults;

                    List<PeakFlag> strong;

                    if (!def.Flags.TryGetStrong(out strong))
                    {
                        this.ShowWeakFailureMessage("peaks");
                    }

                    this._chkIsFlaggedWith.Checked = true;
                    this._lsoFlags.SelectedItem = def.FlagsOp;
                    this._cbFlags.SelectedItems = strong;
                }
                else if (defaults is PeakFilter.ConditionStatistic)
                {
                    PeakFilter.ConditionStatistic def = (PeakFilter.ConditionStatistic)defaults;

                    ConfigurationStatistic strong;

                    if (!def.Statistic.TryGetTarget(out strong))
                    {
                        this.ShowWeakFailureMessage("statistics");
                    }

                    ConfigurationStatistic stat = def.Statistic.GetTarget();

                    if (stat == null)
                    {
                        FrmMsgBox.ShowError(this, "The statistic specified when this condition was created has since been removed. Please select a different statistic.");
                    }

                    this._chkIsStatistic.Checked = true;
                    this._lsoStats.SelectedItem = def.StatisticOp;
                    this._lstIsStatistic.SelectedItem = stat;
                    this._txtStatisticValue.Text = def.StatisticValue.ToString();
                }
                else if (defaults is PeakFilter.ConditionFilter)
                {
                    PeakFilter.ConditionFilter def = (PeakFilter.ConditionFilter)defaults;

                    this._radFilter.Checked = true;
                    this._lsoFilter.SelectedItem = def.FilterOp ? Filter.EElementOperator.Is : Filter.EElementOperator.IsNot;
                    this._ecbFilter.SelectedItem = def.Filter;
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
            if (!this._radAnd.Checked && !this._radOr.Checked)
            {
                error = "Select AND or OR";
                return null;
            }

            PeakFilter.ELogicOperator op = this._radAnd.Checked ? PeakFilter.ELogicOperator.And : PeakFilter.ELogicOperator.Or;

            bool negate = this.checkBox1.Checked;

            if (this._chkIsInCluster.Checked)
            {
                var sel = this._cbClusters.GetSelectionOrNull();

                if (sel == null)
                {
                    error = "Select one or more clusters";
                    return null;
                }

                var en = this._lsoPats.SelectedItem;

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
                var sel = this._cbPeaks.GetSelectionOrNull();

                if (sel == null)
                {
                    error = "Select one or more PEAKs";
                    return null;
                }

                var en = this._lsoPeaks.SelectedItem;

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
                var sel = this._cbFlags.GetSelectionOrNull();

                if (sel == null)
                {
                    error = "Select one or more FLAGs";
                    return null;
                }

                var en = this._lsoFlags.SelectedItem;

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
                var sel = (ConfigurationStatistic)this._lstIsStatistic.SelectedItem;

                if (sel == null)
                {
                    error = "Select the STATISTIC";
                    return null;
                }

                var en = this._lsoStats.SelectedItem;

                if (!en.HasValue)
                {
                    error = "Select the STATISTIC operator";
                    return null;
                }

                double va;

                if (!double.TryParse(this._txtStatisticValue.Text, out va))
                {
                    error = "Enter a valid value to compare against";
                    return null;
                }

                error = null;
                return new PeakFilter.ConditionStatistic(op, negate, en.Value, sel, va);
            }
            else if (this._radFilter.Checked)
            {
                if (!this._ecbFilter.HasSelection)
                {
                    error = "Select the FILTER";
                    return null;
                }

                PeakFilter sel =  this._ecbFilter.SelectedItem;

                var en = this._lsoFilter.SelectedItem;

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
            if (!this._isInitialised)
            {
                return;
            }

            //this.SuspendDrawing();

            string err;
            var sel = this.GetSelection(out err);
            this.label2.Text = err;
            this.label2.Visible = !string.IsNullOrEmpty(err);
            this._btnOk.Enabled = sel != null;

            this._lsoPeaks.Visible = this._chkIsInSet.Checked;
            this._cbPeaks.Visible = this._chkIsInSet.Checked;

            this._lsoFlags.Visible = this._chkIsFlaggedWith.Checked;
            this._cbFlags.Visible = this._chkIsFlaggedWith.Checked;

            this._lsoPats.Visible = this._chkIsInCluster.Checked;
            this._cbClusters.Visible = this._chkIsInCluster.Checked;

            this._lsoFilter.Visible = this._radFilter.Checked;
            this._lstFilter.Visible = this._radFilter.Checked;

            this._lstIsStatistic.Visible = this._chkIsStatistic.Checked;
            this._lsoStats.Visible = this._chkIsStatistic.Checked;
            this._txtStatisticValue.Visible = this._chkIsStatistic.Checked;

            this.UpdatePreview(sel);

            //this.ResumeDrawing();
        }

        private void UpdatePreview(PeakFilter.Condition r)
        {
            if (r != null)
            {
                int passed = this._core.Peaks.Count(r.Preview);
                int failed = this._core.Peaks.Count - passed;
                this._lblSigPeaks.Text = "True: " + passed;
                this._lblInsigPeaks.Text = "False: " + failed;
                this._lblSigPeaks.ForeColor = (failed < passed) ? Color.Blue : this.ForeColor;
                this._lblInsigPeaks.ForeColor = (failed > passed) ? Color.Blue : this.ForeColor;
                this._lblSigPeaks.Visible = true;
                this._lblInsigPeaks.Visible = true;
            }
            else
            {
                this._lblSigPeaks.Visible = false;
                this._lblInsigPeaks.Visible = false;
            }
        }
    }
}
