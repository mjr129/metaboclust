using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Algorithms.Definitions.Base;
using MetaboliteLevels.Data.Algorithms.Definitions.Clusterers;
using MetaboliteLevels.Data.Algorithms.Definitions.Configurations;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Session.Singular;
using MGui.Helpers;

namespace MetaboliteLevels.Data.Algorithms.General
{
    /// <summary>
    /// Algorithms return this class to say what inputs and parameters they require.
    /// </summary>
    class AlgoParameterCollection : IReadOnlyList<AlgoParameter>
    {
        /// <summary>
        /// User configurable parameters (e.g. k in k-means)
        /// 
        /// This is never null but may be empty.
        /// </summary>
        private AlgoParameter[] _parameters;

        /// <summary>
        /// Constructor.
        /// </summary>
        public AlgoParameterCollection(params AlgoParameter[] parameters)
        {
            Debug.Assert( parameters != null );

            this._parameters = parameters;
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
                return _parameters.Length != 0;
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
                object param = parameters[i];

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
                                                               ConfigurationStatistic targ = z.GetTarget();

                                                               if (targ == null)
                                                               {
                                                                   return "?";
                                                               }

                                                               int tmp = IVisualisableExtensions.WhereEnabled(core.AllStatistics).IndexOf(targ);

                                                               return tmp == -1 ? "?" : tmp.ToString();
                                                           },
                                                        "; ") + "}";
                }
            }
            else if (param is WeakReference<Cluster>)
            {
                WeakReference<Cluster> p = (WeakReference<Cluster>)param;
                Cluster c = p.GetTarget();

                if (c == null)
                {
                    return ("?");
                }
                else if (reversable)
                {
                    return core.Clusters.IndexOf( c ).ToString();
                }
                else
                {
                    return (c.ToString());
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
                    return (IVisualisableExtensions.WhereEnabled(core.AllClusterers).IndexOf(c).ToString());
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

                return (reversable ? p.Id : p.DisplayName);
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
            ConfigurationStatistic b = a.GetTarget();

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
        /// Like TryStringToParams but throws an error on failure.
        /// </summary>
        /// <param name="core">Required</param>
        /// <param name="text">Text to convert</param>
        /// <returns>Parameters</returns>
        public object[] StringToParams(Core core, string text)
        {
            object[] parameters = TryStringToParams( core, text );

            if (parameters == null)
            {
                throw new InvalidOperationException("Cannot parse parameters.");
            }

            return parameters;
        }

        /// <summary>
        /// Converts the specified string to a customisable parameter set.
        /// </summary>
        /// <param name="core">Required</param>
        /// <param name="text">Text to convert</param>           
        /// <returns>Parameters or NULL on failure.</returns>
        public object[] TryStringToParams(Core core, string text)
        {
            if (!HasCustomisableParams)
            {
                // Any input is valid if there are no parameters
                // This way we don't get errors from leftover text when inputs are hidden because
                // there are no parameters
                return new object[0];
            }

            List<string> elements = StringHelper.SplitGroups(text);

            if (elements.Count != _parameters.Length)
            {
                // Count mismatch
                return null;
            }

            object[] result = new object[_parameters.Length];

            for (int i = 0; i < _parameters.Length; i++)
            {
                result[i] = TryReadParameter(core, elements[i], _parameters[i].Type);

                if (result[i] == null)
                {
                    // Couldn't read parameter
                    return null;
                }
            }

            return result;
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
                        ConfigurationStatistic[] opts = IVisualisableExtensions.WhereEnabled(core.AllStatistics).ToArray();

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

                        ConfigurationClusterer[] opts = IVisualisableExtensions.WhereEnabled(core.AllClusterers).ToArray();

                        if (ival < 0 || ival >= opts.Length)
                        {
                            return null;
                        }

                        return new WeakReference<ConfigurationClusterer>(opts[ival]);
                    }


                case EAlgoParameterType.WeakRefPeak:
                    {
                        string peakName = elementId;

                        Peak peak = core.Peaks.FirstOrDefault(z => z.DisplayName.ToUpper() == peakName);

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
                        string el = element.Trim();
                        return core.Groups.FirstOrDefault(z => z.Id == el);
                    }

                case EAlgoParameterType.WeakRefClusterArray:
                    {
                        int ival;

                        if (!int.TryParse( element, out ival ))
                        {
                            return null;
                        }

                        Cluster[] opts = core.Clusters.ToArray(); // TODO: Efficient?

                        if (ival < 0 || ival >= opts.Length)
                        {
                            return null;
                        }

                        return new WeakReference<Cluster>( opts[ival] );
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
