using Microsoft.VisualBasic.ApplicationServices;
using PointOfSalesSystem.GetOrSet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows.Forms;
using ZstdSharp.Unsafe;

namespace PointOfSalesSystem
{
    public partial class MenuForm : Form
    {
        private readonly Dashboard dashboardFormRef;
        private readonly Color defaultPanelColor = Color.White;
        private readonly Color selectedPanelColor = Color.FromArgb(242, 106, 36);

        private readonly string username;
        private readonly string userRole;
        private readonly int steps = 5;

        private string userID;
        private string categoryID;
        private const int AnimationDuration = 25;
        private const int FinalWidth = 340;

        public MenuForm(Dashboard dashboardFormRef, string username, string userRole)
        {
            InitializeComponent();

            this.dashboardFormRef = dashboardFormRef;
            this.username = username;
            this.userRole = userRole;
        }

        private void ResetPanelColor(Guna.UI2.WinForms.Guna2Panel panel)
        {
            panel.FillColor = defaultPanelColor;
        }

        private void UpdatePanelColor(Guna.UI2.WinForms.Guna2Panel panel)
        {
            panel.FillColor = selectedPanelColor;
        }

        private async void ExpandPanel()
        {
            int initialLeft = pnlCart.Left; // Initial left position
            int initialWidth = pnlCart.Width; // Initial width

            int increaseAmount = FinalWidth - initialWidth; // Amount to increase the width

            // Calculate the increment value for smooth animation
            int widthIncrement = increaseAmount / steps;
            int leftIncrement = widthIncrement; // Adjust left position with width change

            for (int i = 0; i < steps; i++)
            {
                pnlCart.Width += widthIncrement; // Incrementally increase the width
                pnlCart.Left -= leftIncrement; // Adjust the left position

                await Task.Delay(AnimationDuration / steps);
            }

            // Ensure the final width and position are set
            pnlCart.Width = FinalWidth;
            pnlCart.Left = initialLeft + (initialWidth - FinalWidth); // Adjust final left position
        }

        private async void CollapsePanel()
        {
            int initialLeft = pnlCart.Left; // Initial left position
            int initialWidth = pnlCart.Width; // Current width

            // Calculate the increment value for smooth animation
            int widthDecrement = initialWidth / steps;
            int leftDecrement = widthDecrement; // Adjust left position with width change

            for (int i = 0; i < steps; i++)
            {
                pnlCart.Width -= widthDecrement; // Decrement the width
                pnlCart.Left += leftDecrement; // Adjust the left position

                await Task.Delay(AnimationDuration / steps);
            }

            // Ensure the final width and position are set
            pnlCart.Width = 0;
            pnlCart.Left = initialLeft + initialWidth; // Reset left position to initial state
        }

        private Guna.UI2.WinForms.Guna2Panel GenerateCategoryPanel(string categoryID, string categoryName)
        {
            pnlCategory = new Guna.UI2.WinForms.Guna2Panel
            {
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
                AutoSize = true,
                Padding = new Padding(10, 5, 20, 5),
                MinimumSize = new Size(76, 52),
                MaximumSize = new Size(230, 52),
                BorderColor = Color.Transparent,
                BorderRadius = 15,
                FillColor = Color.White,
                Tag = categoryID
            };

            lblCategoryName = new Label
            {
                Font = new Font("Proxima Nova Lt", 12, FontStyle.Bold),
                ForeColor = Color.Gray,
                Text = categoryName,
                TextAlign = ContentAlignment.TopLeft,
                Location = new Point(25, 18),
                AutoEllipsis = true,
                AutoSize = true,
                MaximumSize = new Size(200,18)
            };

            pnlCategory.Controls.Add(lblCategoryName);

            lblCategoryName.MouseClick += pnlCategory_MouseClick;

            pnlCategory.Click += pnlCategory_Click;

            return pnlCategory;
        }

        private void CopyAndPasteCategoryPanel(string categoryID, string categoryName)
        {
            Guna.UI2.WinForms.Guna2Panel clonedPanel = GenerateCategoryPanel(categoryID, categoryName);
            flpCategories.Controls.Add(clonedPanel);
        }

        private void AddMainCategoryPanel()
        {
            int allItemsCount = DataAccess.GetEntityCount("items");
            lblItemCount.Text = allItemsCount.ToString() + " - All Items";

            lblMainCategoryName.MouseClick += pnlCategory_MouseClick;
            pnlMainCategory.Click += pnlCategory_Click;
            flpCategories.Controls.Add(pnlMainCategory);
        }

        private void SetCategories()
        {
            AddMainCategoryPanel();

            string categoryQuery = "SELECT Category_Id, Category_Name FROM item_category ORDER BY Category_Name ASC";
            List<Category> categories = DataAccess.GetCategories(categoryQuery);

            foreach (Category category in categories)
            {
                CopyAndPasteCategoryPanel(category.CategoryId.ToString(), category.CategoryName);
            }
        }

        private Guna.UI2.WinForms.Guna2Panel GenerateItemsPanel(string inventoryID, byte[] itemImage, string itemName, decimal basePrice, string quantity)
        {
            pnlItem = new Guna.UI2.WinForms.Guna2Panel
            {
                BackColor = Color.Transparent,
                Name = "pnlItem",
                Size = new Size(243, 230),
                BorderColor = Color.FromArgb(247, 247, 247),
                BorderRadius = 15,
                BorderThickness = 1,
                FillColor = Color.White
            };

            noStockPic = new Guna.UI2.WinForms.Guna2PictureBox
            {
                BackColor = Color.Transparent,
                Image = Properties.Resources.no_stock,
                SizeMode = PictureBoxSizeMode.Zoom,
                Name = "noStockPic",
                Location = new Point(11, 10),
                Size = new Size(92, 92),
                FillColor = Color.Transparent,
                UseTransparentBackground = true,
                Visible = quantity == "0"
            };

            itemPic = new Guna.UI2.WinForms.Guna2PictureBox
            {
                BackColor = Color.Transparent,
                Image = FormUtilities.ByteArrayToImage(itemImage) ?? Properties.Resources.CoffeeIcon,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Name = "itemPic",
                Location = new Point(11, 10),
                Size = new Size(90, 90),
                BorderRadius = 15,
                FillColor = Color.Transparent,
                UseTransparentBackground = true
            };

            lblItemName = new Label
            {
                BackColor = Color.Transparent,
                Font = new Font("Proxima Nova Lt", 12, FontStyle.Bold),
                ForeColor = Color.Black,
                Text = itemName ?? "Item name is empty.",
                AutoEllipsis = true,
                Name = "lblItemName",
                AutoSize = true,
                Location = new Point(110, 13),
                MaximumSize = new Size(121, 46)
            };

            lblItemDesc = new Label
            {
                BackColor = Color.Transparent,
                Font = new Font("Proxima Nova Rg", 8, FontStyle.Regular),
                ForeColor = Color.Gray,
                Text = "Item description...",
                AutoEllipsis = true,
                Name = "lblItemDesc",
                AutoSize = true,
                Location = new Point(111, 61),
                MaximumSize = new Size(91, 13)
            };

            lblItemPrice = new Label
            {
                BackColor = Color.Transparent,
                Font = new Font("Proxima Nova Lt", 9, FontStyle.Bold),
                ForeColor = Color.Black,
                Text = "₱" + basePrice.ToString() + " • " + quantity + " Available",
                AutoEllipsis = true,
                Name = "lblItemPrice",
                AutoSize = true,
                Location = new Point(111, 84),
                MaximumSize = new Size(130, 16)
            };

            lblQuantity = new Label
            {
                BackColor = Color.Transparent,
                Font = new Font("Proxima Nova Lt", 9, FontStyle.Bold),
                ForeColor = Color.Black,
                Text = "Quantity",
                AutoEllipsis = true,
                Name = "lblQuantity",
                AutoSize = true,
                Location = new Point(9, 114),
                MaximumSize = new Size(56, 16)
            };

            btnMinus = new Guna.UI2.WinForms.Guna2Button
            {
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
                Name = "btnMinus",
                Location = new Point(11, 138),
                Size = new Size(20, 20),
                Animated = true,
                BorderRadius = 10,
                FillColor = Color.FromArgb(221, 220, 226),
                Image = Properties.Resources.minus_icon,
                ImageSize = new Size(10, 10),
                UseTransparentBackground = true
            };

            txtQuantity = new Guna.UI2.WinForms.Guna2TextBox
            {
                BackColor = Color.Transparent,
                Font = new Font("Proxima Nova Rg", 8, FontStyle.Regular),
                Name = "txtQuantity",
                Location = new Point(37, 138),
                Size = new Size(39, 20),
                Animated = true,
                BorderColor = Color.FromArgb(213, 218, 223),
                BorderRadius = 10,
                FillColor = Color.White,
                Text = "0",
                TextAlign = HorizontalAlignment.Center,
                ForeColor = Color.Black,
                Tag = inventoryID
            };

            btnPlus = new Guna.UI2.WinForms.Guna2Button
            {
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
                Name = "btnPlus",
                Location = new Point(82, 138),
                Size = new Size(20, 20),
                Animated = true,
                BorderRadius = 10,
                FillColor = Color.FromArgb(221, 220, 226),
                Image = Properties.Resources.plus_icon,
                ImageSize = new Size(10, 10),
                UseTransparentBackground = true,
                Tag = quantity
            };

            lblSize = new Label()
            {
                BackColor = Color.Transparent,
                Font = new Font("Proxima Nova Lt", 9, FontStyle.Bold),
                ForeColor = Color.Black,
                Text = "Size",
                AutoEllipsis = true,
                Name = "lblSize",
                AutoSize = true,
                Location = new Point(129, 114),
                MaximumSize = new Size(31, 16)
            };

            btnSmall = new Guna.UI2.WinForms.Guna2Button
            {
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
                Name = "btnSmall",
                Location = new Point(132, 138),
                Size = new Size(20, 20),
                Animated = true,
                BorderColor = Color.FromArgb(30, 57, 50),
                BorderRadius = 10,
                BorderThickness = 0,
                FillColor = Color.FromArgb(246, 239, 239),
                Image = Properties.Resources.tall_icon,
                ImageSize = new Size(10, 10),
                UseTransparentBackground = true,
                Tag = "Tall"
            };

            btnMedium = new Guna.UI2.WinForms.Guna2Button
            {
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
                Name = "btnMedium",
                Location = new Point(158, 138),
                Size = new Size(20, 20),
                Animated = true,
                BorderColor = Color.FromArgb(30, 57, 50),
                BorderRadius = 10,
                BorderThickness = 0,
                FillColor = Color.FromArgb(246, 239, 239),
                Image = Properties.Resources.grande_icon,
                ImageSize = new Size(10, 10),
                UseTransparentBackground = true,
                Tag = "Grande"
            };

            btnLarge = new Guna.UI2.WinForms.Guna2Button
            {
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
                Name = "btnLarge",
                Location = new Point(184, 138),
                Size = new Size(20, 20),
                Animated = true,
                BorderColor = Color.FromArgb(30, 57, 50),
                BorderRadius = 10,
                BorderThickness = 0,
                FillColor = Color.FromArgb(246, 239, 239),
                Image = Properties.Resources.venti_icon,
                ImageSize = new Size(10, 10),
                UseTransparentBackground = true,
                Tag = "Venti"
            };

            lblSugarLevel = new Label
            {
                BackColor = Color.Transparent,
                Font = new Font("Proxima Nova Lt", 9, FontStyle.Bold),
                ForeColor = Color.Black,
                Text = "Sugar (%)",
                AutoEllipsis = true,
                Name = "lblSugarLevel",
                AutoSize = true,
                Location = new Point(9, 175),
                MaximumSize = new Size(63, 16)
            };

            btnSugarThirty = new Guna.UI2.WinForms.Guna2Button
            {
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
                Name = "btnSugarThirty",
                Location = new Point(11, 199),
                Size = new Size(30, 20),
                Animated = true,
                BorderColor = Color.FromArgb(30, 57, 50),
                BorderRadius = 10,
                BorderThickness = 0,
                FillColor = Color.FromArgb(246, 239, 239),
                Image = Properties.Resources.thirty,
                ImageSize = new Size(30, 30),
                UseTransparentBackground = true,
                Tag = "30%"
            };

            btnSugarFifty = new Guna.UI2.WinForms.Guna2Button
            {
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
                Name = "btnSugarFifty",
                Location = new Point(46, 199),
                Size = new Size(30, 20),
                Animated = true,
                BorderColor = Color.FromArgb(30, 57, 50),
                BorderRadius = 10,
                BorderThickness = 0,
                FillColor = Color.FromArgb(246, 239, 239),
                Image = Properties.Resources.fifty,
                ImageSize = new Size(23, 23),
                UseTransparentBackground = true,
                Tag = "50%"
            };

            btnSugarSeventy = new Guna.UI2.WinForms.Guna2Button
            {
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
                Name = "btnSugarSeventy",
                Location = new Point(82, 199),
                Size = new Size(30, 20),
                Animated = true,
                BorderColor = Color.FromArgb(30, 57, 50),
                BorderRadius = 10,
                BorderThickness = 0,
                FillColor = Color.FromArgb(246, 239, 239),
                Image = Properties.Resources.seventy,
                ImageSize = new Size(23, 23),
                UseTransparentBackground = true,
                Tag = "70%"
            };

            lblIceLevel = new Label
            {
                BackColor = Color.Transparent,
                Font = new Font("Proxima Nova Lt", 9, FontStyle.Bold),
                ForeColor = Color.Black,
                Text = "Ice (%)",
                AutoEllipsis = true,
                Name = "lblIceLevel",
                AutoSize = true,
                Location = new Point(129, 175),
                MaximumSize = new Size(44, 16)
            };

            btnIceThirty = new Guna.UI2.WinForms.Guna2Button
            {
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
                Name = "btnIceThirty",
                Location = new Point(132, 199),
                Size = new Size(30, 20),
                Animated = true,
                BorderColor = Color.FromArgb(30, 57, 50),
                BorderRadius = 10,
                BorderThickness = 0,
                FillColor = Color.FromArgb(246, 239, 239),
                Image = Properties.Resources.thirty,
                ImageSize = new Size(30, 30),
                UseTransparentBackground = true,
                Tag = "30%"
            };

            btnIceFifty = new Guna.UI2.WinForms.Guna2Button
            {
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
                Name = "btnIceFifty",
                Location = new Point(168, 199),
                Size = new Size(30, 20),
                Animated = true,
                BorderColor = Color.FromArgb(30, 57, 50),
                BorderRadius = 10,
                BorderThickness = 0,
                FillColor = Color.FromArgb(246, 239, 239),
                Image = Properties.Resources.fifty,
                ImageSize = new Size(23, 23),
                UseTransparentBackground = true,
                Tag = "50%"
            };

            btnIceSeventy = new Guna.UI2.WinForms.Guna2Button
            {
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
                Name = "btnIceSeventy",
                Location = new Point(201, 199),
                Size = new Size(30, 20),
                Animated = true,
                BorderColor = Color.FromArgb(30, 57, 50),
                BorderRadius = 10,
                BorderThickness = 0,
                FillColor = Color.FromArgb(246, 239, 239),
                Image = Properties.Resources.seventy,
                ImageSize = new Size(23, 23),
                UseTransparentBackground = true,
                Tag = "70%"
            };

            btnAddToBilling = new Guna.UI2.WinForms.Guna2Button
            {
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
                Font = new Font("Proxima Nova Lt", 10, FontStyle.Bold),
                ForeColor = Color.White,
                Name = "btnAddToBilling",
                Location = new Point(29, 231),
                Size = new Size(180, 45),
                Animated = true,
                BorderColor = Color.Transparent,
                BorderRadius = 10,
                FillColor = Color.FromArgb(0, 117, 74),
                UseTransparentBackground = true,
                Text = "Add to Billing",
                Visible = false,
                Tag = inventoryID
            };

            btnMinus.Click += btnMinus_Click;
            txtQuantity.TextChanged += txtQuantity_TextChanged;
            txtQuantity.KeyPress += txtQuantity_KeyPress;
            btnPlus.Click += btnPlus_Click;
            btnSmall.Click += btnSize_Click;
            btnMedium.Click += btnSize_Click;
            btnLarge.Click += btnSize_Click;
            btnSugarThirty.Click += btnSugar_Click;
            btnSugarFifty.Click += btnSugar_Click;
            btnSugarSeventy.Click += btnSugar_Click;
            btnIceThirty.Click += btnIce_Click;
            btnIceFifty.Click += btnIce_Click;
            btnIceSeventy.Click += btnIce_Click;
            btnAddToBilling.Click += btnAddToBilling_Click;

            pnlItem.Controls.Add(noStockPic);
            pnlItem.Controls.Add(itemPic);
            pnlItem.Controls.Add(lblItemName);
            pnlItem.Controls.Add(lblItemDesc);
            pnlItem.Controls.Add(lblItemPrice);
            pnlItem.Controls.Add(lblQuantity);
            pnlItem.Controls.Add(btnMinus);
            pnlItem.Controls.Add(txtQuantity);
            pnlItem.Controls.Add(btnPlus);
            pnlItem.Controls.Add(lblSize);
            pnlItem.Controls.Add(btnSmall);
            pnlItem.Controls.Add(btnMedium);
            pnlItem.Controls.Add(btnLarge);
            pnlItem.Controls.Add(lblSugarLevel);
            pnlItem.Controls.Add(btnSugarThirty);
            pnlItem.Controls.Add(btnSugarFifty);
            pnlItem.Controls.Add(btnSugarSeventy);
            pnlItem.Controls.Add(lblIceLevel);
            pnlItem.Controls.Add(btnIceThirty);
            pnlItem.Controls.Add(btnIceFifty);
            pnlItem.Controls.Add(btnIceSeventy);
            pnlItem.Controls.Add(btnAddToBilling);

            return pnlItem;
        }

        private void CopyAndPasteItemPanel(string inventoryID, byte[] itemImage, string itemName, decimal basePrice, string quantity)
        {
            Guna.UI2.WinForms.Guna2Panel clonedPanel = GenerateItemsPanel(inventoryID, itemImage, itemName, basePrice, quantity);
            flpItems.Controls.Add(clonedPanel);
        }

        private string BuildInventoryQuery(string categoryID, string searchText)
        {
            string inventoryQuery;

            if (categoryID != null)
            {
                if (txtSearch.Text == "")
                {
                    inventoryQuery = $@"SELECT iv.inventory_id, i.Item_Id, i.Item_Image, i.Item_Name, i.Category_Id, i.Base_Price, iv.Quantity
                                        FROM inventory iv
                                        INNER JOIN items i ON iv.Item_Id = i.Item_Id
                                        WHERE i.Category_Id = {categoryID}
                                        ORDER BY i.Item_Name ASC";
                }
                else
                {
                    inventoryQuery = $@"SELECT iv.inventory_id, i.Item_Id, i.Item_Image, i.Item_Name, i.Category_Id, i.Base_Price, iv.Quantity
                                        FROM inventory iv
                                        INNER JOIN items i ON iv.Item_Id = i.Item_Id
                                        WHERE i.Category_Id = {categoryID} AND i.Item_Name LIKE CONCAT('%', '{searchText}', '%')
                                        ORDER BY i.Item_Name ASC";
                }
            }
            else
            {
                if (txtSearch.Text == "")
                {
                    inventoryQuery = $@"SELECT iv.inventory_id, i.Item_Id, i.Item_Image, i.Item_Name, i.Category_Id, i.Base_Price, iv.Quantity
                                        FROM inventory iv
                                        INNER JOIN items i ON iv.Item_Id = i.Item_Id
                                        ORDER BY i.Item_Name ASC";
                }
                else
                {
                    inventoryQuery = $@"SELECT iv.inventory_id, i.Item_Id, i.Item_Image, i.Item_Name, i.Category_Id, i.Base_Price, iv.Quantity
                                        FROM inventory iv
                                        INNER JOIN items i ON iv.Item_Id = i.Item_Id
                                        WHERE i.Item_Name LIKE CONCAT('%', '{searchText}', '%')
                                        ORDER BY i.Item_Name ASC";
                }
            }

            return inventoryQuery;
        }

        private void SetItemsFromInventory(string categoryID = null)
        {
            string inventoryQuery = BuildInventoryQuery(categoryID, txtSearch.Text);

            List<Inventory> inventories = DataAccess.GetItemsFromInventory(inventoryQuery);

            foreach (Inventory inventory in inventories)
            {
                CopyAndPasteItemPanel(inventory.InventoryId.ToString(), inventory.ItemImage, inventory.ItemName, inventory.BasePrice, inventory.Quantity.ToString());
            }

            if (flpItems.Controls.Count == 0)
            {
                flpItems.Controls.Clear();
                flpItems.Controls.Add(pnlNoItem);
            }
        }

        private void AddToCart(string inventoryID, string itemQuantity, string itemSize, string itemSugar, string itemIce)
        {
            // Get existing cart items
            List<Cart> cartItems = DataAccess.GetCart($"SELECT * FROM cart WHERE user_id = {this.userID} AND inventory_id = {inventoryID}");

            // If userID and inventoryID exist in the cart table
            if (cartItems.Count > 0)
            {
                Cart existingCartItem = cartItems.FirstOrDefault(item => item.Size == itemSize && item.Sugar == itemSugar && item.Ice == itemIce);

                if (existingCartItem != null)
                {
                    // Get the existing quantity for the item
                    int existingQuantity = existingCartItem.Quantity;

                    // Calculate the new quantity by adding the existing quantity and the new one
                    int newQuantity = existingQuantity + Convert.ToInt32(itemQuantity);

                    // Update the quantity if itemSize, itemSugar, and itemIce are the same
                    DataAccess.UpdateCart(existingCartItem.CartId, newQuantity);
                }
                else
                {
                    // Insert a new record as itemSize, itemSugar, or itemIce differs
                    DataAccess.InsertCart(Convert.ToInt32(this.userID), Convert.ToInt32(inventoryID), Convert.ToInt32(itemQuantity), itemSize, itemSugar, itemIce);
                }
            }
            else
            {
                // If userID and inventoryID do not exist in the cart table, insert data
                DataAccess.InsertCart(Convert.ToInt32(this.userID), Convert.ToInt32(inventoryID), Convert.ToInt32(itemQuantity), itemSize, itemSugar, itemIce);
            }

            SetCartCount();
            flpOrders.Controls.Clear();
            btnCheckout.Text = "Proceed";
            SetCart();
            SetAmount();
        }

        private Guna.UI2.WinForms.Guna2Panel GenerateCartPanel(string cartID, byte[] itemImage, string itemName, string Size, string Sugar, string Ice, int maxQuantity, int currentQuantity, int totalQuantityInCart, string Price)
        {
            pnlOrder = new Guna.UI2.WinForms.Guna2Panel
            {
                BackColor = Color.White,
                Name = "pnlOrder",
                Size = new Size(302, 111),
                Tag = cartID
            };

            itemPic = new Guna.UI2.WinForms.Guna2PictureBox
            {
                BackColor = Color.Transparent,
                Image = FormUtilities.ByteArrayToImage(itemImage) ?? Properties.Resources.CoffeeIcon,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Name = "itemPic",
                Location = new Point(6, 6),
                Size = new Size(77, 97),
                BorderRadius = 10,
                FillColor = Color.Transparent,
                UseTransparentBackground = true
            };

            lblItemName = new Label
            {
                BackColor = Color.Transparent,
                Font = new Font("Proxima Nova Lt", 10, FontStyle.Bold),
                ForeColor = Color.Black,
                Text = itemName ?? "Item name is empty.",
                AutoEllipsis = true,
                Name = "lblItemName",
                AutoSize = true,
                Location = new Point(90, 6),
                MaximumSize = new Size(184, 18)
            };

            btnDeleteOrder = new Guna.UI2.WinForms.Guna2Button
            {
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
                Name = "btnDeleteOrder",
                Location = new Point(276, 3),
                Size = new Size(22, 20),
                Animated = true,
                BorderRadius = 5,
                FillColor = Color.White,
                Image = Properties.Resources.delete_icon,
                ImageSize = new Size(15, 15),
                UseTransparentBackground = true,
                Tag = cartID
            };

            picSize = new Guna.UI2.WinForms.Guna2PictureBox
            {
                BackColor = Color.Transparent,
                Image = Properties.Resources.cup_icon,
                SizeMode = PictureBoxSizeMode.Zoom,
                Name = "picSize",
                Location = new Point(93, 27),
                Size = new Size(19, 23),
                UseTransparentBackground = true
            };

            lblSize = new Label
            {
                BackColor = Color.Transparent,
                Font = new Font("Proxima Nova Lt", 9, FontStyle.Bold),
                ForeColor = Color.Gray,
                Text = Size,
                AutoEllipsis = true,
                Name = "lblSize",
                AutoSize = true,
                Location = new Point(112, 31),
                MaximumSize = new Size(53, 15)
            };

            picSugar = new Guna.UI2.WinForms.Guna2PictureBox
            {
                BackColor = Color.Transparent,
                Image = Properties.Resources.sugar_icon,
                SizeMode = PictureBoxSizeMode.Zoom,
                Name = "picSugar",
                Location = new Point(164, 27),
                Size = new Size(19, 23),
                UseTransparentBackground = true
            };

            lblSugarLevel = new Label
            {
                BackColor = Color.Transparent,
                Font = new Font("Proxima Nova Lt", 9, FontStyle.Bold),
                ForeColor = Color.Gray,
                Text = Sugar,
                AutoEllipsis = true,
                Name = "lblSugarLevel",
                AutoSize = true,
                Location = new Point(183, 31),
                MaximumSize = new Size(32, 15)
            };

            picIce = new Guna.UI2.WinForms.Guna2PictureBox
            {
                BackColor = Color.Transparent,
                Image = Properties.Resources.ice_icon,
                SizeMode = PictureBoxSizeMode.Zoom,
                Name = "picIce",
                Location = new Point(213, 27),
                Size = new Size(19, 23),
                UseTransparentBackground = true
            };

            lblIceLevel = new Label
            {
                BackColor = Color.Transparent,
                Font = new Font("Proxima Nova Lt", 9, FontStyle.Bold),
                ForeColor = Color.Gray,
                Text = Ice,
                AutoEllipsis = true,
                Name = "lblIceLevel",
                AutoSize = true,
                Location = new Point(233, 31),
                MaximumSize = new Size(41, 15)
            };

            lblMultiplier = new Label
            {
                BackColor = Color.Transparent,
                Font = new Font("Proxima Nova Lt", 14, FontStyle.Bold),
                ForeColor = Color.Gray,
                Text = "x",
                Name = "lblMultiplier",
                AutoSize = true,
                Location = new Point(98, 66),
                Size = new Size(19, 23)
            };

            cmbQuantity = new Guna.UI2.WinForms.Guna2ComboBox
            {
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
                Font = new Font("Proxima Nova Lt", 10, FontStyle.Bold),
                ForeColor = Color.Gray,
                ItemHeight = 20,
                Name = "cmbQuantity",
                Location = new Point(115, 66),
                Size = new Size(65, 26),
                BorderThickness = 0,
                FillColor = Color.White,
                TextAlign = HorizontalAlignment.Left,
                TextOffset = new Point(0, 0),
                Tag = cartID
            };

            lblItemPrice = new Label
            {
                BackColor = Color.Transparent,
                Font = new Font("Proxima Nova Lt", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(65, 191, 80),
                RightToLeft = RightToLeft.Yes,
                Text = "₱" + Price,
                AutoEllipsis = true,
                Name = "lblItemPrice",
                AutoSize = false,
                Location = new Point(202, 75),
                MaximumSize = new Size(89, 17)
            };

            for (int i = 1; i <= maxQuantity - totalQuantityInCart + currentQuantity; i++)
            {
                cmbQuantity.Items.Add(i.ToString());
            }

            int selectedIndex = cmbQuantity.Items.IndexOf(currentQuantity.ToString());
            cmbQuantity.SelectedIndex = selectedIndex >= 0 ? selectedIndex : 0;

            btnDeleteOrder.Click += btnDeleteOrder_Click;
            cmbQuantity.SelectedIndexChanged += cmbQuantity_SelectedIndexChanged;

            pnlOrder.Controls.Add(itemPic);
            pnlOrder.Controls.Add(lblItemName);
            pnlOrder.Controls.Add(btnDeleteOrder);
            pnlOrder.Controls.Add(picSize);
            pnlOrder.Controls.Add(lblSize);
            pnlOrder.Controls.Add(picSugar);
            pnlOrder.Controls.Add(lblSugarLevel);
            pnlOrder.Controls.Add(picIce);
            pnlOrder.Controls.Add(lblIceLevel);
            pnlOrder.Controls.Add(lblMultiplier);
            pnlOrder.Controls.Add(cmbQuantity);
            pnlOrder.Controls.Add(lblItemPrice);

            return pnlOrder;
        }

        private void CopyAndPasteCartPanel(string cartID, byte[] itemImage, string itemName, string Size, string Sugar, string Ice, int maxQuantity, int Quantity, int totalQuantityInCart, string Price)
        {
            Guna.UI2.WinForms.Guna2Panel clonedPanel = GenerateCartPanel(cartID, itemImage, itemName, Size, Sugar, Ice, maxQuantity, Quantity, totalQuantityInCart, Price);
            flpOrders.Controls.Add(clonedPanel);
        }

        private void SetCart()
        {
            string cartQuery = $@"SELECT * FROM cart WHERE user_id = {this.userID} ORDER BY cart_id DESC";
            string inventoryQuery = "SELECT * FROM inventory";
            string itemQuery = "SELECT * FROM items";

            List<Cart> carts = DataAccess.GetCart(cartQuery);

            foreach (Cart cart in carts)
            {
                List<Inventory> inventories = DataAccess.GetInventory(inventoryQuery);

                if (inventories.Count > 0)
                {
                    Inventory selectedInventory = inventories.FirstOrDefault(inventory => inventory.InventoryId == cart.InventoryId);

                    if (selectedInventory != null)
                    {
                        List<Item> items = DataAccess.GetItems(itemQuery);

                        if (items.Count > 0)
                        {
                            Item selectedItem = items.FirstOrDefault(item => item.ItemId == selectedInventory.ItemId);

                            if (selectedItem != null)
                            {
                                // Check if the quantity in cart exceeds inventory available
                                if (cart.Quantity > selectedInventory.Quantity)
                                {
                                    // If so, delete the item from cart
                                    DataAccess.DeleteCart(cart.CartId);
                                    continue; // Skip processing this item
                                }

                                int totalQuantityInCart = carts.Where(c => c.InventoryId == cart.InventoryId).Sum(c => c.Quantity);

                                CopyAndPasteCartPanel(cart.CartId.ToString(), selectedItem.ItemImage, selectedItem.ItemName, cart.Size, cart.Sugar, cart.Ice, selectedInventory.Quantity, cart.Quantity, totalQuantityInCart, selectedItem.BasePrice.ToString());
                            }
                        }
                    }
                    else
                    {
                        DataAccess.DeleteCart(cart.CartId);
                        continue;
                    }
                }
            }
        }

        private void UpdateItemInCart(int cartID, int quantity)
        {
            DataAccess.UpdateCart(cartID, quantity);
            flpOrders.Controls.Clear();
            SetCart();
            SetAmount();
        }

        private void DisplayCartCount(Guna.UI2.WinForms.Guna2Button button, Label label)
        {
            label.Parent = button;
            label.Location = new Point(8, 16);
        }

        private void SetCartCount()
        {
            int cartCount = DataAccess.GetEntityCount("cart", this.userID, "user_id");

            if (cartCount > 0)
            {
                lblCartCount.Text = cartCount.ToString();
            }
            else
            {
                lblCartCount.Text = "0";
            }
        }

        private void SetAmount()
        {
            string cartQuery = $@"SELECT * FROM cart WHERE user_id = {this.userID} ORDER BY cart_id DESC";
            string inventoryQuery = "SELECT * FROM inventory";
            string itemQuery = "SELECT * FROM items";

            List<Cart> carts = DataAccess.GetCart(cartQuery);
            decimal totalAmount = 0;

            foreach (Cart cart in carts)
            {
                List<Inventory> inventories = DataAccess.GetInventory(inventoryQuery);

                if (inventories.Count > 0)
                {
                    Inventory selectedInventory = inventories.FirstOrDefault(inventory => inventory.InventoryId == cart.InventoryId);

                    if (selectedInventory != null)
                    {
                        List<Item> items = DataAccess.GetItems(itemQuery);

                        if (items.Count > 0)
                        {
                            Item selectedItem = items.FirstOrDefault(item => item.ItemId == selectedInventory.ItemId);

                            if (selectedItem != null)
                            {
                                // Calculate the total for each item by multiplying its price and quantity
                                decimal itemTotal = selectedItem.BasePrice * cart.Quantity;

                                // Add the item's total to the overall total amount
                                totalAmount += itemTotal;
                            }
                        }
                    }
                }
            }

            // Set the total amount to lblSubTotal
            lblSubTotal.Text = totalAmount.ToString("₱#,##0.00");
            lblTotal.Text = totalAmount.ToString("₱#,##0.00");
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

        private void setUserRoleGreeting()
        {
            if (userRole == "user")
            {
                lblRole.Text = "Cashier on Brew Bliss Café";
            }
            else
            {
                lblRole.Text = "Admin on Brew Bliss Café";
            }
        }

        private void POSForm_Load(object sender, EventArgs e)
        {
            FormUtilities.setUserProfile(userPic, lblUsername, this.username);
            setUserRoleGreeting();
            GetUserID();

            pnlMenu.Width = 1063;
            pnlCart.Width = 0;
            pnlCart.Location = new Point(1086,79);
            pnlCart.BringToFront();
            DisplayCartCount(btnOpen, lblCartCount);
            btnOpen.Location = new Point(1008, 15);
            btnOpen.Size = new Size(42, 79);

            flpCategories.Controls.Clear();
            flpItems.Controls.Clear();
            flpOrders.Controls.Clear();
            SetCategories();
            SetItemsFromInventory();
            SetCart();
            SetCartCount();
            SetAmount();

            if (flpOrders.Controls.Count <= 0)
            {
                flpOrders.Controls.Add(pnlNoOrder);
                btnCheckout.Text = "Add order";
            }
        }

        private void lblCartCount_MouseClick(object sender, MouseEventArgs e)
        {
            ExpandPanel();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            ExpandPanel();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            CollapsePanel();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            flpItems.Controls.Clear();
            if (this.categoryID != null)
            {
                SetItemsFromInventory(this.categoryID);
            }
            else
            {
                SetItemsFromInventory();
            }
        }

        private void pnlCategory_MouseClick(object sender, MouseEventArgs e)
        {
            if (sender is System.Windows.Forms.Control control)
            {
                System.Windows.Forms.Control parentPanel = control.Parent;
                if (parentPanel is Panel)
                {
                    pnlCategory_Click(parentPanel, EventArgs.Empty);
                }
            }
        }

        private void pnlCategory_Click(object sender, EventArgs e)
        {
            if (pnlMainCategory.FillColor == selectedPanelColor)
            {
                pnlMainCategory.FillColor = defaultPanelColor;
                lblMainCategoryName.ForeColor = Color.Gray;
            }

            if (sender is Guna.UI2.WinForms.Guna2Panel clickedPanel)
            {
                this.categoryID = clickedPanel.Tag?.ToString();

                int categoryCount = DataAccess.GetCategoryItemCount(Convert.ToInt32(categoryID));
                int allItemsCount = DataAccess.GetEntityCount("items");
                string categoryName = "";

                foreach (System.Windows.Forms.Control control in clickedPanel.Controls)
                {
                    if (control is Label lblCategoryName)
                    {
                        categoryName = lblCategoryName.Text;
                        break;
                    }
                }

                if (this.categoryID == null)
                {
                    flpItems.Controls.Clear();
                    lblItemCount.Text = allItemsCount.ToString() + " - All Items";
                    SetItemsFromInventory();
                }
                else
                {
                    flpItems.Controls.Clear();
                    lblItemCount.Text = categoryCount.ToString() + " - All " + categoryName;
                    SetItemsFromInventory(categoryID);
                }

                foreach (System.Windows.Forms.Control control in clickedPanel.Controls)
                {
                    if (control is Label lblCategoryName)
                    {
                        lblCategoryName.ForeColor = Color.White;
                    }
                }

                if (selectedPanel != null && selectedPanel != clickedPanel)
                {
                    foreach (System.Windows.Forms.Control control in selectedPanel.Controls)
                    {
                        if (control is Label lblCategoryName)
                        {
                            lblCategoryName.ForeColor = Color.Gray;
                            break;
                        }
                    }

                    ResetPanelColor(selectedPanel);
                }

                UpdatePanelColor(clickedPanel);
                selectedPanel = clickedPanel;
            }
        }

        private void SetBillingControls(Guna.UI2.WinForms.Guna2Panel parentPanel, bool isFirstSet, bool isSecondSet, bool isThirdSet, string quantityCount = null)
        {
            // Find the button with the specified name in parentPanel's controls
            var btnAddToBilling = parentPanel.Controls
                .OfType<Guna.UI2.WinForms.Guna2Button>()
                .FirstOrDefault(control => control.Name == "btnAddToBilling");

            // Check if the button was found
            if (btnAddToBilling != null)
            {
                bool shouldShowButton = isFirstSet && isSecondSet && isThirdSet;
                bool shouldHideButton = quantityCount != null && Convert.ToInt32(quantityCount) == 0;

                // Determine panel size based on conditions
                parentPanel.Size = new Size(243, shouldShowButton ? 289 : 230);

                // Set button visibility based on conditions
                if (shouldShowButton && !shouldHideButton)
                {
                    btnAddToBilling.Visible = true;
                }
                else
                {
                    btnAddToBilling.Visible = false;
                    parentPanel.Size = new Size(243, 230);
                }
            }
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            if (sender is Guna.UI2.WinForms.Guna2Button btnMinus)
            {
                foreach (System.Windows.Forms.Control control in btnMinus.Parent.Controls)
                {
                    if (control is Guna.UI2.WinForms.Guna2TextBox txtQuantity)
                    {
                        if (int.TryParse(txtQuantity.Text, out int quantity) && quantity > 0)
                        {
                            quantity--;
                            txtQuantity.Text = quantity.ToString();
                        }
                    }
                }
            }
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            if (!(sender is Guna.UI2.WinForms.Guna2TextBox txtQuantity)) return;

            Guna.UI2.WinForms.Guna2Panel parentPanel = txtQuantity.Parent as Guna.UI2.WinForms.Guna2Panel;
            string inventoryID = txtQuantity.Tag?.ToString();

            if (string.IsNullOrEmpty(txtQuantity.Text))
            {
                txtQuantity.Text = "0";
            }

            bool isAnySizeSelected = parentPanel.Controls.OfType<Guna.UI2.WinForms.Guna2Button>()
                .Any(control => control is Guna.UI2.WinForms.Guna2Button btnSize &&
                                new[] { "btnSmall", "btnMedium", "btnLarge" }.Contains(btnSize.Name) &&
                                btnSize.BorderThickness == 1);

            bool isAnySugarSelected = parentPanel.Controls.OfType<Guna.UI2.WinForms.Guna2Button>()
                .Any(control => control is Guna.UI2.WinForms.Guna2Button btnSugar &&
                                new[] { "btnSugarThirty", "btnSugarFifty", "btnSugarSeventy" }.Contains(btnSugar.Name) &&
                                btnSugar.BorderThickness == 1);

            bool isAnyIceSelected = parentPanel.Controls.OfType<Guna.UI2.WinForms.Guna2Button>()
                .Any(control => control is Guna.UI2.WinForms.Guna2Button btnIce &&
                                new[] { "btnIceThirty", "btnIceFifty", "btnIceSeventy" }.Contains(btnIce.Name) &&
                                btnIce.BorderThickness == 1);

            SetBillingControls(parentPanel, isAnySizeSelected, isAnySugarSelected, isAnyIceSelected, txtQuantity.Text);
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            if (sender is Guna.UI2.WinForms.Guna2Button btnPlus)
            {
                int maxQuantity = Convert.ToInt32(btnPlus.Tag?.ToString());

                foreach (System.Windows.Forms.Control control in btnPlus.Parent.Controls)
                {
                    if (control is Guna.UI2.WinForms.Guna2TextBox txtQuantity && txtQuantity.Name == "txtQuantity")
                    {
                        int updatedMaxQuantity = FormUtilities.UpdateMaxQuantity(Convert.ToInt32(txtQuantity.Tag), this.userID, maxQuantity);

                        if (int.TryParse(txtQuantity.Text, out int quantity))
                        {
                            if (quantity < updatedMaxQuantity)
                            {
                                quantity++;
                                txtQuantity.Text = quantity.ToString();
                            }
                        }
                    }
                }
            }
        }

        private void SetSizeButtons(Guna.UI2.WinForms.Guna2Panel parentPanel, Guna.UI2.WinForms.Guna2Button selectedButton, string btnOne, string btnTwo, string btnThree)
        {
            List<Guna.UI2.WinForms.Guna2Button> allButtons = parentPanel.Controls
                .OfType<Guna.UI2.WinForms.Guna2Button>()
                .Where(btn => new[] { btnOne, btnTwo, btnThree }.Contains(btn.Name))
                .ToList();

            foreach (var eachButton in allButtons)
            {
                if (eachButton != selectedButton)
                {
                    eachButton.BorderThickness = 0;
                }
            }

            selectedButton.BorderThickness = 1;
        }

        private void btnSize_Click(object sender, EventArgs e)
        {
            if (!(sender is Guna.UI2.WinForms.Guna2Button button)) return;

            Guna.UI2.WinForms.Guna2Panel parentPanel = button.Parent as Guna.UI2.WinForms.Guna2Panel;
            int maxQuantity = 0;
            bool isAmountSet = false;
            bool isAnySugarSelected = false;
            bool isAnyIceSelected = false;

            foreach (System.Windows.Forms.Control control in parentPanel.Controls)
            {
                if (control is Guna.UI2.WinForms.Guna2Button btnPlus && btnPlus.Name == "btnPlus")
                {
                    maxQuantity = Convert.ToInt32(btnPlus.Tag);
                    break;
                }
            }

            foreach (System.Windows.Forms.Control control in parentPanel.Controls)
            {
                if (control is Guna.UI2.WinForms.Guna2TextBox txtQuantity && txtQuantity.Name == "txtQuantity")
                {
                    if (Convert.ToInt32(txtQuantity.Text) > 0)
                    {
                        isAmountSet = true;
                    }
                }
                else if (control is Guna.UI2.WinForms.Guna2Button btnSugar && new[] { "btnSugarThirty", "btnSugarFifty", "btnSugarSeventy" }.Contains(btnSugar.Name))
                {
                    if (btnSugar.BorderThickness == 1)
                    {
                        isAnySugarSelected = true;
                    }
                }
                else if (control is Guna.UI2.WinForms.Guna2Button btnIce && new[] { "btnIceThirty", "btnIceFifty", "btnIceSeventy" }.Contains(btnIce.Name))
                {
                    if (btnIce.BorderThickness == 1)
                    {
                        isAnyIceSelected = true;
                    }
                }
            }

            if (maxQuantity > 0)
            {
                SetSizeButtons(parentPanel, button, "btnSmall", "btnMedium", "btnLarge");
            }

            SetBillingControls(parentPanel, isAmountSet, isAnySugarSelected, isAnyIceSelected);
        }

        private void btnSugar_Click(object sender, EventArgs e)
        {
            if (!(sender is Guna.UI2.WinForms.Guna2Button button)) return;

            Guna.UI2.WinForms.Guna2Panel parentPanel = button.Parent as Guna.UI2.WinForms.Guna2Panel;
            int maxQuantity = 0;
            bool isAmountSet = false;
            bool isAnySizeSelected = false;
            bool isAnyIceSelected = false;

            foreach (System.Windows.Forms.Control control in parentPanel.Controls)
            {
                if (control is Guna.UI2.WinForms.Guna2Button btnPlus && btnPlus.Name == "btnPlus")
                {
                    maxQuantity = Convert.ToInt32(btnPlus.Tag);
                    break;
                }
            }

            foreach (System.Windows.Forms.Control control in parentPanel.Controls)
            {
                if (control is Guna.UI2.WinForms.Guna2TextBox txtQuantity && txtQuantity.Name == "txtQuantity")
                {
                    if (Convert.ToInt32(txtQuantity.Text) > 0)
                    {
                        isAmountSet = true;
                    }
                }
                else if (control is Guna.UI2.WinForms.Guna2Button btnSize && new[] { "btnSmall", "btnMedium", "btnLarge" }.Contains(btnSize.Name))
                {
                    if (btnSize.BorderThickness == 1)
                    {
                        isAnySizeSelected = true;
                    }
                }
                else if (control is Guna.UI2.WinForms.Guna2Button btnIce && new[] { "btnIceThirty", "btnIceFifty", "btnIceSeventy" }.Contains(btnIce.Name))
                {
                    if (btnIce.BorderThickness == 1)
                    {
                        isAnyIceSelected = true;
                    }
                }
            }

            if (maxQuantity > 0)
            {
                SetSizeButtons(parentPanel, button, "btnSugarThirty", "btnSugarFifty", "btnSugarSeventy");
            }

            SetBillingControls(parentPanel, isAmountSet, isAnySizeSelected, isAnyIceSelected);
        }

        private void btnIce_Click(object sender, EventArgs e)
        {
            if (!(sender is Guna.UI2.WinForms.Guna2Button button)) return;

            Guna.UI2.WinForms.Guna2Panel parentPanel = button.Parent as Guna.UI2.WinForms.Guna2Panel;
            int maxQuantity = 0;
            bool isAmountSet = false;
            bool isAnySizeSelected = false;
            bool isAnySugarSelected = false;

            foreach (System.Windows.Forms.Control control in parentPanel.Controls)
            {
                if (control is Guna.UI2.WinForms.Guna2Button btnPlus && btnPlus.Name == "btnPlus")
                {
                    maxQuantity = Convert.ToInt32(btnPlus.Tag);
                    break;
                }
            }

            foreach (System.Windows.Forms.Control control in parentPanel.Controls)
            {
                if (control is Guna.UI2.WinForms.Guna2TextBox txtQuantity && txtQuantity.Name == "txtQuantity")
                {
                    if (Convert.ToInt32(txtQuantity.Text) > 0)
                    {
                        isAmountSet = true;
                    }
                }
                else if (control is Guna.UI2.WinForms.Guna2Button btnSize && new[] { "btnSmall", "btnMedium", "btnLarge" }.Contains(btnSize.Name))
                {
                    if (btnSize.BorderThickness == 1)
                    {
                        isAnySizeSelected = true;
                    }
                }
                else if (control is Guna.UI2.WinForms.Guna2Button btnSugar && new[] { "btnSugarThirty", "btnSugarFifty", "btnSugarSeventy" }.Contains(btnSugar.Name))
                {
                    if (btnSugar.BorderThickness == 1)
                    {
                        isAnySugarSelected = true;
                    }
                }
            }

            if (maxQuantity > 0)
            {
                SetSizeButtons(parentPanel, button, "btnIceThirty", "btnIceFifty", "btnIceSeventy");
            }

            SetBillingControls(parentPanel, isAmountSet, isAnySizeSelected, isAnySugarSelected);
        }

        private void btnAddToBilling_Click(object sender, EventArgs e)
        {
            if (!(sender is Guna.UI2.WinForms.Guna2Button button)) return;

            Guna.UI2.WinForms.Guna2Panel parentPanel = button.Parent as Guna.UI2.WinForms.Guna2Panel;

            string inventoryID = button.Tag.ToString();
            int maxQuantity = 0;
            string itemQuantity = null;
            string selectedSize = null;
            string selectedSugar = null;
            string selectedIce = null;

            foreach (System.Windows.Forms.Control control in parentPanel.Controls)
            {
                if (control is Guna.UI2.WinForms.Guna2Button btnPlus && btnPlus.Name == "btnPlus")
                {
                    maxQuantity = Convert.ToInt32(btnPlus.Tag);
                }
                else if (control is Guna.UI2.WinForms.Guna2TextBox txtQuantity && txtQuantity.Name == "txtQuantity")
                {
                    itemQuantity = txtQuantity.Text;
                    txtQuantity.Text = "0";
                }
                else if (control is Guna.UI2.WinForms.Guna2Button sizeButton &&
                    new[] { "btnSmall", "btnMedium", "btnLarge" }.Contains(sizeButton.Name) &&
                    sizeButton.BorderThickness == 1)
                {
                    selectedSize = sizeButton.Tag.ToString();
                    sizeButton.BorderThickness = 0;
                }
                else if (control is Guna.UI2.WinForms.Guna2Button sugarButton &&
                    new[] { "btnSugarThirty", "btnSugarFifty", "btnSugarSeventy" }.Contains(sugarButton.Name) &&
                    sugarButton.BorderThickness == 1)
                {
                    selectedSugar = sugarButton.Tag.ToString();
                    sugarButton.BorderThickness = 0;
                }
                else if (control is Guna.UI2.WinForms.Guna2Button iceButton &&
                    new[] { "btnIceThirty", "btnIceFifty", "btnIceSeventy" }.Contains(iceButton.Name) &&
                    iceButton.BorderThickness == 1)
                {
                    selectedIce = iceButton.Tag.ToString();
                    iceButton.BorderThickness = 0;
                }
            }

            int updatedMaxQuantity = FormUtilities.UpdateMaxQuantity(Convert.ToInt32(inventoryID), this.userID, maxQuantity);

            // Return the method if the input quantity exceeded the updatedMaxQuantity
            if (updatedMaxQuantity.ToString() != "" && Convert.ToInt32(itemQuantity) > updatedMaxQuantity)
            {
                return;
            }

            if (selectedSize != null && selectedSugar != null && selectedIce != null)
            {
                AddToCart(inventoryID, itemQuantity, selectedSize, selectedSugar, selectedIce);

                parentPanel.Size = new Size(243, 230);
                foreach (System.Windows.Forms.Control control in parentPanel.Controls)
                {
                    if (control is Guna.UI2.WinForms.Guna2Button btnAddToBilling && btnAddToBilling.Name == "btnAddToBilling")
                    {
                        btnAddToBilling.Visible = false;
                        break;
                    }
                }
            }
        }

        private void btnDeleteOrder_Click(object sender, EventArgs e)
        {
            if (sender is Guna.UI2.WinForms.Guna2Button btnDelete && btnDelete.Name == "btnDeleteOrder")
            {
                int cartID = Convert.ToInt32(btnDelete.Tag?.ToString());

                DataAccess.DeleteCart(cartID);
                SetCartCount();
                flpOrders.Controls.Clear();
                SetCart();
                SetAmount();
            }

            if (flpOrders.Controls.Count <= 0)
            {
                flpOrders.Controls.Add(pnlNoOrder);
                btnCheckout.Text = "Add order";
                SetCartCount();
            }
        }

        private void cmbQuantity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is Guna.UI2.WinForms.Guna2ComboBox cmbQuantity)
            {
                int cartID = Convert.ToInt32(cmbQuantity.Tag?.ToString());
                int quantity = Convert.ToInt32(cmbQuantity.SelectedItem.ToString());

                UpdateItemInCart(cartID, quantity);
            }
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            bool containsNoOrderPanel = flpOrders.Controls.ContainsKey("pnlNoOrder");

            if (!containsNoOrderPanel)
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

                dashboard.loadForm(new CheckoutForm(dashboardFormRef, username, userRole));

                this.Close();
            }
        }

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (sender is Guna.UI2.WinForms.Guna2TextBox txtBox)
            {
                int inventoryID = Convert.ToInt32(txtBox.Tag);
                int maxQuantity = 0;
                foreach (System.Windows.Forms.Control control in txtBox.Parent.Controls)
                {
                    if (control is Guna.UI2.WinForms.Guna2Button btnPlus && btnPlus.Name == "btnPlus")
                    {
                        maxQuantity = Convert.ToInt32(btnPlus.Tag);
                        break;
                    }
                }

                // Calculate the updated maxQuantity
                int updatedMaxQuantity = FormUtilities.UpdateMaxQuantity(inventoryID, this.userID, maxQuantity);

                // Allowing only digits and handling other cases
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true; // Disallow non-digit characters
                }
                else if (e.KeyChar != '\b' && int.TryParse(txtBox.Text + e.KeyChar, out int typedValue))
                {
                    // Check if the typed value exceeds maxQuantity
                    if (typedValue > updatedMaxQuantity)
                    {
                        // Automatically change the input to maxQuantity if it's exceeded
                        txtBox.Text = updatedMaxQuantity.ToString();
                        e.Handled = true;
                    }
                    else if (txtBox.Text.Length == 0 && e.KeyChar == '0')
                    {
                        // Disallow '0' as the first character
                        e.Handled = true;
                    }
                    else if (txtBox.Text.Length == 1 && txtBox.Text[0] == '0' && e.KeyChar != '\b')
                    {
                        // Replace '0' with the entered digit
                        txtBox.Text = e.KeyChar.ToString();
                        txtBox.Select(txtBox.Text.Length, 0); // Move cursor to the end
                        e.Handled = true;
                    }
                }
            }
        }

        private Guna.UI2.WinForms.Guna2Panel pnlCategory = new Guna.UI2.WinForms.Guna2Panel();
        private Label lblCategoryName = new Label();
        private Guna.UI2.WinForms.Guna2Panel selectedPanel = null;
        private Guna.UI2.WinForms.Guna2Panel pnlItem = new Guna.UI2.WinForms.Guna2Panel();
        private Guna.UI2.WinForms.Guna2PictureBox itemPic = new Guna.UI2.WinForms.Guna2PictureBox();
        private Guna.UI2.WinForms.Guna2PictureBox noStockPic = new Guna.UI2.WinForms.Guna2PictureBox();
        private Label lblItemName = new Label();
        private Label lblItemDesc = new Label();
        private Label lblItemPrice = new Label();
        private Label lblQuantity = new Label();
        private Guna.UI2.WinForms.Guna2Button btnMinus = new Guna.UI2.WinForms.Guna2Button();
        private Guna.UI2.WinForms.Guna2TextBox txtQuantity = new Guna.UI2.WinForms.Guna2TextBox();
        private Guna.UI2.WinForms.Guna2Button btnPlus = new Guna.UI2.WinForms.Guna2Button();
        private Label lblSize = new Label();
        private Guna.UI2.WinForms.Guna2Button btnSmall = new Guna.UI2.WinForms.Guna2Button();
        private Guna.UI2.WinForms.Guna2Button btnMedium = new Guna.UI2.WinForms.Guna2Button();
        private Guna.UI2.WinForms.Guna2Button btnLarge = new Guna.UI2.WinForms.Guna2Button();
        private Label lblSugarLevel = new Label();
        private Guna.UI2.WinForms.Guna2Button btnSugarThirty = new Guna.UI2.WinForms.Guna2Button();
        private Guna.UI2.WinForms.Guna2Button btnSugarFifty = new Guna.UI2.WinForms.Guna2Button();
        private Guna.UI2.WinForms.Guna2Button btnSugarSeventy = new Guna.UI2.WinForms.Guna2Button();
        private Label lblIceLevel = new Label();
        private Guna.UI2.WinForms.Guna2Button btnIceThirty = new Guna.UI2.WinForms.Guna2Button();
        private Guna.UI2.WinForms.Guna2Button btnIceFifty = new Guna.UI2.WinForms.Guna2Button();
        private Guna.UI2.WinForms.Guna2Button btnIceSeventy = new Guna.UI2.WinForms.Guna2Button();
        private Guna.UI2.WinForms.Guna2Button btnAddToBilling = new Guna.UI2.WinForms.Guna2Button();
        private Guna.UI2.WinForms.Guna2Panel pnlOrder = new Guna.UI2.WinForms.Guna2Panel();
        private Guna.UI2.WinForms.Guna2Button btnDeleteOrder = new Guna.UI2.WinForms.Guna2Button();
        private Guna.UI2.WinForms.Guna2PictureBox picSize = new Guna.UI2.WinForms.Guna2PictureBox();
        private Guna.UI2.WinForms.Guna2PictureBox picSugar = new Guna.UI2.WinForms.Guna2PictureBox();
        private Guna.UI2.WinForms.Guna2PictureBox picIce = new Guna.UI2.WinForms.Guna2PictureBox();
        private Label lblMultiplier = new Label();
        private Guna.UI2.WinForms.Guna2ComboBox cmbQuantity = new Guna.UI2.WinForms.Guna2ComboBox();
    }
}
