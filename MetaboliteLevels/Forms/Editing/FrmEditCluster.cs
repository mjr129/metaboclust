using System;
using System.Linq;
using System.Windows.Forms;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Forms.Generic;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Forms.Editing
{
    /// <summary>
    /// Edits: CLUSTER
    /// </summary>
    public partial class FrmEditCluster : Form
    {
        private readonly Core _core;
        private readonly Cluster _cluster;

        internal static bool Show(Form owner, Core core, Cluster cluster)
        {
            using (FrmEditCluster frm = new FrmEditCluster(core, cluster))
            {
                return UiControls.ShowWithDim(owner, frm) != DialogResult.Cancel;
            }
        }

        private FrmEditCluster()
        {
            InitializeComponent();
            UiControls.SetIcon(this);
            UiControls.CompensateForVisualStyles(this);
        }

        private FrmEditCluster(Core core, Cluster cluster)
            : this()
        {
            this._core = core;
            this._cluster = cluster;

            this._txtName.Text = cluster.OverrideDisplayName;
            this._txtComment.Text = cluster.Comment;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (FrmMsgBox.ShowYesNo(this, "Delete Clustering Method", _cluster.Method.ToString(), "Are you sure that you want to remove all clusters associated with this clustering method?"))
            {
                FrmWait.Show(this, "Deleting Clusters", null, ApplyDelete);

                DialogResult = DialogResult.Abort;
            }
        }

        private void ApplyDelete(ProgressReporter reportProgress)
        {
            _core.SetClusterers(_core.ActiveClusterers.Where(z => z != _cluster.Method), false, reportProgress);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _cluster.OverrideDisplayName = _txtName.Text;
            _cluster.Comment = _txtComment.Text;
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
