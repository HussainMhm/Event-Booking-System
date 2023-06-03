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

        [BindProperty(SupportsGet = true)]
        public string UserName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string EventName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Status { get; set; }

        public bool IsStatusActive => Status == "Aktif";
        public bool IsStatusFinished => Status == "Bitti";

        public async Task<IActionResult> OnGetAsync()
        {
            var query = _context.ReservationsTable.AsQueryable();

            if (!string.IsNullOrEmpty(UserName))
            {
                query = query.Where(r => r.User.Name.Contains(UserName));
            }

            if (!string.IsNullOrEmpty(EventName))
            {
                query = query.Where(r => r.Event.Title.Contains(EventName));
            }

            if (!string.IsNullOrEmpty(Status))
            {
                if (Status == "Aktif")
                {
                    query = query.Where(r => r.Event.Date >= DateTime.Today);
                }
                else if (Status == "Bitti")
                {
                    query = query.Where(r => r.Event.Date < DateTime.Today);
                }
            }

            Reservations = await query
                .Include(r => r.User)
                .Include(r => r.Event)
                .ToListAsync();

            return Page();
        }

        public IActionResult OnPostDelete(int reservationId)
        {
            var reservation = _context.ReservationsTable.Find(reservationId);
            if (reservation != null)
            {
                _context.ReservationsTable.Remove(reservation);
                _context.SaveChanges();
            }
            return RedirectToPage();
        }
    }


}
