using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using it13Project.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;

namespace it13Project.Forms
{
    public partial class AddUser : Form
    {
        private int? editingUserId = null; // null = Add user, not null = Edit usr

        // Add
        public AddUser()
        {
            InitializeComponent();

            // Populate roles from ENUM
            cmbRole.Items.AddRange(new string[]
            {
                "System Administrator",
                "Data Analyst",
                "Marketing Manager",
                "Game Developer",
                "Customer Support",
                "Stakeholder"
            });

            cmbRole.SelectedIndex = 0;
        }

        // Overload Constructor - Edit
        public AddUser(int userId, string name, string email, string role)
        {
            InitializeComponent();
            cmbRole.Items.AddRange(new string[]
            {
                "System Administrator",
                "Data Analyst",
                "Marketing Manager",
                "Game Developer",
                "Customer Support",
                "Stakeholder"
            });

            // User info
            editingUserId = userId;
            txtName.Text = name;
            txtEmail.Text = email;
            cmbRole.SelectedItem = role;

            if (cmbRole.Items.Contains(role))
                cmbRole.SelectedItem = role;
            else
                cmbRole.SelectedIndex = 0;

            // Password left empty unless user wants to change it
            //txtPassword.PlaceholderText = "Leave blank to keep current password";
            txtPassword.Enabled = false;

            this.Text = "Edit User";   // Change title
            btnSave.Text = "Update";   // Change button label
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Name and Email are required.");
                return;
            }
            
            if (cmbRole.SelectedItem == null)
            {
                MessageBox.Show("Please select a role.");
                return;
            }

            try
            {
                string query;

                if (editingUserId == null)
                {
                    // === INSERT NEW USER ===
                    query = "INSERT INTO dbo.Users (name, email, role, password_hash, active) " +
                            "VALUES (@name, @email, @role, @password_hash, 1)";
                }
                else
                {
                    // === UPDATE EXISTING USER ===
                    query = "UPDATE dbo.Users SET name=@name, email=@email, role=@role";

                    if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                        query += ", password_hash=@password_hash";

                    query += " WHERE user_id=@id";
                }
                var parameters = new List<SqlParameter>
        {
            new SqlParameter("@name", txtName.Text),
            new SqlParameter("@email", txtEmail.Text),
            new SqlParameter("@role", cmbRole.SelectedItem.ToString())
        };

                if (editingUserId == null || !string.IsNullOrWhiteSpace(txtPassword.Text))
                    parameters.Add(new SqlParameter("@password_hash", HashPassword(txtPassword.Text)));

                if (editingUserId != null)
                    parameters.Add(new SqlParameter("@id", editingUserId));

                int rows = DatabaseHelper.ExecuteNonQuery(query, parameters.ToArray());

                if (rows > 0)
                {
                    MessageBox.Show(editingUserId == null ? "User added successfully!" : "User updated successfully!");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No changes were made.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
