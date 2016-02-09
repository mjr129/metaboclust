using MetaboliteLevels.Controls;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Settings;
using MetaboliteLevels.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.DataInfo;
using MetaboliteLevels.Forms.Generic;

namespace MetaboliteLevels.Forms.Editing
{
    public partial class FrmObservationFilterCondition : Form
    {
        Core _core;
        private bool _isInitialised;
        private ConditionBox<int> _cbAq;
        private ConditionBox<BatchInfo> _cbBatch;
        private ConditionBox<ConditionInfo> _cbCond;
        private ConditionBox<GroupInfo> _cbGroup;
        private ConditionBox<ObservationInfo> _cbObs;
        private ConditionBox<int> _cbRep;
        private ConditionBox<int> _cbTime;
        private EnumComboBox<Filter.EElementOperator> _lsoAq;
        private EnumComboBox<Filter.EElementOperator> _lsoBatch;
        private EnumComboBox<Filter.EElementOperator> _lsoCond;
        private EnumComboBox<Filter.EElementOperator> _lsoGroup;
        private EnumComboBox<Filter.EElementOperator> _lsoObs;
        private EnumComboBox<Filter.EElementOperator> _lsoRep;
        private EnumComboBox<Filter.EElementOperator> _lsoTime;
        private readonly bool _readOnly;

        /// <summary>
        /// Shows the form.
        /// </summary>
        /// <param name="autoSave">Controls whether to apply to core.DefaultStatistics</param>
        internal static ObsFilter.Condition Show(Form owner, Core core, ObsFilter.Condition defaults, bool readOnly)
        {
            using (FrmObservationFilterCondition frm = new FrmObservationFilterCondition(owner, core, defaults, readOnly))
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
        public FrmObservationFilterCondition()
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
        private FrmObservationFilterCondition(Form owner, Core core, ObsFilter.Condition defaults, bool readOnly)
            : this()
        {
            this._core = core;
            this._readOnly = readOnly;

            ctlTitleBar1.Text = readOnly ? "View Condition" : "Edit Condition";

            // Setup boxes
            _cbAq = ListValueSet.ForAcquisitions(core).CreateConditionBox(_txtAq, _btnAq);
            _cbBatch = ListValueSet.ForBatches(core).CreateConditionBox(_txtBatch, _btnBatch);
            _cbCond = ListValueSet.ForConditions(core).CreateConditionBox(_txtCond, _btnCond);
            _cbGroup = ListValueSet.ForGroups(core).CreateConditionBox(_txtGroup, _btnGroup);
            _cbObs = ListValueSet.ForObservations(core).CreateConditionBox(_txtObs, _btnObs);
            _cbRep = ListValueSet.ForReplicates(core).CreateConditionBox(_txtRep, _btnRep);
            _cbTime = ListValueSet.ForTimes(core).CreateConditionBox(_txtTime, _btnTime);

            _lsoAq = EnumComboBox.Create(this._lstAq, Filter.EElementOperator.Is);
            _lsoBatch = EnumComboBox.Create(this._lstBatch, Filter.EElementOperator.Is);
            _lsoCond = EnumComboBox.Create(this._lstCond, Filter.EElementOperator.Is);
            _lsoGroup = EnumComboBox.Create(this._lstGroup, Filter.EElementOperator.Is);
            _lsoObs = EnumComboBox.Create(this._lstObs, Filter.EElementOperator.Is);
            _lsoRep = EnumComboBox.Create(this._lstRep, Filter.EElementOperator.Is);
            _lsoTime = EnumComboBox.Create(this._lstDay, Filter.EElementOperator.Is);

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

                if (defaults is ObsFilter.ConditionAcquisition)
                {
                    var def = (ObsFilter.ConditionAcquisition)defaults;

                    _chkAq.Checked = true;
                    _lsoAq.SelectedItem = def.Operator;
                    _cbAq.SelectedItems = def.Possibilities;
                }
                else if (defaults is ObsFilter.ConditionBatch)
                {
                    var def = (ObsFilter.ConditionBatch)defaults;

                    _chkBatch.Checked = true;
                    _lsoBatch.SelectedItem = def.Operator;
                    _cbBatch.SelectedItems = def.Possibilities;
                }
                else if (defaults is ObsFilter.ConditionGroup)
                {
                    var def = (ObsFilter.ConditionGroup)defaults;

                    _chkGroup.Checked = true;
                    _lsoGroup.SelectedItem = def.Operator;
                    _cbGroup.SelectedItems = def.Possibilities;
                }
                else if (defaults is ObsFilter.ConditionRep)
                {
                    var def = (ObsFilter.ConditionRep)defaults;

                    _chkRep.Checked = true;
                    _lsoRep.SelectedItem = def.Operator;
                    _cbRep.SelectedItems = def.Possibilities;
                }
                else if (defaults is ObsFilter.ConditionTime)
                {
                    var def = (ObsFilter.ConditionTime)defaults;

                    _chkTime.Checked = true;
                    _lsoTime.SelectedItem = def.Operator;
                    _cbTime.SelectedItems = def.Possibilities;
                }
                else if (defaults is ObsFilter.ConditionObservation)
                {
                    var def = (ObsFilter.ConditionObservation)defaults;

                    _chkObs.Checked = true;
                    _lsoObs.SelectedItem = def.Operator;
                    _cbObs.SelectedItems = def.Possibilities;
                }
                else if (defaults is ObsFilter.ConditionCondition)
                {
                    var def = (ObsFilter.ConditionCondition)defaults;

                    _chkCond.Checked = true;
                    _lsoCond.SelectedItem = def.Operator;
                    _cbCond.SelectedItems = def.Possibilities;
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

        private ObsFilter.Condition GetSelection(out string error)
        {
            if (!_radAnd.Checked && !_radOr.Checked)
            {
                error = "Select AND or OR";
                return null;
            }

            ObsFilter.Condition result;

            if (TryInvoke(this._chkAq, _cbAq, _lsoAq, typeof(ObsFilter.ConditionAcquisition), out result)
                || TryInvoke(this._chkBatch, _cbBatch, _lsoBatch, typeof(ObsFilter.ConditionBatch), out result)
                || TryInvoke(this._chkCond, _cbCond, _lsoCond, typeof(ObsFilter.ConditionCondition), out result)
                || TryInvoke(this._chkGroup, _cbGroup, _lsoGroup, typeof(ObsFilter.ConditionGroup), out result)
                || TryInvoke(this._chkObs, _cbObs, _lsoObs, typeof(ObsFilter.ConditionObservation), out result)
                || TryInvoke(this._chkRep, _cbRep, _lsoRep, typeof(ObsFilter.ConditionRep), out result)
                || TryInvoke(this._chkTime, _cbTime, _lsoTime, typeof(ObsFilter.ConditionTime), out result))
            {
                error = null;
                return result;
            }

            error = "Select the conditions";
            return null;
        }

        private bool TryInvoke<T>(RadioButton radioButton, ConditionBox<T> conditionBox, EnumComboBox<Filter.EElementOperator> enumBox, Type type, out ObsFilter.Condition result)
        {
            if (!radioButton.Checked)
            {
                result = null;
                return false;
            }

            Filter.ELogicOperator op = _radAnd.Checked ? Filter.ELogicOperator.And : Filter.ELogicOperator.Or;

            bool negate = checkBox1.Checked;

            var sel = conditionBox.GetSelection();

            if (sel == null)
            {
                result = null;
                return true;
            }

            var en = enumBox.SelectedItem;

            if (!en.HasValue)
            {
                result = null;
                return true;
            }

            Type[] types = { typeof(Filter.ELogicOperator), typeof(bool), typeof(Filter.EElementOperator), typeof(IEnumerable<T>) };
            result = (ObsFilter.Condition)type.GetConstructor(types).Invoke(new object[] { op, negate, en.Value, sel });
            return true;
        }  

        private void _txtComp_TextChanged(object sender, EventArgs e)
        {
            if (!_isInitialised)
            {
                return;
            }

            string err;
            var sel = GetSelection(out err);
            label2.Text = err;
            label2.Visible = !string.IsNullOrEmpty(err);
            _btnOk.Enabled = sel != null;

            _lsoAq.Visible = _cbAq.Visible = _chkAq.Checked;
            _lsoBatch.Visible = _cbBatch.Visible = _chkBatch.Checked;
            _lsoCond.Visible = _cbCond.Visible = _chkCond.Checked;
            _lsoGroup.Visible = _cbGroup.Visible = _chkGroup.Checked;
            _lsoObs.Visible = _cbObs.Visible = _chkObs.Checked;
            _lsoRep.Visible = _cbRep.Visible = _chkRep.Checked;
            _lsoTime.Visible = _cbTime.Visible = _chkTime.Checked;

            UpdatePreview(sel);
        }

        private void UpdatePreview(ObsFilter.Condition r)
        {
            if (r != null)
            {
                int passed = _core.Observations.Count(r.Preview);
                int failed = _core.Observations.Count - passed;
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
