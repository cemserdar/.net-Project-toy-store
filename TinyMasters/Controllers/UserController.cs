using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

//using NuGet.Protocol.VisualStudio;
using TinyMasters.Models;
using TinyMasters.Models.Entity;
using TinyMasters.ViewModel;

namespace TinyMasters.Controllers
{
    public class UserController : Controller
    {

        private readonly DataContext _dataContext;

        public UserController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IActionResult User()
        {

            List<Order> order = new List<Order>();
            List<Reservation> reservation = new List<Reservation>();
            var sessionUser = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("User"));

            var userContex = _dataContext.UserTbl.Where(x => x.Id == sessionUser.Id);
            var orderContex = _dataContext.OrderTlb.Where(x => x.UserId == sessionUser.Id);
            var productContext = _dataContext.ProductTbl;
            var ProductId = orderContex.Where(x => x.UserId == sessionUser.Id).Select(x => x.ProductId).FirstOrDefault();

            sessionUser.Adress = userContex.Select(x => x.Adress).FirstOrDefault();
            sessionUser.Name = userContex.Select(x => x.Name).FirstOrDefault();
            sessionUser.SubeId = userContex.Select(x => x.SubeId).FirstOrDefault();


            var product = productContext.Where(x => x.Id == orderContex.Where(x => x.UserId == sessionUser.Id).Select(x => x.ProductId).FirstOrDefault());

            var statu = 1;
            if (orderContex.Select(x => x.Unit).FirstOrDefault() == 0)
            {
                statu = 2;
            }

            List<UserViewModel> viewModels = new List<UserViewModel>();


            foreach (var item in orderContex)
            {
                UserViewModel userViewModel = new UserViewModel()
                {
                    UserName = sessionUser.Name,
                    Unit = item.Unit,
                    ProductPrice = item.Price,
                    ProductName = productContext.Where(x => x.Id == ProductId).Select(x => x.Name).FirstOrDefault(),
                    ProductImage = productContext.Where(x => x.Id == ProductId).Select(x => x.ImageUrl).FirstOrDefault(),
                };
                viewModels.Add(userViewModel);
            }
            return View(viewModels);
        }

    }
}

