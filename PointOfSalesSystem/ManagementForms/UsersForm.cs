using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PointOfSalesSystem
{
    public partial class UsersForm : Form
    {
        private readonly ManagementForm manageFormRef;

        private string view;
        private string searchData;

        public UsersForm(ManagementForm manageForm, string view = null, string searchData = null)
        {
            InitializeComponent();

            manageFormRef = manageForm;
            this.view = view;
            this.searchData = searchData;
        }

        public void loadForm(Form newForm)
        {
            FormUtilities.LoadForm(pnlMain, newForm);
        }

        private void UsersForm_Load(object sender, EventArgs e)
        {
            if (view == null)
            {
                view = "list";
            }

            FormUtilities.UpdateButton(pnlMain, btnList, Color.FromArgb(239, 241, 243), Properties.Resources.list_icon_selected, new UsersListView(manageFormRef, this, view));
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            loadForm(new AddUser(manageFormRef, this, view, searchData));
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            view = "list";

            if (searchData == null)
            {
                FormUtilities.UpdateButton(pnlMain, btnList, Color.FromArgb(239, 241, 243), Properties.Resources.list_icon_selected, new UsersListView(manageFormRef, this, view));
            }
            else
            {
                FormUtilities.UpdateButton(pnlMain, btnList, Color.FromArgb(239, 241, 243), Properties.Resources.list_icon_selected, new UsersListView(manageFormRef, this, view, searchData));
            }

            FormUtilities.UpdateButton(pnlMain, btnGrid, Color.White, Properties.Resources.grid_icon_notselected);
        }

        private void btnGrid_Click(object sender, EventArgs e)
        {
            view = "grid";

            if (searchData == null)
            {
                FormUtilities.UpdateButton(pnlMain, btnGrid, Color.FromArgb(239, 241, 243), Properties.Resources.grid_icon_selected, new UsersGridView());
            }
            else
            {
                FormUtilities.UpdateButton(pnlMain, btnGrid, Color.FromArgb(239, 241, 243), Properties.Resources.grid_icon_selected, new UsersGridView());
            }

            FormUtilities.UpdateButton(pnlMain, btnList, Color.White, Properties.Resources.list_icon_notselected);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            searchData = txtSearch.Text;

            if (view == "list")
            {
                FormUtilities.UpdateButton(pnlMain, btnList, Color.FromArgb(239, 241, 243), Properties.Resources.list_icon_selected, new UsersListView(manageFormRef, this, view, searchData));
            }
            else
            {
                FormUtilities.UpdateButton(pnlMain, btnGrid, Color.FromArgb(239, 241, 243), Properties.Resources.grid_icon_selected, new UsersGridView());
            }
        }
    }
}
