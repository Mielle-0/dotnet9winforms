using System.Data;
using System.Security.Cryptography;
using System.Text;
using it13Project.Data;
using Microsoft.Data.SqlClient;
using it13Project.Models;

namespace it13Project.Forms;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        this.AcceptButton = btnLogin;
    }

    private void btnLogin_Click(object sender, EventArgs e)
    {
        string username = txtUsername.Text.Trim();
        string password = txtPassword.Text;

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            MessageBox.Show("Please enter both username and password.");
            return;
        }

        try
        {
            // Query to get stored hash
            string query = "SELECT user_id, name, role, password_hash FROM dbo.Users WHERE name = @Name";

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", username)
            };

            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters.ToArray());

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Invalid username or password.");
                return;
            }

            DataRow row = dt.Rows[0];
            string? storedHash = row["password_hash"].ToString();

            // Hash entered password
            string enteredHash = HashPassword(password);

            if (storedHash == enteredHash)
            {
                // ✅ Successful login
                string? userRole = row["role"].ToString();

                // MessageBox.Show($"Welcome, {username}! Role: {userRole}");

                // You could store user info in a static class/session
                CurrentUser.UserId = Convert.ToInt32(row["user_id"]);
                CurrentUser.Name = username;
                CurrentUser.Role = userRole;

                MainForm dashboard = new MainForm();
                this.Hide();                
                dashboard.FormClosed += (s, args) => this.Close(); 
                dashboard.Show();

                // this.DialogResult = DialogResult.OK;
                // this.Close();
            }
            else
            {
                MessageBox.Show("Invalid email or password.");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error: " + ex.Message);
        }
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
        txtUsername.Clear();
        txtPassword.Clear();
        txtUsername.Focus();
    }

    private string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}
