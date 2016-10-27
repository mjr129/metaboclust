using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Algorithms.Definitions.Base.Misc;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Gui.Controls.Lists;
using MetaboliteLevels.Properties;
using MGui.Datatypes;
using MGui.Helpers;

namespace MetaboliteLevels.Data.Session.Main
{
    [Name("Original data")]
    [Serializable]
    internal class OriginalData : Visualisable, IMatrixProvider
    {
        [XColumn("File")]
        private readonly string _fileName;
        private readonly IntensityMatrix _intentisyMatrix;

        public OriginalData( string name, string fileName, IntensityMatrix intentisyMatrix )
        {
            this.OverrideDisplayName = name;
            this._fileName = fileName;
            this._intentisyMatrix = intentisyMatrix;
        }

        public IntensityMatrix Provide => this._intentisyMatrix;
        public override string DefaultDisplayName => Path.GetFileName( this._fileName );

        public override Image Icon => Resources.ListIconResultOriginal;
        public ISpreadsheet ExportData()
        {
            return this._intentisyMatrix?.ExportData();
        }
    }
}
