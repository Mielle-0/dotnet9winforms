namespace it13Project.Pages
{
    partial class ReviewExplorerPage
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
            this.Name = "ReviewExplorerPage";
            this.Size = new System.Drawing.Size(800, 600);
            this.Padding = new Padding(20);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);

            // Title Label
            var lblTitle = new Label();
            lblTitle.Text = "Review Explorer";
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Height = 40;

            // Filter Panel
            var filterPanel = new FlowLayoutPanel();
            filterPanel.Dock = DockStyle.Top;
            filterPanel.Height = 40;
            filterPanel.Padding = new Padding(0, 5, 0, 5);

            filterPanel.Controls.Add(new Label { Text = "Search Keyword:", AutoSize = true });
            filterPanel.Controls.Add(new TextBox { Width = 200 });
            filterPanel.Controls.Add(new Button { Text = "Search" });

            // Data Grid for reviews
            var dgvReviews = new DataGridView();
            dgvReviews.Dock = DockStyle.Fill;
            dgvReviews.ColumnCount = 5;
            dgvReviews.Columns[0].Name = "Reviewer";
            dgvReviews.Columns[1].Name = "Review Text";
            dgvReviews.Columns[1].Width = 350;
            dgvReviews.Columns[2].Name = "Sentiment";
            dgvReviews.Columns[3].Name = "Helpfulness";
            dgvReviews.Columns[4].Name = "Date";
            dgvReviews.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.Controls.Add(dgvReviews);
            this.Controls.Add(filterPanel);
            this.Controls.Add(lblTitle);
            this.ResumeLayout(false);
        }
        #endregion
    }
}
