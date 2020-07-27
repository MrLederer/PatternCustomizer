using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RegexCustomize.State
{
    interface IRule
    {
        IEnumerable<Match> Detect(string text);
    }
}