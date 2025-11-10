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
using it13Project.Forms;
using it13Project.Models;
using Newtonsoft.Json;

namespace it13Project.Pages
{
    public partial class ReviewPage : UserControl
    {
        private int currentPage = 1;
        private int pageSize = 30;
        private int totalRecords = 0;
        private int totalPages = 0;
        private readonly ReviewsService _reviewsService;
        public ReviewPage()
        {
            _reviewsService = new ReviewsService();
            InitializeComponent();
            // LoadReviewsPaged();
            btnRefresh.PerformClick();
        }

        private void LoadReviewsPaged()
        {
            try
            {
                string search = txtSearch.Text.Trim();
                string sentiment = cboSentiment.SelectedItem?.ToString() ?? "All";
                DateTime? from = dtFrom.Checked ? dtFrom.Value.Date : (DateTime?)null;
                DateTime? to = dtTo.Checked ? dtTo.Value.Date : (DateTime?)null;

                var pagedResult = _reviewsService.GetReviewsPaged(currentPage, pageSize, search, sentiment, from, to);

                totalRecords = pagedResult.TotalCount;
                totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

                dgvReviews.DataSource = null;
                dgvReviews.Columns.Clear();

                dgvReviews.DataSource = pagedResult.Items;

                // Hide game_id / review_id but keep them accessible
                if (!dgvReviews.Columns.Contains("GameId"))
                    dgvReviews.Columns.Insert(0, new DataGridViewTextBoxColumn { Name = "GameId", DataPropertyName = "GameId", Visible = false });

                if (!dgvReviews.Columns.Contains("ReviewId"))
                    dgvReviews.Columns.Insert(1, new DataGridViewTextBoxColumn { Name = "ReviewId", DataPropertyName = "ReviewId", Visible = false });

                // Format columns
                dgvReviews.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                if (dgvReviews.Columns["ReviewDate"] != null)
                    dgvReviews.Columns["ReviewDate"].DefaultCellStyle.Format = "yyyy-MM-dd";
                if (dgvReviews.Columns["Confidence"] != null)
                    dgvReviews.Columns["Confidence"].DefaultCellStyle.Format = "P2";

                lblPageInfo.Text = $"Page {currentPage} of {totalPages} ({totalRecords} reviews)";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading reviews: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<ReviewDisplay> GetSelectedReviews()
        {
            var selected = new List<ReviewDisplay>();

            foreach (DataGridViewRow row in dgvReviews.SelectedRows)
                if (row.DataBoundItem is ReviewDisplay review)
                    selected.Add(review);

            return selected;
        }

        private void btnApplyFilters_Click(object sender, EventArgs e)
        {
            LoadReviewsPaged();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cboSentiment.SelectedIndex = 0; // assuming "All" is index 0
            dtFrom.Checked = false;
            dtTo.Checked = false;
            LoadReviewsPaged();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var selectedReviews = GetSelectedReviews();

            if (selectedReviews.Count == 0)
            {
                MessageBox.Show("Please select at least one review to delete.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Archive {selectedReviews.Count} selected review(s)?",
                                "Confirm",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var ids = selectedReviews.Select(r => r.ReviewId).ToList();
                _reviewsService.ArchiveReviews(ids);

                LoadReviewsPaged();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                LoadReviewsPaged();
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadReviewsPaged();
            }
        }



        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var form = new ReviewAdd())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadReviewsPaged(); // your method to refresh grid
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvReviews.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a review to edit.");
                return;
            }

            // Get the bound ReviewDisplay object
            var review = dgvReviews.SelectedRows[0].DataBoundItem as ReviewDisplay;
            if (review == null) return;

            using (var form = new ReviewAdd(review.ReviewId))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadReviewsPaged();
                }
            }
        }

        private async void btnRunSentiment_Click(object sender, EventArgs e)
        {
            var selectedReviews = GetSelectedReviews();

            if (selectedReviews.Count == 0)
            {
                MessageBox.Show("Please select at least one review to analyze.",
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:8000/");

                    // Prepare request body
                    // var requestData = new
                    // {
                    //     reviews = selectedReviews.Select(r => new
                    //     {
                    //         review_id = r.ReviewId,
                    //         text = r.ReviewText
                    //     }).ToList()
                    // };

                    // Option 1 XXXXXXX
                    // var requestData = selectedReviews.Select(r => r.ReviewText).ToList();


                    // Option 2
                    var requestData = selectedReviews.Select(r => new
                    {
                        review_id = r.ReviewId,
                        text = r.ReviewText
                    }).ToList();




                    string json = JsonConvert.SerializeObject(requestData);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");


                    // Send POST request
                    var response = await client.PostAsync("predict", content);
                    response.EnsureSuccessStatusCode();



                    // X
                    // // Parse response
                    // string responseJson = await response.Content.ReadAsStringAsync();
                    // var predictions = JsonConvert.DeserializeObject<List<SentimentPrediction>>(responseJson);

                    // Y
                    string responseJson = await response.Content.ReadAsStringAsync();
                    var predictions = JsonConvert.DeserializeObject<List<SentimentPrediction>>(responseJson);

                    if (predictions == null || predictions.Any(p => p.PredictedSentiment == null))
                    {
                        MessageBox.Show("Prediction response missing required fields.");
                        return;
                    }

                    // Save results to database
                    foreach (var pred in predictions)
                    {
                        SentimentService.InsertOrReplaceSentiment(pred.ReviewId, pred.PredictedSentiment, pred.Confidence);
                    }

                    MessageBox.Show($"Successfully analyzed {predictions.Count} review(s).",
                        "Sentiment Analysis", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadReviewsPaged(); // refresh table
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error running sentiment analysis: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



    }
}
