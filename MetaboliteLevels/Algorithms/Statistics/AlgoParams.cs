using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Data.DataInfo;

namespace MetaboliteLevels.Algorithms.Statistics
{
    /// <summary>
    /// Algorithms return this class to say what inputs and parameters they require.
    /// </summary>
    class AlgoParameters
    {
        /// <summary>
        /// Type of parameter.
        /// </summary>
        public enum EType
        {
            /// <summary>
            /// Integer
            /// E.g. k in k-means
            /// </summary>
            [Name("Int")]
            Integer,

            /// <summary>
            /// Double
            /// </summary>
            [Name("Double")]
            Double,

            /// <summary>
            /// WeakReference&lt;ConfigurationStatistic&gt;[]
            /// </summary>
            [Name("StatisticArray")]
            WeakRefStatisticArray,


            /// <summary>
            /// WeakReference&lt;Peak&gt;
            /// </summary>
            [Name("Peak")]
            WeakRefPeak,

            /// <summary>
            /// GroupInfo
            /// </summary>
            [Name("Group")]
            Group,

            /// <summary>
            /// WeakReference&lt;ConfigurationClusterer&gt;
            /// </summary>
            [Name("ConfigurationClusterer")]
            WeakRefConfigurationClusterer,
        }

        /// <summary>
        /// Special flags.
        /// </summary>
        [Flags]
        public enum ESpecial
        {
            None = 0x00,

            /// <summary>
            /// Class takes two input vectors as the input (e.g. "control-intensity" vs "drought-intensity" or even "intensity" vs. "time")
            /// (Probably redundant since only and all MetricBase derived classes return this.)
            /// </summary>
            StatisticHasTwoInputs = 0x01,

            /// <summary>
            /// For classes derived from [MetricBase] only.
            /// Designates support for the QuickCalculate method that takes two input vectors and no filters.
            /// Most metrics will support this unless they use some obscure script that requires additional information, such as time or rep№s for each peak.
            /// </summary>
            MetricSupportsQuickCalculate = 0x02,

            /// <summary>
            /// For all classes.
            /// Designates lack of support for observation input filters (e.g. "control" vs. "drought").
            /// Most statistics will support filters on the input vectors unless they themselves wish to perform more complex internal filtering of the input.
            /// </summary>
            AlgorithmIgnoresObservationFilters = 0x04,

            /// <summary>
            /// For ClusterBase derivatives.
            /// Designates lack of support for distance metrics.
            /// </summary>
            ClustererIgnoresDistanceMetrics = 0x08,

            /// <summary>
            /// For ClusterBase derivatives.
            /// Designates that the distance matrix is not required and time can be saved by not generating it.
            /// </summary>
            ClustererIgnoresDistanceMatrix = 0x10,
        }

        /// <summary>
        /// Parameter (e.g. k for k-means)
        /// </summary>
        public class Param
        {
            public readonly string Name; // name
            public readonly EType Type; // type

            public Param(string name, EType type)
            {
                Name = name;
                Type = type;
            }

            /// <summary>
            /// Used in lists
            /// </summary>
            public override string ToString()
            {
                return Name + " (" + Type.ToUiString().ToSmallCaps() + ")";
            }
        }

        /// <summary>
        /// User configurable parameters (e.g. k in k-means)
        /// </summary>
        public readonly Param[] Parameters;

        /// <summary>
        /// See ESpecial
        /// </summary>
        public readonly ESpecial Special;

        /// <summary>
        /// Constructor.
        /// </summary>
        public AlgoParameters(ESpecial special, params Param[] parameters)
        {
            if (parameters != null && parameters.Length != 0)
            {
                this.Parameters = parameters;
            }
            else
            {
                this.Parameters = null;
            }

            this.Special = special;
        }

        /// <summary>
        /// Requires more than 1 input vector?
        /// </summary>
        public bool HasMultipleInputs
        {
            get { return HasSpecial(ESpecial.StatisticHasTwoInputs); }
        }

        /// <summary>
        /// Has the special flag [flag] set.
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        private bool HasSpecial(ESpecial flag)
        {
            return (Special & flag) == flag;
        }

        /// <summary>
        /// Has any customisable parameters?
        /// </summary>
        public bool HasCustomisableParams
        {
            get
            {
                return Parameters != null;
            }
        }

        /// <summary>
        /// The of the customisable parameters.
        /// </summary>
        internal string ParamNames()
        {
            if (!HasCustomisableParams)
            {
                return "No parameters";
            }

            string result = Maths.ArrayToString(Parameters, z => z.Name, ", ");

            if (result.Length > 15)
            {
                return "Parameters";
            }

            return result;
        }

        /// <summary>
        /// Converts the specified customisable parameters [parameters] to a parasable string. 
        /// </summary>
        public static string ParamsToReversableString(object[] parameters, Core core)
        {
            return ParamsToString(null, parameters, true, core);
        }

        /// <summary>
        /// Converts the specified customisable parameters [parameters] to a human readable string. 
        /// </summary>
        public static string ParamsToHumanReadableString(object[] parameters, AlgoBase algorithm)
        {
            return ParamsToString(algorithm, parameters, false, null);
        }

        private static string ParamsToString(AlgoBase algorithm, object[] parameters, bool reversable, Core core)
        {
            if (parameters == null)
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();

            Param[] algoParameters = (!reversable && algorithm != null) ? algorithm.GetParams().Parameters : null;

            for (int i = 0; i < parameters.Length; i++)
            {
                var param = parameters[i];

                if (sb.Length != 0)
                {
                    sb.Append(", ");
                }

                if (!reversable)
                {
                    if (algoParameters != null && i < algoParameters.Length)
                    {
                        sb.Append(algoParameters[i].Name + " = ");
                    }
                    else
                    {
                        sb.Append("Param" + (i + 1) + " = ");
                    }
                }

                sb.Append(ParamToString(reversable, core, param));
            }

            return sb.ToString();
        }

        internal static string ParamToString(object param)
        {
            return ParamToString(false, null, param);
        }

        /// <summary>
        /// Parameter to string
        /// </summary>
        /// <param name="reversable">true: Reversable value suitable for StringToParam, else use most readable output</param>
        /// <param name="core">Core required only if reversable = true</param>
        /// <param name="param">Parameter value</param>
        /// <returns>Parameter as string</returns>
        internal static string ParamToString(bool reversable, Core core, object param)
        {
            if (param is WeakReference<ConfigurationStatistic>[])
            {
                WeakReference<ConfigurationStatistic>[] p = (WeakReference<ConfigurationStatistic>[])param;

                if (!reversable)
                {
                    return "{" + Maths.ArrayToString(p, GetDisplayName, ", ") + "}";
                }
                else
                {
                    return "{" + Maths.ArrayToString(p, z =>
                                                           {
                                                               var targ = z.GetTarget();

                                                               if (targ == null)
                                                               {
                                                                   return "?";
                                                               }

                                                               var tmp = core.Statistics.IndexOf(targ);

                                                               return tmp == -1 ? "?" : tmp.ToString();
                                                           },
                                                        "; ") + "}";
                }
            }
            else if (param is WeakReference<ConfigurationClusterer>)
            {
                WeakReference<ConfigurationClusterer> p = (WeakReference<ConfigurationClusterer>)param;
                ConfigurationClusterer c = p.GetTarget();

                if (c == null)
                {
                    return ("?");
                }
                else if (reversable)
                {
                    return (core.Clusterers.IndexOf(c).ToString());
                }
                else
                {
                    return (c.ToString());
                }
            }
            else if (param is WeakReference<Peak>)
            {
                WeakReference<Peak> p = (WeakReference<Peak>)param;

                Peak peak = p.GetTarget();

                return (peak != null ? peak.DisplayName : "[MISSING PEAK]");
            }
            else if (param is GroupInfo)
            {
                GroupInfo p = (GroupInfo)param;

                return (reversable ? p.Id.ToString() : p.Name);
            }
            else if (param is double)
            {
                double p = (double)param;

                if (p == double.MaxValue)
                {
                    return ("MAX");
                }
                else if (p == double.MinValue)
                {
                    return ("MIN");
                }
                else
                {
                    return (param.ToString());
                }
            }
            else if (param is int)
            {
                int p = (int)param;

                if (p == int.MaxValue)
                {
                    return ("MAX");
                }
                else if (p == int.MinValue)
                {
                    return ("MIN");
                }
                else
                {
                    return (p.ToString());
                }
            }
            else
            {
                return (param.ToString());
            }
        }

        private static string GetDisplayName(WeakReference<ConfigurationStatistic> a)
        {
            var b = a.GetTarget();

            if (b == null)
            {
                return "[MISSING STATISTIC]";
            }
            else
            {
                return b.ToString();
            }
        }

        /// <summary>
        /// Converts the specified string to a customisable parameter set.
        /// </summary>
        public bool TryStringToParams(Core core, string text, out object[] parameters)
        {
            if (!HasCustomisableParams)
            {
                parameters = null;
                return false;
            }

            List<string> elements = UiControls.SplitGroups(text);

            if (elements.Count != Parameters.Length)
            {
                parameters = null;
                return false;
            }

            object[] result = new object[Parameters.Length];

            for (int i = 0; i < Parameters.Length; i++)
            {
                result[i] = TryReadParameter(core, elements[i], Parameters[i].Type);

                if (result[i] == null)
                {
                    parameters = null;
                    return false;
                }
            }

            parameters = result;
            return true;
        }

        public static object TryReadParameter(Core core, string element, EType paramType)
        {
            string elementId = element.Trim().ToUpper();

            switch (paramType)
            {
                case EType.Double:
                    {
                        double vd;

                        if (elementId == "MAX")
                        {
                            return double.MaxValue;
                        }
                        else if (elementId == "MIN")
                        {
                            return double.MinValue;
                        }
                        else if (double.TryParse(element, out vd))
                        {
                            return vd;
                        }
                        else
                        {
                            return null;
                        }
                    }

                case EType.Integer:
                    {
                        int vi;

                        if (elementId == "MAX")
                        {
                            return int.MaxValue;
                        }
                        else if (elementId == "MIN")
                        {
                            return int.MinValue;
                        }
                        else if (int.TryParse(element, out vi))
                        {
                            return vi;
                        }
                        else
                        {
                            return null;
                        }
                    }

                case EType.WeakRefStatisticArray:
                    {
                        string[] e2 = element.Split(",;".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                        WeakReference<ConfigurationStatistic>[] r = new WeakReference<ConfigurationStatistic>[e2.Length];
                        ConfigurationStatistic[] opts = core.Statistics.ToArray();

                        for (int n = 0; n < e2.Length; n++)
                        {
                            int uid;

                            if (!int.TryParse(e2[n].Trim(), out uid))
                            {
                                return null;
                            }

                            if (uid < 0 || uid >= opts.Length)
                            {
                                return null;
                            }

                            ConfigurationStatistic stat = opts[uid];

                            if (stat == null)
                            {
                                return null;
                            }

                            r[n] = new WeakReference<ConfigurationStatistic>(stat);
                        }

                        return r;
                    }

                case EType.WeakRefConfigurationClusterer:
                    {
                        int ival;

                        if (!int.TryParse(element, out ival))
                        {
                            return null;
                        }

                        ConfigurationClusterer[] opts = core.Clusterers.ToArray();

                        if (ival < 0 || ival >= opts.Length)
                        {
                            return null;
                        }

                        return new WeakReference<ConfigurationClusterer>(opts[ival]);
                    }


                case EType.WeakRefPeak:
                    {
                        string peakName = elementId;

                        var peak = core.Peaks.FirstOrDefault(z => z.DisplayName.ToUpper() == peakName);

                        if (peak != null)
                        {
                            return new WeakReference<Peak>(peak);
                        }
                        else
                        {
                            return null;
                        }
                    }

                case EType.Group:
                    {
                        int ival;

                        if (!int.TryParse(element, out ival))
                        {
                            return null;
                        }

                        return core.Groups.First(z => z.Id == ival);
                    }

                default:
                    throw new InvalidOperationException("TryParseParams");
            }
        }

        public bool SupportsQuickCalculate
        {
            get
            {
                return HasSpecial(ESpecial.MetricSupportsQuickCalculate);
            }
        }

        public bool SupportsInputFilters
        {
            get
            {
                return !HasSpecial(ESpecial.AlgorithmIgnoresObservationFilters);
            }
        }
    }
}
