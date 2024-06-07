using Microsoft.AspNetCore.Mvc;

namespace TinyMasters.Controllers
{
    public class SubeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
