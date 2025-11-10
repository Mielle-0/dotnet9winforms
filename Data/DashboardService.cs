using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace it13Project.Data
{
    internal static class DashboardService
    {
        /// <summary>
        /// Gets the total number of distinct games
        /// </summary>
        public static int GetTotalGames()
        {
            string query = "SELECT COUNT(DISTINCT app_id) FROM games";
            return Convert.ToInt32(DatabaseHelper.ExecuteScalar(query));
        }

        public static (int TotalGames, int ReviewedGames) GetReviewedTotalGames()
        {
            string query = @"
        SELECT 
            (SELECT COUNT(DISTINCT game_id) FROM Games) AS GamesNum,
            (SELECT COUNT(DISTINCT game_id) FROM Reviews) AS GamesReviewedNum";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);

            if (dt.Rows.Count > 0)
            {
                int totalGames = Convert.ToInt32(dt.Rows[0]["GamesNum"]);
                int reviewedGames = Convert.ToInt32(dt.Rows[0]["GamesReviewedNum"]);
                return (totalGames, reviewedGames);
            }
            return (0, 0);
        }



        /// <summary>
        /// Gets the total number of reviews
        /// </summary>
        public static int GetTotalReviews()
        {
            string query = "SELECT COUNT(*) FROM reviews";
            return Convert.ToInt32(DatabaseHelper.ExecuteScalar(query));
        }

        /// <summary>
        /// Gets the average sentiment (0–100%)
        /// </summary>
        public static decimal GetAverageSentiment()
        {
            string query = @"
            SELECT 
                CAST(AVG(
                    CASE predicted_sentiment
                        WHEN 'positive' THEN 1.0
                        WHEN 'neutral'  THEN 0.5
                        WHEN 'negative' THEN 0.0
                    END
                ) * 100 AS DECIMAL(5,2))
            FROM ReviewSentiment";

            object result = DatabaseHelper.ExecuteScalar(query);
            return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
        }

        /// <summary>
        /// Gets % of reviews that are recommended
        /// (Steam 'Recommended' + Amazon 4-5 stars count as recommended)
        /// </summary>
        public static decimal GetPercentRecommended()
        {
            string query = @"
        SELECT 
            CAST(
                SUM(CASE 
                        WHEN review_recommendation = 1 THEN 1
                        WHEN review_recommendation = 0 THEN 0
                        WHEN review_score_numeric >= 4 THEN 1
                        WHEN review_score_numeric <= 2 THEN 0
                        ELSE NULL
                    END
                ) * 100.0 / 
                COUNT(
                    CASE 
                        WHEN review_recommendation IS NOT NULL THEN 1
                        WHEN review_score_numeric BETWEEN 1 AND 5 THEN 1
                        ELSE NULL
                    END
                ) 
                AS DECIMAL(5,2)
            )
        FROM Reviews";

            object result = DatabaseHelper.ExecuteScalar(query);
            return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
        }


        public static int GetTotalReviewers()
        {
            string query = "SELECT COUNT(DISTINCT reviewer_id) FROM Reviews";
            return Convert.ToInt32(DatabaseHelper.ExecuteScalar(query));
        }










        /// <summary>
        /// Returns average sentiment score per day.
        /// -1 = negative, 0 = neutral, +1 = positive
        /// </summary>
        public static (List<DateTime> Dates, List<double> Scores) GetSentimentTrend()
        {
            string query = @"
        SELECT 
            CAST(DATEFROMPARTS(YEAR(r.review_date), MONTH(r.review_date), 1) AS DATE) AS ReviewDate,
            AVG(
                CASE 
                    WHEN rs.predicted_sentiment = 'positive' THEN rs.confidence_score
                    WHEN rs.predicted_sentiment = 'negative' THEN -rs.confidence_score
                    ELSE 0
                END
            ) AS SentimentScore
        FROM reviews r
        JOIN ReviewSentiment rs ON r.review_id = rs.review_id
        WHERE r.review_date IS NOT NULL
        GROUP BY YEAR(r.review_date), MONTH(r.review_date)
        ORDER BY ReviewDate;
        ";

            DataTable dt = DatabaseHelper.ExecuteQuery(query);

            var dates = new List<DateTime>();
            var scores = new List<double>();

            foreach (DataRow row in dt.Rows)
            {
                dates.Add(Convert.ToDateTime(row["ReviewDate"]));  // now works, since SQL returns a DATE
                scores.Add(Convert.ToDouble(row["SentimentScore"]));
            }

            return (dates, scores);
        }


        /// <summary>
        /// Returns review count per day.
        /// </summary>
        public static (List<DateTime> Dates, List<int> Counts) GetReviewVolume()
        {
            string query = @"
                SELECT 
                    CAST(review_date AS DATE) AS ReviewDate,
                    COUNT(*) AS ReviewCount
                FROM reviews
                WHERE review_date IS NOT NULL
                GROUP BY CAST(review_date AS DATE)
                ORDER BY ReviewDate;";

            DataTable dt = DatabaseHelper.ExecuteQuery(query);

            var dates = new List<DateTime>();
            var counts = new List<int>();

            foreach (DataRow row in dt.Rows)
            {
                dates.Add(Convert.ToDateTime(row["ReviewDate"]));
                counts.Add(Convert.ToInt32(row["ReviewCount"]));
            }

            return (dates, counts);
        }

        /// <summary>
        /// Returns top 5 games by review count.
        /// </summary>
        public static (List<string> GameNames, List<int> Counts) GetTop5Games()
        {
            string query = @"
                SELECT TOP 5 g.app_name, COUNT(*) AS ReviewCount
                FROM Games g
                JOIN Reviews r ON g.game_id = r.game_id
                JOIN ReviewSentiment rs ON r.review_id = rs.review_id
                WHERE rs.predicted_sentiment = 'positive'
                GROUP BY g.app_name
                ORDER BY ReviewCount DESC;";

            DataTable dt = DatabaseHelper.ExecuteQuery(query);

            var gameNames = new List<string>();
            var counts = new List<int>();

            foreach (DataRow row in dt.Rows)
            {
                gameNames.Add(row["app_name"].ToString());
                counts.Add(Convert.ToInt32(row["ReviewCount"]));
            }

            return (gameNames, counts);
        }
    }
}