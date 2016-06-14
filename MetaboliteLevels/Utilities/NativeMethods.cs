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
        public extern static bool DestroyIcon( IntPtr handle );

        //[DllImport("shlwapi.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        //public static extern int StrCmpLogicalW(string x, string y);

        private static Comparer _comparer = Comparer.Default;    

        public static int StrCmpLogicalW(string x, string y)
        {
            string[] xElements = Regex.Split(x, "([0-9]+)");
            string[] yElements = Regex.Split(y, "([0-9]+)");
            IEnumerable<object> xObjects = xElements.Select(ToIntIfPossible);
            IEnumerable<object> yObjects = yElements.Select(ToIntIfPossible);

            return CompareEnumerables(xObjects, yObjects);
        }

        private static int CompareEnumerables(IEnumerable<object> x, IEnumerable<object> y)
        {
            IEnumerator xi = x.GetEnumerator();
            IEnumerator yi = y.GetEnumerator();

            while (true)
            {
                bool xm = xi.MoveNext();
                bool rm = yi.MoveNext();

                if (!(xm || rm))
                {
                    return 0;
                }

                if (!xm)
                {
                    return -1;
                }

                if (!rm)
                {
                    return 1;
                }

                int compared = _comparer.Compare(xi.Current, yi.Current);

                if (compared != 0)
                {
                    return compared;
                }
            }

        }

        private static object ToIntIfPossible(string arg)
        {
            int v;

            if (int.TryParse(arg, out v))
            {
                return v;
            }

            return arg;
        }

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
