using Microsoft.AspNetCore.Mvc;

namespace TinyMasters.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
