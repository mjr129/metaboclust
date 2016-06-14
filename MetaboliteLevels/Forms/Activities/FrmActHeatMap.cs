using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Forms.Generic;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Viewers.Lists;
using MGui;
using MGui.Helpers;

namespace MetaboliteLevels.Forms.Activities
{
    internal partial class FrmActHeatMap : Form
    {                                  
        private Column column;                   
        private int _zoom = 1;
        private readonly ListViewHelper lvh;
        private HeatPoint[] src;
        private bool _ignoreScrollBarChanges;
        private Color _oorColour;
        private Color _nanColour;
        private Color _minColour;
        private Color _maxColour;
        private bool _sort;
        private Core core;

        public static void Show( Core core, ListViewHelper lvh, Column column )
        {
            FrmActHeatMap frm = new FrmActHeatMap( core, lvh, column );
            frm.Show( lvh.ListView.FindForm() );
        }       

        private FrmActHeatMap( Core core, ListViewHelper lvh, Column column )
        {
            InitializeComponent();
            UiControls.SetIcon( this );

            _maxColour = core.Options.HeatMapMaxColour;
            _nanColour = core.Options.HeatMapNanColour;
            _minColour = core.Options.HeatMapMinColour;
            _oorColour = core.Options.HeatMapOorColour;

            this.core= core;
            this.lvh = lvh;
            this.column = column;
            this.Text = UiControls.GetFileName( column.DisplayName );
            this.ctlTitleBar1.Text = this.Text;

            GenerateHeat();
        }

        class HeatPoint
        {
            public IVisualisable Source;
            public double Value;
            public Color Colour;
            public int Index;
            public double Frac;
        }

        private void GenerateHeat()
        {
            this.toolStripMenuItem1.Image = UiControls.CreateSolidColourImage( null, _maxColour, _maxColour );
            this.minToolStripMenuItem.Image = UiControls.CreateSolidColourImage( null, _minColour, _minColour );
            this.notANumberToolStripMenuItem.Image = UiControls.CreateSolidColourImage( null, _nanColour, _nanColour );
            this.outOfRangeToolStripMenuItem.Image = UiControls.CreateSolidColourImage( null, _oorColour, _oorColour );

            var srca = lvh.GetVisible().ToArray();

            double min = double.MaxValue;
            double max = double.MinValue;
            int numberOfValids = 0;

            for (int n = 0; n < srca.Length; n++)
            {
                var vis = srca[n];             
                double d = GetRow( vis );

                min = Math.Min( d, min );
                max = Math.Max( d, max );
            }

            toolStripMenuItem1.Text += " (" + Maths.SignificantDigits( max) + ")";
            minToolStripMenuItem.Text += " (" + Maths.SignificantDigits( min ) + ")";

            double range = max - min;

            src = new HeatPoint[srca.Length];

            for (int n=0; n < srca.Length; n++)
            {
                var vis = srca[n];                 
                HeatPoint heat = new HeatPoint(  );
                heat.Source = vis;
                heat.Value = GetRow( vis );

                double frac = heat.Value = (heat.Value - min) / range; ;

                heat.Frac = frac;
                if (double.IsNaN( heat.Value ))
                {                    
                    heat.Colour = _nanColour;
                }
                else
                {
                    numberOfValids++;
                    heat.Colour = ColourHelper.Blend( _minColour, _maxColour, frac );
                }         

                heat.Index = n;
                src[n] = heat;
            }

            if (_sort)
            {
                src = src.OrderBy( z => z.Value ).ToArray();
            }

            if (numberOfValids <= (srca.Length/2))
            {
                FrmMsgBox.ShowInfo( this, Text, "The majority of information in this column is not numeric, the heatmap may be missing information.", FrmMsgBox.EDontShowAgainId.HEATMAP_COLUMN_NOT_NUMERICAL );
            }

            GenerateBitmap();
        }

        private void GenerateBitmap(  )
        {
            this.sameAsListToolStripMenuItem.Checked = !_sort;
            this.orderedToolStripMenuItem.Checked = _sort;
            this.defaultToolStripMenuItem.Checked = _zoom == 1;

            if (pictureBox1.ClientSize.Height == 0)
            {
                return;
            }

            Bitmap bitmap = new Bitmap( pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height );

            using (Graphics g = Graphics.FromImage( bitmap ))
            {
                g.Clear( _oorColour );

                for (int x = 0; x < bitmap.Width; x += _zoom)
                {
                    int index = XToIndex( x );

                    if (index >= src.Length)
                    {
                        break;
                    }

                    Color c = src[index].Colour;

                    using (SolidBrush b = new SolidBrush( c ))
                    {
                        g.FillRectangle( b, x, 0, _zoom, bitmap.Height );
                    }
                }
            }

            pictureBox1.Image = bitmap;
            _ignoreScrollBarChanges = true;
            hScrollBar1.Minimum = 0;
            hScrollBar1.Maximum = src.Length - (bitmap.Width / _zoom);
            hScrollBar1.Visible = hScrollBar1.Maximum > 0;
            _ignoreScrollBarChanges = false;
            toolStripStatusLabel2.Text = "(" + (XToIndex( 0 ) + 1) + "-" + (XToIndex( bitmap.Width - 1 ) + 1) + ")/" + src.Length + ". Zoom = " + _zoom + "x";
        }       

        private int XToIndex( int x )
        {
            return (x / _zoom) + Math.Max( 0, hScrollBar1.Value );
        }

        private double GetRow( IVisualisable arg )
        {
            object r = column.GetRow( arg );             
            return Column.AsDouble( r );
        }

        private void pictureBox1_MouseMove( object sender, MouseEventArgs e )
        {
            int index = XToIndex( e.X );

            if (index >= src.Length)
            {
                toolStripStatusLabel1.Visible = false;
                toolStripStatusLabel3.Visible = false;
                return;
            }

            var h = src[ index];
            toolStripStatusLabel1.Text = h.Source.DisplayName + " = (" + index + ", " + h.Value + ")";
            toolStripStatusLabel3.BackColor = h.Colour;
            toolStripStatusLabel1.Visible = true;
            toolStripStatusLabel3.Visible = true;
        }

        private void pictureBox1_MouseUp( object sender, MouseEventArgs e )
        {
            int index = XToIndex( e.X );

            if (index >= src.Length)
            {                                   
                return;
            }
                            
            lvh.ActivateItem( src[index].Source);
        }

        private void pictureBox1_MouseDown( object sender, MouseEventArgs e )
        {

        }

        private void xToolStripMenuItem_Click( object sender, EventArgs e )
        {
            Zoom( 1 );
        }

        private void Zoom( int v )
        {
            _zoom = v;
            GenerateBitmap();
        }

        private void xToolStripMenuItem1_Click( object sender, EventArgs e )
        {
            Zoom( 2 );
        }

        private void zoomInToolStripMenuItem_Click( object sender, EventArgs e )
        {
            Zoom( _zoom + 1 );
        }

        private void defaultToolStripMenuItem_Click( object sender, EventArgs e )
        {
            Zoom( 1 );
        }

        private void zoomInToolStripMenuItem1_Click( object sender, EventArgs e )
        {
            Zoom( _zoom + 1 );
        }

        private void sameAsListToolStripMenuItem_Click( object sender, EventArgs e )
        {
            _sort = false;
            src = src.OrderBy( z => z.Index ).ToArray();
            GenerateBitmap();
        }

        private void orderedToolStripMenuItem_Click( object sender, EventArgs e )
        {
            _sort = true;
            src = src.OrderBy( z => z.Value ).ToArray();
            GenerateBitmap();
        }

        private void FrmActHeatMap_Resize( object sender, EventArgs e )
        {
            GenerateBitmap();
        }

        private void hScrollBar1_ValueChanged( object sender, EventArgs e )
        {
         
        }

        private void hScrollBar1_Scroll( object sender, ScrollEventArgs e )
        {
            if (_ignoreScrollBarChanges)
            {
                return;
            }

            GenerateBitmap();
        }

        private void legendToolStripMenuItem_DropDownOpening( object sender, EventArgs e )
        {
            

        }

        private void toolStripMenuItem1_Click( object sender, EventArgs e )
        {
            if (ColourHelper.EditColor( ref _maxColour ))
            {
                core.Options.HeatMapMaxColour = _maxColour;
                GenerateHeat();
            }
        }

        private void minToolStripMenuItem_Click( object sender, EventArgs e )
        {
            if (ColourHelper.EditColor( ref _minColour ))
            {
                core.Options.HeatMapMinColour = _minColour;
                GenerateHeat();
            }
        }

        private void notANumberToolStripMenuItem_Click( object sender, EventArgs e )
        {
            if (ColourHelper.EditColor( ref _nanColour ))
            {
                core.Options.HeatMapNanColour = _nanColour;
                GenerateHeat();
            }
        }

        private void outOfRangeToolStripMenuItem_Click( object sender, EventArgs e )
        {
            if (ColourHelper.EditColor( ref _oorColour ))
            {
                core.Options.HeatMapOorColour = _oorColour;
                GenerateHeat();
            }
        }

        private void pictureBox1_MouseLeave( object sender, EventArgs e )
        {
            toolStripStatusLabel1.Visible = false;
            toolStripStatusLabel3.Visible = false;
        }
    }
}
