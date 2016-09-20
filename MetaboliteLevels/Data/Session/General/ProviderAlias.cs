using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MetaboliteLevels.Controls.Lists;
using MetaboliteLevels.Data.Algorithms.Definitions.Configurations;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.Singular;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Data.Session.General
{
    internal enum EProviderAlias
    {
        LastCorrection,
        LastTrend,
        User,
    }

    [Name( "Alias" )]
    [Serializable]
    internal class ProviderAlias : Visualisable, IMatrixProvider
    {
        private readonly Core _core;
        private readonly EProviderAlias _source;
        private  WeakReference<IMatrixProvider> _userTarget;

        public ProviderAlias( Core core, EProviderAlias source, IMatrixProvider userTarget )
        {
            this._core = core;
            this._source = source;
            this._userTarget = new WeakReference<IMatrixProvider>(userTarget);
        }             

        /// <summary>
        /// Returns the target matrix, or null if has expired or unavailable.
        /// </summary>
        public IMatrixProvider Target
        {
            get
            {
                switch (_source)
                {
                    case EProviderAlias.LastCorrection:
                        return _core.AllCorrections.WhereEnabled().LastOrDefault();
                    case EProviderAlias.LastTrend:
                        return _core.AllTrends.WhereEnabled().LastOrDefault();
                    case EProviderAlias.User:
                        return _userTarget.GetTarget();
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            set
            {
                if (_source == EProviderAlias.User)
                {
                    _userTarget = new WeakReference<IMatrixProvider>( value );
                }
                else
                {
                    throw new InvalidOperationException( $"Attempt to set {nameof( Target )} on a {nameof( ProviderAlias )} when the {nameof( _source )} is {_source.ToUiString()}." );
                }
            }
        }

        [CanBeNull]
        public IntensityMatrix Provide => Target?.Provide;

        public override string DisplayName => "*" + OverrideDisplayName + " - " + DefaultDisplayName;

        [XColumn("Current target", EColumn.Visible)]
        public override string DefaultDisplayName => Target?.ToString() ?? "Unavailable".ToBold();

        public override UiControls.ImageListOrder Icon => UiControls.ImageListOrder.Matrix;
    }
}
