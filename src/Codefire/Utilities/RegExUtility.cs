using System;
using System.Text.RegularExpressions;

namespace Codefire.Utilities
{
    public static class RegExUtility
    {
        public static bool IsExactMatch(string input, string pattern)
        {
            if (string.IsNullOrEmpty(input)) return false;

            Match expressionMatch = Regex.Match(input, pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            if (!expressionMatch.Success) return false;

            return (expressionMatch.Groups[0].Value == input);
        }

        public static bool Contains(string input, string pattern)
        {
            if (string.IsNullOrEmpty(input)) return false;

            Match expressionMatch = Regex.Match(input, pattern);
            
            return expressionMatch.Success;
        }

        public static string Replace(string input, string pattern, string replace)
        {
            if (string.IsNullOrEmpty(input)) return input;

            return Regex.Replace(input, pattern, replace, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        public static string[] GetOccurences(string input, string pattern)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(pattern)) return new string[] { };

            MatchCollection matches = Regex.Matches(input, pattern);

            string[] occurences = new string[matches.Count];
            for (int x = 0; x < matches.Count; x++)
            {
                occurences[x] = matches[x].Value;
            }
            return occurences;
        }

        public static int OccurenceCount(string input, string pattern)
        {
            return GetOccurences(input, pattern).Length;
        }
    }
}