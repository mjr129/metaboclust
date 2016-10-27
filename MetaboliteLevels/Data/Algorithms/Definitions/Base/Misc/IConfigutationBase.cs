using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Data.Algorithms.Definitions.Base.Misc
{
    /// <summary>
    /// All classes implementing this interface also implement <see cref="ConfigurationBase"/>, so see that class for details.
    /// </summary>           
    [Serializable]
    internal abstract class ConfigurationBase : Visualisable, IDisposable
    {
        public abstract bool HasError { get; }
        public abstract bool HasResults { get; }
        public abstract void ClearResults();
        public abstract bool NeedsUpdate { get; }
        public abstract string Error { get; }
        public abstract ArgsBase UntypedArgs { get; }

        public abstract bool Run( Core core, ProgressReporter prog );

        public abstract void Dispose();
    }

    interface IConfigurationBase<TArgs>
    {
        TArgs Args { get; set; }
    }
}
