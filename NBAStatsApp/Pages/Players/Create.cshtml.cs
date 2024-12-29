using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting.Internal;
using NBAStatsApp.Data;
using NBAStatsApp.Models;

namespace NBAStatsApp.Pages.Players
{
    public class CreateModel : PageModel
    {
        private readonly NBAStatsApp.Data.NbaContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public CreateModel(NBAStatsApp.Data.NbaContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult OnGet()
        {
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Player Player { get; set; } = default!;

        [BindProperty]
        public IFormFile? ImageFile { get; set; } // שדה עבור קובץ התמונה

        // For more information, see https://aka.ms/RazorPagesCRUD.

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (ImageFile != null)
            {
                // שמירת התמונה בשרת
                var fileName = Path.GetFileName(ImageFile.FileName);
                var filePath = Path.Combine("wwwroot/images", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                // שמירת הנתיב במודל
                Player.ImagePlayer = "/images/" + fileName;
            }

            _context.Players.Add(Player);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

    }
}
