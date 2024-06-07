using Microsoft.AspNetCore.Mvc;
using TinyMasters.Models;
using TinyMasters.Models.Entity;
using TinyMasters.ViewModel;

namespace TinyMasters.Controllers
{
    public class OrderController : Controller
    {
        private readonly DataContext _dataContext;

        public OrderController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IActionResult Order(Order order)
        {
            var db = _dataContext;
            var product = db.ProductTbl.Where(p => p.Id == order.ProductId).ToList();
            OrderViewModel viewModel = new OrderViewModel()
            {
                Name = product.FirstOrDefault().Name,
                ProductId = order.ProductId,
                PictureUrl = product.FirstOrDefault().ImageUrl,
                Price = order.Price,
                Unit = 1
            };

            //viewModel.Name = product.Select();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Order(int ProductId, int Unit)
        {
            int urunId = ProductId;
            var orders = _dataContext.OrderTlb.Where(o => o.Id == ProductId).ToList();
            var db = _dataContext;
            var product = db.ProductTbl.Where(p => p.Id == ProductId).ToList();

            Order model = new Order()
            {
                ProductId = ProductId,
                //UserId
                SubeId = product.FirstOrDefault().SubeId,
                Price = product.FirstOrDefault().Price,
                Unit = 0
            };

            Product pr = new Product();

            for (int i = 0; i < Unit; i++)
            {
                model.Unit =+ 1;
                db.OrderTlb.Add(model);
              
            }
            db.SaveChanges();

            return RedirectToAction("Index", "Home");

        }
    }
}
