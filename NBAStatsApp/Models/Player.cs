namespace NBAStatsApp.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double PointsPerGame { get; set; }
        public double AssistsPerGame { get; set; }
        public double ReboundsPerGame { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
        public string ImagePlayer { get; set; }
        public ICollection<Game> Games { get; set; } // קשר של רבים-ל-רבים עם משחקים
    }
}
