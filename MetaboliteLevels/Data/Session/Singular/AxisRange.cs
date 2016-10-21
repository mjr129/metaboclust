using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGui.Helpers;

namespace MetaboliteLevels.Data.Session.Singular
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
            Mode = mode;
            Value = value;
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
                Mode = EAxisRange.Automatic;
                Value = 0;
            }
            else if (fromText == EAxisRange.General.ToUiString())
            {
                Mode = EAxisRange.General;
                Value = 0;
            }
            else
            {
                Mode = EAxisRange.Automatic;
                Value = double.Parse( fromText );
            }
        }

        /// <summary>
        /// ToString (reversable)
        /// </summary> 
        public override string ToString()
        {
            switch (Mode)
            {
                case EAxisRange.Fixed:
                    return Value.ToString();

                default:
                    return Mode.ToUiString();
            }
        }
    }
}
