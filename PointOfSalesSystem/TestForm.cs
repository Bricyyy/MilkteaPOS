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
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            string categoryQuery = "SELECT Category_Id, Category_Name FROM item_category";
            List<Category> categories = DataAccess.GetCategories(categoryQuery);

            cmbFilter.DataSource = categories;

            cmbFilter.DisplayMember = "CategoryName";
            cmbFilter.ValueMember = "CategoryId";
        }
    }
}
