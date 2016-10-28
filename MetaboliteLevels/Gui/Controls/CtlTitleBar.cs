using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Gui.Forms.Selection;
using MetaboliteLevels.Gui.Forms.Text;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Gui.Controls
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
            HideBar,
            Close,
        }

        public CtlTitleBar()
        {
            this.InitializeComponent();
            this.Dock = DockStyle.Top;
            UpdateColours();
        }          

        private void UpdateColours()
        {
            if (HelpIcon == EHelpIcon.HideBar)
            {
                this.tableLayoutPanel1.BackColor = Color.FromKnownColor( KnownColor.Info );
                this.tableLayoutPanel1.ForeColor = Color.FromKnownColor( KnownColor.InfoText );
            }
            else
            {
                this.tableLayoutPanel1.BackColor = UiControls.TitleBackColour;
                this.tableLayoutPanel1.ForeColor = UiControls.TitleForeColour;
            }
        }

        protected override void WndProc( ref Message m )
        {
            base.WndProc( ref m );

            const int WM_PAINT = 0xF;

            switch (m.Msg)
            {
                case WM_PAINT:
                    this.RedrawControlAsBitmap( this.Handle );
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

                this._lblTitle.Text = value;
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
                return this._lblSubTitle.Text;
            }
            set
            {
                this._lblSubTitle.Text = value;
                this._lblSubTitle.Visible = !string.IsNullOrEmpty(value);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Appearance")]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string HelpText
        {
            get
            {
                return this._helpText;
            }
            set
            {
                this._helpText = value;
                this.UpdateHelpButtonVisibility();
            }
        }

        private void UpdateHelpButtonVisibility()
        {
            switch (this._helpIcon)
            {
                case EHelpIcon.Automatic:
                    this._btnHelp.Visible = !string.IsNullOrEmpty( this._helpText );
                    break;

                case EHelpIcon.Off:
                    this._btnHelp.Visible = false;
                    break;

                default:
                    this._btnHelp.Visible = true;
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
                return this._helpIcon;
            }
            set
            {
                if (this._helpIcon == value)
                {
                    return;
                }

                this._helpIcon = value;
                this.UpdateColours();

                switch (this._helpIcon)
                {
                    case EHelpIcon.Automatic:
                    case EHelpIcon.Normal:
                        this._btnHelp.Image = UiControls.RecolourImage( Resources.MnuHelp, tableLayoutPanel1.ForeColor );
                        break;

                    case EHelpIcon.HideBar:
                    case EHelpIcon.Close:
                        this._btnHelp.Image = UiControls.RecolourImage( Resources.MnuCancel, tableLayoutPanel1.ForeColor );
                        break;            

                    case EHelpIcon.ShowBar:
                        this._btnHelp.Image = UiControls.RecolourImage( Resources.MnuHelpBar, tableLayoutPanel1.ForeColor );
                        break;
                }

                
                this.UpdateHelpButtonVisibility();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Appearance")]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string WarningText
        {
            get
            {
                return this._warningText;
            }
            set
            {
                this._warningText = value;
                this._btnWarning.Visible = !string.IsNullOrEmpty(this._warningText);
            }
        }

        private void _btnHelp_Click(object sender, EventArgs e)
        {
            if (this.HelpClicked != null)
            {
                CancelEventArgs e2 = new CancelEventArgs();

                this.HelpClicked(this, e2);

                if (e2.Cancel)
                {
                    return;
                }
            }                     

            if (!string.IsNullOrEmpty( this._helpText ))
            {
                FrmInputMultiLine.ShowFixed( this.FindForm(), "Help", "Help", this.Text, this._helpText );
            }
        }

        private void _btnWarning_Click(object sender, EventArgs e)
        {
            FrmMsgBox.ShowWarning(this.FindForm(), "Errors on form", this._warningText);
        }

        private void _btnWarning_MouseEnter(object sender, EventArgs e)
        {
            this._btnWarning.Text = this._warningText;
        }

        private void _btnWarning_MouseLeave(object sender, EventArgs e)
        {
            this._btnWarning.Text = string.Empty;
        }

        private void tableLayoutPanel1_Paint( object sender, PaintEventArgs e )
        {
            if (this.DrawHBar)
            {
                UiControls.DrawHBar( e.Graphics, this.tableLayoutPanel1, this.tableLayoutPanel1.BackColor, this.tableLayoutPanel1.ForeColor );
            }
        }

        [DefaultValue( true )]
        public bool DrawHBar { get; set; } = true;

        private void tableLayoutPanel1_SizeChanged( object sender, EventArgs e )
        {
            this.MinimumSize = new Size( 0, tableLayoutPanel1.Height );
        }
    }
}
