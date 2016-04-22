using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Utilities;
using static MetaboliteLevels.Utilities.UiControls;

namespace MetaboliteLevels.Forms.Editing
{
    internal partial class FrmNewAlgorithm : Form
    {
        private EInitialFolder _result;

        public static EInitialFolder Show( Form owner, params EInitialFolder[] defaults )
        {
            using (FrmNewAlgorithm frm = new FrmNewAlgorithm( defaults ))
            {
                if (frm.ShowDialog( owner ) == DialogResult.OK)
                {
                    return frm._result;
                }

                return EInitialFolder.None;
            }
        }

        private FrmNewAlgorithm( EInitialFolder[] defaults )
        {
            InitializeComponent();

            if (defaults.Length == 0)
            {
                defaults = new[] { EInitialFolder.FOLDER_CORRECTIONS, EInitialFolder.FOLDER_CLUSTERERS, EInitialFolder.FOLDER_METRICS, EInitialFolder.FOLDER_STATISTICS, EInitialFolder.FOLDER_TRENDS };
            }

            _btnCorrections.Visible = defaults.Contains( EInitialFolder.FOLDER_CORRECTIONS );
            _btnClusterers.Visible = defaults.Contains( EInitialFolder.FOLDER_CLUSTERERS );
            _btnMetrics.Visible = defaults.Contains( EInitialFolder.FOLDER_METRICS );
            _btnStatistics.Visible = defaults.Contains( EInitialFolder.FOLDER_STATISTICS );
            _btnTrend.Visible = defaults.Contains( EInitialFolder.FOLDER_TRENDS );
            _btnShowAll.Visible = defaults.Length != 5;
        }

        private void _chkCor_Click( object sender, EventArgs e )
        {
            Edit( EInitialFolder.FOLDER_CORRECTIONS );
        }

        private void _chkTrend_Click( object sender, EventArgs e )
        {
            Edit( EInitialFolder.FOLDER_TRENDS );
        }

        private void _chkStat_Click( object sender, EventArgs e )
        {
            Edit( EInitialFolder.FOLDER_STATISTICS );
        }

        private void _chkClus_Click( object sender, EventArgs e )
        {
            Edit( EInitialFolder.FOLDER_CLUSTERERS );
        }

        private void button1_Click( object sender, EventArgs e )
        {
            Edit( EInitialFolder.FOLDER_METRICS );
        }

        private void Edit( EInitialFolder folder )
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
