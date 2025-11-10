using Krypton.Toolkit;
using Krypton.Navigator;

namespace it13Project.Forms
{
    partial class GameForm
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
            this.Size = new Size(600, 500);
            this.StartPosition = FormStartPosition.CenterParent;

            nav = new KryptonNavigator
            {
                Dock = DockStyle.Fill,
                Bar = { BarOrientation = VisualOrientation.Top }
            };

            pageDetails = new KryptonPage
            {
                Text = "Details",
                TextTitle = "Game Information",
                TextDescription = "Basic game info",
                AutoHiddenSlideSize = new Size(200, 200)
            };

            pageDescription = new KryptonPage
            {
                Text = "Description",
                TextTitle = "Game Description",
                TextDescription = "Detailed game description",
                AutoHiddenSlideSize = new Size(200, 200)
            };

            // --- Layout for Details Page ---
            detailsLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 8,
                Padding = new Padding(15),
                AutoSize = true
            };
            detailsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
            detailsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            // Controls
            txtAppId = new KryptonTextBox { Dock = DockStyle.Fill, MaxLength = 50 };
            txtAppName = new KryptonTextBox { Dock = DockStyle.Fill, MaxLength = 255 };
            txtGenre = new KryptonTextBox { Dock = DockStyle.Fill, MaxLength = 100 };
            dtRelease = new KryptonDateTimePicker { Dock = DockStyle.Fill, Format = DateTimePickerFormat.Short, ShowCheckBox = true };
            txtDeveloper = new KryptonTextBox { Dock = DockStyle.Fill, MaxLength = 255 };
            txtPublisher = new KryptonTextBox { Dock = DockStyle.Fill, MaxLength = 255 };
            txtSource = new KryptonTextBox { Dock = DockStyle.Fill, MaxLength = 100 };

            // Add to layout
            detailsLayout.Controls.Add(new KryptonLabel { Text = "App ID:", Dock = DockStyle.Fill }, 0, 0);
            detailsLayout.Controls.Add(txtAppId, 1, 0);
            detailsLayout.Controls.Add(new KryptonLabel { Text = "Name:", Dock = DockStyle.Fill }, 0, 1);
            detailsLayout.Controls.Add(txtAppName, 1, 1);
            detailsLayout.Controls.Add(new KryptonLabel { Text = "Genre:", Dock = DockStyle.Fill }, 0, 2);
            detailsLayout.Controls.Add(txtGenre, 1, 2);
            detailsLayout.Controls.Add(new KryptonLabel { Text = "Release Date:", Dock = DockStyle.Fill }, 0, 3);
            detailsLayout.Controls.Add(dtRelease, 1, 3);
            detailsLayout.Controls.Add(new KryptonLabel { Text = "Developer:", Dock = DockStyle.Fill }, 0, 4);
            detailsLayout.Controls.Add(txtDeveloper, 1, 4);
            detailsLayout.Controls.Add(new KryptonLabel { Text = "Publisher:", Dock = DockStyle.Fill }, 0, 5);
            detailsLayout.Controls.Add(txtPublisher, 1, 5);
            detailsLayout.Controls.Add(new KryptonLabel { Text = "Source:", Dock = DockStyle.Fill }, 0, 6);
            detailsLayout.Controls.Add(txtSource, 1, 6);

            pageDetails.Controls.Add(detailsLayout);

            // --- Layout for Description Page ---
            descLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(15),
                ColumnCount = 1,
                RowCount = 2
            };
            descLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            descLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));

            txtDescription = new KryptonTextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };

            btnSave = new KryptonButton
            {
                Text = _isEditMode ? "Update" : "Create",
                Dock = DockStyle.Right,
                Width = 120
            };

            this.btnSave.Click += btnSave_Click;


            descLayout.Controls.Add(txtDescription, 0, 0);
            descLayout.Controls.Add(btnSave, 0, 1);

            pageDescription.Controls.Add(descLayout);

            // Add pages to navigator
            nav.Pages.AddRange(new KryptonPage[] { pageDetails, pageDescription });

            this.Controls.Add(nav);
        }

        #endregion

        // Designer controls
        private KryptonTextBox txtAppId;
        private KryptonTextBox txtAppName;
        private KryptonTextBox txtGenre;
        private KryptonDateTimePicker dtRelease;
        private KryptonTextBox txtDeveloper;
        private KryptonTextBox txtPublisher;
        private KryptonTextBox txtSource;
        private KryptonTextBox txtDescription;
        private KryptonButton btnSave;
        private KryptonNavigator nav;
        private KryptonPage pageDetails, pageDescription;
        private TableLayoutPanel detailsLayout, descLayout;

    }
}