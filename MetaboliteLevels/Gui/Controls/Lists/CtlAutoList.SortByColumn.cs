using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Session.Associational;
using MGui.Helpers;

namespace MetaboliteLevels.Gui.Controls.Lists
{
    partial class CtlAutoList
    {
        class SortByColumn : IComparer<object>
        {
            public Column _col;
            public bool _ascending;

            public SortByColumn(Column c, bool ascending)
            {
                this._col = c;
                this._ascending = ascending;
            }

            public int Compare(object x, object y )
            {
                if (this._ascending)
                {
                    return this.DoCompare(x, y);
                }
                else
                {
                    return -this.DoCompare(x, y);
                }
            }

            private int DoCompare( object x, object y )
            {
                object xv = this._col.GetRow(x);
                object yv = this._col.GetRow(y);

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
                if (xv is Visualisable && yv is Visualisable)
                {
                    xv = ((Visualisable)xv).DisplayName;
                    yv = ((Visualisable)yv).DisplayName;
                }

                if (xv is string)
                {
                    // Compare strings using special method
                    return StringHelper.StrCmpLogicalW((string)xv, (string)yv);  // Will fail if not of the same type
                }
                else if (xv is IComparable)
                {
                    return ((IComparable)xv).CompareTo(yv); // Will fail if not of the same type
                }
                else
                {
                    // If we get here there is an error
                    // System.Diagnostics.Debug.Assert(false);
                    return StringHelper.StrCmpLogicalW(xv.ToString(), yv.ToString());
                }
            }
        }

        public void Clear()
        {
            this.DivertList( null );
        }
    }
}
