using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MetaboliteLevels.Data.Session.Associational;
using MGui.Helpers;

namespace MetaboliteLevels.Data.Algorithms.Definitions.Configurations
{
    /// <summary>
    /// Classes that can provide an intensity matrix
    /// </summary>                                                 
    internal interface IMatrixProvider
    {
        [CanBeNull]
        IntensityMatrix Provide { get; }
    }         

    /// <summary>
    /// Classes that are replacable.
    /// The GUID identifies the particular instance.
    /// </summary>
    internal interface ITransient
    {
        Guid Guid { get; }
    }
}
