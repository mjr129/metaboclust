using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Controls.Lists;
using MetaboliteLevels.Data.Algorithms.Definitions.Configurations;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Data.Session.Associational
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
            OverrideDisplayName = name;
            _fileName = fileName;
            _intentisyMatrix = intentisyMatrix;
        }

        public IntensityMatrix Provide => _intentisyMatrix;
        public override string DefaultDisplayName => Path.GetFileName( _fileName );

        public override Image Icon => Resources.ListIconTestFull;     
    }
}
