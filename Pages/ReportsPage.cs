using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;
using it13Project.Data;
using it13Project.UI;
using Krypton.Toolkit;
using Microsoft.Data.SqlClient;
using ScottPlot.WinForms;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.IO.Image;
using Krypton.Navigator;

namespace it13Project.Pages
{
    public partial class ReportsPage : UserControl
    {
        // Raw data fields
        private string[] months;
        private int[] positive;
        private int[] neutral;
        private int[] negative;

        private string[] games;
        private double[] recommended;
        private double[] notRecommended;

        private string[] recoLabels;
        private double[] recoValues;

        private double[,] genreIntensities;

        public ReportsPage()
        {
            InitializeComponent();
            ApplyRoleBasedTabs(CurrentUser.Role);
            

            // LoadDemoData();
            LoadDataFromDatabase();
        }

        // Add this method to your ReportsPage class

        // Example method in ReportsPage.cs
        private void ApplyRoleBasedTabs(string userRole)
        {
            var tabMap = new Dictionary<string, KryptonPage>(StringComparer.OrdinalIgnoreCase)
                {
                    { "SentimentTrend", tabTrend },
                    { "TopGames", tabTopGames },
                    { "Recommendations", tabReco },
                    { "GenreSentiment", tabGenre },
                    { "ControversialGames", tabControversial },
                    { "LowConfidence", tabLowConf }
                };

            // Get allowed tabs for this role
            if (!RolePermissions.ReportsTabs.TryGetValue(userRole, out var allowedTabs))
                allowedTabs = new List<string>();

            tabControl.Pages.Clear();

            foreach (var tabKey in tabMap.Keys)
            {
                if (allowedTabs.Contains(tabKey))
                    tabControl.Pages.Add(tabMap[tabKey]);
            }

            if (tabControl.Pages.Count > 0)
                tabControl.SelectedPage = tabControl.Pages[0];
        }


        public void LoadDataFromDatabase()
        {
            DateTime? from = dtFrom.Checked ? dtFrom.Value.Date : (DateTime?)null;
            DateTime? to = dtTo.Checked ? dtTo.Value.Date : (DateTime?)null;
            string template = cboTemplate.SelectedItem?.ToString() ?? "Weekly";

            // Call shared logic (no specific game filter on load)
            RefreshReports(from, to, template, null);

            // Populate schedules (only at load)
            dgvSchedules.Rows.Clear();
            dgvSchedules.Rows.Add("Weekly Sentiment", "PDF", "qa@studio.com", "Weekly", DateTime.Today.AddDays(7).ToShortDateString());
            dgvSchedules.Rows.Add("Monthly Overview", "Excel", "execs@studio.com", "Monthly", DateTime.Today.AddMonths(1).ToShortDateString());
            dgvSchedules.Rows.Add("Quarterly Report", "PDF", "stakeholders@studio.com", "Quarterly", DateTime.Today.AddMonths(3).ToShortDateString());

            // ✅ Now create service with default null filters to get game list
            var gameListService = new ReportsService(null, null, template);
            var gameList = gameListService.GetAllGames();
            cboGame.DataSource = gameList;
            cboGame.DisplayMember = "AppName";
            cboGame.ValueMember = "GameId";
            cboGame.SelectedIndex = -1;
        }




        private void RefreshReports(DateTime? from, DateTime? to, string template, int? selectedGameId = null)
        {
            var service = new ReportsService(from, to, template, selectedGameId);

            // 1️⃣ Sentiment Trend
            var trend = service.GetSentimentTrend();
            SetSentimentTrend(trend.Labels, trend.Positive, trend.Negative, trend.Neutral);

            // 2️⃣ Top Games
            var topGames = service.GetTopGames();
            SetTopGames(topGames.Games, topGames.Recommended, topGames.NotRecommended);

            // 3️⃣ Recommendation Breakdown
            var breakdown = service.GetRecommendationBreakdown();
            SetRecommendationPie(breakdown.Labels, breakdown.Values);

            // 4️⃣ Genre Sentiment
            var genre = service.GetGenreSentiment();
            SetGenreSentiment(genre.Genres, genre.Intensities);

            // 5️⃣ Controversial Games
            var controversial = service.GetControversialGames();
            SetControversialGames(
                controversial.AsEnumerable().Select(r => r.Field<string>("Game")).ToArray(),
                controversial.AsEnumerable().Select(r => Convert.ToInt32(r["Positive"])).ToArray(),
                controversial.AsEnumerable().Select(r => Convert.ToInt32(r["Negative"])).ToArray(),
                controversial.AsEnumerable().Select(r => Convert.ToInt32(r["TotalReviews"])).ToArray(),
                controversial.AsEnumerable().Select(r => Convert.ToDecimal(r["ControversyScore"])).ToArray()
            );

            // 6️⃣ Low Confidence Reviews
            var lowConf = service.GetLowConfidenceReviews();
            SetLowConfidence(
                lowConf.AsEnumerable().Select(r => Convert.ToInt32(r["ReviewID"])).ToArray(),
                lowConf.AsEnumerable().Select(r => r.Field<string>("Game")).ToArray(),
                lowConf.AsEnumerable().Select(r => r.Field<string>("Review")).ToArray(),
                lowConf.AsEnumerable().Select(r => Convert.ToDouble(r["Confidence"])).ToArray()
            );
        }



        public void SetSentimentTrend(string[] labels, int[] positive, int[] negative, int[] neutral)
        {
            chartTrend.Plot.Clear();

            double[] xs = Enumerable.Range(0, labels.Length).Select(i => (double)i).ToArray();
            double[] total = positive.Zip(neutral, (p, n) => p + n).Zip(negative, (pn, neg) => pn + neg).Select(v => (double)v).ToArray();

            double[] posPct = positive.Zip(total, (v, t) => v / t * 100).ToArray();
            double[] neuPct = neutral.Zip(total, (v, t) => v / t * 100).ToArray();
            double[] negPct = negative.Zip(total, (v, t) => v / t * 100).ToArray();

            chartTrend.Plot.Add.Scatter(xs, posPct).LegendText = "Positive";
            chartTrend.Plot.Add.Scatter(xs, neuPct).LegendText = "Neutral";
            chartTrend.Plot.Add.Scatter(xs, negPct).LegendText = "Negative";

            // X-axis ticks (genres)
            var ticks = labels
                .Select((g, i) => new ScottPlot.Tick(i + 1, g))
                .ToArray();
            chartTrend.Plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(ticks);
            chartTrend.Plot.Axes.Bottom.MajorTickStyle.Length = 0;

            chartTrend.Plot.Legend.IsVisible = true;
            chartTrend.Plot.XLabel("Months");
            chartTrend.Plot.YLabel("%");
            chartTrend.Refresh();
        }


        public void SetTopGames(string[] games, double[] numRecommended, double[] numNotRecom)
        {

            if (tabTopGames == null) return;
            if (games == null) return;
            if (chartTopGames == null) return;

            double[] xs = Enumerable.Range(0, games.Length).Select(i => (double)i).ToArray();
            chartTopGames.Plot.Clear();

            var bars = xs.Select((val, i) => new ScottPlot.Bar
            {
                Position = i,
                Value = val
            }).ToArray();

            chartTopGames.Plot.Clear();
            var barPlot = chartTopGames.Plot.Add.Bars(numRecommended);
            int count = 0;

            foreach (var bar in barPlot.Bars)
            {
                bar.Label = games[count];
                count++;
            }

            barPlot.ValueLabelStyle.FontSize = 14;
            chartTopGames.Plot.Axes.Margins(bottom: 0, top: .2);

            chartTopGames.Refresh();
        }

        public void SetRecommendationPie(string[] labels, double[] values)
        {
            if (chartReco == null) return;

            chartReco.Plot.Clear();

            var pie = chartReco.Plot.Add.Pie(values);
            pie.ExplodeFraction = 0.1;
            pie.SliceLabelDistance = 0.5;

            // set different labels for slices and legend
            double total = pie.Slices.Select(x => x.Value).Sum();
            for (int i = 0; i < pie.Slices.Count; i++)
            {
                pie.Slices[i].LabelFontSize = 20;
                pie.Slices[i].Label = $"{pie.Slices[i].Value / total:p1}";
                pie.Slices[i].LegendText = $"{labels[i]} ";
            }

            chartReco.Plot.Axes.Frameless();
            chartReco.Plot.HideGrid();

            chartReco.Refresh();
        }





        public void SetGenreSentiment(string[] genres, double[,] intensities)
        {
            if (chartHeatmap == null) return;

            chartHeatmap.Plot.Clear();

            var palette = new ScottPlot.Palettes.Category10();
            var bars = new List<ScottPlot.Bar>();

            for (int i = 0; i < genres.Length; i++)
            {
                double pos = i + 1; // x position (1-based like in your reference)
                double baseValue = 0;

                // Positive
                double positive = intensities[i, 0];
                bars.Add(new ScottPlot.Bar()
                {
                    Position = pos,
                    ValueBase = baseValue,
                    Value = baseValue + positive,
                    FillColor = palette.GetColor(0)
                });
                baseValue += positive;

                // Neutral
                double neutral = intensities[i, 1];
                bars.Add(new ScottPlot.Bar()
                {
                    Position = pos,
                    ValueBase = baseValue,
                    Value = baseValue + neutral,
                    FillColor = palette.GetColor(1)
                });
                baseValue += neutral;

                // Negative
                double negative = intensities[i, 2];
                bars.Add(new ScottPlot.Bar()
                {
                    Position = pos,
                    ValueBase = baseValue,
                    Value = baseValue + negative,
                    FillColor = palette.GetColor(2)
                });
            }

            // Add stacked bars
            chartHeatmap.Plot.Add.Bars(bars);


            // build the legend manually
            chartHeatmap.Plot.Legend.IsVisible = true;
            chartHeatmap.Plot.Legend.Alignment = ScottPlot.Alignment.UpperRight;
            chartHeatmap.Plot.Legend.ManualItems.Add(new() { LabelText = "Positive", FillColor = palette.GetColor(0) });
            chartHeatmap.Plot.Legend.ManualItems.Add(new() { LabelText = "Neutral", FillColor = palette.GetColor(1) });
            chartHeatmap.Plot.Legend.ManualItems.Add(new() { LabelText = "Negative", FillColor = palette.GetColor(2) });


            // X-axis ticks (genres)
            var ticks = genres
                .Select((g, i) => new ScottPlot.Tick(i + 1, g))
                .ToArray();
            chartHeatmap.Plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(ticks);
            chartHeatmap.Plot.Axes.Bottom.MajorTickStyle.Length = 0;

            // Labels and title
            chartHeatmap.Plot.YLabel("Number of Reviews");
            chartHeatmap.Plot.XLabel("Genres");

            // Remove grid for cleaner look
            chartHeatmap.Plot.HideGrid();

            // No padding beneath the bars
            chartHeatmap.Plot.Axes.Margins(bottom: 0);

            chartHeatmap.Refresh();
        }





        public void SetControversialGames(string[] games, int[] positive, int[] negative, int[] totalReviews, decimal[] controversyScores)
        {
            if (gridControversial == null) return;
            if (games.Length != positive.Length) return;
            if (positive.Length != negative.Length) return;

            int pos, neg, total;
            decimal ratio;

            gridControversial.Rows.Clear();

            for (int i = 0; i < games.Length; i++)
            {
                pos = positive[i];
                neg = negative[i];
                total = totalReviews[i];
                ratio = controversyScores[i];

                gridControversial.Rows.Add(games[i], pos, neg, total, ratio);
            }

        }

        public void SetLowConfidence(int[] ids, string[] games, string[] reviews, double[] confidence)
        {
            if (gridLowConf == null) return;
            if (games.Length != ids.Length) return;
            if (reviews.Length != confidence.Length) return;
            if (games.Length != confidence.Length) return;

            gridLowConf.Rows.Clear();

            for (int i = 0; i < games.Length; i++)
                gridLowConf.Rows.Add(ids[i], games[i], reviews[i], confidence[i]);
            
        }


        public void BtnApplyFilters_Click(object sender, EventArgs e)
        {
            DateTime? from = dtFrom.Checked ? dtFrom.Value.Date : (DateTime?)null;
            DateTime? to = dtTo.Checked ? dtTo.Value.Date : (DateTime?)null;
            string template = cboTemplate.SelectedItem?.ToString() ?? "Weekly";

            int? selectedGameId = (cboGame.SelectedItem as Game)?.GameId;

            RefreshReports(from, to, template, selectedGameId);
        }


        private void BtnExportPdf_Click(object sender, EventArgs e)
        {
            var saveDialog = new SaveFileDialog
            {
                Filter = "PDF File|*.pdf",
                Title = "Save PDF File"
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                using (var writer = new PdfWriter(saveDialog.FileName))
                using (var pdf = new PdfDocument(writer))
                using (var doc = new Document(pdf))
                {
                    // === Cover Page ===
                    doc.Add(new Paragraph("Sentiment & Review Report")
                        .SetFontSize(20).SetBold().SetTextAlignment(TextAlignment.CENTER));
                    doc.Add(new Paragraph($"{dtFrom.Value:MMM yyyy} – {dtTo.Value:MMM yyyy}")
                        .SetFontSize(14).SetTextAlignment(TextAlignment.CENTER));
                    doc.Add(new Paragraph($"Generated: {DateTime.Now:yyyy-MM-dd}"));
                    doc.Add(new Paragraph($"Template: {cboTemplate.Text}"));
                    // doc.Add(new AreaBreak());
                    AddEmptyLines(doc);

                    // === Section 1: Sentiment Trend Chart ===
                    if (chartTrend != null && tabControl.Contains(tabTrend))
                    {
                        string chartPath = Path.Combine(Path.GetTempPath(), "trend.png");
                        chartTrend.Plot.SavePng(chartPath, 600, 400);

                        var img = new iText.Layout.Element.Image(
                            iText.IO.Image.ImageDataFactory.Create(chartPath));
                        img.SetAutoScale(true);

                        doc.Add(new Paragraph("Section 1: Sentiment Trend")
                            .SetFontSize(16).SetBold());
                        doc.Add(img);
                        AddEmptyLines(doc);
                    }

                    // === Section 2: Top Games ===
                    if (chartTopGames != null && tabControl.Contains(tabTopGames))
                    {
                        string chartPath = Path.Combine(Path.GetTempPath(), "topGames.png");
                        chartTopGames.Plot.SavePng(chartPath, 600, 400);

                        var img = new iText.Layout.Element.Image(
                            iText.IO.Image.ImageDataFactory.Create(chartPath));
                        img.SetAutoScale(true);

                        doc.Add(new Paragraph("Section 2: Top Games by Positive Sentiment")
                            .SetFontSize(16).SetBold());
                        doc.Add(img);   
                        AddEmptyLines(doc);
                    }

                    // === Section 3: Recommendation Breakdown (Pie) ===
                    if (chartReco != null && tabControl.Contains(tabReco))
                    {
                        string chartPath = Path.Combine(Path.GetTempPath(), "reco.png");
                        chartReco.Plot.SavePng(chartPath, 500, 400);

                        var img = new iText.Layout.Element.Image(
                            iText.IO.Image.ImageDataFactory.Create(chartPath));
                        img.SetAutoScale(true);

                        doc.Add(new Paragraph("Section 3: Recommendation Breakdown")
                            .SetFontSize(16).SetBold());
                        doc.Add(img);
                        AddEmptyLines(doc);
                    }

                    // === Section 4: Genre Sentiment Heatmap ===
                    if (chartHeatmap != null && tabControl.Contains(tabGenre))
                    {
                        string chartPath = Path.Combine(Path.GetTempPath(), "heatmap.png");
                        chartHeatmap.Plot.SavePng(chartPath, 600, 400);

                        var img = new iText.Layout.Element.Image(
                            iText.IO.Image.ImageDataFactory.Create(chartPath));
                        img.SetAutoScale(true);

                        doc.Add(new Paragraph("Section 4: Genre Sentiment Heatmap")
                            .SetFontSize(16).SetBold());
                        doc.Add(img);
                        AddEmptyLines(doc);
                    }

                    // === Section 5: Controversial Games Table ===
                    if (gridControversial != null && tabControl.Contains(tabControversial))
                    {
                        doc.Add(new Paragraph("Section 5: Controversial Games")
                            .SetFontSize(16).SetBold());
                        AddGridToPdfTable(doc, gridControversial);
                        AddEmptyLines(doc);
                    }

                    // === Section 6: Low Confidence Reviews ===
                    if (gridLowConf != null && tabControl.Contains(gridLowConf))
                    {
                        doc.Add(new Paragraph("Section 6: Low Confidence Reviews")
                            .SetFontSize(16).SetBold());
                        AddGridToPdfTable(doc, gridLowConf);
                    }

                    doc.Close();
                }

                MessageBox.Show("PDF exported successfully!");
            }
        }

        private void AddEmptyLines(Document doc)
        {
            doc.Add(new Paragraph(" "));
            doc.Add(new Paragraph(" "));
        }

        private void AddGridToPdfTable(Document doc, KryptonDataGridView grid)
        {
            if (grid == null || grid.Columns.Count == 0) return;

            // === Calculate relative column widths ===
            // You can customize this: e.g., wider for text, narrower for numbers
            float[] colWidths = grid.Columns
                .Cast<DataGridViewColumn>()
                .Select(c => c.ValueType == typeof(int) || c.ValueType == typeof(double) ? 1f : 2f)
                .ToArray();

            var table = new Table(UnitValue.CreatePercentArray(colWidths)).UseAllAvailableWidth();

            // === Header row ===
            foreach (DataGridViewColumn col in grid.Columns)
            {
                var headerCell = new Cell()
                    .Add(new Paragraph(col.HeaderText).SetBold())
                    .SetBackgroundColor(iText.Kernel.Colors.ColorConstants.LIGHT_GRAY)
                    .SetTextAlignment(TextAlignment.CENTER);
                table.AddHeaderCell(headerCell);
            }

            // === Data rows with alternating shading ===
            bool shade = false;
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.IsNewRow) continue;

                foreach (DataGridViewCell cell in row.Cells)
                {
                    string text = cell.Value?.ToString() ?? "";

                    var cellElement = new Paragraph(text);

                    // Align numbers to the right
                    bool isNumeric = double.TryParse(text, out _);
                    var pdfCell = new Cell()
                        .Add(cellElement)
                        .SetTextAlignment(isNumeric ? TextAlignment.RIGHT : TextAlignment.LEFT);

                    // Alternating row shading
                    if (shade)
                        pdfCell.SetBackgroundColor(iText.Kernel.Colors.ColorConstants.WHITE);
                    else
                        pdfCell.SetBackgroundColor(iText.Kernel.Colors.ColorConstants.LIGHT_GRAY);

                    table.AddCell(pdfCell);
                }

                shade = !shade; // toggle shading for next row
            }

            doc.Add(table);
        }



        private void BtnExportExcel_Click(object sender, EventArgs e)
        {
            var saveDialog = new SaveFileDialog
            {
                Filter = "Excel Workbook|*.xlsx",
                Title = "Save Excel File"
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                using (var wb = new XLWorkbook())
                {
                    // === Cover Sheet ===
                    var wsCover = wb.Worksheets.Add("Cover");
                    wsCover.Cell(1, 1).Value = "Sentiment & Review Report";
                    wsCover.Cell(2, 1).Value = $"{dtFrom.Value:MMM yyyy} – {dtTo.Value:MMM yyyy}";
                    wsCover.Cell(3, 1).Value = $"Generated: {DateTime.Now:yyyy-MM-dd}";
                    wsCover.Cell(4, 1).Value = $"Template: {cboTemplate.Text}";
                    wsCover.Columns().AdjustToContents();

                    // === Section 1: Sentiment Trend ===
                    var ws1 = wb.Worksheets.Add("Section 1 - SentimentTrend");
                    AddChartImageToSheet(ws1, chartTrend, "trend");
                    ws1.Cell(20, 1).Value = "Period";
                    ws1.Cell(20, 2).Value = "Positive";
                    ws1.Cell(20, 3).Value = "Neutral";
                    ws1.Cell(20, 4).Value = "Negative";

                    for (int i = 0; i < months.Length; i++)
                    {
                        ws1.Cell(i + 21, 1).Value = months[i];
                        ws1.Cell(i + 21, 2).Value = positive[i];
                        ws1.Cell(i + 21, 3).Value = neutral[i];
                        ws1.Cell(i + 21, 4).Value = negative[i];
                    }
                    ws1.Columns().AdjustToContents();

                    // // === Section 2: Top Games ===
                    // var ws2 = wb.Worksheets.Add("Section 2 - TopGames");
                    // AddChartImageToSheet(ws2, chartTopGames, "topGames");
                    // ws2.Cell(20, 1).Value = "Game";
                    // ws2.Cell(20, 2).Value = "Recommended";
                    // ws2.Cell(20, 3).Value = "Not Recommended";

                    // for (int i = 0; i < games.Length; i++)
                    // {
                    //     ws2.Cell(i + 21, 1).Value = games[i];
                    //     ws2.Cell(i + 21, 2).Value = recommended[i];
                    //     ws2.Cell(i + 21, 3).Value = notRecommended[i];
                    // }
                    // ws2.RangeUsed().CreateTable();
                    // ws2.Columns().AdjustToContents();

                    // === Section 3: Recommendation Breakdown ===
                    var ws3 = wb.Worksheets.Add("Section 3 - Recommendations");
                    AddChartImageToSheet(ws3, chartReco, "reco");
                    ws3.Cell(20, 1).Value = "Label";
                    ws3.Cell(20, 2).Value = "Value";

                    for (int i = 0; i < recoLabels.Length; i++)
                    {
                        ws3.Cell(i + 21, 1).Value = recoLabels[i];
                        ws3.Cell(i + 21, 2).Value = recoValues[i];
                    }
                    ws3.RangeUsed().CreateTable();
                    ws3.Columns().AdjustToContents();

                    // === Section 4: Genre Sentiment Heatmap ===
                    var ws4 = wb.Worksheets.Add("Section 4 - GenreSentiment");
                    AddChartImageToSheet(ws4, chartHeatmap, "heatmap");

                    ws4.Cell(20, 1).Value = "Genre";
                    ws4.Cell(20, 2).Value = "Positive";
                    ws4.Cell(20, 3).Value = "Neutral";
                    ws4.Cell(20, 4).Value = "Negative";

                    for (int i = 0; i < genreIntensities.GetLength(0); i++)
                    {
                        // ws4.Cell(i + 21, 1).Value = genreLabels[i];
                        ws4.Cell(i + 21, 2).Value = genreIntensities[i, 0];
                        ws4.Cell(i + 21, 3).Value = genreIntensities[i, 1];
                        ws4.Cell(i + 21, 4).Value = genreIntensities[i, 2];
                    }
                    ws4.RangeUsed().CreateTable();
                    ws4.Columns().AdjustToContents();

                    // === Section 5: Controversial Games ===
                    var ws5 = wb.Worksheets.Add("Section 5 - ControversialGames");
                    if (gridControversial != null)
                        ExportGridToSheet(gridControversial, ws5);

                    // === Section 6: Low Confidence Reviews (optional) ===
                    // var ws6 = wb.Worksheets.Add("Section 6 - LowConfidenceReviews");
                    // if (gridLowConf != null)
                    //     ExportGridToSheet(gridLowConf, ws6);

                    wb.SaveAs(saveDialog.FileName);
                }

                MessageBox.Show("Excel exported successfully!");
            }
        }


        // === Helper: Insert chart images into Excel sheets ===
        private void AddChartImageToSheet(IXLWorksheet ws, FormsPlot chart, string imageName, int row = 1, int col = 1)
        {
            if (chart == null) return;

            string chartPath = Path.Combine(Path.GetTempPath(), imageName + ".png");
            chart.Plot.SavePng(chartPath, 600, 400);

            var picture = ws.AddPicture(chartPath)
                            .MoveTo(ws.Cell(row, col))
                            .Scale(0.7); // adjust scaling
        }

        private void ExportGridToSheet(DataGridView grid, IXLWorksheet ws)
        {
            for (int col = 0; col < grid.Columns.Count; col++)
                ws.Cell(1, col + 1).Value = grid.Columns[col].HeaderText;

            for (int row = 0; row < grid.Rows.Count; row++)
            {
                for (int col = 0; col < grid.Columns.Count; col++)
                    ws.Cell(row + 2, col + 1).Value = grid.Rows[row].Cells[col].Value?.ToString();
            }

            ws.Columns().AdjustToContents();
        }



    }
}
