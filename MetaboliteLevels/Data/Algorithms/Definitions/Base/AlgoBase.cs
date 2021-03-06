﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Algorithms.General;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Gui.Controls.Lists;
using MetaboliteLevels.Properties;
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

        /// <summary>
        /// Script (if any)
        /// </summary>
        public virtual RScript Script => null;

        /// <summary>
        /// Implements IVisualisable
        /// </summary>              
        public override Image Icon
        {
            get
            {
                return Script != null ? Resources.IconR : Resources.IconBinary;
            }
        }

        /// <summary>
        /// Implements IVisualisable
        /// </summary>              
        public override void GetXColumns( CustomColumnRequest request )
        {
            var result = request.Results.Cast<AlgoBase>();

            result.Add( "R", z => z.Script != null ? 1 : 0 );
            result.Add( "File", z => z.Script?.FileName );
        }             
    }
}
