using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TinyMasters.Models;
using TinyMasters.Models.Entity;

namespace TinyMasters.Controllers
{
    public class LoginController : Controller
    {

        private readonly DataContext _dataContext;

        public LoginController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {

            user.Mail = _dataContext.UserTbl.Select(x => x.Name).FirstOrDefault();
            user.Role = _dataContext.UserTbl.Select(x => x.Role).FirstOrDefault();

            string UserRole = "User";
            string BranchRole = "Branch";
            var claims = new List<Claim>();
            if (user.Role == 1)
            {
                new Claim(ClaimTypes.Email, user.Mail);
                new Claim(ClaimTypes.Role, UserRole);

            }
            else if (user.Role == 2)
            {
                new Claim(ClaimTypes.Name, user.Mail);
                new Claim(ClaimTypes.Role, BranchRole);
            }




            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties();

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            return RedirectToAction("Index", "Home");
        }
    }
}
