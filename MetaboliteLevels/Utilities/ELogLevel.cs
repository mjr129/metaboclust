using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaboliteLevels.Utilities
{
    /// <summary>
    /// Levels of logging
    /// </summary>
    /// <remarks>
    /// In order of severity for use with <see cref="Enumerable.Max"/>.
    /// </remarks>
    public enum ELogLevel
    {
        Information,
        Warning,
        Error,
    }
}
