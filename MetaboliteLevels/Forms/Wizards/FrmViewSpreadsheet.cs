using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Controls;
using MetaboliteLevels.Forms.Activities;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Forms.Wizards
{
    internal partial class FrmViewSpreadsheet : Form
    {
        public static void Show( IWin32Window owner, string markup, FileLoadInfo fli )
        {
            using (FrmViewSpreadsheet frm = new FrmViewSpreadsheet( markup, fli ))
            {
                frm.ShowDialog( owner );
            }
        }

        protected override void OnResizeBegin( EventArgs e )
        {
            base.OnResizeBegin( e );

            tableLayoutPanel1.SuspendDrawingAndLayout();
        }

        protected override void OnResizeEnd( EventArgs e )
        {
            base.OnResizeEnd( e );

            tableLayoutPanel1.ResumeDrawingAndLayout();
        }

        public FrmViewSpreadsheet( string markup, FileLoadInfo fli )
        {
            InitializeComponent();
            UiControls.SetIcon( this );

            tableLayoutPanel1.ColumnStyles.Clear();
            Type t = fli.GetType();

            string[] lines = markup.Split( "\r\n".ToCharArray() );
            string data = null;
            StringBuilder description = new StringBuilder();

            foreach (string line in lines.Skip(1))
            {   
                if (line.StartsWith( "{" ) && line.EndsWith( "}" ))
                {
                    AddDescription( data, description.ToString() );
                    string name = line.Substring( 1, line.Length - 2 );

                    if (name.StartsWith( "=" ))
                    {
                        data =  name.Substring( 1 ) ;
                    }
                    else if (name == "")
                    {
                        data =  "This is the first cell of the spreadsheet and must be left empty." ;
                    }
                    else if (name == "META")
                    {
                        data =  "All further columns -->" ;
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

            AddDescription( data, description.ToString() );
        }

        int _descs = 0;

        private void AddDescription( string data, string description )
        {
            if (data == null)
            {
                return;
            }

            int i = _descs;
            _descs++;
            tableLayoutPanel1.ColumnCount = _descs;
            tableLayoutPanel1.RowCount = Math.Max( 4, _descs );
            tableLayoutPanel1.ColumnStyles.Add( new ColumnStyle( SizeType.Percent, 100 ) );  

            bool noDesc = string.IsNullOrWhiteSpace( description );

            Label labelTitle = new Label()
            {
                Text = data.Trim(),
                Visible = true,
                Dock = DockStyle.Fill,
                AutoSize = true,
                Font = FontHelper.SmallBoldFont,
                ForeColor = noDesc ? Color.Blue : Color.Black,
            };

            Label labelDescription = new Label()
            {
                Text = description.Trim(),
                Visible = true,
                Dock = DockStyle.Fill,
                AutoSize = true,
                Font = FontHelper.SmallRegularFont,
                ForeColor = Color.Black,
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
            };

        Label labelDescription3 = new Label()
        {
            Text = quote,
            Visible = true,
            Dock = DockStyle.Fill,
            AutoSize = true,
            Font = FontHelper.SmallRegularFont,
            ForeColor = Color.Silver,
    };

            tableLayoutPanel1.Controls.Add( labelTitle, i, 0 );
            tableLayoutPanel1.Controls.Add( labelDescription, i, 1 );
            tableLayoutPanel1.Controls.Add( labelDescription2, i, 2 );
            tableLayoutPanel1.Controls.Add( labelDescription3, i, 3 );
        }
    }
}
