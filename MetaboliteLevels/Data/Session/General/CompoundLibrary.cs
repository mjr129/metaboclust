using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaboliteLevels.Data.Session.General
{
    [Serializable]
    class CompoundLibrary
    {
        public readonly string Name;
        public readonly string PathwayToolsFolder;
        public readonly string CompoundFile;
        public readonly string PathwayFile;

        public CompoundLibrary(string name, string pathwayToolsFolder)
        {
            Name = name;
            PathwayToolsFolder = pathwayToolsFolder;
        }

        public CompoundLibrary(string name, string compoundCsv, string pathwayCsv)
        {
            Name = name;
            CompoundFile = compoundCsv;
            PathwayFile = pathwayCsv;
        }

        /// <summary>
        /// Used in lists
        /// </summary>
        public override string ToString()
        {
            return Name;
        }

        public bool ContentsMatch(CompoundLibrary cl)
        {
            return PathwayToolsFolder == cl.PathwayToolsFolder && CompoundFile == cl.CompoundFile && PathwayFile == cl.PathwayFile;
        }
    }
}
