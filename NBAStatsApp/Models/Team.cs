namespace NBAStatsApp.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Coach { get; set; }
        public string ImageTeam { get; set; }

        public ICollection<Game> HomeGames { get; set; } // קשר למשחקי בית
        public ICollection<Game> AwayGames { get; set; } // קשר למשחקי חוץ
        public ICollection<Player> Players { get; set; }//קשר בין השחקנים לקבוצה מסוימת
    }
}
