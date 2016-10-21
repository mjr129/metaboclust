using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaboliteLevels.Types.General
{
    /// <summary>
    /// A range from Min --> Max
    /// </summary>
    [Serializable]
    struct Range : IEnumerable<int>
    {
        public static Range MaxValue { get { return new Range(int.MinValue, int.MaxValue); } }

        public int Min;
        public int Max;

        public Range(int min, int max)
        {
            Min = min;
            Max = max;
        }

        public int Count
        {
            get { return (Max - Min) + 1; }
        }

        internal bool Contains(int p)
        {
            return p >= Min && p <= Max;
        }

        public bool IsMaxValue
        {
            get
            {
                return Min == int.MinValue && Max == int.MaxValue;
            }
        }

        public Range ExpandOrStart(int value)
        {
            if (IsMaxValue)
            {
                return new Range(value, value);
            }
            else
            {
                return Expand(value);
            }
        }

        public Range Expand(int value)
        {
            return new Range(
                 this.Min > value ? value : this.Min,
                 this.Max < value ? value : this.Max);
        }

        public Range Compress(Range range)
        {
            return new Range(
                this.Min < range.Min ? range.Min : this.Min,
                this.Max > range.Max ? range.Max : this.Max);
        }

        public Range Compress(int min, int max)
        {
            return new Range(
                this.Min < min ? min : this.Min,
                this.Max > max ? max : this.Max);
        }

        public override string ToString()
        {
            return Min + " → " + Max;
        }        

        public IEnumerator<int> GetEnumerator()
        {
            return Enumerable.Range(Min, Max - Min + 1).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static Range Find( IEnumerable<int> values)
        {
            int max = int.MinValue;
            int min = int.MaxValue;

            foreach (var i in values)
            {
                if (i > max)
                {
                    max = i;
                }

                if (i < min)
                {
                    min = i;
                }
            }

            return new Range( min, max );
        }
    }
}
