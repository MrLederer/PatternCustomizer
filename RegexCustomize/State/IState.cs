﻿using System;
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
        /// <summary>
        /// Gets the custom setting for a format.
        /// Used to configure the format
        /// </summary>
        /// <param name="formatName">Name of the format.</param>
        /// <returns></returns>
        IFormat GetCustomFormat(FormatName formatName);

        IEnumerable<IRule> GetRules(FormatName formatName);

        IEnumerable<FormatName> GetEnabledFormats();
    }

}
