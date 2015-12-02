using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaboliteLevels.Utilities
{
    class ComparisonComparer<T> : IComparer<T>
    {
        private Comparison<T> order;

        public ComparisonComparer(Comparison<T> order)
        {
            this.order = order;
        }

        public int Compare(T x, T y)
        {
            return order(x, y);
        }
    }
}
