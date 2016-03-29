using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaboliteLevels.Forms.Generic
{
    class Chaperone<T>
    {
        public T Item;

        public Chaperone( T item )
        {
            this.Item = item;
        }

        public Chaperone()
        {
            // N/A
        }

        public bool IsNull => Item == null;
    }
}
