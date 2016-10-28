using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Gui.Controls.Lists;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Data.Session.Associational
{
  

    /// <summary>
    /// Stuff that shows in lists.
    /// </summary>
    [Serializable]
    internal abstract class Visualisable : IColumnProvider , IIconProvider
    {
        /// <summary>
        /// Displayed name of this item.
        /// </summary>
        [XColumn("Name", EColumn.Visible)]
        public virtual string DisplayName
        {
            get
            {
                if (string.IsNullOrEmpty( OverrideDisplayName ))
                {
                    return DefaultDisplayName;
                }
                else
                {
                    return OverrideDisplayName;
                }
            }
        }

        public sealed override string ToString() => DisplayName;

        /// <summary>
        /// Assigned name of this item.
        /// </summary>                               
        public abstract string DefaultDisplayName { get; }

        /// <summary>
        /// User overriden name of this item
        /// </summary>                               
        public virtual string OverrideDisplayName { get; set; }

        /// <summary>
        /// Comments applied to this item.
        /// </summary>
        [XColumn]
        public virtual string Comment { get; set; }

        /// <summary>
        /// Is this object hidden from view
        /// </summary>   
        public virtual bool Hidden { get; set; }

        /// <summary>
        /// Icon for this item (as an index).
        /// </summary>
        public abstract Image Icon { get; }

        /// <summary>
        /// STATIC
        /// Gets columns
        /// </summary>
        public virtual void GetXColumns( CustomColumnRequest request )
        {                                                   
        }

        public virtual EPrevent SupportsHide => EPrevent.None;       
    }       

    [Flags]
    enum EPrevent
    {
        None,
        Hide = 1,
        Comment = 2,
        Name = 4,
    }
}
