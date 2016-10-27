using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Algorithms.Definitions.Base.Misc;
using MetaboliteLevels.Data.Algorithms.Definitions.Clusterers;
using MetaboliteLevels.Data.Algorithms.Definitions.Metrics;
using MetaboliteLevels.Data.Algorithms.General;
using MetaboliteLevels.Data.Database;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Session.Main;
using MetaboliteLevels.Gui.Controls;
using MetaboliteLevels.Gui.Forms.Activities;
using MetaboliteLevels.Gui.Forms.Selection;
using MetaboliteLevels.Gui.Forms.Text;
using MetaboliteLevels.Resx;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Gui.Forms.Editing
{
    public partial class FrmEditConfigurationCluster : Form
    {
        private Core _core;
        private string _comment;
        private EditableComboBox<PeakFilter> _ecbPeakFilter;
        private EditableComboBox<ObsFilter> _ecbObsFilter;
        private EditableComboBox<ClustererBase> _ecbMethod;
        private EditableComboBox<MetricBase> _ecbMeasure;
        private ConditionBox<EClustererStatistics> _cbStatistics;
        private readonly bool _readOnly;
        private EditableComboBox<IMatrixProvider> _ecbSource;

        internal static ArgsClusterer Show(Form owner, Core core, ArgsClusterer def, bool readOnly, bool hideOptimise)
        {
            using (FrmEditConfigurationCluster frm = new FrmEditConfigurationCluster(core, def, readOnly, hideOptimise))
            {
                if (UiControls.ShowWithDim(owner, frm) == DialogResult.OK)
                {
                    UiControls.Assert(!readOnly, "Didn't expect an OK result from a readonly dialogue.");
                    return frm.GetSelection();
                }

                return null;
            }
        }

        private FrmEditConfigurationCluster()
        {
            this.InitializeComponent();
            UiControls.SetIcon(this);    
        }

        private FrmEditConfigurationCluster(Core core, ArgsClusterer def, bool readOnly, bool hideOptimise)
            : this()
        {
            this._core = core;
            this._ecbPeakFilter = DataSet.ForPeakFilter(core).CreateComboBox(this._lstPeakFilter, this._btnPeakFilter,  ENullItemName.RepresentingAll);
            this._ecbObsFilter = DataSet.ForObsFilter(core).CreateComboBox(this._lstObsFilter, this._btnObsFilter,  ENullItemName.RepresentingAll);
            this._ecbMethod = DataSet.ForClustererAlgorithms(core).CreateComboBox(this._lstMethod, this._btnNewStatistic, ENullItemName.RepresentingNone);
            this._ecbMeasure = DataSet.ForMetricAlgorithms(core).CreateComboBox(this._lstMeasure, this._btnNewDistance, ENullItemName.RepresentingNone);
            this._ecbSource = DataSet.ForMatrixProviders( core ).CreateComboBox( this._lstSource, this._btnSource, ENullItemName.NoNullItem );
            this._cbStatistics = DataSet.ForFlagsEnum<EClustererStatistics>( this._core, "Cluster Statistics" ).CreateConditionBox(this._txtStatistics, this._btnSetStatistics);
            this._readOnly = readOnly;

            if (def != null)
            {
                // Name
                this._txtName.Text = def.OverrideDisplayName;

                // Comment
                this._comment = def.Comment;

                // Method
                this._ecbMethod.SelectedItem =(ClustererBase) def.GetAlgorithmOrNull();

                // Params
                this._txtParams.Text = AlgoParameterCollection.ParamsToReversableString(def.Parameters, core);

                // PeakFilter
                this._ecbPeakFilter.SelectedItem = def.PeakFilter;

                // Distance
                this._ecbMeasure.SelectedItem = def.Distance != null ? (MetricBase)def.Distance.Args.GetAlgorithmOrNull() : null;

                // Distance params
                this._txtMeasureParams.Text = StringHelper.ArrayToString(def.Distance?.Args.Parameters);

                // Suppress distance
                this._cbStatistics.SelectedItems = EnumHelper.SplitEnum<EClustererStatistics>(def.Statistics);

                // Input vector
                this._ecbSource.SelectedItem = def.SourceProvider;

                // ObsFilter
                this._ecbObsFilter.SelectedItem = def.ObsFilter;

                // Seperate groups
                this._chkSepGroups.Checked = def.SplitGroups;
            }

            if (readOnly)
            {
                UiControls.MakeReadOnly(this);

                this._btnParameterOptimiser.Visible = false;
                this._btnComment.Enabled = true;
                this.ctlTitleBar1.Text = "View Clustering Algorithm";
            }
            else if (def != null)
            {
                this.ctlTitleBar1.Text = "Edit Clustering Algorithm";
            }
            else
            {
                this.ctlTitleBar1.Text = "New Clustering Algorithm";
            }

            this.CheckAndChange(null, null);

            // UiControls.CompensateForVisualStyles(this);

            if (hideOptimise)
            {
                this._btnParameterOptimiser.Visible = false;
                this._btnOk.Text = "Continue";
            }
        }

        private ArgsClusterer GetSelection()
        {
            IMatrixProvider src;
            PeakFilter peakFilter;
            ObsFilter obsFilter;
            string title;

            this._checker.Clear();

            // Selection
            ClustererBase sel = (ClustererBase)this._ecbMethod.SelectedItem;

            // Title / comments
            title = string.IsNullOrWhiteSpace(this._txtName.Text) ? null : this._txtName.Text;

            // Parameters
            object[] parameters;

            if (sel != null)
            {
                string error;
                parameters = sel.Parameters.TryStringToParams( this._core, this._txtParams.Text, out error );
                this._checker.Check( this._txtParams, parameters != null, error ?? "error" );
            }
            else
            {
                parameters = null;
                this._checker.Check( this._ecbMethod.ComboBox, false, "A method is required." );
            }

            // Peak filter
            peakFilter = this._ecbPeakFilter.SelectedItem;
            this._checker.Check( this._ecbPeakFilter.ComboBox, this._ecbPeakFilter.HasSelection, "Select a valid peak filter" );

            // Suppress metric
            EClustererStatistics suppressMetric;

            if (this._cbStatistics.SelectionValid)
            {
                suppressMetric = (EClustererStatistics)this._cbStatistics.SelectedItems.Cast<int>().Sum();
            }
            else
            {
                this._checker.Check( this._cbStatistics.TextBox, false, "Select a valid set of statistics" );
                suppressMetric = default( EClustererStatistics );
            }

            // Distance metric
            MetricBase dMet;

            dMet = (MetricBase)this._ecbMeasure.SelectedItem;

            // Distance metric params
            object[] dMetParams;

            if (dMet != null)
            {
                string error;
                dMetParams = dMet.Parameters.TryStringToParams( this._core, this._txtMeasureParams.Text, out error );
                this._checker.Check( this._txtMeasureParams, dMetParams != null, error ?? "error" );
            }
            else
            {
                this._checker.Check( this._ecbMeasure.ComboBox,false, "Specify a distance measure" );
                dMetParams = null;
            }

            // Obs source
            src = this._ecbSource.SelectedItem;

            if (sel != null && sel.SupportsObservationFilters)
            {
                this._checker.Check( this._ecbSource.ComboBox, src != null, "Select a valid source" );
            }

            // Vector A
            if (sel==null || !sel.SupportsObservationFilters)
            {
                obsFilter = null;
            }
            else if (this._ecbObsFilter.HasSelection)
            {
                obsFilter = this._ecbObsFilter.SelectedItem;
            }
            else
            {
                this._checker.Check( this._ecbObsFilter.ComboBox, false, "Select a valid observation filter");
                obsFilter = default( ObsFilter );
            }

            if (this._checker.HasErrors)
            {                                  
                return null;
            }

            // Result
            ConfigurationMetric df = dMet != null ? new ConfigurationMetric() : null;
            if (df != null)
            {
                df.Args = new ArgsMetric( dMet.Id, src, dMetParams )
                {
                    OverrideDisplayName = dMet.DisplayName
                };
            }
            ArgsClusterer args = new ArgsClusterer( sel.Id, src, peakFilter, df, obsFilter, this._chkSepGroups.Checked, suppressMetric, parameters )
            {
                OverrideDisplayName = title,
                Comment = this._comment
            };                                                                          

            return args;
        }

        private void CheckAndChange(object sender, EventArgs e)
        {
            ClustererBase stat = (ClustererBase)this._ecbMethod.SelectedItem;
            MetricBase met = this._ecbMeasure.SelectedItem as MetricBase;  

            // Stat has params?
            bool paramsVisible = stat != null && stat.Parameters.HasCustomisableParams;
            this._txtParams.Enabled = paramsVisible;
            this._btnEditParameters.Enabled = paramsVisible;
            this._lblParams.Enabled = paramsVisible;
            this._lblParams.Text = paramsVisible ? stat.Parameters.ParamNames() : "Parameters";

            // Distance
            this.linkLabel1.Visible = stat !=null && !stat.SupportsDistanceMetrics;

            // Distance params
            bool distParamsVisible = met != null && met.Parameters != null && met.Parameters.HasCustomisableParams;
            this._txtMeasureParams.Enabled = distParamsVisible;
            this._btnEditDistanceParameters.Enabled = distParamsVisible;
            this._lblMeasureParams.Enabled = distParamsVisible;
            this._lblMeasureParams.Text = distParamsVisible ? met.Parameters.ParamNames() : "Parameters";

            // Input vector
            bool obsFilterVisible = stat != null && stat.SupportsObservationFilters;
            this._lblApply.Visible = obsFilterVisible;
            this._ecbSource.Enabled = obsFilterVisible;
            //_btnTrendHelp.Enabled = obsFilterVisible;                           
            this._lblAVec.Enabled = obsFilterVisible;
            this._ecbObsFilter.Enabled = obsFilterVisible;
            this._chkSepGroups.Enabled = obsFilterVisible;

            // Is OK?
            this.Check(null, null);
        }

        private void Check(object sender, EventArgs e)
        {
            var sel = this.GetSelection();
            this._txtName.Watermark = sel != null ? sel.DefaultDisplayName : Texts.default_name;
            this._btnOk.Enabled = sel != null;
        }      

        private void _btnEditParameters_Click(object sender, EventArgs e)
        {
            ClustererBase stat = (ClustererBase)this._ecbMethod.SelectedItem;
            FrmEditParameters.Show(stat, this._txtParams, this._core, this._readOnly);
        }

        private void _btnEditDistanceParameters_Click(object sender, EventArgs e)
        {
            var dMet = (MetricBase)this._ecbMeasure.SelectedItem;
            FrmEditParameters.Show(dMet, this._txtParams, this._core, this._readOnly);
        }   

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmMsgBox.ShowInfo(this, "Distance Metric Not Supported",
                               "This clustering algorithm either does not use a distance metric, or it uses its own internal metric. The metric you specify will only be used for clustering performance (e.g. silhouette width) calculations. Disabling performance evaluations may noticeably decrease cluster generation time in this case.");
        }

        private void ctlButton1_Click(object sender, EventArgs e)
        {
            var sel = this.GetSelection();

            if (sel == null)
            {
                FrmMsgBox.ShowError(this, "Parameter Optimiser", "A valid configuration must be selected prior to parameter optimisation.");
                return;
            }

            FrmActEvaluate.Show(this, this._core, sel);

            return;
        }

        private void _btnComment_Click(object sender, EventArgs e)
        {
            string newComment = FrmInputMultiLine.Show(this, this.Text, "Edit Comments", "Enter comments for your algorithm", this._comment);

            if (newComment != null)
            {
                this._comment = newComment;
            }
        }

        private void _btnObs_Click( object sender, EventArgs e )
        {
            DataSet.ForObservations( this._core ).ShowListEditor( this );
        }

        private void _btnTrend_Click( object sender, EventArgs e )
        {
            DataSet.ForTrends( this._core ).ShowListEditor( this );
        }

        private void _btnExperimentalGroups_Click( object sender, EventArgs e )
        {
            DataSet.ForGroups( this._core ).ShowListEditor( this );
        }
    }
}
