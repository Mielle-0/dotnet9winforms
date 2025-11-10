namespace it13Project.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int GameId { get; set; }
        public string? ReviewText { get; set; }
        public string? ReviewScore { get; set; }   // Recommended / Not Recommended
        public string? PredictedSentiment { get; set; }  // Positive / Negative / Neutral
        public decimal? ConfidenceScore { get; set; }
        public int ReviewVotes { get; set; }
        public DateTime? ReviewDate { get; set; }
        public int ReviewerId { get; set; }
    }

}
