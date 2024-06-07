using Microsoft.AspNetCore.Mvc;

namespace TinyMasters.Controllers
{
    public class UserController : Controller
    {
        public IActionResult User()
        {
            return View();
        }
    }
}
