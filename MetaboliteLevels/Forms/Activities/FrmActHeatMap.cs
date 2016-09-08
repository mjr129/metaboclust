using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Algorithms;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Forms.Generic;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Viewers.Lists;
using MGui;
using MGui.Controls;   

namespace MetaboliteLevels.Forms.Activities
{                    
    internal partial class FrmActHeatMap : Form
    {                                  
        private readonly Column _source1D;
        private readonly DistanceMatrix _source2D;
        private readonly ListViewHelper _sourceList;
        private HeatPoint[,] _heatMap;
        private bool _ignoreScrollBarChanges;
        private Color _oorColour;
        private Color _nanColour;
        private Color _minColour;
        private Color _maxColour;
        private bool _sort;
        private readonly Core _core;
        private int _zoom = 1;

        /// <summary>
        /// Shows a 1D heatmap
        /// </summary>        
        public static void Show( Core core, ListViewHelper lvh, Column source )
        {
            FrmActHeatMap frm = new FrmActHeatMap( core, lvh, source, null );
            frm.Show( lvh.ListView.FindForm() );
        }

        /// <summary>
        /// Shows a 2D heatmap
        /// </summary>        
        public static void Show( Core core, ListViewHelper lvh, DistanceMatrix source )
        {
            FrmActHeatMap frm = new FrmActHeatMap( core, lvh, null, source);
            frm.Show( lvh.ListView.FindForm() );
        }

        private FrmActHeatMap( Core core, ListViewHelper lvh, Column source1D, DistanceMatrix source2D )
        {
            InitializeComponent();
            UiControls.SetIcon( this );

            this._maxColour = core.Options.HeatMapMaxColour;
            this._nanColour = core.Options.HeatMapNanColour;
            this._minColour = core.Options.HeatMapMinColour;
            this._oorColour = core.Options.HeatMapOorColour;
                              
            this._core= core;
            this._sourceList = lvh;
            this._source1D = source1D;
            this._source2D = source2D;

            if (source1D != null)
            {
                this.Text = UiControls.GetFileName( source1D.DisplayName );
            }
            else
            {
                this.Text = UiControls.GetFileName( "Distance matrix" );
            }

            this.ctlTitleBar1.Text = this.Text;

            GenerateHeat();
        }

        struct HeatPoint
        {
            public IVisualisable XSource;
            public int XIndex;

            public IVisualisable YSource;
            public int YIndex;

            public double ZFraction;
            public double ZValue;
            public Color ZColour;

            public bool IsEmpty => XSource == null && YSource == null;
        }

        private void GenerateHeat()
        {
            // Set legend colours
            this.toolStripMenuItem1.Image = UiControls.CreateSolidColourImage( null, _maxColour, _maxColour );
            this.minToolStripMenuItem.Image = UiControls.CreateSolidColourImage( null, _minColour, _minColour );
            this.notANumberToolStripMenuItem.Image = UiControls.CreateSolidColourImage( null, _nanColour, _nanColour );
            this.outOfRangeToolStripMenuItem.Image = UiControls.CreateSolidColourImage( null, _oorColour, _oorColour );

            if (_source1D != null)
            {
                Generate1DHeatMap();
            }
            else
            {
                Generate2DHeatMap();
            }

            // Calculate min/max
            double min = double.MaxValue;
            double max = double.MinValue;
            int numberOfValids = 0;

            foreach(HeatPoint hp in _heatMap)
            {
                double val = hp.ZValue;

                if (!double.IsNaN( val ) && !double.IsInfinity( val ))
                {
                    numberOfValids++;
                    min = Math.Min( hp.ZValue, min );
                    max = Math.Max( hp.ZValue, max );
                }
            }

            double range = max - min;

            // Set legend ranges
            toolStripMenuItem1.Text += " (" + Maths.SignificantDigits( max ) + ")";
            minToolStripMenuItem.Text += " (" + Maths.SignificantDigits( min ) + ")";

            // Set colours
            for (int x = 0; x < _heatMap.GetLength( 0 ); x++)
            {
                for (int y = 0; y < _heatMap.GetLength( 1 ); y++)
                {
                    double val = _heatMap[x,y].ZValue;

                    if (!double.IsNaN( val ) && !double.IsInfinity( val ))
                    {
                        _heatMap[x, y].ZFraction = (val - min) / range;
                        _heatMap[x, y].ZColour = ColourHelper.Blend( _minColour, _maxColour, _heatMap[x, y].ZFraction );
                    }
                    else
                    {
                        _heatMap[x, y].ZColour = _nanColour;
                    }
                }
            }

            // Low valid count warning
            if (numberOfValids <= (_heatMap.Length / 2))
            {
                FrmMsgBox.ShowInfo( this, Text, "The majority of information in this column is not numeric, the heatmap may be missing information.", FrmMsgBox.EDontShowAgainId.HeatmapColumnNotNumerical );
            }

            // Generate bitmap
            GenerateBitmap();
        }

        private void Generate2DHeatMap()
        {   
            int n = _source2D.ValueMatrix.NumRows;
            _heatMap = new HeatPoint[n, n];

            for (int y = 0; y < n; y++)
            {
                for (int x = 0; x < n; x++)
                {
                    HeatPoint hp = new HeatPoint(  );
                    hp.XIndex = x;
                    hp.XSource = _source2D.ValueMatrix.Vectors[x].Peak;
                    hp.YIndex = y;
                    hp.YSource = _source2D.ValueMatrix.Vectors[y].Peak;

                    hp.ZValue = _source2D.Values[x, y];

                    _heatMap[x, y] = hp;
                }
            }
        }

        private void Generate1DHeatMap()
        {
            // Get source list
            IVisualisable[] source = _sourceList.GetVisible().ToArray();

            // Generate heatmap values
            HeatPoint[] tsrc = new HeatPoint[source.Length];

            for (int n = 0; n < source.Length; n++)
            {
                var vis = source[n];
                HeatPoint heat = new HeatPoint();
                heat.XSource = vis;
                heat.XIndex = n;
                heat.ZValue = GetRow( vis );
                tsrc[n] = heat;
            }

            // Sort if specified
            if (_sort)
            {
                tsrc = tsrc.OrderBy( z => z.ZValue ).ToArray();
            }

            _heatMap = new HeatPoint[tsrc.Length,1];

            for (int n = 0; n < tsrc.Length; n++)
            {
                _heatMap[n, 0] = tsrc[n];
            }
        }

        private void GenerateBitmap()
        {
            // Set checks
            this.sameAsListToolStripMenuItem.Checked = !_sort;
            this.orderedToolStripMenuItem.Checked = _sort;

            _ignoreScrollBarChanges = true;
            hScrollBar1.Value = 0;
            vScrollBar1.Value = 0;
            _ignoreScrollBarChanges = false;

            hScrollBar1.Maximum = _heatMap.GetLength( 0 ) - 1;
            vScrollBar1.Maximum = _heatMap.GetLength( 1 ) - 1;

            hScrollBar1.Visible = hScrollBar1.Maximum != 0;
            vScrollBar1.Visible = vScrollBar1.Maximum != 0;

            pictureBox1.Rerender();
        }             

        private double GetRow( IVisualisable arg )
        {
            object r = _source1D.GetRow( arg );             
            return Column.AsDouble( r );
        }

        HeatPoint ScreenToHeatMap( Point p )
        {
            int x = p.X / _zoom;
            int y = p.Y / _zoom;

            x += hScrollBar1.Value;
            y += vScrollBar1.Value;

            if (_heatMap.GetLength( 1 ) == 1)
            {
                y = 0;
            }

            if (x < 0 || x >= _heatMap.GetLength( 0 )
                || y < 0 || y >= _heatMap.GetLength(1))
            {
                return new HeatPoint();
            }

            return _heatMap[x, y];
        }

        private void pictureBox1_MouseMove( object sender, MouseEventArgs e )
        {
            HeatPoint h = ScreenToHeatMap( e.Location );

            if (h.IsEmpty)
            {
                _lblSelection.Visible = false;
                toolStripStatusLabel3.Visible = false;
                return;
            }

            if (h.YSource == null)
            {
                _lblSelection.Text = h.XSource.DisplayName + " = (" + h.XIndex + ", " + h.ZValue + ")";
            }
            else
            {
                _lblSelection.Text = "{" + h.XSource.DisplayName + ", " + h.YSource.DisplayName + "} ( {" + h.XIndex + ", " + h.YIndex + " }, " + h.ZValue + ")";
            }

            toolStripStatusLabel3.BackColor = h.ZColour;
            _lblSelection.Visible = true;
            toolStripStatusLabel3.Visible = true;
        }

        private void pictureBox1_MouseUp( object sender, MouseEventArgs e )
        {
            HeatPoint h = ScreenToHeatMap( e.Location );

            if (h.IsEmpty)
            {
                _lblSelection.Visible = false;
                toolStripStatusLabel3.Visible = false;
                return;
            }

            if (h.YSource == null || h.YSource== h.XSource)
            {
                _sourceList.ActivateItem( h.XSource );
            }
            else
            {
                alphaToolStripMenuItem.Text = h.XSource.DisplayName;
                alphaToolStripMenuItem.Tag = h.XSource;
                betaToolStripMenuItem.Text = h.YSource.DisplayName;
                betaToolStripMenuItem.Tag = h.YSource;
                contextMenuStrip1.Show( pictureBox1, e.Location );
            }
        }

        private void pictureBox1_MouseDown( object sender, MouseEventArgs e )
        {

        }  

        private void sameAsListToolStripMenuItem_Click( object sender, EventArgs e )
        {
            _sort = false;
            GenerateHeat();
        }

        private void orderedToolStripMenuItem_Click( object sender, EventArgs e )
        {
            _sort = true;
            GenerateHeat();
        }

        private void FrmActHeatMap_Resize( object sender, EventArgs e )
        {
            //GenerateBitmap();
        }           

        private void eitherScrollBar_Scroll( object sender, ScrollEventArgs e )
        {
            if (!_ignoreScrollBarChanges)
            {
                pictureBox1.Rerender();
            }               
        }

        private void legendToolStripMenuItem_DropDownOpening( object sender, EventArgs e )
        {
            

        }

        private void toolStripMenuItem1_Click( object sender, EventArgs e )
        {
            if (ColourHelper.EditColor( ref _maxColour ))
            {
                _core.Options.HeatMapMaxColour = _maxColour;
                GenerateHeat();
            }
        }

        private void minToolStripMenuItem_Click( object sender, EventArgs e )
        {
            if (ColourHelper.EditColor( ref _minColour ))
            {
                _core.Options.HeatMapMinColour = _minColour;
                GenerateHeat();
            }
        }

        private void notANumberToolStripMenuItem_Click( object sender, EventArgs e )
        {
            if (ColourHelper.EditColor( ref _nanColour ))
            {
                _core.Options.HeatMapNanColour = _nanColour;
                GenerateHeat();
            }
        }

        private void outOfRangeToolStripMenuItem_Click( object sender, EventArgs e )
        {
            if (ColourHelper.EditColor( ref _oorColour ))
            {
                _core.Options.HeatMapOorColour = _oorColour;
                GenerateHeat();
            }
        }

        private void pictureBox1_MouseLeave( object sender, EventArgs e )
        {
            _lblSelection.Visible = false;
            toolStripStatusLabel3.Visible = false;
        }       

        private void alphaToolStripMenuItem_Click( object sender, EventArgs e )
        {
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
            IVisualisable vis = (IVisualisable)tsmi.Tag;
            _sourceList.ActivateItem( vis );
        }

        private void pictureBox1_Render( object sender, RenderEventArgs e )
        {   
            int w = _heatMap.GetLength( 0 );
            int h = _heatMap.GetLength( 1 );
            Bitmap bmp = e.Bitmap;
            Rectangle all = new Rectangle( 0, 0, bmp.Width, bmp.Height);

            BitmapData bdata = bmp.LockBits( all, ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb );
            int size = bdata.Height * bdata.Stride;
            byte[] data = new byte[size];              

            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    int i = (x * 3) + (y * bdata.Stride);

                    HeatPoint hm = ScreenToHeatMap( new Point( x, y ) );

                    Color c = !hm.IsEmpty ? hm.ZColour : _oorColour;

                    data[i + 0] = c.B;
                    data[i + 1] = c.G;
                    data[i + 2] = c.R;
                }
            }

            // Marshal to avoid unsafe code...
            Marshal.Copy( data, 0, bdata.Scan0, data.Length );

            bmp.UnlockBits( bdata );             
        }

        private void defaultToolStripMenuItem_Click( object sender, EventArgs e )
        {
            _zoom = 1;
            pictureBox1.Rerender();
        }

        private void zoomInToolStripMenuItem1_Click( object sender, EventArgs e )
        {
            _zoom += 1;
            pictureBox1.Rerender();
        }

        private void zoomout1ToolStripMenuItem_Click( object sender, EventArgs e )
        {
            _zoom -= 1;

            if (_zoom < 1)
            {
                _zoom = 1;
            }

            pictureBox1.Rerender();
        }
    }
}
