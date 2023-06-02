using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetaX.Data;
using MetaX.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MetaX.Pages.Admin.Category
{
    public class UpdateModel : PageModel
    {
        private readonly MetaxDbContext _db;

        [BindProperty]
        public Model.Category Category { get; set; }

        public UpdateModel(MetaxDbContext db)
        {
            _db = db;
        }

        public IActionResult OnGet(int id)
        {
            Category = _db.CategoriesTable.Find(id);
            if (Category == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.CategoriesTable.Update(Category);
            _db.SaveChanges();

            return RedirectToPage("./Index");
        }
    }
}
