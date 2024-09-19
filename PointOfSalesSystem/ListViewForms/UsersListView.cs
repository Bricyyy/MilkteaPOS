using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using PointOfSalesSystem.DeleteModals;

namespace PointOfSalesSystem
{
    public partial class UsersListView : Form
    {
        private readonly ManagementForm manageFormRef;
        private readonly UsersForm usersFormRef;
        private readonly string view;
        private readonly string searchData;

        public UsersListView(ManagementForm manageForm, UsersForm usersForm, string view, string searchData = null)
        {
            InitializeComponent();

            manageFormRef = manageForm;
            usersFormRef = usersForm;
            this.view = view;
            this.searchData = searchData;
        }

        private void loadTableData()
        {
            string query;

            if (searchData == null)
            {
                query = $@"SELECT userID, userImage, username, emailAddress, role
                           FROM users
                           ORDER BY userID DESC";
            }
            else
            {
                query = $@"SELECT userID, userImage, username, emailAddress, role
                           FROM users
                           WHERE username LIKE CONCAT('%', '{searchData}', '%')
                           ORDER BY userID DESC";
            }

            DataGridViewPopulator.setDataTable(query);
            DataGridViewPopulator.SetUserData(dgvUsers);
        }

        private Form CreateModalBackground()
        {
            var panelLocationOnScreen = usersFormRef.pnlMain.PointToScreen(Point.Empty);

            var modalBackground = new Form
            {
                StartPosition = FormStartPosition.Manual,
                FormBorderStyle = FormBorderStyle.None,
                Opacity = 0.5d,
                BackColor = Color.Black,
                Size = usersFormRef.pnlMain.Size,
                Location = panelLocationOnScreen,
                ShowInTaskbar = false
            };

            return modalBackground;
        }

        private void ShowModalWithBackground(Form modal, Form background)
        {
            background.Show();
            modal.Owner = background;
            modal.ShowDialog();
        }

        private void CheckCurrentUser(string userID)
        {
            try
            {
                List<User> userList = DataAccess.GetUsers($"SELECT * FROM users WHERE userID = '{userID}'");

                if (userList.Count > 0)
                {
                    User user = userList[0];

                    if (user.Username == manageFormRef.username)
                    {
                        Form dashboard = manageFormRef.ParentForm;
                        dashboard.Close();

                        Login login = new Login();
                        login.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RemoveItemFromCart(string userID)
        {
            string cartQuery = $"SELECT user_id FROM cart WHERE user_id = '{userID}'";
            string userId = DataAccess.ExecuteScalar(cartQuery)?.ToString();

            if (!string.IsNullOrEmpty(userId))
            {
                string deleteQuery = $"DELETE FROM cart WHERE user_id = '{userId}'";
                DataAccess.ExecuteNonQuery(deleteQuery);
            }
        }

        private void DeleteUserData(string userID)
        {
            var rowToDelete = dgvUsers.Rows
                .Cast<DataGridViewRow>()
                .FirstOrDefault(r => r.Cells["userIDColumn"].Value.ToString() == userID);

            if (rowToDelete != null)
            {
                dgvUsers.Rows.Remove(rowToDelete);
                DataAccess.DeleteUser(Convert.ToInt32(userID));
                RemoveItemFromCart(userID);
            }

            CheckCurrentUser(userID);
        }

        private void ConfirmDelete(string userID)
        {
            using (var modalBackground = CreateModalBackground())
            {
                using (var verifyUser = new DeleteUserModal(manageFormRef.username))
                {
                    ShowModalWithBackground(verifyUser, modalBackground);

                    if (verifyUser.IsConfirmed)
                    {
                        using (var confirmDelete = new DeleteDataModal())
                        {
                            ShowModalWithBackground(confirmDelete, modalBackground);

                            if (confirmDelete.IsConfirmed)
                            {
                                DeleteUserData(userID);
                            }
                        }
                    }
                }
            }
        }

        private void DeleteDataError(string errorMessage)
        {
            using (DeleteDataModal confirmDelete = new DeleteDataModal(errorMessage))
            {
                Point panelLocationOnScreen = usersFormRef.pnlMain.PointToScreen(Point.Empty);

                Form modalBackground = new Form
                {
                    StartPosition = FormStartPosition.Manual,
                    FormBorderStyle = FormBorderStyle.None,
                    Opacity = 0.5d,
                    BackColor = Color.Black,
                    Size = usersFormRef.pnlMain.Size,
                    Location = panelLocationOnScreen,
                    ShowInTaskbar = false
                };

                modalBackground.Show();

                confirmDelete.Owner = modalBackground;

                confirmDelete.ShowDialog();

                modalBackground.Dispose();
            }
        }

        private void UsersListView_Load(object sender, EventArgs e)
        {
            loadTableData();

            DataGridViewPopulator.headerFormat(dgvUsers);
        }

        private void dgvUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvUsers.Columns[e.ColumnIndex].Name;
            if (e.RowIndex >= 0 && e.RowIndex < dgvUsers.Rows.Count)
            {
                DataGridViewRow row = dgvUsers.Rows[e.RowIndex];
                string userID = row.Cells["userIDColumn"].Value.ToString();

                if (colName == "editData")
                {
                    usersFormRef.loadForm(new EditUserForm(manageFormRef, usersFormRef, userID, view, searchData));
                }
                else if (colName == "deleteData")
                {
                    bool userIdExistsInDelivery = DataChecker.DoesUserIdExistInSales(Convert.ToInt32(userID));

                    if (userIdExistsInDelivery)
                    {
                        DeleteDataError("User is associated with any\nof the existing sales.");
                        return;
                    }

                    ConfirmDelete(userID);

                    FormUtilities.SetLabelCount(manageFormRef.lblItemCount, DataAccess.GetEntityCount("users"));
                }
            }
        }

        private void dgvUsers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewPopulator.FormatImageColumn(e, dgvUsers, "editData", "deleteData");
            DataGridViewPopulator.FormatImageColumn(e, dgvUsers, Properties.Resources.PersonIcon, "imageColumn");
        }

        private void dgvUsers_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            dgvUsers.ClearSelection();

            DataGridViewPopulator.SetRowNumber(dgvUsers, "dataCount");
        }

        private void dgvUsers_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewPopulator.SetCursorOnColumns(dgvUsers, "editData", "deleteData");
        }
    }
}
