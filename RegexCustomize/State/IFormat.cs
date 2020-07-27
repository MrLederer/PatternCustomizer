using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RegexCustomize.State
{

    interface IFormat
    {

        bool TryGetOpacity(out double opacityValue);

        bool TryGetColor(out Color colorValue);

    }
}
