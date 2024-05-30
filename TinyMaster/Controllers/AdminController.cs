using Microsoft.AspNetCore.Mvc;

namespace TinyMaster.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
