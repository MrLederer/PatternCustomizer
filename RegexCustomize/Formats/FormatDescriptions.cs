using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.CodeAnalysis.Classification;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using RegexCustomize.State;
using static RegexCustomize.RegexCustomizePackage;

namespace RegexCustomize.Formats
{
    public class CustomizableClassificationFormatDefinition : ClassificationFormatDefinition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomizableClassificationFormatDefinition"/> class.
        /// This is done this way because <see cref="ClassificationFormatDefinition"/> enforces protected setters
        /// </summary>
        /// <param name="customFormat">The custom format.</param>
        internal CustomizableClassificationFormatDefinition(IFormat customFormat = null)
        {
            if (customFormat == null)
            {
                return;
            }

            string name;
            if (customFormat.TryGetDisplayName(out name))
            {
                DisplayName = name;
            }

            Color color;
            if (customFormat.TryGetColor(out color))
            {
                ForegroundColor = color;
            }

            double opacity;
            if (customFormat.TryGetOpacity(out opacity))
            {
                ForegroundOpacity = opacity;
            }

            bool isItalic;
            if (customFormat.TryGetIsItalic(out isItalic))
            {
                IsItalic = isItalic;
            }

            bool isBold;
            if (customFormat.TryGetIsItalic(out isBold))
            {
                IsBold = isBold;
            }
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.Format0)]
    [Name(Constants.Format0)]
    [UserVisible(true)]
    [Order(After = ClassificationTypeNames.Identifier)]
    public class Format0 : CustomizableClassificationFormatDefinition
    {
        public Format0() : base(currentState.GetCustomFormatOrDefault(Constants.Format0))
        {
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.Format1)]
    [Name(Constants.Format1)]
    [UserVisible(true)]
    [Order(After = ClassificationTypeNames.Identifier)]
    internal sealed class Format1 : CustomizableClassificationFormatDefinition
    {
        public Format1() : base(currentState.GetCustomFormatOrDefault(Constants.Format1))
        {
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.Format2)]
    [Name(Constants.Format2)]
    [UserVisible(true)]
    [Order(After = ClassificationTypeNames.Identifier)]
    internal sealed class Format2 : CustomizableClassificationFormatDefinition
    {
        public Format2() : base(currentState.GetCustomFormatOrDefault(Constants.Format2))
        {
        }
    }
}
