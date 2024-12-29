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
    public class DeleteModel : PageModel
    {
        private readonly NBAStatsApp.Data.NbaContext _context;

        public DeleteModel(NBAStatsApp.Data.NbaContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (Player == null)
            {
                return NotFound();
            }

            var playerToDelete = await _context.Players.FindAsync(id);
            if (playerToDelete != null)
            {
                _context.Players.Remove(playerToDelete);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

