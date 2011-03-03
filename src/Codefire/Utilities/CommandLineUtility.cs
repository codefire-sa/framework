using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Codefire.Utilities
{
    public static class CommandLineUtility
    {
        public static StringDictionary Parse(IEnumerable<string> argumentList)
        {
            var table = new StringDictionary();
            foreach (string argument in argumentList)
            {
                var pair = ParseArgument(argument);
                table.Add(pair.Key, pair.Value);
            }

            return table;
        }

        public static T Build<T>(IEnumerable<string> argumentList)
        {
            var objectType = typeof(T);
            var objectValue = Activator.CreateInstance<T>();
            var bindingFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase;

            foreach (string argument in argumentList)
            {
                var pair = ParseArgument(argument);

                var property = objectType.GetProperty(pair.Key, bindingFlags);
                if (property == null)
                {
                    throw new ArgumentException(string.Format("A matching property of name {0} on type {1} could not be found.", pair.Key, objectType));
                }

                // If the value is null/empty and the property is a bool, we
                // treat it as a flag, which means its presence means true.
                if (String.IsNullOrEmpty(pair.Value) && property.PropertyType == typeof(bool))
                {
                    property.SetValue(objectValue, true, null);
                    continue;
                }

                var typeConverter = TypeDescriptor.GetConverter(property.PropertyType);
                if (typeConverter == null || !typeConverter.CanConvertFrom(typeof(string)))
                {
                    throw new ArgumentException("Unable to convert from a string to a property of type " + property.PropertyType + ".");
                }
                var propertyValue = typeConverter.ConvertFromInvariantString(pair.Value);

                property.SetValue(objectValue, propertyValue, null);
            }

            return objectValue;
        }

        #region [ Private ]

        private static KeyValuePair<string, string> ParseArgument(string argument)
        {
            if (argument == null)
            {
                throw new ArgumentNullException("argument");
            }

            string key;
            string value;

            var isMatch = Regex.IsMatch(argument, @"^[-/\\]");
            if (isMatch)
            {
                string[] splitArg = argument.Substring(1).Split(':', '=');

                //Key is extracted from first element
                key = splitArg[0];

                //Reconstruct the value. We could also do this using substrings.
                if (splitArg.Length > 1)
                {
                    value = splitArg[1];
                }
                else
                {
                    value = string.Empty;
                }
            }
            else
            {
                throw new ArgumentException("Unsupported value line argument format.", argument);
            }

            return new KeyValuePair<string,string>(key, value);
        }

        #endregion
    }
}