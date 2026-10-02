using BillingSystem.Database;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BillingSystem
{
    public partial class CustomerListForm : Form
    {
        public CustomerListForm()
        {
            InitializeComponent();
            ConfigureDataGridView();
        }
        // <-- close constructor

        // Stores the CustomerID of the currently selected row. 
        // 0 means no customer is currently selected. 
        private int _selectedCustomerId = 0;

        private void dgvCustomers_SelectionChanged(object sender, EventArgs e)
        {
            // If no row is selected (e.g., grid is empty), do nothing 
            if (dataGridView1.CurrentRow == null) return;

            // Read the CustomerID value from the selected row 
            var idCell = dataGridView1.CurrentRow.Cells["CustomerID"].Value;

            if (idCell != null && int.TryParse(idCell.ToString(), out int id))
            {
                _selectedCustomerId = id;
            }
        }
        private void dgvCustomers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // e.RowIndex is -1 when the header row is double-clicked — ignore it 
            if (e.RowIndex < 0) return;

            OpenEditForm();
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
                MessageBox.Show($"Error searching customers:\n{ex.Message}", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenEditForm()
        {
            if (_selectedCustomerId == 0)
            {
                MessageBox.Show("Please select a customer to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Open AddCustomerForm in EDIT mode, passing the selected CustomerID 
            AddCustomerForm editForm = new AddCustomerForm(_selectedCustomerId);

            // Refresh the grid automatically once the edit form closes 
            editForm.FormClosed += (s, args) => LoadCustomers();

            editForm.ShowDialog(this);
        }

        private void DeleteCustomer(int customerId)
        {
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // Parameterized DELETE — removes exactly one row 
                    string sql = "DELETE FROM Customers WHERE CustomerID = @CustomerID; ";


                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", customerId);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Customer deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            LoadCustomers();   // Refresh the grid
                            _selectedCustomerId = 0;   // Clear selection tracker 
                        }
                        else
                        {
                            MessageBox.Show("Customer could not be deleted. It may no longer exist.", "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting customer:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Step 1: Make sure a customer is selected 
            if (_selectedCustomerId == 0)
            {
                MessageBox.Show("Please select a customer to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Step 2: Confirm before deleting — this cannot be undone 
            DialogResult confirm = MessageBox.Show("Are you sure you want to delete this customer?\n" + 
                                                   "All billing records for this customer will also be deleted.",
                                                   "Confirm Delete",
                                                   MessageBoxButtons.YesNo,
                                                   MessageBoxIcon.Warning);

            // Step 3: Only delete if the user clicked Yes 
            if (confirm == DialogResult.Yes)
            {
                DeleteCustomer(_selectedCustomerId);
            }
            // If the user clicked No, do nothing — the record is preserved 
        }
    }
}