using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NBAStatsApp.Data;
using NBAStatsApp.Models;

namespace NBAStatsApp.Pages.Teams
{
    public class DetailsModel : PageModel
    {
        private readonly NBAStatsApp.Data.NbaContext _context;

        public DetailsModel(NBAStatsApp.Data.NbaContext context)
        {
            _context = context;
        }

        public Team Team { get; set; } = default!;
        public List<Player> Players { get; set; }

        public async Task<IActionResult> OnGetAsync(int TeamId)
        {
            Team = await _context.Teams
        .Include(t => t.Players) // טוען את השחקנים יחד עם הקבוצה
        .FirstOrDefaultAsync(t => t.Id == TeamId);

            if (Team == null)
            {
                return NotFound();
            }

            Players = Team.Players.ToList(); // מוודאים שהשחקנים נמצאים ב-Players

            return Page();


            /*if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams.FirstOrDefaultAsync(m => m.Id == id);
            if (team == null)
            {
                return NotFound();
            }
            else
            {
                Team = team;
            }
            return Page();*/
        }
    }
}
