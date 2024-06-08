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
            var role = user.Role = _dataContext.UserTbl.Select(x => x.Role).FirstOrDefault();
            var userId = _dataContext.UserTbl.Select(x=>x.Id).FirstOrDefault();
            user.Id = userId;
            string BranchRole = "Branch";
            string UserRole = "User";
            if (role == 1)
            {
                if (user.Mail == _dataContext.UserTbl.Select(x => x.Mail).FirstOrDefault() &&
                user.Password == _dataContext.UserTbl.Select(x => x.Password).FirstOrDefault()
                )
                {
                    List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Mail),
                    new Claim(ClaimTypes.Role, UserRole)
                };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);



                    AuthenticationProperties properties = new AuthenticationProperties()
                    {
                        AllowRefresh = true,

                    };
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity), properties);
                }
                return RedirectToAction("User", "User",user);
            }
            else if (role == 2)
            {
                if (user.Mail == _dataContext.UserTbl.Select(x => x.Mail).FirstOrDefault() &&
           user.Password == _dataContext.UserTbl.Select(x => x.Password).FirstOrDefault()
           )
                {
                    List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Mail),
                    new Claim(ClaimTypes.Role, BranchRole)
                };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    AuthenticationProperties properties = new AuthenticationProperties()
                    {
                        AllowRefresh = true,

                    };
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                       new ClaimsPrincipal(claimsIdentity), properties);
                }
                return RedirectToAction("Index", "Sube",user);
            }

            ViewData["ValidateMessage"] = "Kullanıcı Bulunamadı";


            return View();
        }
    }
}
