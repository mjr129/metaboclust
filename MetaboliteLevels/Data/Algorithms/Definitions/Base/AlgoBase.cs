using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Controls.Lists;
using MetaboliteLevels.Data.Algorithms.General;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.Singular;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Data.Algorithms.Definitions.Base
{                  
    /// <summary>
    /// Base class for all algorithms
    /// (an algorithm is the bit that does the work, it is generally immutable to the user)
    /// </summary>
    internal abstract class AlgoBase : Visualisable
    {
        /// <summary>
        /// ID used to refer to the algorithm in save-data
        /// </summary>
        [XColumn]
        public readonly string Id;         

        /// <summary>
        /// Details of expected parameters passed into the algorithm
        /// </summary>
        private AlgoParameterCollection _parameters;

        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public AlgoBase(string id, string name)
        {
            Id = id;
            OverrideDisplayName = name;
        }                                                

        public AlgoParameterCollection Parameters
        {
            get
            {
                if (_parameters == null)
                {
                    _parameters = CreateParamaterDesription();
                    Debug.Assert(_parameters != null);
                }

                return _parameters;
            }
        }

        protected abstract AlgoParameterCollection CreateParamaterDesription();              
   
        public override string DefaultDisplayName => Id;

        public override EPrevent SupportsHide => EPrevent.Hide;

        /// <summary>
        /// Script (if any)
        /// </summary>
        public virtual RScript Script => null;

        /// <summary>
        /// Implements IVisualisable
        /// </summary>              
        public override UiControls.ImageListOrder Icon
        {
            get
            {
                return Script != null ? UiControls.ImageListOrder.ScriptFile
                           : UiControls.ImageListOrder.ScriptInbuilt;
            }
        }

        /// <summary>
        /// Implements IVisualisable
        /// </summary>              
       public override void GetXColumns(ColumnCollection list, Core core)
        {
            var result = list.Cast<AlgoBase>();
                                               
            result.Add("R", z => z.Script != null ? 1 : 0);
            result.Add("File", z => z.Script?.FileName);
        }
    }
}
