namespace it13Project.Models
{
    public class Game
    {
        public int GameId { get; set; }
        public string? AppId { get; set; }
        public string? AppName { get; set; }
        public string? Genre { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? Developer { get; set; }
        public string? Publisher { get; set; }
    }

}
