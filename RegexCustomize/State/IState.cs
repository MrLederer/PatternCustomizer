using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using FormatName = System.String;

namespace RegexCustomize.State
{
    interface IState
    {
        IFormat GetCustomFormat(FormatName formatName);

        IEnumerable<IRule> GetFormatRules(FormatName formatName);

        IEnumerable<FormatName> GetEnabledFormats();
    }

}
