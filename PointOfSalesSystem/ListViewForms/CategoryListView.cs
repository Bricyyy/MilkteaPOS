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
    public partial class CategoryListView : Form
    {
        private readonly ManagementForm manageFormRef;
        private readonly CategoryForm categoryFormRef;
        private readonly string view;
        private readonly string searchData;

        public CategoryListView(ManagementForm manageForm, CategoryForm categoryForm, string view, string searchData = null)
        {
            InitializeComponent();

            manageFormRef = manageForm;
            categoryFormRef = categoryForm;
            this.view = view;
            this.searchData = searchData;
        }

        private void loadTableData()
        {
            string query;

            if (searchData == null)
            {
                query = $@"SELECT Category_Id, Category_Name
                           FROM item_category
                           ORDER BY Category_Id DESC";
            }
            else
            {
                query = $@"SELECT Category_Id, Category_Name
                           FROM item_category
                           WHERE Category_Name LIKE CONCAT('%', '{searchData}', '%')
                           ORDER BY Category_Id DESC";
            }

            DataGridViewPopulator.setDataTable(query);
            DataGridViewPopulator.SetCategoryData(dgvCategory);
        }

        private void ConfirmDelete(string categoryID)
        {
            using (DeleteDataModal confirmDelete = new DeleteDataModal())
            {
                Point panelLocationOnScreen = categoryFormRef.pnlMain.PointToScreen(Point.Empty);

                Form modalBackground = new Form
                {
                    StartPosition = FormStartPosition.Manual,
                    FormBorderStyle = FormBorderStyle.None,
                    Opacity = 0.5d,
                    BackColor = Color.Black,
                    Size = categoryFormRef.pnlMain.Size,
                    Location = panelLocationOnScreen,
                    ShowInTaskbar = false
                };

                modalBackground.Show();

                confirmDelete.Owner = modalBackground;

                confirmDelete.ShowDialog();

                if (confirmDelete.IsConfirmed)
                {
                    DataGridViewRow rowToDelete = dgvCategory.Rows
                        .Cast<DataGridViewRow>()
                        .Where(r => r.Cells["categoryIDColumn"].Value.ToString() == categoryID)
                        .FirstOrDefault();

                    if (rowToDelete != null)
                    {
                        dgvCategory.Rows.Remove(rowToDelete);
                        DataAccess.DeleteCategory(Convert.ToInt32(categoryID));
                    }
                }

                modalBackground.Dispose();
            }
        }

        private void DeleteDataError(string errorMessage)
        {
            using (DeleteDataModal confirmDelete = new DeleteDataModal(errorMessage))
            {
                Point panelLocationOnScreen = categoryFormRef.pnlMain.PointToScreen(Point.Empty);

                Form modalBackground = new Form
                {
                    StartPosition = FormStartPosition.Manual,
                    FormBorderStyle = FormBorderStyle.None,
                    Opacity = 0.5d,
                    BackColor = Color.Black,
                    Size = categoryFormRef.pnlMain.Size,
                    Location = panelLocationOnScreen,
                    ShowInTaskbar = false
                };

                modalBackground.Show();

                confirmDelete.Owner = modalBackground;

                confirmDelete.ShowDialog();

                modalBackground.Dispose();
            }
        }

        private void CategoryListView_Load(object sender, EventArgs e)
        {
            loadTableData();

            DataGridViewPopulator.headerFormat(dgvCategory);
        }

        private void dgvCategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvCategory.Columns[e.ColumnIndex].Name;
            if (e.RowIndex >= 0 && e.RowIndex < dgvCategory.Rows.Count)
            {
                DataGridViewRow row = dgvCategory.Rows[e.RowIndex];
                string categoryID = row.Cells["categoryIDColumn"].Value.ToString();

                if (colName == "editData")
                {
                    categoryFormRef.loadForm(new EditCategoryForm(manageFormRef, categoryFormRef, categoryID, view, searchData));
                }
                else if (colName == "deleteData")
                {
                    bool categoryIdExistsInItems = DataChecker.DoesCategoryIdExistInItems(Convert.ToInt32(categoryID));

                    if (categoryIdExistsInItems)
                    {
                        DeleteDataError("Category is associated with any\nof the existing items.");
                        return;
                    }

                    ConfirmDelete(categoryID);

                    FormUtilities.SetLabelCount(manageFormRef.lblItemCount, DataAccess.GetEntityCount("item_category"));
                }
            }
        }

        private void dgvCategory_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewPopulator.FormatImageColumn(e, dgvCategory, "editData", "deleteData");
        }

        private void dgvCategory_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            dgvCategory.ClearSelection();

            DataGridViewPopulator.SetRowNumber(dgvCategory, "dataCount");
        }

        private void dgvCategory_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewPopulator.SetCursorOnColumns(dgvCategory, "editData", "deleteData");
        }
    }
}
