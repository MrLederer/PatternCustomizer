using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.VisualStudio.Shell;

namespace PatternCustomizer.Settings
{
    [Guid("706eb56d-1bd0-4e8b-8dce-add8013222c4")]
    internal class PatternsDialog : DialogPage
    {
        protected override IWin32Window Window
        {
            get
            {
                PatternsTable page = new PatternsTable();
                page.patterns = this;
                page.Initialize();
                return page;
            }
        }
    }
}