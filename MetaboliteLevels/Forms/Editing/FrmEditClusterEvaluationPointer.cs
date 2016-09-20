using MetaboliteLevels.Algorithms.Statistics;
using MetaboliteLevels.Controls;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Forms.Generic;
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
using MetaboliteLevels.Algorithms.Statistics.Arguments;
using MetaboliteLevels.Forms.Algorithms.ClusterEvaluation;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Properties;
using MGui;
using MGui.Helpers;

namespace MetaboliteLevels.Forms.Algorithms
{
    public partial class FrmEvaluateClusteringOptions : Form
    {
        private Core _core;
        private ArgsClusterer __backingField_selectedAlgorithm;
        private bool _readonly;                                           

        internal ArgsClusterer SelectedAlgorithm
        {
            get
            {
                return __backingField_selectedAlgorithm;
            }
            set
            {
                __backingField_selectedAlgorithm = value;

                if (value == null)
                {
                    _lstParameters.Items.Clear();
                    _txtAlgorithm.Text = string.Empty;
                    _txtStatistics.Text = string.Empty;
                    return;
                }

                AlgoParameter prevParam = (AlgoParameter)_lstParameters.SelectedItem;

                _lstParameters.Items.Clear();

                _txtAlgorithm.Text = value.ToString();

                foreach (AlgoParameter param in value.GetAlgorithmOrThrow().Parameters)
                {
                    _lstParameters.Items.Add(param);
                }

                _txtStatistics.Text = StringHelper.ArrayToString(EnumHelper.SplitEnum(value.Statistics));

                if (prevParam != null && _lstParameters.Items.Contains(prevParam))
                {
                    _lstParameters.SelectedItem = prevParam;
                }
                else
                {
                    _something_Changed(null, EventArgs.Empty);
                }

                _something_Changed(null, null);
            }
        }

        internal static ClusterEvaluationPointer Show(Form owner, Core core, ClusterEvaluationPointer options, bool readOnly)
        {
            ClusterEvaluationConfiguration config;

            if (options == null)
            {
                config = null;
            }
            else
            {
                config = options.Configuration;

                if (config == null)
                {
                    if (!FrmMsgBox.ShowOkCancel(owner, "Open Configuration", "The selected configuration has been saved to disk and must be loaded before it is viewed or modified."))
                    {
                        return null;
                    }

                    config = FrmWait.Show<ClusterEvaluationResults>(owner, "Loading results", null, z => FrmActEvaluate.LoadResults(core, options.FileName, z))?.Configuration;

                    if (config == null)
                    {
                        return null;
                    }
                }
            }

            using (FrmEvaluateClusteringOptions frm = new FrmEvaluateClusteringOptions(core, config, readOnly))
            {
                if (UiControls.ShowWithDim(owner, frm) == DialogResult.OK)
                {
                    return frm.GetSelection();
                }
            }

            return null;
        }

        private ClusterEvaluationPointer GetSelection()
        {
            _checker.Clear();

            _checker.Check( _txtAlgorithm, SelectedAlgorithm != null, "An algorithm is required" );

            int p = _lstParameters.SelectedIndex;

            _checker.Check( _lstParameters, p == -1, "Requires parameter" );

            AlgoParameter pa = (AlgoParameter)_lstParameters.SelectedItem;

            object[] opts = StringHelper.StringToArray(_txtValues.Text, z => AlgoParameterCollection.TryReadParameter(_core, z, pa.Type), "\r\n");
            int num = (int)_numNumTimes.Value;
            _txtNumberOfValues.Text = opts.Length.ToString();

            _checker.Check( _txtValues, opts.Length != 0 && !(opts.Length == 1 && opts[0] == null), "Enter a valid list of parameters");   
            _checker.Check( _txtValues, !opts.Any( z => z == null ), "One or more values are invalid");   
            _checker.Check( _numNumTimes, num > 0 && num <100, "Repeat count is invalid");

            if (_checker.HasErrors)
            {
                return null;
            }               

            return new ClusterEvaluationPointer(new ClusterEvaluationConfiguration(SelectedAlgorithm, p, opts, num));
        }

        private FrmEvaluateClusteringOptions()
        {
            InitializeComponent();
            UiControls.SetIcon(this);
        }

        private FrmEvaluateClusteringOptions(Core core, ClusterEvaluationConfiguration def, bool readOnly)
            : this()
        {
            _readonly = readOnly;
            _core = core;

            if (def != null)
            {
                SelectedAlgorithm = def.ClustererConfiguration;
                _lstParameters.SelectedIndex = def.ParameterIndex;
                _txtValues.Text = StringHelper.ArrayToString(def.ParameterValues, z => AlgoParameterCollection.ParamToString(true, core, z), "\r\n");
                _numNumTimes.Value = def.NumberOfRepeats;
            }

            // UiControls.CompensateForVisualStyles(this);

            if (readOnly)
            {
                UiControls.MakeReadOnly(this);
            }
        }

        private void _something_Changed(object sender, EventArgs e)
        {
            _btnOk.Enabled = GetSelection() != null;
        }

        private void _btnSetAlgorithm_Click(object sender, EventArgs e)
        {
            ArgsClusterer newAlgo = FrmEditConfigurationCluster.Show(this, _core, SelectedAlgorithm, this._readonly, true);

            if (newAlgo != null)
            {
                SelectedAlgorithm = newAlgo;
            }
        }       

        private void _btnAddRange_Click(object sender, EventArgs e)
        {                                  
            StringBuilder sb = new StringBuilder();

            for (decimal x = _numFrom.Value; x <= _numTo.Value; x += _numStep.Value)
            {
                sb.AppendLine(x.ToString());
            }

            _txtValues.Text = sb.ToString();
        }

        private void _btnAddRepeats_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            for (int x = 0; x <= _numCount.Value; ++x)
            {
                sb.AppendLine(_txtValue.Text);
            }

            _txtValues.Text = sb.ToString();
        }

        private void _btnClear_Click(object sender, EventArgs e)
        {
            _txtValues.Text = string.Empty;
        }
    }
}
