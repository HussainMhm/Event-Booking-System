using MetaX.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MetaX.Pages.Admin.Review
{
    public class IndexModel : PageModel
    {
        private readonly MetaxDbContext _db;

        public List<Model.Review> ReviewListing;

        public IndexModel(MetaxDbContext db)
        {
            _db = db;
        }

        public void OnGet(string eventName, int? rating)
        {
            IQueryable<Model.Review> query = _db.ReviewsTable
                .Include(r => r.Event)
                .Include(r => r.User)
                .AsQueryable();

            if (!string.IsNullOrEmpty(eventName))
                query = query.Where(r => r.Event.Title.Contains(eventName));

            if (rating.HasValue)
                query = query.Where(r => r.Rating == rating.Value);

            ReviewListing = query.ToList();
        }

        public IActionResult OnPostDelete(int reviewId)
        {
            var review = _db.ReviewsTable.Find(reviewId);
            if (review != null)
            {
                _db.ReviewsTable.Remove(review);
                _db.SaveChanges();
            }
            return RedirectToPage();
        }
    }
}
