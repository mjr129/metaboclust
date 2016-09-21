using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaboliteLevels.Data.Session.General
{
    class BackupData
    {
        private IBackup _target;
        private Queue _data = new Queue();

        public BackupData( IBackup target )
        {
            _target = target;
            _target.Backup( this );
        }

        public void Restore()
        {
            _target.Restore( this );
        }

        public void Push( object x )
        {
            _data.Enqueue( x );
        }

        public void Pull<T>( ref T x )
        {
            x = (T)_data.Dequeue();
        }

        internal T Pull<T>()
        {
            return (T)_data.Dequeue();
        }
    }
}
