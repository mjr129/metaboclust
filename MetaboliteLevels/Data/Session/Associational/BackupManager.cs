using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaboliteLevels.Data.Session.Associational
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

            if (!this._data.ContainsKey( itemt ))
            {
                this._data.Add( itemt, new BackupData( itemt ) );
            }
        }

        public void RestoreAll()
        {
            foreach (var kvp in this._data)
            {
                kvp.Value.Restore();
            }

            this._data.Clear();
        }
    }
}
