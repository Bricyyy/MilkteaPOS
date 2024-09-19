namespace PointOfSalesSystem
{
    partial class TestForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Syncfusion.WinForms.DataGrid.GridMultiSelectComboBoxColumn gridMultiSelectComboBoxColumn2 = new Syncfusion.WinForms.DataGrid.GridMultiSelectComboBoxColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn2 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestForm));
            this.sfDataGrid1 = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.cmbFilter = new Syncfusion.Windows.Forms.Tools.MultiSelectionComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.sfDataGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFilter)).BeginInit();
            this.SuspendLayout();
            // 
            // sfDataGrid1
            // 
            this.sfDataGrid1.AccessibleName = "Table";
            gridMultiSelectComboBoxColumn2.HeaderText = "Column1";
            gridMultiSelectComboBoxColumn2.MappingName = "Column1";
            gridTextColumn2.HeaderText = "Column2";
            gridTextColumn2.MappingName = "Column2";
            this.sfDataGrid1.Columns.Add(gridMultiSelectComboBoxColumn2);
            this.sfDataGrid1.Columns.Add(gridTextColumn2);
            this.sfDataGrid1.Location = new System.Drawing.Point(63, 61);
            this.sfDataGrid1.Name = "sfDataGrid1";
            this.sfDataGrid1.Size = new System.Drawing.Size(313, 150);
            this.sfDataGrid1.TabIndex = 0;
            this.sfDataGrid1.Text = "sfDataGrid1";
            // 
            // cmbFilter
            // 
            this.cmbFilter.AutoSizeMode = Syncfusion.Windows.Forms.Tools.AutoSizeModes.None;
            this.cmbFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbFilter.BeforeTouchSize = new System.Drawing.Size(253, 24);
            this.cmbFilter.ButtonStyle = Syncfusion.Windows.Forms.ButtonAppearance.Office2016Colorful;
            this.cmbFilter.DataSource = ((object)(resources.GetObject("cmbFilter.DataSource")));
            this.cmbFilter.FlatBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.cmbFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbFilter.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFilter.GroupHeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.cmbFilter.GroupHeaderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.cmbFilter.Location = new System.Drawing.Point(472, 111);
            this.cmbFilter.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.cmbFilter.Name = "cmbFilter";
            this.cmbFilter.SelectionStart = 23;
            this.cmbFilter.Size = new System.Drawing.Size(253, 24);
            this.cmbFilter.Style = Syncfusion.Windows.Forms.Tools.MultiSelectionComboBoxStyle.Office2016Colorful;
            this.cmbFilter.TabIndex = 1;
            this.cmbFilter.ThemeName = "Office2016Colorful";
            this.cmbFilter.ThemeStyle.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.cmbFilter.TickColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.cmbFilter.UseVisualStyle = true;
            this.cmbFilter.VisualItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.cmbFilter.VisualItemSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(202)))), ((int)(((byte)(239)))));
            this.cmbFilter.VisualItemSelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cmbFilter);
            this.Controls.Add(this.sfDataGrid1);
            this.Name = "TestForm";
            this.Text = "TestForm";
            this.Load += new System.EventHandler(this.TestForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sfDataGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFilter)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Syncfusion.WinForms.DataGrid.SfDataGrid sfDataGrid1;
        private Syncfusion.Windows.Forms.Tools.MultiSelectionComboBox cmbFilter;
    }
}