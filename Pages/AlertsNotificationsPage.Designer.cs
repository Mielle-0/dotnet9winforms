using System;
using Krypton.Toolkit;
using it13Project.UI;
using ScottPlot;
using ScottPlot.WinForms;

namespace it13Project.Pages
{
    partial class AlertsNotificationsPage
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

        #region

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        
        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Name = "AlertsNotificationsPage";
            this.Size = new Size(800, 600);
            this.Padding = new Padding(20);
            this.BackColor = ThemeColors.ContentBackground;

            tableLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 1
            };
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60)); // filters row
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // grid row

            filtersPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 60,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(10),
                WrapContents = false
            };
            filtersPanel.BackColor = ThemeColors.MenuBackground;

            cbGame = CreateFilterComboBox(150);
            cbPlatform = CreateFilterComboBox(120);
            cbTimeframe = CreateFilterComboBox(120);

            filtersPanel.Controls.Add(cbGame);
            filtersPanel.SetFlowBreak(cbGame, false); 
            filtersPanel.Controls.Add(cbPlatform);
            filtersPanel.SetFlowBreak(cbPlatform, false);
            filtersPanel.Controls.Add(cbTimeframe);
            filtersPanel.SetFlowBreak(cbTimeframe, false);

            alertsGrid = new KryptonDataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = ThemeColors.ContentBackground,
                ForeColor = ThemeColors.TextColor,
                EnableHeadersVisualStyles = false,
                ColumnHeadersDefaultCellStyle =
                {
                    BackColor = ThemeColors.MenuBackground,
                    ForeColor = ThemeColors.TextColor
                },
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            alertsGrid.Columns.Add("Time", "Time");
            alertsGrid.Columns.Add("Game", "Game");
            alertsGrid.Columns.Add("Review", "Review Excerpt");
            alertsGrid.Columns.Add("Sentiment", "Sentiment");
            alertsGrid.Columns.Add("AlertType", "Alert Type");
            alertsGrid.Columns.Add("Link", "Link");

            alertsGrid.CellFormatting += AlertsGrid_CellFormatting;

            tableLayout.Controls.Add(filtersPanel, 0, 0);
            tableLayout.Controls.Add(alertsGrid, 0, 1);

            this.Controls.Add(tableLayout);
            this.Load += AlertsControl_Load;
            this.ResumeLayout(false);
        }


        private KryptonComboBox CreateFilterComboBox(int width)
        {
            var cb = new KryptonComboBox
            {
                Width = width,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            // cb.StateCommon.Back.Color1 = ThemeColors.ContentBackground;
            // cb.StateCommon.Content.Color1 = ThemeColors.TextColor;
            return cb;
        }

        #endregion



        private TableLayoutPanel tableLayout;
        private FlowLayoutPanel filtersPanel;
        private KryptonDataGridView alertsGrid;

        private KryptonComboBox cbGame;
        private KryptonComboBox cbPlatform;
        private KryptonComboBox cbTimeframe; 
    }
}
