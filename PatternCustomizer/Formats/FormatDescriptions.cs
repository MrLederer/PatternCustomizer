using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.CodeAnalysis.Classification;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using PatternCustomizer.State;
using static PatternCustomizer.PatternCustomizerPackage;

namespace PatternCustomizer.Formats
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

            if (customFormat.TryGetDisplayName(out var name))
            {
                DisplayName = name;
            }

            if (customFormat.TryGetForegroundColor(out var foregroundColor))
            {
                ForegroundColor = foregroundColor;
            }

            if (customFormat.TryGetBackgroundColor(out var backgroundColor))
            {
                BackgroundColor = backgroundColor;
            }

            if (customFormat.TryGetOpacity(out var opacity))
            {
                ForegroundOpacity = opacity;
            }

            if (customFormat.TryGetIsItalic(out var isItalic))
            {
                IsItalic = isItalic;
            }

            if (customFormat.TryGetIsItalic(out var isBold))
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
    internal sealed class Format0 : CustomizableClassificationFormatDefinition
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

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.Format3)]
    [Name(Constants.Format3)]
    [UserVisible(true)]
    [Order(After = ClassificationTypeNames.Identifier)]
    internal sealed class Format3 : CustomizableClassificationFormatDefinition
    {
        public Format3() : base(currentState.GetCustomFormatOrDefault(Constants.Format3))
        {
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.Format4)]
    [Name(Constants.Format4)]
    [UserVisible(true)]
    [Order(After = ClassificationTypeNames.Identifier)]
    internal sealed class Format4 : CustomizableClassificationFormatDefinition
    {
        public Format4() : base(currentState.GetCustomFormatOrDefault(Constants.Format4))
        {
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.Format5)]
    [Name(Constants.Format5)]
    [UserVisible(true)]
    [Order(After = ClassificationTypeNames.Identifier)]
    internal sealed class Format5 : CustomizableClassificationFormatDefinition
    {
        public Format5() : base(currentState.GetCustomFormatOrDefault(Constants.Format5))
        {
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.Format6)]
    [Name(Constants.Format6)]
    [UserVisible(true)]
    [Order(After = ClassificationTypeNames.Identifier)]
    internal sealed class Format6 : CustomizableClassificationFormatDefinition
    {
        public Format6() : base(currentState.GetCustomFormatOrDefault(Constants.Format6))
        {
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.Format7)]
    [Name(Constants.Format7)]
    [UserVisible(true)]
    [Order(After = ClassificationTypeNames.Identifier)]
    internal sealed class Format7 : CustomizableClassificationFormatDefinition
    {
        public Format7() : base(currentState.GetCustomFormatOrDefault(Constants.Format7))
        {
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.Format8)]
    [Name(Constants.Format8)]
    [UserVisible(true)]
    [Order(After = ClassificationTypeNames.Identifier)]
    internal sealed class Format8 : CustomizableClassificationFormatDefinition
    {
        public Format8() : base(currentState.GetCustomFormatOrDefault(Constants.Format8))
        {
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.Format9)]
    [Name(Constants.Format9)]
    [UserVisible(true)]
    [Order(After = ClassificationTypeNames.Identifier)]
    internal sealed class Format9 : CustomizableClassificationFormatDefinition
    {
        public Format9() : base(currentState.GetCustomFormatOrDefault(Constants.Format9))
        {
        }
    }
}
