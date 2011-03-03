using System;
using System.Collections.Generic;

namespace Codefire.Storm.Engine
{
    public class Inflector
    {
        private List<InflectorRule> _plurals;
        private List<InflectorRule> _singulars;
        private List<string> _uncountables;
        private static Inflector _default;

        static Inflector()
        {
            _default = new Inflector();
            _default.AddPluralRule("$", "s");
            _default.AddPluralRule("s$", "s");
            _default.AddPluralRule("(ax|test)is$", "$1es");
            _default.AddPluralRule("(octop|vir)us$", "$1i");
            _default.AddPluralRule("(alias|status)$", "$1es");
            _default.AddPluralRule("(bu)s$", "$1ses");
            _default.AddPluralRule("(buffal|tomat)o$", "$1oes");
            _default.AddPluralRule("([ti])um$", "$1a");
            _default.AddPluralRule("sis$", "ses");
            _default.AddPluralRule("(?:([^f])fe|([lr])f)$", "$1$2ves");
            _default.AddPluralRule("(hive)$", "$1s");
            _default.AddPluralRule("([^aeiouy]|qu)y$", "$1ies");
            _default.AddPluralRule("(x|ch|ss|sh)$", "$1es");
            _default.AddPluralRule("(matr|vert|ind)ix|ex$", "$1ices");
            _default.AddPluralRule("([m|l])ouse$", "$1ice");
            _default.AddPluralRule("^(ox)$", "$1en");
            _default.AddPluralRule("(quiz)$", "$1zes");
            _default.AddSingularRule("s$", string.Empty);
            _default.AddSingularRule("ss$", "ss");
            _default.AddSingularRule("(n)ews$", "$1ews");
            _default.AddSingularRule("([ti])a$", "$1um");
            _default.AddSingularRule("((a)naly|(b)a|(d)iagno|(p)arenthe|(p)rogno|(s)ynop|(t)he)ses$", "$1$2sis");
            _default.AddSingularRule("(^analy)ses$", "$1sis");
            _default.AddSingularRule("([^f])ves$", "$1fe");
            _default.AddSingularRule("(hive)s$", "$1");
            _default.AddSingularRule("(tive)s$", "$1");
            _default.AddSingularRule("([lr])ves$", "$1f");
            _default.AddSingularRule("([^aeiouy]|qu)ies$", "$1y");
            _default.AddSingularRule("(s)eries$", "$1eries");
            _default.AddSingularRule("(m)ovies$", "$1ovie");
            _default.AddSingularRule("(x|ch|ss|sh)es$", "$1");
            _default.AddSingularRule("([m|l])ice$", "$1ouse");
            _default.AddSingularRule("(bus)es$", "$1");
            _default.AddSingularRule("(o)es$", "$1");
            _default.AddSingularRule("(shoe)s$", "$1");
            _default.AddSingularRule("(cris|ax|test)es$", "$1is");
            _default.AddSingularRule("(octop|vir)i$", "$1us");
            _default.AddSingularRule("(alias|status)$", "$1");
            _default.AddSingularRule("(alias|status)es$", "$1");
            _default.AddSingularRule("^(ox)en", "$1");
            _default.AddSingularRule("(vert|ind)ices$", "$1ex");
            _default.AddSingularRule("(matr)ices$", "$1ix");
            _default.AddSingularRule("(quiz)zes$", "$1");
            _default.AddIrregularRule("person", "people");
            _default.AddIrregularRule("man", "men");
            _default.AddIrregularRule("child", "children");
            _default.AddIrregularRule("sex", "sexes");
            _default.AddIrregularRule("tax", "taxes");
            _default.AddIrregularRule("move", "moves");
            _default.AddUnknownCountRule("equipment");
            _default.AddUnknownCountRule("information");
            _default.AddUnknownCountRule("rice");
            _default.AddUnknownCountRule("money");
            _default.AddUnknownCountRule("species");
            _default.AddUnknownCountRule("series");
            _default.AddUnknownCountRule("fish");
            _default.AddUnknownCountRule("sheep");
        }

        public Inflector()
        {
            _plurals = new List<InflectorRule>();
            _singulars = new List<InflectorRule>();
            _uncountables = new List<string>();
        }

        public static Inflector Default
        {
            get { return _default; }
        }

        public void AddIrregularRule(string singular, string plural)
        {
            AddPluralRule(string.Concat(new object[] { "(", singular[0], ")", singular.Substring(1), "$" }), "$1" + plural.Substring(1));
            AddSingularRule(string.Concat(new object[] { "(", plural[0], ")", plural.Substring(1), "$" }), "$1" + singular.Substring(1));
        }

        public void AddPluralRule(string rule, string replacement)
        {
            _plurals.Add(new InflectorRule(rule, replacement));
        }

        public void AddSingularRule(string rule, string replacement)
        {
            _singulars.Add(new InflectorRule(rule, replacement));
        }

        public void AddUnknownCountRule(string word)
        {
            _uncountables.Add(word.ToLower());
        }

        private string ApplyRules(IList<InflectorRule> rules, string word)
        {
            string str = word;
            if (!_uncountables.Contains(word.ToLower()))
            {
                for (int i = rules.Count - 1; i >= 0; i--)
                {
                    string str2 = rules[i].Apply(word);
                    if (str2 != null)
                    {
                        return str2;
                    }
                }
            }
            return str;
        }

        public string MakePlural(string word)
        {
            return ApplyRules(_plurals, word);
        }

        public string MakeSingular(string word)
        {
            return ApplyRules(_singulars, word);
        }
    }
}