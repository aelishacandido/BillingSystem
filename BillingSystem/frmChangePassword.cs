using BillingSystem.Database;
using BillingSystem.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BillingSystem
{
    public partial class frmChangePassword : Form
    {
        private int currentUserId;

        public frmChangePassword(int loggedInUserID)
        {
            InitializeComponent();

            txtCurrentPassword.UseSystemPasswordChar = true;
            txtNewPassword.UseSystemPasswordChar = true;
            txtRetypePassword.UseSystemPasswordChar = true;

            currentUserId = loggedInUserID;
        }

        private void btnSavePassword_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    string currentPassword = "";

                    string sql = @"SELECT Password
                                   FROM Users
                                   WHERE UserID=@UserID";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue(
                            "@UserID",
                            AppSession.CurrentUserID);

                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            currentPassword = result.ToString();
                        }
                    }

                    if (txtCurrentPassword.Text
                       != currentPassword)
                    {
                        MessageBox.Show(
                            "Current password is incorrect.");

                        return;
                    }

                    if (txtNewPassword.Text
                       != txtRetypePassword.Text)
                    {
                        MessageBox.Show(
                            "Passwords do not match.");

                        return;
                    }

                    StringBuilder errors = new StringBuilder();

                    string password = txtNewPassword.Text;

                    if (password.Length < 8)
                        errors.AppendLine(
                            "Minimum 8 characters required");

                    if (!Regex.IsMatch(password, "[A-Z]"))
                        errors.AppendLine(
                            "At least one uppercase letter required");

                    if (!Regex.IsMatch(password, "[a-z]"))
                        errors.AppendLine(
                            "At least one lowercase letter required");

                    if (!Regex.IsMatch(password, "[0-9]"))
                        errors.AppendLine(
                            "At least one number required");

                    if (!Regex.IsMatch(
                        password,
                        @"[!@#$%^&*()]"))
                        errors.AppendLine(
                            "At least one special character required");

                    if (errors.Length > 0)
                    {
                        MessageBox.Show(
                            errors.ToString());

                        return;
                    }

                    string updateSql = @"UPDATE Users
                                         SET Password=@Password
                                         WHERE UserID=@UserID";

                    using (var updateCmd =
                          new MySqlCommand(
                          updateSql,
                          conn))
                    {
                        updateCmd.Parameters.AddWithValue(
                            "@Password",
                            txtNewPassword.Text);

                        updateCmd.Parameters.AddWithValue(
                            "@UserID",
                            AppSession.CurrentUserID);

                        updateCmd.ExecuteNonQuery();
                    }

                    AuditLogger.Log(
                        "CHANGE_PASSWORD",
                        $"{AppSession.CurrentUsername} changed password.");

                    MessageBox.Show(
                        "Password changed successfully.",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error:\n{ex.Message}",
                    "Change Password Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                 "Are you sure you want to cancel?",
                 "Confirm Cancel",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
