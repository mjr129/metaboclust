using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGui.Helpers;

namespace MetaboliteLevels.Data.Session.General
{
    /// <summary>
    /// Types of IVisualisable.
    /// </summary>
    enum EVisualClass
    {
        None,

        [Name( "Information" )]
        SpecialAll,

        [Name( "Statistics" )]
        SpecialStatistic,

        [Name( "Meta-data" )]
        SpecialMeta,

        [Name( "Internal data" )]
        SpecialAdvanced,

        Peak,
        Cluster,
        Compound,
        Adduct,
        Pathway,
        Assignment,
        Annotation,
    }
}
