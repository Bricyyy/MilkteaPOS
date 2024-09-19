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
    public partial class ItemsListView : Form
    {
        private readonly ManagementForm manageFormRef;
        private readonly ItemsForm itemsFormRef;
        private readonly string view;
        private readonly string searchData;

        private Form modalBackground;

        public ItemsListView(ManagementForm manageForm, ItemsForm itemsForm, string view, string searchData = null)
        {
            InitializeComponent();

            manageFormRef = manageForm;
            itemsFormRef = itemsForm;
            this.view = view;
            this.searchData = searchData;
        }

        private void loadTableData()
        {
            string query;

            if (searchData == null)
            {
                query = $@"SELECT i.Item_Id, i.Item_Image, i.Item_Name, ic.Category_Name, CONCAT('₱', i.Base_Price) AS Base_Price 
                           FROM items i 
                           INNER JOIN item_category ic ON i.Category_Id = ic.Category_Id
                           ORDER BY i.Item_Id DESC";
            }
            else
            {
                query = $@"SELECT i.Item_Id, i.Item_Image,  i.Item_Name, ic.Category_Name, CONCAT('₱', i.Base_Price) AS Base_Price 
                           FROM items i 
                           INNER JOIN item_category ic ON i.Category_Id = ic.Category_Id
                           WHERE i.Item_Name LIKE CONCAT('%', '{searchData}', '%')
                           ORDER BY i.Item_Id DESC";
            }
           
            DataGridViewPopulator.setDataTable(query);
            DataGridViewPopulator.SetItemData(dgvItems);
        }

        private void RemoveItemFromCart(string itemID)
        {
            string inventoryQuery = $"SELECT inventory_id FROM inventory WHERE Item_Id = '{itemID}'";
            string inventoryId = DataAccess.ExecuteScalar(inventoryQuery)?.ToString();

            if (!string.IsNullOrEmpty(inventoryId))
            {
                string deleteQuery = $"DELETE FROM cart WHERE inventory_id = '{inventoryId}'";
                DataAccess.ExecuteNonQuery(deleteQuery);
            }
        }

        private void ConfirmDelete(string itemID)
        {
            using (DeleteDataModal confirmDelete = new DeleteDataModal())
            {
                Point panelLocationOnScreen = itemsFormRef.pnlMain.PointToScreen(Point.Empty);

                Form modalBackground = new Form
                {
                    StartPosition = FormStartPosition.Manual,
                    FormBorderStyle = FormBorderStyle.None,
                    Opacity = 0.5d,
                    BackColor = Color.Black,
                    Size = itemsFormRef.pnlMain.Size,
                    Location = panelLocationOnScreen,
                    ShowInTaskbar = false
                };

                modalBackground.Show();

                confirmDelete.Owner = modalBackground;

                confirmDelete.ShowDialog();

                if (confirmDelete.IsConfirmed)
                {
                    DataGridViewRow rowToDelete = dgvItems.Rows
                        .Cast<DataGridViewRow>()
                        .Where(r => r.Cells["itemIDColumn"].Value.ToString() == itemID)
                        .FirstOrDefault();
                        
                    if (rowToDelete != null)
                    {
                        dgvItems.Rows.Remove(rowToDelete);
                        DataAccess.DeleteItem(Convert.ToInt32(itemID));
                        RemoveItemFromCart(itemID);
                        DataAccess.DeleteInventory(Convert.ToInt32(itemID));
                    }
                }

                modalBackground.Dispose();
            }
        }

        private void DeleteDataError(string errorMessage)
        {
            using (DeleteDataModal confirmDelete = new DeleteDataModal(errorMessage))
            {
                Point panelLocationOnScreen = itemsFormRef.pnlMain.PointToScreen(Point.Empty);

                Form modalBackground = new Form
                {
                    StartPosition = FormStartPosition.Manual,
                    FormBorderStyle = FormBorderStyle.None,
                    Opacity = 0.5d,
                    BackColor = Color.Black,
                    Size = itemsFormRef.pnlMain.Size,
                    Location = panelLocationOnScreen,
                    ShowInTaskbar = false
                };

                modalBackground.Show();

                confirmDelete.Owner = modalBackground;

                confirmDelete.ShowDialog();

                modalBackground.Dispose();
            }
        }

        private void ItemsListView_Load(object sender, EventArgs e)
        {
            loadTableData();

            DataGridViewPopulator.headerFormat(dgvItems);
        }

        private void dgvItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvItems.Columns[e.ColumnIndex].Name;
            if (e.RowIndex >= 0 && e.RowIndex < dgvItems.Rows.Count)
            {
                DataGridViewRow row = dgvItems.Rows[e.RowIndex];
                string itemID = row.Cells["itemIDColumn"].Value.ToString();

                if (colName == "editData")
                {
                    itemsFormRef.loadForm(new EditItemForm(manageFormRef, itemsFormRef, itemID, view, searchData));
                }
                else if (colName == "deleteData")
                {
                    bool itemIdExistsInTables = DataChecker.DoesItemIdExistInTables(Convert.ToInt32(itemID));

                    if (itemIdExistsInTables)
                    {
                        DeleteDataError("Item is associated with any\nof the other tables.");
                        return;
                    }

                    ConfirmDelete(itemID);

                    FormUtilities.SetLabelCount(manageFormRef.lblItemCount, DataAccess.GetEntityCount("items"));
                }
            }
        }

        private void dgvItems_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewPopulator.FormatImageColumn(e, dgvItems, "editData", "deleteData");
            DataGridViewPopulator.FormatImageColumn(e, dgvItems, Properties.Resources.CoffeeIcon, "imageColumn");
        }

        private void dgvItems_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            dgvItems.ClearSelection();

            DataGridViewPopulator.SetRowNumber(dgvItems, "dataCount");
        }

        private void dgvItems_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewPopulator.SetCursorOnColumns(dgvItems, "editData", "deleteData");
        }
    }
}
