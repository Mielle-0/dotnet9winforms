using Microsoft.Data.SqlClient;
using it13Project.Models;
using System.Data;

namespace it13Project.Data
{
    internal class ReviewsService
    {

        public PagedResult<ReviewDisplay> GetReviewsPaged(
        int pageNumber, int pageSize,
        string? search = null, string sentiment = "All",
        DateTime? from = null, DateTime? to = null)
        {
            var result = new PagedResult<ReviewDisplay> { Items = new List<ReviewDisplay>() };

            string baseQuery = @"
            FROM reviews r
            INNER JOIN games a ON r.game_id = a.game_id
            LEFT JOIN reviewsentiment s ON r.review_id = s.review_id
            WHERE (1=1)";

            var parameters = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(search))
            {
                baseQuery += " AND (a.app_name LIKE @search OR r.review_text LIKE @search)";
                parameters.Add(new SqlParameter("@search", $"%{search}%"));
            }

            if (!string.IsNullOrEmpty(sentiment) && sentiment != "All")
            {
                baseQuery += " AND s.predicted_sentiment = @sentiment";
                parameters.Add(new SqlParameter("@sentiment", sentiment));
            }

            if (from.HasValue)
            {
                baseQuery += " AND r.review_date >= @from";
                parameters.Add(new SqlParameter("@from", from.Value));
            }

            if (to.HasValue)
            {
                baseQuery += " AND r.review_date <= @to";
                parameters.Add(new SqlParameter("@to", to.Value));
            }

            // Count total
            // string countQuery = "SELECT COUNT(*) " + baseQuery;
            // result.TotalCount = Convert.ToInt32(DatabaseHelper.ExecuteScalar(countQuery, parameters.ToArray()));
            // result.TotalPages = (int)Math.Ceiling(result.TotalCount / (double)pageSize);


            // parameters.Add(new SqlParameter("@offset", (pageNumber - 1) * pageSize));
            // parameters.Add(new SqlParameter("@pageSize", pageSize));


            // string pageQuery = $@"
            //     SELECT 
            //         r.game_id,
            //         a.app_name,
            //         r.review_text,
            //         r.review_score_numeric,
            //         r.review_recommendation,
            //         s.predicted_sentiment,
            //         s.confidence_score,
            //         r.review_date,
            //         r.review_id
            //     {baseQuery}
            //     ORDER BY r.review_date DESC
            //     OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY";

            // DataTable dt = DatabaseHelper.ExecuteQuery(pageQuery, pageParams.ToArray());



// Count total
string countQuery = "SELECT COUNT(*) " + baseQuery;
var countParams = parameters
    .Select(p => new SqlParameter(p.ParameterName, p.Value))
    .ToArray();

result.TotalCount = Convert.ToInt32(DatabaseHelper.ExecuteScalar(countQuery, countParams));
result.TotalPages = (int)Math.Ceiling(result.TotalCount / (double)pageSize);

// Build params for paged query
var pageParams = parameters
    .Select(p => new SqlParameter(p.ParameterName, p.Value))
    .ToList();

pageParams.Add(new SqlParameter("@offset", (pageNumber - 1) * pageSize));
pageParams.Add(new SqlParameter("@pageSize", pageSize));

string pageQuery = $@"
    SELECT 
        r.game_id,
        a.app_name,
        r.review_text,
        r.review_score_numeric,
        r.review_recommendation,
        s.predicted_sentiment,
        s.confidence_score,
        r.review_date,
        r.review_id
    {baseQuery}
    ORDER BY r.review_date DESC
    OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY";

DataTable dt = DatabaseHelper.ExecuteQuery(pageQuery, pageParams.ToArray());



            foreach (DataRow row in dt.Rows)
            {
                result.Items.Add(new ReviewDisplay
                {
                    ReviewId = Convert.ToInt32(row["review_id"]),
                    AppName = row["app_name"].ToString(),
                    ReviewText = row["review_text"].ToString(),
                    ReviewScore = row["review_score_numeric"] == DBNull.Value ? null : (decimal?)row["review_score_numeric"],
                    Recommendation = row["review_recommendation"] == DBNull.Value ? null : (bool?)row["review_recommendation"],
                    Sentiment = row["predicted_sentiment"]?.ToString(),
                    Confidence = row["confidence_score"] == DBNull.Value ? null : (decimal?)row["confidence_score"],
                    ReviewDate = row["review_date"] == DBNull.Value ? null : (DateTime?)row["review_date"]
                });
            }

            return result;
        }


        public void ArchiveReview(int reviewId)
        {
            string query = @"
                INSERT INTO archived_reviews (
                    original_review_id, review_text, review_votes, review_date,
                    reviewer_id, review_score_numeric, review_recommendation,
                    original_game_id, archived_at
                )
                SELECT review_id, review_text, review_votes, review_date,
                    reviewer_id, review_score_numeric, review_recommendation,
                    game_id, GETUTCDATE()
                FROM reviews
                WHERE review_id = @id;

                DELETE FROM reviews WHERE review_id = @id;
            ";

            DatabaseHelper.ExecuteNonQuery(query, new SqlParameter("@id", reviewId));
        }


        public void ArchiveReviews(IEnumerable<int> reviewIds)
        {
            if (reviewIds == null || !reviewIds.Any())
                return;

            // Convert IDs into a table-valued parameter or IN clause
            // For simplicity, we’ll use a temp table approach with IN clause

            string ids = string.Join(",", reviewIds);

            string query = $@"
                    INSERT INTO archived_reviews (
                        original_review_id, review_text, review_votes, review_date,
                        reviewer_id, review_score_numeric, review_recommendation,
                        original_game_id, archived_at
                    )
                    SELECT review_id, review_text, review_votes, review_date,
                        reviewer_id, review_score_numeric, review_recommendation,
                        game_id, GETUTCDATE()
                    FROM reviews
                    WHERE review_id IN ({ids});

                    DELETE FROM reviews WHERE review_id IN ({ids});
                ";

            DatabaseHelper.ExecuteNonQuery(query);
        }


        public int AddReview(string reviewText, int votes, DateTime reviewDate,
                             string reviewerId, decimal score, bool recommend, int gameId)
        {
            string query = @"
            INSERT INTO reviews 
                (review_text, review_votes, review_date, reviewer_id, review_score_numeric, review_recommendation, game_id)
            OUTPUT INSERTED.review_id
            VALUES (@text, @votes, @date, @reviewer, @score, @recommend, @game_id);";

            object result = DatabaseHelper.ExecuteScalar(query,
                new SqlParameter("@text", reviewText ?? (object)DBNull.Value),
                new SqlParameter("@votes", votes),
                new SqlParameter("@date", reviewDate),
                new SqlParameter("@reviewer", string.IsNullOrWhiteSpace(reviewerId) ? (object)DBNull.Value : reviewerId),
                new SqlParameter("@score", score),
                new SqlParameter("@recommend", recommend),
                new SqlParameter("@game_id", gameId));

            return Convert.ToInt32(result);
        }


        public void UpdateReview(int reviewId, string reviewText, int votes, DateTime reviewDate,
                                 string reviewerId, decimal score, bool recommend, int gameId)
        {
            string query = @"
            UPDATE reviews
            SET review_text = @text,
                review_votes = @votes,
                review_date = @date,
                reviewer_id = @reviewer,
                review_score_numeric = @score,
                review_recommendation = @recommend,
                game_id = @game_id
            WHERE review_id = @id;";

            DatabaseHelper.ExecuteNonQuery(query,
                new SqlParameter("@text", reviewText ?? (object)DBNull.Value),
                new SqlParameter("@votes", votes),
                new SqlParameter("@date", reviewDate),
                new SqlParameter("@reviewer", string.IsNullOrWhiteSpace(reviewerId) ? (object)DBNull.Value : reviewerId),
                new SqlParameter("@score", score),
                new SqlParameter("@recommend", recommend),
                new SqlParameter("@game_id", gameId),
                new SqlParameter("@id", reviewId));
        }
    
    

    }


}