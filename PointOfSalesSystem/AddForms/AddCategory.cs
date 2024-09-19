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
    public partial class AddCategory : Form
    {
        private readonly ManagementForm manageFormRef;
        private readonly CategoryForm categoryFormRef;
        private readonly string view;
        private readonly string searchData;

        private readonly string originalCategoryName = "";

        public AddCategory(ManagementForm manageForm, CategoryForm categoryForm, string view, string searchData)
        {
            InitializeComponent();

            manageFormRef = manageForm;
            categoryFormRef = categoryForm;
            this.view = view;
            this.searchData = searchData;
        }

        private void InsertCategoryData()
        {
            string CategoryName = txtCategoryName.Text;

            DataAccess.InsertCategory(CategoryName);
        }

        private void SaveCategoryData()
        {
            string tableName = "item_category";
            string searchColumn = "Category_Name";
            string dataToCheck = txtCategoryName.Text;

            bool isCategoryNameEmpty = DataChecker.IsTextboxEmpty(txtCategoryName);

            if (isCategoryNameEmpty)
            {
                DataChecker.HandleError(picEdit, lblTitle, lblNameChecker, "Fill out the field.");
                return;
            }

            if (DataChecker.HasDuplicate(tableName, searchColumn, dataToCheck))
            {
                DataChecker.HandleError(picEdit, lblTitle, lblNameChecker, "Category name already exists.");
                return;
            }

            InsertCategoryData();

            AddAnimation.EditInfo(picEdit, lblTitle, Properties.Resources.edit_check_animated, "Category Saved!");

            lblNameChecker.Visible = false;

            txtCategoryName.Text = null;

            FormUtilities.SetLabelCount(manageFormRef.lblCategoryCount, DataAccess.GetEntityCount("item_category"));
            FormUtilities.defaultBtnInfo(btnSave, btnBack);
        }

        private void AddCategory_Load(object sender, EventArgs e)
        {
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
