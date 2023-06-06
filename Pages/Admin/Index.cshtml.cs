using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetaX.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MetaX.Pages.Admin
{
    [Authorize]
    public class IndexModel : PageModel
    {

        private readonly MetaxDbContext _context;

        public int EventCount { get; set; }
        public int UserCount { get; set; }
        public int ReservationCount { get; set; }
        public int ReviewCount { get; set; }
        public decimal AverageRating { get; set; }
        

        public IndexModel(MetaxDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            EventCount = _context.EventsTable.Count();
            UserCount = _context.UsersTable.Count();
            ReservationCount = _context.ReservationsTable.Count();
            ReviewCount = _context.ReviewsTable.Count();
            AverageRating = (decimal)_context.ReviewsTable.Average(e => e.Rating);

        }
    }
}


