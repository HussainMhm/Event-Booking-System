using MetaX.Data;
using MetaX.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MetaX.Pages.User
{
    public class ProfileModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly MetaxDbContext _context;

        public ProfileModel(IHttpContextAccessor httpContextAccessor, MetaxDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        [BindProperty]
        public Model.User User { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            User = await _context.UsersTable.FindAsync(id);

            if (User != null)
            {
                User = new Model.User
                {
                    UserID = User.UserID,
                    Name = User.Name,
                    Surname = User.Surname,
                    Email = User.Email,
                    PhoneNumber = User.PhoneNumber,
                    Password = User.Password
                };
                return Page();
            }


            return RedirectToPage("/Login"); // Kullanıcı giriş yapmadıysa giriş sayfasına yönlendirilir.
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
                user.UserID = User.UserID;
                user.Name = User.Name;
                user.Surname = User.Surname;
                user.Email = User.Email;
                user.PhoneNumber = User.PhoneNumber;
                user.Password = User.Password;

                await _context.SaveChangesAsync();

                return RedirectToPage("/User/Account");
            }

            return RedirectToPage("/Login");
        }
    }
}
