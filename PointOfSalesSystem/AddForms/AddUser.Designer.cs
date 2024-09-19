
namespace PointOfSalesSystem
{
    partial class AddUser
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.picEdit = new Guna.UI2.WinForms.Guna2PictureBox();
            this.btnBack = new Guna.UI2.WinForms.Guna2Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnSave = new Guna.UI2.WinForms.Guna2Button();
            this.pnlImage = new Guna.UI2.WinForms.Guna2Panel();
            this.lblImageChecker = new System.Windows.Forms.Label();
            this.lblItemName = new System.Windows.Forms.Label();
            this.btnRemoveImage = new Guna.UI2.WinForms.Guna2Button();
            this.userPic = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.btnEditImage = new Guna.UI2.WinForms.Guna2Button();
            this.pnlDetails = new Guna.UI2.WinForms.Guna2Panel();
            this.lblRoleChecker = new System.Windows.Forms.Label();
            this.radioAdmin = new Guna.UI2.WinForms.Guna2RadioButton();
            this.radioUser = new Guna.UI2.WinForms.Guna2RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.chkShowConfirmPass = new Guna.UI2.WinForms.Guna2ImageCheckBox();
            this.lblEmailChecker = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtEmail = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblConfirmPassChecker = new System.Windows.Forms.Label();
            this.txtConfirmPassword = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblConfirmPass = new System.Windows.Forms.Label();
            this.chkShowPass = new Guna.UI2.WinForms.Guna2ImageCheckBox();
            this.lblPassChecker = new System.Windows.Forms.Label();
            this.txtPassword = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblNameChecker = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUsername = new Guna.UI2.WinForms.Guna2TextBox();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEdit)).BeginInit();
            this.pnlImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.userPic)).BeginInit();
            this.pnlDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.picEdit);
            this.pnlHeader.Controls.Add(this.btnBack);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.btnSave);
            this.pnlHeader.Location = new System.Drawing.Point(-1, 1);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(993, 61);
            this.pnlHeader.TabIndex = 5;
            // 
            // picEdit
            // 
            this.picEdit.BackColor = System.Drawing.Color.Transparent;
            this.picEdit.Image = global::PointOfSalesSystem.Properties.Resources.edit_static;
            this.picEdit.ImageRotate = 0F;
            this.picEdit.Location = new System.Drawing.Point(426, 21);
            this.picEdit.Name = "picEdit";
            this.picEdit.Size = new System.Drawing.Size(21, 20);
            this.picEdit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picEdit.TabIndex = 4;
            this.picEdit.TabStop = false;
            this.picEdit.UseTransparentBackground = true;
            // 
            // btnBack
            // 
            this.btnBack.Animated = true;
            this.btnBack.BackColor = System.Drawing.Color.Transparent;
            this.btnBack.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(207)))));
            this.btnBack.BorderRadius = 10;
            this.btnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBack.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnBack.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnBack.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnBack.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnBack.FillColor = System.Drawing.Color.Transparent;
            this.btnBack.Font = new System.Drawing.Font("Proxima Nova Lt", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.ForeColor = System.Drawing.Color.Black;
            this.btnBack.Location = new System.Drawing.Point(23, 16);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(68, 32);
            this.btnBack.TabIndex = 2;
            this.btnBack.Text = "Back";
            this.btnBack.UseTransparentBackground = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Proxima Nova Lt", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(451, 21);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(77, 20);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Add User";
            // 
            // btnSave
            // 
            this.btnSave.Animated = true;
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.BorderColor = System.Drawing.Color.Transparent;
            this.btnSave.BorderRadius = 10;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSave.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSave.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSave.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSave.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(122)))), ((int)(((byte)(50)))));
            this.btnSave.Font = new System.Drawing.Font("Proxima Nova Lt", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(897, 16);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(68, 32);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseTransparentBackground = true;
            this.btnSave.Visible = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pnlImage
            // 
            this.pnlImage.BackColor = System.Drawing.Color.White;
            this.pnlImage.Controls.Add(this.lblImageChecker);
            this.pnlImage.Controls.Add(this.lblItemName);
            this.pnlImage.Controls.Add(this.btnRemoveImage);
            this.pnlImage.Controls.Add(this.userPic);
            this.pnlImage.Controls.Add(this.btnEditImage);
            this.pnlImage.Location = new System.Drawing.Point(-1, 63);
            this.pnlImage.Name = "pnlImage";
            this.pnlImage.Size = new System.Drawing.Size(278, 447);
            this.pnlImage.TabIndex = 10;
            // 
            // lblImageChecker
            // 
            this.lblImageChecker.AutoSize = true;
            this.lblImageChecker.Font = new System.Drawing.Font("Proxima Nova Lt", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImageChecker.ForeColor = System.Drawing.Color.IndianRed;
            this.lblImageChecker.Location = new System.Drawing.Point(60, 170);
            this.lblImageChecker.Name = "lblImageChecker";
            this.lblImageChecker.Size = new System.Drawing.Size(138, 13);
            this.lblImageChecker.TabIndex = 44;
            this.lblImageChecker.Text = "Image file size is too large.";
            this.lblImageChecker.Visible = false;
            // 
            // lblItemName
            // 
            this.lblItemName.AutoSize = true;
            this.lblItemName.Font = new System.Drawing.Font("Proxima Nova Lt", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(122)))), ((int)(((byte)(50)))));
            this.lblItemName.Location = new System.Drawing.Point(20, 12);
            this.lblItemName.Name = "lblItemName";
            this.lblItemName.Size = new System.Drawing.Size(84, 18);
            this.lblItemName.TabIndex = 4;
            this.lblItemName.Text = "User Image";
            // 
            // btnRemoveImage
            // 
            this.btnRemoveImage.Animated = true;
            this.btnRemoveImage.BackColor = System.Drawing.Color.Transparent;
            this.btnRemoveImage.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(207)))));
            this.btnRemoveImage.BorderRadius = 10;
            this.btnRemoveImage.BorderThickness = 1;
            this.btnRemoveImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRemoveImage.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnRemoveImage.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnRemoveImage.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnRemoveImage.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnRemoveImage.FillColor = System.Drawing.Color.Transparent;
            this.btnRemoveImage.Font = new System.Drawing.Font("Proxima Nova Lt", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveImage.ForeColor = System.Drawing.Color.Black;
            this.btnRemoveImage.Location = new System.Drawing.Point(145, 107);
            this.btnRemoveImage.Name = "btnRemoveImage";
            this.btnRemoveImage.Size = new System.Drawing.Size(84, 43);
            this.btnRemoveImage.TabIndex = 2;
            this.btnRemoveImage.Text = "Remove";
            this.btnRemoveImage.UseTransparentBackground = true;
            this.btnRemoveImage.Click += new System.EventHandler(this.btnRemoveImage_Click);
            // 
            // userPic
            // 
            this.userPic.BackColor = System.Drawing.Color.Transparent;
            this.userPic.Image = global::PointOfSalesSystem.Properties.Resources.PersonIcon;
            this.userPic.ImageRotate = 0F;
            this.userPic.Location = new System.Drawing.Point(30, 58);
            this.userPic.Name = "userPic";
            this.userPic.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.userPic.Size = new System.Drawing.Size(80, 80);
            this.userPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.userPic.TabIndex = 0;
            this.userPic.TabStop = false;
            this.userPic.UseTransparentBackground = true;
            // 
            // btnEditImage
            // 
            this.btnEditImage.Animated = true;
            this.btnEditImage.BackColor = System.Drawing.Color.Transparent;
            this.btnEditImage.BorderColor = System.Drawing.Color.Transparent;
            this.btnEditImage.BorderRadius = 10;
            this.btnEditImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditImage.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnEditImage.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnEditImage.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnEditImage.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnEditImage.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(160)))), ((int)(((byte)(63)))));
            this.btnEditImage.Font = new System.Drawing.Font("Proxima Nova Lt", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditImage.ForeColor = System.Drawing.Color.White;
            this.btnEditImage.Location = new System.Drawing.Point(145, 58);
            this.btnEditImage.Name = "btnEditImage";
            this.btnEditImage.Size = new System.Drawing.Size(84, 43);
            this.btnEditImage.TabIndex = 3;
            this.btnEditImage.Text = "Edit";
            this.btnEditImage.UseTransparentBackground = true;
            this.btnEditImage.Click += new System.EventHandler(this.btnEditImage_Click);
            // 
            // pnlDetails
            // 
            this.pnlDetails.BackColor = System.Drawing.Color.White;
            this.pnlDetails.Controls.Add(this.lblRoleChecker);
            this.pnlDetails.Controls.Add(this.radioAdmin);
            this.pnlDetails.Controls.Add(this.radioUser);
            this.pnlDetails.Controls.Add(this.label2);
            this.pnlDetails.Controls.Add(this.chkShowConfirmPass);
            this.pnlDetails.Controls.Add(this.lblEmailChecker);
            this.pnlDetails.Controls.Add(this.label6);
            this.pnlDetails.Controls.Add(this.txtEmail);
            this.pnlDetails.Controls.Add(this.lblConfirmPassChecker);
            this.pnlDetails.Controls.Add(this.txtConfirmPassword);
            this.pnlDetails.Controls.Add(this.lblConfirmPass);
            this.pnlDetails.Controls.Add(this.chkShowPass);
            this.pnlDetails.Controls.Add(this.lblPassChecker);
            this.pnlDetails.Controls.Add(this.txtPassword);
            this.pnlDetails.Controls.Add(this.lblNameChecker);
            this.pnlDetails.Controls.Add(this.label5);
            this.pnlDetails.Controls.Add(this.label3);
            this.pnlDetails.Controls.Add(this.label1);
            this.pnlDetails.Controls.Add(this.txtUsername);
            this.pnlDetails.Location = new System.Drawing.Point(278, 63);
            this.pnlDetails.Name = "pnlDetails";
            this.pnlDetails.Size = new System.Drawing.Size(714, 447);
            this.pnlDetails.TabIndex = 11;
            // 
            // lblRoleChecker
            // 
            this.lblRoleChecker.AutoSize = true;
            this.lblRoleChecker.Font = new System.Drawing.Font("Proxima Nova Lt", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoleChecker.ForeColor = System.Drawing.Color.IndianRed;
            this.lblRoleChecker.Location = new System.Drawing.Point(320, 285);
            this.lblRoleChecker.Name = "lblRoleChecker";
            this.lblRoleChecker.Size = new System.Drawing.Size(96, 13);
            this.lblRoleChecker.TabIndex = 35;
            this.lblRoleChecker.Text = "Please select one.";
            this.lblRoleChecker.Visible = false;
            // 
            // radioAdmin
            // 
            this.radioAdmin.AutoSize = true;
            this.radioAdmin.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.radioAdmin.CheckedState.BorderThickness = 0;
            this.radioAdmin.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.radioAdmin.CheckedState.InnerColor = System.Drawing.Color.White;
            this.radioAdmin.CheckedState.InnerOffset = -4;
            this.radioAdmin.Font = new System.Drawing.Font("Proxima Nova Rg", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioAdmin.Location = new System.Drawing.Point(240, 281);
            this.radioAdmin.Name = "radioAdmin";
            this.radioAdmin.Size = new System.Drawing.Size(62, 20);
            this.radioAdmin.TabIndex = 5;
            this.radioAdmin.Text = "Admin";
            this.radioAdmin.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.radioAdmin.UncheckedState.BorderThickness = 2;
            this.radioAdmin.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.radioAdmin.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            // 
            // radioUser
            // 
            this.radioUser.AutoSize = true;
            this.radioUser.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.radioUser.CheckedState.BorderThickness = 0;
            this.radioUser.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.radioUser.CheckedState.InnerColor = System.Drawing.Color.White;
            this.radioUser.CheckedState.InnerOffset = -4;
            this.radioUser.Font = new System.Drawing.Font("Proxima Nova Rg", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioUser.Location = new System.Drawing.Point(183, 281);
            this.radioUser.Name = "radioUser";
            this.radioUser.Size = new System.Drawing.Size(51, 20);
            this.radioUser.TabIndex = 4;
            this.radioUser.Text = "User";
            this.radioUser.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.radioUser.UncheckedState.BorderThickness = 2;
            this.radioUser.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.radioUser.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Proxima Nova Lt", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(120, 281);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 18);
            this.label2.TabIndex = 32;
            this.label2.Text = "Role";
            // 
            // chkShowConfirmPass
            // 
            this.chkShowConfirmPass.BackColor = System.Drawing.Color.Transparent;
            this.chkShowConfirmPass.CheckedState.Image = global::PointOfSalesSystem.Properties.Resources.icons8_eye_96;
            this.chkShowConfirmPass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkShowConfirmPass.Image = global::PointOfSalesSystem.Properties.Resources.icons8_closed_eye_96;
            this.chkShowConfirmPass.ImageOffset = new System.Drawing.Point(0, 0);
            this.chkShowConfirmPass.ImageRotate = 0F;
            this.chkShowConfirmPass.Location = new System.Drawing.Point(402, 180);
            this.chkShowConfirmPass.Name = "chkShowConfirmPass";
            this.chkShowConfirmPass.Size = new System.Drawing.Size(24, 24);
            this.chkShowConfirmPass.TabIndex = 31;
            this.chkShowConfirmPass.UseTransparentBackground = true;
            this.chkShowConfirmPass.CheckedChanged += new System.EventHandler(this.chkShowConfirmPass_CheckedChanged);
            // 
            // lblEmailChecker
            // 
            this.lblEmailChecker.AutoSize = true;
            this.lblEmailChecker.Font = new System.Drawing.Font("Proxima Nova Lt", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmailChecker.ForeColor = System.Drawing.Color.IndianRed;
            this.lblEmailChecker.Location = new System.Drawing.Point(447, 238);
            this.lblEmailChecker.Name = "lblEmailChecker";
            this.lblEmailChecker.Size = new System.Drawing.Size(87, 13);
            this.lblEmailChecker.TabIndex = 30;
            this.lblEmailChecker.Text = "Fill out the field.";
            this.lblEmailChecker.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Proxima Nova Lt", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(114, 234);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 18);
            this.label6.TabIndex = 29;
            this.label6.Text = "Email";
            // 
            // txtEmail
            // 
            this.txtEmail.Animated = true;
            this.txtEmail.BorderRadius = 5;
            this.txtEmail.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtEmail.DefaultText = "";
            this.txtEmail.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtEmail.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtEmail.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtEmail.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtEmail.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtEmail.Font = new System.Drawing.Font("Proxima Nova Rg", 9.75F);
            this.txtEmail.ForeColor = System.Drawing.Color.Black;
            this.txtEmail.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtEmail.Location = new System.Drawing.Point(184, 226);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.PasswordChar = '\0';
            this.txtEmail.PlaceholderText = "";
            this.txtEmail.SelectedText = "";
            this.txtEmail.Size = new System.Drawing.Size(250, 36);
            this.txtEmail.TabIndex = 3;
            this.txtEmail.TextChanged += new System.EventHandler(this.txtEmail_TextChanged);
            // 
            // lblConfirmPassChecker
            // 
            this.lblConfirmPassChecker.AutoSize = true;
            this.lblConfirmPassChecker.Font = new System.Drawing.Font("Proxima Nova Lt", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfirmPassChecker.ForeColor = System.Drawing.Color.IndianRed;
            this.lblConfirmPassChecker.Location = new System.Drawing.Point(447, 186);
            this.lblConfirmPassChecker.Name = "lblConfirmPassChecker";
            this.lblConfirmPassChecker.Size = new System.Drawing.Size(87, 13);
            this.lblConfirmPassChecker.TabIndex = 27;
            this.lblConfirmPassChecker.Text = "Fill out the field.";
            this.lblConfirmPassChecker.Visible = false;
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.Animated = true;
            this.txtConfirmPassword.BorderRadius = 5;
            this.txtConfirmPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtConfirmPassword.DefaultText = "";
            this.txtConfirmPassword.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtConfirmPassword.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtConfirmPassword.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtConfirmPassword.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtConfirmPassword.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtConfirmPassword.Font = new System.Drawing.Font("Proxima Nova Rg", 9.75F);
            this.txtConfirmPassword.ForeColor = System.Drawing.Color.Black;
            this.txtConfirmPassword.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtConfirmPassword.IconRight = global::PointOfSalesSystem.Properties.Resources.icons8_closed_eye_96;
            this.txtConfirmPassword.IconRightOffset = new System.Drawing.Point(5, 0);
            this.txtConfirmPassword.Location = new System.Drawing.Point(184, 174);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.PasswordChar = '●';
            this.txtConfirmPassword.PlaceholderText = "";
            this.txtConfirmPassword.SelectedText = "";
            this.txtConfirmPassword.Size = new System.Drawing.Size(250, 36);
            this.txtConfirmPassword.TabIndex = 2;
            this.txtConfirmPassword.UseSystemPasswordChar = true;
            this.txtConfirmPassword.TextChanged += new System.EventHandler(this.txtConfirmPassword_TextChanged);
            // 
            // lblConfirmPass
            // 
            this.lblConfirmPass.AutoSize = true;
            this.lblConfirmPass.Font = new System.Drawing.Font("Proxima Nova Lt", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfirmPass.Location = new System.Drawing.Point(29, 182);
            this.lblConfirmPass.Name = "lblConfirmPass";
            this.lblConfirmPass.Size = new System.Drawing.Size(130, 18);
            this.lblConfirmPass.TabIndex = 26;
            this.lblConfirmPass.Text = "Confirm Password";
            // 
            // chkShowPass
            // 
            this.chkShowPass.BackColor = System.Drawing.Color.Transparent;
            this.chkShowPass.CheckedState.Image = global::PointOfSalesSystem.Properties.Resources.icons8_eye_96;
            this.chkShowPass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkShowPass.Image = global::PointOfSalesSystem.Properties.Resources.icons8_closed_eye_96;
            this.chkShowPass.ImageOffset = new System.Drawing.Point(0, 0);
            this.chkShowPass.ImageRotate = 0F;
            this.chkShowPass.Location = new System.Drawing.Point(402, 125);
            this.chkShowPass.Name = "chkShowPass";
            this.chkShowPass.Size = new System.Drawing.Size(24, 24);
            this.chkShowPass.TabIndex = 24;
            this.chkShowPass.UseTransparentBackground = true;
            this.chkShowPass.CheckedChanged += new System.EventHandler(this.chkShowPass_CheckedChanged);
            // 
            // lblPassChecker
            // 
            this.lblPassChecker.AutoSize = true;
            this.lblPassChecker.Font = new System.Drawing.Font("Proxima Nova Lt", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassChecker.ForeColor = System.Drawing.Color.IndianRed;
            this.lblPassChecker.Location = new System.Drawing.Point(447, 131);
            this.lblPassChecker.Name = "lblPassChecker";
            this.lblPassChecker.Size = new System.Drawing.Size(87, 13);
            this.lblPassChecker.TabIndex = 23;
            this.lblPassChecker.Text = "Fill out the field.";
            this.lblPassChecker.Visible = false;
            // 
            // txtPassword
            // 
            this.txtPassword.Animated = true;
            this.txtPassword.BorderRadius = 5;
            this.txtPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPassword.DefaultText = "";
            this.txtPassword.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtPassword.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtPassword.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtPassword.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtPassword.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtPassword.Font = new System.Drawing.Font("Proxima Nova Rg", 9.75F);
            this.txtPassword.ForeColor = System.Drawing.Color.Black;
            this.txtPassword.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtPassword.IconRight = global::PointOfSalesSystem.Properties.Resources.icons8_closed_eye_96;
            this.txtPassword.IconRightOffset = new System.Drawing.Point(5, 0);
            this.txtPassword.Location = new System.Drawing.Point(184, 119);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '●';
            this.txtPassword.PlaceholderText = "";
            this.txtPassword.SelectedText = "";
            this.txtPassword.Size = new System.Drawing.Size(250, 36);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            // 
            // lblNameChecker
            // 
            this.lblNameChecker.AutoSize = true;
            this.lblNameChecker.Font = new System.Drawing.Font("Proxima Nova Lt", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNameChecker.ForeColor = System.Drawing.Color.IndianRed;
            this.lblNameChecker.Location = new System.Drawing.Point(447, 77);
            this.lblNameChecker.Name = "lblNameChecker";
            this.lblNameChecker.Size = new System.Drawing.Size(87, 13);
            this.lblNameChecker.TabIndex = 20;
            this.lblNameChecker.Text = "Fill out the field.";
            this.lblNameChecker.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Proxima Nova Lt", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(122)))), ((int)(((byte)(50)))));
            this.label5.Location = new System.Drawing.Point(20, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 18);
            this.label5.TabIndex = 6;
            this.label5.Text = "User Details";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Proxima Nova Lt", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(52, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 18);
            this.label3.TabIndex = 5;
            this.label3.Text = "New Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Proxima Nova Lt", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(83, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 18);
            this.label1.TabIndex = 3;
            this.label1.Text = "Username";
            // 
            // txtUsername
            // 
            this.txtUsername.Animated = true;
            this.txtUsername.BorderRadius = 5;
            this.txtUsername.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtUsername.DefaultText = "";
            this.txtUsername.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtUsername.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtUsername.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtUsername.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtUsername.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtUsername.Font = new System.Drawing.Font("Proxima Nova Rg", 9.75F);
            this.txtUsername.ForeColor = System.Drawing.Color.Black;
            this.txtUsername.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtUsername.Location = new System.Drawing.Point(184, 65);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.PasswordChar = '\0';
            this.txtUsername.PlaceholderText = "";
            this.txtUsername.SelectedText = "";
            this.txtUsername.Size = new System.Drawing.Size(250, 36);
            this.txtUsername.TabIndex = 0;
            this.txtUsername.TextChanged += new System.EventHandler(this.txtUsername_TextChanged);
            // 
            // AddUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(990, 510);
            this.Controls.Add(this.pnlImage);
            this.Controls.Add(this.pnlDetails);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AddUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddUser";
            this.Load += new System.EventHandler(this.AddUser_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEdit)).EndInit();
            this.pnlImage.ResumeLayout(false);
            this.pnlImage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.userPic)).EndInit();
            this.pnlDetails.ResumeLayout(false);
            this.pnlDetails.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlHeader;
        private Guna.UI2.WinForms.Guna2PictureBox picEdit;
        private Guna.UI2.WinForms.Guna2Button btnBack;
        private System.Windows.Forms.Label lblTitle;
        private Guna.UI2.WinForms.Guna2Button btnSave;
        private Guna.UI2.WinForms.Guna2Panel pnlImage;
        private System.Windows.Forms.Label lblItemName;
        private Guna.UI2.WinForms.Guna2Button btnRemoveImage;
        private Guna.UI2.WinForms.Guna2CirclePictureBox userPic;
        private Guna.UI2.WinForms.Guna2Button btnEditImage;
        private Guna.UI2.WinForms.Guna2Panel pnlDetails;
        internal System.Windows.Forms.Label lblPassChecker;
        private Guna.UI2.WinForms.Guna2TextBox txtPassword;
        internal System.Windows.Forms.Label lblNameChecker;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2TextBox txtUsername;
        private Guna.UI2.WinForms.Guna2ImageCheckBox chkShowPass;
        private Guna.UI2.WinForms.Guna2ImageCheckBox chkShowConfirmPass;
        internal System.Windows.Forms.Label lblEmailChecker;
        private System.Windows.Forms.Label label6;
        private Guna.UI2.WinForms.Guna2TextBox txtEmail;
        internal System.Windows.Forms.Label lblConfirmPassChecker;
        private Guna.UI2.WinForms.Guna2TextBox txtConfirmPassword;
        private System.Windows.Forms.Label lblConfirmPass;
        internal System.Windows.Forms.Label lblRoleChecker;
        private Guna.UI2.WinForms.Guna2RadioButton radioAdmin;
        private Guna.UI2.WinForms.Guna2RadioButton radioUser;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label lblImageChecker;
    }
}