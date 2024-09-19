using Microsoft.VisualBasic.ApplicationServices;
using PointOfSalesSystem.DashboardMenu;
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
    public partial class Dashboard : Form
    {
        private readonly string username;

        private byte[] userImage;
        private string userRole;

        public Dashboard(string username)
        {
            InitializeComponent();

            this.username = username;
        }

        public void loadForm(Form newForm)
        {
            FormUtilities.LoadForm(pnlMain, newForm);
        }

        private void setUserData()
        {
            string userQuery = "SELECT * FROM users";
            string specificUser = username;

            List<User> users = DataAccess.GetUsers(userQuery);

            if (users.Count > 0)
            {
                User selectedUser = users.FirstOrDefault(user => user.Username.ToString() == specificUser);

                if (selectedUser != null)
                {
                    if (selectedUser.UserImage != null)
                    {
                        this.userImage = selectedUser.UserImage;
                    }
                    else
                    {
                        this.userImage = null;
                    }

                    this.userRole = selectedUser.Role;
                }
            }
        }

        private void setDashboardOptions()
        {
            if (userRole == "user")
            {
                FormUtilities.LoadForm(pnlMenuOptions, new UserMenu(this, username, userRole));
            }
            else if (userRole == "admin")
            {
                FormUtilities.LoadForm(pnlMenuOptions, new AdminMenu(this, username, userRole));
            }
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            setUserData();
            setDashboardOptions();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            Login login = new Login();
            login.Show();
        }
    }
}
