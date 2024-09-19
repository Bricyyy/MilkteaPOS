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
    public partial class EditSupplierForm : Form
    {
        private readonly ManagementForm manageFormRef;
        private readonly SuppliersForm supplierFormRef;
        private readonly string supplierID;
        private readonly string view;
        private readonly string searchData;

        private string originalSupplierName;
        private string originalAddress;
        private string originalContact;

        public EditSupplierForm(ManagementForm manageForm, SuppliersForm supplierForm, string supplierID, string view, string searchData)
        {
            InitializeComponent();

            manageFormRef = manageForm;
            supplierFormRef = supplierForm;
            this.supplierID = supplierID;
            this.view = view;
            this.searchData = searchData;
        }

        private void setSupplierData()
        {
            string supplierQuery = "SELECT Supplier_Id, Supplier_Name, Address, Contact_Number FROM supplier";
            string specificID = supplierID;

            List<Supplier> suppliers = DataAccess.GetSuppliers(supplierQuery);

            if (suppliers.Count > 0)
            {
                Supplier selectedSupplier = suppliers.FirstOrDefault(supplier => supplier.SupplierId.ToString() == specificID);

                if (selectedSupplier != null)
                {
                    txtSupplierName.Text = selectedSupplier.SupplierName;
                    txtAddress.Text = selectedSupplier.SupplierAddress;
                    txtContact.Text = selectedSupplier.SupplierContact;

                    originalSupplierName = selectedSupplier.SupplierName;
                    originalAddress = selectedSupplier.SupplierAddress;
                    originalContact = selectedSupplier.SupplierContact;
                }
            }
        }

        private void UpdateSupplierData()
        {
            int supplierIdToUpdate = Convert.ToInt32(supplierID);
            string newSupplierName = txtSupplierName.Text;
            string newAddress = txtAddress.Text;
            string newContact = txtContact.Text;

            DataAccess.UpdateSupplier(supplierIdToUpdate, newSupplierName, newAddress, newContact);
        }

        private void SaveSupplierData()
        {
            bool isSupplierNameEmpty = DataChecker.IsTextboxEmpty(txtSupplierName);
            bool isAddressEmpty = DataChecker.IsTextboxEmpty(txtAddress);
            bool isContactEmpty = DataChecker.IsTextboxEmpty(txtContact);

            if (isSupplierNameEmpty || isAddressEmpty || isContactEmpty)
            {
                if (isSupplierNameEmpty)
                {
                    DataChecker.HandleError(picEdit, lblTitle, lblNameChecker, "Fill out the field.");
                }

                if (isAddressEmpty)
                {
                    DataChecker.HandleError(picEdit, lblTitle, lblAddressChecker, "Fill out the field.");
                }

                if (isContactEmpty)
                {
                    DataChecker.HandleError(picEdit, lblTitle, lblNumberChecker, "Fill out the field.");
                }

                return;
            }

            UpdateSupplierData();
            originalSupplierName = txtSupplierName.Text;
            originalAddress = txtAddress.Text;
            originalContact = txtContact.Text;

            AddAnimation.EditInfo(picEdit, lblTitle, Properties.Resources.edit_check_animated, "Supplier Saved!");
            lblNameChecker.Visible = false;
            lblAddressChecker.Visible = false;
            lblNumberChecker.Visible = false;

            FormUtilities.defaultBtnInfo(btnSave, btnBack);
        }

        private void EditSupplierForm_Load(object sender, EventArgs e)
        {
            setSupplierData();
            FormUtilities.defaultBtnInfo(btnSave, btnBack);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (view == "list")
            {
                supplierFormRef.loadForm(new SupplierListView(manageFormRef, supplierFormRef, view, searchData));
            }
            else
            {
                supplierFormRef.loadForm(new SupplierGridView());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveSupplierData();
        }

        private void txtSupplierName_TextChanged(object sender, EventArgs e)
        {
            FormUtilities.UpdateChecker(lblNameChecker);

            if (txtAddress.Text == originalAddress && txtContact.Text == originalContact)
            {
                FormUtilities.UpdateButtonOnTextChanged(lblNameChecker, txtSupplierName, btnSave, btnBack, originalSupplierName);
            }
        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {
            FormUtilities.UpdateChecker(lblAddressChecker);

            if (txtSupplierName.Text == originalSupplierName && txtContact.Text == originalContact)
            {
                FormUtilities.UpdateButtonOnTextChanged(lblAddressChecker, txtAddress, btnSave, btnBack, originalAddress);
            }
        }

        private void txtContact_TextChanged(object sender, EventArgs e)
        {
            FormUtilities.UpdateChecker(lblNumberChecker);

            if (txtAddress.Text == originalAddress && txtSupplierName.Text == originalSupplierName)
            {
                FormUtilities.UpdateButtonOnTextChanged(lblNumberChecker, txtContact, btnSave, btnBack, originalContact);
            }
        }

        private void txtContact_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
