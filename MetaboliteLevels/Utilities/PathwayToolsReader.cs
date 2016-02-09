using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaboliteLevels.Utilities
{
    /// <summary>
    /// Reads data from a PathwayTools database
    /// (see http://bioinformatics.ai.sri.com/ptools/flatfile-format.html).
    /// </summary>
    class PathwayToolsReader : IDisposable
    {
        private const string END_OF_ELEMENT = "//";
        private const string CONCANTENATE_LINE = "/";
        private const string COMMENT = "#";
        private const string KEYVALUEPAIR = " - ";

        private readonly StreamReader _sr;
        private readonly string _fn;
        private int _ln;

        public PathwayToolsReader(string fn, ProgressReporter prog)
        {
            _fn = fn;
            FileStream fs = File.OpenRead(fn);
            ProgressStream ps = new ProgressStream(fs, prog);
            _sr = new StreamReader(ps);
        }

        public void Dispose()
        {
            _sr.Dispose();
        }

        public bool EndOfStream
        {
            get { return _sr.EndOfStream; }
        }

        public Entity ReadNext()
        {
            if (_sr.EndOfStream)
            {
                return null;
            }

            int ls = _ln + 1;

            List<Tuple<string, StringBuilder>> contents = new List<Tuple<string, StringBuilder>>();
            Tuple<string, StringBuilder> latestValue = null;
            string[] delim = new string[] { KEYVALUEPAIR };

            while (true)
            {
                if (_sr.EndOfStream)
                {
                    // End of stream
                    break;
                }

                _ln++;
                string l = _sr.ReadLine();

                if (l == END_OF_ELEMENT)
                {
                    // End of element
                    break;
                }
                else if (l.StartsWith(CONCANTENATE_LINE))
                {
                    // Concatenate to previous
                    latestValue.Item2.AppendLine();
                    latestValue.Item2.Append(l.Substring(CONCANTENATE_LINE.Length));
                }
                else if (l.StartsWith(COMMENT) || string.IsNullOrWhiteSpace(l))
                {
                    // Comment
                }
                else if (l.Contains(KEYVALUEPAIR))
                {
                    // Key value pair
                    string[] e = l.Split(delim, 2, StringSplitOptions.None);

                    string key = e[0];
                    StringBuilder value = new StringBuilder();
                    value.Append(e[1]);

                    latestValue = new Tuple<string, StringBuilder>(key, value);
                    contents.Add(latestValue);
                }
                else if (latestValue != null && latestValue.Item1 == "COMMENT")
                {
                    // Several files I've downloaded contain a corruption whereby occasional comment lines aren't preceded by CONCANTENATE_LINE
                    // We can try to rectify this rather than simply bail out
                    latestValue.Item2.AppendLine();
                    latestValue.Item2.Append(l);

                    Debug.WriteLine("PathwayToolsReader is attempting to deal with an error in the PathwayTools database file.\r\n"
                                    + "It is assumed that an extended comment line should have been preceded with the concatenate line symbol \"/\".\r\n"
                                    + "Please check the database file for errors to remove this warning.\r\n"
                                    + "- Line: " + _ln + "\r\n"
                                    + "- File: \"" + _fn + "\"\r\n"
                                    + "- Data \"" + l + "\"");
                }
                else
                {
                    throw new FormatException("PathwayToolsReader encountered an error in the PathwayTools database file.\r\n"
                                  + "Please check the database file for errors to remove this warning.\r\n"
                                  + "- Line: " + _ln + "\r\n"
                                  + "- File: \"" + _fn + "\"\r\n"
                                  + "- Data \"" + l + "\"");
                }
            }

            return new Entity(this, contents, ls, _ln);
        }

        public class Entity
        {
            private readonly Dictionary<string, List<string>> _contents;
            private readonly PathwayToolsReader _owner;
            private readonly int _firstLine;
            private readonly int _lastLine;

            /// <summary>
            /// Ctor.
            /// </summary>
            public Entity(PathwayToolsReader owner, List<Tuple<string, StringBuilder>> contents, int firstLine, int lastLine)
            {
                this._owner = owner;
                this._firstLine = firstLine;
                this._lastLine = lastLine;
                this._contents = new Dictionary<string, List<string>>();

                foreach (var kvp in contents)
                {
                    List<string> value;

                    if (!_contents.TryGetValue(kvp.Item1, out value))
                    {
                        value = new List<string>();
                        _contents.Add(kvp.Item1, value);
                    }

                    value.Add(kvp.Item2.ToString());
                }
            }

            /// <summary>
            /// Gets the element for the key or throws an exception
            /// </summary>
            public string GetFirst(string key)
            {
                string result = GetFirst(key, null);

                if (result == null)
                {
                    throw ThrowKeyNotFound(key);
                }

                return result;
            }

            /// <summary>
            /// Gets the element for the key or throws an exception
            /// </summary>
            public string GetFirst(string key, string @default)
            {
                List<string> value;

                if (_contents.TryGetValue(key, out value))
                {
                    return value[0];
                }

                return @default;
            }

            /// <summary>
            /// Returns a descriptive keynotfound exception.
            /// </summary>
            private KeyNotFoundException ThrowKeyNotFound(string key)
            {
                return new KeyNotFoundException("PathwayToolsReader didn't find the key \"" + key + "\" in the entity defined in file \""
                                                    + _owner._fn + "\" between lines " + _firstLine + " and " + _lastLine);
            }

            /// <summary>
            /// Gets the element(s) for the key or throws an exception
            /// </summary>
            public List<string> GetAll(string key)
            {
                List<string> value = TryGetAll(key);

                if (value == null)
                {
                    throw ThrowKeyNotFound(key);
                }

                return value;
            }

            /// <summary>
            /// Gets the element(s) for the key or returns null.
            /// </summary>
            public List<string> TryGetAll(string key)
            {
                List<string> value;

                if (_contents.TryGetValue(key, out value))
                {
                    return value;
                }

                return null;
            }

            /// <summary>
            /// Gets all the element(s) for the keys or throws an exception
            /// </summary>
            public List<string> GetAll(params string[] keys)
            {
                List<string> result = GetAll(keys[0]).ToList();

                for (int n = 1; n < keys.Length; n++)
                {
                    result.AddRange(GetAll(keys[n]));
                }

                return result;
            }

            /// <summary>
            /// Gets all the element(s) for the keys it can.
            /// </summary>
            internal List<string> TryGetAll(params string[] keys)
            {
                List<string> result = TryGetAll(keys[0]);

                if (result != null)
                {
                    result = result.ToList();
                }
                else
                {
                    result = new List<string>();
                }

                for (int n = 1; n < keys.Length; n++)
                {
                    var x = TryGetAll(keys[n]);

                    if (x != null)
                    {
                        result.AddRange(x);
                    }
                }

                return result;
            }

            internal void WriteToMeta(MetaInfoCollection collection, MetaInfoHeader header)
            {
                foreach (var t in this._contents)
                {
                    foreach (string value in t.Value)
                    {
                        collection.Write(header, t.Key, value);
                    }
                }
            }
        }
    }
}
