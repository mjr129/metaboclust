using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Session.General;

namespace MetaboliteLevels.Gui.Datatypes
{
    interface ICoreWatcher
    {
        void ChangeCore(Core newCore);
    }
}
