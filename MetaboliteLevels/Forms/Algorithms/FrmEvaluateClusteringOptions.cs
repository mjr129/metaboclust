﻿using MetaboliteLevels.Algorithms.Statistics;
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
using MetaboliteLevels.Forms.Algorithms.ClusterEvaluation;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Properties;

namespace MetaboliteLevels.Forms.Algorithms
{
    public partial class FrmEvaluateClusteringOptions : Form
    {
        private Core _core;
        private ConfigurationClusterer __backingField_selectedAlgorithm;
        private bool _readonly;
        private CtlErrorProvider _errorProvider1 = new CtlErrorProvider();

        internal ConfigurationClusterer SelectedAlgorithm
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

                foreach (AlgoParameter param in value.Cached.Parameters)
                {
                    _lstParameters.Items.Add(param);
                }

                _txtStatistics.Text = StringHelper.ArrayToString(EnumHelper.SplitEnum(value.Args.Statistics));

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

                    config = FrmWait.Show<ClusterEvaluationResults>(owner, "Loading results", null, z => FrmEvaluateClustering.LoadResults(core, options.FileName, z))?.Configuration;

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
            _errorProvider1.Clear();

            if (SelectedAlgorithm == null)
            {
                _txtNumberOfValues.Text = "?";
                _errorProvider1.ShowError(_txtAlgorithm, "Requires algorithm");
                return null;
            }

            int p = _lstParameters.SelectedIndex;

            if (p == -1)
            {
                _txtNumberOfValues.Text = "?";
                _errorProvider1.ShowError(_lstParameters, "Requires parameter");
                return null;
            }

            AlgoParameter pa = (AlgoParameter)_lstParameters.SelectedItem;

            object[] opts = StringHelper.StringToArray(_txtValues.Text, z => AlgoParameterCollection.TryReadParameter(_core, z, pa.Type), "\r\n");
            int num = (int)_numNumTimes.Value;
            _txtNumberOfValues.Text = opts.Length.ToString();

            if (opts.Length == 0 || (opts.Length == 1 && opts[0] == null))
            {
                _errorProvider1.ShowError(_txtValues, "Enter a valid list of parameters");
                return null;
            }

            if (opts.Any(z => z == null))
            {
                _errorProvider1.ShowError(_txtValues, "One or more values are invalid");
                return null;
            }

            if (num <= 0 || num > 99)
            {
                _errorProvider1.ShowError(_numNumTimes, "Repeat count is invalid");
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

            UiControls.CompensateForVisualStyles(this);

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
            ConfigurationClusterer newAlgo = FrmAlgoCluster.Show(this, _core, SelectedAlgorithm, this._readonly, true);

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
