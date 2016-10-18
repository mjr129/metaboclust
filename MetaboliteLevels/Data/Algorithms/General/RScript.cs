using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Utilities;
using MGui.Datatypes;
using MGui.Helpers;

namespace MetaboliteLevels.Data.Algorithms.General
{
    /// <summary>
    /// An R script.
    /// </summary>
    class RScript
    {
        /// <summary>
        /// Parameters of the script.
        /// </summary>
        public readonly AlgoParameterCollection RequiredParameters;

        /// <summary>
        /// Script text
        /// </summary>
        public readonly string Script;

        /// <summary>
        /// Errors parsing the script are stored here.
        /// </summary>
        public string Errors = "";

        /// <summary>
        /// Names of the input parameters in order (e.g. peak-1, peak-2)
        /// 
        /// These can be null in which case they aren't calculated or used (see IsInputPresent())
        /// </summary>
        public readonly string[] InputNames;

        public readonly string FileName;

        public class RScriptMarkup
        {
            public readonly string Summary;
            public readonly string ReturnValue;
            public readonly List<RScriptMarkupElement> Inputs;

            public RScriptMarkup( string inputs )
            {
                Inputs = new List<RScriptMarkupElement>();

                SpreadsheetReader reader = new SpreadsheetReader()
                {
                    HasColNames = false,
                    HasRowNames = false,
                };

                Spreadsheet<string> data = reader.ReadText<string>( inputs );

                foreach (Spreadsheet<string>.Row row in data.Rows)
                {
                    string key = row[0].ToUpper();

                    if (key == "SUMMARY")
                    {
                        Summary = row[2];
                    }
                    else if (key == "RETURNS")
                    {
                        ReturnValue = row[2];
                    }
                    else
                    {
                        Inputs.Add( new RScriptMarkupElement( key, row[1], row[2] ) ); // ID, DefaultName
                    }
                }
            }
        }

        public class RScriptMarkupElement
        {
            public readonly string Comment;
            public readonly string Key;
            public string Name;

            public RScriptMarkupElement( string key, string value, string comment )
            {
                this.Key = key;
                this.Name = value;
                this.Comment = comment;
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="text">Script text</param>
        /// <param name="inputs">Input parameters</param>
        /// <param name="quickCalcCheck">If set then this corresponds to which input parameters must be set for the script to support QuickCalc mode. This should be a string of "0" and "1" the same length as the input parameter array.</param>
        public RScript(string text, string inputs, string fileName)
        {
            Dictionary<string, IAlgoParameterType> conv = AlgoParameterTypes.GetKeys();
            int pos = 0;
            int lpos;
            this.FileName = fileName;

            RScriptMarkup markup = new RScriptMarkup( inputs );

            List<AlgoParameter> parameters = new List<AlgoParameter>();

            while (true)
            {
                lpos = pos;
                string line = ReadLine(text, ref pos);

                if (line != null && line.StartsWith("##"))
                {
                    string[] ee = line.Substring(2).Trim().Split(",".ToCharArray());

                    foreach (string e in ee)
                    {
                        string[] eee = e.Split( "=".ToCharArray(), 3, StringSplitOptions.RemoveEmptyEntries );

                        string name = eee[0];
                        string type = eee[1];
                        string desc = eee.Length >= 3 ? eee[2] : null;

                        if (type != null)
                        {
                            name = name.Trim();
                            type = type.Trim().ToUpper();

                            IAlgoParameterType etype;

                            if (conv.TryGetValue(type, out etype))
                            {
                                parameters.Add(new AlgoParameter(name, desc, etype ) );
                            }
                            else
                            {
                                int ssi = markup.Inputs.FindIndex(z => z.Key == type);

                                if (ssi != -1)
                                {
                                    markup.Inputs[ssi].Name= name;
                                }
                                else
                                {
                                    Errors += $"Cannot parse script arguments: Parameter specification for [{type}] does not exist.";
                                }
                            }
                        }
                    }
                }
                else
                {
                    break;
                }
            }

            string[] inputNamesA = markup.Inputs.Select(z => z.Name == "-" ? null : z.Name).ToArray();

            this.Script = text;
            this.InputNames = inputNamesA;     

            RequiredParameters = new AlgoParameterCollection(parameters.ToArray());
        }

        /// <summary>
        /// Determines if the inputs match the specified mask.
        /// </summary>
        /// <param name="mask">Mask containing '1' (present) '0' (not present) and '-' (doesn't matter)</param>
        /// <returns>Whether inputs batch mask</returns>
        public bool CheckInputMask(string mask)
        {
            UiControls.Assert(InputNames.Length == mask.Length && mask.All(z => z == '0' || z == '1' || z == '-'), "Invalid quick calc check string.");

            for (int i = 0; i < InputNames.Length; i++)
            {
                char c = mask[i];

                switch (c)
                {
                    case '0':
                        if (InputNames[i] != null)
                        {
                            return false;
                        }
                        break;

                    case '1':
                        if (InputNames[i] == null)
                        {
                            return false;
                        }
                        break;

                    case '-':
                    default:
                        break;
                }
            }

            return true;
        }

        private static string ReadLine(string text, ref int pos)
        {
            if (pos >= text.Length)
            {
                return null;
            }

            int start = pos;

            for (int i = pos; i < text.Length; i++)
            {
                if (text[i] == '\n')
                {
                    pos = i + 1;

                    if (text[i - 1] == '\r')
                    {
                        return text.Substring(start, pos - start - 2);
                    }
                    else
                    {
                        return text.Substring(start, pos - start - 1);
                    }
                }
            }

            pos = text.Length;
            return text.Substring(start, pos - start);
        }

        /// <summary>
        /// Determines if an input is present.
        /// 
        /// If it isn't then we can save time by not calculating it,
        /// </summary>
        public bool IsInputPresent(int index)
        {
            return InputNames[index] != null;
        }
    }
}
