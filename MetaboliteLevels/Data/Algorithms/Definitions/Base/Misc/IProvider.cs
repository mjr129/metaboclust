using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MetaboliteLevels.Data.Session.Main;
using MGui.Datatypes;

namespace MetaboliteLevels.Data.Algorithms.Definitions.Base.Misc
{
    /// <summary>
    /// Classes compatible with the EXPORT DATA screen
    /// </summary>
    internal interface IExportProvider
    {
        ISpreadsheet ExportData(   );
    }

    /// <summary>
    /// Classes that can provide an intensity matrix
    /// </summary>                                                 
    internal interface IMatrixProvider : IExportProvider
    {
        [CanBeNull]
        IntensityMatrix Provide { get; }
    }     
}
