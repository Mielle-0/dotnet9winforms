using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using it13Project.Data;
using it13Project.Models;

namespace it13Project.Pages
{
    public partial class SentimentPage : UserControl
    {
        private int currentPage = 1;
        private const int pageSize = 30; // Adjust per page results
        public SentimentPage()
        {
            InitializeComponent();
            btnRefresh.PerformClick();
        }

        private void LoadSentiments()
        {
            // Build filter values
            string search = string.IsNullOrWhiteSpace(txtSearch.Text) ? null : txtSearch.Text;
            string sentiment = cboSentiment.SelectedItem?.ToString();
            DateTime? fromDate = dtFrom.Checked ? dtFrom.Value.Date : (DateTime?)null;
            DateTime? toDate = dtTo.Checked ? dtTo.Value.Date : (DateTime?)null;

            // Fetch paged results
            var sentiments = SentimentService.GetSentimentsPaged(
                currentPage,
                pageSize,
                search,
                sentiment,
                fromDate,
                toDate
            );

            dgvReviews.DataSource = null;
            dgvReviews.AutoGenerateColumns = false;
            dgvReviews.Rows.Clear();

            foreach (var s in sentiments)
            {
                dgvReviews.Rows.Add(
                    s.SentimentId,
                    s.AppName,
                    s.ReviewText,
                    s.PredictedSentiment,
                    s.ConfidenceScore.ToString("0.00")
                );
            }


            // Update page info
            int totalRecords = SentimentService.GetTotalCount(search, sentiment, fromDate, toDate);
            int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            lblPageInfo.Text = $"Page {currentPage} of {totalPages}";

            // Enable/disable navigation
            btnPrev.Enabled = currentPage > 1;
            btnNext.Enabled = currentPage < totalPages;
        }

        private void btnApplyFilters_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            LoadSentiments();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadSentiments();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            currentPage++;
            LoadSentiments();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            txtSearch.Clear();
            cboSentiment.SelectedIndex = 0;
            dtFrom.Checked = false;
            dtTo.Checked = false;
            LoadSentiments();
        }

        private void btnDeleteSentiments_Click(object sender, EventArgs e)
        {
            var selectedIds = new List<int>();

            foreach (DataGridViewRow row in dgvReviews.SelectedRows)
            {
                int sentimentId = Convert.ToInt32(row.Cells["SentimentId"].Value);
                selectedIds.Add(sentimentId);
            }


            if (selectedIds.Count == 0)
            {
                MessageBox.Show("Please select at least one sentiment to delete.",
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Delete {selectedIds.Count} selected sentiment(s)?",
                                "Confirm",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SentimentService.DeleteSentiments(selectedIds);
                LoadSentiments(); // refresh table
            }
        }


        // private void btnDeleteSentiments_Click(object sender, EventArgs e)
        // {
        //     var selectedSentiments = new List<SentimentDto>();

        //     foreach (DataGridViewRow row in dgvReviews.SelectedRows)
        //     {
        //         if (row.DataBoundItem is SentimentDto sentiment)
        //         {
        //             selectedSentiments.Add(sentiment);
        //         }
        //     }

        //     if (selectedSentiments.Count == 0)
        //     {
        //         MessageBox.Show("Please select at least one sentiment to delete.",
        //             "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //         return;
        //     }

        //     if (MessageBox.Show($"Delete {selectedSentiments.Count} selected sentiment(s)?",
        //                         "Confirm",
        //                         MessageBoxButtons.YesNo,
        //                         MessageBoxIcon.Question) == DialogResult.Yes)
        //     {
        //         var ids = selectedSentiments.Select(s => s.SentimentId).ToList();
        //         SentimentService.DeleteSentiments(ids);

        //         LoadSentiments(); // refresh table
        //     }
        // }


        // private List<SentimentDto> GetSelectedSentiments()
        // {
        //     var selected = new List<SentimentDto>();

        //     foreach (DataGridViewRow row in dgvReviews.SelectedRows)
        //     {
        //         if (row.DataBoundItem is SentimentDto sentiment)
        //         {
        //             selected.Add(sentiment);
        //         }
        //     }

        //     return selected;
        // }




    }
}
