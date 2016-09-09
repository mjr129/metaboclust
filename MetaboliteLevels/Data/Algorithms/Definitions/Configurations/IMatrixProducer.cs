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
    internal interface IProvider<out T>
    {
        T Provide { get; }
    }      
}
