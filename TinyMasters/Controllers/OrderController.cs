using Microsoft.AspNetCore.Mvc;
using TinyMasters.Models;
using TinyMasters.Models.Entity;
using TinyMasters.ViewModel;

namespace TinyMasters.Controllers
{
    public class OrderController : Controller
    {
        private readonly DataContext _context;

        public OrderController(DataContext dataContext)
        {
            _context = dataContext;
        }

        public IActionResult Order(Order order)
        {
            var product = _context.ProductTbl.Where(p => p.Id == order.ProductId).FirstOrDefault();
            OrderViewModel viewModel = new OrderViewModel()
            {
                Name = product.Name,
                ProductId = order.ProductId,
                PictureUrl = product.ImageUrl,
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
            //var orders = _context.OrderTlb.Where(o => o.Id == ProductId).ToList();
            var product = _context.ProductTbl.Where(p => p.Id == ProductId).FirstOrDefault();

            Order model = new Order()
            {
                ProductId = ProductId,
                //UserId
                SubeId = product.SubeId,
                Price = product.Price,
                Unit = 0
            };

            //Product pr = new Product();

            for (int i = 0; i < Unit; i++)
            {
                model.Unit =+ 1;
                _context.OrderTlb.Add(model);              
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");

        }
    }
}
