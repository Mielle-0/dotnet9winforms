using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Krypton.Toolkit;
using it13Project.UI;

namespace it13Project.Pages
{
    partial class GameListPage
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Name = "GameListPage";
            this.Size = new Size(800, 600);
            this.Padding = new Padding(20);
            this.BackColor = ThemeColors.ContentBackground;

            tableLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 1
            };

            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45)); // Action bar
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50)); // filter row
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // cards row

            // Action Panel (toolbar)
            actionPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                Height = 40,
                Padding = new Padding(10, 5, 0, 5),
                BackColor = ThemeColors.MenuBackground,
                ForeColor = ThemeColors.TextColor,
                FlowDirection = FlowDirection.LeftToRight
            };

            // Buttons
            btnAdd = new KryptonButton { Text = "Add Game", Margin = new Padding(5, 5, 0, 0) };
            // btnEdit = new KryptonButton { Text = "Edit Selected", Margin = new Padding(5, 5, 0, 0) };
            // btnDelete = new KryptonButton { Text = "Delete Selected", Margin = new Padding(5, 5, 0, 0) };
            btnUpload = new KryptonButton { Text = "Bulk Upload CSV", Margin = new Padding(5, 5, 0, 0), Width = 130 };
            btnRefresh = new KryptonButton { Text = "Refresh List", Margin = new Padding(5, 5, 0, 0) };

            actionPanel.Controls.AddRange(new Control[] { btnAdd, btnEdit, btnUpload, btnRefresh });

            filterPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 60,
                Padding = new Padding(5),
                BackColor = ThemeColors.MenuBackground,
                ForeColor = ThemeColors.TextColor,
                AutoSize = true,
                WrapContents = false
            };

            // Search box
            filterPanel.Controls.Add(new KryptonLabel
            {
                Text = "Search:",
                StateCommon = { ShortText = { Color1 = ThemeColors.TextColor } },
                Margin = new Padding(5, 10, 0, 0)
            });

            txtSearch = new KryptonTextBox
            {
                Width = 150,
                CueHint = { CueHintText = "Search Games" }
            };
            filterPanel.Controls.Add(txtSearch);

            // Reviews filter
            filterPanel.Controls.Add(new KryptonLabel
            {
                Text = "Reviews ≥",
                StateCommon = { ShortText = { Color1 = ThemeColors.TextColor } },
                Margin = new Padding(15, 10, 0, 0)
            });

            numReviews = new KryptonNumericUpDown
            {
                Width = 80,
                Minimum = 0,
                Maximum = 1000000
            };
            filterPanel.Controls.Add(numReviews);

            // Release date filter
            filterPanel.Controls.Add(new KryptonLabel
            {
                Text = "From:",
                StateCommon = { ShortText = { Color1 = ThemeColors.TextColor } },
                Margin = new Padding(15, 10, 0, 0)
            });

            dtReleaseFrom = new KryptonDateTimePicker
            {
                Width = 120,
                Format = DateTimePickerFormat.Short,
                ShowCheckBox = true
            };
            filterPanel.Controls.Add(dtReleaseFrom);

            filterPanel.Controls.Add(new KryptonLabel
            {
                Text = "To:",
                StateCommon = { ShortText = { Color1 = ThemeColors.TextColor } },
                Margin = new Padding(5, 10, 0, 0)
            });

            dtReleaseTo = new KryptonDateTimePicker
            {
                Width = 120,
                Format = DateTimePickerFormat.Short,
                ShowCheckBox = true
            };
            filterPanel.Controls.Add(dtReleaseTo);

            // Apply button
            btnApplyFilters = new KryptonButton
            {
                Text = "Apply Filters",
                OverrideDefault = { Back = { Color1 = ThemeColors.AccentPrimary } },
                StateCommon = { Content = { ShortText = { Color1 = Color.Black } } },
                Margin = new Padding(15, 5, 0, 0),
                Width = 120
            };
            filterPanel.Controls.Add(btnApplyFilters);

            // Card Container (instead of DataGridView)
            flpCards = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                WrapContents = true,
                Padding = new Padding(10),
                BackColor = ThemeColors.ContentBackground
            };

            // Example: add placeholder cards (later replace with dynamic data)
            for (int i = 0; i < 5; i++)
            {
                flpCards.Controls.Add(CreateGameCard(
                    "Game Title " + (i + 1),
                    "This is a short description of the game. It will be truncated to fit inside the card."
                ));
            }

            // Add to controls
            tableLayout.Controls.Add(actionPanel, 0, 0);
            tableLayout.Controls.Add(filterPanel, 0, 1);
            tableLayout.Controls.Add(flpCards, 0, 2);


            flpCards.Scroll += (s, e) =>
            {
                if (flpCards.VerticalScroll.Value + flpCards.ClientSize.Height >= flpCards.VerticalScroll.Maximum)
                {
                    LoadNextBatch(); 
                }
            };

            this.Controls.Add(tableLayout);
            this.Load += GameListControl_Load;
            this.btnApplyFilters.Click += btnApplyFilters_Click;
            this.ResumeLayout(false);
        }

        private Control CreateGameCard(
            string title,
            string description,
            string genre = null,
            DateTime? releaseDate = null,
            string developer = null,
            string publisher = null,
            double? avgSentiment = null,
            int reviewCount = 0,
            Image image = null)
        {
            var group = new KryptonGroup
            {
                Width = 220,
                Height = 180,
                Margin = new Padding(10),
                StateCommon =
                    {
                        Back = { Color1 = ThemeColors.ContentBackground },
                        Border =
                        {
                            DrawBorders = PaletteDrawBorders.All,
                            Rounding = 10,
                            Width = 1,
                            Color1 = ThemeColors.MenuHover
                        }
                    }
            };

            // var layout = new TableLayoutPanel
            // {
            //     Dock = DockStyle.Fill,
            //     RowCount = 1,
            //     ColumnCount = 2,
            //     Padding = new Padding(8)
            // };
            // layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90));
            // layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 2,
                Padding = new Padding(6)
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 60));  // top row
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 40));  // bottom row
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70)); // left col (image)
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100)); // right col (text)

            // Thumbnail
            var pic = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = image ?? Properties.Resources.list1,
                Margin = new Padding(0, 0, 0, 5)
            };

            // Text panel
            var textLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 1
            };
            textLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 22)); // title
            textLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 18)); // genre/date
            textLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 18)); // dev/publisher

            var lblTitle = new KryptonLabel
            {
                Text = title,
                Dock = DockStyle.Fill,
                StateCommon = {
                    ShortText = { Font = new Font("Segoe UI", 11F, FontStyle.Bold), Color1 = ThemeColors.MenuBackground }
                }
            };

            // Enable word wrap directly
            lblTitle.Values.Text = title;
            lblTitle.StateCommon.ShortText.MultiLine = InheritBool.True;

            var lblMeta = new KryptonLabel
            {
                Text = $"{genre ?? "Unknown"} • {releaseDate?.ToString("yyyy") ?? "TBA"}",
                Dock = DockStyle.Fill,
                StateCommon = {
            ShortText = { Font = new Font("Segoe UI", 9F, FontStyle.Italic), Color1 = ThemeColors.MenuBackground }
        }
            };

            var lblDevPub = new KryptonLabel
            {
                Text = $"{developer ?? ""}{(publisher != null ? " / " + publisher : "")}",
                Dock = DockStyle.Fill,
                StateCommon = {
            ShortText = { Font = new Font("Segoe UI", 9F), Color1 = ThemeColors.MenuBackground }
        }
            };

            // var lblDesc = new KryptonLabel
            // {
            //     Text = description.Length > 100 ? description.Substring(0, 97) + "..." : description,
            //     Dock = DockStyle.Fill,
            //     AutoSize = false,
            //     StateCommon = {
            //         ShortText = { Font = new Font("Segoe UI", 9F), Color1 = ThemeColors.MenuBackground }
            //     }
            // };
            var lblDesc = new Label  // <-- Use standard Label for better ellipsis control
            {
                Text = description,
                Dock = DockStyle.Fill,
                AutoSize = false,
                MaximumSize = new Size(0, 40), // restrict height
                TextAlign = ContentAlignment.TopLeft,
                Font = new Font("Segoe UI", 9F),
                ForeColor = ThemeColors.MenuBackground,
                Padding = new Padding(0, 0, 0, 0),
                AutoEllipsis = true   // 👈 shows "..." if text is too long
            };

            var lblSentiment = new KryptonLabel
            {
                Text = reviewCount > 0
                    ? $"⭐ Sentiment: {avgSentiment?.ToString("F2") ?? "0.00"} ({reviewCount} reviews)"
                    : "⭐ No reviews yet",
                Dock = DockStyle.Fill,
                StateCommon = {
            ShortText = { Font = new Font("Segoe UI", 9F, FontStyle.Bold), Color1 = ThemeColors.MenuBackground }
        }
            };


            // Add to layout
            textLayout.Controls.Add(lblTitle, 0, 0);
            textLayout.Controls.Add(lblMeta, 0, 1);
            textLayout.Controls.Add(lblDevPub, 0, 2);
            textLayout.Controls.Add(lblDesc, 0, 3);
            textLayout.Controls.Add(lblSentiment, 0, 4);

            // layout.Controls.Add(pic, 0, 0);
            // layout.Controls.Add(textLayout, 1, 0);
            layout.Controls.Add(pic, 0, 0);
            layout.Controls.Add(textLayout, 0, 1);


            group.Panel.Controls.Add(layout);
            return group;
        }


        private Control CreateGameSummaryCard(
            int gameId,
            string title,
            string genre = null,
            DateTime? releaseDate = null,
            Image image = null,
            double? avgSentiment = null,
            int reviewCount = 0)
        {
            var group = new KryptonGroup
            {
                Width = 220,
                Height = 120,
                Margin = new Padding(10),
                StateCommon =
                {
                    Back = { Color1 = ThemeColors.ContentBackground },
                    Border =
                    {
                        DrawBorders = PaletteDrawBorders.All,
                        Rounding = 10,
                        Width = 1,
                        Color1 = ThemeColors.MenuHover
                    }
                }
            };

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 1,
                ColumnCount = 2,
                Padding = new Padding(6)
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70)); // image
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100)); // text

            // Thumbnail
            var pic = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = image ?? Properties.Resources.list1,
                Margin = new Padding(0)
            };

            // Text panel
            var textLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 1
            };
            textLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 24)); // title
            textLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 18)); // genre/date
            textLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 18)); // new row for sentiment

            var lblTitle = new KryptonLabel
            {
                Text = title,
                Dock = DockStyle.Fill,
                StateCommon = {
                    ShortText = { Font = new Font("Segoe UI", 11F, FontStyle.Bold), Color1 = ThemeColors.MenuBackground }
                }
            };

            var lblMeta = new KryptonLabel
            {
                Text = $"{genre ?? "Unknown"} • {releaseDate?.ToString("yyyy") ?? "TBA"}",
                Dock = DockStyle.Fill,
                StateCommon = {
                    ShortText = { Font = new Font("Segoe UI", 9F, FontStyle.Italic), Color1 = ThemeColors.MenuBackground }
                }
            };

            var lblSentiment = new KryptonLabel
            {
                Text = reviewCount > 0
                    ? $"⭐ {avgSentiment?.ToString("F2") ?? "0.00"} ({reviewCount} reviews)"
                    : "⭐ No reviews yet",
                Dock = DockStyle.Fill,
                StateCommon = {
                    ShortText = { Font = new Font("Segoe UI", 9F, FontStyle.Bold), Color1 = ThemeColors.MenuBackground }
                }
            };

            // Add to layout
            textLayout.Controls.Add(lblTitle, 0, 0);
            textLayout.Controls.Add(lblMeta, 0, 1);
            textLayout.Controls.Add(lblSentiment, 0, 2);

            layout.Controls.Add(pic, 0, 0);
            layout.Controls.Add(textLayout, 1, 0);

            group.Panel.Controls.Add(layout);
            group.Tag = gameId;

            return group;
        }




        #endregion

        private TableLayoutPanel tableLayout;
        private KryptonComboBox cboGenre;
        private KryptonComboBox cboSentiment;
        private KryptonButton btnApplyFilters, btnAdd, btnEdit, btnUpload, btnRefresh;
        private KryptonDataGridView dgvGames;
        private FlowLayoutPanel flpCards, filterPanel, actionPanel;
        private KryptonTextBox txtSearch;
        private KryptonNumericUpDown numReviews;
        private KryptonDateTimePicker dtReleaseFrom;
        private KryptonDateTimePicker dtReleaseTo;

    }
}
