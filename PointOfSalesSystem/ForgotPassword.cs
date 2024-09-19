using PointOfSalesSystem.ExtraClass;
using PointOfSalesSystem.ResetPassForms;
using PointOfSalesSystem.SalesForms;
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
    public partial class ForgotPassword : Form
    {
        public ForgotPassword()
        {
            InitializeComponent();
        }

        public void loadForm(Form newForm)
        {
            FormUtilities.LoadForm(pnlMain, newForm);
        }

        public void SetStepIndicator(int step)
        {
            PictureBox[] stepPictures = { picNumber1, picNumber2, picNumber3 };
            Label[] stepLabels = { lblEnterEmail, lblVerifyEmail, lblResetPassword };

            StepIndicator stepIndicator = new StepIndicator(stepPictures, stepLabels);
            stepIndicator.SetStep(step);
        }

        private void ForgotPassword_Load(object sender, EventArgs e)
        {
            loadForm(new EnterEmailForm(this));

            SetStepIndicator(1);
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
