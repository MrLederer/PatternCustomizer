using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using RegexCustomize.State;

namespace RegexCustomize
{
    [Export(typeof(ITaggerProvider))]
    [ContentType("CSharp")]
    [TagType(typeof(IClassificationTag))]
    internal class RegexCustomizerProvider : ITaggerProvider
    {
#pragma warning disable CS0649
        [Import]
        internal IClassificationTypeRegistryService ClassificationRegistry; // this some Visual studio MEF magic
#pragma warning restore CS0649

        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
        {
            // TODO: try to provide state to here
            return (ITagger<T>)new RegexCustomizer(buffer, ClassificationRegistry, RegexCustomizePackage.currentState);
        }
    }
    class RegexCustomizer : ITagger<IClassificationTag>
    {
        private readonly ITextBuffer _theBuffer;
        private readonly IDictionary<string, IClassificationType> _formatNameToClassType;
#pragma warning disable CS0067
        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;
#pragma warning restore CS0067

        internal RegexCustomizer(ITextBuffer buffer, IClassificationTypeRegistryService registry, IState state)
        {
            _theBuffer = buffer;
            _formatNameToClassType = state.GetEnabledFormats()
                .ToDictionary(formatName => formatName, formatName => registry.GetClassificationType(formatName));
        }

        public IEnumerable<ITagSpan<IClassificationTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            throw new NotImplementedException();
        }
    }
}
