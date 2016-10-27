using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Algorithms.Definitions.Clusterers;
using MetaboliteLevels.Data.Algorithms.General;
using MetaboliteLevels.Data.Evaluation;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Gui.Forms.Activities;
using MetaboliteLevels.Gui.Forms.Selection;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Gui.Forms.Editing
{
    public partial class FrmEvaluateClusteringOptions : Form
    {
        private Core _core;
        private ArgsClusterer _backingField_selectedAlgorithm;
        private bool _readonly;                                           

        internal ArgsClusterer SelectedAlgorithm
        {
            get
            {
                return this._backingField_selectedAlgorithm;
            }
            set
            {
                this._backingField_selectedAlgorithm = value;

                if (value == null)
                {
                    this._lstParameters.Items.Clear();
                    this._txtAlgorithm.Text = string.Empty;
                    this._txtStatistics.Text = string.Empty;
                    return;
                }

                AlgoParameter prevParam = (AlgoParameter)this._lstParameters.SelectedItem;

                this._lstParameters.Items.Clear();

                this._txtAlgorithm.Text = value.ToString();

                foreach (AlgoParameter param in value.GetAlgorithmOrThrow().Parameters)
                {
                    this._lstParameters.Items.Add(param);
                }

                this._txtStatistics.Text = StringHelper.ArrayToString(EnumHelper.SplitEnum(value.Statistics));

                if (prevParam != null && this._lstParameters.Items.Contains(prevParam))
                {
                    this._lstParameters.SelectedItem = prevParam;
                }
                else
                {
                    this._something_Changed(null, EventArgs.Empty);
                }

                this._something_Changed(null, null);
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
            this._checker.Clear();

            this._checker.Check( this._txtAlgorithm, this.SelectedAlgorithm != null, "An algorithm is required" );

            int p = this._lstParameters.SelectedIndex;

            this._checker.Check( this._lstParameters, p == -1, "Requires parameter" );

            AlgoParameter pa = (AlgoParameter)this._lstParameters.SelectedItem;

            string[] lines = this._txtValues.Text.Split( "\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries );
            object[] opts = new object[lines.Length];
            bool inputError = false;

            for(int n=0; n < lines.Length; ++n)
            {
                var args = new FromStringArgs( this._core, lines[n] );
                opts[n] = pa.Type.FromString( args );

                if (opts[n] == null && !inputError)
                {
                    this._checker.Check( this._txtValues, false, args.Error ?? "error" );
                    inputError = true;
                }
            }
                                                                        
            int num = (int)this._numNumTimes.Value;
            this._txtNumberOfValues.Text = opts.Length.ToString();

            this._checker.Check( this._txtValues, opts.Length != 0 || inputError, "Enter a valid list of parameters");   
            this._checker.Check( this._numNumTimes, num > 0 && num <100, "Repeat count is invalid");

            if (this._checker.HasErrors)
            {
                return null;
            }               

            return new ClusterEvaluationPointer(new ClusterEvaluationConfiguration(this.SelectedAlgorithm, p, opts, num));
        }

        private FrmEvaluateClusteringOptions()
        {
            this.InitializeComponent();
            UiControls.SetIcon(this);
        }

        private FrmEvaluateClusteringOptions(Core core, ClusterEvaluationConfiguration def, bool readOnly)
            : this()
        {
            this._readonly = readOnly;
            this._core = core;

            if (def != null)
            {
                this.SelectedAlgorithm = def.ClustererConfiguration;
                this._lstParameters.SelectedIndex = def.ParameterIndex;
                this._txtValues.Text = StringHelper.ArrayToString(def.ParameterValues, AlgoParameterCollection.ParamToString, "\r\n");
                this._numNumTimes.Value = def.NumberOfRepeats;
            }

            // UiControls.CompensateForVisualStyles(this);

            if (readOnly)
            {
                UiControls.MakeReadOnly(this);
            }
        }

        private void _something_Changed(object sender, EventArgs e)
        {
            this._btnOk.Enabled = this.GetSelection() != null;
        }

        private void _btnSetAlgorithm_Click(object sender, EventArgs e)
        {
            ArgsClusterer newAlgo = FrmEditConfigurationCluster.Show(this, this._core, this.SelectedAlgorithm, this._readonly, true);

            if (newAlgo != null)
            {
                this.SelectedAlgorithm = newAlgo;
            }
        }       

        private void _btnAddRange_Click(object sender, EventArgs e)
        {                                  
            StringBuilder sb = new StringBuilder();

            for (decimal x = this._numFrom.Value; x <= this._numTo.Value; x += this._numStep.Value)
            {
                sb.AppendLine(x.ToString());
            }

            this._txtValues.Text = sb.ToString();
        }

        private void _btnAddRepeats_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            for (int x = 0; x <= this._numCount.Value; ++x)
            {
                sb.AppendLine(this._txtValue.Text);
            }

            this._txtValues.Text = sb.ToString();
        }

        private void _btnClear_Click(object sender, EventArgs e)
        {
            this._txtValues.Text = string.Empty;
        }
    }
}
