using System.Data;
using Microsoft.Data.SqlClient;

namespace it13Project.Data
{

    internal class ReportsService
    {
        private readonly DateTime? from;
        private readonly DateTime? to;
        private readonly string template;
        private readonly int? gameId;   // 👈 add gameId

        public ReportsService(DateTime? from, DateTime? to, string template, int? gameId = null)
        {
            this.from = from;
            this.to = to;
            this.template = template;
            this.gameId = gameId;
        }

        private SqlParameter[] GetDateParameters()
        {
            var parameters = new List<SqlParameter>();
            if (from.HasValue)
                parameters.Add(new SqlParameter("@From", from.Value));
            if (to.HasValue)
                parameters.Add(new SqlParameter("@To", to.Value));
            if (gameId.HasValue)
                parameters.Add(new SqlParameter("@GameId", gameId.Value));
            return parameters.ToArray();
        }

        private string GetDateWhere(string column)
        {
            var conditions = new List<string>();
            if (from.HasValue)
                conditions.Add($"{column} >= @From");
            if (to.HasValue)
                conditions.Add($"{column} <= @To");
            if (gameId.HasValue)
                conditions.Add("g.game_id = @GameId");
            return conditions.Count > 0 ? "WHERE " + string.Join(" AND ", conditions) : "";
        }

        private string GetDateGrouping(string column)
        {
            return template switch
            {
                "Weekly" => $"DATEPART(YEAR, {column}), DATEPART(WEEK, {column})",
                "Monthly" => $"FORMAT({column}, 'MMM yyyy')",
                "Quarterly" => $"DATEPART(YEAR, {column}), DATEPART(QUARTER, {column})",
                _ => $"FORMAT({column}, 'MMM yyyy')" // default Monthly
            };
        }

        private string GetDateLabel(string column)
        {
            return template switch
            {
                "Weekly" => $"CONCAT('W', DATEPART(WEEK, {column}), '-', DATEPART(YEAR, {column}))",
                "Monthly" => $"FORMAT({column}, 'MMM yyyy')",
                "Quarterly" => $"CONCAT('Q', DATEPART(QUARTER, {column}), '-', DATEPART(YEAR, {column}))",
                _ => $"FORMAT({column}, 'MMM yyyy')"
            };
        }

        private string GetDateTimeframe(string column)  // New 
        {
            return template switch
            {
                "Weekly" => $"DATEADD(DAY, -((DATEPART(WEEKDAY, {column}) - 1)), CAST({column} AS DATE))",  // start of week (Sunday)
                "Monthly" => $"DATEFROMPARTS(YEAR({column}), MONTH({column}), 1)",
                "Quarterly" => $"DATEFROMPARTS(YEAR({column}), ((DATEPART(QUARTER, {column}) - 1) * 3) + 1, 1)",
                _ => $"DATEFROMPARTS(YEAR({column}), MONTH({column}), 1)"
            };
        }


        // === 1. Sentiment Trend ===
        public (string[] Labels, int[] Positive, int[] Neutral, int[] Negative) GetSentimentTrend()
        {
            string groupBy = GetDateGrouping("r.review_date");
            string label = GetDateLabel("r.review_date");
            string timeframe = GetDateTimeframe("r.review_date"); 
            
            string query = $@"
                SELECT 
                    {label} AS Period,
                    {timeframe} AS PeriodStart,
                    SUM(CASE WHEN s.predicted_sentiment = 'Positive' THEN s.confidence_score ELSE 0 END) AS Positive,
                    SUM(CASE WHEN s.predicted_sentiment = 'Neutral'  THEN s.confidence_score ELSE 0 END) AS Neutral,
                    SUM(CASE WHEN s.predicted_sentiment = 'Negative' THEN s.confidence_score ELSE 0 END) AS Negative
                FROM reviews r
                INNER JOIN games g ON r.game_id = g.game_id
                INNER JOIN reviewsentiment s ON r.review_id = s.review_id
                {GetDateWhere("r.review_date")}
                GROUP BY {groupBy}, {timeframe}
                ORDER BY {timeframe}";

            var dt = DatabaseHelper.ExecuteQuery(query, GetDateParameters());

            return (
                dt.AsEnumerable().Select(r => r.Field<string>("Period")).ToArray(),
                dt.AsEnumerable().Select(r => Convert.ToInt32(r["Positive"])).ToArray(),
                dt.AsEnumerable().Select(r => Convert.ToInt32(r["Neutral"])).ToArray(),
                dt.AsEnumerable().Select(r => Convert.ToInt32(r["Negative"])).ToArray()
            );
        }



        // === 2. Top Games ===
        public (string[] Games, double[] Recommended, double[] NotRecommended) GetTopGames()
        {
            string query = $@"
                SELECT TOP 10 g.app_name,
                       SUM(CASE WHEN r.review_recommendation = 1 THEN 1 ELSE 0 END) AS Recommended,
                       SUM(CASE WHEN r.review_recommendation = 0 THEN 1 ELSE 0 END) AS NotRecommended
                FROM reviews r
                INNER JOIN games g ON r.game_id = g.game_id
                {GetDateWhere("r.review_date")}
                GROUP BY g.app_name
                ORDER BY Recommended DESC";

            var dt = DatabaseHelper.ExecuteQuery(query, GetDateParameters());
            return (
                dt.AsEnumerable().Select(r => r.Field<string>("app_name")).ToArray(),
                dt.AsEnumerable().Select(r => Convert.ToDouble(r["Recommended"])).ToArray(),
                dt.AsEnumerable().Select(r => Convert.ToDouble(r["NotRecommended"])).ToArray()
            );
        }

        // === 3. Recommendation Breakdown ===
        public (string[] Labels, double[] Values) GetRecommendationBreakdown()
        {
            string query = $@"
                    SELECT 
                        CASE 
                            WHEN r.review_recommendation = 1 THEN 'Recommended'
                            WHEN r.review_recommendation = 0 THEN 'Not Recommended'
                            WHEN r.review_recommendation IS NULL AND r.review_score_numeric > 4.0 THEN 'Recommended'
                            ELSE 'Not Recommended'
                        END AS Label,
                        COUNT(*) AS Count
                    FROM reviews r
                    INNER JOIN games g ON r.game_id = g.game_id
                    {GetDateWhere("r.review_date")}
                    GROUP BY 
                        CASE 
                            WHEN r.review_recommendation = 1 THEN 'Recommended'
                            WHEN r.review_recommendation = 0 THEN 'Not Recommended'
                            WHEN r.review_recommendation IS NULL AND r.review_score_numeric > 4.0 THEN 'Recommended'
                            ELSE 'Not Recommended'
                        END
                    ";

            var dt = DatabaseHelper.ExecuteQuery(query, GetDateParameters());
            return (
                dt.AsEnumerable().Select(r => r.Field<string>("Label")).ToArray(),
                dt.AsEnumerable().Select(r => Convert.ToDouble(r["Count"])).ToArray()
            );
        }

        // === 4. Genre Sentiment Heatmap ===
        public (string[] Genres, double[,] Intensities) GetGenreSentiment()
        {
            string query = $@"
                SELECT TOP 10 g.genre,
                       SUM(CASE WHEN s.predicted_sentiment = 'Positive' THEN 1 ELSE 0 END) AS Positive,
                       SUM(CASE WHEN s.predicted_sentiment = 'Neutral' THEN 1 ELSE 0 END) AS Neutral,
                       SUM(CASE WHEN s.predicted_sentiment = 'Negative' THEN 1 ELSE 0 END) AS Negative
                FROM reviews r
                INNER JOIN games g ON r.game_id = g.game_id
                INNER JOIN reviewsentiment s ON r.review_id = s.review_id
                {GetDateWhere("r.review_date")}
                GROUP BY g.genre
                ORDER BY  Positive DESC";

            var dt = DatabaseHelper.ExecuteQuery(query, GetDateParameters());

            string[] genres = dt.AsEnumerable().Select(r => r.Field<string>("genre")).ToArray();
            double[,] intensities = new double[dt.Rows.Count, 3];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                intensities[i, 0] = Convert.ToDouble(dt.Rows[i]["Positive"]);
                intensities[i, 1] = Convert.ToDouble(dt.Rows[i]["Neutral"]);
                intensities[i, 2] = Convert.ToDouble(dt.Rows[i]["Negative"]);
            }

            return (genres, intensities);
        }

        // === 5. Controversial Games ===
        public DataTable GetControversialGames()
        {
            // string query = $@"
                // SELECT g.app_name AS Game,
                //        SUM(CASE WHEN s.predicted_sentiment = 'Positive' THEN 1 ELSE 0 END) AS Positive,
                //        SUM(CASE WHEN s.predicted_sentiment = 'Negative' THEN 1 ELSE 0 END) AS Negative
                // FROM reviews r
                // INNER JOIN games g ON r.game_id = g.game_id
                // INNER JOIN reviewsentiment s ON r.review_id = s.review_id
                // {GetDateWhere("r.review_date")}
                // GROUP BY g.app_name
                // HAVING SUM(CASE WHEN s.predicted_sentiment = 'Positive' THEN 1 ELSE 0 END) > 0
                //    AND SUM(CASE WHEN s.predicted_sentiment = 'Negative' THEN 1 ELSE 0 END) > 0";

            string query = $@"
            WITH GameSentiment AS (
                SELECT
                    g.app_name AS Game,
                    SUM(CASE WHEN s.predicted_sentiment = 'Positive' THEN 1 ELSE 0 END) AS Positive,
                    SUM(CASE WHEN s.predicted_sentiment = 'Negative' THEN 1 ELSE 0 END) AS Negative,
                    COUNT(*) AS TotalReviews,
                    CAST(
                        1.0 * SUM(CASE WHEN s.predicted_sentiment = 'Positive' THEN 1 ELSE 0 END) /
                        NULLIF(COUNT(*), 0)
                        AS DECIMAL(10,4)
                    ) AS PositiveShare
                FROM reviews r
                INNER JOIN games g ON r.game_id = g.game_id
                INNER JOIN reviewsentiment s ON r.review_id = s.review_id
                {GetDateWhere("r.review_date")}
                GROUP BY g.app_name
                HAVING COUNT(*) > 50
            )
            SELECT TOP 20
                Game,
                Positive,
                Negative,
                TotalReviews,
                PositiveShare,
                ABS(PositiveShare - 0.5) AS ControversyDistance,
                (1 - 2 * ABS(PositiveShare - 0.5)) AS ControversyScore  -- 1 = maximally controversial (50/50), 0 = not controversial
            FROM GameSentiment
            ORDER BY ControversyDistance ASC;  -- closest to 50/50 first (most controversial)
            ";

            return DatabaseHelper.ExecuteQuery(query, GetDateParameters());
        }

        // === 6. Low Confidence Reviews ===
        public DataTable GetLowConfidenceReviews()
        {
            string query = $@"
                SELECT top 20 r.review_id AS ReviewID,
                       g.app_name AS Game,
                       s.predicted_sentiment AS Sentiment,
                       r.review_text AS Review,
                       s.confidence_score AS Confidence
                FROM reviews r
                INNER JOIN games g ON r.game_id = g.game_id
                INNER JOIN reviewsentiment s ON r.review_id = s.review_id
                {GetDateWhere("r.review_date")}
                AND s.confidence_score < 0.5
                ORDER BY s.confidence_score ASC";

            return DatabaseHelper.ExecuteQuery(query, GetDateParameters());
        }
        

        public List<Game> GetAllGames()
        {
            string query = "SELECT game_id, app_name FROM games ORDER BY app_name";

            var dt = DatabaseHelper.ExecuteQuery(query);

            return dt.AsEnumerable()
                .Select(r => new Game
                {
                    GameId = r.Field<int>("game_id"),
                    AppName = r.Field<string>("app_name")
                })
                .ToList();
        }
    }



}