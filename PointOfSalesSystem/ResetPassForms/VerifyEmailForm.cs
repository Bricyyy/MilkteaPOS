using PointOfSalesSystem.ExtraClass;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace PointOfSalesSystem.ResetPassForms
{
    public partial class VerifyEmailForm : Form
    {
        private readonly ForgotPassword forgotPassRef;
        private readonly string userEmail;

        private string generatedOTP;
        private Timer countdownTimer;
        private int remainingSeconds = 30;

        public VerifyEmailForm(ForgotPassword forgotPassRef, string userEmail, string generatedOTP)
        {
            InitializeComponent();

            this.forgotPassRef = forgotPassRef;
            this.userEmail = userEmail;
            this.generatedOTP = generatedOTP;

            InitializeTimer();
            StartCountdown();
        }

        private void setCodeTextBox()
        {
            Guna.UI2.WinForms.Guna2TextBox[] otpTextBoxes = { txtCode1, txtCode2, txtCode3, txtCode4, txtCode5, txtCode6 };
            _ = new OTPTextBoxHandler(forgotPassRef, this.generatedOTP, this, otpTextBoxes);
        }

        private void InitializeTimer()
        {
            countdownTimer = new Timer
            {
                Interval = 1000 // 1 second interval
            };
            countdownTimer.Tick += CountdownTimer_Tick;
        }

        private void StartCountdown()
        {
            remainingSeconds = 30;
            countdownTimer.Start();
        }

        private void ResendCode()
        {
            this.generatedOTP = FormUtilities.GenerateOTP(this.userEmail);
            setCodeTextBox();

            lblQuestion.Visible = false;
            lblResend.Visible = false;
            StartCountdown();
        }

        private void VerifyEmailForm_Load(object sender, EventArgs e)
        {
            forgotPassRef.SetStepIndicator(2);

            lblEmail.Text = userEmail;

            setCodeTextBox();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            forgotPassRef.loadForm(new EnterEmailForm(forgotPassRef));
        }

        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            remainingSeconds--;

            if (remainingSeconds >= 0)
            {
                lblTimer.Text = $"Please wait {remainingSeconds} seconds to resend.";
            }
            else
            {
                // Timer has reached 0, stop the timer and show the relevant controls
                countdownTimer.Stop();
                lblQuestion.Visible = true;
                lblResend.Visible = true;
            }
        }

        private void lblResend_Click(object sender, EventArgs e)
        {
            ResendCode();
        }
    }
}
