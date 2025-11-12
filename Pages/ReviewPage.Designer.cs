using Krypton.Toolkit;
using ScottPlot.WinForms;
using it13Project.UI;

namespace it13Project.Pages
{
    partial class ReviewPage
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
            this.Name = "ReviewListPage";
            this.Size = new Size(900, 600);
            this.Padding = new Padding(20);
            this.BackColor = ThemeColors.ContentBackground;

            // Table layout container
            tableLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 1
            };
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45)); // Action bar
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 55)); // Filter row
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // Table row

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

            btnAdd = new KryptonButton { Text = "Add Review", Margin = new Padding(5, 5, 0, 0) };
            btnEdit = new KryptonButton { Text = "Edit Selected", Margin = new Padding(5, 5, 0, 0) };
            btnDelete = new KryptonButton { Text = "Delete Selected", Margin = new Padding(5, 5, 0, 0) };
            btnRefresh = new KryptonButton { Text = "Refresh", Margin = new Padding(5, 5, 0, 0) };
            btnRunModel = new KryptonButton { Text = "Run Sentiment Model", Margin = new Padding(5, 5, 0, 0), Width = 150 };
            btnRemoveSentiment = new KryptonButton { Text = "Remove Sentiment", Margin = new Padding(5, 5, 0, 0), Width = 150 };

            actionPanel.Controls.AddRange(new Control[] { btnAdd, btnEdit, btnDelete, btnRemoveSentiment, btnRunModel, btnRefresh });

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
                CueHint = { CueHintText = "Search review text or app name" }
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
            filterPanel.Controls.Add(cboSentiment);
            cboSentiment.SelectedIndex = 0;

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
                ShowCheckBox = true
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
                ShowCheckBox = true
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

            // Reviews DataGridView
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
                MultiSelect = true
            };

            dgvReviews.Columns.Add("app_name", "App Name");
            dgvReviews.Columns.Add("review_text", "Review");
            dgvReviews.Columns.Add("review_score_numeric", "Score");
            dgvReviews.Columns.Add("review_recommendation", "Recommended");
            dgvReviews.Columns.Add("predicted_sentiment", "Sentiment");
            dgvReviews.Columns.Add("confidence_score", "Confidence");
            dgvReviews.Columns.Add("review_date", "Date");

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
            // lblPageInfo = new KryptonLabel
            // {
            //     Text = "Page 1 of 1",
            //     Margin = new Padding(15, 10, 0, 0),
            //     StateCommon = { ShortText = { Color1 = ThemeColors.TextColor } }
            // };
            // Page Number Input
            txtCurrentPage = new KryptonTextBox
            {
                Width = 40,
                Margin = new Padding(15, 5, 0, 0),
                TextAlign = HorizontalAlignment.Center,
            };

            // Total Pages label
            lblTotalPages = new KryptonLabel
            {
                Margin = new Padding(5, 10, 0, 0),
                StateCommon = { ShortText = { Color1 = ThemeColors.TextColor } }
            };

            // Review count label
            lblReviewCount = new KryptonLabel
            {
                Margin = new Padding(10, 10, 0, 0),
                StateCommon = { ShortText = { Color1 = ThemeColors.TextColor } }
            };

            paginationPanel.Controls.AddRange(new Control[] 
            { 
                btnPrev, btnNext, 
                // lblPageInfo, 
                txtCurrentPage, lblTotalPages, lblReviewCount
            });



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

            btnAdd.Click += btnAdd_Click;
            btnEdit.Click += btnEdit_Click;
            btnApplyFilters.Click += btnApplyFilters_Click;
            btnDelete.Click += btnDelete_Click;
            btnRefresh.Click += btnRefresh_Click;
            btnPrev.Click += btnPrev_Click;
            btnNext.Click += btnNext_Click;
            btnRunModel.Click += btnRunSentiment_Click;
            btnRemoveSentiment.Click += btnDeleteSentiments_Click;

            txtCurrentPage.KeyDown += txtCurrentPage_KeyDown;
            txtSearch.KeyDown += txtSearch_KeyDown;

            this.Controls.Add(tableLayout);
            this.ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel paginationPanel;
        private KryptonButton btnPrev, btnNext;
        private KryptonLabel lblPageInfo;
        private TableLayoutPanel tableLayout;
        private FlowLayoutPanel actionPanel;
        private FlowLayoutPanel filterPanel;
        private KryptonButton btnAdd, btnEdit, btnDelete, btnRefresh, btnApplyFilters, btnRunModel, btnRemoveSentiment;
        private KryptonTextBox txtSearch;
        private KryptonDateTimePicker dtFrom, dtTo;
        private KryptonComboBox cboSentiment;
        private KryptonDataGridView dgvReviews;
        private KryptonTextBox txtPageJump;

        private KryptonTextBox txtCurrentPage;
        private KryptonLabel lblTotalPages;
        private KryptonLabel lblReviewCount;
    }
}
