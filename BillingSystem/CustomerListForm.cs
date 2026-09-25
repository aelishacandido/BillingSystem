using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using MySql.Data.MySqlClient;
using BillingSystem.Database;

namespace BillingSystem
{
    public partial class CustomerListForm : Form
    {
        public CustomerListForm()
        {
            InitializeComponent();
            ConfigureDataGridView();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                // Empty search box → show all customers again 
                LoadCustomers();
            }
            else
            {
                SearchCustomers(keyword);
            }
        } 

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddCustomerForm addCustomerForm = new AddCustomerForm();
            addCustomerForm.ShowDialog();
            LoadCustomers();   // enable this line in Step 4.4 
        }

        private void LoadCustomers()
        {
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // SELECT all customers, most recently added first 
                    string sql = @"SELECT CustomerID, 
                                  FullName, 
                                  Address, 
                                  ContactNumber, 
                                  Email, 
                                  Balance, 
                                  Status 
                           FROM   Customers 
                           ORDER  BY FullName ASC;";

                    using (var adapter = new MySqlDataAdapter(sql, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // Bind the DataTable to the grid 
                        dataGridView1.AutoGenerateColumns = false;
                        dataGridView1.DataSource = dt;

                        // Improve column headers for readability 
                        if (dataGridView1.Columns.Count > 0)
                        {
                            dataGridView1.Columns["CustomerID"].HeaderText = "ID";
                            dataGridView1.Columns["FullName"].HeaderText = "Full Name";
                            dataGridView1.Columns["Address"].HeaderText = "Address";
                            dataGridView1.Columns["ContactNumber"].HeaderText = "Contact No.";
                            dataGridView1.Columns["Email"].HeaderText = "Email";
                            dataGridView1.Columns["Balance"].HeaderText = "Balance (₱)";
                        }

                        lblTitle.Text = $"Customer List  ({dt.Rows.Count} record(s))";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customers:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ConfigureDataGridView()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns["CustomerID"].DataPropertyName = "CustomerID";
            dataGridView1.Columns["FullName"].DataPropertyName = "FullName";
            dataGridView1.Columns["Address"].DataPropertyName = "Address";
            dataGridView1.Columns["ContactNumber"].DataPropertyName = "ContactNumber";
            dataGridView1.Columns["Email"].DataPropertyName = "Email";
            dataGridView1.Columns["Balance"].DataPropertyName = "Balance";
        }
        private void CustomerListForm_Load(object sender, EventArgs e)
        {
            LoadCustomers();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSearch_Click(sender, e);
            }
        }

        private void SearchCustomers(string keyword)
        {
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // Parameterized SELECT with WHERE ... LIKE 
                    string sql = @"SELECT CustomerID, 
                                  FullName, 
                                  Address, 
                                  ContactNumber, 
                                  Email, 
                                  Balance, 
                                  Status 
                           FROM   Customers 
                           WHERE  FullName      LIKE @keyword 
                              OR  Address       LIKE @keyword 
                              OR  ContactNumber LIKE @keyword 
                           ORDER  BY FullName ASC;";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        // %keyword% matches the search text anywhere in the column
                        
                        cmd.Parameters.AddWithValue("@keyword", $"%{keyword}%"); 

                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            dataGridView1.DataSource = dt;
                            lblTitle.Text = $"Customer List  ({dt.Rows.Count} result(s))"; 
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching customers:\n{ex.Message}",
                    "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
 }
