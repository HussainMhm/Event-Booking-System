using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetaX.Data;
using MetaX.Model;
using MetaX.Pages.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MetaX.Pages.User
{
	public class AccountModel : PageModel
	{
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly MetaxDbContext _context;

        public AccountModel(IHttpContextAccessor httpContextAccessor, MetaxDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        [BindProperty]
        public Model.User _User { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userID = _httpContextAccessor.HttpContext.Session.GetString("UserID");
            var userName = _httpContextAccessor.HttpContext.Session.GetString("UserName");
            var userSurname = _httpContextAccessor.HttpContext.Session.GetString("UserSurname");
            var userEmail = _httpContextAccessor.HttpContext.Session.GetString("UserEmail");
            var userPhoneNumber = _httpContextAccessor.HttpContext.Session.GetString("UserPhoneNumber");

            if (userID != null && userName != null && userSurname != null)
            {
                _User = new Model.User
                {
                    UserID = int.Parse(userID),
                    Name = userName,
                    Surname = userSurname,
                    Email = userEmail,
                    PhoneNumber = userPhoneNumber
                };

                return Page();
            }
            return RedirectToPage("/Login"); // Kullanıcı giriş yapmadıysa giriş sayfasına yönlendirilir.

        }

    }
  
}

