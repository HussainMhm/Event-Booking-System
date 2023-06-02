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
    }
}
