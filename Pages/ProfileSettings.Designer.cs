using Krypton.Toolkit;
using it13Project.UI;
namespace it13Project.Pages
{
    partial class ProfileSettings
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Name = "ProfileSettingsPage";
            this.Size = new Size(450, 350); 
            this.Padding = new Padding(20);
            this.BackColor = ThemeColors.ContentBackground;

            // Table layout container
            tableLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 1
            };
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // Main content
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 55)); // Action buttons

            // Content Panel
            contentPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(15),
                BackColor = ThemeColors.MenuBackground,
                ForeColor = ThemeColors.TextColor,
                FlowDirection = FlowDirection.TopDown,
                AutoScroll = true,
                WrapContents = false
            };

            // Username
            contentPanel.Controls.Add(new KryptonLabel
            {
                Text = "Username:",
                StateCommon = { ShortText = { Color1 = ThemeColors.TextColor } },
                Margin = new Padding(0, 10, 0, 0)
            });

            txtUsername = new KryptonTextBox { Width = 220 };
            contentPanel.Controls.Add(txtUsername);

            // Email
            contentPanel.Controls.Add(new KryptonLabel
            {
                Text = "Email:",
                StateCommon = { ShortText = { Color1 = ThemeColors.TextColor } },
                Margin = new Padding(0, 10, 0, 0)
            });

            txtEmail = new KryptonTextBox { Width = 220 };
            contentPanel.Controls.Add(txtEmail);

            // Password
            contentPanel.Controls.Add(new KryptonLabel
            {
                Text = "Password:",
                StateCommon = { ShortText = { Color1 = ThemeColors.TextColor } },
                Margin = new Padding(0, 10, 0, 0)
            });

            txtPassword = new KryptonTextBox
            {
                Width = 220,
                PasswordChar = '•'
            };
            contentPanel.Controls.Add(txtPassword);

            // Confirm Password
            contentPanel.Controls.Add(new KryptonLabel
            {
                Text = "Confirm Password:",
                StateCommon = { ShortText = { Color1 = ThemeColors.TextColor } },
                Margin = new Padding(0, 10, 0, 0)
            });

            txtConfirmPassword = new KryptonTextBox
            {
                Width = 220,
                PasswordChar = '•'
            };
            contentPanel.Controls.Add(txtConfirmPassword);

            // Action Panel (Save/Cancel)
            actionPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                Height = 50,
                Padding = new Padding(10, 5, 0, 5),
                BackColor = ThemeColors.MenuBackground,
                ForeColor = ThemeColors.TextColor,
                FlowDirection = FlowDirection.RightToLeft
            };

            btnSave = new KryptonButton
            {
                Text = "Save",
                Width = 100,
                Margin = new Padding(5)
            };
            btnCancel = new KryptonButton
            {
                Text = "Cancel",
                Width = 100,
                Margin = new Padding(5)
            };

            actionPanel.Controls.AddRange(new Control[] { btnSave, btnCancel });

            // Add layout
            tableLayout.Controls.Add(contentPanel, 0, 0);
            tableLayout.Controls.Add(actionPanel, 0, 1);

            // btnSave.Click += btnSave_Click;
            // btnCancel.Click += btnCancel_Click;

            this.Controls.Add(tableLayout);
            this.ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayout;
        private FlowLayoutPanel actionPanel;
        private FlowLayoutPanel contentPanel;
        private KryptonButton btnSave;
        private KryptonButton btnCancel;

        private KryptonTextBox txtUsername;
        private KryptonTextBox txtEmail;
        private KryptonTextBox txtPassword;
        private KryptonComboBox cboTheme;
        private KryptonCheckBox chkNotifications;
        private KryptonTextBox txtConfirmPassword;
    }
}
