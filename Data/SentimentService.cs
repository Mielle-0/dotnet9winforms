using System.Data;
using it13Project.Models;
using Microsoft.Data.SqlClient;

namespace it13Project.Data
{

    internal class SentimentService
    {
        /// <summary>
        /// Retrieves all sentiment records joined with game and review text
        /// </summary>
        public static DataTable GetAllSentiments()
        {
            string query = @"
                SELECT 
                    s.sentiment_id,
                    g.app_name, 
                    r.review_text, 
                    s.predicted_sentiment, 
                    s.confidence_score,
                    r.review_date
                FROM ReviewSentiment s
                INNER JOIN Reviews r ON s.review_id = r.review_id
                INNER JOIN Games g ON r.game_id = g.game_id
                ORDER BY r.review_date DESC";

            return DatabaseHelper.ExecuteQuery(query);
        }

        /// <summary>
        /// Retrieves filtered sentiment records (search, sentiment, date range)
        /// </summary>
        public static DataTable GetFilteredSentiments(string search, string sentiment, DateTime? from, DateTime? to)
        {
            string query = @"
                SELECT 
                    s.sentiment_id,
                    g.app_name, 
                    r.review_text, 
                    s.predicted_sentiment, 
                    s.confidence_score,
                    r.review_date
                FROM ReviewSentiment s
                INNER JOIN Reviews r ON s.review_id = r.review_id
                INNER JOIN Games g ON r.game_id = g.game_id
                WHERE 1=1 ";

            var parameters = new List<SqlParameter>();

            // Search filter (game or review text)
            if (!string.IsNullOrEmpty(search))
            {
                query += " AND (g.game_name LIKE @search OR r.review_text LIKE @search) ";
                parameters.Add(new SqlParameter("@search", "%" + search + "%"));
            }

            // Sentiment filter
            if (!string.IsNullOrEmpty(sentiment) && sentiment != "All")
            {
                query += " AND s.predicted_sentiment = @sentiment ";
                parameters.Add(new SqlParameter("@sentiment", sentiment));
            }

            // Date range
            if (from.HasValue)
            {
                query += " AND r.review_date >= @fromDate ";
                parameters.Add(new SqlParameter("@fromDate", from.Value));
            }

            if (to.HasValue)
            {
                query += " AND r.review_date <= @toDate ";
                parameters.Add(new SqlParameter("@toDate", to.Value));
            }

            query += " ORDER BY r.review_date DESC";

            return DatabaseHelper.ExecuteQuery(query, parameters.ToArray());
        }

        /// <summary>
        /// Retrieves sentiments with pagination
        /// </summary>
        public static List<SentimentDto> GetSentimentsPaged(int page, int pageSize, string search = null, string sentiment = null, DateTime? from = null, DateTime? to = null)
        {
            int offset = (page - 1) * pageSize;

            string query = @"
        SELECT 
            s.sentiment_id,
            g.app_name, 
            r.review_text, 
            s.predicted_sentiment, 
            s.confidence_score,
            r.review_date
        FROM ReviewSentiment s
        INNER JOIN Reviews r ON s.review_id = r.review_id
        INNER JOIN Games g ON r.game_id = g.game_id
        WHERE 1=1 ";

            var parameters = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(search))
            {
                query += " AND (g.game_name LIKE @search OR r.review_text LIKE @search) ";
                parameters.Add(new SqlParameter("@search", "%" + search + "%"));
            }

            if (!string.IsNullOrEmpty(sentiment) && sentiment != "All")
            {
                query += " AND s.predicted_sentiment = @sentiment ";
                parameters.Add(new SqlParameter("@sentiment", sentiment));
            }

            if (from.HasValue)
            {
                query += " AND r.review_date >= @fromDate ";
                parameters.Add(new SqlParameter("@fromDate", from.Value));
            }

            if (to.HasValue)
            {
                query += " AND r.review_date <= @toDate ";
                parameters.Add(new SqlParameter("@toDate", to.Value));
            }

            query += @"
        ORDER BY r.review_date DESC
        OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY";

            parameters.Add(new SqlParameter("@offset", offset));
            parameters.Add(new SqlParameter("@pageSize", pageSize));

            var dt = DatabaseHelper.ExecuteQuery(query, parameters.ToArray());
            var list = new List<SentimentDto>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new SentimentDto
                {
                    SentimentId = Convert.ToInt32(row["sentiment_id"]),
                    AppName = row["app_name"].ToString(),
                    ReviewText = row["review_text"].ToString(),
                    PredictedSentiment = row["predicted_sentiment"]?.ToString(),
                    ConfidenceScore = (decimal)(row["confidence_score"] != DBNull.Value ? Convert.ToDecimal(row["confidence_score"]) : (decimal?)null),
                    ReviewDate = (DateTime)(row["review_date"] != DBNull.Value ? Convert.ToDateTime(row["review_date"]) : (DateTime?)null)
                });
            }

            return list;
        }


        /// <summary>
        /// Gets total count (useful for pagination)
        /// </summary>
        public static int GetTotalCount(string search = null, string sentiment = null, DateTime? from = null, DateTime? to = null)
        {
            string query = @"
                SELECT COUNT(*)
                FROM ReviewSentiment s
                INNER JOIN Reviews r ON s.review_id = r.review_id
                INNER JOIN Games g ON r.game_id = g.game_id
                WHERE 1=1 ";

            var parameters = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(search))
            {
                query += " AND (g.game_name LIKE @search OR r.review_text LIKE @search) ";
                parameters.Add(new SqlParameter("@search", "%" + search + "%"));
            }

            if (!string.IsNullOrEmpty(sentiment) && sentiment != "All")
            {
                query += " AND s.predicted_sentiment = @sentiment ";
                parameters.Add(new SqlParameter("@sentiment", sentiment));
            }

            if (from.HasValue)
            {
                query += " AND r.review_date >= @fromDate ";
                parameters.Add(new SqlParameter("@fromDate", from.Value));
            }

            if (to.HasValue)
            {
                query += " AND r.review_date <= @toDate ";
                parameters.Add(new SqlParameter("@toDate", to.Value));
            }

            object result = DatabaseHelper.ExecuteScalar(query, parameters.ToArray());
            return Convert.ToInt32(result);
        }


        public static void DeleteSentiments(List<int> sentimentIds)
        {
            if (sentimentIds == null || sentimentIds.Count == 0) return;

            string query = $"DELETE FROM ReviewSentiment WHERE sentiment_id IN ({string.Join(",", sentimentIds)})";
            DatabaseHelper.ExecuteNonQuery(query);
        }





        public static void InsertOrReplaceSentiment(int reviewId, string predictedSentiment, decimal confidence)
        {
            string deleteQuery = "DELETE FROM ReviewSentiment WHERE review_id = @reviewId";
            DatabaseHelper.ExecuteNonQuery(deleteQuery, new SqlParameter("@reviewId", reviewId));

            string insertQuery = @"
                INSERT INTO ReviewSentiment (review_id, predicted_sentiment, confidence_score)
                VALUES (@reviewId, @predictedSentiment, @confidence)";
            
            DatabaseHelper.ExecuteNonQuery(insertQuery,
                new SqlParameter("@reviewId", reviewId),
                new SqlParameter("@predictedSentiment", predictedSentiment),
                new SqlParameter("@confidence", confidence));
        }


    }

}