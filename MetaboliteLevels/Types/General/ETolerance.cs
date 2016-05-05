using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Data.General
{
    public enum ETolerance
    {
        [Name( "absolute" )]
        Absolute,

        [Name( "%" )]
        Percent,

        [Name( "ppm" )]
        PartsPerMillion,
    }
}
