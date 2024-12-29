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
    public class DetailsModel : PageModel
    {
        private readonly NBAStatsApp.Data.NbaContext _context;

        public DetailsModel(NBAStatsApp.Data.NbaContext context)
        {
            _context = context;
        }

        public Player Player { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Player = await _context.Players
                .Include(p => p.Team)  // טוען את הקבוצה של השחקן
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Player == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
