using Microsoft.AspNetCore.Mvc;

namespace TinyMasters.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
