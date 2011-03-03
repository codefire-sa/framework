using System;
using System.Collections.Generic;
using System.Globalization;

namespace Codefire.Utilities
{
    public static class StringUtility
    {
        public static string TrimWhitespace(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            
            return RegExUtility.Replace(input, @"\s+", " ");
        }

        public static string ToTitleCase(string input)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input);
        }
        
        public static bool IsBoolean(string inputValue)
        {
            bool outputValue;
            return (bool.TryParse(inputValue, out outputValue));
        }

        public static bool ToBoolean(string inputValue, bool defaultValue)
        {
            bool outputValue;

            if (!bool.TryParse(inputValue, out outputValue)) outputValue = defaultValue;

            return outputValue;
        }

        public static bool ToBoolean(string inputValue)
        {
            return ToBoolean(inputValue, false);
        }

        public static bool IsInt(string inputValue)
        {
            int outputValue;
            return (int.TryParse(inputValue, out outputValue));
        }

        public static int ToInt(string inputValue, int defaultValue)
        {
            int outputValue;

            if (!int.TryParse(inputValue, out outputValue)) outputValue = defaultValue;

            return outputValue;
        }

        public static int ToInt(string inputValue)
        {
            return ToInt(inputValue, 0);
        }

        public static bool IsDecimal(string inputValue)
        {
            decimal outputValue;

            return (decimal.TryParse(inputValue, out outputValue));
        }

        public static decimal ToDecimal(string inputValue, decimal defaultValue)
        {
            decimal outputValue;

            if (!decimal.TryParse(inputValue, out outputValue)) outputValue = defaultValue;

            return outputValue;
        }

        public static decimal ToDecimal(string inputValue)
        {
            return ToDecimal(inputValue, 0);
        }

        public static bool IsDouble(string inputValue)
        {
            double outputValue;

            return (double.TryParse(inputValue, out outputValue));
        }

        public static double ToDouble(string inputValue, double defaultValue)
        {
            double outputValue;

            if (!double.TryParse(inputValue, out outputValue)) outputValue = defaultValue;

            return outputValue;
        }

        public static double ToDouble(string inputValue)
        {
            return ToDouble(inputValue, 0);
        }

        public static bool IsDateTime(string inputValue)
        {
            DateTime outputValue;

            return (DateTime.TryParse(inputValue, out outputValue));
        }

        public static bool IsDateTime(string inputValue, string format)
        {
            DateTime outputValue;

            return (DateTime.TryParseExact(inputValue, format, null, DateTimeStyles.None, out outputValue));
        }

        public static DateTime? ToDateTime(string inputValue)
        {
            DateTime outputValue;

            if (DateTime.TryParse(inputValue, out outputValue))
            {
                return outputValue;
            }
            else
            {
                return null;
            }
        }

        public static DateTime? ToDateTime(string inputValue, string format)
        {
            DateTime outputValue;

            if (DateTime.TryParseExact(inputValue, format, null, DateTimeStyles.None, out outputValue))
            {
                return outputValue;
            }
            else
            {
                return null;
            }
        }

        public static string Left(string inputValue, int count)
        {
            if (count > inputValue.Length)
                return inputValue;
            else
                return inputValue.Substring(0, count);
        }

        public static string Right(string inputValue, int count)
        {
            var startPos = inputValue.Length - count;

            if (startPos < 0)
                return inputValue;
            else
                return inputValue.Substring(startPos, count);
        }

        public static string BuildList(string separator, params string[] values)
        {
            var list = new List<string>();

            foreach (var item in values)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    list.Add(item);
                }
            }

            return string.Join(separator, list.ToArray());
        }

        public static string FormatSize(int value)
        {
            var sizeNames = new string[] { "B", "KB", "MB", "GB" };

            int counter = 0;
            double size = value;
            while (size >= 1024 && counter + 1 < sizeNames.Length)
            {
                size /= 1024;
                counter++;
            }
            
            return String.Format("{0:0.##} {1}", size, sizeNames[counter]);
        }
    }
}