using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Session.Associational;
using MGui.Helpers;

namespace MetaboliteLevels.Data.Algorithms.Definitions.Configurations
{
    internal interface IMatrixProducer
    {
        IntensityMatrix Product { get; }
    }

    internal class MatrixProducer
    {
        private readonly WeakReference<IMatrixProducer> _im;

        public IntensityMatrix Product => _im.GetTarget().Product;

        public MatrixProducer( IMatrixProducer im )
        {
            _im = new WeakReference<IMatrixProducer>( im );
        }

        public override bool Equals( object obj )
        {
            return Equals( obj as MatrixProducer );
        }

        protected bool Equals( MatrixProducer other )
        {
            if (other == null)
            {
                return false;
            }

            object mine = _im.GetTarget();
            object theirs = other._im.GetTarget();

            if (mine == null || theirs == null)
            {
                // Can't guarantee they were the same
                return false;
            }

            return Equals( mine, theirs );
        }

        public override int GetHashCode()
        {
            return _im.GetHashCode();
        }
    }
}
