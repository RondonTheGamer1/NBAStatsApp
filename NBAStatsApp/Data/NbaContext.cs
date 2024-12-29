using Microsoft.EntityFrameworkCore;
using NBAStatsApp.Models;

namespace NBAStatsApp.Data
{
    public class NbaContext : DbContext
    {
        public NbaContext(DbContextOptions<NbaContext> options)
            : base(options)
        {
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Game> Games { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // קשר בין HomeTeam ו-Game
            modelBuilder.Entity<Game>()
                .HasOne(g => g.HomeTeam)
                .WithMany(t => t.HomeGames)
                .HasForeignKey(g => g.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict);  // אפשר לבחור בין Cascade או Restrict

            // קשר בין AwayTeam ו-Game
            modelBuilder.Entity<Game>()
                .HasOne(g => g.AwayTeam)
                .WithMany(t => t.AwayGames)
                .HasForeignKey(g => g.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Player>()
           .HasOne(p => p.Team)
           .WithMany(t => t.Players)
           .HasForeignKey(p => p.TeamId)
           .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Team>()
            .HasMany(t => t.Players)       // קבוצה מכילה מספר שחקנים
            .WithOne(p => p.Team)          // שחקן שייך לקבוצה אחת
            .HasForeignKey(p => p.TeamId)  // המפתח הזר בטבלת Players
            .OnDelete(DeleteBehavior.Cascade); // מחיקת קבוצה תגרור מחיקת השחקנים שלה

        }
    }
}
