using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Forms.Selection
{
    internal partial class FrmSelectAlgorithmType : Form
    {
        private UiControls.EInitialFolder _result;

        public static UiControls.EInitialFolder Show( Form owner, params UiControls.EInitialFolder[] defaults )
        {
            using (FrmSelectAlgorithmType frm = new FrmSelectAlgorithmType( defaults ))
            {
                if (frm.ShowDialog( owner ) == DialogResult.OK)
                {
                    return frm._result;
                }

                return UiControls.EInitialFolder.None;
            }
        }

        private FrmSelectAlgorithmType( UiControls.EInitialFolder[] defaults )
        {
            InitializeComponent();

            if (defaults.Length == 0)
            {
                defaults = new[] { UiControls.EInitialFolder.FOLDER_CORRECTIONS, UiControls.EInitialFolder.FOLDER_CLUSTERERS, UiControls.EInitialFolder.FOLDER_METRICS, UiControls.EInitialFolder.FOLDER_STATISTICS, UiControls.EInitialFolder.FOLDER_TRENDS };
            }

            _btnCorrections.Visible = defaults.Contains( UiControls.EInitialFolder.FOLDER_CORRECTIONS );
            _btnClusterers.Visible = defaults.Contains( UiControls.EInitialFolder.FOLDER_CLUSTERERS );
            _btnMetrics.Visible = defaults.Contains( UiControls.EInitialFolder.FOLDER_METRICS );
            _btnStatistics.Visible = defaults.Contains( UiControls.EInitialFolder.FOLDER_STATISTICS );
            _btnTrend.Visible = defaults.Contains( UiControls.EInitialFolder.FOLDER_TRENDS );
            _btnShowAll.Visible = defaults.Length != 5;
        }

        private void _chkCor_Click( object sender, EventArgs e )
        {
            Edit( UiControls.EInitialFolder.FOLDER_CORRECTIONS );
        }

        private void _chkTrend_Click( object sender, EventArgs e )
        {
            Edit( UiControls.EInitialFolder.FOLDER_TRENDS );
        }

        private void _chkStat_Click( object sender, EventArgs e )
        {
            Edit( UiControls.EInitialFolder.FOLDER_STATISTICS );
        }

        private void _chkClus_Click( object sender, EventArgs e )
        {
            Edit( UiControls.EInitialFolder.FOLDER_CLUSTERERS );
        }

        private void button1_Click( object sender, EventArgs e )
        {
            Edit( UiControls.EInitialFolder.FOLDER_METRICS );
        }

        private void Edit( UiControls.EInitialFolder folder )
        {
            _result = folder;
            DialogResult = DialogResult.OK;
        }

        private void button2_Click( object sender, EventArgs e )
        {
            _btnCorrections.Visible = true;
            _btnStatistics.Visible = true;
            _btnTrend.Visible = true;
            _btnClusterers.Visible = true;
            _btnMetrics.Visible = true;
            _btnShowAll.Visible = false;
        }
    }
}
