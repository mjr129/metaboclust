﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Session.Singular;

namespace MetaboliteLevels.Controls
{
    interface ICoreWatcher
    {
        void ChangeCore(Core newCore);
    }
}
