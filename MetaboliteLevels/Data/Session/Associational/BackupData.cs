using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaboliteLevels.Data.Session.Associational
{
    class BackupData
    {
        private IBackup _target;
        private Queue _data = new Queue();

        public BackupData( IBackup target )
        {
            this._target = target;
            this._target.Backup( this );
        }

        public void Restore()
        {
            this._target.Restore( this );
        }

        public void Push( object x )
        {
            this._data.Enqueue( x );
        }

        public void Pull<T>( ref T x )
        {
            x = (T)this._data.Dequeue();
        }

        internal T Pull<T>()
        {
            return (T)this._data.Dequeue();
        }
    }
}
