using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetaboliteLevels.Controls
{
    public partial class CtlError : Component
    {
        HashSet<Control> withErrors = new HashSet<Control>();

        public CtlError()
        {
            InitializeComponent();
        }

        public CtlError(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public void Check(Control control, bool condition, string text)
        {
            if (condition)
            {
                Remove(control);
            }
            else
            {
                Set(control, text);
            }
        }

        public void Clear()
        {
            withErrors.Clear();
            errorProvider1.Clear();
        }

        public bool NoErrors
        {
            get
            {
                return withErrors.Count == 0;
            }
        }   

        private void Set(Control control, string text)
        {
            errorProvider1.SetIconAlignment(control, ErrorIconAlignment.MiddleLeft);
            errorProvider1.SetIconPadding(control, 0);
            errorProvider1.SetError(control, text);
            withErrors.Add(control);
        }

        private void Remove(Control control)
        {
            errorProvider1.SetError(control, null);
            withErrors.Remove(control);
        }
    }
}
