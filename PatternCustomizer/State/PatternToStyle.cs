using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternCustomizer.State
{
    class PatternToStyle
    {
        public IRule rule;
        public IFormat format;

        public PatternToStyle(IRule rule, IFormat format)
        {
            this.rule = rule;
            this.format = format;
        }

        public override bool Equals(object obj)
        {
            return obj is PatternToStyle other &&
                   EqualityComparer<IRule>.Default.Equals(rule, other.rule) &&
                   EqualityComparer<IFormat>.Default.Equals(format, other.format);
        }

        public override int GetHashCode()
        {
            var hashCode = -1030903623;
            hashCode = hashCode * -1521134295 + EqualityComparer<IRule>.Default.GetHashCode(rule);
            hashCode = hashCode * -1521134295 + EqualityComparer<IFormat>.Default.GetHashCode(format);
            return hashCode;
        }

    }
}
