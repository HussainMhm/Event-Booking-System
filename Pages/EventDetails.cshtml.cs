using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MetaX.Model;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MetaX.Pages
{
    public class EventDetailsModel : PageModel
    {
        private readonly MetaX.Data.MetaxDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EventDetailsModel(MetaX.Data.MetaxDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public Model.Event Event { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Event = await _context.EventsTable.FirstOrDefaultAsync(e => e.EventID == id);

            if (Event == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Model.Review review)
        {
            if (!ModelState.IsValid)
            {
                // If the review data is not valid, return the page with the error messages
                return Page();
            }

            int currentUserId = GetCurrentUserId();

            // Set the event ID and user ID for the review
            review.EventID = Event.EventID;
            review.UserID = currentUserId;

            // Add the review to the ReviewsTable and save changes to the database
            _context.ReviewsTable.Add(review);
            await _context.SaveChangesAsync();

            return RedirectToPage("/EventDetails", new { id = Event.EventID });
        }

        private int GetCurrentUserId()
        {
            // Retrieve the current user ID from the HttpContext
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return !string.IsNullOrEmpty(userId) ? int.Parse(userId) : 0;
        }
    }
}
