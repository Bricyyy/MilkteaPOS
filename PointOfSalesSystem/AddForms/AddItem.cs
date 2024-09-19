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
    public partial class AddItem : Form
    {
        private readonly ManagementForm manageFormRef;
        private readonly ItemsForm itemsFormRef;
        private readonly string view;
        private readonly string searchData;

        private readonly byte[] originalItemImage = null;
        private readonly string originalItemName = "";
        private readonly int originalCategoryValue = -1;
        private readonly string originalPrice = "";

        private byte[] newImage;

        public AddItem(ManagementForm manageForm, ItemsForm itemsForm, string view, string searchData)
        {
            InitializeComponent();

            manageFormRef = manageForm;
            itemsFormRef = itemsForm;
            this.view = view;
            this.searchData = searchData;
        }

        private void setCategoryData()
        {
            string categoryQuery = "SELECT Category_Id, Category_Name FROM item_category";

            List<Category> categories = DataAccess.GetCategories(categoryQuery);

            if (categories.Count > 0)
            {
                cmbCategory.DisplayMember = "CategoryName";
                cmbCategory.ValueMember = "CategoryId";

                cmbCategory.DataSource = categories;

                cmbCategory.SelectedIndex = -1;
            }
        }

        private void InsertItemData()
        {
            byte[] ItemImage;
            if (newImage != null)
            {
                ItemImage = newImage;
            }
            else
            {
                ItemImage = null;
            }
            string ItemName = txtItemName.Text;
            int CategoryId = Convert.ToInt32(cmbCategory.SelectedValue);
            decimal BasePrice = decimal.Parse(txtPrice.Text);

            DataAccess.InsertItem(ItemImage, ItemName, CategoryId, BasePrice);
        }

        private void resetFields()
        {
            lblImageChecker.Visible = false;
            lblNameChecker.Visible = false;
            lblCategoryChecker.Visible = false;
            lblPriceChecker.Visible = false;

            itemPic.Image = Properties.Resources.CoffeeIcon;
            newImage = null;
            txtItemName.Text = null;
            txtPrice.Text = null;
        }

        private void SaveItemData()
        {
            string tableName = "items";
            string searchColumn = "Item_Name";
            string dataToCheck = txtItemName.Text;

            bool isItemNameEmpty = DataChecker.IsTextboxEmpty(txtItemName);
            bool isCategoryEmpty = DataChecker.IsComboboxEmpty(cmbCategory);
            bool isPriceEmpty = DataChecker.IsTextboxEmpty(txtPrice);

            if (isItemNameEmpty || isCategoryEmpty || isPriceEmpty)
            {
                if (isItemNameEmpty)
                {
                    DataChecker.HandleError(picEdit, lblTitle, lblNameChecker, "Fill out the field.");
                }

                if (isCategoryEmpty)
                {
                    DataChecker.HandleError(picEdit, lblTitle, lblCategoryChecker, "Fill out the field.");
                }

                if (isPriceEmpty)
                {
                    DataChecker.HandleError(picEdit, lblTitle, lblPriceChecker, "Fill out the field.");
                }

                return;
            }

            Dictionary<string, Label> errorLabels = new Dictionary<string, Label>
            {
                { "Maximum file size is 1mb.", lblImageChecker },
                { "Item name already exists.", lblNameChecker },
                { "Enter a valid price.", lblPriceChecker },
            };

            List<string> errorMessages = new List<string>();

            if (newImage != null && DataChecker.IsImageSizeTooLarge(newImage))
            {
                errorMessages.Add("Maximum file size is 1mb.");
            }

            if (DataChecker.HasDuplicate(tableName, searchColumn, dataToCheck))
            {
                errorMessages.Add("Item name already exists.");
            }

            if (!DataChecker.IsValidPrice(txtPrice.Text))
            {
                errorMessages.Add("Enter a valid price.");
            }

            if (errorMessages.Any())
            {
                foreach (var errorMessage in errorMessages)
                {
                    lblTitle.Text = "Add Item";

                    if (errorLabels.TryGetValue(errorMessage, out Label label))
                    {
                        DataChecker.HandleError(picEdit, lblTitle, label, errorMessage);
                    }
                }
                return;
            }

            InsertItemData();

            AddAnimation.EditInfo(picEdit, lblTitle, Properties.Resources.edit_check_animated, "Item Saved!");

            resetFields();
            setCategoryData();

            FormUtilities.SetLabelCount(manageFormRef.lblItemCount, DataAccess.GetEntityCount("items"));
            FormUtilities.defaultBtnInfo(btnSave, btnBack);
            FormUtilities.UpdateRemoveButton(btnRemoveImage, lblImageChecker, itemPic, Properties.Resources.CoffeeIcon);
        }

        private void AddItem_Load(object sender, EventArgs e)
        {
            setCategoryData();

            newImage = null;

            FormUtilities.defaultBtnInfo(btnSave, btnBack);
            FormUtilities.UpdateRemoveButton(btnRemoveImage, lblImageChecker, itemPic, Properties.Resources.CoffeeIcon);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (view == "list")
            {
                itemsFormRef.loadForm(new ItemsListView(manageFormRef, itemsFormRef, view, searchData));
            }
            else
            {
                itemsFormRef.loadForm(new ItemsGridView());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveItemData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEditImage_Click(object sender, EventArgs e)
        {
            newImage = FormUtilities.InsertImage(itemPic, originalItemImage);
            itemPic.SizeMode = PictureBoxSizeMode.StretchImage;

            FormUtilities.UpdateRemoveButton(btnRemoveImage, lblImageChecker, itemPic, Properties.Resources.CoffeeIcon);

            if (txtItemName.Text == originalItemName && cmbCategory.SelectedIndex == originalCategoryValue && txtPrice.Text == originalPrice)
            {
                FormUtilities.UpdateButtonOnImageChanged(lblImageChecker, newImage, btnSave, btnBack, originalItemImage);
            }
        }

        private void btnRemoveImage_Click(object sender, EventArgs e)
        {
            itemPic.Image = Properties.Resources.CoffeeIcon;
            newImage = null;

            FormUtilities.UpdateRemoveButton(btnRemoveImage, lblImageChecker, itemPic, Properties.Resources.CoffeeIcon);

            if (txtItemName.Text == originalItemName && cmbCategory.SelectedIndex == originalCategoryValue && txtPrice.Text == originalPrice)
            {
                FormUtilities.UpdateButtonOnImageChanged(lblImageChecker, newImage, btnSave, btnBack, originalItemImage);
            }
        }

        private void txtItemName_TextChanged(object sender, EventArgs e)
        {
            FormUtilities.UpdateChecker(lblNameChecker);

            if (cmbCategory.SelectedIndex == originalCategoryValue && txtPrice.Text == originalPrice)
            {
                FormUtilities.UpdateButtonOnTextChanged(lblNameChecker, txtItemName, btnSave, btnBack, originalItemName);
            }
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormUtilities.UpdateChecker(lblCategoryChecker);

            if (txtItemName.Text == originalItemName && txtPrice.Text == originalPrice)
            {
                FormUtilities.UpdateButtonOnSelectedIndexChanged(cmbCategory, btnSave, btnBack, originalCategoryValue);
            }
        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
            FormUtilities.UpdateChecker(lblPriceChecker);

            if (cmbCategory.SelectedIndex == originalCategoryValue && txtItemName.Text == originalItemName)
            {
                FormUtilities.UpdateButtonOnTextChanged(lblPriceChecker, txtPrice, btnSave, btnBack, originalPrice);
            }
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
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
    }
}
