using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.VisualStudio.Shell;

namespace PatternCustomizer.Settings
{
    [Guid("f75273e5-c257-4ef2-81e2-2f3e6b79b9f8")]
    internal class StylesDialog : DialogPage
    {
        protected override IWin32Window Window
        {
            get
            {
                StylesTable page = new StylesTable();
                page.styles = this;
                page.Initialize();
                return page;
            }
        }
    }
}