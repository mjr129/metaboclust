using System.Collections.Generic;
using System.Linq;

namespace MetaboliteLevels.Utilities
{
    /// <summary>
    /// Counts stuff.
    /// </summary>
    class Counter<T>
    {
        public Dictionary<T, int> Counts = new Dictionary<T, int>();

        /// <summary>
        /// Adds 1 to the count of item.
        /// </summary>
        public void Increment(T item)
        {
            int c;

            if (Counts.TryGetValue(item, out c))
            {
                Counts[item] = c + 1;
            }
            else
            {
                Counts.Add(item, 1);
            }
        }

        internal KeyValuePair<T, int> FindHighest()
        {
            KeyValuePair<T, int> highest = new KeyValuePair<T, int>(default(T), 0);

            foreach (var kvp in Counts)
            {
                if (kvp.Value > highest.Value)
                {
                    highest = kvp;
                }
            }

            return highest;
        }
    }
}
