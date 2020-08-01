using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PatternCustomizer.State;
using System.Windows.Media;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System.Globalization;

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
            var orderedPatternToStyleMapping = PatternCustomizerPackage.currentState.OrderedPatternToStyleMapping;
            for (int i = 0; i < orderedPatternToStyleMapping.Count; i++)
            {
                AddNewRow(orderedPatternToStyleMapping[i], i);
            }
        }

        private void PatternToStyleTable_Leave(object sender, EventArgs e)
        {
            //export everything to state
        }

        private void AddTableEntryButton_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }

        private void AddNewRow(PatternToStyle selectedValue = default, int index = -1)
        {
            //increase panel rows count by one
            tableLayoutPanel1.RowCount++;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 25));

            // TODO: Add reorder buttons

            //add rule select box
            var patternOptions = new ComboBox();
            patternOptions.DropDownStyle = ComboBoxStyle.DropDownList;
            patternOptions.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.Controls.Add(patternOptions, patternColumnIndex, tableLayoutPanel1.RowCount - 2);
            patternOptions.DataSource = PatternCustomizerPackage.currentState.Rules;
            patternOptions.BindingContext = new BindingContext();
            if (selectedValue.rule != default)
            {
                //patternOptions.DataBindings.Add("SelectedValue", PatternCustomizerPackage.currentState.OrderedPatternToStyleMapping[index], "rule");
                patternOptions.SelectedIndex = patternOptions.Items.IndexOf(selectedValue.rule);
                //patternOptions.SelectedItem = selectedValue.rule;
            }

            //add style select box
            var styleOptions = new ComboBox();
            styleOptions.DropDownStyle = ComboBoxStyle.DropDownList;
            styleOptions.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.Controls.Add(styleOptions, styleColumnIndex, tableLayoutPanel1.RowCount - 2);
            styleOptions.DataSource = PatternCustomizerPackage.currentState.Formats;
            styleOptions.BindingContext = new BindingContext();
            if (selectedValue.format != default)
            {
                styleOptions.CreateControl();
                //styleOptions.DataBindings.Add("SelectedValue", PatternCustomizerPackage.currentState.OrderedPatternToStyleMapping[index], "format");
                styleOptions.SelectedIndex = styleOptions.Items.IndexOf(selectedValue.format);
                //styleOptions.SelectedValue = selectedValue.format;
            }
        }

        private void ManagePatternsButton_Click(object sender, EventArgs e)
        {
            PatternCustomizerPackage.currentState.Rules.Add(new RegexRule("\\s+$", "recognize empty space at end of line"));
        }

        private void ManageStylesButton_Click(object sender, EventArgs e)
        {
            PatternCustomizerPackage.currentState.Formats.Add(new CustomFormat("New chocolate Format", Colors.Chocolate));
        }
    }
}
