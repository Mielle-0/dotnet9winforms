
using System.Drawing;
using System.Windows.Forms;
using Krypton.Toolkit;
using ScottPlot.WinForms;
using it13Project.UI;
using Krypton.Navigator;

namespace it13Project.Pages
{
    partial class ReportsPage
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
            this.Name = "ReportsPage";
            this.Size = new Size(1000, 700);
            this.Padding = new Padding(20);
            this.BackColor = ThemeColors.ContentBackground;

            // Build sections
            var topBar = BuildTopBar();
            tableLayout = BuildMainLayout();
            var filterPanel = BuildFilterPanel();
            var previewPanel = BuildPreviewPanel();
            var schedulePanel = BuildSchedulePanel();

            // Assemble
            // tableLayout.Controls.Add(filterPanel, 0, 0);
            tableLayout.SetColumnSpan(previewPanel, 2);
            tableLayout.Controls.Add(previewPanel, 0, 0);
            tableLayout.SetColumnSpan(schedulePanel, 2);
            tableLayout.Controls.Add(schedulePanel, 0, 1);

            btnExportPdf.Click += BtnExportPdf_Click;
            btnExportExcel.Click += BtnExportExcel_Click;
            this.Controls.Add(tableLayout);
            this.Controls.Add(topBar);
            this.ResumeLayout(false);
        }

        private Control BuildTopBar()
        {
            var topBar = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = ThemeColors.MenuBackground,
                Padding = new Padding(10),
                FlowDirection = FlowDirection.LeftToRight
            };

            cboTemplate = new KryptonComboBox { Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };
            cboTemplate.Items.AddRange(new object[] { "Weekly", "Monthly", "Quarterly" });

            dtFrom = new KryptonDateTimePicker
            {
                Width = 150,
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Today.AddMonths(-3),
                ShowCheckBox = true,
                Checked = false
            };
            dtTo = new KryptonDateTimePicker
            {
                Width = 150,
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Today,
                ShowCheckBox = true,
                Checked = false
            };
            btnApplyFilters = new KryptonButton { Text = "Apply Filters" };
            btnApplyFilters.Click += BtnApplyFilters_Click;

            cboGame = new KryptonComboBox
            {
                Width = 250,
                DropDownStyle = ComboBoxStyle.DropDown,
                AutoCompleteMode = AutoCompleteMode.SuggestAppend,
                AutoCompleteSource = AutoCompleteSource.ListItems
            };

            // TODO: populate this with real game names from DB
            cboGame.Items.AddRange(new object[] { "Elden Ring", "Cyberpunk 2077", "Stardew Valley" });


            topBar.Controls.Add(MakeLabel("Template:"));
            topBar.Controls.Add(cboTemplate);
            topBar.Controls.Add(MakeLabel("Game:"));
            topBar.Controls.Add(cboGame);
            topBar.Controls.Add(MakeLabel("From:"));
            topBar.Controls.Add(dtFrom);
            topBar.Controls.Add(MakeLabel("To:"));
            topBar.Controls.Add(dtTo);
            topBar.Controls.Add(btnApplyFilters);

            return topBar;
        }

        private TableLayoutPanel BuildMainLayout()
        {
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 2
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 70));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 30));
            return layout;
        }

        private Control BuildFilterPanel()
        {
            filterPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                Padding = new Padding(10),
                BackColor = ThemeColors.MenuBackground
            };

            filterPanel.Controls.Add(MakeLabel("Filters", bold: true));
            filterPanel.Controls.Add(new KryptonCheckBox { Text = "Include Charts" });
            filterPanel.Controls.Add(new KryptonCheckBox { Text = "Include Metadata" });
            filterPanel.Controls.Add(new KryptonCheckBox { Text = "Summary Only" });

            return filterPanel;
        }

        private Control BuildPreviewPanel()
        {
            var previewPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10)
            };

            // === Tab Control for Report Sections ===
            tabControl = new KryptonNavigator
            {
                Dock = DockStyle.Fill
            };

            // 1️⃣ Sentiment Trend (Line Chart)
            tabTrend = new KryptonPage { Text = "Sentiment Trend" };
            chartTrend = new FormsPlot { Dock = DockStyle.Fill };
            tabTrend.Controls.Add(chartTrend);

            // 2️⃣ Top Games (Bar Chart)
            tabTopGames = new KryptonPage { Text = "Top Games" };
            chartTopGames = new FormsPlot { Dock = DockStyle.Fill };
            gridTopGames = new KryptonDataGridView { Dock = DockStyle.Fill };
            tabTopGames.Controls.Add(chartTopGames);

            // 3️⃣ Recommendation Breakdown (Pie Chart)
            tabReco = new KryptonPage { Text = "Recommendations" };
            chartReco = new FormsPlot { Dock = DockStyle.Fill };
            tabReco.Controls.Add(chartReco);

            // 4️⃣ Genre Heatmap
            tabGenre = new KryptonPage { Text = "Genre Sentiment" };
            chartHeatmap = new FormsPlot { Dock = DockStyle.Fill };
            tabGenre.Controls.Add(chartHeatmap);

            // 5️⃣ Controversial Games (Table)
            tabControversial = new KryptonPage { Text = "Controversial Games" };
            gridControversial = new KryptonDataGridView { Dock = DockStyle.Fill };
            gridControversial.Columns.Add("Game", "Game");
            gridControversial.Columns.Add("Positive", "Positive");
            gridControversial.Columns.Add("Negative", "Negative");
            gridControversial.Columns.Add("Review #", "Review #"); // Total reviews
            gridControversial.Columns.Add("Ratio", "Pos/Neg Ratio");
            tabControversial.Controls.Add(gridControversial);

            // 6️⃣ Low Confidence Reviews (Table)
            tabLowConf = new KryptonPage { Text = "Low Confidence" };
            gridLowConf = new KryptonDataGridView { Dock = DockStyle.Fill };
            gridLowConf.Columns.Add("ReviewID", "Review ID");
            gridLowConf.Columns.Add("Game", "Game");
            gridLowConf.Columns.Add("Review", "Review");
            gridLowConf.Columns.Add("Confidence", "Confidence");
            tabLowConf.Controls.Add(gridLowConf);

            // Add pages to tab control
            tabControl.Pages.AddRange(new KryptonPage[]
            {
                tabTrend, tabTopGames, tabReco, tabGenre, tabControversial, tabLowConf
            });

            // Name tabs for role-based access
            tabTrend.Name = "SentimentTrend";
            tabTopGames.Name = "TopGames";
            tabReco.Name = "Recommendations";
            tabGenre.Name = "GenreSentiment";
            tabControversial.Name = "ControversialGames";
            tabLowConf.Name = "LowConfidence";

            // === Export Buttons ===
            btnExportPdf = new KryptonButton { Text = "Export PDF", Width = 100 };
            btnExportExcel = new KryptonButton { Text = "Export Excel", Width = 100 };

            var btnExportCurrent = new KryptonButton { Text = "Export Current Tab", Width = 150 };

            var exportBar = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 40,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(0, 5, 0, 5)
            };
            exportBar.Controls.AddRange(new Control[]
            {
                btnExportPdf, btnExportExcel
                // btnExportCurrent
                });

            // Assemble preview panel
            previewPanel.Controls.Add(tabControl);
            previewPanel.Controls.Add(exportBar);

            return previewPanel;
        }

        private Control BuildSchedulePanel()
        {
            dgvSchedules = new KryptonDataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            dgvSchedules.Columns.Add("Name", "Report Name");
            dgvSchedules.Columns.Add("Format", "Format");
            dgvSchedules.Columns.Add("Recipients", "Recipients");
            dgvSchedules.Columns.Add("Frequency", "Frequency");
            dgvSchedules.Columns.Add("NextRun", "Next Run");

            btnSchedule = new KryptonButton { Text = "+ Schedule New Report", Dock = DockStyle.Top };

            var schedulePanel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(10) };
            schedulePanel.Controls.Add(dgvSchedules);
            schedulePanel.Controls.Add(btnSchedule);

            return schedulePanel;
        }

        private KryptonLabel MakeLabel(string text, bool bold = false)
        {
            return new KryptonLabel
            {
                Text = text,
                StateCommon = { ShortText = {
                    Font = new Font("Segoe UI", bold ? 10 : 9, bold ? FontStyle.Bold : FontStyle.Regular),
                    Color1 = ThemeColors.TextColor } }
            };
        }


        #endregion


        private KryptonComboBox cboTemplate;
        private KryptonDateTimePicker dtFrom, dtTo;
        private KryptonButton btnApplyFilters, btnExportPdf, btnExportExcel, btnSchedule;
        private FormsPlot chartPreview;
        private FlowLayoutPanel filterPanel;
        private TableLayoutPanel tableLayout;
        private KryptonDataGridView dgvSchedules;
        private KryptonNavigator tabControl;
        private KryptonComboBox cboGame;

        // Pages
        private KryptonPage tabTrend;
        private KryptonPage tabTopGames;
        private KryptonPage tabReco;
        private KryptonPage tabGenre;
        private KryptonPage tabControversial;
        private KryptonPage tabLowConf;

        // Charts
        private FormsPlot chartTrend;
        private FormsPlot chartTopGames;
        private FormsPlot chartReco;
        private FormsPlot chartHeatmap;

        // Grids
        private KryptonDataGridView gridTopGames;
        private KryptonDataGridView gridControversial;
        private KryptonDataGridView gridLowConf;
    }
}
