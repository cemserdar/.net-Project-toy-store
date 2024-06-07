using Microsoft.AspNetCore.Mvc;
using TinyMasters.Models.Entity;
using TinyMasters.ViewModel;

namespace TinyMasters.Controllers
{
    public class CardController : Controller
    {
        CardViewModel viewModel;
        public IActionResult Index()
        {
            CardModel card = new CardModel();
           
            return View();
        }
    }
}
