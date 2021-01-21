using Microsoft.Extensions.Configuration;
using System;

namespace MSCC.Core
{
    public static class ConfigHelper
    {
        public static IConfiguration Configuration { get; set; }

        public static string APIURL => GetValue("AppSettings:APIURL", string.Empty);

        public static string TDESKey => GetValue("TripleDES:Key", string.Empty);
        public static string TDESVector => GetValue("TripleDES:Vector", string.Empty);

        #region Private - Methods

        /// <summary>
        /// Getting the value of the given key from Config file with specific type.
        /// </summary>
        /// <typeparam name="T">The specified type to be casted.</typeparam>
        /// <param name="key">The config key.</param>
        /// <param name="defaultvalue">The default of the key.</param>
        /// <returns>return a value with dynamic type.</returns>
        private static T GetValue<T>(string key, T defaultvalue) where T : IConvertible
        {
            #region Declare return Var with intial value

            T value = default(T);

            #endregion Declare return Var with intial value

            try
            {
                #region VARS

                dynamic result = null;
                Type valueType = typeof(T);

                #endregion VARS

                #region Check whether the value is exists in the config or not.

                if (!string.IsNullOrWhiteSpace(Configuration.GetSection(key).Value))
                {
                    #region Casting the value into specified type.

                    if (valueType == typeof(int))
                    {
                        int keyValue = default(int);
                        if (int.TryParse(Configuration.GetSection(key).Value, out keyValue))
                            result = keyValue;
                        else
                            result = defaultvalue;
                    }
                    else if (valueType == typeof(double))
                    {
                        double keyValue = default(double);
                        if (double.TryParse(Configuration.GetSection(key).Value, out keyValue))
                            result = keyValue;
                        else
                            result = defaultvalue;
                    }
                    else if (valueType == typeof(long))
                    {
                        long keyValue = default(long);
                        if (long.TryParse(Configuration.GetSection(key).Value, out keyValue))
                            result = keyValue;
                        else
                            result = defaultvalue;
                    }
                    else if (valueType == typeof(byte))
                    {
                        byte keyValue = default(byte);
                        if (byte.TryParse(Configuration.GetSection(key).Value, out keyValue))
                            result = keyValue;
                        else
                            result = defaultvalue;
                    }
                    else if (valueType == typeof(bool))
                    {
                        bool keyValue = default(bool);
                        if (bool.TryParse(Configuration.GetSection(key).Value, out keyValue))
                            result = keyValue;
                        else
                            result = defaultvalue;
                    }
                    else if (valueType == typeof(string))
                    {
                        string keyValue = Configuration.GetSection(key).Value;
                        if (!string.IsNullOrWhiteSpace(keyValue))
                            result = keyValue;
                        else
                            result = defaultvalue;
                    }

                    #endregion Casting the value into specified type.

                    #region Getting the value.

                    value = (T)Convert.ChangeType(result, valueType);

                    #endregion Getting the value.
                }
                else
                {
                }

                #endregion Check whether the value is exists in the config or not.
            }
            catch (Exception e)
            {
                throw e;
            }
            return value;
        }

        #endregion Private - Methods
    }
}