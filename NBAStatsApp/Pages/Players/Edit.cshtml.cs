using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NBAStatsApp.Data;
using NBAStatsApp.Models;

namespace NBAStatsApp.Pages.Players
{
    public class EditModel : PageModel
    {
        private readonly NBAStatsApp.Data.NbaContext _context;

        public EditModel(NBAStatsApp.Data.NbaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Player Player { get; set; } = default!;

        [BindProperty]
        public IFormFile? ImageFile { get; set; } // שדה להעלאת התמונה

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Players.FirstOrDefaultAsync(m => m.Id == id);
            if (player == null)
            {
                return NotFound();
            }
            Player = player;
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // טיפול בהעלאת תמונה
            if (ImageFile != null)
            {
                // שמירת התמונה בשרת
                var fileName = Path.GetFileName(ImageFile.FileName);
                var filePath = Path.Combine("wwwroot/images", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                // עדכון הנתיב בתמונה במודל
                Player.ImagePlayer = "/images/" + fileName;
            }

            _context.Attach(Player).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(Player.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PlayerExists(int id)
        {
            return _context.Players.Any(e => e.Id == id);
        }
    }
}
