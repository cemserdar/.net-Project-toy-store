using Microsoft.AspNetCore.Mvc;

namespace TinyMasters.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
