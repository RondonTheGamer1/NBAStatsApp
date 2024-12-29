using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NBAStatsApp.Data;
using NBAStatsApp.Models;

namespace NBAStatsApp.Pages.Players
{
    public class IndexModel : PageModel
    {
        private readonly NBAStatsApp.Data.NbaContext _context;

        public IndexModel(NBAStatsApp.Data.NbaContext context)
        {
            _context = context;
        }

        public List<Player> Player { get; set; } = default!;
        public string? SearchString { get; set; }

        public async Task OnGetAsync(string? searchString)
        {
            var playersQuery = _context.Players.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                playersQuery = playersQuery.Where(p => p.Name.Contains(searchString));
            }

            Player = await playersQuery
                .Include(p => p.Team) // טוען את הקבוצה של השחקן
                .ToListAsync();
        }
    }
}
