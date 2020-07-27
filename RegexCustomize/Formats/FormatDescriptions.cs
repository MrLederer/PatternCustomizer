using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.CodeAnalysis.Classification;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace RegexCustomize.Formats
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.Format0)]
    [Name(Constants.Format0)]
    [UserVisible(true)]
    [Order(After = ClassificationTypeNames.Identifier)]
    internal sealed class Format0 : ClassificationFormatDefinition
    {
        public Format0()
        {
            DisplayName = "Format 0";
            ForegroundColor = Colors.SaddleBrown;
            ForegroundOpacity = 0.2;
            BackgroundOpacity = 0.2;
            IsItalic = true;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.Format1)]
    [Name(Constants.Format1)]
    [UserVisible(true)]
    [Order(After = ClassificationTypeNames.Identifier)]
    internal sealed class Format1 : ClassificationFormatDefinition
    {
        public Format1()
        {
            DisplayName = "Format 1";
            ForegroundColor = Colors.LightGreen;
            ForegroundOpacity = 0.5;
            BackgroundOpacity = 0.5;
            IsBold = true;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.Format2)]
    [Name(Constants.Format2)]
    [UserVisible(true)]
    [Order(After = ClassificationTypeNames.Identifier)]
    internal sealed class Format2 : ClassificationFormatDefinition
    {
        public Format2()
        {
            DisplayName = "Format 2";
            ForegroundColor = Colors.CadetBlue;
            ForegroundOpacity = 0.9;
            BackgroundOpacity = 0.9;
        }
    }
}
