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

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.Format3)]
        internal static ClassificationTypeDefinition Format3;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.Format4)]
        internal static ClassificationTypeDefinition Format4;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.Format5)]
        internal static ClassificationTypeDefinition Format5;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.Format6)]
        internal static ClassificationTypeDefinition Format6;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.Format7)]
        internal static ClassificationTypeDefinition Format7;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.Format8)]
        internal static ClassificationTypeDefinition Format8;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.Format9)]
        internal static ClassificationTypeDefinition Format9;
#pragma warning restore CS0649
    }
}
