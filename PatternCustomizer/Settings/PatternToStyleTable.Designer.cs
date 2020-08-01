using System.Windows.Forms;

namespace PatternCustomizer.Settings
{
    partial class PatternToStyleTable
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.StyleLabel = new System.Windows.Forms.Label();
            this.PatternLabel = new System.Windows.Forms.Label();
            this.AddTableEntryButton = new System.Windows.Forms.Button();
            this.ManageStylesButton = new System.Windows.Forms.Button();
            this.ManagePatternsButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.Controls.Add(this.StyleLabel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.PatternLabel, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(24, 56);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(652, 447);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // StyleLabel
            // 
            this.StyleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.StyleLabel.AutoSize = true;
            this.StyleLabel.Location = new System.Drawing.Point(280, 7);
            this.StyleLabel.Name = "StyleLabel";
            this.StyleLabel.Size = new System.Drawing.Size(269, 13);
            this.StyleLabel.TabIndex = 3;
            this.StyleLabel.Text = "Style";
            this.StyleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PatternLabel
            // 
            this.PatternLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.PatternLabel.AutoSize = true;
            this.PatternLabel.Location = new System.Drawing.Point(4, 7);
            this.PatternLabel.Name = "PatternLabel";
            this.PatternLabel.Size = new System.Drawing.Size(269, 13);
            this.PatternLabel.TabIndex = 1;
            this.PatternLabel.Text = "Pattern";
            this.PatternLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AddTableEntryButton
            // 
            this.AddTableEntryButton.Location = new System.Drawing.Point(24, 16);
            this.AddTableEntryButton.Name = "AddTableEntryButton";
            this.AddTableEntryButton.Size = new System.Drawing.Size(109, 34);
            this.AddTableEntryButton.TabIndex = 1;
            this.AddTableEntryButton.Text = "Add row";
            this.AddTableEntryButton.UseVisualStyleBackColor = true;
            this.AddTableEntryButton.Click += new System.EventHandler(this.AddTableEntryButton_Click);
            // 
            // ManageStylesButton
            // 
            this.ManageStylesButton.Location = new System.Drawing.Point(289, 16);
            this.ManageStylesButton.Name = "ManageStylesButton";
            this.ManageStylesButton.Size = new System.Drawing.Size(109, 34);
            this.ManageStylesButton.TabIndex = 2;
            this.ManageStylesButton.Text = "Manage styles";
            this.ManageStylesButton.UseVisualStyleBackColor = true;
            this.ManageStylesButton.Click += new System.EventHandler(this.ManageStylesButton_Click);
            // 
            // ManagePatternsButton
            // 
            this.ManagePatternsButton.Location = new System.Drawing.Point(155, 16);
            this.ManagePatternsButton.Name = "ManagePatternsButton";
            this.ManagePatternsButton.Size = new System.Drawing.Size(109, 34);
            this.ManagePatternsButton.TabIndex = 3;
            this.ManagePatternsButton.Text = "Manage patterns";
            this.ManagePatternsButton.UseVisualStyleBackColor = true;
            this.ManagePatternsButton.Click += new System.EventHandler(this.ManagePatternsButton_Click);
            // 
            // PatternToStyleTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.ManagePatternsButton);
            this.Controls.Add(this.ManageStylesButton);
            this.Controls.Add(this.AddTableEntryButton);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PatternToStyleTable";
            this.Size = new System.Drawing.Size(717, 526);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label PatternLabel;
        private Label StyleLabel;
        private Button AddTableEntryButton;
        private Button ManageStylesButton;
        private Button ManagePatternsButton;
    }
}
