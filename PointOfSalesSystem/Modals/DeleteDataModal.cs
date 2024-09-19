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
    public partial class DeleteDataModal : Form
    {
        public bool IsConfirmed
        {
            get { return isConfirmed; }
        }

        private readonly string errorMessage;

        private bool isConfirmed = false;

        public DeleteDataModal(string errorMessage = null)
        {
            InitializeComponent();

            this.errorMessage = errorMessage;
        }

        private void CenterLabels()
        {
            int formCenterX = this.Width / 2;

            int labelTitleWidth = lblTitle.Width;
            int labelInfoWidth = lblInfo.Width;

            int labelTitleX = formCenterX - (labelTitleWidth / 2);
            int labelInfoX = formCenterX - (labelInfoWidth / 2);

            lblTitle.Location = new Point(labelTitleX, lblTitle.Location.Y);
            lblInfo.Location = new Point(labelInfoX, lblInfo.Location.Y);
        }

        private void DataInformation()
        {
            if (errorMessage != null)
            {
                lblTitle.Text = "Data cannot be deleted.";
                lblInfo.Text = errorMessage;

                btnDelete.Visible = false;

                btnCancel.Text = "Okay";
                btnCancel.Location = new Point(140, 182);

                CenterLabels();
            }
        }

        private void DeleteDataModal_Load(object sender, EventArgs e)
        {
            if (Owner != null)
            {
                int x = Owner.Location.X + (Owner.Width - Width) / 2;
                int y = Owner.Location.Y + (Owner.Height - Height) / 2;
                Location = new Point(x, y);
            }

            DataInformation();
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            isConfirmed = true;
            this.Close();
        }
    }
}
