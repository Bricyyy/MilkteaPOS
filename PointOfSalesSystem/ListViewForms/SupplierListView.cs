using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using PointOfSalesSystem.DeleteModals;

namespace PointOfSalesSystem
{
    public partial class SupplierListView : Form
    {
        private readonly ManagementForm manageFormRef;
        private readonly SuppliersForm supplierFormRef;
        private readonly string view;
        private readonly string searchData;

        public SupplierListView(ManagementForm manageForm, SuppliersForm supplierForm, string view, string searchData = null)
        {
            InitializeComponent();

            manageFormRef = manageForm;
            supplierFormRef = supplierForm;
            this.view = view;
            this.searchData = searchData;
        }

        private void loadTableData()
        {
            string query;

            if (searchData == null)
            {
                query = $@"SELECT Supplier_Id, Supplier_Name, Address, Contact_Number
                           FROM supplier
                           ORDER BY Supplier_Id DESC";
            }
            else
            {
                query = $@"SELECT Supplier_Id, Supplier_Name, Address, Contact_Number
                           FROM supplier
                           WHERE Supplier_Name LIKE CONCAT('%', '{searchData}', '%')
                           ORDER BY Supplier_Id DESC";
            }

            DataGridViewPopulator.setDataTable(query);
            DataGridViewPopulator.SetSupplierData(dgvSupplier);
        }

        private void ConfirmDelete(string supplierID)
        {
            using (DeleteDataModal confirmDelete = new DeleteDataModal())
            {
                Point panelLocationOnScreen = supplierFormRef.pnlMain.PointToScreen(Point.Empty);

                Form modalBackground = new Form
                {
                    StartPosition = FormStartPosition.Manual,
                    FormBorderStyle = FormBorderStyle.None,
                    Opacity = 0.5d,
                    BackColor = Color.Black,
                    Size = supplierFormRef.pnlMain.Size,
                    Location = panelLocationOnScreen,
                    ShowInTaskbar = false
                };

                modalBackground.Show();

                confirmDelete.Owner = modalBackground;

                confirmDelete.ShowDialog();

                if (confirmDelete.IsConfirmed)
                {
                    DataGridViewRow rowToDelete = dgvSupplier.Rows
                        .Cast<DataGridViewRow>()
                        .Where(r => r.Cells["supplierIDColumn"].Value.ToString() == supplierID)
                        .FirstOrDefault();

                    if (rowToDelete != null)
                    {
                        dgvSupplier.Rows.Remove(rowToDelete);
                        DataAccess.DeleteSupplier(Convert.ToInt32(supplierID));
                    }
                }

                modalBackground.Dispose();
            }
        }

        private void DeleteDataError(string errorMessage)
        {
            using (DeleteDataModal confirmDelete = new DeleteDataModal(errorMessage))
            {
                Point panelLocationOnScreen = supplierFormRef.pnlMain.PointToScreen(Point.Empty);

                Form modalBackground = new Form
                {
                    StartPosition = FormStartPosition.Manual,
                    FormBorderStyle = FormBorderStyle.None,
                    Opacity = 0.5d,
                    BackColor = Color.Black,
                    Size = supplierFormRef.pnlMain.Size,
                    Location = panelLocationOnScreen,
                    ShowInTaskbar = false
                };

                modalBackground.Show();

                confirmDelete.Owner = modalBackground;

                confirmDelete.ShowDialog();

                modalBackground.Dispose();
            }
        }

        private void SupplierListView_Load(object sender, EventArgs e)
        {
            loadTableData();

            DataGridViewPopulator.headerFormat(dgvSupplier);
        }

        private void dgvSupplier_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvSupplier.Columns[e.ColumnIndex].Name;
            if (e.RowIndex >= 0 && e.RowIndex < dgvSupplier.Rows.Count)
            {
                DataGridViewRow row = dgvSupplier.Rows[e.RowIndex];
                string supplierID = row.Cells["supplierIDColumn"].Value.ToString();

                if (colName == "editData")
                {
                    supplierFormRef.loadForm(new EditSupplierForm(manageFormRef, supplierFormRef, supplierID, view, searchData));
                }
                else if (colName == "deleteData")
                {
                    bool supplierIdExistsInDelivery = DataChecker.DoesSupplierIdExistInDelivery(Convert.ToInt32(supplierID));

                    if (supplierIdExistsInDelivery)
                    {
                        DeleteDataError("Supplier is associated with any\nof the existing deliveries.");
                        return;
                    }

                    ConfirmDelete(supplierID);

                    FormUtilities.SetLabelCount(manageFormRef.lblItemCount, DataAccess.GetEntityCount("supplier"));
                }
            }
        }

        private void dgvSupplier_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewPopulator.FormatImageColumn(e, dgvSupplier, "editData", "deleteData");
        }

        private void dgvSupplier_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            dgvSupplier.ClearSelection();

            DataGridViewPopulator.SetRowNumber(dgvSupplier, "dataCount");
        }

        private void dgvSupplier_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewPopulator.SetCursorOnColumns(dgvSupplier, "editData", "deleteData");
        }
    }
}
