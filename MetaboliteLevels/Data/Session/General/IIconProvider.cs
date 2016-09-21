using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Data.Session.General
{
    internal interface IIconProvider
    {
        UiControls.ImageListOrder Icon { get; }
    }
}
