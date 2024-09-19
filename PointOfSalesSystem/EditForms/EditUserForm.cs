using Org.BouncyCastle.Crypto.Paddings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace PointOfSalesSystem
{
    public partial class EditUserForm : Form
    {
        private readonly ManagementForm manageFormRef;
        private readonly UsersForm usersFormRef;
        private readonly string userID;
        private readonly string view;
        private readonly string searchData;

        private byte[] originalUserImage; 
        private string originalUsername;
        private string originalEmail;
        private string originalRole;

        private byte[] newImage;
        private string newRole;

        public EditUserForm(ManagementForm manageForm, UsersForm usersForm, string userID, string view, string searchData)
        {
            InitializeComponent();

            manageFormRef = manageForm;
            usersFormRef = usersForm;
            this.userID = userID;
            this.view = view;
            this.searchData = searchData;
        }

        private void setUserData()
        {
            string userQuery = "SELECT * FROM users";
            string specificID = userID;

            List<User> users = DataAccess.GetUsers(userQuery);

            if (users.Count > 0)
            {
                User selectedUser = users.FirstOrDefault(user => user.UserId.ToString() == specificID);

                if (selectedUser != null)
                {
                    txtUsername.Text = selectedUser.Username;
                    txtEmail.Text = selectedUser.Email;

                    originalUsername = selectedUser.Username;
                    originalEmail = selectedUser.Email;

                    if (selectedUser.UserImage != null)
                    {
                        userPic.Image = FormUtilities.ByteArrayToImage(selectedUser.UserImage);
                        userPic.SizeMode = PictureBoxSizeMode.StretchImage;
                        originalUserImage = selectedUser.UserImage;
                    }
                    else
                    {
                        userPic.Image = Properties.Resources.PersonIcon;
                        originalUserImage = FormUtilities.ImageToByteArray(userPic);
                    }

                    if (selectedUser.Role == "user")
                    {
                        radioUser.Checked = true;
                        originalRole = "user";
                    }
                    else
                    {
                        radioAdmin.Checked = true;
                        originalRole = "admin";
                    }
                }
            }
        }

        private void UpdateUserData()
        {
            int userIdToUpdate = Convert.ToInt32(userID);

            string newUsername = txtUsername.Text;
            string newPassword = txtPassword.Text;
            string newEmail = txtEmail.Text;
            string newRole;
            if (this.newRole != null)
            {
                newRole = this.newRole;
            }
            else
            {
                newRole = originalRole;
            }

            CheckCurrentUser(newUsername, null);
            DataAccess.UpdateUser(userIdToUpdate, newUsername, newPassword, newEmail, newRole);
        }

        private void CheckCurrentUser(string username = null, byte[] UserImage = null)
        {
            try
            {
                List<User> userList = DataAccess.GetUsers($"SELECT * FROM users WHERE userID = '{userID}'");

                if (userList.Count > 0)
                {
                    User user = userList[0];

                    if (UserImage != null)
                    {
                        manageFormRef.userPic.Image = FormUtilities.ByteArrayToImage(UserImage);
                    }

                    if (username != null)
                    {
                        manageFormRef.lblUsername.Text = "Welcome, " + username + "!";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveUserData()
        {
            string tableName = "users";
            string idColumn = "userID";
            string searchColumn = "username";
            string dataToCheck = txtUsername.Text;
            int user_id = Convert.ToInt32(userID);

            bool isUsernameEmpty = DataChecker.IsTextboxEmpty(txtUsername);
            bool isPasswordEmpty = DataChecker.IsTextboxEmpty(txtPassword);
            bool isConfirmPasswordEmpty = DataChecker.IsTextboxEmpty(txtConfirmPassword);
            bool isEmailEmpty = DataChecker.IsTextboxEmpty(txtEmail);

            if (isUsernameEmpty || isPasswordEmpty || isConfirmPasswordEmpty || isEmailEmpty)
            {
                if (isUsernameEmpty)
                {
                    DataChecker.HandleError(picEdit, lblTitle, lblNameChecker, "Fill out the field.");
                }

                if (isPasswordEmpty)
                {
                    DataChecker.HandleError(picEdit, lblTitle, lblPassChecker, "Fill out the field.");
                }

                if (isConfirmPasswordEmpty)
                {
                    DataChecker.HandleError(picEdit, lblTitle, lblConfirmPassChecker, "Fill out the field.");
                }

                if (isEmailEmpty)
                {
                    DataChecker.HandleError(picEdit, lblTitle, lblEmailChecker, "Fill out the field.");
                }

                return;
            }

            if (DataChecker.HasDuplicate(tableName, idColumn, searchColumn, dataToCheck, user_id))
            {
                DataChecker.HandleError(picEdit, lblTitle, lblNameChecker, "Username already exists.");
                return;
            }

            if (DataChecker.IsPasswordDifferent(txtPassword, txtConfirmPassword))
            {
                DataChecker.HandleError(picEdit, lblTitle, lblConfirmPassChecker, "Password does not match.");
                return;
            }

            if (!DataChecker.IsValidEmail(txtEmail.Text))
            {
                DataChecker.HandleError(picEdit, lblTitle, lblEmailChecker, "Enter a valid email address.");
                return;
            }

            UpdateUserData();

            originalUsername = txtUsername.Text;
            originalEmail = txtEmail.Text;
            originalRole = this.newRole;

            AddAnimation.EditInfo(picEdit, lblTitle, Properties.Resources.edit_check_animated, "User Saved!");
            lblNameChecker.Visible = false;
            lblPassChecker.Visible = false;
            lblConfirmPassChecker.Visible = false;
            lblEmailChecker.Visible = false;
            lblRoleChecker.Visible = false;

            txtPassword.Text = null;
            txtConfirmPassword.Text = null;

            FormUtilities.defaultBtnInfo(btnSave, btnBack);
        }

        private void EditUserForm_Load(object sender, EventArgs e)
        {
            radioUser.CheckedChanged += RadioButton_CheckedChanged;
            radioAdmin.CheckedChanged += RadioButton_CheckedChanged;

            setUserData();

            FormUtilities.defaultBtnInfo(btnSave, btnBack);
            FormUtilities.UpdateRemoveButton(btnRemoveImage, lblImageChecker, userPic, Properties.Resources.PersonIcon);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (view == "list")
            {
                usersFormRef.loadForm(new UsersListView(manageFormRef, usersFormRef, view, searchData));
            }
            else
            {
                usersFormRef.loadForm(new UsersGridView());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveUserData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chkShowPass_CheckedChanged(object sender, EventArgs e)
        {
            FormUtilities.ShowPassword(txtPassword, chkShowPass);
        }

        private void chkShowConfirmPass_CheckedChanged(object sender, EventArgs e)
        {
            FormUtilities.ShowPassword(txtConfirmPassword, chkShowConfirmPass);
        }

        private void btnEditImage_Click(object sender, EventArgs e)
        {
            newImage = FormUtilities.InsertImage(userPic, originalUserImage);
            userPic.SizeMode = PictureBoxSizeMode.StretchImage;

            if (DataChecker.IsImageSizeTooLarge(newImage))
            {
                userPic.Image = FormUtilities.ByteArrayToImage(originalUserImage);

                DataChecker.HandleError(picEdit, lblTitle, lblImageChecker, "Maximum file size is 1mb.");
                return;
            }

            DataAccess.UpdateUserImage(Convert.ToInt32(userID), newImage);
            CheckCurrentUser(null, newImage);

            AddAnimation.EditInfo(picEdit, lblTitle, Properties.Resources.edit_check_animated, "Image Saved!");
            originalUserImage = newImage;

            FormUtilities.UpdateRemoveButton(btnRemoveImage, lblImageChecker, userPic, Properties.Resources.PersonIcon);
        }

        private void btnRemoveImage_Click(object sender, EventArgs e)
        {
            userPic.Image = Properties.Resources.PersonIcon;
            newImage = FormUtilities.ImageToByteArray(userPic);

            DataAccess.UpdateUserImage(Convert.ToInt32(userID), null);
            CheckCurrentUser(null, newImage);

            AddAnimation.EditInfo(picEdit, lblTitle, Properties.Resources.edit_check_animated, "Image Removed!");

            FormUtilities.UpdateRemoveButton(btnRemoveImage, lblImageChecker, userPic, Properties.Resources.PersonIcon);
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            FormUtilities.UpdateChecker(lblNameChecker);

            if (txtEmail.Text == originalEmail && newRole == originalRole && txtPassword.Text == "" && txtConfirmPassword.Text == "")
            {
                FormUtilities.UpdateButtonOnTextChanged(lblNameChecker, txtUsername, btnSave, btnBack, originalUsername);
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            FormUtilities.UpdateChecker(lblPassChecker);

            if (txtEmail.Text == originalEmail && newRole == originalRole && txtUsername.Text == originalUsername && txtConfirmPassword.Text == "")
            {
                FormUtilities.UpdateButtonOnTextChanged(lblPassChecker, txtPassword, btnSave, btnBack, "");
            }
        }

        private void txtConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            FormUtilities.UpdateChecker(lblConfirmPassChecker);

            if (txtEmail.Text == originalEmail && newRole == originalRole && txtUsername.Text == originalUsername && txtPassword.Text == "")
            {
                FormUtilities.UpdateButtonOnTextChanged(lblConfirmPassChecker, txtConfirmPassword, btnSave, btnBack, "");
            }
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            FormUtilities.UpdateChecker(lblEmailChecker);

            if (txtUsername.Text == originalUsername && newRole == originalRole && txtPassword.Text == "" && txtConfirmPassword.Text == "")
            {
                FormUtilities.UpdateButtonOnTextChanged(lblEmailChecker, txtEmail, btnSave, btnBack, originalEmail);
            }
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            newRole = FormUtilities.RoleChanged(sender, radioUser, radioAdmin);

            if (txtUsername.Text == originalUsername && txtEmail.Text == originalEmail && txtPassword.Text == "" && txtConfirmPassword.Text == "")
            {
                FormUtilities.UpdateButtonOnRoleChanged(lblRoleChecker, newRole, btnSave, btnBack, originalRole);
            }
        }
    }
}
