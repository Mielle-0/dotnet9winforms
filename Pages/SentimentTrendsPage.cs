using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using it13Project.UI;
using ScottPlot;
using it13Project.Data;

namespace it13Project.Pages
{
    public partial class SentimentTrendsPage : UserControl
    {
        private bool displayReview = false;
        private readonly TrendService trendService = new TrendService();
        private List<GameInfo> _allGames;
        public SentimentTrendsPage()
        {
            InitializeComponent();
            trendService.LoadSentimentData();

            _allGames = TrendService.GetAllGames();
            // Populate everything on load
            PopulateKpiCards();
            PopulateTrendChart();
            PopulatePieChart();

            reviewGroup.Visible = displayReview;
            if (displayReview)
                PopulateReviewFeed();
                
            
        }

        // ---------------- KPI ----------------
        private void PopulateKpiCards()
        {
            try
            {
                var (total, posPct, neuPct, negPct) = trendService.GetKpiData();

                lblTotalReviews.Text = $"Total Reviews: {total}";
                lblPositive.Text = $"Positive: {posPct}%";
                lblNeutral.Text = $"Neutral: {neuPct}%";
                lblNegative.Text = $"Negative: {negPct}%";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load KPI data.\n\nError: " + ex.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------------- Trend Chart ----------------
        private void PopulateTrendChart()
        {
            try
            {
                var (months, pos, neu, neg) = trendService.GetTrendChartData();

                sentimentTrendPlot.Plot.Clear();

                double[] xs = Enumerable.Range(0, months.Length).Select(i => (double)i).ToArray();
                double[] total = pos.Zip(neu, (p, n) => p + n)
                                    .Zip(neg, (pn, ng) => pn + ng)
                                    .Select(v => (double)v)
                                    .ToArray();

                double[] posPct = pos.Zip(total, (v, t) => t > 0 ? v / t * 100 : 0).ToArray();
                double[] neuPct = neu.Zip(total, (v, t) => t > 0 ? v / t * 100 : 0).ToArray();
                double[] negPct = neg.Zip(total, (v, t) => t > 0 ? v / t * 100 : 0).ToArray();

                sentimentTrendPlot.Plot.Add.Scatter(xs, posPct).LegendText = "Positive";
                sentimentTrendPlot.Plot.Add.Scatter(xs, neuPct).LegendText = "Neutral";
                sentimentTrendPlot.Plot.Add.Scatter(xs, negPct).LegendText = "Negative";

                sentimentTrendPlot.Plot.Axes.Bottom.TickGenerator =
                    new ScottPlot.TickGenerators.NumericManual(xs, months);
                sentimentTrendPlot.Plot.Axes.Bottom.Label.Text = "Month";
                sentimentTrendPlot.Plot.Axes.Left.Label.Text = "Sentiment (%)";
                sentimentTrendPlot.Plot.Title("Sentiment Trends");
                sentimentTrendPlot.Plot.Legend.IsVisible = true;

                sentimentTrendPlot.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load trend chart.\n\nError: " + ex.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------------- Pie Chart ----------------
        private void PopulatePieChart()
        {
            try
            {
                var (pos, neu, neg) = trendService.GetPieChartData();

                sentimentPiePlot.Plot.Clear();
                var totals = new[] { pos, neu, neg };
                var labels = new[] { "Positive", "Neutral", "Negative" };
                var colors = new[]
                {
                    ScottPlot.Color.FromHex("#2ecc71"),   // Green
                    ScottPlot.Color.FromHex("#f1c40f"),   // Goldenrod/Yellow
                    ScottPlot.Color.FromHex("#e74c3c")    // Red
                };

                var slices = totals.Select((val, i) =>
                    new ScottPlot.PieSlice(val, colors[i], labels[i])).ToList();

                sentimentPiePlot.Plot.Add.Pie(slices).ExplodeFraction = 0.05;
                sentimentPiePlot.Plot.Title("Overall Distribution");

                // Hide axis/ticks
                sentimentPiePlot.Plot.Axes.Left.IsVisible = false;
                sentimentPiePlot.Plot.Axes.Right.IsVisible = false;
                sentimentPiePlot.Plot.Axes.Top.IsVisible = false;
                sentimentPiePlot.Plot.Axes.Bottom.IsVisible = false;

                sentimentPiePlot.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load pie chart.\n\nError: " + ex.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------------- Review Feed ----------------
        private void PopulateReviewFeed()
        {
            try
            {
                reviewListView.Items.Clear();

                foreach (var review in trendService.GetReviewFeed())
                {
                    reviewListView.Items.Add(new ListViewItem(new[]
                    {
                        review.text,
                        review.sentiment,
                        review.confidence
                    }));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load review feed.\n\nError: " + ex.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void AddSelectedGameChip(GameInfo game)
        {
            // Prevent duplicates
            if (selectedGamesPanel.Controls.OfType<System.Windows.Forms.Label>().Any(l => l.Tag is int id && id == game.GameId))
                return;

            var chip = new System.Windows.Forms.Label
            {
                Text = game.Name + " ✕",
                Tag = game.GameId, 
                AutoSize = true,
                Width = 50,
                Height = 24,
                MaximumSize = new Size(200, 24),
                Padding = new Padding(5, 3, 5, 3),
                Margin = new Padding(3),
                BackColor = ThemeColors.AccentPrimary,
                ForeColor = System.Drawing.Color.Black,
                Cursor = Cursors.Hand,
                AutoEllipsis = true,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Remove chip on click
            chip.Click += (s, e) =>
            {
                selectedGamesPanel.Controls.Remove(chip);
            };


            selectedGamesPanel.Controls.Add(chip);
        }


        private void PopulateTrendChartFiltered()
        {
            try
            {
                // Collect selected game IDs from chips
                var selectedGameIds = selectedGamesPanel.Controls
                    .OfType<System.Windows.Forms.Label>()
                    .Where(l => l.Tag is int)
                    .Select(l => (int)l.Tag)
                    .ToList();

                // Collect date range if checkboxes are checked
                DateTime? startDate = dtpStart.Checked ? dtpStart.Value.Date : (DateTime?)null;
                DateTime? endDate = dtpEnd.Checked ? dtpEnd.Value.Date : (DateTime?)null;

                // Load filtered data into service
                trendService.LoadSentimentDataFiltered(selectedGameIds, startDate, endDate);

                // Retrieve the processed trend data
                var (months, pos, neu, neg) = trendService.GetTrendChartData();

                sentimentTrendPlot.Plot.Clear();

                double[] xs = Enumerable.Range(0, months.Length).Select(i => (double)i).ToArray();
                double[] total = pos.Zip(neu, (p, n) => p + n)
                                    .Zip(neg, (pn, ng) => pn + ng)
                                    .Select(v => (double)v)
                                    .ToArray();

                double[] posPct = pos.Zip(total, (v, t) => t > 0 ? v / t * 100 : 0).ToArray();
                double[] neuPct = neu.Zip(total, (v, t) => t > 0 ? v / t * 100 : 0).ToArray();
                double[] negPct = neg.Zip(total, (v, t) => t > 0 ? v / t * 100 : 0).ToArray();

                sentimentTrendPlot.Plot.Add.Scatter(xs, posPct).LegendText = "Positive";
                sentimentTrendPlot.Plot.Add.Scatter(xs, neuPct).LegendText = "Neutral";
                sentimentTrendPlot.Plot.Add.Scatter(xs, negPct).LegendText = "Negative";

                sentimentTrendPlot.Plot.Axes.Bottom.TickGenerator =
                    new ScottPlot.TickGenerators.NumericManual(xs, months);
                sentimentTrendPlot.Plot.Axes.Bottom.Label.Text = "Month";
                sentimentTrendPlot.Plot.Axes.Left.Label.Text = "Sentiment (%)";
                sentimentTrendPlot.Plot.Title("Sentiment Trends");
                sentimentTrendPlot.Plot.Legend.IsVisible = true;

                sentimentTrendPlot.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load trend chart.\n\nError: " + ex.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulatePieChartFiltered()
        {
            try
            {
                // Just reuse what service has after filtered load
                var (pos, neu, neg) = trendService.GetPieChartData();

                sentimentPiePlot.Plot.Clear();

                var totals = new[] { pos, neu, neg };
                var labels = new[] { "Positive", "Neutral", "Negative" };
                var colors = new[]
                {
            ScottPlot.Color.FromHex("#2ecc71"), // Green
            ScottPlot.Color.FromHex("#f1c40f"), // Yellow
            ScottPlot.Color.FromHex("#e74c3c")  // Red
        };

                var slices = totals.Select((val, i) =>
                    new ScottPlot.PieSlice(val, colors[i], labels[i])).ToList();

                sentimentPiePlot.Plot.Add.Pie(slices).ExplodeFraction = 0.05;
                sentimentPiePlot.Plot.Title("Overall Distribution");

                // Hide axis/ticks
                sentimentPiePlot.Plot.Axes.Left.IsVisible = false;
                sentimentPiePlot.Plot.Axes.Right.IsVisible = false;
                sentimentPiePlot.Plot.Axes.Top.IsVisible = false;
                sentimentPiePlot.Plot.Axes.Bottom.IsVisible = false;

                sentimentPiePlot.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load pie chart.\n\nError: " + ex.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }






        private void BtnApplyFilters_Click(object sender, EventArgs e)
        {
            PopulateTrendChartFiltered();
            PopulatePieChartFiltered();
        }


        private void BtnShowSelectedGames_Click(object sender, EventArgs e)
        {
            if (selectedGamesPanel.Visible)
            {
                selectedGamesPanel.Visible = false;
                btnShowSelectedGames.Text = "Selected Games ▼";
            }
            else
            {
                // Position the panel under the button
                var screenPos = btnShowSelectedGames.Parent.PointToScreen(btnShowSelectedGames.Location);
                var formPos = this.PointToClient(screenPos);

                selectedGamesPanel.Location = new Point(formPos.X, formPos.Y + btnShowSelectedGames.Height);
                selectedGamesPanel.Width = 300; // adjust as needed
                selectedGamesPanel.Visible = true;
                selectedGamesPanel.BringToFront();

                btnShowSelectedGames.Text = "Selected Games ▲";
            }
        }


    }
}
