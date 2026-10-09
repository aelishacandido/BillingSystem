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
using ClosedXML.Excel;
using System.IO;
using PdfSharp.Pdf;
using PdfSharp.Drawing;

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

        private void btnAnalytics_Click(object sender, EventArgs e)
        {
            frmAnalytics analytics = new frmAnalytics();
            analytics.ShowDialog();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            // Make sure there is something to export 
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("There are no records to export.", "Export to Excel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Let the user choose where to save the file 
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "Excel Workbook (*.xlsx)|*.xlsx";
                saveDialog.FileName = "CustomerList.xlsx";

                if (saveDialog.ShowDialog() != DialogResult.OK) return;

                try
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Customers");

                        // Write column headers in row 1 
                        for (int col = 0; col < dataGridView1.Columns.Count; col++)
                        {
                            worksheet.Cell(1, col + 1).Value = dataGridView1.Columns[col].HeaderText;
                            worksheet.Cell(1, col + 1).Style.Font.Bold = true;
                        }

                        // Write each data row starting from row 2 
                        for (int row = 0; row < dataGridView1.Rows.Count; row++)
                        {
                            for (int col = 0; col < dataGridView1.Columns.Count; col++)
                            {
                                var cellValue = dataGridView1.Rows[row].Cells[col].Value;
                                worksheet.Cell(row + 2, col + 1).Value = cellValue?.ToString() ?? "";
                            }
                        }

                        // Auto-adjust column widths to fit the content 
                        worksheet.Columns().AdjustToContents();

                        workbook.SaveAs(saveDialog.FileName);
                    }

                    MessageBox.Show("Customer list exported successfully to Excel.", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error exporting to Excel:\n{ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            // Make sure there is something to export 
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("There are no records to export.", "Export to PDF", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "PDF Document (*.pdf)|*.pdf";
                saveDialog.FileName = "CustomerList.pdf";

                if (saveDialog.ShowDialog() != DialogResult.OK) return;

                try
                {
                    using (PdfDocument document = new PdfDocument())
                    {
                        // Create a new page set to Landscape orientation 
                        PdfPage page = document.AddPage();
                        page.Orientation = PdfSharp.PageOrientation.Landscape;

                        XGraphics gfx = XGraphics.FromPdfPage(page);
                        {
                            XFont titleFont = new XFont("Arial", 16, XFontStyleEx.Bold);
                            XFont headerFont = new XFont("Arial", 10, XFontStyleEx.Bold);
                            XFont cellFont = new XFont("Arial", 9, XFontStyleEx.Regular);

                            // Title 
                            gfx.DrawString("Customer List Report", titleFont, XBrushes.Black,
                                new XRect(0, 20, page.Width.Point, 30), XStringFormats.TopCenter);

                            int columnCount = dataGridView1.Columns.Count;
                            double margin = 30;
                            double tableWidth = page.Width.Point - (margin * 2);
                            double colWidth = tableWidth / columnCount;
                            double rowHeight = 22;
                            double y = 60;

                            // Draw column headers 
                            double x = margin;
                            for (int col = 0; col < columnCount; col++)
                            {
                                gfx.DrawString(dataGridView1.Columns[col].HeaderText, headerFont,
                                    XBrushes.Black, new XRect(x, y, colWidth, rowHeight),
                                    XStringFormats.CenterLeft);
                                x += colWidth;
                            }

                            y += rowHeight;
                            gfx.DrawLine(XPens.Black, margin, y, page.Width.Point - margin, y);

                            // Draw each data row 
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                x = margin;
                                y += rowHeight;

                                // Start a new page if we run out of vertical space 
                                if (y > page.Height.Point - margin)
                                {
                                    page = document.AddPage();
                                    page.Orientation = PdfSharp.PageOrientation.Landscape;
                                    gfx.Dispose();
                                    gfx = XGraphics.FromPdfPage(page);
                                    y = 40;
                                }

                                for (int col = 0; col < columnCount; col++)
                                {
                                    string text = row.Cells[col].Value?.ToString() ?? "";
                                    gfx.DrawString(text, cellFont, XBrushes.Black,
                                        new XRect(x, y, colWidth, rowHeight),
                                        XStringFormats.CenterLeft);
                                    x += colWidth;
                                }
                            }
                        }

                        gfx.Dispose();   // finish drawing before saving 
                        document.Save(saveDialog.FileName);
                    }

                    MessageBox.Show("Customer list exported successfully to PDF.", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error exporting to PDF:\n{ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}