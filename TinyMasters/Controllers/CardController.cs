using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TinyMasters.Models.Entity;
using TinyMasters.ViewModel;

namespace TinyMasters.Controllers
{
    public class CardController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Cart") == null)
                return View(new List<CardViewModel>());

            var model = JsonConvert.DeserializeObject<List<CardViewModel>>(HttpContext.Session.GetString("Cart"));
            if (model == null)
                model = new List<CardViewModel>();

            return View(model);
        }
    }
}
