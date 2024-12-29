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
    public class DetailsModel : PageModel
    {
        private readonly NBAStatsApp.Data.NbaContext _context;

        public DetailsModel(NBAStatsApp.Data.NbaContext context)
        {
            _context = context;
        }

        public Game Game { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // טוען את המשחק כולל את הקבוצות
            var game = await _context.Games
                .Include(g => g.HomeTeam)  // טוען את קבוצה הבית
                .Include(g => g.AwayTeam)  // טוען את קבוצה החוץ
                .FirstOrDefaultAsync(m => m.Id == id);

            if (game == null)
            {
                return NotFound();
            }
            else
            {
                Game = game;
            }

            return Page();
        }
    }
}
