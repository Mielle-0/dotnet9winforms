using it13Project.UI;
using Krypton.Toolkit;

namespace it13Project.Forms
{
    partial class GameEdit
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
            this.txtTitle = new Krypton.Toolkit.KryptonTextBox();
            this.txtGenre = new Krypton.Toolkit.KryptonTextBox();
            this.dtRelease = new Krypton.Toolkit.KryptonDateTimePicker();
            this.txtDeveloper = new Krypton.Toolkit.KryptonTextBox();
            this.txtPublisher = new Krypton.Toolkit.KryptonTextBox();
            this.txtDescription = new Krypton.Toolkit.KryptonTextBox();

            this.btnSave = new Krypton.Toolkit.KryptonButton();
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
            this.txtTitle = new Krypton.Toolkit.KryptonTextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                Text = "Game Title"
            };
            this.txtTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            layout.SetColumnSpan(this.txtTitle, 2);

            // Static labels
            var lblGenreLabel = CreateStaticLabel("Genre:");
            var lblReleaseLabel = CreateStaticLabel("Release:");
            var lblDeveloperLabel = CreateStaticLabel("Developer:");
            var lblPublisherLabel = CreateStaticLabel("Publisher:");

            // Value labels
            this.txtGenre = new Krypton.Toolkit.KryptonTextBox
            {
                Dock = DockStyle.Fill,
                Text = "Action"
            };

            this.dtRelease = new Krypton.Toolkit.KryptonDateTimePicker
            {
                Dock = DockStyle.Fill,
                Format = DateTimePickerFormat.Short,
                ShowCheckBox = true
            };

            this.txtDeveloper = new Krypton.Toolkit.KryptonTextBox
            {
                Dock = DockStyle.Fill,
                Text = "XYZ"
            };

            this.txtPublisher = new Krypton.Toolkit.KryptonTextBox
            {
                Dock = DockStyle.Fill,
                Text = "ABC"
            };


            // 
            // txtDescription
            // 
            this.txtDescription.Multiline = true;
            this.txtDescription.ScrollBars = ScrollBars.Vertical;
            this.txtDescription.Dock = DockStyle.Fill;
            this.txtDescription.StateCommon.Content.Font = new Font("Segoe UI", 10F);
            this.txtDescription.StateCommon.Back.Color1 = ThemeColors.ContentBackground;
            this.txtDescription.StateCommon.Border.DrawBorders =
                Krypton.Toolkit.PaletteDrawBorders.All;
            this.txtDescription.Text = "Description goes here...";

            // 
            // btnClose
            // 
            this.btnClose.Anchor = AnchorStyles.Right;
            this.btnClose.Size = new Size(100, 35);
            this.btnClose.StateCommon.Back.Color1 = Color.Red;
            this.btnClose.StateCommon.Content.ShortText.Color1 = ThemeColors.TextColor;
            this.btnClose.Text = "Close";
            this.btnClose.Click += (s, e) => this.Close();

            // 
            // btnSave
            // 
            this.btnSave.Anchor = AnchorStyles.Right;
            this.btnSave.Size = new Size(100, 35);
            this.btnSave.StateCommon.Back.Color1 = Color.Green;
            this.btnSave.StateCommon.Content.ShortText.Color1 = ThemeColors.TextColor;
            this.btnSave.Text = "Save";
            this.btnSave.Click += btnSave_Click;

            var buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft
            };
            buttonPanel.Controls.Add(this.btnClose);
            buttonPanel.Controls.Add(this.btnSave);

            // add controls to layout
            layout.Controls.Add(this.txtTitle, 0, 0);

            layout.Controls.Add(lblGenreLabel, 0, 1);
            layout.Controls.Add(this.txtGenre, 1, 1);

            layout.Controls.Add(lblReleaseLabel, 0, 2);
            layout.Controls.Add(this.dtRelease, 1, 2);

            layout.Controls.Add(lblDeveloperLabel, 0, 3);
            layout.Controls.Add(this.txtDeveloper, 1, 3);

            layout.Controls.Add(lblPublisherLabel, 0, 4);
            layout.Controls.Add(this.txtPublisher, 1, 4);

            layout.Controls.Add(this.txtDescription, 0, 5);
            layout.SetColumnSpan(this.txtDescription, 2);

            layout.Controls.Add(buttonPanel, 1, 6);

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
        private Krypton.Toolkit.KryptonTextBox txtTitle;
        private Krypton.Toolkit.KryptonTextBox txtGenre;
        private Krypton.Toolkit.KryptonDateTimePicker dtRelease;
        private Krypton.Toolkit.KryptonTextBox txtDeveloper;
        private Krypton.Toolkit.KryptonTextBox txtPublisher;

        private Krypton.Toolkit.KryptonTextBox txtDescription;
        private Krypton.Toolkit.KryptonButton btnClose, btnSave;
        private Krypton.Toolkit.KryptonGroup groupBox;


    }
}