using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetaX.Data;
using MetaX.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MetaX.Pages
{
    public class ReviewModel : PageModel
    {
        private readonly MetaxDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReviewModel(MetaxDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public Model.Review Review { get; set; }

        public List<Reservation> Reservations { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userIdString = _httpContextAccessor.HttpContext.Session.GetString("UserID");
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                // Handle invalid or missing user ID
                return BadRequest();
            }

            // Get the reservations for the logged-in user
            Reservations = await _context.ReservationsTable
                .Include(r => r.Event)
                .Where(r => r.UserID == userId)
                .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // If the review data is not valid, return the page with the error messages
                return Page();
            }

            var userIdString = _httpContextAccessor.HttpContext.Session.GetString("UserID");
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                // Handle invalid or missing user ID
                return BadRequest();
            }

            Review.UserID = userId;

            _context.ReviewsTable.Add(Review);
            await _context.SaveChangesAsync();

            return RedirectToPage("/User/Review");
        }
    }
}
