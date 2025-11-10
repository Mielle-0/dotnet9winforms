namespace it13Project.Models
{
    public class SentimentSummary
    {
        public int SummaryId { get; set; }
        public int GameId { get; set; }
        public DateTime? DateRangeStart { get; set; }
        public DateTime? DateRangeEnd { get; set; }
        public int PositiveCount { get; set; }
        public int NegativeCount { get; set; }
        public int NeutralCount { get; set; }
        public decimal? AvgConfidence { get; set; }
        public string? TrendDirection { get; set; } // Up / Down / Stable
    }
}
