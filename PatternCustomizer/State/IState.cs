using System.Collections.Generic;
using System.ComponentModel;
using FormatName = System.String;

namespace PatternCustomizer.State
{
    interface IState : INotifyPropertyChanged
    {
        BindingList<PatternToStyle> OrderedPatternToStyleMapping { get; set; }

        BindingList<IRule> Rules { get; set; }

        BindingList<IFormat> Formats { get; set; }

        /// <summary>
        /// Gets the custom setting for a format.
        /// Used to configure the format
        /// </summary>
        /// <param name="formatName">Name of the format.</param>
        /// <returns></returns>
        IFormat GetCustomFormatOrDefault(FormatName formatName);

        IEnumerable<IRule> GetRules(FormatName formatName);

        IEnumerable<FormatName> GetEnabledDeclaredFormatNames();

        IState Save(string filepath);

        IState Load(string filepath);
    }

}
