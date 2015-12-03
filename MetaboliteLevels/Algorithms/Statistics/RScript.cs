using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Algorithms.Statistics
{
    /// <summary>
    /// An R script.
    /// </summary>
    class RScript
    {
        /// <summary>
        /// Parameters of the script.
        /// </summary>
        public readonly AlgoParameters RequiredParameters;

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

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="text">Script text</param>
        /// <param name="inputs">Input parameters</param>
        /// <param name="quickCalcCheck">If set then this corresponds to which input parameters must be set for the script to support QuickCalc mode. This should be a string of "0" and "1" the same length as the input parameter array.</param>
        public RScript(string text, string inputs, string quickCalcCheck, string dmCalcCheck, AlgoParameters.ESpecial extraFlags)
        {
            var conv = EnumHelper.GetEnumKeys<AlgoParameters.EType>();
            int pos = 0;
            int lpos;

            string[] ie = inputs.Split(",".ToCharArray());
            List<Tuple<string, string>> inputNames = new List<Tuple<string, string>>(); // ID, Name

            foreach (string iee in ie)
            {
                string[] ieee = iee.Split("=".ToCharArray());
                inputNames.Add(new Tuple<string, string>(ieee[0].ToUpper(), ieee[1])); // ID, DefaultName
            }

            var Parameters = new List<AlgoParameters.Param>();

            while (true)
            {
                lpos = pos;
                string line = ReadLine(text, ref pos);

                if (line != null && line.StartsWith("##"))
                {
                    string[] ee = line.Substring(2).Trim().Split(",".ToCharArray());

                    foreach (string e in ee)
                    {
                        string name = e;
                        string type = StringHelper.SplitEquals(ref name);

                        if (type != null)
                        {
                            name = name.Trim();
                            type = type.Trim().ToUpper();

                            AlgoParameters.EType etype;

                            if (conv.TryGetValue(type, out etype))
                            {
                                Parameters.Add(new AlgoParameters.Param(name, etype));
                            }
                            else
                            {
                                int ssi = inputNames.FindIndex(z => z.Item1 == type);

                                if (ssi != -1)
                                {
                                    inputNames[ssi] = new Tuple<string, string>(inputNames[ssi].Item1, name);
                                }
                                else
                                {
                                    Errors += string.Format("Cannot parse script arguments: Parameter specification for [{0}] does not exist.", type);
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

            string[] inputNamesA = inputNames.Select(z => z.Item2 == "-" ? null : z.Item2).ToArray();

            bool supportsQuickCalculate;

            if (quickCalcCheck != null)
            {
                supportsQuickCalculate = true;

                UiControls.Assert(inputNamesA.Length == quickCalcCheck.Length && quickCalcCheck.All(z => z == '0' || z == '1'), "Invalid quick calc check string.");

                for (int i = 0; i < inputNamesA.Length; i++)
                {
                    if ((inputNamesA[i] == null) != (quickCalcCheck[i] == '1'))
                    {
                        supportsQuickCalculate = false;
                        break;
                    }
                }
            }
            else
            {
                supportsQuickCalculate = false;
            }

            bool supportsDistanceMeasure;

            if (dmCalcCheck != null)
            {
                supportsDistanceMeasure = true;

                UiControls.Assert(inputNamesA.Length == dmCalcCheck.Length && dmCalcCheck.All(z => z == '-' || z == '1'),
                                  "Invalid distance check string.");

                for (int i = 0; i < inputNamesA.Length; i++)
                {
                    if ((dmCalcCheck[i] == '1') && (inputNamesA[i] == null))
                    {
                        supportsDistanceMeasure = false;
                        break;
                    }
                }
            }
            else
            {
                supportsDistanceMeasure = true;
            }


            Script = text.Substring(lpos);

            this.InputNames = inputNamesA;

            if (supportsQuickCalculate)
            {
                extraFlags |= AlgoParameters.ESpecial.MetricSupportsQuickCalculate;
            }

            if (!supportsDistanceMeasure)
            {
                extraFlags |= AlgoParameters.ESpecial.ClustererIgnoresDistanceMetrics | AlgoParameters.ESpecial.ClustererIgnoresDistanceMatrix;
            }

            RequiredParameters = new AlgoParameters(extraFlags, Parameters.Count != 0 ? Parameters.ToArray() : null);
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
