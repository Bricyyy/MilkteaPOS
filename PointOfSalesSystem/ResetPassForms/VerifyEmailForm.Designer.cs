namespace PointOfSalesSystem.ResetPassForms
{
    partial class VerifyEmailForm
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
            this.guna2ShadowPanel1 = new Guna.UI2.WinForms.Guna2ShadowPanel();
            this.lblTimer = new System.Windows.Forms.Label();
            this.lblResend = new System.Windows.Forms.Label();
            this.lblQuestion = new System.Windows.Forms.Label();
            this.lblChecker = new System.Windows.Forms.Label();
            this.txtCode6 = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtCode5 = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtCode4 = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtCode3 = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtCode2 = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtCode1 = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.btnBack = new Guna.UI2.WinForms.Guna2Button();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.guna2ShadowPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2ShadowPanel1
            // 
            this.guna2ShadowPanel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2ShadowPanel1.Controls.Add(this.lblTimer);
            this.guna2ShadowPanel1.Controls.Add(this.lblResend);
            this.guna2ShadowPanel1.Controls.Add(this.lblQuestion);
            this.guna2ShadowPanel1.Controls.Add(this.lblChecker);
            this.guna2ShadowPanel1.Controls.Add(this.txtCode6);
            this.guna2ShadowPanel1.Controls.Add(this.txtCode5);
            this.guna2ShadowPanel1.Controls.Add(this.txtCode4);
            this.guna2ShadowPanel1.Controls.Add(this.txtCode3);
            this.guna2ShadowPanel1.Controls.Add(this.txtCode2);
            this.guna2ShadowPanel1.Controls.Add(this.txtCode1);
            this.guna2ShadowPanel1.Controls.Add(this.lblEmail);
            this.guna2ShadowPanel1.Controls.Add(this.btnBack);
            this.guna2ShadowPanel1.Controls.Add(this.Label3);
            this.guna2ShadowPanel1.Controls.Add(this.Label2);
            this.guna2ShadowPanel1.FillColor = System.Drawing.Color.White;
            this.guna2ShadowPanel1.Location = new System.Drawing.Point(0, 0);
            this.guna2ShadowPanel1.Name = "guna2ShadowPanel1";
            this.guna2ShadowPanel1.ShadowColor = System.Drawing.Color.Black;
            this.guna2ShadowPanel1.Size = new System.Drawing.Size(375, 335);
            this.guna2ShadowPanel1.TabIndex = 0;
            // 
            // lblTimer
            // 
            this.lblTimer.Font = new System.Drawing.Font("Proxima Nova Rg", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(207)))));
            this.lblTimer.Location = new System.Drawing.Point(53, 221);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(272, 15);
            this.lblTimer.TabIndex = 26;
            this.lblTimer.Text = "Please wait 30 seconds to resend.";
            this.lblTimer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblResend
            // 
            this.lblResend.AutoSize = true;
            this.lblResend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblResend.Font = new System.Drawing.Font("Proxima Nova Rg", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResend.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(19)))));
            this.lblResend.Location = new System.Drawing.Point(165, 280);
            this.lblResend.Name = "lblResend";
            this.lblResend.Size = new System.Drawing.Size(49, 16);
            this.lblResend.TabIndex = 25;
            this.lblResend.Text = "Resend";
            this.lblResend.Visible = false;
            this.lblResend.Click += new System.EventHandler(this.lblResend_Click);
            // 
            // lblQuestion
            // 
            this.lblQuestion.AutoSize = true;
            this.lblQuestion.Font = new System.Drawing.Font("Proxima Nova Rg", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuestion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(207)))));
            this.lblQuestion.Location = new System.Drawing.Point(117, 264);
            this.lblQuestion.Name = "lblQuestion";
            this.lblQuestion.Size = new System.Drawing.Size(147, 16);
            this.lblQuestion.TabIndex = 24;
            this.lblQuestion.Text = "Did not receive the code?";
            this.lblQuestion.Visible = false;
            // 
            // lblChecker
            // 
            this.lblChecker.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblChecker.AutoSize = true;
            this.lblChecker.Font = new System.Drawing.Font("Proxima Nova Lt", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChecker.ForeColor = System.Drawing.Color.IndianRed;
            this.lblChecker.Location = new System.Drawing.Point(119, 190);
            this.lblChecker.Name = "lblChecker";
            this.lblChecker.Size = new System.Drawing.Size(138, 13);
            this.lblChecker.TabIndex = 23;
            this.lblChecker.Text = "Verification code is invalid.";
            this.lblChecker.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblChecker.Visible = false;
            // 
            // txtCode6
            // 
            this.txtCode6.Animated = true;
            this.txtCode6.BorderRadius = 5;
            this.txtCode6.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCode6.DefaultText = "";
            this.txtCode6.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtCode6.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtCode6.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtCode6.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtCode6.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtCode6.Font = new System.Drawing.Font("Proxima Nova Rg", 15.75F);
            this.txtCode6.ForeColor = System.Drawing.Color.Black;
            this.txtCode6.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtCode6.Location = new System.Drawing.Point(288, 141);
            this.txtCode6.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.txtCode6.Name = "txtCode6";
            this.txtCode6.PasswordChar = '\0';
            this.txtCode6.PlaceholderText = "";
            this.txtCode6.SelectedText = "";
            this.txtCode6.Size = new System.Drawing.Size(37, 44);
            this.txtCode6.TabIndex = 5;
            this.txtCode6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCode5
            // 
            this.txtCode5.Animated = true;
            this.txtCode5.BorderRadius = 5;
            this.txtCode5.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCode5.DefaultText = "";
            this.txtCode5.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtCode5.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtCode5.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtCode5.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtCode5.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtCode5.Font = new System.Drawing.Font("Proxima Nova Rg", 15.75F);
            this.txtCode5.ForeColor = System.Drawing.Color.Black;
            this.txtCode5.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtCode5.Location = new System.Drawing.Point(241, 141);
            this.txtCode5.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.txtCode5.Name = "txtCode5";
            this.txtCode5.PasswordChar = '\0';
            this.txtCode5.PlaceholderText = "";
            this.txtCode5.SelectedText = "";
            this.txtCode5.Size = new System.Drawing.Size(37, 44);
            this.txtCode5.TabIndex = 4;
            this.txtCode5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCode4
            // 
            this.txtCode4.Animated = true;
            this.txtCode4.BorderRadius = 5;
            this.txtCode4.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCode4.DefaultText = "";
            this.txtCode4.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtCode4.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtCode4.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtCode4.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtCode4.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtCode4.Font = new System.Drawing.Font("Proxima Nova Rg", 15.75F);
            this.txtCode4.ForeColor = System.Drawing.Color.Black;
            this.txtCode4.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtCode4.Location = new System.Drawing.Point(194, 141);
            this.txtCode4.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.txtCode4.Name = "txtCode4";
            this.txtCode4.PasswordChar = '\0';
            this.txtCode4.PlaceholderText = "";
            this.txtCode4.SelectedText = "";
            this.txtCode4.Size = new System.Drawing.Size(37, 44);
            this.txtCode4.TabIndex = 3;
            this.txtCode4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCode3
            // 
            this.txtCode3.Animated = true;
            this.txtCode3.BorderRadius = 5;
            this.txtCode3.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCode3.DefaultText = "";
            this.txtCode3.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtCode3.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtCode3.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtCode3.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtCode3.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtCode3.Font = new System.Drawing.Font("Proxima Nova Rg", 15.75F);
            this.txtCode3.ForeColor = System.Drawing.Color.Black;
            this.txtCode3.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtCode3.Location = new System.Drawing.Point(147, 141);
            this.txtCode3.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.txtCode3.Name = "txtCode3";
            this.txtCode3.PasswordChar = '\0';
            this.txtCode3.PlaceholderText = "";
            this.txtCode3.SelectedText = "";
            this.txtCode3.Size = new System.Drawing.Size(37, 44);
            this.txtCode3.TabIndex = 2;
            this.txtCode3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCode2
            // 
            this.txtCode2.Animated = true;
            this.txtCode2.BorderRadius = 5;
            this.txtCode2.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCode2.DefaultText = "";
            this.txtCode2.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtCode2.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtCode2.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtCode2.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtCode2.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtCode2.Font = new System.Drawing.Font("Proxima Nova Rg", 15.75F);
            this.txtCode2.ForeColor = System.Drawing.Color.Black;
            this.txtCode2.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtCode2.Location = new System.Drawing.Point(100, 141);
            this.txtCode2.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.txtCode2.Name = "txtCode2";
            this.txtCode2.PasswordChar = '\0';
            this.txtCode2.PlaceholderText = "";
            this.txtCode2.SelectedText = "";
            this.txtCode2.Size = new System.Drawing.Size(37, 44);
            this.txtCode2.TabIndex = 1;
            this.txtCode2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCode1
            // 
            this.txtCode1.Animated = true;
            this.txtCode1.BorderRadius = 5;
            this.txtCode1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCode1.DefaultText = "";
            this.txtCode1.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtCode1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtCode1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtCode1.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtCode1.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtCode1.Font = new System.Drawing.Font("Proxima Nova Rg", 15.75F);
            this.txtCode1.ForeColor = System.Drawing.Color.Black;
            this.txtCode1.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtCode1.Location = new System.Drawing.Point(53, 141);
            this.txtCode1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.txtCode1.Name = "txtCode1";
            this.txtCode1.PasswordChar = '\0';
            this.txtCode1.PlaceholderText = "";
            this.txtCode1.SelectedText = "";
            this.txtCode1.Size = new System.Drawing.Size(37, 44);
            this.txtCode1.TabIndex = 0;
            this.txtCode1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblEmail
            // 
            this.lblEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEmail.Font = new System.Drawing.Font("Proxima Nova Rg", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.Location = new System.Drawing.Point(77, 101);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(228, 18);
            this.lblEmail.TabIndex = 7;
            this.lblEmail.Text = "sample@gmail.com";
            this.lblEmail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnBack
            // 
            this.btnBack.Animated = true;
            this.btnBack.BackColor = System.Drawing.Color.Transparent;
            this.btnBack.BorderRadius = 10;
            this.btnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBack.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnBack.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnBack.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnBack.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnBack.FillColor = System.Drawing.Color.Transparent;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Image = global::PointOfSalesSystem.Properties.Resources.left_arrow_icon;
            this.btnBack.ImageSize = new System.Drawing.Size(30, 30);
            this.btnBack.Location = new System.Drawing.Point(12, 12);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(40, 40);
            this.btnBack.TabIndex = 6;
            this.btnBack.UseTransparentBackground = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Font = new System.Drawing.Font("Proxima Nova Rg", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(74, 85);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(231, 16);
            this.Label3.TabIndex = 5;
            this.Label3.Text = "Your verification code is sent by email to:";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Proxima Nova Lt", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(87, 29);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(209, 23);
            this.Label2.TabIndex = 4;
            this.Label2.Text = "Enter Verification Code";
            // 
            // VerifyEmailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(375, 335);
            this.Controls.Add(this.guna2ShadowPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "VerifyEmailForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VerifyEmail";
            this.Load += new System.EventHandler(this.VerifyEmailForm_Load);
            this.guna2ShadowPanel1.ResumeLayout(false);
            this.guna2ShadowPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2ShadowPanel guna2ShadowPanel1;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label2;
        private Guna.UI2.WinForms.Guna2Button btnBack;
        internal Guna.UI2.WinForms.Guna2TextBox txtCode1;
        internal System.Windows.Forms.Label lblEmail;
        public System.Windows.Forms.Label lblChecker;
        internal Guna.UI2.WinForms.Guna2TextBox txtCode6;
        internal Guna.UI2.WinForms.Guna2TextBox txtCode5;
        internal Guna.UI2.WinForms.Guna2TextBox txtCode4;
        internal Guna.UI2.WinForms.Guna2TextBox txtCode3;
        internal Guna.UI2.WinForms.Guna2TextBox txtCode2;
        internal System.Windows.Forms.Label lblTimer;
        internal System.Windows.Forms.Label lblResend;
        internal System.Windows.Forms.Label lblQuestion;
    }
}