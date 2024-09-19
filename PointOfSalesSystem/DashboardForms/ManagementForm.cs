using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;
using System.Windows.Forms;
using ZstdSharp.Unsafe;

namespace PointOfSalesSystem
{
    public partial class ManagementForm : Form
    {
        public string username;

        public ManagementForm(string username)
        {
            InitializeComponent();

            this.username = username;
        }

        public void setCount()
        {
            FormUtilities.SetLabelCount(lblItemCount, DataAccess.GetEntityCount("items"));
            FormUtilities.SetLabelCount(lblCategoryCount, DataAccess.GetEntityCount("item_category"));
            FormUtilities.SetLabelCount(lblSupplierCount, DataAccess.GetEntityCount("supplier"));
            FormUtilities.SetLabelCount(lblUserCount, DataAccess.GetEntityCount("users"));
        }

        private void CenterPictureBoxInButton(PictureBox pictureBox, Guna.UI2.WinForms.Guna2Button button)
        {
            pictureBox.Parent = button;

            int padding = 18;
            int picX = button.Width - pictureBox.Width - padding;
            int picY = (button.Height - pictureBox.Height) / 2;
            pictureBox.Location = new Point(picX, picY);
        }

        private void CenterLabelInPictureBox(Label label, PictureBox pictureBox)
        {
            label.Parent = pictureBox;
            label.AutoSize = false;
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Dock = DockStyle.Fill;

            int labelX = (pictureBox.Width - label.Width) / 2;
            int labelY = (pictureBox.Height - label.Height) / 2;
            label.Location = new Point(labelX, labelY);
        }

        private void ManagementForm_Load(object sender, EventArgs e)
        {
            FormUtilities.setUserProfile(userPic, lblUsername, this.username);
            setCount();

            CenterPictureBoxInButton(picCountItem, btnItems);
            CenterPictureBoxInButton(picCountCategory, btnCategory);
            CenterPictureBoxInButton(picCountSuppliers, btnSupplier);
            CenterPictureBoxInButton(picCountUsers, btnUsers);

            CenterLabelInPictureBox(lblItemCount, picCountItem);
            CenterLabelInPictureBox(lblCategoryCount, picCountCategory);
            CenterLabelInPictureBox(lblSupplierCount, picCountSuppliers);
            CenterLabelInPictureBox(lblUserCount, picCountUsers);

            FormUtilities.UpdateButton(pnlMain, btnItems, picCountItem, Color.White, Color.FromArgb(176, 122, 50), Properties.Resources.count_background_selected, new ItemsForm(this));
        }

        private void btnItems_Click(object sender, EventArgs e)
        {
            FormUtilities.UpdateButton(pnlMain, btnItems, picCountItem, Color.White, Color.FromArgb(176, 122, 50), Properties.Resources.count_background_selected, new ItemsForm(this));

            FormUtilities.UpdateButton(pnlMain, btnCategory, picCountCategory, Color.FromArgb(176, 122, 50), Color.White, Properties.Resources.count_background_notselected);
            FormUtilities.UpdateButton(pnlMain, btnSupplier, picCountSuppliers, Color.FromArgb(176, 122, 50), Color.White, Properties.Resources.count_background_notselected);
            FormUtilities.UpdateButton(pnlMain, btnUsers, picCountUsers, Color.FromArgb(176, 122, 50), Color.White, Properties.Resources.count_background_notselected);
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            FormUtilities.UpdateButton(pnlMain, btnCategory, picCountCategory, Color.White, Color.FromArgb(176, 122, 50), Properties.Resources.count_background_selected, new CategoryForm(this));

            FormUtilities.UpdateButton(pnlMain, btnItems, picCountItem, Color.FromArgb(176, 122, 50), Color.White, Properties.Resources.count_background_notselected);
            FormUtilities.UpdateButton(pnlMain, btnSupplier, picCountSuppliers, Color.FromArgb(176, 122, 50), Color.White, Properties.Resources.count_background_notselected);
            FormUtilities.UpdateButton(pnlMain, btnUsers, picCountUsers, Color.FromArgb(176, 122, 50), Color.White, Properties.Resources.count_background_notselected);
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            FormUtilities.UpdateButton(pnlMain, btnSupplier, picCountSuppliers, Color.White, Color.FromArgb(176, 122, 50), Properties.Resources.count_background_selected, new SuppliersForm(this));

            FormUtilities.UpdateButton(pnlMain, btnCategory, picCountCategory, Color.FromArgb(176, 122, 50), Color.White, Properties.Resources.count_background_notselected);
            FormUtilities.UpdateButton(pnlMain, btnItems, picCountItem, Color.FromArgb(176, 122, 50), Color.White, Properties.Resources.count_background_notselected);
            FormUtilities.UpdateButton(pnlMain, btnUsers, picCountUsers, Color.FromArgb(176, 122, 50), Color.White, Properties.Resources.count_background_notselected);
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            FormUtilities.UpdateButton(pnlMain, btnUsers, picCountUsers, Color.White, Color.FromArgb(176, 122, 50), Properties.Resources.count_background_selected, new UsersForm(this));

            FormUtilities.UpdateButton(pnlMain, btnCategory, picCountCategory, Color.FromArgb(176, 122, 50), Color.White, Properties.Resources.count_background_notselected);
            FormUtilities.UpdateButton(pnlMain, btnSupplier, picCountSuppliers, Color.FromArgb(176, 122, 50), Color.White, Properties.Resources.count_background_notselected);
            FormUtilities.UpdateButton(pnlMain, btnItems, picCountItem, Color.FromArgb(176, 122, 50), Color.White, Properties.Resources.count_background_notselected);
        }
    }
}
