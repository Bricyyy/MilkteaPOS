using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;

namespace PointOfSalesSystem
{
    class DataGridViewPopulator
    {
        public static DataTable dataTable;

        public static void setDataTable(string query)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        conn.Open();

                        dataTable = new DataTable();

                        using (MySqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dataTable.Load(dr);
                            }
                        }
                        conn.Close();
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void SetItemData(DataGridView dataGridView)
        {
            dataGridView.AutoGenerateColumns = false;
            dataGridView.DataSource = dataTable;

            UpdateOrInsertColumn(dataGridView, "itemIDColumn", "ID", "Item_Id", GetColumnIndex(dataGridView, "itemIDColumn"));
            UpdateOrInsertColumn(dataGridView, "imageColumn", "Image", "Item_Image", GetColumnIndex(dataGridView, "imageColumn"));
            UpdateOrInsertColumn(dataGridView, "itemnameColumn", "Item Name", "Item_Name", GetColumnIndex(dataGridView, "itemnameColumn"));
            UpdateOrInsertColumn(dataGridView, "categorynameColumn", "Category", "Category_Name", GetColumnIndex(dataGridView, "categorynameColumn"));
            UpdateOrInsertColumn(dataGridView, "itempriceColumn", "Base Price", "Base_Price", GetColumnIndex(dataGridView, "itempriceColumn"));
        }

        public static void SetCategoryData(DataGridView dataGridView)
        {
            dataGridView.AutoGenerateColumns = false;
            dataGridView.DataSource = dataTable;

            UpdateOrInsertColumn(dataGridView, "categoryIDColumn", "ID", "Category_Id", GetColumnIndex(dataGridView, "categoryIDColumn"));
            UpdateOrInsertColumn(dataGridView, "categorynameColumn", "Category Name", "Category_Name", GetColumnIndex(dataGridView, "categorynameColumn"));
        }

        public static void SetSupplierData(DataGridView dataGridView)
        {
            dataGridView.AutoGenerateColumns = false;
            dataGridView.DataSource = dataTable;

            UpdateOrInsertColumn(dataGridView, "supplierIDColumn", "ID", "Supplier_Id", GetColumnIndex(dataGridView, "supplierIDColumn"));
            UpdateOrInsertColumn(dataGridView, "suppliernameColumn", "Supplier Name", "Supplier_Name", GetColumnIndex(dataGridView, "suppliernameColumn"));
            UpdateOrInsertColumn(dataGridView, "addressColumn", "Address", "Address", GetColumnIndex(dataGridView, "addressColumn"));
            UpdateOrInsertColumn(dataGridView, "contactColumn", "Contact No.", "Contact_Number", GetColumnIndex(dataGridView, "contactColumn"));
        }

        public static void SetUserData(DataGridView dataGridView)
        {
            dataGridView.AutoGenerateColumns = false;
            dataGridView.DataSource = dataTable;

            UpdateOrInsertColumn(dataGridView, "userIDColumn", "ID", "userID", GetColumnIndex(dataGridView, "userIDColumn"));
            UpdateOrInsertColumn(dataGridView, "imageColumn", "Image", "userImage", GetColumnIndex(dataGridView, "imageColumn"));
            UpdateOrInsertColumn(dataGridView, "usernameColumn", "Username", "username", GetColumnIndex(dataGridView, "usernameColumn"));
            UpdateOrInsertColumn(dataGridView, "emailColumn", "Email", "emailAddress", GetColumnIndex(dataGridView, "emailColumn"));
            UpdateOrInsertColumn(dataGridView, "roleColumn", "Role", "role", GetColumnIndex(dataGridView, "roleColumn"));
        }

        private static int GetColumnIndex(DataGridView dataGridView, string columnName)
        {
            if (dataGridView.Columns.Contains(columnName))
            {
                return dataGridView.Columns[columnName].Index;
            }
            return -1;
        }

        private static void UpdateOrInsertColumn(DataGridView dataGridView, string columnName, string headerText, string dataPropertyName, int columnIndex)
        {
            if (dataGridView.Columns.Contains(columnName))
            {
                dataGridView.Columns.Remove(columnName);
            }

            DataGridViewColumn column;

            if (headerText == "Image")
            {
                column = new DataGridViewImageColumn
                {
                    DataPropertyName = dataPropertyName,
                    HeaderText = headerText,
                    Name = columnName
                };
            }
            else
            {
                column = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = dataPropertyName,
                    HeaderText = headerText,
                    Name = columnName
                };
                column.DefaultCellStyle.Font = new Font("Proxima Nova Rg", 10, FontStyle.Regular);
            }

            if (headerText == "ID")
            {
                column.Visible = false;
            }

            if (columnIndex != -1)
            {
                dataGridView.Columns.Insert(columnIndex, column);
            }
            else
            {
                dataGridView.Columns.Add(column);
            }
        }

        private static Image ResizeImage(Image imgToResize, int width, int height)
        {
            Bitmap bitmap = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(imgToResize, 0, 0, width, height);
            }
            return bitmap;
        }

        public static void headerFormat(DataGridView dgvData)
        {
            dgvData.Columns["selectData"].Width = 30;
            dgvData.Columns["editData"].Width = 30;
            dgvData.Columns["deleteData"].Width = 30;

            dgvData.Columns["editData"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvData.Columns["deleteData"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            if (dgvData.Columns.Contains("imageColumn"))
            {
                dgvData.Columns["imageColumn"].Width = 65;
                dgvData.Columns["imageColumn"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        public static void SetRowNumber(DataGridView dgv, string columnName)
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                dgv.Rows[i].Cells[columnName].Value = (i + 1).ToString();
            }
        }

        public static void FormatImageColumn(DataGridViewCellFormattingEventArgs e, DataGridView dgv, string columnName1 = null, string columnName2 = null)
        {
            if (dgv.Columns[e.ColumnIndex] is DataGridViewImageColumn && e.RowIndex >= 0)
            {
                if (dgv.Columns[e.ColumnIndex].Name == columnName1 || dgv.Columns[e.ColumnIndex].Name == columnName2)
                {
                    int imageSize = 20;

                    if (e.Value != null && e.Value is Image)
                    {
                        Image img = (Image)e.Value;
                        e.Value = ResizeImage(img, imageSize, imageSize);
                        e.FormattingApplied = true;
                    }
                }
            }
        }

        public static void FormatImageColumn(DataGridViewCellFormattingEventArgs e, DataGridView dgv, Image defaultImage, string columnName1 = null)
        {
            if (dgv.Columns[e.ColumnIndex] is DataGridViewImageColumn imageColumn &&
                e.RowIndex >= 0 &&
                imageColumn.Name == columnName1)
            {
                int imageSize = 30;

                if (e.Value is byte[] imageData)
                {
                    try
                    {
                        using (MemoryStream ms = new MemoryStream(imageData))
                        {
                            Image img = Image.FromStream(ms);
                            e.Value = ResizeImage(FormUtilities.CropToCircle(ResizeImage(img, imageSize, imageSize)), imageSize, imageSize);
                            e.FormattingApplied = true;
                        }
                    }
                    catch (ArgumentException ex)
                    {
                        MessageBox.Show("Invalid image data: " + ex.Message);
                    }
                }
                else if (e.Value is Image imageValue)
                {
                    e.Value = ResizeImage(FormUtilities.CropToCircle(ResizeImage(imageValue, imageSize, imageSize)), imageSize, imageSize);
                    e.FormattingApplied = true;
                }
                else if (e.Value == DBNull.Value || e.Value == null)
                {
                    e.Value = ResizeImage(FormUtilities.CropToCircle(ResizeImage(defaultImage, imageSize, imageSize)), imageSize, imageSize);
                    e.FormattingApplied = true;
                }
            }
        }

        public static void SetCursorOnColumns(DataGridView dgv, string columnName1, string columnName2)
        {
            dgv.CellMouseEnter += (sender, e) =>
            {
                if (e.ColumnIndex == dgv.Columns[columnName1].Index || e.ColumnIndex == dgv.Columns[columnName2].Index)
                {
                    dgv.Cursor = Cursors.Hand;
                }
                else
                {
                    dgv.Cursor = Cursors.Default;
                }
            };

            dgv.CellMouseLeave += (sender, e) =>
            {
                dgv.Cursor = Cursors.Default;
            };
        }
    }
}
