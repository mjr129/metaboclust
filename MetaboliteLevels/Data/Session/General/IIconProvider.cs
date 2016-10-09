using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Data.Session.General
{
    internal interface IIconProvider
    {
        Image Icon { get; }
    }
}
