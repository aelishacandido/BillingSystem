using System;
using MySql.Data.MySqlClient;
using BillingSystem.Database;

namespace BillingSystem.Utils
{
    /// <summary> 
    /// Provides a single method to insert an audit log entry. 
    /// Call AuditLogger.Log(action, details) from any form. 
    /// </summary> 
    public static class AuditLogger
    {
        public static void Log(string action, string details = "")
        {
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string sql = @"INSERT INTO AuditLogs (UserID, Username, Role, Action, Details) VALUES (@UserID, @Username, @Role, @Action, @Details);";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID",AppSession.CurrentUserID);
                        cmd.Parameters.AddWithValue("@Username",AppSession.CurrentUsername);
                        cmd.Parameters.AddWithValue("@Role",AppSession.CurrentRole);
                        cmd.Parameters.AddWithValue("@Action", action);
                        cmd.Parameters.AddWithValue("@Details", details);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                // Silently ignore log failures — never crash the app 
                // because of a logging error 
            }
        }
    }
}