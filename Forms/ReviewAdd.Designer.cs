using System.ComponentModel;
using Krypton.Toolkit;
using it13Project.UI;
namespace it13Project.Forms
{
    partial class ReviewAdd
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Name = "AddReviewForm";
            this.Size = new Size(600, 450);
            this.Padding = new Padding(20);
            this.BackColor = ThemeColors.ContentBackground;

            // Main Layout
            mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 8,
                ColumnCount = 2,
                AutoSize = true
            };
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            // --- Review Text ---
            mainLayout.Controls.Add(new KryptonLabel
            {
                Text = "Review Text:",
                Margin = new Padding(5, 10, 0, 0),
                StateCommon = { ShortText = { Color1 = Color.Black } }
            }, 0, 0);

            txtReviewText = new KryptonTextBox
            {
                Multiline = true,
                Height = 100,
                Dock = DockStyle.Fill
            };
            mainLayout.Controls.Add(txtReviewText, 1, 0);

            // --- Votes ---
            mainLayout.Controls.Add(new KryptonLabel
            {
                Text = "Votes:",
                Margin = new Padding(5, 10, 0, 0),
                StateCommon = { ShortText = { Color1 = Color.Black } }
            }, 0, 1);

            numVotes = new KryptonNumericUpDown
            {
                Minimum = 0,
                Maximum = 100000,
                Width = 100
            };
            mainLayout.Controls.Add(numVotes, 1, 1);

            // --- Review Date ---
            mainLayout.Controls.Add(new KryptonLabel
            {
                Text = "Date:",
                Margin = new Padding(5, 10, 0, 0),
                StateCommon = { ShortText = { Color1 = Color.Black } }
            }, 0, 2);

            dtpReviewDate = new KryptonDateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Width = 150
            };
            mainLayout.Controls.Add(dtpReviewDate, 1, 2);

            // --- Reviewer ID ---
            mainLayout.Controls.Add(new KryptonLabel
            {
                Text = "Reviewer ID:",
                Margin = new Padding(5, 10, 0, 0),
                StateCommon = { ShortText = { Color1 = Color.Black } }
            }, 0, 3);

            txtReviewerId = new KryptonTextBox { Width = 200 };
            mainLayout.Controls.Add(txtReviewerId, 1, 3);

            // --- Score ---
            mainLayout.Controls.Add(new KryptonLabel
            {
                Text = "Score:",
                Margin = new Padding(5, 10, 0, 0),
                StateCommon = { ShortText = { Color1 = Color.Black } }
            }, 0, 4);

            numScore = new KryptonNumericUpDown
            {
                DecimalPlaces = 1,
                Increment = 0.1M,
                Minimum = 0,
                Maximum = 10,
                Width = 100
            };
            mainLayout.Controls.Add(numScore, 1, 4);

            // --- Recommendation ---
            mainLayout.Controls.Add(new KryptonLabel
            {
                Text = "Recommended:",
                Margin = new Padding(5, 10, 0, 0),
                StateCommon = { ShortText = { Color1 = Color.Black } }
            }, 0, 5);

            chkRecommended = new KryptonCheckBox { Text = "Yes" };
            mainLayout.Controls.Add(chkRecommended, 1, 5);

            // --- Game Selection ---
            mainLayout.Controls.Add(new KryptonLabel
            {
                Text = "Game:",
                Margin = new Padding(5, 10, 0, 0),
                StateCommon = { ShortText = { Color1 = Color.Black } }
            }, 0, 6);

            cmbGame = new KryptonComboBox
            {
                Width = 250,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            mainLayout.Controls.Add(cmbGame, 1, 6);

            // --- Buttons ---
            buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                Padding = new Padding(0, 20, 0, 0)
            };

            btnSave = new KryptonButton
            {
                Text = "Save",
                Width = 100,
                OverrideDefault = { Back = { Color1 = ThemeColors.AccentPrimary } },
                StateCommon = { Content = { ShortText = { Color1 = Color.Black } } }
            };

            btnCancel = new KryptonButton
            {
                Text = "Cancel",
                Width = 100
            };

            buttonPanel.Controls.Add(btnSave);
            buttonPanel.Controls.Add(btnCancel);
            mainLayout.Controls.Add(buttonPanel, 1, 7);

            // Add Layout to Form
            this.Controls.Add(mainLayout);
            this.ResumeLayout(false);
        }



        #endregion

        
        #region Controls

        private TableLayoutPanel mainLayout;
        private FlowLayoutPanel buttonPanel;

        // Review Fields
        private KryptonTextBox txtReviewText;
        private KryptonNumericUpDown numVotes;
        private KryptonDateTimePicker dtpReviewDate;
        private KryptonTextBox txtReviewerId;
        private KryptonNumericUpDown numScore;
        private KryptonCheckBox chkRecommended;
        private KryptonComboBox cmbGame;

        // Buttons
        private KryptonButton btnSave;
        private KryptonButton btnCancel;

        #endregion

    }
}