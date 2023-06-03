using System.Collections.Generic;
using System.Linq;
using MetaX.Data;
using MetaX.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MetaX.Pages.Admin.Event
{
    public class IndexModel : PageModel
    {
        private readonly MetaxDbContext _db;

        [BindProperty(SupportsGet = true)]
        public string SearchInput { get; set; }

        [BindProperty(SupportsGet = true)]
        public string LocationInput { get; set; }

        public List<Model.Event> EventListing { get; set; }
        public List<string> EventLocations { get; set; }

        public IndexModel(MetaxDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            EventListing = _db.EventsTable.ToList();
            EventLocations = _db.EventsTable.Select(e => e.Location).Distinct().ToList();
        }

        public IActionResult OnPost()
        {
            var events = _db.EventsTable.AsQueryable();

            // Perform search based on event name
            if (!string.IsNullOrEmpty(SearchInput))
            {
                events = events.Where(e => e.Title.Contains(SearchInput));
            }

            // Perform filtering based on location
            if (!string.IsNullOrEmpty(LocationInput) && LocationInput != "All Locations")
            {
                events = events.Where(e => e.Location == LocationInput);
            }

            EventListing = events.ToList();
            EventLocations = _db.EventsTable.Select(e => e.Location).Distinct().ToList();

            return Page();
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
