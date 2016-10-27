using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Gui.Controls.Charts;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Gui.Forms.Activities
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
            this.InitializeComponent();
        }

        public FrmPopoutPlot( ISelectionHolder selectionHolder, Core core, ChartHelper chart)
            : this()
        {
            this._chart = new ChartHelper(selectionHolder, core, this, true);
            this._chart.Chart.SetPlot(chart.Chart.CurrentPlot); // TODO: Not sure about using same plot on two graphs!
        }
    }
}
