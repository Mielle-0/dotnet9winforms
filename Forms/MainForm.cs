using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Krypton.Toolkit;
using it13Project.Pages;
using it13Project.UI;
using it13Project.Data;

namespace it13Project.Forms
{
    public partial class MainForm : Form
    {
        private KryptonButton? currentButton;

        public MainForm()
        {
            InitializeComponent();
            // Load the default page
            ActivateButton(btnDashboard);
            LoadPage(new DashboardPage(), "Dashboard");

            ApplyRolePermissions();
            SidebarControl();
        }


        // Button is active
        private void ActivateButton(object btnSender)
        {
            if (btnSender == null) return;

            if (currentButton != (KryptonButton)btnSender)
            {
                DisableButton();
                currentButton = (KryptonButton)btnSender;
                currentButton.StateCommon.Back.Color1 = ThemeColors.AccentPrimary;
                currentButton.StateCommon.Back.Color2 = ThemeColors.AccentPrimary;
                currentButton.StateCommon.Content.ShortText.Color1 = ThemeColors.TextColor;
                currentButton.StateCommon.Content.ShortText.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);

            }
        }

        // Button not active
        private void DisableButton()
        {
            foreach (Control previousBtn in panelMenu.Controls)
            {
                if (previousBtn is KryptonButton btn)
                {
                    btn.StateCommon.Back.Color1 = ThemeColors.MenuBackground;
                    btn.StateCommon.Content.ShortText.Color1 = ThemeColors.TextColor;
                    btn.StateCommon.Content.ShortText.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Regular);
                }
            }
        }

        // Load user control
        private void LoadPage(UserControl page, string pageKey)
        {
            if (!RolePermissions.HasAccess(CurrentUser.Role, pageKey))
            {
                MessageBox.Show("You do not have access to this page.");
                return;
            }

            panelContent.Controls.Clear();
            page.Dock = DockStyle.Fill;
            panelContent.Controls.Add(page);
            page.BringToFront();
        }


        // Simple Access Control
        private void SidebarControl()
        {

            btnInfluentialReviewers.Visible = false;
            btnModelManagement.Visible = false;
        }

        private void ApplyRolePermissions()
        {
            var role = CurrentUser.Role;

            btnDashboard.Visible = RolePermissions.HasAccess(role, "Dashboard");
            btnGameList.Visible = RolePermissions.HasAccess(role, "GameList");
            btnSentimentTrends.Visible = RolePermissions.HasAccess(role, "SentimentTrends");
            btnInfluentialReviewers.Visible = RolePermissions.HasAccess(role, "InfluentialReviewers");
            btnAlerts.Visible = RolePermissions.HasAccess(role, "Alerts");
            btnReports.Visible = RolePermissions.HasAccess(role, "Reports");
            btnModelManagement.Visible = RolePermissions.HasAccess(role, "ModelManagement");
            btnAdminSettings.Visible = RolePermissions.HasAccess(role, "AdminSettings");
            btnUserManagement.Visible = RolePermissions.HasAccess(role, "UserManagement");
            btnReviewPage.Visible = RolePermissions.HasAccess(role, "ReviewPage");
            btnSentimentPage.Visible = RolePermissions.HasAccess(role, "SentimentPage");
            btnProfileSettings.Visible = RolePermissions.HasAccess(role, "ProfileSettings");
        }



        // --- Navigation Button Click Events ---
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            LoadPage(new DashboardPage(), "Dashboard");
        }

        private void btnGameList_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            LoadPage(new GameListPage(), "GameList");
        }

        private void btnSentimentTrends_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            LoadPage(new SentimentTrendsPage(), "SentimentTrends");
        }

        private void btnInfluentialReviewers_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            LoadPage(new InfluentialReviewersPage(), "InfluentialReviewers");
        }

        private void btnAlerts_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            LoadPage(new AlertsNotificationsPage(), "Alerts");
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            LoadPage(new ReportsPage(), "Reports");
        }

        private void btnModelManagement_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            LoadPage(new ModelManagementPage(), "ModelManagement");
        }

        private void btnAdminSettings_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            LoadPage(new AdminSettingsPage(), "AdminSettings");
        }

        private void btnUserManagement_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            LoadPage(new UserManagement(), "UserManagement");
        }

        private void btnReviewPage_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            LoadPage(new ReviewPage(), "ReviewPage");
        }

        private void btnSentimentPage_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            LoadPage(new SentimentPage(), "SentimentPage");
        }

        private void btnProfileSettings_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            LoadPage(new ProfileSettings(), "ProfileSettings");
        }

        
        
    }
}
