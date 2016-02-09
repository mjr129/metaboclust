using MetaboliteLevels.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetaboliteLevels.Forms.Editing
{
    public partial class FrmEditUpdate : Form
    {
        private readonly EChangeLevel _changeLevel;

        [Flags]
        public enum EChangeLevel
        {
            None = 0,
            Correction = 1,
            Trend = 2,
            Statistic = 4,
            Cluster = 8,
        }

        private static EChangeLevel Show(Form owner, EChangeLevel level)
        {
            using (FrmEditUpdate frm = new FrmEditUpdate(level))
            {
                if (UiControls.ShowWithDim(owner, frm) == DialogResult.OK)
                {
                    return frm.GetResult();
                }

                return EChangeLevel.None;
            }
        }     

        public static EChangeLevel ShowTrendsChanged(Form owner)
        {
            return Show(owner, EChangeLevel.Trend);
        }

        public static EChangeLevel ShowCorrectionsChanged(Form owner)
        {
            return Show(owner, EChangeLevel.Correction);
        }

        private EChangeLevel GetResult()
        {
            EChangeLevel res = EChangeLevel.None;

            if (this._chkClus.Checked) res |= EChangeLevel.Cluster;
            if (this._chkTrend.Checked) res |= EChangeLevel.Trend;
            if (this._chkCor.Checked) res |= EChangeLevel.Correction;
            if (this._chkStat.Checked) res |= EChangeLevel.Statistic;

            return res;
        }

        private FrmEditUpdate()
        {
            InitializeComponent();
        }

        private FrmEditUpdate(EChangeLevel changeLevel)
            : this()
        {
            this._changeLevel = changeLevel;

            switch (_changeLevel)
            {
                case EChangeLevel.None:
                    _chkCor.Text = "Update corrections";
                    _chkTrend.Text = "Update trends";
                    _chkStat.Text = "Update statistics";
                    _chkClus.Text = "Update clusters";

                    this.ctlTitleBar1.Text = "Update values";
                    this.ctlTitleBar1.SubText = "Select what you wish to update";
                    break;

                case EChangeLevel.Correction:
                    _chkCor.Checked = true;
                    _chkCor.Enabled = false;

                    _chkTrend.Checked = true;
                    _chkStat.Checked = true;
                    _chkClus.Checked = true;

                    _chkCor.Text = "Update corrections";
                    _chkTrend.Text = "Update trends to reflect new corrections";
                    _chkStat.Text = "Update statistics to reflect new trends or corrections or corrections";
                    _chkClus.Text = "Update clusters to reflect new trends";

                    this.ctlTitleBar1.Text = "Corrections Changed";
                    this.ctlTitleBar1.SubText = "Changes to the data will affect values, select what you'd like to update";
                    break;

                case EChangeLevel.Trend:
                    _chkCor.Visible = false;
                    _chkTrend.Checked = true;
                    _chkTrend.Enabled = false;

                    _chkStat.Checked = true;
                    _chkClus.Checked = true;

                    _chkCor.Text = "N/A";
                    _chkTrend.Text = "Update trends";
                    _chkStat.Text = "Update statistics to reflect new trends";
                    _chkClus.Text = "Update clusters to reflect new trends";

                    this.ctlTitleBar1.Text = "Trends Changed";
                    this.ctlTitleBar1.SubText = "Changes to the trends may affect calculations that use them, select what you'd like to update";
                    break;

                default:
                    throw new SwitchException(_changeLevel);
            }
        }

        private void _chkClus_CheckedChanged(object sender, EventArgs e)
        {
            button1.Enabled = GetResult() != EChangeLevel.None;

            _chkClus.Enabled = _chkTrend.Checked;

            if (!_chkTrend.Checked)
            {
                _chkClus.Checked = false;
            }

            if (_changeLevel == EChangeLevel.Correction)
            {
                _chkStat.Enabled = _chkTrend.Checked;

                if (!_chkTrend.Checked)
                {
                    _chkStat.Checked = false;
                }
            }
        }

        internal static string GetUpdateMessage(EChangeLevel f)
        {
            List<string> list = new List<string>();

            if (f.HasFlag(EChangeLevel.Correction)) list.Add("corrections");
            if (f.HasFlag(EChangeLevel.Trend)) list.Add("trends");
            if (f.HasFlag(EChangeLevel.Statistic)) list.Add("statistics");
            if (f.HasFlag(EChangeLevel.Cluster)) list.Add("clusters");

            StringBuilder sb = new StringBuilder();

            for (int index = 0; index < list.Count; index++)
            {
                if (index == 0)
                {
                    sb.Append(list[index].ToSentence());
                }
                else
                {
                    if (index == list.Count - 1)
                    {
                        sb.Append(" and ");
                    }
                    else
                    {
                        sb.Append(", ");
                    }

                    sb.Append(list[index]);
                }
            }

            return sb.ToString().ToSentence() + " have been updated.";
        }
    }
}
