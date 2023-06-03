using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MetaX.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MetaX.Model;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Event = await _db.EventsTable.FindAsync(id);
            if (Event == null)
            {
                return NotFound();
            }

            ViewData["Categories"] = await _db.CategoriesTable.ToListAsync();
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
