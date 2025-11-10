namespace it13Project.Models
{
    public class Alert
    {
        public int AlertId { get; set; }
        public int GameId { get; set; }
        public string? AlertType { get; set; }
        public string? AlertMessage { get; set; }
        public DateTime? AlertDate { get; set; }
        public string? Status { get; set; } // Open / Assigned / Resolved
        public int? AssignedTo { get; set; }
    }
}
