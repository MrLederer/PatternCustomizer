using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegexCustomize.State
{
    internal class CustomState : IState
    {
        public IFormat GetCustomFormat(string formatName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetEnabledFormats()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IRule> GetFormatRules(string formatName)
        {
            throw new NotImplementedException();
        }
    }
}
