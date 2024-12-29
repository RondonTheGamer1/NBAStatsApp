using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NBAStatsApp.Data;
using NBAStatsApp.Models;

namespace NBAStatsApp.Pages.Games
{
    public class CreateModel : PageModel
    {
        private readonly NBAStatsApp.Data.NbaContext _context;

        public CreateModel(NBAStatsApp.Data.NbaContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["AwayTeamId"] = new SelectList(_context.Teams, "Id", "Name");
        ViewData["HomeTeamId"] = new SelectList(_context.Teams, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Game Game { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Games.Add(Game);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
