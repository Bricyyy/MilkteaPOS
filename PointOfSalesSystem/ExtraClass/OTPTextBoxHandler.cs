using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using PointOfSalesSystem.ResetPassForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PointOfSalesSystem.ExtraClass
{
    public class OTPTextBoxHandler
    {
        private readonly ForgotPassword forgotPassRef;
        private readonly VerifyEmailForm verifyEmailRef;
        private readonly string generatedOTP;
        private readonly Guna.UI2.WinForms.Guna2TextBox[] otpTextBoxes;

        public OTPTextBoxHandler(ForgotPassword forgotPassRef, string generatedOTP, VerifyEmailForm verifyEmailRef, Guna.UI2.WinForms.Guna2TextBox[] textBoxes)
        {
            this.forgotPassRef = forgotPassRef;
            this.generatedOTP = generatedOTP;
            this.verifyEmailRef = verifyEmailRef;

            if (textBoxes.Length != 6)
                throw new ArgumentException("There should be 6 TextBoxes for OTP input.");

            otpTextBoxes = textBoxes;

            // Attach the TextChanged event handler to each TextBox
            for (int i = 0; i < otpTextBoxes.Length; i++)
            {
                otpTextBoxes[i].MaxLength = 1;  // Set max length to 1
                otpTextBoxes[i].TextChanged += OTPTextBox_TextChanged;
                otpTextBoxes[i].KeyDown += OTPTextBox_KeyDown;
                otpTextBoxes[i].KeyPress += OTPTextBox_KeyPress;
                otpTextBoxes[i].Tag = i;  // Store the index in the Tag property
            }
        }

        private bool AreAllTextBoxesFilled()
        {
            foreach (Guna.UI2.WinForms.Guna2TextBox textBox in otpTextBoxes)
            {
                if (string.IsNullOrEmpty(textBox.Text) || !char.IsDigit(textBox.Text[0]))
                {
                    return false;
                }
            }

            return true;
        }

        private string GetAllNumbers()
        {
            StringBuilder result = new StringBuilder();

            foreach (Guna.UI2.WinForms.Guna2TextBox textBox in otpTextBoxes)
            {
                if (!string.IsNullOrEmpty(textBox.Text) && char.IsDigit(textBox.Text[0]))
                {
                    result.Append(textBox.Text[0]);
                }
            }

            return result.ToString();
        }

        private void OTPTextBox_TextChanged(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2TextBox currentTextBox = (Guna.UI2.WinForms.Guna2TextBox)sender;
            int currentIndex = (int)currentTextBox.Tag;

            verifyEmailRef.lblChecker.Visible = false;

            // Check if the current TextBox contains a number
            if (!string.IsNullOrEmpty(currentTextBox.Text))
            {
                // Move focus to the next available TextBox or set the new number to the next available TextBox if available
                for (int i = currentIndex + 1; i < otpTextBoxes.Length; i++)
                {
                    if (string.IsNullOrEmpty(otpTextBoxes[i].Text))
                    {
                        otpTextBoxes[i].Focus();
                        return; // Exit the loop once focus is set to the next available TextBox
                    }
                }
            }
            else
            {
                // Move focus to the previous TextBox if available
                if (currentIndex > 0)
                {
                    otpTextBoxes[currentIndex - 1].Focus();
                }
            }

            if (AreAllTextBoxesFilled())
            {
                string enteredOTP = GetAllNumbers();

                if (this.generatedOTP == enteredOTP)
                {
                    this.forgotPassRef.loadForm(new ResetPassForm(forgotPassRef, verifyEmailRef.lblEmail.Text, generatedOTP));
                }
                else
                {
                    this.verifyEmailRef.lblChecker.Visible = true;
                }
            }
        }

        private void OTPTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            Guna.UI2.WinForms.Guna2TextBox currentTextBox = (Guna.UI2.WinForms.Guna2TextBox)sender;
            int currentIndex = (int)currentTextBox.Tag;

            if (e.KeyCode == Keys.Back && string.IsNullOrEmpty(currentTextBox.Text))
            {
                // Backspace key is pressed in an empty TextBox, move focus to the previous TextBox
                if (currentIndex > 0)
                {
                    otpTextBoxes[currentIndex - 1].Focus();
                }
            }
        }

        private void OTPTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only numeric input, backspace, and enter
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != '\r')
            {
                e.Handled = true;
            }
            else if (char.IsDigit(e.KeyChar))
            {
                Guna.UI2.WinForms.Guna2TextBox currentTextBox = (Guna.UI2.WinForms.Guna2TextBox)sender;
                int currentIndex = (int)currentTextBox.Tag;

                // Move focus to the next available TextBox or set the new number to the next available TextBox if available
                for (int i = currentIndex + 1; i < otpTextBoxes.Length; i++)
                {
                    if (string.IsNullOrEmpty(otpTextBoxes[i].Text))
                    {
                        otpTextBoxes[i].Focus();
                        return; // Exit the loop once focus is set to the next available TextBox
                    }
                }
            }
        }
    }
}
