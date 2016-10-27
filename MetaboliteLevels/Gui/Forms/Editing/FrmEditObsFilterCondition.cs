using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Database;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Session.Main;
using MetaboliteLevels.Gui.Controls;
using MetaboliteLevels.Utilities;
using MGui.Datatypes;

namespace MetaboliteLevels.Gui.Forms.Editing
{
    public partial class FrmEditObsFilterCondition : Form
    {
        Core                                          _core;
        private bool                                  _isInitialised;
        private ConditionBox<int>                     _cbAq;
        private ConditionBox<BatchInfo>               _cbBatch;
        private ConditionBox<ObservationInfo>           _cbCond;
        private ConditionBox<GroupInfo>               _cbGroup;
        private ConditionBox<ObservationInfo>         _cbObs;
        private ConditionBox<int>                     _cbRep;
        private ConditionBox<int>                     _cbTime;
        private EnumComboBox<Filter.EElementOperator> _lsoAq;
        private EnumComboBox<Filter.EElementOperator> _lsoBatch;
        private EnumComboBox<Filter.EElementOperator> _lsoCond;
        private EnumComboBox<Filter.EElementOperator> _lsoGroup;
        private EnumComboBox<Filter.EElementOperator> _lsoObs;
        private EnumComboBox<Filter.EElementOperator> _lsoRep;
        private EnumComboBox<Filter.EElementOperator> _lsoTime;
        private readonly bool                         _readOnly;                                      

        /// <summary>
        /// Shows the form.
        /// </summary>
        /// <param name="autoSave">Controls whether to apply to core.DefaultStatistics</param>
        internal static ObsFilter.Condition Show(Form owner, Core core, ObsFilter.Condition defaults, bool readOnly)
        {
            using (FrmEditObsFilterCondition frm = new FrmEditObsFilterCondition(owner, core, defaults, readOnly))
            {
                if (UiControls.ShowWithDim(owner, frm) == DialogResult.OK)
                {                 
                    var r = frm.GetSelection();
                    UiControls.Assert(r != null, "Expected selection to be present if dialogue returned OK.");

                    return r;
                }

                return null;
            }
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        public FrmEditObsFilterCondition()
        {
            this.InitializeComponent();
            this._lblPreviewTitle.BackColor = UiControls.PreviewBackColour;
            this._lblPreviewTitle.ForeColor = UiControls.PreviewForeColour;
            this.DoubleBuffered = true;
            UiControls.SetIcon(this);
            // UiControls.CompensateForVisualStyles(this);
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        private FrmEditObsFilterCondition(Form owner, Core core, ObsFilter.Condition defaults, bool readOnly)
            : this()
        {
            this._core     = core;
            this._readOnly = readOnly;

            this.ctlTitleBar1.Text = readOnly ? "View Condition" : "Edit Condition";

            // Setup boxes
            this._cbAq    = DataSet.ForAcquisitions(core).CreateConditionBox(this._txtAq   , this._btnAq);
            this._cbBatch = DataSet.ForBatches     (core).CreateConditionBox(this._txtBatch, this._btnBatch);
            this._cbCond  = DataSet.ForConditions  (core).CreateConditionBox(this._txtCond , this._btnCond); 
            this._cbGroup = DataSet.ForGroups      (core).CreateConditionBox(this._txtGroup, this._btnGroup);
            this._cbObs   = DataSet.ForObservations(core).CreateConditionBox(this._txtObs  , this._btnObs);
            this._cbRep   = DataSet.ForReplicates  (core).CreateConditionBox(this._txtRep  , this._btnRep);
            this._cbTime  = DataSet.ForTimes       (core).CreateConditionBox(this._txtTime , this._btnTime);

            this._lsoAq    = EnumComboBox.Create   (this._lstAq      , Filter.EElementOperator.Is);
            this._lsoBatch = EnumComboBox.Create   (this._lstBatch   , Filter.EElementOperator.Is);
            this._lsoCond  = EnumComboBox.Create   (this._lstCond    , Filter.EElementOperator.Is);    
            this._lsoGroup = EnumComboBox.Create   (this._lstGroup   , Filter.EElementOperator.Is);
            this._lsoObs   = EnumComboBox.Create   (this._lstObs     , Filter.EElementOperator.Is);
            this._lsoRep   = EnumComboBox.Create   (this._lstRep     , Filter.EElementOperator.Is);
            this._lsoTime  = EnumComboBox.Create   (this._lstDay     , Filter.EElementOperator.Is);

            this._isInitialised = true;

            if (defaults == null)
            {
                this.checkBox1.Checked = false;
                this._radAnd.Checked   = true;
                this.something_Changed(null, null);
            }
            else
            {
                // Not
                this.checkBox1.Checked = defaults.Negate;
                this._radAnd.Checked   = defaults.CombiningOperator == Filter.ELogicOperator.And;
                this._radOr.Checked    = defaults.CombiningOperator == Filter.ELogicOperator.Or;

                if (defaults      is ObsFilter.ConditionAcquisition)
                {
                    var def                = (ObsFilter.ConditionAcquisition)defaults;
                    this._chkAq.Checked         = true;
                    this._lsoAq.SelectedItem    = def.Operator;
                    this._cbAq.SelectedItems    = def.Possibilities;
                }
                else if (defaults is ObsFilter.ConditionBatch)
                {
                    var def                = (ObsFilter.ConditionBatch)defaults;
                    this._chkBatch.Checked      = true;
                    this._lsoBatch.SelectedItem = def.Operator;
                    this._cbBatch.SelectedItems = def.Possibilities;
                }
                else if (defaults is ObsFilter.ConditionGroup)
                {
                    var def                = (ObsFilter.ConditionGroup)defaults;
                    this._chkGroup.Checked      = true;
                    this._lsoGroup.SelectedItem = def.Operator;
                    this._cbGroup.SelectedItems = def.Possibilities;
                }
                else if (defaults is ObsFilter.ConditionRep)
                {
                    var def                = (ObsFilter.ConditionRep)defaults;
                    this._chkRep.Checked        = true;
                    this._lsoRep.SelectedItem   = def.Operator;
                    this._cbRep.SelectedItems   = def.Possibilities;
                }
                else if (defaults is ObsFilter.ConditionTime)
                {
                    var def                = (ObsFilter.ConditionTime)defaults;
                    this._chkTime.Checked       = true;
                    this._lsoTime.SelectedItem  = def.Operator;
                    this._cbTime.SelectedItems  = def.Possibilities;
                }
                else if (defaults is ObsFilter.ConditionObservation)
                {
                    var def                = (ObsFilter.ConditionObservation)defaults;
                    this._chkObs.Checked        = true;
                    this._lsoObs.SelectedItem   = def.Operator;
                    this._cbObs.SelectedItems   = def.Possibilities;
                }
                else if (defaults is ObsFilter.ConditionCondition)
                {
                    var def                = (ObsFilter.ConditionCondition)defaults;
                    this._chkCond.Checked       = true;
                    this._lsoCond.SelectedItem  = def.Operator;
                    this._cbCond.SelectedItems  = def.Possibilities;
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

        private ObsFilter.Condition GetSelection()
        {
            this._checker.Clear();

            if (!this._radAnd.Checked && !this._radOr.Checked)
            {
                this._checker.Check( this._radAnd, false, "Select AND or OR" );
                this._checker.Check( this._radOr, false, "Select AND or OR" );  
            }    

            ObsFilter.Condition result;

            if (!( this.TryInvoke(this._chkAq   , this._cbAq,    this._lsoAq,    typeof(ObsFilter.ConditionAcquisition), out result)
                || this.TryInvoke(this._chkBatch, this._cbBatch, this._lsoBatch, typeof(ObsFilter.ConditionBatch),       out result)
                || this.TryInvoke(this._chkCond , this._cbCond,  this._lsoCond,  typeof(ObsFilter.ConditionCondition),   out result)
                || this.TryInvoke(this._chkGroup, this._cbGroup, this._lsoGroup, typeof(ObsFilter.ConditionGroup),       out result)
                || this.TryInvoke(this._chkObs  , this._cbObs,   this._lsoObs,   typeof(ObsFilter.ConditionObservation), out result)
                || this.TryInvoke(this._chkRep  , this._cbRep,   this._lsoRep,   typeof(ObsFilter.ConditionRep),         out result)
                || this.TryInvoke(this._chkTime , this._cbTime,  this._lsoTime,  typeof(ObsFilter.ConditionTime),        out result)))
            {
                this._checker.Check( this._chkAq   , false, "Select a condition" );
                this._checker.Check( this._chkBatch, false, "Select a condition" );
                this._checker.Check( this._chkCond , false, "Select a condition" );
                this._checker.Check( this._chkGroup, false, "Select a condition" );
                this._checker.Check( this._chkObs  , false, "Select a condition" );
                this._checker.Check( this._chkRep  , false, "Select a condition" );
                this._checker.Check( this._chkTime , false, "Select a condition" );

                return null;
            }

            if (this._checker.HasErrors)
            {
                return null;
            }
                                            
            return result;
        }

        private bool TryInvoke<T>(RadioButton radioButton, ConditionBox<T> conditionBox, EnumComboBox<Filter.EElementOperator> enumBox, Type type, out ObsFilter.Condition result)
        {
            if (!radioButton.Checked)
            {
                result = null;
                return false;
            }

            Filter.ELogicOperator op = this._radAnd.Checked ? Filter.ELogicOperator.And : Filter.ELogicOperator.Or;

            bool negate = this.checkBox1.Checked;

            var sel = conditionBox.GetSelectionOrNull();

            this._checker.Check( conditionBox.TextBox, conditionBox.SelectionValid, "Select a condition" );

            var en = enumBox.SelectedItem;

            this._checker.Check( enumBox.ComboBox, enumBox.HasSelection, "Select a condition" );

            if (sel==null || !en.HasValue)
            {
                result = null;
                return true;
            }

            Type[] types = { typeof(Filter.ELogicOperator), typeof(bool), typeof(Filter.EElementOperator), typeof(IEnumerable<T>) };
            result = (ObsFilter.Condition)type.GetConstructor(types).Invoke(new object[] { op, negate, en.Value, sel });
            return true;
        }  

        private void something_Changed(object sender, EventArgs e)
        {
            if (!this._isInitialised)
            {
                return;
            }
                       
            var sel = this.GetSelection();
            this.label2.Visible = sel == null;
            this._btnOk.Enabled = sel != null;

            this._lsoAq.Visible    = this._cbAq.Visible    = this._chkAq.Checked;
            this._lsoBatch.Visible = this._cbBatch.Visible = this._chkBatch.Checked;
            this._lsoCond.Visible  = this._cbCond.Visible  = this._chkCond.Checked;
            this._lsoGroup.Visible = this._cbGroup.Visible = this._chkGroup.Checked;
            this._lsoObs.Visible   = this._cbObs.Visible   = this._chkObs.Checked;
            this._lsoRep.Visible   = this._cbRep.Visible   = this._chkRep.Checked;
            this._lsoTime.Visible  = this._cbTime.Visible  = this._chkTime.Checked;     

            this.UpdatePreview(sel);
        }

        private void UpdatePreview(ObsFilter.Condition r)
        {
            if (r != null)
            {
                int passed = this._core.Observations.Count(r.Preview);
                int failed = this._core.Observations.Count - passed;
                this._lblSigPeaks.Text        = "True: " + passed;
                this._lblInsigPeaks.Text      = "False: " + failed;
                this._lblSigPeaks.ForeColor   = (failed < passed) ? Color.Blue : this.ForeColor;
                this._lblInsigPeaks.ForeColor = (failed > passed) ? Color.Blue : this.ForeColor;
                this._lblSigPeaks.Visible     = true;
                this._lblInsigPeaks.Visible   = true;
            }
            else
            {
                this._lblSigPeaks.Visible = false;
                this._lblInsigPeaks.Visible = false;
            }
        }
    }
}
