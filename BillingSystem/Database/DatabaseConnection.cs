using System;
using MySql.Data.MySqlClient;

namespace BillingSystem.Database
{
    // This class provides a reusable database connection. 
    // Every form that needs to query MySQL will call 
    // DatabaseConnection.GetConnection() instead of 
    // writing its own connection string. 
    public class DatabaseConnection
    {
        // Connection string settings — update Password if needed 
        // To the server machine's IP address: 
        private const string SERVER = "localhost";  
        private const string DATABASE = "BillingDB";
        /*private const string UID = "root";
        private const string PASSWORD = "Elay123%";  // Add your MySQL password here 
        */
        private const string UID = "billing_user";
        private const string PASSWORD = "BillingPass123!";
        private static string ConnectionString =>

$"server={SERVER};database={DATABASE};uid={UID};pwd={PASSWORD};";

        // Returns an open-ready MySqlConnection object. 
        // Always use inside a 'using' block so it closes automatically. 
        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        // Returns true if the app can connect to MySQL. 
        // Used by LoginForm on startup to warn the user early. 
        public static bool TestConnection()
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}