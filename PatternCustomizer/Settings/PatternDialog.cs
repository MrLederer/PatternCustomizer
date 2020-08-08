using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.VisualStudio.Shell;

namespace PatternCustomizer.Settings
{
    [Guid("23e56964-e8f9-4a4c-8285-09ed093971a2")]
    internal class PatternToStyleCustom : DialogPage
    {
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