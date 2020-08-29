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
using System.IO;

namespace PatternCustomizer.Settings
{
    public partial class PatternToStyleTable : UserControl
    {
        const int patternColumnIndex = 0;
        const int styleColumnIndex = 1;
        const int RemoveRuleToPatternColumnIndex = 2;

        public PatternToStyleTable()
        {
            InitializeComponent();
        }

        internal PatternToStyleDialog patternToStyle;

        public void Initialize()
        {
            var orderedPatternToStyleMapping = PatternCustomizerPackage.currentState.OrderedPatternToStyleMapping;
            for (int i = 0; i < orderedPatternToStyleMapping.Count; i++)
            {
                AddNewRow(orderedPatternToStyleMapping[i]);
            }
        }

        private void AddTableEntryButton_Click(object sender, EventArgs e)
        {
            var currentState = PatternCustomizerPackage.currentState;
            if (currentState.Formats.Any() && currentState.Rules.Any())
            {
                var newEntry = new PatternToStyle(0, 0);
                currentState.OrderedPatternToStyleMapping.Add(newEntry);
                AddNewRow(newEntry);
            }
            else
            {
                // add some msgbox to the user to add pattern and style
                throw new Exception("Can not create a rule without patterns and styles");   
            }
        }

        private void AddNewRow(PatternToStyle selectedValue)
        {
            //increase panel rows count by one
            tableLayoutPanel1.RowCount++;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            // TODO: Add reorder buttons

            //add rule select box
            var patternOptions = new ComboBox();
            patternOptions.DropDownStyle = ComboBoxStyle.DropDownList;
            patternOptions.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            tableLayoutPanel1.Controls.Add(patternOptions, patternColumnIndex, tableLayoutPanel1.RowCount - 2);
            patternOptions.DataSource = PatternCustomizerPackage.currentState.Rules;
            patternOptions.BindingContext = new BindingContext();
            patternOptions.CreateControl();
            patternOptions.DataBindings.Add(new Binding("SelectedIndex", selectedValue, "RuleIndex", true, DataSourceUpdateMode.OnPropertyChanged));
            patternOptions.SelectedIndex = selectedValue.RuleIndex;

            //add style select box
            var styleOptions = new ComboBox();
            styleOptions.DropDownStyle = ComboBoxStyle.DropDownList;
            styleOptions.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            tableLayoutPanel1.Controls.Add(styleOptions, styleColumnIndex, tableLayoutPanel1.RowCount - 2);
            styleOptions.DataSource = PatternCustomizerPackage.currentState.Formats;
            styleOptions.BindingContext = new BindingContext();
            styleOptions.CreateControl();
            styleOptions.DataBindings.Add(new Binding ("SelectedIndex", selectedValue, "FormatIndex", true, DataSourceUpdateMode.OnPropertyChanged));
            styleOptions.SelectedIndex = selectedValue.FormatIndex;

            //add delete button
            var deleteRulesToPatternBtn = new Button();
            deleteRulesToPatternBtn.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            deleteRulesToPatternBtn.Name = selectedValue.ToString();
            deleteRulesToPatternBtn.Text = "Remove";
            deleteRulesToPatternBtn.UseVisualStyleBackColor = true;
            deleteRulesToPatternBtn.Click += RemoveRuleToStyleEventHandlerCreator(selectedValue);
            tableLayoutPanel1.Controls.Add(deleteRulesToPatternBtn, RemoveRuleToPatternColumnIndex, tableLayoutPanel1.RowCount - 2);
        }

        private EventHandler RemoveRuleToStyleEventHandlerCreator(PatternToStyle value)
        {
            return new EventHandler((object sender, EventArgs e) =>
            {
                var rowIndex = PatternCustomizerPackage.currentState.OrderedPatternToStyleMapping.IndexOf(value) + 1;
                PatternCustomizerPackage.currentState.OrderedPatternToStyleMapping.Remove(value);
                RemoveArbitraryRow(tableLayoutPanel1, rowIndex);
            });
        }

        private void ImportButton_Click(object sender, EventArgs e)
        {
            var filePath = string.Empty;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "json files (*.json)|*.json|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;

                    try
                    {
                        PatternCustomizerPackage.currentState.Load(filePath);
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show("An error occured: " + exception.Message);
                    }
                    finally 
                    {
                        ClearRows();
                        Initialize();
                    }
                }
            }
        }

        private void ClearRows()
        {
            for (int i = tableLayoutPanel1.RowCount - 2; i > 0; i--)
            {
                RemoveArbitraryRow(tableLayoutPanel1, i);
            }   
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            var filePath = string.Empty;
            Stream stream;
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = "c:\\";
                saveFileDialog.Filter = "json files (*.json)|*.json|All files (*.*)|*.*";
                saveFileDialog.FilterIndex = 2;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.FileName = "setting.json";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = saveFileDialog.FileName;
                    try
                    {
                        PatternCustomizerPackage.currentState.Save(filePath);
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show("An error occured: " + exception.Message);
                    }
                }
            }
            
        }

        public static void RemoveArbitraryRow(TableLayoutPanel panel, int rowIndex)
        {
            if (rowIndex >= panel.RowCount)
            {
                return;
            }

            // delete all controls of row that we want to delete
            for (int i = 0; i < panel.ColumnCount; i++)
            {
                var control = panel.GetControlFromPosition(i, rowIndex);
                panel.Controls.Remove(control);
            }

            // move up row controls that comes after row we want to remove
            for (int i = rowIndex + 1; i < panel.RowCount; i++)
            {
                for (int j = 0; j < panel.ColumnCount; j++)
                {
                    var control = panel.GetControlFromPosition(j, i);
                    if (control != null)
                    {
                        panel.SetRow(control, i - 1);
                    }
                }
            }

            var removeStyle = panel.RowCount - 1;

            if (panel.RowStyles.Count > removeStyle)
                panel.RowStyles.RemoveAt(removeStyle);

            panel.RowCount--;
        }
    }
}
