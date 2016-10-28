using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Gui.Forms.Activities;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Gui.Forms.Wizards
{
    internal partial class FrmViewSpreadsheet : Form
    {
        public static void Show( IWin32Window owner, string markup )
        {
            using (FrmViewSpreadsheet frm = new FrmViewSpreadsheet( markup ))
            {
                frm.ShowDialog( owner );
            }
        }

        protected override void OnResizeBegin( EventArgs e )
        {
            base.OnResizeBegin( e );

            this.tableLayoutPanel1.SuspendDrawingAndLayout();
        }

        protected override void OnResizeEnd( EventArgs e )
        {
            base.OnResizeEnd( e );

            this.tableLayoutPanel1.ResumeDrawingAndLayout();
        }

        public FrmViewSpreadsheet( string markup )
        {
            this.InitializeComponent();
            UiControls.SetIcon( this );

            FileLoadInfo fli = UiControls.GetFileLoadInfo();

            this.tableLayoutPanel1.ColumnStyles.Clear();
            Type t = fli.GetType();

            string[] lines = markup.Split( "\r\n".ToCharArray() );
            string data = null;
            StringBuilder description = new StringBuilder();

            foreach (string line in lines.Skip( 1 ))
            {
                if (line.StartsWith( "{" ) && line.EndsWith( "}" ))
                {                
                    this.AddDescription( data, description.ToString() );
                    string name = line.Substring( 1, line.Length - 2 );

                    if (name.StartsWith( "=" ))
                    {
                        data = name.Substring( 1 );
                    }
                    else if (name == "")
                    {
                        data = "This is the first cell of the spreadsheet and must be left empty.";
                    }
                    else if (name == "META")
                    {
                        data = "All further columns -->";
                    }
                    else
                    {
                        FieldInfo field = t.GetField( name );
                        string[] value = (string[])field.GetValue( fli );
                        data = "\"" + string.Join( "\" or \"", value ) + "\"";
                    }

                    description.Clear();
                }
                else
                {
                    description.AppendLine( line );
                }
            }

            this.AddDescription( data, description.ToString() );
        }

        int _descs = 0;

        private void AddDescription( string data, string description )
        {
            if (data == null)
            {
                return;
            }

            data = data.Replace( "¶", "\r\n" );
            description = description.Replace( "¶", "\r\n" );


            int i = this._descs;
            this._descs++;
            this.tableLayoutPanel1.ColumnCount = this._descs;
            this.tableLayoutPanel1.RowCount = Math.Max( 4, this._descs );
            this.tableLayoutPanel1.ColumnStyles.Add( new ColumnStyle( SizeType.Percent, 100 ) );

            bool noDesc = string.IsNullOrWhiteSpace( description );
            var padding = new Padding( 8, 8, 8, 8 );

            Label labelTitle = new Label()
            {
                Text = data.Trim(),
                Visible = true,
                Dock = DockStyle.Fill,
                AutoSize = true,
                Font = FontHelper.SmallBoldFont,
                ForeColor = noDesc ? Color.Blue : Color.Black,
                Padding = padding,
            };

            Label labelDescription = new Label()
            {
                Text = description.Trim(),
                Visible = true,
                Dock = DockStyle.Fill,
                AutoSize = true,
                Font = FontHelper.SmallRegularFont,
                ForeColor = Color.Black,
                Padding = padding,
            };

            string quote = noDesc ? "" : "\"";

            Label labelDescription2 = new Label()
            {
                Text = quote,
                Visible = true,
                Dock = DockStyle.Fill,
                AutoSize = true,
                Font = FontHelper.SmallRegularFont,
                ForeColor = Color.Gray,
                Padding = padding,
            };

            Label labelDescription3 = new Label()
            {
                Text = quote,
                Visible = true,
                Dock = DockStyle.Fill,
                AutoSize = true,
                Font = FontHelper.SmallRegularFont,
                ForeColor = Color.Silver,
                Padding = padding,
            };

            this.tableLayoutPanel1.Controls.Add( labelTitle, i, 0 );
            this.tableLayoutPanel1.Controls.Add( labelDescription, i, 1 );
            this.tableLayoutPanel1.Controls.Add( labelDescription2, i, 2 );
            this.tableLayoutPanel1.Controls.Add( labelDescription3, i, 3 );
        }
    }
}
