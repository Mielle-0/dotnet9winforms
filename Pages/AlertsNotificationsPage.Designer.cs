using System;
using Krypton.Toolkit;
using it13Project.UI;
using ScottPlot;
using ScottPlot.WinForms;

namespace it13Project.Pages
{
    partial class AlertsNotificationsPage
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

        #region

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        
        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Name = "AlertsPage";
            this.Size = new Size(900, 600);
            this.Padding = new Padding(20);
            this.BackColor = ThemeColors.ContentBackground;

            // Table layout container
            tableLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1
            };

            // Action Panel
            actionPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                Height = 40,
                Padding = new Padding(10, 5, 0, 5),
                BackColor = ThemeColors.MenuBackground,
                ForeColor = ThemeColors.TextColor,
                FlowDirection = FlowDirection.LeftToRight
            };

            btnRefresh = new KryptonButton { Text = "Refresh", Margin = new Padding(5, 5, 0, 0) };
            actionPanel.Controls.Add(btnRefresh);

            // Filter Panel
            filterPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 60,
                Padding = new Padding(5),
                BackColor = ThemeColors.MenuBackground,
                ForeColor = ThemeColors.TextColor,
                AutoSize = true,
                WrapContents = false
            };

            // Alert Type filter
            filterPanel.Controls.Add(new KryptonLabel
            {
                Text = "Alert Type:",
                StateCommon = { ShortText = { Color1 = ThemeColors.TextColor } },
                Margin = new Padding(5, 10, 0, 0)
            });

            cboAlertType = new KryptonComboBox
            {
                Width = 150,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cboAlertType.Items.AddRange(new object[] { "All", "Sentiment Alert", "Review Alert" });
            cboAlertType.SelectedIndex = 0;
            filterPanel.Controls.Add(cboAlertType);

            btnApplyFilters = new KryptonButton
            {
                Text = "Apply Filters",
                OverrideDefault = { Back = { Color1 = ThemeColors.AccentPrimary } },
                StateCommon = { Content = { ShortText = { Color1 = System.Drawing.Color.Black } } },
                Margin = new Padding(15, 5, 0, 0),
                Width = 120
            };
            filterPanel.Controls.Add(btnApplyFilters);

            // Alerts DataGridView
            dgvAlerts = new KryptonDataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = ThemeColors.ContentBackground,
                BorderStyle = BorderStyle.None,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = true,
                AutoGenerateColumns = false
            };

            dgvAlerts.Columns.Clear();
            dgvAlerts.Columns.Add("AlertId", "ID");
            dgvAlerts.Columns["AlertId"].Visible = false;
            dgvAlerts.Columns.Add("GameName", "Game Name");
            dgvAlerts.Columns.Add("AlertType", "Alert Type");
            dgvAlerts.Columns.Add("Severity", "Severity");
            dgvAlerts.Columns.Add("Message", "Message");
            dgvAlerts.Columns.Add("DateTime", "Date/Time");

            // Add some hard-coded sample alerts
            dgvAlerts.Rows.Add(1, "CyberQuest", "Sentiment Alert", "Critical", 
                "Negative reviews spiked by 30% in last 24h", DateTime.Now.ToString());
            dgvAlerts.Rows.Add(2, "Dragon Slayer", "Review Alert", "Warning", 
                "Review flagged: 'game crashes on start'", DateTime.Now.ToString());
            dgvAlerts.Rows.Add(3, "PuzzleWorld", "Sentiment Alert", "Info", 
                "Positive sentiment increased by 15% this week", DateTime.Now.ToString());

            // Pagination Panel
            paginationPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                Height = 40,
                Padding = new Padding(10, 5, 0, 5),
                BackColor = ThemeColors.MenuBackground,
                ForeColor = ThemeColors.TextColor,
                FlowDirection = FlowDirection.LeftToRight
            };

            btnPrev = new KryptonButton { Text = "← Previous", Margin = new Padding(5, 5, 0, 0), Width = 100 };
            btnNext = new KryptonButton { Text = "Next →", Margin = new Padding(5, 5, 0, 0), Width = 100 };
            lblPageInfo = new KryptonLabel
            {
                Text = "Page 1 of 1",
                Margin = new Padding(15, 10, 0, 0),
                StateCommon = { ShortText = { Color1 = ThemeColors.TextColor } }
            };
            paginationPanel.Controls.AddRange(new Control[] { btnPrev, btnNext, lblPageInfo });

            // Add layout
            tableLayout.RowCount = 4;
            tableLayout.RowStyles.Clear();
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45)); // Action bar
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 55)); // Filter row
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // Table row
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45)); // Pagination row

            tableLayout.Controls.Add(actionPanel, 0, 0);
            tableLayout.Controls.Add(filterPanel, 0, 1);
            tableLayout.Controls.Add(dgvAlerts, 0, 2);
            tableLayout.Controls.Add(paginationPanel, 0, 3);

            // Events
            // btnApplyFilters.Click += btnApplyFilters_Click;
            // btnRefresh.Click += btnRefresh_Click;
            // btnPrev.Click += btnPrev_Click;
            // btnNext.Click += btnNext_Click;

            this.Controls.Add(tableLayout);
            this.ResumeLayout(false);
        }

        private KryptonComboBox CreateFilterComboBox(int width)
        {
            var cb = new KryptonComboBox
            {
                Width = width,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            // cb.StateCommon.Back.Color1 = ThemeColors.ContentBackground;
            // cb.StateCommon.Content.Color1 = ThemeColors.TextColor;
            return cb;
        }


        #endregion



        private TableLayoutPanel tableLayout;
        private FlowLayoutPanel filterPanel;
        private FlowLayoutPanel paginationPanel;
        private FlowLayoutPanel actionPanel;

        private KryptonComboBox cbGame;
        private KryptonComboBox cbPlatform;
        private KryptonComboBox cbTimeframe; 
        
        // Buttons
        private KryptonButton btnRefresh;
        private KryptonButton btnApplyFilters;
        private KryptonButton btnPrev;
        private KryptonButton btnNext;

        // Labels
        private KryptonLabel lblPageInfo;

        // Filters
        private KryptonComboBox cboAlertType;

        // DataGridView
        private KryptonDataGridView dgvAlerts;
    }
}
