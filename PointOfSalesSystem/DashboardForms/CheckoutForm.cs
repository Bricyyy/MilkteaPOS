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
    public partial class CheckoutForm : Form
    {
        private readonly Dashboard dashboardFormRef;
        private readonly string username;
        private readonly string userRole;

        private string userID;
        private string newPayment;
        private decimal totalAmount;
        private decimal pay;
        private decimal change;

        public CheckoutForm(Dashboard dashboardFormRef, string username, string userRole)
        {
            InitializeComponent();

            this.dashboardFormRef = dashboardFormRef;
            this.username = username;
            this.userRole = userRole;
        }

        private void GetUserID()
        {
            string userQuery = "SELECT * FROM users";

            List<User> users = DataAccess.GetUsers(userQuery);

            if (users.Count > 0)
            {
                User selectedUser = users.FirstOrDefault(user => user.Username == this.username);

                if (selectedUser != null)
                {
                    this.userID = selectedUser.UserId.ToString();
                }
            }
        }

        private void BackToMenu()
        {
            Dashboard dashboard = Application.OpenForms.OfType<Dashboard>().FirstOrDefault();

            if (dashboard == null)
            {
                dashboard = new Dashboard(username);
                dashboard.Show();
            }
            else
            {
                dashboard.Focus();
            }

            dashboard.loadForm(new MenuForm(dashboardFormRef, username, userRole));

            this.Close();
        }

        private void ResetOtherPanelBorders(Guna.UI2.WinForms.Guna2Panel selectedPanel)
        {
            // Reset the border thickness of panels other than the selected panel
            foreach (Control control in selectedPanel.Parent.Controls)
            {
                if (control is Guna.UI2.WinForms.Guna2Panel otherPanel && otherPanel != selectedPanel)
                {
                    otherPanel.BorderThickness = 0;
                }
            }
        }

        private Guna.UI2.WinForms.Guna2Panel GenerateCartPanel(string cartID, byte[] itemImage, string itemName, string itemCategory, string Size, string Sugar, string Ice, int Quantity, string Price)
        {
            pnlOrder = new Guna.UI2.WinForms.Guna2Panel
            {
                BackColor = Color.White,
                Name = "pnlOrder",
                Size = new Size(582, 100)
            };

            itemPic = new Guna.UI2.WinForms.Guna2PictureBox
            {
                BackColor = Color.Transparent,
                Image = FormUtilities.ByteArrayToImage(itemImage) ?? Properties.Resources.CoffeeIcon,
                SizeMode = PictureBoxSizeMode.Zoom,
                Name = "itemPic",
                Location = new Point(6, 5),
                Size = new Size(90, 90),
                BorderRadius = 5,
                FillColor = Color.Transparent,
                UseTransparentBackground = true
            };

            lblItemName = new Label
            {
                BackColor = Color.Transparent,
                Font = new Font("Proxima Nova Lt", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 57, 50),
                Text = itemName ?? "Item name is empty.",
                AutoEllipsis = true,
                Name = "lblItemName",
                AutoSize = true,
                Location = new Point(113, 32),
                MaximumSize = new Size(166, 16)
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
                Location = new Point(113, 48),
                MaximumSize = new Size(155, 13)
            };

            lblUnits = new Label
            {
                BackColor = Color.Transparent,
                Font = new Font("Proxima Nova Lt", 9, FontStyle.Bold),
                ForeColor = Color.Gray,
                Text = "Units",
                Name = "lblUnits",
                Location = new Point(294, 32),
                Size = new Size(36, 16)
            };

            lblItemQuantity = new Label
            {
                BackColor = Color.Transparent,
                Font = new Font("Proxima Nova Lt", 8, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 57, 50),
                Text = "x" + Quantity.ToString(),
                AutoEllipsis = true,
                Name = "lblItemQuantity",
                AutoSize = false,
                Location = new Point(294, 48),
                MaximumSize = new Size(36, 13)
            };

            lblSize = new Label
            {
                BackColor = Color.Transparent,
                Font = new Font("Proxima Nova Lt", 9, FontStyle.Bold),
                ForeColor = Color.Gray,
                Text = "Size",
                Name = "lblSize",
                Location = new Point(346, 32),
                Size = new Size(31, 16)
            };

            lblItemSize = new Label
            {
                BackColor = Color.Transparent,
                Font = new Font("Proxima Nova Lt", 8, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 57, 50),
                Text = Size,
                AutoEllipsis = true,
                Name = "lblItemSize",
                AutoSize = false,
                Location = new Point(346, 48),
                MaximumSize = new Size(56, 13)
            };

            lblSugar = new Label
            {
                BackColor = Color.Transparent,
                Font = new Font("Proxima Nova Lt", 9, FontStyle.Bold),
                ForeColor = Color.Gray,
                Text = "Sugar",
                Name = "lblSugar",
                Location = new Point(408, 32),
                Size = new Size(42, 16)
            };

            lblItemSugar = new Label
            {
                BackColor = Color.Transparent,
                Font = new Font("Proxima Nova Lt", 8, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 57, 50),
                Text = Sugar,
                AutoEllipsis = true,
                Name = "lblItemSugar",
                AutoSize = false,
                Location = new Point(408, 48),
                MaximumSize = new Size(53, 13)
            };

            lblIce = new Label
            {
                BackColor = Color.Transparent,
                Font = new Font("Proxima Nova Lt", 9, FontStyle.Bold),
                ForeColor = Color.Gray,
                Text = "Ice",
                Name = "lblIce",
                Location = new Point(467, 32),
                Size = new Size(23, 16)
            };

            lblItemIce = new Label
            {
                BackColor = Color.Transparent,
                Font = new Font("Proxima Nova Lt", 8, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 57, 50),
                Text = Ice,
                AutoEllipsis = true,
                Name = "lblItemIce",
                AutoSize = false,
                Location = new Point(467, 48),
                MaximumSize = new Size(39, 13)
            };

            lblPrice = new Label
            {
                BackColor = Color.Transparent,
                Font = new Font("Proxima Nova Lt", 9, FontStyle.Bold),
                ForeColor = Color.Gray,
                Text = "Price",
                Name = "lblPrice",
                Location = new Point(515, 32),
                Size = new Size(36, 16)
            };

            lblItemPrice = new Label
            {
                BackColor = Color.Transparent,
                Font = new Font("Proxima Nova Lt", 8, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 57, 50),
                Text = "₱" + Price,
                AutoEllipsis = true,
                Name = "lblItemPrice",
                AutoSize = false,
                Location = new Point(515, 48),
                MaximumSize = new Size(55, 13)
            };

            pnlOrder.Controls.Add(itemPic);
            pnlOrder.Controls.Add(lblItemName);
            pnlOrder.Controls.Add(lblItemCategory);
            pnlOrder.Controls.Add(lblUnits);
            pnlOrder.Controls.Add(lblItemQuantity);
            pnlOrder.Controls.Add(lblSize);
            pnlOrder.Controls.Add(lblItemSize);
            pnlOrder.Controls.Add(lblSugar);
            pnlOrder.Controls.Add(lblItemSugar);
            pnlOrder.Controls.Add(lblIce);
            pnlOrder.Controls.Add(lblItemIce);
            pnlOrder.Controls.Add(lblPrice);
            pnlOrder.Controls.Add(lblItemPrice);

            return pnlOrder;
        }

        private void CopyAndPasteCartPanel(string cartID, byte[] itemImage, string itemName, string itemCategory, string Size, string Sugar, string Ice, int Quantity, string Price)
        {
            Guna.UI2.WinForms.Guna2Panel clonedPanel = GenerateCartPanel(cartID, itemImage, itemName, itemCategory, Size, Sugar, Ice, Quantity, Price);
            flpOrders.Controls.Add(clonedPanel);
        }

        private void SetCartAndAmount()
        {
            string cartQuery = $"SELECT cart.*, inventory.inventory_id, inventory.Quantity, items.Item_Id, items.Item_Image, items.Item_Name, items.Base_Price, item_category.Category_Name " +
                               $"FROM cart " +
                               $"INNER JOIN inventory ON cart.inventory_id = inventory.inventory_id " +
                               $"INNER JOIN items ON inventory.Item_Id = items.Item_Id " +
                               $"INNER JOIN item_category ON items.Category_Id = item_category.Category_Id " +
                               $"WHERE cart.user_id = {this.userID} ORDER BY cart.cart_id DESC";

            List<OrderSummary> orders = DataAccess.GetOrderSummary(cartQuery);
            decimal totalAmount = 0;

            foreach (OrderSummary order in orders)
            {
                // Generate panel for each order details
                CopyAndPasteCartPanel(order.CartId.ToString(), order.ItemImage, order.ItemName, order.CategoryName, order.Size, order.Sugar, order.Ice, order.Quantity, order.BasePrice.ToString());

                // Calculate and accumulate total amount within the loop
                decimal itemTotal = order.BasePrice * order.Quantity;
                totalAmount += itemTotal;
            }

            this.totalAmount = totalAmount;

            // Set the total amount to lblSubTotal and lblTotal
            lblSubtotal.Text = totalAmount.ToString("₱#,##0.00");
            lblTotal.Text = totalAmount.ToString("₱#,##0.00");
        }

        private string GenerateReceiptNumber()
        {
            string randomLetters = FormUtilities.GenerateRandomLetters(4);

            // Get the last receipt number sequence from the database and increment by 1
            int lastSequenceNumber = DataAccess.GetLastSequenceNumberFromSales(); // This should retrieve the last sequence number from the database
            int nextSequenceNumber = (lastSequenceNumber % 9) + 1; // Ensures it's between 1 and 9

            // Generate the date part
            string datePart = DateTime.Now.ToString("ddyy");

            // Combine all parts to form the receipt number
            string receiptNumber = $"{randomLetters}{nextSequenceNumber:D1}{datePart}";

            return receiptNumber;
        }

        private string GenerateCartQuery()
        {
            return $"SELECT cart.*, inventory.inventory_id, inventory.Quantity, items.Item_Id, items.Item_Image, items.Item_Name, items.Base_Price, item_category.Category_Name " +
                   $"FROM cart " +
                   $"INNER JOIN inventory ON cart.inventory_id = inventory.inventory_id " +
                   $"INNER JOIN items ON inventory.Item_Id = items.Item_Id " +
                   $"INNER JOIN item_category ON items.Category_Id = item_category.Category_Id " +
                   $"WHERE cart.user_id = {this.userID} ORDER BY cart.cart_id DESC";
        }

        private bool CheckAvailableQuantity(List<OrderSummary> orders)
        {
            foreach (OrderSummary order in orders)
            {
                int availableQuantity = DataAccess.GetAvailableQuantity(order.InventoryId);

                if (availableQuantity < order.Quantity)
                {
                    MessageBox.Show("Insufficient quantity available for one or more items. Please adjust your order.");
                    return false;
                }
            }

            return true;
        }

        private void ProcessSales(string receiptNo, List<OrderSummary> orders)
        {
            foreach (OrderSummary order in orders)
            {
                string itemDescription = $"{order.Size} {order.Sugar} {order.Ice}";
                int newQuantity = DataAccess.GetAvailableQuantity(order.InventoryId) - order.Quantity;

                DataAccess.InsertSales(Convert.ToInt32(this.userID), receiptNo, DateTime.Now, order.ItemId, order.Quantity, itemDescription, this.totalAmount, this.pay, this.change);
                DataAccess.UpdateInventory(order.InventoryId, newQuantity);
                DataAccess.DeleteCart(order.CartId);
            }
        }

        private void ClearUIAfterCheckout()
        {
            flpOrders.Controls.Clear();
            this.totalAmount = 0;
            this.pay = 0;
            this.change = 0;
            lblSubtotal.Text = "₱0.00";
            lblTotal.Text = "₱0.00";
            txtName.Text = "";
            txtAmount.Text = "";
            lblChange.Text = "₱0.00";
            btnCheckout.Text = "Done";
        }

        private void PrintBill()
        {
            string cartQuery = GenerateCartQuery();
            string receiptNo = GenerateReceiptNumber();

            List<OrderSummary> orders = DataAccess.GetOrderSummary(cartQuery);

            if (!CheckAvailableQuantity(orders))
            {
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

                DialogResult result = orderInfo.ShowDialog();

                if (result == DialogResult.OK)
                {
                    ProcessSales(receiptNo, orders);
                    ClearUIAfterCheckout();
                }

                modalBackground.Dispose();
            }
        }

        private void CheckoutForm_Load(object sender, EventArgs e)
        {
            dtpDate.Checked = false;
            dtpDate.Value = DateTime.Now;
            radioCash.CheckedChanged += RadioButton_CheckedChanged;
            radioCard.CheckedChanged += RadioButton_CheckedChanged;
            radioPaypal.CheckedChanged += RadioButton_CheckedChanged;
            radioApplePay.CheckedChanged += RadioButton_CheckedChanged;

            GetUserID();

            flpOrders.Controls.Clear();
            SetCartAndAmount();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            BackToMenu();
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            dtpDate.Value = DateTime.Now;
            dtpDate.Checked = false;
        }

        private void dtpDate_MouseClick(object sender, MouseEventArgs e)
        {
            dtpDate.Checked = false;
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            newPayment = FormUtilities.PaymentChanged(sender, radioCash, radioCard, radioPaypal, radioApplePay);

            if (sender is Guna.UI2.WinForms.Guna2CustomRadioButton radioButton)
            {
                // Retrieve the corresponding panel for the clicked radio button
                Guna.UI2.WinForms.Guna2Panel parentPanel = null;

                if (radioButton == radioCash)
                {
                    parentPanel = pnlCash;
                }
                else if (radioButton == radioCard)
                {
                    parentPanel = pnlCard;
                }
                else if (radioButton == radioPaypal)
                {
                    parentPanel = pnlPaypal;
                }
                else if (radioButton == radioApplePay)
                {
                    parentPanel = pnlApplePay;
                }

                // Set the border thickness of the clicked radio button's associated panel
                parentPanel.BorderThickness = 1;

                // Reset the border thickness of other panels
                ResetOtherPanelBorders(parentPanel);
            }
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            lblAmountChecker.Visible = false;

            this.pay = txtAmount.Text != "" ? Convert.ToDecimal(txtAmount.Text) : 0;
            this.change = pay - this.totalAmount;

            lblChange.Text = txtAmount.Text != "" ? change.ToString(("₱#,##0.00")) : "₱0.00";
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            if (btnCheckout.Text == "Done")
            {
                BackToMenu();
                return;
            }

            if (string.IsNullOrEmpty(txtAmount.Text) || this.totalAmount > this.pay)
            {
                lblAmountChecker.Visible = true;
            }
            else
            {
                PrintBill();
            }
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (sender is Guna.UI2.WinForms.Guna2TextBox textBox)
            {
                // Allow only digits, a single decimal point, and control characters
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                {
                    e.Handled = true;
                }

                // Allow only one decimal point
                if (e.KeyChar == '.' && (textBox.Text.IndexOf('.') > -1 || textBox.Text.Length == 0))
                {
                    e.Handled = true;
                }

                // Limit to two digits after the decimal point
                if (textBox.Text.Contains('.'))
                {
                    int index = textBox.Text.IndexOf('.');
                    if (textBox.Text.Length - index > 2 && !char.IsControl(e.KeyChar))
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        private Guna.UI2.WinForms.Guna2Panel pnlOrder = new Guna.UI2.WinForms.Guna2Panel();
        private Guna.UI2.WinForms.Guna2PictureBox itemPic = new Guna.UI2.WinForms.Guna2PictureBox();
        private Label lblItemName = new Label();
        private Label lblItemCategory = new Label();
        private Label lblUnits = new Label();
        private Label lblItemQuantity = new Label();
        private Label lblSize = new Label();
        private Label lblItemSize = new Label();
        private Label lblSugar = new Label();
        private Label lblItemSugar = new Label();
        private Label lblIce = new Label();
        private Label lblItemIce = new Label();
        private Label lblPrice = new Label();
        private Label lblItemPrice = new Label();
    }
}
