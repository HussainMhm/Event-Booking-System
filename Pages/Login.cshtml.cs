using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MetaX.Model;
using MetaX.Data;


namespace MetaX.Pages
{
    public class LoginModel : PageModel
    {
        private readonly MetaxDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginModel(MetaxDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public void OnGet()
        {
        }

        public bool IsUserLoggedIn()
        {
            return !string.IsNullOrEmpty(_httpContextAccessor.HttpContext.Session.GetString("UserID"));
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _context.UsersTable.FirstOrDefaultAsync(u => u.Email == Email && u.Password == Password);

            if (user == null)
            {
                // Kullanıcı bulunamadı, giriş başarısız oldu
                return Page();
            }

            // Kullanıcıyı oturum verilerine kaydet
            _httpContextAccessor.HttpContext.Session.SetString("UserID", user.UserID.ToString());
            _httpContextAccessor.HttpContext.Session.SetString("UserName", user.Name);
            _httpContextAccessor.HttpContext.Session.SetString("UserSurname", user.Surname);
            _httpContextAccessor.HttpContext.Session.SetString("UserEmail", user.Email);
            _httpContextAccessor.HttpContext.Session.SetString("UserPhoneNumber", user.PhoneNumber);


            return RedirectToPage("/User/Account");
        }

    }

}
