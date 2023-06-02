using MetaX.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace MetaX.Pages.Admin.Event
{
    public class IndexModel : PageModel
    {
        private readonly MetaxDbContext _db;

        public List<Model.Event> EventListing { get; set; }

        public IndexModel(MetaxDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            EventListing = _db.EventsTable.ToList();
        }
        public IActionResult OnPostDelete(int eventId)
        {
            var _event = _db.EventsTable.Find(eventId);
            if (_event != null)
            {
                _db.EventsTable.Remove(_event);
                _db.SaveChanges();
            }
            return RedirectToPage();
        }
    }
}