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
            var reservedContext = _dataContext.ReservationTbl.Where(x => x.Product == ProductId);

            var reserved = _dataContext.ReservationTbl.Where(x => x.User == sessionUser.Id).Select(x => x.Product).FirstOrDefault();



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

            UserViewModel userViewModel = new UserViewModel();
            foreach (var item in orderContex)
            {
                userViewModel.UserName = sessionUser.Name;
                userViewModel.Unit = orderContex.Where(x => x.UserId == sessionUser.Id).Select(s => s.Unit).FirstOrDefault();
                userViewModel.ProductPrice = orderContex.Where(x => x.UserId == sessionUser.Id).Select(x => x.Price).FirstOrDefault();
                userViewModel.ProductName = productContext.Where(x => x.Id == ProductId).Select(x => x.Name).FirstOrDefault();
                userViewModel.ProductImage = productContext.Where(x => x.Id == ProductId).Select(x => x.ImageUrl).FirstOrDefault();
                userViewModel.ReservedProductImage = productContext.Where(x => x.Id == ProductId).Select(x => x.ImageUrl).FirstOrDefault();
                //userViewModel.ReservedProductName = reservedContext.Where(x => x.Id == reserved).Select(x => x.Product);
                userViewModel.ReservedProductStatu = statu;
            }
            viewModels.Add(userViewModel);
            return View(viewModels);
        }
    }
}
