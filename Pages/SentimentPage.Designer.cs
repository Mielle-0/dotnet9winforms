using it13Project.UI;
using Krypton.Toolkit;

namespace it13Project.Pages
{
    partial class SentimentPage
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
            this.Name = "ReviewSentimentPage";
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

            // btnAdd = new KryptonButton { Text = "Add Review", Margin = new Padding(5, 5, 0, 0) };
            btnEdit = new KryptonButton { Text = "Edit Selected", Margin = new Padding(5, 5, 0, 0) };
            btnDelete = new KryptonButton { Text = "Delete Selected", Margin = new Padding(5, 5, 0, 0) };
            btnRefresh = new KryptonButton { Text = "Refresh", Margin = new Padding(5, 5, 0, 0) };

            actionPanel.Controls.AddRange(new Control[] { btnEdit, btnDelete, btnRefresh });

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

            // Search box
            filterPanel.Controls.Add(new KryptonLabel
            {
                Text = "Search:",
                StateCommon = { ShortText = { Color1 = ThemeColors.TextColor } },
                Margin = new Padding(5, 10, 0, 0)
            });

            txtSearch = new KryptonTextBox
            {
                Width = 200,
                CueHint = { CueHintText = "Search review text or game name" }
            };
            filterPanel.Controls.Add(txtSearch);

            // Sentiment filter
            filterPanel.Controls.Add(new KryptonLabel
            {
                Text = "Sentiment:",
                StateCommon = { ShortText = { Color1 = ThemeColors.TextColor } },
                Margin = new Padding(15, 10, 0, 0)
            });

            cboSentiment = new KryptonComboBox
            {
                Width = 120,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cboSentiment.Items.AddRange(new object[] { "All", "Positive", "Negative", "Neutral" });
            cboSentiment.SelectedIndex = 0;
            filterPanel.Controls.Add(cboSentiment);

            // Date filters
            filterPanel.Controls.Add(new KryptonLabel
            {
                Text = "From:",
                StateCommon = { ShortText = { Color1 = ThemeColors.TextColor } },
                Margin = new Padding(15, 10, 0, 0)
            });

            dtFrom = new KryptonDateTimePicker
            {
                Width = 120,
                Format = DateTimePickerFormat.Short,
                ShowCheckBox = true,
                Checked = false
            };
            filterPanel.Controls.Add(dtFrom);

            filterPanel.Controls.Add(new KryptonLabel
            {
                Text = "To:",
                StateCommon = { ShortText = { Color1 = ThemeColors.TextColor } },
                Margin = new Padding(5, 10, 0, 0)
            });

            dtTo = new KryptonDateTimePicker
            {
                Width = 120,
                Format = DateTimePickerFormat.Short,
                ShowCheckBox = true,
                Checked = false
            };
            filterPanel.Controls.Add(dtTo);

            // Apply button
            btnApplyFilters = new KryptonButton
            {
                Text = "Apply Filters",
                OverrideDefault = { Back = { Color1 = ThemeColors.AccentPrimary } },
                StateCommon = { Content = { ShortText = { Color1 = Color.Black } } },
                Margin = new Padding(15, 5, 0, 0),
                Width = 120
            };
            filterPanel.Controls.Add(btnApplyFilters);

            // Sentiment DataGridView
            dgvReviews = new KryptonDataGridView
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
            dgvReviews.Columns.Clear();
            var colId = new DataGridViewTextBoxColumn
            {
                Name = "SentimentId",
                DataPropertyName = "SentimentId", // Must match SentimentDto property
                Visible = false
            };
            dgvReviews.Columns.Add(colId);
            // dgvReviews.Columns.Add("app_name", "Game Name");
            // dgvReviews.Columns.Add("review_text", "Review Text");
            // dgvReviews.Columns.Add("predicted_sentiment", "Predicted Sentiment");
            // dgvReviews.Columns.Add("confidence_score", "Confidence Score");

            dgvReviews.Columns.Add("GameName", "Game Name");
            dgvReviews.Columns.Add("ReviewText", "Review Text");
            dgvReviews.Columns.Add("PredictedSentiment", "Sentiment");
            dgvReviews.Columns.Add("ConfidenceScore", "Confidence Score");

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
            tableLayout.Controls.Add(dgvReviews, 0, 2);
            tableLayout.Controls.Add(paginationPanel, 0, 3);

            // Events
            btnApplyFilters.Click += btnApplyFilters_Click;
            btnRefresh.Click += btnRefresh_Click;
            btnPrev.Click += btnPrev_Click;
            btnNext.Click += btnNext_Click;
            btnDelete.Click += btnDeleteSentiments_Click;

            this.Controls.Add(tableLayout);
            this.ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel paginationPanel;
        private KryptonButton btnPrev, btnNext, btnApplyFilters, btnRefresh, btnEdit, btnDelete;
        private KryptonLabel lblPageInfo;
        private TableLayoutPanel tableLayout;
        private FlowLayoutPanel filterPanel, actionPanel;
        private KryptonTextBox txtSearch;
        private KryptonDateTimePicker dtFrom, dtTo;
        private KryptonComboBox cboSentiment;
        private KryptonDataGridView dgvReviews;

    }
}
