using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MetaX.Model;
using MetaX.Data;

namespace MetaX.Pages
{
    public class SignupModel : PageModel
    {
        private readonly MetaxDbContext _db;

        public SignupModel(MetaxDbContext context)
        {
            _db = context;
        }

        [BindProperty]
        public Model.User User { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.UsersTable.Add(User);
            await _db.SaveChangesAsync();

            return RedirectToPage("/Login");
        }
    }
}

