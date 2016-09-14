using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Viewers.Lists;

namespace MetaboliteLevels.Algorithms.Statistics
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
       public override IEnumerable<Column> GetColumns(Core core)
        {
            List<Column<AlgoBase>> result = new List<Column<AlgoBase>>();

            result.Add("Name", EColumn.Visible, z => z.DisplayName);
            result.Add("ID", z => z.Id);
            result.Add("R", z => z.Script != null ? 1 : 0);
            result.Add("File", z => z.Script?.FileName);

            return result;
        }
    }
}
