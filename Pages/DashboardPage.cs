using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using it13Project.Data;

namespace it13Project.Pages
{
    // private  lvAlerts;
    // private ListView lvReports;
    public partial class DashboardPage : UserControl
    {
        public DashboardPage()
        {
            InitializeComponent();

        }

        private void DashboardControl_Load(object sender, EventArgs e)
        {
            PopulateKpiCards();
            PopulateCharts();
            PopulateList();
        }

        private void LoadAlerts(ListView listView)
        {
            string query = "SELECT GameName, AlertMessage, AlertDate FROM Alerts ORDER BY AlertDate DESC";
            var dt = DatabaseHelper.ExecuteQuery(query);

            listView.Items.Clear();
            foreach (DataRow row in dt.Rows)
            {
                var item = new ListViewItem(row["GameName"].ToString());
                item.SubItems.Add(row["AlertMessage"].ToString());
                item.SubItems.Add(Convert.ToDateTime(row["AlertDate"]).ToString("yyyy-MM-dd"));
                listView.Items.Add(item);
            }
        }

        private void LoadReports(ListView listView)
        {
            string query = "SELECT ReportName, Schedule FROM Reports ORDER BY Schedule ASC";
            var dt = DatabaseHelper.ExecuteQuery(query);

            listView.Items.Clear();
            foreach (DataRow row in dt.Rows)
            {
                var item = new ListViewItem(row["ReportName"].ToString());
                item.SubItems.Add(row["Schedule"].ToString());
                listView.Items.Add(item);
            }
        }

        private void PopulateKpiCards()
        {
            try
            {
                kpiLayout.Controls.Clear();

                // kpiLayout.Controls.Add(
                //     CreateModernStatPanel(
                //         Properties.Resources.iconGame,
                //         "Review / Total Games",
                //         DashboardService.GetTotalGames().ToString("N0"),
                //         Color.FromArgb(52, 152, 219)
                //     )
                // );

                var (totalGames, reviewedGames) = DashboardService.GetReviewedTotalGames();

                kpiLayout.Controls.Add(
                    CreateModernStatPanel(
                        Properties.Resources.iconGame,
                        "Reviewed / Total Games",
                        $"{reviewedGames:N0} / {totalGames:N0}",
                        Color.FromArgb(52, 152, 219)
                    )
                );

                kpiLayout.Controls.Add(
                    CreateModernStatPanel(
                        Properties.Resources.iconReviews,
                        "Total Reviews",
                        DashboardService.GetTotalReviews().ToString("N0"),
                        Color.FromArgb(46, 204, 113)
                    )
                );

                kpiLayout.Controls.Add(
                    CreateModernStatPanel(
                        Properties.Resources.iconSentiment,
                        "Avg. Sentiment",
                        DashboardService.GetAverageSentiment().ToString("F0") + "%",
                        Color.FromArgb(155, 89, 182)
                    )
                );

                kpiLayout.Controls.Add(
                    CreateModernStatPanel(
                        Properties.Resources.iconRecommend,
                        // "% Recommended",
                        "# of Reviewers",
                        DashboardService.GetTotalReviewers().ToString("N0"),
                        // DashboardService.GetPercentRecommended().ToString("F0") + "%",
                        Color.FromArgb(241, 196, 15)
                    )
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to load KPI cards.\n\nError: " + ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


        private void PopulateCharts()
        {
            try
            {
                // Sentiment Trend
                var sentimentTrend = DashboardService.GetSentimentTrend();
                chartsLayout.Controls.Add(
                    CreateModernSectionPanel(
                        "Sentiment Trend",
                        graphType: GraphType.Signal,
                        dataX: sentimentTrend.Dates.Select(d => d.ToOADate()).ToArray(), // DateTime → double
                        dataY: sentimentTrend.Scores.ToArray()                          // List<double> → double[]
                    ), 0, 0);

                // Review Volume
                var reviewVolume = DashboardService.GetReviewVolume();
                chartsLayout.Controls.Add(
                    CreateModernSectionPanel(
                        "Review Volume",
                        graphType: GraphType.Bar,
                        dataX: reviewVolume.Dates.Select(d => d.ToOADate()).ToArray(),  // DateTime → double
                        dataY: reviewVolume.Counts.Select(c => (double)c).ToArray()     // int → double
                    ), 1, 0);

                // Top 5 Games
                var top5Games = DashboardService.GetTop5Games();
                chartsLayout.Controls.Add(
                    CreateModernSectionPanel(
                        "Highest Reviews",
                        dataY: top5Games.Counts.Select(c => (double)c).ToArray(),
                        labels: top5Games.GameNames.ToArray(),
                        graphType: GraphType.Bar
                    ), 2, 0);



            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to load charts.\n\nError: " + ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


        private void PopulateList()
        {
            alertsListView.Items.Clear();

            var alert1 = new ListViewItem("Cyberpunk 2077");
            alert1.SubItems.Add("Players reporting crashes");
            alert1.SubItems.Add(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"));
            alertsListView.Items.Add(alert1);

            var alert2 = new ListViewItem("Elden Ring");
            alert2.SubItems.Add("Negative reviews about difficulty");
            alert2.SubItems.Add(DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd"));
            alertsListView.Items.Add(alert2);

            var alert3 = new ListViewItem("Call of Duty");
            alert3.SubItems.Add("Server downtime complaints");
            alert3.SubItems.Add(DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"));
            alertsListView.Items.Add(alert3);


            // --- Reports List ---
            reportsListView.Items.Clear();

            var report1 = new ListViewItem("Weekly Sales Report");
            report1.SubItems.Add("Every Monday, 9 AM");
            reportsListView.Items.Add(report1);

            var report2 = new ListViewItem("Monthly Engagement Report");
            report2.SubItems.Add("1st of the month, 10 AM");
            reportsListView.Items.Add(report2);

            var report3 = new ListViewItem("Quarterly Financial Report");
            report3.SubItems.Add("Next: " + DateTime.Now.AddMonths(1).ToString("MMMM yyyy"));
            reportsListView.Items.Add(report3);
        }




    }
}
