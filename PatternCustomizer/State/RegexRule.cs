using System;
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
        public string DisplayName { get; set; }
        public string RegexPattern { get { return _regex.ToString(); } set { _regex = new Regex(value, RegexOptions.Compiled); } }

        private Regex _regex;

        public RegexRule()
        {
            this.RegexPattern = string.Empty;
            this.DisplayName = string.Empty;
        }

        public RegexRule(string regexPattern, string name)
        {
            this.RegexPattern = regexPattern;
            this.DisplayName = name;
        }


        public IEnumerable<Match> Detect(string text)
        {
            return _regex.Matches(text).Cast<Match>();
        }
        public override bool Equals(object obj)
        {
            var other = obj as IRule;
            return other != null &&
                other.DisplayName == this.DisplayName &&
                other.RegexPattern == this.RegexPattern;
        }

        public override int GetHashCode()
        {
            var rollingHash = this.DisplayName != null ? this.DisplayName.GetHashCode() : 0;
            rollingHash ^= this.RegexPattern != null ? this.RegexPattern.GetHashCode() : 0;
            return rollingHash;
        }

        public override string ToString()
        {
            return DisplayName;
        }

        public IRule Clone()
        {
            return new RegexRule(this.RegexPattern, this.DisplayName);
        }

        object ICloneable.Clone()
        {
            return Clone();
        }
    }
}
