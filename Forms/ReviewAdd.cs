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
using Microsoft.Data.SqlClient;

namespace it13Project.Forms
{
    public partial class ReviewAdd : Form
    {
        private readonly ReviewsService _reviewsService = new ReviewsService();
        private readonly int? _editingReviewId;

        public ReviewAdd(int? reviewId = null)
        {
            InitializeComponent();
            _editingReviewId = reviewId;

            if (_editingReviewId.HasValue)
            {
                LoadReviewData(_editingReviewId.Value);
                this.Text = "Edit Review";
                btnSave.Text = "Update";
            }
            else
            {
                this.Text = "Add Review";
            }

            btnSave.Click += btnSave_Click;
            btnCancel.Click += (s, e) => this.Close();
        }

            // public ReviewAdd(ReviewDisplay review) : this()
            // {
            //     if (review == null) throw new ArgumentNullException(nameof(review));

            //     _editingReviewId = review.ReviewId;

            //     // Populate fields
            //     txtReviewText.Text = review.ReviewText;
            //     numScore.Value = review.ReviewScore ?? 0;
            //     chkRecommended.Checked = review.Recommendation ?? false;
            //     dtpReviewDate.Value = review.ReviewDate ?? DateTime.Now;

            //     // Game selection
            //     cmbGame.SelectedValue = review.GameId;

            //     // Optional fields (if you later add votes/reviewer ID)
            //     // numVotes.Value = review.ReviewVotes ?? 0;
            //     // txtReviewerId.Text = review.ReviewerId;

            //     this.Text = "Edit Review";
            //     btnSave.Text = "Update";
            // }

        /// <summary>
        /// Load data for editing
        /// </summary>
        private void LoadReviewData(int reviewId)
        {
            string query = "SELECT * FROM reviews WHERE review_id = @id";
            var dt = DatabaseHelper.ExecuteQuery(query, new SqlParameter("@id", reviewId));

            if (dt.Rows.Count == 1)
            {
                var row = dt.Rows[0];
                txtReviewText.Text = row["review_text"]?.ToString();
                numVotes.Value = row["review_votes"] == DBNull.Value ? 0 : Convert.ToDecimal(row["review_votes"]);
                dtpReviewDate.Value = row["review_date"] == DBNull.Value ? DateTime.Today : Convert.ToDateTime(row["review_date"]);
                txtReviewerId.Text = row["reviewer_id"]?.ToString();
                numScore.Value = row["review_score_numeric"] == DBNull.Value ? 0 : Convert.ToDecimal(row["review_score_numeric"]);
                chkRecommended.Checked = row["review_recommendation"] != DBNull.Value && (bool)row["review_recommendation"];
                cmbGame.SelectedValue = row["game_id"];
            }
        }

        /// <summary>
        /// Save or update the review
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbGame.SelectedValue == null)
            {
                MessageBox.Show("Please select a game.");
                return;
            }

            try
            {
                if (_editingReviewId.HasValue)
                {
                    // Update
                    _reviewsService.UpdateReview(
                        _editingReviewId.Value,
                        txtReviewText.Text,
                        (int)numVotes.Value,
                        dtpReviewDate.Value,
                        txtReviewerId.Text,
                        numScore.Value,
                        chkRecommended.Checked,
                        (int)cmbGame.SelectedValue
                    );

                    MessageBox.Show("Review updated successfully.");
                }
                else
                {
                    // Add
                    _reviewsService.AddReview(
                        txtReviewText.Text,
                        (int)numVotes.Value,
                        dtpReviewDate.Value,
                        txtReviewerId.Text,
                        numScore.Value,
                        chkRecommended.Checked,
                        (int)cmbGame.SelectedValue
                    );

                    MessageBox.Show("Review added successfully.");
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

    }
}
