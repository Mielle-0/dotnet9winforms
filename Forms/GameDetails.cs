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
    public partial class GameDetails : Form
    {
        private readonly int _gameId;

        public GameDetails(int gameId)
        {
            InitializeComponent();
            _gameId = gameId;
            LoadGameDetails();
        }

        private void LoadGameDetails()
        {
            string query = @"
        SELECT game_id, app_name, genre, release_date, developer, publisher, description
        FROM Games
        WHERE game_id = @id";

            var dt = DatabaseHelper.ExecuteQuery(query, new SqlParameter("@id", _gameId));

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Game not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            var row = dt.Rows[0];

            lblTitle.Text = row["app_name"]?.ToString();

            // Assign only the values, since static labels already say "Genre:", "Release:", etc.
            lblGenre.Text = row["genre"]?.ToString() ?? "N/A";
            lblRelease.Text = row["release_date"] != DBNull.Value
                ? Convert.ToDateTime(row["release_date"]).ToShortDateString()
                : "N/A";
            lblDeveloper.Text = row["developer"]?.ToString() ?? "N/A";
            lblPublisher.Text = row["publisher"]?.ToString() ?? "N/A";

            lblDescription.Text = row["description"]?.ToString() ?? "No description.";
        }


    }
}
