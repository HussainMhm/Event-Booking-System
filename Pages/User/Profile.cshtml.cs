using MetaX.Data;
using MetaX.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MetaX.Pages.User
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        private readonly MetaxDbContext _context;

        public ProfileModel(MetaxDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Model.User User { get; set; }

        [Authorize]
        public async Task<IActionResult> OnGetAsync(int id)
        {
            User = await _context.UsersTable.FindAsync(id);

            //if (User != null)
            //{
                return Page();
            //}

            //return RedirectToPage("/Login"); // Redirect to login page if user is not found
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _context.UsersTable.FindAsync(User.UserID);

            if (user != null)
            {
                user.Name = User.Name;
                user.Surname = User.Surname;
                user.Email = User.Email;
                user.PhoneNumber = User.PhoneNumber;
                user.Password = User.Password;

                await _context.SaveChangesAsync();

                TempData["UpdateSuccess"] = true; // Store a flag in TempData

                return RedirectToPage("/User/Index");
            }

            return Page(); // Return the page itself without redirection
        }
    }
}
