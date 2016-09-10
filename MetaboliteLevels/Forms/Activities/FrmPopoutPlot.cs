using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MCharting;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Viewers.Charts;

namespace MetaboliteLevels.Forms.Generic
{
    internal partial class FrmPopoutPlot : Form
    {
        private readonly ChartHelper _chart;

        public static void Show(Form owner, ISelectionHolder selectionHolder, Core core, ChartHelper chart)
        {
            using (FrmPopoutPlot frm = new FrmPopoutPlot( selectionHolder, core, chart))
            {
                frm.ShowDialog(owner);
            }
        }

        public FrmPopoutPlot()
        {
            InitializeComponent();
        }

        public FrmPopoutPlot( ISelectionHolder selectionHolder, Core core, ChartHelper chart)
            : this()
        {
            _chart = new ChartHelper(selectionHolder, core, this, true);
            _chart.Chart.SetPlot(chart.Chart.CurrentPlot); // TODO: Not sure about using same plot on two graphs!
        }
    }
}
