using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TinyMasters.Models;
using TinyMasters.Models.Entity;
using TinyMasters.ViewModel;

namespace TinyMasters.Controllers
{
    public class CategoryController : Controller
    {
        private readonly DataContext _dataContext;

        public CategoryController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var categorylist = await (from c in _dataContext.CategorieTbl
                                      select new CategoryViewModel()
                                      {
                                          CategoryId = c.Id,
                                          CategoryName = c.Name,
                                          Products = (from p in _dataContext.ProductTbl
                                                      where p.CategoryId == c.Id
                                                      select new ProductViewModel
                                                      {
                                                          ProductId = p.Id,
                                                          ProductName = p.Name,
                                                          Decsription = p.Description,
                                                          PictureUrl = p.ImageUrl,
                                                          Price = p.Price
                                                      }).AsNoTracking().ToList()
                                      }).AsNoTracking().ToListAsync();


            return View(categorylist);
        }

        [HttpPost]
        public async Task<IActionResult> Index(int ProductId, string Name, Product product)
        {
            CardModel card = new CardModel();
            card.Id = ProductId;
            card.Name = Name;
            card.Price = _dataContext.ProductTbl.Where(p => p.Id == ProductId).Select(p => p.Price).FirstOrDefault();
            card.ImageUrl = _dataContext.ProductTbl.Where(p => p.Id == ProductId).Select(p => p.ImageUrl).FirstOrDefault();

            List<CardModel> cardModels = new List<CardModel>();
            cardModels.Add(card);


            return RedirectToAction("Index", "Card",cardModels);
        }
    }
}
