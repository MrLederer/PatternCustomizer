using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.Debugger.ComponentInterfaces;
using Microsoft.VisualStudio.Debugger.Evaluation;
using Microsoft.VisualStudio.Debugger.Interop;
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
        private string _settingFile;

        public CustomState(IEnumerable<(IRule, IFormat)> rulesAndFormats = null)
        {
            var safeRulesAndFormats = rulesAndFormats.NullToEmpty();
            this._settingFile = StateUtils.GetDefaultFilePath();
            this.Rules = safeRulesAndFormats.Select(_ => _.Item1)
                .Distinct()
                .ToBindingList();
            this.Formats = safeRulesAndFormats.Select(_ => _.Item2)
                .Distinct()
                .ToBindingList();
            this.OrderedPatternToStyleMapping = safeRulesAndFormats.Select(_ => new PatternToStyle(Rules.IndexOf(_.Item1), Formats.IndexOf(_.Item2)))
                .ToBindingList();
            UpdateInternalState();
            OrderedPatternToStyleMapping.ListChanged += PatternToStyleListChanged;
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
            return Constants.AllDeclaredFormatNames.Take(Formats.Count);
        }

        public IEnumerable<IRule> GetRules(string formatName)
        {
            (IFormat, IEnumerable<IRule>) result;
            if (_state.TryGetValue(formatName, out result))
            {
                return result.Item2;
            }
            else
            {
                return Enumerable.Empty<IRule>();
            }
        }

        public IState Load()
        {
            if (File.Exists(this._settingFile))
            {
                var settingJson = File.ReadAllText(this._settingFile);
                var (mapping, rules, formats) = settingJson.FromJson<(IEnumerable<(int, int)>, IEnumerable<IRule>, IEnumerable<IFormat>)>();
                this.Rules = rules.ToBindingList();
                this.Formats = formats.ToBindingList();
                this.OrderedPatternToStyleMapping = mapping.Select(_ => new PatternToStyle(_.Item1, _.Item2)).ToBindingList();
                UpdateInternalState();
                this.OrderedPatternToStyleMapping.ListChanged += PatternToStyleListChanged;
            }
            return this;
        }

        public IState Save()
        {
            var mappingCopy = this.OrderedPatternToStyleMapping.Select(_ => (_.RuleIndex, _.FormatIndex));
            var formatsCopy = this.Formats.Select(_ => _.Clone());
            var rulesCopy = this.Rules.Select(_ => _.Clone());
            var serializedObj = (mappingCopy, rulesCopy, formatsCopy).ToJson();
            var directory = Path.GetDirectoryName(this._settingFile);
            Directory.CreateDirectory(directory);
            File.WriteAllText(this._settingFile, serializedObj);
            return this;
        }

        private void PatternToStyleListChanged(object sender, ListChangedEventArgs e)
        {
            UpdateInternalState();
        }

        private void UpdateInternalState()
        {
            var formatEntries = Formats.Count();
            if (Constants.AllDeclaredFormatNames.Length < formatEntries)
            {
                throw new NotSupportedException($"Can't configure more than {formatEntries} formats");
            }

            using (var formatsIterator = Formats.GetEnumerator())
            {
                using (var declaredNamesIterator = Constants.AllDeclaredFormatNames.Take(formatEntries).GetEnumerator())
                {
                    while (formatsIterator.MoveNext() && declaredNamesIterator.MoveNext())
                    {
                        formatsIterator.Current.DeclaredFormatName = declaredNamesIterator.Current;
                    }
                }
            }

            _state = this.OrderedPatternToStyleMapping.GroupBy(_ => this.Formats.ElementAt(_.FormatIndex), _ => this.Rules.ElementAt(_.RuleIndex))
                .ToDictionary(_ => _.Key.DeclaredFormatName, _ => (format: _.Key, rules: _.AsEnumerable()));
        }
    }
}
