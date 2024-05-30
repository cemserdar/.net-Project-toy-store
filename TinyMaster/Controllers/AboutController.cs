using Microsoft.AspNetCore.Mvc;

namespace TinyMaster.Controllers
{
    public class AboutController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
