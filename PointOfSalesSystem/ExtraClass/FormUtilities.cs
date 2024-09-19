using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.SessionState;
using System.Windows.Forms;

namespace PointOfSalesSystem
{
    internal class FormUtilities
    {
        public static void LoadForm(Panel panel, Form newForm)
        {
            if (panel.Controls.Count > 0)
            {
                foreach (Control control in panel.Controls)
                {
                    control.Dispose();
                }
                panel.Controls.Clear();
            }

            newForm.TopLevel = false;
            newForm.Dock = DockStyle.Fill;
            panel.Controls.Add(newForm);
            newForm.Show();
        }

        public static string GenerateOTP(string recipientEmail)
        {
            // Generate a random 6-digit OTP
            Random random = new Random();
            int otp = random.Next(100000, 999999);

            // Configure the SMTP client for Gmail
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("trialkoito1@gmail.com", "dbsgsgyczddifklg"),
                EnableSsl = true,
            };

            // Construct the email message
            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress("trialkoito1@gmail.com"),
                Subject = "Verification Code",
                Body = $"Your verification code is: {otp}",
                IsBodyHtml = false,
            };

            // Add recipient email address
            mailMessage.To.Add(recipientEmail);

            try
            {
                // Send the email
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending email: {ex.Message}");
            }

            // Return the generated OTP as a string
            return otp.ToString("D6");
        }

        public static string GenerateRandomLetters(int length)
        {
            // Create a random generator for letters
            Random random = new Random();
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            // Generate a string of random letters
            string randomString = new string(Enumerable.Repeat(letters, length).Select(s => s[random.Next(s.Length)]).ToArray());

            return randomString;
        }

        public static void setUserProfile(Guna.UI2.WinForms.Guna2CirclePictureBox userPic, Label lblUsername, string username)
        {
            string userQuery = "SELECT * FROM users";
            string specificUser = username;

            List<User> users = DataAccess.GetUsers(userQuery);

            if (users.Count > 0)
            {
                User selectedUser = users.FirstOrDefault(user => user.Username.ToString() == specificUser);

                if (selectedUser != null)
                {
                    if (selectedUser.UserImage != null)
                    {
                        userPic.Image = ByteArrayToImage(selectedUser.UserImage);
                        lblUsername.Text = "Welcome, " + selectedUser.Username + "!";
                    }
                    else
                    {
                        userPic.Image = Properties.Resources.PersonIcon;
                        lblUsername.Text = "Welcome, " + selectedUser.Username + "!";
                    }

                    userPic.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
        }

        public static int UpdateMaxQuantity(int inventoryID, string userID, int maxQuantity)
        {
            string cartQuery = $"SELECT SUM(item_quantity) as TotalQuantity FROM cart WHERE inventory_id = {inventoryID} AND user_id = {userID}";
            int totalQuantity = DataAccess.GetTotalQuantity(cartQuery);
            int updatedMaxQuantity = maxQuantity - totalQuantity;

            return updatedMaxQuantity;
        }

        public static Image CropToCircle(Image img)
        {
            Bitmap circleImage = new Bitmap(img.Width, img.Height);
            using (Graphics g = Graphics.FromImage(circleImage))
            {
                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddEllipse(0, 0, img.Width, img.Height);
                    g.SetClip(path);
                    g.DrawImage(img, 0, 0);
                }
            }
            return circleImage;
        }

        public static byte[] ImageToByteArray(Guna.UI2.WinForms.Guna2CirclePictureBox image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Image.Save(ms, image.Image.RawFormat);
                return ms.ToArray();
            }
        }

        public static byte[] ImageToByteArray(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                return ms.ToArray();
            }
        }

        public static Image ByteArrayToImage(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0)
                return null;

            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                return Image.FromStream(ms);
            }
        }

        public static byte[] InsertImage(Guna.UI2.WinForms.Guna2CirclePictureBox picturebox, byte[] originalImage)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedImagePath = openFileDialog.FileName;

                try
                {
                    byte[] imageBytes = File.ReadAllBytes(selectedImagePath);

                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        Image selectedImage = Image.FromStream(ms);
                        picturebox.Image = selectedImage;
                        picturebox.SizeMode = PictureBoxSizeMode.Zoom;

                        return imageBytes;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Image Loading Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return originalImage;
        }

        public static void ShowPassword(Guna.UI2.WinForms.Guna2TextBox txtPassword, Guna.UI2.WinForms.Guna2ImageCheckBox chkShowPass)
        {
            if (chkShowPass.Checked)
            {
                txtPassword.IconRight = Properties.Resources.icons8_eye_96;
                txtPassword.UseSystemPasswordChar = false;
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.IconRight = Properties.Resources.icons8_closed_eye_96;
                txtPassword.UseSystemPasswordChar = true;
                txtPassword.PasswordChar = '\u25CF';
            }
        }

        public static string RoleChanged(object sender, Guna.UI2.WinForms.Guna2RadioButton radioUser, Guna.UI2.WinForms.Guna2RadioButton radioAdmin)
        {
            string newRole = "";

            Guna.UI2.WinForms.Guna2RadioButton selectedRadioButton = (Guna.UI2.WinForms.Guna2RadioButton)sender;

            if (selectedRadioButton.Checked)
            {
                if (selectedRadioButton == radioUser)
                {
                    newRole = "user";
                }
                else if (selectedRadioButton == radioAdmin)
                {
                    newRole = "admin";
                }
            }

            return newRole;
        }

        public static string PaymentChanged(object sender, Guna.UI2.WinForms.Guna2CustomRadioButton radioCash, Guna.UI2.WinForms.Guna2CustomRadioButton radioCard, Guna.UI2.WinForms.Guna2CustomRadioButton radioPaypal, Guna.UI2.WinForms.Guna2CustomRadioButton radioApplePay)
        {
            string newPayment = "";

            Guna.UI2.WinForms.Guna2CustomRadioButton selectedRadioButton = (Guna.UI2.WinForms.Guna2CustomRadioButton)sender;

            if (selectedRadioButton.Checked)
            {
                if (selectedRadioButton == radioCash)
                {
                    newPayment = "Cash";
                    UncheckOtherRadios(radioCard, radioPaypal, radioApplePay);
                }
                else if (selectedRadioButton == radioCard)
                {
                    newPayment = "Card";
                    UncheckOtherRadios(radioCash, radioPaypal, radioApplePay);
                }
                else if (selectedRadioButton == radioPaypal)
                {
                    newPayment = "Paypal";
                    UncheckOtherRadios(radioCash, radioCard, radioApplePay);
                }
                else if (selectedRadioButton == radioApplePay)
                {
                    newPayment = "Apple Pay";
                    UncheckOtherRadios(radioCash, radioCard, radioPaypal);
                }
            }

            return newPayment;
        }

        private static void UncheckOtherRadios(params Guna.UI2.WinForms.Guna2CustomRadioButton[] radioButtons)
        {
            foreach (var radioButton in radioButtons)
            {
                radioButton.Checked = false;
            }
        }

        public static void SetLabelCount(Label label, int count)
        {
            label.Text = count == 0 ? "0" : count.ToString();
        }

        public static void defaultBtnInfo(Guna.UI2.WinForms.Guna2Button btnSave, Guna.UI2.WinForms.Guna2Button btnBack)
        {
            btnSave.Visible = false;
            btnBack.Text = "Back";
        }

        public static void editedBtnInfo(Guna.UI2.WinForms.Guna2Button btnSave, Guna.UI2.WinForms.Guna2Button btnBack)
        {
            btnSave.Visible = true;
            btnBack.Text = "Cancel";
        }

        public static void UpdateChecker(Label label)
        {
            if (label.Visible)
            {
                label.Visible = false;
            }
        }

        public static void UpdateRemoveButton(Guna.UI2.WinForms.Guna2Button btnRemove, Label label, Guna.UI2.WinForms.Guna2CirclePictureBox pictureBox, Image image)
        {
            if (DataChecker.IsImageDefault(pictureBox, image))
            {
                btnRemove.Enabled = false;

            }
            else
            {
                btnRemove.Enabled = true;
            }

            label.Visible = false;
        }

        public static void UpdateButtonOnImageChanged(Label label, byte[] newImage, Guna.UI2.WinForms.Guna2Button btnSave, Guna.UI2.WinForms.Guna2Button btnBack, byte[] originalData)
        {
            label.Visible = false;

            if (newImage != originalData)
            {
                editedBtnInfo(btnSave, btnBack);
            }
            else if (newImage == originalData)
            {
                defaultBtnInfo(btnSave, btnBack);
            }
        }

        public static void UpdateButtonOnTextChanged(Label label, Guna.UI2.WinForms.Guna2TextBox textbox, Guna.UI2.WinForms.Guna2Button btnSave, Guna.UI2.WinForms.Guna2Button btnBack, string originalData)
        {
            label.Visible = false;

            if (textbox.Text != originalData)
            {
                editedBtnInfo(btnSave, btnBack);
            }
            else if (textbox.Text == originalData)
            {
                defaultBtnInfo(btnSave, btnBack);
            }
        }

        public static void UpdateButtonOnSelectedIndexChanged(Guna.UI2.WinForms.Guna2ComboBox combobox, Guna.UI2.WinForms.Guna2Button btnSave, Guna.UI2.WinForms.Guna2Button btnBack, string originalData)
        {
            if (combobox.SelectedValue?.ToString() != originalData)
            {
                editedBtnInfo(btnSave, btnBack);
            }
            else if (combobox.SelectedValue?.ToString() == originalData)
            {
                defaultBtnInfo(btnSave, btnBack);
            }
        }

        public static void UpdateButtonOnSelectedIndexChanged(Guna.UI2.WinForms.Guna2ComboBox combobox, Guna.UI2.WinForms.Guna2Button btnSave, Guna.UI2.WinForms.Guna2Button btnBack, int originalData)
        {
            if (combobox.SelectedIndex != originalData)
            {
                editedBtnInfo(btnSave, btnBack);
            }
            else if (combobox.SelectedIndex == originalData)
            {
                defaultBtnInfo(btnSave, btnBack);
            }
        }

        public static void UpdateButtonOnRoleChanged(Label label, string newRole, Guna.UI2.WinForms.Guna2Button btnSave, Guna.UI2.WinForms.Guna2Button btnBack, string originalData)
        {
            label.Visible = false;

            if (newRole != originalData)
            {
                editedBtnInfo(btnSave, btnBack);
            }
            else if (newRole == originalData)
            {
                defaultBtnInfo(btnSave, btnBack);
            }
        }

        public static void UpdateButton(Panel panel, Guna.UI2.WinForms.Guna2Button button, Color fillColor, Color textColor, Image image = null, Form form = null)
        {
            button.FillColor = fillColor;
            button.ForeColor = textColor;

            if (image != null)
            {
                button.Image = image;
            }
            if (form != null)
            {
                LoadForm(panel, form);
            }
        }

        public static void UpdateButton(Panel panel, Guna.UI2.WinForms.Guna2Button button, Guna.UI2.WinForms.Guna2PictureBox pictureBox, Color fillColor, Color textColor, Image image = null, Form form = null)
        {
            button.FillColor = fillColor;
            button.ForeColor = textColor;

            if (image != null)
            {
                pictureBox.Image = image;
            }
            if (form != null)
            {
                LoadForm(panel, form);
            }
        }

        public static void UpdateButton(Panel panel, Guna.UI2.WinForms.Guna2Button button, Color fillColor, Image image = null, Form form = null)
        {
            button.FillColor = fillColor;
            if (image != null)
            {
                button.Image = image;
            }
            if (form != null)
            {
                LoadForm(panel, form);
            }
        }
    }
}
