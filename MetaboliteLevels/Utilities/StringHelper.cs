using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Viewers.Lists;

namespace MetaboliteLevels.Utilities
{
    /// <summary>
    /// Helper functions for strings.
    /// </summary>
    static class StringHelper
    {
        /// <summary>
        /// (MJR) Converts an string to small caps.
        /// </summary>
        public static string ToSmallCaps(this string x)
        {
            //string caps = "ᴀʙᴄᴅᴇꜰɢʜɪᴊᴋʟᴍɴᴏᴘʀꜱᴛᴜᴠᴡʏᴢ";
            string caps = "ᴀʙᴄᴅᴇғɢʜɪᴊᴋʟᴍɴᴏᴘǫʀsᴛᴜᴠᴡxʏᴢ";
            char[] result = new char[x.Length];

            for (int n = 0; n < x.Length; n++)
            {
                char c = x[n];

                if (c >= 'a' && c <= 'z')
                {
                    result[n] = caps[c - 'a'];
                }
                else if (c >= 'A' && c <= 'Z')
                {
                    result[n] = caps[c - 'A'];
                }
                else
                {
                    result[n] = c;
                }
            }

            return new string(result);
        }

        /// <summary>
        /// (MJR) Like PadRight / PadLeft but also truncates the string if it is too long.
        /// </summary>
        public static string FixWidth(this string str, int wid = 10, bool r = true)
        {
            if (str.Length > wid)
            {
                return str.Substring(0, wid);
            }
            else if (r)
            {
                return str.PadRight(wid);
            }
            else
            {
                return str.PadLeft(wid);
            }
        }

        /// <summary>
        /// (MJR) Capitalises the first letter of the string.
        /// Other characters remain unchanged.
        /// </summary>
        public static string ToSentence(this string self)
        {
            if (self == null)
            {
                return null;
            }

            if (self.Length <= 1)
            {
                return self.ToUpper();
            }

            return self.Substring(0, 1).ToUpper() + self.Substring(1);
        }

        /// <summary>
        /// (MJR) Capitalises the first letter of the string and characters after a space or punctuation.
        /// </summary>
        public static string ToTitle(this string self)
        {
            char[] r = new char[self.Length];
            bool u = true;

            for (int n = 0; n < self.Length; n++)
            {
                r[n] = u ? char.ToUpper(self[n]) : self[n];
                u = !char.IsLetterOrDigit(self[n]);
            }

            return new string(r);
        }

        /// <summary>
        /// (MJR) Formats x unless it is null or empty, in which case it returns empty.
        /// </summary>
        public static string FormatIf(this string x, string prefix = "", string suffix = "")
        {
            if (string.IsNullOrEmpty(x))
            {
                return "";
            }

            return prefix + x + suffix;
        }

        /// <summary>
        /// Converts a string to a subscript.
        /// Handles numbers only.
        /// </summary>
        internal static string ToSubScript(int value)
        {
            string txtt = value.ToString();
            byte[] txt = Encoding.ASCII.GetBytes(txtt);
            byte c0 = 0x30;
            string chars = "₀₁₂₃₄₅₆₇₈₉";
            StringBuilder sb = new StringBuilder(txtt.Length);

            foreach (byte c in txt)
            {
                char cn;

                if (c == 0x2D)
                {
                    cn = '₋';
                }
                else if (c == 0x2e)
                {
                    cn = '.';
                }
                else
                {
                    cn = chars[c - c0];
                }

                sb.Append(cn);
            }

            return sb.ToString();
        }

        /// <summary>
        /// (MJR) Splits a string about "," accounting for nested sequences using "{" and "}".
        /// </summary>
        internal static List<string> SplitGroups(this string text)
        {
            bool isInBrackets = false;
            StringBuilder current = new StringBuilder();
            char[] delimiters = { ',' };
            char[] startBracket = { '{' };
            char[] endBracket = { '}' };
            char[] ignorable = { ' ' };

            bool isNewElement = true;

            List<string> result = new List<string>();

            foreach (char c in text)
            {
                if (isInBrackets && endBracket.Contains(c))
                {
                    isInBrackets = false;
                }
                else if (!isInBrackets && delimiters.Contains(c))
                {
                    result.Add(current.ToString());
                    current.Clear();
                }
                else if (!isInBrackets && isNewElement && startBracket.Contains(c))
                {
                    isInBrackets = true;
                }
                else
                {
                    if (isNewElement && !ignorable.Contains(c))
                    {
                        isNewElement = false;
                    }

                    current.Append(c);
                }
            }

            result.Add(current.ToString());
            current.Clear();

            return result;
        }

        /// <summary>
        /// Converts a string to circled text (numbers in the range 1..20 only ).
        /// </summary>
        internal static string Circle(int value)
        {
            //string chars = "⒈⒉⒊⒋⒌⒍⒎⒏⒐⒑⒒⒓⒔⒕⒖⒗⒘⒙⒚⒛";
            string chars = "⑴⑵⑶⑷⑸⑹⑺⑻⑼⑽⑾⑿⒀⒁⒂⒃⒄⒅⒆⒇";
            //string chars = "⓪①②③④⑤⑥⑦⑧⑨⑩⑪⑫⑬⑭⑮⑯⑰⑱⑲⑳";

            if (value < 1 || value >= chars.Length)
            {
                return "(" + value + ")";
            }

            return chars[value - 1].ToString();
        }

        /// <summary>
        /// Converts a string to an array of [T], using a specified [conversion] and splitting about [sep]
        /// </summary>           
        public static T[] StringToArray<T>(string str, Converter<string, T> conversion, string sep = ",")
        {
            string[] elements = str.Split(new[] { sep }, StringSplitOptions.RemoveEmptyEntries);

            T[] result = new T[elements.Length];

            for (int i = 0; i < elements.Length; i++)
            {
                string element = elements[i].Trim();
                result[i] = conversion(element);
            }

            return result;
        }

        /// <summary>
        /// Returns a [timeSpan] as a number of minutes or seconds.
        /// </summary>                                             
        internal static string TimeAsString(TimeSpan timeSpan)
        {
            if (timeSpan.TotalMinutes > 2)
            {
                return (int)timeSpan.TotalMinutes + " minutes";
            }
            else if (timeSpan.TotalMinutes > 1)
            {
                return (int)timeSpan.TotalMinutes + " minute";
            }
            else
            {
                return (int)timeSpan.TotalSeconds + " seconds";
            }
        }

        /// <summary>
        /// Like string::join this converts an array to a string, but doesn't consider the type.
        /// </summary>                                           
        public static string ArrayToString(IEnumerable array, string delimiter = ", ")
        {               
            if (array == null)
            {
                return null;
            }

            StringBuilder sb = new StringBuilder();

            foreach (object t in array)
            {
                if (sb.Length != 0)
                {
                    sb.Append(delimiter);
                }

                if (t != null)
                {
                    sb.Append(t.ToString());
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Converts an array of integers to a string, including ranges (e.g. 2-5) where possible.
        /// </summary>
        /// <param name="list">Values</param>
        /// <param name="prefix">Prefix for each value</param>
        /// <returns>String</returns>
        public static string ArrayToStringInt(IEnumerable<int> list, string prefix = "")
        {
            if (list == null)
            {
                return null;
            }

            List<int> hs = new List<int>(list);

            hs.Sort();

            if (hs.Count == 0)
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();

            int? onHold = null;

            sb.Append(hs[0]);

            for (int i = 1; i < hs.Count; i++)
            {
                int c = hs[i];

                if (hs[i] == (hs[i - 1] + 1))
                {
                    if (!onHold.HasValue)
                    {
                        // One more!
                        onHold = hs[i - 1];
                    }
                }
                else
                {
                    if (onHold.HasValue)
                    {
                        // Sequence broken
                        int s = onHold.Value;
                        int e = hs[i - 1];
                        onHold = null;

                        WriteFromTo(sb, s, e, prefix);
                    }

                    sb.Append(", ");
                    sb.Append(prefix);
                    sb.Append(c);
                }
            }

            if (onHold.HasValue)
            {
                // Sequence broken
                int s = onHold.Value;
                int e = hs[hs.Count - 1];
                WriteFromTo(sb, s, e, prefix);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Used by [ArrayToStringInt], this writes a range (x-y), unless the range is 1 in which
        /// case it writes two individual items (x, y).
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="rangeStart"></param>
        /// <param name="rangeEnd"></param>
        /// <param name="prefix"></param>
        private static void WriteFromTo(StringBuilder sb, int rangeStart, int rangeEnd, string prefix)
        {
            if (rangeEnd == rangeStart + 1)
            {
                sb.Append(", ");
                sb.Append(prefix);
                sb.Append(rangeEnd);
            }
            else
            {
                sb.Append("-");
                sb.Append(prefix);
                sb.Append(rangeEnd);
            }
        }

        /// <summary>
        /// Undoes a [ArrayToStringInt], converting an string, possibly including ranges, to an
        /// array of integers.
        /// </summary>        
        public static List<int> StringToArrayInt(string str, EErrorHandler error)
        {
            List<int> result = new List<int>();

            foreach (string elem in str.Split(new[] { ",", ";" }, StringSplitOptions.RemoveEmptyEntries))
            {
                string elem2 = elem.Trim();

                int i = elem2.IndexOf('-', 1);

                if (i != -1)
                {
                    string[] elem3 = elem2.Split("-".ToCharArray(), 2, StringSplitOptions.RemoveEmptyEntries);

                    if (elem3.Length == 2)
                    {
                        int iS, iE;

                        if (int.TryParse(elem3[0].Trim(), out iS)
                            && int.TryParse(elem3[1].Trim(), out iE))
                        {
                            for (int iC = iS; iC <= iE; iC++)
                            {
                                result.Add(iC);
                            }
                        }
                        else
                        {
                            switch (error)
                            {
                                case EErrorHandler.Ignore:
                                    break;

                                case EErrorHandler.ReturnNull:
                                    return null;

                                case EErrorHandler.ThrowError:
                                default:
                                    throw new FormatException("Cannot parse: " + elem2);
                            }
                        }
                    }
                }
                else
                {
                    int iC;

                    if (int.TryParse(elem2, out iC))
                    {
                        result.Add(iC);
                    }
                    else
                    {
                        switch (error)
                        {
                            case EErrorHandler.Ignore:
                                break;

                            case EErrorHandler.ReturnNull:
                                return null;

                            case EErrorHandler.ThrowError:
                            default:
                                throw new FormatException("Cannot parse: " + elem2);
                        }
                    }
                }
            }

            return result;
        }
                                         
        /// <summary>
        /// Converts an array to a string
        /// </summary>                   
        public static string ArrayToString(IEnumerable array, Converter<object, string> conversion, string delimiter = ", ")
        {
            if (array == null)
            {
                return null;
            }

            StringBuilder sb = new StringBuilder();

            foreach (object t in array)
            {
                if (sb.Length != 0)
                {
                    sb.Append(delimiter);
                }

                sb.Append(conversion(t));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Converts an array to a string
        /// </summary>                   
        public static string ArrayToString<T>(IEnumerable<WeakReference<T>> array, Converter<T, string> conversion, string delimiter = ", ")
            where T : class
        {
            return ArrayToString(array, z => WeakReferenceHelper.SafeToString(z, conversion), delimiter);
        }

        /// <summary>
        /// Converts an array to a string
        /// </summary>                   
        public static string ArrayToString<T>(IEnumerable<T> array, Converter<T, string> conversion, string delimiter = ", ")
        {
            if (array == null)
            {
                return null;
            }

            StringBuilder sb = new StringBuilder();

            foreach (T t in array)
            {
                if (sb.Length != 0)
                {
                    sb.Append(delimiter);
                }

                sb.Append(conversion(t));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Splits a string into a field:value pair about a specified symbol
        /// </summary>
        /// <param name="elem">The original string --> the first part</param>
        /// <param name="splitSign">What to split about</param>
        /// <returns>The last part</returns>
        internal static string SplitEquals(ref string elem, string splitSign = "=")
        {
            int i = elem.IndexOf(splitSign);

            if (i != -1)
            {
                string result = elem.Substring(i + 1);
                elem = elem.Substring(0, i);
                return result;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Splits a string into a field:value pair about a specified symbol
        /// </summary>
        internal static bool SplitEquals(string elem, out string left, out string right, string splitSign = "=")
        {
            int i = elem.IndexOf(splitSign);

            if (i != -1)
            {
                right = elem.Substring(i + 1);
                left = elem.Substring(0, i);
                return true;
            }
            else
            {
                left = elem;
                right = null;
                return false;
            }
        }

        /// <summary>
        /// Describes a as a fraction of b (as a string).
        /// </summary>
        internal static string AsFraction(int a, int b)
        {
            return a.ToString() + " / " + b + " (" + (((double)a / b) * 100).ToString("F00") + "%)";
        }

        /// <summary>
        /// Displays a number of bytes as b, kb or Mb
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        internal static string DisplayBytes(long bytes)
        {
            if (bytes < 1024)
            {
                return bytes + "b";
            }
            else if (bytes < (1024 * 1024))
            {
                return (bytes / 1024) + "kb";
            }
            else
            {
                return (bytes / (1024 * 1024)) + "Mb";
            }
        }
    }

    /// <summary>
    /// A TypeConverter for [ParseElementCollection].
    /// Used by the "options" PropertyGrid to provide editing for [ParseElementCollection].
    /// (All this verbosity really does is converts to and from a string).
    /// </summary>
    class ParseElementCollectionConverter : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                return new ParseElementCollection((string)value);
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return ParseElementCollection.GetOriginal((ParseElementCollection)value);
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    /// <summary>
    /// Collection of 'ParseElement's. 
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(ParseElementCollectionConverter))]
    class ParseElementCollection
    {
        /// <summary>
        /// A string and whether it is in brackets.
        /// </summary>
        [Serializable]
        private class ParseElement
        {
            public readonly string Value;
            public readonly bool IsInBrackets;

            public ParseElement(string value, bool isInBrackets)
            {
                this.Value = value;
                this.IsInBrackets = isInBrackets;
            }
                                         
            public override string ToString()
            {
                if (IsInBrackets)
                {
                    return "{" + Value + "}";
                }
                else
                {
                    return Value;
                }
            }
        }

        private readonly List<ParseElement> _contents;

        /// <summary>
        /// Parses a string like
        /// abc{xyz}abc{xyz}
        /// </summary>
        public ParseElementCollection(string x)
        {
            _contents = new List<ParseElement>();
            StringBuilder sb = new StringBuilder();
            bool isOpen = false;

            // Could be more efficient but it suitable for purpose
            foreach (char c in x)
            {
                if (!isOpen && c == '{')
                {
                    if (sb.Length != 0)
                    {
                        _contents.Add(new ParseElement(sb.ToString(), false));
                    }
                    sb.Clear();
                    isOpen = true;
                }
                else if (isOpen && c == '}')
                {
                    _contents.Add(new ParseElement(sb.ToString(), true));
                    sb.Clear();
                    isOpen = false;
                }
                else
                {
                    sb.Append(c);
                }
            }

            if (sb.Length != 0)
            {
                _contents.Add(new ParseElement(sb.ToString(), isOpen));
            }
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <param name="propertyTarget">Elements {!xxx} will get the xxx property from this object, elements {xxx} will get the xxx value from QueryValue(xxx)</param>
        /// <returns>String</returns>
        public string ConvertToString(IVisualisable propertyTarget, Core core)
        {
            var r = new StringBuilder();

            foreach (var x in _contents)
            {
                if (x.IsInBrackets)
                {
                    r.Append(Column.AsString(propertyTarget.QueryProperty(x.Value, core), EListDisplayMode.Content));
                }
                else
                {
                    r.Append(x.Value);
                }
            }

            return r.ToString();
        }

        private string GetOriginal()
        {
            return string.Join(string.Empty, _contents);
        }

        public static string GetOriginal(ParseElementCollection collection)
        {
            if (collection == null)
            {
                return string.Empty;
            }

            return collection.GetOriginal();
        }

        public static bool IsNullOrEmpty(ParseElementCollection collection)
        {
            return (collection == null || collection._contents.Count == 0
                    || (collection._contents.Count == 1 && string.IsNullOrWhiteSpace(collection._contents[0].Value)));
        }
    }
}
