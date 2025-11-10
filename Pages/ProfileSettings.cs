using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using it13Project.Data;

namespace it13Project.Pages
{
    public partial class ProfileSettings : UserControl
    {
        public ProfileSettings()
        {
            InitializeComponent();
            LoadUserData();
        }

        private void LoadUserData()
        {
            if (CurrentUser.UserId == 0)
            {
                MessageBox.Show("No user is logged in.");
                return;
            }

            var row = ProfileSettingsService.GetUserById(CurrentUser.UserId);
            if (row != null)
            {
                txtUsername.Text = row["name"]?.ToString();
                txtEmail.Text = row["email"]?.ToString();
            }
        }

        private void btnSave_Click(object? sender, EventArgs e)
        {
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Passwords do not match!");
                return;
            }

            bool updated = ProfileSettingsService.UpdateUserProfile(
                CurrentUser.UserId,
                txtUsername.Text.Trim(),
                txtEmail.Text.Trim(),
                string.IsNullOrWhiteSpace(txtPassword.Text) ? null : txtPassword.Text
            );

            if (updated)
            {
                CurrentUser.Name = txtUsername.Text.Trim(); // update cache
                MessageBox.Show("Profile updated successfully!");
            }
            else
                MessageBox.Show("No changes were made.");
            
        }
    }
}
