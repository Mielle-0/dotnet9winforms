using System;
using System.Collections.Generic;
using System.Data;

namespace it13Project.Data
{
    internal class TrendService
    {
        // Class-level cache
        private Dictionary<string, (double pos, double neu, double neg)> monthlyData;
        private (int pos, int neu, int neg) totalData;

        /// <summary>
        /// Shared helper to assign sentiment values (positive, neutral, negative).
        /// </summary>
        private void AssignSentimentValue(string sentiment, double value,
            ref double pos, ref double neu, ref double neg)
        {
            switch (sentiment.ToLower())
            {
                case "positive": pos = value; break;
                case "neutral": neu = value; break;
                case "negative": neg = value; break;
            }
        }

        /// <summary>
        /// Loads sentiment data (both monthly trend and totals) in one query.
        /// Call this once when initializing the page.
        /// </summary>
        public void LoadSentimentData()
        {
            string sql = @"
                SELECT 
                    FORMAT(R.review_date, 'MMM') AS Month,
                    RS.predicted_sentiment,
                    AVG(RS.confidence_score) AS AvgConfidence,
                    COUNT(*) AS SentimentCount,
                    MIN(R.review_date) AS DateOrder
                FROM dbo.Reviews R
                JOIN dbo.ReviewSentiment RS ON R.review_id = RS.review_id
                WHERE R.review_date IS NOT NULL
                GROUP BY FORMAT(R.review_date, 'MMM'), RS.predicted_sentiment, YEAR(R.review_date), MONTH(R.review_date)
                ORDER BY DateOrder;";

            DataTable dt = DatabaseHelper.ExecuteQuery(sql);

            // Reset containers
            monthlyData = new Dictionary<string, (double pos, double neu, double neg)>();
            int totalPos = 0, totalNeu = 0, totalNeg = 0;

            foreach (DataRow row in dt.Rows)
            {
                string month = row["Month"].ToString();
                string sentiment = row["predicted_sentiment"].ToString();
                double avgConfidence = row["AvgConfidence"] != DBNull.Value ? Convert.ToDouble(row["AvgConfidence"]) : 0;
                int count = Convert.ToInt32(row["SentimentCount"]);

                if (!monthlyData.ContainsKey(month))
                    monthlyData[month] = (0, 0, 0);

                var current = monthlyData[month];
                AssignSentimentValue(sentiment, avgConfidence, ref current.pos, ref current.neu, ref current.neg);
                monthlyData[month] = current;

                // Update totals using counts
                switch (sentiment.ToLower())
                {
                    case "positive": totalPos += count; break;
                    case "neutral": totalNeu += count; break;
                    case "negative": totalNeg += count; break;
                }
            }

            totalData = (totalPos, totalNeu, totalNeg);
        }

        /// <summary>
        /// Gets KPI summary (total reviews, positive %, neutral %, negative %).
        /// </summary>
        public (int totalReviews, int positivePct, int neutralPct, int negativePct) GetKpiData()
        {
            string sql = @"
                SELECT RS.predicted_sentiment, COUNT(*) AS SentimentCount
                FROM dbo.ReviewSentiment RS
                JOIN dbo.Reviews R ON RS.review_id = R.review_id
                GROUP BY RS.predicted_sentiment;";

            DataTable dt = DatabaseHelper.ExecuteQuery(sql);

            int pos = 0, neu = 0, neg = 0;
            foreach (DataRow row in dt.Rows)
            {
                string sentiment = row["predicted_sentiment"].ToString().ToLower();
                int count = Convert.ToInt32(row["SentimentCount"]);

                switch (sentiment)
                {
                    case "positive": pos = count; break;
                    case "neutral": neu = count; break;
                    case "negative": neg = count; break;
                }
            }

            int total = pos + neu + neg;
            int posPct = total > 0 ? pos * 100 / total : 0;
            int neuPct = total > 0 ? neu * 100 / total : 0;
            int negPct = total > 0 ? neg * 100 / total : 0;

            return (total, posPct, neuPct, negPct);
        }


        /// <summary>
        /// Retrieves the most recent reviews with sentiment and confidence.
        /// </summary>
        public List<(string text, string sentiment, string confidence)> GetReviewFeed(int top = 20)
        {
            string sql = $@"
                SELECT TOP {top}
                    R.review_text,
                    RS.predicted_sentiment,
                    RS.confidence_score
                FROM dbo.Reviews R
                JOIN dbo.ReviewSentiment RS ON R.review_id = RS.review_id
                ORDER BY R.review_date DESC;";

            DataTable dt = DatabaseHelper.ExecuteQuery(sql);

            var reviews = new List<(string, string, string)>();

            foreach (DataRow row in dt.Rows)
            {
                string text = row["review_text"].ToString();
                string sentiment = row["predicted_sentiment"].ToString();
                string confidence = row["confidence_score"].ToString();
                reviews.Add((text, sentiment, confidence));
            }

            return reviews;
        }



        /// <summary>
        /// Returns data for trend chart (monthly confidence scores).
        /// </summary>
        public (string[] months, double[] positive, double[] neutral, double[] negative) GetTrendChartData()
        {
            var months = new List<string>();
            var pos = new List<double>();
            var neu = new List<double>();
            var neg = new List<double>();

            foreach (var kv in monthlyData)
            {
                months.Add(kv.Key);
                pos.Add(kv.Value.pos);
                neu.Add(kv.Value.neu);
                neg.Add(kv.Value.neg);
            }

            return (months.ToArray(), pos.ToArray(), neu.ToArray(), neg.ToArray());
        }


        /// <summary>
        /// Returns totals for pie chart (Positive, Neutral, Negative).
        /// </summary>
        public (int positive, int neutral, int negative) GetPieChartData()
        {
            return totalData;
        }


        /// <summary>
        /// Retrieves all Games for filtering
        /// </summary>
        public static List<GameInfo> GetAllGames()
        {
            string query = @"
                        SELECT DISTINCT game_id, app_name
                        FROM games
                        ORDER BY app_name;
                    ";

            DataTable dt = DatabaseHelper.ExecuteQuery(query);

            var games = new List<GameInfo>();
            foreach (DataRow row in dt.Rows)
            {
                games.Add(new GameInfo
                {
                    GameId = Convert.ToInt32(row["game_id"]),
                    Name = row["app_name"].ToString()
                });
            }

            return games;
        }

        /// <summary>
        /// Filters Charts using Listed Games and Date Time Picker
        /// </summary>
        // public void LoadSentimentDataFiltered(List<int> gameIds, DateTime? startDate, DateTime? endDate)
        // {
        //     // Build WHERE conditions
        //     var conditions = new List<string> { "R.review_date IS NOT NULL" };

        //     if (gameIds != null && gameIds.Any())
        //     {
        //         string ids = string.Join(",", gameIds);
        //         conditions.Add($"R.game_id IN ({ids})");
        //     }

        //     if (startDate.HasValue)
        //         conditions.Add($"R.review_date >= '{startDate:yyyy-MM-dd}'");

        //     if (endDate.HasValue)
        //         conditions.Add($"R.review_date <= '{endDate:yyyy-MM-dd}'");

        //     string whereClause = string.Join(" AND ", conditions);

        //     string sql = $@"
        // SELECT
        //     FORMAT(R.review_date, 'yyyy-MM') AS YearMonth,
        //     RS.predicted_sentiment,
        //     AVG(RS.confidence_score) AS AvgConfidence,
        //     COUNT(*) AS SentimentCount,
        //     MIN(R.review_date) AS DateOrder
        // FROM dbo.Reviews R
        // JOIN dbo.ReviewSentiment RS ON R.review_id = RS.review_id
        // WHERE {whereClause}
        // GROUP BY FORMAT(R.review_date, 'yyyy-MM'), RS.predicted_sentiment, YEAR(R.review_date), MONTH(R.review_date)
        // ORDER BY DateOrder;";

        //     DataTable dt = DatabaseHelper.ExecuteQuery(sql);

        //     // Reset containers
        //     monthlyData = new Dictionary<string, (double pos, double neu, double neg)>();
        //     int totalPos = 0, totalNeu = 0, totalNeg = 0;

        //     foreach (DataRow row in dt.Rows)
        //     {
        //         string yearMonth = row["YearMonth"].ToString();
        //         string sentiment = row["predicted_sentiment"].ToString();
        //         double avgConfidence = row["AvgConfidence"] != DBNull.Value ? Convert.ToDouble(row["AvgConfidence"]) : 0;
        //         int count = Convert.ToInt32(row["SentimentCount"]);

        //         if (!monthlyData.ContainsKey(yearMonth))
        //             monthlyData[yearMonth] = (0, 0, 0);

        //         var current = monthlyData[yearMonth];
        //         AssignSentimentValue(sentiment, avgConfidence, ref current.pos, ref current.neu, ref current.neg);
        //         monthlyData[yearMonth] = current;

        //         // Update totals using counts
        //         switch (sentiment.ToLower())
        //         {
        //             case "positive": totalPos += count; break;
        //             case "neutral": totalNeu += count; break;
        //             case "negative": totalNeg += count; break;
        //         }
        //     }

        //     totalData = (totalPos, totalNeu, totalNeg);
        // }
        public void LoadSentimentDataFiltered(List<int> gameIds, DateTime? startDate, DateTime? endDate)
        {
            // Build WHERE conditions
            var conditions = new List<string> { "R.review_date IS NOT NULL" };

            if (gameIds != null && gameIds.Any())
            {
                string ids = string.Join(",", gameIds);
                conditions.Add($"R.game_id IN ({ids})");
            }

            if (startDate.HasValue)
                conditions.Add($"R.review_date >= '{startDate:yyyy-MM-dd}'");

            if (endDate.HasValue)
                conditions.Add($"R.review_date <= '{endDate:yyyy-MM-dd}'");

            string whereClause = string.Join(" AND ", conditions);

            string sql = $@"
        SELECT
            FORMAT(R.review_date, 'MMM yyyy') AS YearMonth, -- ✅ Friendly label e.g. Jan 2025
            RS.predicted_sentiment,
            AVG(RS.confidence_score) AS AvgConfidence,
            COUNT(*) AS SentimentCount,
            MIN(R.review_date) AS DateOrder
        FROM dbo.Reviews R
        JOIN dbo.ReviewSentiment RS ON R.review_id = RS.review_id
        WHERE {whereClause}
        GROUP BY FORMAT(R.review_date, 'MMM yyyy'), RS.predicted_sentiment, YEAR(R.review_date), MONTH(R.review_date)
        ORDER BY DateOrder;";

            DataTable dt = DatabaseHelper.ExecuteQuery(sql);

            // Reset containers
            monthlyData = new Dictionary<string, (double pos, double neu, double neg)>();
            int totalPos = 0, totalNeu = 0, totalNeg = 0;

            foreach (DataRow row in dt.Rows)
            {
                string yearMonth = row["YearMonth"].ToString(); // already "Jan 2025"
                string sentiment = row["predicted_sentiment"].ToString();
                double avgConfidence = row["AvgConfidence"] != DBNull.Value ? Convert.ToDouble(row["AvgConfidence"]) : 0;
                int count = Convert.ToInt32(row["SentimentCount"]);

                if (!monthlyData.ContainsKey(yearMonth))
                    monthlyData[yearMonth] = (0, 0, 0);

                var current = monthlyData[yearMonth];
                AssignSentimentValue(sentiment, avgConfidence, ref current.pos, ref current.neu, ref current.neg);
                monthlyData[yearMonth] = current;

                // Update totals using counts
                switch (sentiment.ToLower())
                {
                    case "positive": totalPos += count; break;
                    case "neutral": totalNeu += count; break;
                    case "negative": totalNeg += count; break;
                }
            }

            totalData = (totalPos, totalNeu, totalNeg);
        }



    }

    public class GameInfo
    {
        public int GameId { get; set; }
        public string Name { get; set; }
        public override string ToString() => Name; // so ListBox displays the name
    }

}   