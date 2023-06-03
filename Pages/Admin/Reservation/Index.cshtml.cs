using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MetaX.Data;
using MetaX.Model;

namespace MetaX.Pages.Admin.Reservation
{
    public class IndexModel : PageModel
    {
        private readonly MetaxDbContext _context;

        public IndexModel(MetaxDbContext context)
        {
            _context = context;
        }

        public IList<Model.Reservation> Reservations { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Reservations = await _context.ReservationsTable
                .Include(r => r.User)
                .Include(r => r.Event)
                .ToListAsync();

            return Page();
        }
    }
}
