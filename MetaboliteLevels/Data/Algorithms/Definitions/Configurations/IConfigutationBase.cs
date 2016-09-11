using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Viewers.Lists;

namespace MetaboliteLevels.Data.Algorithms.Definitions.Configurations
{
    /// <summary>
    /// All classes implementing this interface also implement <see cref="ConfigurationBase"/>, so see that class for details.
    /// </summary>           
    internal interface IConfigurationBase : IDisposable, IVisualisable
    {
        bool HasError { get; }        
        string AlgoName { get; }
        string ArgsToString { get; }
        bool HasResults { get; }    
        void ClearResults();
        bool NeedsUpdate { get; }
        string Error { get; }
        bool Run( Core core, ProgressReporter prog );
        bool CheckIsAvailable();
    }
}
