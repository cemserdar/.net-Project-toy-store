using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
            var userRole = _dataContext.UserTbl
                .Where(x => x.Mail == user.Mail)
                .Select(x => x.Role)
                .FirstOrDefault();

            int role = userRole;
            var userId = _dataContext.UserTbl.Select(x => x.Id).FirstOrDefault();
            if (role == 2)
            {
                var subeId = _dataContext.UserTbl.Where(x => x.Mail == user.Mail).Select(x => x.SubeId).FirstOrDefault();
                user.SubeId = subeId;
                var subeName = _dataContext.SubeTbl.Where(x=>x.Id == user.SubeId).Select(x => x.Name).FirstOrDefault();
                user.Name = subeName;
            }

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
                string userJson = JsonConvert.SerializeObject(user);
                HttpContext.Session.SetString("User", userJson);
                return RedirectToAction("Index", "Home");
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
                string subeJson = JsonConvert.SerializeObject(user);
                HttpContext.Session.SetString("Sube", subeJson);
                return RedirectToAction("Index", "Sube", user);
            }

            ViewData["ValidateMessage"] = "Kullanıcı Bulunamadı";


            return View();
        }
    }
}
