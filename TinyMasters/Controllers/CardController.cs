using Microsoft.AspNetCore.Mvc;
using TinyMasters.Models.Entity;
using TinyMasters.ViewModel;

namespace TinyMasters.Controllers
{
    public class CardController : Controller
    {
        public IActionResult Index(CardModel card)
        {

            List<CardViewModel> model = new List<CardViewModel>();
            List<CardModel> cardModels = new List<CardModel>();
            cardModels.Add(card);

            foreach (var item in cardModels)
            {
                CardViewModel cardViewModel = new CardViewModel()
                {
                    Id = cardModels.Select(i => i.Id).FirstOrDefault(),
                    Name = cardModels.Select(n => n.Name).FirstOrDefault(),
                    ImageUrl = cardModels.Select(u => u.ImageUrl).FirstOrDefault(),
                    Price = cardModels.Select(p => p.Price).FirstOrDefault(),
                };
                model.Add(cardViewModel);
            }


            return View(model);
        }
    }
}
