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
using it13Project.Forms;

namespace it13Project.Pages
{
    public partial class GameListPage : UserControl
    {
        Random rnd = new Random();
        List<int> randomIds = new List<int>();
        int maxId = 104044;
        int minId = 1;
        private int currentOffset = 0;
        private const int batchSize = 20;
        private string currentSearch;
        private int currentMinReviews;
        private DateTime? currentFrom;
        private DateTime? currentTo;

        public GameListPage()
        {
            InitializeComponent();
        }

        private void GameListControl_Load(object sender, EventArgs e)
        {
            var games = LoadRandomGames(50);
            LoadGames(games);
        }

        private void ShowGameDetails(int gameId)
        {
            using (var d = new GameDetails(gameId))
                d.ShowDialog(this);
        }

        private void EditGame(int gameId)
        {
            using (var d = new GameEdit(gameId))
            {
                if (d.ShowDialog(this) == DialogResult.OK)
                {
                    var games = LoadRandomGames(50);
                    LoadGames(games);
                }
            }
        }

        private void DeleteGame(int gameId)
        {
            if (MessageBox.Show(this, "Delete this game?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            // delete in DB (example)
            DatabaseHelper.ExecuteNonQuery(
                "DELETE FROM Reviews WHERE game_id = @id; DELETE FROM Games WHERE game_id = @id;",
                new SqlParameter("@id", gameId));

            // remove card from UI
            var cardToRemove = flpCards.Controls.Cast<Control>().FirstOrDefault(c => (int)c.Tag == gameId);
            if (cardToRemove != null) flpCards.Controls.Remove(cardToRemove);
        }

        private void btnApplyFilters_Click(object sender, System.EventArgs e)
        {
            // reset filters
            currentSearch = txtSearch.Text;
            currentMinReviews = (int)numReviews.Value;
            currentFrom = dtReleaseFrom.Checked ? dtReleaseFrom.Value.Date : (DateTime?)null;
            currentTo = dtReleaseTo.Checked ? dtReleaseTo.Value.Date : (DateTime?)null;
            currentOffset = 0;

            flpCards.Controls.Clear(); // reset the panel

            LoadNextBatch();
        }

        public void LoadGames(IEnumerable<GameData> games)
        {
            flpCards.Controls.Clear(); // remove existing cards

            foreach (var game in games)
            {

                var card = CreateGameSummaryCard(
                    gameId: game.GameId,
                    title: game.Title,
                    genre: game.Genre,
                    releaseDate: game.ReleaseDate,
                    avgSentiment: game.AvgSentiment,
                    reviewCount: game.ReviewCount
                );

                card.DoubleClick += (s, e) => ShowGameDetails((int)card.Tag);

                // attach right-click context menu
                var menu = new ContextMenuStrip();
                menu.Items.Add("Details", null, (s, e) => ShowGameDetails((int)card.Tag));
                menu.Items.Add("Edit", null, (s, e) => EditGame((int)card.Tag));
                menu.Items.Add("Delete", null, (s, e) =>
                {
                    DeleteGame((int)card.Tag);
                    var toRemove = flpCards.Controls.Cast<Control>().FirstOrDefault(c => (int)c.Tag == (int)card.Tag);
                    if (toRemove != null) flpCards.Controls.Remove(toRemove);
                });
                card.ContextMenuStrip = menu;

                // finally add to panel
                flpCards.Controls.Add(card);
            }
        }

        private IEnumerable<GameData> LoadRandomGames(int batchSize = 20)
        {
            var games = new List<GameData>();

            // 1️⃣ Get min and max game_id (all sources)
            string minMaxQuery = @"
            SELECT MIN(game_id) AS MinID, MAX(game_id) AS MaxID FROM Games
            ";
            var dtMinMax = DatabaseHelper.ExecuteQuery(minMaxQuery);

            if (dtMinMax.Rows.Count == 0)
                return games;

            int minId = Convert.ToInt32(dtMinMax.Rows[0]["MinID"]);
            int maxId = Convert.ToInt32(dtMinMax.Rows[0]["MaxID"]);

            // 2️⃣ Generate random IDs
            var random = new Random();
            var randomIds = new HashSet<int>();
            while (randomIds.Count < batchSize)
            {
                randomIds.Add(random.Next(minId, maxId + 1));
            }

            // 3️⃣ Build parameterized query for those IDs
            var idParameters = randomIds.Select((id, index) => new SqlParameter($"@id{index}", id)).ToArray();
            var inClause = string.Join(",", idParameters.Select(p => p.ParameterName));

            string query = $@"
                SELECT 
                    g.game_id,
                    g.app_name,
                    g.description,
                    g.genre,
                    g.release_date,
                    g.developer,
                    g.publisher,
                    COUNT(r.review_id) AS ReviewCount,
                    AVG(
                        CASE 
                            WHEN rs.predicted_sentiment = 'positive' THEN rs.confidence_score
                            WHEN rs.predicted_sentiment = 'negative' THEN -rs.confidence_score
                            ELSE 0
                        END
                    ) AS AvgSentiment
                FROM Games g
                LEFT JOIN Reviews r ON g.game_id = r.game_id
                LEFT JOIN ReviewSentiment rs ON r.review_id = rs.review_id
                WHERE g.game_id IN ({inClause})
                GROUP BY g.game_id, g.app_name, g.description, g.genre, g.release_date, g.developer, g.publisher;
            ";


            // 4️⃣ Execute query
            var dt = DatabaseHelper.ExecuteQuery(query, idParameters);

            // 5️⃣ Map to GameData
            foreach (DataRow row in dt.Rows)
            {
                games.Add(new GameData
                {
                    GameId = Convert.ToInt32(row["game_id"]),
                    Title = row["app_name"]?.ToString(),
                    Description = row["description"]?.ToString(),
                    Genre = row["genre"]?.ToString(),
                    ReleaseDate = row["release_date"] != DBNull.Value ? Convert.ToDateTime(row["release_date"]) : (DateTime?)null,
                    Developer = row["developer"]?.ToString(),
                    Publisher = row["publisher"]?.ToString(),
                    Image = null,
                    ReviewCount = row["ReviewCount"] != DBNull.Value ? Convert.ToInt32(row["ReviewCount"]) : 0,
                    AvgSentiment = row["AvgSentiment"] != DBNull.Value ? Convert.ToDouble(row["AvgSentiment"]) : (double?)null
                });
            }

            return games;
        }


        private IEnumerable<GameData> LoadFilteredGames(
            string search,
            int minReviews,
            DateTime? from,
            DateTime? to,
            int offset,
            int batchSize)
        {
            var games = new List<GameData>();

            string query = @"
                SELECT 
                    g.game_id,
                    g.app_name,
                    g.description,
                    g.genre,
                    g.release_date,
                    g.developer,
                    g.publisher,
                    COUNT(r.review_id) AS ReviewCount,
                    AVG(
                        CASE 
                            WHEN rs.predicted_sentiment = 'positive' THEN rs.confidence_score
                            WHEN rs.predicted_sentiment = 'negative' THEN -rs.confidence_score
                            ELSE 0
                        END
                    ) AS AvgSentiment
                FROM Games g
                LEFT JOIN Reviews r ON g.game_id = r.game_id
                LEFT JOIN ReviewSentiment rs ON r.review_id = rs.review_id
                WHERE 1=1
                    AND (
                        (@search IS NULL OR @search = '')
                        OR g.app_name LIKE '%' + @search + '%'
                        OR g.publisher LIKE '%' + @search + '%'
                        OR g.developer LIKE '%' + @search + '%'
                    )
                    AND (@from IS NULL OR g.release_date >= @from)
                    AND (@to IS NULL OR g.release_date <= @to)
                GROUP BY g.game_id, g.app_name, g.description, g.genre, g.release_date, g.developer, g.publisher
                HAVING COUNT(r.review_id) >= @minReviews
                ORDER BY g.app_name
                OFFSET @offset ROWS FETCH NEXT @batchSize ROWS ONLY;
            ";

            var parameters = new[]
            {
                new SqlParameter("@search", string.IsNullOrWhiteSpace(search) ? (object)DBNull.Value : search),
                new SqlParameter("@from", from.HasValue ? (object)from.Value.Date : DBNull.Value),
                new SqlParameter("@to", to.HasValue ? (object)to.Value.Date : DBNull.Value),
                new SqlParameter("@minReviews", minReviews),
                new SqlParameter("@offset", offset),
                new SqlParameter("@batchSize", batchSize)
            };

            var dt = DatabaseHelper.ExecuteQuery(query, parameters);

            foreach (DataRow row in dt.Rows)
            {
                games.Add(new GameData
                {
                    GameId = Convert.ToInt32(row["game_id"]),
                    Title = row["app_name"]?.ToString(),
                    Description = row["description"]?.ToString(),
                    Genre = row["genre"]?.ToString(),
                    ReleaseDate = row["release_date"] != DBNull.Value ? Convert.ToDateTime(row["release_date"]) : (DateTime?)null,
                    Developer = row["developer"]?.ToString(),
                    Publisher = row["publisher"]?.ToString(),
                    Image = null,
                    ReviewCount = row["ReviewCount"] != DBNull.Value ? Convert.ToInt32(row["ReviewCount"]) : 0,
                    AvgSentiment = row["AvgSentiment"] != DBNull.Value ? Convert.ToDouble(row["AvgSentiment"]) : (double?)null
                });
            }

            return games;
        }

        private void LoadNextBatch()
        {
            var games = LoadFilteredGames(
                currentSearch,
                currentMinReviews,
                currentFrom,
                currentTo,
                currentOffset,
                batchSize
            );

            LoadGames(games); // your existing method to add cards
            currentOffset += batchSize;
        }


    }

    public class GameData
    {
        public int GameId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public Image? Image { get; set; }
        public string? Genre { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? Developer { get; set; }
        public string? Publisher { get; set; }
        public int ReviewCount { get; set; }
        public double? AvgSentiment { get; set; }
    }

}
