namespace PointOfSalesSystem
{
    partial class EditSupplierForm
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
            this.lblItemName = new System.Windows.Forms.Label();
            this.btnDeleteImage = new Guna.UI2.WinForms.Guna2Button();
            this.itemPic = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.btnEditImage = new Guna.UI2.WinForms.Guna2Button();
            this.pnlDetails = new Guna.UI2.WinForms.Guna2Panel();
            this.lblAddressChecker = new System.Windows.Forms.Label();
            this.txtAddress = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblNumberChecker = new System.Windows.Forms.Label();
            this.lblNameChecker = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtContact = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtSupplierName = new Guna.UI2.WinForms.Guna2TextBox();
            this.pnlIngredients = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2TextBox3 = new Guna.UI2.WinForms.Guna2TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEdit)).BeginInit();
            this.pnlImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.itemPic)).BeginInit();
            this.pnlDetails.SuspendLayout();
            this.pnlIngredients.SuspendLayout();
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
            this.lblTitle.Size = new System.Drawing.Size(100, 20);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Edit Supplier";
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
            this.pnlImage.Controls.Add(this.lblItemName);
            this.pnlImage.Controls.Add(this.btnDeleteImage);
            this.pnlImage.Controls.Add(this.itemPic);
            this.pnlImage.Controls.Add(this.btnEditImage);
            this.pnlImage.Location = new System.Drawing.Point(-1, 63);
            this.pnlImage.Name = "pnlImage";
            this.pnlImage.Size = new System.Drawing.Size(278, 200);
            this.pnlImage.TabIndex = 6;
            // 
            // lblItemName
            // 
            this.lblItemName.AutoSize = true;
            this.lblItemName.Font = new System.Drawing.Font("Proxima Nova Lt", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(122)))), ((int)(((byte)(50)))));
            this.lblItemName.Location = new System.Drawing.Point(20, 12);
            this.lblItemName.Name = "lblItemName";
            this.lblItemName.Size = new System.Drawing.Size(110, 18);
            this.lblItemName.TabIndex = 4;
            this.lblItemName.Text = "Supplier Image";
            // 
            // btnDeleteImage
            // 
            this.btnDeleteImage.Animated = true;
            this.btnDeleteImage.BackColor = System.Drawing.Color.Transparent;
            this.btnDeleteImage.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(207)))));
            this.btnDeleteImage.BorderRadius = 10;
            this.btnDeleteImage.BorderThickness = 1;
            this.btnDeleteImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeleteImage.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDeleteImage.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDeleteImage.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDeleteImage.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDeleteImage.FillColor = System.Drawing.Color.Transparent;
            this.btnDeleteImage.Font = new System.Drawing.Font("Proxima Nova Lt", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteImage.ForeColor = System.Drawing.Color.Black;
            this.btnDeleteImage.Location = new System.Drawing.Point(145, 107);
            this.btnDeleteImage.Name = "btnDeleteImage";
            this.btnDeleteImage.Size = new System.Drawing.Size(84, 43);
            this.btnDeleteImage.TabIndex = 2;
            this.btnDeleteImage.Text = "Remove";
            this.btnDeleteImage.UseTransparentBackground = true;
            // 
            // itemPic
            // 
            this.itemPic.BackColor = System.Drawing.Color.Transparent;
            this.itemPic.Image = global::PointOfSalesSystem.Properties.Resources.PersonIcon;
            this.itemPic.ImageRotate = 0F;
            this.itemPic.Location = new System.Drawing.Point(30, 58);
            this.itemPic.Name = "itemPic";
            this.itemPic.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.itemPic.Size = new System.Drawing.Size(80, 92);
            this.itemPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.itemPic.TabIndex = 0;
            this.itemPic.TabStop = false;
            this.itemPic.UseTransparentBackground = true;
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
            // 
            // pnlDetails
            // 
            this.pnlDetails.BackColor = System.Drawing.Color.White;
            this.pnlDetails.Controls.Add(this.lblAddressChecker);
            this.pnlDetails.Controls.Add(this.txtAddress);
            this.pnlDetails.Controls.Add(this.lblNumberChecker);
            this.pnlDetails.Controls.Add(this.lblNameChecker);
            this.pnlDetails.Controls.Add(this.label5);
            this.pnlDetails.Controls.Add(this.label3);
            this.pnlDetails.Controls.Add(this.label2);
            this.pnlDetails.Controls.Add(this.label1);
            this.pnlDetails.Controls.Add(this.txtContact);
            this.pnlDetails.Controls.Add(this.txtSupplierName);
            this.pnlDetails.Location = new System.Drawing.Point(278, 63);
            this.pnlDetails.Name = "pnlDetails";
            this.pnlDetails.Size = new System.Drawing.Size(714, 447);
            this.pnlDetails.TabIndex = 7;
            // 
            // lblAddressChecker
            // 
            this.lblAddressChecker.AutoSize = true;
            this.lblAddressChecker.Font = new System.Drawing.Font("Proxima Nova Lt", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddressChecker.ForeColor = System.Drawing.Color.IndianRed;
            this.lblAddressChecker.Location = new System.Drawing.Point(447, 132);
            this.lblAddressChecker.Name = "lblAddressChecker";
            this.lblAddressChecker.Size = new System.Drawing.Size(87, 13);
            this.lblAddressChecker.TabIndex = 23;
            this.lblAddressChecker.Text = "Fill out the field.";
            this.lblAddressChecker.Visible = false;
            // 
            // txtAddress
            // 
            this.txtAddress.Animated = true;
            this.txtAddress.BorderRadius = 5;
            this.txtAddress.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtAddress.DefaultText = "";
            this.txtAddress.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtAddress.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtAddress.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtAddress.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtAddress.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtAddress.Font = new System.Drawing.Font("Proxima Nova Rg", 9.75F);
            this.txtAddress.ForeColor = System.Drawing.Color.Black;
            this.txtAddress.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtAddress.Location = new System.Drawing.Point(184, 120);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.PasswordChar = '\0';
            this.txtAddress.PlaceholderText = "";
            this.txtAddress.SelectedText = "";
            this.txtAddress.Size = new System.Drawing.Size(250, 36);
            this.txtAddress.TabIndex = 1;
            this.txtAddress.TextChanged += new System.EventHandler(this.txtAddress_TextChanged);
            // 
            // lblNumberChecker
            // 
            this.lblNumberChecker.AutoSize = true;
            this.lblNumberChecker.Font = new System.Drawing.Font("Proxima Nova Lt", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberChecker.ForeColor = System.Drawing.Color.IndianRed;
            this.lblNumberChecker.Location = new System.Drawing.Point(447, 187);
            this.lblNumberChecker.Name = "lblNumberChecker";
            this.lblNumberChecker.Size = new System.Drawing.Size(87, 13);
            this.lblNumberChecker.TabIndex = 21;
            this.lblNumberChecker.Text = "Fill out the field.";
            this.lblNumberChecker.Visible = false;
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
            this.label5.Size = new System.Drawing.Size(116, 18);
            this.label5.TabIndex = 6;
            this.label5.Text = "Supplier Details";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Proxima Nova Lt", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(96, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 18);
            this.label3.TabIndex = 5;
            this.label3.Text = "Address";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Proxima Nova Lt", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(70, 183);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "Contact No.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Proxima Nova Lt", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(51, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 18);
            this.label1.TabIndex = 3;
            this.label1.Text = "Supplier Name";
            // 
            // txtContact
            // 
            this.txtContact.Animated = true;
            this.txtContact.BorderRadius = 5;
            this.txtContact.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtContact.DefaultText = "";
            this.txtContact.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtContact.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtContact.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtContact.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtContact.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtContact.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtContact.ForeColor = System.Drawing.Color.Black;
            this.txtContact.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtContact.Location = new System.Drawing.Point(184, 174);
            this.txtContact.Name = "txtContact";
            this.txtContact.PasswordChar = '\0';
            this.txtContact.PlaceholderText = "";
            this.txtContact.SelectedText = "";
            this.txtContact.Size = new System.Drawing.Size(250, 36);
            this.txtContact.TabIndex = 2;
            this.txtContact.TextChanged += new System.EventHandler(this.txtContact_TextChanged);
            this.txtContact.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtContact_KeyPress);
            // 
            // txtSupplierName
            // 
            this.txtSupplierName.Animated = true;
            this.txtSupplierName.BorderRadius = 5;
            this.txtSupplierName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSupplierName.DefaultText = "";
            this.txtSupplierName.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtSupplierName.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtSupplierName.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSupplierName.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSupplierName.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSupplierName.Font = new System.Drawing.Font("Proxima Nova Rg", 9.75F);
            this.txtSupplierName.ForeColor = System.Drawing.Color.Black;
            this.txtSupplierName.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSupplierName.Location = new System.Drawing.Point(184, 65);
            this.txtSupplierName.Name = "txtSupplierName";
            this.txtSupplierName.PasswordChar = '\0';
            this.txtSupplierName.PlaceholderText = "";
            this.txtSupplierName.SelectedText = "";
            this.txtSupplierName.Size = new System.Drawing.Size(250, 36);
            this.txtSupplierName.TabIndex = 0;
            this.txtSupplierName.TextChanged += new System.EventHandler(this.txtSupplierName_TextChanged);
            // 
            // pnlIngredients
            // 
            this.pnlIngredients.BackColor = System.Drawing.Color.White;
            this.pnlIngredients.Controls.Add(this.guna2TextBox3);
            this.pnlIngredients.Controls.Add(this.label4);
            this.pnlIngredients.Location = new System.Drawing.Point(-1, 265);
            this.pnlIngredients.Name = "pnlIngredients";
            this.pnlIngredients.Size = new System.Drawing.Size(278, 245);
            this.pnlIngredients.TabIndex = 8;
            // 
            // guna2TextBox3
            // 
            this.guna2TextBox3.Animated = true;
            this.guna2TextBox3.BorderThickness = 0;
            this.guna2TextBox3.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.guna2TextBox3.DefaultText = "";
            this.guna2TextBox3.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.guna2TextBox3.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.guna2TextBox3.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox3.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox3.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox3.Font = new System.Drawing.Font("Proxima Nova Rg", 11.25F);
            this.guna2TextBox3.ForeColor = System.Drawing.Color.Black;
            this.guna2TextBox3.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox3.Location = new System.Drawing.Point(30, 55);
            this.guna2TextBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.guna2TextBox3.Multiline = true;
            this.guna2TextBox3.Name = "guna2TextBox3";
            this.guna2TextBox3.PasswordChar = '\0';
            this.guna2TextBox3.PlaceholderText = "No items...";
            this.guna2TextBox3.SelectedText = "";
            this.guna2TextBox3.Size = new System.Drawing.Size(211, 155);
            this.guna2TextBox3.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Proxima Nova Lt", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(122)))), ((int)(((byte)(50)))));
            this.label4.Location = new System.Drawing.Point(20, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 18);
            this.label4.TabIndex = 5;
            this.label4.Text = "Supplier Items";
            // 
            // EditSupplierForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(990, 510);
            this.Controls.Add(this.pnlIngredients);
            this.Controls.Add(this.pnlImage);
            this.Controls.Add(this.pnlDetails);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "EditSupplierForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EditSupplierForm";
            this.Load += new System.EventHandler(this.EditSupplierForm_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEdit)).EndInit();
            this.pnlImage.ResumeLayout(false);
            this.pnlImage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.itemPic)).EndInit();
            this.pnlDetails.ResumeLayout(false);
            this.pnlDetails.PerformLayout();
            this.pnlIngredients.ResumeLayout(false);
            this.pnlIngredients.PerformLayout();
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
        private Guna.UI2.WinForms.Guna2Button btnDeleteImage;
        private Guna.UI2.WinForms.Guna2CirclePictureBox itemPic;
        private Guna.UI2.WinForms.Guna2Button btnEditImage;
        private Guna.UI2.WinForms.Guna2Panel pnlDetails;
        internal System.Windows.Forms.Label lblNumberChecker;
        internal System.Windows.Forms.Label lblNameChecker;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2TextBox txtContact;
        private Guna.UI2.WinForms.Guna2TextBox txtSupplierName;
        private Guna.UI2.WinForms.Guna2TextBox txtAddress;
        internal System.Windows.Forms.Label lblAddressChecker;
        private Guna.UI2.WinForms.Guna2Panel pnlIngredients;
        private Guna.UI2.WinForms.Guna2TextBox guna2TextBox3;
        private System.Windows.Forms.Label label4;
    }
}