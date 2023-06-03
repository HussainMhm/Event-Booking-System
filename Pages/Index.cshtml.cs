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

        public List<Model.Review> PopulerReviews { get; set; }
        public List<Model.Event> EarlyEvents { get; set; }
        public List<string> PopulerKategoriIds { get; set; }
        

        
        public IndexModel(MetaxDbContext db)
        {
            _db = db;
        }

        public async Task OnGetAsync()
        {
            PopulerReviews = await _db.ReviewsTable
                .OrderByDescending(r => r.Rating)
                .Take(2)
                .ToListAsync();

            EarlyEvents = await _db.EventsTable
                .Where(e => e.Date.Date >= DateTime.Now.Date)
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
