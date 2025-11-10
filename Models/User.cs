namespace it13Project.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }   // Admin, Analyst, Marketing
        public string? PasswordHash { get; set; }
    }
}
