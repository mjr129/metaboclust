using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Types.General;
using MSerialisers;

namespace MetaboliteLevels.Data.Session.General
{
    /// <summary>
    /// Experimental group information.
    /// </summary>
    [Serializable]
    [DeferSerialisation]
    internal sealed class GroupInfo : GroupInfoBase
    {
        public GroupInfo( string groupId, int order, Range timeRange, string name, string shortName, int displayPriority )
            : base( groupId, order, timeRange, name, shortName, displayPriority )
        {
            // NA
        }

        public override string DefaultDisplayName
        {
            get
            {
                return Id;
            }
        }
    }
}
