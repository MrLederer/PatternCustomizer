using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace PatternCustomizer.State
{
    [JsonObject(MemberSerialization.Fields)]
    internal class RegexRule : IRule
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string Name { get; set; }
        public string RegexPattern { get { return _regex.ToString(); } set { _regex = new Regex(value, RegexOptions.Compiled); } }

        private Regex _regex;


        public RegexRule(string regexPattern, string name)
        {
            this.RegexPattern = regexPattern;
            this.Name = name;
        }


        public IEnumerable<Match> Detect(string text)
        {
            return _regex.Matches(text).Cast<Match>();
        }
        public override bool Equals(object obj)
        {
            var other = obj as IRule;
            return other != null &&
                other.Name == this.Name &&
                other.RegexPattern == this.RegexPattern;
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode() ^
                this.RegexPattern.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
