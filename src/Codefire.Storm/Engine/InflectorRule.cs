using System;
using System.Text.RegularExpressions;

namespace Codefire.Storm.Engine
{
    public class InflectorRule
    {
        public readonly Regex _regex;
        public readonly string _replacement;

        public InflectorRule(string regexPattern, string replacementText)
        {
            _regex = new Regex(regexPattern, RegexOptions.IgnoreCase);
            _replacement = replacementText;
        }

        public string Apply(string word)
        {
            if (!_regex.IsMatch(word)) return null;

            string str = _regex.Replace(word, _replacement);
            if (word == word.ToUpper())
            {
                str = str.ToUpper();
            }

            return str;
        }
    }
}