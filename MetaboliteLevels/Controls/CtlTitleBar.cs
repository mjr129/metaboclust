using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Design;
using MetaboliteLevels.Forms;
using MetaboliteLevels.Forms.Selection;
using MetaboliteLevels.Forms.Text;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Properties;

namespace MetaboliteLevels.Controls
{
    public partial class CtlTitleBar : UserControl
    {
        public event CancelEventHandler HelpClicked;
        private string _helpText;
        private EHelpIcon _helpIcon;
        private string _warningText;

        public enum EHelpIcon
        {
            Automatic,
            Off,
            Normal,
            ShowBar,
            HideBar
        }

        public CtlTitleBar()
        {
            InitializeComponent();
            Dock = DockStyle.Top;

            tableLayoutPanel1.BackColor = UiControls.TitleBackColour;
            tableLayoutPanel1.ForeColor = UiControls.TitleForeColour;
        }

        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad( e );
        }

        protected override void WndProc( ref Message m )
        {
            base.WndProc( ref m );

            const int WM_PAINT = 0xF;

            switch (m.Msg)
            {
                case WM_PAINT:
                    RedrawControlAsBitmap( this.Handle );
                    break;
            }
        }

        protected override void OnPaint( PaintEventArgs e )
        {
            base.OnPaint( e );

            e.Graphics.DrawLine( Pens.Red, 0, 0, 32, 32 );
        }

        void RedrawControlAsBitmap( IntPtr hwnd )
        {
            Control c = Control.FromHandle( hwnd );

            Bitmap bm = new Bitmap( c.Width, c.Height );
            c.DrawToBitmap( bm, c.ClientRectangle );

            Graphics g = c.CreateGraphics(); // c.CreateGraphics();

            g.DrawImage( bm, new Point( -1, -1 ) );

            g.DrawLine( Pens.Red, 0, 0, 32, 32 );
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {

                _lblTitle.Text = value;
                base.Text = value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [Category("Appearance")]
        public string SubText
        {
            get
            {
                return _lblSubTitle.Text;
            }
            set
            {
                _lblSubTitle.Text = value;
                _lblSubTitle.Visible = !string.IsNullOrEmpty(value);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Appearance")]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string HelpText
        {
            get
            {
                return _helpText;
            }
            set
            {
                _helpText = value;
                UpdateHelpButtonVisibility();
            }
        }

        private void UpdateHelpButtonVisibility()
        {
            switch (_helpIcon)
            {
                case EHelpIcon.Automatic:
                    _btnHelp.Visible = !string.IsNullOrEmpty( _helpText );
                    break;

                case EHelpIcon.Off:
                    _btnHelp.Visible = false;
                    break;

                default:
                    _btnHelp.Visible = true;
                    break;
            }

            
        }

        [EditorBrowsable( EditorBrowsableState.Always ), Browsable( true ), DesignerSerializationVisibility( DesignerSerializationVisibility.Visible )]
        [Category( "Appearance" )]
        [DefaultValue( EHelpIcon.Automatic)]
        public EHelpIcon HelpIcon
        {
            get
            {
                return _helpIcon;
            }
            set
            {
                if (_helpIcon == value)
                {
                    return;
                }

                _helpIcon = value;

                switch (_helpIcon)
                {
                    case EHelpIcon.Automatic:
                    case EHelpIcon.Normal:
                        _btnHelp.Image = UiControls.RecolourImage( Resources.MnuHelp );
                        break;

                    case EHelpIcon.HideBar:
                        _btnHelp.Image = UiControls.RecolourImage( Resources.MnuCancel );
                        break;            

                    case EHelpIcon.ShowBar:
                        _btnHelp.Image = UiControls.RecolourImage( Resources.MnuHelpBar );
                        break;
                }

                UpdateHelpButtonVisibility();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Appearance")]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string WarningText
        {
            get
            {
                return _warningText;
            }
            set
            {
                _warningText = value;
                _btnWarning.Visible = !string.IsNullOrEmpty(_warningText);
            }
        }

        private void _btnHelp_Click(object sender, EventArgs e)
        {
            if (HelpClicked != null)
            {
                CancelEventArgs e2 = new CancelEventArgs();

                HelpClicked(this, e2);

                if (e2.Cancel)
                {
                    return;
                }
            }                     

            if (!string.IsNullOrEmpty( _helpText ))
            {
                FrmInputMultiLine.ShowFixed( this.FindForm(), "Help", "Help", Text, _helpText );
            }
        }

        private void _btnWarning_Click(object sender, EventArgs e)
        {
            FrmMsgBox.ShowWarning(this.FindForm(), "Errors on form", _warningText);
        }

        private void _btnWarning_MouseEnter(object sender, EventArgs e)
        {
            _btnWarning.Text = _warningText;
        }

        private void _btnWarning_MouseLeave(object sender, EventArgs e)
        {
            _btnWarning.Text = string.Empty;
        }

        private void tableLayoutPanel1_Paint( object sender, PaintEventArgs e )
        {
            if (DrawHBar)
            {
                UiControls.DrawHBar( e.Graphics, tableLayoutPanel1 );
            }
        }

        [DefaultValue( true )]
        public bool DrawHBar { get; set; } = true;
    }
}
