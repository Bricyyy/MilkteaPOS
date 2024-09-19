using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace PointOfSalesSystem
{
    public partial class EditItemForm : Form
    {
        private readonly ManagementForm manageFormRef;
        private readonly ItemsForm itemsFormRef;    
        private readonly string itemID;
        private readonly string view;
        private readonly string searchData;

        private byte[] originalItemImage;
        private string originalItemName;
        private string originalCategoryValue;
        private string originalPrice;

        private byte[] newImage;

        public EditItemForm(ManagementForm manageForm, ItemsForm itemsForm, string itemID, string view, string searchData)
        {
            InitializeComponent();

            manageFormRef = manageForm;
            itemsFormRef = itemsForm;
            this.itemID = itemID;
            this.view = view;
            this.searchData = searchData;
        }

        private void setItemData()
        {
            string itemQuery = "SELECT Item_Id, Item_Image, Item_Name, Category_Id, Base_Price FROM items";
            string categoryQuery = "SELECT Category_Id, Category_Name FROM item_category";
            string specificID = itemID;

            List<Item> items = DataAccess.GetItems(itemQuery);

            if (items.Count > 0)
            {
                Item selectedItem = items.FirstOrDefault(item => item.ItemId.ToString() == specificID);

                if (selectedItem != null)
                {
                    txtItemName.Text = selectedItem.ItemName;
                    txtPrice.Text = selectedItem.BasePrice.ToString();

                    if (selectedItem.ItemImage != null)
                    {
                        itemPic.Image = FormUtilities.ByteArrayToImage(selectedItem.ItemImage);
                        itemPic.SizeMode = PictureBoxSizeMode.StretchImage;
                        originalItemImage = selectedItem.ItemImage;
                    }
                    else
                    {
                        itemPic.Image = Properties.Resources.CoffeeIcon;
                        originalItemImage = FormUtilities.ImageToByteArray(itemPic);
                    }

                    List<Category> categories = DataAccess.GetCategories(categoryQuery);

                    cmbCategory.DisplayMember = "CategoryName";
                    cmbCategory.ValueMember = "CategoryId";

                    cmbCategory.DataSource = categories;

                    cmbCategory.SelectedValue = selectedItem.CategoryId;

                    originalItemName = selectedItem.ItemName;
                    originalCategoryValue = selectedItem.CategoryId.ToString();
                    originalPrice = selectedItem.BasePrice.ToString();
                }
            }
        }

        private void UpdateItemData()
        {
            int itemIdToUpdate = Convert.ToInt32(itemID);
            string newItemName = txtItemName.Text;
            int newCategoryId = Convert.ToInt32(cmbCategory.SelectedValue);
            decimal newBasePrice = decimal.Parse(txtPrice.Text);

            DataAccess.UpdateItem(itemIdToUpdate, newItemName, newCategoryId, newBasePrice);
        }

        private void SaveItemData()
        {
            string tableName = "items";
            string idColumn = "Item_Id";
            string searchColumn = "Item_Name";
            string dataToCheck = txtItemName.Text;
            int item_id = Convert.ToInt32(itemID);

            bool isItemNameEmpty = DataChecker.IsTextboxEmpty(txtItemName);
            bool isPriceEmpty = DataChecker.IsTextboxEmpty(txtPrice);

            if (isItemNameEmpty || isPriceEmpty)
            {
                if (isItemNameEmpty)
                {
                    DataChecker.HandleError(picEdit, lblTitle, lblNameChecker, "Fill out the field.");
                }

                if (isPriceEmpty)
                {
                    DataChecker.HandleError(picEdit, lblTitle, lblPriceChecker, "Fill out the field.");
                }

                return;
            }

            if (DataChecker.HasDuplicate(tableName, idColumn, searchColumn, dataToCheck, item_id))
            {
                DataChecker.HandleError(picEdit, lblTitle, lblNameChecker, "Item name already exists.");
                return;
            }

            if (!DataChecker.IsValidPrice(txtPrice.Text))
            {
                DataChecker.HandleError(picEdit, lblTitle, lblPriceChecker, "Enter a valid price.");
                return;
            }

            UpdateItemData();
            originalItemName = txtItemName.Text;
            originalCategoryValue = cmbCategory.SelectedValue.ToString();
            originalPrice = txtPrice.Text;

            AddAnimation.EditInfo(picEdit, lblTitle, Properties.Resources.edit_check_animated, "Item Saved!");
            lblNameChecker.Visible = false;
            lblPriceChecker.Visible = false;

            FormUtilities.defaultBtnInfo(btnSave, btnBack);
        }

        private void EditItemForm_Load(object sender, EventArgs e)
        {
            setItemData();

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

            if (DataChecker.IsImageSizeTooLarge(newImage))
            {
                itemPic.Image = FormUtilities.ByteArrayToImage(originalItemImage);

                DataChecker.HandleError(picEdit, lblTitle, lblImageChecker, "Maximum file size is 1mb.");
                return;
            }

            DataAccess.UpdateItemImage(Convert.ToInt32(itemID), newImage);
            AddAnimation.EditInfo(picEdit, lblTitle, Properties.Resources.edit_check_animated, "Image Saved!");
            originalItemImage = newImage;

            FormUtilities.UpdateRemoveButton(btnRemoveImage, lblImageChecker, itemPic, Properties.Resources.CoffeeIcon);
        }

        private void btnRemoveImage_Click(object sender, EventArgs e)
        {
            itemPic.Image = Properties.Resources.CoffeeIcon;
            newImage = FormUtilities.ImageToByteArray(itemPic);

            DataAccess.UpdateItemImage(Convert.ToInt32(itemID), null);
            AddAnimation.EditInfo(picEdit, lblTitle, Properties.Resources.edit_check_animated, "Image Removed!");

            FormUtilities.UpdateRemoveButton(btnRemoveImage, lblImageChecker, itemPic, Properties.Resources.CoffeeIcon);
        }

        private void txtItemName_TextChanged(object sender, EventArgs e)
        {
            FormUtilities.UpdateChecker(lblNameChecker);

            if (cmbCategory.SelectedValue?.ToString() == originalCategoryValue && txtPrice.Text == originalPrice)
            {
                FormUtilities.UpdateButtonOnTextChanged(lblNameChecker, txtItemName, btnSave, btnBack, originalItemName);
            }
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtItemName.Text == originalItemName && txtPrice.Text == originalPrice)
            {
                FormUtilities.UpdateButtonOnSelectedIndexChanged(cmbCategory, btnSave, btnBack, originalCategoryValue);
            }
        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
            FormUtilities.UpdateChecker(lblPriceChecker);

            if (cmbCategory.SelectedValue?.ToString() == originalCategoryValue && txtItemName.Text == originalItemName)
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
