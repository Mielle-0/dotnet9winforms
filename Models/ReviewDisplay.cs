namespace it13Project
{
    public class ReviewDisplay
    {
        public int ReviewId { get; set; }
        public string AppName { get; set; }
        public string ReviewText { get; set; }
        public decimal? ReviewScore { get; set; }
        public bool? Recommendation { get; set; }
        public string Sentiment { get; set; }
        public decimal? Confidence { get; set; }
        public DateTime? ReviewDate { get; set; }
    }

    public class PagedResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

    }
}