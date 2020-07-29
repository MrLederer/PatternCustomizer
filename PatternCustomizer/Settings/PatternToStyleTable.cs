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
        const int patternColumnIndex = 0;
        const int styleColumnIndex = 1;
        public PatternToStyleTable()
        {
            InitializeComponent();
        }

        internal PatternToStyleCustom patternToStyle;

        public void Initialize()
        {
            foreach (var entry  in PatternCustomizerPackage.currentState.OrderedPatternToStyleMapping)
            {

            }
        }

        private void PatternToStyleTable_Leave(object sender, EventArgs e)
        {
            //patternToStyle.OptionString = textBox1.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void AddTableEntryButton_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }

        private void AddNewRow()
        {
            //get a reference to the previous existent row
            RowStyle temp = tableLayoutPanel1.RowStyles[tableLayoutPanel1.RowCount - 1];
            //increase panel rows count by one
            tableLayoutPanel1.RowCount++;
            //add a new RowStyle as a copy of the previous one
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));

            // TODO: Add reorder buttons

            //add rule select box
            var patternOptions = new ComboBox();
            patternOptions.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            patternOptions.DataSource = PatternCustomizerPackage.currentState.DistinctRules;
            tableLayoutPanel1.Controls.Add(patternOptions, patternColumnIndex, tableLayoutPanel1.RowCount - 2);

            //add style select box
            var styleOptions = new ComboBox();
            styleOptions.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            styleOptions.DataSource = PatternCustomizerPackage.currentState.DistinctFormats;
            tableLayoutPanel1.Controls.Add(styleOptions, styleColumnIndex, tableLayoutPanel1.RowCount - 2);
        }
    }
}
