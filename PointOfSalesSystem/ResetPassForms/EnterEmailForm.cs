using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace PointOfSalesSystem.ResetPassForms
{
    public partial class EnterEmailForm : Form
    {
        private readonly ForgotPassword forgotPassRef;

        public EnterEmailForm(ForgotPassword forgotPassRef)
        {
            InitializeComponent();

            this.forgotPassRef = forgotPassRef;
        }

        private void SendCode()
        {
            string email = txtEmail.Text.Trim();

            if (!string.IsNullOrEmpty(email))
            {
                if (DataChecker.DoesEmailExistInUsers(email))
                {
                    forgotPassRef.loadForm(new VerifyEmailForm(forgotPassRef, email, FormUtilities.GenerateOTP(email)));
                }
                else
                {
                    ShowErrorMessage("Email does not exist.");
                }
            }
            else
            {
                ShowErrorMessage("Email cannot be empty.");
            }
        }

        private void ShowErrorMessage(string message)
        {
            lblChecker.Text = message;
            lblChecker.Visible = true;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            forgotPassRef.Close();

            Login login = new Login();
            login.Show();
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            lblChecker.Visible = false;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            SendCode();
        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendCode();
            }
        }
    }
}
