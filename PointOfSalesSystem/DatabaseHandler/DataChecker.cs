using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace PointOfSalesSystem
{
    public class DataChecker
    {
        public static void HandleError(PictureBox pictureBox, Label title, Label label, string errorMessage)
        {
            AddAnimation.EditInfo(pictureBox, title, Properties.Resources.edit_cross_animated, "Error Saving!");
            label.Text = errorMessage;
            label.Visible = true;
        }

        public static void HandleError(Label label, string errorMessage)
        {
            label.Text = errorMessage;
            label.Visible = true;
        }

        public static bool HasDuplicate(string tableName, string idColumn, string searchColumn, string data, int dataID)
        {
            string query = $"SELECT COUNT(*) FROM {tableName} WHERE {searchColumn} = @Data AND {idColumn} != @DataID";

            int count = 0;

            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Data", data);
                    cmd.Parameters.AddWithValue("@DataID", dataID);

                    try
                    {
                        conn.Open();
                        count = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("An error occurred: " + ex.Message);
                    }
                }
            }

            return count > 0;
        }

        public static bool HasDuplicate(string tableName, string searchColumn, string data)
        {
            string query = $"SELECT COUNT(*) FROM {tableName} WHERE {searchColumn} = @Data";

            int count = 0;

            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Data", data);

                    try
                    {
                        conn.Open();
                        count = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("An error occurred: " + ex.Message);
                    }
                }
            }

            return count > 0;
        }

        public static bool DoesItemIdExistInTables(int itemID)
        {
            string combinedQuery = @"
                                    SELECT 'sales' AS TableName, COUNT(*) AS Count
                                    FROM sales 
                                    WHERE Item_Id = @itemID
                                    UNION ALL
                                    SELECT 'delivery' AS TableName, COUNT(*) AS Count
                                    FROM delivery 
                                    WHERE Item_Id = @itemID";

            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(combinedQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@itemID", itemID);

                    try
                    {
                        conn.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int count = Convert.ToInt32(reader["Count"]);
                                if (count > 0)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            return false;
        }

        public static bool DoesCategoryIdExistInItems(int categoryId)
        {
            string query = $"SELECT COUNT(*) FROM items WHERE Category_Id = {categoryId}";

            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return false;
                    }
                }
            }
        }

        public static bool DoesSupplierIdExistInDelivery(int supplierID)
        {
            string query = $"SELECT COUNT(*) FROM delivery WHERE Supplier_Id = {supplierID}";

            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return false;
                    }
                }
            }
        }

        public static bool DoesUserIdExistInSales(int userID)
        {
            string query = $"SELECT COUNT(*) FROM sales WHERE User_Id = {userID}";

            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return false;
                    }
                }
            }
        }

        public static bool DoesEmailExistInUsers(string email)
        {
            string query = "SELECT COUNT(*) FROM users WHERE emailAddress = @Email";

            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@Email", email);

                        conn.Open();
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return false;
                    }
                }
            }
        }


        public static bool IsTextboxEmpty(Guna.UI2.WinForms.Guna2TextBox textBox)
        {
            return string.IsNullOrWhiteSpace(textBox.Text);
        }

        public static bool IsComboboxEmpty(Guna.UI2.WinForms.Guna2ComboBox comboBox)
        {
            return comboBox.SelectedIndex == -1;
        }

        public static bool IsPasswordDifferent(Guna.UI2.WinForms.Guna2TextBox password, Guna.UI2.WinForms.Guna2TextBox confirmpassword)
        {
            return password.Text != confirmpassword.Text;
        }

        public static bool IsAnyRadioButtonChecked(Guna.UI2.WinForms.Guna2RadioButton radioUser, Guna.UI2.WinForms.Guna2RadioButton radioAdmin)
        {
            return radioUser.Checked || radioAdmin.Checked;
        }

        public static bool IsImageDefault(Guna.UI2.WinForms.Guna2CirclePictureBox pictureBox, Image image)
        {
            System.Drawing.Image defaultImage = image;

            if (pictureBox.Image != null && defaultImage != null)
            {
                try
                {
                    using (MemoryStream msPictureBox = new MemoryStream(), msDefaultImage = new MemoryStream())
                    {
                        if (pictureBox.Image.RawFormat.Guid != defaultImage.RawFormat.Guid)
                            return false;

                        pictureBox.Image.Save(msPictureBox, pictureBox.Image.RawFormat);
                        defaultImage.Save(msDefaultImage, defaultImage.RawFormat);

                        byte[] bytesPictureBox = msPictureBox.ToArray();
                        byte[] bytesDefaultImage = msDefaultImage.ToArray();

                        return bytesPictureBox.SequenceEqual(bytesDefaultImage);
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return false;
        }

        public static bool IsImageSizeTooLarge(Guna.UI2.WinForms.Guna2CirclePictureBox pictureBox)
        {
            // Assuming max size for MEDIUMBLOB is 16MB (in bytes)
            long maxByteSize = 1 * 1024 * 1024; // 16 MB in bytes

            if (pictureBox.Image != null)
            {
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    // Save the image to a MemoryStream to calculate its size
                    pictureBox.Image.Save(ms, pictureBox.Image.RawFormat);

                    // Check if the image size exceeds the max allowed size
                    if (ms.Length > maxByteSize)
                    {
                        return true; // Image size is too large
                    }
                }
            }

            return false; // Image size is within limits
        }

        public static bool IsImageSizeTooLarge(byte[] pictureBoxBytes)
        {
            // Assuming max size for MEDIUMBLOB is 16MB (in bytes)
            long maxByteSize = 1 * 1024 * 1024; // 16 MB in bytes

            if (pictureBoxBytes != null)
            {
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream(pictureBoxBytes))
                {
                    // Check if the image size exceeds the max allowed size
                    if (ms.Length > maxByteSize)
                    {
                        return true; // Image size is too large
                    }
                }
            }

            return false; // Image size is within limits or image is null
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidPrice(string text)
        {
            string pattern = @"^\d+(\.\d{1,2})?$";

            Regex regex = new Regex(pattern);

            return regex.IsMatch(text);
        }
    }
}
