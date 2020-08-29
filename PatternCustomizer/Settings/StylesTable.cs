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
    public partial class StylesTable : UserControl
    {

        public StylesTable()
        {
            InitializeComponent();
        }

        internal StylesDialog styles;

        public void Initialize()
        {
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ReadOnly = false;
            this.dataGridView1.AllowUserToDeleteRows = true;
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AllowUserToOrderColumns = false;
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
            this.dataGridView1.DataSource = PatternCustomizerPackage.currentState.Formats;
            this.dataGridView1.BindingContext = new BindingContext();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PatternCustomizerPackage.currentState.Formats.Add(new CustomFormat());
        }
    }
}
