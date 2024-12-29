using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NBAStatsApp.Data;
using NBAStatsApp.Models;

namespace NBAStatsApp.Pages.Games
{
    public class IndexModel : PageModel
    {
        private readonly NBAStatsApp.Data.NbaContext _context;

        public IndexModel(NBAStatsApp.Data.NbaContext context)
        {
            _context = context;
        }

        public IList<Game> Game { get; set; } = default!;
        public string? SearchString { get; set; }

        public async Task OnGetAsync(string? searchString)
        {
            var gamesQuery = _context.Games
                .Include(g => g.AwayTeam)
                .Include(g => g.HomeTeam)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                // כאן אפשר לחפש לפי תאריך בצורה גמישה
                DateTime searchDate;
                if (DateTime.TryParse(searchString, out searchDate))
                {
                    // חיפוש לפי תאריך מלא (יום, חודש ושנה)
                    gamesQuery = gamesQuery.Where(g => g.GameDate.Date == searchDate.Date);
                }
                else
                {
                    // אם לא מצליחים להמיר את החיפוש לתאריך, אפשר להוסיף חיפוש לפי פרמטרים אחרים
                    gamesQuery = gamesQuery.Where(g => g.HomeTeam.Name.Contains(searchString) || g.AwayTeam.Name.Contains(searchString));
                }
            }

            Game = await gamesQuery.ToListAsync();
        }
    }
}
