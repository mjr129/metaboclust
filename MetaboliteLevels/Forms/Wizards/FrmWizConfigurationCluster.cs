using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MetaboliteLevels.Controls;
using MetaboliteLevels.Controls.Charts;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Data;
using MetaboliteLevels.Data.Algorithms.Definitions.Clusterers;
using MetaboliteLevels.Data.Algorithms.Definitions.Configurations;
using MetaboliteLevels.Data.Algorithms.Definitions.Metrics;
using MetaboliteLevels.Data.Algorithms.General;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Session.Singular;
using MetaboliteLevels.Forms.Activities;
using MetaboliteLevels.Forms.Selection;
using MetaboliteLevels.Types.General;
using MetaboliteLevels.Types.UI;

namespace MetaboliteLevels.Forms.Wizards
{
    public partial class FrmWizConfigurationCluster : Form
    {
        CtlWizard _wizard;
        private Core _core;
        private EditableComboBox<ObsFilter> _ecbFilter;
        private EditableComboBox<MetricBase> _ecbDistance;
        private EditableComboBox<PeakFilter> _ecbPeakFilter;
        private readonly ChartHelperForPeaks _chart;
        private readonly EditableComboBox<IMatrixProvider> _ecbSource;
        private EditableComboBox<Peak> _ecbSeedPeak;
        private EditableComboBox<GroupInfo> _ecbSeedGroup;

        internal static bool Show( Form owner, Core core )
        {
            using (FrmWizConfigurationCluster frm = new FrmWizConfigurationCluster( core, FrmMain.SearchForSelectedPeak( owner ) ))
            {
                return UiControls.ShowWithDim( owner, frm ) == DialogResult.OK;
            }
        }

        public FrmWizConfigurationCluster()
        {
            InitializeComponent();
            UiControls.SetIcon( this );
        }

        private FrmWizConfigurationCluster( Core core, Peak current )
            : this()
        {
            this._core = core;

            _chart = new ChartHelperForPeaks( null, core, panel1 );

            _ecbFilter = DataSet.ForObsFilter( core ).CreateComboBox( _lstFilters, _btnEditFilters, ENullItemName.RepresentingAll );
            _ecbSource = DataSet.ForMatrixProviders( core ).CreateComboBox( _lstSource, _btnSource, ENullItemName.RepresentingNone );
            _ecbSeedPeak = DataSet.ForPeaks( core ).CreateComboBox( _lstSeedPeak, _btnSeedPeak, ENullItemName.NoNullItem );
            _ecbSeedGroup = DataSet.ForGroups( core ).CreateComboBox( _lstGroups, _btnGroups, ENullItemName.NoNullItem );
            _lstGroups.Items.AddRange( NamedItem.GetRange( core.Groups, z => z.DisplayName ).ToArray() );
            _lstGroups.SelectedIndex = 0;

            _ecbPeakFilter = DataSet.ForPeakFilter( core ).CreateComboBox( _lstPeakFilter, _btnPeakFilter, ENullItemName.RepresentingAll );
            _ecbDistance = DataSet.ForMetricAlgorithms( core ).CreateComboBox( _lstDistanceMeasure, _btnDistanceMeasure, ENullItemName.NoNullItem );
            _lstDistanceMeasure.SelectedIndexChanged += _lstDistanceMeasure_SelectedIndexChanged;

            this._ecbSeedPeak.SelectedItem = current;

            _wizard = CtlWizard.BindNew( this, tabControl1, CtlWizardOptions.DEFAULT | CtlWizardOptions.DialogResultCancel | CtlWizardOptions.HandleBasicChanges );
            _wizard.OkClicked += wizard_OkClicked;
            _wizard.TitleHelpText = Resx.Manual.DKMeansPlusPlus;
            _wizard.PermitAdvance += this.ValidatePage;
            _wizard.Revalidate();    
        }

        private void _lstDistanceMeasure_SelectedIndexChanged( object sender, EventArgs e )
        {
            _txtDistanceParams.Visible = _ecbDistance.HasSelection && _ecbDistance.SelectedItem.Parameters.Count != 0;
            label10.Visible = _txtDistanceParams.Visible;
        }

        private string GetStatName( ConfigurationStatistic input )
        {
            return input.ToString();
        }      

        private bool ValidatePage( int p )
        {
            _txtStopN.Enabled = _radStopN.Checked;
            _txtStopD.Enabled = _radStopD.Checked;
            _ecbSeedGroup.Enabled = _chkClusterIndividually.Checked;

            ctlError1.Clear();

            switch (p)
            {
                case 0:
                    ctlError1.Check( _ecbSource.ComboBox, _ecbSource.HasSelection, "Select an item" );
                    break;

                case 1:
                    ctlError1.Check( _ecbFilter.ComboBox, _ecbFilter.HasSelection, "Select an item" );
                    ctlError1.Check( _chkClusterIndividually, _chkClusterIndividually.Checked || _chkClusterTogether.Checked, "Select an option" );
                    break;

                case 2:
                    string errorMessage = null;
                    ctlError1.Check( _ecbPeakFilter.ComboBox, _ecbPeakFilter.HasSelection, "Select an item" );
                    ctlError1.Check( _ecbDistance.ComboBox, _ecbDistance.HasSelection, "Select an item" );
                    object[] x = _ecbDistance.SelectedItem?.Parameters.TryStringToParams( _core, _txtDistanceParams.Text, out errorMessage );
                    ctlError1.Check( _txtDistanceParams, x != null, errorMessage );
                    break;

                case 3:
                    ctlError1.Check(_ecbSeedPeak.ComboBox, _ecbSeedPeak.HasSelection, "Select an item");
                    ctlError1.Check(_ecbSeedGroup.ComboBox, _chkClusterTogether.Checked || _ecbSeedGroup.HasSelection, "Select an item");
                    ctlError1.Check( _ecbSeedPeak.ComboBox, !_ecbSeedPeak.HasSelection || _ecbPeakFilter.SelectedItem == null || _ecbPeakFilter.SelectedItem.Test( _ecbSeedPeak.SelectedItem ), $"The selected peak {{{_ecbSeedPeak.SelectedItem}}} is excluded by the peak filter {{{_ecbPeakFilter.SelectedItem}}} and cannot be clustered." ); // Catch a common problem early
                    break;

                case 4:
                    int unused;
                    ctlError1.Check( _radStopN, _radStopN.Checked || _radStopD.Checked, "Select an item" );
                    ctlError1.Check( _txtStopN, !_radStopN.Checked || int.TryParse( _txtStopN.Text, out unused ), "Invalid" );
                    ctlError1.Check( _radStopD, !_radStopD.Checked || int.TryParse( _txtStopD.Text, out unused ), "Invalid" );
                    break;

                case 5:
                    ctlError1.Check( _radFinishK, _radFinishK.Checked || _radFinishStop.Checked, "Select an item" );
                    break;

                case 6:
                    break;

                default:
                    break;
            }

            return !ctlError1.HasErrors;
        }

        void wizard_OkClicked( object sender, EventArgs e )
        {
            // Check
            if (!_wizard.RevalidateAll())
            {
                FrmMsgBox.ShowError( this, "Not all options have been selected." );
                return;
            }

            int param1_numClusters = _radStopN.Checked ? int.Parse( _txtStopN.Text ) : int.MinValue;
            double param2_distanceLimit = _radStopD.Checked ? double.Parse( _txtStopD.Text ) : double.MinValue;
            Debug.Assert( _ecbSeedPeak.HasSelection, "Expected a seed peak to be selected" );
            WeakReference<Peak> param3_seedPeak = new WeakReference<Peak>( _ecbSeedPeak.SelectedItem );
            GroupInfo param4_seedGroup = NamedItem<GroupInfo>.Extract( _lstGroups.SelectedItem );
            int param5_doKMeans = _radFinishK.Checked ? 1 : 0;
            IMatrixProvider source = _ecbSource.SelectedItem;
            object[] parameters = { param1_numClusters, param2_distanceLimit, param3_seedPeak, param4_seedGroup, param5_doKMeans };
            string name = "DK";

            // Create a constraint that only allows overlapping timepoints
            HashSet<int> overlappingPoints = new HashSet<int>();
            var fil = _ecbFilter.SelectedItem ?? ObsFilter.Empty;
            var passed = fil.Test( source.Provide.Columns.Select( z => z.Observation ) ).Passed;
            HashSet<GroupInfo> groups = new HashSet<GroupInfo>( passed.Select( z => z.Group ) );
            bool needsExFilter = false;

            foreach (int ctp in _core.Times)
            {
                bool trueInAny = false;
                bool falseInAny = false;

                foreach (GroupInfo g in groups)
                {
                    if (passed.Any( z => z.Group == g && z.Time == ctp ))
                    {
                        trueInAny = true;
                    }
                    else
                    {
                        falseInAny = true;
                    }
                }

                if (trueInAny && !falseInAny) // i.e. true in all          TT
                {
                    overlappingPoints.Add( ctp );
                }
                else if (trueInAny) // i.e. true in one but not all        TF
                {
                    needsExFilter = true;
                }
                //else if (falseInAny) //  False in all (accptable)        FT
                // else       //     No groups                             FF
            }

            ObsFilter trueFilter;

            if (needsExFilter)
            {
                List<ObsFilter.Condition> conditions = new List<ObsFilter.Condition>
                                                       {
                                                           new ObsFilter.ConditionFilter(Filter.ELogicOperator.And, false,
                                                                                         _ecbFilter.SelectedItem, true),
                                                           new ObsFilter.ConditionTime(Filter.ELogicOperator.And, false,
                                                                                       Filter.EElementOperator.Is,
                                                                                       overlappingPoints)
                                                       };


                trueFilter = new ObsFilter( null, null, conditions );
                _core.AddObsFilter( trueFilter );
            }
            else
            {
                trueFilter = _ecbFilter.SelectedItem;
            }

            ArgsClusterer args = new ArgsClusterer(
                Algo.ID_DKMEANSPPWIZ,
                _ecbSource.SelectedItem,
                _ecbPeakFilter.SelectedItem,
                new ConfigurationMetric()
                {
                    Args = new ArgsMetric( _ecbDistance.SelectedItem.Id, _ecbSource.SelectedItem, _ecbDistance.SelectedItem.Parameters.StringToParams( _core, _txtDistanceParams.Text ) )
                },
                trueFilter,
                _chkClusterIndividually.Checked,
                EClustererStatistics.None,
                parameters )
            {
                OverrideDisplayName = name,
                Comment = "Generated using wizard"
            };

            ConfigurationClusterer config = new ConfigurationClusterer() { Args = args };

            FrmWait.Show( this, "Clustering", null, z => _core.AddClusterer( config, z ) );
            DialogResult = DialogResult.OK;
        }

        private void _lstSeedPeak_SelectedIndexChanged( object sender, EventArgs e )
        {
            _chart.Plot( new StylisedPeak( _ecbSeedPeak.SelectedItem ) );
        }
    }
}
