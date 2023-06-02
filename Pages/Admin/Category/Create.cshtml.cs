using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MetaX.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MetaX.Pages.Admin.Category
{
    public class CreateModel : PageModel
    {
        private readonly MetaxDbContext _db;

        public CreateModel(MetaxDbContext db)
        {
            _db = db;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            public string Name { get; set; }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var category = new Model.Category
            {
                Name = Input.Name
            };

            _db.CategoriesTable.Add(category);
            _db.SaveChanges();

            return RedirectToPage("./Index");
        }
    }
}