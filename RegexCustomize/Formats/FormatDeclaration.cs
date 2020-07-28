using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace PatternCustomizer.Formats
{
    internal static class FormatDeclaration
    {
#pragma warning disable CS0649
        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.Format0)]
        internal static ClassificationTypeDefinition Format0;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.Format1)]
        internal static ClassificationTypeDefinition Format1;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.Format2)]
        internal static ClassificationTypeDefinition Format2;
#pragma warning restore CS0649
    }
}
