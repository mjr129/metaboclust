﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Controls;
using MetaboliteLevels.Data.Algorithms.Definitions.Clusterers;
using MetaboliteLevels.Data.Algorithms.Definitions.Configurations;
using MetaboliteLevels.Data.Algorithms.Definitions.Metrics;
using MetaboliteLevels.Data.Algorithms.General;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Session.Singular;
using MetaboliteLevels.Forms.Activities;
using MetaboliteLevels.Forms.Selection;
using MetaboliteLevels.Forms.Text;
using MetaboliteLevels.Resx;
using MetaboliteLevels.Types.General;
using MetaboliteLevels.Types.UI;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Forms.Editing
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
            InitializeComponent();
            UiControls.SetIcon(this);    
        }

        private FrmEditConfigurationCluster(Core core, ArgsClusterer def, bool readOnly, bool hideOptimise)
            : this()
        {
            _core = core;
            _ecbPeakFilter = DataSet.ForPeakFilter(core).CreateComboBox(_lstPeakFilter, _btnPeakFilter,  ENullItemName.All);
            _ecbObsFilter = DataSet.ForObsFilter(core).CreateComboBox(_lstObsFilter, _btnObsFilter,  ENullItemName.All);
            _ecbMethod = DataSet.ForClustererAlgorithms(core).CreateComboBox(_lstMethod, _btnNewStatistic, ENullItemName.None);
            _ecbMeasure = DataSet.ForMetricAlgorithms(core).CreateComboBox(_lstMeasure, _btnNewDistance, ENullItemName.None);
            _ecbSource = DataSet.ForMatrixProviders( core ).CreateComboBox( _lstSource, _btnSource, ENullItemName.NoNullItem );
            _cbStatistics = DataSet.ForFlagsEnum<EClustererStatistics>( _core, "Cluster Statistics" ).CreateConditionBox(_txtStatistics, _btnSetStatistics);
            _readOnly = readOnly;

            if (def != null)
            {
                // Name
                _txtName.Text = def.OverrideDisplayName;

                // Comment
                _comment = def.Comment;

                // Method
                _ecbMethod.SelectedItem =(ClustererBase) def.GetAlgorithmOrNull();

                // Params
                _txtParams.Text = AlgoParameterCollection.ParamsToReversableString(def.Parameters, core);

                // PeakFilter
                _ecbPeakFilter.SelectedItem = def.PeakFilter;

                // Distance
                _ecbMeasure.SelectedItem = def.Distance != null ? (MetricBase)def.Distance.Args.GetAlgorithmOrNull() : null;

                // Distance params
                _txtMeasureParams.Text = StringHelper.ArrayToString(def.Distance?.Args.Parameters);

                // Suppress distance
                _cbStatistics.SelectedItems = EnumHelper.SplitEnum<EClustererStatistics>(def.Statistics);

                // Input vector
                _ecbSource.SelectedItem = def.SourceProvider;

                // ObsFilter
                _ecbObsFilter.SelectedItem = def.ObsFilter;

                // Seperate groups
                _chkSepGroups.Checked = def.SplitGroups;
            }

            if (readOnly)
            {
                UiControls.MakeReadOnly(this);

                _btnParameterOptimiser.Visible = false;
                _btnComment.Enabled = true;
                ctlTitleBar1.Text = "View Clustering Algorithm";
            }
            else if (def != null)
            {
                ctlTitleBar1.Text = "Edit Clustering Algorithm";
            }
            else
            {
                ctlTitleBar1.Text = "New Clustering Algorithm";
            }

            CheckAndChange(null, null);

            // UiControls.CompensateForVisualStyles(this);

            if (hideOptimise)
            {
                _btnParameterOptimiser.Visible = false;
                _btnOk.Text = "Continue";
            }
        }

        private ArgsClusterer GetSelection()
        {
            IMatrixProvider src;
            PeakFilter peakFilter;
            ObsFilter obsFilter;
            string title;

            _checker.Clear();

            // Selection
            ClustererBase sel = (ClustererBase)this._ecbMethod.SelectedItem;

            // Title / comments
            title = string.IsNullOrWhiteSpace(_txtName.Text) ? null : _txtName.Text;

            // Parameters
            object[] parameters;

            if (sel != null)
            {
                string error;
                parameters = sel.Parameters.TryStringToParams( _core, _txtParams.Text, out error );
                _checker.Check( _txtParams, parameters != null, error ?? "error" );
            }
            else
            {
                parameters = null;
                _checker.Check( _ecbMethod.ComboBox, false, "A method is required." );
            }

            // Peak filter
            peakFilter = _ecbPeakFilter.SelectedItem;
            _checker.Check( _ecbPeakFilter.ComboBox, _ecbPeakFilter.HasSelection, "Select a valid peak filter" );

            // Suppress metric
            EClustererStatistics suppressMetric;

            if (_cbStatistics.SelectionValid)
            {
                suppressMetric = (EClustererStatistics)_cbStatistics.SelectedItems.Cast<int>().Sum();
            }
            else
            {
                _checker.Check( _cbStatistics.TextBox, false, "Select a valid set of statistics" );
                suppressMetric = default( EClustererStatistics );
            }

            // Distance metric
            MetricBase dMet;

            dMet = (MetricBase)_ecbMeasure.SelectedItem;

            // Distance metric params
            object[] dMetParams;

            if (dMet != null)
            {
                string error;
                dMetParams = dMet.Parameters.TryStringToParams( _core, _txtMeasureParams.Text, out error );
                _checker.Check( _txtMeasureParams, dMetParams != null, error ?? "error" );
            }
            else
            {
                _checker.Check( _ecbMeasure.ComboBox,false, "Specify a distance measure" );
                dMetParams = null;
            }

            // Obs source
            src = _ecbSource.SelectedItem;

            if (sel != null && sel.SupportsObservationFilters)
            {
                _checker.Check( _ecbSource.ComboBox, src != null, "Select a valid source" );
            }

            // Vector A
            if (sel==null || !sel.SupportsObservationFilters)
            {
                obsFilter = null;
            }
            else if (this._ecbObsFilter.HasSelection)
            {
                obsFilter = _ecbObsFilter.SelectedItem;
            }
            else
            {
                _checker.Check( _ecbObsFilter.ComboBox, false, "Select a valid observation filter");
                obsFilter = default( ObsFilter );
            }

            if (_checker.HasErrors)
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
            ArgsClusterer args = new ArgsClusterer( sel.Id, src, peakFilter, df, obsFilter, _chkSepGroups.Checked, suppressMetric, parameters )
            {
                OverrideDisplayName = title,
                Comment = _comment
            };                                                                          

            return args;
        }

        private void CheckAndChange(object sender, EventArgs e)
        {
            ClustererBase stat = (ClustererBase)_ecbMethod.SelectedItem;
            MetricBase met = _ecbMeasure.SelectedItem as MetricBase;  

            // Stat has params?
            bool paramsVisible = stat != null && stat.Parameters.HasCustomisableParams;
            _txtParams.Enabled = paramsVisible;
            _btnEditParameters.Enabled = paramsVisible;
            _lblParams.Enabled = paramsVisible;
            _lblParams.Text = paramsVisible ? stat.Parameters.ParamNames() : "Parameters";

            // Distance
            linkLabel1.Visible = stat !=null && !stat.SupportsDistanceMetrics;

            // Distance params
            bool distParamsVisible = met != null && met.Parameters != null && met.Parameters.HasCustomisableParams;
            _txtMeasureParams.Enabled = distParamsVisible;
            _btnEditDistanceParameters.Enabled = distParamsVisible;
            _lblMeasureParams.Enabled = distParamsVisible;
            _lblMeasureParams.Text = distParamsVisible ? met.Parameters.ParamNames() : "Parameters";

            // Input vector
            bool obsFilterVisible = stat != null && stat.SupportsObservationFilters;
            _lblApply.Visible = obsFilterVisible;
            _ecbSource.Enabled = obsFilterVisible;
            //_btnTrendHelp.Enabled = obsFilterVisible;                           
            _lblAVec.Enabled = obsFilterVisible;
            _ecbObsFilter.Enabled = obsFilterVisible;
            _chkSepGroups.Enabled = obsFilterVisible;

            // Is OK?
            Check(null, null);
        }

        private void Check(object sender, EventArgs e)
        {
            var sel = GetSelection();
            _txtName.Watermark = sel != null ? sel.DefaultDisplayName : Texts.default_name;
            _btnOk.Enabled = sel != null;
        }      

        private void _btnEditParameters_Click(object sender, EventArgs e)
        {
            ClustererBase stat = (ClustererBase)_ecbMethod.SelectedItem;
            FrmEditParameters.Show(stat, _txtParams, _core, _readOnly);
        }

        private void _btnEditDistanceParameters_Click(object sender, EventArgs e)
        {
            var dMet = (MetricBase)_ecbMeasure.SelectedItem;
            FrmEditParameters.Show(dMet, _txtParams, _core, _readOnly);
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

            FrmActEvaluate.Show(this, _core, sel);

            return;
        }

        private void _btnComment_Click(object sender, EventArgs e)
        {
            string newComment = FrmInputMultiLine.Show(this, Text, "Edit Comments", "Enter comments for your algorithm", _comment);

            if (newComment != null)
            {
                _comment = newComment;
            }
        }

        private void _btnObs_Click( object sender, EventArgs e )
        {
            DataSet.ForObservations( _core ).ShowListEditor( this );
        }

        private void _btnTrend_Click( object sender, EventArgs e )
        {
            DataSet.ForTrends( _core ).ShowListEditor( this );
        }

        private void _btnExperimentalGroups_Click( object sender, EventArgs e )
        {
            DataSet.ForGroups( _core ).ShowListEditor( this );
        }
    }
}
