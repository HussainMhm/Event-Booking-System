using MetaX.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

        public void OnGet()
        {
            ReviewListing = _db.ReviewsTable.ToList();
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