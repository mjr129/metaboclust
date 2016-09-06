using System.Drawing;
using System.Windows.Forms;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Utilities;
using System;
using MetaboliteLevels.Settings;
using System.Collections.Generic;
using MetaboliteLevels.Controls;
using System.Linq;

namespace MetaboliteLevels.Forms.Generic
{
    public partial class FrmMsgBox : Form
    {
        /// <summary>
        /// Don't show again IDs
        /// These are saved as TEXT, their numerical values are not used.
        /// </summary>
        public enum EDontShowAgainId
        {
            None,
            SaveBetweenEvaluations,
            ImportResultsNotice,
            ImportResultsDetailNotice,
            ChangeExperimentalGroupsId,
            EditWithResults,
            ExportDataNotice,
            PlsrMode,
            HeatmapColumnNotNumerical,
            HelpSideBar
        }

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
            return ShowOkCancel(owner, title, message, EDontShowAgainId.None, null, image);
        }

        internal static bool ShowOkCancel(Form owner, string title, string message, EDontShowAgainId dontShowAgainId, DialogResult? dontShowAgainValue, Image image = null)
        {
            if (image == null)
            {
                image = Resources.MsgHelp;
            }

            ButtonSet[] buttons = { new ButtonSet("OK", Resources.MnuAccept, DialogResult.OK)
                                  , new ButtonSet("Cancel", Resources.MnuBack, DialogResult.Cancel) };

            return Show(owner, title, null, message, image, buttons, null, null, dontShowAgainId, DialogResult.OK) == DialogResult.OK;
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

            return Show(owner, title, null, message, image, buttons);
        }

        public static bool ShowYesNo(Form owner, string title, string subTitle, string message, Image image = null)
        {
            if (image == null)
            {
                image = Resources.MsgHelp;
            }

            ButtonSet[] buttons = { new ButtonSet("Yes", Resources.MnuAccept, DialogResult.Yes)
                                  , new ButtonSet("No", Resources.MnuCancel, DialogResult.No) };

            return Show(owner, title, subTitle, message, image, buttons) == DialogResult.Yes;
        }

        public static bool ShowYesNo(Form owner, string title, string message, Image image = null)
        {
            return ShowYesNo(owner, title, null, message, image);
        }

        public static void ShowCompleted(Form owner, string title, string message)
        {
            ButtonSet[] buttons = { new ButtonSet("OK", Resources.MnuAccept, DialogResult.OK) };
            Show(owner, title, null, message, Resources.MsgAccept, buttons);
        }

        public static void ShowHelp(Form owner, string title, string message)
        {
            ButtonSet[] buttons = { new ButtonSet("OK", Resources.MnuAccept, DialogResult.OK) };
            Show(owner, title, null, message, Resources.MsgHelp, buttons);
        }

        public static void ShowInfo(Form owner, string title, string message)
        {
            ButtonSet[] buttons = { new ButtonSet("OK", Resources.MnuAccept, DialogResult.OK) };
            Show(owner, title, null, message, Resources.MsgInfo, buttons);
        }

        public static void ShowInfo(Form owner, string title, string message, EDontShowAgainId dontShowAgainId )
        {
            ButtonSet[] buttons = { new ButtonSet("OK", Resources.MnuAccept, DialogResult.OK) };
            Show(owner, title, null, message, Resources.MsgInfo, buttons, null, null, dontShowAgainId, null);
        }

        public static void ShowWarning(Form owner, string title, string message, EDontShowAgainId dontShowAgainId = EDontShowAgainId.None )
        {
            ButtonSet[] buttons = { new ButtonSet("OK", Resources.MnuAccept, DialogResult.OK) };
            Show(owner, title, null, message, Resources.MsgWarning, buttons, dontShowAgainId, null);
        }

        public static void ShowError(Form owner, string title, string message)
        {
            ButtonSet[] buttons = { new ButtonSet("OK", Resources.MnuAccept, DialogResult.OK) };
            Show(owner, title, null, message, Resources.MsgError, buttons);
        }

        public static void ShowError(Form owner, string message)
        {
            ButtonSet[] buttons = { new ButtonSet("OK", Resources.MnuAccept, DialogResult.OK) };
            Show(owner, "Error", null, message, Resources.MsgError, buttons);
        }

        internal static void ShowError( Form owner, Exception ex )
        {
            ShowError( owner, "An error occured", ex );
        }

        internal static void ShowError(Form owner, string subTitle, Exception ex)
        {
            ButtonSet[] buttons = { new ButtonSet("Abort", Resources.MnuAccept, DialogResult.No) ,
                                    new ButtonSet("Details", Resources.MnuAccept, DialogResult.Yes)  };

            if (Show(owner, "Error", subTitle, ex.Message, Resources.MsgError, buttons) == DialogResult.Yes)
            {
                FrmInputMultiLine.ShowFixed(owner, "Error", "Error Details", ex.Message, ex.ToString());
            }
        }

        List<CtlButton> _buttons = new List<CtlButton>();
        private readonly DialogResult? _notAgainConstraint;

        /// <summary>
        /// Constructor. See Show method for parameter descriptions.
        /// </summary>                                                                                           
        private FrmMsgBox(string title, string subTitle, string message, Image image, IEnumerable<ButtonSet> buttons, DialogResult? defaultButton, DialogResult? cancelButton, bool notAgainVisible, DialogResult? notAgainConstraint)
            : this()
        {
            this.ctlTitleBar1.Text = title;
            this.ctlTitleBar1.SubText = subTitle;
            this.label1.Text = message;
            this.pictureBox1.Image = image;
            this._chkNotAgain.Visible = notAgainVisible;
            this._notAgainConstraint = notAgainConstraint;

            foreach (ButtonSet s in buttons)
            {
                CtlButton b = new CtlButton();

                b.Text = s.Text;
                b.Image = s.Image;
                b.DialogResult = s.Result;
                b.Visible = true;
                b.UseDefaultSize = true;

                flowLayoutPanel1.Controls.Add(b);

                _buttons.Add(b);
            }

            if (defaultButton.HasValue)
            {
                this.AcceptButton = this._buttons.First(z => z.DialogResult == defaultButton.Value);
            }
            else
            {
                this.AcceptButton = _buttons[0];
            }

            if (cancelButton.HasValue)
            {
                this.CancelButton = this._buttons.First(z => z.DialogResult == cancelButton.Value);
            }
            else
            {
                this.CancelButton = this._buttons[this._buttons.Count - 1];
            }

            // UiControls.CompensateForVisualStyles(this);
        }

        public static DialogResult Show(Form owner, string title, string subTitle, string message, Image image, IEnumerable<ButtonSet> buttons)
        {
            return Show(owner, title, subTitle, message, image, buttons, null, null, EDontShowAgainId.None, null);
        }
        public static DialogResult Show(Form owner, string title, string subTitle, string message, Image image, IEnumerable<ButtonSet> buttons, EDontShowAgainId dontShowAgainId, DialogResult? dontShowAgainValue)
        {
            return Show(owner, title, subTitle, message, image, buttons, null, null, dontShowAgainId, dontShowAgainValue);
        }

        public static DialogResult Show(Form owner, string title, string subTitle, string message, Image image, IEnumerable<ButtonSet> buttons, DialogResult? defaultButton, DialogResult? cancelButton)
        {
            return Show(owner, title, subTitle, message, image, buttons, defaultButton, cancelButton, EDontShowAgainId.None, null);
        }

        /// <summary>
        /// Shows something
        /// </summary>
        /// <param name="owner">Owner form</param>
        /// <param name="title">Title (or NULL for no title)</param>
        /// <param name="subTitle">Subtitle (or NULL for no subtitle)</param>
        /// <param name="message">Message (or NULL for no message)</param>
        /// <param name="image">Image (or NULL for no image)</param>
        /// <param name="buttons">Buttons to display</param>
        /// <param name="defaultButton">AcceptButton index (or NULL to automatically select the first button)</param>
        /// <param name="cancelButton">CancelButton index (or NULL to automatically select the last button)</param>
        /// <param name="dontShowAgainId">ID of don't show again status (or NULL to disable option).</param>
        /// <param name="dontShowAgainValue">Which button to enable if dontShowAgainId is set (or NULL to allow any and remember the result)</param>
        /// <returns>Dialog result of selected button, or dontShowAgainValue if previously set to not show again.</returns>
        public static DialogResult Show(Form owner, string title, string subTitle, string message, Image image, IEnumerable<ButtonSet> buttons, DialogResult? defaultButton, DialogResult? cancelButton, EDontShowAgainId dontShowAgainId, DialogResult? dontShowAgainValue)
        {
            string id = null;

            if (dontShowAgainId != EDontShowAgainId.None)
            {
                id = "FrmMsgBox." + dontShowAgainId;
                int v;

                if (MainSettings.Instance.DoNotShowAgain.TryGetValue(id, out v))
                {
                    if (dontShowAgainValue.HasValue)
                    {
                        return dontShowAgainValue.Value;
                    }
                    else
                    {
                        return (DialogResult)v;
                    }
                }
            }

            using (FrmMsgBox frm = new FrmMsgBox(title, subTitle, message, image, buttons, defaultButton, cancelButton, dontShowAgainId != EDontShowAgainId.None, dontShowAgainValue))
            {
                DialogResult result = UiControls.ShowWithDim(owner, frm);

                if (frm._chkNotAgain.Checked)
                {
                    MainSettings.Instance.DoNotShowAgain.Add("FrmMsgBox." + dontShowAgainId, (int)result);
                    MainSettings.Instance.Save();
                }

                return result;
            }
        }

        public FrmMsgBox()
        {
            InitializeComponent();
            UiControls.SetIcon(this);
        }

        private void _chkNotAgain_CheckedChanged(object sender, EventArgs e)
        {
            if (_notAgainConstraint.HasValue)
            {
                CtlButton firstButton = null;
                int enabledCount = 0;

                foreach (CtlButton button in _buttons)
                {
                    button.Enabled = (button.DialogResult == _notAgainConstraint.Value) || (!_chkNotAgain.Checked);

                    if (button.Enabled)
                    {
                        firstButton = button;
                        enabledCount++;
                    }
                }

                if (_chkNotAgain.Checked && enabledCount == 1)
                {
                    firstButton.PerformClick();
                }
            }
            else
            {
                if (_chkNotAgain.Checked && _buttons.Count == 1)
                {
                    _buttons[0].PerformClick();
                }
            }
        }
    }
}
