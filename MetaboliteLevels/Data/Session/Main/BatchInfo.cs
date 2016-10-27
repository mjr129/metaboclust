using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Session.General;
using MSerialisers;

namespace MetaboliteLevels.Data.Session.Main
{          
    /// <summary>
    /// Batch information
    /// </summary>
    [Serializable]
    [DeferSerialisation]
    internal sealed class BatchInfo : GroupInfoBase
    {
        public BatchInfo( string batchId, int order, Range acqusitionRange, string name, string shortName, int displayPriority )
            : base( batchId, order, acqusitionRange, name, shortName, displayPriority )
        {
            // NA
        }

        public override string DefaultDisplayName
        {
            get
            {
                return this.Id;
            }
        }
    }
}
