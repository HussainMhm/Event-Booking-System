using System.Linq;
using MetaX.Data;
using MetaX.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MetaX.Pages.Admin.Event
{
    public class DeleteModel : PageModel
    {
        private readonly MetaxDbContext _db;

        [BindProperty]
        public Model.Event Event { get; set; }

        public DeleteModel(MetaxDbContext db)
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

        public IActionResult OnPost(int id)
        {
            var eventToDelete = _db.EventsTable.Find(id);
            if (eventToDelete == null)
            {
                return NotFound();
            }

            _db.EventsTable.Remove(eventToDelete);
            _db.SaveChanges();

            return RedirectToPage("./Index");
        }
    }
}
