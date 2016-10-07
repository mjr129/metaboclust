using System;
using System.Linq;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Collections;

namespace MetaboliteLevels.Utilities
{
    /// <summary>
    /// Windows API.
    /// </summary>
    static class NativeMethods
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool Beep(uint dwFreq, uint dwDuration);

        [System.Runtime.InteropServices.DllImport( "user32.dll", CharSet = CharSet.Auto )]
        public static extern bool DestroyIcon( IntPtr handle );

        //[DllImport("shlwapi.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        //public static extern int StrCmpLogicalW(string x, string y);

       

        [StructLayout( LayoutKind.Sequential )]
        public struct MARGINS
        {
            public int Left;
            public int Right;
            public int Top;
            public int Bottom;
        }

        [DllImport( "dwmapi.dll", PreserveSig = false )]
        public static extern void DwmExtendFrameIntoClientArea( IntPtr hwnd, ref MARGINS margins );

        [DllImport( "dwmapi.dll", PreserveSig = false )]
        public static extern bool DwmIsCompositionEnabled();
    }
}
