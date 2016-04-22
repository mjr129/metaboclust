using MetaboliteLevels.Data.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaboliteLevels.Viewers
{
    interface ICoreWatcher
    {
        void ChangeCore(Core newCore);
    }
}
