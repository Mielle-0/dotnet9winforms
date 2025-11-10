using System.Drawing.Drawing2D;
using Krypton.Toolkit;
using it13Project.UI;

namespace it13Project.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>

        private void InitializeComponent()
        {
            SuspendLayout();

            // Form styling
            this.BackColor = ThemeColors.ContentBackground;
            this.Font = new Font("Segoe UI", 9F);

            // Krypton controls
            panelMenu = new KryptonPanel();
            panelLogo = new KryptonPanel();
            panelContent = new KryptonPanel();
            lblLogo = new KryptonLabel();

            btnDashboard = new KryptonButton();
            btnGameList = new KryptonButton();
            btnSentimentTrends = new KryptonButton();
            btnInfluentialReviewers = new KryptonButton();
            btnAlerts = new KryptonButton();
            btnReports = new KryptonButton();
            btnModelManagement = new KryptonButton();
            btnAdminSettings = new KryptonButton();
            btnUserManagement = new KryptonButton();
            btnReviewPage = new KryptonButton();
            btnSentimentPage = new KryptonButton();
            btnProfileSettings = new KryptonButton();

            // panelMenu
            panelMenu.StateCommon.Color1 = ThemeColors.MenuBackground;
            panelMenu.Dock = DockStyle.Left;
            panelMenu.Width = 230;
            panelMenu.Padding = new Padding(10, 10, 10, 10);
            panelMenu.AutoScroll = true;
            // panelMenu.AutoScrollMinSize = 1;
            panelMenu.VerticalScroll.Visible = false;

            // Add Buttons to Sidebar
            panelMenu.Controls.AddRange(new Control[]
            {
                btnProfileSettings, btnUserManagement, btnAdminSettings,
                btnReviewPage, btnSentimentPage,
                btnModelManagement, btnReports,
                btnAlerts, btnInfluentialReviewers,
                btnSentimentTrends, btnGameList,
                btnDashboard, panelLogo
            });

            // Button styling helper
            void StyleKryptonButton(KryptonButton button, string text, EventHandler clickHandler)
            {
                button.ButtonStyle = ButtonStyle.NavigatorStack;
                button.Values.Text = text;
                button.Dock = DockStyle.Top;
                button.Height = 55;

                // Text
                button.StateCommon.Content.ShortText.Font = new Font("Segoe UI Semibold", 11F);
                button.StateCommon.Content.ShortText.Color1 = ThemeColors.TextColor;
                button.StateCommon.Content.Padding = new Padding(15, 0, 0, 0);

                // Background
                button.StateCommon.Back.Color1 = ThemeColors.MenuBackground;
                button.StateCommon.Back.Color2 = ThemeColors.MenuBackground;

                // Hover state
                button.StateTracking.Back.Color1 = ThemeColors.MenuHover;
                button.StateTracking.Back.Color2 = ThemeColors.MenuHover;

                // Pressed / active
                button.StatePressed.Back.Color1 = ThemeColors.AccentPrimary;
                button.StatePressed.Back.Color2 = ThemeColors.AccentPrimary;
                button.StatePressed.Content.ShortText.Color1 = ThemeColors.TextColor;

                // Rounded edges
                button.StateCommon.Border.Rounding = 6;
                button.StateCommon.Border.DrawBorders = PaletteDrawBorders.All;

                if (clickHandler != null)
                    button.Click += clickHandler;
            }

            // Buttons
            StyleKryptonButton(btnDashboard, "📊 Dashboard", btnDashboard_Click);
            StyleKryptonButton(btnGameList, "🎮 Game List", btnGameList_Click);
            StyleKryptonButton(btnSentimentTrends, "📉 Trends", btnSentimentTrends_Click);
            StyleKryptonButton(btnInfluentialReviewers, "🌟 Influential Reviewers", btnInfluentialReviewers_Click);
            StyleKryptonButton(btnAlerts, "🔔 Alerts", btnAlerts_Click);
            StyleKryptonButton(btnReports, "📈 Reports", btnReports_Click);
            StyleKryptonButton(btnModelManagement, "📊 Model Management", btnModelManagement_Click);
            StyleKryptonButton(btnAdminSettings, "⚙️ Admin Settings", btnAdminSettings_Click);
            StyleKryptonButton(btnUserManagement, "👥 User Management", btnUserManagement_Click);
            StyleKryptonButton(btnReviewPage, "📋 Reviews", btnReviewPage_Click);
            StyleKryptonButton(btnSentimentPage, "💞 Sentiments", btnSentimentPage_Click);
            StyleKryptonButton(btnProfileSettings, "👤 Profile", btnProfileSettings_Click);

            // Highlight dashboard (active state)
            btnDashboard.StateCommon.Back.Color1 = ThemeColors.AccentPrimary;
            btnDashboard.StateCommon.Back.Color2 = ThemeColors.AccentPrimary;
            btnDashboard.StateCommon.Content.ShortText.Color1 = ThemeColors.TextColor;

            // panelLogo
            panelLogo.StateCommon.Color1 = ThemeColors.MenuBackground;
            panelLogo.StateCommon.Color2 = ThemeColors.MenuBackground;
            panelLogo.StateCommon.ColorStyle = PaletteColorStyle.Linear;
            panelLogo.Dock = DockStyle.Top;
            panelLogo.Height = 120;

            // Add a PictureBox for logo image placeholder
            var picLogo = new PictureBox
            {
                Dock = DockStyle.Top,
                Height = 60,
                Width = 60,
                SizeMode = PictureBoxSizeMode.Zoom,
                Margin = new Padding(0, 10, 0, 5),
                Image = Properties.Resources.LogoPlaceholder,
                BackColor = ThemeColors.MenuBackground
            };

            // lblLogo under image
            lblLogo.Dock = DockStyle.Top;
            lblLogo.Height = 40;
            lblLogo.Values.Text = "Game Review CRM";
            lblLogo.StateCommon.ShortText.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblLogo.StateCommon.ShortText.Color1 = ThemeColors.TextColor;
            lblLogo.StateCommon.ShortText.TextH = PaletteRelativeAlign.Center;
            lblLogo.StateCommon.ShortText.TextV = PaletteRelativeAlign.Center;

            // Add controls into logo panel
            panelLogo.Controls.Add(lblLogo);
            panelLogo.Controls.Add(picLogo);


            // panelContent
            panelContent.StateCommon.Color1 = ThemeColors.ContentBackground;
            panelContent.Dock = DockStyle.Fill;
            panelContent.Padding = new Padding(20);

            // MainForm
            this.ClientSize = new Size(1280, 760);
            this.Controls.Add(panelContent);
            this.Controls.Add(panelMenu);
            this.MinimumSize = new Size(1280, 760);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Game Review Sentiment CRM";

            ResumeLayout(false);
        }

        #endregion

        private KryptonPanel panelMenu;
        private KryptonButton btnDashboard;
        private KryptonPanel panelLogo;
        private KryptonButton btnAdminSettings;
        private KryptonButton btnModelManagement;
        private KryptonButton btnReports;
        private KryptonButton btnAlerts;
        private KryptonButton btnInfluentialReviewers;
        private KryptonButton btnSentimentTrends;
        private KryptonButton btnGameList;
        private KryptonButton btnUserManagement;
        private KryptonButton btnReviewPage;
        private KryptonButton btnSentimentPage;
        private KryptonButton btnProfileSettings;
        private KryptonLabel lblLogo;
        private KryptonPanel panelContent;

    }

}