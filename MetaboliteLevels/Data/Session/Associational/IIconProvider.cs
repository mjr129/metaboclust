using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace MetaboliteLevels.Data.Session.Associational
{
    internal interface IIconProvider
    {
        [NotNull] Image Icon { get; }
    }
}
