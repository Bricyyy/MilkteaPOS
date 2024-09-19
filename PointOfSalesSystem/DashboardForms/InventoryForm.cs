using Guna.UI2.WinForms.Suite;
using MySql.Data.MySqlClient;
using PointOfSalesSystem.CustomObjects;
using PointOfSalesSystem.DashboardMenu;
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
using ZstdSharp.Unsafe;

namespace PointOfSalesSystem
{
    public partial class InventoryForm : Form
    {
        private readonly Dashboard dashboardFormRef;
        private readonly AdminMenu adminmenuFormRef;

        private readonly Color defaultPanelColor = Color.DimGray;
        private readonly Color selectedPanelColor = Color.FromArgb(242, 106, 36);

        private readonly string username;

        private string categoryID;
        private const int AnimationDuration = 100;

        public InventoryForm(Dashboard dashboardFormRef, AdminMenu adminmenuFormRef, string username)
        {
            InitializeComponent();

            this.dashboardFormRef = dashboardFormRef;
            this.adminmenuFormRef = adminmenuFormRef;
            this.username = username;
        }

        private async void CollapsePanel()
        {
            int collapseWidth = pnlCategoriesTitle.Width; // Width to collapse to

            // Animation for collapsing pnlCategoriesTitle and flpCategories while expanding pnlItemsTitle and flpItems
            while (pnlCategoriesTitle.Width > 0)
            {
                // Collapse pnlCategoriesTitle and flpCategories
                pnlCategoriesTitle.Width -= (int)(collapseWidth * 0.05); // Adjust the collapse rate as needed
                flpCategories.Width -= (int)(collapseWidth * 0.05);

                // Expand pnlItemsTitle and flpItems
                pnlItemsTitle.Width += (int)(collapseWidth * 0.05);
                flpItems.Width += (int)(collapseWidth * 0.05);

                // Adjust the expanding panel's position to match the collapsing panel's original position
                pnlItemsTitle.Left -= (int)(collapseWidth * 0.05);
                flpItems.Left -= (int)(collapseWidth * 0.05);

                await Task.Delay(AnimationDuration / 20);
            }

            pnlCategoriesTitle.Visible = false; // Hide the collapsed panels
            flpCategories.Visible = false;

            // Animation complete, adjust the expanded panel's position to match the original position of the collapsing panel
            pnlItemsTitle.Left = pnlCategoriesTitle.Left;
            flpItems.Left = flpCategories.Left;

            btnExpand.Visible = true;
            lblItemTitle.Location = new Point(100, 20);
            txtSearch.Location = new Point(774, 12);
        }

        private async void ExpandPanel()
        {
            int originalWidth = 290; // Original width of pnlCategoriesTitle
            int originalItemsWidth = 763;
            int originalPnlItemsTitleX = 309; // Default X position of pnlItemsTitle
            int originalFlpItemsX = 309; // Default X position of flpItems

            // Show the collapsed panels
            pnlCategoriesTitle.Visible = true;
            flpCategories.Visible = true;

            int steps = 20; // Define the number of steps for the animation
            int widthChangePerStep = (originalWidth - pnlCategoriesTitle.Width) / steps;
            int xChangePerStep = (originalPnlItemsTitleX - pnlItemsTitle.Left) / steps;

            for (int i = 0; i < steps; i++)
            {
                pnlCategoriesTitle.Width += widthChangePerStep;
                flpCategories.Width += widthChangePerStep;

                pnlItemsTitle.Width -= widthChangePerStep;
                flpItems.Width -= widthChangePerStep;

                pnlItemsTitle.Left += xChangePerStep;
                flpItems.Left += xChangePerStep;

                await Task.Delay(AnimationDuration / steps);
            }

            // Ensure the final width matches the original width
            pnlCategoriesTitle.Width = originalWidth;
            flpCategories.Width = originalWidth;

            // Set pnlItemsTitle and flpItems back to their default sizes and locations
            pnlItemsTitle.Width = originalItemsWidth;
            flpItems.Width = originalItemsWidth;
            pnlItemsTitle.Left = originalPnlItemsTitleX;
            flpItems.Left = originalFlpItemsX;

            btnExpand.Visible = false;
            lblItemTitle.Location = new Point(21, 20);
            txtSearch.Location = new Point(470, 13);
        }

        private void ResetPanelColor(Guna.UI2.WinForms.Guna2Panel panel)
        {
            panel.FillColor = defaultPanelColor;
        }

        private void UpdatePanelColor(Guna.UI2.WinForms.Guna2Panel panel)
        {
            panel.FillColor = selectedPanelColor;
        }

        private Guna.UI2.WinForms.Guna2Panel GenerateCategoryPanel(string categoryID, string categoryName, string categoryCount)
        {
            pnlCategories = new Guna.UI2.WinForms.Guna2Panel
            {
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
                Size = new Size(120, 84),
                BorderColor = Color.Transparent,
                BorderRadius = 15,
                FillColor = Color.DimGray,
                Tag = categoryID
            };

            lblCategoryName = new Label
            {
                Font = new Font("Proxima Nova Lt", 10, FontStyle.Bold),
                ForeColor = Color.White,
                Text = categoryName,
                Location = new Point(11, 11),
                AutoEllipsis = true,
                AutoSize = true,
                MaximumSize = new Size(96, 34)
            };

            lblCategoryCount = new Label
            {
                Font = new Font("Proxima Nova Lt", 8, FontStyle.Bold),
                ForeColor = Color.White,
                Text = categoryCount + " items",
                Location = new Point(11, 61)
            };

            picArrow = new Guna.UI2.WinForms.Guna2PictureBox
            {
                Image = Properties.Resources.right_arrow_icon,
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(88, 58),
                Size = new Size(20,20),
                UseTransparentBackground = true,
                Visible = false
            };

            pnlCategories.Controls.Add(picArrow);
            pnlCategories.Controls.Add(lblCategoryName);
            pnlCategories.Controls.Add(lblCategoryCount);

            picArrow.MouseClick += pnlCategories_MouseClick;
            lblCategoryName.MouseClick += pnlCategories_MouseClick;
            lblCategoryCount.MouseClick += pnlCategories_MouseClick;

            pnlCategories.Click += pnlCategories_Click;

            return pnlCategories;
        }

        private void CopyAndPasteCategoryPanel(string categoryID, string categoryName, string categoryCount)
        {
            Guna.UI2.WinForms.Guna2Panel clonedPanel = GenerateCategoryPanel(categoryID, categoryName, categoryCount);
            flpCategories.Controls.Add(clonedPanel);
        }

        private void AddMainCategoryPanel()
        {
            int allItemsCount = DataAccess.GetEntityCount("inventory");

            lblMainCategoryCount.Text = allItemsCount.ToString() + " items";

            picMainArrow.MouseClick += pnlCategories_MouseClick;
            lblMainCategoryName.MouseClick += pnlCategories_MouseClick;
            lblMainCategoryCount.MouseClick += pnlCategories_MouseClick;

            pnlMainCategory.Click += pnlCategories_Click;

            flpCategories.Controls.Add(pnlMainCategory);
        }

        private void SetCategories()
        {
            AddMainCategoryPanel();

            string categoryQuery = "SELECT Category_Id, Category_Name FROM item_category ORDER BY Category_Name ASC";
            List<Category> categories = DataAccess.GetCategories(categoryQuery);

            foreach (Category category in categories)
            {
                int categoryCount = DataAccess.GetCategoryItemCount(category.CategoryId);

                CopyAndPasteCategoryPanel(category.CategoryId.ToString(), category.CategoryName, categoryCount.ToString());
            }
        }

        private Guna.UI2.WinForms.Guna2Panel GenerateItemsPanel(string inventoryID, byte[] itemImage, string itemName, decimal basePrice, string quantity)
        {
            pnlItems = new Guna.UI2.WinForms.Guna2Panel
            {
                BackColor = Color.Transparent,
                Size = new Size(237, 101),
                BorderColor = Color.FromArgb(30, 57, 50),
                BorderRadius = 15,
                BorderThickness = 1,
                FillColor = Color.White,
            };

            picItem = new Guna.UI2.WinForms.Guna2PictureBox
            {
                Image = FormUtilities.ByteArrayToImage(itemImage) ?? Properties.Resources.CoffeeIcon,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Location = new Point(0, 0),
                Size = new Size(92, 101),
                BorderRadius = 15,
                CustomizableEdges = new Guna.UI2.WinForms.Suite.CustomizableEdges
                {
                    BottomLeft = true,
                    BottomRight = false,
                    TopLeft = true,
                    TopRight = false
                },
                FillColor = Color.Transparent,
                UseTransparentBackground = true
            };

            lblItemName = new Label
            {
                Font = new Font("Proxima Nova Lt", 10, FontStyle.Bold),
                ForeColor = Color.Black,
                Text = itemName ?? "Item name is empty.",
                TextAlign = ContentAlignment.TopLeft,
                Location = new Point(102, 7),
                AutoEllipsis = true,
                AutoSize = true,
                MaximumSize = new Size(115, 36)
            };

            lblItemPriceCount = new Label
            {
                Font = new Font("Proxima Nova Rg", 9, FontStyle.Regular),
                ForeColor = Color.Gray,
                Text = "₱" + basePrice.ToString() + " • " + quantity + " Available",
                Name = "lblItemPriceCount",
                Location = new Point(102, 50),
                AutoEllipsis = true,
                AutoSize = true,
                MaximumSize = new Size(135, 16)
            };

            btnMinus = new Guna.UI2.WinForms.Guna2Button
            {
                Cursor = Cursors.Hand,
                Location = new Point(103, 73),
                Size = new Size(30, 20),
                Animated = true,
                BorderRadius = 10,
                FillColor = Color.FromArgb(221, 220, 226),
                Image = Properties.Resources.minus_icon,
                ImageSize = new Size(15, 15),
                UseTransparentBackground = true
            };

            txtQuantity = new Guna.UI2.WinForms.Guna2TextBox()
            {
                Font = new Font("Proxima Nova Rg", 8, FontStyle.Regular),
                Location = new Point(139, 73),
                Size = new Size(50, 20),
                TextAlign = HorizontalAlignment.Center,
                Animated = true,
                BorderRadius = 5,
                Text = quantity,
                ForeColor = Color.Black,
                Tag = inventoryID
            };

            btnPlus = new Guna.UI2.WinForms.Guna2Button
            {
                Cursor = Cursors.Hand,
                Location = new Point(195, 73),
                Size = new Size(30, 20),
                Animated = true,
                BorderRadius = 10,
                FillColor = Color.FromArgb(221, 220, 226),
                Image = Properties.Resources.plus_icon,
                ImageSize = new Size(15, 15),
                UseTransparentBackground = true
            };

            btnMinus.Click += btnMinus_Click;
            txtQuantity.TextChanged += txtQuantity_TextChanged;
            txtQuantity.KeyPress += txtQuantity_KeyPress;
            btnPlus.Click += btnPlus_Click;

            pnlItems.Controls.Add(picItem);
            pnlItems.Controls.Add(lblItemName);
            pnlItems.Controls.Add(lblItemPriceCount);
            pnlItems.Controls.Add(btnMinus);
            pnlItems.Controls.Add(txtQuantity);
            pnlItems.Controls.Add(btnPlus);

            return pnlItems;
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
                                        ORDER BY iv.Quantity ASC, i.Item_Name ASC";
                }
                else
                {
                    inventoryQuery = $@"SELECT iv.inventory_id, i.Item_Id, i.Item_Image, i.Item_Name, i.Category_Id, i.Base_Price, iv.Quantity
                                        FROM inventory iv
                                        INNER JOIN items i ON iv.Item_Id = i.Item_Id
                                        WHERE i.Category_Id = {categoryID} AND i.Item_Name LIKE CONCAT('%', '{searchText}', '%')
                                        ORDER BY iv.Quantity ASC, i.Item_Name ASC";
                }
            }
            else
            {
                if (txtSearch.Text == "")
                {
                    inventoryQuery = $@"SELECT iv.inventory_id, i.Item_Id, i.Item_Image, i.Item_Name, i.Category_Id, i.Base_Price, iv.Quantity
                                        FROM inventory iv
                                        INNER JOIN items i ON iv.Item_Id = i.Item_Id
                                        ORDER BY iv.Quantity ASC, i.Item_Name ASC";
                }
                else
                {
                    inventoryQuery = $@"SELECT iv.inventory_id, i.Item_Id, i.Item_Image, i.Item_Name, i.Category_Id, i.Base_Price, iv.Quantity
                                        FROM inventory iv
                                        INNER JOIN items i ON iv.Item_Id = i.Item_Id
                                        WHERE i.Item_Name LIKE CONCAT('%', '{searchText}', '%')
                                        ORDER BY iv.Quantity ASC, i.Item_Name ASC";
                }
            }

            return inventoryQuery;
        }

        private void SetItemsFromInventory(string categoryID = null)
        {
            int categoryCount = DataAccess.GetCategoryItemCount(Convert.ToInt32(categoryID));

            string inventoryQuery = BuildInventoryQuery(categoryID, txtSearch.Text);

            if (categoryID != null && categoryCount.ToString() == "0")
            {
                flpItems.Controls.Clear();
                flpItems.Controls.Add(pnlMainItem);
            }
            else
            {
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
        }

        private void UpdateQuantity(int inventoryID, int quantity)
        {
            int inventoryIdToUpdate = inventoryID;
            int newQuantity = quantity;

            DataAccess.UpdateInventory(inventoryIdToUpdate, newQuantity);
        }

        private void InventoryForm_Load(object sender, EventArgs e)
        {
            FormUtilities.setUserProfile(userPic, lblUsername, this.username);

            flpCategories.Controls.Clear();
            flpItems.Controls.Clear();
            SetCategories();
            SetItemsFromInventory();
        }

        private void btnCollapse_Click(object sender, EventArgs e)
        {
            CollapsePanel();
        }

        private void btnExpand_Click(object sender, EventArgs e)
        {
            ExpandPanel();
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

        private void pnlCategories_MouseClick(object sender, MouseEventArgs e)
        {
            if (sender is Control control)
            {
                Control parentPanel = control.Parent;
                if (parentPanel is Panel)
                {
                    pnlCategories_Click(parentPanel, EventArgs.Empty);
                }
            }
        }

        private void pnlCategories_Click(object sender, EventArgs e)
        {
            if (pnlMainCategory.FillColor == selectedPanelColor)
            {
                pnlMainCategory.FillColor = defaultPanelColor;
                picMainArrow.Visible = false;
            }

            if (sender is Guna.UI2.WinForms.Guna2Panel clickedPanel)
            {
                this.categoryID = clickedPanel.Tag?.ToString();

                if (this.categoryID == null)
                {
                    flpItems.Controls.Clear();
                    SetItemsFromInventory();
                }
                else
                {
                    flpItems.Controls.Clear();
                    SetItemsFromInventory(categoryID);
                }

                foreach (Control control in clickedPanel.Controls)
                {
                    if (control is Guna.UI2.WinForms.Guna2PictureBox picArrow)
                    {
                        picArrow.Visible = true;
                    }
                }

                if (selectedPanel != null && selectedPanel != clickedPanel)
                {
                    foreach (Control control in selectedPanel.Controls)
                    {
                        if (control is Guna.UI2.WinForms.Guna2PictureBox picArrow)
                        {
                            picArrow.Visible = false;
                            break;
                        }
                    }

                    ResetPanelColor(selectedPanel);
                }

                UpdatePanelColor(clickedPanel);
                selectedPanel = clickedPanel;
            }
        }

        private void pnlItems_MouseClick(object sender, MouseEventArgs e)
        {
            if (sender is Control control)
            {
                Control parentPanel = control.Parent;
                if (parentPanel is Panel)
                {
                    pnlMainItem_Click(parentPanel, EventArgs.Empty);
                }
            }
        }

        private void pnlMainItem_Click(object sender, EventArgs e)
        {
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, adminmenuFormRef.btnManagement, Color.FromArgb(176, 122, 50), Color.White, Properties.Resources.manage_icon_white, new ManagementForm(username));

            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, adminmenuFormRef.btnMenu, Color.Transparent, Color.FromArgb(176, 164, 164), Properties.Resources.menu_icon_gray);
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, adminmenuFormRef.btnDelivery, Color.Transparent, Color.FromArgb(176, 164, 164), Properties.Resources.delivery_icon_gray);
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, adminmenuFormRef.btnHistory, Color.Transparent, Color.FromArgb(176, 164, 164), Properties.Resources.history_icon_gray);
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, adminmenuFormRef.btnInventory, Color.Transparent, Color.FromArgb(176, 164, 164), Properties.Resources.inventory_icon_gray);
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, adminmenuFormRef.btnHome, Color.Transparent, Color.FromArgb(176, 164, 164), Properties.Resources.home_icon_gray);
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, adminmenuFormRef.btnSettings, Color.Transparent, Color.FromArgb(176, 164, 164), Properties.Resources.settings_icon_gray);
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            if (sender is Guna.UI2.WinForms.Guna2Button btnMinus)
            {
                foreach (Control control in btnMinus.Parent.Controls)
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
            if (sender is Guna.UI2.WinForms.Guna2TextBox txtQuantity)
            {
                Control parent = txtQuantity.Parent;
                string inventoryID = txtQuantity.Tag?.ToString();

                if (txtQuantity.Text == "")
                {
                    txtQuantity.Text = "0";
                }

                foreach (Control control in parent.Controls)
                {
                    if (control is Label label && label.Name == "lblItemPriceCount")
                    {
                        if (label != null)
                        {
                            string[] parts = label.Text.Split(new[] { " • " }, StringSplitOptions.RemoveEmptyEntries);
                            if (parts.Length == 2)
                            {
                                parts[1] = txtQuantity.Text + " Available";
                                label.Text = string.Join(" • ", parts);
                            }
                        }

                        break;
                    }
                }

                UpdateQuantity(Convert.ToInt32(inventoryID), Convert.ToInt32(txtQuantity.Text));
            }
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            if (sender is Guna.UI2.WinForms.Guna2Button btnPlus)
            {
                foreach (Control control in btnPlus.Parent.Controls)
                {
                    if (control is Guna.UI2.WinForms.Guna2TextBox txtQuantity)
                    {
                        if (int.TryParse(txtQuantity.Text, out int quantity))
                        {
                            quantity++;
                            txtQuantity.Text = quantity.ToString();
                        }
                    }
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

        private Guna.UI2.WinForms.Guna2Panel pnlCategories = new Guna.UI2.WinForms.Guna2Panel();
        private Label lblCategoryName = new Label();
        private Label lblCategoryCount = new Label();
        private Guna.UI2.WinForms.Guna2PictureBox picArrow = new Guna.UI2.WinForms.Guna2PictureBox();
        private Guna.UI2.WinForms.Guna2Panel selectedPanel = null;
        private Guna.UI2.WinForms.Guna2Panel pnlItems = new Guna.UI2.WinForms.Guna2Panel();
        private Label lblItemName = new Label();
        private Guna.UI2.WinForms.Guna2PictureBox picItem = new Guna.UI2.WinForms.Guna2PictureBox();
        private Label lblItemPriceCount = new Label();
        private Guna.UI2.WinForms.Guna2TextBox txtQuantity = new Guna.UI2.WinForms.Guna2TextBox();
        private Guna.UI2.WinForms.Guna2Button btnMinus = new Guna.UI2.WinForms.Guna2Button();
        private Guna.UI2.WinForms.Guna2Button btnPlus = new Guna.UI2.WinForms.Guna2Button();
    }
}
