using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Forms.Generic;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Viewers.Lists;
using MGui;

namespace MetaboliteLevels.Forms.Activities
{
    internal partial class FrmActHeatMap : Form
    {
        private IDataSet dataSet;
        private Column column;
        private FrmMain frmMain;

        public static void Show( FrmMain owner, IDataSet dataSet, Column column )
        {
            using (FrmActHeatMap frm = new FrmActHeatMap( owner, dataSet, column ))
            {
                frm.Show( owner );
            }
        }       

        private FrmActHeatMap( FrmMain owner, IDataSet dataSet, Column column )
        {
            InitializeComponent();
            UiControls.SetIcon( this );

            this.frmMain = owner;
            this.dataSet = dataSet;
            this.column = column;
            this.Text = dataSet.Title + " - " + column.DisplayName;

            IVisualisable[] src = dataSet.UntypedGetList(false).Cast<IVisualisable>().ToArray();

            Bitmap bitmap = new Bitmap( src.Length, 64 );

            double min = double.MaxValue;
            double max = double.MinValue;

            for (int n = 0; n < src.Length; n++)
            {
                IVisualisable si = src[n];
                object r = column.GetRow( si );
                double d = Convert.ToDouble( r );

                min = Math.Min( d, min );
                max = Math.Max( d, max );
            }

            double range = max - min;

            using (Graphics g = Graphics.FromImage( bitmap ))
            {
                for (int n = 0; n < src.Length; n++)
                {
                    IVisualisable si = src[n];
                    object r = column.GetRow( si );
                    double d = Convert.ToDouble( r );

                    d = (d - min) / range;

                    Color c = ColourHelper.Blend( Color.Black, Color.Yellow, d );

                    using (Pen p = new Pen( c ))
                    {
                        g.DrawLine( p, n, 0, n, bitmap.Height );
                    }
                }
            }

            pictureBox1.Image = bitmap;
        }
    }
}
