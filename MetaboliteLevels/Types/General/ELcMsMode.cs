using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Data.General
{
    /// <summary>
    /// LC-MS modes.
    /// </summary>
    public enum ELcmsMode
    {
        /// <summary>
        /// Negative ion
        /// </summary>
        [Name("Negative mode LC-MS (-)")]
        Negative = -1,

        /// <summary>
        /// Mixed (expects mode for each peak in peak information file)
        /// </summary>
        [Name("Mixed mode LC-MS (+/-)")]
        Mixed = 0,

        /// <summary>
        /// Positive ion
        /// </summary>
        [Name("Positive mode LC-MS (+)")]
        Positive = 1,

        /// <summary>
        /// None (NMR or ignore)
        /// </summary>
        [Name("Other")]
        None = 2,
    }        
}
