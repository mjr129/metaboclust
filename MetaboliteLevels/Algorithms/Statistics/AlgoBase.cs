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

    abstract class AlgoBase : IVisualisable
    {
        public readonly string Id;
        public readonly string Name;
        public string Description { get; set; }
        private AlgoParameterCollection _parameters;

        public AlgoBase(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
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

        VisualClass IVisualisable.VisualClass => VisualClass.None;
        string ITitlable.DisplayName => Name;

        string ITitlable.DefaultDisplayName => Name;

        string ITitlable.OverrideDisplayName
        {
            get
            {
                return null;
            }
            set
            {
                // No action
            }
        }

        string ITitlable.Comment
        {
            get
            {
                return null;
            }
            set
            {
                // No action
            }
        }

        bool ITitlable.Enabled
        {
            get
            {
                return true;
            }
            set
            {
                // No action
            }
        }

        /// <summary>
        /// Script, if any
        /// </summary>
        public virtual RScript Script
        {
            get { return null; }
        }

        public bool IsMathDotNet { get; protected set; }

        UiControls.ImageListOrder IVisualisable.GetIcon()
        {
            return Script != null ? UiControls.ImageListOrder.ScriptFile
                : IsMathDotNet ? UiControls.ImageListOrder.ScriptMathDotNet
                : UiControls.ImageListOrder.ScriptInbuilt;
        }

        void IVisualisable.RequestContents(ContentsRequest list)
        {
            // NA
        }

        IEnumerable<Column> IVisualisable.GetColumns(Core core)
        {
            List<Column<AlgoBase>> result = new List<Column<AlgoBase>>();

            result.Add("Name", EColumn.Visible, z => z.Name);
            result.Add("ID", z => z.Id);
            result.Add("R", z => z.Script != null ? 1 : 0);
            result.Add("File", z => z.Script?.FileName);

            return result;
        }
    }
}
