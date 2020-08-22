using System.ComponentModel;
using System.Windows.Media;
using Microsoft.VisualStudio.Shell.Interop;

namespace PatternCustomizer.State
{

    interface IFormat : INotifyPropertyChanged, ICloneable<IFormat>
    {
        string DisplayName { get; set; }

        string DeclaredFormatName { get; set; }

        Color? ForegroundColor { get; set; }


        Color? BackgroundColor { get; set; }

        double? Opacity { get; set; }

        bool? IsItalic { get; set; }

        bool? IsBold { get; set; }

        bool TryGetDisplayName(out string name);

        bool TryGetOpacity(out double opacityValue);

        bool TryGetForegroundColor(out Color colorValue);

        bool TryGetBackgroundColor(out Color colorValue);

        bool TryGetIsItalic(out bool isItalicValue);

        bool TryGetIsBold(out bool isBoldValue);

        ColorableItemInfo ConvertToItemInfo(uint defaultTransparentColor);
    }
}
