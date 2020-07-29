using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using FormatName = System.String;

namespace PatternCustomizer.State
{
    // TODO: remove this json setting, and start using setting store
    internal class CustomState : IState
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [JsonProperty(ItemTypeNameHandling = TypeNameHandling.Auto)]
        public IList<(IRule, IFormat)> OrderedPatternToStyleMapping { get; set; }
        public IList<IRule> DistinctRules { get; set; }
        public IList<IFormat> DistinctFormats { get; set; }

        private IDictionary<FormatName, (IFormat, IEnumerable<IRule>)> _state;
        private string _settingFile;

        public CustomState(IEnumerable<(IRule, IFormat)> rulesAndFormats = null)
        {
            this._settingFile = StateUtils.GetDefaultFilePath();
            this.OrderedPatternToStyleMapping = rulesAndFormats.NullToEmpty().ToList();
            RebuildStateFromRulesAndFormats(this.OrderedPatternToStyleMapping);
        }

        private void RebuildStateFromRulesAndFormats(IEnumerable<(IRule, IFormat)> rulesAndFormats)
        {
            DistinctRules = rulesAndFormats.Select(_ => _.Item1)
                .Distinct()
                .ToList();
            DistinctFormats = rulesAndFormats.Select(_ => _.Item2)
                .Distinct()
                .ToList();

            AssignedDeclaredFormatNames();

            _state = rulesAndFormats.GroupBy(_ => _.Item2, _ => _.Item1)
                .ToDictionary(_ => _.Key.DeclaredFormatName, _ => (format: _.Key, rules: _.AsEnumerable()));
        }

        public IFormat GetCustomFormatOrDefault(FormatName formatName)
        {
            (IFormat, IEnumerable<IRule>) formatAndRules;
            if (_state.TryGetValue(formatName, out formatAndRules))
            {
                return formatAndRules.Item1;
            }
            return default;
        }

        public IEnumerable<FormatName> GetEnabledDeclaredFormatNames()
        {
            return Constants.AllDeclaredFormatNames.Take(DistinctFormats.Count);
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
                this.OrderedPatternToStyleMapping  = savedSetting.OrderedPatternToStyleMapping;
                RebuildStateFromRulesAndFormats(this.OrderedPatternToStyleMapping);
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

        private void AssignedDeclaredFormatNames()
        {
            var formatEntries = DistinctFormats.Count();
            if (Constants.AllDeclaredFormatNames.Length < formatEntries)
            {
                throw new NotSupportedException($"Can't configure more than {formatEntries} formats");
            }

            using (var formatsIterator = DistinctFormats.GetEnumerator())
            {
                using (var declaredNamesIterator = Constants.AllDeclaredFormatNames.Take(formatEntries).GetEnumerator())
                {
                    while (formatsIterator.MoveNext() && declaredNamesIterator.MoveNext())
                    {
                        formatsIterator.Current.DeclaredFormatName = declaredNamesIterator.Current;
                    }
                }
            }
        }
    }
}
