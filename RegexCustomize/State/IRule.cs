using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PatternCustomizer.State
{
    interface IRule
    {
        IEnumerable<Match> Detect(string text);
    }
}