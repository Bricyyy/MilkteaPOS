using Microsoft.VisualBasic.ApplicationServices;
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
    public partial class AddUser : Form
    {
        private readonly ManagementForm manageFormRef;
        private readonly UsersForm usersFormRef;
        private readonly string view;
        private readonly string searchData;

        private readonly byte[] originalUserImage = null;
        private readonly string originalUsername = "";
        private readonly string originalPassword = "";
        private readonly string originalConfirmPassword = "";
        private readonly string originalEmail = "";
        private readonly string originalRole = null;

        private byte[] newImage;
        private string newRole;

        public AddUser(ManagementForm manageForm, UsersForm usersForm, string view, string searchData)
        {
            InitializeComponent();

            manageFormRef = manageForm;
            usersFormRef = usersForm;
            this.view = view;
            this.searchData = searchData;
        }

        private void InsertUserData()
        {
            byte[] Image;
            if (newImage != null)
            {
                Image = newImage;
            }
            else
            {
                Image = null;
            }
            string Username = txtUsername.Text;
            string Password = txtPassword.Text;
            string Email = txtEmail.Text;
            string Role = this.newRole;

            DataAccess.InsertUser(Image, Username, Password, Email, Role);
        }

        private void resetFields()
        {
            lblImageChecker.Visible = false;
            lblNameChecker.Visible = false;
            lblPassChecker.Visible = false;
            lblConfirmPassChecker.Visible = false;
            lblEmailChecker.Visible = false;
            lblRoleChecker.Visible = false;

            userPic.Image = Properties.Resources.PersonIcon;
            newImage = null;
            txtUsername.Text = null;
            txtPassword.Text = null;
            txtConfirmPassword.Text = null;
            txtEmail.Text = null;
            radioUser.Checked = false;
            radioAdmin.Checked = false;
            newRole = null;
        }

        private void SaveUserData()
        {
            string tableName = "users";
            string searchColumn = "username";
            string dataToCheck = txtUsername.Text;

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

                if (newRole == null)
                {
                    DataChecker.HandleError(picEdit, lblTitle, lblRoleChecker, "Please select a role.");
                }

                return;
            }

            Dictionary<string, Label> errorLabels = new Dictionary<string, Label>
            {
                { "Please select a role.", lblRoleChecker },
                { "Maximum file size is 1mb.", lblImageChecker },
                { "Username already exists.", lblNameChecker },
                { "Password does not match.", lblConfirmPassChecker },
                { "Enter a valid email address.", lblEmailChecker }
            };

            List<string> errorMessages = new List<string>();

            if (newRole == null)
            {
                errorMessages.Add("Please select a role.");
            }

            if (newImage != null && DataChecker.IsImageSizeTooLarge(newImage))
            {
                errorMessages.Add("Maximum file size is 1mb.");
            }

            if (DataChecker.HasDuplicate(tableName, searchColumn, dataToCheck))
            {
                errorMessages.Add("Username already exists.");
            }

            if (DataChecker.IsPasswordDifferent(txtPassword, txtConfirmPassword))
            {
                errorMessages.Add("Password does not match.");
            }

            if (!DataChecker.IsValidEmail(txtEmail.Text))
            {
                errorMessages.Add("Enter a valid email address.");
            }

            if (errorMessages.Any())
            {
                foreach (var errorMessage in errorMessages)
                {
                    lblTitle.Text = "Add User";

                    if (errorLabels.TryGetValue(errorMessage, out Label label))
                    {
                        DataChecker.HandleError(picEdit, lblTitle, label, errorMessage);
                    }
                }
                return;
            }

            InsertUserData();

            AddAnimation.EditInfo(picEdit, lblTitle, Properties.Resources.edit_check_animated, "User Saved!");

            resetFields();

            FormUtilities.SetLabelCount(manageFormRef.lblUserCount, DataAccess.GetEntityCount("users"));
            FormUtilities.defaultBtnInfo(btnSave, btnBack);
            FormUtilities.UpdateRemoveButton(btnRemoveImage, lblImageChecker, userPic, Properties.Resources.PersonIcon);
        }

        private void AddUser_Load(object sender, EventArgs e)
        {
            radioUser.CheckedChanged += RadioButton_CheckedChanged;
            radioAdmin.CheckedChanged += RadioButton_CheckedChanged;

            newImage = null;
            newRole = null;

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

            FormUtilities.UpdateRemoveButton(btnRemoveImage, lblImageChecker, userPic, Properties.Resources.PersonIcon);

            if (txtUsername.Text == originalUsername && txtEmail.Text == originalEmail && txtPassword.Text == "" && txtConfirmPassword.Text == "" && newRole == originalRole)
            {
                FormUtilities.UpdateButtonOnImageChanged(lblImageChecker, newImage, btnSave, btnBack, originalUserImage);
            }
        }

        private void btnRemoveImage_Click(object sender, EventArgs e)
        {
            userPic.Image = Properties.Resources.PersonIcon;
            newImage = null;

            FormUtilities.UpdateRemoveButton(btnRemoveImage, lblImageChecker, userPic, Properties.Resources.PersonIcon);

            if (txtUsername.Text == originalUsername && txtEmail.Text == originalEmail && txtPassword.Text == "" && txtConfirmPassword.Text == "" && newRole == originalRole)
            {
                FormUtilities.UpdateButtonOnImageChanged(lblImageChecker, newImage, btnSave, btnBack, originalUserImage);
            }
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
                FormUtilities.UpdateButtonOnTextChanged(lblPassChecker, txtPassword, btnSave, btnBack, originalPassword);
            }
        }

        private void txtConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            FormUtilities.UpdateChecker(lblConfirmPassChecker);

            if (txtEmail.Text == originalEmail && newRole == originalRole && txtUsername.Text == originalUsername && txtPassword.Text == "")
            {
                FormUtilities.UpdateButtonOnTextChanged(lblConfirmPassChecker, txtConfirmPassword, btnSave, btnBack, originalConfirmPassword);
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

            FormUtilities.UpdateChecker(lblRoleChecker);

            if (txtUsername.Text == originalUsername && txtEmail.Text == originalEmail && txtPassword.Text == "" && txtConfirmPassword.Text == "")
            {
                FormUtilities.UpdateButtonOnRoleChanged(lblRoleChecker, newRole, btnSave, btnBack, originalRole);
            }
        }
    }
}
