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
using Krypton.Toolkit;

namespace it13Project.Forms
{
    public partial class GameForm : Form
    {

        private readonly bool _isEditMode;
        private readonly int? _gameId;
        public GameForm(bool isEditMode = false, int? gameId = null)
        {
            InitializeComponent();
            _isEditMode = isEditMode;
            _gameId = gameId;

            if (_isEditMode && _gameId.HasValue)
            {
                LoadGameData(_gameId.Value);
            }

            btnSave.Text = _isEditMode ? "Update" : "Create";
        }





        private void btnSave_Click(object sender, EventArgs e)
        {
            var game = new Game
            {
                GameId = _gameId ?? 0,
                AppId = txtAppId.Text.Trim(),
                AppName = txtAppName.Text.Trim(),
                Genre = string.IsNullOrWhiteSpace(txtGenre.Text) ? null : txtGenre.Text.Trim(),
                ReleaseDate = dtRelease.Checked ? dtRelease.Value.Date : (DateTime?)null,
                Developer = string.IsNullOrWhiteSpace(txtDeveloper.Text) ? null : txtDeveloper.Text.Trim(),
                Publisher = string.IsNullOrWhiteSpace(txtPublisher.Text) ? null : txtPublisher.Text.Trim(),
                Description = string.IsNullOrWhiteSpace(txtDescription.Text) ? null : txtDescription.Text.Trim(),
                Source = string.IsNullOrWhiteSpace(txtSource.Text) ? null : txtSource.Text.Trim()
            };

            bool success;
            if (_isEditMode)
            {
                success = GameService.UpdateGame(game);
                KryptonMessageBox.Show(success ? "Game updated successfully." : "Update failed.");
            }
            else
            {
                int newId = GameService.AddGame(game);
                success = newId > 0;
                KryptonMessageBox.Show(success ? "Game added successfully." : "Insert failed.");
            }

            if (success)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }


        private void LoadGameData(int gameId)
        {
            var game = GameService.GetGameById(gameId);
            if (game == null)
            {
                KryptonMessageBox.Show("Game not found.");
                this.Close();
                return;
            }

            txtAppId.Text = game.AppId;
            txtAppName.Text = game.AppName;
            txtGenre.Text = game.Genre;
            if (game.ReleaseDate.HasValue)
            {
                dtRelease.Value = game.ReleaseDate.Value;
                dtRelease.Checked = true;
            }
            else
            {
                dtRelease.Checked = false;
            }
            txtDeveloper.Text = game.Developer;
            txtPublisher.Text = game.Publisher;
            txtSource.Text = game.Source;
            txtDescription.Text = game.Description;
        }


    }
}
