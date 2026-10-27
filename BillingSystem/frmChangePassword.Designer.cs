namespace BillingSystem
{
    partial class frmChangePassword
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
            lbl_changePassword = new Label();
            lblCurrentPassword = new Label();
            txtCurrentPassword = new TextBox();
            lblNewPassword = new Label();
            txtNewPassword = new TextBox();
            lblRetypePassword = new Label();
            txtRetypePassword = new TextBox();
            btnSavePassword = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // lbl_changePassword
            // 
            lbl_changePassword.AutoSize = true;
            lbl_changePassword.Font = new Font("Bookman Old Style", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_changePassword.Location = new Point(123, 21);
            lbl_changePassword.Name = "lbl_changePassword";
            lbl_changePassword.Size = new Size(196, 24);
            lbl_changePassword.TabIndex = 0;
            lbl_changePassword.Text = "Change Password ";
            lbl_changePassword.TextAlign = ContentAlignment.TopCenter;
            // 
            // lblCurrentPassword
            // 
            lblCurrentPassword.AutoSize = true;
            lblCurrentPassword.Font = new Font("Baskerville Old Face", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCurrentPassword.Location = new Point(32, 83);
            lblCurrentPassword.Name = "lblCurrentPassword";
            lblCurrentPassword.Size = new Size(139, 21);
            lblCurrentPassword.TabIndex = 1;
            lblCurrentPassword.Text = "Current Password:";
            // 
            // txtCurrentPassword
            // 
            txtCurrentPassword.Location = new Point(183, 77);
            txtCurrentPassword.Name = "txtCurrentPassword";
            txtCurrentPassword.Size = new Size(231, 27);
            txtCurrentPassword.TabIndex = 2;
            // 
            // lblNewPassword
            // 
            lblNewPassword.AutoSize = true;
            lblNewPassword.Font = new Font("Baskerville Old Face", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblNewPassword.Location = new Point(32, 132);
            lblNewPassword.Name = "lblNewPassword";
            lblNewPassword.Size = new Size(117, 21);
            lblNewPassword.TabIndex = 3;
            lblNewPassword.Text = "New Password:";
            // 
            // txtNewPassword
            // 
            txtNewPassword.Location = new Point(183, 126);
            txtNewPassword.Name = "txtNewPassword";
            txtNewPassword.Size = new Size(231, 27);
            txtNewPassword.TabIndex = 4;
            // 
            // lblRetypePassword
            // 
            lblRetypePassword.AutoSize = true;
            lblRetypePassword.Font = new Font("Baskerville Old Face", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRetypePassword.Location = new Point(32, 185);
            lblRetypePassword.Name = "lblRetypePassword";
            lblRetypePassword.Size = new Size(134, 21);
            lblRetypePassword.TabIndex = 5;
            lblRetypePassword.Text = "Retype Password:";
            // 
            // txtRetypePassword
            // 
            txtRetypePassword.Location = new Point(183, 179);
            txtRetypePassword.Name = "txtRetypePassword";
            txtRetypePassword.Size = new Size(231, 27);
            txtRetypePassword.TabIndex = 6;
            // 
            // btnSavePassword
            // 
            btnSavePassword.Font = new Font("Baskerville Old Face", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSavePassword.Location = new Point(85, 249);
            btnSavePassword.Name = "btnSavePassword";
            btnSavePassword.Size = new Size(129, 29);
            btnSavePassword.TabIndex = 7;
            btnSavePassword.Text = "Save Password";
            btnSavePassword.UseVisualStyleBackColor = true;
            btnSavePassword.Click += btnSavePassword_Click;
            // 
            // btnCancel
            // 
            btnCancel.Font = new Font("Baskerville Old Face", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancel.Location = new Point(234, 249);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(129, 29);
            btnCancel.TabIndex = 8;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // frmChangePassword
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(437, 323);
            Controls.Add(btnCancel);
            Controls.Add(btnSavePassword);
            Controls.Add(txtRetypePassword);
            Controls.Add(lblRetypePassword);
            Controls.Add(txtNewPassword);
            Controls.Add(lblNewPassword);
            Controls.Add(txtCurrentPassword);
            Controls.Add(lblCurrentPassword);
            Controls.Add(lbl_changePassword);
            Name = "frmChangePassword";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmChangePassword";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbl_changePassword;
        private Label lblCurrentPassword;
        private TextBox txtCurrentPassword;
        private Label lblNewPassword;
        private TextBox txtNewPassword;
        private Label lblRetypePassword;
        private TextBox txtRetypePassword;
        private Button btnSavePassword;
        private Button btnCancel;
    }
}