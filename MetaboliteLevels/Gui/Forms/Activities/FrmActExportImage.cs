using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Gui.Controls.Charts;
using MetaboliteLevels.Gui.Forms.Editing;
using MetaboliteLevels.Gui.Forms.Selection;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Gui.Forms.Activities
{
    internal partial class FrmActExportImage : Form
    {
        private readonly Core _core;
        private readonly ChartHelper _chart;

        public static bool Show(Form owner, Core core, ChartHelper chart, string title, string caption)
        {
            using (FrmActExportImage frm = new FrmActExportImage(core, chart, title, caption))
            {
                return (UiControls.ShowWithDim(owner, frm) == DialogResult.OK);
            }
        }

        public FrmActExportImage(Core core, ChartHelper chart, string title, string caption)
        {
            this.InitializeComponent();
            UiControls.SetIcon(this);

            this._core = core;
            this._chart = chart;
            this._txtTitle.Text = title;
            this._txtCaption.Text = caption;
            this._numX.Value = chart.Chart.ClientSize.Width;
            this._numY.Value = chart.Chart.ClientSize.Height;

            // UiControls.CompensateForVisualStyles(this);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            this.RenderPreview();
        }

        private void _btnPreferences_Click(object sender, EventArgs e)
        {
            if (FrmEditCoreOptions.Show(this, this._core, false))
            {
                this.RenderPreview();
            }
        }

        private void RenderPreview()
        {
            if (this._chkPreview.Checked)
            {
                Bitmap bitmap = this.RenderBitmap();

                this._imgPreview.Image = bitmap;
                this._lblPreview.Visible = true;
                this._imgPreview.Visible = true;
            }
            else
            {
                this._lblPreview.Visible = false;
                this._imgPreview.Visible = false;
            }
        }

        private Bitmap RenderBitmap()
        {
            Bitmap bitmap = this._chart.CreateBitmap((int)this._numX.Value, (int)this._numY.Value);

            if (this._chkWatermark.Checked)
            {
                UiControls.DrawWatermark(bitmap, this._core, this._txtTitle.Text);
            }

            if (this._chkCaption.Checked)
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    SizeF size = g.MeasureString(this._txtCaption.Text, this.Font);
                    g.DrawString(this._txtCaption.Text, this.Font, Brushes.Black, 0, bitmap.Height - size.Height);
                }
            }

            return bitmap;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void _chkClipboard_CheckedChanged(object sender, EventArgs e)
        {
            this._lblFileName.Visible = this._txtFileName.Visible = !this._chkClipboard.Checked;
        }

        private void ctlButton1_Click(object sender, EventArgs e)
        {
            using (Bitmap bitmap = this.RenderBitmap())
            {
                if (this._chkClipboard.Checked)
                {
                    Clipboard.SetImage(bitmap);
                }
                else
                {
                    string fileName;

                    if (string.IsNullOrWhiteSpace(this._txtFileName.Text))
                    {
                        fileName = UiControls.BrowseForFile(this, null, UiControls.EFileExtension.PngOrEmf, FileDialogMode.SaveAs, UiControls.EInitialFolder.SavedImages);

                        if (fileName == null)
                        {
                            return;
                        }
                    }
                    else
                    {
                        fileName = this._txtFileName.Text;

                        if (!fileName.Contains("\\"))
                        {
                            fileName = UiControls.GetNewFile(Path.Combine(UiControls.GetOrCreateFixedFolder(UiControls.EInitialFolder.SavedImages), fileName));
                        }
                    }

                    try
                    {
                        bitmap.Save(fileName);

                        this.DialogResult = DialogResult.OK;
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
            Clipboard.SetText(this._txtCaption.Text);
        }

        private void _btnCopyTitle_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this._txtTitle.Text);
        }
    }
}
