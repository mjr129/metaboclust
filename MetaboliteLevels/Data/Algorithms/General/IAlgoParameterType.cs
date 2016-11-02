using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JetBrains.Annotations;
using MetaboliteLevels.Data.Algorithms.Definitions.Base.Misc;
using MetaboliteLevels.Data.Session.General;
using RDotNet;

namespace MetaboliteLevels.Data.Algorithms.General
{
    /// <summary>
    /// Represents a type of parameter requested for an algorithm.
    /// </summary>
    internal interface IAlgoParameterType
    {
        /// <summary>
        /// Converts text (from user input) to the parameter type.
        /// </summary>                                            
        /// <returns>Result, or null on failure.</returns>
        [CanBeNull]
        object FromString( FromStringArgs args );

        /// <summary>
        /// Opens a GUI browse to select a value for the parameter type.
        /// </summary>                                                  
        /// <returns>Result, or null if cancelled.</returns>
        [CanBeNull]
        object Browse( Form owner, Core core, object sel );

        /// <summary>
        /// Creates an R symbol in <paramref name="rEngine"/> with the specified <paramref name="name"/> and <paramref name="value"/>.
        /// </summary>                                    
        void SetSymbol( REngine rEngine, string name, object value );

        /// <summary>
        /// Obtains the name of this type, used in user R scripts.
        /// </summary>
        [NotNull]
        string Name { get; }

        /// <summary>
        /// Things the user can call this type.
        /// </summary>
        [NotNull]
        IEnumerable<string> Aliases { get; }

        /// <summary>
        /// Obtains tracking details (i.e. pointers to the current results held by objects).
        /// See <see cref="SourceTracker"/> for details.
        /// </summary>
        /// <param name="param">Parameter value to track</param>
        /// <returns>Null, or one or more references which will be used to test for changes to this parameter. Note: Use NULL for paramters that are actually NULL, not WeakReference(null), since it is null may be interpreted as an expired reference.</returns>
        [CanBeNull]
        WeakReference[] TrackChanges( object param );

        string TryToString( object x );
    }
}
