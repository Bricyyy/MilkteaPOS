using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;

namespace PointOfSalesSystem
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void sendLogin()
        {
            try
            {
                string username = txtUsername.Text;
                string password = txtPassword.Text;

                List<User> userList = DataAccess.GetUsers($"SELECT * FROM users WHERE username = '{username}'");

                if (userList.Count > 0)
                {
                    User user = userList[0];

                    bool passwordMatch = BCrypt.Net.BCrypt.Verify(password, user.Password);

                    if (user.Username == username && passwordMatch)
                    {
                        this.Hide();

                        Dashboard dashboard = new Dashboard(username);
                        dashboard.Show();
                    }
                    else
                    {
                        txtPassword.Text = "";
                        lblChecker.Visible = true;
                    }
                }
                else
                {
                    txtPassword.Text = "";
                    lblChecker.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            sendLogin();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            lblChecker.Visible = false;
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                sendLogin();
            }
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            lblChecker.Visible = false;
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                sendLogin();
            }
        }

        private void chkShowPass_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowPass.Checked)
            {
                txtPassword.IconRight = Properties.Resources.icons8_eye_96;
                txtPassword.UseSystemPasswordChar = false;
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.IconRight = Properties.Resources.icons8_closed_eye_96;
                txtPassword.UseSystemPasswordChar = true;
                txtPassword.PasswordChar = '\u25CF';
            }
        }

        private void lblBackupRestore_Click(object sender, EventArgs e)
        {
            this.Hide();

            BackupRestore backuprestore = new BackupRestore();
            backuprestore.Show();
        }

        private void lblForgotPass_Click(object sender, EventArgs e)
        {
            this.Hide();

            ForgotPassword forgotPassword = new ForgotPassword();
            forgotPassword.Show();
        }
    }
}
