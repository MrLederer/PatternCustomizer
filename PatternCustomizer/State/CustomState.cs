using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using EnvDTE;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using FormatName = System.String;

namespace PatternCustomizer.State
{
    // TODO: remove this json setting, and start using setting store
    internal class CustomState : IState
    {
        private Guid s_textEditorCategory = new Guid("A27B4E24-A735-4d1d-B8E7-9716E1E3D8E0");

        public event PropertyChangedEventHandler PropertyChanged;

        public BindingList<PatternToStyle> OrderedPatternToStyleMapping { get; set; }
        public BindingList<IRule> Rules { get; set; }
        public BindingList<IFormat> Formats { get; set; }

        private Task<UpdatingFontAndColorStatus> m_fontsAndColorStatus;
        private IDictionary<FormatName, (IFormat, IEnumerable<IRule>)> m_state;

        public CustomState(IEnumerable<(IRule, IFormat)> rulesAndFormats = null)
        {
            var safeRulesAndFormats = rulesAndFormats.NullToEmpty();
            this.Rules = safeRulesAndFormats.Select(_ => _.Item1)
                .Distinct()
                .ToBindingList();
            this.Formats = safeRulesAndFormats.Select(_ => _.Item2)
                .Distinct()
                .ToBindingList();
            this.OrderedPatternToStyleMapping = safeRulesAndFormats.Select(_ => new PatternToStyle(Rules.IndexOf(_.Item1), Formats.IndexOf(_.Item2)))
                .ToBindingList();
            UpdateFormatDeclaredName();
            UpdateInternalState();
            OrderedPatternToStyleMapping.ListChanged += PatternToStyleListChanged;
            Formats.ListChanged += StyleListChanged;
            m_fontsAndColorStatus = UpdateFormatsAsync();
        }

        public IFormat GetCustomFormatOrDefault(FormatName formatName)
        {
            if (m_state.TryGetValue(formatName, out var formatAndRules))
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
            if (m_state.TryGetValue(formatName, out var result))
            {
                return result.Item2;
            }
            else
            {
                return Enumerable.Empty<IRule>();
            }
        }

        public IState Load(string filepath)
        {
            if (File.Exists(filepath))
            {
                var settingJson = File.ReadAllText(filepath);
                var (mappings, rules, formats) = settingJson.FromJson<(IEnumerable<(int, int)>, IEnumerable<IRule>, IEnumerable<IFormat>)>();

                this.OrderedPatternToStyleMapping.Clear();

                this.Rules.Clear();
                foreach (var rule in rules)
                {
                    this.Rules.Add(rule);
                }

                this.Formats.Clear();
                foreach (var format in formats)
                {
                    this.Formats.Add(format);
                }
                UpdateFormatDeclaredName();
                
                foreach (var mapping in mappings)
                {
                    this.OrderedPatternToStyleMapping.Add(new PatternToStyle(mapping.Item1, mapping.Item2));
                }
                UpdateInternalState();

                if (filepath != StateUtils.DefaultFilePath)
                {
                    Save(StateUtils.DefaultFilePath);
                }
                m_fontsAndColorStatus = UpdateFormatsAsync();
            }
            return this;
        }

        public IState Save(string filepath)
        {
            var mappingCopy = this.OrderedPatternToStyleMapping.Select(_ => (_.RuleIndex, _.FormatIndex));
            var formatsCopy = this.Formats.Select(_ => _.Clone());
            var rulesCopy = this.Rules.Select(_ => _.Clone());
            var serializedObj = (mappingCopy, rulesCopy, formatsCopy).ToJson();
            var directory = Path.GetDirectoryName(filepath);
            Directory.CreateDirectory(directory);
            File.WriteAllText(filepath, serializedObj);
            return this;
        }

        private void PatternToStyleListChanged(object sender, ListChangedEventArgs e)
        {
            UpdateInternalState();
            Save(StateUtils.DefaultFilePath);
        }

        private void StyleListChanged(object sender, ListChangedEventArgs e)
        {
            UpdateFormatDeclaredName();
            UpdateFormatsAsync();
        }

        private async Task<UpdatingFontAndColorStatus> UpdateFormatsAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            var fontAndColorStorageService = Package.GetGlobalService(typeof(SVsFontAndColorStorage)) as IVsFontAndColorStorage;
            try
            {
                UpdateFormats(fontAndColorStorageService);
                return UpdatingFontAndColorStatus.UP_TO_DATE;
            }
            catch
            {
                return UpdatingFontAndColorStatus.NEED_TO_UPDATE;
            }
        }

        private void UpdateFormatDeclaredName()
        {
            var formatEntries = Formats.Count();
            if (Constants.AllDeclaredFormatNames.Length < formatEntries)
            {
                // MSGBOX Tell user that can not configure more formats
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
        }

        private void UpdateFormats(IVsFontAndColorStorage fontAndColorStorageService)
        {
            if (fontAndColorStorageService != null)
            {
                uint flags = (uint)(__FCSTORAGEFLAGS.FCSF_LOADDEFAULTS | __FCSTORAGEFLAGS.FCSF_READONLY);
                var hr = fontAndColorStorageService.OpenCategory(ref s_textEditorCategory, flags);
                ErrorHandler.ThrowOnFailure(hr);
                try
                {
                    hr = ((IVsFontAndColorUtilities)fontAndColorStorageService).EncodeAutomaticColor(out uint defaultTransparentColor);
                    ErrorHandler.ThrowOnFailure(hr);
                    foreach (var format in Formats)
                    {
                        hr = fontAndColorStorageService.SetItem(format.DeclaredFormatName, new[] { format.ConvertToItemInfo(defaultTransparentColor) });
                        ErrorHandler.ThrowOnFailure(hr);
                    }
                }
                finally
                {
                    hr = fontAndColorStorageService.CloseCategory();
                    ErrorHandler.ThrowOnFailure(hr);
                }
            }
        }

        private void UpdateInternalState()
        {
            //if (this.OrderedPatternToStyleMapping.Count != this.OrderedPatternToStyleMapping.Select(_ => _.RuleIndex).Distinct().Count())
            //{
            //    // MSGBOX Tell user that a pattern should not have two mappings
            //    throw new NotSupportedException($"Can't configure more than one mapping for a single pattern");
            //}

            m_state = this.OrderedPatternToStyleMapping.GroupBy(_ => this.Formats.ElementAt(_.FormatIndex), _ => this.Rules.ElementAt(_.RuleIndex))
                .ToDictionary(_ => _.Key.DeclaredFormatName, _ => (format: _.Key, rules: _.AsEnumerable()));
        }
    }
}
