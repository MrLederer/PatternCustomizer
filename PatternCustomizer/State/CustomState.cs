using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.PlatformUI;
using Newtonsoft.Json;
using FormatName = System.String;

namespace PatternCustomizer.State
{
    // TODO: remove this json setting, and start using setting store
    internal class CustomState : IState
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public BindingList<PatternToStyle> OrderedPatternToStyleMapping { get; set; }
        public BindingList<IRule> Rules { get; set; }
        public BindingList<IFormat> Formats { get; set; }

        private IDictionary<FormatName, (IFormat, IEnumerable<IRule>)> _state;
        private HashSet<IFormat> _usedAndDistinctFormats;
        private string _settingFile;

        public CustomState(IEnumerable<(IRule, IFormat)> rulesAndFormats = null)
        {
            this._settingFile = StateUtils.GetDefaultFilePath();
            this.OrderedPatternToStyleMapping = rulesAndFormats.NullToEmpty().Select(_ => new PatternToStyle(_.Item1, _.Item2)).ToBindingList();
            this.Rules = OrderedPatternToStyleMapping.Select(_ => _.rule)
                .Distinct()
                .ToBindingList();
            this.Formats = OrderedPatternToStyleMapping.Select(_ => _.format)
                .Distinct()
                .ToBindingList();

            RebuildInternalStateFromPublicState();
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
            return Constants.AllDeclaredFormatNames.Take(_usedAndDistinctFormats.Count);
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
                var (mapping, rules, Formats) = settingJson.FromJson<(IEnumerable<(IRule, IFormat)>, IEnumerable<IRule>, IEnumerable<IFormat>)>();
                this.OrderedPatternToStyleMapping = mapping.Select(_ => new PatternToStyle(_.Item1, _.Item2)).ToBindingList();
                this.Rules = rules.ToBindingList();
                this.Formats = Formats.ToBindingList();
                RebuildInternalStateFromPublicState();
            }
            return this;
        }

        public IState Save()
        {
            var mappingCopy = this.OrderedPatternToStyleMapping.Select(_ => (_.rule.Clone(), _.format.Clone()));
            var formatsCopy = this.Formats.Select(_ => _.Clone());
            var rulesCopy = this.Rules.Select(_ => _.Clone());
            var serializedObj = (mappingCopy, rulesCopy, formatsCopy).ToJson();
            var directory = Path.GetDirectoryName(this._settingFile);
            Directory.CreateDirectory(directory);
            File.WriteAllText(this._settingFile, serializedObj);
            return this;
        }

        private void RebuildInternalStateFromPublicState()
        {
            _usedAndDistinctFormats = this.OrderedPatternToStyleMapping.Select(_ => _.format).ToHashSet();
            var formatEntries = _usedAndDistinctFormats.Count();
            if (Constants.AllDeclaredFormatNames.Length < formatEntries)
            {
                throw new NotSupportedException($"Can't configure more than {formatEntries} formats");
            }

            using (var formatsIterator = _usedAndDistinctFormats.GetEnumerator())
            {
                using (var declaredNamesIterator = Constants.AllDeclaredFormatNames.Take(formatEntries).GetEnumerator())
                {
                    while (formatsIterator.MoveNext() && declaredNamesIterator.MoveNext())
                    {
                        formatsIterator.Current.DeclaredFormatName = declaredNamesIterator.Current;
                    }
                }
            }

            _state = this.OrderedPatternToStyleMapping.GroupBy(_ => _.format, _ => _.rule)
                .ToDictionary(_ => _.Key.DeclaredFormatName, _ => (format: _.Key, rules: _.AsEnumerable()));
        }
    }
}
