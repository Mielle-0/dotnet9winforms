using Krypton.Toolkit;
using it13Project.UI;
namespace it13Project.Pages
{
    partial class UserManagement
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
        this.Dock = DockStyle.Fill;
        this.Padding = new Padding(15);
        this.BackColor = ThemeColors.ContentBackground;

        // === Main Layout ===
        tableLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            RowCount = 2,
            ColumnCount = 1
        };
        tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 55)); // Toolbar
        tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // Grid

        // === Toolbar ===
        toolbar = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            Height = 50,
            Padding = new Padding(10, 5, 0, 5),
            BackColor = ThemeColors.MenuBackground,
            ForeColor = ThemeColors.TextColor,
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = false
        };

        txtSearch = new KryptonTextBox
        {
            Width = 200,
            CueHint = { CueHintText = "Search users..." }
        };

        cmbFilterRole = new KryptonComboBox
        {
            Width = 150,
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        cmbFilterRole.Items.AddRange(new[] { "All Roles", "Admin", "Analyst", "Marketing" });
        cmbFilterRole.SelectedIndex = 0;

        cmbFilterStatus = new KryptonComboBox
        {
            Width = 150,
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        cmbFilterStatus.Items.AddRange(new[] { "All Statuses", "Active", "Inactive" });
        cmbFilterStatus.SelectedIndex = 0;

        btnRefresh = new KryptonButton
        {
            Text = "Refresh",
            Margin = new Padding(5, 5, 0, 0),
            Width = 90
        };
        btnRefresh.Click += btnRefresh_Click;

        btnAddUser = new KryptonButton
        {
            Text = "Add User",
            Margin = new Padding(5, 5, 0, 0),
            Width = 100
        };
        btnAddUser.Click += btnAddUser_Click;

        btnEditUser = new KryptonButton
        {
            Text = "Edit User",
            Margin = new Padding(5, 5, 0, 0),
            Width = 100
        };
        btnEditUser.Click += btnEditUser_Click;

        btnDeactivateUser = new KryptonButton
        {
            Text = "Deactivate",
            Margin = new Padding(5, 5, 0, 0),
            Width = 110
        };
        btnDeactivateUser.Click += btnDeactivateUser_Click;

        toolbar.Controls.AddRange(new Control[]
        {
            txtSearch,
            cmbFilterRole,
            cmbFilterStatus,
            btnRefresh,
            btnAddUser,
            btnEditUser,
            btnDeactivateUser
        });

        // === Users Grid ===
        dgvUsers = new KryptonDataGridView
        {
            Dock = DockStyle.Fill,
            ReadOnly = true,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            MultiSelect = false,
            BackgroundColor = ThemeColors.ContentBackground,
            BorderStyle = BorderStyle.None
        };

        // Columns (you can adjust depending on schema)
        dgvUsers.Columns.Add("user_id", "User ID");
        dgvUsers.Columns.Add("name", "Name");
        dgvUsers.Columns.Add("email", "Email");
        dgvUsers.Columns.Add("role", "Role");
        dgvUsers.Columns.Add("status", "Status");

        // === Add to Layout ===
        tableLayout.Controls.Add(toolbar, 0, 0);
        tableLayout.Controls.Add(dgvUsers, 0, 1);

        this.Controls.Add(tableLayout);
        this.ResumeLayout(false);
    }


    private TableLayoutPanel tableLayout;
    private FlowLayoutPanel toolbar;
    private KryptonTextBox txtSearch;
    private KryptonComboBox cmbFilterRole;
    private KryptonComboBox cmbFilterStatus;
    private KryptonButton btnAddUser, btnEditUser, btnDeactivateUser, btnRefresh;
    private KryptonDataGridView dgvUsers;

        #endregion
    }
}
