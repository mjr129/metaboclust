using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Properties;
using MGui.Helpers;

namespace MetaboliteLevels.Forms.Selection
{
    /// <summary>
    /// Buttons for use with <see cref="FrmMsgBox"/>.
    /// </summary>
    public class MsgBoxButton
    {                 
        public string Text;
        public Image Image;
        public DialogResult Result;

        public MsgBoxButton( string text, Image image, DialogResult result )
        {
            this.Text = text;
            this.Image = image;
            this.Result = result;
        }

        public MsgBoxButton( DialogResult result )
            : this( result.ToUiString(), result )
        {
            // NA
        }

        public MsgBoxButton( string text, DialogResult result )
        {
            this.Result = result;
            this.Text = text;

            switch (result)
            {
                case DialogResult.OK:
                    this.Image = Resources.MnuAccept;
                    break;

                case DialogResult.Cancel:
                    this.Image = Resources.MnuCancel;
                    break;

                case DialogResult.Abort:
                    this.Image = Resources.MnuCancel;
                    break;

                case DialogResult.Retry:
                    this.Image = Resources.MnuRefresh;
                    break;

                case DialogResult.Ignore:
                    this.Image = Resources.MnuCancel;
                    break;

                case DialogResult.Yes:
                    this.Image = Resources.MnuAccept;
                    break;

                case DialogResult.No:
                    this.Image = Resources.MnuCancel;
                    break;

                case DialogResult.None:
                default:
                    throw new ArgumentOutOfRangeException( nameof( result ), result, null );
            }
        }
    }
}
