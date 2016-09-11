using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Data.Session.Associational;
using MGui.Helpers;

namespace MetaboliteLevels.Data.Algorithms.Definitions.Configurations
{
    /// <summary>
    /// Classes that can provide a <paramref name="T"/>.
    /// </summary>
    /// <typeparam name="T">Type provided by the class.</typeparam>
    internal interface IProvider<out T>            
    {
        T Provide { get; }
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
