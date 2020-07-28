using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternCustomizer
{
    public static class Constants
    {
        public const string Format0 = nameof(Format0);
        public const string Format1 = nameof(Format1);
        public const string Format2 = nameof(Format2);
        // TODO: make 10 formats available
        //public const string Format3 = nameof(Format3);
        //public const string Format4 = nameof(Format4);
        //public const string Format5 = nameof(Format5);
        //public const string Format6 = nameof(Format6);
        //public const string Format7 = nameof(Format7);
        //public const string Format8 = nameof(Format8);
        //public const string Format9 = nameof(Format9);
        public static string[] AllFormats = new string[] {
            Format0,
            Format1,
            Format2
        };
    }
}
