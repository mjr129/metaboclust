using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Settings;
using MetaboliteLevels.Forms;
using MetaboliteLevels.Algorithms;
using MetaboliteLevels.Algorithms.Statistics;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Algorithms.Statistics.Metrics;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Forms.Editing;
using MetaboliteLevels.Algorithms.Statistics.Arguments;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Controls
{
    /// <summary>
    /// Control to prompt user to select statistics.
    /// </summary>
    public partial class CtlStatistics : UserControl
    {
        Core _core;
        private EditableComboBox<PeakFilter> _ecbFilter;

        internal class Result
        {
            public readonly PeakFilter PeakFilter;
            public readonly ConfigurationMetric DistanceMetric;

            public Result(PeakFilter insigId, ConfigurationMetric distanceMetric)
            {
                this.PeakFilter = insigId;
                this.DistanceMetric = distanceMetric;
            }
        }

        public CtlStatistics()
        {
            InitializeComponent();
        }

        internal void Bind(Core core)
        {
            UiControls.Assert(_core == null, "Already bound.");

            _core = core;

            _ecbFilter = EditableComboBox.ForPeakFilter(_lstFilter, _btnFilter, core);

            this.comboBox1.Items.AddRange(Algo.Instance.Metrics.ToArray());
            this.comboBox1.SelectedItem = Algo.Instance.Metrics.Get(Algo.ID_METRIC_EUCLIDEAN);
            this._txtParams.Text = string.Empty;
        }

        internal Result Retrieve()
        {
            var dm = ((MetricBase)(this.comboBox1.SelectedItem));
            object[] parameters;

            if (dm.GetParams().HasCustomisableParams)
            {
                if (!dm.GetParams().TryStringToParams(_core, _txtParams.Text, out parameters))
                {
                    return null;
                }
            }
            else
            {
                parameters = null;
            }

            if (!_ecbFilter.HasSelection)
            {
                return null;
            }

            ArgsMetric args = new ArgsMetric(parameters);

            return new Result(_ecbFilter.SelectedItem, new ConfigurationMetric(null, null, dm.Id, args));
        }

        internal bool IsValid()
        {
            return this._ecbFilter.HasSelection && _core != null;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dm = ((MetricBase)(this.comboBox1.SelectedItem));

            if (dm != null && dm.GetParams().HasCustomisableParams)
            {
                label1.Text = dm.GetParams().ParamNames();
                _txtParams.Text = "";
                tableLayoutPanel1.Visible = true;
                _btnEditParameters.Visible = true;
            }
            else
            {
                tableLayoutPanel1.Visible = false;
                _btnEditParameters.Visible = false;
            }
        }

        private void _btnEditParameters_Click(object sender, EventArgs e)
        {
            var dm = ((MetricBase)(this.comboBox1.SelectedItem));
            FrmEditParameters.Show(dm, _txtParams, _core);
        }
    }
}
