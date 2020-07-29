using System.ComponentModel;
using System.Windows.Media;
using Newtonsoft.Json;

namespace PatternCustomizer.State
{
    [JsonObject(MemberSerialization.Fields)]
    class CustomFormat : IFormat
    {
        public string Name { get; set; }
        public Color? Color { get; set; }
        public double? Opacity { get; set; }
        public bool? IsItalic { get; set; }
        public bool? IsBold { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public CustomFormat(Color? color = null, double? opacity = null, bool? isItalic = null, bool? isBold = null, string name = null)
        {
            this.Name = name;
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
            if (Name != null)
            {
                name = Name;
            }
            else
            {
                name = default;
            }
            return Name != null;
        }

        public override bool Equals(object obj)
        {
            var other = obj as IFormat;
            return other != null &&
                other.Name == this.Name &&
                other.Color == this.Color &&
                other.Opacity == this.Opacity &&
                other.IsItalic == this.IsItalic &&
                other.IsBold == this.IsBold;
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode() ^
                this.Color.GetHashCode() ^
                this.Opacity.GetHashCode() ^
                this.IsItalic.GetHashCode() ^
                this.IsBold.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
