using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Forms.Editing;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Viewers.Charts;

namespace MetaboliteLevels.Forms.Generic
{
    internal partial class FrmImageExport : Form
    {
        private readonly Core _core;
        private readonly ChartHelper _chart;

        public static bool Show(Form owner, Core core, ChartHelper chart, string title, string caption)
        {
            using (FrmImageExport frm = new FrmImageExport(core, chart, title, caption))
            {
                return (UiControls.ShowWithDim(owner, frm) == DialogResult.OK);
            }
        }

        public FrmImageExport(Core core, ChartHelper chart, string title, string caption)
        {
            InitializeComponent();
            UiControls.SetIcon(this);

            _core = core;
            _chart = chart;
            _txtTitle.Text = title;
            _txtCaption.Text = caption;
            _numX.Value = chart.Chart.ClientSize.Width;
            _numY.Value = chart.Chart.ClientSize.Height;

            UiControls.CompensateForVisualStyles(this);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            RenderPreview();
        }

        private void _btnPreferences_Click(object sender, EventArgs e)
        {
            if (FrmOptions2.Show(this, _core, false))
            {
                RenderPreview();
            }
        }

        private void RenderPreview()
        {
            if (_chkPreview.Checked)
            {
                Bitmap bitmap = RenderBitmap();

                _imgPreview.Image = bitmap;
                _lblPreview.Visible = true;
                _imgPreview.Visible = true;
            }
            else
            {
                _lblPreview.Visible = false;
                _imgPreview.Visible = false;
            }
        }

        private Bitmap RenderBitmap()
        {
            Bitmap bitmap = _chart.CreateBitmap((int)this._numX.Value, (int)this._numY.Value);

            if (_chkWatermark.Checked)
            {
                UiControls.DrawWatermark(bitmap, _core, _txtTitle.Text);
            }

            if (_chkCaption.Checked)
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    SizeF size = g.MeasureString(_txtCaption.Text, Font);
                    g.DrawString(_txtCaption.Text, Font, Brushes.Black, 0, bitmap.Height - size.Height);
                }
            }

            return bitmap;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void _chkClipboard_CheckedChanged(object sender, EventArgs e)
        {
            _lblFileName.Visible = _txtFileName.Visible = !_chkClipboard.Checked;
        }

        private void ctlButton1_Click(object sender, EventArgs e)
        {
            using (Bitmap bitmap = RenderBitmap())
            {
                if (_chkClipboard.Checked)
                {
                    Clipboard.SetImage(bitmap);
                }
                else
                {
                    string fileName;

                    if (string.IsNullOrWhiteSpace(_txtFileName.Text))
                    {
                        fileName = UiControls.BrowseForFile(this, null, UiControls.EFileExtension.PngOrEmf, FileDialogMode.SaveAs, UiControls.EInitialFolder.SavedImages);

                        if (fileName == null)
                        {
                            return;
                        }
                    }
                    else
                    {
                        fileName = _txtFileName.Text;

                        if (!fileName.Contains("\\"))
                        {
                            fileName = UiControls.GetNewFile(Path.Combine(UiControls.GetOrCreateFixedFolder(UiControls.EInitialFolder.SavedImages), fileName));
                        }
                    }

                    try
                    {
                        bitmap.Save(fileName);

                        DialogResult = DialogResult.OK;
                    }
                    catch (Exception ex)
                    {
                        FrmMsgBox.ShowError(this, ex);
                    }
                }
            }
        }

        private void _btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(_txtCaption.Text);
        }

        private void _btnCopyTitle_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(_txtTitle.Text);
        }
    }
}
