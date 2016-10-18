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
using MGui.Datatypes;
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
                                          
            var fieldsAsStrings = parameters.Select( z => ParamToString( z ) );

            if (reversable || algorithm == null)
            {
                return AlgoParameterTypes.ExternalConvertor.WriteFields( fieldsAsStrings);
            }
            else
            {
                return string.Join( ", ", algorithm.Parameters.Zip( fieldsAsStrings ).Select( z => z.Item1.Name + " = " + AlgoParameterTypes.ExternalConvertor.WriteField( z.Item2 ) ) );
            }                 
        }   

        /// <summary>
        /// Parameter to string
        /// </summary>                                                            
        /// <param name="param">Parameter value</param>
        /// <returns>Parameter as string</returns>                                        
        internal static string ParamToString( object param )
        {
            return AlgoParameterTypes.ToString( param );
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
            string error;
            object[] parameters = TryStringToParams( core, text,  out error);

            if (parameters == null)
            {
                throw new InvalidOperationException( "Cannot parse parameters, the following error was returned: " + error );
            }

            return parameters;
        }                                                                

        /// <summary>
        /// Converts the specified string to a customisable parameter set.
        /// </summary>
        /// <param name="core">Required</param>
        /// <param name="text">Text to convert</param>           
        /// <returns>Parameters or NULL on failure.</returns>
        public object[] TryStringToParams(Core core, string text, out string error)
        {
            if (!HasCustomisableParams)
            {
                // Any input is valid if there are no parameters
                // This way we don't get errors from leftover text when inputs are hidden because
                // there are no parameters
                error = null;
                return new object[0];
            }

            string[] elements = AlgoParameterTypes.ExternalConvertor.ReadFields( text);

            if (elements.Length != _parameters.Length)
            {
                // Count mismatch
                error = $"This algorithm takes {{{_parameters.Length}}} parameters but {{{elements.Length}}} were provided.";
                return null;
            }

            object[] result = new object[_parameters.Length];

            for (int i = 0; i < _parameters.Length; i++)
            {
                var x = new FromStringArgs( core, elements[i] );
                result[i] = _parameters[i].Type.FromString( x );

                if (result[i] == null)
                {
                    // Couldn't read parameter
                    error = $"Error in parameter #{i + 1}: {x.Error}";
                    return null;
                }
            }

            error = null;
            return result;
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
