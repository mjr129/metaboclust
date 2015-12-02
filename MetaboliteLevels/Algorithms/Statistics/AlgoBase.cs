using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaboliteLevels.Algorithms.Statistics
{
    abstract class AlgoBase
    {
        public readonly string Id;
        public readonly string Name;
        public string Description { get; set; }

        public AlgoBase(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }

        public abstract AlgoParameters GetParams();
    }
}
