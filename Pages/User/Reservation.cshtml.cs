using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetaX.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace MetaX.Pages.Reservations
{
    public class IndexModel : PageModel
    {
        private readonly MetaxDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public List<Model.Reservation> Reservations { get; set; }

        public IndexModel(MetaxDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }

            public async Task OnGetAsync()
            {
                var userId = _httpContextAccessor.HttpContext.Session.GetString("UserID");

            Reservations = await _db.ReservationsTable
                .Include(r => r.User)  // Include the related User data
                .Include(r => r.Event)  // Include the related Event data
                .Where(r => r.UserID == int.Parse(userId))
                .OrderBy(r => r.ReservationDate)
                .ToListAsync();
        }
        public IActionResult OnPostDelete(int reservationId)
        {
            var _event = _db.ReservationsTable.Find(reservationId);
            if (_event != null)
            {
                _db.ReservationsTable.Remove(_event);
                _db.SaveChanges();
            }
            return RedirectToPage();
        }
    }
}
