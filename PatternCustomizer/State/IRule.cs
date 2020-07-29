using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace PatternCustomizer.State
{
    interface IRule : INotifyPropertyChanged
    {
        string Name { get; set; }

        string RegexPattern { get; set; }

        IEnumerable<Match> Detect(string text);
    }
}