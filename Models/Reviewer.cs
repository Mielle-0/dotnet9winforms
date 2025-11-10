namespace it13Project.Models
{
    public class Reviewer
    {
        public int ReviewerId { get; set; }
        public string? Username { get; set; }
        public string? ProfileUrl { get; set; }
        public int TotalReviews { get; set; }
        public int TotalHelpfulnessVotes { get; set; }
    }

}
