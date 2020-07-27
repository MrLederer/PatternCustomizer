using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RegexCustomize.State
{
    internal class CustomState : IState
    {
        public IFormat GetCustomFormat(string formatName)
        {
            return new Format();
        }

        public IEnumerable<string> GetEnabledFormats()
        {
            return new string[] { Constants.Format0, Constants.Format1, Constants.Format2 };
        }
        public IEnumerable<IRule> GetRules(string formatName)
        {
            switch (formatName)
            {
                case Constants.Format0:
                    return new IRule[] { new RegexRule(new Regex("l", RegexOptions.Compiled)) };
                case Constants.Format1:
                    return new IRule[] { new RegexRule(new Regex("a", RegexOptions.Compiled)) };
                default:
                    return new IRule[] { new RegexRule(new Regex("b", RegexOptions.Compiled)) };
            }
        }
    }
}
