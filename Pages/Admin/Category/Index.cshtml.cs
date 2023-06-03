using MetaX.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MetaX.Pages.Admin.Category
{
    public class IndexModel : PageModel
    {
        private readonly MetaxDbContext _db;

        public List<Model.Category> CategoryListing { get; set; }

        [BindProperty]
        public string SearchInput { get; set; }

        public IndexModel(MetaxDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            CategoryListing = _db.CategoriesTable.ToList();
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(SearchInput))
            {
                CategoryListing = _db.CategoriesTable.ToList();
            }
            else
            {
                CategoryListing = _db.CategoriesTable.Where(c => EF.Functions.Like(c.Name, $"%{SearchInput}%")).ToList();
            }
            return Page();
        }

        public IActionResult OnPostDelete(int categoryId)
        {
            var category = _db.CategoriesTable.Find(categoryId);
            if (category != null)
            {
                _db.CategoriesTable.Remove(category);
                _db.SaveChanges();
            }
            return RedirectToPage();
        }
    }
}
