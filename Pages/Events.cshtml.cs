using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetaX.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MetaX.Pages
{
    public class EventsModel : PageModel
    {
        private readonly MetaX.Data.MetaxDbContext _context;

        public EventsModel(MetaX.Data.MetaxDbContext context)
        {
            _context = context;
        }

        public IList<Model.Event> Events { get; set; }

        public IList<Category> Categories { get; set; }

        public int TotalPages { get; set; }

        public async Task OnGetAsync(int? pageNumber, string category)
        {
            var pageSize = 9;

            IQueryable<Model.Event> eventsQuery = _context.EventsTable.AsQueryable();

            // Fetch categories from the database
            Categories = await _context.CategoriesTable.ToListAsync();

            // Filter events by category if a category is selected
            if (!string.IsNullOrEmpty(category))
            {
                eventsQuery = eventsQuery.Where(e => e.Category == category);
            }

            // Calculate total pages
            var count = await eventsQuery.CountAsync();
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            // Paginate events
            if (pageNumber.HasValue && pageNumber.Value > 1)
            {
                eventsQuery = eventsQuery.Skip((pageNumber.Value - 1) * pageSize);
            }

            eventsQuery = eventsQuery.Take(pageSize);

            Events = await eventsQuery.ToListAsync();
        }
    }
}
