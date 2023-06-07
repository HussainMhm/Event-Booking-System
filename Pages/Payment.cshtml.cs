using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MetaX.Model;

namespace MetaX.Pages
{
    public class PaymentModel : PageModel
    {
        private readonly MetaX.Data.MetaxDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        [BindProperty(SupportsGet = true)]
        public int EventId { get; set; }
        
        public decimal Price { get; set; }

        public string EventName { get; set; }
        
        public PaymentModel(MetaX.Data.MetaxDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task OnGetAsync()
        {
            var eventDetails = await _context.EventsTable.FirstOrDefaultAsync(e => e.EventID == EventId);
            if(eventDetails != null)
            {
                EventName = eventDetails.Title;
                Price = eventDetails.Price;
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var eventDetails = await _context.EventsTable.FirstOrDefaultAsync(e => e.EventID == EventId);
            if(eventDetails == null)
            {
                return RedirectToPage("/Index");
            }

            // Retrieve the user ID from the session
            var userIdString = _httpContextAccessor.HttpContext.Session.GetString("UserID");
            if(string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                // Handle case where the user ID is not found in the session or is not a valid integer
                return RedirectToPage("/Login");
            }

            // Create a new reservation.
            var reservation = new Reservation
            {
                EventID = EventId,
                UserID = userId, // Use the user ID retrieved from the session
                ReservationDate = DateTime.Now
            };

            // Add the new reservation to the context and save changes.
            _context.ReservationsTable.Add(reservation);
            await _context.SaveChangesAsync();

            return RedirectToPage("/User/Reservation", new { id = reservation.ReservationID });
        }
    }
}
