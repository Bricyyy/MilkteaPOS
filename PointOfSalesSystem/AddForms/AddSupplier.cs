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
    public partial class AddSupplier : Form
    {
        private readonly ManagementForm manageFormRef;
        private readonly SuppliersForm suppliersFormRef;
        private readonly string view;
        private readonly string searchData;

        private readonly string originalSupplierName = "";
        private readonly string originalAddress = "";
        private readonly string originalContact = "";

        public AddSupplier(ManagementForm manageForm, SuppliersForm suppliersForm, string view, string searchData)
        {
            InitializeComponent();

            manageFormRef = manageForm;
            suppliersFormRef = suppliersForm;
            this.view = view;
            this.searchData = searchData;
        }

        private void InsertSupplierData()
        {
            string SupplierName = txtSupplierName.Text;
            string Address = txtAddress.Text;
            string Contact = txtContact.Text;

            DataAccess.InsertSupplier(SupplierName, Address, Contact);
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

            InsertSupplierData();

            AddAnimation.EditInfo(picEdit, lblTitle, Properties.Resources.edit_check_animated, "Supplier Saved!");

            lblNameChecker.Visible = false;
            lblAddressChecker.Visible = false;
            lblNumberChecker.Visible = false;

            txtSupplierName.Text = null;
            txtAddress.Text = null;
            txtContact.Text = null;

            FormUtilities.SetLabelCount(manageFormRef.lblSupplierCount, DataAccess.GetEntityCount("supplier"));
            FormUtilities.defaultBtnInfo(btnSave, btnBack);
        }

        private void AddSupplier_Load(object sender, EventArgs e)
        {
            FormUtilities.defaultBtnInfo(btnSave, btnBack);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (view == "list")
            {
                suppliersFormRef.loadForm(new SupplierListView(manageFormRef, suppliersFormRef, view, searchData));
            }
            else
            {
                suppliersFormRef.loadForm(new SupplierGridView());
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

            if (txtSupplierName.Text == originalSupplierName && txtAddress.Text == originalAddress)
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
