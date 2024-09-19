using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PointOfSalesSystem
{
    public partial class BackupRestore : Form
    {
        private string connectionString;
        private SqlConnection conn;
        private SqlCommand cmd;
        private SqlDataReader dr;

        public BackupRestore()
        {
            InitializeComponent();
        }

        private void ServerConnect()
        {
            try
            {
                connectionString = $"Server={txtServer.Text};User Id={txtUsername.Text};Password={txtPassword.Text};";
                conn = new SqlConnection(connectionString);
                conn.Open();

                cmd = new SqlCommand("SELECT name FROM sys.databases", conn);
                dr = cmd.ExecuteReader();

                cmbDatabase.Items.Clear();

                while (dr.Read())
                {
                    cmbDatabase.Items.Add(dr["name"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn?.Close();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();

            Login login = new Login();
            login.Show();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            ServerConnect();
        }
    }
}
