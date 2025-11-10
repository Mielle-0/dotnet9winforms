using System.ComponentModel;
using Krypton.Toolkit;

namespace it13Project.Forms
{
    partial class ReviewEdit
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
            this.numScore = new NumericUpDown();
            this.chkRecommendation = new KryptonCheckBox();
            this.cboSentiment = new KryptonComboBox();
            this.txtConfidence = new KryptonTextBox();
            this.dtReviewDate = new KryptonDateTimePicker();

            this.actionPanel = new FlowLayoutPanel();
            this.btnSave = new KryptonButton();
            this.btnCancel = new KryptonButton();

            ((ISupportInitialize)(this.kryptonPanel)).BeginInit();
            this.kryptonPanel.SuspendLayout();
            this.tableLayout.SuspendLayout();
            this.actionPanel.SuspendLayout();
            ((ISupportInitialize)(this.cboSentiment)).BeginInit();
            ((ISupportInitialize)(this.numScore)).BeginInit();
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

            // AppName (read-only – typically you don't change app name during review edit)
            this.lblAppName.Text = "App Name:";
            this.tableLayout.Controls.Add(this.lblAppName, 0, 0);
            this.txtAppName.ReadOnly = true;
            this.tableLayout.Controls.Add(this.txtAppName, 1, 0);

            // ReviewText (editable, multi-line)
            this.lblReviewText.Text = "Review:";
            this.tableLayout.Controls.Add(this.lblReviewText, 0, 1);
            this.txtReviewText.Multiline = true;
            this.txtReviewText.Height = 120;
            this.tableLayout.Controls.Add(this.txtReviewText, 1, 1);
            this.tableLayout.SetRowSpan(this.txtReviewText, 3);

            // Score
            this.lblScore.Text = "Score:";
            this.tableLayout.Controls.Add(this.lblScore, 0, 4);
            this.numScore.Minimum = 0;
            this.numScore.Maximum = 10;
            this.numScore.DecimalPlaces = 0;
            this.tableLayout.Controls.Add(this.numScore, 1, 4);

            // Recommendation
            this.lblRecommendation.Text = "Recommended:";
            this.tableLayout.Controls.Add(this.lblRecommendation, 0, 5);
            this.tableLayout.Controls.Add(this.chkRecommendation, 1, 5);

            // Sentiment
            this.lblSentiment.Text = "Sentiment:";
            this.tableLayout.Controls.Add(this.lblSentiment, 0, 6);
            this.cboSentiment.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboSentiment.Items.AddRange(new object[] { "Positive", "Neutral", "Negative" });
            this.tableLayout.Controls.Add(this.cboSentiment, 1, 6);

            // Confidence (read-only or editable depending on design)
            this.lblConfidence.Text = "Confidence:";
            this.tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            this.tableLayout.Controls.Add(this.lblConfidence, 0, 7);
            this.tableLayout.Controls.Add(this.txtConfidence, 1, 7);

            // Review date
            this.lblDate.Text = "Date:";
            this.tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            this.tableLayout.Controls.Add(this.lblDate, 0, 8);
            this.dtReviewDate.Format = DateTimePickerFormat.Short;
            this.tableLayout.Controls.Add(this.dtReviewDate, 1, 8);

            // actionPanel
            this.actionPanel.Dock = DockStyle.Bottom;
            this.actionPanel.Height = 48;
            this.actionPanel.FlowDirection = FlowDirection.RightToLeft;
            this.actionPanel.Padding = new Padding(6);
            this.actionPanel.Controls.Add(this.btnSave);
            this.actionPanel.Controls.Add(this.btnCancel);

            // btnSave
            this.btnSave.Text = "Save";

            // btnCancel
            this.btnCancel.Text = "Cancel";

            // ReviewEditForm
            this.ClientSize = new System.Drawing.Size(700, 520);
            this.Controls.Add(this.kryptonPanel);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Edit Review";

            // this.btnSave.Click += new EventHandler(this.btnSave_Click);
            // this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            ((ISupportInitialize)(this.cboSentiment)).EndInit();
            ((ISupportInitialize)(this.numScore)).EndInit();
            this.tableLayout.ResumeLayout(false);
            this.kryptonPanel.ResumeLayout(false);
            this.actionPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        

        private KryptonPanel kryptonPanel;
        private TableLayoutPanel tableLayout;
        private KryptonLabel lblAppName, lblReviewText, lblScore, lblRecommendation, lblSentiment, lblConfidence, lblDate;
        private KryptonTextBox txtAppName;
        private KryptonTextBox txtReviewText;
        private NumericUpDown numScore;
        private KryptonCheckBox chkRecommendation;
        private KryptonComboBox cboSentiment;
        private KryptonTextBox txtConfidence;
        private KryptonDateTimePicker dtReviewDate;
        private FlowLayoutPanel actionPanel;
        private KryptonButton btnSave, btnCancel;
    }
}