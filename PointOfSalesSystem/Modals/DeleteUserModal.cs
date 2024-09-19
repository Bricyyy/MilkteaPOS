using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PointOfSalesSystem.DeleteModals
{
    public partial class DeleteUserModal : Form
    {
        public bool IsConfirmed
        {
            get { return isConfirmed; }
        }

        private readonly string username;

        private bool isConfirmed = false;

        public DeleteUserModal(string username)
        {
            InitializeComponent();

            this.username = username;
        }

        private void ConfirmPassword()
        {
            try
            {
                string username = this.username;
                string password = txtPassword.Text;

                List<User> userList = DataAccess.GetUsers($"SELECT * FROM users WHERE username = '{username}'");

                if (userList.Count > 0)
                {
                    User user = userList[0];

                    bool passwordMatch = BCrypt.Net.BCrypt.Verify(password, user.Password);

                    if (user.Username == username && passwordMatch)
                    {
                        isConfirmed = true;
                        this.Close();
                    }
                    else
                    {
                        txtPassword.Text = "";
                        lblPassChecker.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteUserModal_Load(object sender, EventArgs e)
        {
            if (Owner != null)
            {
                int x = Owner.Location.X + (Owner.Width - Width) / 2;
                int y = Owner.Location.Y + (Owner.Height - Height) / 2;
                Location = new Point(x, y);
            }

            txtUsername.Text = this.username;
        }

        private void tmrModalEffect_Tick(object sender, EventArgs e)
        {
            if (Opacity > 1)
            {
                tmrModalEffect.Stop();
            }
            else
            {
                Opacity += .04;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            isConfirmed = false;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            isConfirmed = false;
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            ConfirmPassword();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            lblPassChecker.Visible = false;
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConfirmPassword();
            }
        }
    }
}
