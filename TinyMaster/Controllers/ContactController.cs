using Microsoft.AspNetCore.Mvc;

namespace TinyMaster.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
