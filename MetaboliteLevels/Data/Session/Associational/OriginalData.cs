using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Algorithms.Definitions.Configurations;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Viewers.Lists;
using MGui.Helpers;

namespace MetaboliteLevels.Data.Session.Associational
{
    [Name("Original data")]
    [Serializable]
    internal class OriginalData : Visualisable, IMatrixProvider
    {
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

        public override UiControls.ImageListOrder Icon => UiControls.ImageListOrder.Matrix;

        public override IEnumerable<Column> GetColumns( Core core )
        {
            List<Column<OriginalData>> result = new List<Column<OriginalData>>();

            result.Add( "Name", EColumn.Visible, z => z.DisplayName );
            result.Add( "File", EColumn.Visible, z => z._fileName );

            return result;
        }      
    }
}
