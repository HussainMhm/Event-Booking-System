using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetaX.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MetaX.Pages.Admin.Category
{
    [Area("Admin")]
    public class DeleteModel : PageModel
    {
        private readonly MetaxDbContext _db;

        public DeleteModel(MetaxDbContext db)
        {
            _db = db;
        }

        [BindProperty(SupportsGet = true)]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public IActionResult OnGet()
        {
            var category = _db.CategoriesTable.Find(CategoryId);
            if (category == null)
            {
                return NotFound();
            }

            CategoryName = category.Name;

            return Page();
        }

        public IActionResult OnPost()
        {
            var category = _db.CategoriesTable.Find(CategoryId);
            if (category == null)
            {
                return NotFound();
            }

            _db.CategoriesTable.Remove(category);
            _db.SaveChanges();

            return RedirectToPage("./Index");
        }
    }
}
