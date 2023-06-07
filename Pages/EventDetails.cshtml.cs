using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MetaX.Pages
{
    public class EventDetailsModel : PageModel
    {
        private readonly MetaX.Data.MetaxDbContext _context;

        public EventDetailsModel(MetaX.Data.MetaxDbContext context)
        {
            _context = context;
        }

        public Model.Event Event { get; set; }

        public async Task OnGetAsync(int id)
        {
            Event = await _context.EventsTable.FirstOrDefaultAsync(e => e.EventID == id);
        }
    }
}
