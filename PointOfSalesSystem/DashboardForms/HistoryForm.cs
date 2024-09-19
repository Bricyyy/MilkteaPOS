using PointOfSalesSystem.SalesForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace PointOfSalesSystem
{
    public partial class HistoryForm : Form
    {
        private readonly string username;
        private readonly string userRole;

        private string userID;

        public HistoryForm(string username, string userRole)
        {
            InitializeComponent();

            this.username = username;
            this.userRole = userRole;
        }

        public void loadForm(Form newForm)
        {
            FormUtilities.LoadForm(pnlMain, newForm);
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

        private void setDateRange()
        {
            dtpFirstRange.Value = DateTime.Now;
            dtpSecondRange.Value = DateTime.Now;
            dtpFirstRange.Checked = false;
            dtpSecondRange.Checked = false;

            dtpFirstRange.CustomFormat = "MMMM dd, yyyy";
            dtpSecondRange.CustomFormat = "MMMM dd, yyyy";
        }
         
        private void HistoryForm_Load(object sender, EventArgs e)
        {
            FormUtilities.setUserProfile(userPic, lblUsername, this.username);
            setUserRoleGreeting();
            GetUserID();

            setDateRange();

            loadForm(new OrderHistoryLists(this));
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            loadForm(new OrderHistoryLists(this));
        }

        private void dtpFirstRange_ValueChanged(object sender, EventArgs e)
        {
            dtpFirstRange.Checked = false;

            loadForm(new OrderHistoryLists(this));
        }

        private void dtpFirstRange_MouseClick(object sender, MouseEventArgs e)
        {
            dtpFirstRange.Checked = false;
        }

        private void dtpSecondRange_ValueChanged(object sender, EventArgs e)
        {
            dtpSecondRange.Checked = false;

            loadForm(new OrderHistoryLists(this));
        }

        private void dtpSecondRange_MouseClick(object sender, MouseEventArgs e)
        {
            dtpSecondRange.Checked = false;
        }
    }
}
