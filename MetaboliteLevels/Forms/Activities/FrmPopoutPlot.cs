using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Controls.Charts;
using MetaboliteLevels.Data.Session.Singular;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Forms.Activities
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
