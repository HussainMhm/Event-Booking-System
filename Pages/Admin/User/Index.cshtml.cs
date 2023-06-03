using MetaX.Data;
using MetaX.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MetaX.Pages.Admin.User
{
    public class IndexModel : PageModel
    {
        private readonly MetaxDbContext _db;

        public List<Model.User> UserListing;

        [BindProperty(SupportsGet = true)]
        public string Name { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Surname { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Email { get; set; }

        [BindProperty(SupportsGet = true)]
        public string PhoneNumber { get; set; }

        public IndexModel(MetaxDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            IQueryable<Model.User> query = _db.UsersTable.AsQueryable();

            if (!string.IsNullOrEmpty(Name))
                query = query.Where(u => u.Name.Contains(Name));

            if (!string.IsNullOrEmpty(Surname))
                query = query.Where(u => u.Surname.Contains(Surname));

            if (!string.IsNullOrEmpty(Email))
                query = query.Where(u => u.Email.Contains(Email));

            if (!string.IsNullOrEmpty(PhoneNumber))
                query = query.Where(u => u.PhoneNumber.Contains(PhoneNumber));

            UserListing = query.ToList();
        }

        public IActionResult OnPostDelete(int userId)
        {
            var user = _db.UsersTable.Find(userId);
            if (user != null)
            {
                _db.UsersTable.Remove(user);
                _db.SaveChanges();
            }
            return RedirectToPage();
        }
    }
}
