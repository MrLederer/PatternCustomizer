using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PatternCustomizer.State
{
    internal class RegexRule : IRule
    {
        private Regex _regex;

        public RegexRule(Regex regex)
        {
            this._regex = regex;
        }
        public IEnumerable<Match> Detect(string text)
        {
            return _regex.Matches(text).Cast<Match>();
        }
    }
}
