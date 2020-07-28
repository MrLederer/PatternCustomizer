using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatternCustomizer.Settings
{
    public partial class PatternToStyleTable : UserControl
    {
        public PatternToStyleTable()
        {
            InitializeComponent();
        }

        internal PatternToStyleCustom patternToStyle;

        public void Initialize()
        {
            //textBox1.Text = patternToStyle.OptionString;
        }

        private void PatternToStyleTable_Leave(object sender, EventArgs e)
        {
            //patternToStyle.OptionString = textBox1.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
