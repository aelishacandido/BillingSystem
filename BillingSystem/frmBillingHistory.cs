using BillingSystem.Database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BillingSystem
{
    public partial class frmBillingHistory : Form
    {
        private int customerId;
        public frmBillingHistory(int customerId)
        {
            this.customerId = customerId;
            InitializeComponent();

            MessageBox.Show("Customer ID Received: " + customerId);
        }

        private void frmBillingHistory_Load(object sender, EventArgs e)
        {
            LoadCustomerName();
            LoadBillingRecords();

            MessageBox.Show("Form Loaded");
        }

        private void LoadCustomerName()
        {
            string query = @"SELECT FullName
                             FROM Customers
                             WHERE CustomerID = @CustomerID";

            using (MySqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CustomerID", customerId);

                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        lblTitle.Text =
                            "Billing History - " + result.ToString();
                    }
                }
            }
        }

        private void LoadBillingRecords()
        {
            string query = @"
                SELECT
                    BillingMonth,
                    PreviousReading,
                    PresentReading,
                    Consumption,
                    RatePerCubic,
                    TotalAmount,
                    Status
                FROM Billing
                WHERE CustomerID = @CustomerID
                ORDER BY BillingDate DESC";

            using (MySqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CustomerID", customerId);

                    MySqlDataAdapter adapter =
                        new MySqlDataAdapter(cmd);

                    DataTable dt = new DataTable();

                    adapter.Fill(dt);

                    dgvBillingHistory.AutoGenerateColumns = true;
                    dgvBillingHistory.DataSource = dt;

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show(
                            "No billing records found for this customer.",
                            "No Records Found",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
        }
        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
