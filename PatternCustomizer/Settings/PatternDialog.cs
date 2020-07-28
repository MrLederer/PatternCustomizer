using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.VisualStudio.Shell;

namespace PatternCustomizer.Settings
{
    public class PatternToStyleGrid : DialogPage
    {
        private int optionInt = 256;

        [Category("My Category")]
        [DisplayName("My Integer Option")]
        [Description("My integer option")]
        public int OptionInteger
        {
            get { return optionInt; }
            set { optionInt = value; }
        }
    }

    [Guid("00000000-0000-0000-0000-000000000000")]
    internal class PatternToStyleCustom : DialogPage
    {
        private string optionValue = "alpha";

        public string OptionString
        {
            get { return optionValue; }
            set { optionValue = value; }
        }

        protected override IWin32Window Window
        {
            get
            {
                PatternToStyleTable page = new PatternToStyleTable();
                page.patternToStyle = this;
                page.Initialize();
                return page;
            }
        }
    }
}