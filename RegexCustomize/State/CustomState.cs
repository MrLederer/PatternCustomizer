﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FormatName = System.String;

namespace RegexCustomize.State
{
    internal class CustomState : IState
    {
        private IDictionary<FormatName, (IFormat, IEnumerable<IRule>)> _state;
        public CustomState(IEnumerable<(IRule, IFormat)> rulesAndFormats)
        {
            var stateEntries = rulesAndFormats.Count();
            if (Constants.AllFormats.Length < stateEntries)
            {
                throw new NotSupportedException($"Can't configure more than {stateEntries} formats");
            }

            this._state = rulesAndFormats
                .Zip(Constants.AllFormats.Take(stateEntries), (ruleAndFormat, formatName) => (formatName, rule: ruleAndFormat.Item1, format: ruleAndFormat.Item2))
                .ToDictionary(_ => _.formatName, _ => (_.format, rules: _.rule.ToEnumerable()));
        }

        public IFormat GetCustomFormatOrDefault(string formatName)
        {
            (IFormat, IEnumerable<IRule>) formatAndRules;
            if (_state.TryGetValue(formatName, out formatAndRules))
            {
                return formatAndRules.Item1;
            }
            return default;
        }

        public IEnumerable<string> GetEnabledFormats()
        {
            return Constants.AllFormats.Take(_state.Count());
        }
        public IEnumerable<IRule> GetRules(string formatName)
        {
            return _state[formatName].Item2;
        }
    }
}
