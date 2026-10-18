using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using BillingSystem.Database;
using BillingSystem.Utils;

namespace BillingSystem
{
    public partial class frmManagePermissions : Form
    {
        // Maps each PermissionName to its checkbox control 
        private Dictionary<string, CheckBox> _permCheckboxes;

        public frmManagePermissions()
        {
            InitializeComponent();
        }

        private void frmManagePermissions_Load(object sender, EventArgs e)
        {
            // Map PermissionName strings to their checkbox controls 
            _permCheckboxes = new Dictionary<string, CheckBox>
            {
                { "AddCustomer",       chkAddCustomer    },
                { "EditCustomer",      chkEditCustomer   },
                { "DeleteCustomer",    chkDeleteCustomer },
                { "Analytics",         chkAnalytics      },
                { "ExportExcel",       chkExportExcel    },
                { "ExportPdf",         chkExportPdf      },
                { "AuditLogs",         chkAuditLogs      },
            };

            // Populate dropdown and load first role 
            //cmbRole.Items.AddRange(new object[] { "Admin", "Cashier" }); 
            cmbRole.SelectedIndex = 0;
        }

        private void cmbRole_SelectedIndexChanged(object sender, EventArgs
e)
        {
            if (cmbRole.SelectedItem != null)
                LoadPermissions(cmbRole.SelectedItem.ToString());
        }

        private void LoadPermissions(string role)
        {
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string sql = @"SELECT PermissionName, IsAllowed 
                                   FROM   UserPermissions 
                                   WHERE  Role = @Role;";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Role", role);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string permName = reader.GetString("PermissionName");
                                if (permName == "ManagePermissions")
                                    continue;   // no checkbox for this one 
                                bool isAllowed = reader.GetBoolean("IsAllowed");

                                if (_permCheckboxes.ContainsKey(permName))
                                    _permCheckboxes[permName].Checked = isAllowed;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading permissions:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbRole.SelectedItem == null) return;
            string role = cmbRole.SelectedItem.ToString();

            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string sql = @"UPDATE UserPermissions 
                                   SET    IsAllowed = @IsAllowed 
                                   WHERE  Role = @Role 
                                     AND  PermissionName = @PermName;";

                    foreach (var kvp in _permCheckboxes)
                    {
                        using (var cmd = new MySqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@IsAllowed",
                                kvp.Value.Checked ? 1 : 0);
                            cmd.Parameters.AddWithValue("@Role", role);
                            cmd.Parameters.AddWithValue("@PermName", kvp.Key);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                AuditLogger.Log("MANAGE_PERMISSIONS",
                    $"Permissions updated for role '{role}' by {AppSession.CurrentUsername}.");


                MessageBox.Show("Permissions saved successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving permissions:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}