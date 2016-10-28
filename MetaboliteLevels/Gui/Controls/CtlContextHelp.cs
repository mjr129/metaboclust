using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Utilities;
using MGui.Controls;
using MGui.Helpers;

namespace MetaboliteLevels.Gui.Controls
{
    public partial class CtlContextHelp : Component
    {
        private CtlTitleBar _titleBar;
        private ToolTip _toolTip;
        private CtlContextHelpInner _panel1;
        private Splitter _splitter;
        private bool _visible;
        private ErrorProvider _errorProvider;
        private string _doNotShowAgainKey;

        public CtlContextHelp()
        {
            this.InitializeComponent();
        }

        public CtlContextHelp( IContainer container )
        {
            container.Add( this );

            this.InitializeComponent();
        }

        /// <summary>
        /// Options for this control
        /// </summary>
        [Flags]
        public enum EFlags
        {
            /// <summary>
            /// No flags (default)
            /// </summary>
            None = 0,

            /// <summary>
            /// Help when clicking labels
            /// </summary>
            HelpOnClick = 1,

            /// <summary>
            /// Help when controls get focus
            /// </summary>
            HelpOnFocus = 2,

            /// <summary>
            /// Allow special FILEFORMAT markup in help text
            /// </summary>
            FileFormats = 4,

            /// <summary>
            /// Help when hovering mouse over controls
            /// </summary>
            HelpOnHover = 8,
        }

        public void Bind( Control owner, CtlTitleBar titleBar, ToolTip toolTip, EFlags flags )
        {
            // DEFAULT
            _doNotShowAgainKey = owner.Name + ".context_help";

            // ICON
            this._errorProvider = new ErrorProvider( );
            _errorProvider.Icon = Icon.FromHandle( Resources.IconBarHelp.GetHicon() );
            _errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            // SPLITTER
            this._splitter = new Splitter()
            {
                Visible = _visible,
                Width = 8,
                BackColor = System.Drawing.Color.Black,
                Dock = DockStyle.Right,
            };

            _splitter.Paint += _splitter_Paint;

            owner.Controls.Add( _splitter );

            // HELP PANEL
            this._panel1 = new CtlContextHelpInner
            {
                Dock = DockStyle.Right,
                Visible = _visible,
                HandleFileFormats = flags.Has( EFlags.FileFormats ),
                Size = new System.Drawing.Size( 192, 0 )
            };

            _panel1.CloseClicked += _panel1_CloseClicked;
            owner.Controls.Add( this._panel1 );

            // TITLE BAR
            this._titleBar = titleBar;
            if (titleBar.HelpText != null)
            {
                _panel1.DefaultText = titleBar.HelpText + "\r\n\r\n" + _panel1.DefaultText;
                titleBar.HelpText = null;
            }
            titleBar.HelpIcon = CtlTitleBar.EHelpIcon.ShowBar;
            titleBar.HelpClicked += this.TitleBar_HelpClicked;

            // TOOL TIP
            this._toolTip = toolTip;

            if (toolTip != null)
            {
                toolTip.Active = flags.Has( EFlags.HelpOnHover );
                toolTip.InitialDelay = 1;
                toolTip.Popup += this.ToolTip_Popup;
            }

            // CONTROL BINDINGS
            if (flags.Has( EFlags.HelpOnClick ) || flags.Has( EFlags.HelpOnFocus ))
            {
                foreach (Control control in FormHelper.EnumerateControls( owner ))
                {

                    if (control is CtlLabel)
                    {
                        if (flags.Has( EFlags.HelpOnClick ))
                        {
                            control.MouseEnter += this.Control_MouseEnter;
                            control.MouseLeave += this.Control_MouseLeave;
                            control.MouseDown += this.Control_Click;
                            control.Cursor = Cursors.Help;
                        }
                    }
                    else
                    {
                        if (flags.Has( EFlags.HelpOnFocus ))
                        {
                            control.GotFocus += this.Control_Click;
                            control.MouseDown += this.Control_Click;
                        }
                    }
                }
            }

            // DEFAULT VISIBILITY
            Visible = !MainSettings.Instance.DoNotShowAgain.ContainsKey( _doNotShowAgainKey );
        }

        private void _splitter_Paint( object sender, PaintEventArgs e )
        {
            CtlSplitter.Draw( e.Graphics, false, new Rectangle( 0, 0, _splitter.Width, _splitter.Height ), 0 );
        }

        private void _panel1_CloseClicked( object sender, EventArgs e )
        {
            Visible = false;
        }

        private void Control_MouseLeave( object sender, EventArgs e )
        {
            CtlLabel label = (CtlLabel)sender;

            label.LabelStyle = ELabelStyle.Normal;
            label.Font = FontHelper.RegularFont;
        }

        private void Control_MouseEnter( object sender, EventArgs e )
        {
            CtlLabel label = (CtlLabel)sender;

            label.LabelStyle = ELabelStyle.Highlight;
            label.Font = FontHelper.UnderlinedFont;
        }

        private void Control_Click( object sender, EventArgs e )
        {
            if (sender is Label)
            {
                Visible = true;
            }

            ShowHelp( (Control)sender );
        }   

        private void TitleBar_HelpClicked( object sender, CancelEventArgs e )
        {
            Visible = !Visible;
        }

        [EditorBrowsable( EditorBrowsableState.Never ), Browsable( false ), DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
        public bool Visible
        {
            get
            {
                return _visible;
            }
            set
            {
                _visible = value;

                if (_visible)
                {
                    MainSettings.Instance.DoNotShowAgain.Remove( _doNotShowAgainKey );
                }
                else
                {
                    MainSettings.Instance.DoNotShowAgain[_doNotShowAgainKey] = 1;
                }

                MainSettings.Instance.Save(MainSettings.EFlags.DoNotShowAgain);

                if (_panel1 != null)
                {
                    _panel1.Visible = value;
                    _splitter.Visible = value;
                    this._titleBar.HelpIcon = value ? CtlTitleBar.EHelpIcon.Off : CtlTitleBar.EHelpIcon.ShowBar;
                }
            }
        }

        public void ShowHelp( string text )
        {
            if (!this._panel1.Visible)
            {
                return;
            }

            this._panel1.Text = text;

            _errorProvider.Clear();
        }

        public void ShowHelp( Control control )
        {
            if (!this._panel1.Visible)
            {
                return;
            }

            string text = this._toolTip.GetToolTip( control );

            this._panel1.Text = text;

            _errorProvider.Clear();
            _errorProvider.SetIconAlignment( control, ErrorIconAlignment.MiddleRight );
            _errorProvider.SetIconPadding( control, 4 );
            _errorProvider.SetError( control, "" );
        }

        private void ToolTip_Popup( object sender, PopupEventArgs e )
        {                
            ShowHelp( e.AssociatedControl );
            e.Cancel = true;
        }
    }
}
