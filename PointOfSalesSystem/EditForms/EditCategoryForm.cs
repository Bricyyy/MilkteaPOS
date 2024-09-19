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
    public partial class EditCategoryForm : Form
    {
        private readonly ManagementForm manageFormRef;
        private readonly CategoryForm categoryFormRef;
        private readonly string categoryID;
        private readonly string view;
        private readonly string searchData;

        private string originalCategoryName;

        public EditCategoryForm(ManagementForm manageForm, CategoryForm categoryForm, string categoryID, string view, string searchData)
        {
            InitializeComponent();

            manageFormRef = manageForm;
            categoryFormRef = categoryForm;
            this.categoryID = categoryID;
            this.view = view;
            this.searchData = searchData;
        }

        private void setCategoryData()
        {
            string categoryQuery = "SELECT Category_Id, Category_Name FROM item_category";
            string specificID = categoryID;

            List<Category> categories = DataAccess.GetCategories(categoryQuery);

            if (categories.Count > 0)
            {
                Category selectedCategory = categories.FirstOrDefault(category => category.CategoryId.ToString() == specificID);

                if (selectedCategory != null)
                {
                    txtCategoryName.Text = selectedCategory.CategoryName;

                    originalCategoryName = selectedCategory.CategoryName;
                }
            }
        }

        private void UpdateCategoryData()
        {
            int categoryIdToUpdate = Convert.ToInt32(categoryID);
            string newCategoryName = txtCategoryName.Text;

            DataAccess.UpdateCategory(categoryIdToUpdate, newCategoryName);
        }

        private void SaveCategoryData()
        {
            string tableName = "item_category";
            string idColumn = "Category_Id";
            string searchColumn = "Category_Name";
            string dataToCheck = txtCategoryName.Text;
            int category_id = Convert.ToInt32(categoryID);

            bool isCategoryNameEmpty = DataChecker.IsTextboxEmpty(txtCategoryName);

            if (isCategoryNameEmpty)
            {
                DataChecker.HandleError(picEdit, lblTitle, lblNameChecker, "Fill out the field.");
                return;
            }

            if (DataChecker.HasDuplicate(tableName, idColumn, searchColumn, dataToCheck, category_id))
            {
                DataChecker.HandleError(picEdit, lblTitle, lblNameChecker, "Category name already exists.");
                return;
            }

            UpdateCategoryData();
            originalCategoryName = txtCategoryName.Text;

            AddAnimation.EditInfo(picEdit, lblTitle, Properties.Resources.edit_check_animated, "Category Saved!");
            lblNameChecker.Visible = false;

            FormUtilities.defaultBtnInfo(btnSave, btnBack);
        }

        private void EditCategoryForm_Load(object sender, EventArgs e)
        {
            setCategoryData();
            FormUtilities.defaultBtnInfo(btnSave, btnBack);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (view == "list")
            {
                categoryFormRef.loadForm(new CategoryListView(manageFormRef, categoryFormRef, view, searchData));
            }
            else
            {
                categoryFormRef.loadForm(new CategoryGridView());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveCategoryData();
        }

        private void txtCategoryName_TextChanged(object sender, EventArgs e)
        {
            FormUtilities.UpdateButtonOnTextChanged(lblNameChecker, txtCategoryName, btnSave, btnBack, originalCategoryName);
        }
    }
}
