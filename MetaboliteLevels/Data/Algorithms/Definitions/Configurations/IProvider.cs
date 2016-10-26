using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MetaboliteLevels.Data.Session.Associational;
using MGui.Datatypes;
using MGui.Helpers;

namespace MetaboliteLevels.Data.Algorithms.Definitions.Configurations
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
