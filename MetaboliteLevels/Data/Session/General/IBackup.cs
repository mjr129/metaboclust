using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Session.Associational;

namespace MetaboliteLevels.Data.Session.General
{
    interface IBackup
    {
        void Backup( BackupData data );
        void Restore( BackupData data );
    }
}
