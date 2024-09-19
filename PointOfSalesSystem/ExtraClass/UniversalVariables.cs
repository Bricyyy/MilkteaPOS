using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PointOfSalesSystem
{

    public static class UniversalVariables
    {
        public static DataTable dataTable = new DataTable();
        public static MySqlConnection conn = new MySqlConnection();
        public static DataSet ds = new DataSet();
        public static MySqlCommand cmd = new MySqlCommand();
        public static MySqlDataReader dr;
        public static string sql;

        public static string ConnectionString = "server=localhost;username=bryan;port=3307;password=0102bryan;database=posdb";

        public static void connect()
        {
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            conn.Open();
            dr = cmd.ExecuteReader();
        }
    }   
}