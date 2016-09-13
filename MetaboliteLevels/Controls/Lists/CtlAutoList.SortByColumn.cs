using System;
using System.Collections;
using System.Collections.Generic;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Viewers.Lists
{
    partial class CtlAutoList
    {
        class SortByColumn : IComparer<object>
        {
            public Column col;
            public bool ascending;

            public SortByColumn(Column c, bool ascending)
            {
                this.col = c;
                this.ascending = ascending;
            }

            public int Compare(object x, object y )
            {
                if (ascending)
                {
                    return DoCompare(x, y);
                }
                else
                {
                    return -DoCompare(x, y);
                }
            }

            private int DoCompare( object x, object y )
            {
                object xv = col.GetRow(x);
                object yv = col.GetRow(y);

                // Compare nulls
                if (xv == null)
                {
                    if (yv == null)
                    {
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                }
                else if (yv == null)
                {
                    return -1;
                }

                // Compare lists using counts (or first item if counts are the same)
                if (xv is IList && yv is IList)
                {
                    var lx = ((IList)xv);
                    var ly = ((IList)yv);

                    if (lx.Count == ly.Count)
                    {
                        if (lx.Count != 0)
                        {
                            xv = lx[0];
                            yv = ly[0];
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    else
                    {
                        xv = lx.Count;
                        yv = ly.Count;
                    }
                }

                // Compare IVisualisables using names
                if (xv is IVisualisable && yv is IVisualisable)
                {
                    xv = ((IVisualisable)xv).DisplayName;
                    yv = ((IVisualisable)yv).DisplayName;
                }

                if (xv is string)
                {
                    // Compare strings using special method
                    return NativeMethods.StrCmpLogicalW((string)xv, (string)yv);  // Will fail if not of the same type
                }
                else if (xv is IComparable)
                {
                    return ((IComparable)xv).CompareTo(yv); // Will fail if not of the same type
                }
                else
                {
                    // If we get here there is an error
                    // System.Diagnostics.Debug.Assert(false);
                    return NativeMethods.StrCmpLogicalW(xv.ToString(), yv.ToString());
                }
            }
        }

        public void Clear()
        {
            DivertList( null );
        }
    }
}
