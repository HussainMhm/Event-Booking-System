using MetaX.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetaX.Pages
{
    public class IndexModel : PageModel
    {
        private readonly MetaxDbContext _db;

        public List<Model.Event> EarlyEvents { get; set; }
        public List<string> PopulerKategoriIds { get; set; }

        public IndexModel(MetaxDbContext db)
        {
            _db = db;
        }

        public async Task OnGetAsync()
        {
            EarlyEvents = await _db.EventsTable
                .OrderBy(e => e.Date)
                .Take(3)
                .ToListAsync();

            PopulerKategoriIds = await _db.EventsTable
                .GroupBy(e => e.Category)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .Take(4)
                .ToListAsync();
        }
    }
}
