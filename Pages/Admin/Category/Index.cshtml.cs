using MetaX.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MetaX.Pages.Admin.Category
{
    public class IndexModel : PageModel
    {
        private readonly MetaxDbContext _db;

        public List<Model.Category> CategoryListing;

        public IndexModel(MetaxDbContext db)
        {
           _db = db;
        }

        public void OnGet()
        {
            CategoryListing = _db.CategoriesTable.ToList();
        }
    }
}
