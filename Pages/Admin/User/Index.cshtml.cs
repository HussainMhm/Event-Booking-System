using MetaX.Data;
using MetaX.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MetaX.Pages.Admin.User
{
    public class IndexModel : PageModel
    {
        private readonly MetaxDbContext _db;

        public List<Model.User> UserListing;

        public IndexModel(MetaxDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            UserListing = _db.UsersTable.ToList();
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
