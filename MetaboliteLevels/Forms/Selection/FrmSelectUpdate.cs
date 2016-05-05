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
using MetaboliteLevels.Properties;
using MGui;
using MGui.Helpers;

namespace MetaboliteLevels.Forms.Editing
{
    public partial class FrmSelectUpdate : Form
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
            using (FrmSelectUpdate frm = new FrmSelectUpdate(level))
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

        private FrmSelectUpdate()
        {
            InitializeComponent();
        }

        private FrmSelectUpdate(EChangeLevel changeLevel)
            : this()
        {
            this._changeLevel = changeLevel;

            float resize = 2.0f;
            var sz = new Size((int)(_chkCor.Image.Size.Width * resize), (int)(_chkCor.Image.Size.Height * 2));
            _chkCor.Image = new Bitmap(_chkCor.Image, sz);
            _chkTrend.Image = new Bitmap(_chkTrend.Image, sz);
            _chkStat.Image = new Bitmap(_chkStat.Image, sz);
            _chkClus.Image = new Bitmap(_chkClus.Image, sz);

            switch (_changeLevel)
            {
                case EChangeLevel.None:
                    toolTip1.SetToolTip(_chkCor, "Update corrections");
                    toolTip1.SetToolTip(_chkTrend, "Update trends");
                    toolTip1.SetToolTip(_chkStat, "Update statistics");
                    toolTip1.SetToolTip(_chkClus, "Update clusters");

                    this.ctlTitleBar1.Text = "Update values";
                    this.ctlTitleBar1.SubText = "Select what you wish to update";
                    break;

                case EChangeLevel.Correction:
                    _chkCor.Checked = true;
                    _chkCor.Enabled = false;

                    _chkTrend.Checked = true;
                    _chkStat.Checked = true;
                    _chkClus.Checked = true;

                    toolTip1.SetToolTip(_chkCor, "Update corrections");
                    toolTip1.SetToolTip(_chkTrend, "Update trends to reflect new corrections");
                    toolTip1.SetToolTip(_chkStat, "Update statistics to reflect new trends or corrections");
                    toolTip1.SetToolTip(_chkClus, "Update clusters to reflect new trends or corrections");

                    this.ctlTitleBar1.Text = "Corrections Changed";
                    this.ctlTitleBar1.SubText = "Corrections to the data will affect other results, select what you'd like to update";
                    break;

                case EChangeLevel.Trend:
                    _chkCor.Checked = false;
                    _chkCor.Enabled = false;
                    _chkTrend.Checked = true;
                    _chkTrend.Enabled = false;

                    _chkStat.Checked = true;
                    _chkClus.Checked = true;

                    toolTip1.SetToolTip(_chkCor, "Update corrections");
                    toolTip1.SetToolTip(_chkTrend, "Update trends");
                    toolTip1.SetToolTip(_chkStat, "Update statistics to reflect new trends");
                    toolTip1.SetToolTip(_chkClus, "Update clusters to reflect new trends");

                    this.ctlTitleBar1.Text = "Trends Changed";
                    this.ctlTitleBar1.SubText = "Corrections to the trends will affect other results, select what you'd like to update";
                    break;

                default:
                    throw new SwitchException(_changeLevel);
            }
        }

        private void _chkClus_CheckedChanged(object senderr, EventArgs e)
        {
            CheckBox sender = (CheckBox)senderr;

            sender.Font = new Font(sender.Font, sender.Checked ? FontStyle.Regular : FontStyle.Strikeout);

            if (sender.Tag == null)
            {
                sender.Tag = new Tuple<Image, Image>(UiControls.Inset(sender.Image, Resources.OverlayYes), UiControls.Inset(sender.Image, Resources.OverlayNo));
            }

            var img = (Tuple<Image, Image>)sender.Tag;

            sender.Image = sender.Checked ? img.Item1 : img.Item2;

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
