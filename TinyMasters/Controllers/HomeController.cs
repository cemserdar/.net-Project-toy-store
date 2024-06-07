using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TinyMasters.Models;
using TinyMasters.Models.Entity;
using TinyMasters.ViewModel;

namespace TinyMasters.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly DataContext _dataContext;

        public HomeController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IActionResult Index()
        {

            List<Product> product = new List<Product>();
            foreach (var item in _dataContext.ProductTbl)
            {
                Product urun = new Product();
                {
                    urun.Id = item.Id;
                    urun.Name = item.Name;
                    urun.Description = item.Description;
                    urun.Unit = item.Unit;
                    urun.Price = item.Price;
                    urun.ImageUrl = item.ImageUrl;

                }
                product.Add(urun);
            }

            List<HomeViewModel> homeViewModel = new List<HomeViewModel>();
            foreach (var item in product)
            {
                HomeViewModel viewModel = new HomeViewModel();
                {
                    viewModel.ProductId = item.Id;
                    viewModel.Price = item.Price;
                    viewModel.Name = item.Name;
                    viewModel.Decsription = item.Description;
                    viewModel.PictureUrl = item.ImageUrl;
                }
                homeViewModel.Add(viewModel);
            }

            return View(homeViewModel);
        }
        [HttpPost]
        public IActionResult Index(int UrunId)
        {
            var urun = _dataContext.ProductTbl.Where(u => u.Id == UrunId);



            Order order = new Order();
            {
                order.ProductId = UrunId;
                order.Price = urun.FirstOrDefault().Price;
                order.Unit = urun.FirstOrDefault().Unit;
                order.SubeId = urun.FirstOrDefault().SubeId;
                
            }
            OrderViewModel orderViewModel = new OrderViewModel()
            {
                Name = urun.First().Name,
                PictureUrl = urun.First().ImageUrl,
                Price = order.Price,
                Unit = order.Unit,
                ProductId = order.ProductId,
            };

            return RedirectToAction("Order", "Order", order);
        }



    }
}
