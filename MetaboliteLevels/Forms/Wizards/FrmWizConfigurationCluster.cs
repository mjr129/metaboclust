using System;
using System.Collections.Generic;
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
        Peak _lowestPeak;
        Peak _highestPeak;
        Peak _current;
        private EditableComboBox<ObsFilter> _ecbFilter;
        private EditableComboBox<MetricBase> _ecbDistance;
        private EditableComboBox<PeakFilter> _ecbPeakFilter;
        private readonly ChartHelperForPeaks _chart;
        private readonly EditableComboBox<IMatrixProvider> _ecbSource;

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

            _ecbFilter = DataSet.ForObsFilter( core ).CreateComboBox( _lstFilters, _btnEditFilters, ENullItemName.All );
            _ecbSource = DataSet.ForMatrixProviders( core ).CreateComboBox( _lstSource, _btnSource, ENullItemName.None );
            _lstGroups.Items.AddRange( NamedItem.GetRange( core.Groups, z => z.DisplayName ).ToArray() );
            _lstGroups.SelectedIndex = 0;

            _ecbPeakFilter = DataSet.ForPeakFilter( core ).CreateComboBox( _lstPeakFilter, _btnPeakFilter, ENullItemName.All );
            _ecbDistance = DataSet.ForMetricAlgorithms( core ).CreateComboBox( _lstDistanceMeasure, _btnDistanceMeasure, ENullItemName.NoNullItem );
            _lstDistanceMeasure.SelectedIndexChanged += _lstDistanceMeasure_SelectedIndexChanged;

            this._current = current;
            _lblSeedCurrent.Text = current != null ? current.DisplayName : "None";
            _radSeedCurrent.Enabled = current != null;
            _lblSeedCurrent.Enabled = current != null;

            _lstStat.Items.AddRange( NamedItem.GetRange( core.AllStatistics.WhereEnabled(), GetStatName ).ToArray() );

            _wizard = CtlWizard.BindNew( this, tabControl1, CtlWizardOptions.DEFAULT | CtlWizardOptions.DialogResultCancel | CtlWizardOptions.HandleBasicChanges );
            _wizard.OkClicked += wizard_OkClicked;
            _wizard.TitleHelpText = Manual.DKMeansPlusPlus;
            _wizard.PermitAdvance += this.ValidatePage;
            _wizard.Revalidate();

            UpdateStatBox();

            // UiControls.CompensateForVisualStyles(this);
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

        private void UpdateStatBox()
        {
            ConfigurationStatistic stat = NamedItem<ConfigurationStatistic>.Extract( _lstStat.SelectedItem );
            bool b = stat != null;

            if (b)
            {
                _lowestPeak = _core.Peaks.Where( λ => λ.GetStatistic( stat ).IsValid() ).FindLowest( λ => λ.GetStatistic( stat ) );
                _highestPeak = _core.Peaks.Where( λ => λ.GetStatistic( stat ).IsValid() ).FindHighest( λ => λ.GetStatistic( stat ) );
                _lblSeedStudent.Text = _lowestPeak.DisplayName;
                _lblSeedPearson.Text = _highestPeak.DisplayName;
            }
            else
            {
                _lblSeedStudent.Text = "None";
                _lblSeedPearson.Text = "None";
            }

            _radSeedLowest.Enabled = b;
            _radSeedHighest.Enabled = b;
            _lblSeedStudent.Enabled = b;
            _lblSeedPearson.Enabled = b;
        }

        private void _lblSeedStudent_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e )
        {
            _radSeedLowest.Checked = true;
        }

        private void _lblSeedPearson_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e )
        {
            _radSeedHighest.Checked = true;
        }

        private void _lblSeedCurrent_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e )
        {
            _radSeedCurrent.Checked = true;
        }

        private bool ValidatePage( int p )
        {
            double tmpd;
            int tmpi;

            switch (p)
            {
                case 0:
                    return _ecbSource.HasSelection;

                case 1:
                    return _ecbFilter.HasSelection;

                case 2:
                    return ((_radSeedLowest.Checked || _radSeedHighest.Checked)
                        && (_lstStat.SelectedItem != null) || _radSeedCurrent.Checked)
                        && _lstGroups.SelectedItem != null;

                case 3:
                    return _ecbPeakFilter.HasSelection &&
                        _ecbDistance.HasSelection
                        && _ecbDistance.SelectedItem.Parameters.TryStringToParams( _core, _txtDistanceParams.Text ) != null;

                case 4:
                    return (_radStopN.Checked && int.TryParse( _txtStopN.Text, out tmpi ))
                        || (_radStopD.Checked && double.TryParse( _txtStopD.Text, out tmpd ));

                case 5:
                    return _radFinishK.Checked || _radFinishStop.Checked;

                case 6:
                    return true;

                default:
                    return false;
            }
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
            WeakReference<Peak> param3_seedPeak = new WeakReference<Peak>( _radSeedCurrent.Checked ? _current : _radSeedHighest.Checked ? _highestPeak : _lowestPeak );
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

        private void OptionChanged( object sender, EventArgs e )
        {
            if (_wizard != null)
            {
                _wizard.Revalidate();
            }

            if (_radSeedCurrent.Checked)
            {
                _chart.Plot( new StylisedPeak( _current ) );
            }
            else if (_radSeedHighest.Checked)
            {
                _chart.Plot( new StylisedPeak( _highestPeak ) );
            }
            else if (_radSeedLowest.Checked)
            {
                _chart.Plot( new StylisedPeak( _lowestPeak ) );
            }
        }

        private void _radStopN_CheckedChanged( object sender, EventArgs e )
        {
            _txtStopN.Enabled = _radStopN.Checked;
            OptionChanged( sender, e );
        }

        private void _radStopD_CheckedChanged( object sender, EventArgs e )
        {
            _txtStopD.Enabled = _radStopD.Checked;
            OptionChanged( sender, e );
        }

        private void _lstStat_SelectedIndexChanged( object sender, EventArgs e )
        {
            UpdateStatBox();
            OptionChanged( sender, e );
        }

        private void _chkClusterIndividually_CheckedChanged( object sender, EventArgs e )
        {
            _lstGroups.Enabled = _chkClusterIndividually.Checked;
        }
    }
}
