using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace MetaboliteLevels.Utilities
{
    class SwitchException : Exception
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public SwitchException(object value)
            : base("Invalid switch in or near \"" + new StackFrame(1, true).GetMethod().Name + "\": " + value)
        {
            // NA
        }
    }
}
