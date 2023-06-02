using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MetaX.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MetaX.Model;

namespace MetaX.Pages.Admin.Event
{
    public class CreateModel : PageModel
    {
        private readonly MetaxDbContext _db;

        [BindProperty]
        public Model.Event Event { get; set; }

        public CreateModel(MetaxDbContext db)
        {
            _db = db;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.EventsTable.Add(Event);
            _db.SaveChanges();

            return RedirectToPage("Index");
        }
    }
}
