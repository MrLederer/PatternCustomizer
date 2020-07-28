using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;

namespace PatternCustomizer.State
{

    interface IFormat
    {
        bool TryGetDisplayName(out string name);

        bool TryGetOpacity(out double opacityValue);

        bool TryGetColor(out Color colorValue);

        bool TryGetIsItalic(out bool isItalicValue);

        bool TryGetIsBold(out bool isBoldValue);
    }
}
