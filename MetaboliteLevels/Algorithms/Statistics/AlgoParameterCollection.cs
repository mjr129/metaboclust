﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Data.DataInfo;
using System.Collections;

namespace MetaboliteLevels.Algorithms.Statistics
{
    /// <summary>
    /// Algorithms return this class to say what inputs and parameters they require.
    /// </summary>
    class AlgoParameterCollection : IReadOnlyList<AlgoParameter>
    {
        /// <summary>
        /// User configurable parameters (e.g. k in k-means)
        /// </summary>
        private AlgoParameter[] _parameters;

        /// <summary>
        /// Constructor.
        /// </summary>
        public AlgoParameterCollection(params AlgoParameter[] parameters)
        {
            if (parameters != null && parameters.Length != 0)
            {
                this._parameters = parameters;
            }
            else
            {
                this._parameters = null;
            }
        }

        /// <summary>
        /// Accessor
        /// </summary>
        public AlgoParameter this[int index]
        {
            get
            {
                return _parameters[index];
            }
        }

        /// <summary>
        /// Accessor
        /// </summary>
        public int Count
        {
            get
            {
                return _parameters.Length;
            }
        }

        /// <summary>
        /// Has any customisable parameters?
        /// </summary>
        public bool HasCustomisableParams
        {
            get
            {
                return _parameters != null;
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

            string result = StringHelper.ArrayToString(_parameters, z => z.Name, ", ");

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

            AlgoParameter[] algoParameters = (!reversable && algorithm != null) ? algorithm.Parameters._parameters : null;

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
                    return "{" + StringHelper.ArrayToString(p, GetDisplayName, ", ") + "}";
                }
                else
                {
                    return "{" + StringHelper.ArrayToString(p, z =>
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

            List<string> elements = StringHelper.SplitGroups(text);

            if (elements.Count != _parameters.Length)
            {
                parameters = null;
                return false;
            }

            object[] result = new object[_parameters.Length];

            for (int i = 0; i < _parameters.Length; i++)
            {
                result[i] = TryReadParameter(core, elements[i], _parameters[i].Type);

                if (result[i] == null)
                {
                    parameters = null;
                    return false;
                }
            }

            parameters = result;
            return true;
        }

        public static object TryReadParameter(Core core, string element, EAlgoParameterType paramType)
        {
            string elementId = element.Trim().ToUpper();

            switch (paramType)
            {
                case EAlgoParameterType.Double:
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

                case EAlgoParameterType.Integer:
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

                case EAlgoParameterType.WeakRefStatisticArray:
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

                case EAlgoParameterType.WeakRefConfigurationClusterer:
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


                case EAlgoParameterType.WeakRefPeak:
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

                case EAlgoParameterType.Group:
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

        public IEnumerator<AlgoParameter> GetEnumerator()
        {
            return ((IEnumerable<AlgoParameter>)_parameters).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _parameters.GetEnumerator();
        }
    }
}