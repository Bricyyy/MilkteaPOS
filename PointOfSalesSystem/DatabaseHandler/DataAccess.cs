using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Drawing;
using BCrypt.Net;
using System.Windows.Forms;
using PointOfSalesSystem.GetOrSet;
using System.Data.SqlClient;
using System.Data;

namespace PointOfSalesSystem
{
    public class DataAccess
    {
        public static object ExecuteScalar(string query)
        {
            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                return cmd.ExecuteScalar();
            }
        }

        public static int ExecuteNonQuery(string query)
        {
            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                return cmd.ExecuteNonQuery();
            }
        }

        public static int GetEntityCount(string tableName, string id = null, string columnName = null)
        {
            int count = 0;
            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                string query;
                
                if (id != null && columnName != null)
                {
                    query = $"SELECT COUNT(*) FROM {tableName} WHERE {columnName} = {id}";
                }
                else
                {
                    query = $"SELECT COUNT(*) FROM {tableName}";
                }
                
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        count = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            return count;
        }

        public static int GetCategoryItemCount(int categoryId)
        {
            string itemCountQuery = $"SELECT COUNT(*) AS ItemCount FROM items WHERE Category_Id = {categoryId}";

            int count = 0;

            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(itemCountQuery, conn))
                {
                    try
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            count = Convert.ToInt32(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

            return count;
        }

        public static DataTable GetDataAsDataTable(string query)
        {
            DataTable dataTable = new DataTable();

            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dataTable);
            }

            return dataTable;
        }

        public static int GetUserIdByEmail(string email)
        {
            int userId = -1; // Default value if user ID is not found

            string query = "SELECT userID FROM users WHERE emailAddress = @Email";

            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();

                        cmd.Parameters.AddWithValue("@Email", email);

                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            userId = Convert.ToInt32(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

            return userId;
        }


        public static List<Item> GetItems(string query)
        {
            List<Item> itemList = new List<Item>();

            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Item item = new Item
                                {
                                    ItemId = Convert.ToInt32(reader["Item_Id"]),
                                    ItemImage = reader["Item_Image"] != DBNull.Value ? (byte[])reader["Item_Image"] : null,
                                    ItemName = reader["Item_Name"].ToString(),
                                    CategoryId = Convert.ToInt32(reader["Category_Id"]),
                                    BasePrice = Convert.ToDecimal(reader["Base_Price"])
                                };

                                itemList.Add(item);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

            return itemList;
        }

        public static List<Category> GetCategories(string query)
        {
            List<Category> categoryList = new List<Category>();

            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Category category = new Category
                                {
                                    CategoryId = Convert.ToInt32(reader["Category_Id"]),
                                    CategoryName = reader["Category_Name"].ToString()
                                };

                                categoryList.Add(category);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

            return categoryList;
        }

        public static List<Supplier> GetSuppliers(string query)
        {
            List<Supplier> supplierList = new List<Supplier>();

            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Supplier supplier = new Supplier
                                {
                                    SupplierId = Convert.ToInt32(reader["Supplier_Id"]),
                                    SupplierName = reader["Supplier_Name"].ToString(),
                                    SupplierAddress = reader["Address"].ToString(),
                                    SupplierContact = reader["Contact_Number"].ToString()
                                };

                                supplierList.Add(supplier);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

            return supplierList;
        }

        public static List<User> GetUsers(string query)
        {
            List<User> userList = new List<User>();

            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                User user = new User
                                {
                                    UserId = Convert.ToInt32(reader["userID"]),
                                    UserImage = reader["userImage"] != DBNull.Value ? (byte[])reader["userImage"] : null,
                                    Username = reader["username"].ToString(),
                                    Password = reader["password"].ToString(),
                                    Email = reader["emailAddress"].ToString(),
                                    Role = reader["role"].ToString()
                                };

                                userList.Add(user);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

            return userList;
        }

        public static List<Inventory> GetInventory(string query)
        {
            List<Inventory> inventoryList = new List<Inventory>();

            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Inventory category = new Inventory
                                {
                                    InventoryId = Convert.ToInt32(reader["inventory_id"]),
                                    ItemId = Convert.ToInt32(reader["Item_Id"]),
                                    Quantity = Convert.ToInt32(reader["Quantity"])
                                };

                                inventoryList.Add(category);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

            return inventoryList;
        }

        public static List<Delivery> GetDeliveries(string query)
        {
            List<Delivery> deliveryList = new List<Delivery>();

            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Delivery category = new Delivery
                                {
                                    DeliveryId = Convert.ToInt32(reader["Delivery_Id"]),
                                    DeliveryNo = reader["Delivery_No"].ToString(),
                                    DeliveryDate = reader.GetDateTime(reader.GetOrdinal("Delivery_Date")),
                                    ItemId = Convert.ToInt32(reader["Item_Id"]),
                                    Quantity = Convert.ToInt32(reader["Quantity"]),
                                    SupplierId = Convert.ToInt32(reader["Supplier_Id"])
                                };

                                deliveryList.Add(category);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

            return deliveryList;
        }

        public static List<Delivery> GetDeliveriesWithItems(string query)
        {
            List<Delivery> deliveryList = new List<Delivery>();

            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Delivery category = new Delivery
                                {
                                    DeliveryId = Convert.ToInt32(reader["Delivery_Id"]),
                                    DeliveryNo = reader["Delivery_No"].ToString(),
                                    DeliveryDate = reader.GetDateTime(reader.GetOrdinal("Delivery_Date")),
                                    ItemImage = reader["Item_Image"] != DBNull.Value ? (byte[])reader["Item_Image"] : null,
                                    ItemName = reader["Item_Name"].ToString(),
                                    ItemCategory = reader["Category_Name"].ToString(),
                                    Quantity = Convert.ToInt32(reader["Quantity"]),
                                    SupplierName = reader["Supplier_Name"].ToString()
                                };

                                deliveryList.Add(category);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

            return deliveryList;
        }

        public static List<Inventory> GetItemsFromInventory(string query)
        {
            List<Inventory> inventoryList = new List<Inventory>();

            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Inventory inventory = new Inventory
                                {
                                    InventoryId = Convert.ToInt32(reader["inventory_id"]),
                                    ItemId = Convert.ToInt32(reader["Item_Id"]),
                                    ItemImage = reader["Item_Image"] != DBNull.Value ? (byte[])reader["Item_Image"] : null,
                                    ItemName = reader["Item_Name"].ToString(),
                                    CategoryId = Convert.ToInt32(reader["Category_Id"]),
                                    BasePrice = Convert.ToDecimal(reader["Base_Price"]),
                                    Quantity = Convert.ToInt32(reader["Quantity"]),
                                };

                                inventoryList.Add(inventory);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

            return inventoryList;
        }

        public static List<Cart> GetCart(string query)
        {
            List<Cart> cartList = new List<Cart>();

            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Cart cart = new Cart
                                {
                                    CartId = Convert.ToInt32(reader["cart_id"]),
                                    UserId = Convert.ToInt32(reader["user_id"]),
                                    InventoryId = Convert.ToInt32(reader["inventory_id"]),
                                    Quantity = Convert.ToInt32(reader["item_quantity"]),
                                    Size = reader["item_size"].ToString(),
                                    Sugar = reader["item_sugar"].ToString(),
                                    Ice = reader["item_ice"].ToString()
                                };

                                cartList.Add(cart);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

            return cartList;
        }

        public static List<OrderSummary> GetOrderSummary(string query)
        {
            List<OrderSummary> orderList = new List<OrderSummary>();

            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                OrderSummary order = new OrderSummary
                                {
                                    CartId = Convert.ToInt32(reader["cart_id"]),
                                    InventoryId = Convert.ToInt32(reader["inventory_id"]),
                                    ItemAvailable = Convert.ToInt32(reader["Quantity"]),
                                    ItemId = Convert.ToInt32(reader["Item_Id"]),
                                    ItemImage = reader["Item_Image"] != DBNull.Value ? (byte[])reader["Item_Image"] : null,
                                    ItemName = reader["Item_Name"].ToString(),
                                    CategoryName = reader["Category_Name"].ToString(),
                                    Size = reader["item_size"].ToString(),
                                    Sugar = reader["item_sugar"].ToString(),
                                    Ice = reader["item_ice"].ToString(),
                                    Quantity = Convert.ToInt32(reader["item_quantity"]),
                                    BasePrice = Convert.ToDecimal(reader["Base_Price"])
                                };

                                orderList.Add(order);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

            return orderList;
        }

        public static int GetTotalQuantity(string query)
        {
            int totalQuantity = 0;

            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            totalQuantity = Convert.ToInt32(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

            return totalQuantity;
        }

        public static List<Sales> GetSales(string query)
        {
            List<Sales> saleList = new List<Sales>();

            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Sales sale = new Sales
                                {
                                    ReceiptId = Convert.ToInt32(reader["Receipt_Id"]),
                                    UserId = Convert.ToInt32(reader["User_Id"]),
                                    ReceiptNo = reader["Receipt_No"].ToString(),
                                    ReceiptDate = reader.IsDBNull(reader.GetOrdinal("Receipt_Date")) ? DateTime.MinValue : Convert.ToDateTime(reader["Receipt_Date"]),
                                    ItemId = Convert.ToInt32(reader["Item_Id"]),
                                    Quantity = Convert.ToInt32(reader["Quantity"]),
                                    ItemDesc = reader["Item_Desc"].ToString(),
                                    TotalAmount = Convert.ToDecimal(reader["Total_Amount"]),
                                    AmountPaid = Convert.ToDecimal(reader["Amount_Paid"]),
                                    TotalChange = Convert.ToDecimal(reader["Total_Change"])
                                };

                                saleList.Add(sale);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

            return saleList;
        }

        public static int GetLastSequenceNumberFromSales()
        {
            int lastSequenceNumber = 0;

            string query = "SELECT Receipt_No FROM sales ORDER BY Receipt_Id DESC LIMIT 1"; // Adjust your query to retrieve the latest receipt number

            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            string lastReceiptNo = result.ToString();

                            // Extract the sequence number from the last receipt number
                            string sequencePart = lastReceiptNo.Substring(4, 1); // Assuming the sequence part is the 5th character
                            lastSequenceNumber = int.Parse(sequencePart);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

            return lastSequenceNumber;
        }

        public static int GetLastSequenceNumberFromDelivery()
        {
            int lastSequenceNumber = 0;
            string query = "SELECT Delivery_No FROM delivery ORDER BY Delivery_Id DESC LIMIT 1";

            // Adjust your query to retrieve the latest delivery number
            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            string lastDeliveryNo = result.ToString();

                            // Extract the sequence number from the last delivery number
                            string sequencePart = lastDeliveryNo.Substring(0, 2); // Assuming the sequence part is the first 2 characters
                            lastSequenceNumber = int.Parse(sequencePart);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

            return lastSequenceNumber;
        }

        public static int GetAvailableQuantity(int inventoryId)
        {
            int availableQuantity = 0;
            string query = $"SELECT Quantity FROM inventory WHERE inventory_id = {inventoryId}";

            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            availableQuantity = Convert.ToInt32(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

            return availableQuantity;
        }

        public static List<string> GetDistinctReceiptNumbers(string query)
        {
            List<string> distinctReceipts = new List<string>();

            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string receiptNo = reader["Receipt_No"].ToString();
                                distinctReceipts.Add(receiptNo);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

            return distinctReceipts;
        }

        public static void InsertItem(byte[] itemImage, string itemName, int categoryId, decimal basePrice)
        {
            string insertItemQuery = "INSERT INTO items (Item_Image, Item_Name, Category_Id, Base_Price) VALUES (@ItemImage, @ItemName, @CategoryId, @BasePrice)";
            string insertInventoryQuery = "INSERT INTO inventory (Item_Id, Quantity) VALUES (@ItemId, 0)";

            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(insertItemQuery, conn))
                {
                    MySqlTransaction transaction = conn.BeginTransaction();
                    cmd.Transaction = transaction;

                    try
                    {
                        // Insert item into the items table
                        cmd.Parameters.Add("@ItemImage", MySqlDbType.MediumBlob).Value = (object)itemImage ?? DBNull.Value;
                        cmd.Parameters.AddWithValue("@ItemName", itemName);
                        cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                        cmd.Parameters.AddWithValue("@BasePrice", basePrice);
                        cmd.ExecuteNonQuery();

                        // Retrieve the last inserted Item_Id (auto-incremented)
                        long lastInsertedItemId = cmd.LastInsertedId;

                        // Insert into the inventory table with the retrieved Item_Id
                        MySqlCommand inventoryCmd = new MySqlCommand(insertInventoryQuery, conn);
                        inventoryCmd.Parameters.AddWithValue("@ItemId", lastInsertedItemId);
                        inventoryCmd.ExecuteNonQuery();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            transaction.Rollback();
                        }
                        catch (Exception rollbackEx)
                        {
                            MessageBox.Show("Rollback error: " + rollbackEx.Message);
                        }

                        MessageBox.Show("Transaction error: " + ex.Message);
                    }
                }
            }
        }

        public static void InsertCategory(string categoryName)
        {
            string query = "INSERT INTO item_category (Category_Name) VALUES (@CategoryName)";

            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CategoryName", categoryName);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        public static void InsertSupplier(string supplierName, string supplierAddress, string contactNumber)
        {
            string query = "INSERT INTO supplier (Supplier_Name, Address, Contact_Number) VALUES (@SupplierName, @Address, @ContactNumber)";

            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SupplierName", supplierName);
                    cmd.Parameters.AddWithValue("@Address", supplierAddress);
                    cmd.Parameters.AddWithValue("@ContactNumber", contactNumber);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        public static void InsertUser(byte[] userImage, string userName, string userPassword, string userEmail, string role)
        {
            string query = "INSERT INTO users (userImage, username, password, emailAddress, role) VALUES (@UserImage, @Username, @Password, @Email, @Role)";

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userPassword);

            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    MySqlParameter userImageParam = new MySqlParameter("@UserImage", MySqlDbType.MediumBlob)
                    {
                        Value = (object)userImage ?? DBNull.Value
                    };
                    cmd.Parameters.Add(userImageParam);
                    cmd.Parameters.AddWithValue("@Username", userName);
                    cmd.Parameters.AddWithValue("@Password", hashedPassword);
                    cmd.Parameters.AddWithValue("@Email", userEmail);
                    cmd.Parameters.AddWithValue("@Role", role);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        public static void InsertCart(int userID, int inventoryID, int quantity, string size, string sugar, string ice)
        {
            string query = "INSERT INTO cart (user_id, inventory_id, item_quantity, item_size, item_sugar, item_ice) VALUES (@UserID, @InventoryID, @Quantity, @Size, @Sugar, @Ice)";

            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.Parameters.AddWithValue("@InventoryID", inventoryID);
                    cmd.Parameters.AddWithValue("@Quantity", quantity);
                    cmd.Parameters.AddWithValue("@Size", size);
                    cmd.Parameters.AddWithValue("@Sugar", sugar);
                    cmd.Parameters.AddWithValue("@Ice", ice);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        public static void InsertSales(int userID, string receiptNo, DateTime receiptDate, int itemID, int itemQuantity, string itemDesc, decimal totalAmount, decimal amountPaid, decimal totalChange)
        {
            string query = "INSERT INTO sales (User_Id, Receipt_No, Receipt_Date, Item_Id, Quantity, Item_Desc, Total_Amount, Amount_Paid, Total_Change) VALUES (@UserID, @ReceiptNo, @ReceiptDate, @ItemID, @Quantity, @ItemDesc, @TotalAmount, @AmountPaid, @TotalChange)";

            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.Parameters.AddWithValue("@ReceiptNo", receiptNo);
                    cmd.Parameters.AddWithValue("@ReceiptDate", receiptDate);
                    cmd.Parameters.AddWithValue("@ItemID", itemID);
                    cmd.Parameters.AddWithValue("@Quantity", itemQuantity);
                    cmd.Parameters.AddWithValue("@ItemDesc", itemDesc);
                    cmd.Parameters.AddWithValue("@TotalAmount", totalAmount);
                    cmd.Parameters.AddWithValue("@AmountPaid", amountPaid);
                    cmd.Parameters.AddWithValue("@TotalChange", totalChange);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        public static void InsertDelivery(string deliveryNo, DateTime deliveryDate, int itemID, int itemQuantity, int supplierID)
        {
            string query = "INSERT INTO delivery (Delivery_No, Delivery_Date, Item_Id, Quantity, Supplier_Id) VALUES (@DeliveryNo, @DeliveryDate, @ItemID, @Quantity, @SupplierId)";

            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@DeliveryNo", deliveryNo);
                    cmd.Parameters.AddWithValue("@DeliveryDate", deliveryDate);
                    cmd.Parameters.AddWithValue("@ItemID", itemID);
                    cmd.Parameters.AddWithValue("@Quantity", itemQuantity);
                    cmd.Parameters.AddWithValue("@SupplierId", supplierID);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        public static void UpdateInventory(int inventoryId, int quantity)
        {
            string query = "UPDATE inventory SET Quantity = @Quantity WHERE inventory_id = @InventoryId";

            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Quantity", quantity);
                    cmd.Parameters.AddWithValue("@InventoryId", inventoryId);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        public static void UpdateItem(int itemId, string itemName, int categoryId, decimal basePrice)
        {
            string query = "UPDATE items SET Item_Name = @ItemName, Category_Id = @CategoryId, Base_Price = @BasePrice WHERE Item_Id = @ItemId";

            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ItemName", itemName);
                    cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                    cmd.Parameters.AddWithValue("@BasePrice", basePrice);
                    cmd.Parameters.AddWithValue("@ItemId", itemId);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        public static void UpdateItemImage(int itemId, byte[] itemImage)
        {
            string query = "UPDATE items SET Item_Image = @ItemImage WHERE Item_Id = @ItemId";

            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ItemId", itemId);
                    cmd.Parameters.AddWithValue("@ItemImage", itemImage != null ? (object)itemImage : DBNull.Value);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        public static void UpdateCategory(int categoryId, string categoryName)
        {
            string query = "UPDATE item_category SET Category_Name = @CategoryName WHERE Category_Id = @CategoryId";

            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CategoryName", categoryName);
                    cmd.Parameters.AddWithValue("@CategoryId", categoryId);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        public static void UpdateSupplier(int supplierId, string supplierName, string supplierAddress, string supplierContact)
        {
            string query = "UPDATE supplier SET Supplier_Name = @SupplierName, Address = @SupplierAddress, Contact_Number = @SupplierContact WHERE Supplier_Id = @SupplierId";

            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SupplierName", supplierName);
                    cmd.Parameters.AddWithValue("@SupplierAddress", supplierAddress);
                    cmd.Parameters.AddWithValue("@SupplierContact", supplierContact);
                    cmd.Parameters.AddWithValue("@SupplierId", supplierId);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        public static void UpdateUser(int userId, string username, string password, string email, string role)
        {
            string query = "UPDATE users SET username = @Username, password = @Password, emailAddress = @Email, role = @Role WHERE userID = @UserId";

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", hashedPassword);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Role", role);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        public static bool UpdateUser(int userId, string password)
        {
            string query = "UPDATE users SET password = @Password WHERE userID = @UserId";

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@Password", hashedPassword);

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return false;
                    }
                }
            }
        }


        public static void UpdateUserImage(int userId, byte[] userImage)
        {
            string query = "UPDATE users SET userImage = @UserImage WHERE userID = @UserId";

            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@UserImage", userImage != null ? (object)userImage : DBNull.Value);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        public static void UpdateCart(int cartID, int quantity)
        {
            string query = "UPDATE cart SET item_quantity = @Quantity WHERE cart_id = @CartID";

            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Quantity", quantity);
                    cmd.Parameters.AddWithValue("@CartID", cartID);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        public static void DeleteInventory(int itemID)
        {
            string query = "DELETE FROM inventory WHERE Item_Id = @ItemId";

            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ItemId", itemID);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        public static void DeleteItem(int itemId)
        {
            string deleteInventoryQuery = "DELETE FROM inventory WHERE Item_Id = @ItemId";
            string deleteItemQuery = "DELETE FROM items WHERE Item_Id = @ItemId";

            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                conn.Open();
                using (MySqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Delete from inventory table first to respect foreign key constraints
                        using (MySqlCommand inventoryCmd = new MySqlCommand(deleteInventoryQuery, conn))
                        {
                            inventoryCmd.Parameters.AddWithValue("@ItemId", itemId);
                            inventoryCmd.Transaction = transaction;
                            inventoryCmd.ExecuteNonQuery();
                        }

                        // Then delete from items table
                        using (MySqlCommand itemCmd = new MySqlCommand(deleteItemQuery, conn))
                        {
                            itemCmd.Parameters.AddWithValue("@ItemId", itemId);
                            itemCmd.Transaction = transaction;
                            int rowsAffectedItems = itemCmd.ExecuteNonQuery();

                            if (rowsAffectedItems == 0)
                            {
                                // Item with given ItemId doesn't exist in items table
                                MessageBox.Show("Item not found.");
                            }
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            transaction.Rollback();
                        }
                        catch (Exception rollbackEx)
                        {
                            MessageBox.Show("Rollback error: " + rollbackEx.Message);
                        }

                        MessageBox.Show("Transaction error: " + ex.Message);
                    }
                }
            }
        }

        public static void DeleteCategory(int categoryId)
        {
            string query = "DELETE FROM item_category WHERE Category_Id = @CategoryId";

            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CategoryId", categoryId);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        public static void DeleteSupplier(int supplierId)
        {
            string query = "DELETE FROM supplier WHERE Supplier_Id = @SupplierId";

            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SupplierId", supplierId);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        public static void DeleteUser(int userId)
        {
            string query = "DELETE FROM users WHERE userID = @UserId";

            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        public static void DeleteCart(int cartID)
        {
            string query = "DELETE FROM cart WHERE cart_id = @CartID";

            using (MySqlConnection conn = new MySqlConnection(UniversalVariables.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CartID", cartID);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
    }
}
