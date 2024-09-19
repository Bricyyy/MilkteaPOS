using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PointOfSalesSystem.ResetPassForms
{
    public partial class ResetPassForm : Form
    {
        private readonly ForgotPassword forgotPassRef;
        private readonly string userEmail;
        private readonly string generatedOTP;

        public ResetPassForm(ForgotPassword forgotPassRef, string userEmail, string generatedOTP)
        {
            InitializeComponent();
            this.forgotPassRef = forgotPassRef;
            this.userEmail = userEmail;
            this.generatedOTP = generatedOTP;
        }

        private void UpdateUserData()
        {
            int userIdToUpdate = DataAccess.GetUserIdByEmail(this.userEmail);
            string newPassword = txtNewPass.Text;

            //MessageBox.Show($"User ID: {userIdToUpdate}\nNew Password: {newPassword}");

            if(DataAccess.UpdateUser(userIdToUpdate, newPassword))
            {
                forgotPassRef.loadForm(new ResetSuccessfullyForm(forgotPassRef, userEmail));
            }
        }

        private void SetNewPassword()
        {
            bool isPasswordEmpty = DataChecker.IsTextboxEmpty(txtNewPass);
            bool isConfirmPasswordEmpty = DataChecker.IsTextboxEmpty(txtConfirmPass);

            if (isPasswordEmpty || isConfirmPasswordEmpty)
            {
                if (isPasswordEmpty)
                {
                    DataChecker.HandleError(lblChecker, "Fill out the field.");
                }

                if (isConfirmPasswordEmpty)
                {
                    DataChecker.HandleError(lblChecker, "Fill out the field.");
                }

                return;
            }

            if (DataChecker.IsPasswordDifferent(txtNewPass, txtConfirmPass))
            {
                DataChecker.HandleError(lblChecker, "Password does not match.");
                return;
            }

            UpdateUserData();

            lblChecker.Visible = false;

            txtNewPass.Text = null;
            txtConfirmPass.Text = null;
        }

        private void ResetPassForm_Load(object sender, EventArgs e)
        {
            forgotPassRef.SetStepIndicator(3);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            forgotPassRef.loadForm(new VerifyEmailForm(forgotPassRef, userEmail, generatedOTP));
        }

        private void txtNewPass_TextChanged(object sender, EventArgs e)
        {
            lblChecker.Visible = false;
        }

        private void txtConfirmPass_TextChanged(object sender, EventArgs e)
        {
            lblChecker.Visible = false;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            SetNewPassword();
        }
    }
}
