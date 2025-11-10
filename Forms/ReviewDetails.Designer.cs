using System.ComponentModel;
using Krypton.Toolkit;

namespace it13Project.Forms
{
    partial class ReviewDetails
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
            this.components = new Container();
            this.kryptonPanel = new KryptonPanel();
            this.tableLayout = new TableLayoutPanel();
            this.lblAppName = new KryptonLabel();
            this.lblReviewText = new KryptonLabel();
            this.lblScore = new KryptonLabel();
            this.lblRecommendation = new KryptonLabel();
            this.lblSentiment = new KryptonLabel();
            this.lblConfidence = new KryptonLabel();
            this.lblDate = new KryptonLabel();

            this.txtAppName = new KryptonTextBox();
            this.txtReviewText = new KryptonTextBox();
            this.txtScore = new KryptonTextBox();
            this.txtRecommendation = new KryptonTextBox();
            this.txtSentiment = new KryptonTextBox();
            this.txtConfidence = new KryptonTextBox();
            this.txtDate = new KryptonTextBox();

            this.actionPanel = new FlowLayoutPanel();
            this.btnClose = new KryptonButton();

            ((ISupportInitialize)(this.kryptonPanel)).BeginInit();
            this.kryptonPanel.SuspendLayout();
            this.tableLayout.SuspendLayout();
            this.actionPanel.SuspendLayout();
            this.SuspendLayout();

            // kryptonPanel
            this.kryptonPanel.Dock = DockStyle.Fill;
            this.kryptonPanel.Padding = new Padding(12);
            this.kryptonPanel.Controls.Add(this.tableLayout);
            this.kryptonPanel.Controls.Add(this.actionPanel);

            // tableLayout
            this.tableLayout.Dock = DockStyle.Fill;
            this.tableLayout.RowCount = 7;
            this.tableLayout.ColumnCount = 2;
            this.tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            this.tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            for (int i = 0; i < 7; i++)
                this.tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            this.tableLayout.Padding = new Padding(6);

            // Labels and read-only textboxes
            // AppName
            this.lblAppName.Text = "App Name:";
            this.lblAppName.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tableLayout.Controls.Add(this.lblAppName, 0, 0);
            this.txtAppName.ReadOnly = true;
            this.tableLayout.Controls.Add(this.txtAppName, 1, 0);

            // ReviewText (use taller textbox by spanning rows)
            this.lblReviewText.Text = "Review:";
            this.tableLayout.Controls.Add(this.lblReviewText, 0, 1);
            this.txtReviewText.ReadOnly = true;
            this.txtReviewText.Multiline = true;
            this.txtReviewText.Height = 120;
            this.tableLayout.Controls.Add(this.txtReviewText, 1, 1);
            this.tableLayout.SetRowSpan(this.txtReviewText, 3);

            // Score
            this.lblScore.Text = "Score:";
            this.tableLayout.Controls.Add(this.lblScore, 0, 4);
            this.txtScore.ReadOnly = true;
            this.tableLayout.Controls.Add(this.txtScore, 1, 4);

            // Recommendation
            this.lblRecommendation.Text = "Recommended:";
            this.tableLayout.Controls.Add(this.lblRecommendation, 0, 5);
            this.txtRecommendation.ReadOnly = true;
            this.tableLayout.Controls.Add(this.txtRecommendation, 1, 5);

            // Sentiment
            this.lblSentiment.Text = "Sentiment:";
            this.tableLayout.Controls.Add(this.lblSentiment, 0, 6);
            this.txtSentiment.ReadOnly = true;
            this.tableLayout.Controls.Add(this.txtSentiment, 1, 6);

            // Confidence and Date - placed below main rows in a small separate area
            // We'll add them to the action area as small read-only fields (or you can place them above)
            // For simplicity, append them below the table by adding more rows if desired:
            // (instead, we'll re-use lblConfidence & txtConfidence and lblDate & txtDate in the table)
            this.lblConfidence.Text = "Confidence:";
            this.tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            this.tableLayout.Controls.Add(this.lblConfidence, 0, 7);
            this.txtConfidence.ReadOnly = true;
            this.tableLayout.Controls.Add(this.txtConfidence, 1, 7);

            this.lblDate.Text = "Date:";
            this.tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            this.tableLayout.Controls.Add(this.lblDate, 0, 8);
            this.txtDate.ReadOnly = true;
            this.tableLayout.Controls.Add(this.txtDate, 1, 8);

            // actionPanel
            this.actionPanel.Dock = DockStyle.Bottom;
            this.actionPanel.Height = 48;
            this.actionPanel.FlowDirection = FlowDirection.RightToLeft;
            this.actionPanel.Padding = new Padding(6);
            this.actionPanel.Controls.Add(this.btnClose);

            // btnClose
            this.btnClose.Text = "Close";
            this.btnClose.DialogResult = DialogResult.OK;

            // ReviewDetailsForm
            this.ClientSize = new System.Drawing.Size(600, 480);
            this.Controls.Add(this.kryptonPanel);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Review Details";


            // btnClose.Click += btnClose_Click;
            this.tableLayout.ResumeLayout(false);
            this.kryptonPanel.ResumeLayout(false);
            this.actionPanel.ResumeLayout(false);
            ((ISupportInitialize)(this.kryptonPanel)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        

        private KryptonPanel kryptonPanel;
        private TableLayoutPanel tableLayout;
        private KryptonLabel lblAppName, lblReviewText, lblScore, lblRecommendation, lblSentiment, lblConfidence, lblDate;
        private KryptonTextBox txtAppName;
        private KryptonTextBox txtReviewText;
        private KryptonTextBox txtScore;
        private KryptonTextBox txtRecommendation;
        private KryptonTextBox txtSentiment;
        private KryptonTextBox txtConfidence;
        private KryptonTextBox txtDate;
        private FlowLayoutPanel actionPanel;
        private KryptonButton btnClose;
    }
}