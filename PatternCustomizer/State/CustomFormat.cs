using System.ComponentModel;
using System.Windows.Media;
using Newtonsoft.Json;

namespace PatternCustomizer.State
{
    [JsonObject(MemberSerialization.Fields)]
    class CustomFormat : IFormat
    {
        public string DisplayName { get; set; }
        public string DeclaredFormatName { get; set; }
        public Color? Color { get; set; }
        public double? Opacity { get; set; }
        public bool? IsItalic { get; set; }
        public bool? IsBold { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public CustomFormat(string name, Color ? color = null, double? opacity = null, bool? isItalic = null, bool? isBold = null)
        {
            this.DisplayName = name ?? "";
            this.Color = color;
            this.Opacity = opacity;
            this.IsItalic = isItalic;
            this.IsBold = isBold;
        }

        public bool TryGetColor(out Color colorValue)
        {
            if (Color.HasValue)
            {
                colorValue = Color.Value;
            }
            else
            {
                colorValue = default;
            }
            return Color.HasValue;
        }

        public bool TryGetOpacity(out double opacityValue)
        {
            if (Opacity.HasValue)
            {
                opacityValue = Opacity.Value;
            }
            else
            {
                opacityValue = default;
            }
            return Opacity.HasValue;
        }

        public bool TryGetIsItalic(out bool isItalicValue)
        {
            if (IsItalic.HasValue)
            {
                isItalicValue = IsItalic.Value;
            }
            else
            {
                isItalicValue = default;
            }
            return Opacity.HasValue;
        }

        public bool TryGetIsBold(out bool isBoldValue)
        {
            if (IsBold.HasValue)
            {
                isBoldValue = IsBold.Value;
            }
            else
            {
                isBoldValue = default;
            }
            return IsBold.HasValue;
        }

        public bool TryGetDisplayName(out string name)
        {
            if (DisplayName != null)
            {
                name = DisplayName;
            }
            else
            {
                name = default;
            }
            return DisplayName != null;
        }

        public override bool Equals(object obj)
        {
            var other = obj as IFormat;
            return other != null &&
                other.DisplayName == this.DisplayName &&
                other.Color == this.Color &&
                other.Opacity == this.Opacity &&
                other.IsItalic == this.IsItalic &&
                other.IsBold == this.IsBold;
        }

        public override int GetHashCode()
        {
            var rollingHash = this.DisplayName != null ? this.DisplayName.GetHashCode() : 0;
            rollingHash ^= this.Color != null ? this.Color.GetHashCode() : 0;
            rollingHash ^= this.Opacity != null ? this.Opacity.GetHashCode() : 0;
            rollingHash ^= this.IsItalic != null ? this.IsItalic.GetHashCode() : 0;
            rollingHash ^= this.IsBold != null ? this.IsBold.GetHashCode() : 0;
            return rollingHash;
        }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
