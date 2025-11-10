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
    public partial class GameEdit : Form
    {
        private int _gameId;

        public GameEdit(int gameId)
        {
            InitializeComponent();
            _gameId = gameId;
            LoadGameForEdit();
        }

        private void LoadGameForEdit()
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

            txtTitle.Text = row["app_name"]?.ToString();
            txtGenre.Text = row["genre"]?.ToString();
            dtRelease.Value = row["release_date"] != DBNull.Value
                                    ? Convert.ToDateTime(row["release_date"])
                                    : DateTime.Today;
            dtRelease.Checked = row["release_date"] != DBNull.Value;

            txtDeveloper.Text = row["developer"]?.ToString();
            txtPublisher.Text = row["publisher"]?.ToString();
            txtDescription.Text = row["description"]?.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string query = @"
        UPDATE Games
        SET app_name = @name,
            genre = @genre,
            release_date = @release,
            developer = @developer,
            publisher = @publisher,
            description = @description
        WHERE game_id = @id";

            int rows = DatabaseHelper.ExecuteNonQuery(query,
                new SqlParameter("@id", _gameId),
                new SqlParameter("@name", txtTitle.Text),
                new SqlParameter("@genre", string.IsNullOrWhiteSpace(txtGenre.Text) ? DBNull.Value : (object)txtGenre.Text),
                new SqlParameter("@release", dtRelease.Checked ? (object)dtRelease.Value : DBNull.Value),
                new SqlParameter("@developer", string.IsNullOrWhiteSpace(txtDeveloper.Text) ? DBNull.Value : (object)txtDeveloper.Text),
                new SqlParameter("@publisher", string.IsNullOrWhiteSpace(txtPublisher.Text) ? DBNull.Value : (object)txtPublisher.Text),
                new SqlParameter("@description", string.IsNullOrWhiteSpace(txtDescription.Text) ? DBNull.Value : (object)txtDescription.Text)
            );

            if (rows > 0)
            {
                MessageBox.Show("Game updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("No changes were made.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }

}
