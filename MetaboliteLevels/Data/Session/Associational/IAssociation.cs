using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace MetaboliteLevels.Data.Session.Associational
{
    interface IAssociation
    {
        [NotNull] ContentsRequest OriginalRequest { get; }

        [NotNull] object Associated { get; }
    }
}
