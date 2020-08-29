using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PatternCustomizer.State
{
    class PatternToStyle : INotifyPropertyChanged
    {
        public int RuleIndex
        { 
            get 
            {
                return _ruleIndex;
            }
            set 
            {
                _ruleIndex = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("RuleIndex"));
                }
            } 
        }
        public int FormatIndex
        {
            get
            {
                return _formatIndex;
            }
            set
            {
                _formatIndex = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("FormatIndex"));
                }
            }
        }

        private int _ruleIndex;
        private int _formatIndex;
        public PatternToStyle(int ruleIndex, int formatIndex)
        {
            this._ruleIndex = ruleIndex;
            this._formatIndex = formatIndex;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override bool Equals(object obj)
        {
            return obj is PatternToStyle other &&
                other.FormatIndex == this.FormatIndex &&
                other.RuleIndex == this.RuleIndex;
        }

        public override int GetHashCode()
        {
            var hashCode = -1030903623;
            hashCode = hashCode * -1521134295 + _ruleIndex;
            hashCode = hashCode * -1521134295 + _formatIndex;
            return hashCode;
        }
    }
}
