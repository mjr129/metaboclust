using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Gui.Controls;
using MetaboliteLevels.Gui.Forms.Text;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Utilities;
using MGui.Datatypes;
using MGui.Helpers;

namespace MetaboliteLevels.Gui.Forms.Selection
{
    internal partial class FrmMsgBox : Form
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
            HelpSideBar,
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

            MsgBoxButton[] buttons = { new MsgBoxButton("OK", Resources.MnuAccept, DialogResult.OK)
                                  , new MsgBoxButton("Cancel", Resources.MnuBack, DialogResult.Cancel) };

            return Show(new MsgBox( owner, title, null, message, image, buttons, null, null, dontShowAgainId, DialogResult.OK) )== DialogResult.OK;
        }

        public static DialogResult ShowYesCancel(Form owner, string title, string message, Image image = null)
        {
            if (image == null)
            {
                image = Resources.MsgHelp;
            }

            MsgBoxButton[] buttons = { new MsgBoxButton("Yes", Resources.MnuAccept, DialogResult.Yes)
                                  , new MsgBoxButton("No", Resources.MnuCancel, DialogResult.No)
                                  , new MsgBoxButton("Cancel", Resources.MnuBack, DialogResult.Cancel) };

            return Show(owner, title, null, message, image, buttons);
        }

        public static bool ShowYesNo(Form owner, string title, string subTitle, string message, Image image = null)
        {
            if (image == null)
            {
                image = Resources.MsgHelp;
            }

            MsgBoxButton[] buttons = { new MsgBoxButton("Yes", Resources.MnuAccept, DialogResult.Yes)
                                  , new MsgBoxButton("No", Resources.MnuCancel, DialogResult.No) };

            return Show(owner, title, subTitle, message, image, buttons) == DialogResult.Yes;
        }

        public static bool ShowYesNo(Form owner, string title, string message, Image image = null)
        {
            return ShowYesNo(owner, title, null, message, image);
        }

        public static void ShowCompleted(Form owner, string title, string message)
        {
            MsgBoxButton[] buttons = { new MsgBoxButton("OK", Resources.MnuAccept, DialogResult.OK) };
            Show(owner, title, null, message, Resources.MsgAccept, buttons);
        }

        public static void ShowHelp(Form owner, string title, string message)
        {
            MsgBoxButton[] buttons = { new MsgBoxButton("OK", Resources.MnuAccept, DialogResult.OK) };
            Show(owner, title, null, message, Resources.MsgHelp, buttons);
        }

        public static void ShowInfo(Form owner, string title, string message)
        {
            MsgBoxButton[] buttons = { new MsgBoxButton("OK", Resources.MnuAccept, DialogResult.OK) };
            Show(owner, title, null, message, Resources.MsgInfo, buttons);
        }

        public static void ShowInfo(Form owner, string title, string message, EDontShowAgainId dontShowAgainId )
        {
            MsgBoxButton[] buttons = { new MsgBoxButton("OK", Resources.MnuAccept, DialogResult.OK) };
            Show( new MsgBox( owner, title, null, message, Resources.MsgInfo, buttons, null, null, dontShowAgainId, null ) );
        }

        public static void ShowWarning(Form owner, string title, string message, EDontShowAgainId dontShowAgainId = EDontShowAgainId.None )
        {
            MsgBoxButton[] buttons = { new MsgBoxButton("OK", Resources.MnuAccept, DialogResult.OK) };
            Show(owner, title, null, message, Resources.MsgWarning, buttons, dontShowAgainId, null);
        }

        public static void ShowError(Form owner, string title, string message)
        {
            MsgBoxButton[] buttons = { new MsgBoxButton("OK", Resources.MnuAccept, DialogResult.OK) };
            Show(owner, title, null, message, Resources.MsgError, buttons);
        }

        public static void ShowError(Form owner, string message)
        {
            MsgBoxButton[] buttons = { new MsgBoxButton("OK", Resources.MnuAccept, DialogResult.OK) };
            Show(owner, "Error", null, message, Resources.MsgError, buttons);
        }

        internal static void ShowError( Form owner, Exception ex )
        {
            ShowError( owner, "An error occured", ex );
        }

        internal static void ShowError(Form owner, string subTitle, Exception ex)
        {
            MsgBoxButton[] buttons = { new MsgBoxButton("Abort", Resources.MnuAccept, DialogResult.No) ,
                                    new MsgBoxButton("Details", Resources.MnuAccept, DialogResult.Yes)  };

            if (Show(owner, "Error", subTitle, ex.Message, Resources.MsgError, buttons) == DialogResult.Yes)
            {
                FrmInputMultiLine.ShowFixed(owner, "Error", "Error Details", ex.Message, ex.ToString());
            }
        }

        List<CtlButton> _buttons = new List<CtlButton>();
        private readonly DialogResult? _notAgainConstraint;

        internal static void Show( Form owner, ELogLevel level, string message )
        {                   
            switch (level)
            {
                case ELogLevel.Information:
                    FrmMsgBox.ShowInfo( owner, level.ToUiString(), message );
                    break;

                case ELogLevel.Error:
                    FrmMsgBox.ShowError( owner, level.ToUiString(), message );
                    break;

                case ELogLevel.Warning:
                    FrmMsgBox.ShowWarning( owner, level.ToUiString(), message );
                    break;
            }
        }

        /// <summary>
        /// Constructor. See Show method for parameter descriptions.
        /// </summary>                                                                                           
        private FrmMsgBox(MsgBox e)
            : this()
        {
            this.ctlTitleBar1.Text = e.Title;
            this.ctlTitleBar1.SubText = e.SubTitle;
            this.ctlTitleBar1.HelpText = e.HelpText;
            this.label1.Text = e.Message;
            this.pictureBox1.Image = e.Image;
            this._chkNotAgain.Visible = e.DontShowAgainId != null;
            this._notAgainConstraint = e.DontShowAgainValue;

            if (e.Buttons == null)
            {
                e.Buttons = new MsgBoxButton[] { new MsgBoxButton( DialogResult.OK ) };
            }

            foreach (MsgBoxButton s in e.Buttons)
            {
                CtlButton b = new CtlButton();

                b.Text = s.Text;
                b.Image = s.Image;
                b.DialogResult = s.Result;
                b.Visible = true;
                b.UseDefaultSize = true;

                this.flowLayoutPanel1.Controls.Add(b);

                this._buttons.Add(b);
            }

            if (e.DefaultButton.HasValue)
            {
                this.AcceptButton = this._buttons.First(z => z.DialogResult == e.DefaultButton.Value);
            }
            else
            {
                this.AcceptButton = this._buttons[0];
            }

            if (e.CancelButton.HasValue)
            {
                this.CancelButton = this._buttons.First(z => z.DialogResult == e.CancelButton.Value);
            }
            else
            {
                this.CancelButton = this._buttons[this._buttons.Count - 1];
            }                                             
        }

        /// <summary>
        /// Gets the messagebox icon associated with a particular log level.
        /// </summary>                                                
        public static Image GetIcon( ELogLevel level )
        {
            switch (level)
            {
                case ELogLevel.Information: return Resources.MsgInfo;
                case ELogLevel.Error: return Resources.MsgError;     
                case ELogLevel.Warning: return Resources.MsgWarning;
                default: throw new SwitchException( "level", level );
            }
        }

        public static DialogResult Show(Form owner, string title, string subTitle, string message, Image image, IEnumerable<MsgBoxButton> buttons)
        {
            return Show( new MsgBox( owner, title, subTitle, message, image, buttons, null, null, EDontShowAgainId.None, null ) );
        }
        public static DialogResult Show(Form owner, string title, string subTitle, string message, Image image, IEnumerable<MsgBoxButton> buttons, EDontShowAgainId dontShowAgainId, DialogResult? dontShowAgainValue)
        {
            return Show( new MsgBox( owner, title, subTitle, message, image, buttons, null, null, dontShowAgainId, dontShowAgainValue ) );
        }

        public static DialogResult Show(Form owner, string title, string subTitle, string message, Image image, IEnumerable<MsgBoxButton> buttons, DialogResult? defaultButton, DialogResult? cancelButton)
        {
            return Show( new MsgBox( owner, title, subTitle, message, image, buttons, defaultButton, cancelButton, EDontShowAgainId.None, null ) );
        }

        /// <summary>
        /// Shows something
        /// </summary>       
        /// <returns>Dialog result of selected button, or <see cref="MsgBox.DontShowAgainValue"/> if previously set to not show again.</returns>
        public static DialogResult Show(MsgBox e)
        {
            string key = null;

            if (e.DontShowAgainId != null)
            {
                int v;
                key = GetKey( e.DontShowAgainId );

                if (MainSettings.Instance.DoNotShowAgain.TryGetValue(key, out v))
                {
                    // User said "don't show again"

                    if (e.DontShowAgainValue.HasValue)
                    {
                        // Always use the same result
                        return e.DontShowAgainValue.Value;
                    }
                    else
                    {
                        // Use the user's last selection
                        return (DialogResult)v;
                    }
                }
            }

            using (FrmMsgBox frm = new FrmMsgBox(e))
            {
                DialogResult result = UiControls.ShowWithDim(e.Owner, frm);

                if (frm._chkNotAgain.Checked)
                {
                    MainSettings.Instance.DoNotShowAgain.Add(key, (int)result);
                    MainSettings.Instance.Save( MainSettings.EFlags.DoNotShowAgain);
                }

                return result;
            }
        }

        private static string GetKey( string dontShowAgainId )
        {
            return "FrmMsgBox." + dontShowAgainId;
        }

        public FrmMsgBox()
        {
            this.InitializeComponent();
            UiControls.SetIcon(this);
        }

        private void _chkNotAgain_CheckedChanged(object sender, EventArgs e)
        {
            if (this._notAgainConstraint.HasValue)
            {
                CtlButton firstButton = null;
                int enabledCount = 0;

                foreach (CtlButton button in this._buttons)
                {
                    button.Enabled = (button.DialogResult == this._notAgainConstraint.Value) || (!this._chkNotAgain.Checked);

                    if (button.Enabled)
                    {
                        firstButton = button;
                        enabledCount++;
                    }
                }

                if (this._chkNotAgain.Checked && enabledCount == 1)
                {
                    firstButton.PerformClick();
                }
            }
            else
            {
                if (this._chkNotAgain.Checked && this._buttons.Count == 1)
                {
                    this._buttons[0].PerformClick();
                }
            }
        }

        internal static void ShowHint( Form owner, string text, EDontShowAgainId id, Image image = null )
        {
            if (image == null)
            {
                image = Resources.MsgHelp;
            }
                                                                             
            FrmMsgBox.Show( owner,
                            "Hint",
                            null,
                            text,
                            image,
                            new[] { new MsgBoxButton( DialogResult.OK ) },
                            id,
                            DialogResult.OK );
        }
    }

    class MsgBox
    {
        public IWin32Window Owner;
        public string SubTitle;
        public string Message;
        public IEnumerable<MsgBoxButton> Buttons;
        public DialogResult? DefaultButton;
        public DialogResult? CancelButton;
        public string DontShowAgainId;
        public DialogResult? DontShowAgainValue;
        public string HelpText;
        public ELogLevel Level;
        private string _title;
        private Image _image;

        public Image Image
        {
            get { return this._image ?? FrmMsgBox.GetIcon( this.Level ); }
            set { this._image = value; }
        }

        public string Title
        {
            get { return this._title ?? this.Level.ToUiString(); }
            set { this._title = value; }
        }     

        public MsgBox( IWin32Window owner, string title, string subTitle, string message, Image image, IEnumerable<MsgBoxButton> buttons, DialogResult? defaultButton, DialogResult? cancelButton, string dontShowAgainId, DialogResult? dontShowAgainValue )
        {
            this.Owner = owner;
            this.Title = title;
            this.SubTitle = subTitle;
            this.Message = message;
            this.Image = image;
            this.Buttons = buttons;
            this.DefaultButton = defaultButton;
            this.CancelButton = cancelButton;
            this.DontShowAgainId = dontShowAgainId;
            this.DontShowAgainValue = dontShowAgainValue;
        }

        public MsgBox( IWin32Window owner, string title, string subTitle, string message, Image image, IEnumerable<MsgBoxButton> buttons, DialogResult? defaultButton, DialogResult? cancelButton, FrmMsgBox.EDontShowAgainId dontShowAgainId, DialogResult? dontShowAgainValue )
        {
            this.Owner = owner;
            this.Title = title;
            this.SubTitle = subTitle;
            this.Message = message;
            this.Image = image;
            this.Buttons = buttons;
            this.DefaultButton = defaultButton;
            this.CancelButton = cancelButton;
            this.DontShowAgainId = dontShowAgainId == FrmMsgBox.EDontShowAgainId.None ? null : dontShowAgainId.ToString(); ;
            this.DontShowAgainValue = dontShowAgainValue;
        }

        public MsgBox()
        {       
            // NA                                                                                            
        }

        public DialogResult Show( IWin32Window owner )
        {
            this.Owner = owner;
            return FrmMsgBox.Show( this );
        }
    }
}
