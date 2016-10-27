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
                return this.Id;
            }
        }
    }
}
