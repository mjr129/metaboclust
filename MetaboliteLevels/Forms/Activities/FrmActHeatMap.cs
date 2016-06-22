using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
using MGui.Helpers;

namespace MetaboliteLevels.Forms.Activities
{
    internal partial class FrmActHeatMap : Form
    {                                  
        private Column _source1D;
        private DistanceMatrix _source2D;
        private int _zoom = 1;
        private readonly ListViewHelper _sourceList;
        private HeatPoint[,] _heatMap;
        private bool _ignoreScrollBarChanges;
        private Color _oorColour;
        private Color _nanColour;
        private Color _minColour;
        private Color _maxColour;
        private bool _sort;
        private Core _core;               

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

            this.Text = UiControls.GetFileName( source1D.DisplayName );
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
        }

        private void GenerateHeat()
        {
            // Set legend colours
            this.toolStripMenuItem1.Image = UiControls.CreateSolidColourImage( null, _maxColour, _maxColour );
            this.minToolStripMenuItem.Image = UiControls.CreateSolidColourImage( null, _minColour, _minColour );
            this.notANumberToolStripMenuItem.Image = UiControls.CreateSolidColourImage( null, _nanColour, _nanColour );
            this.outOfRangeToolStripMenuItem.Image = UiControls.CreateSolidColourImage( null, _oorColour, _oorColour );

            Generate1DHeatMap();

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
                FrmMsgBox.ShowInfo( this, Text, "The majority of information in this column is not numeric, the heatmap may be missing information.", FrmMsgBox.EDontShowAgainId.HEATMAP_COLUMN_NOT_NUMERICAL );
            }

            // Generate bitmap
            GenerateBitmap();
        }

        private void Generate2DHeatMap()
        {   
            int n = _source2D.ValueMatrix.NumVectors;
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

            _heatMap = new HeatPoint[1, tsrc.Length];

            for (int n = 0; n < tsrc.Length; n++)
            {
                _heatMap[1, n] = tsrc[n];
            }
        }

        private void GenerateBitmap(  )
        {
            // Set checks
            this.sameAsListToolStripMenuItem.Checked = !_sort;
            this.orderedToolStripMenuItem.Checked = _sort;
            this.defaultToolStripMenuItem.Checked = _zoom == 1;

            _ignoreScrollBarChanges = true;
            hScrollBar1.Minimum = 0;
            hScrollBar1.Maximum = _heatMap.Length - (pictureBox1.ClientSize.Width / _zoom);
            hScrollBar1.Visible = hScrollBar1.Maximum > 0;
            _ignoreScrollBarChanges = false;
            toolStripStatusLabel2.Text = "(" + (XToIndex( 0 ) + 1) + "-" + (XToIndex( pictureBox1.ClientSize.Width - 1 ) + 1) + ")/" + _heatMap.Length + ". Zoom = " + _zoom + "x";

            pictureBox1.Rerender(); 
        }       

        private int XToIndex( int x )
        {
            return (x / _zoom) + Math.Max( 0, hScrollBar1.Value );
        }

        private int YToIndex( int y )
        {
            return (y / _zoom) + Math.Max( 0, vScrollBar1.Value );
        }

        private double GetRow( IVisualisable arg )
        {
            object r = _source1D.GetRow( arg );             
            return Column.AsDouble( r );
        }

        private void pictureBox1_MouseMove( object sender, MouseEventArgs e )
        {
            int xIndex = XToIndex( e.X );
            int yIndex = YToIndex( e.Y );

            if (xIndex >= _heatMap.GetLength(0)||yIndex>_heatMap.GetLength(1))
            {
                toolStripStatusLabel1.Visible = false;
                toolStripStatusLabel3.Visible = false;
                return;
            }

            HeatPoint h = _heatMap[ xIndex, yIndex];

            if (h.YSource == null)
            {
                toolStripStatusLabel1.Text = h.XSource.DisplayName + " = (" + xIndex + ", " + h.ZValue + ")";
            }
            else
            {
                toolStripStatusLabel1.Text = "{" + h.XSource.DisplayName + ", " + h.YSource.DisplayName + "} ( {" + xIndex + ", " + yIndex + " }, " + h.ZValue + ")";
            }

            toolStripStatusLabel3.BackColor = h.ZColour;
            toolStripStatusLabel1.Visible = true;
            toolStripStatusLabel3.Visible = true;
        }

        private void pictureBox1_MouseUp( object sender, MouseEventArgs e )
        {
            int xIndex = XToIndex( e.X );
            int yIndex = YToIndex( e.Y );

            if (xIndex >= _heatMap.GetLength( 0 ) || yIndex > _heatMap.GetLength( 1 ))
            {
                toolStripStatusLabel1.Visible = false;
                toolStripStatusLabel3.Visible = false;
                return;
            }

            if (_heatMap[xIndex, yIndex].YSource == null || _heatMap[xIndex, yIndex].YSource== _heatMap[xIndex, yIndex].XSource)
            {
                _sourceList.ActivateItem( _heatMap[xIndex, yIndex].XSource );
            }
            else
            {
                alphaToolStripMenuItem.Text = _heatMap[xIndex, yIndex].XSource.DisplayName;
                alphaToolStripMenuItem.Tag = _heatMap[xIndex, yIndex].XSource;
                betaToolStripMenuItem.Text = _heatMap[xIndex, yIndex].YSource.DisplayName;
                betaToolStripMenuItem.Tag = _heatMap[xIndex, yIndex].YSource;
                contextMenuStrip1.Show( pictureBox1, e.Location );
            }
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
            GenerateHeat();
        }

        private void orderedToolStripMenuItem_Click( object sender, EventArgs e )
        {
            _sort = true;
            GenerateHeat();
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
            toolStripStatusLabel1.Visible = false;
            toolStripStatusLabel3.Visible = false;
        }

        private void pictureBox1_DrawBuffer( object sender, DrawBufferEventArgs e )
        {
            Graphics g = e.Graphics;

            g.Clear( _oorColour );

            for (int y = 0; y < e.Bitmap.Height; y += _zoom)
            {
                int yIndex = YToIndex( y );

                if (yIndex >= _heatMap.GetLength(1))
                {
                    break;
                }

                for (int x = 0; x < e.Bitmap.Width; x += _zoom)
                {
                    int xIndex = XToIndex( x );

                    if (xIndex >= _heatMap.GetLength( 0 ))
                    {
                        break;
                    }

                    Color c = _heatMap[xIndex, yIndex].ZColour;

                    using (SolidBrush b = new SolidBrush( c ))
                    {
                        g.FillRectangle( b, x, 0, _zoom, e.Bitmap.Height );
                    }
                }
            }
        }

        private void alphaToolStripMenuItem_Click( object sender, EventArgs e )
        {
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
            IVisualisable vis = (IVisualisable)tsmi.Tag;
            _sourceList.ActivateItem( vis );
        }
    }
}
