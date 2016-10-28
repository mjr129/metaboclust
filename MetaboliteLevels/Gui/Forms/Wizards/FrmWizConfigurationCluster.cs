using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Algorithms.Definitions.Base.Misc;
using MetaboliteLevels.Data.Algorithms.Definitions.Clusterers;
using MetaboliteLevels.Data.Algorithms.Definitions.Metrics;
using MetaboliteLevels.Data.Algorithms.Definitions.Statistics;
using MetaboliteLevels.Data.Algorithms.General;
using MetaboliteLevels.Data.Database;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Session.Main;
using MetaboliteLevels.Gui.Controls;
using MetaboliteLevels.Gui.Controls.Charts;
using MetaboliteLevels.Gui.Datatypes;
using MetaboliteLevels.Gui.Forms.Activities;
using MetaboliteLevels.Gui.Forms.Selection;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Gui.Forms.Wizards
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
            this.InitializeComponent();
            UiControls.SetIcon( this );
        }

        private FrmWizConfigurationCluster( Core core, Peak current )
            : this()
        {
            this._core = core;

            this._chart = new ChartHelperForPeaks( null, core, this.panel1 );

            this._ecbFilter = DataSet.ForObsFilter( core ).CreateComboBox( this._lstFilters, this._btnEditFilters, ENullItemName.RepresentingAll );
            this._ecbSource = DataSet.ForMatrixProviders( core ).CreateComboBox( this._lstSource, this._btnSource, ENullItemName.RepresentingNone );
            this._ecbSeedPeak = DataSet.ForPeaks( core ).CreateComboBox( this._lstSeedPeak, this._btnSeedPeak, ENullItemName.NoNullItem );
            this._ecbSeedGroup = DataSet.ForGroups( core ).CreateComboBox( this._lstGroups, this._btnGroups, ENullItemName.NoNullItem );
            this._lstGroups.Items.AddRange( NamedItem.GetRange( core.Groups, z => z.DisplayName ).ToArray() );
            this._lstGroups.SelectedIndex = 0;

            this._ecbPeakFilter = DataSet.ForPeakFilter( core ).CreateComboBox( this._lstPeakFilter, this._btnPeakFilter, ENullItemName.RepresentingAll );
            this._ecbDistance = DataSet.ForMetricAlgorithms( core ).CreateComboBox( this._lstDistanceMeasure, this._btnDistanceMeasure, ENullItemName.NoNullItem );
            this._lstDistanceMeasure.SelectedIndexChanged += this._lstDistanceMeasure_SelectedIndexChanged;

            this._ecbSeedPeak.SelectedItem = current;

            this._wizard = CtlWizard.BindNew( this, this.tabControl1, CtlWizardOptions.DEFAULT | CtlWizardOptions.DialogResultCancel | CtlWizardOptions.HandleBasicChanges );
            this._wizard.OkClicked += this.wizard_OkClicked;
            this._wizard.TitleHelpText = Resx.Manual.DKMeansPlusPlus;
            this._wizard.PermitAdvance += this.ValidatePage;
            this._wizard.Revalidate();    
        }

        private void _lstDistanceMeasure_SelectedIndexChanged( object sender, EventArgs e )
        {
            this._txtDistanceParams.Visible = this._ecbDistance.HasSelection && this._ecbDistance.SelectedItem.Parameters.Count != 0;
            this.label10.Visible = this._txtDistanceParams.Visible;
        }

        private string GetStatName( ConfigurationStatistic input )
        {
            return input.ToString();
        }      

        private bool ValidatePage( int p )
        {
            this._txtStopN.Enabled = this._radStopN.Checked;
            this._txtStopD.Enabled = this._radStopD.Checked;
            this._ecbSeedGroup.Enabled = this._chkClusterIndividually.Checked;

            this.ctlError1.Clear();

            switch (p)
            {
                case 0:
                    this.ctlError1.Check( this._ecbSource.ComboBox, this._ecbSource.HasSelection, "Select an item" );
                    break;

                case 1:
                    this.ctlError1.Check( this._ecbFilter.ComboBox, this._ecbFilter.HasSelection, "Select an item" );
                    this.ctlError1.Check( this._chkClusterIndividually, this._chkClusterIndividually.Checked || this._chkClusterTogether.Checked, "Select an option" );
                    break;

                case 2:
                    string errorMessage = null;
                    this.ctlError1.Check( this._ecbPeakFilter.ComboBox, this._ecbPeakFilter.HasSelection, "Select an item" );
                    this.ctlError1.Check( this._ecbDistance.ComboBox, this._ecbDistance.HasSelection, "Select an item" );
                    object[] x = this._ecbDistance.SelectedItem?.Parameters.TryStringToParams( this._core, this._txtDistanceParams.Text, out errorMessage );
                    this.ctlError1.Check( this._txtDistanceParams, x != null, errorMessage );
                    break;

                case 3:
                    this.ctlError1.Check(this._ecbSeedPeak.ComboBox, this._ecbSeedPeak.HasSelection, "Select an item");
                    this.ctlError1.Check(this._ecbSeedGroup.ComboBox, this._chkClusterTogether.Checked || this._ecbSeedGroup.HasSelection, "Select an item");
                    this.ctlError1.Check( this._ecbSeedPeak.ComboBox, !this._ecbSeedPeak.HasSelection || this._ecbPeakFilter.SelectedItem == null || this._ecbPeakFilter.SelectedItem.Test( this._ecbSeedPeak.SelectedItem ), $"The selected peak {{{this._ecbSeedPeak.SelectedItem}}} is excluded by the peak filter {{{this._ecbPeakFilter.SelectedItem}}} and cannot be clustered." ); // Catch a common problem early
                    break;

                case 4:
                    int unused;
                    this.ctlError1.Check( this._radStopN, this._radStopN.Checked || this._radStopD.Checked, "Select an item" );
                    this.ctlError1.Check( this._txtStopN, !this._radStopN.Checked || int.TryParse( this._txtStopN.Text, out unused ), "Invalid" );
                    this.ctlError1.Check( this._radStopD, !this._radStopD.Checked || int.TryParse( this._txtStopD.Text, out unused ), "Invalid" );
                    break;

                case 5:
                    this.ctlError1.Check( this._radFinishK, this._radFinishK.Checked || this._radFinishStop.Checked, "Select an item" );
                    break;

                case 6:
                    break;

                default:
                    break;
            }

            return !this.ctlError1.HasErrors;
        }

        void wizard_OkClicked( object sender, EventArgs e )
        {
            // Check
            if (!this._wizard.RevalidateAll())
            {
                FrmMsgBox.ShowError( this, "Not all options have been selected." );
                return;
            }

            int param1_numClusters = this._radStopN.Checked ? int.Parse( this._txtStopN.Text ) : int.MinValue;
            double param2_distanceLimit = this._radStopD.Checked ? double.Parse( this._txtStopD.Text ) : double.MinValue;
            Debug.Assert( this._ecbSeedPeak.HasSelection, "Expected a seed peak to be selected" );
            WeakReference<Peak> param3_seedPeak = new WeakReference<Peak>( this._ecbSeedPeak.SelectedItem );
            GroupInfo param4_seedGroup = NamedItem<GroupInfo>.Extract( this._lstGroups.SelectedItem );
            int param5_doKMeans = this._radFinishK.Checked ? 1 : 0;
            IMatrixProvider source = this._ecbSource.SelectedItem;
            object[] parameters = { param1_numClusters, param2_distanceLimit, param3_seedPeak, param4_seedGroup, param5_doKMeans };
            string name = "DK";

            // Create a constraint that only allows overlapping timepoints
            HashSet<int> overlappingPoints = new HashSet<int>();
            var fil = this._ecbFilter.SelectedItem ?? ObsFilter.Empty;
            var passed = fil.Test( source.Provide.Columns.Select( z => z.Observation ) ).Passed;
            HashSet<GroupInfo> groups = new HashSet<GroupInfo>( passed.Select( z => z.Group ) );
            bool needsExFilter = false;

            foreach (int ctp in this._core.Times)
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
                                                                                         this._ecbFilter.SelectedItem, true),
                                                           new ObsFilter.ConditionTime(Filter.ELogicOperator.And, false,
                                                                                       Filter.EElementOperator.Is,
                                                                                       overlappingPoints)
                                                       };


                trueFilter = new ObsFilter( null, null, conditions );
                this._core.AddObsFilter( trueFilter );
            }
            else
            {
                trueFilter = this._ecbFilter.SelectedItem;
            }

            ArgsClusterer args = new ArgsClusterer(
                Algo.ID_DKMEANSPPWIZ,
                this._ecbSource.SelectedItem,
                this._ecbPeakFilter.SelectedItem,
                new ConfigurationMetric()
                {
                    Args = new ArgsMetric( this._ecbDistance.SelectedItem.Id, this._ecbSource.SelectedItem, this._ecbDistance.SelectedItem.Parameters.StringToParams( this._core, this._txtDistanceParams.Text ) )
                },
                trueFilter,
                this._chkClusterIndividually.Checked,
                EClustererStatistics.None,
                parameters,
                "DK")
            {
                OverrideDisplayName = name,
                Comment = "Generated using wizard"
            };

            ConfigurationClusterer config = new ConfigurationClusterer() { Args = args };

            FrmWait.Show( this, "Clustering", null, z => this._core.AddClusterer( config, z ) );
            this.DialogResult = DialogResult.OK;
        }

        private void _lstSeedPeak_SelectedIndexChanged( object sender, EventArgs e )
        {
            this._chart.Plot( new StylisedPeak( this._ecbSeedPeak.SelectedItem ) );
        }
    }
}
