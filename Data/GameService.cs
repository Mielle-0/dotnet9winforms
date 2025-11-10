using System.Data;
using Microsoft.Data.SqlClient;

namespace it13Project.Data
{

    internal class Game
    {
        public int GameId { get; set; }
        public string? AppId { get; set; }
        public string? AppName { get; set; }
        public string? Genre { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? Developer { get; set; }
        public string? Publisher { get; set; }
        public string? Description { get; set; }
        public string? Source { get; set; }
    }

    internal static class GameService
    {
        /// <summary>
        /// Get all games
        /// </summary>
        public static List<Game> GetAllGames()
        {
            string query = "SELECT * FROM games ORDER BY app_name";
            var dt = DatabaseHelper.ExecuteQuery(query);

            var list = new List<Game>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(MapGame(row));
            }
            return list;
        }

        /// <summary>
        /// Get game by ID
        /// </summary>
        public static Game GetGameById(int gameId)
        {
            string query = "SELECT * FROM Games WHERE game_id = @id";
            var dt = DatabaseHelper.ExecuteQuery(query,
                new SqlParameter("@id", gameId));

            if (dt.Rows.Count == 0)
                return null;

            return MapGame(dt.Rows[0]);
        }

        /// <summary>
        /// Insert a new game and return new ID
        /// </summary>
        public static int AddGame(Game game)
        {
            string query = @"
                INSERT INTO Games (app_id, app_name, genre, release_date, developer, publisher, description, source)
                VALUES (@app_id, @app_name, @genre, @release_date, @developer, @publisher, @description, @source);
                SELECT SCOPE_IDENTITY();";

            object result = DatabaseHelper.ExecuteScalar(query,
                new SqlParameter("@app_id", game.AppId ?? (object)DBNull.Value),
                new SqlParameter("@app_name", game.AppName),
                new SqlParameter("@genre", (object)game.Genre ?? DBNull.Value),
                new SqlParameter("@release_date", (object)game.ReleaseDate ?? DBNull.Value),
                new SqlParameter("@developer", (object)game.Developer ?? DBNull.Value),
                new SqlParameter("@publisher", (object)game.Publisher ?? DBNull.Value),
                new SqlParameter("@description", (object)game.Description ?? DBNull.Value),
                new SqlParameter("@source", (object)game.Source ?? DBNull.Value)
            );

            return Convert.ToInt32(result);
        }

        /// <summary>
        /// Update an existing game
        /// </summary>
        public static bool UpdateGame(Game game)
        {
            string query = @"
                UPDATE Games
                SET app_id = @app_id,
                    app_name = @app_name,
                    genre = @genre,
                    release_date = @release_date,
                    developer = @developer,
                    publisher = @publisher,
                    description = @description,
                    source = @source
                WHERE game_id = @id";

            int rows = DatabaseHelper.ExecuteNonQuery(query,
                new SqlParameter("@id", game.GameId),
                new SqlParameter("@app_id", game.AppId ?? (object)DBNull.Value),
                new SqlParameter("@app_name", game.AppName),
                new SqlParameter("@genre", (object)game.Genre ?? DBNull.Value),
                new SqlParameter("@release_date", (object)game.ReleaseDate ?? DBNull.Value),
                new SqlParameter("@developer", (object)game.Developer ?? DBNull.Value),
                new SqlParameter("@publisher", (object)game.Publisher ?? DBNull.Value),
                new SqlParameter("@description", (object)game.Description ?? DBNull.Value),
                new SqlParameter("@source", (object)game.Source ?? DBNull.Value)
            );

            return rows > 0;
        }

        /// <summary>
        /// Delete a game
        /// </summary>
        public static bool DeleteGame(int gameId)
        {
            string query = "DELETE FROM Games WHERE game_id = @id";
            int rows = DatabaseHelper.ExecuteNonQuery(query,
                new SqlParameter("@id", gameId));

            return rows > 0;
        }

        /// <summary>
        /// Helper to map DataRow to Game object
        /// </summary>
        private static Game MapGame(DataRow row)
        {
            return new Game
            {
                GameId = Convert.ToInt32(row["game_id"]),
                AppId = row["app_id"].ToString(),
                AppName = row["app_name"].ToString(),
                Genre = row["genre"] == DBNull.Value ? null : row["genre"].ToString(),
                ReleaseDate = row["release_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["release_date"]),
                Developer = row["developer"] == DBNull.Value ? null : row["developer"].ToString(),
                Publisher = row["publisher"] == DBNull.Value ? null : row["publisher"].ToString(),
                Description = row["description"] == DBNull.Value ? null : row["description"].ToString(),
                Source = row["source"] == DBNull.Value ? null : row["source"].ToString()
            };
        }
    }
}