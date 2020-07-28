using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;

namespace PatternCustomizer.State
{
    class CustomFormat : IFormat
    {
        private string _name;
        private Color? _color;
        private double? _opacity;
        private bool? _isItalic;
        private bool? _isBold;

        public CustomFormat(Color? color = null, double? opacity = null, bool? isItalic = null, bool? isBold = null, string name = null)
        {
            this._name = name;
            this._color = color;
            this._opacity = opacity;
            this._isItalic = isItalic;
            this._isBold = isBold;
        }

        public bool TryGetColor(out Color colorValue)
        {
            if (_color.HasValue)
            {
                colorValue = _color.Value;
            }
            else
            {
                colorValue = default;
            }
            return _color.HasValue;
        }

        public bool TryGetOpacity(out double opacityValue)
        {
            if (_opacity.HasValue)
            {
                opacityValue = _opacity.Value;
            }
            else
            {
                opacityValue = default;
            }
            return _opacity.HasValue;
        }

        public bool TryGetIsItalic(out bool isItalicValue)
        {
            if (_isItalic.HasValue)
            {
                isItalicValue = _isItalic.Value;
            }
            else
            {
                isItalicValue = default;
            }
            return _opacity.HasValue;
        }

        public bool TryGetIsBold(out bool isBoldValue)
        {
            if (_isBold.HasValue)
            {
                isBoldValue = _isBold.Value;
            }
            else
            {
                isBoldValue = default;
            }
            return _isBold.HasValue;
        }

        public bool TryGetDisplayName(out string name)
        {
            if (_name != null)
            {
                name = _name;
            }
            else
            {
                name = default;
            }
            return _name != null;
        }
    }
}
