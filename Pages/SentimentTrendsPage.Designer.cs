
using ScottPlot.WinForms;
using Krypton.Toolkit;
using it13Project.UI;
using it13Project.Data;

namespace it13Project.Pages
{
    partial class SentimentTrendsPage
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
            this.Name = "SentimentTrendsPage";
            this.Size = new Size(1000, 700);
            this.Padding = new Padding(20);
            this.Font = new Font("Segoe UI", 9.75F);

            // Filter Panel
            filterPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                ColumnCount = 7,
                RowCount = 1,
                BackColor = ThemeColors.MenuBackground,
                Padding = new Padding(10),
                AutoSize = true
            };

            // Row heights
            filterPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));   // top row: filters
            filterPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));   // bottom row: chips

            lblFilter = new KryptonLabel
            {
                Text = "Select Games:",
                AutoSize = true,
                ForeColor = ThemeColors.TextColor,
                StateCommon = { ShortText = { Font = new Font("Segoe UI", 10F, FontStyle.Regular), Color1 = ThemeColors.TextColor } }
            };

            // Search textbox
            txtGameSearch = new KryptonTextBox
            {
                Width = 250,
                Height = 25,
                CueHint = { CueHintText = "Search Games..." }
            };

            // Suggestions dropdown (hidden by default)
            lstSuggestions = new ListBox
            {
                Width = 250,
                Height = 100,
                Visible = false
            };

            // Date range controls
            lblDateRange = new KryptonLabel
            {
                Text = "Date Range:",
                AutoSize = true,
                Margin = new Padding(15, 5, 0, 0), // spacing from game selector
                StateCommon = { ShortText = { Font = new Font("Segoe UI", 10F, FontStyle.Regular), Color1 = ThemeColors.TextColor } }
            };

            dtpStart = new KryptonDateTimePicker
            {
                Width = 120,
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Today.AddMonths(-3),
                ShowCheckBox = true
            };

            lblTo = new KryptonLabel
            {
                Text = "to",
                AutoSize = true,
                StateCommon = { ShortText = { Color1 = ThemeColors.TextColor } },
                Margin = new Padding(5, 5, 5, 0)
            };

            dtpEnd = new KryptonDateTimePicker
            {
                Width = 120,
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Today,
                ShowCheckBox = true
            };

            btnApplyFilters = new KryptonButton
            {
                Text = "Apply Filters",
                OverrideDefault = { Back = { Color1 = ThemeColors.AccentPrimary } },
                StateCommon = { Content = { ShortText = { Color1 = Color.Black } } },
                Margin = new Padding(10, 0, 0, 0),
            };

            btnShowSelectedGames = new KryptonButton
            {
                Text = "Selected Games ▼",
                Width = 150,
                Height = 25,
                Margin = new Padding(5, 0, 0, 0)
            };

            // Container for selected games ("chips")
            selectedGamesPanel = new FlowLayoutPanel
            {
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                Padding = new Padding(5),
                BackColor = ThemeColors.ContentBackground,
                BorderStyle = BorderStyle.FixedSingle,
                Visible = false,
                // Width = filterPanel.Width - 20
                MaximumSize = new Size(800, 0)
            };

            btnShowSelectedGames.Click += BtnShowSelectedGames_Click;
            btnApplyFilters.Click += BtnApplyFilters_Click;

            txtGameSearch.TextChanged += (s, e) =>
            {
                var query = txtGameSearch.Text.Trim();
                if (string.IsNullOrWhiteSpace(query))
                {
                    lstSuggestions.Visible = false;
                    return;
                }

                var matches = _allGames
                    .Where(g => g.Name.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0)
                    .Take(5)
                    .ToList();

                lstSuggestions.Items.Clear();
                foreach (var m in matches)
                    lstSuggestions.Items.Add(m);

                if (matches.Any())
                {
                    // 🔹 Position dropdown under the search box
                    var screenPos = txtGameSearch.Parent.PointToScreen(txtGameSearch.Location);
                    var formPos = this.PointToClient(screenPos);

                    lstSuggestions.Location = new Point(formPos.X, formPos.Y + txtGameSearch.Height);
                    lstSuggestions.BringToFront();
                    lstSuggestions.Visible = true;
                }
                else
                {
                    lstSuggestions.Visible = false;
                }
            };

            lstSuggestions.Click += (s, e) =>
            {
                if (lstSuggestions.SelectedItem is GameInfo selectedGame)
                {
                    AddSelectedGameChip(selectedGame);
                    txtGameSearch.Clear();
                    lstSuggestions.Visible = false;
                }
            };


            // Row 0: filters
            filterPanel.Controls.Add(lblFilter, 0, 0);
            filterPanel.Controls.Add(txtGameSearch, 1, 0);
            filterPanel.Controls.Add(lblDateRange, 2, 0);
            filterPanel.Controls.Add(dtpStart, 3, 0);
            filterPanel.Controls.Add(lblTo, 4, 0);
            filterPanel.Controls.Add(dtpEnd, 5, 0);
            filterPanel.Controls.Add(btnApplyFilters, 6, 0);
            filterPanel.Controls.Add(btnShowSelectedGames, 7, 0); // add in top row

            // Row 1: chips (span across all columns)
            // filterPanel.Controls.Add(selectedGamesPanel, 0, 1);
            // filterPanel.SetColumnSpan(selectedGamesPanel, 7);


            this.Controls.Add(lstSuggestions);
            lstSuggestions.BringToFront();
            lstSuggestions.Visible = false;
            
            // Add it to the top-level form (or container), not inside filterPanel
            this.Controls.Add(selectedGamesPanel);
            selectedGamesPanel.BringToFront();


            // KPI Summary Panel
            kpiPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(5),
                BackColor = ThemeColors.ContentBackground
            };

            lblTotalReviews = new KryptonLabel { Text = "Total Reviews: 0", Width = 200, StateCommon = { ShortText = { Font = new Font("Segoe UI", 11F, FontStyle.Bold) } } };
            lblPositive = new KryptonLabel { Text = "Positive: 0%", Width = 200 };
            lblNeutral = new KryptonLabel { Text = "Neutral: 0%", Width = 200 };
            lblNegative = new KryptonLabel { Text = "Negative: 0%", Width = 200 };

            kpiPanel.Controls.Add(lblTotalReviews);
            kpiPanel.Controls.Add(lblPositive);
            kpiPanel.Controls.Add(lblNeutral);
            kpiPanel.Controls.Add(lblNegative);

            // Charts Split
            var chartSplit = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Vertical,
                SplitterDistance = 600
            };

            sentimentTrendPlot = new FormsPlot { Dock = DockStyle.Fill };
            sentimentPiePlot = new FormsPlot { Dock = DockStyle.Fill };

            chartSplit.Panel1.Controls.Add(sentimentTrendPlot);
            chartSplit.Panel2.Controls.Add(sentimentPiePlot);

            reviewGroup = CreateStyledListView(
                "Recent Reviews",
                new[] { "Review", "Sentiment", "Confidence (%)" },
                out reviewListView
            );

            reviewGroup.Dock = DockStyle.Bottom;
            reviewGroup.Height = 250;
            reviewGroup.Width = this.ClientSize.Width;

            // Add everything to the control
            this.Controls.Add(chartSplit);
            this.Controls.Add(reviewGroup);
            this.Controls.Add(kpiPanel);
            this.Controls.Add(filterPanel);

            this.BackColor = ThemeColors.ContentBackground;
            this.ResumeLayout(false);
            this.AutoScroll = true;
        }


        private Krypton.Toolkit.KryptonGroup CreateStyledListView(
            string title,
            string[] columns,
            out ListView listView)
        {
            var group = new Krypton.Toolkit.KryptonGroup
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(8),
                StateCommon =
                {
                    Back = { Color1 = ThemeColors.ContentBackground },
                    Border =
                    {
                        DrawBorders = PaletteDrawBorders.All,
                        Rounding = 10,
                        Width = 1,
                        Color1 = ThemeColors.MenuHover
                    }
                }
            };

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 1
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 35));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            var lblTitle = new Krypton.Toolkit.KryptonLabel
            {
                Text = title,
                Dock = DockStyle.Fill,
                StateCommon =
                {
                    ShortText =
                    {
                        Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                        Color1 = Color.Black
                    }
                },
                Padding = new Padding(6, 3, 6, 3),
            };

            // 👇 local variable
            var lv = new ListView
            {
                Dock = DockStyle.Fill,
                View = View.Details,
                FullRowSelect = true,
                HideSelection = false,
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 11F),
                BackColor = ThemeColors.ContentBackground,
                ForeColor = ThemeColors.MenuBackground,
                HeaderStyle = ColumnHeaderStyle.Nonclickable
            };

            for (int i = 0; i < columns.Length; i++)
            {
                var col = lv.Columns.Add(columns[i]);
                if (i == 0)
                    col.Width = -2; // auto-size Review column to fill
                else
                    col.Width = 140; // fixed width for Sentiment and Confidence
            }

            // Double buffering
            typeof(Control).GetProperty("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(lv, true, null);

            lv.OwnerDraw = true;
            lv.DrawColumnHeader += (s, e) =>
            {
                e.Graphics.FillRectangle(new SolidBrush(ThemeColors.MenuHover), e.Bounds);
                TextRenderer.DrawText(e.Graphics, e.Header.Text, lv.Font, e.Bounds, ThemeColors.TextColor);
            };

            lv.DrawItem += (s, e) => e.DrawDefault = true;
            lv.DrawSubItem += (s, e) =>
            {
                var bgColor = e.Item.Selected ? ThemeColors.AccentPrimary : lv.BackColor;
                var textColor = e.Item.Selected ? ThemeColors.TextColor : lv.ForeColor;

                // e.Graphics.FillRectangle(new SolidBrush(bgColor), e.Bounds);
                // TextRenderer.DrawText(e.Graphics, e.SubItem.Text, lv.Font, e.Bounds, textColor);
                e.Graphics.FillRectangle(new SolidBrush(bgColor), e.Bounds);

                TextRenderer.DrawText(
                    e.Graphics,
                    e.SubItem.Text,
                    lv.Font,
                    e.Bounds,
                    textColor,
                    TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);

            };

            layout.Controls.Add(lblTitle, 0, 0);
            layout.Controls.Add(lv, 0, 1);

            group.Panel.Controls.Add(layout);

            // 👇 assign after lambdas are done
            listView = lv;
            lv.Resize += (s, e) =>
            {
                if (lv.Columns.Count > 0)
                {
                    int fixedWidth = lv.Columns.Cast<ColumnHeader>().Skip(1).Sum(c => c.Width);
                    lv.Columns[0].Width = lv.ClientSize.Width - fixedWidth;
                }
            };


            return group;
        }



        #endregion


        private KryptonLabel lblTotalReviews, lblPositive,
                            lblNeutral, lblNegative,
                            lblFilter, lblDateRange, lblTo;
        private FormsPlot sentimentTrendPlot, sentimentPiePlot;
        // KPI Labels
        private FlowLayoutPanel kpiPanel, selectedGamesPanel;
        private TableLayoutPanel filterPanel;
        private KryptonDateTimePicker dtpStart, dtpEnd;
        private KryptonDataGridView reviewGrid;
        private KryptonCheckedListBox clbGames;
        private KryptonGroup reviewGroup;
        private ListView reviewListView;
        private KryptonButton btnApplyFilters, btnShowSelectedGames;
        private KryptonTextBox txtGameSearch;
        private ListBox lstSuggestions;


    }
}
