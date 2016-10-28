using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Algorithms.Definitions.Base;
using MGui.Helpers;

namespace MetaboliteLevels.Data.Algorithms.General
{
    /// <summary>
    /// Collection of statistical methods identifiable by ID.
    /// </summary>
    class AlgoCollection<T> : IEnumerable<T>
        where T : AlgoBase
    {
        private readonly Dictionary<string, T> _collection = new Dictionary<string, T>();

        public T Add(T stat)
        {
            this._collection.Add(stat.Id, stat);
            return stat;
        }

        public void AddRange(IEnumerable<T> stats)
        {
            foreach (T stat in stats)
            {
                this._collection.Add(stat.Id, stat);
            }
        }

        public T Get(string id)
        {
            DebugHelper.NotifySlowFunction();

            T v;

            if (id == null)
            {
                return null;
            }

            if (this._collection.TryGetValue(id, out v))
            {
                return v;
            }

            return null;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return this._collection.Values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this._collection.Values.GetEnumerator();
        }

        public T[] ToArray()
        {
            return this._collection.Values.ToArray();
        }

        internal void Clear()
        {
            this._collection.Clear();
        }
    }
}
