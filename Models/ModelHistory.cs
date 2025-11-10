namespace it13Project.Models
{
    public class ModelHistory
    {
        public int ModelId { get; set; }
        public string? ModelName { get; set; }
        public string? Version { get; set; }
        public DateTime? DateUploaded { get; set; }
        public decimal? Precision { get; set; }
        public decimal? Recall { get; set; }
        public decimal? F1Score { get; set; }
    }
}
