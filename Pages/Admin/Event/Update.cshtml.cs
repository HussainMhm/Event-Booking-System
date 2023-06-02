using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MetaX.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MetaX.Pages.Admin.Event
{
    public class UpdateModel : PageModel
    {
        private readonly MetaxDbContext _db;

        [BindProperty]
        public Model.Event Event { get; set; }

        public UpdateModel(MetaxDbContext db)
        {
            _db = db;
        }

        public IActionResult OnGet(int id)
        {
            Event = _db.EventsTable.Find(id);
            if (Event == null)
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

            _db.EventsTable.Update(Event);
            _db.SaveChanges();

            return RedirectToPage("/Admin/Event/Index");
        }
    }
}
