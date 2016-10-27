using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGui.Datatypes;
using MGui.Helpers;

namespace MetaboliteLevels.Data.Session.General
{
    [Serializable]
    internal struct AxisRange
    {
        /// <summary>
        /// Scaling mode
        /// </summary>
        public EAxisRange Mode;

        /// <summary>
        /// Value when <see cref="Mode"/> = <see cref="EAxisRange.Fixed"/>.
        /// </summary>
        public double Value;

        /// <summary>
        /// CONSTRUCTOR
        /// </summary> 
        public AxisRange( EAxisRange mode, double value )
        {
            this.Mode = mode;
            this.Value = value;
        }

        /// <summary>
        /// CONSTRUCTOR, reversable, see <see cref="ToString"/>.
        /// </summary> 
        public AxisRange( string fromText )
        {
            if (fromText == null)
            {
                throw new ArgumentNullException( nameof( fromText ) );
            }

            if (fromText == EAxisRange.Automatic.ToUiString())
            {
                this.Mode = EAxisRange.Automatic;
                this.Value = 0;
            }
            else if (fromText == EAxisRange.General.ToUiString())
            {
                this.Mode = EAxisRange.General;
                this.Value = 0;
            }
            else
            {
                this.Mode = EAxisRange.Automatic;
                this.Value = double.Parse( fromText );
            }
        }

        /// <summary>
        /// ToString (reversable)
        /// </summary> 
        public override string ToString()
        {
            switch (this.Mode)
            {
                case EAxisRange.Fixed:
                    return this.Value.ToString();

                default:
                    return this.Mode.ToUiString();
            }
        }

        public double? GetValue()
        {
            switch (this.Mode)
            {
                case EAxisRange.Automatic:
                case EAxisRange.General:
                    return null;

                case EAxisRange.Fixed:
                    return this.Value;

                default:
                    throw new SwitchException( this.Mode );
            }
        }
    }
}
