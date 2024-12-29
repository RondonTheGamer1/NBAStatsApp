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
    public class IndexModel : PageModel
    {
        private readonly NBAStatsApp.Data.NbaContext _context;

        public IndexModel(NBAStatsApp.Data.NbaContext context)
        {
            _context = context;
        }

        public IList<Team> Team { get; set; } = default!;

        public async Task OnGetAsync(string SearchString)
        {
            IQueryable<Team> nbaTeam = from s in _context.Teams select s;
            if (!string.IsNullOrEmpty(SearchString))
            {
                nbaTeam = nbaTeam.Where(s => s.Name.Contains(SearchString));
            }
            Team = await nbaTeam.ToListAsync();

            /*public async Task OnGetAsync()
            {
                Team = await _context.Teams.ToListAsync();

            }*/
        }
    }
}
