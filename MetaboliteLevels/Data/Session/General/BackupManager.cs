using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Session.Associational;

namespace MetaboliteLevels.Data.Session.General
{
    class BackupManager
    {
        private Dictionary<IBackup, BackupData> _data = new Dictionary<IBackup, BackupData>();

        public void Backup( object item, string reason )
        {
            var itemt = item as IBackup;

            if (itemt == null)
            {
                return;
            }

            if (!_data.ContainsKey( itemt ))
            {
                _data.Add( itemt, new BackupData( itemt ) );
            }
        }

        public void RestoreAll()
        {
            foreach (var kvp in _data)
            {
                kvp.Value.Restore();
            }

            _data.Clear();
        }
    }
}
