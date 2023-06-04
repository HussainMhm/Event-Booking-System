using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MetaX.Pages.User
{
	public class ProfileModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProfileModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public UserModel User { get; set; }

        public void OnGet()
        {
            var userID = _httpContextAccessor.HttpContext.Session.GetString("UserID");
            var userName = _httpContextAccessor.HttpContext.Session.GetString("UserName");
            var userSurname = _httpContextAccessor.HttpContext.Session.GetString("UserSurname");
            var userEmail = _httpContextAccessor.HttpContext.Session.GetString("UserEmail");
            var userPhoneNumber = _httpContextAccessor.HttpContext.Session.GetString("UserPhoneNumber");

            if (userID != null && userName != null && userSurname != null)
            {
                User = new UserModel
                {
                    UserID = int.Parse(userID),
                    Name = userName,
                    Surname = userSurname,
                    Email = userEmail,
                    PhoneNumber = userPhoneNumber
                };
            }
        }
    }

    public class UserModel
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}