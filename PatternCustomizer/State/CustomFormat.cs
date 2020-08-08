using System;
using System.ComponentModel;
using System.Windows.Media;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TextManager.Interop;
using Newtonsoft.Json;

namespace PatternCustomizer.State
{
    [JsonObject(MemberSerialization.Fields)]
    class CustomFormat : IFormat
    {
        public string DisplayName { get; set; }
        public string DeclaredFormatName { get; set; }
        public Color? ForegroundColor { get; set; }
        public Color? BackgroundColor { get; set; }
        public double? Opacity { get; set; }
        public bool? IsItalic { get; set; }
        public bool? IsStrikethrough { get; set; }
        public bool? IsBold { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public CustomFormat(string name, Color? foregroundColor = null, Color? backgroundColor = null, double? opacity = null, bool? isItalic = null, bool? isBold = null, bool? isStrikethrough = null)
        {
            this.DisplayName = name ?? "";
            this.ForegroundColor = foregroundColor;
            this.BackgroundColor = backgroundColor;
            this.Opacity = opacity;
            this.IsItalic = isItalic;
            this.IsBold = isBold;
        }

        public bool TryGetForegroundColor(out Color colorValue)
        {
            if (ForegroundColor.HasValue)
            {
                colorValue = ForegroundColor.Value;
                return true;
            }
            else
            {
                colorValue = default;
                return false;
            }
        }


        public bool TryGetBackgroundColor(out Color colorValue)
        {
            if (BackgroundColor.HasValue)
            {
                colorValue = BackgroundColor.Value;
                return true;
            }
            else
            {
                colorValue = default;
                return false;
            }
        }

        public bool TryGetOpacity(out double opacityValue)
        {
            if (Opacity.HasValue)
            {
                opacityValue = Opacity.Value;
                return true;
            }
            else
            {
                opacityValue = default;
                return false;
            }
        }

        public bool TryGetIsItalic(out bool isItalicValue)
        {
            if (IsItalic.HasValue)
            {
                isItalicValue = IsItalic.Value;
                return true;
            }
            else
            {
                isItalicValue = default;
                return false;
            }
        }

        public bool TryGetIsStrikethrough(out bool isStrikethrough)
        {
            if (IsStrikethrough.HasValue)
            {
                isStrikethrough = IsStrikethrough.Value;
                return true;
            }
            else
            {
                isStrikethrough = default;
                return false;
            }
        }

        public bool TryGetIsBold(out bool isBoldValue)
        {
            if (IsBold.HasValue)
            {
                isBoldValue = IsBold.Value;
                return true;
            }
            else
            {
                isBoldValue = default;
                return false;
            }
        }

        public bool TryGetDisplayName(out string name)
        {
            if (DisplayName != null)
            {
                name = DisplayName;
                return true;
            }
            else
            {
                name = default;
                return false;
            }
        }

        public override bool Equals(object obj)
        {
            var other = obj as IFormat;
            return other != null &&
                other.DisplayName == this.DisplayName &&
                other.ForegroundColor == this.ForegroundColor &&
                other.BackgroundColor == this.BackgroundColor &&
                other.Opacity == this.Opacity &&
                other.IsItalic == this.IsItalic &&
                other.IsBold == this.IsBold;
        }

        public override int GetHashCode()
        {
            var rollingHash = this.DisplayName != null ? this.DisplayName.GetHashCode() : 0;
            rollingHash ^= this.ForegroundColor != null ? this.ForegroundColor.GetHashCode() : 0;
            rollingHash ^= this.BackgroundColor != null ? this.BackgroundColor.GetHashCode() : 0;
            rollingHash ^= this.Opacity != null ? this.Opacity.GetHashCode() : 0;
            rollingHash ^= this.IsItalic != null ? this.IsItalic.GetHashCode() : 0;
            rollingHash ^= this.IsBold != null ? this.IsBold.GetHashCode() : 0;
            return rollingHash;
        }

        public override string ToString()
        {
            return DisplayName;
        }

        public IFormat Clone()
        {
            return new CustomFormat(this.DisplayName, this.ForegroundColor, this.BackgroundColor, this.Opacity, this.IsItalic, this.IsBold, this.IsStrikethrough);
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        public ColorableItemInfo ConvertToItemInfo()
        {
            var result = new ColorableItemInfo();
            if (this.TryGetForegroundColor(out var foregroundColor))
            {
                result.crForeground  = (uint)foregroundColor.ToRGB();
                result.bForegroundValid = 1;
            }
            else
            {
                result.bForegroundValid = 0;
            }
            
            if (this.TryGetBackgroundColor(out var backgroundColor))
            {
                result.crBackground = (uint)backgroundColor.ToRGB();
                result.bBackgroundValid = 1;
            }
            else
            {
                result.bBackgroundValid = 0;
            }

            if (this.TryGetIsStrikethrough(out var isStrikethrough))
            {
                result.dwFontFlags |= (uint)FONTFLAGS.FF_STRIKETHROUGH;
                result.bFontFlagsValid = 1;
            }
            if (this.TryGetIsBold(out var isBold))
            {
                result.dwFontFlags |= (uint)FONTFLAGS.FF_BOLD;
                result.bFontFlagsValid = 1;
            }

            return result;
        }
    }
}
