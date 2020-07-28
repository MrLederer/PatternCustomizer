﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using FormatName = System.String;

namespace PatternCustomizer.State
{
    internal class CustomState : IState
    {
        private string _settingFile;

        [JsonProperty(ItemTypeNameHandling = TypeNameHandling.Auto)]
        private IDictionary<FormatName, (IFormat, IEnumerable<IRule>)> _state;

        public CustomState(IEnumerable<(IRule, IFormat)> rulesAndFormats = null)
        {
            rulesAndFormats = rulesAndFormats ?? Enumerable.Empty<(IRule, IFormat)>();

            var stateEntries = rulesAndFormats.Count();
            if (Constants.AllFormats.Length < stateEntries)
            {
                throw new NotSupportedException($"Can't configure more than {stateEntries} formats");
            }
            this._settingFile = StateUtils.GetDefaultFilePath();
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

        public IState Load()
        {
            if (File.Exists(this._settingFile))
            {
                var settingJson = File.ReadAllText(this._settingFile);
                var savedSetting = settingJson.FromJson<CustomState>();
                _state = savedSetting._state;
            }
            return this;
        }

        public IState Save()
        {
            var serializedObj = this.ToJson();
            var directory = Path.GetDirectoryName(this._settingFile);
            Directory.CreateDirectory(directory);
            File.WriteAllText(this._settingFile, serializedObj);
            return this;
        }
    }
}