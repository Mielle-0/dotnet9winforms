namespace it13Project.Models
{
    public class SentimentDto
    {
        public int SentimentId { get; set; }
        public string AppName { get; set; }
        public string ReviewText { get; set; }
        public string PredictedSentiment { get; set; }
        public decimal ConfidenceScore { get; set; }
        public DateTime ReviewDate { get; set; }
    }


}