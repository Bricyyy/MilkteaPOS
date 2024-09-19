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
    public partial class ResetSuccessfullyForm : Form
    {
        private readonly ForgotPassword forgotPassRef;
        private readonly string userEmail;

        private Timer countdownTimer;
        private int remainingSeconds = 5;

        public ResetSuccessfullyForm(ForgotPassword forgotPassRef, string userEmail)
        {
            InitializeComponent();

            this.forgotPassRef = forgotPassRef;
            this.userEmail = userEmail;

            InitializeTimer();
            StartCountdown();
        }

        private void BackToLogin()
        {
            forgotPassRef.Close();

            Login login = new Login();
            login.Show();
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
            countdownTimer.Start();
        }

        private void ResetSuccessfullyForm_Load(object sender, EventArgs e)
        {
            lblEmail.Text = this.userEmail;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            countdownTimer.Stop();
            BackToLogin();
        }

        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            remainingSeconds--;

            if (remainingSeconds >= 0)
            {
                lblTimer.Text = $"You will be redirected to Login in {remainingSeconds} seconds.";
            }
            else
            {
                countdownTimer.Stop();
                BackToLogin();
            }
        }
    }
}
