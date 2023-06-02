using MetaX.Data;
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
    }
}
