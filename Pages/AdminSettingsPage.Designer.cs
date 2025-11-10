using it13Project.UI;
using Krypton.Toolkit;
using Krypton.Navigator;
using ScottPlot;

namespace it13Project.Pages
{
    partial class AdminSettingsPage
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
            this.Name = "AdminSettingsPage";
            this.Size = new Size(900, 700);
            this.Padding = new Padding(20);
            this.BackColor = ThemeColors.ContentBackground;

            // Main scrollable container
            var mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = ThemeColors.ContentBackground
            };
            this.Controls.Add(mainPanel);

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                ColumnCount = 1,
                RowCount = 5,
                Padding = new Padding(10),
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            mainPanel.Controls.Add(layout);

            // === User Roles & Templates ===
            var grpRoles = CreateHeaderGroup("User Roles & Permissions", 250);

            var dgvRoles = new KryptonDataGridView
            {
                Dock = DockStyle.Fill,
                Height = 300,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            dgvRoles.Columns.Add("Username", "Username");
            dgvRoles.Columns.Add("Role", "Role");
            dgvRoles.Columns.Add("Permissions", "Permissions");
            grpRoles.Panel.Controls.Add(dgvRoles);

            var roleButtons = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 40 };
            roleButtons.Controls.Add(new KryptonButton { Text = "Add Role" });
            roleButtons.Controls.Add(new KryptonButton { Text = "Edit Role" });
            roleButtons.Controls.Add(new KryptonButton { Text = "Delete Role" });
            roleButtons.Controls.Add(new KryptonButton { Text = "Role Templates" });
            grpRoles.Panel.Controls.Add(roleButtons);

            // === API / Data Source Settings ===
            var grpApi = CreateHeaderGroup("API / Data Source", 150);

            var apiLayout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, AutoSize = true };
            apiLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150));
            apiLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            apiLayout.Controls.Add(new KryptonLabel { Text = "API URL:", StateCommon = { ShortText = { Color1 = System.Drawing.Color.Black } } }, 0, 0);
            var txtApiUrl = new KryptonTextBox { Dock = DockStyle.Fill };
            apiLayout.Controls.Add(txtApiUrl, 1, 0);

            apiLayout.Controls.Add(new KryptonLabel { Text = "API Key:", StateCommon = { ShortText = { Color1 = System.Drawing.Color.Black } } }, 0, 1);
            var txtApiKey = new KryptonTextBox { Dock = DockStyle.Fill, PasswordChar = '*' };
            apiLayout.Controls.Add(txtApiKey, 1, 1);

            apiLayout.Controls.Add(new KryptonLabel { Text = "Refresh Interval (mins):", StateCommon = { ShortText = { Color1 = System.Drawing.Color.Black } } }, 0, 2);
            var numInterval = new KryptonNumericUpDown { Minimum = 1, Maximum = 1440, Value = 60 };
            apiLayout.Controls.Add(numInterval, 1, 2);

            var btnTestApi = new KryptonButton { Text = "Test Connection", Dock = DockStyle.Right };
            apiLayout.Controls.Add(btnTestApi, 1, 3);

            grpApi.Panel.Controls.Add(apiLayout);

            // === Sentiment Thresholds ===
            var grpThresholds = CreateHeaderGroup("Sentiment Thresholds", 150);

            var threshLayout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, AutoSize = true };
            threshLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200));
            threshLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            threshLayout.Controls.Add(new KryptonLabel { Text = "Positive Threshold ≥", StateCommon = { ShortText = { Color1 = System.Drawing.Color.Black } } }, 0, 0);
            var numPos = new KryptonNumericUpDown { Minimum = 0, Maximum = 1, DecimalPlaces = 2, Increment = 0.05M, Value = 0.6M };
            threshLayout.Controls.Add(numPos, 1, 0);

            threshLayout.Controls.Add(new KryptonLabel { Text = "Negative Threshold ≤", StateCommon = { ShortText = { Color1 = System.Drawing.Color.Black } } }, 0, 1);
            var numNeg = new KryptonNumericUpDown { Minimum = -1, Maximum = 0, DecimalPlaces = 2, Increment = 0.05M, Value = -0.3M };
            threshLayout.Controls.Add(numNeg, 1, 1);

            var btnReset = new KryptonButton { Text = "Reset Defaults" };
            threshLayout.Controls.Add(btnReset, 1, 2);

            grpThresholds.Panel.Controls.Add(threshLayout);

            // === Model Management ===
            var grpModelMgmt = CreateHeaderGroup("Model Management", 200);

            var modelLayout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, AutoSize = true };
            modelLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 180));
            modelLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            // Model List
            modelLayout.Controls.Add(new KryptonLabel { Text = "Available Models:", StateCommon = { ShortText = { Color1 = System.Drawing.Color.Black } } }, 0, 0);
            var lstModels = new KryptonListBox { Dock = DockStyle.Fill, Height = 100 };
            modelLayout.Controls.Add(lstModels, 1, 0);

            // Upload Button
            var btnUploadModel = new KryptonButton { Text = "Upload Model" };
            modelLayout.Controls.Add(btnUploadModel, 1, 1);

            // Activate Button
            var btnActivateModel = new KryptonButton { Text = "Activate Selected" };
            modelLayout.Controls.Add(btnActivateModel, 1, 2);

            // Model Info
            modelLayout.Controls.Add(new KryptonLabel { Text = "Active Model:", StateCommon = { ShortText = { Color1 = System.Drawing.Color.Black } } }, 0, 3);
            var lblActiveModel = new KryptonLabel { Text = "(none)", StateCommon = { ShortText = { Color1 = System.Drawing.Color.DarkGreen } } };
            modelLayout.Controls.Add(lblActiveModel, 1, 3);

            grpModelMgmt.Panel.Controls.Add(modelLayout);

            // Add Model Management group to layout (add before the action bar)
            layout.Controls.Add(grpModelMgmt);

            // === Audit Log ===
            var grpAudit = CreateHeaderGroup("Audit Log", 250);

            dgvAuditLog = new KryptonDataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            dgvAuditLog.Columns.Add("Date", "Date");
            dgvAuditLog.Columns.Add("User", "User");
            dgvAuditLog.Columns.Add("Action", "Action");
            grpAudit.Panel.Controls.Add(dgvAuditLog);

            layout.Controls.Add(grpApi);
            layout.Controls.Add(grpThresholds);
            layout.Controls.Add(grpRoles);
            layout.Controls.Add(grpAudit);

            // === Action Bar ===
            var actionBar = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 50,
                FlowDirection = FlowDirection.RightToLeft,
                Padding = new Padding(10),
                BackColor = ThemeColors.MenuBackground
            };

            btnSave = new KryptonButton { Text = "Save Changes", StateCommon = { Content = { ShortText = { Color1 = System.Drawing.Color.Black } } } };
            btnCancel = new KryptonButton { Text = "Cancel", StateCommon = { Content = { ShortText = { Color1 = System.Drawing.Color.Black } } } };
            actionBar.Controls.Add(btnSave);
            actionBar.Controls.Add(btnCancel);

            this.Controls.Add(actionBar);

            this.ResumeLayout(false);
        }
        
        private KryptonHeaderGroup CreateHeaderGroup(string title, int height = 250)
        {
            var headerGroup = new KryptonHeaderGroup
            {
                Dock = DockStyle.Top,
                Height = height,
                ValuesPrimary = { Heading = title },
                HeaderVisibleSecondary = false
            };
            // remove the default image (green KR box)
            headerGroup.ValuesPrimary.Image = null;

            // Style background + header
            headerGroup.StateCommon.Back.Color1 = ThemeColors.ContentBackground;
            headerGroup.StateCommon.Border.DrawBorders = PaletteDrawBorders.All;

            headerGroup.StateCommon.HeaderPrimary.Back.Color1 = ThemeColors.MenuBackground;
            headerGroup.StateCommon.HeaderPrimary.Back.Color2 = ThemeColors.MenuBackground;
            headerGroup.StateCommon.HeaderPrimary.Content.ShortText.Color1 = ThemeColors.TextColor;
            headerGroup.StateCommon.HeaderPrimary.Content.ShortText.Font = new Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);

            return headerGroup;
        }


        private void StyleGroupBoxWithHeader(KryptonGroupBox box, string headerText)
        {
            // Ensure groupbox has no default Text (we'll use our header control)
            box.Text = string.Empty;

            // Style the group box surface
            box.StateCommon.Back.Color1 = ThemeColors.MenuBackground;
            // box.StateCommon.Border.DrawBorders = PaletteDrawBorders.All;
            box.StateCommon.Content.ShortText.Color1 = ThemeColors.TextColor;

            // Create a KryptonHeader to act as the colored title bar
            var header = new KryptonHeader
            {
                Dock = DockStyle.Top,
                Values = { Heading = headerText }, // visible heading text
                // ensure header uses the same background & text colors
                StateCommon = {
                    Back = { Color1 = ThemeColors.MenuBackground, Color2 = ThemeColors.MenuBackground },
                    Content = { ShortText = { Color1 = ThemeColors.TextColor, Font = new Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold) } }
                }
            };

            // Add header to the GroupBox.Panel and force it to be docked before other controls
            box.Panel.Controls.Add(header);
            box.Panel.Controls.SetChildIndex(header, 0); // ensure header docks first (top)

            // Add top padding so content doesn't overlap header
            var current = box.Panel.Padding;
            box.Panel.Padding = new Padding(current.Left, 8, current.Right, current.Bottom);
        }


        #endregion


        private KryptonButton btnSave, btnCancel;

        // Controls for Roles
        private KryptonDataGridView dgvRoles;
        private KryptonButton btnAddRole, btnEditRole, btnDeleteRole;

        // Controls for API
        private KryptonTextBox txtApiUrl, txtApiKey;
        private KryptonNumericUpDown numRefreshInterval;
        private KryptonButton btnTestConnection;

        // Controls for Thresholds
        private KryptonNumericUpDown numPositiveThreshold, numNegativeThreshold;
        private KryptonButton btnResetThresholds;

        // Controls for Role Templates
        private KryptonListBox lstTemplates;
        private KryptonButton btnApplyTemplate;

        // Controls for Audit Log
        private KryptonDataGridView dgvAuditLog;
    }
}
