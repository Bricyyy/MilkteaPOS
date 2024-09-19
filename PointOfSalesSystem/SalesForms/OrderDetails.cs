using PointOfSalesSystem.GetOrSet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PointOfSalesSystem.SalesForms
{
    public partial class OrderDetails : Form
    {
        private readonly HistoryForm historyFormRef;
        private readonly string receiptNo;

        public OrderDetails(HistoryForm historyForm, string receiptNo)
        {
            InitializeComponent();

            this.historyFormRef = historyForm;
            this.receiptNo = receiptNo;
        }

        private User GetSelectedUser(int userId)
        {
            string userQuery = "SELECT * FROM users";
            List<User> users = DataAccess.GetUsers(userQuery);
            return users.FirstOrDefault(user => user.UserId == userId);
        }

        private void SetUserInformation(User selectedUser)
        {
            if (selectedUser.UserImage != null)
            {
                userPic.Image = FormUtilities.ByteArrayToImage(selectedUser.UserImage);
            }
            else
            {
                userPic.Image = Properties.Resources.PersonIcon;
            }

            lblUsername.Text = selectedUser.Username;
            string role = selectedUser.Role;
            if (!string.IsNullOrEmpty(role))
            {
                lblRole.Text = char.ToUpper(role[0]) + role.Substring(1);
            }

            lblEmail.Text = selectedUser.Email;

            userPic.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void SetOrderData()
        {
            lblOrderNo.Text = "Order: #" + this.receiptNo;

            string salesQuery = $"SELECT Receipt_Id, User_Id, Receipt_No, Receipt_Date, Item_Id, Quantity, Item_Desc, Total_Amount, Amount_Paid, Total_Change FROM sales WHERE Receipt_No LIKE CONCAT('%', '{this.receiptNo}', '%') ORDER BY Receipt_Id DESC";
            List<Sales> sales = DataAccess.GetSales(salesQuery);

            if (sales.Count == 0) return;

            Sales sale = sales[0];

            User selectedUser = GetSelectedUser(sale.UserId);

            if (selectedUser != null)
            {
                SetUserInformation(selectedUser);
            }

            string formattedDate = sale.ReceiptDate.ToString("MMMM dd, yyyy");
            lblDate.Text = formattedDate;

            lblTotal.Text = "₱" + sale.TotalAmount.ToString();
            lblPaid.Text = "₱" + sale.AmountPaid.ToString();
            lblChange.Text = "₱" + sale.TotalChange.ToString();
        }

        private Guna.UI2.WinForms.Guna2Panel GenerateItemPanel(byte[] itemImage, string itemName, string categoryName, string itemDesc, string basePrice, string itemQuantity, string totalAmount)
        {
            pnlItem = new Guna.UI2.WinForms.Guna2Panel
            {
                BackColor = Color.White,
                Name = "pnlItem",
                Size = new Size(601, 77)
            };

            itemPic = new Guna.UI2.WinForms.Guna2PictureBox
            {
                BackColor = Color.Transparent,
                Image = FormUtilities.ByteArrayToImage(itemImage) ?? Properties.Resources.CoffeeIcon,
                SizeMode = PictureBoxSizeMode.Zoom,
                Name = "itemPic",
                Location = new Point(8, 8),
                Size = new Size(60, 60),
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
                AutoEllipsis = true,
                Name = "lblItemName",
                AutoSize = true,
                Location = new Point(74, 22),
                MaximumSize = new Size(142, 15)
            };

            lblCategoryName = new Label
            {
                BackColor = Color.Transparent,
                Font = new Font("Proxima Nova Lt", 8, FontStyle.Bold),
                ForeColor = Color.Gray,
                Text = categoryName,
                AutoEllipsis = true,
                Name = "lblCategoryName",
                AutoSize = true,
                Location = new Point(74, 41),
                MaximumSize = new Size(155, 13)
            };

            lblItemDetails = new Label
            {
                BackColor = Color.Transparent,
                Font = new Font("Proxima Nova Lt", 8, FontStyle.Bold),
                ForeColor = Color.Gray,
                Text = itemDesc,
                AutoEllipsis = true,
                Name = "lblItemDetails",
                AutoSize = true,
                Location = new Point(222, 19),
                MaximumSize = new Size(80, 45)
            };

            lblItemPrice = new Label
            {
                BackColor = Color.Transparent,
                Font = new Font("Proxima Nova Lt", 8, FontStyle.Bold),
                ForeColor = Color.Gray,
                Text = "₱" + basePrice,
                TextAlign = ContentAlignment.MiddleCenter,
                AutoEllipsis = true,
                Name = "lblItemPrice",
                AutoSize = true,
                Location = new Point(321, 30),
                MinimumSize = new Size(99, 16),
                MaximumSize = new Size(99, 16)
            };

            lblQuantity = new Label
            {
                BackColor = Color.Transparent,
                Font = new Font("Proxima Nova Lt", 8, FontStyle.Bold),
                ForeColor = Color.Gray,
                Text = "x" + itemQuantity,
                TextAlign = ContentAlignment.MiddleCenter,
                AutoEllipsis = true,
                Name = "lblQuantity",
                AutoSize = true,
                Location = new Point(428, 30),
                MinimumSize = new Size(74, 16),
                MaximumSize = new Size(74, 16)
            };

            lblItemTotal = new Label
            {
                BackColor = Color.Transparent,
                Font = new Font("Proxima Nova Lt", 8, FontStyle.Bold),
                ForeColor = Color.Gray,
                RightToLeft = RightToLeft.Yes,
                Text = "₱" + totalAmount,
                TextAlign = ContentAlignment.MiddleLeft,
                AutoEllipsis = true,
                Name = "lblItemTotal",
                AutoSize = false,
                Location = new Point(511, 28),
                MaximumSize = new Size(87, 20)
            };

            pnlItem.Controls.Add(itemPic);
            pnlItem.Controls.Add(lblItemName);
            pnlItem.Controls.Add(lblCategoryName);
            pnlItem.Controls.Add(lblItemDetails);
            pnlItem.Controls.Add(lblItemPrice);
            pnlItem.Controls.Add(lblQuantity);
            pnlItem.Controls.Add(lblItemTotal);

            return pnlItem;
        }

        string GetPrefix(int index)
        {
            switch (index)
            {
                case 0:
                    return "Size:";
                case 1:
                    return "Sugar:";
                case 2:
                    return "Ice:";
                default:
                    return string.Empty;
            }
        }

        private void CopyAndPasteItemsPanel(byte[] itemImage, string itemName, string categoryName, string itemDesc, string basePrice, string itemQuantity, string totalAmount)
        {
            string[] words = itemDesc.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                words[i] = $"{GetPrefix(i)} {words[i]}";
            }

            string formattedDetails = string.Join(Environment.NewLine, words);

            Guna.UI2.WinForms.Guna2Panel clonedPanel = GenerateItemPanel(itemImage, itemName, categoryName, formattedDetails, basePrice, itemQuantity, totalAmount);
            flpItems.Controls.Add(clonedPanel);
        }

        private void SetOrders()
        {
            string salesQuery = $"SELECT * FROM sales WHERE Receipt_No LIKE CONCAT('%', '{this.receiptNo}', '%') ORDER BY Receipt_Id DESC";
            List<Sales> sales = DataAccess.GetSales(salesQuery);

            foreach (Sales sale in sales)
            {
                // Retrieve item details using Item_Id from the sales table
                string itemQuery = $"SELECT * FROM items WHERE Item_Id = {sale.ItemId}";
                List<Item> items = DataAccess.GetItems(itemQuery);

                if (items.Count > 0)
                {
                    Item item = items[0]; // Assuming only one item is fetched for an item ID

                    // Retrieve Category_Name for the item's Category_Id
                    string categoryQuery = $"SELECT * FROM item_category WHERE Category_Id = {item.CategoryId}";
                    List<Category> categories = DataAccess.GetCategories(categoryQuery);

                    string categoryName = (categories.Count > 0) ? categories[0].CategoryName : "N/A";

                    // Calculate totalAmount
                    decimal totalAmount = item.BasePrice * sale.Quantity;

                    // Generate items panel using retrieved information
                    CopyAndPasteItemsPanel(item.ItemImage, item.ItemName, categoryName, sale.ItemDesc, item.BasePrice.ToString(), sale.Quantity.ToString(), totalAmount.ToString());
                }
            }
        }

        private void OrderDetails_Load(object sender, EventArgs e)
        {
            SetOrderData();

            flpItems.Controls.Clear();
            SetOrders();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            historyFormRef.loadForm(new OrderHistoryLists(historyFormRef));
        }

        private Guna.UI2.WinForms.Guna2Panel pnlItem = new Guna.UI2.WinForms.Guna2Panel();
        private Guna.UI2.WinForms.Guna2PictureBox itemPic = new Guna.UI2.WinForms.Guna2PictureBox();
        private Label lblItemName = new Label();
        private Label lblItemDetails = new Label();
        private Label lblCategoryName = new Label();
        private Label lblItemPrice = new Label();
        private Label lblQuantity = new Label();
        private Label lblItemTotal = new Label();
    }
}
