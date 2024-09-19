using PointOfSalesSystem.GetOrSet;
using PointOfSalesSystem.Modals;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PointOfSalesSystem
{
    public partial class DeliverForm : Form
    {
        private readonly Dashboard dashboardFormRef;
        private readonly string username;

        private Color defaultForeColor = Color.FromArgb(30, 57, 50);
        private Color placeholderForeColor = Color.Gray;
        private Dictionary<ComboBox, bool> dropdownStates = new Dictionary<ComboBox, bool>();

        public DeliverForm(Dashboard dashboardFormRef, string username)
        {
            InitializeComponent();

            this.dashboardFormRef = dashboardFormRef;
            this.username = username;
        }

        private void cmbSetPlaceholders()
        {
            cmbFilterItem.Items.Insert(0, "Select Item...");
            cmbFilterItem.SelectedIndex = 0;
            cmbFilterCategory.Items.Insert(0, "Select Category...");
            cmbFilterCategory.SelectedIndex = 0;
            cmbFilterSupplier.Items.Insert(0, "Select Supplier...");
            cmbFilterSupplier.SelectedIndex = 0;
        }

        private void cmbSetItem()
        {
            string itemQuery = "SELECT Item_Id, Item_Name FROM items ORDER BY Item_Name ASC";

            DataTable itemDataTable = DataAccess.GetDataAsDataTable(itemQuery);

            if (itemDataTable.Rows.Count > 0)
            {
                cmbItem.DisplayMember = "Item_Name";
                cmbItem.ValueMember = "Item_Id";

                // Set the data source for cmbItem
                cmbItem.DataSource = itemDataTable;

                cmbItem.SelectedIndex = -1;

                // Create a new DataTable for cmbFilterItem
                DataTable filterItemDataTable = itemDataTable.Copy();

                DataRow placeholderRow = filterItemDataTable.NewRow();
                placeholderRow["Item_Id"] = -1;
                placeholderRow["Item_Name"] = "Select Item...";
                filterItemDataTable.Rows.InsertAt(placeholderRow, 0);

                cmbFilterItem.DisplayMember = "Item_Name";
                cmbFilterItem.ValueMember = "Item_Id";

                // Set the data source for cmbFilterItem using the new DataTable
                cmbFilterItem.DataSource = filterItemDataTable;

                cmbFilterItem.SelectedIndex = 0;
            }
        }

        private void cmbSetCategory()
        {
            string categoryQuery = "SELECT Category_Id, Category_Name FROM item_category ORDER BY Category_Name ASC";

            DataTable dataTable = DataAccess.GetDataAsDataTable(categoryQuery);

            if (dataTable.Rows.Count > 0)
            {
                DataRow placeholderRow = dataTable.NewRow();
                placeholderRow["Category_Id"] = -1;
                placeholderRow["Category_Name"] = "Select Category...";
                dataTable.Rows.InsertAt(placeholderRow, 0);

                cmbFilterCategory.DisplayMember = "Category_Name";
                cmbFilterCategory.ValueMember = "Category_Id";

                cmbFilterCategory.DataSource = dataTable;

                cmbFilterCategory.SelectedIndex = 0; // Set the placeholder as default
            }
        }

        private void cmbSetSupplier()
        {
            string supplierQuery = "SELECT Supplier_Id, Supplier_Name FROM supplier ORDER BY Supplier_Name ASC";

            DataTable supplierDataTable = DataAccess.GetDataAsDataTable(supplierQuery);

            if (supplierDataTable.Rows.Count > 0)
            {
                cmbSupplier.DisplayMember = "Supplier_Name";
                cmbSupplier.ValueMember = "Supplier_Id";

                // Set the data source for cmbSupplier
                cmbSupplier.DataSource = supplierDataTable;

                cmbSupplier.SelectedIndex = -1;

                // Create a new DataTable for cmbFilterSupplier
                DataTable filterSupplierDataTable = supplierDataTable.Copy();

                DataRow placeholderRow = filterSupplierDataTable.NewRow();
                placeholderRow["Supplier_Id"] = -1;
                placeholderRow["Supplier_Name"] = "Select Supplier...";
                filterSupplierDataTable.Rows.InsertAt(placeholderRow, 0);

                cmbFilterSupplier.DisplayMember = "Supplier_Name";
                cmbFilterSupplier.ValueMember = "Supplier_Id";

                // Set the data source for cmbFilterSupplier using the new DataTable
                cmbFilterSupplier.DataSource = filterSupplierDataTable;

                cmbFilterSupplier.SelectedIndex = 0;
            }
        }

        private void cmbSetDropDownEvents()
        {
            cmbFilterItem.DropDown += ComboBox_DropDown;
            cmbFilterCategory.DropDown += ComboBox_DropDown;
            cmbFilterSupplier.DropDown += ComboBox_DropDown;

            cmbFilterItem.DropDownClosed += ComboBox_DropDownClosed;
            cmbFilterCategory.DropDownClosed += ComboBox_DropDownClosed;
            cmbFilterSupplier.DropDownClosed += ComboBox_DropDownClosed;
        }

        private string GenerateDeliveryNumber()
        {
            int lastSequenceNumber = DataAccess.GetLastSequenceNumberFromDelivery(); // Get the last sequence number from the database
            int newSequenceNumber = (lastSequenceNumber % 99) + 1; // Increment the sequence number within the range 1 to 99

            // Pad the sequence number with leading zeros if needed
            string sequence = newSequenceNumber.ToString().PadLeft(2, '0');

            string currentDate = DateTime.Now.ToString("MMdd"); // Get the date in MMdd format
            string randomLetters = FormUtilities.GenerateRandomLetters(4); // Generate 4 random letters

            // Concatenate the parts to form the delivery number
            string deliveryNumber = $"{sequence}{currentDate}{randomLetters}";

            return deliveryNumber;
        }

        private void InsertDeliveryData()
        {
            string deliveryNo = GenerateDeliveryNumber();
            int itemId = Convert.ToInt32(cmbItem.SelectedValue);
            int quantity = Convert.ToInt32(txtQuantity.Text);
            int supplierId = Convert.ToInt32(cmbSupplier.SelectedValue);

            DataAccess.InsertDelivery(deliveryNo, DateTime.Now, itemId, quantity, supplierId);
        }

        private void UpdateItemQuantity()
        {
            int itemID = Convert.ToInt32(cmbItem.SelectedValue);
            int quantity = Convert.ToInt32(txtQuantity.Text);

            string inventoryQuery = $"SELECT * FROM inventory WHERE Item_Id = {itemID}";
            List<Inventory> inventories = DataAccess.GetInventory(inventoryQuery);

            if (inventories.Count > 0)
            {
                Inventory selectedInventory = inventories.FirstOrDefault();

                if (selectedInventory != null)
                {
                    int quantityAvailable = selectedInventory.Quantity;

                    int newQuantity = quantity + quantityAvailable;

                    DataAccess.UpdateInventory(selectedInventory.InventoryId, newQuantity);
                }
            }
        }

        private void ResetFields()
        {
            cmbItem.SelectedIndex = -1;
            txtQuantity.Text = "0";
            cmbSupplier.SelectedIndex = -1;
            btnInsert.Text = "Add Delivery";
        }

        private void SaveRecord()
        {
            bool isItemSelected = cmbItem.SelectedIndex > -1;
            bool isQuantityValid = Convert.ToInt32(txtQuantity.Text) > 0;
            bool isSupplierSelected = cmbSupplier.SelectedIndex > -1;

            if (!isItemSelected || !isQuantityValid || !isSupplierSelected)
            {
                if (!isItemSelected)
                {
                    lblItemChecker.Visible = true;
                }

                if (!isQuantityValid)
                {
                    lblQuantityChecker.Visible = true;
                }

                if (!isSupplierSelected)
                {
                    lblSupplierChecker.Visible = true;
                }

                return;
            }

            using (Confirmation orderInfo = new Confirmation())
            {
                Point panelLocationOnScreen = dashboardFormRef.pnlMain.PointToScreen(Point.Empty);

                Form modalBackground = new Form
                {
                    StartPosition = FormStartPosition.Manual,
                    FormBorderStyle = FormBorderStyle.None,
                    Opacity = 0.5d,
                    BackColor = Color.Black,
                    Size = dashboardFormRef.pnlMain.Size,
                    Location = panelLocationOnScreen,
                    ShowInTaskbar = false
                };

                modalBackground.Show();

                orderInfo.Owner = modalBackground;
                orderInfo.lblTitle.Text = "Record Added!";
                orderInfo.lblInfo.Text = "Updated item in inventory.";

                DialogResult result = orderInfo.ShowDialog();

                if (result == DialogResult.OK)
                {
                    InsertDeliveryData();
                    UpdateItemQuantity();
                    ResetFields();

                    setCurrentWeekDeliveryCounts();
                    setItemCounts();

                    flpDeliveries.Controls.Clear();
                    SetDeliveries();
                }

                modalBackground.Dispose();
            }
        }

        private Guna.UI2.WinForms.Guna2Panel GenerateDeliveryPanel(string deliveryID, string deliveryNo, string deliveryDate, string deliveryTime, byte[] itemImage, string itemName, string itemCategory, string itemQuantity, string supplierName)
        {
            pnlDelivery = new Guna.UI2.WinForms.Guna2Panel
            {
                BackColor = Color.Transparent,
                Name = "pnlDelivery",
                Size = new Size(708, 76),
                BorderRadius = 10,
                FillColor = Color.White,
                Tag = deliveryID
            };

            lblDeliveryNo = new Label
            {
                BackColor = Color.Transparent,
                Font = new Font("Proxima Nova Lt", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 57, 50),
                Text = "#" + deliveryNo,
                AutoEllipsis = true,
                Name = "lblDeliveryNo",
                AutoSize = true,
                Location = new Point(7, 32),
                MaximumSize = new Size(87, 16)
            };

            lblDeliveryDate = new Label
            {
                BackColor = Color.Transparent,
                Font = new Font("Proxima Nova Lt", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 57, 50),
                Text = deliveryDate,
                AutoEllipsis = true,
                Name = "lblDeliveryDate",
                AutoSize = true,
                Location = new Point(108, 24),
                MaximumSize = new Size(131, 16)
            };

            lblDeliveryTime = new Label
            {
                BackColor = Color.Transparent,
                Font = new Font("Proxima Nova Lt", 8, FontStyle.Bold),
                ForeColor = Color.Gray,
                Text = deliveryTime,
                AutoEllipsis = true,
                Name = "lblDeliveryTime",
                AutoSize = true,
                Location = new Point(108, 40),
                MaximumSize = new Size(121, 13)
            };

            picItem = new Guna.UI2.WinForms.Guna2PictureBox
            {
                Image = FormUtilities.ByteArrayToImage(itemImage) ?? Properties.Resources.CoffeeIcon,
                SizeMode = PictureBoxSizeMode.Zoom,
                Name = "picItem",
                Location = new Point(249, 5),
                Size = new Size(65, 65),
                BorderRadius = 5,
                FillColor = Color.Transparent,
                UseTransparentBackground = true
            };

            lblItemName = new Label
            {
                BackColor = Color.Transparent,
                Font = new Font("Proxima Nova Lt", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 57, 50),
                Text = itemName,
                TextAlign = ContentAlignment.MiddleLeft,
                AutoEllipsis = true,
                Name = "lblItemName",
                AutoSize = true,
                Location = new Point(322, 13),
                MinimumSize = new Size(121, 35),
                MaximumSize = new Size(121, 35)
            };

            lblItemCategory = new Label
            {
                BackColor = Color.Transparent,
                Font = new Font("Proxima Nova Lt", 8, FontStyle.Bold),
                ForeColor = Color.Gray,
                Text = itemCategory,
                AutoEllipsis = true,
                Name = "lblItemCategory",
                AutoSize = true,
                Location = new Point(322, 47),
                MaximumSize = new Size(121, 13)
            };

            lblItemQuantity = new Label
            {
                BackColor = Color.Transparent,
                Font = new Font("Proxima Nova Lt", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 57, 50),
                Text = "x" + itemQuantity,
                AutoEllipsis = true,
                Name = "lblItemQuantity",
                AutoSize = true,
                Location = new Point(455, 32),
                MaximumSize = new Size(56, 16)
            };

            lblSupplierName = new Label
            {
                BackColor = Color.Transparent,
                Font = new Font("Proxima Nova Lt", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 57, 50),
                Text = supplierName,
                AutoEllipsis = true,
                Name = "lblSupplierName",
                AutoSize = true,
                Location = new Point(534, 32),
                MaximumSize = new Size(159, 16)
            };

            pnlDelivery.Controls.Add(lblDeliveryNo);
            pnlDelivery.Controls.Add(lblDeliveryDate);
            pnlDelivery.Controls.Add(lblDeliveryTime);
            pnlDelivery.Controls.Add(picItem);
            pnlDelivery.Controls.Add(lblItemName);
            pnlDelivery.Controls.Add(lblItemCategory);
            pnlDelivery.Controls.Add(lblItemQuantity);
            pnlDelivery.Controls.Add(lblSupplierName);

            return pnlDelivery;
        }

        private void CopyAndPasteDeliveryPanel(int deliveryID, string deliveryNo, DateTime deliveryDateTime, byte[] itemImage, string itemName, string itemCategory, int itemQuantity, string supplierName)
        {
            string formattedDate = deliveryDateTime.ToString("MMMM dd, yyyy");
            string formattedTime = deliveryDateTime.ToString("hh:mmtt").ToUpper();

            Guna.UI2.WinForms.Guna2Panel clonedPanel = GenerateDeliveryPanel(deliveryID.ToString(), deliveryNo.ToString(), formattedDate, formattedTime, itemImage, itemName, itemCategory, itemQuantity.ToString(), supplierName);
            flpDeliveries.Controls.Add(clonedPanel);
        }

        private void AppendFilter(ref string query)
        {
            if (!query.Contains("WHERE"))
            {
                query += "WHERE ";
            }
            else
            {
                query += "AND ";
            }
        }

        private string BuildInventoryQuery(string searchText, string itemID, string categoryID, string supplierID, DateTime? selectedDate)
        {
            // Initialize the query variable with a default value to prevent compile-time errors
            string deliveryQuery = "SELECT d.Delivery_Id, d.Delivery_No, d.Delivery_Date, d.Item_Id, d.Quantity, d.Supplier_Id, " +
                                   "i.Item_Image, i.Item_Name, ic.Category_Name, s.Supplier_Name " +
                                   "FROM delivery d " +
                                   "INNER JOIN items i ON d.Item_Id = i.Item_Id " +
                                   "LEFT JOIN item_category ic ON i.Category_Id = ic.Category_Id " +
                                   "INNER JOIN supplier s ON d.Supplier_Id = s.Supplier_Id ";

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                // Append WHERE clause if searchText is not empty
                deliveryQuery += $"WHERE d.Delivery_No LIKE '%{searchText}%' ";
            }

            if (!string.IsNullOrWhiteSpace(itemID))
            {
                AppendFilter(ref deliveryQuery);
                deliveryQuery += $"d.Item_Id = {itemID} ";
            }

            if (!string.IsNullOrWhiteSpace(categoryID))
            {
                AppendFilter(ref deliveryQuery);
                deliveryQuery += $"i.Category_Id = {categoryID} ";
            }

            if (!string.IsNullOrWhiteSpace(supplierID))
            {
                AppendFilter(ref deliveryQuery);
                deliveryQuery += $"d.Supplier_Id = {supplierID} ";
            }

            if (selectedDate.HasValue)
            {
                string formattedDate = selectedDate.Value.ToString("yyyy-MM-dd");
                deliveryQuery += $" AND DATE(d.Delivery_Date) = '{formattedDate}'";
            }

            deliveryQuery += "ORDER BY d.Delivery_Id DESC;";

            return deliveryQuery;
        }

        private void SetDeliveries()
        {
            string searchText = txtSearch.Text.Trim();
            string itemID = (cmbFilterItem.SelectedIndex > 0) ? cmbFilterItem.SelectedValue.ToString() : null;
            string categoryID = (cmbFilterCategory.SelectedIndex > 0) ? cmbFilterCategory.SelectedValue.ToString() : null;
            string supplierID = (cmbFilterSupplier.SelectedIndex > 0) ? cmbFilterSupplier.SelectedValue.ToString() : null;

            DateTime selectedDate = dtpDelivery.Value.Date;

            string deliveryQuery;

            if (!string.IsNullOrEmpty(searchText) || !string.IsNullOrEmpty(itemID) || !string.IsNullOrEmpty(categoryID) || !string.IsNullOrEmpty(supplierID))
            {
                deliveryQuery = BuildInventoryQuery(searchText, itemID, categoryID, supplierID, selectedDate);
            }
            else
            {
                deliveryQuery = BuildInventoryQuery(null, null, null, null, selectedDate);
            }

            List<Delivery> deliveries = DataAccess.GetDeliveriesWithItems(deliveryQuery);

            foreach (Delivery delivery in deliveries)
            {
                CopyAndPasteDeliveryPanel(delivery.DeliveryId, delivery.DeliveryNo, delivery.DeliveryDate, delivery.ItemImage, delivery.ItemName, delivery.ItemCategory, delivery.Quantity, delivery.SupplierName);
            }
        }

        private int CalculateTotalQuantityForCurrentWeek(List<Delivery> deliveryList)
        {
            DateTime today = DateTime.Today;
            int currentWeekTotalQuantity = 0;

            // Get the start and end dates for the current week
            DateTime startOfWeek = today.AddDays(-(int)today.DayOfWeek);
            DateTime endOfWeek = startOfWeek.AddDays(6);

            foreach (var delivery in deliveryList)
            {
                // Check if the delivery date falls within the current week
                if (delivery.DeliveryDate >= startOfWeek && delivery.DeliveryDate <= endOfWeek)
                {
                    currentWeekTotalQuantity += delivery.Quantity;
                }
            }

            return currentWeekTotalQuantity;
        }

        private int CalculateTotalQuantity(List<Inventory> inventoryList)
        {
            int totalQuantity = 0;

            foreach (var inventory in inventoryList)
            {
                totalQuantity += inventory.Quantity;
            }

            return totalQuantity;
        }

        private void setCurrentWeekDeliveryCounts()
        {
            string deliveryQuery = "SELECT * FROM delivery";

            List<Delivery> deliveryList = DataAccess.GetDeliveries(deliveryQuery);

            int currentWeekTotalQuantity = CalculateTotalQuantityForCurrentWeek(deliveryList);

            lblItemPerWeek.Text = currentWeekTotalQuantity.ToString();
        }

        private void setItemCounts()
        {
            string inventoryQuery = "SELECT * FROM inventory";

            List<Inventory> inventoryList = DataAccess.GetInventory(inventoryQuery);

            int totalQuantity = CalculateTotalQuantity(inventoryList);

            lblAllItem.Text = totalQuantity.ToString();
        }

        private void DeliverForm_Load(object sender, EventArgs e)
        {
            FormUtilities.setUserProfile(userPic, lblUsername, this.username);

            dtpDelivery.Checked = false;
            dtpDelivery.Value = DateTime.Now;

            setCurrentWeekDeliveryCounts();
            setItemCounts();

            flpDeliveries.Controls.Clear();
            SetDeliveries();
            cmbSetItem();
            cmbSetCategory();
            cmbSetSupplier();
            cmbSetDropDownEvents();
        }

        private void dtpDate_MouseClick(object sender, MouseEventArgs e)
        {
            dtpDelivery.Checked = false;
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            dtpDelivery.Checked = false;

            flpDeliveries.Controls.Clear();
            SetDeliveries();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            flpDeliveries.Controls.Clear();
            SetDeliveries();
        }

        private void cmbFilterItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            if (comboBox.SelectedIndex == 0 && (!dropdownStates.ContainsKey(comboBox) || !dropdownStates[comboBox]))
            {
                comboBox.ForeColor = placeholderForeColor;
            }
            else
            {
                comboBox.ForeColor = defaultForeColor;
            }

            flpDeliveries.Controls.Clear();
            SetDeliveries();
        }

        private void cmbFilterCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            if (comboBox.SelectedIndex == 0 && (!dropdownStates.ContainsKey(comboBox) || !dropdownStates[comboBox]))
            {
                comboBox.ForeColor = placeholderForeColor;
            }
            else
            {
                comboBox.ForeColor = defaultForeColor;
            }

            flpDeliveries.Controls.Clear();
            SetDeliveries();
        }

        private void cmbFilterSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            if (comboBox.SelectedIndex == 0 && (!dropdownStates.ContainsKey(comboBox) || !dropdownStates[comboBox]))
            {
                comboBox.ForeColor = placeholderForeColor;
            }
            else
            {
                comboBox.ForeColor = defaultForeColor;
            }

            flpDeliveries.Controls.Clear();
            SetDeliveries();
        }

        private void cmbItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblItemChecker.Visible = false;

            if (cmbItem.SelectedIndex > -1)
            {
                btnInsert.Text = "Save";
            }
            else
            {
                btnInsert.Text = "Add Delivery";
            }
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            lblQuantityChecker.Visible = false;

            if (txtQuantity.Text == "")
            {
                txtQuantity.Text = "0";
            }

            if (int.TryParse(txtQuantity.Text, out int quantity) && quantity > 0)
            {
                btnInsert.Text = "Save";
            }
            else
            {
                btnInsert.Text = "Add Delivery";
            }
        }

        private void cmbSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblSupplierChecker.Visible = false;

            if (cmbSupplier.SelectedIndex > -1)
            {
                btnInsert.Text = "Save";
            }
            else
            {
                btnInsert.Text = "Add Delivery";
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            SaveRecord();
        }

        private void ComboBox_DropDown(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            dropdownStates[comboBox] = true;
            comboBox.ForeColor = defaultForeColor;
        }

        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            if (!dropdownStates.ContainsKey(comboBox))
            {
                dropdownStates[comboBox] = false;
            }
            else
            {
                dropdownStates[comboBox] = false;

                if (comboBox.SelectedIndex == 0)
                {
                    comboBox.ForeColor = placeholderForeColor;
                }
                else
                {
                    comboBox.ForeColor = defaultForeColor;
                }
            }
        }

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            Guna.UI2.WinForms.Guna2TextBox txtBox = sender as Guna.UI2.WinForms.Guna2TextBox;

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Disallow non-digit characters
                e.Handled = true;
            }
            else if (txtBox.Text.Length == 1 && txtBox.Text[0] == '0' && e.KeyChar != '\b')
            {
                // Replace '0' with the entered digit
                txtBox.Text = e.KeyChar.ToString();
                txtBox.Select(txtBox.Text.Length, 0); // Move cursor to the end
                e.Handled = true;
            }
            else if (txtBox.Text.Length == 0 && e.KeyChar == '0')
            {
                // Disallow '0' as the first character
                e.Handled = true;
            }
            else if (txtBox.SelectionStart == 1 && txtBox.Text[0] == '0' && e.KeyChar != '\b')
            {
                // Allow only one leading '0'
                e.Handled = true;
            }
        }

        private Guna.UI2.WinForms.Guna2Panel pnlDelivery = new Guna.UI2.WinForms.Guna2Panel();
        private Label lblDeliveryNo = new Label();
        private Label lblDeliveryDate = new Label();
        private Label lblDeliveryTime = new Label();
        private Guna.UI2.WinForms.Guna2PictureBox picItem = new Guna.UI2.WinForms.Guna2PictureBox();
        private Label lblItemName = new Label();
        private Label lblItemCategory = new Label();
        private Label lblItemQuantity= new Label();
        private Label lblSupplierName = new Label();
    }
}
