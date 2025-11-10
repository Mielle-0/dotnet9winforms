using it13Project.UI;

namespace it13Project.Forms
{
    partial class GameDetails
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
            this.lblTitle = new Krypton.Toolkit.KryptonLabel();
            this.lblGenre = new Krypton.Toolkit.KryptonLabel();
            this.lblRelease = new Krypton.Toolkit.KryptonLabel();
            this.lblDeveloper = new Krypton.Toolkit.KryptonLabel();
            this.lblPublisher = new Krypton.Toolkit.KryptonLabel();
            this.lblDescription = new Krypton.Toolkit.KryptonTextBox();
            this.btnClose = new Krypton.Toolkit.KryptonButton();

            this.mainPanel = new Krypton.Toolkit.KryptonPanel();
            this.groupBox = new Krypton.Toolkit.KryptonGroup();

            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).BeginInit();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox)).BeginInit();
            this.groupBox.Panel.SuspendLayout();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();

            // 
            // mainPanel
            // 
            this.mainPanel.Dock = DockStyle.Fill;
            this.mainPanel.StateCommon.Color1 = ThemeColors.MenuBackground;
            this.mainPanel.Controls.Add(this.groupBox);

            // 
            // groupBox
            // 
            this.groupBox.Dock = DockStyle.Fill;
            this.groupBox.StateCommon.Back.Color1 = ThemeColors.ContentBackground;
            this.groupBox.StateCommon.Border.Rounding = 10;

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 6,
                ColumnCount = 2,
                Padding = new Padding(20),
                AutoScroll = true
            };

            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120)); // Labels
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));  // Values

            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));  // Title full-width
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));  // Genre
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));  // Release
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));  // Developer
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));  // Publisher
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));  // Description
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));  // Close button

            // 
            // lblTitle
            // 
            this.lblTitle.Dock = DockStyle.Fill;
            this.lblTitle.StateCommon.ShortText.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblTitle.StateCommon.ShortText.Color1 = ThemeColors.MenuBackground;
            this.lblTitle.Text = "Game Title";
            layout.SetColumnSpan(this.lblTitle, 2);

            // Static labels
            var lblGenreLabel = CreateStaticLabel("Genre:");
            var lblReleaseLabel = CreateStaticLabel("Release:");
            var lblDeveloperLabel = CreateStaticLabel("Developer:");
            var lblPublisherLabel = CreateStaticLabel("Publisher:");

            // Value labels
            this.lblGenre.StateCommon.ShortText.Color1 = ThemeColors.MenuBackground;
            this.lblGenre.Text = "Action";

            this.lblRelease.StateCommon.ShortText.Color1 = ThemeColors.MenuBackground;
            this.lblRelease.Text = "2020";

            this.lblDeveloper.StateCommon.ShortText.Color1 = ThemeColors.MenuBackground;
            this.lblDeveloper.Text = "XYZ";

            this.lblPublisher.StateCommon.ShortText.Color1 = ThemeColors.MenuBackground;
            this.lblPublisher.Text = "ABC";

            // 
            // lblDescription
            // 
            this.lblDescription.Multiline = true;
            this.lblDescription.ReadOnly = true;
            this.lblDescription.ScrollBars = ScrollBars.Vertical;
            this.lblDescription.Dock = DockStyle.Fill;
            this.lblDescription.StateCommon.Content.Font = new Font("Segoe UI", 10F);
            this.lblDescription.StateCommon.Back.Color1 = ThemeColors.ContentBackground;
            this.lblDescription.StateCommon.Border.DrawBorders =
                Krypton.Toolkit.PaletteDrawBorders.All;
            this.lblDescription.Text = "Description goes here...";

            // 
            // btnClose
            // 
            this.btnClose.Anchor = AnchorStyles.Right;
            this.btnClose.Size = new Size(100, 35);
            this.btnClose.StateCommon.Back.Color1 = ThemeColors.AccentSecondary;
            this.btnClose.StateCommon.Content.ShortText.Color1 = ThemeColors.TextColor;
            this.btnClose.Text = "Close";
            this.btnClose.Click += (s, e) => this.Close();

            // add controls to layout
            layout.Controls.Add(this.lblTitle, 0, 0);

            layout.Controls.Add(lblGenreLabel, 0, 1);
            layout.Controls.Add(this.lblGenre, 1, 1);

            layout.Controls.Add(lblReleaseLabel, 0, 2);
            layout.Controls.Add(this.lblRelease, 1, 2);

            layout.Controls.Add(lblDeveloperLabel, 0, 3);
            layout.Controls.Add(this.lblDeveloper, 1, 3);

            layout.Controls.Add(lblPublisherLabel, 0, 4);
            layout.Controls.Add(this.lblPublisher, 1, 4);

            layout.Controls.Add(this.lblDescription, 0, 5);
            layout.SetColumnSpan(this.lblDescription, 2);

            layout.Controls.Add(this.btnClose, 1, 6);

            this.groupBox.Panel.Controls.Add(layout);


            // Remove after debugging
            // this.BackColor = Color.Green;
            // mainPanel.StateCommon.Color1 = Color.Blue;
            // groupBox.StateCommon.Back.Color1 = Color.Purple;
            // layout.BackColor = Color.Red;
            groupBox.Size = new Size(600, 450);
            layout.Size = new Size(600, 450);


            // 
            // GameDetailsForm
            // 
            this.ClientSize = new Size(600, 450);
            this.Controls.Add(this.mainPanel);
            this.mainPanel.Dock = DockStyle.Fill;
            this.Text = "Game Details";

            this.mainPanel.ResumeLayout(false);
            this.groupBox.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupBox)).EndInit();
            this.groupBox.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        // helper for static labels
        private Krypton.Toolkit.KryptonLabel CreateStaticLabel(string text)
        {
            return new Krypton.Toolkit.KryptonLabel
            {
                Dock = DockStyle.Fill,
                StateCommon = { ShortText = { Color1 = ThemeColors.MenuBackground } },
                Text = text
            };
        }



        #endregion



        private Krypton.Toolkit.KryptonPanel mainPanel;
        private Krypton.Toolkit.KryptonLabel lblTitle;
        private Krypton.Toolkit.KryptonLabel lblGenre;
        private Krypton.Toolkit.KryptonLabel lblRelease;
        private Krypton.Toolkit.KryptonLabel lblDeveloper;
        private Krypton.Toolkit.KryptonLabel lblPublisher;
        private Krypton.Toolkit.KryptonTextBox lblDescription;
        private Krypton.Toolkit.KryptonButton btnClose;
        private Krypton.Toolkit.KryptonGroup groupBox;
    }
}