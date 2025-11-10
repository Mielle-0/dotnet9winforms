using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace it13Project.Pages
{
    public partial class AlertsNotificationsPage : UserControl
    {
        public AlertsNotificationsPage()
        {
            InitializeComponent();
            PopulateFilters();
            PopulateAlerts(); // example data
        }

        private void AlertsControl_Load(object sender, EventArgs e)
        {

            // LoadAlertData();
        }

        private void PopulateFilters()
        {
            cbGame.Items.AddRange(new string[] { "All Games", "Game A", "Game B" });
            cbPlatform.Items.AddRange(new string[] { "All Platforms", "Steam", "Amazon" });
            cbTimeframe.Items.AddRange(new string[] { "Last 24h", "Last 7 Days", "Last 30 Days" });

            cbGame.SelectedIndex = 0;
            cbPlatform.SelectedIndex = 0;
            cbTimeframe.SelectedIndex = 0;
        }

        private void PopulateAlerts()
        {
            // Example alerts
            AddAlert(DateTime.Now, "Game A", "Crash bug on level 3", "Negative", "Critical", "https://steam.com");
            AddAlert(DateTime.Now, "Game B", "Great graphics!", "Positive", "Info", "https://amazon.com");
        }

        private void AddAlert(DateTime time, string game, string review, string sentiment, string alertType, string link)
        {
            alertsGrid.Rows.Add(time, game, review, sentiment, alertType, link);
        }
        
        private void AlertsGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (alertsGrid.Columns[e.ColumnIndex].Name == "Sentiment" && e.Value != null)
            {
                string? sentiment = e.Value.ToString();
                e.CellStyle.ForeColor = sentiment switch
                {
                    "Positive" => Color.LimeGreen,
                    "Neutral" => Color.Gray,
                    "Negative" => Color.Red,
                    _ => Color.Black
                };
            }
        }
    }
}
