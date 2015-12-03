using System.Drawing;
using System.Windows.Forms;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Utilities;
using System;
using MetaboliteLevels.Settings;

namespace MetaboliteLevels.Forms.Generic
{
    public partial class FrmMsgBox : Form
    {
        public class ButtonSet
        {
            public string Text;
            public Image Image;
            public DialogResult Result;

            public ButtonSet(string text, Image image, DialogResult result)
            {
                this.Text = text;
                this.Image = image;
                this.Result = result;
            }
        }

        internal static bool ShowOkCancel(Form owner, string title, string message, Image image = null)
        {
            if (image == null)
            {
                image = Resources.MsgHelp;
            }

            ButtonSet[] buttons = { new ButtonSet("OK", Resources.MnuAccept, DialogResult.Yes)
                                  , new ButtonSet("Cancel", Resources.MnuBack, DialogResult.No) };

            return Show(owner, title, null, message, image, buttons, 0, -1) == DialogResult.Yes;
        }

        public static DialogResult ShowYesCancel(Form owner, string title, string message, Image image = null)
        {
            if (image == null)
            {
                image = Resources.MsgHelp;
            }

            ButtonSet[] buttons = { new ButtonSet("Yes", Resources.MnuAccept, DialogResult.Yes)
                                  , new ButtonSet("No", Resources.MnuCancel, DialogResult.No)
                                  , new ButtonSet("Cancel", Resources.MnuBack, DialogResult.Cancel) };

            return Show(owner, title, null, message, image, buttons, 0, -1);
        }

        public static bool ShowYesNo(Form owner, string title, string subTitle, string message, Image image = null)
        {
            if (image == null)
            {
                image = Resources.MsgHelp;
            }

            ButtonSet[] buttons = { new ButtonSet("Yes", Resources.MnuAccept, DialogResult.Yes)
                                  , new ButtonSet("No", Resources.MnuCancel, DialogResult.No) };

            return Show(owner, title, subTitle, message, image, buttons, 0, -1) == DialogResult.Yes;
        }

        public static bool ShowYesNo(Form owner, string title, string message, Image image = null)
        {
            return ShowYesNo(owner, title, null, message, image);
        }

        public static void ShowCompleted(Form owner, string title, string message)
        {
            ButtonSet[] buttons = { new ButtonSet("OK", Resources.MnuAccept, DialogResult.OK) };
            Show(owner, title, null, message, Resources.MsgAccept, buttons, 0, 0);
        }

        public static void ShowHelp(Form owner, string title, string message)
        {
            ButtonSet[] buttons = { new ButtonSet("OK", Resources.MnuAccept, DialogResult.OK) };
            Show(owner, title, null, message, Resources.MsgHelp, buttons, 0, 0);
        }

        public static void ShowInfo(Form owner, string title, string message)
        {
            ButtonSet[] buttons = { new ButtonSet("OK", Resources.MnuAccept, DialogResult.OK) };
            Show(owner, title, null, message, Resources.MsgInfo, buttons, 0, 0);
        }

        public static void ShowWarning(Form owner, string title, string message)
        {
            ButtonSet[] buttons = { new ButtonSet("OK", Resources.MnuAccept, DialogResult.OK) };
            Show(owner, title, null, message, Resources.MsgWarning, buttons, 0, 0);
        }

        public static void ShowError(Form owner, string title, string message)
        {
            ButtonSet[] buttons = { new ButtonSet("OK", Resources.MnuAccept, DialogResult.OK) };
            Show(owner, title, null, message, Resources.MsgError, buttons, 0, 0);
        }

        public static void ShowError(Form owner, string message)
        {
            ButtonSet[] buttons = { new ButtonSet("OK", Resources.MnuAccept, DialogResult.OK) };
            Show(owner, "Error", null, message, Resources.MsgError, buttons, 0, 0);
        }

        internal static void ShowError(Form owner, string subTitle, Exception ex)
        {
            ButtonSet[] buttons = { new ButtonSet("Abort", Resources.MnuAccept, DialogResult.No) ,
                                      new ButtonSet("Details", Resources.MnuAccept, DialogResult.Yes)  };

            if (Show(owner, "Error", subTitle, ex.Message, Resources.MsgError, buttons, 0, 0) == DialogResult.Yes)
            {
                FrmInputLarge.ShowFixed(owner, "Error", "Error Details", ex.Message, ex.ToString());
            }
        }

        internal static void ShowError(Form owner, Exception ex)
        {
            ShowError(owner, "An error occured", ex);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="title">Title (or NULL for no title)</param>
        /// <param name="subTitle">Subtitle (or NULL for no subtitle)</param>
        /// <param name="message">Message (or NULL for no message)</param>
        /// <param name="image">Image (or NULL for no image)</param>
        /// <param name="buttons">Buttons (only non-NULL entries used, use NULL or an empty array for just an OK button)</param>
        /// <param name="defaultButton">AcceptButton index (or -1 to automatically select the first button)</param>
        /// <param name="cancelButton">CancelButton index (or -1 to automatically select the last button)</param>
        private FrmMsgBox(string title, string subTitle, string message, Image image, ButtonSet[] buttons, int defaultButton, int cancelButton, bool notAgainVisible)
            : this()
        {
            this.ctlTitleBar1.Text = title;
            this.ctlTitleBar1.SubText = subTitle;
            this.label1.Text = message;
            this.pictureBox1.Image = image;
            this._chkNotAgain.Visible = notAgainVisible;

            int lastId = -1;

            if (buttons != null)
            {
                for (int i = 0; i < buttons.Length; i++)
                {
                    ButtonSet s = buttons[i];

                    if (s != null)
                    {
                        Button b = this.GetButton(i);

                        b.Text = s.Text;
                        b.Image = s.Image;
                        b.DialogResult = s.Result;
                        b.Visible = true;

                        lastId = i;
                    }
                }
            }

            if (lastId == -1)
            {
                Button b = this.GetButton(0);

                b.Text = "OK";
                b.Image = Resources.MnuAccept;
                b.DialogResult = DialogResult.OK;
                b.Visible = true;

                lastId = 0;
            }

            this.AcceptButton = this.GetButton(defaultButton >= 0 ? defaultButton : 0);
            this.CancelButton = this.GetButton(cancelButton >= 0 ? cancelButton : lastId);

            UiControls.CompensateForVisualStyles(this);
        }

        public static DialogResult Show2(Form owner, string title, string subTitle = null, string message = null, Image image = null, ButtonSet button1 = null, ButtonSet button2 = null, ButtonSet button3 = null, int defaultButton = -1, int cancelButton = -1, string dontShowAgainId = null)
        {
            ButtonSet[] buttons = new ButtonSet[]
            {
                button1,
                button2,
                button3
            };

            return Show(owner, title, subTitle, message, image, buttons, defaultButton, cancelButton, dontShowAgainId);
        }

        public static DialogResult Show(Form owner, string title, string subTitle = null, string message = null, Image image = null, ButtonSet[] buttons = null, int defaultButton = -1, int cancelButton = -1, string dontShowAgainId = null)
        {
            if (dontShowAgainId != null)
            {
                if (MainSettings.Instance.DoNotShowAgain.Contains("FrmMsgBox." + dontShowAgainId))
                {
                    return DialogResult.OK;
                }
            }

            using (FrmMsgBox frm = new FrmMsgBox(title, subTitle, message, image, buttons, defaultButton, cancelButton, dontShowAgainId != null))
            {
                if (frm._chkNotAgain.Checked)
                {
                    MainSettings.Instance.DoNotShowAgain.Add("FrmMsgBox." + dontShowAgainId);
                    MainSettings.Instance.Save();
                }

                return UiControls.ShowWithDim(owner, frm);
            }
        }

        private Button GetButton(int d)
        {
            switch (d)
            {
                case -1:
                    return null;

                case 0:
                    return _btn1;

                case 1:
                    return _btn2;

                case 2:
                    return _btn3;

                default:
                    throw new SwitchException(d);
            }
        }

        public FrmMsgBox()
        {
            InitializeComponent();
            UiControls.SetIcon(this);
        }
    }
}
