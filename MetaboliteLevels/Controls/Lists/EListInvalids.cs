using System;

namespace MetaboliteLevels.Viewers.Lists
{
    /// <summary>
    /// Indicates what has changed inside a list
    /// </summary>
    [Flags]
    enum EListInvalids
    {
        /// <summary>
        /// Nothing
        /// </summary>
        None = 0,

        // Use by listview only
        _SourceChanged = 1,
        _ColumnsChanged = 2,
        _ColumnVisibilitiesChanged = 4,
        _ContentsChanged = 8,
        _ValuesChanged = 16,

        /// <summary>
        /// The source list.
        /// </summary>
        SourceChanged = _SourceChanged | _ColumnsChanged | _ColumnVisibilitiesChanged | _ContentsChanged | _ValuesChanged,

        /// <summary>
        /// Item values (not the items themselves)
        /// </summary>
        ValuesChanged = _ValuesChanged,

        /// <summary>
        /// Which items (the items themselves)
        /// </summary>
        ContentsChanged = _ContentsChanged | _ValuesChanged,

        /// <summary>
        /// Visible columns
        /// </summary>
        ToggleColumn = _ColumnVisibilitiesChanged | _ContentsChanged | _ValuesChanged,

        /// <summary>
        /// Previews
        /// </summary>
        PreviewsChanged = _ValuesChanged,

        /// <summary>
        /// Order of items
        /// </summary>
        Sorted = _SourceChanged | _ColumnVisibilitiesChanged,

        /// <summary>
        /// Filter on items
        /// </summary>
        Filter = _SourceChanged | _ColumnVisibilitiesChanged,
    }
}
