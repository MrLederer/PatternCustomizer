using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RegexCustomize.State
{
    class Format : IFormat
    {
        private Color? _color;
        private double? _opacity;

        public Format(Color? color = null, double? opacity = null)
        {
            this._color = color;
            this._opacity = opacity;
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
    }
}
