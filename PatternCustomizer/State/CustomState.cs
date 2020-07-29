using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using FormatName = System.String;

namespace PatternCustomizer.State
{
    internal class CustomState : IState
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _settingFile;

        public IList<(IRule, IFormat)> orderedPatternToStyleMapping { get; set; }
        public ISet<IRule> rules { get; set; }

        public ISet<IFormat> formats { get; set; }

        // TODO: remove this json setting, and start using setting store
        [JsonProperty(ItemTypeNameHandling = TypeNameHandling.Auto)]
        private IDictionary<FormatName, (IFormat, IEnumerable<IRule>)> _state;

        public CustomState(IEnumerable<(IRule, IFormat)> rulesAndFormats = null)
        {
            rulesAndFormats = rulesAndFormats ?? Enumerable.Empty<(IRule, IFormat)>();

            formats = rulesAndFormats.Select(_ => _.Item2)
                .ToHashSet();
            rules = rulesAndFormats.Select(_ => _.Item1)
                .ToHashSet();


            var formatEntries = formats.Count();
            if (Constants.AllFormats.Length < formatEntries)
            {
                throw new NotSupportedException($"Can't configure more than {formatEntries} formats");
            }

            this._settingFile = StateUtils.GetDefaultFilePath();
            this._state = rulesAndFormats
                .Zip(Constants.AllFormats.Take(formatEntries), (ruleAndFormat, formatName) => (formatName, rule: ruleAndFormat.Item1, format: ruleAndFormat.Item2))
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
