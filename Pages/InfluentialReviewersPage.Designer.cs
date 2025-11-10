namespace it13Project.Pages
{
    partial class InfluentialReviewersPage
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
            this.Name = "InfluentialReviewersPage";
            this.Size = new System.Drawing.Size(800, 600);
            this.Padding = new Padding(20);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);

            // Title Label
            var lblTitle = new Label { Text = "Influential Reviewers", Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold), Dock = DockStyle.Top, Height = 40 };

            // Top Panel
            var topPanel = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 40, FlowDirection = FlowDirection.LeftToRight };
            topPanel.Controls.Add(new Button { Text = "Export Contact List", AutoSize = true });

            // Data Grid for reviewers
            var dgvReviewers = new DataGridView();
            dgvReviewers.Dock = DockStyle.Fill;
            dgvReviewers.ColumnCount = 4;
            dgvReviewers.Columns[0].Name = "Reviewer Name";
            dgvReviewers.Columns[1].Name = "Total Influence (Votes)";
            dgvReviewers.Columns[2].Name = "Review Count";
            dgvReviewers.Columns[3].Name = "Avg. Sentiment";
            dgvReviewers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReviewers.Rows.Add("GamerX123", "15,482", "56", "85% Positive");
            dgvReviewers.Rows.Add("CritiqueQueen", "12,101", "32", "72% Positive");

            this.Controls.Add(dgvReviewers);
            this.Controls.Add(topPanel);
            this.Controls.Add(lblTitle);
            this.ResumeLayout(false);
        }
        #endregion
    }
}
