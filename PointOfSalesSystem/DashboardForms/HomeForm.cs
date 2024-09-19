using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.WinForms;

namespace PointOfSalesSystem
{
    public partial class HomeForm : Form
    {
        private readonly string username;

        public HomeForm(string username)
        {
            InitializeComponent();

            this.username = username;
        }

        private void HomeForm_Load(object sender, EventArgs e)
        {
            FormUtilities.setUserProfile(userPic, lblUsername, this.username);
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
